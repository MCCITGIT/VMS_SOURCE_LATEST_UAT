
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports VMS.DataAccess
Imports VMS.Web

Partial Class BulkLoadDropUpload
    Inherits System.Web.UI.Page

    Private _processYear As String = String.Empty
    Private _processMonth As String = String.Empty
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        AddAttribute()
        If Not IsPostBack Then
            LoadProcessYearMonth()
            lblProcessYear.Text = _processYear
            lblProcessMonth.Text = _processMonth
        End If
    End Sub
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
    Private Sub AddAttribute()
        btnSubmit.Attributes.Add("onclick", "return ValidateSubmit();")
    End Sub

    Private Sub LoadProcessYearMonth()
        Dim standrdParams As New MonthlyUnitDespatch
        Dim standardYrMnth As DataSet = standrdParams.GetMnthsYr(Constant.Common.ActiveStatus)

        If standardYrMnth IsNot Nothing AndAlso standardYrMnth.Tables.Count > 0 AndAlso
            standardYrMnth.Tables(0).Rows.Count > 1 Then
            _processYear = standardYrMnth.Tables(0).Rows(0)("param_char_value").ToString()
            _processMonth = standardYrMnth.Tables(0).Rows(1)("param_char_value").ToString()
        Else
            _processYear = DateTime.Now.Year.ToString()
            _processMonth = DateTime.Now.Month.ToString("00")
        End If
    End Sub

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs)
        Dim workbook As IWorkbook = New HSSFWorkbook()
        Dim sheet As ISheet = workbook.CreateSheet("BulkLoadDrop")

        Dim textStyle As ICellStyle = workbook.CreateCellStyle()
        textStyle.DataFormat = workbook.CreateDataFormat().GetFormat("@")

        sheet.SetDefaultColumnStyle(0, textStyle)
        sheet.SetDefaultColumnStyle(1, textStyle)
        sheet.SetDefaultColumnStyle(2, textStyle)

        Dim header As IRow = sheet.CreateRow(0)
        header.CreateCell(0).SetCellValue("Depot Code")
        header.CreateCell(1).SetCellValue("SKU Code")
        header.CreateCell(2).SetCellValue("Unit Code")
        header.CreateCell(3).SetCellValue("Qty")

        Const templateRows As Integer = 500
        For r As Integer = 1 To templateRows
            Dim dataRow As IRow = sheet.CreateRow(r)
            For c As Integer = 0 To 2
                Dim cell As ICell = dataRow.CreateCell(c)
                cell.CellStyle = textStyle
                cell.SetCellValue(String.Empty)
            Next
        Next

        sheet.SetColumnWidth(0, 15 * 256)
        sheet.SetColumnWidth(1, 15 * 256)
        sheet.SetColumnWidth(2, 15 * 256)
        sheet.SetColumnWidth(3, 10 * 256)

        Using ms As New MemoryStream()
            workbook.Write(ms)
            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment; filename=BulkLoadDropUploadTemplate.xls")
            Response.BinaryWrite(ms.ToArray())
            Response.End()
        End Using
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        lblMsg.Text = String.Empty
        lblMsg.ForeColor = Drawing.Color.Red
        lbtnDwnloadFile.Visible = False
        pnlErrorList.Visible = False
        gvLoadDropList.DataSource = Nothing
        gvLoadDropList.DataBind()

        LoadProcessYearMonth()

        Dim userInfo As VMSUserEntity = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Try
            If Not fupUploadFile.HasFile Then
                lblMsg.Text = "Please upload an Excel file."
                Return
            End If

            Dim fileExt As String = Path.GetExtension(fupUploadFile.FileName).ToLower()
            If fileExt <> ".xls" Then
                lblMsg.Text = "Only Excel files (.xls) allowed."
                Return
            End If

            Dim dt As DataTable = ReadExcelToDT(fupUploadFile.FileContent)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                lblMsg.Text = "No data found in the uploaded file."
                Return
            End If

            Dim uploadDt As DataTable = BuildUploadDataTable(dt)
            If uploadDt.Rows.Count = 0 Then
                lblMsg.Text = "No valid data rows found in the uploaded file."
                Return
            End If

            Dim loadDropObj As New LoadDropAddUpdateClass
            Dim dsResult As DataSet = loadDropObj.GetBulkLoadDropList(uploadDt, _processYear, _processMonth)

            If dsResult Is Nothing Then
                lblMsg.Text = "Validation failed."
                Return
            End If

            Dim hasErrors As Boolean = dsResult.Tables.Count > 1 AndAlso dsResult.Tables(1).Rows.Count > 0
            Dim hasValidRows As Boolean = dsResult.Tables.Count > 0 AndAlso dsResult.Tables(0).Rows.Count > 0

            If hasErrors Then
                Session("dtBulkLoadDropError") = dsResult.Tables(1)
                gvLoadDropList.DataSource = dsResult.Tables(1)
                gvLoadDropList.DataBind()
                pnlErrorList.Visible = True
                lblMsg.Text = "There are some errors in the file. Please correct and upload again."
                lbtnDwnloadFile.Visible = True
                Return
            End If

            If Not hasValidRows Then
                lblMsg.Text = "No valid records found to upload."
                Return
            End If

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            Dim dtReturn As DataSet = loadDropObj.InsertBulkLoadDrop(dsResult.Tables(0), _processYear, _processMonth, userInfo.userIDEntity, sqlConn, sqlTrans)

            If dtReturn IsNot Nothing AndAlso dtReturn.Tables.Count > 0 AndAlso
                Convert.ToInt32(dtReturn.Tables(0).Rows(0)("InsertedCount")) > 0 Then
                sqlTrans.Commit()
                lblMsg.Text = "Records uploaded successfully."
                lblMsg.ForeColor = Drawing.Color.Green
                Session("dtBulkLoadDropError") = Nothing
            Else
                sqlTrans.Rollback()
                lblMsg.Text = "Something went wrong."
            End If

        Catch ex As System.Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                sqlTrans.Rollback()
            End If
            lblMsg.Text = ex.Message
            lblMsg.ForeColor = Drawing.Color.Red
        Finally
            If Not (sqlConn Is Nothing) Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Protected Sub lbtnDwnloadFile_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = TryCast(Session("dtBulkLoadDropError"), DataTable)
        If dt IsNot Nothing Then
            ExportToExcel(dt)
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/LoadDropList.aspx")
    End Sub

    Private Function BuildUploadDataTable(ByVal source As DataTable) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("depot_code", GetType(String))
        dt.Columns.Add("sku_code", GetType(String))
        dt.Columns.Add("unit_code", GetType(String))
        dt.Columns.Add("qty", GetType(Int32))

        Dim depotCol As String = GetColumnName(source, "Depot Code", "depot_code")
        Dim skuCol As String = GetColumnName(source, "SKU Code", "sku_code")
        Dim unitCol As String = GetColumnName(source, "Unit Code", "unit_code")
        Dim qtyCol As String = GetColumnName(source, "Qty", "qty")

        If depotCol = String.Empty OrElse skuCol = String.Empty OrElse unitCol = String.Empty OrElse qtyCol = String.Empty Then
            Throw New Exception("Invalid template. Required columns: Depot Code, SKU Code, Unit Code, Qty.")
        End If

        Dim rowMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

        For Each row As DataRow In source.Rows
            If IsRowEmpty(row, depotCol, skuCol, unitCol, qtyCol) Then
                Continue For
            End If

            Dim qtyValue As Integer
            If Not Integer.TryParse(Convert.ToString(row(qtyCol)).Trim(), qtyValue) Then
                qtyValue = 0
            End If

            Dim depotCode As String = FormatDepotCode(Convert.ToString(row(depotCol)).Trim())
            Dim skuCode As String = Convert.ToString(row(skuCol)).Trim()
            Dim unitCode As String = Convert.ToString(row(unitCol)).Trim()
            Dim rowKey As String = depotCode & "|" & skuCode & "|" & unitCode

            If rowMap.ContainsKey(rowKey) Then
                Dim existingIndex As Integer = rowMap(rowKey)
                dt.Rows(existingIndex)("qty") = CInt(dt.Rows(existingIndex)("qty")) + qtyValue
            Else
                rowMap(rowKey) = dt.Rows.Count
                dt.Rows.Add(depotCode, skuCode, unitCode, qtyValue)
            End If
        Next

        Return dt
    End Function

    Private Function FormatDepotCode(ByVal value As String) As String
        If String.IsNullOrWhiteSpace(value) Then
            Return String.Empty
        End If

        value = value.Trim()

        If value.Contains(".") Then
            Dim numericValue As Double
            If Double.TryParse(value, numericValue) AndAlso numericValue = Math.Truncate(numericValue) Then
                value = Convert.ToInt64(numericValue).ToString()
            End If
        End If

        If value.Length < 3 AndAlso value.All(Function(ch) Char.IsDigit(ch)) Then
            Return value.PadLeft(3, "0"c)
        End If

        Return value
    End Function

    Private Function GetColumnName(ByVal dt As DataTable, ByVal displayName As String, ByVal altName As String) As String
        If dt.Columns.Contains(displayName) Then
            Return displayName
        End If
        If dt.Columns.Contains(altName) Then
            Return altName
        End If
        Return String.Empty
    End Function

    Private Function IsRowEmpty(ByVal row As DataRow, ByVal depotCol As String, ByVal skuCol As String, ByVal unitCol As String, ByVal qtyCol As String) As Boolean
        Return String.IsNullOrWhiteSpace(Convert.ToString(row(depotCol))) AndAlso
            String.IsNullOrWhiteSpace(Convert.ToString(row(skuCol))) AndAlso
            String.IsNullOrWhiteSpace(Convert.ToString(row(unitCol))) AndAlso
            String.IsNullOrWhiteSpace(Convert.ToString(row(qtyCol)))
    End Function

    Private Function ReadExcelToDT(stream As Stream) As DataTable
        Dim workbook As IWorkbook = New HSSFWorkbook(stream)
        Dim sheet As ISheet = workbook.GetSheetAt(0)
        Dim dt As New DataTable()
        Dim formatter As New DataFormatter()

        Dim headerRow As IRow = sheet.GetRow(0)
        If headerRow Is Nothing Then
            Return dt
        End If

        For i As Integer = 0 To headerRow.LastCellNum - 1
            Dim headerCell As ICell = headerRow.GetCell(i)
            dt.Columns.Add(If(headerCell IsNot Nothing, formatter.FormatCellValue(headerCell), "Column" & i))
        Next

        For r As Integer = 1 To sheet.LastRowNum
            Dim excelRow As IRow = sheet.GetRow(r)
            If excelRow Is Nothing Then
                Continue For
            End If

            Dim dr As DataRow = dt.NewRow()
            Dim hasValue As Boolean = False

            For c As Integer = 0 To dt.Columns.Count - 1
                Dim cell As ICell = excelRow.GetCell(c)
                Dim cellValue As String = GetCellText(cell, formatter)
                dr(c) = cellValue
                If Not String.IsNullOrWhiteSpace(cellValue) Then
                    hasValue = True
                End If
            Next

            If hasValue Then
                dt.Rows.Add(dr)
            End If
        Next

        Return dt
    End Function

    Private Function GetCellText(cell As ICell, formatter As DataFormatter) As String
        If cell Is Nothing Then
            Return String.Empty
        End If

        If cell.CellType = CellType.Numeric Then
            Dim num As Double = cell.NumericCellValue
            If num = Math.Truncate(num) Then
                Return Convert.ToInt64(num).ToString()
            End If
        End If

        Return formatter.FormatCellValue(cell)
    End Function

    Private Sub ExportToExcel(dt As DataTable)
        Dim workbook As IWorkbook = New HSSFWorkbook()
        Dim sheet As ISheet = workbook.CreateSheet("Errors")

        Dim header As IRow = sheet.CreateRow(0)
        For i As Integer = 0 To dt.Columns.Count - 1
            header.CreateCell(i).SetCellValue(dt.Columns(i).ColumnName)
        Next

        For r As Integer = 0 To dt.Rows.Count - 1
            Dim excelRow As IRow = sheet.CreateRow(r + 1)
            For c As Integer = 0 To dt.Columns.Count - 1
                excelRow.CreateCell(c).SetCellValue(dt.Rows(r)(c).ToString())
            Next
        Next

        Using ms As New MemoryStream()
            workbook.Write(ms)
            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment; filename=BulkLoadDropUpload_Errors.xls")
            Response.BinaryWrite(ms.ToArray())
            Response.End()
        End Using
    End Sub

End Class
