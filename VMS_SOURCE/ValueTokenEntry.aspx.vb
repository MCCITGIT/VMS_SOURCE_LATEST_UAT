Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VMS.MsSqlDataAccess

Partial Class ValueTokenEntry
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim Obj As TokenRequisitionClass = New TokenRequisitionClass()

#Region "Page Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim lblTitle As Label = CType(Me.Master.FindControl("lblTitle"), Label)
        'lblTitle.Text = "TOKEN REQUISITION ENTRY"
        If (ViewState("Product") Is Nothing) Then
            ViewState("Product") = CreateEstTable()
        End If

        CheckLogin()
        AddAttribute()
        If (Not IsPostBack) Then

            gvProduct.DataSource = Nothing
            gvProduct.DataBind()

            PopulateFactory()
            PopulateType()
            PopulateMonth()
            PopulateYear()

            ddlTokenMonth.SelectedValue = DateTime.Now.Month.ToString("D2")
            ddlTokenYear.SelectedValue = DateTime.Now.Year.ToString()

            ddlTokenMonth.Enabled = False
            ddlTokenYear.Enabled = False

            If (Request.QueryString("r") IsNot Nothing) Then
                Dim ts_session_id As Int32 = 0
                Try
                    ts_session_id = Convert.ToInt32(Request.QueryString("r"))
                    lblReqId.Text = Convert.ToInt32(Request.QueryString("r"))
                Catch ex As Exception
                    Throw ex
                End Try
                LoadForEdit(ts_session_id)

            End If
        End If
    End Sub
#End Region

#Region "Event Handler"
    Protected Sub ddlFactory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFactory.SelectedIndexChanged
        PopulateSite()
        If (ddlSite.Items.Count > 1) Then
        Else
            PopulateVendor()
            PopulateProduct()
            PopulateDenomination()
        End If

        'PopulateVendor()
        'PopulateProduct()
        'PopulateDenomination()
    End Sub

    Protected Sub ddlVendor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVendor.SelectedIndexChanged
        PopulateProduct()
    End Sub

    Protected Sub ddlProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProduct.SelectedIndexChanged
        PopulatePackSize()
        PopulateDenomination()
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        lblErrorMessage.Text = String.Empty
        Dim tm_product As String = String.Empty
        Dim product_name As String = String.Empty
        Dim denomination_hdr As Int64 = 0
        Dim denomination_txt As String = String.Empty

        Dim Factory As String = String.Empty
        If (ddlFactory.SelectedIndex > 0) Then
            Factory = Convert.ToString(ddlFactory.SelectedValue)
            ddlFactory.Enabled = False
        Else
            ddlFactory.Focus()
            lblErrorMessage.Text = "Please select factory."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Exit Sub
        End If

        If (ddlSite.Items.Count > 1) Then
            If (ddlSite.SelectedIndex > 0) Then
                Factory = Convert.ToString(ddlSite.SelectedValue)
                ddlSite.Enabled = False
            Else
                ddlSite.Focus()
                lblErrorMessage.Text = "Please select site."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If
        Else
            ddlSite.Enabled = False
        End If

        If ((ddlProduct.SelectedIndex > 0)) Then
            tm_product = ddlProduct.SelectedValue.ToString()
            product_name = ddlProduct.SelectedItem.Text
        Else
            lblErrorMessage.Text = "Please select product."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Exit Sub
        End If

        Dim tm_pack As String = String.Empty
        Dim pack_name As String = String.Empty

        If (ddlPackSize.SelectedIndex > 0) Then
            tm_pack = ddlPackSize.SelectedValue.ToString()
            pack_name = ddlPackSize.SelectedItem.Text
        Else
            lblErrorMessage.Text = "Please select product."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Exit Sub
        End If
        Dim tm_qty As Int32 = 0
        Try
            tm_qty = Convert.ToInt32(txtQuantity.Text.Trim())
            If (Not hdnFldCartoonSize.Value.Equals(String.Empty)) Then
                If (tm_qty Mod (Convert.ToInt32(hdnFldCartoonSize.Value)) <> 0) Then
                    lblErrorMessage.Text = "Qty must be multiple of " + hdnFldCartoonSize.Value.ToString()
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblErrorMessage.Text = "Qty must be numeric."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Exit Sub
        End Try

        If (ddlDenomination.SelectedIndex > 0) Then
            denomination_hdr = Convert.ToInt64(ddlDenomination.SelectedValue)
            denomination_txt = Convert.ToString(ddlDenomination.SelectedItem.Text)
        Else
            lblErrorMessage.Text = "Please select denomination."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Exit Sub
        End If

        Dim dt As DataTable = ViewState("Product")
        Dim sum As Integer = dt.AsEnumerable.Sum((Function(x) x("tm_qty")))

        If ((sum + tm_qty) > 100000) Then
            lblErrorMessage.Text = "Sum of qty cannot be greater than 100000."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Exit Sub
        End If

        Dim result = dt.AsEnumerable().Where(Function(r) r("tm_product").ToString().Equals(tm_product.Trim) And r("tm_pack").Equals(tm_pack.Trim))

        If (result.Any()) Then
            Dim selectedTable As DataTable = result.CopyToDataTable()
            If (selectedTable IsNot Nothing And dt.Rows.Count > 0) Then
                lblErrorMessage.Text = "This Product & Pack is already added."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If
        End If

        Dim dr As DataRow = dt.NewRow()
        dr("tm_product") = tm_product
        dr("product_name") = product_name
        dr("tm_pack") = tm_pack
        dr("pack_name") = pack_name
        dr("tm_qty") = tm_qty
        dr("denomination_hdr_id") = denomination_hdr
        dr("denomination_hdr_txt") = denomination_txt

        dt.Rows.Add(dr)

        ddlProduct.SelectedIndex = 0
        ddlProduct_SelectedIndexChanged(ddlProduct, e)
        txtQuantity.Text = ""


        gvProduct.DataSource = dt
        gvProduct.DataBind()
        Disabledropdown()

    End Sub

    Protected Sub gvProduct_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvProduct.RowCommand
        If (e.CommandName = "RowDelete") Then
            Dim dt As DataTable = CType(ViewState("Product"), DataTable)
            If dt.Rows.Count > 0 Then
                Dim RowIndex = Convert.ToInt32(e.CommandArgument)
                dt.Rows(RowIndex).Delete()
                dt.AcceptChanges()
                ViewState("Product") = dt
                gvProduct.DataSource = dt
                gvProduct.DataBind()

                Disabledropdown()
            End If
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Try
            sqlConn = CType(DBFactory.GetHelper.OpenConnection, SqlConnection)
            sqlTrans = sqlConn.BeginTransaction
            Dim Factory As String = String.Empty

            If (ddlFactory.SelectedIndex > 0) Then
                Factory = Convert.ToString(ddlFactory.SelectedValue)
            Else
                ddlFactory.Focus()
                lblErrorMessage.Text = "Please select factory."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            If (ddlSite.Items.Count > 1) Then
                If (ddlSite.SelectedIndex > 0) Then
                    Factory = Convert.ToString(ddlSite.SelectedValue)
                Else
                    ddlSite.Focus()
                    lblErrorMessage.Text = "Please select site."
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    Exit Sub
                End If
            End If
            
            Dim Vendor = String.Empty
            If (ddlVendor.SelectedIndex > 0) Then
                Vendor = Convert.ToString(ddlVendor.SelectedValue)
            Else
                ddlVendor.Focus()
                lblErrorMessage.Text = "Please select vendor."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim Type = String.Empty
            If (ddlType.SelectedIndex > 0) Then
                Type = Convert.ToString(ddlType.SelectedValue)
            Else
                ddlType.Focus()
                lblErrorMessage.Text = "Please select type."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim ts_session_month = String.Empty
            If (ddlTokenMonth.SelectedIndex > 0) Then
                ts_session_month = Convert.ToString(ddlTokenMonth.SelectedValue)
            Else
                ddlType.Focus()
                lblErrorMessage.Text = "Please select Token Month."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim Months = Convert.ToInt32(ts_session_month)
            If (Months < DateTime.Now.Month) Then
                ddlType.Focus()
                lblErrorMessage.Text = "Sorry! You cannot select past month."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim ts_session_year = String.Empty
            If (ddlTokenYear.SelectedIndex > 0) Then
                ts_session_year = Convert.ToString(ddlTokenYear.SelectedValue)
            Else
                ddlType.Focus()
                lblErrorMessage.Text = "Please select Token Year."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim dt = CType(ViewState("Product"), DataTable)
            If (dt IsNot Nothing And dt.Rows.Count > 0) Then
                Dim sum As Int64 = dt.AsEnumerable().Sum(Function(x) x("tm_qty"))
                If (sum > 100000) Then
                    lblErrorMessage.Text = "Sum of qty cannot be greater than 100000."
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    Exit Sub
                End If
            Else
                lblErrorMessage.Text = "Please add atleast one product."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            dt.Columns.Remove("product_name")
            dt.Columns.Remove("pack_name")
            dt.Columns.Remove("denomination_hdr_txt")

            Dim returnResult As Int64 = 0

            If (btnSubmit.Text <> "Update") Then
                returnResult = Obj.Insert(Factory, Vendor, Type, ts_session_month, ts_session_year, userInfo.userIDEntity, dt, sqlConn, sqlTrans)
                If (returnResult > 0) Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record saved successfully.');window.location.href='ValueTokenList.aspx';", True)
                    sqlTrans.Commit()
                Else
                    sqlTrans.Rollback()
                    lblErrorMessage.Text = "Error in save!!!"
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                End If
            Else
                Dim RequisitionId As Int32 = 0
                RequisitionId = Convert.ToInt32(hdnID.Value)
                returnResult = Obj.Update(RequisitionId, Factory, Vendor, Type, ts_session_month, ts_session_year, userInfo.userIDEntity, dt, sqlConn, sqlTrans)
                If (returnResult > 0) Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updated successfully.');window.location.href='ValueTokenList.aspx';", True)
                    sqlTrans.Commit()
                Else
                    sqlTrans.Rollback()
                    lblErrorMessage.Text = "Error in update!!!"
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                End If
            End If

        Catch ex As Exception
            If ((sqlTrans IsNot Nothing)) Then
                sqlTrans.Rollback()
            End If
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        Finally
            If ((sqlConn IsNot Nothing)) Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Protected Sub lbtnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("ValueTokenList.aspx")
    End Sub

    Protected Sub ddlPackSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPackSize.SelectedIndexChanged
        PopulateDenomination()
    End Sub
#End Region

#Region "Custom Event"
    Private Sub CheckLogin()
        If (Session(Constant.SessionKeys.UserInfo) IsNot Nothing) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx", True)
        End If
    End Sub

    Public Shared Function CreateEstTable() As DataTable
        Dim estTable As DataTable = New DataTable("Details")
        Dim dtColumn As DataColumn

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "tm_product"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "product_name"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "tm_pack"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "pack_name"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.GetType("System.Int32")
        dtColumn.ColumnName = "tm_qty"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.GetType("System.Int64")
        dtColumn.ColumnName = "denomination_hdr_id"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.GetType("System.String")
        dtColumn.ColumnName = "denomination_hdr_txt"
        estTable.Columns.Add(dtColumn)

        Return estTable
    End Function

    Private Sub AddAttribute()
        btnAdd.Attributes.Add("onclick", "return ValidateAdd('" + ddlProduct.ClientID + "','" + ddlPackSize.ClientID + "','" + ddlDenomination.ClientID + "','" + txtQuantity.ClientID + "','" + lblErrorMessage.ClientID + "','" + btnAdd.ClientID + "' );")
        'btnSubmit.Attributes.Add("onclick", "return ValidateSubmit('" + ddlFactory.ClientID + "','" + ddlVendor.ClientID + "','" + ddlType.ClientID + "','" + ddlTokenMonth.ClientID + "','" + ddlTokenYear.ClientID + "','" + lblErrorMessage.ClientID + "','" + btnSubmit.ClientID + "' );")
    End Sub

    Public Sub LoadForEdit(ByVal ts_session_id As Int32)
        Dim barcode As String = String.Empty
        Dim ds As DataSet
        Try
            ds = Obj.GetDetailsForEdit(ts_session_id)
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                hdnID.Value = ts_session_id.ToString()
                ddlFactory.SelectedValue = ds.Tables(0).Rows(0)("ts_factory_code").ToString()
                ddlFactory_SelectedIndexChanged(ddlFactory, New EventArgs())
                ddlSite.SelectedValue = ds.Tables(0).Rows(0)("uas_site_code").ToString()
                ddlSite_SelectedIndexChanged(ddlSite, New EventArgs())
                ddlSite.Enabled = False
                ddlVendor.SelectedValue = ds.Tables(0).Rows(0)("ts_vendor_code").ToString()
                ddlType.SelectedValue = ds.Tables(0).Rows(0)("tm_type").ToString()
                ddlTokenMonth.SelectedValue = ds.Tables(0).Rows(0)("ts_session_month").ToString()
                ddlTokenYear.SelectedValue = ds.Tables(0).Rows(0)("ts_session_year").ToString()
                barcode = ds.Tables(0).Rows(0)("ts_barcode_generated_yn").ToString()
                PopulateProduct()
            End If

            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(1) Is Nothing) And ds.Tables(1).Rows.Count > 0)) Then
                ViewState("Product") = ds.Tables(1)
                gvProduct.DataSource = ds.Tables(1)
                gvProduct.DataBind()
                Disabledropdown()
            End If

            btnSubmit.Text = "Update"
            If (barcode <> "N") Then
                btnSubmit.Visible = False
                btnAdd.Visible = False
                ddlVendor.Enabled = False
                ddlFactory.Enabled = False
                ddlSite.Enabled = False
                ddlPackSize.Enabled = False
                ddlType.Enabled = False
                ddlTokenMonth.Enabled = False
                ddlTokenYear.Enabled = False
                ddlProduct.Enabled = False
                ddlPackSize.Enabled = False
                txtQuantity.Enabled = False
                gvProduct.Enabled = False
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateType()
        Dim ds As DataSet
        Try
            ds = Obj.GetLovDetails("TOKEN_TYPE")
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlType.DataSource = ds.Tables(0)
                ddlType.DataTextField = "lov_value"
                ddlType.DataValueField = "lov_code"
                ddlType.DataBind()
                ddlType.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                If (ds.Tables(0).Rows.Count = 1) Then
                    ddlType.SelectedValue = ds.Tables(0).Rows(0)("lov_code").ToString()
                End If
            Else
                ddlType.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateMonth()
        Dim ds As DataSet
        Try
            ds = Obj.GetLovDetails("MONTH_NAME")
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlTokenMonth.DataSource = ds.Tables(0)
                ddlTokenMonth.DataTextField = "lov_value"
                ddlTokenMonth.DataValueField = "lov_code"
                ddlTokenMonth.DataBind()
            End If
            ddlTokenMonth.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateYear()
        Try
            Dim Years As Int32 = DateTime.Now.Year
            For index As Integer = 0 To 4 Step 1
                ddlTokenYear.Items.Insert(index, New ListItem(Years.ToString(), Years.ToString(), True))
                Years = Years + 1
            Next
            ddlTokenYear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateFactory()
        Dim ds As DataSet
        Try
            ds = Obj.GetFactory(userInfo.userIDEntity)
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlFactory.DataSource = ds.Tables(0)
                ddlFactory.DataTextField = "depot_name"
                ddlFactory.DataValueField = "appl_depot_code"
                ddlFactory.DataBind()
                ddlFactory.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                If (ds.Tables(0).Rows.Count = 1) Then
                    ddlFactory.SelectedValue = ds.Tables(0).Rows(0)("appl_depot_code").ToString()
                    ddlFactory_SelectedIndexChanged(ddlFactory, New EventArgs())
                    ddlFactory.Enabled = False
                End If
            Else
                ddlFactory.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateSite()
        ddlSite.Items.Clear()
        Try
            If (ddlFactory.SelectedIndex > 0) Then
                Dim Factory As String = ddlFactory.SelectedValue.ToString()
                Dim ds As DataSet
                ds = Obj.GetSite(Factory)
                If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                    ddlSite.DataSource = ds.Tables(0)
                    ddlSite.DataTextField = "uas_site_name"
                    ddlSite.DataValueField = "uas_site_code"
                    ddlSite.DataBind()
                    ddlSite.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    If (ds.Tables(0).Rows.Count = 1) Then
                        ddlSite.SelectedValue = ds.Tables(0).Rows(0)("uas_site_code").ToString()
                        ddlSite_SelectedIndexChanged(ddlSite, New EventArgs())
                    End If
                Else
                    ddlSite.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateVendor()
        ddlVendor.Items.Clear()
        Try
            If (ddlFactory.SelectedIndex > 0) Then
                Dim Factory As String = String.Empty
                If (ddlSite.Items.Count > 1) Then
                    If (ddlSite.SelectedIndex > 0) Then
                        Factory = Convert.ToString(ddlSite.SelectedValue)
                    End If
                Else
                    Factory = ddlFactory.SelectedValue.ToString()
                End If

                Dim ds As DataSet
                ds = Obj.GetVendor(Factory)
                If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                    ddlVendor.DataSource = ds.Tables(0)
                    ddlVendor.DataTextField = "vm_vendor_name"
                    ddlVendor.DataValueField = "fvpp_vendor_code"
                    ddlVendor.DataBind()
                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    If (ds.Tables(0).Rows.Count = 1) Then
                        ddlVendor.SelectedValue = ds.Tables(0).Rows(0)("fvpp_vendor_code").ToString()
                    End If

                    'As needed by client on 06-07-2020 start
                    Try
                        'ddlVendor.SelectedValue = "V2"
                        'ddlVendor.Enabled = False
                        'ddlVendor_SelectedIndexChanged(ddlVendor, New EventArgs())
                    Catch ex As Exception

                    End Try
                    'As needed by client on 06-07-2020 end
                Else
                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub
    Public Sub PopulateProduct()
        ddlPackSize.Items.Clear()
        ddlProduct.Items.Clear()
        ddlDenomination.Items.Clear()
        If (((ddlFactory.SelectedIndex > 0) AndAlso (ddlVendor.SelectedIndex > 0))) Then
            'Dim Factory As String = ddlFactory.SelectedValue.ToString

            Dim Factory As String = String.Empty
            If (ddlSite.Items.Count > 1) Then
                If (ddlSite.SelectedIndex > 0) Then
                    Factory = Convert.ToString(ddlSite.SelectedValue)
                End If
            Else
                Factory = ddlFactory.SelectedValue.ToString()
            End If

            Dim Vendor As String = ddlVendor.SelectedValue.ToString
            Dim ds As DataSet
            Try
                ds = Obj.GetProductForNewReq(Factory, Vendor)
                If ((Not (ds Is Nothing) _
                AndAlso ((ds.Tables.Count > 0) _
                AndAlso (Not (ds.Tables(0) Is Nothing) _
                AndAlso (ds.Tables(0).Rows.Count > 0))))) Then
                    ddlProduct.DataSource = ds.Tables(0)
                    ddlProduct.DataTextField = "pm_product_name"
                    ddlProduct.DataValueField = "fvpp_product_code"
                    ddlProduct.DataBind()
                    ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    If ((ds.Tables(0).Rows.Count = 1)) Then
                        ddlProduct.SelectedValue = ds.Tables(0).Rows(0)("fvpp_product_code").ToString
                        ddlProduct_SelectedIndexChanged(ddlProduct, New EventArgs())
                    End If
                Else
                    ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            Catch ex As Exception
                Dim returnUrl = "~/ExceptionPage.aspx"
                Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                Response.Redirect(returnUrl)
            End Try
        End If
    End Sub

    Public Sub PopulatePackSize()
        ddlPackSize.Items.Clear()
        ddlDenomination.Items.Clear()
        Try
            If (((ddlFactory.SelectedIndex > 0) _
                AndAlso ((ddlVendor.SelectedIndex > 0) _
                AndAlso (ddlProduct.SelectedIndex > 0)))) Then
                'Dim Factory As String = ddlFactory.SelectedValue.ToString

                Dim Factory As String = String.Empty
                If (ddlSite.Items.Count > 1) Then
                    If (ddlSite.SelectedIndex > 0) Then
                        Factory = Convert.ToString(ddlSite.SelectedValue)
                    End If
                Else
                    Factory = ddlFactory.SelectedValue.ToString()
                End If

                Dim Vendor As String = ddlVendor.SelectedValue.ToString
                Dim Product As String = ddlProduct.SelectedValue.ToString
                Dim ds As DataSet
                ds = Obj.GetPackSize(Factory, Vendor, Product)
                If ((Not (ds Is Nothing) _
                    AndAlso ((ds.Tables.Count > 0) _
                    AndAlso (Not (ds.Tables(0) Is Nothing) _
                    AndAlso (ds.Tables(0).Rows.Count > 0))))) Then
                    ddlPackSize.DataSource = ds.Tables(0)
                    ddlPackSize.DataTextField = "psm_pack_size"
                    ddlPackSize.DataValueField = "fvpp_pack_size_code"
                    ddlPackSize.DataBind()
                    ddlPackSize.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    If ((ds.Tables(0).Rows.Count = 1)) Then
                        ddlPackSize.SelectedValue = ds.Tables(0).Rows(0)("fvpp_pack_size_code").ToString
                    End If
                End If
            Else
                ddlPackSize.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateDenomination()
        hdnFldCartoonSize.Value = ""
        ddlDenomination.Items.Clear()
        If (ddlProduct.SelectedIndex > 0 And ddlPackSize.SelectedIndex > 0) Then
            Dim ProductCode As String = ddlProduct.SelectedValue.ToString
            Dim PackSizeCode As String = ddlPackSize.SelectedValue.ToString
            Dim ds As DataSet
            ds = Obj.GetProductDenomination(ProductCode, PackSizeCode)
            If ((Not (ds Is Nothing) _
                            AndAlso ((ds.Tables.Count > 0) _
                            AndAlso (Not (ds.Tables(0) Is Nothing) _
                            AndAlso (ds.Tables(0).Rows.Count > 0))))) Then
                ddlDenomination.DataSource = ds.Tables(0)
                ddlDenomination.DataTextField = "denomination_text_val"
                ddlDenomination.DataValueField = "denomination_hdr_id"
                ddlDenomination.DataBind()
                ddlDenomination.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                If (ds.Tables(0).Rows.Count = 1) Then
                    ddlDenomination.SelectedValue = ds.Tables(0).Rows(0)("denomination_hdr_id").ToString()
                End If
                hdnFldCartoonSize.Value = ds.Tables(0).Rows(0)("cartoon_size").ToString()
            Else
                ddlDenomination.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        End If
    End Sub

    Private Sub Disabledropdown()
        If (gvProduct.Rows.Count > 0) Then
            ddlFactory.Enabled = False
            ddlVendor.Enabled = False
        Else
            ddlFactory.Enabled = False
            ddlVendor.Enabled = True
        End If
    End Sub
#End Region

    Protected Sub ddlSite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSite.SelectedIndexChanged
        PopulateVendor()
        PopulateProduct()
        PopulateDenomination()
    End Sub
End Class
