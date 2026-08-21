'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Change_Password.aspx.vb
'Created Date	: 23-November-2007
'Created By	    : Arun 
'Version	    : R02.00.00
'Description	: Code behind for Change Password Page

'Modified By       Modified On       Version         Reason
'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source (Cancel button and Status-driven redirect)

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class Change_Password
    Inherits System.Web.UI.Page

#Region "Page Load Event Handler"
    'Page load event handler occurs at the time of page and page post back
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If the page post occurs the page the condition does not validate for true
        If (Not IsPostBack) Then

            Dim userInfo As VMSUserEntity = New VMSUserEntity()
            If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
                userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
            End If

            'Changes By Sumeet 26-02-2015 (Start)
            If (Not (Request.QueryString("Status") Is Nothing)) Then

                Dim UserStatus As String = Request.QueryString("Status")
                'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source
                hdnStatus.Value = Request.QueryString("Status")

                If (UserStatus = "FirstTimeEntry") Then

                ElseIf (UserStatus = "PasswordExpired") Then
                    lblPwdErrMsg.Text = "Your password had expired . Please Change the password ."
                End If

            End If

            'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source
            hdnOldPwd.Value = userInfo.userPWDEntity.ToString
            'hdnOldPwd.Value = Session("PWD")
            AddAttributes()
            txtOldPwd.Focus()
        End If

    End Sub

#End Region

#Region "AddAttributes"
    Private Sub AddAttributes()

        btnSubmit.Attributes.Add("OnClick", "return ValidateChangePwd()")
        'txtConPwd.Attributes.Add("Onblur", "return fnPwdDetails(this.value)")

    End Sub
#End Region


#Region "btnSubmit_Click"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim ChngPwd As New ChangePassword()
        Dim Rowsaffected As Integer
        Dim Pwd As String = ""
        Pwd = txtConPwd.Text

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        End If

        'Rowsaffected = ChngPwd.UpdatePassword(Session("COMPANY"), Session("UID"), Pwd)
        Rowsaffected = ChngPwd.UpdatePassword(userInfo.userCompanyEntity.ToString, userInfo.userIDEntity.ToString, txtNewPwd.Text.Trim(), txtOldPwd.Text.Trim())
        If (Rowsaffected = 0) Then
            'lblConfirmMsg.Visible = True
            Dim alertmessage As String
            alertmessage = "Password Changed Successfully"
            Me.CreateMessageAlert(Me, alertmessage, "alertKey")
            'Response.Redirect("~/ChangePasswordAutoLogout.aspx")
        ElseIf (Rowsaffected = 1) Then
            lblErrMsg.Text = "Old Password entered is incorrect! Password not updated successfully."
            lblErrMsg.ForeColor = Drawing.Color.Red
        ElseIf (Rowsaffected = 2) Then
            lblErrMsg.Text = "you have used this password before.Please enter a new password."
            lblErrMsg.ForeColor = Drawing.Color.Red

        End If
    End Sub
#End Region

#Region "CreateMessageAlert"
    Public Sub CreateMessageAlert(ByVal senderpage As System.Web.UI.Page, ByVal alertMsg As String, ByVal alertKey As String)
        Dim strScript As String
        strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>"
        If Not (ClientScript.IsStartupScriptRegistered(alertKey)) Then
            ClientScript.RegisterStartupScript(Me.GetType(), alertKey, strScript)
        End If
        Response.Redirect("~/Login.aspx")
    End Sub
#End Region


#Region "btnCancel_Click"
    'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If (hdnStatus.Value.Equals("FirstTimeEntry")) Then
            Response.Redirect("~/Login.aspx")
        ElseIf (hdnStatus.Value.Equals("PasswordExpired")) Then
            Response.Redirect("~/Login.aspx")
        Else
            Response.Redirect("~/Home.aspx")
        End If
    End Sub
#End Region

End Class
