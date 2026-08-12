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
Imports System.IO
Imports System.Security.Permissions
Imports Microsoft.Win32

Partial Class IndentsList
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
                    'ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                Else
                    ' ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub
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
    '    Response.Redirect("AddUpdateIndentEntry.aspx", True)
    'End Sub

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
            Dim hdndoc As HiddenField = CType(e.Row.FindControl("hdndoc"), HiddenField)
            Dim btnSendMail As LinkButton = CType(e.Row.FindControl("btnSendMail"), LinkButton)
            Dim btndownload As LinkButton = CType(e.Row.FindControl("btndownload"), LinkButton)
            Dim btnView As LinkButton = CType(e.Row.FindControl("btnView"), LinkButton)

            Dim chkDelete As CheckBox = CType(e.Row.FindControl("chkDelete"), CheckBox)
            chkDelete.Attributes.Add("onclick", "rwslctToggleSelect('" & chkDelete.ClientID & "');")
            If rowView("created_user_depot").ToString <> rowView("depot_code").ToString Then
                chkDelete.Enabled = False
            End If

            Dim lblApprvRejctStatus As Label = CType(e.Row.FindControl("lblApprvRejctStatus"), Label)

            If rowView("ViewHistoryVisibility") = "Y" Then
                btnView.Visible = True
            Else
                btnView.Visible = False
            End If

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

            If rowView("indh_inv_req_mail_yn") = "Y" Then
                e.Row.BackColor = Drawing.Color.Yellow
            End If



            Dim txtRemarks As TextBox = CType(e.Row.FindControl("txtRemarks"), TextBox)

            If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                txtRemarks.Enabled = False
            End If

            e.Row.Cells(6).ForeColor = Drawing.Color.Blue
            e.Row.Cells(7).ForeColor = Drawing.Color.Blue

            If (hdndoc.Value = String.Empty) Then
                btnSendMail.Visible = True
                btndownload.Visible = False
            Else
                btndownload.Visible = True
                btnSendMail.Visible = False
            End If

            If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
                btnSendMail.Visible = False
            End If

            If Not IsDBNull(rowView("approved_yn")) AndAlso (rowView("approved_yn") = "Y" Or rowView("approved_yn") = "N") Then
                btnSendMail.Visible = False
            End If

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

                        'Dim ApprovalStatus As String

                        indntDetail.IndentDepot = depot_code
                        indntDetail.IndentFinYear = fin_year
                        indntDetail.IndentFinMonth = fin_month
                        indntDetail.IndentID = CType(indent_no, Integer)

                        If (rdobtnApprove.Checked = True) Then
                            indntDetail.IndentApproveYN = Constant.Common.ActiveStatus

                            'ApprovalStatus = "Approved"
                        Else
                            indntDetail.IndentApproveYN = Constant.Common.InActiveStatus

                            'ApprovalStatus = "Rejected"
                        End If

                        indntDetail.IndentCreatedUser = userInfo.userIDEntity

                        Dim txtRemarks As TextBox = CType(row.FindControl("txtRemarks"), TextBox)

                        indntDetail.IndentRemarks = txtRemarks.Text

                        RecordUpdated += indtMaster.IndentEntryApproveReject(indntDetail, sqlConn, sqlTrans)


                        'SendMail(depot_code, fin_year, fin_month, CType(indent_no, Integer), ApprovalStatus, txtRemarks.Text)



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

    Protected Sub btnAddNewIndent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewIndent.Click
        SaveSearchCriteria()
        Response.Redirect("IndentEntry_Add.aspx", True)
    End Sub

    Protected Sub btnAddOtherIndent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOtherIndent.Click
        SaveSearchCriteria()
        Response.Redirect("AddUpdateIndentEntry.aspx", True)
    End Sub

    Private Sub btnAddIndustrialIndent_Click(sender As Object, e As EventArgs) Handles btnAddIndustrialIndent.Click
        SaveSearchCriteria()
        Response.Redirect("AddUpdateIndustrialIndentEntry.aspx", True)
    End Sub


    Protected Sub btnAddSTPIndent_Click(sender As Object, e As EventArgs) Handles btnAddSTPIndent.Click
        SaveSearchCriteria()
        Response.Redirect("AddUpdateSTPIndentEntry.aspx", True)
    End Sub


    'Private Sub SendMail(ByVal Depot As String, ByVal FinYear As String, ByVal FinMonth As String, ByVal IndentId As Integer, ByVal ApprovalStatus As String, ByVal Remarks As String)
    '    Dim ds As DataSet

    '    Dim vndprdskuIndentMaster As New IndentMaster()
    '    Dim indent_header As New IndentHeaderEntity()

    '    indent_header.IndentDepot = Depot
    '    indent_header.IndentFinYear = FinYear
    '    indent_header.IndentFinMonth = FinMonth
    '    indent_header.IndentID = IndentId

    '    ds = vndprdskuIndentMaster.GetIndentDetailsMail(indent_header)

    '    If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
    '        Try
    '            'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '            Dim mailobj As New EmailSMSsender
    '            Dim mailEntity As New MailEntity
    '            mailEntity.ToAddress = ds.Tables(2).Rows(0)("MailIds_To").ToString
    '            mailEntity.CCAddress = ds.Tables(2).Rows(0)("MailIds_CC").ToString
    '            'mailEntity.BCCAddress = ds.Tables(0).Rows(0)("bccID").ToString
    '            mailEntity.MailSubject = "New Indent Approval - Indent ID " & IndentId.ToString

    '            Dim MailBody As String = String.Empty

    '            '-----------------------------------------------------------------------------------------------
    '            Dim bodyBuilder As StringBuilder = New StringBuilder()

    '            bodyBuilder.Append("<div>Please find below the details.</div>")
    '            bodyBuilder.Append("<table cellpadding='2' style='text-align: Left; width:95%; border: 1px solid black;'>")

    '            Dim rowcount As Integer = 0

    '            For Each dr As DataRow In ds.Tables(0).Rows
    '                rowcount += 1
    '                Dim styleStr As String

    '                If rowcount Mod 2 = 0 Then
    '                    styleStr = " background-color: #CDF7F2;"
    '                Else
    '                    styleStr = ""
    '                End If


    '                bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
    '                bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Indent ID:-</td>")
    '                bodyBuilder.Append("<td>" & IndentId.ToString() & "</td>")
    '                bodyBuilder.Append("</tr>")
    '                bodyBuilder.Append("</br>")
    '                bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
    '                bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Indent Date:-</td>")
    '                bodyBuilder.Append("<td>" & dr("indh_indent_date").ToString() & "</td>")
    '                bodyBuilder.Append("</tr>")
    '                bodyBuilder.Append("</br>")
    '                bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
    '                bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Region:-</td>")
    '                bodyBuilder.Append("<td>" & dr("depot_regn").ToString() & "</td>")
    '                bodyBuilder.Append("</tr>")
    '                bodyBuilder.Append("</br>")
    '                bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
    '                bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Depot:-</td>")
    '                bodyBuilder.Append("<td>" & dr("DepotName").ToString() & "</td>")
    '                bodyBuilder.Append("</tr>")
    '                bodyBuilder.Append("</br>")

    '                bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
    '                bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Approval Status:-</td>")
    '                bodyBuilder.Append("<td>" & dr("ApprovalStatus").ToString() & "</td>")
    '                bodyBuilder.Append("</tr>")
    '                bodyBuilder.Append("</br>")

    '                bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
    '                bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Remarks:-</td>")
    '                bodyBuilder.Append("<td>" & dr("Remarks").ToString() & "</td>")
    '                bodyBuilder.Append("</tr>")

    '                bodyBuilder.Append("</br>")
    '                bodyBuilder.Append("</tr>")

    '            Next

    '            'bodyBuilder.Append("<tr style=' background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>")
    '            'bodyBuilder.Append("<td colspan='4'>This is a System Generated Mail. Please Do not Reply.</td>")
    '            'bodyBuilder.Append("</tr>")
    '            bodyBuilder.Append("<tr>")
    '            bodyBuilder.Append("<td colspan='4'>")
    '            bodyBuilder.Append("<table cellpadding='8' style='text-align: Left; width:100%;border:1px solid grey;'>")
    '            bodyBuilder.Append("<tr style=' background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;'>")
    '            'bodyBuilder.Append("<td style='width:10%;'>#</td>")
    '            bodyBuilder.Append("<td style='width:15%;border:1px solid grey;text-align:center;'>Product Code</td>")
    '            bodyBuilder.Append("<td style='width:35%;border:1px solid grey;text-align:center;'>Product Name</td>")
    '            bodyBuilder.Append("<td style='width:15%;border:1px solid grey;text-align:center;'>Quantity</td>")
    '            bodyBuilder.Append("</tr>")
    '            Dim rowcount1 As Integer = 0
    '            For Each dr As DataRow In ds.Tables(1).Rows
    '                rowcount1 += 1
    '                Dim styleStr As String

    '                If rowcount Mod 2 = 0 Then
    '                    styleStr = " background-color: #CDF7F2;"
    '                Else
    '                    styleStr = ""
    '                End If

    '                bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;'>")
    '                bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("v_sku_code").ToString() & "</td>")
    '                bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("sku_desc").ToString() & "</td>")
    '                bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("indd_sku_nop").ToString() & "</td>")
    '                bodyBuilder.Append("</tr>")
    '            Next
    '            bodyBuilder.Append("<tr style=' background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>")
    '            bodyBuilder.Append("<td colspan='5' style='text-align:center;'>This is a System Generated Mail. Please Do not Reply.</td>")
    '            bodyBuilder.Append("</tr>")
    '            bodyBuilder.Append("</table>")
    '            bodyBuilder.Append("</td>")
    '            bodyBuilder.Append("</tr>")
    '            bodyBuilder.Append("</table>")
    '            'mailobj.mailBody = bodyBuilder.ToString()

    '            mailEntity.MailBody = bodyBuilder.ToString()
    '            '------------------------------------------------------------------------------------------------
    '            Dim recipNo As String = mailobj.sendMailHTML(mailEntity)
    '            'Next
    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End If


    'End Sub

    Private Sub DownloadDocument(ByVal fileName As String)
        Try
            'Dim genReportPath As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & "\" & userInfo.userCompanyEntity & "\" & "Machine Scrap Selling" & "\"

            Dim genReportPath As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & "Berger" & "\" & "Invoice_Docs" & "\"
            Dim appReceiptFileAbsolutePath = String.Concat(genReportPath, fileName)
            If File.Exists(appReceiptFileAbsolutePath) Then
                Response.Clear()
                Response.Charset = String.Empty
                Response.ContentType = GetMIMEType(appReceiptFileAbsolutePath)
                Response.AppendHeader("Content-Disposition", String.Concat("attachment; filename= """, fileName, """"))
                Response.TransmitFile(appReceiptFileAbsolutePath)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Flush()
            Else
                'lblPopMessageShow.Text = "File not found."
                'lblPopMessageShow.ForeColor = Drawing.Color.Red
                'Button1.OnClientClick = "return RefreshScreen()"
                'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetMIMEType(ByVal filepath As String) As String
        Dim regPerm As RegistryPermission = New RegistryPermission(RegistryPermissionAccess.Read, "\\HKEY_CLASSES_ROOT")
        Dim classesRoot As RegistryKey = Registry.ClassesRoot
        Dim fi = New FileInfo(filepath)
        Dim dotExt As String = LCase(fi.Extension)
        Dim typeKey As RegistryKey = classesRoot.OpenSubKey("MIME\Database\Content Type")
        Dim keyname As String = String.Empty

        For Each keyname In typeKey.GetSubKeyNames()
            Dim curKey As RegistryKey = classesRoot.OpenSubKey(String.Concat("MIME\Database\Content Type\", keyname))
            If LCase(curKey.GetValue("Extension")) = dotExt Then
                'Debug.WriteLine("Content type was " + keyname)
                Return keyname
            End If
        Next
        Return keyname
    End Function


    Protected Sub gvIndentList_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            Dim indIndentMaster As New IndentMaster()
            Dim dsIndentmail, ds As DataSet
            Dim depotemail As String
            Dim ccemail As String
            Dim bccemail As String
            If (String.Compare(e.CommandName, "download", StringComparison.CurrentCultureIgnoreCase) = 0) Then
                If String.IsNullOrEmpty(e.CommandArgument) Then
                    Exit Sub
                End If
                DownloadDocument(e.CommandArgument.ToString().Trim())
            ElseIf e.CommandName = "SendMail" Then
                Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

                Dim depotCode As String = CType(gvIndentList.Rows(rowIndex).FindControl("lblDepotCode"), Label).Text

                Dim indentId As String = CType(gvIndentList.Rows(rowIndex).FindControl("hdnindentId"), HiddenField).Value.ToString

                Dim finyr As String = CType(gvIndentList.Rows(rowIndex).FindControl("hdnfinyr"), HiddenField).Value.ToString

                Dim finmonth As String = CType(gvIndentList.Rows(rowIndex).FindControl("hdnfinmonth"), HiddenField).Value.ToString

                dsIndentmail = indIndentMaster.GetIndentDepotEmail(depotCode)

                If (Not (dsIndentmail Is Nothing) AndAlso dsIndentmail.Tables(0).Rows.Count > 0) Then
                    depotemail = dsIndentmail.Tables(0).Rows(0)("email")
                    ccemail = dsIndentmail.Tables(0).Rows(0)("cc_mail")
                    bccemail = dsIndentmail.Tables(0).Rows(0)("bcc_mail")
                    hdnpopindentId.Value = indentId
                    hdnemail.Value = depotemail
                    hdnpopdepot.Value = depotCode
                    hdnpopfinyr.Value = finyr
                    hdnpopfinmonth.Value = finmonth
                    hdnccmail.Value = ccemail
                    hdnbccmail.Value = bccemail
                    txtRemarks.Text = String.Empty
                    mp1.Show()
                    'SendMail(depotemail, indentId)
                End If
            ElseIf e.CommandName = "ViewHistory" Then
                Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

                Dim depotCode As String = CType(gvIndentList.Rows(rowIndex).FindControl("lblDepotCode"), Label).Text

                Dim indentId As String = CType(gvIndentList.Rows(rowIndex).FindControl("hdnindentId"), HiddenField).Value.ToString

                Dim finyr As String = CType(gvIndentList.Rows(rowIndex).FindControl("hdnfinyr"), HiddenField).Value.ToString

                Dim finmonth As String = CType(gvIndentList.Rows(rowIndex).FindControl("hdnfinmonth"), HiddenField).Value.ToString

                ds = indIndentMaster.GetIndentHistory(finyr, depotCode, indentId)

                If (Not (ds Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    gvIndentHistory.DataSource = ds
                    gvIndentHistory.DataBind()
                    lblpoperror1.Text = String.Empty

                Else
                    gvIndentHistory.DataSource = Nothing
                    gvIndentHistory.DataBind()
                    lblpoperror1.Text = "No Records Found. !!"
                End If
                mp2.Show()

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/XP_ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub SendMail(ByVal Depotmail As String, ByVal IndentId As String, ByVal DepotCode As String, ByVal year As String, ByVal month As String, ByVal cc As String, ByVal bcc As String)
        Dim objemail As EmailSMSsender = New EmailSMSsender()
        Dim indIndentMaster As New IndentMaster()
        Dim mailsubject As String = String.Empty
        Dim mailBody As String = String.Empty
        Dim ToAddress As String = String.Empty
        Dim CCAddress As String = String.Empty
        Dim BCCAddress As String = String.Empty
        Dim response As String = String.Empty
        Dim dsdetail As New DataSet()

        ToAddress = Depotmail
        CCAddress = cc
        BCCAddress = bcc
        mailBody = "Dear sir,<br/>Please upload customer po for indent no - " + IndentId + "<br/> Ho Remark : " + txtRemarks.Text + " .<br/><br/><hr> <p><b>Disclaimer</b> : This is a system generated email. Please do not reply to this email.</p><p>*** If you have received this message in error, please notify the sender immediately and delete this message from your system ***</p></div>"
        mailsubject = "REQUEST FOR UPLOADING CUSTOMER PO (indent no - " + IndentId + " )"
        Dim mailobj As EmailSMSsender = New EmailSMSsender()
        Dim mailEntity As MailEntity = New MailEntity()
        Dim sb As New StringBuilder()
        Dim sbMsg As New StringBuilder()

        If txtRemarks.Text = String.Empty Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "ShowAlert", "alert('Please add HO Remark');", True)
            Exit Sub
        End If

        Dim indent_header As New IndentHeaderEntity()

        indent_header.IndentDepot = DepotCode
        indent_header.IndentFinYear = year
        indent_header.IndentFinMonth = month
        indent_header.IndentID = Convert.ToInt64(IndentId)

        dsdetail = indIndentMaster.GetIndentDetails(indent_header)

        If dsdetail IsNot Nothing AndAlso dsdetail.Tables.Count > 0 AndAlso dsdetail.Tables(0) IsNot Nothing AndAlso dsdetail.Tables(0).Rows.Count > 0 Then

            Dim drRecords As DataTable = dsdetail.Tables(0)

            sb.Append("Dear sir,<br/>Please upload customer po for indent no - " + IndentId + "<br/> Ho Remark : " + txtRemarks.Text + " ")
            sb.Append("<br/><br/>")
            sb.Append("<table style=""border-collapse:collapse;"">")
            sb.Append("<tr style=""font-size:12px; font-weight:bold; background-color:#c2d69a; color:#000000;"">" &
                                  "<td style=""border: thin solid #111111; text-align:center; width:10%; padding: 5px 5px;"">#</td>" &
                                  "<td style=""border: thin solid #111111; text-align:center; width:35%; padding: 5px 5px;"">SKU Code</td>" &
                                  "<td style=""border: thin solid #111111; text-align:center; width:10%; padding: 5px 5px;"">Indent NOP</td>" &
                                  "<td style=""border: thin solid #111111; text-align:center; width:45%; padding: 5px 5px;"">Justification for additional load.</td>")
            Dim index As Integer = 1
            For Each row1 As DataRow In drRecords.Rows
                sb.Append("<tr style=""font-size:11px;"">" &
                                      "<td style=""border: thin solid #111111; text-align:center; padding: 5px 3px;"">" & index.ToString() & "</td>" &
                                      "<td style=""border: thin solid #111111; text-align:left; padding: 5px 3px;"">" & row1("sku_desc").ToString() & "</td>" &
                                      "<td style=""border: thin solid #111111; text-align:center; padding: 5px 3px;"">" & row1("indd_sku_nop").ToString() & "</td>" &
                                      "<td style=""border: thin solid #111111; text-align:left; padding: 5px 3px;"">" & row1("indd_remarks").ToString() & "</td>")
                index += 1
            Next

            sb.Append("</table>")
            sb.Append("<br />")
            sb.Append("<hr> <p><b>Disclaimer</b> : This is a system generated email. Please do not reply to this email.</p><p>*** If you have received this message in error, please notify the sender immediately and delete this message from your system ***</p></div>")

            mailBody = sb.ToString()
        End If




        If ToAddress <> "" Then
            mailEntity.ToAddress = ToAddress
            mailEntity.CCAddress = CCAddress
            mailEntity.BCCAddress = BCCAddress
            mailEntity.MailSubject = mailsubject
            mailEntity.MailBody = mailBody
            mailEntity.Sender_Task = "MailSendToDepot"
            response = "Email Sent Successfully"

            Dim recipNo As Integer = mailobj.sendMail(mailEntity)

            If recipNo = 0 Then
                response = "Email Sent Failed"
                'lblPopMessage.Text = response
                'lblPopMessage.ForeColor = System.Drawing.Color.Red
            Else
                InvoiceRequestMailStatusUpdate(IndentId, DepotCode, year)
                'lblPopMessage.Text = response
                'lblPopMessage.ForeColor = System.Drawing.Color.Green
            End If

            'ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "ShowPopup", "$('#myModalMsg').modal();", True)
            ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "ShowAlert", "alert('" & response & "');", True)
        End If
    End Sub

    Protected Sub InvoiceRequestMailStatusUpdate(ByVal IndentId As String, ByVal depotCode As String, ByVal finYr As String)

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
        Dim msg As String = "Please add HO Remark"

        Try


            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            If txtRemarks.Text = String.Empty Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.[GetType](), "ShowAlert", "alert('" & msg & "');", True)
                Exit Sub
            End If

            RecordUpdated = indtMaster.IndentRequestMailSentUpdate(IndentId, depotCode, finYr, txtRemarks.Text, userInfo.userIDEntity, sqlConn, sqlTrans)


            If (RecordUpdated > 0) Then
                sqlTrans.Commit()
            Else
                sqlTrans.Rollback()
            End If

        Catch ex As Exception

            sqlTrans.Rollback()
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

        Finally

            sqlConn.Close()
            PopulateIndentList()
        End Try
    End Sub

    Protected Sub btnSentMail_Click1(sender As Object, e As EventArgs)
        Dim depotMail As String = hdnemail.Value
        Dim indentId As String = hdnpopindentId.Value
        Dim depotcode As String = hdnpopdepot.Value
        Dim finyr As String = hdnpopfinyr.Value
        Dim finmonth As String = hdnpopfinmonth.Value
        Dim ccmail As String = hdnccmail.Value
        Dim bccmail As String = hdnbccmail.Value
        SendMail(depotMail, indentId, depotcode, finyr, finmonth, ccmail, bccmail)
    End Sub

End Class
