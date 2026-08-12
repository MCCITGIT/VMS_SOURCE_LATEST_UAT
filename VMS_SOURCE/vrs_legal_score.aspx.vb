Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.IO
Imports AjaxControlToolkit
Imports Microsoft.Win32
Imports System.Security.Permissions
Imports NPOI.SS.Formula.Functions
Partial Class vrs_legal_score
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Global Variable"
    Dim HeaderId As Int32
    Dim DtldId As Int32
#End Region
#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        ''  AddAttributes()
        If Not IsPostBack Then
            div2.Visible = True
            PopulateVendor()
            Populate_Quarter()
            ' btnSubmit.Visible = False
            btnConSub.Visible = False
            If userInfo.userGroupCodeEntity.Equals("UNIT") Then
                BindGrid()
            End If

            'PopulateLegal_Statutory_Status()

        End If

    End Sub
#End Region
#Region "Fin Year"
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
                    Populate_Quarter()
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
#Region "AddAttributes"
    Private Sub AddAttributes()
        btnSubmit.Attributes.Add("onclick", "return ValidateSubmit_vr1();")
        btnsearch.Attributes.Add("onclick", "return ValidateSearch();")
    End Sub
#End Region
#Region "Populate Dropdown"
    Private Sub PopulateVendor()
        CheckLogin()
        Try
            'Dim obj As New vrs_legalscore_class
            Dim Obj As New QualityControlClass
            Dim ds As New DataSet
            ddlvendor.Items.Clear()
            ' ds = obj.GetVendor_DataList()
            ds = Obj.GetVendor(userInfo.userIDEntity)
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlvendor.DataSource = ds.Tables(0)
                ddlvendor.DataTextField = "vendor_name"
                ddlvendor.DataValueField = "vendor_code"
                ddlvendor.DataBind()
                ddlvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'If Not (ds.Tables(0).Rows.Count = 1) Then
                '    ddlvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'End If

                If ds.Tables(0).Rows.Count = 1 Then
                    ddlvendor.SelectedIndex = 1
                    ddlvendor.Enabled = False
                End If

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#Region "Populate Quarter"
    Private Sub Populate_Quarter()
        Try
            Dim obj As New vrs_legalscore_class
            Dim ds As New DataSet
            ddlquartor.Items.Clear()
            ds = obj.Get_QuarterList_vr1(userInfo.userIDEntity, ddlFinYear.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlquartor.DataSource = ds.Tables(0)
                ddlquartor.DataTextField = "qm_quarter_short_code"
                ddlquartor.DataValueField = "qm_id"
                ddlquartor.DataBind()
                If (ds.Tables(0).Rows.Count > 0) Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        Dim currentquarter As String = row("qm_current_quarter").ToString()
                        'If currentquarter = "Y" Then
                        '    ddlquartor.SelectedValue = row("qm_id").ToString()
                        '    ddlquartor.Enabled = False
                        'End If
                    Next
                End If
                If Not (ds.Tables(0).Rows.Count = 1) Then
                    ddlquartor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub

    Protected Sub ddlFinYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(ddlFinYear.SelectedValue) Then
            ddlquartor.Items.Clear()
        Else
            Populate_Quarter()
        End If
    End Sub
#End Region

#End Region
#Region "Date Format"
    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime

        If (stringdate.Equals(String.Empty)) Then
            Return SqlDateTime.MinValue
        End If

        If Not (stringdate = String.Empty) Then
            If stringdate.Contains("/") Then
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
            ElseIf stringdate.Contains("-") Then
                Dim ddate As String() = stringdate.Split("-")
                Dim arrlist As New ArrayList
                Dim index As Integer = 0

                While index <= ddate.Length - 1
                    arrlist.Add(ddate(index))
                    System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
                End While
                Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(2))
                Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
                Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(0))

                Dim dt As DateTime = New DateTime(yyyy, mm, dd)
                dt = FormatDateTime(dt, DateFormat.LongDate)
                Return dt
            End If
        End If
    End Function


#End Region
#Region "Populate Grid"
    Private Sub BindGrid()
        Try
            Dim obj As New vrs_legalscore_class
            Dim ds As New DataSet
            ds = obj.GetLegalScoreDetails(ddlvendor.SelectedValue, ddlquartor.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then

                gvLegalScoreList.DataSource = ds.Tables(0)
                gvLegalScoreList.DataBind()

                If userInfo.userGroupCodeEntity = "HO" OrElse userInfo.userGroupCodeEntity = "HO-ADMIN" OrElse userInfo.userGroupCodeEntity = "HO-MARKETING" Then
                    gvLegalScoreList.Columns(3).Visible = True
                Else
                    gvLegalScoreList.Columns(3).Visible = False
                End If
            Else
                gvLegalScoreList.DataSource = Nothing
                gvLegalScoreList.DataBind()
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click

        If String.IsNullOrEmpty(ddlquartor.Text) Then
            lblError.Text = "Please Select Quartor."
            ddlquartor.Focus()
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlvendor.Text) Then
            lblError.Text = "Please Select Vendor."
            ddlvendor.Focus()
            Exit Sub
        End If

        BindGrid()
        ddlquartor.Enabled = False
        ddlvendor.Enabled = False
        btnsearch.Enabled = False

    End Sub
    Protected Sub lnkDownload_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim fileName As String = e.CommandArgument.ToString()
        If Not String.IsNullOrEmpty(fileName) Then
            DownloadDocument(fileName)
        End If
    End Sub
    Private Sub DownloadDocument(ByVal fileName As String)
        Try
            Dim genReportPath As String = ConfigurationManager.AppSettings("UPLOAD_DOCS_FOLDER_ABS_PATH") & "\"
            Dim fullPath As String = Path.Combine(genReportPath, fileName)
            Dim filebytes As Byte() = File.ReadAllBytes(fullPath)
            If File.Exists(fullPath) Then
                Response.Clear()
                Response.ContentType = GetMIMEType(fullPath)
                Response.AppendHeader("Content-Disposition", "attachment; filename=""" & Path.GetFileName(fileName) & """")
                Response.TransmitFile(fullPath)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.BinaryWrite(filebytes)
                Response.Flush()
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            Else
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetMIMEType(ByVal filePath As String) As String
        Select Case Path.GetExtension(filePath).ToLower()
            Case ".pdf"
                Return "application/pdf"
            Case ".txt"
                Return "text/plain"
            Case ".jpg", ".jpeg"
                Return "image/jpeg"
            Case ".png"
                Return "image/png"
            Case Else
                Return "application/octet-stream"
        End Select
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx", True)
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim path = "~/vrs_legal_score.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Check if Quarter is selected
        If String.IsNullOrEmpty(ddlquartor.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a quarter');", True)
            Return
        End If

        ' Check if Vendor is selected
        If String.IsNullOrEmpty(ddlvendor.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor');", True)
            Return
        End If

        ' Check if GridView has rows
        If gvLegalScoreList.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please fill all the fields');", True)
            Return
        End If

        Dim dt As New DataTable()
        Dim count As Integer = 0

        ' Add columns to the DataTable
        dt.Columns.Add("ParameterId", GetType(String))
        dt.Columns.Add("Obligation", GetType(String))
        dt.Columns.Add("Availability", GetType(String))
        dt.Columns.Add("Max Score", GetType(String))
        dt.Columns.Add("Obtained Score", GetType(String))
        dt.Columns.Add("File Path", GetType(String))
        dt.Columns.Add("Valid From", GetType(String))
        dt.Columns.Add("Valid Till", GetType(String))
        dt.Columns.Add("Valid Issue", GetType(String))

        ' Loop through each row in GridView
        For Each row As GridViewRow In gvLegalScoreList.Rows
            Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)
            Dim txtIssueAuthority As TextBox = CType(row.FindControl("txtIssueAuthority"), TextBox)
            Dim txtValidDate As TextBox = CType(row.FindControl("txtValidDate"), TextBox)
            Dim txtValidFromDate As TextBox = CType(row.FindControl("txtValidFromDate"), TextBox)
            Dim FileUpload1 As FileUpload = CType(row.FindControl("FileUpload1"), FileUpload)
            Dim lblFileName As Label = CType(row.FindControl("lblFileName"), Label)
            Dim hdnFilePath As HiddenField = CType(row.FindControl("hdnFilePath"), HiddenField)

            ' Validate input fields and file upload
            'If txtObtainedScore IsNot Nothing Then
            '    If String.IsNullOrWhiteSpace(txtObtainedScore.Text) Or
            '   String.IsNullOrWhiteSpace(txtIssueAuthority.Text) Or
            '   String.IsNullOrWhiteSpace(txtValidDate.Text) Or
            '   String.IsNullOrWhiteSpace(txtValidFromDate.Text) Then
            '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please fill all the fields');", True)
            '        row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F08080")
            '        count += 1
            '    Else
            row.BackColor = System.Drawing.Color.White
                    ' If file is uploaded, store the file name in the label
                    If FileUpload1.HasFile Then
                        lblFileName.Text = FileUpload1.PostedFile.FileName
                        lblFileName.Visible = True
                    End If
            '    End If
            'End If
        Next

        If count > 0 Then
            Return
        Else
            ' If validation passed, collect the data
            For Each row As GridViewRow In gvLegalScoreList.Rows
                Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)
                Dim txtIssueAuthority As TextBox = CType(row.FindControl("txtIssueAuthority"), TextBox)
                Dim txtValidDate As TextBox = CType(row.FindControl("txtValidDate"), TextBox)
                Dim txtValidFromDate As TextBox = CType(row.FindControl("txtValidFromDate"), TextBox)
                Dim FileUpload1 As FileUpload = CType(row.FindControl("FileUpload1"), FileUpload)
                Dim lblFileName As Label = CType(row.FindControl("lblFileName"), Label)

                ' If Not String.IsNullOrWhiteSpace(txtObtainedScore.Text) AndAlso
                'Not String.IsNullOrWhiteSpace(txtIssueAuthority.Text) AndAlso
                'Not String.IsNullOrWhiteSpace(txtValidDate.Text) AndAlso
                'Not String.IsNullOrWhiteSpace(txtValidFromDate.Text) Then

                ' Collect data from each row
                Dim parameterId As String = CType(row.FindControl("hdnParameterCode"), HiddenField).Value
                Dim parametershortname As String = CType(row.FindControl("hdnparamshortname"), HiddenField).Value
                Dim obligation As String = CType(row.FindControl("lblObligation"), Label).Text
                    Dim availability As String = CType(row.FindControl("lblAvailability"), Label).Text
                    Dim targetScore As Integer = Convert.ToDecimal(CType(row.FindControl("lblTargetScore"), Label).Text)
                    Dim obtainedScore As Integer = Convert.ToDecimal(txtObtainedScore.Text)
                    Dim validFromDate As String = txtValidFromDate.Text
                    Dim validDate As String = txtValidDate.Text
                    Dim issueAuthority As String = txtIssueAuthority.Text


                    ' Define the folder and file paths for saving the file
                    Dim DOC_ABS_PATH As String = ConfigurationManager.AppSettings("UPLOAD_DOCS_FOLDER_ABS_PATH")
                    Dim FolderPath As String = "Legal_Score/"
                    Dim FolderPath2 As String = "Legal_Score/" & DateTime.UtcNow.ToString("yyyyMMdd") & "_" & Guid.NewGuid().ToString()
                    Dim fullFolderPath As String = Path.Combine(DOC_ABS_PATH, FolderPath)

                    ' Create folder if it doesn't exist
                    If Not Directory.Exists(fullFolderPath) Then
                        Directory.CreateDirectory(fullFolderPath)
                    End If

                    Dim fileLocation As String = ""
                    ' If file is uploaded, save it
                    If FileUpload1.HasFile Then
                    Dim FileName As String = "Legal_Score" & DateTime.UtcNow.ToString("yyyyMMddss") & "_" & parametershortname & Path.GetExtension(FileUpload1.PostedFile.FileName)
                    fileLocation = Path.Combine(FolderPath, FileName)
                        Dim fullFilePath As String = Path.Combine(fullFolderPath, FileName)
                        FileUpload1.SaveAs(fullFilePath)
                    ElseIf Not String.IsNullOrEmpty(lblFileName.Text) Then
                    Dim FileName As String = "Legal_Score" & DateTime.UtcNow.ToString("yyyyMMddss") & "_" & parametershortname & Path.GetExtension(lblFileName.Text)
                    fileLocation = Path.Combine(FolderPath, FileName)
                        Dim fullFilePath As String = Path.Combine(fullFolderPath, FileName)
                    FileUpload1.SaveAs(fullFilePath)
                End If

                    ' Add values to DataTable
                    dt.Rows.Add(parameterId, obligation, availability, targetScore, obtainedScore, fileLocation, validFromDate, validDate, issueAuthority)
                '  End If
            Next

            ' If there are rows in the DataTable, proceed to save in the database
            If dt.Rows.Count > 0 Then
                Dim sqlConn As SqlConnection = Nothing
                Dim sqlTrans As SqlTransaction = Nothing
                Dim obj As New vrs_legalscore_class

                Dim RecordInserted As Integer
                Dim status As String = String.Empty
                Dim flag As Boolean = False
                Try
                    sqlConn = DBFactory.GetHelper.OpenConnection()
                    sqlTrans = sqlConn.BeginTransaction()
                    Dim check As String = "S"
                    RecordInserted = obj.SubmitAuditDetails(Convert.ToInt64(ddlquartor.SelectedValue), ddlvendor.SelectedValue, userInfo.userIDEntity, check, dt, sqlConn, sqlTrans)
                    If (RecordInserted > 0) Then
                        'sqlTrans.Rollback()
                        sqlTrans.Commit()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submitted successfully.');", True)
                    Else
                        sqlTrans.Rollback()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submission Failed!');", True)
                    End If
                Catch ex As Exception
                    If (sqlTrans IsNot Nothing) Then
                        sqlTrans.Rollback()
                    End If
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submission Failed!');", True)

                Finally
                    If (sqlConn IsNot Nothing) Then
                        sqlConn.Close()
                    End If
                    BindGrid()
                End Try
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
            End If
        End If
    End Sub



    Protected Sub btnConSubmit_Click(sender As Object, e As EventArgs) Handles btnConSub.Click
        ' Check if Quarter is selected
        If String.IsNullOrEmpty(ddlquartor.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a quarter');", True)
            Return
        End If

        ' Check if Vendor is selected
        If String.IsNullOrEmpty(ddlvendor.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor');", True)
            Return
        End If

        ' Check if GridView has rows
        If gvLegalScoreList.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please fill all the fields');", True)
            Return
        End If

        Dim dt As New DataTable()
        Dim count As Integer = 0

        ' Add columns to the DataTable
        dt.Columns.Add("ParameterId", GetType(String))
        dt.Columns.Add("Obligation", GetType(String))
        dt.Columns.Add("Availability", GetType(String))
        dt.Columns.Add("Max Score", GetType(String))
        dt.Columns.Add("Obtained Score", GetType(String))
        dt.Columns.Add("File Path", GetType(String))
        dt.Columns.Add("Valid From", GetType(String))
        dt.Columns.Add("Valid Till", GetType(String))
        dt.Columns.Add("Valid Issue", GetType(String))


        If count > 0 Then
            Return
        Else

            For Each row As GridViewRow In gvLegalScoreList.Rows
                Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)
                Dim txtIssueAuthority As TextBox = CType(row.FindControl("txtIssueAuthority"), TextBox)
                Dim txtValidDate As TextBox = CType(row.FindControl("txtValidDate"), TextBox)
                Dim txtValidFromDate As TextBox = CType(row.FindControl("txtValidFromDate"), TextBox)
                Dim FileUpload1 As FileUpload = CType(row.FindControl("FileUpload1"), FileUpload)
                Dim lblFileName As Label = CType(row.FindControl("lblFileName"), Label)



                ' Collect data from each row
                Dim parameterId As String = CType(row.FindControl("hdnParameterCode"), HiddenField).Value
                    Dim obligation As String = CType(row.FindControl("lblObligation"), Label).Text
                    Dim availability As String = CType(row.FindControl("lblAvailability"), Label).Text
                    Dim targetScore As Integer = Convert.ToDecimal(CType(row.FindControl("lblTargetScore"), Label).Text)
                    Dim obtainedScore As Integer = Convert.ToDecimal(txtObtainedScore.Text)
                    Dim validFromDate As String = txtValidFromDate.Text
                    Dim validDate As String = txtValidDate.Text
                    Dim issueAuthority As String = txtIssueAuthority.Text

                    Dim fileLocation As String = CType(row.FindControl("hdnFilePath"), HiddenField).Value
                    ' Add values to DataTable
                    dt.Rows.Add(parameterId, obligation, availability, targetScore, obtainedScore, fileLocation, validFromDate, validDate, issueAuthority)

            Next

            ' If there are rows in the DataTable, proceed to save in the database
            If dt.Rows.Count > 0 Then
                Dim sqlConn As SqlConnection = Nothing
                Dim sqlTrans As SqlTransaction = Nothing
                Dim obj As New vrs_legalscore_class

                Dim RecordInserted As Integer
                Dim status As String = String.Empty
                Dim flag As Boolean = False
                Try
                    sqlConn = DBFactory.GetHelper.OpenConnection()
                    sqlTrans = sqlConn.BeginTransaction()
                    Dim check As String = "C"
                    RecordInserted = obj.SubmitAuditDetails(Convert.ToInt64(ddlquartor.SelectedValue), ddlvendor.SelectedValue, userInfo.userIDEntity, check, dt, sqlConn, sqlTrans)
                    If (RecordInserted > 0) Then
                        sqlTrans.Commit()
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
                    BindGrid()
                End Try
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
            End If
        End If
    End Sub

    Protected Sub gvLegalScoreList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim txtObtainedScore As TextBox = CType(e.Row.FindControl("txtObtainedScore"), TextBox)
            Dim txtValidDate As TextBox = CType(e.Row.FindControl("txtValidDate"), TextBox)
            Dim txtValidFromDate As TextBox = CType(e.Row.FindControl("txtValidFromDate"), TextBox)
            Dim txtIssueAuthority As TextBox = CType(e.Row.FindControl("txtIssueAuthority"), TextBox)
            Dim FileUpload1 As FileUpload = CType(e.Row.FindControl("FileUpload1"), FileUpload)
            Dim hdnenableYN As HiddenField = CType(e.Row.FindControl("hdnenableYN"), HiddenField)
            Dim hdnsubmitbutton As HiddenField = CType(e.Row.FindControl("hdnsubmitbutton"), HiddenField)
            Dim hdnconfirmbutton As HiddenField = CType(e.Row.FindControl("hdnconfirmbutton"), HiddenField)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)

            If Not hdnenableYN.Value = "Y" Then
                txtObtainedScore.Enabled = False
                txtValidFromDate.Enabled = False
                txtValidDate.Enabled = False
                txtIssueAuthority.Enabled = False
                FileUpload1.Visible = False
            End If
            If String.IsNullOrEmpty(lblStatus.Text) Then
                gvLegalScoreList.Columns(10).Visible = False
            End If
            If hdnsubmitbutton.Value = "Y" Then
                btnSubmit.Visible = True
            Else
                btnSubmit.Visible = False
            End If
            If hdnconfirmbutton.Value = "Y" Then
                btnConSub.Visible = True
            Else
                btnConSub.Visible = False
            End If

        End If
    End Sub
End Class
