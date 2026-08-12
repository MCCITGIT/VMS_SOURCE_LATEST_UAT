Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class TestCaseTestMasterAddUpdate
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
        AddAttributes()
        If Not IsPostBack Then
            PopulateFrequency()
            'PopulateUOM()
            PopulateResultType()
            ddlResultType_SelectedIndexChanged(Nothing, Nothing)
            PopulateResultSubType()
            If Not (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                PopulateData()
            End If
        End If

    End Sub

#End Region

#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        imgbtnAdd.OnClientClick = "return ValidateAddOption();"
        btnSubmit.OnClientClick = "return ValidateSubmit();"
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

#Region "Populate Dropdown"
    Private Sub PopulateFrequency()
        CheckLogin()
        Try
            Dim obj As New Common
            Dim dsUnitSet As New DataSet
            ddlFrequency.Items.Clear()
            dsUnitSet = obj.GetLovDetails("Berger", "TC_FREQUENCY", "Y")
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlFrequency.DataSource = dsUnitSet.Tables(0)
                ddlFrequency.DataTextField = "Lov_Value"
                ddlFrequency.DataValueField = "Lov_Code"
                ddlFrequency.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlFrequency.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
    'Private Sub PopulateUOM()
    '    CheckLogin()
    '    Try
    '        Dim obj As New Common
    '        Dim dsUnitSet As New DataSet

    '        dsUnitSet = obj.GetLovDetails("Berger", "TC_UOM", "Y")
    '        ddlUOM.Items.Clear()
    '        If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
    '            ddlUOM.DataSource = dsUnitSet.Tables(0)
    '            ddlUOM.DataTextField = "Lov_Value"
    '            ddlUOM.DataValueField = "Lov_Code"
    '            ddlUOM.DataBind()
    '            If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
    '                ddlUOM.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim returnUrl As String = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '        Server.Transfer(returnUrl)
    '    End Try

    'End Sub
    Private Sub PopulateResultType()
        CheckLogin()
        Try
            Dim obj As New Common
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetLovDetails("Berger", "TC_TEST_TYPE", "Y")
            ddlResultType.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlResultType.DataSource = dsUnitSet.Tables(0)
                ddlResultType.DataTextField = "Lov_Value"
                ddlResultType.DataValueField = "Lov_Code"
                ddlResultType.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlResultType.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
    Private Sub PopulateResultSubType()
        CheckLogin()
        Try
            Dim obj As New Common
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetLovDetails("Berger", "TC_TEST_OPERATOR", "Y")
            ddlResultSubType.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlResultSubType.DataSource = dsUnitSet.Tables(0)
                ddlResultSubType.DataTextField = "Lov_Value"
                ddlResultSubType.DataValueField = "Lov_Code"
                ddlResultSubType.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlResultSubType.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region
    Private Sub PopulateData()
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsProductSet As New DataSet
            If (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                lblReqId.ForeColor = Drawing.Color.Red
                btnSubmit.Style.Remove("display")
            Else
                dsProductSet = obj.GetTestData(Convert.ToInt32(Request.QueryString("id")), userInfo.userIDEntity)
                txtTestName.Text = dsProductSet.Tables(0).Rows(0)("th_test_name").ToString()
                txtTestName.Enabled = False
                ddlFrequency.SelectedValue = dsProductSet.Tables(0).Rows(0)("th_frequency").ToString()
                If Not String.IsNullOrEmpty(dsProductSet.Tables(0).Rows(0)("th_frequency").ToString()) Then
                    ddlFrequency.Enabled = False
                End If
                'ddlUOM.SelectedValue = dsProductSet.Tables(1).Rows(0)("td_uom").ToString()
                txtUOM.Text = dsProductSet.Tables(1).Rows(0)("td_uom").ToString()
                'If Not String.IsNullOrEmpty(dsProductSet.Tables(1).Rows(0)("td_uom").ToString()) Then
                '    txtUOM.Enabled = False
                'End If
                ddlResultType.SelectedValue = dsProductSet.Tables(0).Rows(0)("th_type").ToString()
                If Not String.IsNullOrEmpty(dsProductSet.Tables(0).Rows(0)("th_type").ToString()) Then
                    ddlResultType.Enabled = False
                End If
                ddlResultType_SelectedIndexChanged(Nothing, Nothing)
                ddlResultSubType.SelectedValue = dsProductSet.Tables(0).Rows(0)("th_sub_type").ToString()
                'If Not String.IsNullOrEmpty(dsProductSet.Tables(0).Rows(0)("th_sub_type").ToString()) Then
                '    ddlResultSubType.Enabled = False
                'End If
                ddlResultSubType_SelectedIndexChanged(Nothing, Nothing)
                If ddlResultType.SelectedValue = "TT01" Then
                    txtMaxValue.Text = dsProductSet.Tables(1).Rows(0)("td_max_value").ToString()
                    txtMinValue.Text = dsProductSet.Tables(1).Rows(0)("td_min_value").ToString()
                    txtTypeValue.Text = dsProductSet.Tables(1).Rows(0)("td_value").ToString()

                    'txtMaxValue.Enabled = False
                    'txtMinValue.Enabled = False
                    'txtTypeValue.Enabled = False
                ElseIf ddlResultType.SelectedValue = "TT02" Then
                    gvResultTypeOption.DataSource = dsProductSet.Tables(1)
                    gvResultTypeOption.DataBind()
                End If

                lblReqId.ForeColor = Drawing.Color.Black
                lblReqId.Text = dsProductSet.Tables(0).Rows(0)("th_test_id").ToString()
                'btnSubmit.Style.Add("display", "none")
            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub

    Protected Sub ddlResultType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlResultType.SelectedIndexChanged
        PopulateResultSubType()
        If ddlResultType.SelectedValue = "TT01" Then
            pnlTypeValue.Visible = True
            pnlTypeOption.Visible = False
        ElseIf ddlResultType.SelectedValue = "TT02" Then
            pnlTypeValue.Visible = False
            pnlTypeOption.Visible = True
        Else
            pnlTypeValue.Visible = False
            pnlTypeOption.Visible = False
        End If
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        If Not String.IsNullOrEmpty(txtResultTypeOption.Text.Trim()) Then
            Dim tbl As New DataTable()
            tbl.Columns.Add("td_dtl_id", GetType(Int64))
            tbl.Columns.Add("td_min_value", GetType(Decimal))
            tbl.Columns.Add("td_max_value", GetType(Decimal))
            tbl.Columns.Add("td_uom", GetType(String))
            tbl.Columns.Add("td_option", GetType(String))
            tbl.Columns.Add("td_free_text", GetType(String))

            For Each row As GridViewRow In gvResultTypeOption.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim lblTypeOption As String = DirectCast(row.FindControl("lblTypeOption"), Label).Text
                    Dim hdnTypeOptionId As String = DirectCast(row.FindControl("hdnTypeOptionId"), HiddenField).Value
                    tbl.Rows.Add(Val(hdnTypeOptionId), DBNull.Value, DBNull.Value, DBNull.Value, lblTypeOption, DBNull.Value)
                    If txtResultTypeOption.Text = lblTypeOption Then
                        lblErrorMessage.Text = "Option already exists."
                        Exit Sub
                    End If
                End If
            Next
            tbl.Rows.Add(0, DBNull.Value, DBNull.Value, DBNull.Value, txtResultTypeOption.Text.Trim(), DBNull.Value)
            gvResultTypeOption.DataSource = tbl
            gvResultTypeOption.DataBind()
        Else
            lblErrorMessage.Text = "Please enter Result Type Option."
            ddlResultType.Focus()
            Exit Sub
        End If
    End Sub
    Protected Sub gvResultTypeOption_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvResultTypeOption.RowCommand
        If e.CommandName = "trash" Then
            Dim button As ImageButton = TryCast(e.CommandSource, ImageButton)
            Dim row As GridViewRow = DirectCast(button.NamingContainer, GridViewRow)
            Dim index As Integer = row.RowIndex

            Dim tbl As New DataTable()
            tbl.Columns.Add("td_dtl_id", GetType(Int64))
            tbl.Columns.Add("td_min_value", GetType(Decimal))
            tbl.Columns.Add("td_max_value", GetType(Decimal))
            tbl.Columns.Add("td_uom", GetType(String))
            tbl.Columns.Add("td_option", GetType(String))
            tbl.Columns.Add("td_free_text", GetType(String))

            For Each row1 As GridViewRow In gvResultTypeOption.Rows
                If row1.RowType = DataControlRowType.DataRow Then
                    Dim lblTypeOption As String = DirectCast(row1.FindControl("lblTypeOption"), Label).Text
                    Dim hdnTypeOptionId As String = DirectCast(row1.FindControl("hdnTypeOptionId"), HiddenField).Value
                    tbl.Rows.Add(Val(hdnTypeOptionId), DBNull.Value, DBNull.Value, DBNull.Value, lblTypeOption, DBNull.Value)
                End If
            Next
            tbl.Rows.RemoveAt(index)
            gvResultTypeOption.DataSource = tbl
            gvResultTypeOption.DataBind()
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        CheckLogin()
        lblErrorMessage.Text = ""
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New QualityControlClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False
        Try
            If String.IsNullOrEmpty(txtTestName.Text) Then
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please enter valid test name.');", True)
                lblErrorMessage.Text = "Please enter valid test name."
                txtTestName.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(ddlFrequency.Text) Then
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please enter valid test name.');", True)
                lblErrorMessage.Text = "Please select one frequency."
                ddlFrequency.Focus()
                Exit Sub
            End If
            If String.IsNullOrEmpty(ddlResultType.Text) Then
                'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please enter valid test name.');", True)
                lblErrorMessage.Text = "Please select result type."
                ddlResultType.Focus()
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(ddlResultType.SelectedValue) AndAlso ddlResultType.SelectedValue.Equals("TT01") Then
                If String.IsNullOrEmpty(ddlResultSubType.SelectedValue) Then
                    lblErrorMessage.Text = "Please select sub type."
                    ddlResultSubType.Focus()
                    Exit Sub
                Else
                    If ddlResultSubType.SelectedValue.Equals("6") Then
                        If String.IsNullOrEmpty(txtMinValue.Text.Trim()) Then
                            lblErrorMessage.Text = "Please enter min value."
                            txtMinValue.Focus()
                            Exit Sub
                        End If
                        If String.IsNullOrEmpty(txtMaxValue.Text.Trim()) Then
                            lblErrorMessage.Text = "Please enter max value."
                            txtMaxValue.Focus()
                            Exit Sub
                        End If

                    Else
                        If String.IsNullOrEmpty(txtTypeValue.Text.Trim()) Then
                            lblErrorMessage.Text = "Please enter value."
                            txtTypeValue.Focus()
                            Exit Sub
                        End If
                    End If

                    If String.IsNullOrEmpty(txtUOM.Text.Trim()) Then
                        lblErrorMessage.Text = "Please enter uom."
                        txtUOM.Focus()
                        Exit Sub
                    End If
                End If
            ElseIf Not String.IsNullOrEmpty(ddlResultType.SelectedValue) AndAlso ddlResultType.SelectedValue.Equals("TT02") Then
                If gvResultTypeOption.Rows.Count = 0 Then
                    lblErrorMessage.Text = "Please enter atleast one result type option."
                    txtResultTypeOption.Focus()
                    Exit Sub
                End If
            End If

            Dim tbl As New DataTable()
            tbl.Columns.Add("td_dtl_id", GetType(Int64))
            tbl.Columns.Add("td_min_value", GetType(Decimal))
            tbl.Columns.Add("td_max_value", GetType(Decimal))
            tbl.Columns.Add("td_uom", GetType(String))
            tbl.Columns.Add("td_option", GetType(String))
            tbl.Columns.Add("td_free_text", GetType(String))

            If ddlResultType.SelectedValue = "TT01" Then
                If ddlResultSubType.SelectedValue = "6" Then
                    tbl.Rows.Add(0, Val(txtMinValue.Text), Val(txtMaxValue.Text), txtUOM.Text.Trim(), DBNull.Value, DBNull.Value)
                Else
                    tbl.Rows.Add(0, Val(txtTypeValue.Text), DBNull.Value, txtUOM.Text.Trim(), DBNull.Value, DBNull.Value)
                End If
            ElseIf ddlResultType.SelectedValue = "TT02" Then
                For Each row As GridViewRow In gvResultTypeOption.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim lblTypeOption As String = DirectCast(row.FindControl("lblTypeOption"), Label).Text
                        Dim hdnTypeOptionId As String = DirectCast(row.FindControl("hdnTypeOptionId"), HiddenField).Value
                        tbl.Rows.Add(Val(hdnTypeOptionId), DBNull.Value, DBNull.Value, txtUOM.Text.Trim(), lblTypeOption, DBNull.Value)
                    End If
                Next
            Else
                tbl.Rows.Add(0, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value)
            End If

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            RecordInserted = obj.TestCaseInsertUpdate(Val(lblReqId.Text), txtTestName.Text.Trim(), ddlFrequency.SelectedValue, ddlResultType.SelectedValue, ddlResultSubType.SelectedValue, tbl, userInfo.userIDEntity, sqlConn, sqlTrans)
            If (RecordInserted > 0) Then
                sqlTrans.Commit()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location.href='TestCaseTestMasterList.aspx';", True)
            Else
                sqlTrans.Rollback()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
            End If
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
    End Sub
    Protected Sub gvResultTypeOption_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvResultTypeOption.RowDataBound
        Dim btnEdit As ImageButton = TryCast(e.Row.FindControl("btnEdit"), ImageButton)

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)
            If rowView IsNot Nothing Then
                Dim dtl_id As Integer = Val(rowView("td_dtl_id"))
                If btnEdit IsNot Nothing Then
                    If dtl_id > 0 Then
                        btnEdit.Visible = False
                    Else
                        btnEdit.Visible = True
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub ddlResultSubType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlResultSubType.SelectedIndexChanged
        If ddlResultSubType.SelectedValue = "" Then
            divTypeValue.Visible = False
            divMinValue.Visible = False
            divMaxValue.Visible = False
        ElseIf ddlResultSubType.SelectedValue = "6" Then
            divTypeValue.Visible = False
            divMinValue.Visible = True
            divMaxValue.Visible = True
        Else
            divTypeValue.Visible = True
            divMinValue.Visible = False
            divMaxValue.Visible = False
        End If
    End Sub
End Class
