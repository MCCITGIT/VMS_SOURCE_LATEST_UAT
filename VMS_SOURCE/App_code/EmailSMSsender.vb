Imports Microsoft.VisualBasic
Imports System.Net
'Imports System.Web.Mail
Imports System.Net.Mail
Imports System.IO
Imports System.Collections.Generic
Imports Constant
Imports System.Data
Imports Newtonsoft.Json
'Namespace PROTECTON.Web
Public Class EmailSMSsender
    Private Class Settings
        ' ''for Gmail
        ' ''**************************
        'Public Const SMTP_HOST As String = "smtp.gmail.com"
        'Public Const SMTP_PORT As Integer = 587
        ' ''**************************

        ''for Gmail
        ''**************************
        'Public Const SMTP_HOST As String = "nmra.dataone.in"
        'Public Const SMTP_PORT As Integer = 25
        ''**************************

        'for YahooMail
        '**************************
        'Public Const SMTP_HOST As String = "smtp.mail.yahoo.com"
        'Public Const SMTP_PORT As Integer = 995
        '**************************

        ''for HOTMail
        ''**************************
        'Public Const SMTP_HOST As String = "smtp.live.com"
        'Public Const SMTP_PORT As Integer = 587
        '**************************

        'Public Const SMTP_HOST As String = ""
        'Public Const SMTP_PORT As Integer

        'Public Const MAIL_ID As String = "mazumdar.rohan@gmail.com"
        'Public Const MAIL_PSW As String = ""


        Public SMTP_HOST As String = ConfigurationManager.AppSettings.Get("SMTP_HOST")
        Public SMTP_PORT As Integer = CType(ConfigurationManager.AppSettings.Get("SMTP_PORT"), Integer)
        Public EmailId As String = ConfigurationManager.AppSettings.Get("EMAIL_ADDRESS")
        Public Password As String = ConfigurationManager.AppSettings.Get("EMAIL_PSW")


        Public Const SMS_ID As String = ""
        Public Const SMS_USER As String = "mcc4it"
        Public Const SMS_PSW As String = "mcc999"
        Public Const SMS_PREFIX As String = "@smscountry.net"

    End Class

    'Public Function get_SMTPHOST() As String
    '    Return Settings.SMTP_HOST
    'End Function

    'Public Function get_SMTPPORT() As String
    '    Return Settings.SMTP_PORT
    'End Function

    'Public Function get_MailId() As String
    '    Return Settings.MAIL_ID
    'End Function

    'Public Function get_MailPassword() As String
    '    Return Settings.MAIL_PSW
    'End Function


    Public Function get_SMSID() As String
        Return Settings.SMS_ID
    End Function
    Public Function get_SMSUser() As String
        Return Settings.SMS_USER
    End Function
    Public Function get_SMSPassword() As String
        Return Settings.SMS_PSW
    End Function
    Public Function get_SMSPrefix() As String
        Return Settings.SMS_PREFIX
    End Function

#Region "Address Resolving"
    Public Function Address_resolve(ByVal address As String) As String()
        Dim addresscollection(100) As String
        Try

            Dim email_add As String = String.Empty
            Dim startIndex As Integer = 1
            Dim endIndex As Integer
            Dim add_len As Integer = Len(address)
            Dim copyaddress As String = address

            For i As Integer = 0 To Len(copyaddress)
                endIndex = InStr(copyaddress, ";", CompareMethod.Binary)

                If endIndex = 0 Then
                    addresscollection(i) = (Trim(Mid(copyaddress, startIndex, Len(copyaddress))))
                    Exit For
                Else
                    addresscollection(i) = (Trim(Mid(copyaddress, startIndex, endIndex - 1)))
                End If

                copyaddress = Mid(copyaddress, endIndex + 1)
            Next
        Catch ex As Exception
            addresscollection = Nothing
        End Try
        Return addresscollection
    End Function
#End Region

#Region "MOBILE No Resolving"
    Public Function Mobile_resolve(ByVal address As String) As String()
        Dim nocollection(100) As String

        Try

            Dim email_add As String = String.Empty
            Dim startIndex As Integer = 1
            Dim endIndex As Integer
            Dim add_len As Integer = Len(address)
            Dim copyaddress As String = address

            For i As Integer = 0 To Len(copyaddress)
                endIndex = InStr(copyaddress, ";", CompareMethod.Binary)

                If endIndex = 0 Then
                    nocollection(i) = (Trim(Mid(copyaddress, startIndex, Len(copyaddress))))
                    Exit For
                Else
                    nocollection(i) = (Trim(Mid(copyaddress, startIndex, endIndex - 1)))
                End If

                copyaddress = Mid(copyaddress, endIndex + 1)
            Next
        Catch ex As Exception
            nocollection = Nothing
        End Try
        Return nocollection
    End Function
#End Region

#Region "Making sms No to mailaddress"
    Function Sms_sender(ByVal address() As String, ByVal Message As String) As String
        Dim formatingString As String
        Try


            For i As Integer = 0 To address.Length - 1
                Dim no As String = address(i)
                If no <> String.Empty Or no <> Nothing Then
                    formatingString = "91" + no
                    SendSMS(formatingString, Message)

                End If

            Next
        Catch ex As Exception
            Throw ex

        End Try
        Return "done"
    End Function
#End Region

#Region "SendSMS"
    Function SendSMS(ByVal Mobile_Number As String, ByVal Message As String, Optional ByVal SID As String = "SMSCntry", Optional ByVal MType As String = "N", Optional ByVal DR As String = "N") As String
        Dim stringpost As System.Object = "User=" & get_SMSUser() & "&passwd=" & get_SMSPassword() & "&mobilenumber=" & Mobile_Number & "&message=" & Message & "&SID=" & SID & "&MTYPE=" & MType & "&DR=" & DR
        Dim functionReturnValue As String = Nothing
        functionReturnValue = ""
        Dim objWebRequest As HttpWebRequest = Nothing
        Dim objWebResponse As HttpWebResponse = Nothing
        Dim objStreamWriter As StreamWriter = Nothing
        Dim objStreamReader As StreamReader = Nothing
        Dim objProxy1 As WebProxy = Nothing
        Try
            Dim stringResult As String = Nothing
            objWebRequest = DirectCast(WebRequest.Create("http://www.smscountry.com/SMSCwebservice.asp?"), HttpWebRequest)
            objWebRequest.Method = "POST"
            ''If (objProxy1 IsNot Nothing) Then
            ''    objWebRequest.Proxy = objProxy1
            ''End If
            objWebRequest.Proxy = objProxy1
            ' Use below code if you want to SETUP PROXY. 
            'Parameters to pass: 1. ProxyAddress 2. Port 
            'You can find both the parameters in Connection settings of your internet explorer.
            'Dim myProxy As New WebProxy("YOUR PROXY", PROXPORT)
            'myProxy.BypassProxyOnLocal = True
            'wrGETURL.Proxy = myProxy
            objWebRequest.ContentType = "application/x-www-form-urlencoded"
            objStreamWriter = New StreamWriter(objWebRequest.GetRequestStream())
            objStreamWriter.Write(stringpost)
            objStreamWriter.Flush()
            objStreamWriter.Close()
            objWebResponse = DirectCast(objWebRequest.GetResponse(), HttpWebResponse)
            objStreamReader = New StreamReader(objWebResponse.GetResponseStream())
            stringResult = objStreamReader.ReadToEnd()
            objStreamReader.Close()
            Return (stringResult)
        Catch ex As Exception
            Throw ex
        Finally
            ''If (objStreamWriter IsNot Nothing) Then
            ''    objStreamWriter.Close()
            ''End If
            objStreamWriter.Close()
            ''If (objStreamReader IsNot Nothing) Then
            ''    objStreamReader.Close()
            ''End If
            objStreamReader.Close()
            objWebRequest = Nothing
            objWebResponse = Nothing
            objProxy1 = Nothing
        End Try
    End Function
#End Region

#Region "Send Email"
    'Public Function sendMail(ByVal Mailodj As MailEntity, ByVal mail As System.Net.Mail.MailMessage) As String
    '    Dim sendingreport As String

    '    Try


    '        Dim mailaddress(100) As String
    '        mailaddress = Address_resolve(Mailodj.FromAddress)
    '        mail.From = New MailAddress(mailaddress(0))

    '        mailaddress = Address_resolve(Mailodj.ToAddress)
    '        'mail.To = Mailodj.ToAddress
    '        For i As Integer = 0 To mailaddress.Length - 1
    '            If mailaddress(i) <> String.Empty Or mailaddress(i) <> Nothing Then
    '                mail.To.Add(New MailAddress(mailaddress(i)))
    '            End If

    '        Next

    '        mailaddress = Address_resolve(Mailodj.CCAddress)
    '        'mail.Cc = Mailodj.CCAddress
    '        For i As Integer = 0 To mailaddress.Length - 1
    '            If mailaddress(i) <> String.Empty Or mailaddress(i) <> Nothing Then
    '                mail.CC.Add(New System.Net.Mail.MailAddress(mailaddress(i)))
    '            End If
    '        Next

    '        mailaddress = Address_resolve(Mailodj.BCCAddress)
    '        'mail.Bcc = Mailodj.BCCAddress
    '        For i As Integer = 0 To mailaddress.Length - 1
    '            If mailaddress(i) <> String.Empty Or mailaddress(i) <> Nothing Then
    '                mail.Bcc.Add(New System.Net.Mail.MailAddress(mailaddress(i)))
    '            End If
    '        Next

    '        mail.Subject = Mailodj.MailSubject
    '        mail.BodyEncoding = System.Text.Encoding.UTF8
    '        mail.Body = Mailodj.MailBody

    '        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        'Sending SMTP configuration for main server
    '        'Dim smtpmail As SmtpClient = New SmtpClient()
    '        'smtpmail.Credentials = New Net.NetworkCredential("mcc4it", "xyz123")
    '        'smtpmail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
    '        'smtpmail.Send(mail)

    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        'Sending SMTP configuration for testing purpose using gmail or any other smtp service
    '        Dim smtpmail As SmtpClient = New SmtpClient("smtp.gmail.com", 587)
    '        smtpmail.Credentials = New NetworkCredential("mazumdar.rohan@gmail.com", "raptophanuman")
    '        smtpmail.EnableSsl = True
    '        smtpmail.Send(mail)
    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



    '        ' ****** Test Code From Google -- start
    '        'Dim serv = New SmtpClient
    '        'Dim msgMail = New MailMessage
    '        'msgMail.To.Add("mazumdar.rohan@gmail.com")
    '        'msgMail.Body = "body"
    '        'msgMail.Subject = "subj"
    '        'msgMail.BodyEncoding = System.Text.Encoding.ASCII
    '        'msgMail.IsBodyHtml = False
    '        'serv.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
    '        'serv.Credentials = New NetworkCredential("mcc4it", "xyz123")
    '        'serv.Send(msgMail)
    '        ' ****** Test Code From Google -- end

    '    Catch ex As Exception
    '        sendingreport = "Failed"
    '        Return sendingreport
    '        Throw ex
    '    Finally
    '        mail.Dispose()
    '    End Try
    '    sendingreport = "Done"
    '    Return sendingreport
    'End Function
#End Region


    'Public Function sendEMail(ByVal to_email As String, ByVal cc_email As String, ByVal attachment_path As String, ByVal subject As String, ByVal body As String) As String

    '    Dim mail As MailMessage = New MailMessage()
    '    Dim result As String = String.Empty

    '    Try

    '        mail.From = New MailAddress("mailservice@bergerapps.in", "BERGER VMS PORTAL SERVICE")

    '        Dim toEMail As String()
    '        toEMail = to_email.Split(",")
    '        For Each email As String In toEMail
    '            If Not (email.Trim().Equals(String.Empty)) Then
    '                mail.To.Add(New MailAddress(email))
    '            End If
    '        Next

    '        Dim ccEMail As String()

    '        If Not (cc_email.Equals(String.Empty)) Then
    '            ccEMail = cc_email.Split(",")
    '            For Each email As String In ccEMail
    '                If Not (email.Trim().Equals(String.Empty)) Then
    '                    mail.CC.Add(New MailAddress(email))
    '                End If
    '            Next
    '        End If

    '        'mail.CC.Add(New MailAddress("joydeepmajumdar@bergerindia.com"))
    '        'mail.CC.Add(New MailAddress("sandeep_dey@hotmail.com"))
    '        'mail.CC.Add(New MailAddress("mail.bergertaxation@gmail.com"))
    '        mail.CC.Add(New MailAddress("bmsamanta@gmail.com"))

    '        mail.Subject = subject
    '        mail.BodyEncoding = System.Text.Encoding.UTF8
    '        mail.Body = body

    '        If Not (attachment_path.Equals(String.Empty)) Then
    '            mail.Attachments.Add(New System.Net.Mail.Attachment(attachment_path))
    '        End If

    '        Dim serv = New SmtpClient()
    '        serv.DeliveryMethod = SmtpDeliveryMethod.Network
    '        serv.Host = Constant.Common.SMTP_HOST
    '        serv.Port = Constant.Common.SMTP_PORT
    '        serv.Credentials = New System.Net.NetworkCredential(Constant.Common.MAIL_NETWORK_CREDENTIAL_USERNAME, Constant.Common.MAIL_NETWORK_CREDENTIAL_PASSWORD)

    '        serv.Send(mail)

    '        result = "Mail sent successfully."

    '    Catch ex As Exception
    '        result = ex.Message
    '    End Try

    '    Return result

    'End Function

    'Public Function sendMailHTML(ByVal Mailodj As MailEntity) As String
    '    Dim sendingreport As String
    '    Dim Setting As New Settings()
    '    Dim CommonObj As New Common
    '    Dim ds As New DataSet

    '    Dim mail As New System.Net.Mail.MailMessage
    '    Try
    '        Dim mailaddress(100) As String
    '        mail.From = New MailAddress(Setting.EmailId, "VMS")
    '        '----------
    '        Dim mailTo As String()
    '        mailTo = Mailodj.ToAddress.Split(",")

    '        For i = 0 To mailTo.Length - 1
    '            If mailTo(i).Trim <> String.Empty Then
    '                mail.To.Add(New MailAddress(mailTo(i)))
    '            End If
    '        Next


    '        If Mailodj.CCAddress.Length > 0 Then
    '            Dim engCC As String()
    '            engCC = Mailodj.CCAddress.Split(",")
    '            If Not engCC Is Nothing Then
    '                For i = 0 To engCC.Length - 1
    '                    If engCC(i).Trim <> String.Empty Then
    '                        mail.CC.Add(New MailAddress(engCC(i)))
    '                    End If
    '                Next
    '            End If
    '        End If
    '        If Mailodj.BCCAddress.Length > 0 Then
    '            Dim engBCC As String()
    '            engBCC = Mailodj.BCCAddress.Split(",")
    '            If Not engBCC Is Nothing Then
    '                For i = 0 To engBCC.Length - 1
    '                    If engBCC(i).Trim <> String.Empty Then
    '                        mail.Bcc.Add(New MailAddress(engBCC(i)))
    '                    End If
    '                Next
    '            End If
    '        End If

    '        mail.Subject = Mailodj.MailSubject
    '        mail.BodyEncoding = System.Text.Encoding.UTF8
    '        mail.Body = Mailodj.MailBody

    '        mail.IsBodyHtml = True
    '        'If Mailodj.Attachment_Path <> String.Empty Then
    '        '    Dim atchPath As String()
    '        '    atchPath = Mailodj.Attachment_Path.Split(";")
    '        '    If Not atchPath Is Nothing Then
    '        '        For i = 0 To atchPath.Length - 1
    '        '            If atchPath(i).Trim <> String.Empty Then
    '        '                mail.Attachments.Add(New System.Net.Mail.Attachment(atchPath(i).Trim))
    '        '            End If
    '        '        Next
    '        '    End If
    '        'End If

    '        Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(Mailodj.MailBody, Nothing, "text/html")
    '        mail.AlternateViews.Add(htmlView)

    '        Dim serv = New SmtpClient
    '        serv.DeliveryMethod = SmtpDeliveryMethod.Network
    '        serv.Host = Setting.SMTP_HOST
    '        serv.Port = Setting.SMTP_PORT
    '        serv.Credentials = New NetworkCredential(Setting.EmailId.ToString, Setting.Password.ToString)
    '        serv.EnableSsl = False
    '        serv.Send(mail)
    '    Catch ex As Exception
    '        sendingreport = "Email Sent Failed"
    '        Return sendingreport
    '        Throw ex
    '    Finally
    '        mail = Nothing
    '    End Try
    '    sendingreport = "Email Sent Successfully"
    '    Return sendingreport
    'End Function
    Public Function sendMail(ByVal Obj As MailEntity) As Integer
        Dim returnData As Integer = 0

        Try

            Dim mailEntityObj As MailEntityNew = New MailEntityNew() With {
            .mailFromAddress = Obj.FromAddress,
            .mailToAddress = Obj.ToAddress,
            .mailCCAddress = Obj.CCAddress,
            .mailBCCAddress = Obj.BCCAddress,
            .mailSubject = Obj.MailSubject,
            .mailBody = Obj.MailBody,
            .mailAttachement = Obj.Attachment_Path,
            .mailSenderApp = Obj.Sender_App,
            .mailSenderTask = Obj.Sender_Task
            }
            mailEntityObj.mailAttachement = (If(mailEntityObj.mailAttachement Is Nothing, "", mailEntityObj.mailAttachement))
            Dim postData As String = JsonConvert.SerializeObject(mailEntityObj)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim httpWReq As HttpWebRequest = CType(WebRequest.Create("https://bpilweb.bergerindia.com/mccapis/email/v1/send"), HttpWebRequest)
            'Dim httpWReq As HttpWebRequest = CType(WebRequest.Create("http://localhost:1951/email/v1/send"), HttpWebRequest)
            httpWReq.Accept = "application/json"
            httpWReq.Method = "POST"
            httpWReq.ContentType = "application/json"
            httpWReq.Headers.Add("Authorization", "Basic " & ConfigurationManager.AppSettings("MCCWebAPIAuthToken").ToString())

            Dim encoding = New UTF8Encoding()
            Dim data = encoding.GetBytes(postData)
            httpWReq.ContentLength = data.Length

            Using stream = httpWReq.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Dim httpResponse As HttpWebResponse = CType(httpWReq.GetResponse(), HttpWebResponse)
            Dim responseJson As String = New StreamReader(httpResponse.GetResponseStream()).ReadToEnd()
            Dim resobj = Newtonsoft.Json.JsonConvert.DeserializeObject(Of MailResponse)(responseJson)
            returnData = resobj.responseCode
        Catch ex As Exception
            Dim exMsg = ex.Message
            returnData = 0
        End Try

        Return returnData
    End Function
End Class
'End Namespace
