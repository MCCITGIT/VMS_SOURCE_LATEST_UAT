Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class FormulationMaster
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        btnSubmit.Attributes.Add("onclick", "return validateInputs();")
        If Not IsPostBack Then
            BrandDetailsListLoad()
            RawMaterialListLoad()
            If Not String.IsNullOrWhiteSpace(Request.QueryString("brandcode")) AndAlso
               Not String.IsNullOrWhiteSpace(Request.QueryString("rawcode")) AndAlso
               Not String.IsNullOrWhiteSpace(Request.QueryString("producode")) Then
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
    Private Sub ShadeListLoad()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
            gdShadedtls.DataSource = Nothing
            gdShadedtls.DataBind()
            Exit Sub
        End If

        ds = obj.GetShadeCodeList(hdnProductCode.Value.Trim())

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            gdShadedtls.DataSource = ds.Tables(0)
            gdShadedtls.DataBind()
        Else
            gdShadedtls.DataSource = Nothing
            gdShadedtls.DataBind()
        End If
    End Sub
    Protected Sub btnLoadShade_Click(sender As Object, e As EventArgs) Handles btnLoadShade.Click
        ShadeListLoad()
    End Sub
    Protected Sub gdShadedtls_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdShadedtls.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then Exit Sub

        Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)
        If rowView Is Nothing Then Exit Sub

        Dim txtRatio As TextBox = CType(e.Row.FindControl("txtratio"), TextBox)
        Dim txtMeasurement As TextBox = CType(e.Row.FindControl("txtmeasurement"), TextBox)

        If Not txtRatio Is Nothing AndAlso rowView.DataView.Table.Columns.Contains("opcd_ratio") AndAlso Not IsDBNull(rowView("opcd_ratio")) Then
            txtRatio.Text = Convert.ToString(rowView("opcd_ratio"))
        End If

        If Not txtMeasurement Is Nothing AndAlso rowView.DataView.Table.Columns.Contains("opcd_unit") AndAlso Not IsDBNull(rowView("opcd_unit")) Then
            txtMeasurement.Text = Convert.ToString(rowView("opcd_unit"))
        End If
    End Sub
    Private Sub RawMaterialListLoad()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        ds = obj.GetRawMaterial()

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlRawMat.DataSource = ds
                ddlRawMat.DataTextField = "Raw_Mat_Name"
                ddlRawMat.DataValueField = "Raw_Mat_Code"
                ddlRawMat.DataBind()
            Else
                ddlRawMat.DataSource = Nothing
                ddlRawMat.DataBind()
            End If
            ddlRawMat.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim obj As New OPC_VendorClass()
        Dim rowsAffected As Integer = 0

        Try
            If String.IsNullOrWhiteSpace(ddlBrand.SelectedValue) Then
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Please Select Brand.")
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Please Select Product.")
                Exit Sub
            End If

            If String.IsNullOrWhiteSpace(ddlRawMat.SelectedValue) Then
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Please Select Raw Material.")
                Exit Sub
            End If

            Dim dtFormulation As DataTable = New DataTable()
            dtFormulation.Columns.Add(New DataColumn("opcd_shade_code", GetType(String)))
            dtFormulation.Columns.Add(New DataColumn("opcd_ratio", GetType(Integer)))
            dtFormulation.Columns.Add(New DataColumn("opcd_unit", GetType(String)))

            Dim totalRatio As Integer = 0

            For Each row As GridViewRow In gdShadedtls.Rows
                If row.RowType <> DataControlRowType.DataRow Then
                    Continue For
                End If

                Dim hdnShadecode As HiddenField = CType(row.FindControl("hdnShadecode"), HiddenField)
                Dim txtRatio As TextBox = CType(row.FindControl("txtratio"), TextBox)
                Dim txtMeasurement As TextBox = CType(row.FindControl("txtmeasurement"), TextBox)

                Dim shadeCode As String = If(hdnShadecode Is Nothing, String.Empty, Convert.ToString(hdnShadecode.Value).Trim())
                Dim ratioText As String = If(txtRatio Is Nothing, String.Empty, Convert.ToString(txtRatio.Text).Trim())
                Dim measurement As String = If(txtMeasurement Is Nothing, String.Empty, Convert.ToString(txtMeasurement.Text).Trim())

                If String.IsNullOrWhiteSpace(ratioText) AndAlso String.IsNullOrWhiteSpace(measurement) Then
                    Continue For
                End If

                If String.IsNullOrWhiteSpace(ratioText) Then
                    lblErrorMessage.Text = ""
                    RmActionPopup.ShowError(Me, "Please enter Consumption Ratio for all entered records.")
                    Exit Sub
                End If

                If String.IsNullOrWhiteSpace(measurement) Then
                    lblErrorMessage.Text = ""
                    RmActionPopup.ShowError(Me, "Please enter Unit of Measurement for all entered records.")
                    Exit Sub
                End If

                Dim ratioValue As Integer = 0
                If Not Integer.TryParse(ratioText, ratioValue) Then
                    lblErrorMessage.Text = ""
                    RmActionPopup.ShowError(Me, "Please enter valid integer Consumption Ratio.")
                    Exit Sub
                End If

                totalRatio += ratioValue

                Dim dr As DataRow = dtFormulation.NewRow()
                dr("opcd_shade_code") = shadeCode
                dr("opcd_ratio") = ratioValue
                dr("opcd_unit") = measurement
                dtFormulation.Rows.Add(dr)
            Next

            If dtFormulation.Rows.Count = 0 Then
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Please enter at least one record in the grid.")
                Exit Sub
            End If

            If totalRatio > 100 Then
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Total Consumption Ratio should be within 100%.")
                Exit Sub
            End If

            rowsAffected = obj.InsertFormulation(Val(hdnId.Value), ddlBrand.SelectedValue, ddlRawMat.SelectedValue, hdnProductCode.Value.Trim(), userInfo.userIDEntity, dtFormulation)

            If rowsAffected > 0 Then
                lblErrorMessage.Text = ""
                RmActionPopup.ShowSuccess(Me, "Submitted Successfully.")
                ddlBrand.SelectedIndex = 0
                ddlRawMat.SelectedIndex = 0
                txtProductSearch.Text = String.Empty
                hdnProductCode.Value = String.Empty
                gdShadedtls.DataSource = Nothing
                gdShadedtls.DataBind()
            Else
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Something went wrong. Try again.")
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/FormulationMstrList.aspx", True)
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim path = "~/FormulationMaster.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub
    Private Sub Binddata()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        Dim brandCode As String = Convert.ToString(Request.QueryString("brandcode"))
        Dim rawCode As String = Convert.ToString(Request.QueryString("rawcode"))
        Dim produCode As String = Convert.ToString(Request.QueryString("producode"))

        ds = obj.GetFormulationEditList(brandCode, rawCode, produCode)

        If ds Is Nothing OrElse ds.Tables.Count = 0 Then Exit Sub

        If Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim hdrRow As DataRow = ds.Tables(0).Rows(0)
            hdnId.Value = Convert.ToString(hdrRow("opc_id"))
            Dim hdrBrandCode As String = Convert.ToString(hdrRow("Brand_Code"))
            Dim hdrRawCode As String = Convert.ToString(hdrRow("Raw_Mat_Code"))
            Dim hdrProductCode As String = Convert.ToString(hdrRow("Sku_Code"))
            Dim hdrProductDesc As String = Convert.ToString(hdrRow("Sku_Desc"))

            If ddlBrand.Items.FindByValue(hdrBrandCode) IsNot Nothing Then
                ddlBrand.SelectedValue = hdrBrandCode
            End If

            If ddlRawMat.Items.FindByValue(hdrRawCode) IsNot Nothing Then
                ddlRawMat.SelectedValue = hdrRawCode
            End If

            hdnProductCode.Value = hdrProductCode
            txtProductSearch.Text = hdrProductDesc & " (" & hdrProductCode & ")"
        End If

        If ds.Tables.Count > 1 AndAlso Not (ds.Tables(1) Is Nothing) AndAlso ds.Tables(1).Rows.Count > 0 Then
            Dim dtShade As DataTable = New DataTable()
            dtShade.Columns.Add(New DataColumn("Shade_Code", GetType(String)))
            dtShade.Columns.Add(New DataColumn("Shade_Desc", GetType(String)))
            dtShade.Columns.Add(New DataColumn("opcd_ratio", GetType(Integer)))
            dtShade.Columns.Add(New DataColumn("opcd_unit", GetType(String)))

            For Each srcRow As DataRow In ds.Tables(1).Rows
                Dim dr As DataRow = dtShade.NewRow()
                dr("Shade_Code") = Convert.ToString(srcRow("opcd_shade_code"))
                dr("Shade_Desc") = Convert.ToString(srcRow("Shade_Desc"))
                dr("opcd_ratio") = If(IsDBNull(srcRow("opcd_ratio")), 0, Convert.ToInt32(srcRow("opcd_ratio")))
                dr("opcd_unit") = Convert.ToString(srcRow("opcd_unit"))
                dtShade.Rows.Add(dr)
            Next

            gdShadedtls.DataSource = dtShade
            gdShadedtls.DataBind()
        Else
            gdShadedtls.DataSource = Nothing
            gdShadedtls.DataBind()
        End If
    End Sub
End Class
