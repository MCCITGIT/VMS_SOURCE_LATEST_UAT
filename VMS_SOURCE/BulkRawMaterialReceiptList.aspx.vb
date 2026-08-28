Imports System.Data
Imports VMS.Web

Partial Class BulkRawMaterialReceiptList
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            BindDropDown()
            BindData()
        End If
    End Sub
    Private Sub BindDropDown()
        PopulateRawMatVendor()
        PopulateStatus()
    End Sub
    Private Sub PopulateRawMatVendor()
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetRawMaterialVendorList()

        ddlRawMatvendor.Items.Clear()
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlRawMatvendor.DataSource = ds.Tables(0)
            ddlRawMatvendor.DataTextField = "vendor_name"
            ddlRawMatvendor.DataValueField = "vendor_code"
            ddlRawMatvendor.DataBind()
        End If
        ddlRawMatvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub

    Private Sub PopulateStatus()
        'ddlStatus.Items.Clear()
        'ddlStatus.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        ddlStatus.Items.Insert(0, New ListItem("Pending", "P"))
        ddlStatus.Items.Insert(1, New ListItem("Received", "R"))
    End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs)
        lblErrorMessage.Text = String.Empty
        gvReceipt.PageIndex = 0
        BindData()
    End Sub

    Protected Sub gvReceipt_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "ViewReceipt" Then
                Dim row As GridViewRow = Nothing
                Dim clickedControl As Control = TryCast(e.CommandSource, Control)
                If Not clickedControl Is Nothing Then
                    row = TryCast(clickedControl.NamingContainer, GridViewRow)
                End If

                If row Is Nothing Then
                    Dim rowIndex As Integer = 0
                    If Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) AndAlso rowIndex >= 0 AndAlso rowIndex < gvReceipt.Rows.Count Then
                        row = gvReceipt.Rows(rowIndex)
                    End If
                End If

                If row Is Nothing Then
                    Throw New Exception("Unable to determine selected row.")
                End If

                Dim hdnDespatchId As HiddenField = CType(row.FindControl("hdnDespatchId"), HiddenField)
                Dim hdnReceivedId As HiddenField = CType(row.FindControl("hdnReceivedId"), HiddenField)
                Dim receiveId As String = String.Empty
                If hdnReceivedId IsNot Nothing Then
                    receiveId = Convert.ToString(hdnReceivedId.Value).Trim()
                    If receiveId.Contains(",") Then
                        receiveId = receiveId.Split(","c)(0).Trim()
                    End If
                End If

                Dim redirectUrl As String
                If ddlStatus.SelectedValue = "R" AndAlso Not String.IsNullOrWhiteSpace(receiveId) Then
                    redirectUrl = "~/BulkRawMaterialReceiptDtls.aspx?receive_id=" & Server.UrlEncode(receiveId) & "&despatch_id=" & Server.UrlEncode(hdnDespatchId.Value)
                Else
                    redirectUrl = "~/BulkRawMaterialReceiptDtls.aspx?despatch_id=" & Server.UrlEncode(hdnDespatchId.Value)
                End If
                Response.Redirect(redirectUrl, False)
                Context.ApplicationInstance.CompleteRequest()
            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub BindData()
        Try
            Dim obj As New OPC_VendorClass()

            Dim ds As DataSet = obj.GetRawMaterialReceiptList(ddlRawMatvendor.SelectedValue, ddlStatus.SelectedValue)

            'Grid
            Dim table As DataTable = ds.Tables(0)
            RmGridHelper.BindPaged(gvReceipt, table)


            'Summary
            If ds.Tables.Count > 1 Then
                UpdateSummary(ds.Tables(1))
            Else
                UpdateSummary(Nothing)
            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub UpdateSummary(ByVal summaryTable As DataTable)

        Dim totalCount As Integer = 0
        Dim pendingCount As Integer = 0
        Dim receivedCount As Integer = 0

        If summaryTable IsNot Nothing AndAlso summaryTable.Rows.Count > 0 Then
            Dim row As DataRow = summaryTable.Rows(0)
            totalCount = If(IsDBNull(row("TotalRecords")), 0, Convert.ToInt32(row("TotalRecords")))
            pendingCount = If(IsDBNull(row("TotalPendingRecords")), 0, Convert.ToInt32(row("TotalPendingRecords")))
            receivedCount = If(IsDBNull(row("TotalReceivedRecords")), 0, Convert.ToInt32(row("TotalReceivedRecords")))
        End If

        lblTotalCount.Text = totalCount.ToString()
        lblPendingCount.Text = pendingCount.ToString()
        lblReceivedCount.Text = receivedCount.ToString()

    End Sub

    Protected Sub gvReceipt_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvReceipt.PageIndexChanging
        gvReceipt.PageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class
