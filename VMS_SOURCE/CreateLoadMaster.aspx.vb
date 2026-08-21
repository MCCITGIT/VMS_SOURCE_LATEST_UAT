'***************************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : CreateLoadMaster.aspx.vb
'Created Date	: 14/12/2011
'Created By	    : Riddhikusal Datta 
'Version	    : R01.00.00
'Description	: Code behind for CReatLoadMaster

'Modified By       Modified On       Version         Reason
'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source (Send Mail feature)

'****************************************************************
Imports Microsoft.VisualBasic
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient
'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports System.Text
Imports System.Configuration

Partial Class CreateLoadMaster
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim sr As StreamReader
    Dim linerd As Char()
    Dim filesavestrt As String
    Dim filesaveend As String
    Dim saveLocation As String
    Dim D1, D2 As Date
#Region "Login Check"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region

    '#Region "check Stock Master"
    '    Private Sub StockMasterCheck()
    '        CheckLogin()
    '        Dim ScreenDS As DataSet
    '        Dim LoadObj As New CreateLoadMasterClass
    '        ScreenDS = LoadObj.CheckStockMaster
    '        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
    '            If CType(ScreenDS.Tables(0).Rows(0)(0), Integer) = 0 Then
    '                btnProcess.Enabled = False
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Stock Master Is not Updated for current month and year')</script>", False)
    '            End If
    '        End If
    '    End Sub
    '#End Region

#Region "check Estimation Data"
    Private Sub EstimationDataAndStockCheck()
        CheckLogin()
        Dim ScreenDS As DataSet
        Dim LoadObj As New CreateLoadMasterClass
        ScreenDS = LoadObj.CheckStockAndEstimate
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            If CType(ScreenDS.Tables(0).Rows(0)(0), Integer) = 0 AndAlso CType(ScreenDS.Tables(0).Rows(0)(1), Integer) = 0 Then
                btnProcess.Enabled = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Estimation Data and Stock Master are not Updated for current month and year')</script>", False)
            ElseIf CType(ScreenDS.Tables(0).Rows(0)(0), Integer) = 0 Then
                btnProcess.Enabled = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Stock Master is not Updated for current month and year')</script>", False)
            ElseIf CType(ScreenDS.Tables(0).Rows(0)(1), Integer) = 0 Then
                btnProcess.Enabled = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Estimate Data is not Updated for current month and year')</script>", False)

            End If
        End If
    End Sub
#End Region

#Region "Get Screen Details"
    Private Sub GetScreenDetails()
        Dim ScreenDS As DataSet
        Dim LoadObj As New CreateLoadMasterClass
        ScreenDS = LoadObj.GetSCreenDetails
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            lblYear.Text = ScreenDS.Tables(0).Rows(0)("year").ToString
            lblMonth.Text = ScreenDS.Tables(0).Rows(0)("month").ToString
            lblStockAsOn.Text = ScreenDS.Tables(0).Rows(0)("load_stock_as_on").ToString
        End If
    End Sub
#End Region
#Region "Load Master Creation"
    Private Sub LoadMasterCreate()
        CheckLogin()
        Dim LoadObj As New CreateLoadMasterClass
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()
        D1 = Date.Now.ToLongTimeString
        hdnStart.Value = Format(DateTime.Now, "hh:mm:ss:fff")

        numRowsAffected = LoadObj.CreateLoadMaster(sqlConn, sqlTrans, userInfo.userIDEntity)
        If numRowsAffected > 0 Then
            sqlTrans.Commit()
            GetErrorDetails()
            hdnEnd.Value = Format(DateTime.Now, "hh:mm:ss:fff")
            D2 = Date.Now.ToLongTimeString
            lblStartTime.Text = hdnStart.Value
            lblEndTime.Text = hdnEnd.Value
            lblEclapsedTime.Text = (D2 - D1).ToString
            tabSummery.Visible = True
        Else
            sqlTrans.Rollback()

        End If
    End Sub
#End Region

'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source
#Region "Get Load Master Data For Mail"
    Private Sub ShowAlert(ByVal message As String)
        Dim safeMessage As String = message.Replace("\", "\\").Replace("'", "\'").Replace(vbCr, " ").Replace(vbLf, " ")
        ClientScript.RegisterStartupScript(Me.GetType(), "Message_" & DateTime.Now.Ticks.ToString(), "<script type='text/javascript' language='javascript'>alert('" & safeMessage & "');</script>", False)
    End Sub

    Private Sub LoadMasterDataForMail()
        CheckLogin()
        Try
            Dim LoadObj As New CreateLoadMasterClass
            Dim ds As DataSet = LoadObj.GetLoadMasterDataForMail()

            If (ds Is Nothing) OrElse ds.Tables.Count = 0 OrElse ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                ShowAlert("No records found to send mail.")
                Return
            End If

            Dim attachmentPath As String = ExportLoadMasterMailExcel(ds)
            If String.IsNullOrEmpty(attachmentPath) Then
                ShowAlert("Failed to generate mail attachment.")
                Return
            End If

            If Not File.Exists(attachmentPath) Then
                ShowAlert("Mail attachment file was not created: " & attachmentPath)
                Return
            End If

            Dim mailResult As Integer = SendLoadMasterMail(attachmentPath)
            If mailResult > 0 Then
                ShowAlert("Mail sent successfully.")
            Else
                ShowAlert("Mail sending failed.")
            End If
        Catch ex As Exception
            ShowAlert("Error while sending mail: " & ex.Message)
        End Try
    End Sub

    Private Function ExportLoadMasterMailExcel(ByVal dset As DataSet) As String
        Dim generatedFilePath As String = String.Empty
        Try
            Dim templatePath As String = Server.MapPath(Request.ApplicationPath) & "\Templates\AutoLoadMasterData_Template.xlsx"
            If Not File.Exists(templatePath) Then
                Throw New FileNotFoundException("Excel template not found: " & templatePath)
            End If

            Dim fs As FileStream = New FileStream(templatePath, FileMode.Open, FileAccess.Read)
            Dim templateWorkbook As XSSFWorkbook = New XSSFWorkbook(fs)

            Dim font3 As IFont = templateWorkbook.CreateFont()
            font3.Color = IndexedColors.Black.Index
            font3.FontName = "Calibri"
            font3.FontHeightInPoints = 10

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
            styleRight.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00 ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")

            Dim sheet As XSSFSheet = templateWorkbook.GetSheetAt(0)

            ' Image2 layout:
            ' A1 = "As On Date : dd-MM-yyyy"
            ' B1:E1 (merged) = "Auto Load Master Data (Not Generated)"
            Dim headerRow As XSSFRow = sheet.GetRow(0)
            If headerRow Is Nothing Then
                headerRow = sheet.CreateRow(0)
            End If

            Dim asOnLabelCell As XSSFCell = headerRow.GetCell(0)
            If asOnLabelCell Is Nothing Then
                asOnLabelCell = headerRow.CreateCell(0)
            End If
            asOnLabelCell.SetCellValue("As On Date : " & Format(Now, "dd-MM-yyyy"))

            Dim titleCell As XSSFCell = headerRow.GetCell(1)
            If titleCell Is Nothing Then
                titleCell = headerRow.CreateCell(1)
            End If
            titleCell.SetCellValue("Auto Load Master Data (Not Generated)")

            Dim RowsIndex As Integer = 2
            For i As Integer = 0 To dset.Tables(0).Rows.Count - 1
                Dim dr As DataRow = dset.Tables(0).Rows(i)
                Dim row As XSSFRow = sheet.CreateRow(RowsIndex)
                Dim colIndex As Integer = 0

                Dim cell As XSSFCell = row.CreateCell(colIndex)
                cell.SetCellValue(If(IsDBNull(dr("depot_name")), String.Empty, Convert.ToString(dr("depot_name"))))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(If(IsDBNull(dr("tmp_depot")), String.Empty, Convert.ToString(dr("tmp_depot"))))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(If(IsDBNull(dr("tmp_sku")), String.Empty, Convert.ToString(dr("tmp_sku"))))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                If IsDBNull(dr("tmp_est")) Then
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = styleCenter
                Else
                    Dim estimateValue As Decimal
                    If Decimal.TryParse(Convert.ToString(dr("tmp_est")), estimateValue) Then
                        cell.SetCellValue(Convert.ToDouble(estimateValue))
                        cell.CellStyle = styleRight
                    Else
                        cell.SetCellValue(Convert.ToString(dr("tmp_est")))
                        cell.CellStyle = styleCenter
                    End If
                End If
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(If(IsDBNull(dr("priority_rank")), String.Empty, Convert.ToString(dr("priority_rank"))))
                cell.CellStyle = styleCenter

                RowsIndex += 1
            Next

            Dim genReportPath As String = ConfigurationManager.AppSettings("UPLOAD_DOCS_FOLDER_ABS_PATH") & "LoadMasterMail\" & Format(Date.Now, "dd_MM_yyyy") & "\"

            If Not Directory.Exists(genReportPath) Then
                Directory.CreateDirectory(genReportPath)
            End If

            Dim file_name As String = "AutoLoadMasterData_" & DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") & ".xlsx"
            generatedFilePath = genReportPath & file_name
            Dim fl As FileStream = New FileStream(generatedFilePath, FileMode.Create)
            templateWorkbook.Write(fl)
            fl.Close()
            fs.Close()
        Catch ex As Exception
            Throw ex
        End Try

        Return generatedFilePath
    End Function

    Private Function SendLoadMasterMail(ByVal attachmentPath As String) As Integer
        Dim mailResult As Integer = 0
        Try
            Dim LoadObj As New CreateLoadMasterClass
            Dim dsMail As DataSet = LoadObj.GetLoadMasterMailIds()

            Dim toAddress As String = String.Empty
            Dim ccAddress As String = String.Empty
            Dim bccAddress As String = String.Empty

            If (Not (dsMail Is Nothing) AndAlso dsMail.Tables.Count > 0 AndAlso Not (dsMail.Tables(0) Is Nothing) AndAlso dsMail.Tables(0).Rows.Count > 0) Then
                Dim drMail As DataRow = dsMail.Tables(0).Rows(0)
                If Not IsDBNull(drMail("to_mail")) Then
                    toAddress = Convert.ToString(drMail("to_mail")).Trim()
                End If
                If Not IsDBNull(drMail("cc_mail")) Then
                    ccAddress = Convert.ToString(drMail("cc_mail")).Trim()
                End If
                If Not IsDBNull(drMail("bcc_mail")) Then
                    bccAddress = Convert.ToString(drMail("bcc_mail")).Trim()
                End If
            End If

            If String.IsNullOrWhiteSpace(toAddress) Then
                Return mailResult
            End If

            If String.IsNullOrWhiteSpace(bccAddress) Then
                bccAddress = "automailer@mccit.co.in"
            End If

            Dim bodyBuilder As New StringBuilder()
            bodyBuilder.Append("Dear Sir/Madam,<br/><br/>")
            bodyBuilder.Append("Please find attached the Auto Load Master data.<br/><br/>")
            bodyBuilder.Append("<b>Auto-load is not generated on L1 since it is Inactive</b>.<br/><br/>")
            bodyBuilder.Append("Year: " & lblYear.Text & "<br/>")
            bodyBuilder.Append("Month: " & lblMonth.Text & "<br/><br/>")
            bodyBuilder.Append("This is a System Generated Mail. Please Do not Reply.")

            Dim mailobj As New EmailSMSsender()
            Dim mailEntity As New MailEntity()
            mailEntity.ToAddress = toAddress
            mailEntity.CCAddress = ccAddress
            mailEntity.BCCAddress = bccAddress
            mailEntity.MailSubject = "Auto-load is not generated on L1 since it is Inactive"
            mailEntity.MailBody = bodyBuilder.ToString()
            mailEntity.Attachment_Path = attachmentPath
            mailEntity.Sender_Task = "sendMail_CreateLoadMaster_InactiveL1"

            mailResult = mailobj.sendMail(mailEntity)
        Catch ex As Exception
            Throw ex
        End Try

        Return mailResult
    End Function
#End Region

#Region "Get Error Details"
    Private Sub GetErrorDetails()
        CheckLogin()
        Dim ScreenDS As DataSet
        Dim LoadObj As New CreateLoadMasterClass
        ScreenDS = LoadObj.GetNotFoundCount(userInfo.userIDEntity)
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            gvNotFound.DataSource = ScreenDS
            gvNotFound.DataBind()
            Label4.Visible = True
            ddlPageSize.Visible = True
            lblNotFound.Text = "Vendor Unit not Found for Following Cases"
        Else
            gvNotFound.Visible = False
            Label4.Visible = False
            ddlPageSize.Visible = False
            lblNotFound.Text = ""
        End If
    End Sub
#End Region
#Region "Populate page size dropdown"

    ' Populates the page size dropdown
    Private Sub PageSizeDropdown()
        ddlPageSize.Items.Clear()
        'Gets the page size from the web.config file
        Dim configPagesize As String = ConfigurationManager.AppSettings.Get("PageSize")
        Dim numbers As String() = configPagesize.Split(",")
        Dim index As Integer = 0

        While index <= numbers.Length - 1
            Try
                Dim size As Integer = Convert.ToInt32(numbers(index))
                'Adds the page size to drop down list
                ddlPageSize.Items.Add(New ListItem(size.ToString, size.ToString))
            Catch exp As Exception
                ddlPageSize.Items.Clear()
                'LoadDefaultPageSize()
            End Try
            System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
        End While
        gvNotFound.PageSize = ddlPageSize.SelectedValue
    End Sub

#End Region

    Protected Sub ddlPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        gvNotFound.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue)
        GetErrorDetails()
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        LoadMasterCreate()
        btnProcess.Enabled = False
    End Sub

    'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source
    Protected Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        LoadMasterDataForMail()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckLogin()
            GetScreenDetails()
            PageSizeDropdown()
            EstimationDataAndStockCheck()

        End If
    End Sub

    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    '    CheckLogin()

    '    Dim ReportViewer As New ReportViewer_DC

    '    ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Stock_Upload_Summary_Report
    '    ReportViewer.ReportCase = Constant.ReportView.ReportCase.StockUploadSummaryRptCase

    '    ReportViewer.Active = Constant.Common.ActiveStatus
    '    ReportViewer.StckUpldProcessYear = lblYear.Text
    '    ReportViewer.StckUpldProcessMonth = lblMonth.Text

    '    ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)

    'End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As EventArgs) Handles ImageButton2.Click
        CheckLogin()

        Dim ReportViewer As New ReportViewer_DC

        ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Stock_Upload_Summary_Report
        ReportViewer.ReportCase = Constant.ReportView.ReportCase.StockUploadSummaryRptCase

        ReportViewer.Active = Constant.Common.ActiveStatus
        ReportViewer.StckUpldProcessYear = lblYear.Text
        ReportViewer.StckUpldProcessMonth = lblMonth.Text

        ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)
    End Sub

    Protected Sub gvNotFound_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvNotFound.PageIndexChanging
        gvNotFound.PageIndex = e.NewPageIndex
        GetErrorDetails()

    End Sub

    Protected Sub gvNotFound_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNotFound.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim pageIdx As Integer = gvNotFound.PageIndex * ddlPageSize.SelectedValue
            e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
            
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

End Class
