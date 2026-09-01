Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class VprDashboard
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            SetDefaultDateFilter()
            BindGrid()
        End If
    End Sub
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub SetDefaultDateFilter()
        Dim currentYear As Integer = DateTime.Now.Year
        txtFromDate.Text = New DateTime(currentYear, 1, 1).ToString("dd-MM-yyyy")
        txtToDate.Text = New DateTime(currentYear, 12, 31).ToString("dd-MM-yyyy")
        txtVendorName.Text = String.Empty
    End Sub

    Private Sub BindGrid()
        Try
            Dim vendorName As String = txtVendorName.Text.Trim()

            Dim fromDate As DateTime = "2025-01-01"
            Dim toDate As DateTime = "2025-01-01"

            Dim obj As POLinkingRequestClass = New POLinkingRequestClass()
            Dim ds As DataSet = obj.GetVendorPaymentDashboardDetails(vendorName, fromDate, toDate, userInfo.userIDEntity)
            Dim table As DataTable = RmGridHelper.GetTable(ds)
            'RmGridHelper.BindPaged(gvRmPendingList, table)
            If table IsNot Nothing AndAlso table.Rows.Count > 0 Then
                gvFgVendorlist.Visible = True
                RmGridHelper.BindPaged(gvFgVendorlist, table)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        BindGrid()
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        txtVendorName.Text = ""
        txtFromDate.Text = String.Empty
        txtToDate.Text = String.Empty
    End Sub

    Protected Sub gvFgVendorlist_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvFgVendorlist.PageIndex = e.NewPageIndex

        BindGrid()
    End Sub
End Class
