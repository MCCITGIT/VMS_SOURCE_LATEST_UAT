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
Imports NPOI.XSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.HSSF.UserModel
Imports Ionic.Zip

Partial Class vrs_legal_score_appr_rej
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Global Variable"
    Dim HeaderId As Int32
    Dim DtldId As Int32
#End Region
#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        AddAttributes()
        If Not IsPostBack Then
            PopulateApproveStatus()
            PopulateVendor()
            PopulateFinYear()
            'PopulateLegal_Statutory_Status()
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
#Region "AddAttributes"
    Private Sub AddAttributes()
        btnsearch.Attributes.Add("onclick", "return ValidateSearch();")
    End Sub
#End Region
#Region "Populate Dropdown"

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
    Private Sub PopulateVendor()
        CheckLogin()
        Try

            Dim Obj As New vrs_legalscore_class
            Dim ds As New DataSet
            ddlvendor.Items.Clear()

            ds = Obj.GetVendorListForApproval(ddlStatus.SelectedValue, ddlquartor.SelectedValue)
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlvendor.DataSource = ds.Tables(0)
                ddlvendor.DataTextField = "vendor_name"
                ddlvendor.DataValueField = "vendor_code"
                ddlvendor.DataBind()
                ddlvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                If ds.Tables(0).Rows.Count = 1 Then
                    ddlvendor.SelectedIndex = 1

                End If

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub

    Private Sub PopulateApproveStatus()
        CheckLogin()
        Try

            Dim Obj As New LovDetails
            Dim ds As New DataSet
            ddlStatus.Items.Clear()
            ' ds = obj.GetVendor_DataList()
            ds = Obj.GetLovDetailsList("Berger", "APPROVAL_STATUS_LEGAL")
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlStatus.DataSource = ds.Tables(0)
                ddlStatus.DataTextField = "lov_value"
                ddlStatus.DataValueField = "lov_code"
                ddlStatus.DataBind()
                ddlStatus.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                If ds.Tables(0).Rows.Count > 0 Then
                    'ddlStatus.SelectedIndex = 1

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
            Dim pendingcount As Int32
            ds = obj.GetLegalScoreApprRejDetails(ddlvendor.SelectedValue, ddlquartor.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                For Each row As DataRow In ds.Tables(0).Rows
                    If row.IsNull("obt_score") OrElse row("obt_score").ToString().Trim() = "" Then
                        row("obt_score") = 0
                    End If
                    If row.IsNull("status").ToString() = "P" Then
                        pendingcount = 1
                    End If
                Next

                Dim dv As New DataView(ds.Tables(0))
                dv.RowFilter = "status = 'P'"
                If (dv.Count > 0) Then
                    btnApprove.Visible = True
                    btnReject.Visible = True
                Else
                    btnApprove.Visible = False
                    btnReject.Visible = False
                End If


                gvLegalScoreList.DataSource = ds.Tables(0)
                    gvLegalScoreList.DataBind()

                    If userInfo.userGroupCodeEntity = "HO" OrElse
               userInfo.userGroupCodeEntity = "HO-ADMIN" OrElse
               userInfo.userGroupCodeEntity = "HO-MARKETING" Then

                        gvLegalScoreList.Columns(3).Visible = True
                    Else
                        gvLegalScoreList.Columns(3).Visible = False
                    End If

                    'Dim confirmStatus As String = ds.Tables(1).Rows(0)("vlsh_confirm_status").ToString()

                    'If confirmStatus = "Y" Then
                    '    For Each row As GridViewRow In gvLegalScoreList.Rows 
                    '        If row.RowType = DataControlRowType.DataRow Then
                    '            Dim txtObtainedScore As TextBox = CType(row.FindControl("txtObtainedScore"), TextBox)
                    '            Dim txtValidDate As TextBox = CType(row.FindControl("txtValidDate"), TextBox)
                    '            Dim txtValidFromDate As TextBox = CType(row.FindControl("txtValidFromDate"), TextBox)
                    '            Dim txtIssueAuthority As TextBox = CType(row.FindControl("txtIssueAuthority"), TextBox)
                    '            Dim FileUpload1 As FileUpload = CType(row.FindControl("FileUpload1"), FileUpload)
                    '            txtObtainedScore.Enabled = False
                    '            txtValidFromDate.Enabled = False
                    '            txtValidDate.Enabled = False
                    '            txtIssueAuthority.Enabled = False
                    '            FileUpload1.Visible = False
                    '        End If
                    '    Next
                    'End If
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

        div2.Visible = True

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
        Dim path = "~/vrs_legal_score_appr_reject.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub

    'Public Sub btnApprove_Click(sender As Object, e As EventArgs)
    '    Dim RecordInserted As Integer
    '    Dim obj As New vrs_legalscore_class
    '    Dim sqlConn As SqlConnection = Nothing
    '    Dim sqlTrans As SqlTransaction = Nothing
    '    sqlConn = DBFactory.GetHelper.OpenConnection()
    '    sqlTrans = sqlConn.BeginTransaction()
    '    Dim btn As LinkButton = CType(sender, LinkButton)
    '    Dim gvRow As GridViewRow = CType(btn.NamingContainer, GridViewRow)

    '    Dim lblScore As TextBox = CType(gvRow.FindControl("txtTargetScore"), TextBox)
    '    Dim targetScore As Integer = Convert.ToInt32(lblScore.Text)

    '    Dim ddlAvailability As DropDownList = CType(gvRow.FindControl("ddlAvailability"), DropDownList)
    '    Dim availability As String = ddlAvailability.SelectedValue

    '    Dim parameterCode As String = btn.CommandArgument.ToString()
    '    RecordInserted = obj.UpdateLegalScoreStatus(ddlquartor.SelectedValue, ddlvendor.SelectedValue, Convert.ToInt64(parameterCode), availability, Convert.ToInt64(targetScore), "Y", userInfo.userIDEntity, "", sqlConn, sqlTrans)
    '    If (RecordInserted > 0) Then
    '        sqlTrans.Commit()
    '        BindGrid()
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approved successfully.');", True)

    '    Else
    '        sqlTrans.Rollback()
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approval Failed!');", True)
    '    End If
    'End Sub

    'Private Sub RejectParameter(parameterCode As String, remarks As String, tgtScr As Int64, ByVal avl As String)
    '    Dim RecordInserted As Integer
    '    Dim obj As New vrs_legalscore_class
    '    Dim sqlConn As SqlConnection = Nothing
    '    Dim sqlTrans As SqlTransaction = Nothing
    '    sqlConn = DBFactory.GetHelper.OpenConnection()
    '    sqlTrans = sqlConn.BeginTransaction()

    '    RecordInserted = obj.UpdateLegalScoreStatus(ddlquartor.SelectedValue, ddlvendor.SelectedValue, Convert.ToInt64(parameterCode), avl, tgtScr, "N", userInfo.userIDEntity, remarks, sqlConn, sqlTrans)
    '    If (RecordInserted > 0) Then
    '        sqlTrans.Commit()
    '        txtRejectRemarks.Text = ""
    '        BindGrid()
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record rejected successfully.');", True)

    '    Else
    '        sqlTrans.Rollback()
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record rejection Failed!');", True)
    '    End If
    'End Sub


    Protected Sub gvLegalScoreList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            btnDownloadDoc.Visible = False
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim status As String = drv("status").ToString()
            'Dim filecount As Int64 = 0
            Dim pnlRemarks As Panel = CType(e.Row.FindControl("pnlRemarks"), Panel)
            Dim lblRemarks As Label = CType(e.Row.FindControl("lblRemarks"), Label)
            Dim ddlAvailability As DropDownList = CType(e.Row.FindControl("ddlAvailability"), DropDownList)
            Dim hdnAvailability As HiddenField = CType(e.Row.FindControl("hdnAvailability"), HiddenField)
            Dim txtTargetScore As TextBox = CType(e.Row.FindControl("txtTargetScore"), TextBox)
            Dim hdnFilePath As HiddenField = CType(e.Row.FindControl("hdnFilePath"), HiddenField)

            Dim lblstatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)

            If (Convert.ToString(hdnFilePath.Value) <> "") Then
                hdnFilecount.Value += 1
            End If
            If (hdnFilecount.Value > 0) Then
                btnDownloadDoc.Visible = True
            End If
            If (lblstatus.Text = "Approved" Or lblstatus.Text = "Rejected") Then
                chkSelect.Enabled = False

                'Else
                '    btnApprove.Visible = True
                '    btnReject.Visible = True
            End If


            'Dim parameterCode As String = drv("parameter_code").ToString()
            'Dim rejectTgtScr As String = txtTargetScore.Text
            'Dim avl As String = hdnAvailability.Value

            'If btnReject IsNot Nothing AndAlso ddlAvailability IsNot Nothing AndAlso txtTargetScore IsNot Nothing Then
            '    Dim paramCodeEscaped As String = parameterCode.Replace("'", "\'")
            '    Dim targetScoreClientID As String = txtTargetScore.ClientID
            '    Dim availabilityClientID As String = ddlAvailability.ClientID

            '    Dim jsTargetScore As String = "document.getElementById('" & targetScoreClientID & "').value"
            '    Dim jsAvailability As String = "document.getElementById('" & availabilityClientID & "').value"

            '    btnReject.OnClientClick = "return showRemarksPopup('" & paramCodeEscaped & "', " & jsTargetScore & ", " & jsAvailability & ");"
            'End If



            Dim ds As DataSet
            Dim obj As New vrs_legalscore_class
            ds = obj.GetLegalScoreApprRejDetails(ddlvendor.SelectedValue, ddlquartor.SelectedValue)
            If ddlAvailability IsNot Nothing AndAlso hdnAvailability IsNot Nothing Then
                ddlAvailability.DataSource = ds.Tables(1)
                ddlAvailability.DataTextField = "avaliability_desc"
                ddlAvailability.DataValueField = "avaliability_code"
                ddlAvailability.DataBind()

                If ddlAvailability.Items.FindByValue(hdnAvailability.Value) IsNot Nothing Then
                    ddlAvailability.SelectedValue = hdnAvailability.Value
                End If
            End If
        End If

    End Sub



    Protected Sub ddlAvailability_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddl As DropDownList = CType(sender, DropDownList)
            Dim row As GridViewRow = CType(ddl.NamingContainer, GridViewRow)

            Dim hdnObligation As HiddenField = CType(row.FindControl("hdnObligation"), HiddenField)
            Dim hdnAvailability As HiddenField = CType(row.FindControl("hdnAvailability"), HiddenField)
            Dim hdnRejectTargetScore As HiddenField = CType(row.FindControl("hdnRejectTargetScore"), HiddenField)
            Dim obligation As String = hdnObligation.Value
            Dim availability As String = ddl.SelectedValue
            Dim obj As New vrs_legalscore_class
            Dim ds As New DataSet
            ds = obj.GetTargetScore(obligation, availability)
            Dim score As Integer = ds.Tables(0).Rows(0)("vlsm_score")

            Dim txtTargetScore As TextBox = CType(row.FindControl("txtTargetScore"), TextBox)
            If txtTargetScore IsNot Nothing Then
                txtTargetScore.Text = score.ToString()
            End If

            Dim hdnTargetScore As HiddenField = CType(row.FindControl("hdnTargetScore"), HiddenField)
            If hdnTargetScore IsNot Nothing Then
                hdnTargetScore.Value = score.ToString()
            End If
            hdnAvailability.Value = availability
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub


    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateVendor()
    End Sub
    Protected Sub ddlquartor_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateVendor()
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Dim dsLegalScore As DataSet
        Dim obj As New vrs_legalscore_class
        dsLegalScore = obj.GetLegal_score_DtlsReport(Val(ddlquartor.SelectedValue), ddlvendor.SelectedValue)
        If (Not (dsLegalScore Is Nothing) AndAlso dsLegalScore.Tables.Count > 0) Then
            ExportToExcelSheet(dsLegalScore)
        End If
    End Sub

    Private Sub ExportToExcelSheet(ByVal dset As DataSet)
        Try
            'Opening the Excel template...
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\Legal_Score_details_Report.xlsx", FileMode.Open, FileAccess.Read)

            'Getting the complete workbook...
            Dim templateWorkbook As XSSFWorkbook = New XSSFWorkbook(fs)

            Dim font3 As IFont = templateWorkbook.CreateFont()
            font3.Color = HSSFColor.Black.Index
            font3.FontName = "Calibri"
            font3.FontHeightInPoints = 10

            Dim headerStyle As ICellStyle = templateWorkbook.CreateCellStyle()
            headerStyle.SetFont(font3)
            headerStyle.FillPattern = FillPattern.SolidForeground
            headerStyle.FillForegroundColor = IndexedColors.White.Index ' or any other color
            headerStyle.Alignment = HorizontalAlignment.Center
            headerStyle.VerticalAlignment = VerticalAlignment.Center



            Dim styleCenter As ICellStyle = templateWorkbook.CreateCellStyle()
            styleCenter.VerticalAlignment = VerticalAlignment.Center
            styleCenter.Alignment = HorizontalAlignment.Center
            styleCenter.SetFont(font3)
            styleCenter.BorderRight = BorderStyle.Thin
            styleCenter.BorderBottom = BorderStyle.Thin
            styleCenter.BorderTop = BorderStyle.Thin
            styleCenter.BorderLeft = BorderStyle.Thin


            Dim styleLeft As ICellStyle = templateWorkbook.CreateCellStyle()
            styleLeft.VerticalAlignment = VerticalAlignment.Center
            styleLeft.Alignment = HorizontalAlignment.Left
            styleLeft.SetFont(font3)
            styleLeft.BorderRight = BorderStyle.Thin
            styleLeft.BorderBottom = BorderStyle.Thin
            styleLeft.BorderTop = BorderStyle.Thin
            styleLeft.BorderLeft = BorderStyle.Thin

            Dim styleRight As ICellStyle = templateWorkbook.CreateCellStyle()
            styleRight.VerticalAlignment = VerticalAlignment.Center
            styleRight.Alignment = HorizontalAlignment.Right
            styleRight.SetFont(font3)
            styleRight.BorderRight = BorderStyle.Thin
            styleRight.BorderBottom = BorderStyle.Thin
            styleRight.BorderTop = BorderStyle.Thin
            styleRight.BorderLeft = BorderStyle.Thin

            Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValue.SetFont(font3)
            styleValue.BorderRight = BorderStyle.Thin
            styleValue.BorderBottom = BorderStyle.Thin
            styleValue.BorderTop = BorderStyle.Thin
            styleValue.BorderLeft = BorderStyle.Thin
            styleValue.VerticalAlignment = VerticalAlignment.Center
            styleValue.Alignment = HorizontalAlignment.Right
            styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")


            Dim styleValue1 As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValue1.SetFont(font3)
            styleValue1.BorderRight = BorderStyle.Thin
            styleValue1.BorderBottom = BorderStyle.Thin
            styleValue1.BorderTop = BorderStyle.Thin
            styleValue1.BorderLeft = BorderStyle.Thin
            styleValue1.VerticalAlignment = VerticalAlignment.Center
            styleValue1.Alignment = HorizontalAlignment.Right
            styleValue1.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")




            Dim styleDate As ICellStyle = templateWorkbook.CreateCellStyle()
            styleDate.VerticalAlignment = VerticalAlignment.Center
            styleDate.Alignment = HorizontalAlignment.Center
            styleDate.BorderRight = BorderStyle.Thin
            styleDate.BorderBottom = BorderStyle.Thin
            styleDate.BorderTop = BorderStyle.Thin
            styleDate.BorderLeft = BorderStyle.Thin
            Dim formatIdDate = HSSFDataFormat.GetBuiltinFormat("dd/MM/yyyy")

            If formatIdDate = -1 Then
                Dim newDataFormat = templateWorkbook.CreateDataFormat()
                styleDate.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy")
            Else
                styleDate.DataFormat = formatIdDate
            End If

            Dim sheet As XSSFSheet = templateWorkbook.GetSheet("LegalScore")
            Dim RowsIndex As Integer
            Dim DateString As String = "_" & DateTime.Today.ToString("dd_MM_yyyy")

            Dim row As XSSFRow
            Dim cell As XSSFCell


            row = sheet.GetRow(0)
            cell = row.GetCell(0)
            cell.SetCellValue("Report as on - " + DateTime.Today.ToString("dd/MM/yyyy"))
            'cell.CellStyle = headerStyle

            RowsIndex = 2
            Dim count = 0
            Dim colIndex As Integer = 0

            Dim dt As DataTable = dset.Tables(0)

            For i = 0 To dset.Tables(0).Rows.Count - 1


                row = sheet.CreateRow(RowsIndex)
                colIndex = 0

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("Sr_No")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vm_quarter")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vendor_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("parameter_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vlm_obligation")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vlm_availability")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vlsm_score")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("obt_score")))
                cell.CellStyle = styleValue
                colIndex += 1


                cell = row.CreateCell(colIndex)
                Try
                    cell.SetCellValue(Convert.ToDateTime(dset.Tables(0).Rows(i)("valid_from")))
                Catch ex As Exception
                    cell.SetCellValue("")
                End Try
                cell.CellStyle = styleDate
                colIndex += 1

                cell = row.CreateCell(colIndex)
                Try
                    cell.SetCellValue(Convert.ToDateTime(dset.Tables(0).Rows(i)("valid_till")))
                Catch ex As Exception
                    cell.SetCellValue("")
                End Try
                cell.CellStyle = styleDate
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("valid_auth")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("status")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("remarks")))
                cell.CellStyle = styleLeft
                colIndex += 1

                RowsIndex = RowsIndex + 1

            Next

            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "Legal_Score_details_Report" & DateString & ".xlsx"
            Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)
            templateWorkbook.Write(fl)
            fl.Close()
            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            Response.WriteFile(genReportPath & file_name)
            Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        Catch ex As Exception
            Throw ex
        End Try


    End Sub



    Protected Sub btnApprove_Click(sender As Object, e As EventArgs)
        Dim dtApproveLegal As New DataTable

        dtApproveLegal.Columns.Add("param_id", GetType(Int64))
        dtApproveLegal.Columns.Add("avail", GetType(String))
        dtApproveLegal.Columns.Add("target_scr", GetType(Int64))
        dtApproveLegal.Columns.Add("status", GetType(String))
        dtApproveLegal.Columns.Add("rejected_remarks", GetType(String))

        If (gvLegalScoreList.Rows.Count > 0) Then
            For index = 0 To gvLegalScoreList.Rows.Count - 1
                Dim row As GridViewRow = gvLegalScoreList.Rows(index)
                Dim checkselect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                If (checkselect.Checked = True) Then
                    Dim hdnparamCode As HiddenField = CType(row.FindControl("hdnparamcode"), HiddenField)
                    Dim paramcode As Int64 = hdnparamCode.Value
                    Dim lblScore As TextBox = CType(row.FindControl("txtTargetScore"), TextBox)
                    Dim targetScore As Integer = Convert.ToInt32(lblScore.Text)
                    Dim ddlAvailability As DropDownList = CType(row.FindControl("ddlAvailability"), DropDownList)
                    Dim availability As String = ddlAvailability.SelectedValue

                    dtApproveLegal.Rows.Add(paramcode, availability, targetScore, "Y", "")
                End If
            Next


            Dim RecordInserted As Integer
            Dim obj As New vrs_legalscore_class
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            If (dtApproveLegal.Rows.Count > 0) Then
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                RecordInserted = obj.UpdateLegalScoreStatus(ddlquartor.SelectedValue, ddlvendor.SelectedValue, "Y", userInfo.userIDEntity, dtApproveLegal, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    sqlTrans.Commit()
                    BindGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approved successfully.');", True)
                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approval Failed!');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select atleast one item');", True)
                Return
            End If
        End If
    End Sub
    Protected Sub btnReject_Click(sender As Object, e As EventArgs)
        Dim dtRejectLegal As New DataTable

        dtRejectLegal.Columns.Add("param_id", GetType(Int64))
        dtRejectLegal.Columns.Add("avail", GetType(String))
        dtRejectLegal.Columns.Add("target_scr", GetType(Int64))
        dtRejectLegal.Columns.Add("status", GetType(String))
        dtRejectLegal.Columns.Add("rejected_remarks", GetType(String))


        If (gvLegalScoreList.Rows.Count > 0) Then
            For index = 0 To gvLegalScoreList.Rows.Count - 1
                Dim row As GridViewRow = gvLegalScoreList.Rows(index)
                Dim checkselect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                If (checkselect.Checked = True) Then
                    Dim hdnparamCode As HiddenField = CType(row.FindControl("hdnparamcode"), HiddenField)
                    Dim paramcode As Int64 = hdnparamCode.Value
                    Dim lblScore As TextBox = CType(row.FindControl("txtTargetScore"), TextBox)
                    Dim targetScore As Integer = Convert.ToInt32(lblScore.Text)
                    Dim ddlAvailability As DropDownList = CType(row.FindControl("ddlAvailability"), DropDownList)
                    Dim availability As String = ddlAvailability.SelectedValue
                    Dim txtrejRemarks As TextBox = CType(row.FindControl("txtrejRemarks"), TextBox)

                    If String.IsNullOrEmpty(txtrejRemarks.Text) Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please give remarks');", True)
                        Return
                    End If
                    dtRejectLegal.Rows.Add(paramcode, availability, targetScore, "N", txtrejRemarks.Text)
                End If
            Next

            Dim RecordInserted As Integer
            Dim obj As New vrs_legalscore_class
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            If (dtRejectLegal.Rows.Count > 0) Then
                RecordInserted = obj.UpdateLegalScoreStatus(ddlquartor.SelectedValue, ddlvendor.SelectedValue, "N", userInfo.userIDEntity, dtRejectLegal, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    sqlTrans.Commit()
                    BindGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approved successfully.');", True)
                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approval Failed!');", True)
                End If
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select atleast one item');", True)
                Return

            End If
        End If

    End Sub

    Protected Sub btnDownloadDoc_Click(sender As Object, e As EventArgs)

        Using zip As New ZipFile()
            Try
                For Each row As GridViewRow In gvLegalScoreList.Rows
                    Dim hdnFilepath As HiddenField = CType(row.FindControl("hdnFilePath"), HiddenField)
                    Dim filePath As String = Convert.ToString(hdnFilepath.Value)
                    Dim genReportPath As String = ConfigurationManager.AppSettings("UPLOAD_DOCS_FOLDER_ABS_PATH") & "\"
                    Dim fullPath As String = Path.Combine(genReportPath, filePath)
                    If File.Exists(fullPath) Then
                        zip.AddFile(fullPath, "")
                    End If
                Next

                ' Send ZIP to browser

            Catch ex As Exception
                Dim msg As String = ex.ToString()
            Finally
                Response.Clear()
                Response.BufferOutput = False
                Response.ContentType = "application/zip"
                Response.AddHeader("content-disposition", "attachment; filename=" + Convert.ToString(ddlvendor.SelectedValue) + ".Zip")

                zip.Save(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Try
        End Using

    End Sub

    Protected Sub ddlFinYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(ddlFinYear.SelectedValue) Then
            ddlquartor.Items.Clear()
        Else
            Populate_Quarter()
        End If
    End Sub




End Class
