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
Partial Class Lov_Details
    Inherits System.Web.UI.Page

#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.MaintainScrollPositionOnPostBack = True

        If (Not IsPostBack) Then
            AddAttributes()
            populateLovType()
            RetrieveSearchCriteria()
            LovDetailsListLoad()

        End If
    End Sub
#End Region

#Region "AddAttributes"

    Private Sub AddAttributes()

        btnInsert.Attributes.Add("onClick", "return ValidateLDLdivControls();")
        txtSeq.Attributes.Add("OnKeyPress", "KeyPressNumeric()")

    End Sub
#End Region

#Region "Save Search Criteria"
    ' Saves the current search criteria in session
    Private Sub SaveSearchCriteria()
        Dim LovDetailsSearchInfo As New LovDetailsSearchCriteria
        LovDetailsSearchInfo.LovType = ddlLOV.SelectedValue

        Session(Constant.SessionKeys.LovDetailsSearchInfo) = LovDetailsSearchInfo


    End Sub
#End Region

#Region "Retrieve Search Criteria"
    ' Retrieve the existing search criteria in session
    Private Sub RetrieveSearchCriteria()
        If (Not (Session(Constant.SessionKeys.LovDetailsSearchInfo) Is Nothing)) Then
            Dim LovDetailsSearchInfo As New LovDetailsSearchCriteria
            LovDetailsSearchInfo = Session(Constant.SessionKeys.LovDetailsSearchInfo)
            ddlLOV.SelectedValue = LovDetailsSearchInfo.LovType


        End If

    End Sub
#End Region

#Region "LovDetails List Load"

    Private Sub LovDetailsListLoad()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim LovDetailsGet As New LovDetails
        Dim LovDetailsList As DataSet
        Dim lovtype As String
        lovtype = ddlLOV.SelectedValue

        LovDetailsList = LovDetailsGet.GetLovDetailsList(userInfo.userCompanyEntity, lovtype)
        If (Not (LovDetailsList Is Nothing) AndAlso LovDetailsList.Tables.Count > 0) Then
            If (Not (LovDetailsList.Tables(0) Is Nothing) AndAlso LovDetailsList.Tables(0).Rows.Count > 0) Then
                gvLovDetails.DataSource = LovDetailsList
                gvLovDetails.DataBind()
                Div_Lov_Details_Grid.Visible = False
            Else
                gvLovDetails.DataSource = Nothing
                gvLovDetails.DataBind()
                Div_Lov_Details_Grid.Visible = True
                txtType.Text = ""
                txtDesc.Text = ""
                txtValue.Text = ""
                txtSeq.Text = ""
                txtField1.Text = ""
                txtField2.Text = ""
                txtField3.Text = ""
            End If
        End If

    End Sub

#End Region

#Region "Populate LovType"
    Public Sub populateLovType()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim StatusTypeSet As New DataSet
        StatusTypeSet = ObjDocumentType.GetLovTypeDetails(userInfo.userCompanyEntity)
        If (Not (StatusTypeSet Is Nothing) AndAlso StatusTypeSet.Tables.Count > 0 AndAlso Not (StatusTypeSet.Tables(0) Is Nothing) AndAlso StatusTypeSet.Tables(0).Rows.Count > 0) Then
            ddlLOV.DataSource = StatusTypeSet.Tables(0)
            ddlLOV.DataTextField = "Lov_type"
            ddlLOV.DataValueField = "Lov_type"
            ddlLOV.DataBind()
        End If
    End Sub
#End Region

#Region "Date Format"

    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime

        If Not (stringdate = String.Empty) Then

            Dim ddate As String() = stringdate.Split("/")
            Dim arrlist As New ArrayList
            Dim index As Integer = 0

            While index <= ddate.Length - 1
                arrlist.Add(ddate(index))
                System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
            End While
            Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(0))
            Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
            Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(2))

            Dim dt As DateTime = New DateTime(yyyy, mm, dd)
            dt = FormatDateTime(dt, DateFormat.LongDate)

            Return dt
        End If

    End Function

#End Region

#Region "ddlLOV_SelectedIndexChanged"
    Protected Sub ddlLOV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLOV.SelectedIndexChanged
        SaveSearchCriteria()
        LovDetailsListLoad()
    End Sub
#End Region

#Region "gvLovDetails_RowCancelingEdit"

    Protected Sub gvLovDetails_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        Try
            gvLovDetails.EditIndex = -1
            LovDetailsListLoad()

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "gvLovDetails_RowCommand"

    Protected Sub gvLovDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLovDetails.RowCommand

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        If e.CommandName = "insert" Then

            Dim Type As String = ddlLOV.SelectedValue
            Dim txtCode As TextBox = gvLovDetails.FooterRow.FindControl("txtCode")
            Dim Code As String = txtCode.Text
            Dim txtDesc As TextBox = gvLovDetails.FooterRow.FindControl("txtDesc")
            Dim Desc As String = txtDesc.Text
            Dim txtValue As TextBox = gvLovDetails.FooterRow.FindControl("txtValue")
            Dim Value As String = txtValue.Text
            Dim txtSeq As TextBox = gvLovDetails.FooterRow.FindControl("txtSeq")
            Dim Seq As Integer = txtSeq.Text
            Dim txtField1 As TextBox = gvLovDetails.FooterRow.FindControl("txtField1")
            Dim Field1 As String = txtField1.Text
            Dim txtField2 As TextBox = gvLovDetails.FooterRow.FindControl("txtField2")
            Dim Field2 As String = txtField2.Text
            Dim txtField3 As TextBox = gvLovDetails.FooterRow.FindControl("txtField3")
            Dim Field3 As String = txtField3.Text
            Dim ddlActive As DropDownList = gvLovDetails.FooterRow.FindControl("ddlActive")
            Dim Active As String = ddlActive.SelectedValue

            Dim Numrowsaffected As Integer
            Dim LovDetailsAdd As New LovDetails()
            Numrowsaffected = LovDetailsAdd.InsertLovDetails(userInfo.userCompanyEntity, Type, Desc, Value, Seq, Field1, Field2, Field3, Active, userInfo.userIDEntity, Code)
            'populateLovType()
            LovDetailsListLoad()

        End If

    End Sub
#End Region

#Region "gvLovDetails_RowDataBound"

    Protected Sub gvLovDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLovDetails.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Dim pageIdx As Integer = gvLovDetails.PageIndex * ddlPageSize.SelectedValue
            'e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim imagebttn As LinkButton = e.Row.Cells(7).FindControl("btnUpdate")
            If Not (imagebttn Is Nothing) Then
                Dim txtBox As TextBox = e.Row.Cells(0).FindControl("txtCode")
                Dim hdnBox As HiddenField = e.Row.Cells(0).FindControl("hdntxtCode")
                'Dim type As String = ddlLOV.SelectedValue
                If Not (txtBox Is Nothing) Then
                    txtBox.Attributes.Add("onBlur", "return fnCompareLovDetailsCode(this.value,'" + hdnBox.Value + "','" + ddlLOV.SelectedValue + "');")
                End If
                imagebttn.Attributes.Add("onclick", "return fnValidateForgvLovDetails('" + e.Row.RowIndex.ToString + "');")
            End If
        End If

        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim imagebttn As LinkButton = e.Row.FindControl("btnInsert")
            If Not (imagebttn Is Nothing) Then
                Dim txtBox As TextBox = e.Row.FindControl("txtCode")
                'Dim type As String = ddlLOV.SelectedValue
                If Not (txtBox Is Nothing) Then
                    txtBox.Attributes.Add("onBlur", "return fnCompareLovDetailsCode(this.value,0,'" + ddlLOV.SelectedValue + "');")
                End If
                'Dim test As String = gvLovDetails.FooterRow.FindControl("txtType").ID
                imagebttn.Attributes.Add("onclick", "return fnValidateForgvLovDetails('" + e.Row.RowIndex.ToString + "');")

            End If
        End If

    End Sub

#End Region

#Region "gvLovDetails_RowEditing"

    Protected Sub gvLovDetails_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvLovDetails.RowEditing

        gvLovDetails.EditIndex = e.NewEditIndex
        LovDetailsListLoad()

    End Sub

#End Region

#Region "gvLovDetails_RowUpdating"

    Protected Sub gvLovDetails_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvLovDetails.RowUpdating

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim index As Integer = gvLovDetails.EditIndex
        Dim row As GridViewRow = gvLovDetails.Rows(index)

        Try

            Dim Type As String = ddlLOV.SelectedValue
            Dim txtCode As TextBox = row.FindControl("txtCode")
            Dim Code As String = txtCode.Text
            Dim hdntxtCode As HiddenField = row.FindControl("hdntxtCode")
            Dim hdnCode As String = hdntxtCode.Value
            Dim txtDesc As TextBox = row.FindControl("txtDesc")
            Dim Desc As String = txtDesc.Text
            Dim txtValue As TextBox = row.FindControl("txtValue")
            Dim Value As String = txtValue.Text
            Dim txtSeq As TextBox = row.FindControl("txtSeq")
            Dim Seq As Integer = txtSeq.Text
            Dim txtField1 As TextBox = row.FindControl("txtField1")
            Dim Field1 As String = txtField1.Text
            Dim txtField2 As TextBox = row.FindControl("txtField2")
            Dim Field2 As String = txtField2.Text
            Dim txtField3 As TextBox = row.FindControl("txtField3")
            Dim Field3 As String = txtField3.Text
            Dim ddlActive As DropDownList = row.FindControl("ddlActive")
            Dim Active As String = ddlActive.SelectedValue
            Dim Recorddeleted As Integer
            Dim LovDetailsAdd As New LovDetails()
            Recorddeleted = LovDetailsAdd.LovDetailsUpdate(userInfo.userCompanyEntity, Type, Desc, Value, Seq, Field1, Field2, Field3, Active, userInfo.userIDEntity, Code, hdnCode)
            gvLovDetails.EditIndex = -1
            LovDetailsListLoad()

        Catch ex As Exception

        End Try
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
        Dim LovDetailsAdd As New LovDetails()
        Numrowsaffected = LovDetailsAdd.InsertLovDetails(userInfo.userCompanyEntity, ddlLOV.SelectedValue, txtDesc.Text, txtValue.Text, txtSeq.Text, txtField1.Text, txtField2.Text, txtField3.Text, ddlActive.SelectedValue, userInfo.userIDEntity, txtType.Text)
        'populateLovType()
        LovDetailsListLoad()
    End Sub
#End Region

End Class
