Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlClient
Partial Class TokenReceiveList
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim Obj As TokenReceiveClass = New TokenReceiveClass()

#Region "Page Load Event"
    Private Sub TokenReceiveList_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckLogin()
        If (Not IsPostBack) Then
            PopulateVendor()
            PopulateFactory()
            BindGrid()
        End If
    End Sub
#End Region

#Region "Event Handler"
    Protected Sub ddlFactory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFactory.SelectedIndexChanged
        PopulateSite()
        PopulateVendor()
    End Sub
    Protected Sub gvTokenDespatchList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTokenDespatchList.PageIndexChanging
        gvTokenDespatchList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    'Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
    '    BindGrid()
    'End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        BindGrid()
    End Sub

    Protected Sub gvTokenDespatchList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTokenDespatchList.RowCommand
        Try
            If (e.CommandName = "ViewDetails") Then
                Dim gvr As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
                Dim rowIndex As Integer = gvr.RowIndex

                Dim lblReceiveId As Label = CType(gvTokenDespatchList.Rows(rowIndex).FindControl("lblReceiveId"), Label)
                If (Not lblReceiveId.Text.Equals(String.Empty)) Then
                    Response.Redirect(String.Concat("TokenReceiveEntry.aspx?td=", lblReceiveId.Text.ToString), False)
                End If
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub
#End Region

#Region "Custom method"
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

    Public Sub BindGrid()
        Dim ds As DataSet
        Try
            gvTokenDespatchList.DataSource = Nothing
            gvTokenDespatchList.DataBind()

            ds = Obj.GetList(ddlSite.SelectedValue, ddlVendor.SelectedValue, userInfo.userIDEntity)
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing))) Then
                gvTokenDespatchList.DataSource = ds.Tables(0)
                gvTokenDespatchList.DataBind()
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
    End Sub

End Class
