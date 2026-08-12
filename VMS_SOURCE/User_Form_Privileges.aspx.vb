'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : User_Form_Privileges.aspx.vb
'Created Date	: 04-December-2007
'Created By	    : Saravanan
'Version	    : R02.00.00
'Description	: Code behind for User Form Privileges(Read,Add,Edit,Delete,Print,Approval,QuickLink) Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports VMS.Web
Imports System.Data.SqlTypes

Partial Class User_Form_Access
    Inherits System.Web.UI.Page

#Region "Page Load Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.MaintainScrollPositionOnPostBack = True

        If Not IsPostBack Then
            populateUserGroupCode()
            UserPrivilegesGetDetails()
        End If
    End Sub

#End Region

#Region "populateUserGroupCode"

    Public Sub populateUserGroupCode()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New UserProfile
        Dim UserGroupCodeSet As New DataSet

        UserGroupCodeSet = ObjDocumentType.UserGroup_Get(userInfo.userCompanyEntity)
        If (Not (UserGroupCodeSet Is Nothing) AndAlso UserGroupCodeSet.Tables.Count > 0 AndAlso Not (UserGroupCodeSet.Tables(0) Is Nothing) AndAlso UserGroupCodeSet.Tables(0).Rows.Count > 0) Then
            ddlUsrGrp.DataSource = UserGroupCodeSet.Tables(0)
            ddlUsrGrp.DataTextField = "grp_user_group_desc"
            ddlUsrGrp.DataValueField = "grp_user_group_code"
            ddlUsrGrp.DataBind()
        End If

    End Sub

#End Region

#Region "User Privileges Get Details"

    Private Sub UserPrivilegesGetDetails()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim UserPrivilegesGet As New UserFormAccess
        Dim UserPrivilegesList As DataSet

        UserPrivilegesList = UserPrivilegesGet.User_Privileges_Get(userInfo.userCompanyEntity, ddlUsrGrp.SelectedValue)
        If (Not (UserPrivilegesList Is Nothing) AndAlso UserPrivilegesList.Tables.Count > 0) Then
            If (Not (UserPrivilegesList.Tables(0) Is Nothing) AndAlso UserPrivilegesList.Tables(0).Rows.Count > 0) Then
                gvUsrFrmAccess.DataSource = UserPrivilegesList
                gvUsrFrmAccess.DataBind()
                Div_Usr_Frm_Access_Grid.Visible = False
            Else
                gvUsrFrmAccess.DataSource = Nothing
                gvUsrFrmAccess.DataBind()
                Div_Usr_Frm_Access_Grid.Visible = True
            End If
        End If

    End Sub

#End Region

#Region "gvUsrFrmAccess_RowCancelingEdit"

    Protected Sub gvUsrFrmAccess_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        Try
            gvUsrFrmAccess.EditIndex = -1
            UserPrivilegesGetDetails()

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "gvUsrFrmAccess_RowEditing"

    Protected Sub gvUsrFrmAccess_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvUsrFrmAccess.RowEditing

        gvUsrFrmAccess.EditIndex = e.NewEditIndex
        UserPrivilegesGetDetails()

    End Sub

#End Region

#Region "gvUsrFrmAccess_RowUpdating"

    Protected Sub gvUsrFrmAccess_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvUsrFrmAccess.RowUpdating

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim index As Integer = gvUsrFrmAccess.EditIndex
        Dim row As GridViewRow = gvUsrFrmAccess.Rows(index)
        Dim strAccessType As String = ""

        Try

            Dim chkRead As CheckBox = row.FindControl("ChkRead")
            If chkRead.Checked Then
                If strAccessType = "" Then
                    strAccessType = Constant.UserPrivilegesType.Read
                Else
                    strAccessType = strAccessType & "," & Constant.UserPrivilegesType.Read
                End If
            End If

            Dim chkAdd As CheckBox = row.FindControl("ChkAdd")
            If chkAdd.Checked Then
                If strAccessType = "" Then
                    strAccessType = Constant.UserPrivilegesType.Add
                Else
                    strAccessType = strAccessType & "," & Constant.UserPrivilegesType.Add
                End If
            End If

            Dim chkEdit As CheckBox = row.FindControl("ChkEdit")
            If chkEdit.Checked Then
                If strAccessType = "" Then
                    strAccessType = Constant.UserPrivilegesType.Edit
                Else
                    strAccessType = strAccessType & "," & Constant.UserPrivilegesType.Edit
                End If
            End If

            Dim chkDelete As CheckBox = row.FindControl("ChkDelete")
            If chkDelete.Checked Then
                If strAccessType = "" Then
                    strAccessType = Constant.UserPrivilegesType.Delete
                Else
                    strAccessType = strAccessType & "," & Constant.UserPrivilegesType.Delete
                End If
            End If

            Dim chkPrint As CheckBox = row.FindControl("ChkPrint")
            If chkPrint.Checked Then
                If strAccessType = "" Then
                    strAccessType = Constant.UserPrivilegesType.Print
                Else
                    strAccessType = strAccessType & "," & Constant.UserPrivilegesType.Print
                End If
            End If

            Dim chkApproval As CheckBox = row.FindControl("ChkApproval")
            If chkApproval.Checked Then
                If strAccessType = "" Then
                    strAccessType = Constant.UserPrivilegesType.Approval
                Else
                    strAccessType = strAccessType & "," & Constant.UserPrivilegesType.Approval
                End If
            End If

            Dim ddlQuickLink As DropDownList = row.FindControl("ddlQuickLink")

            Dim hdnFormCode As HiddenField = row.FindControl("hdnFormCode")

            Dim numRowsAffected As Integer
            Dim UserPrivilegesUpdate As New UserFormAccess
            numRowsAffected = UserPrivilegesUpdate.UserPrivileges_Update(userInfo.userCompanyEntity, ddlUsrGrp.SelectedValue, strAccessType, ddlQuickLink.SelectedValue, userInfo.userIDEntity, hdnFormCode.Value)

            If numRowsAffected > 0 Then
                gvUsrFrmAccess.EditIndex = -1
                UserPrivilegesGetDetails()
            End If

        Catch ex As Exception


        End Try
    End Sub
#End Region

#Region "gvUsrFrmAccess_RowDataBound"

    Protected Sub gvUsrFrmAccess_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvUsrFrmAccess.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not (rowView("QUICK_LINK").Equals(DBNull.Value)) Then
                Dim cb As DropDownList = e.Row.Cells(8).FindControl("ddlQuickLink")
                cb.SelectedValue = rowView("QUICK_LINK").ToString.Trim
            End If

            If Not (rowView("FORM_ACCESS_TYPE").Equals(DBNull.Value)) Then

                Dim strAccessType As String = rowView("FORM_ACCESS_TYPE").ToString()
                Dim arrayAccessType As String() = strAccessType.Split(",")
                Dim index As Integer = 0

                For index = 0 To arrayAccessType.Length - 1

                    Dim acType As String = arrayAccessType(index)

                    If acType = Constant.UserPrivilegesType.Read Then
                        Dim cb As CheckBox = e.Row.Cells(2).FindControl("chkRead")
                        cb.Checked = True
                    ElseIf acType = Constant.UserPrivilegesType.Add Then
                        Dim cb As CheckBox = e.Row.Cells(3).FindControl("chkAdd")
                        cb.Checked = True
                    ElseIf acType = Constant.UserPrivilegesType.Edit Then
                        Dim cb As CheckBox = e.Row.Cells(4).FindControl("chkEdit")
                        cb.Checked = True
                    ElseIf acType = Constant.UserPrivilegesType.Delete Then
                        Dim cb As CheckBox = e.Row.Cells(5).FindControl("chkDelete")
                        cb.Checked = True
                    ElseIf acType = Constant.UserPrivilegesType.Print Then
                        Dim cb As CheckBox = e.Row.Cells(6).FindControl("chkPrint")
                        cb.Checked = True
                    ElseIf acType = Constant.UserPrivilegesType.Approval Then
                        Dim cb As CheckBox = e.Row.Cells(7).FindControl("chkApproval")
                        cb.Checked = True
                    End If

                Next

            End If
        End If
    End Sub

#End Region

#Region "User Group Selected index Changed Events"

    Protected Sub ddlUsrGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsrGrp.SelectedIndexChanged
        UserPrivilegesGetDetails()
    End Sub

#End Region


End Class
