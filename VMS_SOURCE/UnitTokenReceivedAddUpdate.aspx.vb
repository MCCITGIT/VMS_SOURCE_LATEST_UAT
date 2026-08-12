Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class UnitTokenReceivedAddUpdate
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
        Dim out As Integer = 0
        AddAttributes()
        If Not IsPostBack Then

            If (Not (String.IsNullOrEmpty(Request.QueryString("receiveid")))) And (Not (String.IsNullOrEmpty(Request.QueryString("tokenVendor")))) And (Not (String.IsNullOrEmpty(Request.QueryString("requisition")))) And (Not (String.IsNullOrEmpty(Request.QueryString("despatch")))) Then

                PopulateUnit()
                PopulateTokenVendor(ddlTokenVendor)
                PopulateRequisition()
                PopulateDespatch()
                ddlTokenVendor.Items.Clear()
                ddlTokenVendor.Items.Insert(0, New ListItem(Request.QueryString("tokenVendor"), Request.QueryString("tokenVendor")))
                If (Integer.TryParse(Request.QueryString("receiveid"), out)) Then
                    If Not (out = 0) Then
                        lblReqId.Text = out.ToString
                        btnSubmit.Style.Add("display", "none")
                    Else
                        btnSubmit.Style.Remove("display")
                    End If
                End If

                If (Integer.TryParse(Request.QueryString("requisition"), out)) Then
                    ddlRequisition.Items.Clear()
                    ddlRequisition.Items.Insert(0, New ListItem(Request.QueryString("requisition"), Request.QueryString("requisition")))

                End If
                If (Integer.TryParse(Request.QueryString("despatch"), out)) Then
                    ddlDespatch.Items.Clear()
                    ddlDespatch.Items.Insert(0, New ListItem(Request.QueryString("despatch"), Request.QueryString("despatch")))
                End If
                gvRequisitionItemsList.PageIndex = 0
                BindGrid()
            Else
                Response.Redirect("UnitTokenReceivedList.aspx", False)
            End If

        End If

    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        btnSubmit.OnClientClick = "return ValidateSubmit();"
        lblReqId.ForeColor = Drawing.Color.Red
        ddlDespatch.Enabled = False
        ddlRequisition.Enabled = False
        ddlVendorUnit.Enabled = False
        ddlTokenVendor.Enabled = False
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
            Dim obj As New UnitApplicableProductAssignClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetUnitName(Constant.Common.ActiveStatus)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendorUnit.DataSource = dsUnitSet.Tables(0)
                ddlVendorUnit.DataTextField = "unit_name"
                ddlVendorUnit.DataValueField = "unit_code"
                ddlVendorUnit.DataBind()
                If (userInfo.userGroupCodeEntity.Equals("UNIT")) Then
                    ddlVendorUnit.SelectedValue = userInfo.userIDEntity
                    ddlVendorUnit.Enabled = False
                End If
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
        Try
            Dim out As Integer = 0
            Dim obj As New UnitTokenReceivedClass
            Dim dsProductSet As New DataSet

            If ((Not (String.IsNullOrEmpty("receiveid"))) And Integer.TryParse(ddlRequisition.SelectedValue, out) And Integer.TryParse(ddlDespatch.SelectedValue, out)) Then
                dsProductSet = obj.GetDespatchList(Convert.ToInt32(ddlRequisition.SelectedValue), Convert.ToInt32(ddlDespatch.SelectedValue), Convert.ToInt32(Request.QueryString("receiveid")))
                lblReqId.ForeColor = Drawing.Color.Red
                If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                    lblSite.Text = dsProductSet.Tables(0).Rows(0)("trh_site").ToString
                End If

            Else

            End If

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvRequisitionItemsList.DataSource = dsProductSet.Tables(0)
                gvRequisitionItemsList.DataBind()

            Else
                gvRequisitionItemsList.DataSource = Nothing
                gvRequisitionItemsList.DataBind()

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Populate Requisition"
    Private Sub PopulateRequisition()
        CheckLogin()
        Try
            Dim obj As New TokenRequisitionDespatchClass
            Dim dsVendorRequisitionSet As New DataSet
            ddlRequisition.Items.Clear()
            dsVendorRequisitionSet = obj.GetVendorRequisition(Constant.Common.ActiveStatus, ddlTokenVendor.SelectedValue, userInfo.userIDEntity)
            If (Not (dsVendorRequisitionSet Is Nothing) AndAlso dsVendorRequisitionSet.Tables.Count > 0 AndAlso Not (dsVendorRequisitionSet.Tables(0) Is Nothing) AndAlso dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                ddlRequisition.DataSource = dsVendorRequisitionSet.Tables(0)
                ddlRequisition.DataTextField = "trh_id"
                ddlRequisition.DataValueField = "trh_id"
                ddlRequisition.DataBind()


            End If
            ddlRequisition.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region
#Region "Populate Despatch"
    Private Sub PopulateDespatch()
        CheckLogin()
        Try
            Dim out As Integer = 0
            Dim obj As New UnitTokenReceivedClass
            Dim dsVendorRequisitionSet As New DataSet
            ddlDespatch.Items.Clear()
            If (Integer.TryParse(ddlRequisition.SelectedValue, out)) Then
                dsVendorRequisitionSet = obj.GetDespatchList(Convert.ToInt32(ddlRequisition.SelectedValue), 0, 0)
                If (Not (dsVendorRequisitionSet Is Nothing) AndAlso dsVendorRequisitionSet.Tables.Count > 0 AndAlso Not (dsVendorRequisitionSet.Tables(0) Is Nothing) AndAlso dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                    ddlDespatch.DataSource = dsVendorRequisitionSet.Tables(0)
                    ddlDespatch.DataTextField = "tdd_despatch_id"
                    ddlDespatch.DataValueField = "tdd_despatch_id"
                    ddlDespatch.DataBind()

                End If
            End If
            ddlDespatch.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
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
            Dim obj As New UnitTokenReceivedClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetTokenVendorList(userInfo.userIDEntity, Constant.Common.ActiveStatus)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then

                ddl.DataSource = dsUnitSet.Tables(0)
                ddl.DataTextField = "tvm_name"
                ddl.DataValueField = "tvm_code"
                ddl.DataBind()

            End If
            ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region
    Protected Sub gvProductList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRequisitionItemsList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Dim ddl As DropDownList = CType(e.Row.FindControl("ddlTokenVendor"), DropDownList)
            Dim out As Integer = 0
            Dim txtQty As TextBox = CType(e.Row.FindControl("txtRecieveQty"), TextBox)
            txtQty.Attributes.Add("onblur", "return validateQty('" & txtQty.ClientID & "');")
            If ((Not (String.IsNullOrEmpty(Request.QueryString("receiveid")))) And Integer.TryParse(Request.QueryString("receiveid"), out)) Then
                If Not (out = 0) Then
                    Dim lblDespatchedQty As Label = CType(e.Row.FindControl("lblDespatchedQty"), Label)
                    If (lblDespatchedQty.Text.Equals(txtQty.Text)) Then
                        txtQty.Enabled = False
                        gvRequisitionItemsList.HeaderRow.Cells(4).Text = "Received Qty."
                        e.Row.BackColor = Drawing.Color.LightGreen
                    Else
                        txtQty.Enabled = False
                        gvRequisitionItemsList.HeaderRow.Cells(4).Text = "Received Qty."
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
        Dim obj As New UnitTokenReceivedClass
        Dim out As Integer = 0
        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False
        Try

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            Dim dt As New DataTable
            dt.Columns.Add("tred_sku", GetType(String))
            dt.Columns.Add("tred_despatch_qty", GetType(Integer))
            dt.Columns.Add("tred_received_qty", GetType(Integer))
            dt.Columns.Add("created_user", GetType(String))
            dt.Columns.Add("active", GetType(String))
            If (gvRequisitionItemsList.Rows.Count > 0) Then
                For Each gvrow As GridViewRow In gvRequisitionItemsList.Rows
                    Dim hdnUnit As HiddenField = CType(gvrow.FindControl("hdnUnit"), HiddenField)
                    Dim hdnProductId As HiddenField = CType(gvrow.FindControl("hdnProductId"), HiddenField)
                    Dim hdnTokenVendor As HiddenField = CType(gvrow.FindControl("hdnTokenVendor"), HiddenField)
                    Dim hdnDespatchId As HiddenField = CType(gvrow.FindControl("hdnDespatchId"), HiddenField)
                    Dim hdnDespatchedQty As HiddenField = CType(gvrow.FindControl("hdnDespatchedQty"), HiddenField)
                    Dim txtRecieveQty As TextBox = CType(gvrow.FindControl("txtRecieveQty"), TextBox)
                    If ((Not (txtRecieveQty.Text.Equals(String.Empty))) And Integer.TryParse(txtRecieveQty.Text, out)) Then
                        If (Convert.ToInt32(hdnDespatchedQty.Value) < Convert.ToInt32(txtRecieveQty.Text)) Then
                            lblErrorMessage.Text = "Receive quantity can't be greater than despatch qty."
                            Exit Sub
                        End If

                        Dim dr As DataRow = dt.NewRow()
                        dr("tred_sku") = hdnProductId.Value
                        dr("tred_despatch_qty") = Convert.ToInt32(hdnDespatchedQty.Value)
                        dr("tred_received_qty") = Convert.ToInt32(txtRecieveQty.Text)
                        dr("created_user") = userInfo.userIDEntity
                        dr("active") = Constant.Common.ActiveStatus
                        dt.Rows.Add(dr)
                    Else
                        dt.Clear()
                        flag = True
                    End If

                Next
                dt.AcceptChanges()
                If (flag) Then
                    lblErrorMessage.Text = "Invalid quantity."
                Else
                    If (dt.Rows.Count > 0) Then
                        RecordInserted = obj.TokenReceiveInsertUpdate(Convert.ToInt32(ddlDespatch.SelectedValue), Convert.ToInt32(ddlRequisition.SelectedValue), ddlVendorUnit.SelectedValue, userInfo.userIDEntity, Constant.Common.ActiveStatus, ddlTokenVendor.SelectedValue, dt, Constant.Common.Token_Req_Status_Received, sqlConn, sqlTrans)
                        If (RecordInserted > 0) Then
                            sqlTrans.Commit()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location.href='UnitTokenReceivedList.aspx';", True)

                        Else
                            sqlTrans.Rollback()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                        End If
                    Else
                        lblErrorMessage.Text = "Please fill at least one quantity."
                    End If
                End If


            Else
                lblErrorMessage.Text = "No data available."
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
            BindGrid()
        End Try
    End Sub
    Protected Sub ddlTokenVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTokenVendor.SelectedIndexChanged
        PopulateRequisition()
        PopulateDespatch()
        BindGrid()
    End Sub

    Protected Sub ddlRequisition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRequisition.SelectedIndexChanged
        PopulateDespatch()
        BindGrid()
    End Sub
    Protected Sub ddlDespatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDespatch.SelectedIndexChanged
        BindGrid()
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("UnitTokenReceivedList.aspx", False)
    End Sub
End Class
