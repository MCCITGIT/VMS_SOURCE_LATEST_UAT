Imports System.Data
Imports System.Data.SqlTypes
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.IO
Imports VMS.Web
Imports VMS.DataAccess


Partial Class TokenRequestAddUpdate_Factory
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        AddAttribute()
        If Not IsPostBack Then
            PopulateFactoryList()
            PopulateVendorList()
            PopulateVendorList()
            PopulateProductList()
            PopulatePackSizeList()
            PopulateMonth()
            PopulateYear()
            PopulateTokenType()
            PopulateTokenDenomination()
            PopulateRequisitionMonth()
            PopulateRequisitionYear()
            PopulateCartonCapacity()

            gvTokenDetails.DataSource = Nothing
            gvTokenDetails.DataBind()

            If Not Request.QueryString(Constant.SessionKeys.SessionId) Is Nothing Then
                hdnSessionId.Value = Request.QueryString(Constant.SessionKeys.SessionId)
                GetTokenDetails_InUpdate(CType(hdnSessionId.Value, Int64))
                btnSubmit.Text = "Update"
            End If
        End If

    End Sub


#Region "Check Login"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region

#Region "Add Attribute"
    Private Sub AddAttribute()
        'txtUserID.Attributes.Add("onblur", "ValidateUserID('" + txtUserID.ClientID + "')")
        'txtUserID.Attributes.Add("onblur", "compareUserID(this.value);")
        'btnSubmit.OnClientClick = "return ValidateForm('" _
        '                                        & txtUserID.ClientID & "', '" _
        '                                        & txtEmployeesFirstName.ClientID & "', '" _
        '                                        & txtEmployeesLastName.ClientID & "', '" _
        '                                        & ddlUserGroup.ClientID & "', '" _
        '                                        & ddlDepartment.ClientID & "', '" _
        '                                        & txtEmail.ClientID & "', '" _
        '                                        & txtMobilePhoneNo.ClientID & "', '" _
        '                                        & txtDesignation.ClientID & "', '" _
        '                                        & txtJoinDate.ClientID & "', '" _
        '                                        & ddlDepot.ClientID & "', '" _
        '                                        & lblValidationMessage.ClientID & "', '" _
        '                                        & txtEmployeeId.ClientID & "', '" _
        '                                        & btnSubmit.ClientID & "');"
        txtQuantity.Attributes.Add("onblur", "validateNumber('" + txtQuantity.ClientID + "')")
        btnAdd.Attributes.Add("onclick", "return validateAdd();")
        btnSubmit.Attributes.Add("onclick", "return validateSubmit();")


    End Sub
#End Region

#Region "Date Format"
    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime

        If (stringdate.Equals(String.Empty)) Then
            Return SqlDateTime.MinValue
        End If

        If Not (stringdate = String.Empty) Then
            Dim ddate As String() = stringdate.Split("/")
            Dim arrlist As New ArrayList
            Dim index As Integer = 0

            While index <= ddate.Length - 1
                arrlist.Add(ddate(index))
                System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
            End While
            Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(0))
            Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
            Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(2))

            Dim dt As DateTime = New DateTime(yyyy, mm, dd)
            dt = FormatDateTime(dt, DateFormat.LongDate)
            Return dt
        End If

    End Function
#End Region

#Region "Populate Factory List "
    Private Sub PopulateFactoryList()

        Dim obj As New VMS.Web.Common
        Try
            Dim ds As DataSet = obj.GetUserApplicableDepotList(userInfo.userIDEntity, userInfo.userGroupCodeEntity)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlFactory.DataSource = ds.Tables(0)
                    ddlFactory.DataTextField = "depot"
                    ddlFactory.DataValueField = "depot_code"
                    ddlFactory.DataBind()
                    'ddlFactory.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    If ds.Tables(0).Rows.Count = 1 Then
                        ddlFactory.SelectedValue = ds.Tables(0).Rows(0)("depot_code").ToString
                        ddlFactory.Enabled = False
                        PopulateVendorList()
                    End If

                End If
            End If
            ddlFactory.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

#Region "Populate Vendor List "
    Private Sub PopulateVendorList()
        ddlVendor.Items.Clear()
        Dim obj As New TokenRequestAddUpdateMstr()
        Try
            Dim ds As DataSet = obj.GetFactoryApplicableVendorList(ddlFactory.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlVendor.DataSource = ds.Tables(0)
                    ddlVendor.DataTextField = "VendorName"
                    ddlVendor.DataValueField = "vfl_vendor_code"
                    ddlVendor.DataBind()
                    'ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    If ds.Tables(0).Rows.Count = 1 Then
                        ddlVendor.SelectedValue = ds.Tables(0).Rows(0)("vfl_vendor_code").ToString
                        ddlVendor.Enabled = False

                    End If

                End If
            End If
            ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

#Region "Populate Product List "
    Private Sub PopulateProductList()

        Dim obj As New TokenRequestAddUpdateMstr()
        ddlProduct.Items.Clear()
        Try
            Dim ds As DataSet = obj.GetApplicableProductList(ddlFactory.SelectedValue, ddlVendor.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlProduct.DataSource = ds.Tables(0)
                    ddlProduct.DataTextField = "ProductName"
                    ddlProduct.DataValueField = "vfl_product_code"
                    ddlProduct.DataBind()
                    'ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
            ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

#Region "Populate Pack Size List "
    Private Sub PopulatePackSizeList()

        Dim obj As New TokenRequestAddUpdateMstr()
        ddlPackSize.Items.Clear()
        Try
            Dim ds As DataSet = obj.GetApplicablePackSizetList(ddlFactory.SelectedValue, ddlVendor.SelectedValue, ddlProduct.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlPackSize.DataSource = ds.Tables(0)
                    ddlPackSize.DataTextField = "PackSizeName"
                    ddlPackSize.DataValueField = "vfl_product_packsize"
                    ddlPackSize.DataBind()
                    'ddlPackSize.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
            ddlPackSize.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub
#End Region

    Protected Sub ddlFactory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFactory.SelectedIndexChanged
        PopulateVendorList()
        PopulateProductList()
        PopulatePackSizeList()
        PopulateTokenDenomination()
    End Sub

    Protected Sub ddlVendor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVendor.SelectedIndexChanged
        PopulateProductList()
        PopulatePackSizeList()
        PopulateTokenDenomination()
    End Sub

    Protected Sub ddlProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProduct.SelectedIndexChanged
        PopulatePackSizeList()
        PopulateTokenDenomination()
    End Sub


#Region "Populate Month From Lov "
    Private Sub PopulateMonth()

        Dim obj As New VMS.Web.Common
        Dim mstr As New TokenRequestAddUpdate_FactoryMstr()
        Dim DS1 As DataSet
        Try
            Dim ds As DataSet = obj.GetLovDetails("MONTH_NAME", "Y")
            'ddlContractType.Items.Clear()
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlMonth.DataSource = ds.Tables(0)
                    ddlMonth.DataTextField = "lov_value"
                    ddlMonth.DataValueField = "lov_code"
                    ddlMonth.DataBind()

                    DS1 = mstr.GetTokenMonth(Month(Date.Now).ToString("00"))
                    If (Not (DS1 Is Nothing) AndAlso DS1.Tables.Count > 0) Then
                        If (Not (DS1.Tables(0) Is Nothing) AndAlso DS1.Tables(0).Rows.Count > 0) Then

                            ddlMonth.SelectedValue = Convert.ToString(DS1.Tables(0).Rows(0)("lov_code"))
                        End If
                    End If
                    ddlMonth.Enabled = False
                End If
            End If
            ddlMonth.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

#Region "Populate Year From Lov "
    Private Sub PopulateYear()

        Dim obj As New VMS.Web.Common
        Try
            Dim ds As DataSet = obj.GetLovDetails("TOKEN_YEAR", "Y")
            'ddlContractType.Items.Clear()
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlYear.DataSource = ds.Tables(0)
                    ddlYear.DataTextField = "lov_value"
                    ddlYear.DataValueField = "lov_code"
                    ddlYear.DataBind()

                    'ddlYear.SelectedItem.Text = Year(Date.Now)
                    ddlYear.SelectedValue = Right(Year(Date.Now), 1)
                    ddlYear.Enabled = False
                End If
            End If
            ddlYear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

#Region "Populate Token Type Lov "
    Private Sub PopulateTokenType()

        Dim obj As New VMS.Web.Common
        Try
            Dim ds As DataSet = obj.GetLovDetails("TOKEN_TYPE", "Y")
            'ddlContractType.Items.Clear()
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlTokenType.DataSource = ds.Tables(0)
                    ddlTokenType.DataTextField = "lov_value"
                    ddlTokenType.DataValueField = "lov_code"
                    ddlTokenType.DataBind()
                End If
            End If
            ddlTokenType.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

    '#Region "Populate Token Denomination "
    '    Private Sub PopulateTokenDenomination()

    '        Dim obj As New BD_PROLEAD.Web.Common
    '        Dim mstr As New TokenRequestAddUpdate_FactoryMstr()
    '        Dim DS1 As DataSet
    '        Try
    '            Dim ds As DataSet = obj.GetLovDetails("TOKEN_VALUE", "Y")
    '            ddlValue.Items.Clear()
    '            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
    '                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
    '                    ddlValue.DataSource = ds.Tables(0)
    '                    ddlValue.DataTextField = "lov_value"
    '                    ddlValue.DataValueField = "lov_code"
    '                    ddlValue.DataBind()

    '                    DS1 = mstr.GetProductDenominationValue(ddlProduct.SelectedValue, ddlPackSize.SelectedValue)
    '                    If (Not (DS1 Is Nothing) AndAlso DS1.Tables.Count > 0) Then
    '                        If (Not (DS1.Tables(0) Is Nothing) AndAlso DS1.Tables(0).Rows.Count > 0) Then
    '                            ddlValue.SelectedValue = Convert.ToString(DS1.Tables(0).Rows(0)("ppdl_denomination_code"))
    '                            'ddlValue.Enabled = False

    '                        End If
    '                    End If

    '                    ddlValue.Enabled = False

    '                End If
    '            End If
    '            ddlValue.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    '        Catch ex As Exception
    '            Dim returnUrl As String = "~/ExceptionPage.aspx"
    '            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '            Response.Redirect(returnUrl)
    '        End Try


    '    End Sub
    '#End Region

#Region "Populate Token Denomination "
    Private Sub PopulateTokenDenomination()

        Dim obj As New VMS.Web.Common
        Dim mstr As New TokenRequestAddUpdate_FactoryMstr()
        Dim DS1 As DataSet
        Try
            Dim ds As DataSet = mstr.GetProductDenominationValue(ddlProduct.SelectedValue, ddlPackSize.SelectedValue)
            ddlValue.Items.Clear()
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlValue.DataSource = ds.Tables(0)
                    ddlValue.DataTextField = "DenominationDesc"
                    ddlValue.DataValueField = "ppdl_denomination_code"
                    ddlValue.DataBind()

                    'DS1 = mstr.GetProductDenominationValue(ddlProduct.SelectedValue, ddlPackSize.SelectedValue)
                    'If (Not (DS1 Is Nothing) AndAlso DS1.Tables.Count > 0) Then
                    '    If (Not (DS1.Tables(0) Is Nothing) AndAlso DS1.Tables(0).Rows.Count > 0) Then
                    '        ddlValue.SelectedValue = Convert.ToString(DS1.Tables(0).Rows(0)("ppdl_denomination_code"))
                    '        'ddlValue.Enabled = False

                    '    End If
                    'End If

                    If (ds.Tables(0).Rows.Count = 1) Then
                        ddlValue.SelectedValue = Convert.ToString(ds.Tables(0).Rows(0)("ppdl_denomination_code"))
                    End If


                    ddlValue.Enabled = False

                End If
            End If
            ddlValue.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

#Region "Populate Requisition Month From Lov "
    Private Sub PopulateRequisitionMonth()

        Dim obj As New VMS.Web.Common
        Try
            Dim ds As DataSet = obj.GetLovDetails("REQUISITION_MONTH", "Y")
            'ddlContractType.Items.Clear()
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlRequisitionMonth.DataSource = ds.Tables(0)
                    ddlRequisitionMonth.DataTextField = "lov_value"
                    ddlRequisitionMonth.DataValueField = "lov_code"
                    ddlRequisitionMonth.DataBind()

                    ddlRequisitionMonth.SelectedValue = Month(Date.Now).ToString("00")
                    ddlRequisitionMonth.Enabled = False
                End If
            End If
            ddlRequisitionMonth.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

#Region "Populate Requisition Year From Lov "
    Private Sub PopulateRequisitionYear()

        Dim obj As New VMS.Web.Common
        Try
            Dim ds As DataSet = obj.GetLovDetails("REQUISITION_YEAR", "Y")
            'ddlContractType.Items.Clear()
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddlRequisitionYear.DataSource = ds.Tables(0)
                    ddlRequisitionYear.DataTextField = "lov_value"
                    ddlRequisitionYear.DataValueField = "lov_code"
                    ddlRequisitionYear.DataBind()

                    ddlRequisitionYear.SelectedValue = Year(Date.Now)
                    ddlRequisitionYear.Enabled = False
                End If
            End If
            ddlRequisitionYear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim AddedToken As Int32 = 0
        Dim TotalAddedToken As Int32 = 0

        For Each grdvw As GridViewRow In gvTokenDetails.Rows
            Dim hdnQuantity As HiddenField = grdvw.FindControl("hdnQuantity")
            AddedToken = AddedToken + Convert.ToInt32(Val(hdnQuantity.Value))

        Next
        TotalAddedToken = AddedToken + Convert.ToInt32(txtQuantity.Text.Trim())

        If Not (TotalAddedToken > 100000) Then

            ddlFactory.Enabled = False
            ddlVendor.Enabled = False
            ddlRequisitionMonth.Enabled = False
            ddlRequisitionYear.Enabled = False
            ddlTokenType.Enabled = False

            Dim OtherCount As Integer = 0
            For Each gvr As GridViewRow In gvTokenDetails.Rows
                Dim hdnFactoryCode As HiddenField = gvr.FindControl("hdnFactoryCode")
                Dim hdnVendorCode As HiddenField = gvr.FindControl("hdnVendorCode")
                Dim hdnTokenType As HiddenField = gvr.FindControl("hdnTokenType")
                Dim hdnTokenMonth As HiddenField = gvr.FindControl("hdnTokenMonth")
                Dim hdnTokenYear As HiddenField = gvr.FindControl("hdnTokenYear")
                Dim hdnProduct As HiddenField = gvr.FindControl("hdnProduct")
                Dim hdnPackSize As HiddenField = gvr.FindControl("hdnPackSize")
                Dim hdnTokenValue As HiddenField = gvr.FindControl("hdnTokenValue")
                Dim hdnRequisitionMonth As HiddenField = gvr.FindControl("hdnRequisitionMonth")
                Dim hdnRequisitionYear As HiddenField = gvr.FindControl("hdnRequisitionYear")

                Dim FactoryCode As String = ddlFactory.SelectedValue
                Dim VendorCode As String = ddlVendor.SelectedValue
                Dim TokenType As String = ddlTokenType.SelectedValue
                Dim Tokenmonth As String = ddlMonth.SelectedValue
                Dim TokenYear As String = ddlYear.SelectedValue
                Dim Product As String = ddlProduct.SelectedValue
                Dim PackSize As String = ddlPackSize.SelectedValue
                Dim TokenValue As String = ddlValue.SelectedValue
                Dim RequisitionMonth As String = ddlRequisitionMonth.Text
                Dim Requisitionyear As String = ddlRequisitionYear.SelectedValue

                If (FactoryCode = hdnFactoryCode.Value AndAlso VendorCode = hdnVendorCode.Value AndAlso TokenType = hdnTokenType.Value AndAlso Tokenmonth = hdnTokenMonth.Value AndAlso TokenYear = hdnTokenYear.Value AndAlso Product = hdnProduct.Value AndAlso PackSize = hdnPackSize.Value AndAlso TokenValue = hdnTokenValue.Value AndAlso RequisitionMonth = hdnRequisitionMonth.Value AndAlso Requisitionyear = hdnRequisitionYear.Value) Then
                    OtherCount += 1
                End If
            Next
            If OtherCount = 0 Then
                Dim dtList As New DataTable
                Dim dr As DataRow

                dtList.Columns.Add(New DataColumn("tm_factory_code", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_vendor_code", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_type", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_token_month", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_token_year", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_product", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_pack", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_denomination", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_qty", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_requisition_month", GetType(String)))
                dtList.Columns.Add(New DataColumn("tm_requisition_year", GetType(String)))


                dtList.Columns.Add(New DataColumn("factory_code", GetType(String)))
                dtList.Columns.Add(New DataColumn("vendor_code", GetType(String)))
                dtList.Columns.Add(New DataColumn("type", GetType(String)))
                dtList.Columns.Add(New DataColumn("token_month", GetType(String)))
                dtList.Columns.Add(New DataColumn("token_year", GetType(String)))
                dtList.Columns.Add(New DataColumn("product", GetType(String)))
                dtList.Columns.Add(New DataColumn("pack", GetType(String)))
                dtList.Columns.Add(New DataColumn("denomination", GetType(String)))
                dtList.Columns.Add(New DataColumn("qty", GetType(String)))
                dtList.Columns.Add(New DataColumn("requisition_month", GetType(String)))
                dtList.Columns.Add(New DataColumn("requisition_year", GetType(String)))

                dr = dtList.NewRow

                Dim RowIndex As Integer = 0
                For RowIndex = 0 To gvTokenDetails.Rows.Count - 1
                    Dim lbl As Label, hdn As HiddenField

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblFactory")
                    dr("factory_code") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblVendor")
                    dr("vendor_code") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblTokenType")
                    dr("type") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblMonth")
                    dr("token_month") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblYear")
                    dr("token_year") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblproduct")
                    dr("product") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblPacksize")
                    dr("pack") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblValue")
                    dr("denomination") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblQuantity")
                    dr("qty") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblRequisitionMonth")
                    dr("requisition_month") = lbl.Text

                    lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblRequisitionYear")
                    dr("requisition_year") = lbl.Text


                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnFactoryCode")
                    dr("tm_factory_code") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnVendorCode")
                    dr("tm_vendor_code") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenType")
                    dr("tm_type") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenMonth")
                    dr("tm_token_month") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenYear")
                    dr("tm_token_year") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnProduct")
                    dr("tm_product") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnPackSize")
                    dr("tm_pack") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenValue")
                    dr("tm_denomination") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnQuantity")
                    dr("tm_qty") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnRequisitionMonth")
                    dr("tm_requisition_month") = hdn.Value

                    hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnRequisitionYear")
                    dr("tm_requisition_year") = hdn.Value


                    dtList.Rows.Add(dr)
                    dr = dtList.NewRow
                Next
                dr("tm_factory_code") = ddlFactory.SelectedValue
                dr("tm_vendor_code") = ddlVendor.SelectedValue
                dr("tm_type") = ddlTokenType.SelectedValue
                dr("tm_token_month") = ddlMonth.SelectedValue
                dr("tm_token_year") = ddlYear.SelectedValue
                dr("tm_product") = ddlProduct.SelectedValue
                dr("tm_pack") = ddlPackSize.SelectedValue
                dr("tm_denomination") = ddlValue.SelectedValue
                dr("tm_qty") = txtQuantity.Text.Trim()
                dr("tm_requisition_month") = ddlRequisitionMonth.SelectedValue
                dr("tm_requisition_year") = ddlRequisitionYear.SelectedValue
                'dr("amount") = lblCostPerHead_Self.Text
                dr("factory_code") = ddlFactory.SelectedItem
                dr("vendor_code") = ddlVendor.SelectedItem
                dr("type") = ddlTokenType.SelectedItem
                dr("token_month") = ddlMonth.SelectedItem
                dr("token_year") = ddlYear.SelectedItem
                dr("product") = ddlProduct.SelectedItem
                dr("pack") = ddlPackSize.SelectedItem
                dr("denomination") = ddlValue.SelectedItem
                dr("qty") = txtQuantity.Text.Trim()
                dr("requisition_month") = ddlRequisitionMonth.SelectedItem
                dr("requisition_year") = ddlRequisitionYear.SelectedItem

                dtList.Rows.Add(dr)

                gvTokenDetails.DataSource = dtList
                gvTokenDetails.DataBind()
                'ClearOtherDetails()
                lblValidationMessage.Text = ""
            Else
                lblValidationMessage.Text = "Record Already Exists."
            End If

        Else

            lblValidationMessage.Text = "Total quantity against requisition can not exceed more than 1 lac."
        End If



    End Sub

    Protected Sub gvTokenDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTokenDetails.RowCommand
        If e.CommandName = "CmdRemove" Then
            Dim dtList As New DataTable
            Dim dr As DataRow

            Dim row As GridViewRow = CType(((CType(e.CommandSource, Button)).NamingContainer), GridViewRow)
            Dim CmdRemoveRowIndex As Integer = row.RowIndex


            '=================================================================

            dtList.Columns.Add(New DataColumn("tm_factory_code", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_vendor_code", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_type", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_token_month", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_token_year", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_product", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_pack", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_denomination", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_qty", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_requisition_month", GetType(String)))
            dtList.Columns.Add(New DataColumn("tm_requisition_year", GetType(String)))

            dtList.Columns.Add(New DataColumn("factory_code", GetType(String)))
            dtList.Columns.Add(New DataColumn("vendor_code", GetType(String)))
            dtList.Columns.Add(New DataColumn("type", GetType(String)))
            dtList.Columns.Add(New DataColumn("token_month", GetType(String)))
            dtList.Columns.Add(New DataColumn("token_year", GetType(String)))
            dtList.Columns.Add(New DataColumn("product", GetType(String)))
            dtList.Columns.Add(New DataColumn("pack", GetType(String)))
            dtList.Columns.Add(New DataColumn("denomination", GetType(String)))
            dtList.Columns.Add(New DataColumn("qty", GetType(String)))
            dtList.Columns.Add(New DataColumn("requisition_month", GetType(String)))
            dtList.Columns.Add(New DataColumn("requisition_year", GetType(String)))

            dr = dtList.NewRow

            Dim RowIndex As Integer = 0
            For RowIndex = 0 To gvTokenDetails.Rows.Count - 1
                Dim lbl As Label, hdn As HiddenField

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblFactory")
                dr("factory_code") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblVendor")
                dr("vendor_code") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblTokenType")
                dr("type") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblMonth")
                dr("token_month") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblYear")
                dr("token_year") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblproduct")
                dr("product") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblPacksize")
                dr("pack") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblValue")
                dr("denomination") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblQuantity")
                dr("qty") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblRequisitionMonth")
                dr("requisition_month") = lbl.Text

                lbl = gvTokenDetails.Rows(RowIndex).FindControl("lblRequisitionYear")
                dr("requisition_year") = lbl.Text


                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnFactoryCode")
                dr("tm_factory_code") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnVendorCode")
                dr("tm_vendor_code") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenType")
                dr("tm_type") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenMonth")
                dr("tm_token_month") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenYear")
                dr("tm_token_year") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnProduct")
                dr("tm_product") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnPackSize")
                dr("tm_pack") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnTokenValue")
                dr("tm_denomination") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnQuantity")
                dr("tm_qty") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnRequisitionMonth")
                dr("tm_requisition_month") = hdn.Value

                hdn = gvTokenDetails.Rows(RowIndex).FindControl("hdnRequisitionYear")
                dr("tm_requisition_year") = hdn.Value

                dtList.Rows.Add(dr)
                dr = dtList.NewRow
            Next

            '==================================================================

            dtList.Rows.RemoveAt(CmdRemoveRowIndex)

            gvTokenDetails.DataSource = dtList
            gvTokenDetails.DataBind()


        End If

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("~/TokenRequestList_Factory.aspx")
    End Sub

#Region "Populate Token Details"
    Private Sub GetTokenDetails_InUpdate(ByVal SessionId As Int64)

        Dim obj As New TokenRequestAddUpdateMstr()

        Try
            Dim ds As DataSet = obj.GetTokenDetails(SessionId)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then

                    ddlFactory.SelectedValue = Convert.ToString(ds.Tables(0).Rows(0)("tm_factory_code"))
                    ddlVendor.SelectedValue = Convert.ToString(ds.Tables(0).Rows(0)("tm_vendor_code"))
                    ddlRequisitionMonth.SelectedValue = Convert.ToString(ds.Tables(0).Rows(0)("tm_requisition_month"))
                    ddlRequisitionYear.SelectedValue = Convert.ToString(ds.Tables(0).Rows(0)("tm_requisition_year"))
                    ddlTokenType.SelectedValue = Convert.ToString(ds.Tables(0).Rows(0)("tm_type"))

                    ddlFactory.Enabled = False
                    ddlVendor.Enabled = False
                    ddlRequisitionMonth.Enabled = False
                    ddlRequisitionYear.Enabled = False
                    ddlTokenType.Enabled = False

                    If (Convert.ToString(ds.Tables(0).Rows(0)("TokenGenerationStatus")) = "Y") Then
                        btnAdd.Enabled = False
                        btnSubmit.Enabled = False
                    Else
                        btnAdd.Enabled = True
                        btnSubmit.Enabled = True
                    End If

                    PopulateVendorList()
                    PopulateProductList()
                    PopulatePackSizeList()


                    gvTokenDetails.DataSource = ds.Tables(0)
                    gvTokenDetails.DataBind()

                End If
            End If
            'ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        'Dim sqlConn As SqlConnection = Nothing
        'Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim SessionId As Integer = 0
        'Dim DeleteDetails As Integer

        Dim mstr As New TokenRequestAddUpdateMstr()
        Dim Entity As New TokenGenerationEntity


        If Not (btnSubmit.Text = "Update") Then

            Dim xml_hdr As String = "<root>"
            xml_hdr = xml_hdr & "<row>"
            xml_hdr = xml_hdr & "<UserId>" & userInfo.userIDEntity & "</UserId>"
            xml_hdr = xml_hdr & "<Factory>" & ddlFactory.SelectedValue & "</Factory>"
            xml_hdr = xml_hdr & "<Vendor>" & ddlVendor.SelectedValue & "</Vendor>"
            xml_hdr = xml_hdr & "<ReqMonth>" & ddlRequisitionMonth.SelectedValue & "</ReqMonth>"
            xml_hdr = xml_hdr & "<ReqYear>" & ddlRequisitionYear.SelectedValue & "</ReqYear>"            
            xml_hdr = xml_hdr & "</row>"
            xml_hdr = xml_hdr & "</root>"

            Dim xml_dtl As String = "<root>"
            For Each row As GridViewRow In gvTokenDetails.Rows

                Dim hdnTokenType As HiddenField = TryCast(row.FindControl("hdnTokenType"), HiddenField)
                Dim hdnTokenMonth As HiddenField = TryCast(row.FindControl("hdnTokenMonth"), HiddenField)
                Dim hdnTokenYear As HiddenField = TryCast(row.FindControl("hdnTokenYear"), HiddenField)
                Dim hdnProduct As HiddenField = TryCast(row.FindControl("hdnProduct"), HiddenField)
                Dim hdnPackSize As HiddenField = TryCast(row.FindControl("hdnPackSize"), HiddenField)
                Dim hdnTokenValue As HiddenField = TryCast(row.FindControl("hdnTokenValue"), HiddenField)
                Dim hdnQuantity As HiddenField = TryCast(row.FindControl("hdnQuantity"), HiddenField)
                Dim lblSrl As Label = TryCast(row.FindControl("lblSrl"), Label)

                xml_dtl = xml_dtl & "<row>"
                xml_dtl = xml_dtl & "<tm_type>" & hdnTokenType.Value & "</tm_type>"
                xml_dtl = xml_dtl & "<tm_product>" & hdnProduct.Value & "</tm_product>"
                xml_dtl = xml_dtl & "<tm_pack>" & hdnPackSize.Value & "</tm_pack>"
                xml_dtl = xml_dtl & "<tm_denomination>" & hdnTokenValue.Value & "</tm_denomination>"
                xml_dtl = xml_dtl & "<tm_qty>" & hdnQuantity.Value & "</tm_qty>"
                xml_dtl = xml_dtl & "<tm_token_month>" & hdnTokenMonth.Value & "</tm_token_month>"
                xml_dtl = xml_dtl & "<tm_token_year>" & hdnTokenYear.Value & "</tm_token_year>"
                xml_dtl = xml_dtl & "<tm_srl>" & lblSrl.Text.Trim() & "</tm_srl>"
                xml_dtl = xml_dtl & "</row>"
            Next
            xml_dtl = xml_dtl & "</root>"

            numRowsAffected = mstr.SubmitTokenData(SessionId, xml_hdr, xml_dtl)
            If (numRowsAffected > 0) Then
                'sendMail(SessionId)
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location='TokenRequestList_Factory.aspx';", True)
                'lblPopMessage.Text = "Record inserted Successfully."
                'ModalPopupExtender1.Show()
            End If


            '            sqlConn = DBFactory.GetHelper.OpenConnection()
            '            sqlTrans = sqlConn.BeginTransaction()

            '            Try
            '                Dim Factory As String = ddlFactory.SelectedValue
            '                Dim Vendor As String = ddlVendor.SelectedValue
            '                Dim RequisitionMonth As String = ddlRequisitionMonth.SelectedValue
            '                Dim RequisitionYear As String = ddlRequisitionYear.SelectedValue

            '                SessionId = mstr.InsertTokenSession(userInfo.userIDEntity, "Y", Factory, Vendor, RequisitionMonth, RequisitionYear, sqlConn, sqlTrans)

            '                If (SessionId > 0) Then

            '                    DeleteDetails = mstr.DeleteTokenDetails(SessionId, sqlConn, sqlTrans)

            '                    If (DeleteDetails >= 0) Then

            '                        For i = 0 To gvTokenDetails.Rows.Count - 1

            '                            Dim hdnFactoryCode As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnFactoryCode")
            '                            Dim hdnVendorCode As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnVendorCode")
            '                            Dim hdnTokenType As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenType")
            '                            Dim hdnTokenMonth As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenMonth")
            '                            Dim hdnTokenYear As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenYear")
            '                            Dim hdnProduct As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnProduct")
            '                            Dim hdnPackSize As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnPackSize")
            '                            Dim hdnTokenValue As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenValue")
            '                            Dim hdnQuantity As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnQuantity")

            '                            Dim hdnRequisitionMonth As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnRequisitionMonth")
            '                            Dim hdnRequisitionYear As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnRequisitionYear")

            '                            Entity.factory_code = hdnFactoryCode.Value
            '                            Entity.Vendor_code = hdnVendorCode.Value
            '                            Entity.tgtype = hdnTokenType.Value
            '                            Entity.tgmonth = hdnTokenMonth.Value
            '                            Entity.tgyear = hdnTokenYear.Value
            '                            Entity.tgproduct = hdnProduct.Value
            '                            Entity.tgpack = hdnPackSize.Value
            '                            Entity.tgdenomination = hdnTokenValue.Value
            '                            Entity.tgquantity = CType(hdnQuantity.Value, Int32)
            '                            Entity.createduser = userInfo.userIDEntity
            '                            Entity.tgrefsrlno = CType(SessionId, Int64)
            '                            Entity.tgsrlno = i + 1
            '                            Entity.Requisition_month = hdnRequisitionMonth.Value
            '                            Entity.Requisition_year = hdnRequisitionYear.Value

            'numRowsAffected = mstr.InsertTokenDetails(Entity, sqlConn, sqlTrans)

            '                        Next

            '                        If (numRowsAffected > 0) Then
            '                            sqlTrans.Commit()
            '                            'sendMail(SessionId)
            '                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location='TokenRequestList_Factory.aspx';", True)
            '                            'lblPopMessage.Text = "Record inserted Successfully."
            '                            'ModalPopupExtender1.Show()
            '                        Else
            '                            sqlTrans.Rollback()
            '                            GoTo z
            '                        End If

            '                    Else
            '                        sqlTrans.Rollback()
            '                        GoTo z

            '                    End If

            '                Else
            '                    sqlTrans.Rollback()
            '                    GoTo z
            '                End If

            '            Catch ex As Exception
            '                If Not (sqlTrans Is Nothing) Then
            '                    sqlTrans.Rollback()
            '                End If

            '                Dim returnUrl As String = "~/ExceptionPage.aspx"
            '                Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            '                Server.Transfer(returnUrl)
            '                'Finally
            '                '    If Not (sqlConn Is Nothing) Then
            '                '        'sqlConn is set to close state after completing the transaction
            '                '        sqlConn.Close()
            '                '    End If
            '            End Try

            'z:          sqlConn.Close()


        Else
            SessionId = CType(hdnSessionId.Value, Int64)

            Dim xml_hdr As String = "<root>"
            xml_hdr = xml_hdr & "<row>"
            xml_hdr = xml_hdr & "<UserId>" & userInfo.userIDEntity & "</UserId>"
            xml_hdr = xml_hdr & "<Factory>" & ddlFactory.SelectedValue & "</Factory>"
            xml_hdr = xml_hdr & "<Vendor>" & ddlVendor.SelectedValue & "</Vendor>"
            xml_hdr = xml_hdr & "<ReqMonth>" & ddlRequisitionMonth.SelectedValue & "</ReqMonth>"
            xml_hdr = xml_hdr & "<ReqYear>" & ddlRequisitionYear.SelectedValue & "</ReqYear>"
            xml_hdr = xml_hdr & "</row>"
            xml_hdr = xml_hdr & "</root>"

            Dim xml_dtl As String = "<root>"
            For Each row As GridViewRow In gvTokenDetails.Rows

                Dim hdnTokenType As HiddenField = TryCast(row.FindControl("hdnTokenType"), HiddenField)
                Dim hdnTokenMonth As HiddenField = TryCast(row.FindControl("hdnTokenMonth"), HiddenField)
                Dim hdnTokenYear As HiddenField = TryCast(row.FindControl("hdnTokenYear"), HiddenField)
                Dim hdnProduct As HiddenField = TryCast(row.FindControl("hdnProduct"), HiddenField)
                Dim hdnPackSize As HiddenField = TryCast(row.FindControl("hdnPackSize"), HiddenField)
                Dim hdnTokenValue As HiddenField = TryCast(row.FindControl("hdnTokenValue"), HiddenField)
                Dim hdnQuantity As HiddenField = TryCast(row.FindControl("hdnQuantity"), HiddenField)
                Dim lblSrl As Label = TryCast(row.FindControl("lblSrl"), Label)

                xml_dtl = xml_dtl & "<row>"
                xml_dtl = xml_dtl & "<tm_type>" & hdnTokenType.Value & "</tm_type>"
                xml_dtl = xml_dtl & "<tm_product>" & hdnProduct.Value & "</tm_product>"
                xml_dtl = xml_dtl & "<tm_pack>" & hdnPackSize.Value & "</tm_pack>"
                xml_dtl = xml_dtl & "<tm_denomination>" & hdnTokenValue.Value & "</tm_denomination>"
                xml_dtl = xml_dtl & "<tm_qty>" & hdnQuantity.Value & "</tm_qty>"
                xml_dtl = xml_dtl & "<tm_token_month>" & hdnTokenMonth.Value & "</tm_token_month>"
                xml_dtl = xml_dtl & "<tm_token_year>" & hdnTokenYear.Value & "</tm_token_year>"
                xml_dtl = xml_dtl & "<tm_srl>" & lblSrl.Text.Trim() & "</tm_srl>"
                xml_dtl = xml_dtl & "</row>"
            Next
            xml_dtl = xml_dtl & "</root>"

            numRowsAffected = mstr.SubmitTokenData(SessionId, xml_hdr, xml_dtl)
            If (numRowsAffected > 0) Then
                'sendMail(SessionId)
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location='TokenRequestList_Factory.aspx';", True)
                'lblPopMessage.Text = "Record inserted Successfully."
                'ModalPopupExtender1.Show()
            End If


            '            sqlConn = DBFactory.GetHelper.OpenConnection()
            '            sqlTrans = sqlConn.BeginTransaction()

            '            Try

            '                SessionId = CType(hdnSessionId.Value, Int64)

            '                If (SessionId > 0) Then

            '                    DeleteDetails = mstr.DeleteTokenDetails(SessionId, sqlConn, sqlTrans)

            '                    If (DeleteDetails >= 0) Then

            '                        For i = 0 To gvTokenDetails.Rows.Count - 1

            '                            Dim hdnFactoryCode As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnFactoryCode")
            '                            Dim hdnVendorCode As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnVendorCode")
            '                            Dim hdnTokenType As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenType")
            '                            Dim hdnTokenMonth As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenMonth")
            '                            Dim hdnTokenYear As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenYear")
            '                            Dim hdnProduct As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnProduct")
            '                            Dim hdnPackSize As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnPackSize")
            '                            Dim hdnTokenValue As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnTokenValue")
            '                            Dim hdnQuantity As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnQuantity")
            '                            Dim hdnRequisitionMonth As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnRequisitionMonth")
            '                            Dim hdnRequisitionYear As HiddenField = gvTokenDetails.Rows(i).FindControl("hdnRequisitionYear")

            '                            Entity.factory_code = hdnFactoryCode.Value
            '                            Entity.Vendor_code = hdnVendorCode.Value
            '                            Entity.tgtype = hdnTokenType.Value
            '                            Entity.tgmonth = hdnTokenMonth.Value
            '                            Entity.tgyear = hdnTokenYear.Value
            '                            Entity.tgproduct = hdnProduct.Value
            '                            Entity.tgpack = hdnPackSize.Value
            '                            Entity.tgdenomination = hdnTokenValue.Value
            '                            Entity.tgquantity = CType(hdnQuantity.Value, Int32)
            '                            Entity.createduser = userInfo.userIDEntity
            '                            Entity.tgrefsrlno = CType(SessionId, Int64)
            '                            Entity.tgsrlno = i + 1
            '                            Entity.Requisition_month = hdnRequisitionMonth.Value
            '                            Entity.Requisition_year = hdnRequisitionYear.Value

            '                            numRowsAffected = mstr.InsertTokenDetails(Entity, sqlConn, sqlTrans)

            '                        Next

            '                        If (numRowsAffected > 0) Then
            '                            sqlTrans.Commit()
            '                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');", True)
            '                            'lblPopMessage.Text = "Record inserted Successfully."
            '                            'ModalPopupExtender1.Show()
            '                        Else
            '                            sqlTrans.Rollback()
            '                            GoTo z
            '                        End If

            '                    Else
            '                        sqlTrans.Rollback()
            '                        GoTo y

            '                    End If

            '                Else
            '                    sqlTrans.Rollback()
            '                    GoTo y
            '                End If

            '            Catch ex As Exception
            '                Dim returnUrl As String = "~/ExceptionPage.aspx"
            '                Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            '                Server.Transfer(returnUrl)
            '                'Finally
            '                '    If Not (sqlConn Is Nothing) Then
            '                '        'sqlConn is set to close state after completing the transaction
            '                '        sqlConn.Close()
            '                '    End If
            '            End Try

            'y:          sqlConn.Close()

        End If

    End Sub

#Region "Populate Carton Capacity "
    Private Sub PopulateCartonCapacity()

        Dim ds As DataSet

        Try
            Dim mstr As New TokenRequestAddUpdate_FactoryMstr()
            ds = mstr.GetKartonCapacity("KARTON_CAPACITY")

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    hdnCartonCapacity.Value = Convert.ToString(ds.Tables(0).Rows(0)(0))

                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try


    End Sub
#End Region


    Protected Sub ddlPackSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPackSize.SelectedIndexChanged
        PopulateTokenDenomination()
    End Sub

    '#Region "Sent Mail"
    '    Public Sub sendMail(ByVal ReqId As String)
    '        Dim email_mstr As EmailMaster = New EmailMaster()

    '        Try
    '            Dim result As String = String.Empty

    '            Dim subject As String = "New Requisition Added" + " (  - Factory - " + ddlFactory.SelectedItem.ToString() + " - Requisition Id - " + ReqId

    '            Dim body As String = Environment.NewLine + Environment.NewLine _
    '             + " New Requisition Request added in portal by Factory - " + ddlFactory.SelectedItem.ToString() _
    '             + Environment.NewLine _
    '             + Environment.NewLine _
    '             + "Kindly check in the Portal." _
    '             + Environment.NewLine _
    '             + "====================================================================="

    '            Dim ds As DataSet
    '            Dim obj As New TokenRequestAddUpdate_FactoryMstr()
    '            ds = obj.GetMailIds("TOKEN_REQ_MAIL_ID")
    '            result = email_mstr.sendEMail( _
    '             ds.Tables(0).Rows(0)("MailIds_To").ToString, _
    '             ds.Tables(0).Rows(0)("MailIds_To").ToString, _
    '             String.Empty, _
    '             subject, _
    '             body)
    '            'result = email_mstr.sendEMail( _
    '            '"bmsamanta@gmail.com", _
    '            '"bmsamanta@gmail.com", _
    '            'String.Empty, _
    '            'subject, _
    '            'body)
    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '#End Region

End Class
