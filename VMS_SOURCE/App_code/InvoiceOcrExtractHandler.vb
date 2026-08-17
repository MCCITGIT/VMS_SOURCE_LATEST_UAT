Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Web.SessionState

Public Class InvoiceExtractResponse
    Public Property success As Boolean
    Public Property message As String
    Public Property invoice_no As String
    Public Property invoice_date As String
    Public Property amount As Decimal?
End Class

Public Class InvoiceOcrExtractHandler
    Implements IHttpHandler
    Implements IRequiresSessionState

    Private Const DefaultInvoiceOcrApiUrl As String = "https://bpilmobileuat.bergerindia.com/COLORANT_OCR/api/extract-invoice"
    Private Const SessionInvoiceOcrTempFile As String = "InvoiceOcrTempFile"
    Private Const SessionInvoiceOcrFileName As String = "InvoiceOcrFileName"

    Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/json"
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Try
            'If context.Session Is Nothing OrElse context.Session(Constant.SessionKeys.UserInfo) Is Nothing Then
            '    WriteJson(context, False, "Session expired. Please login again.", Nothing)
            '    context.Response.StatusCode = 401
            '    Return
            'End If

            If Not String.Equals(context.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) Then
                WriteJson(context, False, "Only POST is allowed.", Nothing)
                context.Response.StatusCode = 405
                Return
            End If

            If context.Request.Files Is Nothing OrElse context.Request.Files.Count = 0 Then
                WriteJson(context, False, "Please select an invoice PDF file.", Nothing)
                context.Response.StatusCode = 400
                Return
            End If

            Dim uploaded = context.Request.Files(0)
            If uploaded Is Nothing OrElse uploaded.ContentLength <= 0 Then
                WriteJson(context, False, "Uploaded file is empty.", Nothing)
                context.Response.StatusCode = 400
                Return
            End If

            Dim fileName As String = Path.GetFileName(If(uploaded.FileName, "invoice.pdf"))
            If Not String.Equals(Path.GetExtension(fileName), ".pdf", StringComparison.OrdinalIgnoreCase) Then
                WriteJson(context, False, "Please upload a PDF invoice file.", Nothing)
                context.Response.StatusCode = 400
                Return
            End If

            Dim contentType As String = ResolveContentType(fileName, uploaded.ContentType)
            Dim fileBytes As Byte()

            Using ms As New MemoryStream()
                uploaded.InputStream.CopyTo(ms)
                fileBytes = ms.ToArray()
            End Using

            Dim apiResponseJson As String = PostMultipartToOcrApi(DefaultInvoiceOcrApiUrl, GetOcrApiKey(), fileName, contentType, fileBytes)
            Dim ocrResult As InvoiceExtractResponse = DeserializeInvoiceResponse(apiResponseJson)

            If ocrResult Is Nothing OrElse Not ocrResult.success Then
                WriteJson(context, False, If(ocrResult IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(ocrResult.message), ocrResult.message, "Unable to extract invoice details."), ocrResult)
                context.Response.StatusCode = 422
                Return
            End If

            SaveInvoiceFileToSession(context, fileBytes, fileName)
            context.Response.Write(apiResponseJson)
        Catch ex As WebException
            Dim errBody As String = String.Empty
            If ex.Response IsNot Nothing Then
                Using respStream = ex.Response.GetResponseStream()
                    If respStream IsNot Nothing Then
                        Using reader As New StreamReader(respStream)
                            errBody = reader.ReadToEnd()
                        End Using
                    End If
                End Using
            End If

            If Not String.IsNullOrWhiteSpace(errBody) Then
                context.Response.StatusCode = 502
                context.Response.Write(errBody)
            Else
                WriteJson(context, False, "Invoice OCR API call failed: " & ex.Message, Nothing)
                context.Response.StatusCode = 502
            End If
        Catch ex As Exception
            WriteJson(context, False, "Invoice OCR processing failed: " & ex.Message, Nothing)
            context.Response.StatusCode = 500
        End Try
    End Sub

    Private Shared Function GetOcrApiKey() As String
        Dim apiKey As String = ConfigurationManager.AppSettings("ColorantInvoiceOcrApiKey")
        Return If(apiKey, String.Empty).Trim()
    End Function

    Private Shared Function PostMultipartToOcrApi(apiUrl As String, apiKey As String, fileName As String, contentType As String, fileBytes As Byte()) As String
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls

        Dim boundary As String = "----InvoiceOcrBoundary" & DateTime.UtcNow.Ticks.ToString("x")
        Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
        request.Method = "POST"
        request.Accept = "application/json"
        request.ContentType = "multipart/form-data; boundary=" & boundary
        request.Timeout = 120000
        request.ReadWriteTimeout = 120000

        If Not String.IsNullOrWhiteSpace(apiKey) Then
            request.Headers.Add("X-API-Key", apiKey.Trim())
        End If

        Dim preamble As String =
            "--" & boundary & vbCrLf &
            "Content-Disposition: form-data; name=""file""; filename=""" & fileName & """" & vbCrLf &
            "Content-Type: " & contentType & vbCrLf & vbCrLf

        Dim epilogue As String = vbCrLf & "--" & boundary & "--" & vbCrLf

        Dim preambleBytes As Byte() = Encoding.UTF8.GetBytes(preamble)
        Dim epilogueBytes As Byte() = Encoding.UTF8.GetBytes(epilogue)
        request.ContentLength = preambleBytes.Length + fileBytes.Length + epilogueBytes.Length

        Using reqStream = request.GetRequestStream()
            reqStream.Write(preambleBytes, 0, preambleBytes.Length)
            reqStream.Write(fileBytes, 0, fileBytes.Length)
            reqStream.Write(epilogueBytes, 0, epilogueBytes.Length)
        End Using

        Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using respStream = response.GetResponseStream()
                Using reader As New StreamReader(respStream)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Using
    End Function

    Private Shared Function DeserializeInvoiceResponse(responseJson As String) As InvoiceExtractResponse
        If String.IsNullOrWhiteSpace(responseJson) Then
            Return Nothing
        End If

        Dim serializer As New JavaScriptSerializer()
        Return serializer.Deserialize(Of InvoiceExtractResponse)(responseJson)
    End Function

    Private Shared Function ResolveContentType(fileName As String, uploadedContentType As String) As String
        If Not String.IsNullOrWhiteSpace(uploadedContentType) AndAlso Not String.Equals(uploadedContentType, "application/octet-stream", StringComparison.OrdinalIgnoreCase) Then
            Return uploadedContentType
        End If

        If String.Equals(Path.GetExtension(fileName), ".pdf", StringComparison.OrdinalIgnoreCase) Then
            Return "application/pdf"
        End If

        Return "application/octet-stream"
    End Function

    Private Shared Sub SaveInvoiceFileToSession(context As HttpContext, fileBytes As Byte(), fileName As String)
        ClearSessionInvoiceFile(context)

        Dim tempFilePath As String = Path.Combine(Path.GetTempPath(), "VMS_Invoice_" & context.Session.SessionID & Path.GetExtension(fileName))
        File.WriteAllBytes(tempFilePath, fileBytes)

        context.Session(SessionInvoiceOcrTempFile) = tempFilePath
        context.Session(SessionInvoiceOcrFileName) = fileName
    End Sub

    Private Shared Sub ClearSessionInvoiceFile(context As HttpContext)
        If context.Session(SessionInvoiceOcrTempFile) IsNot Nothing Then
            Dim tempFilePath As String = Convert.ToString(context.Session(SessionInvoiceOcrTempFile))
            If Not String.IsNullOrEmpty(tempFilePath) AndAlso File.Exists(tempFilePath) Then
                Try
                    File.Delete(tempFilePath)
                Catch
                End Try
            End If
        End If

        context.Session.Remove(SessionInvoiceOcrTempFile)
        context.Session.Remove(SessionInvoiceOcrFileName)
    End Sub

    Private Shared Sub WriteJson(context As HttpContext, success As Boolean, message As String, ocrResult As InvoiceExtractResponse)
        Dim payload As New Dictionary(Of String, Object) From {
            {"success", success},
            {"message", message}
        }

        If ocrResult IsNot Nothing Then
            payload("invoice_no") = If(ocrResult.invoice_no, String.Empty)
            payload("invoice_date") = If(ocrResult.invoice_date, String.Empty)
            payload("amount") = ocrResult.amount
        End If

        Dim serializer As New JavaScriptSerializer()
        context.Response.Write(serializer.Serialize(payload))
    End Sub
End Class