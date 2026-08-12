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
Partial Class UnitWiseDespatchDetailsReport
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckLogin()
        txtFromDate.Attributes.Add("ReadOnly", "true")
        txtToDate.Attributes.Add("ReadOnly", "true")
        If Not IsPostBack Then

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
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim reportObj As New UnitWiseDespatchDetailsReportClass
            Dim FromDate As SqlDateTime = FormatDate(txtFromDate.Text)
            Dim ToDate As SqlDateTime = FormatDate(txtToDate.Text)

            Dim ds As DataSet

            ds = reportObj.UnitWiseDespatchDetailsReport(FromDate, ToDate, userInfo.userIDEntity)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ExportToExcelSheet1(ds)
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

#Region "Export to Excel using Dll"
    Private Sub ExportToExcelSheet1(ByVal dset As DataSet)
        'Opening the Excel template...
        Dim fs As FileStream = New FileStream(AppDomain.CurrentDomain.BaseDirectory & "Templates\UnitWiseDespatchReport.xls", FileMode.Open, FileAccess.Read)

        'Getting the complete workbook...
        Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)

        'Getting the worksheet by its name...
        Dim sheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")


        Dim font1 As IFont = templateWorkbook.CreateFont()
        font1.Color = HSSFColor.Black.Index
        font1.Boldweight = NPOI.SS.UserModel.FontBoldWeight.Normal
        font1.FontName = "Arial"
        font1.FontHeightInPoints = 9

        Dim font2 As IFont = templateWorkbook.CreateFont()
        font2.Color = HSSFColor.Black.Index
        font2.Boldweight = NPOI.SS.UserModel.FontBoldWeight.Normal
        font2.FontName = "Arial"
        font2.FontHeightInPoints = 9

        Dim font3 As IFont = templateWorkbook.CreateFont()
        font3.Color = HSSFColor.Yellow.Index
        font3.Boldweight = NPOI.SS.UserModel.FontBoldWeight.Bold
        font3.FontName = "Calibri"
        font3.FontHeightInPoints = 9
        font3.IsItalic = True

        Dim font4 As IFont = templateWorkbook.CreateFont()
        font4.Color = HSSFColor.Red.Index
        font4.Boldweight = NPOI.SS.UserModel.FontBoldWeight.Bold
        font4.FontName = "Calibri"

        font4.FontHeightInPoints = 9
        font4.IsItalic = True

        Dim style1 As ICellStyle = templateWorkbook.CreateCellStyle()
        style1.VerticalAlignment = VerticalAlignment.Center
        style1.BottomBorderColor = HSSFColor.Black.Index
        style1.SetFont(font1)

        Dim style2 As ICellStyle = templateWorkbook.CreateCellStyle()
        style2.VerticalAlignment = VerticalAlignment.Center
        style2.SetFont(font2)
        style2.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_(* #,##0.00_);_(* (#,##0.00);_(* "" ""??_);_(@_)")

        Dim style11 As ICellStyle = templateWorkbook.CreateCellStyle()
        style11.VerticalAlignment = VerticalAlignment.Center
        style11.SetFont(font1)

        Dim style12 As ICellStyle = templateWorkbook.CreateCellStyle()
        style12.VerticalAlignment = VerticalAlignment.Center
        style12.SetFont(font2)

        Dim style13 As ICellStyle = templateWorkbook.CreateCellStyle()
        style13.VerticalAlignment = VerticalAlignment.Center
        style13.SetFont(font2)

        Dim style3 As ICellStyle = templateWorkbook.CreateCellStyle()
        style3.VerticalAlignment = VerticalAlignment.Center
        style3.SetFont(font4)
        style3.FillForegroundColor = HSSFColor.SkyBlue.Index
        'style3.FillBackgroundColor = HSSFColor.SKY_BLUE.index
        style3.FillPattern = FillPattern.SolidForeground
        'style3.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_(* #,##0.00_);_(* (#,##0.00);_(* "" ""??_);_(@_)")

        Dim style4 As ICellStyle = templateWorkbook.CreateCellStyle()
        style4.VerticalAlignment = VerticalAlignment.Center
        style4.SetFont(font3)
        style4.FillForegroundColor = HSSFColor.LightYellow.Index
        style4.FillPattern = FillPattern.SolidForeground


        Dim style5 As ICellStyle = templateWorkbook.CreateCellStyle()
        style5.VerticalAlignment = VerticalAlignment.Center
        style5.SetFont(font3)
        'style5.FillForegroundColor = HSSFColor.YELLOW.index
        style5.FillPattern = FillPattern.SolidForeground


        Dim style33 As ICellStyle = templateWorkbook.CreateCellStyle()
        style33.VerticalAlignment = VerticalAlignment.Center
        style33.SetFont(font4)
        style33.FillForegroundColor = HSSFColor.Automatic.Index
        'style33.FillPattern = FillPatternType.SOLID_FOREGROUND

        Dim style35 As ICellStyle = templateWorkbook.CreateCellStyle()
        style35.VerticalAlignment = VerticalAlignment.Center
        style35.Alignment = HorizontalAlignment.Center
        style35.SetFont(font4)
        style35.FillForegroundColor = HSSFColor.Automatic.Index
        'style33.FillPattern = FillPatternType.SOLID_FOREGROUND


        Dim style32 As ICellStyle = templateWorkbook.CreateCellStyle()
        style32.VerticalAlignment = VerticalAlignment.Center
        style32.SetFont(font1)
        'style3.FillForegroundColor = HSSFColor.PALE_BLUE.index
        'style32.FillPattern = FillPatternType.SOLID_FOREGROUND
        'Dim ProductGroup_SKUTotal As Decimal = 0
        'Dim FactorySKUTotal As Decimal = 0
        'Dim 

        Dim RowIndex As Integer

        Dim group1StartIndex As Integer
        Dim group2StartIndex As Integer
        Dim group3StartIndex As Integer
        Dim group4StartIndex As Integer

        Dim row As HSSFRow
        Dim cell As HSSFCell

        Dim DateString As String = "_" & DateTime.Today.ToString("dd_MM_yyyy")

        row = sheet.GetRow(0)
        cell = row.GetCell(0)
        cell.SetCellValue("Report Date - " & DateTime.Today.ToString("dd/MM/yyyy"))

        row = sheet.GetRow(0)
        cell = row.GetCell(2)
        cell.SetCellValue("UNIT WISE DESPATCH REPORT -( " & txtFromDate.Text.Trim() & " To " & txtToDate.Text.Trim() & " )")

        RowIndex = 2

        group1StartIndex = RowIndex
        group2StartIndex = RowIndex
        group3StartIndex = RowIndex
        group4StartIndex = RowIndex

        Dim dt As DataTable = dset.Tables(0)

        If dt.Rows.Count > 0 Then

            For i = 0 To dt.Rows.Count - 1

                If (Convert.ToString(dt.Rows(i)("load_vend_unit")).Equals("zzz") _
             And Convert.ToString(dt.Rows(i)("Region")).Equals("xx") _
             And Convert.ToString(dt.Rows(i)("SKUCode")).Equals("pp")) Then

                    row = sheet.CreateRow(RowIndex)

                    cell = row.CreateCell(0)
                    cell.SetCellValue("Grand Total")
                    cell.CellStyle = style5

                    cell = row.CreateCell(1)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(2)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(3)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(4)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(5)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(6)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("vms_SKU_Vol"))))
                    cell.CellStyle = style5

                    cell = row.CreateCell(7)
                    cell.SetCellType(CellType.Numeric)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("dd_sku_vol"))))
                    cell.CellStyle = style5

                    sheet.GroupRow(group1StartIndex, RowIndex - 1)
                    group1StartIndex = RowIndex + 1
                    group2StartIndex = group1StartIndex
                    group3StartIndex = group1StartIndex
                    group4StartIndex = group1StartIndex

                ElseIf (Convert.ToString(dt.Rows(i)("Region")).Equals("xx") _
          And Convert.ToString(dt.Rows(i)("SKUCode")).Equals("pp")) Then

                    row = sheet.CreateRow(RowIndex)
                    'Get Factory  Total=================================
                    cell = row.CreateCell(0)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("Source")) + " Total")
                    cell.CellStyle = style5


                    cell = row.CreateCell(1)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(2)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(3)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(4)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(5)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(6)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("vms_SKU_Vol"))))
                    cell.CellStyle = style5

                    cell = row.CreateCell(7)
                    cell.SetCellType(CellType.Numeric)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("dd_sku_vol"))))
                    cell.CellStyle = style5


                    sheet.GroupRow(group2StartIndex, RowIndex - 1)
                    group2StartIndex = RowIndex + 1
                    group3StartIndex = group2StartIndex
                    group4StartIndex = group2StartIndex

                    RowIndex = RowIndex + 1
                ElseIf (Convert.ToString(dt.Rows(i)("DepotCode")).Equals(String.Empty) _
                        And Not Convert.ToString(dt.Rows(i)("SKUCode")).Equals("pp") _
                        And Not Convert.ToString(dt.Rows(i)("load_vend_unit")).Equals("zzz") _
                       And Convert.ToString(dt.Rows(i)("Region")).Equals("xx")) Then

                    row = sheet.CreateRow(RowIndex)
                    'Prod Group Total=================================
                    cell = row.CreateCell(0)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style3

                    cell = row.CreateCell(1)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style3

                    cell = row.CreateCell(2)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("SKUDesc")) & " Total")
                    cell.CellStyle = style3

                    cell = row.CreateCell(3)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style3

                    cell = row.CreateCell(4)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style3

                    cell = row.CreateCell(5)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style3

                    cell = row.CreateCell(6)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("vms_SKU_Vol"))))
                    cell.CellStyle = style3

                    cell = row.CreateCell(7)
                    cell.SetCellType(CellType.Numeric)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("dd_sku_vol"))))
                    cell.CellStyle = style3

                    sheet.GroupRow(group3StartIndex, RowIndex - 1)
                    group3StartIndex = RowIndex + 1
                    group4StartIndex = group3StartIndex

                    RowIndex = RowIndex + 1

                ElseIf (Not Convert.ToString(dt.Rows(i)("SKUCode")).Equals("pp") _
                    And Not Convert.ToString(dt.Rows(i)("load_vend_unit")).Equals("zzz") _
                    And Convert.ToString(dt.Rows(i)("DepotCode")).Equals(String.Empty) _
                       And Not Convert.ToString(dt.Rows(i)("Region")).Equals("xx")
                    ) Then

                    row = sheet.CreateRow(RowIndex)
                    'Prod Group Total=================================
                    cell = row.CreateCell(0)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(1)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(2)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(3)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("Region")) & " Total")
                    cell.CellStyle = style35

                    cell = row.CreateCell(4)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(5)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(6)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("vms_SKU_Vol"))))
                    cell.CellStyle = style33

                    cell = row.CreateCell(7)
                    cell.SetCellType(CellType.Numeric)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("dd_sku_vol"))))
                    cell.CellStyle = style33


                    sheet.GroupRow(group4StartIndex, RowIndex - 1)
                    group4StartIndex = RowIndex + 1
                    'group4StartIndex = group3StartIndex

                    RowIndex = RowIndex + 1


                ElseIf (Not Convert.ToString(dt.Rows(i)("load_vend_unit")).Equals("zzz") _
              And Not Convert.ToString(dt.Rows(i)("Region")).Equals("xx") _
              And Not Convert.ToString(dt.Rows(i)("SKUCode")).Equals("pp") _
              And Not Convert.ToString(dt.Rows(i)("DepotCode")).Equals(String.Empty)) Then

                    row = sheet.CreateRow(RowIndex)

                    cell = row.CreateCell(0)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("Source")))


                    cell = row.CreateCell(1)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("SKUCode")))


                    cell = row.CreateCell(2)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("SKUDesc")))

                    cell = row.CreateCell(3)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("Region")))


                    cell = row.CreateCell(4)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("DepotCode")))

                    cell = row.CreateCell(5)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("DepotName")))

                    cell = row.CreateCell(6)
                    cell.SetCellType(CellType.Numeric)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("vms_SKU_Vol"))))
                    cell.CellStyle = style32

                    cell = row.CreateCell(7)
                    cell.SetCellType(CellType.Numeric)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("dd_sku_vol"))))
                    cell.CellStyle = style32


                    RowIndex = RowIndex + 1

                End If

            Next
        End If


        Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"

        If Not (Directory.Exists(genReportPath)) Then
            Directory.CreateDirectory(genReportPath)
        End If

        Dim file_name As String = "UnitWiseTotalDespatch" & DateString & ".xls"

        'Writing workbook's data stream to the root directory
        Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)

        templateWorkbook.Write(fl)
        fl.Close()

        Response.Clear()
        Response.Charset = ""
        'Response.ClearHeaders()
        Response.ContentType = "application/vnd.ms-excel"

        'Response.ContentType = "application/vnd.ms-xls; charset=utf-8"
        'Response.AppendHeader("Pragma", "public")
        'Response.AppendHeader("Cache-Control", "public, max-age=3800")

        Response.WriteFile(genReportPath & file_name)
        Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Response.End()

    End Sub

#End Region



    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub
End Class
