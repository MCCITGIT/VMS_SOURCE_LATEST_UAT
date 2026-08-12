'***************************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : User_Form_Access.aspx.vb
'Created Date	: 27-November-2007
'Created By	    : Arun 
'Version	    : R02.00.00
'Description	: Code behind for UserFormAccess Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class UsrFrmAccssNewMod
    Inherits System.Web.UI.Page


#Region "Page_Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            populateUserGroupCode()
            populateUserID()
            populateAvlbForms()
            populateApplForms()

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

        UserGroupCodeSet = ObjDocumentType.UserGroup_Get((userInfo.userCompanyEntity))
        If (Not (UserGroupCodeSet Is Nothing) AndAlso UserGroupCodeSet.Tables.Count > 0 AndAlso Not (UserGroupCodeSet.Tables(0) Is Nothing) AndAlso UserGroupCodeSet.Tables(0).Rows.Count > 0) Then
            ddlUsrGrp.DataSource = UserGroupCodeSet.Tables(0)
            ddlUsrGrp.DataTextField = "grp_user_group_desc"
            ddlUsrGrp.DataValueField = "grp_user_group_code"
            ddlUsrGrp.DataBind()
            'ddlUsrGrp.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
            'If (Not (Session(Constant.SessionKeys.UPListSearchInfo) Is Nothing)) Then
            '    Dim UPListSearchInfo As UserProfileListSearchCriteria
            '    UPListSearchInfo = CType(Session(Constant.SessionKeys.UPListSearchInfo), UserProfileListSearchCriteria)
            '    ddlUsrGrp.SelectedValue = UPListSearchInfo.UserUserGroup
            'End If
        End If

        populateApplForms()

    End Sub
#End Region

#Region "populateUserID"
    Public Sub populateUserID()
        ddlUsrID.Items.Clear()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New UserFormAccess
        Dim UserIDSet As New DataSet
        Dim UsrGrp As String
        UsrGrp = ddlUsrGrp.SelectedValue
        UserIDSet = ObjDocumentType.UserID_Get(userInfo.userCompanyEntity, UsrGrp)
        If (Not (UserIDSet Is Nothing) AndAlso UserIDSet.Tables.Count > 0 AndAlso Not (UserIDSet.Tables(0) Is Nothing) AndAlso UserIDSet.Tables(0).Rows.Count > 0) Then
            ddlUsrID.DataSource = UserIDSet.Tables(0)
            ddlUsrID.DataTextField = "usp_user_id"
            ddlUsrID.DataValueField = "usp_user_id"
            ddlUsrID.DataBind()
            ddlUsrID.Items.Insert(0, New ListItem(Constant.Common.All, "All", True))

        End If

        populateAvlbForms()

    End Sub
#End Region

#Region "populateAvlbForms"
    Public Sub populateAvlbForms()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New UserFormAccess
        Dim UserFormsSet As New DataSet
        UserFormsSet = ObjDocumentType.UserForms_Get(userInfo.userCompanyEntity, ddlUsrGrp.SelectedValue, ddlUsrID.SelectedValue)
        If (Not (UserFormsSet Is Nothing) AndAlso UserFormsSet.Tables.Count > 0 AndAlso Not (UserFormsSet.Tables(0) Is Nothing) AndAlso UserFormsSet.Tables(0).Rows.Count > 0) Then
            LstAvlbFrms.DataSource = UserFormsSet.Tables(0)
            LstAvlbFrms.DataTextField = "form_desc"
            LstAvlbFrms.DataValueField = "form_code"
            LstAvlbFrms.DataBind()
            'ddlUsrID.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))

        End If
    End Sub
#End Region

#Region "populateApplForms"
    Public Sub populateApplForms()

        LstApplFrms.Items.Clear()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New UserFormAccess
        Dim UserFormSet As New DataSet
        UserFormSet = ObjDocumentType.UserApplForms_Get(userInfo.userCompanyEntity, ddlUsrGrp.SelectedValue, ddlUsrID.SelectedValue)
        If (Not (UserFormSet Is Nothing) AndAlso UserFormSet.Tables.Count > 0 AndAlso Not (UserFormSet.Tables(0) Is Nothing) AndAlso UserFormSet.Tables(0).Rows.Count > 0) Then
            LstApplFrms.DataSource = UserFormSet.Tables(0)
            LstApplFrms.DataTextField = "form_desc"
            LstApplFrms.DataValueField = "form_code"
            LstApplFrms.DataBind()
            'ddlUsrID.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))

        End If
    End Sub
#End Region

#Region "ddlUsrGrp_SelectedIndexChanged"
    Protected Sub ddlUsrGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsrGrp.SelectedIndexChanged
        populateUserID()
        populateAvlbForms()
        populateApplForms()
    End Sub
#End Region

#Region "ddlUsrID_SelectedIndexChanged"
    Protected Sub ddlUsrID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsrID.SelectedIndexChanged

        populateAvlbForms()
        populateApplForms()
    End Sub
#End Region

#Region "btnRL_Click"
    Protected Sub btnRL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRL.Click

        Dim i As Integer
        Dim col As New Collection
        For i = 0 To LstAvlbFrms.Items.Count - 1
            If LstAvlbFrms.Items(i).Selected = True Then
                LstApplFrms.Items.Insert(0, New ListItem(LstAvlbFrms.Items(i).Text, LstAvlbFrms.Items(i).Value))
                col.Add(LstAvlbFrms.Items(i))
                'LstAvlbFrms.Items.RemoveAt(i)
            End If
        Next
        If col.Count > 0 Then
            For k As Integer = 1 To col.Count
                LstAvlbFrms.Items.Remove(CType(col(k), ListItem))
            Next
        End If
    End Sub
#End Region

#Region "btnLR_Click"
    Protected Sub btnLR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLR.Click

        Dim i As Integer
        'Dim col_LR As New Collection
        For i = 0 To LstApplFrms.Items.Count - 1
            If LstApplFrms.Items(i).Selected = True Then
                LstAvlbFrms.Items.Insert(0, New ListItem(LstApplFrms.Items(i).Text, LstApplFrms.Items(i).Value))
                UserLogin.col_LR.Add(LstApplFrms.Items(i))
            End If
        Next

        If UserLogin.col_LR.Count > 0 Then
            For k As Integer = 1 To UserLogin.col_LR.Count
                LstApplFrms.Items.Remove(CType(UserLogin.col_LR(k), ListItem))
            Next
        End If


    End Sub
#End Region

#Region "btnSubmit_Click"

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim groupcode As String = ddlUsrGrp.SelectedValue
        Dim userid As String = ddlUsrID.SelectedValue
        Dim desc As String
        Dim code As String
        Dim ss As Integer
        If UserLogin.col_LR.Count > 0 Then
            For k As Integer = 1 To UserLogin.col_LR.Count
                ss = CType(UserLogin.col_LR(k), ListItem).Value
                'LstApplFrms.Items.Remove(CType(col_LR(k), ListItem))
                Dim Numrowsdeleted As Integer
                Dim UsrFrmDel As New UserFormAccess()
                Numrowsdeleted = UsrFrmDel.DeleteUsrFrm(userInfo.userCompanyEntity, ss, groupcode, userid)
            Next
        End If
        'Dim Numrowsdeleted As Integer
        'Dim UsrFrmDel As New UserFormAccess()
        'Numrowsdeleted = UsrFrmDel.DeleteUsrFrm(userInfo.userCompanyEntity, userInfo.userIDEntity, groupcode, userid)

        Dim i As Integer
        Dim Numrowsaffected As Integer
        For i = 0 To LstApplFrms.Items.Count - 1
            desc = LstApplFrms.Items(i).Text
            code = LstApplFrms.Items(i).Value

            Dim UsrFrmAdd As New UserFormAccess()
            Numrowsaffected = UsrFrmAdd.InsertUsrFrm(userInfo.userCompanyEntity, desc, code, userInfo.userIDEntity, groupcode, userid)

        Next
        'If (Numrowsaffected > 0) Then
        '    Response.Redirect("~/User_Form_Access.aspx")
        'End If
    End Sub

#End Region

#Region "btnCancel_Click"
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/home.aspx")
    End Sub
#End Region

#Region "Reset button Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("~/UsrFrmAccssNewMod.aspx")
    End Sub
#End Region

End Class
