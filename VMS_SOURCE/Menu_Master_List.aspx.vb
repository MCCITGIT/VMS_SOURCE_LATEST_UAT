'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Menu_Master_List.aspx.vb
'Created Date	: 29-November-2007
'Created By	    : Arun 
'Version	    : R02.00.00
'Description	: Code behind for LOVDetails Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class Menu_Master_List
    Inherits System.Web.UI.Page

#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.MaintainScrollPositionOnPostBack = True

        If (Not IsPostBack) Then
            AddAttributes()
            ParentMenuListLoad(ddlParentForm)
            MenuMstrListLoad()
        End If
    End Sub
#End Region

#Region "AddAttributes"

    Private Sub AddAttributes()

        'btnInsert.Attributes.Add("onClick", "return ValidateMMLdivControls();")
        ' txtSeq.Attributes.Add("OnKeyPress", "KeyPressNumeric()")

    End Sub
#End Region

#Region "Parent Menu List Load"

    Private Sub ParentMenuListLoad(ByVal ddl As DropDownList)

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If


        Dim MenuMstrGet As New LovDetails
        Dim ParentMenuLit As DataSet

        ParentMenuLit = MenuMstrGet.GetParentMenuList()
        If (Not (ParentMenuLit Is Nothing) AndAlso ParentMenuLit.Tables.Count > 0) Then
            If (Not (ParentMenuLit.Tables(0) Is Nothing) AndAlso ParentMenuLit.Tables(0).Rows.Count > 0) Then
                ddl.DataSource = ParentMenuLit.Tables(0)
                ddl.DataTextField = "fmm_name"
                ddl.DataValueField = "fmm_id"
                ddl.DataBind()
                ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            Else
                ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        End If

    End Sub

#End Region

#Region "MenuMstr List Load"

    Private Sub MenuMstrListLoad()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If


        Dim MenuMstrGet As New LovDetails
        Dim MenuMstrList As DataSet
        Dim parentid As Int64
        If (ddlParentForm.SelectedValue <> "") Then
            parentid = CLng(ddlParentForm.SelectedValue)
        End If
        MenuMstrList = MenuMstrGet.GetMenuMstrList(parentid)
        If (Not (MenuMstrList Is Nothing) AndAlso MenuMstrList.Tables.Count > 0) Then
            If (Not (MenuMstrList.Tables(0) Is Nothing) AndAlso MenuMstrList.Tables(0).Rows.Count > 0) Then
                gvMenuMaster.DataSource = MenuMstrList
                gvMenuMaster.DataBind()
                ' Div_Menu_Master_Grid.Visible = False
            Else
                gvMenuMaster.DataSource = Nothing
                gvMenuMaster.DataBind()
                ' Div_Menu_Master_Grid.Visible = True
            End If
        End If

    End Sub

#End Region

#Region "gvMenuMaster_RowCancelingEdit"

    Protected Sub gvMenuMaster_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        Try
            gvMenuMaster.EditIndex = -1
            MenuMstrListLoad()

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "gvMenuMaster_RowEditing"

    Protected Sub gvMenuMaster_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvMenuMaster.RowEditing

        gvMenuMaster.EditIndex = e.NewEditIndex
        MenuMstrListLoad()

    End Sub

#End Region

#Region "gvMenuMaster_RowCommand"

    Protected Sub gvMenuMaster_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMenuMaster.RowCommand

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        If e.CommandName = "insert" Then
            Dim parentid As Int64 = 0
            Dim ddlParent_ftr As DropDownList = gvMenuMaster.FooterRow.FindControl("ddlParent_ftr")
            If (ddlParent_ftr.SelectedValue <> "") Then
                parentid = Convert.ToInt64(ddlParent_ftr.SelectedValue)
            End If
            Dim txtFrmName_ftr As TextBox = gvMenuMaster.FooterRow.FindControl("txtFrmName_ftr")
            Dim frmName As String = txtFrmName_ftr.Text
            Dim txtFrmLink_ftr As TextBox = gvMenuMaster.FooterRow.FindControl("txtFrmLink_ftr")
            Dim frmlink As String = txtFrmLink_ftr.Text
            Dim txtSeq As TextBox = gvMenuMaster.FooterRow.FindControl("txtFrmSeq_ftr")
            Dim Seq As Integer = txtSeq.Text
            Dim ddlActive As DropDownList = gvMenuMaster.FooterRow.FindControl("ddlActive_ftr")
            Dim Active As String = ddlActive.SelectedValue

            Dim Numrowsaffected As Integer
            Dim MenuMstrAdd As New LovDetails()
            Numrowsaffected = MenuMstrAdd.InsertMenuMstr(parentid, frmName, frmlink, Seq, Active, userInfo.userIDEntity)

            MenuMstrListLoad()

        End If

    End Sub
#End Region

#Region "gvMenuMaster_RowUpdating"

    Protected Sub gvMenuMaster_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvMenuMaster.RowUpdating

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim index As Integer = gvMenuMaster.EditIndex
        Dim row As GridViewRow = gvMenuMaster.Rows(index)


        Try

            Dim ddlParent As DropDownList = row.FindControl("ddlParent")
            Dim parentid As Int64 = Convert.ToInt64(ddlParent.SelectedValue)
            Dim txtFrmName As TextBox = row.FindControl("txtFrmName")
            Dim frmName As String = txtFrmName.Text
            Dim txtFrmLink As TextBox = row.FindControl("txtFrmLink")
            Dim frmlink As String = txtFrmLink.Text
            Dim txtFrmSeq As TextBox = row.FindControl("txtFrmSeq")
            Dim Seq As Integer = txtFrmSeq.Text
            Dim ddlActive As DropDownList = row.FindControl("ddlActive")
            Dim Active As String = ddlActive.SelectedValue
            Dim hdnId As HiddenField = row.FindControl("hdnId")
            Dim frmid=hdnId.Value

            Dim Recordupdated As Integer
            Dim MenuMstrUpdate As New LovDetails()
            Recordupdated = MenuMstrUpdate.MenuMstrUpdate(frmid, parentid, frmName, frmlink, Seq, Active, userInfo.userIDEntity)
            gvMenuMaster.EditIndex = -1
            MenuMstrListLoad()

        Catch ex As Exception
            Dim msg As String = ex.Message
        End Try
    End Sub
#End Region

#Region "gvMenuMaster_RowDataBound"

    Protected Sub gvMenuMaster_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMenuMaster.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim rowview As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim btnEdit As LinkButton = CType(e.Row.FindControl("btnEdit"), LinkButton)
            Dim btnUpdate As LinkButton = CType(e.Row.FindControl("btnUpdate"), LinkButton)
            Dim btnCancel As LinkButton = CType(e.Row.FindControl("btnCancel"), LinkButton)
            Dim txtFrmName As TextBox = CType(e.Row.FindControl("txtFrmName"), TextBox)
            Dim txtFrmLink As TextBox = CType(e.Row.FindControl("txtFrmLink"), TextBox)
            Dim ddlParent As DropDownList = CType(e.Row.FindControl("ddlParent"), DropDownList)
            Dim txtFrmSeq As TextBox = CType(e.Row.FindControl("txtFrmSeq"), TextBox)
            Dim ddlActive As DropDownList = CType(e.Row.FindControl("ddlActive"), DropDownList)

            If ddlActive IsNot Nothing Then
                ParentMenuListLoad(ddlParent)

                ddlParent.SelectedValue = rowview("fmm_parent_id").ToString()
                ddlActive.SelectedValue = rowview("active").ToString()

                ' txtFrmSeq.Attributes.Add("onkeypress", "KeyPressNumeric()")

                'btnUpdate.Attributes.Add("onclick",
                '"return ValidateSubmit('" &
                'txtFrmName.ClientID & "','" &
                'txtFrmLink.ClientID & "','" &
                'ddlParent.ClientID & "','" &
                'txtFrmSeq.ClientID & "','" &
                'btnUpdate.ClientID & "','" &
                'lblErrorMessage.ClientID & "')")
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            'Dim lblSrl_ftr As Label = CType(e.Row.FindControl("lblSrl_ftr"), Label)
            'lblSrl_ftr.Text = (gvMenuMaster.Rows.Count + 1).ToString()

            Dim btnUpdate As LinkButton = CType(e.Row.FindControl("btnSubmit"), LinkButton)
            Dim txtFrmName As TextBox = CType(e.Row.FindControl("txtFrmName_ftr"), TextBox)
            Dim txtFrmLink As TextBox = CType(e.Row.FindControl("txtFrmLink_ftr"), TextBox)
            Dim ddlParent As DropDownList = CType(e.Row.FindControl("ddlParent_ftr"), DropDownList)
            Dim txtFrmSeq As TextBox = CType(e.Row.FindControl("txtFrmSeq_ftr"), TextBox)
            Dim ddlActive As DropDownList = CType(e.Row.FindControl("ddlActive_ftr"), DropDownList)

            ParentMenuListLoad(ddlParent)
            ' txtFrmSeq.Attributes.Add("onkeypress", "KeyPressNumeric()")

            'btnUpdate.Attributes.Add("onclick",
            '"return ValidateSubmit('" &
            'txtFrmName.ClientID & "','" &
            'txtFrmLink.ClientID & "','" &
            'ddlParent.ClientID & "','" &
            'txtFrmSeq.ClientID & "','" &
            'btnUpdate.ClientID & "','" &
            'lblErrorMessage.ClientID & "')")
        End If


    End Sub

#End Region

    Protected Sub ddlParentForm_SelectedIndexChanged(sender As Object, e As EventArgs)
        MenuMstrListLoad()
    End Sub
End Class
