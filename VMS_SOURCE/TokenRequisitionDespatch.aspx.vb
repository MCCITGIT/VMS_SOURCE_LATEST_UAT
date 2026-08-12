'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : UnitApplicableVendorAssign.aspx.vb
'Created Date	: 13-09-2018
'Created By	    : Debayan Das
'Version	    : R01.00.00
'Description	: Code behind for Unit Applicable Product Assign Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class TokenRequisitionDespatch
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
        AddAttributes()

        If Not IsPostBack Then

            If Not (String.IsNullOrEmpty(Request.QueryString("despatchid"))) Then

                ddlTokenVendor.Enabled = False
                ddlVendorUnit.Enabled = False
                ddlVendorRequisition.Enabled = False
                txtChallanDate.Enabled = False
                txt_road_permit.Enabled = False
                txt_transporter.Enabled = False
                'txt_truck_no.Enabled = False
                txt_vendor_challan_no.Enabled = False
                aCalendar.Visible = False
                lblReqId.ForeColor = Drawing.Color.Red
                btnSubmit.Style.Add("display", "none")
                BindGrid()
                PopulateProductName()
                GetProductPackSize()
            ElseIf Not (String.IsNullOrEmpty(Request.QueryString("id"))) And Not (String.IsNullOrEmpty(Request.QueryString("unit"))) Then
                ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                ddlPack_Size.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                PopulateUnit()
                ddlVendorUnit.SelectedValue = Request.QueryString("unit")
                PopulateTokenVendor(ddlTokenVendor)
                PopulateRequisition()
                ddlVendorRequisition.Items.Clear()
                ddlVendorRequisition.Items.Insert(0, New ListItem(Request.QueryString("id"), Request.QueryString("id")))
                gvRequisitionItemsList.PageIndex = 0
                PopulateProductName()
                GetProductPackSize()
                ddlVendorUnit.Enabled = False
                ddlVendorRequisition.Enabled = False
                BindGrid()
                lblReqId.ForeColor = Drawing.Color.Red
                Dim flag As Integer = 0
                For Each drow As GridViewRow In gvRequisitionItemsList.Rows
                    If (drow.Enabled) Then
                        flag = 1
                        Exit For
                    Else
                        flag = 0
                    End If

                Next
                Dim flag2 = False
                If (Not (gvRequisitionItemsList.DataSource Is Nothing)) Then
                    If (CType(gvRequisitionItemsList.DataSource, DataTable).Rows.Count > 0) Then
                        flag2 = True
                    Else
                        flag2 = False
                    End If
                Else
                    flag2 = False
                End If
                If ((flag2) AndAlso (flag = 1)) Then
                    btnSubmit.Style.Remove("display")
                Else
                    btnSubmit.Style.Add("display", "none")
                End If
                '    ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                '    ddlPack_Size.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                '    PopulateUnit()
                '    PopulateTokenVendor(ddlTokenVendor)
                '    PopulateRequisition()
                '    gvRequisitionItemsList.PageIndex = 0
                '    PopulateProductName()
                '    GetProductPackSize()

                '    BindGrid()
                '    lblReqId.ForeColor = Drawing.Color.Red
            End If


            End If

    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        btnSubmit.OnClientClick = "return ValidateSubmit();"
        txtChallanDate.Attributes.Add("readonly", "true")
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

#Region "Populate Unit"
    Private Sub PopulateUnit()
        CheckLogin()
        Try
            ddlVendorUnit.Items.Clear()
            Dim obj As New TokenRequisitionDespatchClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetUnitName(Constant.Common.ActiveStatus, userInfo.userIDEntity)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendorUnit.DataSource = dsUnitSet.Tables(0)
                ddlVendorUnit.DataTextField = "unit_name"
                ddlVendorUnit.DataValueField = "unit_code"
                ddlVendorUnit.DataBind()
                If (dsUnitSet.Tables(0).Rows.Count > 0) Then
                    ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            Else
                ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('No New Requision found.');window.location.href='TokenRequisitionList_Vendor.aspx';", True)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub ddlVendorUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorUnit.SelectedIndexChanged

        PopulateRequisition()
        PopulateProductName()
        GetProductPackSize()
        BindGrid()
    End Sub


    Protected Sub ddlVendorRequisition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorRequisition.SelectedIndexChanged

        PopulateProductName()
        GetProductPackSize()
        BindGrid()
    End Sub

    Protected Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduct.SelectedIndexChanged

        GetProductPackSize()
        BindGrid()
    End Sub

    Protected Sub ddlPack_Size_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPack_Size.SelectedIndexChanged

        BindGrid()
    End Sub

#Region "Populate Requisition"
    Private Sub PopulateRequisition()
        CheckLogin()
        Try
            ddlVendorRequisition.Items.Clear()
            Dim obj As New TokenRequisitionDespatchClass
            Dim dsVendorRequisitionSet As New DataSet

            dsVendorRequisitionSet = obj.GetVendorRequisition(Constant.Common.ActiveStatus, userInfo.userIDEntity, ddlVendorUnit.SelectedValue)
            If (Not (dsVendorRequisitionSet Is Nothing) AndAlso dsVendorRequisitionSet.Tables.Count > 0 AndAlso Not (dsVendorRequisitionSet.Tables(0) Is Nothing) AndAlso dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                ddlVendorRequisition.DataSource = dsVendorRequisitionSet.Tables(0)
                ddlVendorRequisition.DataTextField = "trh_id"
                ddlVendorRequisition.DataValueField = "trh_id"
                ddlVendorRequisition.DataBind()
                If (dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                    ddlVendorRequisition.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            Else
                ddlVendorRequisition.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Populate Product Name"
    Private Sub PopulateProductName()
        CheckLogin()
        Try
            ddlProduct.Items.Clear()
            Dim obj As New TokenRequisitionDespatchClass
            Dim dsProductName As New DataSet
            Dim VendorRequisition As Integer = 0
            If Integer.TryParse(ddlVendorRequisition.SelectedValue, VendorRequisition) Then
                dsProductName = obj.GetProductName(Integer.Parse(ddlVendorRequisition.SelectedValue), ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue, 0)
                If (Not (dsProductName Is Nothing) AndAlso dsProductName.Tables.Count > 0 AndAlso Not (dsProductName.Tables(0) Is Nothing) AndAlso dsProductName.Tables(0).Rows.Count > 0) Then
                    ddlProduct.DataSource = dsProductName.Tables(0)
                    ddlProduct.DataTextField = "sku_desc"
                    ddlProduct.DataValueField = "sku_new_code"
                    ddlProduct.DataBind()
                    If (dsProductName.Tables(0).Rows.Count > 0) Then
                        ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                    End If
                Else
                    ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            Else
                ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
            End If
         
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Populate Product Pack Size"
    Private Sub GetProductPackSize()
        CheckLogin()
        Try
            ddlPack_Size.Items.Clear()
            Dim obj As New TokenRequisitionDespatchClass
            Dim dsProductPackSize As New DataSet
            Dim VendorRequisition As Integer = 0
            If Integer.TryParse(ddlVendorRequisition.SelectedValue, VendorRequisition) Then
                dsProductPackSize = obj.GetProductPackSize(Integer.Parse(ddlVendorRequisition.SelectedValue), ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue, 0, ddlProduct.SelectedValue)
                If (Not (dsProductPackSize Is Nothing) AndAlso dsProductPackSize.Tables.Count > 0 AndAlso Not (dsProductPackSize.Tables(0) Is Nothing) AndAlso dsProductPackSize.Tables(0).Rows.Count > 0) Then
                    ddlPack_Size.DataSource = dsProductPackSize.Tables(0)
                    ddlPack_Size.DataTextField = "sku_volume"
                    ddlPack_Size.DataValueField = "packsize"
                    ddlPack_Size.DataBind()
                    If (dsProductPackSize.Tables(0).Rows.Count > 0) Then
                        ddlPack_Size.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                    End If
                Else
                    ddlPack_Size.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            Else
                ddlPack_Size.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
            End If
           
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Dim res As Integer = 0
        Try
            Dim obj As New TokenRequisitionDespatchClass
            Dim dsProductSet As New DataSet
            If Not (String.IsNullOrEmpty(Request.Item("id"))) And Not (String.IsNullOrEmpty(Request.Item("unit"))) Then
                If (Integer.TryParse(ddlVendorRequisition.SelectedValue, res)) Then
                    dsProductSet = obj.GetProductList(ddlVendorRequisition.SelectedValue, ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue, 0, ddlProduct.SelectedValue, ddlPack_Size.SelectedValue)
                    If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                        lblSite.Text = dsProductSet.Tables(0).Rows(0)("trh_site").ToString
                    End If
                    lblReqId.ForeColor = Drawing.Color.Red
                    btnSubmit.Visible = True
                End If

            Else
                If (Integer.TryParse((Request.QueryString("despatchid")), res)) Then


                    dsProductSet = obj.GetProductList(0, ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue, Integer.Parse((Request.QueryString("despatchid"))), ddlProduct.SelectedValue, ddlPack_Size.SelectedValue)
                    lblReqId.ForeColor = Drawing.Color.Red
                    If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                        lblReqId.Text = dsProductSet.Tables(0).Rows(0)("tdh_despatch_id").ToString
                        ddlTokenVendor.Items.Clear()
                        ddlTokenVendor.Items.Insert(0, New ListItem(dsProductSet.Tables(0).Rows(0)("tokenVendor").ToString, dsProductSet.Tables(0).Rows(0)("tokenVendor").ToString))
                        ddlVendorUnit.Items.Clear()
                        ddlVendorUnit.Items.Insert(0, New ListItem(dsProductSet.Tables(0).Rows(0)("unit").ToString, dsProductSet.Tables(0).Rows(0)("unit").ToString))
                        ddlVendorRequisition.Items.Clear()
                        ddlVendorRequisition.Items.Insert(0, New ListItem(dsProductSet.Tables(0).Rows(0)("trd_requisition_id").ToString, dsProductSet.Tables(0).Rows(0)("trd_requisition_id").ToString))
                        txt_transporter.Text = dsProductSet.Tables(0).Rows(0)("tdh_transporter").ToString
                        'txt_truck_no.Text = dsProductSet.Tables(0).Rows(0)("tdh_truck_no").ToString
                        txt_vendor_challan_no.Text = dsProductSet.Tables(0).Rows(0)("tdh_vendor_challan_no").ToString
                        txt_road_permit.Text = dsProductSet.Tables(0).Rows(0)("tdh_road_permit").ToString
                        lblSite.Text = dsProductSet.Tables(0).Rows(0)("trh_site").ToString
                        txtChallanDate.Text = dsProductSet.Tables(0).Rows(0)("tdh_vendor_challan_date").ToString
                    End If

                    btnSubmit.Visible = False

                Else
                    If (Integer.TryParse(ddlVendorRequisition.SelectedValue, res)) Then

                        dsProductSet = obj.GetProductList(ddlVendorRequisition.SelectedValue, ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue, Integer.Parse((Request.QueryString("despatchid"))), ddlProduct.SelectedValue, ddlPack_Size.SelectedValue)
                        lblReqId.ForeColor = Drawing.Color.Red
                        If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                            lblReqId.Text = dsProductSet.Tables(0).Rows(0)("tdh_despatch_id").ToString
                            ddlTokenVendor.Items.Clear()
                            ddlTokenVendor.Items.Insert(0, New ListItem(dsProductSet.Tables(0).Rows(0)("tokenVendor").ToString, dsProductSet.Tables(0).Rows(0)("tokenVendor").ToString))
                            ddlVendorUnit.Items.Clear()
                            ddlVendorUnit.Items.Insert(0, New ListItem(dsProductSet.Tables(0).Rows(0)("unit").ToString, dsProductSet.Tables(0).Rows(0)("unit").ToString))
                            ddlVendorRequisition.Items.Clear()
                            ddlVendorRequisition.Items.Insert(0, New ListItem(dsProductSet.Tables(0).Rows(0)("trd_requisition_id").ToString, dsProductSet.Tables(0).Rows(0)("trd_requisition_id").ToString))
                            txt_transporter.Text = dsProductSet.Tables(0).Rows(0)("tdh_transporter").ToString
                            'txt_truck_no.Text = dsProductSet.Tables(0).Rows(0)("tdh_truck_no").ToString
                            txt_vendor_challan_no.Text = dsProductSet.Tables(0).Rows(0)("tdh_vendor_challan_no").ToString
                            txt_road_permit.Text = dsProductSet.Tables(0).Rows(0)("tdh_road_permit").ToString
                            lblSite.Text = dsProductSet.Tables(0).Rows(0)("trh_site").ToString
                            txtChallanDate.Text = dsProductSet.Tables(0).Rows(0)("tdh_vendor_challan_date").ToString
                        End If

                    End If
                    btnSubmit.Visible = False
                End If
            End If




                If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvRequisitionItemsList.DataSource = dsProductSet.Tables(0)
                gvRequisitionItemsList.DataBind()
                btnSubmit.Visible = True
            Else
                gvRequisitionItemsList.DataSource = Nothing
                gvRequisitionItemsList.DataBind()
                btnSubmit.Visible = False
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRequisitionItemsList.PageIndexChanging
        gvRequisitionItemsList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
#Region "Populate Token Vendor List"
    Private Sub PopulateTokenVendor(ddl As DropDownList)
        CheckLogin()
        Try
            Dim obj As New UnitApplicableVendorAssignClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetTokenVendorList(String.Empty, Constant.Common.ActiveStatus)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then

                ddl.DataSource = dsUnitSet.Tables(0)
                ddl.DataTextField = "tvm_name"
                ddl.DataValueField = "tvm_code"
                ddl.DataBind()
                ddlTokenVendor.SelectedValue = userInfo.userIDEntity
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region
    Protected Sub gvProductList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRequisitionItemsList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim Res As Integer = 0
            If (Integer.TryParse((Request.QueryString("despatchid")), Res)) Then
                gvRequisitionItemsList.HeaderRow.Cells(4).Visible = False
                e.Row.Cells(4).Visible = False
                gvRequisitionItemsList.HeaderRow.Cells(6).Visible = False
                e.Row.Cells(6).Visible = False
                gvRequisitionItemsList.HeaderRow.Cells(3).Visible = False
                e.Row.Cells(3).Visible = False
                Dim output As Integer = 0
                Dim txtDespatchQty As TextBox = CType(e.Row.FindControl("txtDespatchQty"), TextBox)
                txtDespatchQty.Attributes.Add("onblur", "return validateQty('" & txtDespatchQty.ClientID & "');")
                Dim txtQty As Label = CType(e.Row.FindControl("txtQty"), Label)
                Dim lblDespatched As Label = CType(e.Row.FindControl("lblDespatched"), Label)
                If Not (lblDespatched.Text.Equals(String.Empty) And txtQty.Text.Equals(String.Empty)) Then
                    If (Integer.TryParse(lblDespatched.Text, output) And Integer.TryParse(txtQty.Text, Res)) Then
                        If (Res - output) = 0 Then
                            e.Row.Enabled = False
                            e.Row.BackColor = Drawing.Color.LightGreen
                        End If
                    End If
                    If (lblDespatched.Text.Equals("0")) Then
                        e.Row.Enabled = False
                        e.Row.Style.Add("opacity", "0.55")
                    End If
                End If
            Else
                gvRequisitionItemsList.HeaderRow.Cells(5).Visible = False
                e.Row.Cells(5).Visible = False
                Dim output As Integer = 0
                Dim txtDespatchQty As TextBox = CType(e.Row.FindControl("txtDespatchQty"), TextBox)
                txtDespatchQty.Attributes.Add("onblur", "return validateQty('" & txtDespatchQty.ClientID & "');")
                Dim lblPendingQty As Label = CType(e.Row.FindControl("txtPendingQty"), Label)
                If Not (lblPendingQty.Text.Equals(String.Empty)) Then
                    If (Integer.TryParse(lblPendingQty.Text, output)) Then
                        If (output = 0) Then
                            e.Row.Enabled = False
                            e.Row.BackColor = Drawing.Color.LightGreen
                        End If
                    End If
                End If
            End If
        End If



    End Sub
    Protected Sub gvTokenVendorList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRequisitionItemsList.RowCommand

    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        CheckLogin()
        lblErrorMessage.Text = ""
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New TokenRequisitionDespatchClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False
        Try
            If (Not (txt_road_permit.Text.Equals(String.Empty)) And Not (txt_transporter.Text.Equals(String.Empty)) And Not (txt_vendor_challan_no.Text.Equals(String.Empty)) And Not (txtChallanDate.Text.Equals(String.Empty)) And Not (ddlVendorUnit.SelectedValue.Equals(String.Empty)) And Not (ddlVendorRequisition.SelectedValue.Equals(String.Empty))) Then

                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                Dim dt As New DataTable
                dt.Columns.Add("tdd_sku", GetType(String))
                dt.Columns.Add("tdd_requisition_qty", GetType(Integer))
                dt.Columns.Add("tdd_despatch_qty", GetType(Integer))
                If (gvRequisitionItemsList.Rows.Count > 0) Then
                    For Each gvrow As GridViewRow In gvRequisitionItemsList.Rows

                        Dim hdnUnit As HiddenField = CType(gvrow.FindControl("hdnUnit"), HiddenField)
                        Dim hdnProductId As HiddenField = CType(gvrow.FindControl("hdnProductId"), HiddenField)
                        Dim hdnTokenVendor As HiddenField = CType(gvrow.FindControl("hdnTokenVendor"), HiddenField)
                        Dim txtQty As Label = CType(gvrow.FindControl("txtQty"), Label)
                        Dim txtPendingQty As Label = CType(gvrow.FindControl("txtPendingQty"), Label)
                        Dim txtDespatchQty As TextBox = CType(gvrow.FindControl("txtDespatchQty"), TextBox)
                        Dim requisition_qty As Integer = 0
                        Dim despatch_qty As Integer = 0
                        Dim pending_qty As Integer = 0
                        If (Not (txtQty.Text.Equals(String.Empty)) And Not (txtDespatchQty.Text.Equals(String.Empty)) And Not (txtPendingQty.Text.Equals(String.Empty))) Then
                            If Integer.TryParse(txtQty.Text, requisition_qty) And Integer.TryParse(txtDespatchQty.Text, despatch_qty) And Integer.TryParse(txtPendingQty.Text, pending_qty) Then
                                If Not (despatch_qty = 0) Then
                                    If (pending_qty < despatch_qty) Then
                                        lblErrorMessage.Text = "The despatch quantity can't more than pending quantity."
                                        Exit Sub

                                    Else
                                        Dim dr As DataRow = dt.NewRow()
                                        dr("tdd_sku") = hdnProductId.Value
                                        dr("tdd_requisition_qty") = txtQty.Text
                                        dr("tdd_despatch_qty") = txtDespatchQty.Text
                                        dt.Rows.Add(dr)
                                    End If

                                End If

                            Else
                                lblErrorMessage.Text = "The quantity entered is not numeric. Please add a numeric quantity."
                                Exit Sub
                            End If


                        End If

                    Next
                    dt.AcceptChanges()
                    If (dt.Rows.Count > 0) Then
                        'For Each dr As DataRow In dt.Rows
                        '    If (dr("tdd_despatch_qty").ToString.Equals(String.Empty)) Then
                        '        dr("tdd_despatch_qty") = 0
                        '    End If

                        'Next
                        'dt.AcceptChanges()
                        RecordInserted = obj.TokenDespatchInsertUpdate(0, ddlVendorRequisition.SelectedValue, ddlVendorUnit.SelectedValue, userInfo.userIDEntity, txt_transporter.Text.Trim, String.Empty, txt_vendor_challan_no.Text.Trim, FormatDate(Request.Form(txtChallanDate.UniqueID).Trim), txt_road_permit.Text.Trim, userInfo.userIDEntity, Constant.Common.ActiveStatus, Constant.Common.Token_Req_Status_In_Transit, dt, sqlConn, sqlTrans)
                        If (RecordInserted > 0) Then
                            sqlTrans.Commit()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location.href='TokenRequisitionList_Vendor.aspx';", True)

                        Else
                            sqlTrans.Rollback()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                        End If
                    Else
                        lblErrorMessage.Text = "Please fill at least one quantity."
                    End If

                Else
                    lblErrorMessage.Text = "No data available"
                End If

            Else
                lblErrorMessage.Text = "Required field's can't be blank."
            End If
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
            ' BindGrid()
        End Try
    End Sub

#Region "Date Format"

    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime
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
    Protected Sub ddlTokenVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorRequisition.SelectedIndexChanged
        BindGrid()
    End Sub
    Protected Sub txtDespatchQty_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim multiplier As Decimal = GetStandardParameter("TokenMultiplier")
            Dim txt As TextBox = TryCast(sender, TextBox)
            Dim qty As Integer = 0
            If (multiplier > 0) Then

                If (Integer.TryParse(txt.Text, qty)) Then
                    If Not ((qty Mod multiplier) = 0) Then
                        txt.Text = String.Empty
                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "window.alert('" & "Despatch Quantity has be multiple of " & Convert.ToInt32(multiplier).ToString & "." & "');", True)
                    Else

                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "document.getElementById('" & lblErrorMessage.ClientID & "').innerText='';", True)
                    End If
                Else
                    txt.Text = String.Empty
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "document.getElementById('" & lblErrorMessage.ClientID & "').innerText='Invalid Quantity.';", True)

                End If

            Else
                txt.Text = String.Empty
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "document.getElementById('" & lblErrorMessage.ClientID & "').innerText='Invalid Multiplier.';", True)

            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally

        End Try

    End Sub
#Region "Get values for a particular Standard Parameter."

    Private Function GetStandardParameter(ByVal param_name As String) As Decimal

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnStandardParameter As New Common()
        Dim dsStandardParameter As DataSet

        Dim result As Decimal = 0

        Try

            dsStandardParameter = cmnStandardParameter.GetStandardParameterValues(param_name)

            If Not (dsStandardParameter Is Nothing) Then

                If Not (dsStandardParameter.Tables(0).Rows.Count = 0) Then
                    result = Decimal.Parse(dsStandardParameter.Tables(0).Rows(0)("param_decimal_value").ToString)
                Else
                    Dim returnUrl As String = "~/ExceptionPage.aspx"
                    Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    Server.Transfer(returnUrl)
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

        Return result

    End Function

#End Region
    Protected Sub gvRequisitionItemsList_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim txtDespatchQty As TextBox = CType(e.Row.FindControl("txtDespatchQty"), TextBox)
                AddHandler txtDespatchQty.TextChanged, AddressOf txtDespatchQty_TextChanged

        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CheckLogin()
        If Not (String.IsNullOrEmpty(Request.QueryString("despatchid"))) Then
            Response.Redirect("TokenRequisitionList_Vendor.aspx?id=" & ddlVendorRequisition.SelectedValue, False)
        ElseIf Not (String.IsNullOrEmpty(Request.QueryString("id"))) And Not (String.IsNullOrEmpty(Request.QueryString("unit"))) Then
            Response.Redirect("TokenVendorReqList_ForDespatch.aspx?id=" & ddlVendorRequisition.SelectedValue, False)
        End If

    End Sub
End Class
