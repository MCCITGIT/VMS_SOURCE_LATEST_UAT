Imports VMS.Web
Imports System.Data
Imports System.Collections.Generic

Partial Class FormulationMatrixList
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()

        If Not IsPostBack Then
            BindDropdown()
            Binddata()
        End If
    End Sub

    Private Sub CheckLogin()
        If Not (Session(Constant.SessionKeys.UserInfo) Is Nothing) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub BindDropdown()
        BrandDetailsListLoad()
        VendorDetailsListLoad()
    End Sub

    Private Sub BrandDetailsListLoad()
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.BindBrandMasterList()

        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
            If Not ds.Tables(0) Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
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
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetUnitName(Constant.Common.ActiveStatus)

        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
            If Not ds.Tables(0) Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
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

    Private Sub Binddata()
        Dim obj As New OPC_VendorClass()
        Dim productCode As String = hdnSkucode.Value.Trim()
        If productCode = "" Then
            productCode = hdnProductCode.Value.Trim()
        End If

        Dim ds As DataSet = obj.GetFormulationMatrixList(productCode, txtrawmatid.Value, ddlBrand.SelectedValue, ddlvendor.SelectedValue)
        If (ds Is Nothing OrElse ds.Tables.Count = 0) AndAlso hdnSkucode.Value.Trim() <> "" AndAlso hdnProductCode.Value.Trim() <> "" AndAlso
           Not hdnSkucode.Value.Trim().Equals(hdnProductCode.Value.Trim(), StringComparison.OrdinalIgnoreCase) Then
            ds = obj.GetFormulationMatrixList(hdnProductCode.Value.Trim(), txtrawmatid.Value, ddlBrand.SelectedValue, ddlvendor.SelectedValue)
        End If

        Dim table As DataTable = RmGridHelper.GetTable(ds)
        ApplyLookupNames(table)
        RmGridHelper.BindPaged(gvFormulationMatrixList, table)
    End Sub

    Private Sub ApplyLookupNames(ByVal table As DataTable)
        If table Is Nothing Then
            Return
        End If

        EnsureColumn(table, "brand_name")
        EnsureColumn(table, "vendor_name")
        EnsureColumn(table, "product_name")
        EnsureColumn(table, "rawmat_name")

        For Each row As DataRow In table.Rows
            Dim brandCode As String = GetColumnValue(row, "brand_code")
            Dim vendorCode As String = GetColumnValue(row, "vendor_code")
            Dim brandName As String = GetColumnValue(row, "brand_name")
            Dim vendorName As String = GetColumnValue(row, "vendor_name")

            If brandName = "" OrElse brandName.Equals(brandCode, StringComparison.OrdinalIgnoreCase) Then
                Dim lookupBrand As String = GetDropDownText(ddlBrand, brandCode)
                If lookupBrand <> "" Then
                    row("brand_name") = lookupBrand
                End If
            End If

            If vendorName = "" OrElse vendorName.Equals(vendorCode, StringComparison.OrdinalIgnoreCase) Then
                Dim lookupVendor As String = GetDropDownText(ddlvendor, vendorCode)
                If lookupVendor <> "" Then
                    row("vendor_name") = lookupVendor
                End If
            End If

            If GetColumnValue(row, "product_name") = "" Then
                row("product_name") = GetColumnValue(row, "product_code")
            End If
            If GetColumnValue(row, "rawmat_name") = "" Then
                row("rawmat_name") = GetColumnValue(row, "rawmat_code")
            End If
        Next
    End Sub

    Private Shared Sub EnsureColumn(ByVal table As DataTable, ByVal columnName As String)
        If Not table.Columns.Contains(columnName) Then
            table.Columns.Add(columnName, GetType(String))
        End If
    End Sub

    Private Shared Function GetColumnValue(ByVal row As DataRow, ByVal columnName As String) As String
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) OrElse IsDBNull(row(columnName)) Then
            Return String.Empty
        End If
        Return Convert.ToString(row(columnName)).Trim()
    End Function

    Private Shared Function GetDropDownText(ByVal dropdown As DropDownList, ByVal value As String) As String
        If dropdown Is Nothing OrElse String.IsNullOrWhiteSpace(value) Then
            Return String.Empty
        End If

        Dim item As ListItem = dropdown.Items.FindByValue(value)
        If item IsNot Nothing Then
            Return item.Text.Trim()
        End If

        For Each listItem As ListItem In dropdown.Items
            If listItem.Value.Trim().Equals(value.Trim(), StringComparison.OrdinalIgnoreCase) Then
                Return listItem.Text.Trim()
            End If
        Next

        Return String.Empty
    End Function

    Protected Sub gvFormulationMatrixList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFormulationMatrixList.RowCommand
        Try
            If e.CommandName <> "View" Then
                Exit Sub
            End If

            Dim row As GridViewRow = Nothing
            Dim clickedControl As Control = TryCast(e.CommandSource, Control)
            If clickedControl IsNot Nothing Then
                row = TryCast(clickedControl.NamingContainer, GridViewRow)
            End If

            If row Is Nothing Then
                Dim rowIndex As Integer = 0
                If Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) AndAlso rowIndex >= 0 AndAlso rowIndex < gvFormulationMatrixList.Rows.Count Then
                    row = gvFormulationMatrixList.Rows(rowIndex)
                End If
            End If

            If row Is Nothing Then
                Throw New Exception("Unable to determine selected row.")
            End If

            Dim hdnGridProductCode As HiddenField = CType(row.FindControl("hdnGridProductCode"), HiddenField)
            Dim lblProduct As Label = CType(row.FindControl("lblProduct"), Label)

            Dim produCode As String = If(hdnGridProductCode Is Nothing, String.Empty, Convert.ToString(hdnGridProductCode.Value).Trim())
            Dim produName As String = If(lblProduct Is Nothing, String.Empty, Convert.ToString(lblProduct.Text).Trim())

            Dim redirectUrl = "FormulationMatrix.aspx?producode=" & Server.UrlEncode(produCode) & "&produname=" & Server.UrlEncode(produName) & "&skucode=" & Server.UrlEncode(produCode)
            Response.Redirect(redirectUrl, False)
            Context.ApplicationInstance.CompleteRequest()
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Response.Redirect("~/ExceptionPage.aspx")
        End Try
    End Sub

    Protected Sub ImgbtnAdd_Click(sender As Object, e As EventArgs) Handles ImgbtnAdd.Click
        Response.Redirect("~/FormulationMatrix.aspx")
    End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        gvFormulationMatrixList.PageIndex = 0
        Binddata()
    End Sub

    Protected Sub gvFormulationMatrixList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvFormulationMatrixList.PageIndexChanging
        gvFormulationMatrixList.PageIndex = e.NewPageIndex
        Binddata()
    End Sub
End Class
