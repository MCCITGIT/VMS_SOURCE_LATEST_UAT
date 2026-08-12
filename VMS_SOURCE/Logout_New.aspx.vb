'****************************************************************************************
'Copyright	    : TransGuard, MCC, KOLKATA
'Source	        : Logout.aspx.vb
'Created Date	: 28-February-2007
'Created By	    : Vivek Subbiah
'Version	        : R01.01.00
'Description	    :Code behind file for user logout
'
'Modified By       Modified On       Version         Reason
'
'****************************************************************************************
Imports VMS.Web
Partial Class Logout_New
    Inherits System.Web.UI.Page


#Region "Page load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SignOut()
    End Sub

#End Region
#Region "SignOut Activities"

    ' Kill the Session. Reset the appropriate session variables and expire the cookie
    Private Sub SignOut()
        Try
            Session.Abandon()
            FormsAuthentication.SignOut()
            FormsAuthentication.Initialize()
            Dim context As HttpContext = HttpContext.Current
            'Session(Constant.SessionKeys.GetServerTime) = Nothing
            'Session(Constant.SessionKeys.UserLogged) = False
            'Session(Constant.SessionKeys.UserLogged) = Nothing
            'Session("PartyId") = Nothing
            Dim cookie As New HttpCookie(FormsAuthentication.FormsCookieName, String.Empty)
            cookie.Path = FormsAuthentication.FormsCookiePath
            cookie.Expires = DateTime.Now
            context.Response.Cookies.Remove(FormsAuthentication.FormsCookieName)
            context.Response.Cookies.Add(cookie)


        Catch ex As Exception

        End Try

        Dim nextpage As String = "../VMS"
        Response.Write("<Script language=javaScript >")
        Response.Write("{")
        Response.Write("var backhistory=history.length;")
        Response.Write("history.go(-(backhistory+backhistory+backhistory));")
        Response.Write(" window.location.href='" & nextpage & "'; ")


        Response.Write("}")
        Response.Write("</script>")

        Response.Redirect("~/Login.aspx")
    End Sub
#End Region

End Class