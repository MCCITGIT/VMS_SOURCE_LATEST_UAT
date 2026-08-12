<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        'Server.ClearError
        'Server.Transfer("~/Login.aspx")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.       
    End Sub
    
    Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        'Code that runs for user authentication
        If Not (HttpContext.Current.User Is Nothing) Then
            If HttpContext.Current.User.Identity.IsAuthenticated Then
                If TypeOf HttpContext.Current.User.Identity Is FormsIdentity Then
                    Dim ID As FormsIdentity
                    Dim ticket As FormsAuthenticationTicket
                    Dim roles(1) As String
          
                    ID = HttpContext.Current.User.Identity
                    ticket = ID.Ticket
                    roles(0) = ticket.UserData
                    HttpContext.Current.User = New System.Security.Principal.GenericPrincipal(ID, roles)
                End If
            End If
        End If
    End Sub
    
</script>