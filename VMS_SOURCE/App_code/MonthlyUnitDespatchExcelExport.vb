Imports System.Data
Imports System.Globalization
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel

Public Class MonthlyUnitDespatchExcelExport

    Public Shared Sub PrepareDataForExcel(ByVal ds As DataSet)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim ltrVal As Object = ds.Tables(0).Rows(i)("LTR")
            If ltrVal IsNot DBNull.Value AndAlso Convert.ToDouble(ltrVal) = 0.0 Then
                ds.Tables(0).Rows(i)("LTR") = DBNull.Value
            End If

            Dim kgVal As Object = ds.Tables(0).Rows(i)("KG")
            If kgVal IsNot DBNull.Value AndAlso Convert.ToDouble(kgVal) = 0.0 Then
                ds.Tables(0).Rows(i)("KG") = DBNull.Value
            End If

            Dim approved As String = Convert.ToString(ds.Tables(0).Rows(i)("Approved Y/N"))
            If approved = "Y" Then
                ds.Tables(0).Rows(i)("Approved Y/N") = "Yes"
            ElseIf approved = "N" OrElse approved = "" Then
                ds.Tables(0).Rows(i)("Approved Y/N") = "No"
            End If
        Next
    End Sub

    Public Shared Sub ExportToExcelSheet(ByVal dset As DataSet, ByVal fromDate As String, ByVal toDate As String, ByVal companyCode As String, ByVal templateBasePath As String, ByVal response As HttpResponse)
        Dim templatePath As String = templateBasePath & "Templates\Unit_Wise_Despatch_Report.xlsx"
        Dim useTemplate As Boolean = File.Exists(templatePath)

        Dim templateWorkbook As XSSFWorkbook = Nothing
        Dim sheet As XSSFSheet = Nothing

        If useTemplate Then
            Try
                Using fs As New FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    templateWorkbook = New XSSFWorkbook(fs)
                End Using
                sheet = GetReportSheet(templateWorkbook)
                If sheet Is Nothing Then
                    useTemplate = False
                End If
            Catch
                useTemplate = False
            End Try
        End If

        If Not useTemplate Then
            templateWorkbook = New XSSFWorkbook()
            sheet = CType(templateWorkbook.CreateSheet("Sheet1"), XSSFSheet)
        End If

        Dim fontTitle As IFont = templateWorkbook.CreateFont()
        fontTitle.FontName = "Calibri"
        fontTitle.FontHeightInPoints = 14
        fontTitle.Boldweight = CShort(FontBoldWeight.Bold)

        Dim fontBold As IFont = templateWorkbook.CreateFont()
        fontBold.FontName = "Calibri"
        fontBold.FontHeightInPoints = 11
        fontBold.Boldweight = CShort(FontBoldWeight.Bold)

        Dim fontNormal As IFont = templateWorkbook.CreateFont()
        fontNormal.FontName = "Calibri"
        fontNormal.FontHeightInPoints = 10

        Dim fontHeader As IFont = templateWorkbook.CreateFont()
        fontHeader.FontName = "Calibri"
        fontHeader.FontHeightInPoints = 10
        fontHeader.Boldweight = CShort(FontBoldWeight.Bold)
        fontHeader.Color = IndexedColors.White.Index

        Dim styleTitle As ICellStyle = templateWorkbook.CreateCellStyle()
        styleTitle.Alignment = HorizontalAlignment.Center
        styleTitle.VerticalAlignment = VerticalAlignment.Center
        styleTitle.SetFont(fontTitle)

        Dim styleReportAsOf As ICellStyle = templateWorkbook.CreateCellStyle()
        styleReportAsOf.Alignment = HorizontalAlignment.Left
        styleReportAsOf.VerticalAlignment = VerticalAlignment.Center
        styleReportAsOf.SetFont(fontBold)
        styleReportAsOf.FillForegroundColor = IndexedColors.LightYellow.Index
        styleReportAsOf.FillPattern = FillPattern.SolidForeground

        Dim styleColHeader As ICellStyle = templateWorkbook.CreateCellStyle()
        styleColHeader.Alignment = HorizontalAlignment.Center
        styleColHeader.VerticalAlignment = VerticalAlignment.Center
        styleColHeader.SetFont(fontHeader)
        styleColHeader.FillForegroundColor = IndexedColors.Grey50Percent.Index
        styleColHeader.FillPattern = FillPattern.SolidForeground
        styleColHeader.BorderTop = BorderStyle.Thin
        styleColHeader.BorderBottom = BorderStyle.Thin
        styleColHeader.BorderLeft = BorderStyle.Thin
        styleColHeader.BorderRight = BorderStyle.Thin
        styleColHeader.WrapText = True

        Dim styleMid As ICellStyle = CreateBorderedStyle(templateWorkbook, fontNormal, HorizontalAlignment.Center)
        Dim styleLeft As ICellStyle = CreateBorderedStyle(templateWorkbook, fontNormal, HorizontalAlignment.Left)
        Dim styleRight As ICellStyle = CreateBorderedStyle(templateWorkbook, fontNormal, HorizontalAlignment.Right)
        styleRight.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0")

        Dim styleValue As ICellStyle = CreateBorderedStyle(templateWorkbook, fontNormal, HorizontalAlignment.Right)
        styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("#,##0.00")

        Dim headers() As String = {
            "Srl", "Type", "Source", "Site Name", "Oracle Vendor ID", "Oracle Vendor Name", "Oracle Vendor Site",
            "PO No.", "Process Year", "Month", "Challan Id.", "Vendor Challan No.", "Vendor Challan Date",
            "Challan Creation Date", "SKU Code", "Product", "Shade", "Pack", "Description",
            "Region", "Depot Code", "Depot Name", "UOM", "NOP", "LTR", "KG",
            "Transporter", "Vehicle Number", "Road Permit No.", "Approved Y/N",
            "Release Date", "Release Number", "GRN No", "GRN Date"
        }
        Dim lastCol As Integer = headers.Length - 1
        Const reportAsOnColSpan As Integer = 2

        Dim row As XSSFRow
        Dim cell As XSSFCell
        Dim headerRowIndex As Integer

        If useTemplate Then
            row = sheet.GetRow(0)
            If row Is Nothing Then row = sheet.CreateRow(0)
            cell = row.GetCell(0)
            If cell Is Nothing Then cell = row.CreateCell(0)
            cell.SetCellValue("Report As Of : " & toDate)

            cell = row.GetCell(reportAsOnColSpan + 1)
            If cell Is Nothing Then cell = row.CreateCell(reportAsOnColSpan + 1)
            cell.SetCellValue("Unit Wise Despatch Report")

            row = sheet.GetRow(1)
            If row Is Nothing Then row = sheet.CreateRow(1)
            cell = row.GetCell(0)
            If cell Is Nothing Then cell = row.CreateCell(0)
            cell.SetCellValue("From Date : " & fromDate & "      To Date : " & toDate)

            headerRowIndex = 2
        Else
            row = CType(sheet.CreateRow(0), XSSFRow)
            row.HeightInPoints = 22
            cell = CType(row.CreateCell(0), XSSFCell)
            cell.SetCellValue("Report As Of : " & toDate)
            cell.CellStyle = styleReportAsOf
            sheet.AddMergedRegion(New CellRangeAddress(0, 0, 0, reportAsOnColSpan))

            cell = CType(row.CreateCell(reportAsOnColSpan + 1), XSSFCell)
            cell.SetCellValue("Unit Wise Despatch Report")
            cell.CellStyle = styleTitle
            sheet.AddMergedRegion(New CellRangeAddress(0, 0, reportAsOnColSpan + 1, lastCol))

            row = CType(sheet.CreateRow(1), XSSFRow)
            cell = CType(row.CreateCell(0), XSSFCell)
            cell.SetCellValue("From Date : " & fromDate)
            cell.CellStyle = styleReportAsOf
            sheet.AddMergedRegion(New CellRangeAddress(1, 1, 0, reportAsOnColSpan))

            cell = CType(row.CreateCell(reportAsOnColSpan + 1), XSSFCell)
            cell.SetCellValue("To Date : " & toDate)
            cell.CellStyle = styleReportAsOf
            sheet.AddMergedRegion(New CellRangeAddress(1, 1, reportAsOnColSpan + 1, lastCol))

            headerRowIndex = 2
            row = CType(sheet.CreateRow(headerRowIndex), XSSFRow)
            row.HeightInPoints = 18
            For h As Integer = 0 To headers.Length - 1
                cell = CType(row.CreateCell(h), XSSFCell)
                cell.SetCellValue(headers(h))
                cell.CellStyle = styleColHeader
            Next
        End If

        Dim rowIndex As Integer = headerRowIndex + 1
        Dim templateStyleRow As XSSFRow = sheet.GetRow(rowIndex)

        For i As Integer = 0 To dset.Tables(0).Rows.Count - 1
            row = sheet.GetRow(rowIndex)
            If row Is Nothing Then row = sheet.CreateRow(rowIndex)

            For c As Integer = 0 To headers.Length - 1
                cell = row.GetCell(c)
                If cell Is Nothing Then cell = row.CreateCell(c)

                Dim colName As String = headers(c)
                Dim rawVal As Object

                If colName = "Srl" Then
                    rawVal = i + 1
                ElseIf colName = "Type" Then
                    rawVal = dset.Tables(0).Rows(i)("DespatchType")
                Else
                    rawVal = dset.Tables(0).Rows(i)(colName)
                End If

                If useTemplate Then
                    SetTemplateCellValue(cell, colName, rawVal)
                    If templateStyleRow IsNot Nothing AndAlso templateStyleRow.GetCell(c) IsNot Nothing Then
                        cell.CellStyle = templateStyleRow.GetCell(c).CellStyle
                    End If
                ElseIf colName = "NOP" Then
                    cell.SetCellValue(SafeToDouble(rawVal))
                    cell.CellStyle = styleRight
                ElseIf colName = "LTR" OrElse colName = "KG" Then
                    If rawVal Is Nothing OrElse rawVal Is DBNull.Value Then
                        cell.SetCellValue(String.Empty)
                        cell.CellStyle = styleMid
                    Else
                        cell.SetCellValue(SafeToDouble(rawVal))
                        cell.CellStyle = styleValue
                    End If
                ElseIf colName = "Srl" Then
                    cell.SetCellValue(SafeToDouble(rawVal))
                    cell.CellStyle = styleMid
                ElseIf colName = "Depot Code" OrElse colName = "Shade" OrElse colName = "Pack" Then
                    cell.SetCellValue(CleanExcelText(rawVal))
                    cell.CellStyle = styleMid
                ElseIf colName = "Type" OrElse colName = "Source" OrElse colName = "Site Name" OrElse colName = "Oracle Vendor Name" _
                    OrElse colName = "Oracle Vendor Site" OrElse colName = "Description" OrElse colName = "Depot Name" _
                    OrElse colName = "Transporter" OrElse colName = "GRN No" Then
                    cell.SetCellValue(Convert.ToString(rawVal))
                    cell.CellStyle = styleLeft
                Else
                    cell.SetCellValue(Convert.ToString(rawVal))
                    cell.CellStyle = styleMid
                End If
            Next
            rowIndex = rowIndex + 1
        Next

        If Not useTemplate Then
            Dim widths() As Integer = {
                8, 16, 28, 18, 14, 24, 18, 14, 10, 8, 12, 16, 14, 16, 16, 10, 8, 10, 28,
                10, 12, 16, 8, 10, 10, 10, 18, 14, 14, 12, 12, 14, 18, 12
            }
            For w As Integer = 0 To Math.Min(widths.Length, headers.Length) - 1
                sheet.SetColumnWidth(w, widths(w) * 256)
            Next
            sheet.CreateFreezePane(0, headerRowIndex + 1)
        End If

        Dim dateString As String = "_" & DateTime.Today.ToString("dd_MM_yyyy")
        Dim genReportPath As String = templateBasePath & "Excel_Reports\"

        If Not Directory.Exists(genReportPath) Then
            Directory.CreateDirectory(genReportPath)
        End If

        Dim fileName As String = companyCode & "_Unit_Wise_Despatch_Report" & dateString & ".xlsx"
        Dim fl As FileStream = New FileStream(genReportPath & fileName, FileMode.Create)
        templateWorkbook.Write(fl)
        fl.Close()

        response.Clear()
        response.Charset = ""
        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        response.AppendHeader("content-disposition", "attachment; filename=" & fileName)
        response.WriteFile(genReportPath & fileName)
        response.End()
    End Sub

    Private Shared Function GetReportSheet(ByVal workbook As XSSFWorkbook) As XSSFSheet
        Dim sheet As ISheet = workbook.GetSheet("Sheet1")
        If sheet Is Nothing AndAlso workbook.NumberOfSheets > 0 Then
            sheet = workbook.GetSheetAt(0)
        End If
        If sheet Is Nothing Then
            Return Nothing
        End If
        Return CType(sheet, XSSFSheet)
    End Function

    Private Shared Sub SetTemplateCellValue(ByVal cell As ICell, ByVal colName As String, ByVal rawVal As Object)
        If colName = "NOP" OrElse colName = "Srl" Then
            cell.SetCellValue(SafeToDouble(rawVal))
        ElseIf colName = "LTR" OrElse colName = "KG" Then
            If rawVal Is Nothing OrElse rawVal Is DBNull.Value Then
                cell.SetCellValue(String.Empty)
            Else
                cell.SetCellValue(SafeToDouble(rawVal))
            End If
        ElseIf colName = "Depot Code" OrElse colName = "Shade" OrElse colName = "Pack" Then
            cell.SetCellValue(CleanExcelText(rawVal))
        Else
            cell.SetCellValue(Convert.ToString(rawVal))
        End If
    End Sub

    Private Shared Function CreateBorderedStyle(ByVal workbook As IWorkbook, ByVal font As IFont, ByVal alignment As HorizontalAlignment) As ICellStyle
        Dim style As ICellStyle = workbook.CreateCellStyle()
        style.Alignment = alignment
        style.VerticalAlignment = VerticalAlignment.Center
        style.SetFont(font)
        style.BorderTop = BorderStyle.Thin
        style.BorderBottom = BorderStyle.Thin
        style.BorderLeft = BorderStyle.Thin
        style.BorderRight = BorderStyle.Thin
        Return style
    End Function

    Private Shared Function CleanExcelText(ByVal value As Object) As String
        Dim text As String = Convert.ToString(value)
        If String.IsNullOrEmpty(text) Then
            Return String.Empty
        End If
        Return text.TrimStart("'"c)
    End Function

    Private Shared Function SafeToDouble(ByVal value As Object) As Double
        If value Is Nothing OrElse value Is DBNull.Value Then
            Return 0R
        End If

        Dim result As Double
        If Double.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, result) Then
            Return result
        End If
        If Double.TryParse(Convert.ToString(value), result) Then
            Return result
        End If
        Return 0R
    End Function

End Class
