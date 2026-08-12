Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class TokenRequisitionStatusList
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            AddAttributes()
            PopulateUnit()
            PopulateTokenVendor(ddlTokenVendor)
            gvRequistionList.PageIndex = 0
            PopulateRequisition()
            'PopulateStatus()
            BindGrid()

        End If

    End Sub

#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()

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
                If (dsUnitSet.Tables(0).Rows.Count > 0) Then
                    ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
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
            ddlVendorRequisition.Items.Clear()
            Dim obj As New TokenVendorRequisitionClass
            Dim dsVendorRequisitionSet As New DataSet

            dsVendorRequisitionSet = obj.GetRequisitionForUnitByVendor_Unit(Constant.Common.ActiveStatus, ddlTokenVendor.SelectedValue, ddlVendorUnit.SelectedValue)
            If (Not (dsVendorRequisitionSet Is Nothing) AndAlso dsVendorRequisitionSet.Tables.Count > 0 AndAlso Not (dsVendorRequisitionSet.Tables(0) Is Nothing) AndAlso dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                ddlVendorRequisition.DataSource = dsVendorRequisitionSet.Tables(0)
                ddlVendorRequisition.DataTextField = "trh_id"
                ddlVendorRequisition.DataValueField = "trh_id"
                ddlVendorRequisition.DataBind()
                If (dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                    ddlVendorRequisition.Items.Insert(0, New ListItem(Constant.Common.Selec, 0))
                End If
            Else
                ddlVendorRequisition.Items.Insert(0, New ListItem(Constant.Common.Selec, 0))
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub ddlTokenVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTokenVendor.SelectedIndexChanged
        PopulateRequisition()
        BindGrid()
    End Sub

    '#Region "Populate Status from Lov"
    '    Private Sub PopulateStatus()
    '        CheckLogin()
    '        Try
    '            Dim obj As New Common()
    '            Dim ds As New DataSet

    '            ds = obj.GetLovDetails(Constant.Common.Company, "T_REQ_STATUS", Constant.Common.ActiveStatus)
    '            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then

    '                ddlStatus.DataSource = ds.Tables(0)
    '                ddlStatus.DataTextField = "Lov_Value"
    '                ddlStatus.DataValueField = "Lov_Code"
    '                ddlStatus.DataBind()
    '                If (ds.Tables(0).Rows.Count > 0) Then
    '                    ddlStatus.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
    '                End If
    '                ddlStatus.SelectedValue = Constant.Common.Token_Req_Status_New
    '            Else
    '                ddlStatus.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
    '            End If
    '        Catch ex As Exception
    '            Dim returnUrl As String = "~/ExceptionPage.aspx"
    '            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '            Server.Transfer(returnUrl)
    '        End Try

    '    End Sub
    '#End Region


#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim obj As New TokenVendorRequisitionClass
            Dim dsProductSet As New DataSet
            Dim RequisitionId As Integer = 0
            If Integer.TryParse(ddlVendorRequisition.SelectedValue, RequisitionId) Then
                dsProductSet = obj.GetTokenStatusList(userInfo.userIDEntity, userInfo.userGroupCodeEntity, ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue, Integer.Parse(ddlVendorRequisition.SelectedValue), String.Empty)
            Else
                dsProductSet = obj.GetTokenStatusList(userInfo.userIDEntity, userInfo.userGroupCodeEntity, ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue, 0, String.Empty)
            End If

            If (userInfo.userGroupCodeEntity.Equals("VENDOR")) Then
                lblTokenVendor.Text = dsProductSet.Tables(0).Rows(0)("tokenVendorName").ToString
            End If

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvRequistionList.DataSource = dsProductSet.Tables(0)
                gvRequistionList.DataBind()
            Else
                gvRequistionList.DataSource = Nothing
                gvRequistionList.DataBind()
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub gvRequistionList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRequistionList.PageIndexChanging
        gvRequistionList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        gvRequistionList.PageIndex = 0
        BindGrid()
    End Sub

    Protected Sub gvRequistionList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRequistionList.RowCommand
        CheckLogin()
        If (e.CommandName.Equals("EditRequisition")) Then
            'Dim gvrow As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Response.Redirect("TokeRequisitionStatusDetails.aspx?id=" & e.CommandArgument.ToString)
        End If
    End Sub
End Class
