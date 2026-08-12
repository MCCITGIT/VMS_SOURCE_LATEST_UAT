'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : UnitApplicableProductAssign.aspx.vb
'Created Date	: 13-09-2018
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
Imports System.Collections.Generic

Partial Class UnitApplicableProductAssign
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

        If Not IsPostBack Then

            AddAttributes()
            PopulateUnit()
            PopulateVendorUnitProduct()
            gvProductList.PageIndex = 0
            BindGrid()

        End If

    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        btnSubmit.OnClientClick = "return validateSubmit();"
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

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Populate Product dropdown."

    Private Sub PopulateVendorUnitProduct()

        CheckLogin()
        Try
            Dim obj As New UnitApplicableProductAssignClass
            Dim dsProductSet As New DataSet

            dsProductSet = obj.GetProductNameFromUnit(ddlVendorUnit.SelectedValue)
            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                ddlVendorProduct.DataSource = dsProductSet.Tables(0)
                ddlVendorProduct.DataTextField = "sku_desc"
                ddlVendorProduct.DataValueField = "sku_new_code"
                ddlVendorProduct.DataBind()

                If Not (dsProductSet.Tables(0).Rows.Count = 1) Then
                    ddlVendorProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

#End Region
#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim obj As New UnitApplicableProductAssignClass
            Dim dsProductSet As New DataSet

            dsProductSet = obj.GetProductList(ddlVendorUnit.SelectedValue, ddlVendorProduct.SelectedValue, ddlActive.SelectedValue)
            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvProductList.DataSource = dsProductSet.Tables(0)
                gvProductList.DataBind()
            Else
                gvProductList.DataSource = Nothing
                gvProductList.DataBind()
            End If
            Dim flag = False
            For Each gridRow As GridViewRow In gvProductList.Rows
                Dim chk1 As CheckBox = CType(gridRow.FindControl("chkAppl"), CheckBox)
                If (chk1 IsNot Nothing) Then
                    If (chk1.Checked) Then
                        flag = True
                        Exit For
                    Else
                        flag = False
                    End If
                End If
            Next
            If (flag) Then
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 2).Visible = True
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 3).Visible = True
                For Each gridRow As GridViewRow In gvProductList.Rows
                    gridRow.Cells(gridRow.Cells.Count - 2).Visible = True
                    gridRow.Cells(gridRow.Cells.Count - 3).Visible = True
                Next

            Else
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 2).Visible = False
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 3).Visible = False
                For Each gridRow As GridViewRow In gvProductList.Rows
                    gridRow.Cells(gridRow.Cells.Count - 2).Visible = False
                    gridRow.Cells(gridRow.Cells.Count - 3).Visible = False
                Next

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try

    End Sub
#End Region


    Protected Sub ddlVendorUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorUnit.SelectedIndexChanged
        PopulateVendorUnitProduct()
    End Sub
    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvProductList.PageIndexChanging
        gvProductList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    'Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
    '    BindGrid()
    'End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        BindGrid()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        lblErrorMessage.Text = String.Empty
        CheckLogin()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New UnitApplicableProductAssignClass
        Dim obj1 As New UnitApplicableVendorAssignClass
        Dim list As New List(Of Integer)
        Dim RecordUpdated As Integer
        Dim status As String = String.Empty
        Try


            'Dim flag1 = False
            'For Each gridRow As GridViewRow In gvProductList.Rows
            '    Dim chk1 As CheckBox = CType(gridRow.FindControl("chkAppl"), CheckBox)
            '    If (chk1 IsNot Nothing) Then
            '        If (chk1.Checked) Then
            '            flag1 = True
            '            Exit For
            '        Else
            '            flag1 = False
            '        End If
            '    End If
            'Next
            'If Not (flag1) Then
            '    lblErrorMessage.Text = "Please select at least one product."
            '    Exit Sub
            'Else
            Dim chkflag = False
            For Each gridRow As GridViewRow In gvProductList.Rows
                Dim chk1 As CheckBox = CType(gridRow.FindControl("chkAppl"), CheckBox)
                If (chk1 IsNot Nothing) Then
                    If (chk1.Checked) Then
                        chkflag = True
                        Exit For
                    Else
                        chkflag = False
                    End If
                End If
            Next
            If Not (chkflag) Then
                lblErrorMessage.Text = "Please select atleast one record."
                Exit Sub
            End If
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            For RowIndex As Integer = 0 To gvProductList.Rows.Count - 1
                Dim row As GridViewRow = gvProductList.Rows(RowIndex)

                Dim chk As CheckBox = CType(row.FindControl("chkAppl"), CheckBox)
                Dim chkd As Boolean = chk.Checked

                If (chkd = True) Then
                    status = Constant.Common.ActiveStatus
                Else
                    status = Constant.Common.InActiveStatus
                End If
                Dim hdnUnit As HiddenField = CType(gvProductList.Rows(RowIndex).FindControl("hdnUnit"), HiddenField)
                Dim hdnActive As HiddenField = CType(gvProductList.Rows(RowIndex).FindControl("hdnActive"), HiddenField)
                Dim hdnSkuCode As HiddenField = CType(gvProductList.Rows(RowIndex).FindControl("hdnSkuCode"), HiddenField)
                Dim txtDenomination As TextBox = CType(gvProductList.Rows(RowIndex).FindControl("txtDenomination"), TextBox)
                Dim ddlTokenVendor As DropDownList = CType(gvProductList.Rows(RowIndex).FindControl("ddlTokenVendor"), DropDownList)

                If (hdnUnit IsNot Nothing And hdnActive IsNot Nothing And hdnSkuCode IsNot Nothing And txtDenomination IsNot Nothing And ddlTokenVendor IsNot Nothing) Then
                    Dim denomination As Decimal = 0
                    If (Decimal.TryParse(txtDenomination.Text.Trim, denomination)) Then
                        denomination = Decimal.Parse(txtDenomination.Text)
                    Else
                        denomination = 0.0
                    End If
                    RecordUpdated = obj.InsertApplicableProduct(hdnUnit.Value.Trim, hdnSkuCode.Value.Trim, denomination, ddlTokenVendor.SelectedValue.Trim, userInfo.userIDEntity, status, sqlConn, sqlTrans)

                End If
                list.Add(RecordUpdated)


            Next
            Dim flag As Integer = 0
            For x As Integer = 0 To list.Count - 1
                If (list(x) > 0) Then
                    flag = list(x)
                ElseIf (list(x) = -1) Then
                    flag = -1
                    Exit For
                Else
                    flag = 0
                    Exit For
                End If
            Next
            If (flag > 0) Then
                sqlTrans.Commit()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submitted Successfully.');", True)
            ElseIf (flag = 0) Then
                sqlTrans.Rollback()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submission Failed!');", True)
            ElseIf (flag = -1) Then
                sqlTrans.Rollback()
                Throw New Exception
            End If
            'End If


        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect("~/ExceptionPage.aspx")

        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
            BindGrid()
        End Try

    End Sub
    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkAll As CheckBox = CType(gvProductList.HeaderRow.FindControl("chkAll"), CheckBox)
        For RowIndex As Integer = 0 To gvProductList.Rows.Count - 1
            Dim row As GridViewRow = gvProductList.Rows(RowIndex)
            Dim chk As CheckBox = CType(row.FindControl("chkAppl"), CheckBox)
            If (chkAll.Checked) Then
                chk.Checked = True
                chkAppl_CheckedChanged(chk, New EventArgs)
            Else
                chk.Checked = False
                chkAppl_CheckedChanged(chk, New EventArgs)
            End If
        Next
    End Sub
    Protected Sub gvProductList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvProductList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlTokenVendor"), DropDownList)
            Dim hdnTokenVendor As HiddenField = CType(e.Row.FindControl("hdnTokenVendor"), HiddenField)
            PopulateTokenVendor(ddl)
            If (ddl.Items.Count > 0 And Not (hdnTokenVendor.Value.Equals(String.Empty))) Then
                ddl.SelectedItem.Selected = False
                ddl.SelectedValue = hdnTokenVendor.Value
            End If
            Dim hdnStatus As HiddenField = CType(e.Row.FindControl("hdnStatus"), HiddenField)
            If (hdnStatus.Value.Equals(Constant.Common.ActiveStatus)) Then
                e.Row.BackColor = Drawing.Color.LightGreen
                CType(e.Row.Cells(e.Row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).Visible = True
                CType(e.Row.Cells(e.Row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Visible = True

            ElseIf (hdnStatus.Value.Equals(Constant.Common.InActiveStatus)) Then
                'e.Row.BackColor = Drawing.Color.LightCoral
                CType(e.Row.Cells(e.Row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).Visible = False
                CType(e.Row.Cells(e.Row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Visible = False
            Else
                CType(e.Row.Cells(e.Row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).Visible = False
                CType(e.Row.Cells(e.Row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Visible = False
            End If
            CType(e.Row.Cells(e.Row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Attributes.Add("onblur", "return isDecimal('" & CType(e.Row.Cells(e.Row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).ClientID & "');")

        End If
        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            row = e.Row.Controls(0).Controls(0).Controls(0)
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

    Protected Sub chkAppl_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            If (chk IsNot Nothing) Then
                Dim row As GridViewRow = CType(chk.Parent.Parent, GridViewRow)
                If (chk.Checked) Then
                    CType(row.Cells(row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).Visible = True
                    CType(row.Cells(row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Visible = True
                    CType(row.Cells(row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).SelectedItem.Selected = False
                    CType(row.Cells(row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).Items(0).Selected = True
                    CType(row.Cells(row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Text = String.Empty
                Else
                    CType(row.Cells(row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).Visible = False
                    CType(row.Cells(row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).SelectedItem.Selected = False
                    CType(row.Cells(row.Cells.Count - 2).FindControl("ddlTokenVendor"), DropDownList).Items(0).Selected = True
                    CType(row.Cells(row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Text = String.Empty
                    CType(row.Cells(row.Cells.Count - 3).FindControl("txtDenomination"), TextBox).Visible = False
                End If
                row.BackColor = IIf(row.RowIndex Mod 2 <> 0, Drawing.Color.WhiteSmoke, Drawing.Color.White)
            Else

            End If
            Dim flag = False
            For Each gridRow As GridViewRow In gvProductList.Rows
                Dim chk1 As CheckBox = CType(gridRow.FindControl("chkAppl"), CheckBox)
                If (chk1 IsNot Nothing) Then
                    If (chk1.Checked) Then
                        flag = True
                        Exit For
                    Else
                        flag = False
                    End If
                End If
            Next
            If (flag) Then
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 2).Visible = True
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 3).Visible = True
                For Each gridRow As GridViewRow In gvProductList.Rows
                    gridRow.Cells(gridRow.Cells.Count - 2).Visible = True
                    gridRow.Cells(gridRow.Cells.Count - 3).Visible = True
                Next

            Else
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 2).Visible = False
                gvProductList.HeaderRow.Cells(gvProductList.HeaderRow.Cells.Count - 3).Visible = False
                For Each gridRow As GridViewRow In gvProductList.Rows
                    gridRow.Cells(gridRow.Cells.Count - 2).Visible = False
                    gridRow.Cells(gridRow.Cells.Count - 3).Visible = False
                Next

            End If
        Catch ex As Exception

        End Try
    End Sub

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
            Response.Redirect(returnUrl)
        End Try

    End Sub
#End Region

End Class
