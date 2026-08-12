Partial Class ReportErrPage
    Inherits System.Web.UI.Page

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Response.Write("<script language='javascript'> { window.close();}</script>")


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnOk.Attributes.Add("onclick", "window.close();")


    End Sub
End Class
