Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class FormulationMstrList
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
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub BindDropdown()
        BrandDetailsListLoad()
        RawMaterialListLoad()
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
    Private Sub Binddata()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        ds = obj.GetFormulationDataList(ddlBrand.SelectedValue, ddlRawMat.SelectedValue, hdnProductCode.Value)

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                gvFormulationList.DataSource = ds
                gvFormulationList.DataBind()
            Else
                gvFormulationList.DataSource = Nothing
                gvFormulationList.DataBind()
            End If
        End If
    End Sub
    Protected Sub gvFormulationList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFormulationList.RowCommand
        Try
            If e.CommandName = "View" Then
                Dim row As GridViewRow = Nothing
                Dim clickedControl As Control = TryCast(e.CommandSource, Control)
                If Not clickedControl Is Nothing Then
                    row = TryCast(clickedControl.NamingContainer, GridViewRow)
                End If

                If row Is Nothing Then
                    Dim rowIndex As Integer = 0
                    If Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) AndAlso rowIndex >= 0 AndAlso rowIndex < gvFormulationList.Rows.Count Then
                        row = gvFormulationList.Rows(rowIndex)
                    End If
                End If

                If row Is Nothing Then
                    Throw New Exception("Unable to determine selected row.")
                End If

                Dim hdnBrandCode As HiddenField = CType(row.FindControl("hdnbrandcode"), HiddenField)
                Dim hdnRawCode As HiddenField = CType(row.FindControl("hdnRawCode"), HiddenField)
                Dim hdnSkuCode As HiddenField = CType(row.FindControl("hdnskucode"), HiddenField)

                Dim brandCode As String = If(hdnBrandCode Is Nothing, String.Empty, Convert.ToString(hdnBrandCode.Value))
                Dim rawCode As String = If(hdnRawCode Is Nothing, String.Empty, Convert.ToString(hdnRawCode.Value))
                Dim produCode As String = If(hdnSkuCode Is Nothing, String.Empty, Convert.ToString(hdnSkuCode.Value))

                Dim redirectUrl = "FormulationMaster.aspx?brandcode=" & Server.UrlEncode(brandCode) & "&rawcode=" & Server.UrlEncode(rawCode) & "&producode=" & Server.UrlEncode(produCode)
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
    Protected Sub ImgbtnAdd_Click(sender As Object, e As EventArgs) Handles ImgbtnAdd.Click
        Response.Redirect("~/FormulationMaster.aspx")
    End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        Binddata()
    End Sub
    Protected Sub gvFormulationList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvFormulationList.PageIndexChanging
        gvFormulationList.PageIndex = e.NewPageIndex
        Binddata()
    End Sub
End Class
