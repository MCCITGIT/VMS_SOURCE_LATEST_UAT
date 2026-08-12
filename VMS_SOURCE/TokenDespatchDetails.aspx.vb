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

Partial Class TokenDespatchDetails
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
                    LoadTokenDespatchDetails()
                    ddlFactory.Enabled = False
                    ddlSite.Enabled = False
                    ddlVendor.Enabled = False
                Catch ex As Exception
                    Response.Redirect("TokenDespatchList.aspx")
                End Try
            Else
                Response.Redirect("TokenDespatchList.aspx")
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
        LoadTokenDespatchDetails()
    End Sub
    Private Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
        PopulateProduct()
        LoadTokenDespatchDetails()
    End Sub

    Private Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduct.SelectedIndexChanged
        PopulatePackSize()
        LoadTokenDespatchDetails_Filter()
    End Sub

    Private Sub ddlPackSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPackSize.SelectedIndexChanged
        LoadTokenDespatchDetails_Filter()
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
                        LoadTokenDespatchDetails_Filter()
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

  
    Public Sub LoadTokenDespatchDetails()
        If (Request.QueryString("td") IsNot Nothing) Then
            Dim trh_id As Int32 = 0
            Try
                trh_id = Convert.ToInt64(Request.QueryString("td"))
                Dim ds As DataSet
                Try
                    ds = Obj.GetTokenDespatchDetails(trh_id)
                    If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                        Try
                            ddlSite.SelectedValue = ds.Tables(0).Rows(0)("site_code").ToString()
                            ddlSite_SelectedIndexChanged(ddlFactory, New EventArgs())
                            ddlVendor.SelectedValue = ds.Tables(0).Rows(0)("vendor_code").ToString()
                        Catch ex As Exception

                        End Try
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
            Catch ex As Exception
                Response.Redirect("TokenDespatchList.aspx")
            End Try
        Else
            Response.Redirect("TokenDespatchList.aspx")
        End If



    End Sub

    Public Sub LoadTokenDespatchDetails_Filter()
        If (Request.QueryString("td") IsNot Nothing) Then
            Dim trh_id As Int32 = 0
            Try
                trh_id = Convert.ToInt64(Request.QueryString("td"))
                Dim ds As DataSet
                Try
                    ds = Obj.GetTokenDespatchDetails(trh_id)
                    If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                        
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
            Catch ex As Exception
                Response.Redirect("TokenDespatchList.aspx")
            End Try
        Else
            Response.Redirect("TokenDespatchList.aspx")
        End If



    End Sub
#End Region

    Protected Sub ddlSite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSite.SelectedIndexChanged
        PopulateVendor()
        PopulateProduct()
        PopulatePackSize()
        'PopulateDespatchedCartonList()
    End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        LoadTokenDespatchDetails_Filter()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("TokenDespatchList.aspx")
    End Sub
End Class
