'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : TokenRequisitionList_Vendor.aspx.vb
'Created Date	: 17-09-2018
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
Partial Class TokenRequisitionList_Vendor
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
            Dim out As Integer = 0
            If (Not (String.IsNullOrEmpty(Request.QueryString("id"))) And (Integer.TryParse(Request.QueryString("id"), out))) Then
                PopulateTokenVendor(ddlTokenVendor)
                ddlTokenVendor.SelectedItem.Selected = False
                ddlTokenVendor.SelectedValue = userInfo.userIDEntity
                ddlTokenVendor.Enabled = False
                ddlVendorUnit.Enabled = False
                ddlVendorRequisition.Enabled = False
                PopulateUnit()
                'PopulateRequisition()
                ddlVendorRequisition.Items.Clear()
                ddlVendorRequisition.Items.Insert(0, New ListItem(Request.QueryString("id"), Request.QueryString("id")))
                ddlVendorRequisition.SelectedValue = Request.QueryString("id")
                PopulateDespatch()
                gvRequistionList.PageIndex = 0
                BindGrid()
            Else
                Response.Redirect("TokenVendorRequisitionList.aspx", False)
            End If

        End If

    End Sub

#End Region

#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()

    End Sub
#End Region
#Region "Populate Requisition"
    Private Sub PopulateRequisition()
        CheckLogin()
        Try
            Dim obj As New UnitTokenReceivedClass
            Dim dsVendorRequisitionSet As New DataSet
            ddlVendorRequisition.Items.Clear()
            dsVendorRequisitionSet = obj.GetRequisitionList(ddlVendorUnit.SelectedValue, ddlTokenVendor.SelectedValue)
            If (Not (dsVendorRequisitionSet Is Nothing) AndAlso dsVendorRequisitionSet.Tables.Count > 0 AndAlso Not (dsVendorRequisitionSet.Tables(0) Is Nothing) AndAlso dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                ddlVendorRequisition.DataSource = dsVendorRequisitionSet.Tables(0)
                ddlVendorRequisition.DataTextField = "tdh_requisition_id"
                ddlVendorRequisition.DataValueField = "tdh_requisition_id"
                ddlVendorRequisition.DataBind()
            End If
            ddlVendorRequisition.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

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
#Region "Populate Despatch"
    Private Sub PopulateDespatch()
        CheckLogin()
        Try
            Dim out As Integer = 0
            Dim obj As New UnitTokenReceivedClass
            Dim dsVendorRequisitionSet As New DataSet
            ddlDespatchId.Items.Clear()
            If (Integer.TryParse(ddlVendorRequisition.SelectedValue, out)) Then
                dsVendorRequisitionSet = obj.GetDespatchId(Convert.ToInt32(ddlVendorRequisition.SelectedValue))
                If (Not (dsVendorRequisitionSet Is Nothing) AndAlso dsVendorRequisitionSet.Tables.Count > 0 AndAlso Not (dsVendorRequisitionSet.Tables(0) Is Nothing) AndAlso dsVendorRequisitionSet.Tables(0).Rows.Count > 0) Then
                    ddlDespatchId.DataSource = dsVendorRequisitionSet.Tables(0)
                    ddlDespatchId.DataTextField = "tdh_despatch_id"
                    ddlDespatchId.DataValueField = "tdh_despatch_id"
                    ddlDespatchId.DataBind()

                End If
            End If
            ddlDespatchId.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region
#Region "Populate Unit"
    Private Sub PopulateUnit()
        CheckLogin()
        Try
            Dim obj As New TokenRequisitionVendorClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetUnitName(userInfo.userIDEntity)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendorUnit.DataSource = dsUnitSet.Tables(0)
                ddlVendorUnit.DataTextField = "unit_name"
                ddlVendorUnit.DataValueField = "unit_code"
                ddlVendorUnit.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            Else
                ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
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
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
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
#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim obj As New TokenRequisitionDespatchClass
            Dim dsProductSet As New DataSet
            Dim out As Integer = 0
            If ((Not (ddlVendorRequisition.SelectedValue.Equals(String.Empty))) And Integer.TryParse(ddlVendorRequisition.SelectedValue, out)) Then
                If ((Not (ddlDespatchId.SelectedValue.Equals(String.Empty))) And Integer.TryParse(ddlDespatchId.SelectedValue, out)) Then
                    dsProductSet = obj.GetDespatchList(userInfo.userIDEntity, Integer.Parse(ddlVendorRequisition.SelectedValue), ddlVendorUnit.SelectedValue, Integer.Parse(ddlDespatchId.SelectedValue))
                Else
                    dsProductSet = obj.GetDespatchList(userInfo.userIDEntity, Integer.Parse(ddlVendorRequisition.SelectedValue), ddlVendorUnit.SelectedValue, 0)
                End If

            Else
                dsProductSet = obj.GetDespatchList(userInfo.userIDEntity, Integer.Parse(ddlVendorRequisition.SelectedValue), ddlVendorUnit.SelectedValue, 0)
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
    Protected Sub ddlVendorUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorUnit.SelectedIndexChanged
        PopulateRequisition()
    End Sub
    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRequistionList.PageIndexChanging
        gvRequistionList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    Protected Sub gvTokenVendorList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRequistionList.RowCommand
        CheckLogin()
        If (e.CommandName.Equals("EditRequisition")) Then
            'Dim gvrow As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Response.Redirect("TokenRequisitionDespatch.aspx?despatchid=" & e.CommandArgument.ToString)
        End If
        If (e.CommandName.Equals("Print")) Then

            Dim gvRow As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim out As Integer = 0

            Dim index As Integer = gvRow.RowIndex
            Dim row As GridViewRow = gvRequistionList.Rows(index)
            Dim ReportViewer As New ReportViewer_DC

            ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Token_Despatched_Advice_Report
            ReportViewer.ReportCase = Constant.ReportView.ReportCase.TokenDespatchedAdviceRptCase

            'hdn = row.FindControl("hdnUnit")
            If (Integer.TryParse(e.CommandArgument.ToString, out)) Then
                ReportViewer.DsptchId = e.CommandArgument.ToString
                ReportViewer.Active = Constant.Common.ActiveStatus
                'Response.Redirect("ReportViewer.aspx", False)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)
            Else
                lblErrorMessage.Text = "ERROR"

            End If


        End If

    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        CheckLogin()
        Dim out As Integer = 0
        If (Not (String.IsNullOrEmpty(Request.QueryString("id"))) And (Integer.TryParse(Request.QueryString("id"), out))) Then
            Response.Redirect("TokenRequisitionDespatch.aspx?id=" & Request.QueryString("id") & "&unit=" & ddlVendorUnit.SelectedValue, False)
        Else
            Response.Redirect("TokenVendorRequisitionList.aspx", False)
        End If

    End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        BindGrid()
    End Sub
    Protected Sub ddlVendorRequisition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorRequisition.SelectedIndexChanged
        PopulateDespatch()
    End Sub
End Class
