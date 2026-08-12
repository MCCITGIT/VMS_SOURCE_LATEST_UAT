Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlClient

Partial Class ValueTokenList
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim Obj As TokenRequisitionClass = New TokenRequisitionClass()

#Region "Page Load Event"
    Private Sub ValueTokenList_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim lblTitle As Label = CType(Me.Master.FindControl("lblTitle"), Label)
        'lblTitle.Text = "TOKEN REQUISITION LIST"
        CheckLogin()
        If (Not IsPostBack) Then
            PopulateFactory()
            PopulateMonth()
            PopulateYear()

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString("D2")
            ddlYear.SelectedValue = DateTime.Now.Year.ToString()
            BindGrid()
        End If
    End Sub
#End Region

#Region "Event handler"
    Protected Sub ddlFactory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFactory.SelectedIndexChanged
        PopulateSite()
        PopulateVendor()
    End Sub

    Protected Sub gvList_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvList.PageIndexChanging
        gvList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles imgbtnSearch.Click
        BindGrid()
    End Sub

    Protected Sub gvList_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvList.RowCommand
        If (e.CommandName = "EditRow") Then
            CheckLogin()
            Dim gvr As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim rowIndex As Integer = gvr.RowIndex
            Dim RequisitionId As String = CType(gvList.Rows(rowIndex).FindControl("lblSrl"), Label).Text
            If (Not RequisitionId.Equals(String.Empty)) Then
                Response.Redirect("ValueTokenEntry.aspx?r=" + RequisitionId)
            End If
        End If
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
    Public Sub PopulateMonth()
        Dim ds As DataSet
        Try
            ds = Obj.GetLovDetails("MONTH_NAME")
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlMonth.DataSource = ds.Tables(0)
                ddlMonth.DataTextField = "lov_value"
                ddlMonth.DataValueField = "lov_code"
                ddlMonth.DataBind()
            End If
            ddlMonth.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
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
                ddlYear.Items.Insert(index, New ListItem(Years.ToString(), Years.ToString(), True))
                Years = Years + 1
            Next
            ddlYear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
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
        Dim factory As String = ddlFactory.SelectedValue.ToString
        Dim site As String = ddlSite.SelectedValue.ToString
        Dim vendor As String = ddlVendor.SelectedValue.ToString
        Dim ts_session_month As String = ddlMonth.SelectedValue.ToString
        Dim ts_session_year As String = ddlYear.SelectedValue.ToString
        Dim Requisition_id As Integer = 0
        Dim ds As DataSet

        If (Not txtRequisitionId.Text.Trim().Equals(String.Empty)) Then
            Try
                Requisition_id = Convert.ToInt32(txtRequisitionId.Text.Trim())
            Catch ex As Exception
                Requisition_id = -1
            End Try
        End If
        Try
            ds = Obj.GetList(factory, site, vendor, ts_session_month, ts_session_year, Requisition_id, userInfo.userIDEntity)
            If ((Not (ds Is Nothing) _
            AndAlso ((ds.Tables.Count > 0) _
            AndAlso Not (ds.Tables(0) Is Nothing)))) Then
                gvList.DataSource = ds.Tables(0)
                gvList.DataBind()
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
