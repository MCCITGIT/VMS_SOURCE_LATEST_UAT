Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class Product_Formulation
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private Const GridTableKey As String = "VendorRawMatGridTable"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        'btnSubmit.Attributes.Add("onclick", "return validateInputs();")
        btnAdd.Attributes.Add("onclick", "return validateAddRawMaterial();")

        If Not IsPostBack Then
            BrandDetailsListLoad()
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

                    If productName <> "" AndAlso productCode <> "" Then
                        productDetails.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(productName, productCode))
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
        End If
        Return dt
    End Function
    Private Sub BindRawMatGrid()
        'If gvVendorRawMat.Columns.Count > 4 Then
        '    gvVendorRawMat.Columns(4).Visible = (gvVendorRawMat.EditIndex >= 0)
        'End If
        gvVendorRawMat.DataSource = GetGridTable()
        gvVendorRawMat.DataBind()
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        If gvVendorRawMat.EditIndex >= 0 Then
            gvVendorRawMat.EditIndex = -1
        End If

        CaptureGridRates()
        btnSubmit.Visible = True

        If ddlBrand.SelectedIndex <= 0 Then
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "Please select Brand."
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "Please enter Product."
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtrawmatid.Value) Then
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "Please enter Raw Material."
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtRatio.Text) Then
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "Please enter Consumption Ratio."
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtmeasurement.Text) Then
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "Please enter Unit of Measurement."
            Exit Sub
        End If

        Dim dt As DataTable = GetGridTable()
        Dim selectedbrandCode As String = ddlBrand.SelectedValue.Trim()
        Dim selectedProductCode As String = hdnProductCode.Value.Trim()
        Dim selectedRawMatCode As String = txtrawmatid.Value.Trim()

        For Each row As DataRow In dt.Rows
            If Convert.ToString(row("brand_code")).Trim().Equals(selectedbrandCode, StringComparison.OrdinalIgnoreCase) AndAlso
               Convert.ToString(row("rawmat_code")).Trim().Equals(selectedRawMatCode, StringComparison.OrdinalIgnoreCase) Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Selected Raw Material already added."
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

        dr("product_code") = hdnProductCode.Value
        dr("product_name") = hdnProductName.Value

        dr("rawmat_code") = selectedRawMatCode
        dr("rawmat_name") = rawMatName

        dr("ratio") = txtRatio.Text
        dr("unit") = txtmeasurement.Text
        dt.Rows.Add(dr)

        ViewState(GridTableKey) = dt

        gvVendorRawMat.EditIndex = -1
        BindRawMatGrid()
        UpdateRatioTotal()
        ClearControl()

        Dim productCodeToKeep As String = hdnProductCode.Value.Trim()
        Dim productNameToKeep As String = hdnProductName.Value.Trim()

        hdnProductCode.Value = productCodeToKeep
        hdnProductName.Value = productNameToKeep
        txtProductSearch.Text = productNameToKeep
        txtProductSearch.Enabled = False
    End Sub
    Private Sub ClearControl()
        txtSearchText.Text = String.Empty
        txtrawmatid.Value = String.Empty
        txtRatio.Text = String.Empty
        txtmeasurement.Text = String.Empty
        lblErrorMessage.Text = ""
    End Sub
    Private Sub UpdateRatioTotal()
        Dim dt As DataTable = GetGridTable()

        Dim totalRatio As Decimal = 0D

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim ratio As Decimal

                If Decimal.TryParse(Convert.ToString(row("ratio")), ratio) Then
                    totalRatio += ratio
                End If
            Next
        End If

        Dim footerRow As GridViewRow = gvVendorRawMat.FooterRow

        If footerRow IsNot Nothing Then
            Dim lblRatioTotal As Label = TryCast(footerRow.FindControl("lblRatioTotal"), Label)
            Dim lblRatioStatus As Label = TryCast(footerRow.FindControl("lblRatioStatus"), Label)

            If lblRatioTotal IsNot Nothing Then
                lblRatioTotal.Text = totalRatio.ToString("0.00") & "%"
            End If

            If lblRatioStatus IsNot Nothing Then
                If totalRatio > 100D Then
                    lblRatioStatus.Text = "Exceed 100%"
                    lblRatioStatus.ForeColor = System.Drawing.Color.Red
                Else
                    lblRatioStatus.Text = "Within 100%"
                    lblRatioStatus.ForeColor = System.Drawing.Color.Green
                End If
            End If
        End If
    End Sub
    Private Sub BrandDetailsListLoad()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        ds = obj.GetBrandMasterList()

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
    Protected Sub ddlBrand_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlBrand.Enabled = False
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim obj As New OPC_VendorClass()
        Dim rowsAffected As Integer = 0
        Dim totalRatio As Integer = 0
        Try
            If ddlBrand.SelectedIndex <= 0 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please select Brand."
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please enter Product."
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

                Dim ratioValue As Integer = 0
                If Not Integer.TryParse(ratioText, ratioValue) Then
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    lblErrorMessage.Text = "Please enter valid integer Consumption Ratio."
                    Exit Sub
                End If

                totalRatio += ratioText

                Dim dr As DataRow = dt.NewRow()
                dr("fd_rawmat_code") = rawmatcode
                dr("fd_ratio") = ratioText
                dr("fd_unit") = measurement
                dt.Rows.Add(dr)
            Next
            If totalRatio > 100 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Total Consumption Ratio should be within 100%."
                Exit Sub
            End If

            rowsAffected = obj.Insert_Formulation(Val(hdnId.Value), ddlBrand.SelectedValue, hdnProductCode.Value.Trim(), dt, userInfo.userIDEntity)

            If rowsAffected > 0 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Green
                lblErrorMessage.Text = "Submitted Successfully."
                ddlBrand.SelectedIndex = 0
                txtProductSearch.Text = String.Empty
                hdnProductCode.Value = String.Empty
                hdnProductName.Value = String.Empty
                gvVendorRawMat.DataSource = Nothing
                gvVendorRawMat.DataBind()
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Something went wrong. Try again."
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
            hdnProductCode.Value = hdrProductCode
            txtProductSearch.Text = hdrProductDesc & " (" & hdrProductCode & ")"

            ddlBrand.Enabled = False
            txtProductSearch.Enabled = False
            'btnSubmit.Visible = True
            'btnSubmit.Text = "Update"
        End If

        If ds.Tables.Count > 1 AndAlso Not (ds.Tables(1) Is Nothing) AndAlso ds.Tables(1).Rows.Count > 0 Then
            gvVendorRawMat.DataSource = ds.Tables(1)
            gvVendorRawMat.DataBind()

            'Calculate Ratio Total
            Dim totalRatio As Decimal = 0D
            For Each row As DataRow In ds.Tables(1).Rows
                If Not IsDBNull(row("ratio")) AndAlso
                   Not String.IsNullOrWhiteSpace(Convert.ToString(row("ratio"))) Then
                    totalRatio += Convert.ToDecimal(row("ratio"))
                End If
            Next
            'Find footer controls
            Dim lblRatioTotal As Label =
                TryCast(gvVendorRawMat.FooterRow.FindControl("lblRatioTotal"), Label)
            Dim lblRatioStatus As Label =
                TryCast(gvVendorRawMat.FooterRow.FindControl("lblRatioStatus"), Label)
            If lblRatioTotal IsNot Nothing Then
                lblRatioTotal.Text = totalRatio.ToString("0.00") & "%"
            End If

            If lblRatioStatus IsNot Nothing Then
                If totalRatio = 100D Then
                    lblRatioStatus.Text = "Within 100%"
                ElseIf totalRatio < 100D Then
                    lblRatioStatus.Text = "Below 100%"
                Else
                    lblRatioStatus.Text = "Exceeds 100%"
                End If
            End If
        Else
            gvVendorRawMat.DataSource = Nothing
            gvVendorRawMat.DataBind()
        End If
    End Sub
    Protected Sub btnCancel_Click1(sender As Object, e As EventArgs)
        Response.Redirect("~/FormulationMstrList.aspx")
    End Sub

End Class
