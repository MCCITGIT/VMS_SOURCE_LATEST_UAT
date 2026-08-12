'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Lov_Details_List.aspx.vb
'Created Date	: 28-November-2007
'Created By	    : Arun 
'Version	    : R02.00.00
'Description	: Code behind for LOVDetails Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class Lov_Master_List
    Inherits System.Web.UI.Page

#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.MaintainScrollPositionOnPostBack = True

        If (Not IsPostBack) Then
            LovMstrListLoad()
        End If
    End Sub
#End Region

#Region "AddAttributes"

    Private Sub AddAttributes()

        btnInsert.Attributes.Add("onClick", "return ValidateLMAdivControls();")
        txtSeq.Attributes.Add("OnKeyPress", "KeyPressNumeric()")

    End Sub
#End Region

#Region "LovMstr List Load"

    Private Sub LovMstrListLoad()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim LovMstrGet As New LovDetails
        Dim LovMstrList As DataSet

        LovMstrList = LovMstrGet.GetLovMstrList(userInfo.userCompanyEntity)
        If (Not (LovMstrList Is Nothing) AndAlso LovMstrList.Tables.Count > 0) Then
            If (Not (LovMstrList.Tables(0) Is Nothing) AndAlso LovMstrList.Tables(0).Rows.Count > 0) Then
                gvLovMstr.DataSource = LovMstrList
                gvLovMstr.DataBind()
                Div_Lov_Mstr_Grid.Visible = False
            Else
                gvLovMstr.DataSource = Nothing
                gvLovMstr.DataBind()
                Div_Lov_Mstr_Grid.Visible = True
            End If
        End If

    End Sub

#End Region

#Region "gvLovMstr_RowCancelingEdit"

    Protected Sub gvLovMstr_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        Try
            gvLovMstr.EditIndex = -1
            LovMstrListLoad()

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "gvLovMstr_RowEditing"

    Protected Sub gvLovMstr_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvLovMstr.RowEditing

        gvLovMstr.EditIndex = e.NewEditIndex
        LovMstrListLoad()

    End Sub

#End Region

#Region "gvLovMstr_RowCommand"

    Protected Sub gvLovMstr_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLovMstr.RowCommand

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        If e.CommandName = "insert" Then


            Dim txtType As TextBox = gvLovMstr.FooterRow.FindControl("txtType")
            Dim Type As String = txtType.Text
            Dim txtDesc As TextBox = gvLovMstr.FooterRow.FindControl("txtDesc")
            Dim Desc As String = txtDesc.Text
            Dim ddlValue As DropDownList = gvLovMstr.FooterRow.FindControl("ddlValue")
            Dim Value As String = ddlValue.SelectedValue
            Dim txtSeq As TextBox = gvLovMstr.FooterRow.FindControl("txtSeq")
            Dim Seq As Integer = txtSeq.Text
            Dim ddlField1 As DropDownList = gvLovMstr.FooterRow.FindControl("ddlField1")
            Dim Field1 As String = ddlField1.SelectedValue
            Dim ddlField2 As DropDownList = gvLovMstr.FooterRow.FindControl("ddlField2")
            Dim Field2 As String = ddlField2.SelectedValue
            Dim ddlField3 As DropDownList = gvLovMstr.FooterRow.FindControl("ddlField3")
            Dim Field3 As String = ddlField3.SelectedValue
            Dim ddlActive As DropDownList = gvLovMstr.FooterRow.FindControl("ddlActive")
            Dim Active As String = ddlActive.SelectedValue

            Dim Numrowsaffected As Integer
            Dim LovMstrAdd As New LovDetails()
            Numrowsaffected = LovMstrAdd.InsertLovMstr(userInfo.userCompanyEntity, Type, Desc, Value, Seq, Field1, Field2, Field3, Active, userInfo.userIDEntity)

            LovMstrListLoad()

        End If

    End Sub
#End Region

#Region "gvLovMstr_RowUpdating"

    Protected Sub gvLovMstr_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvLovMstr.RowUpdating

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim index As Integer = gvLovMstr.EditIndex
        Dim row As GridViewRow = gvLovMstr.Rows(index)


        Try


            Dim txtType As TextBox = row.FindControl("txtType")
            Dim Type As String = txtType.Text
            Dim hdntxtType As HiddenField = row.FindControl("hdntxtType")
            Dim hdnType As String = hdntxtType.Value
            Dim txtDesc As TextBox = row.FindControl("txtDesc")
            Dim Desc As String = txtDesc.Text
            Dim ddlValue As DropDownList = row.FindControl("ddlValue")
            Dim Value As String = ddlValue.SelectedValue
            Dim txtSeq As TextBox = row.FindControl("txtSeq")
            Dim Seq As Integer = txtSeq.Text
            Dim ddlField1 As DropDownList = row.FindControl("ddlField1")
            Dim Field1 As String = ddlField1.SelectedValue
            Dim ddlField2 As DropDownList = row.FindControl("ddlField2")
            Dim Field2 As String = ddlField2.SelectedValue
            Dim ddlField3 As DropDownList = row.FindControl("ddlField3")
            Dim Field3 As String = ddlField3.SelectedValue
            Dim ddlActive As DropDownList = row.FindControl("ddlActive")
            Dim Active As String = ddlActive.SelectedValue
            Dim Recorddeleted As Integer
            Dim LovMstrAdd As New LovDetails()
            Recorddeleted = LovMstrAdd.LovMstrUpdate(userInfo.userCompanyEntity, Type, Desc, Value, Seq, Field1, Field2, Field3, Active, userInfo.userIDEntity, hdnType)
            gvLovMstr.EditIndex = -1
            LovMstrListLoad()

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "gvLovMstr_RowDataBound"

    Protected Sub gvLovMstr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLovMstr.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Dim pageIdx As Integer = gvLovDetails.PageIndex * ddlPageSize.SelectedValue
            'e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim imagebttn As LinkButton = e.Row.Cells(8).FindControl("btnUpdate")
            If Not (imagebttn Is Nothing) Then
                Dim txtType As TextBox = e.Row.Cells(0).FindControl("txtType")
                Dim hdnBox As HiddenField = e.Row.Cells(0).FindControl("hdntxtType")
                'Dim type As String = ddlLOV.SelectedValue
                If Not (txtType Is Nothing) Then
                    txtType.Attributes.Add("onBlur", "return fnCompareLovMstrType(this.value,'" + hdnBox.Value + "');")
                End If
                imagebttn.Attributes.Add("onclick", "return fnValidateForgvLovMstr('" + e.Row.RowIndex.ToString + "');")
            End If
        End If

        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim imagebttn As LinkButton = e.Row.FindControl("btnInsert")
            If Not (imagebttn Is Nothing) Then
                Dim txtType As TextBox = e.Row.FindControl("txtType")
                'Dim type As String = ddlLOV.SelectedValue
                If Not (txtType Is Nothing) Then
                    txtType.Attributes.Add("onBlur", "return fnCompareLovMstrType(this.value,0);")
                End If
                'Dim test As String = gvLovDetails.FooterRow.FindControl("txtType").ID
                imagebttn.Attributes.Add("onclick", "return fnValidateForgvLovMstr('" + e.Row.RowIndex.ToString + "');")

            End If
        End If

    End Sub

#End Region

#Region "btnInsert_Click"
    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnInsert.Click

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim Numrowsaffected As Integer
        Dim LovMstrAdd As New LovDetails()
        Numrowsaffected = LovMstrAdd.InsertLovMstr(userInfo.userCompanyEntity, txtType.Text, txtDesc.Text, ddlValue.SelectedValue, txtSeq.Text, ddlField1.SelectedValue, ddlField1.SelectedValue, ddlField1.SelectedValue, ddlActive.SelectedValue, userInfo.userIDEntity)

        LovMstrListLoad()
    End Sub
#End Region

End Class
