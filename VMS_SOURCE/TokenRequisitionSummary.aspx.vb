Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports VMS.Web
Partial Class TokenRequisitionSummary
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CheckLogin()
        If Not IsPostBack Then

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

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim ds As New DataSet
        CheckLogin()
        Dim obj As New TokenRequisitionSummaryClass
        Try
            ds = obj.GetTokenRequisitionSummaryData(userInfo.userGroupCodeEntity, userInfo.userIDEntity)
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ExportToExcel(ds)
                Else
                    lblErrMsg.Text = "No Data Found..."
                End If
            Else
                lblErrMsg.Text = "No Data Found..."
            End If

        Catch ex As Exception
            'Dim returnUrl As String = "~/ExceptionPage.aspx"
            'Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorExporttoExcel
            'Server.Transfer(returnUrl)
        End Try


    End Sub

    Private Function GetDsForExcel() As DataSet
        Dim ds As New System.Data.DataSet
        Dim obj As New TokenRequisitionSummaryClass
        Try
            ds = obj.GetTokenRequisitionSummaryData(userInfo.userGroupCodeEntity, userInfo.userIDEntity)
        Catch ex As Exception
            Console.Write(ex)
        End Try
        Return ds
    End Function

    Private Sub ExportToExcel(ds As DataSet)

        If (ds.Tables(0).Rows.Count > 0) Then


            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\TokenRequisitionSummaryReportTemplate.xls", FileMode.Open, FileAccess.Read)

            Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)
            Dim ReportSheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")


            Dim font3 As IFont = templateWorkbook.CreateFont()
            font3.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
            font3.FontName = "Calibri"
            font3.FontHeightInPoints = 10


            Dim style1 As ICellStyle = templateWorkbook.CreateCellStyle()
            style1.Alignment = HorizontalAlignment.RIGHT
            style1.SetFont(font3)
            Dim currency As Short = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")
            style1.DataFormat = currency

            Dim styleCenter As ICellStyle = templateWorkbook.CreateCellStyle()
            styleCenter.VerticalAlignment = VerticalAlignment.CENTER
            styleCenter.Alignment = HorizontalAlignment.CENTER
            styleCenter.SetFont(font3)
            styleCenter.BorderRight = BorderStyle.THIN
            styleCenter.BorderBottom = BorderStyle.THIN
            styleCenter.BorderTop = BorderStyle.THIN
            styleCenter.BorderLeft = BorderStyle.THIN


            Dim styleLeft As ICellStyle = templateWorkbook.CreateCellStyle()
            styleLeft.VerticalAlignment = VerticalAlignment.CENTER
            styleLeft.Alignment = HorizontalAlignment.LEFT
            styleLeft.SetFont(font3)
            styleLeft.BorderRight = BorderStyle.THIN
            styleLeft.BorderBottom = BorderStyle.THIN
            styleLeft.BorderTop = BorderStyle.THIN
            styleLeft.BorderLeft = BorderStyle.THIN

            Dim styleRight As ICellStyle = templateWorkbook.CreateCellStyle()
            styleRight.VerticalAlignment = VerticalAlignment.CENTER
            styleRight.Alignment = HorizontalAlignment.RIGHT
            styleRight.SetFont(font3)
            styleRight.BorderRight = BorderStyle.THIN
            styleRight.BorderBottom = BorderStyle.THIN
            styleRight.BorderTop = BorderStyle.THIN
            styleRight.BorderLeft = BorderStyle.THIN

            Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValue.SetFont(font3)
            styleValue.BorderRight = BorderStyle.THIN
            styleValue.BorderBottom = BorderStyle.THIN
            styleValue.BorderTop = BorderStyle.THIN
            styleValue.BorderLeft = BorderStyle.THIN
            styleValue.VerticalAlignment = VerticalAlignment.CENTER
            styleValue.Alignment = HorizontalAlignment.RIGHT
            styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")

            Dim styleDate As ICellStyle = templateWorkbook.CreateCellStyle()
            styleDate.VerticalAlignment = VerticalAlignment.CENTER
            styleDate.Alignment = HorizontalAlignment.CENTER
            styleDate.BorderRight = BorderStyle.THIN
            styleDate.BorderBottom = BorderStyle.THIN
            styleDate.BorderTop = BorderStyle.THIN
            styleDate.BorderLeft = BorderStyle.THIN
            Dim formatIdDate = HSSFDataFormat.GetBuiltinFormat("dd/MM/yyyy")

            If formatIdDate = -1 Then
                Dim newDataFormat = templateWorkbook.CreateDataFormat()
                styleDate.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy")
            Else
                styleDate.DataFormat = formatIdDate
            End If

            Dim Row As HSSFRow
            Dim Cell As HSSFCell


            Row = ReportSheet.GetRow(0)
            Cell = Row.GetCell(0)

            Cell.SetCellValue("Report As On - " & DateTime.Today.ToString("dd/MM/yyyy"))
            Cell = Row.GetCell(1)

            Cell.SetCellValue("Token Requisition Summary")


            Dim SheetRowIndex As Integer = 2
            Dim colIndex As Integer = 0

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Row = ReportSheet.CreateRow(SheetRowIndex)
                colIndex = 0

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToString(ds.Tables(0).Rows(i)("RequisitionId")))
                Cell.CellStyle = styleCenter
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToString(ds.Tables(0).Rows(i)("Barcode_Generated")))
                Cell.CellStyle = styleCenter
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToString(ds.Tables(0).Rows(i)("Factory_Code")))
                Cell.CellStyle = styleCenter
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToString(ds.Tables(0).Rows(i)("Factory_Name")))
                Cell.CellStyle = styleLeft
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToString(ds.Tables(0).Rows(i)("Vendor_Name")))
                Cell.CellStyle = styleLeft
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToString(ds.Tables(0).Rows(i)("Month")))
                Cell.CellStyle = styleCenter
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToString(ds.Tables(0).Rows(i)("Year")))
                Cell.CellStyle = styleCenter
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Try
                    Cell.SetCellValue(Convert.ToDateTime(ds.Tables(0).Rows(i)("Requisition_Date")))
                Catch ex As Exception
                    Cell.SetCellValue("")
                End Try
                Cell.CellStyle = styleDate
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Try
                    Cell.SetCellValue(Convert.ToDateTime(ds.Tables(0).Rows(i)("Generation_Date")))
                Catch ex As Exception
                    Cell.SetCellValue("")
                End Try
                Cell.CellStyle = styleDate
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToInt64(ds.Tables(0).Rows(i)("TotalRequisitionQty")))
                Cell.CellStyle = styleRight
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(Convert.ToInt64(ds.Tables(0).Rows(i)("TotalDespatchQty")))
                Cell.CellStyle = styleRight
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Try
                    Cell.SetCellValue(ds.Tables(0).Rows(i)("DespatchDate"))
                Catch ex As Exception
                    Cell.SetCellValue("")
                End Try
                Cell.CellStyle = styleDate
                colIndex += 1

                SheetRowIndex = SheetRowIndex + 1
            Next
            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If

            Dim DateString As String = "_" & Format(Now, "dd-MM-yyyy_HH-mm-ss")
            Dim file_name As String = "Token_Requisition_Summary_" + DateString + ".xls"

            Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)

            templateWorkbook.Write(fl)
            fl.Close()

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.WriteFile(genReportPath & file_name)
            Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Flush()
            ' REMOVE TEMP FILE AFTER DOWNLOAD
            'If (File.Exists(genReportPath & file_name)) Then
            '    File.Delete(genReportPath & file_name)
            'End If

            Response.End()

        End If
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("Home.aspx")
    End Sub
End Class

