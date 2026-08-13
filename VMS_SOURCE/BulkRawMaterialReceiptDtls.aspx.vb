Imports System.Data
Imports VMS.Web

Partial Class BulkRawMaterialReceiptDtls
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim ObjDocumentType As New VMS.Web.Common()
    Dim dtItmDtlsactual As New DataTable()

    Private Const DefaultSubInventory As String = "New"
    Private Const DefaultLocator As String = "New"

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub AddAttributes()
        btnAdd.Attributes.Add("onClick", "return validateAdjustment();")
        txtQtyPop.Attributes.Add("onblur", "return validatereceivequantitycheck('" & txtQtyPop.ClientID & "','" & lblDespopQty.ClientID & "');")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        AddAttributes()
        If Not IsPostBack Then
            InitItemDespTable()
            BindData()
        End If
    End Sub

    Private Sub InitItemDespTable()
        dtItmDtlsactual = New DataTable()
        dtItmDtlsactual.Columns.Add(New DataColumn("item_code", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("item_name", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("item_type", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("item_type_code", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("sub_inventory", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("sub_inventory_code", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("locator", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("locator_code", GetType(String)))
        dtItmDtlsactual.Columns.Add(New DataColumn("received_qty", GetType(Decimal)))
        dtItmDtlsactual.Columns.Add(New DataColumn("request_quant", GetType(Decimal)))
        ViewState("ItemDesp") = dtItmDtlsactual
    End Sub

    Private Sub BindData()
        BindHeaderAndDetails()
    End Sub

    Private Sub BindHeaderAndDetails()
        Try
            Dim despatchId As Integer = 0
            Integer.TryParse(Convert.ToString(Request.QueryString("despatch_id")), despatchId)
            If despatchId <= 0 Then
                lblErrorMessage.ForeColor = Drawing.Color.Red
                lblErrorMessage.Text = "Invalid despatch id."
                Return
            End If

            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet = obj.GetRawMaterial_DespatchHdrList(despatchId.ToString())

            If ds Is Nothing OrElse ds.Tables.Count = 0 Then
                lblErrorMessage.ForeColor = Drawing.Color.Red
                lblErrorMessage.Text = "Despatch data not found."
                gvVendorRawMat.DataSource = Nothing
                gvVendorRawMat.DataBind()
                Return
            End If

            If Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0 AndAlso ds.Tables(0).Columns.Contains("courier_id") Then
                Dim hdr As DataRow = ds.Tables(0).Rows(0)
                hdnDespatchId.Value = GetColumnValue(hdr, "despatch_id", "despatchid")
                hdnRequisitionId.Value = GetColumnValue(hdr, "requisition_id", "request_id")
                txtCourierno.Text = Convert.ToString(hdr("courier_id"))
            End If

            Dim dtDetails As DataTable = Nothing
            If ds.Tables.Count > 1 AndAlso Not (ds.Tables(1) Is Nothing) AndAlso ds.Tables(1).Columns.Contains("rawmat_code") Then
                dtDetails = ds.Tables(1)
            ElseIf ds.Tables(0).Columns.Contains("rawmat_code") Then
                dtDetails = ds.Tables(0)
            End If

            If Not (dtDetails Is Nothing) AndAlso dtDetails.Rows.Count > 0 Then
                SeedItemDespFromDetails(dtDetails)
                GridBind()
                lblErrorMessage.Text = String.Empty
            Else
                gvVendorRawMat.DataSource = Nothing
                gvVendorRawMat.DataBind()
                lblErrorMessage.ForeColor = Drawing.Color.Red
                lblErrorMessage.Text = "No pending raw material found for receipt."
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub SeedItemDespFromDetails(ByVal dtDetails As DataTable)
        dtItmDtlsactual = CType(ViewState("ItemDesp"), DataTable)
        If dtItmDtlsactual Is Nothing Then
            InitItemDespTable()
            dtItmDtlsactual = CType(ViewState("ItemDesp"), DataTable)
        End If
        dtItmDtlsactual.Rows.Clear()

        For Each dr As DataRow In dtDetails.Rows
            Dim rawmatCode As String = Convert.ToString(dr("rawmat_code"))
            Dim rawmatName As String = Convert.ToString(dr("rawmat_name"))
            Dim requestQty As Decimal = ToDecimal(GetColumnValue(dr, "request_qty", "reqst_Qty"))
            ' Prefer despatch qty for grid; fall back to pending only if despatch columns are missing/zero
            Dim receiveQty As Decimal = GetFirstPositiveQty(dr, "despatch_qunt", "dispatch_qty", "pending_qty")
            Dim subInventory As String = GetColumnValue(dr, "sub_inventory")
            Dim locator As String = GetColumnValue(dr, "locater", "locator")

            If String.IsNullOrWhiteSpace(subInventory) Then
                subInventory = DefaultSubInventory
            End If
            If String.IsNullOrWhiteSpace(locator) Then
                locator = DefaultLocator
            End If

            dtItmDtlsactual.Rows.Add(rawmatCode, rawmatName, "Good", "G", subInventory, subInventory, locator, locator, receiveQty, requestQty)
        Next

        ViewState("ItemDesp") = dtItmDtlsactual
    End Sub

    Private Sub GridBind()
        dtItmDtlsactual = CType(ViewState("ItemDesp"), DataTable)
        If dtItmDtlsactual Is Nothing Then
            gvVendorRawMat.DataSource = Nothing
            gvVendorRawMat.DataBind()
            Return
        End If

        Dim dtShow As New DataTable()
        dtShow.Columns.Add("rawmat_code")
        dtShow.Columns.Add("rawmat_name")
        dtShow.Columns.Add("despatch_qunt", GetType(Decimal))
        dtShow.Columns.Add("reqst_Qty", GetType(Decimal))
        dtShow.Columns.Add("sub_inventory")
        dtShow.Columns.Add("locater")

        Dim distinctItem As DataTable = dtItmDtlsactual.DefaultView.ToTable("Temp", True, "item_code", "item_name")
        If distinctItem.Rows.Count > 0 Then
            For Each itmrow As DataRow In distinctItem.Rows
                Dim condi As String = "item_code = '" & Convert.ToString(itmrow("item_code")).Replace("'", "''") & "'"
                Dim firstRows As DataRow() = dtItmDtlsactual.Select(condi)
                Dim drSum As DataRow = dtShow.NewRow()
                drSum("rawmat_code") = itmrow("item_code")
                drSum("rawmat_name") = itmrow("item_name")
                drSum("despatch_qunt") = ToDecimal(dtItmDtlsactual.Compute("Sum(received_qty)", condi))
                Dim reqObj As Object = dtItmDtlsactual.Compute("Max(request_quant)", condi)
                drSum("reqst_Qty") = If(reqObj Is Nothing OrElse IsDBNull(reqObj), 0D, ToDecimal(reqObj))
                If firstRows.Length > 0 Then
                    drSum("sub_inventory") = firstRows(0)("sub_inventory_code")
                    drSum("locater") = firstRows(0)("locator_code")
                Else
                    drSum("sub_inventory") = DefaultSubInventory
                    drSum("locater") = DefaultLocator
                End If
                dtShow.Rows.Add(drSum)
            Next
        End If

        gvVendorRawMat.DataSource = dtShow
        gvVendorRawMat.DataBind()
        gvVendorRawMat.Visible = (dtShow.Rows.Count > 0)
    End Sub

    Protected Sub gvVendorRawMat_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName <> "Adjustment" Then
            Return
        End If

        Try
            lblmsg.Text = String.Empty
            lblAdjustError.Text = String.Empty

            Dim row As GridViewRow = CType(CType(e.CommandSource, Button).NamingContainer, GridViewRow)

            Dim hdnitem As HiddenField = CType(row.FindControl("hdnRawMatCode"), HiddenField)
            Dim hdnsubinvemtory As HiddenField = CType(row.FindControl("hdnsubinvemtory"), HiddenField)
            Dim hdnlocator As HiddenField = CType(row.FindControl("hdnlocator"), HiddenField)
            Dim lblitem As Label = CType(row.FindControl("lblRawMatName"), Label)
            Dim lbldespQty As Label = CType(row.FindControl("lblDespQty"), Label)
            Dim txtQty As TextBox = CType(row.FindControl("txtQty"), TextBox)

            populateSubInventory(ddlSubInventoryPop)

            If Not String.IsNullOrWhiteSpace(hdnsubinvemtory.Value) AndAlso ddlSubInventoryPop.Items.FindByValue(hdnsubinvemtory.Value) IsNot Nothing Then
                ddlSubInventoryPop.SelectedValue = hdnsubinvemtory.Value
            End If
            populateLocator(ddlLocatorPop)
            If Not String.IsNullOrWhiteSpace(hdnlocator.Value) AndAlso ddlLocatorPop.Items.FindByValue(hdnlocator.Value) IsNot Nothing Then
                ddlLocatorPop.SelectedValue = hdnlocator.Value
            End If

            hdnDespQtygv.Value = lbldespQty.Text
            lblHItemPop.Text = lblitem.Text
            lblQtyPop.Text = lbldespQty.Text
            hdnItemCodePop.Value = hdnitem.Value
            hdnItemTypePop.Value = "G"
            hdnRequestQtyPop.Value = If(txtQty Is Nothing, "0", txtQty.Text)
            txtQtyPop.Text = lbldespQty.Text

            populatepopgv(hdnItemCodePop.Value)
            RefreshAdjustBalanceAndContinue()

            mpAdjust.Show()
            upAdjustPopup.Update()
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect("~/ExceptionPage.aspx")
        End Try
    End Sub

    Protected Sub gvVendorRawMat_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lbldespQty As Label = CType(e.Row.FindControl("lblDespQty"), Label)
            If lbldespQty IsNot Nothing Then
                txtQtyPop.Text = lbldespQty.Text
            End If
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Dim dtItmDtls As DataTable = CreateAdjustTable()

        If gvAdjustDtls.Rows.Count > 0 Then
            For Each gvRow As GridViewRow In gvAdjustDtls.Rows
                AddAdjustRowFromGrid(dtItmDtls, gvRow)
            Next
        End If

        If dtItmDtls.Rows.Count > 0 Then
            Dim condi As String = "item_code = '" & hdnItemCodePop.Value.Replace("'", "''") & "' AND sub_inventory_code = '" & ddlSubInventoryPop.SelectedValue.Replace("'", "''") & "' AND locator_code = '" & ddlLocatorPop.SelectedValue.Replace("'", "''") & "'"
            Dim itmrow As DataRow() = dtItmDtls.Select(condi)
            If itmrow.Length > 0 Then
                lblmsg.Text = "You have already added this sub inventory and locator"
            Else
                AppendAdjustLine(dtItmDtls)
            End If
        Else
            AppendAdjustLine(dtItmDtls)
        End If

        mpAdjust.Show()
        upAdjustPopup.Update()
    End Sub

    Private Sub AppendAdjustLine(ByVal dtItmDtls As DataTable)
        Dim subInvText As String = If(ddlSubInventoryPop.SelectedItem Is Nothing, String.Empty, ddlSubInventoryPop.SelectedItem.Text)
        Dim locatorText As String = If(ddlLocatorPop.SelectedItem Is Nothing, String.Empty, ddlLocatorPop.SelectedItem.Text)
        Dim addQty As Decimal = ToDecimal(txtQtyPop.Text)

        If addQty <= 0D Then
            lblmsg.Text = "Receive Quantity can not be 0 or Negative."
            Return
        End If
        If addQty > ToDecimal(lblDespopQty.Text) Then
            lblmsg.Text = "Receive quantity can not greater than Balance Quantity."
            Return
        End If

        dtItmDtls.Rows.Add(hdnItemCodePop.Value, lblHItemPop.Text, "Good", "G", subInvText, ddlSubInventoryPop.SelectedValue, locatorText, ddlLocatorPop.SelectedValue, addQty)
        gvAdjustDtls.DataSource = dtItmDtls
        gvAdjustDtls.DataBind()
        lblmsg.Text = String.Empty
        RefreshAdjustBalanceAndContinue()
    End Sub

    Protected Sub btnAdjustContinue_Click(sender As Object, e As EventArgs)
        If Not CanContinueAdjustment() Then
            lblAdjustError.Text = "Please allocate full despatch quantity before Continue."
            mpAdjust.Show()
            Return
        End If

        dtItmDtlsactual = CType(ViewState("ItemDesp"), DataTable)
        If dtItmDtlsactual Is Nothing Then
            InitItemDespTable()
            dtItmDtlsactual = CType(ViewState("ItemDesp"), DataTable)
        End If

        If dtItmDtlsactual.Rows.Count > 0 Then
            Dim condi As String = "item_code = '" & hdnItemCodePop.Value.Replace("'", "''") & "'"
            Dim itmrow As DataRow() = dtItmDtlsactual.Select(condi)
            If itmrow.Length > 0 Then
                For Each dr As DataRow In itmrow
                    dr.Delete()
                Next
            End If
            dtItmDtlsactual.AcceptChanges()
        End If

        Dim requestQty As Decimal = ToDecimal(hdnRequestQtyPop.Value)
        If gvAdjustDtls.Rows.Count > 0 Then
            For Each gvRow As GridViewRow In gvAdjustDtls.Rows
                Dim lblitemname As Label = CType(gvRow.FindControl("lblitemname"), Label)
                Dim hdnitemcode As HiddenField = CType(gvRow.FindControl("hdnitemcode"), HiddenField)
                Dim lbltype As Label = CType(gvRow.FindControl("lbltype"), Label)
                Dim hdntypecode As HiddenField = CType(gvRow.FindControl("hdntypecode"), HiddenField)
                Dim lblsubinventory As Label = CType(gvRow.FindControl("lblsubinventory"), Label)
                Dim hdnsubinventorycode As HiddenField = CType(gvRow.FindControl("hdnsubinventorycode"), HiddenField)
                Dim lbllocator As Label = CType(gvRow.FindControl("lbllocator"), Label)
                Dim hdnlocatorcode As HiddenField = CType(gvRow.FindControl("hdnlocatorcode"), HiddenField)
                Dim lblQtygv As Label = CType(gvRow.FindControl("lblQtygv"), Label)
                dtItmDtlsactual.Rows.Add(hdnitemcode.Value, lblitemname.Text, lbltype.Text, hdntypecode.Value, lblsubinventory.Text, hdnsubinventorycode.Value, lbllocator.Text, hdnlocatorcode.Value, ToDecimal(lblQtygv.Text), requestQty)
            Next
        End If

        dtItmDtlsactual.AcceptChanges()
        ViewState("ItemDesp") = dtItmDtlsactual
        GridBind()
        ClearModal()
        mpAdjust.Hide()
    End Sub

    Protected Sub gvAdjustDtls_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName <> "Remove" Then
            Return
        End If

        Dim row As GridViewRow = CType(CType(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
        Dim rowindex As Integer = row.RowIndex
        Dim dtItmDtls As DataTable = CreateAdjustTable()

        If gvAdjustDtls.Rows.Count > 0 Then
            For Each gvRow As GridViewRow In gvAdjustDtls.Rows
                AddAdjustRowFromGrid(dtItmDtls, gvRow)
            Next
            dtItmDtls.Rows.RemoveAt(rowindex)
            dtItmDtls.AcceptChanges()
        End If

        gvAdjustDtls.DataSource = dtItmDtls
        gvAdjustDtls.DataBind()
        RefreshAdjustBalanceAndContinue()
        mpAdjust.Show()
        upAdjustPopup.Update()
    End Sub

    Protected Sub ddlSubInventoryPop_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            populateLocator(ddlLocatorPop)
            mpAdjust.Show()
            upAdjustPopup.Update()
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Response.Redirect("~/ExceptionPage.aspx")
        End Try
    End Sub

    Protected Sub lbtnExit_Click(sender As Object, e As EventArgs)
        ClearModal()
        mpAdjust.Hide()
    End Sub

    Private Function GetAdjustGridQtySum() As Decimal
        Dim total As Decimal = 0D
        For Each gvRow As GridViewRow In gvAdjustDtls.Rows
            Dim lblQtygv As Label = CType(gvRow.FindControl("lblQtygv"), Label)
            If lblQtygv IsNot Nothing Then
                total += ToDecimal(lblQtygv.Text)
            End If
        Next
        Return total
    End Function

    Private Sub RefreshAdjustBalanceAndContinue()
        Dim originalQty As Decimal = ToDecimal(hdnDespQtygv.Value)
        Dim allocatedQty As Decimal = GetAdjustGridQtySum()
        Dim balanceQty As Decimal = originalQty - allocatedQty
        If balanceQty < 0D Then
            balanceQty = 0D
        End If

        lblDespopQty.Text = FormatQty(balanceQty)
        txtQtyPop.Text = FormatQty(balanceQty)
        btnAdjustContinue.Visible = True
        btnAdjustContinue.Enabled = CanContinueAdjustment()
    End Sub

    Private Function CanContinueAdjustment() As Boolean
        Dim originalQty As Decimal = ToDecimal(hdnDespQtygv.Value)
        Dim allocatedQty As Decimal = GetAdjustGridQtySum()
        Return gvAdjustDtls.Rows.Count > 0 AndAlso originalQty > 0D AndAlso allocatedQty = originalQty
    End Function

    Private Sub populateSubInventory(ByVal ddlSubInventory As DropDownList)
        Dim ds As DataSet = ObjDocumentType.GetLovDetails(Constant.Common.Company, "SUB_INVENTORY", Constant.Common.ActiveStatus)
        If Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim dv As DataView = ds.Tables(0).DefaultView
            dv.Sort = "lov_value ASC"
            ddlSubInventory.DataSource = dv
            ddlSubInventory.DataTextField = "lov_value"
            ddlSubInventory.DataValueField = "lov_code"
            ddlSubInventory.DataBind()
            ddlSubInventory.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Else
            ddlSubInventory.Items.Clear()
            ddlSubInventory.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub

    Private Sub populateLocator(ByVal ddlLocator As DropDownList)
        Dim lovType As String
        If String.Compare(ddlSubInventoryPop.SelectedValue, "New", StringComparison.CurrentCultureIgnoreCase) = 0 OrElse String.IsNullOrEmpty(ddlSubInventoryPop.SelectedValue) Then
            lovType = "CB_SUB_INVENTORY"
        Else
            lovType = "CB_SCRAP_LOCATOR"
        End If

        Dim ds As DataSet = ObjDocumentType.GetLovDetails(Constant.Common.Company, lovType, Constant.Common.ActiveStatus)
        If Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim dv As DataView = ds.Tables(0).DefaultView
            dv.Sort = "lov_value ASC"
            ddlLocator.DataSource = dv
            ddlLocator.DataTextField = "lov_value"
            ddlLocator.DataValueField = "lov_code"
            ddlLocator.DataBind()
            ddlLocator.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Else
            ddlLocator.Items.Clear()
            ddlLocator.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub

    Private Sub populatepopgv(ByVal item As String)
        Dim dtItmDtls As DataTable = CType(ViewState("ItemDesp"), DataTable)
        Dim dtShowAdj As DataTable = CreateAdjustTable()

        If dtItmDtls Is Nothing Then
            gvAdjustDtls.DataSource = Nothing
            gvAdjustDtls.DataBind()
            Return
        End If

        Dim condi As String = "item_code = '" & item.Replace("'", "''") & "'"
        Dim itmrow As DataRow() = dtItmDtls.Select(condi)
        If itmrow.Length > 0 Then
            For Each dr As DataRow In itmrow
                Dim drSA As DataRow = dtShowAdj.NewRow()
                drSA("item_code") = Convert.ToString(dr("item_code"))
                drSA("item_name") = dr("item_name")
                drSA("item_type") = dr("item_type")
                drSA("item_type_code") = dr("item_type_code")
                drSA("sub_inventory") = dr("sub_inventory")
                drSA("sub_inventory_code") = dr("sub_inventory_code")
                drSA("locator") = dr("locator")
                drSA("locator_code") = dr("locator_code")
                drSA("received_qty") = dr("received_qty")
                dtShowAdj.Rows.Add(drSA)
            Next
            gvAdjustDtls.DataSource = dtShowAdj
            gvAdjustDtls.DataBind()
        Else
            gvAdjustDtls.DataSource = Nothing
            gvAdjustDtls.DataBind()
        End If
    End Sub

    Private Sub ClearModal()
        If ddlSubInventoryPop.Items.Count > 0 Then
            ddlSubInventoryPop.SelectedIndex = 0
        End If
        ddlLocatorPop.Items.Clear()
        txtQtyPop.Text = String.Empty
        lblDespopQty.Text = "0"
        hdnDespQtygv.Value = String.Empty
        hdnRequestQtyPop.Value = String.Empty
        lblmsg.Text = String.Empty
        lblAdjustError.Text = String.Empty
        gvAdjustDtls.DataSource = Nothing
        gvAdjustDtls.DataBind()
        hdnItemCodePop.Value = String.Empty
        hdnItemTypePop.Value = String.Empty
        lblHItemPop.Text = String.Empty
        lblQtyPop.Text = "0"
    End Sub

    Private Function CreateAdjustTable() As DataTable
        Dim dtItmDtls As New DataTable()
        dtItmDtls.Columns.Add(New DataColumn("item_code", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("item_name", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("item_type", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("item_type_code", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("sub_inventory", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("sub_inventory_code", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("locator", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("locator_code", GetType(String)))
        dtItmDtls.Columns.Add(New DataColumn("received_qty", GetType(Decimal)))
        Return dtItmDtls
    End Function

    Private Sub AddAdjustRowFromGrid(ByVal dtItmDtls As DataTable, ByVal gvRow As GridViewRow)
        Dim lblitemname As Label = CType(gvRow.FindControl("lblitemname"), Label)
        Dim hdnitemcode As HiddenField = CType(gvRow.FindControl("hdnitemcode"), HiddenField)
        Dim lbltype As Label = CType(gvRow.FindControl("lbltype"), Label)
        Dim hdntypecode As HiddenField = CType(gvRow.FindControl("hdntypecode"), HiddenField)
        Dim lblsubinventory As Label = CType(gvRow.FindControl("lblsubinventory"), Label)
        Dim hdnsubinventorycode As HiddenField = CType(gvRow.FindControl("hdnsubinventorycode"), HiddenField)
        Dim lbllocator As Label = CType(gvRow.FindControl("lbllocator"), Label)
        Dim hdnlocatorcode As HiddenField = CType(gvRow.FindControl("hdnlocatorcode"), HiddenField)
        Dim lblQtygv As Label = CType(gvRow.FindControl("lblQtygv"), Label)
        dtItmDtls.Rows.Add(hdnitemcode.Value, lblitemname.Text, lbltype.Text, hdntypecode.Value, lblsubinventory.Text, hdnsubinventorycode.Value, lbllocator.Text, hdnlocatorcode.Value, ToDecimal(lblQtygv.Text))
    End Sub

    Private Function GetColumnValue(ByVal row As DataRow, ByVal ParamArray columnNames As String()) As String
        If row Is Nothing OrElse columnNames Is Nothing Then
            Return String.Empty
        End If

        For Each columnName As String In columnNames
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return Convert.ToString(row(columnName))
            End If
        Next

        Return String.Empty
    End Function

    Private Function GetFirstPositiveQty(ByVal row As DataRow, ByVal ParamArray columnNames As String()) As Decimal
        If row Is Nothing OrElse columnNames Is Nothing Then
            Return 0D
        End If

        Dim fallbackQty As Decimal = 0D
        Dim hasFallback As Boolean = False

        For Each columnName As String In columnNames
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Dim qty As Decimal = ToDecimal(row(columnName))
                If qty > 0D Then
                    Return qty
                End If
                If Not hasFallback Then
                    fallbackQty = qty
                    hasFallback = True
                End If
            End If
        Next

        Return fallbackQty
    End Function

    Private Function ToDecimal(ByVal value As Object) As Decimal
        Dim result As Decimal = 0D
        If value Is Nothing OrElse IsDBNull(value) Then
            Return 0D
        End If
        Decimal.TryParse(Convert.ToString(value), result)
        Return result
    End Function

    Private Function FormatQty(ByVal value As Decimal) As String
        If value = Decimal.Truncate(value) Then
            Return value.ToString("0")
        End If
        Return value.ToString("0.##")
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/BulkRawMaterialReceiptList.aspx", False)
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        lblErrorMessage.Text = String.Empty
        InitItemDespTable()
        BindData()
    End Sub
End Class
