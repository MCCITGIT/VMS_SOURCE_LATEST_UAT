Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.IO

Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.Util
Imports System.Globalization
Imports NPOI.SS.Util
Partial Class DirectDespatch_OrderDetails
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            ' txtFromDate.Text = Format(Date.Now, "dd/MM/yyyy")
            'txtToDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture())
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture())
        End If

    End Sub

#Region "Check Login"
    Private Sub CheckLogin()

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
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
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim reportObj As New DirectDespatch_OrderClass
            Dim FromDate As SqlDateTime = FormatDate(txtFromDate.Text)
            Dim ToDate As SqlDateTime = FormatDate(txtToDate.Text)

            Dim ds As DataSet

            ds = reportObj.DirectDespatch_OrderDetailsReport(FromDate, ToDate, userInfo.userIDEntity)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ExportToExcelSheet(ds)
                Else
                    lblErrMsg.Text = "No data found."
                End If
            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorExporttoExcel
            Server.Transfer(returnUrl)
        End Try
    End Sub
    Private Sub ExportToExcelSheet(ByVal dset As DataSet)
        'Opening the Excel template...
        Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\DirectDespatch_OrderDetailsReport.xls", FileMode.Open, FileAccess.Read)

        'Getting the complete workbook...
        Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)

        Dim font1 As IFont = templateWorkbook.CreateFont()
        font1.Color = HSSFColor.BLACK.index
        font1.FontName = "Calibri"
        font1.FontHeightInPoints = 9

        Dim font2 As IFont = templateWorkbook.CreateFont()
        font2.Color = HSSFColor.BLACK.index
        font2.FontName = "Calibri"
        font2.FontHeightInPoints = 9

        Dim font3 As IFont = templateWorkbook.CreateFont()
        font3.Color = HSSFColor.YELLOW.index
        font3.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        font3.FontName = "Calibri"
        font3.FontHeightInPoints = 9
        font3.IsItalic = True

        Dim font4 As IFont = templateWorkbook.CreateFont()
        font4.Color = HSSFColor.RED.index
        font4.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        font4.FontName = "Calibri"

        font4.FontHeightInPoints = 9
        font4.IsItalic = True

        Dim font5 As IFont = templateWorkbook.CreateFont()
        font5.Color = HSSFColor.BLUE.index
        font5.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        font5.FontName = "Calibri"
        font5.FontHeightInPoints = 9
        font5.IsItalic = True

        Dim stylecenter As ICellStyle = templateWorkbook.CreateCellStyle()
        stylecenter.VerticalAlignment = VerticalAlignment.CENTER
        stylecenter.Alignment = HorizontalAlignment.CENTER
        stylecenter.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN
        stylecenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN
        stylecenter.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN
        stylecenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN

        stylecenter.SetFont(font1)

        Dim styleRight As ICellStyle = templateWorkbook.CreateCellStyle()
        styleRight.VerticalAlignment = VerticalAlignment.CENTER
        styleRight.Alignment = HorizontalAlignment.RIGHT
        styleRight.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN
        styleRight.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN
        styleRight.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN
        styleRight.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN

        styleRight.SetFont(font1)

        ''style2.FillPattern = FillPattern.SolidForeground
        ''style2.FillForegroundColor = HSSFColor.BLACK.index


        Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
        styleValue.SetFont(font1)
        styleValue.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue.VerticalAlignment = VerticalAlignment.CENTER
        styleValue.Alignment = HorizontalAlignment.RIGHT
        styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00 ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")

        Dim styleValue1 As ICellStyle = templateWorkbook.CreateCellStyle()
        styleValue1.SetFont(font1)
        styleValue1.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue1.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue1.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue1.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN
        styleValue1.VerticalAlignment = VerticalAlignment.CENTER
        styleValue1.Alignment = HorizontalAlignment.RIGHT
        styleValue1.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")


        Dim styleleft As ICellStyle = templateWorkbook.CreateCellStyle()
        styleleft.VerticalAlignment = VerticalAlignment.CENTER
        styleleft.Alignment = HorizontalAlignment.LEFT
        styleleft.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN
        styleleft.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN
        styleleft.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN
        styleleft.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN
        styleleft.SetFont(font2)

        Dim styleDate As ICellStyle = templateWorkbook.CreateCellStyle()
        styleDate.VerticalAlignment = VerticalAlignment.CENTER
        styleDate.Alignment = HorizontalAlignment.CENTER
        styleDate.SetFont(font2)

        styleDate.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN
        styleDate.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN
        styleDate.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN
        styleDate.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN
        styleDate.VerticalAlignment = VerticalAlignment.CENTER

        Dim formatIdDate = HSSFDataFormat.GetBuiltinFormat("dd/MM/yyyy")

        If formatIdDate = -1 Then
            Dim newDataFormat = templateWorkbook.CreateDataFormat()
            styleDate.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy")
        Else
            styleDate.DataFormat = formatIdDate
        End If

        'Getting the worksheet by its name...
        Dim sheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")

        Dim RowsIndex As Integer

        Dim row As HSSFRow
        Dim cell As HSSFCell

        Dim DateString As String = "_" & DateTime.Today.ToString("dd_MM_yyyy")

        row = sheet.GetRow(0)
        cell = row.GetCell(0)

        cell.SetCellValue("Report as on - " + DateTime.Today.ToString("dd/MM/yyyy"))
        RowsIndex = 2
        Dim dt As DataTable = dset.Tables(0)

        For i = 0 To dt.Rows.Count - 1

            row = sheet.CreateRow(RowsIndex)

            'cell = row.CreateCell(0)
            'cell.SetCellValue(i + 1)
            'cell.CellStyle = styleleft

            cell = row.CreateCell(0)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("org_regn")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(1)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("Depot_Name")))
            cell.CellStyle = styleleft

            cell = row.CreateCell(2)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("vom_org_name")))
            cell.CellStyle = styleleft

            cell = row.CreateCell(3)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("vm_vendor_name")))
            cell.CellStyle = styleleft

            cell = row.CreateCell(4)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("ddrh_order_sl_no")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(5)
            Try
                cell.SetCellValue(Convert.ToDateTime(dset.Tables(0).Rows(i)("created_date")))
            Catch ex As Exception
                cell.SetCellValue("")
            End Try
            cell.CellStyle = styleDate

            cell = row.CreateCell(6)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("ddrh_approval_status")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(7)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("ddrh_po_no")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(8)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("ddrd_sku_code")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(9)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("sku_desc")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(10)
            cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("ddrd_sku_qty"))))
            cell.CellStyle = styleRight

            cell = row.CreateCell(11)
            'cell.SetCellType(CellType.NUMERIC)
            cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("ddrd_sku_volume"))))
            cell.CellStyle = styleRight

            cell = row.CreateCell(12)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("ddah_release_num")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(13)
            Try
                'cell.SetCellValue(Convert.ToDateTime(dset.Tables(0).Rows(i)("ddah_release_date")))
                cell.SetCellValue(Convert.ToDateTime(dt.Rows(i)("ddah_release_date")))
            Catch ex As Exception
                cell.SetCellValue("")
            End Try
            cell.CellStyle = styleDate

            cell = row.CreateCell(14)
            If dt.Rows(i)("vendor_rank") Is DBNull.Value Then
                cell.SetCellValue("")
            Else
                cell.SetCellValue(Convert.ToInt32(dt.Rows(i)("vendor_rank")))
            End If
            cell.CellStyle = styleRight

            cell = row.CreateCell(15)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("vendor_reason")))
            cell.CellStyle = styleleft

            cell = row.CreateCell(16)
            cell.SetCellValue(Convert.ToString(dt.Rows(i)("order_status")))
            cell.CellStyle = styleRight

            cell = row.CreateCell(17)

            cell.SetCellValue(Convert.ToString(dt.Rows(i)("invoice_no")))
            cell.CellStyle = stylecenter

            cell = row.CreateCell(18)
            Try
                cell.SetCellValue(Convert.ToString(dt.Rows(i)("invoice_date")))
            Catch ex As Exception
                cell.SetCellValue("")
            End Try
            cell.CellStyle = styleDate

            RowsIndex = RowsIndex + 1
        Next

        Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"

        If Not (Directory.Exists(genReportPath)) Then
            Directory.CreateDirectory(genReportPath)
        End If

        Dim file_name As String = "DirectDespatch_OrderDetailsReport" & DateString & ".xls"

        'Writing workbook's data stream to the root directory
        Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)

        templateWorkbook.Write(fl)
        fl.Close()

        Response.Clear()
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Response.WriteFile(genReportPath & file_name)
        Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Response.End()
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.SuppressContent = True
        HttpContext.Current.ApplicationInstance.CompleteRequest()
    End Sub

End Class
