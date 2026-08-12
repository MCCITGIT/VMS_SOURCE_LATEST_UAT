'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Change_Password.aspx.vb
'Created Date	: 16-January-2007
'Created By	    : Arun 
'Version	    : R02.00.00
'Description	: Code behind for Change Password Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class ChangePasswordLink
    Inherits System.Web.UI.Page


#Region "Page Load Event Handler"
    'Page load event handler occurs at the time of page and page post back
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If the page post occurs the page the condition does not validate for true
        If (Not IsPostBack) Then

            'Dim userInfo As VMSUserEntity = New VMSUserEntity()
            'If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            '    userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
            'End If

            'hdnOldPwd.Value = userInfo.userPWDEntity.ToString
            'hdnOldPwd.Value = Session("PWD")
            AddAttributes()
        End If

    End Sub

#End Region

#Region "AddAttributes"
    Private Sub AddAttributes()

        btnSubmit.Attributes.Add("OnClick", "return ValidateChangePwdLink()")
        'txtConPwd.Attributes.Add("Onblur", "return fnPwdLinkDetails(this.value,'txtUserName')")

    End Sub
#End Region

#Region "btnSubmit_Click"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim ChngPwd As New ChangePassword()
        Dim Rowsaffected As Integer
        Dim Uname As String = ""
        Uname = txtUserName.Text
        Dim Pwd As String = ""
        Pwd = txtOldPwd.Text
        Dim CPwd As String = ""
        CPwd = txtConPwd.Text

        'Dim userInfo As VMSUserEntity = New VMSUserEntity()
        'If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
        '    userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        'End If

        'Rowsaffected = ChngPwd.UpdatePassword(Session("COMPANY"), Session("UID"), Pwd)
        Rowsaffected = ChngPwd.ChangePassword(Uname, Pwd, CPwd)
        If (Rowsaffected > 0) Then
            'lblConfirmMsg.Visible = True
            Dim alertmessage As String
            alertmessage = "Password Changed Successfully"
            Me.CreateMessageAlert(Me, alertmessage, "alertKey")
            Response.Redirect("~/Login.aspx")
        Else
            Dim alertmessage As String
            alertmessage = "Invalid Username/Password"
            Me.CreateMessageAlert(Me, alertmessage, "alertKey")
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

    End Sub
#End Region

#Region "btnCancel_Click"
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Login.aspx")
    End Sub
#End Region

End Class
