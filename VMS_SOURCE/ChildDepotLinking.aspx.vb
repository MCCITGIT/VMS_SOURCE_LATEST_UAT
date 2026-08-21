Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class ChildDepotLinking
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


            PopulateDepotDropdown()


            RetrieveSearchCriteria()

            PopulateParentDepotGridList()

        End If

    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        btnSubmit.Attributes.Add("onClick", "return ValidateDetails('" + chkbxChildDepotList.ClientID + "','" + hdnParentDepot.ClientID + "','" + lblPopValidationMessage.ClientID + "','" + btnSubmit.ClientID + "');")
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

        indentSearchInfo.IndentDepot = ddlDepot.SelectedValue
        Session(Constant.SessionKeys.IndentListSearchInfo) = indentSearchInfo

    End Sub

#End Region

#Region "Retrieve Search Criteria."

    ' Retrieve the existing search criteria in session
    Private Sub RetrieveSearchCriteria()

        If (Not (Session(Constant.SessionKeys.IndentListSearchInfo) Is Nothing)) Then

            Dim indentSearchInfo As New IndentListSearchCriteria

            indentSearchInfo = Session(Constant.SessionKeys.IndentListSearchInfo)
            ddlDepot.SelectedValue = indentSearchInfo.IndentDepot

        End If

        SaveSearchCriteria()

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

        Dim cmnDepot As New ChildDepotLinkingClass()
        Dim dsDepot As DataSet

        ddlDepot.Items.Clear()

        Try

            dsDepot = cmnDepot.GetParentdepotname(ddlDepot.SelectedValue)

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
            'Server.Transfer(returnUrl)
            Response.Redirect(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate Depot dropdown."

    Private Sub PopulateChildDepotList(ByVal parent_depot As String, ByVal chkList As CheckBoxList)

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New ChildDepotLinkingClass()
        Dim dsDepot As DataSet

        ' chkList.Items.Clear()

        Try

            dsDepot = cmnDepot.GetChildDepotList(parent_depot)

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    chkList.DataSource = dsDepot
                    chkList.DataTextField = "depot_name"
                    chkList.DataValueField = "depot_code"
                    chkList.DataBind()
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region



#Region "Populate Parent Depot Grid."

    Private Sub PopulateParentDepotGridList()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim indIndentMaster As New ChildDepotLinkingClass()
        Dim dsIndentList As DataSet


        dsIndentList = indIndentMaster.GetParentdepotname(ddlDepot.SelectedValue)
        If (Not (dsIndentList Is Nothing)) Then
            gvParentDepotList.Visible = True

            gvParentDepotList.DataSource = dsIndentList.Tables(0)


            gvParentDepotList.DataBind()
        End If


    End Sub
#End Region


    'Protected Sub imgbtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAdd.Click
    '    SaveSearchCriteria()
    '    Response.Redirect("AddUpdateIndentEntry.aspx", True)
    'End Sub

    Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
        SaveSearchCriteria()
        PopulateParentDepotGridList()
        gvParentDepotList.PageIndex = 0
    End Sub


    Protected Sub gvParentDepotList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvParentDepotList.RowDataBound
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            'Dim rdobtnApprove As RadioButton = CType(e.Row.FindControl("rdobtnApprove"), RadioButton)
            'Dim rdobtnReject As RadioButton = CType(e.Row.FindControl("rdobtnReject"), RadioButton)

            e.Row.Cells(1).ForeColor = Drawing.Color.Blue

        End If
        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            'row = e.Row.Controls(0).Controls(0).Controls(0)
            For Each cell As TableCell In row.Cells
                Dim lb As Control = cell.Controls(0)


                If (TypeOf (lb) Is Label) Then

                    'CType(lb, Label).ForeColor = System.Drawing.Color.Red
                    CType(lb, Label).CssClass = "lblpager"
                    'CType(lb, Label).Width = 20
                    'CType(lb, Label).Height = 15
                    'set current pager

                ElseIf (TypeOf (lb) Is LinkButton) Then

                    CType(lb, LinkButton).CssClass = "lnkpager"
                    'CType(lb, LinkButton).Width = 20
                    'CType(lb, LinkButton).Height = 15
                    'CType(lb, LinkButton).ForeColor = Drawing.Color.Black
                End If

            Next
        End If
    End Sub

    Protected Sub gvParentDepotList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvParentDepotList.PageIndexChanging
        Try
            gvParentDepotList.PageIndex = e.NewPageIndex
            PopulateParentDepotGridList()
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Protected Sub gvParentDepotList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvParentDepotList.RowCommand
        Dim gv_row As GridViewRow = Nothing
        Dim index As Integer = Nothing
        ''Dim ul As New SchemeListClass
        Dim numRowsAffected As Integer = 0
        Dim sqlconn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim redirect As Boolean = False
        Dim indtMaster As New IndentMaster
        Dim ds As DataSet
        Dim VendorRank As String = String.Empty

        Try


            If (e.CommandName = "View") Then
                Try
                    Dim userInfo As VMSUserEntity = New VMSUserEntity()
                    If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
                        userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
                    Else
                        Response.Redirect("~/Login.aspx")
                    End If
                    gv_row = CType(CType(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                    index = gv_row.RowIndex

                    Dim parent_depot_code As HiddenField = gvParentDepotList.Rows(index).FindControl("hdnDepotCode")
                    Dim hdnChildDepotCode As HiddenField = gvParentDepotList.Rows(index).FindControl("hdnChildDepotCode")
                    Dim hdnParentDepotName As HiddenField = gvParentDepotList.Rows(index).FindControl("hdnParentDepotName")

                    lblPopupParentDepotHdr.Text = hdnParentDepotName.Value
                    hdnParentDepot.Value = parent_depot_code.Value
                    PopulateChildDepotList(parent_depot_code.Value, chkbxChildDepotList)
                    Dim tmpDepotList() As String = hdnChildDepotCode.Value.ToString.Split(",")
                    For Each lstitm As ListItem In chkbxChildDepotList.Items
                        If lstitm.Value <> String.Empty AndAlso Array.IndexOf(tmpDepotList, lstitm.Value) >= 0 Then
                            lstitm.Selected = True
                        End If
                    Next
                    ModalPopupExtender2.Show()

                Catch ex As Exception
                    sqlTrans.Rollback()
                    lblErrorMessage.Text = "Some Error Occured."
                Finally
                    If Not sqlconn Is Nothing Then
                        sqlconn.Close()
                    End If
                End Try

            End If


        Catch ex As Exception
            Dim returnurl As String = "~/exceptionpage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message.ToString()
            Response.Redirect(returnurl)
        End Try
        PopulateParentDepotGridList()
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
        Dim helper As New ChildDepotLinkingClass
        Dim RecordUpdated As Integer
        Dim DeleteRecord As Integer

        Try

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            DeleteRecord = helper.DeleteLinkedChildDetails(hdnParentDepot.Value, sqlConn, sqlTrans)
            If (chkbxChildDepotList.Items.Count > 0) Then
                For Each lstitm As ListItem In chkbxChildDepotList.Items
                    If lstitm.Selected Then
                        RecordUpdated = helper.InsertUpdateChildDepotLinking(hdnParentDepot.Value, lstitm.Value, userInfo.userIDEntity, sqlConn, sqlTrans)
                    End If
                Next

            End If
            If (RecordUpdated > 0) Then
                sqlTrans.Commit()
                lblPopMessage.Text = "Child Depot Linked Successfully. !!"
                lblPopMessage.ForeColor = System.Drawing.Color.Green
                ModalPopupExtender1.Show()
            Else
                sqlTrans.Rollback()
                lblPopMessage.Text = "Some Thing Went Wrong. !!"
                lblPopMessage.ForeColor = System.Drawing.Color.Red
                ModalPopupExtender1.Show()
            End If

            PopulateParentDepotGridList()

        Catch ex As Exception

            sqlTrans.Rollback()
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

        Finally

            If Not (sqlConn Is Nothing) Then
                sqlConn.Close()
            End If

        End Try
    End Sub



End Class
