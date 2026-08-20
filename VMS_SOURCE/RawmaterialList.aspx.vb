Imports VMS.Web
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class RawmaterialList
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            PopulateVendor()
            BindData()
        End If
    End Sub

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub BindData()
        Try
            Dim ds As DataSet
            Dim obj As New OPC_VendorClass()
            ds = obj.GetRawmaterialList(ddlVendor.SelectedValue)

            Dim table As DataTable = RmGridHelper.GetTable(ds)
            RmGridHelper.BindPaged(gvRawMatList, table)
            UpdateSummary(table)
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub UpdateSummary(ByVal sourceTable As DataTable)
        Dim totalCount As Integer = 0
        Dim gstCount As Integer = 0
        Dim emailCount As Integer = 0

        If sourceTable IsNot Nothing Then
            totalCount = sourceTable.Rows.Count
            For Each row As DataRow In sourceTable.Rows
                If sourceTable.Columns.Contains("gst_no") AndAlso Not String.IsNullOrWhiteSpace(Convert.ToString(row("gst_no"))) Then
                    gstCount += 1
                End If
                If sourceTable.Columns.Contains("email") AndAlso Not String.IsNullOrWhiteSpace(Convert.ToString(row("email"))) Then
                    emailCount += 1
                End If
            Next
        End If

        'lblTotalCount.Text = totalCount.ToString()
        'lblGstCount.Text = gstCount.ToString()
        'lblEmailCount.Text = emailCount.ToString()
    End Sub

    Protected Sub gvRawMatList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRawMatList.PageIndexChanging
        gvRawMatList.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Private Sub PopulateVendor()
        Dim obj As New OPC_VendorClass()
        Dim ds As New DataSet()
        ds = obj.GetRawMaterialVendorList()

        ddlVendor.Items.Clear()
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlVendor.DataSource = ds.Tables(0)
            ddlVendor.DataTextField = "vendor_name"
            ddlVendor.DataValueField = "vendor_code"
            ddlVendor.DataBind()
        End If
        ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub
    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs)
        gvRawMatList.PageIndex = 0
        BindData()
    End Sub

    Protected Sub ImgbtnAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/VendorRawMaterialLink.aspx")
    End Sub
    Protected Sub gvRawMatList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRawMatList.RowCommand
        Try
            If e.CommandName = "View" Then
                Dim row As GridViewRow = Nothing
                Dim clickedControl As Control = TryCast(e.CommandSource, Control)
                If Not clickedControl Is Nothing Then
                    row = TryCast(clickedControl.NamingContainer, GridViewRow)
                End If

                If row Is Nothing Then
                    Dim rowIndex As Integer = 0
                    If Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) AndAlso rowIndex >= 0 AndAlso rowIndex < gvRawMatList.Rows.Count Then
                        row = gvRawMatList.Rows(rowIndex)
                    End If
                End If

                If row Is Nothing Then
                    Throw New Exception("Unable to determine selected row.")
                End If

                Dim VendorCode As Label = CType(row.FindControl("lblVendorCode"), Label)

                Dim redirectUrl = "VendorRawMaterialLink.aspx?vendorcode=" & Server.UrlEncode(VendorCode.Text)
                Response.Redirect(redirectUrl, False)
                Context.ApplicationInstance.CompleteRequest()
                Exit Sub
            End If
        Catch ex As System.Threading.ThreadAbortException
            ' Ignore redirect thread-abort behavior.
        Catch ex As Exception
            Dim returnUrl As String = "~/XP_ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Response.Redirect(returnUrl)
        End Try
    End Sub
End Class
