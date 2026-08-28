
Imports System.Data
Imports VMS.Web

Partial Class RMProdutwiseConsumption
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            'AddAttributes()

            'Populate_Quarter()
            'PopulateVendor()
            'PopulateVendorBrand(String.Empty)
            If Not (String.IsNullOrEmpty(Request.QueryString("vendorid")) AndAlso String.IsNullOrEmpty(Request.QueryString("productCode"))) Then
                Dim vendorId As String = Request.QueryString("vendorid")

                Dim Productcode As String = Request.QueryString("productCode")

                bindConsumptionDetails(vendorId, Productcode)

            Else
                'btnBack.PostBackUrl = "Home.aspx"
            End If
        End If

    End Sub

#End Region

#Region "Check Login"
    Private Sub CheckLogin()

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

    End Sub
#End Region

#Region "Bind Consumption Details"
    Private Sub bindConsumptionDetails(ByVal vendorcode As String, ByVal productcode As String)
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim userGroup As String
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
            userGroup = userInfo.userGroupCodeEntity
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New QualityControlClass
        Dim ds As New DataSet
        ds = obj.GetRmConsumtionSupplyProductDetails(vendorcode, "", userInfo.userIDEntity.ToString())
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            Dim distinctTable As DataTable = ds.Tables(1).DefaultView.ToTable(True, "productcode", "productname", "vendorid")
            ViewState("RmDtls") = ds.Tables(1)
            rptcusumeProduct.DataSource = distinctTable
            rptcusumeProduct.DataBind()

            'MergeGridRows()
            rptAllocation.DataSource = ds.Tables(0)
            rptAllocation.DataBind()
            rptSupply.DataSource = ds.Tables(0)
            rptSupply.DataBind()
            rptConsumption.DataSource = ds.Tables(0)
            rptConsumption.DataBind()
            rptRemaining.DataSource = ds.Tables(0)
            rptRemaining.DataBind()

            lbvendor.InnerHtml = Convert.ToString(ds.Tables(0).Rows(0)("vendorname"))
            ' lbproduct.InnerHtml = Convert.ToString(ds.Tables(0).Rows(0)("productname"))
            dispatchvol.InnerHtml = Convert.ToString(ds.Tables(1).Rows(0)("total_despatch_production_yield"))
            ' rmquarter.InnerHtml = Convert.ToString(ddlQuarter.SelectedItem)
            ' mpRmConsumtion.Show()
        Else
            'gvLyTyDetails.DataSource = Nothing
            'gvLyTyDetails.DataBind()
            'mpLYTyDetails.Hide()
            ' mpRmConsumtion.Hide()
        End If

    End Sub
#End Region

    Protected Sub gvConsumption_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        'If e.Row.RowType = DataControlRowType.DataRow Then


        '    Dim currentRow As GridViewRow = e.Row
        '    Dim index As Integer = e.Row.RowIndex

        '    If index > 0 Then

        '        Dim previousRow As GridViewRow = gvConsumption.Rows(index - 1)


        '        If currentRow.Cells(0).Text = previousRow.Cells(0).Text Then

        '            If previousRow.Cells(0).RowSpan = 0 Then
        '                previousRow.Cells(0).RowSpan = 2
        '            Else
        '                previousRow.Cells(0).RowSpan += 1
        '            End If

        '            currentRow.Cells(0).Visible = False

        '        End If


        '    End If

    End Sub



    Protected Sub rptcusumeProduct_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim item As RepeaterItem = e.Item
        Dim productcode As String = TryCast(item.FindControl("hdnProductCode"), HiddenField).Value
        Dim gvConsumption As GridView = CType(e.Item.FindControl("gvConsumption"), GridView)
        Dim filterExpression As String = "productcode = '" & productcode.Replace("'", "''") & "'"
        Dim dtrmdetl As DataTable = ViewState("RmDtls")
        If Not String.IsNullOrEmpty(productcode) AndAlso dtrmdetl.Rows.Count > 0 Then
            Dim dr() As DataRow = dtrmdetl.Select(filterExpression)
            If dr.Length > 0 Then
                Dim FilterDtInt As DataTable = dr.CopyToDataTable()
                gvConsumption.DataSource = FilterDtInt
                gvConsumption.DataBind()
            End If
        End If
    End Sub
End Class
