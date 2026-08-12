
'****************************************************************************************
'Copyright	    : VMS, Edify India, Chennai
'Source	        : FileCiew.aspx.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.01.00
'Description	: Code behind file for Document Download and view
'
' Modified By       Modified On           Version               Reason
'****************************************************************************************
Imports VMS.Web
Partial Class FileView
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim downloadFileName As String = String.Empty

        downloadFileName = Request.QueryString("url")

        If Not Request.QueryString(Constant.SessionKeys.File_Type) Is Nothing Then
            Dim type As String = Request.QueryString(Constant.SessionKeys.File_Type)
            FileIssueDownload(downloadFileName, type)
        End If


    End Sub


#Region "File Download and display"

    Private Sub FileIssueDownload(ByVal downloadFileName As String, ByVal type As String)

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If


        Try

            Dim fileNamePath As String
            Select Case type
                Case "Issue"
                    fileNamePath = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & userInfo.userCompanyEntity & "\" & "Issue Docs" & "\" & downloadFileName
                Case "Booking"
                    fileNamePath = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & userInfo.userCompanyEntity & "\" & "Machine Booking Docs" & "\" & downloadFileName
            End Select
            'get file object as FileInfo  
            Dim fileExtention As String = String.Empty
            ' Set the Response Content Type based on the file extension
            fileExtention = GetFileExtension(fileNamePath)
            Dim contentType As String = String.Empty
            Select Case fileExtention.ToUpper()
                Case Constant.FileExtension.DOC
                    contentType = Constant.ContentType.Word
                Case Constant.FileExtension.DOCX
                    contentType = Constant.ContentType.Word
                Case Constant.FileExtension.PDF
                    contentType = Constant.ContentType.PDF
                Case Constant.FileExtension.TXT
                    contentType = Constant.ContentType.Text
                Case Constant.FileExtension.XLS
                    contentType = Constant.ContentType.Excel
                Case Constant.FileExtension.XLSX
                    contentType = Constant.ContentType.Excel
                Case Constant.FileExtension.JPG
                    contentType = Constant.ContentType.JPEG
                Case Constant.FileExtension.JPEG
                    contentType = Constant.ContentType.JPEG
                Case Constant.FileExtension.GIF
                    contentType = Constant.ContentType.GIF

            End Select

            Dim file As System.IO.FileInfo = New System.IO.FileInfo(fileNamePath) '-- if the file exists on the server  
            If file.Exists Then 'set appropriate headers  
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = contentType
                Response.WriteFile(file.FullName)
                'Response.End() 'if file does not exist  

            Else
                Dim returnUrl As String = "~/ExceptionPage.aspx"
                Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.Filenotfound
                Server.Transfer(returnUrl)

            End If 'nothing in the URL as HTTP GET  
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorFileDownloadDisplay
            Server.Transfer(returnUrl)
        End Try
    End Sub

#End Region

#Region "Get File Extension"

    ' Gets the File extension from the file Name
    Private Function GetFileExtension(ByVal fileName As String) As String
        Dim extension As String = String.Empty
        If (fileName.LastIndexOf(".") >= 0) Then
            extension = fileName.Substring(fileName.LastIndexOf(".") + 1)
        End If

        Return extension
    End Function

#End Region

End Class
