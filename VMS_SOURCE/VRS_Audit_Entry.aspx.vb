
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports VMS.Web

Partial Class VRS_Audit_Entry
    Inherits System.Web.UI.Page
#Region "Page_Load Event"
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        CheckLogin()
        If Not IsPostBack Then
            PopulateVendor()
            PopulateFinYear()
            btnSubmit.Visible = False
            btnConSub.Visible = False
            'ddlQuarter.Enabled = True
            'ddlVendor.Enabled = True
        End If
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

#Region "Populate Vendor dropdown."

    Private Sub PopulateVendor()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New VRSAuditClass()
        Dim ds As DataSet

        Try

            ds = obj.GetVendorDetails(userInfo.userIDEntity)

            If Not (ds Is Nothing) Then

                If Not (ds.Tables(0).Rows.Count = 0) Then

                    ddlVendor.DataSource = ds
                    ddlVendor.DataTextField = "vendor_name"
                    ddlVendor.DataValueField = "vendor_code"
                    ddlVendor.DataBind()

                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))

                    If ds.Tables(0).Rows.Count = 1 Then
                        ddlVendor.SelectedIndex = 1
                        ddlVendor.Enabled = False
                    End If

                Else
                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate FinYear"
    Private Sub PopulateFinYear()
        CheckLogin()
        Try
            Dim Obj As New vrs_legalscore_class
            Dim ds As New DataSet
            ddlFinYear.Items.Clear()
            ds = Obj.GetFinYear(userInfo.userIDEntity)
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlFinYear.DataSource = ds.Tables(0)
                ddlFinYear.DataTextField = "fin_year_text"
                ddlFinYear.DataValueField = "fin_year"
                ddlFinYear.DataBind()
                ddlFinYear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'If Not (ds.Tables(0).Rows.Count = 1) Then
                '    ddlvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'End If

                If ds.Tables(0).Rows.Count = 1 Then
                    ddlFinYear.SelectedIndex = 1
                    ddlFinYear.Enabled = False
                    PopulateQuarter()
                End If

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub
#End Region

#Region "Populate Quarter dropdown."

    Private Sub PopulateQuarter()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New VRSAuditClass()
        Dim ds As DataSet

        Try

            ds = obj.Get_QuarterList_vr1(userInfo.userIDEntity, ddlFinYear.SelectedValue)

            If Not (ds Is Nothing) Then

                If Not (ds.Tables(0).Rows.Count = 0) Then

                    ddlQuarter.DataSource = ds
                    ddlQuarter.DataTextField = "qm_quarter_short_code"
                    ddlQuarter.DataValueField = "qm_id"
                    ddlQuarter.DataBind()
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                    If (ds.Tables(0).Rows.Count > 0) Then
                        For Each row As DataRow In ds.Tables(0).Rows
                            Dim currentquarter As String = row("qm_current_quarter").ToString()
                            'If currentquarter = "Y" Then
                            '    ddlQuarter.SelectedValue = row("qm_id").ToString()
                            '    ddlQuarter.Enabled = False
                            'End If
                        Next
                    End If
                Else
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("Home.aspx")
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim obj As New VRSAuditClass()
        Dim ds As DataSet
        If String.IsNullOrEmpty(ddlVendor.SelectedValue) Then
            lblErrorMessage.Text = "Please select a vendor"
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Font.Size = 10
        End If

        If String.IsNullOrEmpty(ddlQuarter.SelectedValue) Then
            lblErrorMessage.Text = "Please select a quarter"
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Font.Size = 10
        End If

        If Not String.IsNullOrEmpty(ddlQuarter.SelectedValue) And Not String.IsNullOrEmpty(ddlVendor.SelectedValue) Then
            ds = obj.GetAuditDetails(ddlVendor.SelectedValue, Convert.ToInt64(ddlQuarter.SelectedValue))
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    gvAuditList.DataSource = ds
                    gvAuditList.DataBind()
                    ddlQuarter.Enabled = False
                    ddlVendor.Enabled = False
                    If Convert.ToString(ds.Tables(0).Rows(0)("ah_confirm_status")) = "Y" Then
                        btnSubmit.Visible = False
                        btnConSub.Visible = False
                    Else
                        btnSubmit.Visible = True
                        btnConSub.Visible = True
                    End If


                    'For Each row As DataRow In ds.Tables(0).Rows
                    '    If row("ah_confirm_status").ToString() = "Y" Then
                    '        btnSubmit.Visible = False
                    '        btnConSub.Visible = False
                    '        Exit For
                    '    Else
                    '        btnSubmit.Visible = True
                    '        btnConSub.Visible = True
                    '        Exit For
                    '    End If
                    'Next

                    lblErrorMessage.Text = String.Empty
                Else
                    gvAuditList.DataSource = Nothing
                    gvAuditList.DataBind()
                    lblErrorMessage.Text = String.Empty
                End If
            End If

        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrEmpty(ddlQuarter.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a quarter');", True)
            Return
        End If

        If String.IsNullOrEmpty(ddlVendor.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor');", True)
            Return
        End If

        If gvAuditList.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please search and fill all the fields');", True)
            Return
        End If

        Dim dt As New DataTable()
        Dim count As Integer = 0


        dt.Columns.Add("ParameterId", GetType(String))
        dt.Columns.Add("ParameterType", GetType(String))
        dt.Columns.Add("MaxScore", GetType(String))
        dt.Columns.Add("ObtainedScore", GetType(String))
        dt.Columns.Add("paramremarks", GetType(String))



        For Each row As GridViewRow In gvAuditList.Rows
            Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)

            If txtObtainedScore IsNot Nothing Then
                If String.IsNullOrWhiteSpace(txtObtainedScore.Text) Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please fill all the fields');", True)
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F08080")
                    count += 1
                Else
                    row.BackColor = System.Drawing.Color.White
                End If
                If Not String.IsNullOrWhiteSpace(txtObtainedScore.Text) Then
                    'Dim parameterId As String = CType(row.FindControl("lblPId"), Label).Text
                    Dim parameterId As String = CType(row.FindControl("lblPId"), HiddenField).Value
                    Dim parameterType As String = CType(row.FindControl("lblParameterType"), Label).Text
                    Dim maxScore As String = CType(row.FindControl("lblMaxScore"), Label).Text
                    Dim obtainedScore As Integer = Convert.ToDecimal(txtObtainedScore.Text)
                    Dim totalScore As HiddenField = CType(row.FindControl("hdnMaxScore"), HiddenField)
                    Dim txtRemarks As String = CType(row.FindControl("txtAuditRemarks"), TextBox).Text

                    dt.Rows.Add(parameterId, parameterType, maxScore, obtainedScore, txtRemarks)
                End If
            End If
        Next

        If count > 0 Then
            Return
        Else
            If dt.Rows.Count > 0 Then
                Dim sqlConn As SqlConnection = Nothing
                Dim sqlTrans As SqlTransaction = Nothing
                Dim obj As New VRSAuditClass

                Dim RecordInserted As Integer
                Dim status As String = String.Empty
                Dim flag As Boolean = False
                Try
                    sqlConn = DBFactory.GetHelper.OpenConnection()
                    sqlTrans = sqlConn.BeginTransaction()
                    Dim check As String = "S"
                    RecordInserted = obj.SubmitAuditDetails(Convert.ToInt64(ddlQuarter.SelectedValue), ddlVendor.SelectedValue, userInfo.userIDEntity, check, dt, sqlConn, sqlTrans)
                    If (RecordInserted > 0) Then
                        sqlTrans.Commit()

                        'For Each row As GridViewRow In gvAuditList.Rows
                        '    Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)
                        '    If txtObtainedScore IsNot Nothing Then
                        '        txtObtainedScore.Text = String.Empty
                        '    End If
                        'Next
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submitted successfully.');", True)
                    Else
                        sqlTrans.Rollback()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submission Failed!');", True)
                    End If
                Catch ex As Exception
                    If (sqlTrans IsNot Nothing) Then
                        sqlTrans.Rollback()
                    End If
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
                Finally
                    If (sqlConn IsNot Nothing) Then
                        sqlConn.Close()
                    End If
                    btnSearch_Click(sender, New EventArgs)
                End Try
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
            End If
        End If


    End Sub


    Protected Sub btnConSubmit_Click(sender As Object, e As EventArgs) Handles btnConSub.Click

        If String.IsNullOrEmpty(ddlQuarter.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a quarter');", True)
            Return
        End If

        If String.IsNullOrEmpty(ddlVendor.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor');", True)
            Return
        End If

        If gvAuditList.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please search and fill all the fields');", True)
            Return
        End If

        Dim dt As New DataTable()
        Dim count As Integer = 0


        dt.Columns.Add("ParameterId", GetType(String))
        dt.Columns.Add("ParameterType", GetType(String))
        dt.Columns.Add("MaxScore", GetType(String))
        dt.Columns.Add("ObtainedScore", GetType(String))
        dt.Columns.Add("paramremarks", GetType(String))


        For Each row As GridViewRow In gvAuditList.Rows
            Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)

            If txtObtainedScore IsNot Nothing Then
                If String.IsNullOrWhiteSpace(txtObtainedScore.Text) Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please fill all the fields');", True)
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F08080")
                    count += 1
                Else
                    row.BackColor = System.Drawing.Color.White
                End If
                If Not String.IsNullOrWhiteSpace(txtObtainedScore.Text) Then
                    'Dim parameterId As String = CType(row.FindControl("lblPId"), Label).Text
                    Dim parameterId As String = CType(row.FindControl("lblPId"), HiddenField).Value
                    Dim parameterType As String = CType(row.FindControl("lblParameterType"), Label).Text
                    Dim maxScore As String = CType(row.FindControl("lblMaxScore"), Label).Text
                    Dim obtainedScore As Integer = Convert.ToDecimal(txtObtainedScore.Text)
                    Dim totalScore As HiddenField = CType(row.FindControl("hdnMaxScore"), HiddenField)
                    Dim txtRemarks As String = CType(row.FindControl("txtAuditRemarks"), TextBox).Text

                    dt.Rows.Add(parameterId, parameterType, maxScore, obtainedScore, txtRemarks)
                End If
            End If
        Next

        If count > 0 Then
            Return
        Else
            If dt.Rows.Count > 0 Then
                Dim sqlConn As SqlConnection = Nothing
                Dim sqlTrans As SqlTransaction = Nothing
                Dim obj As New VRSAuditClass

                Dim RecordInserted As Integer
                Dim status As String = String.Empty
                Dim flag As Boolean = False
                Try
                    sqlConn = DBFactory.GetHelper.OpenConnection()
                    sqlTrans = sqlConn.BeginTransaction()
                    Dim check As String = "C"
                    RecordInserted = obj.SubmitAuditDetails(Convert.ToInt64(ddlQuarter.SelectedValue), ddlVendor.SelectedValue, userInfo.userIDEntity, check, dt, sqlConn, sqlTrans)
                    If (RecordInserted > 0) Then
                        sqlTrans.Commit()

                        'For Each row As GridViewRow In gvAuditList.Rows
                        '    Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)
                        '    If txtObtainedScore IsNot Nothing Then
                        '        txtObtainedScore.Text = String.Empty
                        '    End If
                        'Next
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submitted successfully.');", True)
                        btnSubmit.Visible = False
                        btnConSub.Visible = False
                    Else
                        sqlTrans.Rollback()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submission Failed!');", True)
                    End If
                Catch ex As Exception
                    If (sqlTrans IsNot Nothing) Then
                        sqlTrans.Rollback()
                    End If
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
                Finally
                    If (sqlConn IsNot Nothing) Then
                        sqlConn.Close()
                    End If
                    btnSearch_Click(sender, New EventArgs)
                End Try
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
            End If
        End If
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        'Response.Redirect(Request.Url.ToString())
        Dim path = "~/VRS_Audit_Entry.aspx"
        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub

    Protected Sub ddlFinYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(ddlFinYear.SelectedValue) Then
            ddlQuarter.Items.Clear()
        Else
            PopulateQuarter()
        End If
    End Sub

End Class
