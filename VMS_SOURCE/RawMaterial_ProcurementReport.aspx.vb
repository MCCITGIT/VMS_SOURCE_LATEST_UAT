Imports System.Data
Imports VMS.Web
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports System.Globalization

Partial Class RawMaterial_ProcurementReport
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If (Not IsPostBack) Then
            PopulateUnit()
            PopulateVendor()
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture())
            txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture())
        End If
    End Sub
#End Region

#Region "PopulateVendor"
    Public Sub PopulateVendor()
        Dim obj As New OPC_VendorClass()
        Dim ds As New DataSet()
        ds = obj.GetRawMaterialVendorList()

        ddlVendor.Items.Clear()
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlVendor.DataSource = ds.Tables(0)
            ddlVendor.DataTextField = "vendor_name"
            ddlVendor.DataValueField = "vendor_code"
            ddlVendor.DataBind()
        End If
        ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub
#End Region
#Region "Populate Unit"
    Private Sub PopulateUnit()
        Dim obj As New OPC_VendorClass()
        Dim UnitSet As DataSet = obj.GetUnitName(Constant.Common.ActiveStatus)

        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlUnit.SelectedValue = userInfo.userUnitEntity
        '    ddlUnit.Enabled = False
        'End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlUnit.SelectedValue = userInfo.userBranchEntity
            ddlUnit.Enabled = False
        End If
    End Sub
#End Region
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Home.aspx")
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim cls As New OPC_VendorClass
        Dim ExcelSet As New DataSet
        Try
            Dim FromDate As SqlDateTime = FormatDate(txtFromDate.Text)
            Dim ToDate As SqlDateTime = FormatDate(txtTodate.Text)
            ExcelSet = cls.GetRawMeterial_ProcurementReport(ddlUnit.SelectedValue, ddlVendor.SelectedValue, FromDate, ToDate)

            If (ExcelSet.Tables(0).Rows.Count > 0) Then
                ExportToExcelSheet(ExcelSet)
            Else
                lblErrMsg.Text = "No Records Found"
            End If
        Catch ex As Exception
            Dim str As String = ex.Message.ToString()
        End Try
    End Sub
    Private Sub ExportToExcelSheet(ByVal dset As DataSet)
        Try
            'Opening the Excel template......
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\RawMeterialProcurementReportTemplate.xlsx", FileMode.Open, FileAccess.Read)

            'Getting the complete workbook...
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

            Dim font4 As IFont = templateWorkbook.CreateFont()
            font4.Color = IndexedColors.Red.Index
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
            'styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")
            styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")

            Dim styleValueCenter As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValueCenter.SetFont(font3)
            styleValueCenter.VerticalAlignment = VerticalAlignment.Center
            styleValueCenter.Alignment = HorizontalAlignment.Center
            styleValueCenter.BorderRight = BorderStyle.Thin
            styleValueCenter.BorderBottom = BorderStyle.Thin
            styleValueCenter.BorderTop = BorderStyle.Thin
            styleValueCenter.BorderLeft = BorderStyle.Thin
            styleValueCenter.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")

            Dim styleValueDec As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValueDec.SetFont(font3)
            styleValueDec.BorderRight = BorderStyle.Thin
            styleValueDec.BorderBottom = BorderStyle.Thin
            styleValueDec.BorderTop = BorderStyle.Thin
            styleValueDec.BorderLeft = BorderStyle.Thin
            styleValueDec.VerticalAlignment = VerticalAlignment.Center
            styleValueDec.Alignment = HorizontalAlignment.Right
            styleValueDec.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")

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

            Dim sheet As XSSFSheet = templateWorkbook.GetSheet("Summary")
            Dim RowsIndex As Integer

            Dim row As XSSFRow
            Dim cell As XSSFCell

            Dim DateString As String = "_" & DateTime.Today.ToString("dd_MM_yyyy")
            row = sheet.GetRow(0)
            cell = row.GetCell(0)
            cell.SetCellValue("Raw Material Procurement Report As on -" + Format(Now, "dd-MM-yyyy").ToString)

            RowsIndex = 2
            Dim count = 0
            Dim colIndex As Integer = 0

            For i = 0 To dset.Tables(0).Rows.Count - 1
                Dim drSummary As DataRow = dset.Tables(0).Rows(i)
                row = sheet.CreateRow(RowsIndex)
                colIndex = 0

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowString(drSummary, "vendor_code"))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowString(drSummary, "vendor_name"))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowString(drSummary, "rawmat_vendor_code"))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowString(drSummary, "rawmat_vendor_name"))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowString(drSummary, "request_id"))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                TrySetDateCell(cell, drSummary, "Request_date")
                cell.CellStyle = styleDate
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowDouble(drSummary, "request_qty"))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowString(drSummary, "despatch_id"))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                TrySetDateCell(cell, drSummary, "despatch_date")
                cell.CellStyle = styleDate
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowDouble(drSummary, "despatch_qty"))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowString(drSummary, "received_id"))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                TrySetDateCell(cell, drSummary, "received_date")
                cell.CellStyle = styleDate
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowDouble(drSummary, "received_qty"))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(GetRowDouble(drSummary, "pending_qty"))
                cell.CellStyle = styleValue
                colIndex += 1

                RowsIndex = RowsIndex + 1
            Next

            If dset.Tables.Count > 1 AndAlso Not (dset.Tables(1) Is Nothing) Then
                Dim detailSheet As XSSFSheet = templateWorkbook.GetSheet("Details")
                row = detailSheet.GetRow(0)
                cell = row.GetCell(0)
                cell.SetCellValue("Raw Material Procurement Details Report As on -" + Format(Now, "dd-MM-yyyy").ToString)

                RowsIndex = 2
                Dim dtDetails As DataTable = dset.Tables(1)
                For i = 0 To dtDetails.Rows.Count - 1
                    Dim dr As DataRow = dtDetails.Rows(i)
                    row = detailSheet.CreateRow(RowsIndex)
                    colIndex = 0

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "vendor_code"))
                    cell.CellStyle = styleCenter
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "vendor_name"))
                    cell.CellStyle = styleLeft
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "rawmat_vendor_code"))
                    cell.CellStyle = styleCenter
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "rawmat_vendor_name"))
                    cell.CellStyle = styleLeft
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "request_id"))
                    cell.CellStyle = styleCenter
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    TrySetDateCell(cell, dr, "Request_date")
                    cell.CellStyle = styleDate
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowDouble(dr, "request_qty"))
                    cell.CellStyle = styleValue
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "request_rawmaterial"))
                    cell.CellStyle = styleLeft
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "despatch_id"))
                    cell.CellStyle = styleCenter
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    TrySetDateCell(cell, dr, "despatch_date")
                    cell.CellStyle = styleDate
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowDouble(dr, "despatch_qty"))
                    cell.CellStyle = styleValue
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "despatch_rawmaterial"))
                    cell.CellStyle = styleLeft
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "received_id"))
                    cell.CellStyle = styleCenter
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    TrySetDateCell(cell, dr, "received_date")
                    cell.CellStyle = styleDate
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowDouble(dr, "received_qty"))
                    cell.CellStyle = styleValue
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowString(dr, "received_rawmaterial"))
                    cell.CellStyle = styleLeft
                    colIndex += 1

                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(GetRowDouble(dr, "pending_qty"))
                    cell.CellStyle = styleValue
                    colIndex += 1

                    RowsIndex = RowsIndex + 1
                Next
            End If

            Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"
            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "RawMaterial_Procurement_Report Report_" & DateString & ".xlsx"
            Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)
            templateWorkbook.Write(fl)
            fl.Close()
            Response.Clear()
            Response.Charset = ""
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

    Private Function GetRowString(ByVal dr As DataRow, ByVal columnName As String) As String
        If dr Is Nothing OrElse dr.Table Is Nothing OrElse Not dr.Table.Columns.Contains(columnName) Then
            Return String.Empty
        End If
        If dr(columnName) Is Nothing OrElse dr(columnName) Is DBNull.Value Then
            Return String.Empty
        End If
        Return Convert.ToString(dr(columnName))
    End Function

    Private Function GetRowDouble(ByVal dr As DataRow, ByVal columnName As String) As Double
        Dim textValue As String = GetRowString(dr, columnName)
        Dim numericValue As Double
        If Double.TryParse(textValue, numericValue) Then
            Return numericValue
        End If
        Return 0
    End Function

    Private Sub TrySetDateCell(ByVal cell As XSSFCell, ByVal dr As DataRow, ByVal columnName As String)
        Dim textValue As String = GetRowString(dr, columnName)
        If String.IsNullOrWhiteSpace(textValue) Then
            Return
        End If
        Try
            cell.SetCellValue(Convert.ToDateTime(textValue))
        Catch ex As Exception
        End Try
    End Sub

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
End Class
