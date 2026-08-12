'****************************************************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : ExceptionPage.aspx.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.01.00
'Description	: Code behind file for Exception Handler
'
' Modified By       Modified On           Version               Reason
'****************************************************************************************

Imports System.Data
Partial Class ExceptionPage
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        Try
            'When an unhandled exception occur's, it will get the last 
            'server error and display's it to the user
            If (Not (Session Is Nothing) AndAlso Not (Session(Constant.SessionKeys.ErrMessage)) Is Nothing) Then
                lblErr.Text = Session(Constant.SessionKeys.ErrMessage).ToString()
                Session(Constant.SessionKeys.ErrMessage) = Nothing

            End If
        Catch Ex As Exception
            'Suppress any errors in this page to avoid circular reference
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'Page.RegisterStartupScript("BackScript", "<script language='javascript'>history.go(-1);</script>")
        Server.Transfer("~/Home.aspx")
    End Sub
End Class
