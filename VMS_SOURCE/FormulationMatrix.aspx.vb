Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Linq
Imports VMS.Web

Partial Class FormulationMatrix
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private Const GridTableKey As String = "FormulationMatrixGridTable"

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not txtProductSearch.Enabled OrElse String.Equals(txtProductSearch.Attributes("readonly"), "readonly", StringComparison.OrdinalIgnoreCase) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "lockProductSearch", "syncProductResetButtonState();", True)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()

        If Not IsPostBack Then
            InitializeGridTable()
            BindMatrixGrid()
            If Not String.IsNullOrWhiteSpace(Request.QueryString("producode")) Then
                hdnProductCode.Value = Convert.ToString(Request.QueryString("producode")).Trim()
                hdnProductName.Value = Convert.ToString(Request.QueryString("produname")).Trim()
                hdnSkucode.Value = Convert.ToString(Request.QueryString("skucode")).Trim()
                If hdnSkucode.Value = "" Then
                    hdnSkucode.Value = hdnProductCode.Value
                End If
                If hdnProductName.Value <> "" Then
                    txtProductSearch.Text = hdnProductName.Value
                Else
                    txtProductSearch.Text = hdnProductCode.Value
                End If
                txtProductSearch.Attributes("readonly") = "readonly"
                LoadFormulationMatrix()
            End If
        End If
    End Sub

    Private Sub CheckLogin()
        If Not (Session(Constant.SessionKeys.UserInfo) Is Nothing) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    <System.Web.Script.Services.ScriptMethod(),
    System.Web.Services.WebMethod()>
    Public Shared Function ProductSearch(ByVal prefixText As String) As String()
        Dim productDetails As List(Of String) = New List(Of String)()

        If String.IsNullOrWhiteSpace(prefixText) OrElse prefixText.Trim().Length < 3 Then
            Return productDetails.ToArray()
        End If

        Try
            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet = obj.GetProduct(prefixText.Trim())

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0) Is Nothing Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim productCode As String = Convert.ToString(dr("product_code")).Trim()
                    Dim productName As String = Convert.ToString(dr("product_name")).Trim()
                    Dim skuCode As String = If(dr.Table.Columns.Contains("sku_code"), Convert.ToString(dr("sku_code")).Trim(), String.Empty)

                    If productName <> "" AndAlso productCode <> "" Then
                        Dim itemValue As String = If(skuCode <> "", productCode & "|" & skuCode, productCode)
                        productDetails.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(productName, itemValue))
                    End If
                Next
            End If
        Catch
        End Try

        Return productDetails.ToArray()
    End Function

    <System.Web.Script.Services.ScriptMethod(),
    System.Web.Services.WebMethod()>
    Public Shared Function RawMaterialSearch(ByVal prefixText As String) As String()
        Dim rawMaterialDetails As List(Of String) = New List(Of String)()

        If String.IsNullOrWhiteSpace(prefixText) OrElse prefixText.Trim().Length < 3 Then
            Return rawMaterialDetails.ToArray()
        End If

        Try
            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet = obj.GetRawMatList(prefixText.Trim())

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0) Is Nothing Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim rawMaterialId As String = Convert.ToString(dr("Raw_Mat_Code")).Trim()
                    Dim rawMaterialName As String = Convert.ToString(dr("Raw_Mat_Name")).Trim()

                    If rawMaterialName <> "" AndAlso rawMaterialId <> "" Then
                        rawMaterialDetails.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rawMaterialName, rawMaterialId))
                    End If
                Next
            End If
        Catch
        End Try

        Return rawMaterialDetails.ToArray()
    End Function

    Private Sub InitializeGridTable()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add(New DataColumn("id", GetType(String)))
        dt.Columns.Add(New DataColumn("header_id", GetType(String)))
        dt.Columns.Add(New DataColumn("brand_code", GetType(String)))
        dt.Columns.Add(New DataColumn("brand_name", GetType(String)))
        dt.Columns.Add(New DataColumn("vendor_code", GetType(String)))
        dt.Columns.Add(New DataColumn("vendor_name", GetType(String)))
        dt.Columns.Add(New DataColumn("product_code", GetType(String)))
        dt.Columns.Add(New DataColumn("product_name", GetType(String)))
        dt.Columns.Add(New DataColumn("rawmat_code", GetType(String)))
        dt.Columns.Add(New DataColumn("rawmat_name", GetType(String)))
        dt.Columns.Add(New DataColumn("ratio", GetType(String)))
        dt.Columns.Add(New DataColumn("rate", GetType(String)))
        ViewState(GridTableKey) = dt
    End Sub

    Private Function GetGridTable() As DataTable
        Dim dt As DataTable = TryCast(ViewState(GridTableKey), DataTable)
        If dt Is Nothing Then
            InitializeGridTable()
            dt = TryCast(ViewState(GridTableKey), DataTable)
        End If
        Return dt
    End Function

    Private Sub CaptureGridRates()
        Dim dt As DataTable = GetGridTable()
        For i As Integer = 0 To gvFormulationMatrix.Rows.Count - 1
            Dim txtRate As TextBox = CType(gvFormulationMatrix.Rows(i).FindControl("txtRate"), TextBox)
            Dim hdnRawMatCode As HiddenField = CType(gvFormulationMatrix.Rows(i).FindControl("hdnRawMatCode"), HiddenField)
            Dim hdnBrandCode As HiddenField = CType(gvFormulationMatrix.Rows(i).FindControl("hdnBrandCode"), HiddenField)
            Dim hdnVendorCode As HiddenField = CType(gvFormulationMatrix.Rows(i).FindControl("hdnVendorCode"), HiddenField)

            If txtRate Is Nothing Then
                Continue For
            End If

            Dim rateValue As String = Convert.ToString(txtRate.Text).Trim()
            Dim rawMatCode As String = If(hdnRawMatCode Is Nothing, String.Empty, Convert.ToString(hdnRawMatCode.Value).Trim())
            Dim brandCode As String = If(hdnBrandCode Is Nothing, String.Empty, Convert.ToString(hdnBrandCode.Value).Trim())
            Dim vendorCode As String = If(hdnVendorCode Is Nothing, String.Empty, Convert.ToString(hdnVendorCode.Value).Trim())

            Dim hdnHeaderId As HiddenField = CType(gvFormulationMatrix.Rows(i).FindControl("hdnHeaderId"), HiddenField)
            Dim hdnGridProductCode As HiddenField = CType(gvFormulationMatrix.Rows(i).FindControl("hdnGridProductCode"), HiddenField)
            Dim headerId As String = If(hdnHeaderId Is Nothing, String.Empty, Convert.ToString(hdnHeaderId.Value).Trim())
            Dim productCode As String = If(hdnGridProductCode Is Nothing, String.Empty, Convert.ToString(hdnGridProductCode.Value).Trim())

            For Each row As DataRow In dt.Rows
                If Convert.ToString(row("rawmat_code")).Trim().Equals(rawMatCode, StringComparison.OrdinalIgnoreCase) AndAlso
                   Convert.ToString(row("brand_code")).Trim().Equals(brandCode, StringComparison.OrdinalIgnoreCase) AndAlso
                   Convert.ToString(row("vendor_code")).Trim().Equals(vendorCode, StringComparison.OrdinalIgnoreCase) AndAlso
                   (headerId = "" OrElse Convert.ToString(row("header_id")).Trim() = headerId) AndAlso
                   (productCode = "" OrElse Convert.ToString(row("product_code")).Trim().Equals(productCode, StringComparison.OrdinalIgnoreCase)) Then
                    row("rate") = rateValue
                    Exit For
                End If
            Next
        Next
        ViewState(GridTableKey) = dt
    End Sub

    Private Sub BindMatrixGrid()
        Dim dt As DataTable = GetGridTable()
        Dim filteredTable As DataTable = dt
        Dim rawMatFilter As String = Convert.ToString(txtrawmatid.Value).Trim()

        If rawMatFilter <> "" AndAlso dt.Rows.Count > 0 Then
            filteredTable = dt.Clone()
            For Each row As DataRow In dt.Rows
                If Convert.ToString(row("rawmat_code")).Trim().Equals(rawMatFilter, StringComparison.OrdinalIgnoreCase) Then
                    filteredTable.ImportRow(row)
                End If
            Next
        End If

        gvFormulationMatrix.DataSource = filteredTable
        gvFormulationMatrix.DataBind()
        btnSubmit.Visible = filteredTable.Rows.Count > 0
        SetSubmitButtonText(dt)
    End Sub

    Private Sub SetSubmitButtonText(ByVal dt As DataTable)
        Dim hasExisting As Boolean = False
        If dt IsNot Nothing Then
            For Each row As DataRow In dt.Rows
                If Convert.ToString(row("id")).Trim() <> "" Then
                    hasExisting = True
                    Exit For
                End If
            Next
        End If
        btnSubmit.Text = If(hasExisting, "Update", "Submit")
    End Sub

    Protected Sub txtProductSearch_TextChanged(sender As Object, e As EventArgs)
        txtProductSearch.Attributes("readonly") = "readonly"
        LoadFormulationMatrix()
    End Sub

    Protected Sub btnResetProductPostback_Click(sender As Object, e As EventArgs)
        InitializeGridTable()
        txtrawmatid.Value = String.Empty
        txtSearchText.Text = String.Empty
        lblErrorMessage.Text = ""
        BindMatrixGrid()
        btnSubmit.Visible = False
    End Sub

    Protected Sub btnFilterRawMat_Click(sender As Object, e As EventArgs)
        CaptureGridRates()
        If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
            ShowValidation("Please select Product.")
            Return
        End If
        BindMatrixGrid()
    End Sub

    Private Sub LoadFormulationMatrix()
        lblErrorMessage.Text = ""

        If String.IsNullOrWhiteSpace(hdnProductCode.Value) AndAlso String.IsNullOrWhiteSpace(hdnSkucode.Value) Then
            ShowValidation("Please select Product.")
            InitializeGridTable()
            BindMatrixGrid()
            Return
        End If

        Dim dt As DataTable = GetGridTable()
        dt.Rows.Clear()

        Dim obj As New OPC_VendorClass()
        FillGridFromFormulationMaster(dt, obj)
        ApplySavedMatrixRates(dt, obj)

        ViewState(GridTableKey) = dt
        BindMatrixGrid()

        If dt.Rows.Count = 0 Then
            ShowValidation("No formulation details found for the selected product.")
        End If
    End Sub

    Private Sub FillGridFromFormulationMaster(ByVal dt As DataTable, ByVal obj As OPC_VendorClass)
        Dim productCode As String = hdnProductCode.Value.Trim()
        Dim skuCode As String = hdnSkucode.Value.Trim()

        Dim listDs As DataSet = obj.GetFormulationDataList(String.Empty, String.Empty, productCode, String.Empty)
        If Not HasData(listDs) AndAlso skuCode <> "" AndAlso Not skuCode.Equals(productCode, StringComparison.OrdinalIgnoreCase) Then
            listDs = obj.GetFormulationDataList(String.Empty, String.Empty, skuCode, String.Empty)
        End If
        If Not HasData(listDs) Then
            Return
        End If

        For Each hdrRow As DataRow In listDs.Tables(0).Rows
            Dim brandCode As String = GetRowValue(hdrRow, "Brand_Code", "brand_code")
            Dim brandName As String = GetRowValue(hdrRow, "Brand_Name", "brand_name")
            Dim vendorCode As String = GetRowValue(hdrRow, "vendor_code", "Vendor_Code", "unit_code")
            Dim vendorName As String = GetRowValue(hdrRow, "vendor_name", "Vendor_Name")
            Dim headerProductCode As String = GetRowValue(hdrRow, "Sku_Code", "product_code", "opc_product_code")
            Dim productName As String = GetRowValue(hdrRow, "Sku_Desc", "product_name")
            Dim headerIdValue As Integer = ParseInteger(GetRowValue(hdrRow, "fh_id", "opc_id", "header_id", "id"))

            If String.IsNullOrWhiteSpace(productName) AndAlso Not String.IsNullOrWhiteSpace(hdnProductName.Value) Then
                productName = hdnProductName.Value.Trim()
            End If
            If String.IsNullOrWhiteSpace(headerProductCode) Then
                headerProductCode = If(productCode <> "", productCode, skuCode)
            End If
            If headerIdValue <= 0 Then
                Continue For
            End If

            Dim detailDs As DataSet = obj.GetFormulationEditList(brandCode, headerIdValue, headerProductCode)
            Dim detailTable As DataTable = Nothing
            If detailDs IsNot Nothing AndAlso detailDs.Tables.Count > 1 AndAlso HasTableRows(detailDs.Tables(1)) Then
                detailTable = detailDs.Tables(1)
            ElseIf detailDs IsNot Nothing AndAlso HasTableRows(detailDs.Tables(0)) AndAlso Not detailDs.Tables(0).Columns.Contains("Brand_Code") Then
                detailTable = detailDs.Tables(0)
            End If

            If detailTable Is Nothing Then
                Continue For
            End If

            For Each detailRow As DataRow In detailTable.Rows
                AddMatrixRow(dt, String.Empty, headerIdValue.ToString(), brandCode, brandName, vendorCode, vendorName, headerProductCode, productName, detailRow)
            Next
        Next
    End Sub

    Private Sub ApplySavedMatrixRates(ByVal dt As DataTable, ByVal obj As OPC_VendorClass)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Return
        End If

        Dim productCodes As New List(Of String)
        AddUniqueCode(productCodes, hdnSkucode.Value)
        AddUniqueCode(productCodes, hdnProductCode.Value)
        For Each row As DataRow In dt.Rows
            AddUniqueCode(productCodes, Convert.ToString(row("product_code")))
        Next

        Dim matrixTable As DataTable = Nothing
        For Each productCode As String In productCodes
            Dim matrixDs As DataSet = obj.GetFormulationMatrixList(productCode, String.Empty, String.Empty, String.Empty)
            If HasData(matrixDs) Then
                matrixTable = MergeMatrixTables(matrixTable, matrixDs.Tables(0))
            End If
        Next

        If matrixTable Is Nothing Then
            Dim allDs As DataSet = obj.GetFormulationMatrixList(String.Empty, String.Empty, String.Empty, String.Empty)
            If HasData(allDs) Then
                matrixTable = allDs.Tables(0)
            End If
        End If
        If matrixTable Is Nothing OrElse matrixTable.Rows.Count = 0 Then
            Return
        End If

        For Each matrixRow As DataRow In matrixTable.Rows
            Dim matrixId As String = GetRowValue(matrixRow, "id")
            Dim headerId As String = GetRowValue(matrixRow, "header_id")
            Dim brandCode As String = GetRowValue(matrixRow, "brand_code")
            Dim vendorCode As String = GetRowValue(matrixRow, "vendor_code")
            Dim savedProductCode As String = GetRowValue(matrixRow, "product_code")
            Dim rawMatCode As String = GetRowValue(matrixRow, "rawmat_code")
            Dim rate As String = GetRowValue(matrixRow, "rate")

            For Each row As DataRow In dt.Rows
                Dim headerMatch As Boolean = (headerId = "" OrElse Convert.ToString(row("header_id")).Trim() = headerId)
                Dim productMatch As Boolean = (savedProductCode = "" OrElse
                    Convert.ToString(row("product_code")).Trim().Equals(savedProductCode, StringComparison.OrdinalIgnoreCase))

                If headerMatch AndAlso productMatch AndAlso
                   Convert.ToString(row("brand_code")).Trim().Equals(brandCode, StringComparison.OrdinalIgnoreCase) AndAlso
                   Convert.ToString(row("vendor_code")).Trim().Equals(vendorCode, StringComparison.OrdinalIgnoreCase) AndAlso
                   Convert.ToString(row("rawmat_code")).Trim().Equals(rawMatCode, StringComparison.OrdinalIgnoreCase) Then
                    row("id") = matrixId
                    If rate <> "" Then
                        row("rate") = rate
                    End If
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Shared Sub AddUniqueCode(ByVal codes As List(Of String), ByVal value As String)
        Dim code As String = Convert.ToString(value).Trim()
        If code = "" Then
            Return
        End If
        For Each existing As String In codes
            If existing.Equals(code, StringComparison.OrdinalIgnoreCase) Then
                Return
            End If
        Next
        codes.Add(code)
    End Sub

    Private Shared Function MergeMatrixTables(ByVal target As DataTable, ByVal source As DataTable) As DataTable
        If source Is Nothing Then
            Return target
        End If
        If target Is Nothing Then
            Return source.Copy()
        End If
        For Each sourceRow As DataRow In source.Rows
            Dim sourceId As String = GetRowValue(sourceRow, "id")
            Dim exists As Boolean = False
            For Each targetRow As DataRow In target.Rows
                If GetRowValue(targetRow, "id") = sourceId AndAlso sourceId <> "" Then
                    exists = True
                    Exit For
                End If
            Next
            If Not exists Then
                target.ImportRow(sourceRow)
            End If
        Next
        Return target
    End Function

    Private Sub FillGridFromTable(ByVal dt As DataTable, ByVal sourceTable As DataTable)
        For Each sourceRow As DataRow In sourceTable.Rows
            Dim dr As DataRow = dt.NewRow()
            dr("id") = GetRowValue(sourceRow, "id", "fm_id", "matrix_id")
            dr("header_id") = GetRowValue(sourceRow, "header_id", "fh_id", "opc_id")
            dr("brand_code") = GetRowValue(sourceRow, "brand_code", "Brand_Code")
            dr("brand_name") = GetRowValue(sourceRow, "brand_name", "Brand_Name")
            dr("vendor_code") = GetRowValue(sourceRow, "vendor_code", "Vendor_Code", "unit_code")
            dr("vendor_name") = GetRowValue(sourceRow, "vendor_name", "Vendor_Name")
            dr("product_code") = GetRowValue(sourceRow, "product_code", "Sku_Code")
            dr("product_name") = GetRowValue(sourceRow, "product_name", "Sku_Desc")
            dr("rawmat_code") = GetRowValue(sourceRow, "rawmat_code", "Raw_Mat_Code", "fd_rawmat_code")
            dr("rawmat_name") = GetRowValue(sourceRow, "rawmat_name", "Raw_Mat_Name")
            dr("ratio") = GetRowValue(sourceRow, "ratio", "fd_ratio")
            dr("rate") = GetRowValue(sourceRow, "rate", "fm_rate")
            dt.Rows.Add(dr)
        Next
    End Sub

    Private Sub AddMatrixRow(ByVal dt As DataTable, ByVal id As String, ByVal headerId As String, ByVal brandCode As String, ByVal brandName As String,
                             ByVal vendorCode As String, ByVal vendorName As String, ByVal productCode As String, ByVal productName As String, ByVal detailRow As DataRow)
        Dim rawMatCode As String = GetRowValue(detailRow, "rawmat_code", "Raw_Mat_Code", "fd_rawmat_code", "opcd_rawmat_code", "opc_rawmat_code")
        If String.IsNullOrWhiteSpace(rawMatCode) Then
            Return
        End If

        If MatrixRowExists(dt, brandCode, vendorCode, rawMatCode) Then
            Return
        End If

        Dim dr As DataRow = dt.NewRow()
        dr("id") = If(String.IsNullOrWhiteSpace(id), GetRowValue(detailRow, "id", "fm_id"), id)
        dr("header_id") = headerId
        dr("brand_code") = If(String.IsNullOrWhiteSpace(brandCode), GetRowValue(detailRow, "brand_code", "Brand_Code"), brandCode)
        dr("brand_name") = If(String.IsNullOrWhiteSpace(brandName), GetRowValue(detailRow, "brand_name", "Brand_Name"), brandName)
        dr("vendor_code") = If(String.IsNullOrWhiteSpace(vendorCode), GetRowValue(detailRow, "vendor_code", "Vendor_Code"), vendorCode)
        dr("vendor_name") = If(String.IsNullOrWhiteSpace(vendorName), GetRowValue(detailRow, "vendor_name", "Vendor_Name"), vendorName)
        dr("product_code") = If(String.IsNullOrWhiteSpace(productCode), GetRowValue(detailRow, "product_code", "Sku_Code"), productCode)
        dr("product_name") = If(String.IsNullOrWhiteSpace(productName), GetRowValue(detailRow, "product_name", "Sku_Desc"), productName)
        dr("rawmat_code") = rawMatCode
        dr("rawmat_name") = GetRowValue(detailRow, "rawmat_name", "Raw_Mat_Name")
        dr("ratio") = GetRowValue(detailRow, "ratio", "fd_ratio", "opcd_ratio")
        dr("rate") = GetRowValue(detailRow, "rate", "fm_rate")
        dt.Rows.Add(dr)
    End Sub

    Private Shared Function MatrixRowExists(ByVal dt As DataTable, ByVal brandCode As String, ByVal vendorCode As String, ByVal rawMatCode As String) As Boolean
        For Each row As DataRow In dt.Rows
            If Convert.ToString(row("brand_code")).Trim().Equals(brandCode, StringComparison.OrdinalIgnoreCase) AndAlso
               Convert.ToString(row("vendor_code")).Trim().Equals(vendorCode, StringComparison.OrdinalIgnoreCase) AndAlso
               Convert.ToString(row("rawmat_code")).Trim().Equals(rawMatCode, StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Shared Function TryParseRate(ByVal value As String, ByRef numericRate As Decimal) As Boolean
        numericRate = 0D
        If String.IsNullOrWhiteSpace(value) Then
            Return False
        End If

        Dim rateText As String = value.Trim()
        If Decimal.TryParse(rateText, NumberStyles.Number, CultureInfo.InvariantCulture, numericRate) OrElse
           Decimal.TryParse(rateText, NumberStyles.Number, CultureInfo.CurrentCulture, numericRate) Then
            Return numericRate > 0D
        End If

        Return False
    End Function

    Private Shared Function ParseInteger(ByVal value As String) As Integer
        Dim intValue As Integer = 0
        If Integer.TryParse(value, intValue) Then
            Return intValue
        End If

        Dim decimalValue As Decimal = 0D
        If Decimal.TryParse(value, decimalValue) Then
            Return CInt(decimalValue)
        End If

        Return 0
    End Function

    Protected Sub gvFormulationMatrix_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFormulationMatrix.RowCommand
        If e.CommandName <> "UpdateRow" Then
            Exit Sub
        End If

        CaptureGridRates()

        Dim rowIndex As Integer = 0
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) Then
            Exit Sub
        End If
        If rowIndex < 0 OrElse rowIndex >= gvFormulationMatrix.Rows.Count Then
            Exit Sub
        End If

        Dim gridRow As GridViewRow = gvFormulationMatrix.Rows(rowIndex)
        Dim hdnId As HiddenField = CType(gridRow.FindControl("hdnId"), HiddenField)
        Dim txtRate As TextBox = CType(gridRow.FindControl("txtRate"), TextBox)
        Dim rateValue As String = If(txtRate Is Nothing, String.Empty, Convert.ToString(txtRate.Text).Trim())

        Dim matrixId As Integer = 0
        Integer.TryParse(If(hdnId Is Nothing, String.Empty, hdnId.Value), matrixId)

        If matrixId <= 0 Then
            ShowValidation("Please submit the formulation matrix before updating individual rows.")
            Exit Sub
        End If

        Dim numericRate As Decimal = 0D
        If String.IsNullOrWhiteSpace(rateValue) OrElse Not Decimal.TryParse(rateValue, numericRate) OrElse numericRate <= 0D Then
            ShowValidation("Please enter a valid Rate greater than 0.")
            Exit Sub
        End If

        Try
            Dim obj As New OPC_VendorClass()
            Dim rowsAffected As Integer = obj.UpdateFormulationMatrix(matrixId, rateValue, userInfo.userIDEntity)
            If rowsAffected > 0 Then
                Dim dt As DataTable = GetGridTable()
                Dim rawMatCode As HiddenField = CType(gridRow.FindControl("hdnRawMatCode"), HiddenField)
                Dim brandCode As HiddenField = CType(gridRow.FindControl("hdnBrandCode"), HiddenField)
                Dim vendorCode As HiddenField = CType(gridRow.FindControl("hdnVendorCode"), HiddenField)
                For Each row As DataRow In dt.Rows
                    If Convert.ToString(row("id")).Trim() = Convert.ToString(matrixId) OrElse
                       (Convert.ToString(row("rawmat_code")).Trim().Equals(If(rawMatCode Is Nothing, "", rawMatCode.Value.Trim()), StringComparison.OrdinalIgnoreCase) AndAlso
                        Convert.ToString(row("brand_code")).Trim().Equals(If(brandCode Is Nothing, "", brandCode.Value.Trim()), StringComparison.OrdinalIgnoreCase) AndAlso
                        Convert.ToString(row("vendor_code")).Trim().Equals(If(vendorCode Is Nothing, "", vendorCode.Value.Trim()), StringComparison.OrdinalIgnoreCase)) Then
                        row("rate") = rateValue
                    End If
                Next
                ViewState(GridTableKey) = dt
                BindMatrixGrid()
                RmActionPopup.ShowSuccess(Me, "Record updated successfully.")
            Else
                ShowValidation("Unable to update record.")
            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect("~/ExceptionPage.aspx")
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
            ShowValidation("Please select Product.")
            Exit Sub
        End If

        If gvFormulationMatrix.Rows.Count = 0 Then
            ShowValidation("No formulation details found for the selected product.")
            Exit Sub
        End If

        Dim saveTable As DataTable = New DataTable()
        saveTable.Columns.Add("id", GetType(Integer))
        saveTable.Columns.Add("header_id", GetType(Integer))
        saveTable.Columns.Add("brand_code", GetType(String))
        saveTable.Columns.Add("vendor_code", GetType(String))
        saveTable.Columns.Add("product_code", GetType(String))
        saveTable.Columns.Add("rawmat_code", GetType(String))
        saveTable.Columns.Add("ratio", GetType(String))
        saveTable.Columns.Add("rate", GetType(String))

        Dim dtGrid As DataTable = GetGridTable()

        For Each gridRow As GridViewRow In gvFormulationMatrix.Rows
            If gridRow.RowType <> DataControlRowType.DataRow Then
                Continue For
            End If

            Dim txtRate As TextBox = CType(gridRow.FindControl("txtRate"), TextBox)
            Dim hdnId As HiddenField = CType(gridRow.FindControl("hdnId"), HiddenField)
            Dim hdnHeaderId As HiddenField = CType(gridRow.FindControl("hdnHeaderId"), HiddenField)
            Dim hdnBrandCode As HiddenField = CType(gridRow.FindControl("hdnBrandCode"), HiddenField)
            Dim hdnVendorCode As HiddenField = CType(gridRow.FindControl("hdnVendorCode"), HiddenField)
            Dim hdnGridProductCode As HiddenField = CType(gridRow.FindControl("hdnGridProductCode"), HiddenField)
            Dim hdnRawMatCode As HiddenField = CType(gridRow.FindControl("hdnRawMatCode"), HiddenField)
            Dim lblRatio As Label = CType(gridRow.FindControl("lblRatio"), Label)

            Dim rateText As String = If(txtRate Is Nothing, String.Empty, Convert.ToString(txtRate.Text).Trim())
            Dim numericRate As Decimal = 0D
            If Not TryParseRate(rateText, numericRate) Then
                ShowValidation("Please enter a valid Rate greater than 0 for all raw materials.")
                Exit Sub
            End If

            Dim existingId As Integer = ParseInteger(If(hdnId Is Nothing, String.Empty, hdnId.Value))
            Dim headerId As Integer = ParseInteger(If(hdnHeaderId Is Nothing, String.Empty, hdnHeaderId.Value))
            Dim brandCode As String = If(hdnBrandCode Is Nothing, String.Empty, Convert.ToString(hdnBrandCode.Value).Trim())
            Dim vendorCode As String = If(hdnVendorCode Is Nothing, String.Empty, Convert.ToString(hdnVendorCode.Value).Trim())
            Dim productCode As String = If(hdnGridProductCode Is Nothing, String.Empty, Convert.ToString(hdnGridProductCode.Value).Trim())
            Dim rawMatCode As String = If(hdnRawMatCode Is Nothing, String.Empty, Convert.ToString(hdnRawMatCode.Value).Trim())
            Dim ratioText As String = If(lblRatio Is Nothing, String.Empty, Convert.ToString(lblRatio.Text).Trim())
            Dim normalizedRate As String = numericRate.ToString("0.00", CultureInfo.InvariantCulture)

            If String.IsNullOrWhiteSpace(rawMatCode) Then
                ShowValidation("Raw material details are missing. Please select the product again.")
                Exit Sub
            End If

            Dim dr As DataRow = saveTable.NewRow()
            dr("id") = existingId
            dr("header_id") = headerId
            dr("brand_code") = brandCode
            dr("vendor_code") = vendorCode
            dr("product_code") = If(productCode <> "", productCode, hdnSkucode.Value.Trim())
            dr("rawmat_code") = rawMatCode
            dr("ratio") = ratioText
            dr("rate") = normalizedRate
            saveTable.Rows.Add(dr)

            For Each row As DataRow In dtGrid.Rows
                If Convert.ToString(row("rawmat_code")).Trim().Equals(rawMatCode, StringComparison.OrdinalIgnoreCase) AndAlso
                   Convert.ToString(row("brand_code")).Trim().Equals(brandCode, StringComparison.OrdinalIgnoreCase) AndAlso
                   Convert.ToString(row("vendor_code")).Trim().Equals(vendorCode, StringComparison.OrdinalIgnoreCase) Then
                    row("rate") = normalizedRate
                    If existingId > 0 Then
                        row("id") = existingId.ToString()
                    End If
                    Exit For
                End If
            Next
        Next

        If saveTable.Rows.Count = 0 Then
            ShowValidation("No formulation details found for the selected product.")
            Exit Sub
        End If

        ViewState(GridTableKey) = dtGrid

        Try
            Dim obj As New OPC_VendorClass()
            Dim rowsAffected As Integer = obj.InsertFormulationMatrix(userInfo.userIDEntity, saveTable)
            If rowsAffected > 0 Then
                lblErrorMessage.Text = ""
                Dim successMessage As String = If(btnSubmit.Text = "Update", "Updated Successfully.", "Submitted Successfully.")
                RmActionPopup.ShowSuccess(Me, successMessage, "FormulationMatrixList.aspx")
            Else
                ShowValidation("Unable to save formulation matrix. Please try again.")
            End If
        Catch ex As Exception
            ShowValidation("Unable to save formulation matrix. Please try again.")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/FormulationMatrixList.aspx")
    End Sub

    Private Sub ShowValidation(ByVal message As String)
        lblErrorMessage.Text = ""
        RmActionPopup.ShowError(Me, message)
    End Sub

    Private Shared Function HasData(ByVal ds As DataSet) As Boolean
        Return ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso HasTableRows(ds.Tables(0))
    End Function

    Private Shared Function HasTableRows(ByVal table As DataTable) As Boolean
        Return table IsNot Nothing AndAlso table.Rows.Count > 0
    End Function

    Private Shared Function GetRowValue(ByVal row As DataRow, ParamArray columnNames As String()) As String
        If row Is Nothing Then
            Return String.Empty
        End If

        For Each columnName As String In columnNames
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return Convert.ToString(row(columnName)).Trim()
            End If
        Next

        Return String.Empty
    End Function
End Class
