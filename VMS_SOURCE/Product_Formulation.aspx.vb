Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class Product_Formulation
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private Const GridTableKey As String = "VendorRawMatGridTable"
    Private gridRatioTotal As Decimal = 0D
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not txtProductSearch.Enabled OrElse String.Equals(txtProductSearch.Attributes("readonly"), "readonly", StringComparison.OrdinalIgnoreCase) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "lockProductSearch", "syncProductResetButtonState();", True)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        btnSubmit.Attributes.Add("onclick", "return validateFormulationSubmit();")
        btnAdd.Attributes.Add("onclick", "return validateAddRawMaterial();")

        If Not IsPostBack Then
            BrandDetailsListLoad()
            VendorDetailsListLoad()
            InitializeGridTable()
            If Not String.IsNullOrWhiteSpace(Request.QueryString("brandcode")) AndAlso
               Not String.IsNullOrWhiteSpace(Request.QueryString("producode")) AndAlso
                Not String.IsNullOrWhiteSpace(Request.QueryString("id")) Then
                Binddata()
            End If
        End If
    End Sub
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
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
                    Dim sku_code As String = Convert.ToString(dr("sku_code")).Trim()

                    If productName <> "" AndAlso productCode <> "" Then
                        productDetails.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(productName, productCode & "|" & sku_code))
                    End If
                Next
            End If
        Catch ex As Exception
            ' Keep autocomplete resilient; return collected items.
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
        Catch ex As Exception
            ' Keep autocomplete resilient; return whatever is already collected.
        End Try

        Return rawMaterialDetails.ToArray()
    End Function

    Private Sub InitializeGridTable()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add(New DataColumn("brand_code", GetType(String)))
        dt.Columns.Add(New DataColumn("brand_name", GetType(String)))
        dt.Columns.Add(New DataColumn("vendor_code", GetType(String)))
        dt.Columns.Add(New DataColumn("vendor_name", GetType(String)))
        dt.Columns.Add(New DataColumn("product_code", GetType(String)))
        dt.Columns.Add(New DataColumn("product_name", GetType(String)))
        dt.Columns.Add(New DataColumn("rawmat_code", GetType(String)))
        dt.Columns.Add(New DataColumn("rawmat_name", GetType(String)))
        dt.Columns.Add(New DataColumn("ratio", GetType(String)))
        dt.Columns.Add(New DataColumn("unit", GetType(String)))
        ViewState(GridTableKey) = dt
    End Sub
    Private Sub CaptureGridRates()
        Dim dt As DataTable = GetGridTable()
        For i As Integer = 0 To gvVendorRawMat.Rows.Count - 1
            Dim txtRate As TextBox = CType(gvVendorRawMat.Rows(i).FindControl("txtRate"), TextBox)
            If i < dt.Rows.Count AndAlso Not txtRate Is Nothing Then
                dt.Rows(i)("rate") = Convert.ToString(txtRate.Text).Trim()
            End If
        Next
        ViewState(GridTableKey) = dt
    End Sub
    Private Function GetGridTable() As DataTable
        Dim dt As DataTable = TryCast(ViewState(GridTableKey), DataTable)
        If dt Is Nothing Then
            InitializeGridTable()
            dt = TryCast(ViewState(GridTableKey), DataTable)
        Else
            If Not dt.Columns.Contains("vendor_code") Then
                dt.Columns.Add("vendor_code", GetType(String))
            End If
            If Not dt.Columns.Contains("vendor_name") Then
                dt.Columns.Add("vendor_name", GetType(String))
            End If
            ViewState(GridTableKey) = dt
        End If
        Return dt
    End Function
    Private Sub BindRawMatGrid()
        gridRatioTotal = 0D
        gvVendorRawMat.DataSource = GetGridTable()
        gvVendorRawMat.DataBind()
    End Sub

    Protected Sub gvVendorRawMat_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvVendorRawMat.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblRatio As Label = TryCast(e.Row.FindControl("lblRatio"), Label)
            If lblRatio IsNot Nothing Then
                Dim ratio As Decimal
                If Decimal.TryParse(lblRatio.Text.Trim(), ratio) Then
                    gridRatioTotal += ratio
                End If
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.TableSection = TableRowSection.TableFooter
            SetRatioFooterLabels(e.Row, gridRatioTotal)
        End If
    End Sub

    Private Sub SetRatioFooterLabels(ByVal footerRow As GridViewRow, ByVal totalRatio As Decimal)
        Dim lblRatioTotal As Label = TryCast(footerRow.FindControl("lblRatioTotal"), Label)
        Dim lblRatioStatus As Label = TryCast(footerRow.FindControl("lblRatioStatus"), Label)

        If lblRatioTotal IsNot Nothing Then
            lblRatioTotal.Text = totalRatio.ToString("0.00") & "%"
        End If

        If lblRatioStatus IsNot Nothing Then
            If totalRatio > 100D Then
                lblRatioStatus.Text = "Exceed 100%"
                lblRatioStatus.ForeColor = Drawing.Color.Red
            Else
                lblRatioStatus.Text = "Within 100%"
                lblRatioStatus.ForeColor = Drawing.Color.Green
            End If
        End If
    End Sub
    Private Sub ShowValidation(ByVal message As String)
        lblErrorMessage.Text = ""
        RmActionPopup.ShowError(Me, message)
    End Sub

    Private Function ValidateSubmitInputs() As Boolean
        ClearInlineValidation()

        Dim isValid As Boolean = True

        If ddlBrand.SelectedIndex <= 0 Then
            AppendInlineValidation("Brand", "Please select Brand.")
            isValid = False
        End If

        If ddlvendor.SelectedIndex <= 0 Then
            AppendInlineValidation("Vendor", "Please select Vendor.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
            AppendInlineValidation("Product", "Please enter Product.")
            isValid = False
        End If

        Dim gridRowCount As Integer = 0
        Dim totalRatio As Integer = 0

        For Each row As GridViewRow In gvVendorRawMat.Rows
            If row.RowType <> DataControlRowType.DataRow Then
                Continue For
            End If

            gridRowCount += 1
            Dim lblRatio As Label = CType(row.FindControl("lblRatio"), Label)
            Dim ratioText As String = If(lblRatio Is Nothing, String.Empty, Convert.ToString(lblRatio.Text).Trim())
            Dim ratioValue As Integer = 0

            If Not Integer.TryParse(ratioText, ratioValue) Then
                AppendInlineValidation("Grid", "Please enter valid integer Consumption Ratio.")
                Return False
            End If

            totalRatio += ratioValue
        Next

        If gridRowCount = 0 Then
            AppendInlineValidation("Grid", "Please enter at least one record in the grid.")
            isValid = False
        ElseIf totalRatio <> 100 Then
            AppendInlineValidation("Grid", "Total Consumption Ratio should be equal 100%.")
            isValid = False
        End If

        Return isValid
    End Function

    Private Function ValidateAddInputs() As Boolean
        ClearInlineValidation()

        Dim isValid As Boolean = True

        If ddlBrand.SelectedIndex <= 0 Then
            AppendInlineValidation("Brand", "Please select Brand.")
            isValid = False
        End If

        If ddlvendor.SelectedIndex <= 0 Then
            AppendInlineValidation("Vendor", "Please select Vendor.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
            AppendInlineValidation("Product", "Please enter Product.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtSearchText.Text.Trim()) Then
            AppendInlineValidation("RawMaterial", "Please enter Raw Material.")
            isValid = False
        ElseIf String.IsNullOrWhiteSpace(txtrawmatid.Value) Then
            AppendInlineValidation("RawMaterial", "Please select Raw Material from the list.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtRatio.Text) Then
            AppendInlineValidation("Ratio", "Please enter Consumption Ratio.")
            isValid = False
        End If

        Return isValid
    End Function

    Private Sub ClearInlineValidation()
        ddlBrand.CssClass = "form-control select2"
        ddlvendor.CssClass = "form-control select2"
        txtProductSearch.CssClass = "form-control"
        txtSearchText.CssClass = "form-control"
        txtRatio.CssClass = "form-control"
        valBrand.Text = String.Empty
        valVendor.Text = String.Empty
        valProduct.Text = String.Empty
        valSearchText.Text = String.Empty
        valRatio.Text = String.Empty
        valGrid.Text = String.Empty
    End Sub

    Private Sub AppendInlineValidation(ByVal fieldKey As String, ByVal message As String)
        Select Case fieldKey
            Case "Brand"
                ddlBrand.CssClass = "form-control select2 field-invalid"
                valBrand.Text = message
            Case "Vendor"
                ddlvendor.CssClass = "form-control select2 field-invalid"
                valVendor.Text = message
            Case "Product"
                txtProductSearch.CssClass = "form-control field-invalid"
                valProduct.Text = message
            Case "RawMaterial"
                txtSearchText.CssClass = "form-control field-invalid"
                valSearchText.Text = message
            Case "Ratio"
                txtRatio.CssClass = "form-control field-invalid"
                valRatio.Text = message
            Case "Grid"
                valGrid.Text = message
        End Select
    End Sub

    Private Sub ShowInlineValidation(ByVal fieldKey As String, ByVal message As String)
        ClearInlineValidation()
        AppendInlineValidation(fieldKey, message)
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        If gvVendorRawMat.EditIndex >= 0 Then
            gvVendorRawMat.EditIndex = -1
        End If

        CaptureGridRates()
        btnSubmit.Visible = True

        If Not ValidateAddInputs() Then
            Exit Sub
        End If

        Dim dt As DataTable = GetGridTable()
        Dim selectedbrandCode As String = ddlBrand.SelectedValue.Trim()
        Dim selectedVendorCode As String = ddlvendor.SelectedValue.Trim()
        Dim selectedProductCode As String = hdnProductCode.Value.Trim()
        Dim selectedRawMatCode As String = txtrawmatid.Value.Trim()

        For Each row As DataRow In dt.Rows
            If Convert.ToString(row("brand_code")).Trim().Equals(selectedbrandCode, StringComparison.OrdinalIgnoreCase) AndAlso
               Convert.ToString(row("vendor_code")).Trim().Equals(selectedVendorCode, StringComparison.OrdinalIgnoreCase) AndAlso
               Convert.ToString(row("rawmat_code")).Trim().Equals(selectedRawMatCode, StringComparison.OrdinalIgnoreCase) Then
                ShowInlineValidation("RawMaterial", "Selected Raw Material already added.")
                Exit Sub
            End If
        Next

        Dim rawMatName As String = txtSearchText.Text.Trim()
        If rawMatName.Contains("(") Then
            rawMatName = rawMatName.Substring(0, rawMatName.LastIndexOf("("c)).Trim()
        End If

        'dt.Columns.Add(New DataColumn("vendor_code", GetType(String)))
        'dt.Columns.Add(New DataColumn("vendor_name", GetType(String)))
        'dt.Columns.Add(New DataColumn("product_code", GetType(String)))
        'dt.Columns.Add(New DataColumn("product_name", GetType(String)))
        'dt.Columns.Add(New DataColumn("rawmat_code", GetType(String)))
        'dt.Columns.Add(New DataColumn("rawmat_name", GetType(String)))
        'dt.Columns.Add(New DataColumn("ratio", GetType(String)))
        'dt.Columns.Add(New DataColumn("unit", GetType(String)))

        Dim dr As DataRow = dt.NewRow()

        dr("brand_code") = ddlBrand.SelectedValue
        dr("brand_name") = ddlBrand.SelectedItem.Text

        dr("vendor_code") = ddlvendor.SelectedValue
        dr("vendor_name") = ddlvendor.SelectedItem.Text

        dr("product_code") = hdnProductCode.Value
        dr("product_name") = hdnProductName.Value

        dr("rawmat_code") = selectedRawMatCode
        dr("rawmat_name") = rawMatName

        dr("ratio") = txtRatio.Text
        'dr("unit") = txtmeasurement.Text
        dr("unit") = ""
        dt.Rows.Add(dr)

        ViewState(GridTableKey) = dt

        gvVendorRawMat.EditIndex = -1
        BindRawMatGrid()
        ClearInlineValidation()
        ClearControl()

        Dim productCodeToKeep As String = hdnProductCode.Value.Trim()
        Dim productNameToKeep As String = hdnProductName.Value.Trim()

        hdnProductCode.Value = productCodeToKeep
        hdnProductName.Value = productNameToKeep
        txtProductSearch.Text = productNameToKeep
        txtProductSearch.Enabled = False
    End Sub

    Protected Sub gvVendorRawMat_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvVendorRawMat.RowCommand
        If e.CommandName <> "DeleteRow" Then
            Exit Sub
        End If

        Dim rowIndex As Integer = 0
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) Then
            Exit Sub
        End If

        If gvVendorRawMat.EditIndex >= 0 Then
            gvVendorRawMat.EditIndex = -1
        End If

        Dim dt As DataTable = GetGridTable()
        If rowIndex < 0 OrElse rowIndex >= dt.Rows.Count Then
            Exit Sub
        End If

        dt.Rows.RemoveAt(rowIndex)
        dt.AcceptChanges()
        ViewState(GridTableKey) = dt

        BindRawMatGrid()
        btnSubmit.Visible = dt.Rows.Count > 0
        lblErrorMessage.Text = ""
        RmActionPopup.ShowSuccess(Me, "Record deleted successfully.")
    End Sub

    Private Sub ClearControl()
        txtSearchText.Text = String.Empty
        txtrawmatid.Value = String.Empty
        txtRatio.Text = String.Empty
        txtmeasurement.Text = String.Empty
        lblErrorMessage.Text = ""
    End Sub
    Private Sub BrandDetailsListLoad()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        ds = obj.BindBrandMasterList()

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlBrand.DataSource = ds
                ddlBrand.DataTextField = "brand_name"
                ddlBrand.DataValueField = "brand_id"
                ddlBrand.DataBind()
            Else
                ddlBrand.DataSource = Nothing
                ddlBrand.DataBind()
            End If
            ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub

    Private Sub VendorDetailsListLoad()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        ds = obj.GetUnitName(Constant.Common.ActiveStatus)

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlvendor.DataSource = ds
                ddlvendor.DataTextField = "unit_name"
                ddlvendor.DataValueField = "unit_code"
                ddlvendor.DataBind()
            Else
                ddlvendor.DataSource = Nothing
                ddlvendor.DataBind()
            End If
            ddlvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub

    Protected Sub ddlBrand_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlBrand.Enabled = False
    End Sub

    Protected Sub ddlvendor_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlvendor.Enabled = False
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim obj As New OPC_VendorClass()
        Dim rowsAffected As Integer = 0
        Try
            If Not ValidateSubmitInputs() Then
                Exit Sub
            End If

            Dim dt As DataTable = New DataTable()
            dt.Columns.Add(New DataColumn("fd_rawmat_code", GetType(String)))
            dt.Columns.Add(New DataColumn("fd_ratio", GetType(String)))
            dt.Columns.Add(New DataColumn("fd_unit", GetType(String)))

            For Each row As GridViewRow In gvVendorRawMat.Rows
                If row.RowType <> DataControlRowType.DataRow Then
                    Continue For
                End If

                Dim hdnRawMatCode As HiddenField = CType(row.FindControl("hdnRawMatCode"), HiddenField)
                Dim lblRatio As Label = CType(row.FindControl("lblRatio"), Label)
                Dim lblUnit As Label = CType(row.FindControl("lblUnit"), Label)

                Dim rawmatcode As String = If(hdnRawMatCode Is Nothing, String.Empty, Convert.ToString(hdnRawMatCode.Value).Trim())
                Dim ratioText As String = If(lblRatio Is Nothing, String.Empty, Convert.ToString(lblRatio.Text).Trim())
                Dim measurement As String = If(lblUnit Is Nothing, String.Empty, Convert.ToString(lblUnit.Text).Trim())

                Dim dr As DataRow = dt.NewRow()
                dr("fd_rawmat_code") = rawmatcode
                dr("fd_ratio") = ratioText
                dr("fd_unit") = measurement
                dt.Rows.Add(dr)
            Next

            rowsAffected = obj.Insert_Formulation(Val(hdnId.Value), ddlBrand.SelectedValue, ddlvendor.SelectedValue, hdnProductCode.Value.Trim(), dt, userInfo.userIDEntity)

            If rowsAffected > 0 Then
                ClearInlineValidation()
                lblErrorMessage.Text = ""
                RmActionPopup.ShowSuccess(Me, "Submitted Successfully.", "FormulationMstrList.aspx")
                ddlBrand.SelectedIndex = 0
                txtProductSearch.Text = String.Empty
                hdnProductCode.Value = String.Empty
                hdnProductName.Value = String.Empty
                gvVendorRawMat.DataSource = Nothing
                gvVendorRawMat.DataBind()
            Else
                ShowInlineValidation("Grid", "Something went wrong. Try again.")
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub
    Private Sub Binddata()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        Dim brandCode As String = Convert.ToString(Request.QueryString("brandcode"))
        Dim produCode As String = Convert.ToString(Request.QueryString("producode"))
        Dim id As String = Convert.ToString(Request.QueryString("id"))

        ds = obj.GetFormulationEditList(brandCode, id, produCode)

        If ds Is Nothing OrElse ds.Tables.Count = 0 Then Exit Sub

        If Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim hdrRow As DataRow = ds.Tables(0).Rows(0)
            hdnId.Value = Convert.ToString(hdrRow("opc_id"))
            Dim hdrBrandCode As String = Convert.ToString(hdrRow("Brand_Code"))
            Dim hdrProductCode As String = Convert.ToString(hdrRow("Sku_Code"))
            Dim hdrProductDesc As String = Convert.ToString(hdrRow("Sku_Desc"))

            If ddlBrand.Items.FindByValue(hdrBrandCode) IsNot Nothing Then
                ddlBrand.SelectedValue = hdrBrandCode
            End If

            Dim hdrVendorCode As String = GetHeaderColumnValue(hdrRow, "vendor_code", "Vendor_Code", "unit_code")
            If Not String.IsNullOrWhiteSpace(hdrVendorCode) AndAlso ddlvendor.Items.FindByValue(hdrVendorCode) IsNot Nothing Then
                ddlvendor.SelectedValue = hdrVendorCode
            End If

            hdnProductCode.Value = hdrProductCode
            txtProductSearch.Text = hdrProductDesc & " (" & hdrProductCode & ")"

            ddlBrand.Enabled = False
            ddlvendor.Enabled = False
            txtProductSearch.Enabled = False
            'btnSubmit.Visible = True
            'btnSubmit.Text = "Update"
        End If

        If ds.Tables.Count > 1 AndAlso Not (ds.Tables(1) Is Nothing) AndAlso ds.Tables(1).Rows.Count > 0 Then
            Dim dtGrid As DataTable = ds.Tables(1).Copy()
            ApplyVendorToGridTable(dtGrid)
            ViewState(GridTableKey) = dtGrid
            BindRawMatGrid()
        Else
            gvVendorRawMat.DataSource = Nothing
            gvVendorRawMat.DataBind()
        End If
    End Sub
    Protected Sub btnCancel_Click1(sender As Object, e As EventArgs)
        Response.Redirect("~/FormulationMstrList.aspx")
    End Sub

    Private Sub populateRecipe()
        Dim apiUrl As String = "https://oic-dev-axbw0xev3jux-hy.integration.ap-hyderabad-1.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/BPIL_IMPORT_RECIPE_DETAIL_V1/1.0/bpil/recipe/import"
        Dim postData = New With {Key .SKU = hdnSkucode.Value}
        Dim ds As DataSet = OPC_VendorClass.PostApiWithHeadersToDataSet(apiUrl, postData).Result

        If (ds IsNot Nothing) AndAlso (ds.Tables.Count > 0) AndAlso (ds.Tables(0) IsNot Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            Dim recipeTable As DataTable = ds.Tables(0).Copy()
            If Not recipeTable.Columns.Contains("RecipeWithVersion") Then
                recipeTable.Columns.Add("RecipeWithVersion", GetType(String))
            End If

            For Each row As DataRow In recipeTable.Rows
                Dim recipe As String = Convert.ToString(row("RECIPE NUMBER")).Trim()
                Dim version As String = Convert.ToString(row("VERSION")).Trim()
                row("RecipeWithVersion") = BuildRecipeDropDownValue(recipe, version)
            Next

            ddlRecipe.DataSource = recipeTable
            ddlRecipe.DataTextField = "RecipeWithVersion"
            ddlRecipe.DataValueField = "RecipeWithVersion"
            ddlRecipe.DataBind()
            ddlRecipe.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub

    Private Shared Function BuildRecipeDropDownValue(ByVal recipe As String, ByVal version As String) As String
        Return String.Join("|", recipe.Trim(), version.Trim())
    End Function

    Private Sub ParseSelectedRecipe(ByRef recipeNo As String, ByRef recipeVersion As String)
        recipeNo = String.Empty
        recipeVersion = String.Empty

        If ddlRecipe.SelectedIndex <= 0 OrElse String.IsNullOrWhiteSpace(ddlRecipe.SelectedValue) Then
            Return
        End If

        Dim recipeParts As String() = ddlRecipe.SelectedValue.Split("|"c)
        If recipeParts.Length > 0 Then
            recipeNo = recipeParts(0).Trim()
        End If
        If recipeParts.Length > 1 Then
            recipeVersion = recipeParts(1).Trim()
        End If
    End Sub
    Protected Sub txtProductSearch_TextChanged(sender As Object, e As EventArgs)
        txtProductSearch.Attributes("readonly") = "readonly"
        populateRecipe()
    End Sub
    Private Sub populateGridGrid(ByVal recipeNo As String, ByVal recipeVersion As String)
        lblErrorMessage.Text = String.Empty

        If String.IsNullOrWhiteSpace(recipeNo) OrElse String.IsNullOrWhiteSpace(recipeVersion) Then
            Return
        End If

        Try
            Dim apiUrl As String = "https://oic-dev-axbw0xev3jux-hy.integration.ap-hyderabad-1.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/BPIL_IMPORT_RECIPE_INGREDIE/1.0/bpil/ingredient/import"
            Dim postData As New Dictionary(Of String, String) From {
                {"RECIPE NO", recipeNo.Trim()},
                {"VERSION", recipeVersion.Trim()}
            }

            Dim ds As DataSet = OPC_VendorClass.PostApiWithHeadersToDataSet(apiUrl, postData).Result
            If ds Is Nothing OrElse ds.Tables.Count = 0 OrElse ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                ShowValidation("Recipe details were not returned from API. Please try again.")
                Return
            End If

            ViewState(GridTableKey) = BuildGridTableFromRecipeApi(ds.Tables(0))
            BindRawMatGrid()
            btnSubmit.Visible = True
        Catch ex As Exception
            ShowValidation("Unable to load recipe ingredients. Please try again.")
        End Try
    End Sub

    Private Function BuildGridTableFromRecipeApi(ByVal apiTable As DataTable) As DataTable
        Dim dt As DataTable = GetGridTable()
        dt.Rows.Clear()

        Dim brandCode As String = If(ddlBrand.SelectedIndex > 0, ddlBrand.SelectedValue.Trim(), String.Empty)
        Dim brandName As String = If(ddlBrand.SelectedIndex > 0, ddlBrand.SelectedItem.Text.Trim(), String.Empty)
        Dim vendorCode As String = If(ddlvendor.SelectedIndex > 0, ddlvendor.SelectedValue.Trim(), String.Empty)
        Dim vendorName As String = If(ddlvendor.SelectedIndex > 0, ddlvendor.SelectedItem.Text.Trim(), String.Empty)
        Dim productCode As String = hdnProductCode.Value.Trim()
        Dim productName As String = hdnProductName.Value.Trim()

        Dim sortedRows As IEnumerable(Of DataRow) = apiTable.AsEnumerable()
        If apiTable.Columns.Contains("LINE NUMBER") Then
            sortedRows = sortedRows.OrderBy(Function(row)
                                                Dim lineNo As Integer
                                                Integer.TryParse(Convert.ToString(row("LINE NUMBER")).Trim(), lineNo)
                                                Return lineNo
                                            End Function)
        End If

        For Each apiRow As DataRow In sortedRows
            Dim ingredientCode As String = GetApiColumnValue(apiRow, apiTable, "INGREDIENT")
            If String.IsNullOrWhiteSpace(ingredientCode) Then
                Continue For
            End If

            Dim rawMatCode As String = GetRawMaterialCodeFromIngredient(ingredientCode)
            Dim unit As String = GetUnitFromIngredientCode(ingredientCode)

            Dim dr As DataRow = dt.NewRow()
            dr("brand_code") = brandCode
            dr("brand_name") = brandName
            dr("vendor_code") = vendorCode
            dr("vendor_name") = vendorName
            dr("product_code") = productCode
            dr("product_name") = productName
            dr("rawmat_code") = rawMatCode
            dr("rawmat_name") = GetRawMaterialName(rawMatCode)
            dr("ratio") = ConvertRecipeQuantityToRatio(GetApiColumnValue(apiRow, apiTable, "Quantity"))
            dr("unit") = unit
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Function GetRawMaterialName(ByVal rawMatCode As String) As String
        If String.IsNullOrWhiteSpace(rawMatCode) Then
            Return String.Empty
        End If

        Try
            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet = obj.GetRawMatList(rawMatCode.Trim())
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0) Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    If Convert.ToString(row("Raw_Mat_Code")).Trim().Equals(rawMatCode.Trim(), StringComparison.OrdinalIgnoreCase) Then
                        Return Convert.ToString(row("Raw_Mat_Name")).Trim()
                    End If
                Next

                Return Convert.ToString(ds.Tables(0).Rows(0)("Raw_Mat_Name")).Trim()
            End If
        Catch
        End Try

        Return rawMatCode.Trim()
    End Function

    Private Shared Function ConvertRecipeQuantityToRatio(ByVal quantityText As String) As String
        Dim quantity As Decimal
        If Not Decimal.TryParse(quantityText.Trim(), quantity) Then
            Return quantityText.Trim()
        End If

        If quantity <= 1D Then
            Return (quantity * 100D).ToString("0.##")
        End If

        Return quantity.ToString("0.##")
    End Function

    Private Shared ReadOnly IngredientUnitSuffixes As String() = {"BULKKG", "BULKLTR", "BULKMT", "BULKGM", "BULKLT", "KG", "LTR", "LT", "MT", "GM"}

    Private Shared Function GetUnitFromIngredientCode(ByVal ingredientCode As String) As String
        If String.IsNullOrWhiteSpace(ingredientCode) Then
            Return String.Empty
        End If

        Dim code As String = ingredientCode.Trim().ToUpperInvariant()
        For Each suffix As String In IngredientUnitSuffixes.OrderByDescending(Function(item) item.Length)
            If code.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) Then
                Return suffix
            End If
        Next

        Return String.Empty
    End Function

    Private Shared Function GetRawMaterialCodeFromIngredient(ByVal ingredientCode As String) As String
        If String.IsNullOrWhiteSpace(ingredientCode) Then
            Return String.Empty
        End If

        Dim code As String = ingredientCode.Trim()
        Dim unitSuffix As String = GetUnitFromIngredientCode(code)
        If Not String.IsNullOrWhiteSpace(unitSuffix) AndAlso code.Length > unitSuffix.Length Then
            Return code.Substring(0, code.Length - unitSuffix.Length)
        End If

        Return code
    End Function

    Private Shared Function GetApiColumnValue(ByVal row As DataRow, ByVal table As DataTable, ParamArray columnNames As String()) As String
        For Each columnName As String In columnNames
            If table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return Convert.ToString(row(columnName)).Trim()
            End If
        Next

        Return String.Empty
    End Function

    Private Sub ApplyVendorToGridTable(ByVal dtGrid As DataTable)
        If dtGrid Is Nothing Then
            Return
        End If

        If Not dtGrid.Columns.Contains("vendor_code") Then
            dtGrid.Columns.Add("vendor_code", GetType(String))
        End If
        If Not dtGrid.Columns.Contains("vendor_name") Then
            dtGrid.Columns.Add("vendor_name", GetType(String))
        End If

        Dim vendorCode As String = If(ddlvendor.SelectedIndex > 0, ddlvendor.SelectedValue.Trim(), String.Empty)
        Dim vendorName As String = If(ddlvendor.SelectedIndex > 0, ddlvendor.SelectedItem.Text.Trim(), String.Empty)

        For Each row As DataRow In dtGrid.Rows
            If String.IsNullOrWhiteSpace(Convert.ToString(row("vendor_code"))) Then
                row("vendor_code") = vendorCode
            End If
            If String.IsNullOrWhiteSpace(Convert.ToString(row("vendor_name"))) Then
                row("vendor_name") = vendorName
            End If
        Next
    End Sub

    Private Shared Function GetHeaderColumnValue(ByVal row As DataRow, ParamArray columnNames As String()) As String
        For Each columnName As String In columnNames
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return Convert.ToString(row(columnName)).Trim()
            End If
        Next

        Return String.Empty
    End Function

    Protected Sub ddlRecipe_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim recipeNo As String = String.Empty
        Dim recipeVersion As String = String.Empty
        ParseSelectedRecipe(recipeNo, recipeVersion)
        populateGridGrid(recipeNo, recipeVersion)
    End Sub
End Class
