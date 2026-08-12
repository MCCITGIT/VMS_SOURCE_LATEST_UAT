'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : IndentList.aspx.vb
'Created Date	: 14-December-2011
'Created By	    : Rohan Mazumdar 
'Version	    : R02.00.00
'Description	: Code behind for IndentList Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class IndentsList_HO
    Inherits System.Web.UI.Page

#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then

            AddAttributes()

            PopulateRegionDropdown()
            PopulateDepotDropdown()
            PopulateProductCategory()

            lblFinYear.Text = GetStandardParameter(Constant.Common.StandardParameter_ProcessYear)

            lblFinMonth.Text = GetStandardParameter(Constant.Common.StandardParameter_ProcessMonth)

            If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                ddlStatus.SelectedValue = "E"
            Else
                btnSubmit.Enabled = False
            End If

            RetrieveSearchCriteria()

            PopulateIndentList()

        End If

    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        btnSubmit.Attributes.Add("onClick", "return validateForm()")
    End Sub
#End Region


#Region "Save Search Criteria."
    ' Saves the current search criteria in session
    Private Sub SaveSearchCriteria()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Session(Constant.SessionKeys.IndentListSearchInfo) = Nothing

        Dim indentSearchInfo As New IndentListSearchCriteria
        indentSearchInfo.IndentRegion = ddlRegion.SelectedValue
        indentSearchInfo.IndentDepot = ddlDepot.SelectedValue

        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
            indentSearchInfo.IndentStatus = "E"
        Else
            indentSearchInfo.IndentStatus = ddlStatus.SelectedValue
        End If

        Session(Constant.SessionKeys.IndentListSearchInfo) = indentSearchInfo

    End Sub

#End Region

#Region "Retrieve Search Criteria."

    ' Retrieve the existing search criteria in session
    Private Sub RetrieveSearchCriteria()

        If (Not (Session(Constant.SessionKeys.IndentListSearchInfo) Is Nothing)) Then

            Dim indentSearchInfo As New IndentListSearchCriteria

            indentSearchInfo = Session(Constant.SessionKeys.IndentListSearchInfo)
            ddlRegion.SelectedValue = indentSearchInfo.IndentRegion
            ddlDepot.SelectedValue = indentSearchInfo.IndentDepot
            ddlStatus.SelectedValue = indentSearchInfo.IndentStatus

        End If

        SaveSearchCriteria()

    End Sub

#End Region


#Region "Populate Depot dropdown."

    Private Sub PopulateProductCategory()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New IndentMaster()
        Dim dsDepot As DataSet

        ddlproduct.Items.Clear()

        Try

            dsDepot = cmnDepot.GetProductCategory()

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlproduct.DataSource = dsDepot
                    ddlproduct.DataTextField = "product_name"
                    ddlproduct.DataValueField = "product_name"
                    ddlproduct.DataBind()
                    ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                Else
                    ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate Region dropdown."

    Private Sub PopulateRegionDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnRegion As New Common()
        Dim dsRegion As DataSet

        Try

            dsRegion = cmnRegion.GetLovDetails(userInfo.userCompanyEntity, Constant.Common.REGION_TYPE, Constant.Common.ActiveStatus)

            If Not (dsRegion Is Nothing) Then

                If Not (dsRegion.Tables(0).Rows.Count = 0) Then

                    ddlRegion.DataSource = dsRegion
                    ddlRegion.DataTextField = "Lov_Value"
                    ddlRegion.DataValueField = "Lov_Code"
                    ddlRegion.DataBind()

                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, "", True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                        ddlRegion.SelectedValue = userInfo.userRegionEntity
                        ddlRegion.Enabled = False
                    End If

                Else
                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate Depot dropdown."

    Private Sub PopulateDepotDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New Common()
        Dim dsDepot As DataSet

        ddlDepot.Items.Clear()

        Try

            dsDepot = cmnDepot.Getdepotname(ddlRegion.SelectedValue)

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlDepot.DataSource = dsDepot
                    ddlDepot.DataTextField = "depot_name"
                    ddlDepot.DataValueField = "depot_code"
                    ddlDepot.DataBind()

                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.REGION) Then
                        ddlDepot.SelectedValue = userInfo.userBranchEntity
                        ddlDepot.Enabled = False
                    End If

                Else
                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Get values for a particular Standard Parameter."

    Private Function GetStandardParameter(ByVal param_name As String) As String

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnStandardParameter As New Common()
        Dim dsStandardParameter As DataSet

        Dim result As String = String.Empty

        Try

            dsStandardParameter = cmnStandardParameter.GetStandardParameterValues(param_name)

            If Not (dsStandardParameter Is Nothing) Then

                If Not (dsStandardParameter.Tables(0).Rows.Count = 0) Then
                    result = dsStandardParameter.Tables(0).Rows(0)("param_char_value")
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

#Region "Populate SKU Codes gridview in case of New Indent Entry."

    Private Sub PopulateIndentList()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim Competitor As String = String.Empty
        Dim indIndentMaster As New IndentMaster()
        Dim dsIndentList As DataSet

        Dim indent_header As New IndentHeaderEntity()

        indent_header.IndentDepot = ddlDepot.SelectedValue
        indent_header.IndentFinYear = lblFinYear.Text
        indent_header.IndentFinMonth = lblFinMonth.Text
        indent_header.IndentStatus = ddlStatus.SelectedValue

        For Each lstitm As ListItem In ddlproduct.Items
            If lstitm.Selected Then
                Competitor = Competitor + lstitm.Value + ","

            End If
        Next

        dsIndentList = indIndentMaster.GetIndentList(indent_header, userInfo.userIDEntity, Competitor)
        If (Not (dsIndentList Is Nothing)) Then
            gvIndentList.Visible = True

            gvIndentList.DataSource = dsIndentList.Tables(0)

            Dim primary(3) As String

            primary(0) = "depot_code"
            primary(1) = "fin_year"
            primary(2) = "fin_month"
            primary(3) = "indent_no"

            gvIndentList.DataKeyNames = primary

            gvIndentList.DataBind()
        End If

        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
            gvIndentList.Columns(0).Visible = False
            gvIndentList.Columns(1).Visible = True
            gvIndentList.Columns(2).Visible = True
        Else
            gvIndentList.Columns(0).Visible = True
            gvIndentList.Columns(1).Visible = False
            gvIndentList.Columns(2).Visible = False
            btnSubmit.Text = "Delete"
        End If

        If (Not (dsIndentList.Tables(1) Is Nothing)) Then
            hdnSubmitAccess.Value = Convert.ToString(dsIndentList.Tables(1).Rows(0)("usp_approval_access"))
            If (hdnSubmitAccess.Value = "N") Then
                btnSubmit.Visible = False
            End If
        End If

    End Sub
#End Region


    'Protected Sub imgbtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAdd.Click
    '    SaveSearchCriteria()
    '    Response.Redirect("AddUpdateIndentEntry_HO.aspx", True)
    'End Sub

    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        SaveSearchCriteria()
        Response.Redirect("AddUpdateIndentEntry_HO.aspx", True)
    End Sub

    'Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
    '    SaveSearchCriteria()
    '    PopulateIndentList()
    'End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        SaveSearchCriteria()
        PopulateIndentList()
    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepotDropdown()
    End Sub

    Protected Sub gvIndentList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIndentList.RowDataBound
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim rdobtnApprove As RadioButton = CType(e.Row.FindControl("rdobtnApprove"), RadioButton)
            Dim rdobtnReject As RadioButton = CType(e.Row.FindControl("rdobtnReject"), RadioButton)

            Dim chkDelete As CheckBox = CType(e.Row.FindControl("chkDelete"), CheckBox)
            chkDelete.Attributes.Add("onclick", "rwslctToggleSelect('" & chkDelete.ClientID & "');")

            Dim lblApprvRejctStatus As Label = CType(e.Row.FindControl("lblApprvRejctStatus"), Label)

            If IsDBNull(rowView("approved_yn")) Then
                lblApprvRejctStatus.Text = "Entered"
            Else
                If rowView("approved_yn") = "Y" Then
                    e.Row.BackColor = Drawing.Color.LawnGreen
                    lblApprvRejctStatus.Text = "Approved"
                ElseIf rowView("approved_yn") = "N" Then
                    e.Row.BackColor = Drawing.Color.Pink
                    lblApprvRejctStatus.Text = "Rejected"
                End If
                chkDelete.Visible = False
                rdobtnApprove.Visible = False
                rdobtnReject.Visible = False
            End If

            Dim txtRemarks As TextBox = CType(e.Row.FindControl("txtRemarks"), TextBox)

            If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                txtRemarks.Enabled = False
            End If

            e.Row.Cells(6).ForeColor = Drawing.Color.Blue
            e.Row.Cells(7).ForeColor = Drawing.Color.Blue

        End If

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim indtMaster As New IndentMaster
        Dim RecordUpdated As Integer

        If (btnSubmit.Text = "Delete") Then

            Try

                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                For RowIndex As Integer = 0 To gvIndentList.Rows.Count - 1
                    Dim row As GridViewRow = gvIndentList.Rows(RowIndex)

                    Dim chkDelete As CheckBox = CType(row.FindControl("chkDelete"), CheckBox)
                    Dim chkd As Boolean = chkDelete.Checked

                    If (chkd = True) Then

                        Dim depot_code As String = CType(gvIndentList.DataKeys(RowIndex).Values(0), String)
                        Dim fin_year As String = CType(gvIndentList.DataKeys(RowIndex).Values(1), String)
                        Dim fin_month As String = CType(gvIndentList.DataKeys(RowIndex).Values(2), String)
                        Dim indent_no As String = CType(gvIndentList.DataKeys(RowIndex).Values(3), String)

                        Dim indntDetail As New IndentHeaderEntity

                        indntDetail.IndentDepot = depot_code
                        indntDetail.IndentFinYear = fin_year
                        indntDetail.IndentFinMonth = fin_month
                        indntDetail.IndentID = CType(indent_no, Integer)

                        RecordUpdated += indtMaster.DeleteIndentHeaderandDetails(indntDetail, sqlConn, sqlTrans)

                    End If
                Next

                If (RecordUpdated > 0) Then
                    sqlTrans.Commit()
                Else
                    sqlTrans.Rollback()
                End If

                PopulateIndentList()

            Catch ex As Exception

                sqlTrans.Rollback()
                Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
                HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

            Finally

                sqlConn.Close()

            End Try
        Else
            Try

                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                For RowIndex As Integer = 0 To gvIndentList.Rows.Count - 1
                    Dim row As GridViewRow = gvIndentList.Rows(RowIndex)

                    Dim rdobtnApprove As RadioButton = CType(row.FindControl("rdobtnApprove"), RadioButton)
                    Dim rdobtnReject As RadioButton = CType(row.FindControl("rdobtnReject"), RadioButton)

                    If (rdobtnApprove.Checked = True Or rdobtnReject.Checked = True) Then

                        Dim depot_code As String = CType(gvIndentList.DataKeys(RowIndex).Values(0), String)
                        Dim fin_year As String = CType(gvIndentList.DataKeys(RowIndex).Values(1), String)
                        Dim fin_month As String = CType(gvIndentList.DataKeys(RowIndex).Values(2), String)
                        Dim indent_no As String = CType(gvIndentList.DataKeys(RowIndex).Values(3), String)

                        Dim indntDetail As New IndentHeaderEntity

                        indntDetail.IndentDepot = depot_code
                        indntDetail.IndentFinYear = fin_year
                        indntDetail.IndentFinMonth = fin_month
                        indntDetail.IndentID = CType(indent_no, Integer)

                        If (rdobtnApprove.Checked = True) Then
                            indntDetail.IndentApproveYN = Constant.Common.ActiveStatus
                        Else
                            indntDetail.IndentApproveYN = Constant.Common.InActiveStatus
                        End If

                        indntDetail.IndentCreatedUser = userInfo.userIDEntity

                        Dim txtRemarks As TextBox = CType(row.FindControl("txtRemarks"), TextBox)

                        indntDetail.IndentRemarks = txtRemarks.Text

                        RecordUpdated += indtMaster.IndentEntryApproveReject(indntDetail, sqlConn, sqlTrans)

                    End If
                Next

                If (RecordUpdated > 0) Then
                    sqlTrans.Commit()
                Else
                    sqlTrans.Rollback()
                End If

                PopulateIndentList()

            Catch ex As Exception

                sqlTrans.Rollback()
                Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
                HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

            Finally

                sqlConn.Close()

            End Try
        End If
    End Sub

    'Protected Sub imgbtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnPrint.Click
    '    Response.Redirect("Monthly_Depot_Indent_List.aspx", True)
    'End Sub

    Protected Sub imgbtnPrint_Click(sender As Object, e As EventArgs) Handles imgbtnPrint.Click
        Response.Redirect("Monthly_Depot_Indent_List.aspx", True)
    End Sub
End Class
