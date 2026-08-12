Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class TokenReceiveEntry
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim Obj As TokenReceiveClass = New TokenReceiveClass()
    Dim entity As TokenReceiveEntity = New TokenReceiveEntity()

#Region "Page Load Event"
    Private Sub TokenReceiveEntry_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckLogin()
        If (Not IsPostBack) Then
            PopulateFactory()
            ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            ddlPackSize.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If (Request.QueryString("td") IsNot Nothing) Then
                Dim trh_id As Int32 = 0
                Try
                    trh_id = Convert.ToInt32(Request.QueryString("td"))
                    LoadForEdit(trh_id)
                    ddlFactory.Enabled = False
                    ddlVendor.Enabled = False
                    btnSubmit.Visible = False
                    gvProduct.Enabled = False
                Catch ex As Exception
                    Response.Redirect("TokenReceiveList.aspx")
                End Try
            End If
        End If
    End Sub
#End Region

#Region "Event Handler"
    Protected Sub ddlFactory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFactory.SelectedIndexChanged
        PopulateSite()
        PopulateVendor()
        PopulateProduct()
        PopulatePackSize()
        PopulateDespatchedCartonList()
    End Sub
    Private Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
        PopulateProduct()
        PopulateDespatchedCartonList()
    End Sub

    Private Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduct.SelectedIndexChanged
        PopulatePackSize()
        PopulateDespatchedCartonList()
    End Sub

    Private Sub ddlPackSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPackSize.SelectedIndexChanged
        PopulateDespatchedCartonList()
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Try
            Dim tdh_factory_code As String = String.Empty
            If (ddlFactory.SelectedIndex > 0) Then
                tdh_factory_code = ddlSite.SelectedValue.ToString()
            Else
                ddlFactory.Focus()
                lblErrorMessage.Text = "Please select site."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim tdh_vendor_code = String.Empty
            If (ddlVendor.SelectedIndex > 0) Then
                tdh_vendor_code = Convert.ToString(ddlVendor.SelectedValue)
            Else
                ddlVendor.Focus()
                lblErrorMessage.Text = "Please select vendor."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            If gvProduct.Rows.Count <= 0 Then
                gvProduct.Focus()
                lblErrorMessage.Text = "Please select at least one item."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim created_user = userInfo.userIDEntity
            Dim dtlTokenReceive As List(Of DtlTokenReceive) = New List(Of DtlTokenReceive)()

            entity.trh_factory_code = tdh_factory_code
            entity.trh_vendor_code = tdh_vendor_code
            entity.created_user = created_user.ToString()
            For index As Integer = 0 To gvProduct.Rows.Count - 1
                Dim chbxCarton As CheckBox = CType(gvProduct.Rows(index).FindControl("chbxCarton"), CheckBox)
                If chbxCarton.Checked Then
                    Dim lblRequisitionId As Label = CType(gvProduct.Rows(index).FindControl("lblRequisitionId"), Label)
                    Dim lblCartonId As Label = CType(gvProduct.Rows(index).FindControl("lblCartonId"), Label)
                    Dim hdnMonth As HiddenField = CType(gvProduct.Rows(index).FindControl("hdnMonth"), HiddenField)
                    Dim lblTokenYear As Label = CType(gvProduct.Rows(index).FindControl("lblTokenYear"), Label)
                    Dim lblQty As Label = CType(gvProduct.Rows(index).FindControl("lblQty"), Label)
                    Dim txtReceiveQty As TextBox = CType(gvProduct.Rows(index).FindControl("txtReceiveQty"), TextBox)

                    Dim trd_carton_id = Convert.ToInt32(lblCartonId.Text.Trim())
                    Dim trd_session_id = Convert.ToInt32(lblRequisitionId.Text.Trim())
                    Dim trd_token_month = hdnMonth.Value.Trim()
                    Dim trd_token_year = lblTokenYear.Text.Trim()
                    Dim trd_qty = Convert.ToInt32(lblQty.Text.Trim())
                    Dim trd_receive_qty = 0

                    Try
                        trd_receive_qty = Convert.ToInt32(txtReceiveQty.Text.Trim())
                    Catch ex As Exception
                        txtReceiveQty.Focus()
                        lblErrorMessage.Text = "All Receipt qty must be whole number."
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red
                        Exit Sub
                    End Try

                    If (trd_receive_qty > trd_qty) Then
                        txtReceiveQty.Focus()
                        lblErrorMessage.Text = "Receipt qty cannot be greater than despatched qty."
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red
                        Exit Sub
                    End If

                    dtlTokenReceive.Add(New DtlTokenReceive With {.trd_carton_id = trd_carton_id,
                        .trd_session_id = trd_session_id,
                        .trd_token_month = trd_token_month,
                        .trd_token_year = trd_token_year,
                        .trd_qty = trd_qty,
                        .trd_receive_qty = trd_receive_qty})
                End If
            Next

            If (dtlTokenReceive.Count > 0) Then
                entity.dtlTokenReceive = dtlTokenReceive
            Else
                ddlVendor.Focus()
                lblErrorMessage.Text = "Please select atleast one Carton."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                Exit Sub
            End If

            sqlConn = CType(DBFactory.GetHelper.OpenConnection, SqlConnection)
            sqlTrans = sqlConn.BeginTransaction
            Dim returnResult = 0
            returnResult = Obj.Insert(entity, sqlConn, sqlTrans)

            If (returnResult > 0) Then
                sqlTrans.Commit()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Receipt successfully.');window.location.href='TokenReceiveList.aspx';", True)
            Else
                sqlTrans.Rollback()
                lblErrorMessage.Text = "Error in Receipt!!!"
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
            End If

        Catch ex As Exception
            If sqlTrans IsNot Nothing Then
                sqlTrans.Rollback()
            End If
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        Finally
            If sqlConn IsNot Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Custom Method"
    Private Sub CheckLogin()
        If (Session(Constant.SessionKeys.UserInfo) IsNot Nothing) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx", True)
        End If
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
                Dim Obj As TokenRequisitionClass = New TokenRequisitionClass()
                ds = Obj.GetSite(Factory)
                If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                    ddlSite.DataSource = ds.Tables(0)
                    ddlSite.DataTextField = "uas_site_name"
                    ddlSite.DataValueField = "uas_site_code"
                    ddlSite.DataBind()
                    ddlSite.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    If (ds.Tables(0).Rows.Count = 1) Then
                        ddlSite.SelectedValue = ds.Tables(0).Rows(0)("uas_site_code").ToString()
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

    'Public Sub PopulateVendor()
    '    ddlVendor.Items.Clear()
    '    Try
    '        If (ddlFactory.SelectedIndex > 0) Then
    '            Dim Factory As String = ddlFactory.SelectedValue.ToString()
    '            Dim ds As DataSet
    '            ds = Obj.GetVendor(Factory)
    '            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
    '                ddlVendor.DataSource = ds.Tables(0)
    '                ddlVendor.DataTextField = "vm_vendor_name"
    '                ddlVendor.DataValueField = "fvpp_vendor_code"
    '                ddlVendor.DataBind()
    '                ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    '                If (ds.Tables(0).Rows.Count = 1) Then
    '                    ddlVendor.SelectedValue = ds.Tables(0).Rows(0)("fvpp_vendor_code").ToString()
    '                    PopulateProduct()
    '                End If
    '            Else
    '                ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim returnUrl = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '        Response.Redirect(returnUrl)
    '    End Try
    'End Sub

    Public Sub PopulateVendor()
        ddlVendor.Items.Clear()
        Try
            If (ddlFactory.SelectedIndex > 0) Then
                ' Dim Factory As String = ddlFactory.SelectedValue.ToString()

                Dim Factory As String = String.Empty
                If (ddlSite.Items.Count > 1) Then
                    If (ddlSite.SelectedIndex > 0) Then
                        Factory = Convert.ToString(ddlSite.SelectedValue)
                    End If
                Else
                    Factory = ddlFactory.SelectedValue.ToString()
                End If

                Dim ds As DataSet
                Dim Obj As TokenRequisitionClass = New TokenRequisitionClass()
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
        If (((ddlFactory.SelectedIndex > 0) AndAlso (ddlVendor.SelectedIndex > 0))) Then
            Dim Factory As String = ddlFactory.SelectedValue.ToString
            Dim Site As String = ddlSite.SelectedValue.ToString
            Dim Vendor As String = ddlVendor.SelectedValue.ToString
            Dim ds As DataSet
            Try
                Dim Obj As TokenRequisitionClass = New TokenRequisitionClass()
                ds = Obj.GetProductForNewReq(Site, Vendor)
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
                        PopulatePackSize()
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
        Try
            If (((ddlFactory.SelectedIndex > 0) _
                AndAlso ((ddlVendor.SelectedIndex > 0) _
                AndAlso (ddlProduct.SelectedIndex > 0)))) Then
                Dim Factory As String = ddlFactory.SelectedValue.ToString
                Dim Site As String = ddlSite.SelectedValue.ToString
                Dim Vendor As String = ddlVendor.SelectedValue.ToString
                Dim Product As String = ddlProduct.SelectedValue.ToString
                Dim ds As DataSet
                Dim Obj As TokenRequisitionClass = New TokenRequisitionClass()
                ds = Obj.GetPackSize(Site, Vendor, Product)
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
                        PopulateDespatchedCartonList()
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

    Public Sub PopulateDespatchedCartonList()
        gvProduct.DataSource = Nothing
        gvProduct.DataBind()
        If (ddlVendor.SelectedIndex > 0 And ddlFactory.SelectedIndex > 0) Then
            Dim Vendor = ddlVendor.SelectedValue.ToString()
            Dim Factory = ddlFactory.SelectedValue.ToString()
            Dim Site = ddlSite.SelectedValue.ToString()
            Try
                Dim ds = Obj.GetDespatchedCartonList(Site, Vendor)
                If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing))) Then
                    Dim dataView As DataView = ds.Tables(0).DefaultView
                    If dataView Is Nothing Then
                        Exit Sub
                    End If

                    If ddlProduct.SelectedIndex > 0 Then
                        dataView.RowFilter = "ProductName = '" + ddlProduct.SelectedItem.ToString().Trim() + "'"
                    End If

                    If (ddlProduct.SelectedIndex > 0 And ddlPackSize.SelectedIndex > 0) Then
                        dataView.RowFilter = "PackSize = '" + ddlPackSize.SelectedItem.ToString().Trim() + "' and ProductName='" + ddlProduct.SelectedItem.ToString().Trim() + "'"
                    End If

                    gvProduct.DataSource = dataView
                    gvProduct.DataBind()

                End If
            Catch ex As Exception
                Dim returnUrl = "~/ExceptionPage.aspx"
                Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                Response.Redirect(returnUrl)
            End Try
        End If

    End Sub
    Public Sub LoadForEdit(ByVal trh_id As Int32)
        Dim Vendor = ddlVendor.SelectedValue.ToString()
        Dim Factory = ddlFactory.SelectedValue.ToString()
        Dim ds As DataSet
        Try
            ds = Obj.GetDetailsForEdit(trh_id)
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlSite.SelectedValue = ds.Tables(0).Rows(0)("trh_factory_code").ToString()
                ddlSite_SelectedIndexChanged(ddlFactory, New EventArgs())
                ddlVendor.SelectedValue = ds.Tables(0).Rows(0)("trh_vendor_code").ToString()

                gvProduct.DataSource = ds.Tables(1)
                gvProduct.DataBind()
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub
#End Region

    Protected Sub ddlSite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSite.SelectedIndexChanged
        PopulateVendor()
        PopulateProduct()
        PopulatePackSize()
        'PopulateDespatchedCartonList()
    End Sub

    'Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
    '    PopulateDespatchedCartonList()
    'End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        PopulateDespatchedCartonList()
    End Sub
End Class
