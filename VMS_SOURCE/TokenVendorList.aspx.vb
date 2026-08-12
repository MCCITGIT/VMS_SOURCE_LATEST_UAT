'**************************************************
'Source	        : TokenVendorList.aspx.vb
'Created Date	: 13-09-2018
'Created By	    : Debayan Das 
'Version	    : R01.00.00
'Description	: Code behind for Vendor Profile List Page

'Modified By       Modified On       Version         Reason

'**************************************************
Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Partial Class TokenVendorList
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page Load Event Handler"
    'Page load event handler occurs at the time of page and page post back
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            PopulateUnit()
            BindGrid()
            AddAttributes()

        End If

    End Sub
#End Region

#Region "AddAttributes"
    Private Sub AddAttributes()
        'ImgbtnAdd.Attributes.Add("OnClick", "return ValidateVandorUnit();")
    End Sub
#End Region
#Region "Vendor Profile Grid Row DataBound"
    Protected Sub gvVendorProfile_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVendorList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblCode As Label = CType(e.Row.FindControl("lblvendorcode"), Label)
            lblCode.Text = "<a style='color:blue' href='TokenVendorAddUpdate.aspx?vendorCode=" & lblCode.Text & "' >" & lblCode.Text & "</a>"

        End If

        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            row = e.Row.Controls(0).Controls(0).Controls(0)
            For Each cell As TableCell In row.Cells
                Dim lb As Control = cell.Controls(0)


                If (TypeOf (lb) Is Label) Then

                    'CType(lb, Label).ForeColor = System.Drawing.Color.Red
                    CType(lb, Label).CssClass = "lblpager"
                    CType(lb, Label).Width = 20
                    CType(lb, Label).Height = 15
                    'set current pager

                ElseIf (TypeOf (lb) Is LinkButton) Then

                    CType(lb, LinkButton).CssClass = "lnkpager"
                    CType(lb, LinkButton).Width = 20
                    CType(lb, LinkButton).Height = 15
                    CType(lb, LinkButton).ForeColor = Drawing.Color.Black
                End If

            Next
        End If
    End Sub
#End Region

#Region "BindGrid"
    Protected Sub BindGrid()

        Dim userInfo As VMSUserEntity = New VMSUserEntity
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Try
            Dim VendorSet As New DataSet
            Dim obj As New TokenVendorListClass
            VendorSet = obj.GetTokenVendorList(ddlVendorUnit.SelectedValue, txtSearch.Text, ddlActive.SelectedValue)
            If (Not (VendorSet Is Nothing) AndAlso VendorSet.Tables.Count > 0 AndAlso Not (VendorSet.Tables(0) Is Nothing) AndAlso VendorSet.Tables(0).Rows.Count > 0) Then
                gvVendorList.Visible = True
                gvVendorList.DataSource = VendorSet.Tables(0)
                gvVendorList.DataBind()
                'Div_Vendor_List_Grid.Visible = False
            Else
                gvVendorList.Visible = False
                'Div_Vendor_List_Grid.Visible = True
                'ddlWeekSelect.SelectedValue = Session(Constant.SessionKeys.CurrentWeek)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorBindingGrid
            Server.Transfer(returnUrl)
        End Try
    End Sub
#End Region

#Region "Grid Page Index Changed"
    ' Event Handler for Page Changing
    Protected Sub gvVendorProfile_IndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvVendorList.PageIndex = e.NewPageIndex
        BindGrid()
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
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            End If
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

#Region "Add Click Event"
    'Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
    '    Response.Redirect("TokenVendorAddUpdate.aspx", False)
    'End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("TokenVendorAddUpdate.aspx", False)
    End Sub

#End Region
    'Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
    '    gvVendorList.PageIndex = 0
    '    BindGrid()
    'End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        gvVendorList.PageIndex = 0
        BindGrid()
    End Sub

End Class
