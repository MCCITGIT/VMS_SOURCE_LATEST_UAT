Imports VMS.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.Util
Imports System.Data.OleDb
Imports System.Globalization

Partial Class OCSpecificationUpload
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
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
    'Protected Sub imgbtndownload_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtndownload.Click
    '    Try
    '        Dim ds As DataSet
    '        Dim ocspecificationds As New DataSet
    '        Dim mstr As New OCSpecification
    '        Dim Obj As Common = New Common()
    '        'ds = Obj.GetLovDetails("BERGER", "OCS_PRODUCTS", "Y")
    '        ds = mstr.GetProdDetails(userInfo.userIDEntity)
    '        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
    '            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
    '                ExportToExcel(ds)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim returnUrl = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '        Response.Redirect(returnUrl)
    '    End Try
    'End Sub

    Protected Sub imgbtndownload_Click(sender As Object, e As EventArgs) Handles imgbtndownload.Click
        Try
            Dim ds As DataSet
            Dim ocspecificationds As New DataSet
            Dim mstr As New OCSpecification
            Dim Obj As Common = New Common()
            'ds = Obj.GetLovDetails("BERGER", "OCS_PRODUCTS", "Y")
            ds = mstr.GetProdDetails(userInfo.userIDEntity)
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ExportToExcel(ds)
                End If
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub ExportToExcel(ds As DataSet)
        Dim objOCSpecification As New OCSpecification
        Dim ms As MemoryStream = New MemoryStream()
        Dim WorkBook As HSSFWorkbook = New HSSFWorkbook()
        Dim dsresult As DataSet
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim ProductCode As String = ds.Tables(0).Rows(i)("lov_code").ToString()
            dsresult = objOCSpecification.OCSpecificationReportDownload(ProductCode)

            If (Not (dsresult Is Nothing) AndAlso dsresult.Tables.Count > 0) Then
                If (Not (dsresult.Tables(0) Is Nothing) AndAlso dsresult.Tables(0).Rows.Count > 0) Then
                    If (dsresult.Tables(0).Rows.Count > 0) Then
                        Dim ReportSheet As HSSFSheet = WorkBook.CreateSheet(ds.Tables(0).Rows(i)("lov_value").ToString)
                        Dim Row As HSSFRow = Nothing
                        Dim Cell As HSSFCell = Nothing
                        Dim alignLeft As ICellStyle = WorkBook.CreateCellStyle()
                        Dim alignRight As ICellStyle = WorkBook.CreateCellStyle()
                        Dim alignCenter As ICellStyle = WorkBook.CreateCellStyle()

                        Dim bgWhite As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim alignCenterBoldText As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        alignCenterBoldText.FillForegroundColor = HSSFColor.LightYellow.Index
                        alignCenterBoldText.FillPattern = FillPattern.SolidForeground
                        Dim dataStyleLeft As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim dataStyleRight As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim dataStyleCenter As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim topHeaderStyle As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim topHeaderStyle1 As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)

                        dataStyleCenter.Alignment = HorizontalAlignment.Center
                        dataStyleLeft.Alignment = HorizontalAlignment.Left
                        dataStyleRight.Alignment = HorizontalAlignment.Right
                        topHeaderStyle.Alignment = HorizontalAlignment.Left
                        topHeaderStyle.VerticalAlignment = VerticalAlignment.Center
                        topHeaderStyle.FillForegroundColor = HSSFColor.LightCornflowerBlue.Index
                        topHeaderStyle.FillPattern = FillPattern.SolidForeground

                        topHeaderStyle1.Alignment = HorizontalAlignment.Center
                        topHeaderStyle1.VerticalAlignment = VerticalAlignment.Center
                        topHeaderStyle1.FillForegroundColor = HSSFColor.LightGreen.Index
                        topHeaderStyle1.FillPattern = FillPattern.SolidForeground

                        alignLeft.Alignment = HorizontalAlignment.Left
                        alignRight.Alignment = HorizontalAlignment.Right
                        alignCenter.Alignment = HorizontalAlignment.Center
                        alignCenter.VerticalAlignment = VerticalAlignment.Center
                        alignCenterBoldText.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.VerticalAlignment = VerticalAlignment.Center
                        alignCenterBoldText.Alignment = HorizontalAlignment.Center

                        Dim headerfont = WorkBook.CreateFont()
                        headerfont.FontHeightInPoints = 11
                        headerfont.FontName = "Calibri"
                        headerfont.Boldweight = CShort(NPOI.SS.UserModel.FontBoldWeight.Bold)
                        topHeaderStyle.SetFont(headerfont)
                        topHeaderStyle1.SetFont(headerfont)
                        Dim font = WorkBook.CreateFont()
                        font.FontHeightInPoints = 11
                        font.FontName = "Calibri"
                        font.Boldweight = CShort(NPOI.SS.UserModel.FontBoldWeight.Bold)
                        alignCenterBoldText.SetFont(font)
                        alignCenterBoldText.IsLocked = True

                        'Row = CType(ReportSheet.CreateRow(0), HSSFRow)
                        'Cell = CType(Row.CreateCell(0), HSSFCell)
                        'Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                        'Cell.SetCellValue("REPORT AS ON - " & DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                        'Cell.CellStyle = topHeaderStyle


                        'Cell = CType(Row.CreateCell(2), HSSFCell)
                        'Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                        'Cell.SetCellValue("QC Specification Report")
                        'Cell.CellStyle = topHeaderStyle1

                        'Dim lastColoum As Int16 = dsresult.Tables(0).Columns.Count - 1
                        'Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 1)
                        'Dim cra1 = New NPOI.SS.Util.CellRangeAddress(0, 0, 2, lastColoum)
                        'ReportSheet.AddMergedRegion(cra)
                        'ReportSheet.AddMergedRegion(cra1)

                        Row = CType(ReportSheet.CreateRow(0), HSSFRow)
                        Dim SheetRowIndex As Integer = 1
                        Dim colIndex As Integer = 0
                        For j = 0 To dsresult.Tables(0).Columns.Count - 1
                            Cell = CType(Row.CreateCell(j), HSSFCell)
                            Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                            Cell.SetCellValue(dsresult.Tables(0).Columns(j).ColumnName)
                            Cell.CellStyle = alignCenterBoldText
                            Cell.CellStyle.Alignment = HorizontalAlignment.Center
                        Next

                        For l = 0 To dsresult.Tables(0).Rows.Count - 1
                            Row = CType(ReportSheet.CreateRow(SheetRowIndex), HSSFRow)
                            For k = 0 To dsresult.Tables(0).Columns.Count - 1
                                Cell = CType(Row.CreateCell(k), HSSFCell)
                                Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                                Cell.SetCellValue(dsresult.Tables(0).Rows(l)(k).ToString)
                                Cell.CellStyle = dataStyleCenter
                            Next
                            SheetRowIndex = SheetRowIndex + 1
                        Next
                        For m As Integer = 0 To dsresult.Tables(0).Columns.Count - 1
                            ReportSheet.AutoSizeColumn(m)
                        Next

                    End If
                End If
            End If
        Next
        Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"
        If Not (Directory.Exists(genReportPath)) Then
            Directory.CreateDirectory(genReportPath)
        End If

        Dim file_name As String = "QC_SpecificationListReport" & ".xls"
        Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)
        WorkBook.Write(fl)
        fl.Close()

        Response.Clear()
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"

        Response.WriteFile(genReportPath & file_name)
        Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
    End Sub
    'Protected Sub imgbtnupload_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnupload.Click
    '    If Request.Files("UploadFile").ContentLength <= 0 Then
    '        Return
    '    End If
    '    Dim fileExtension As String = Path.GetExtension(Request.Files("UploadFile").FileName)
    '    Dim a As String = Request.Files("UploadFile").FileName
    '    If fileExtension <> ".xls" AndAlso fileExtension <> ".xlsx" Then
    '        Return
    '    End If
    '    'Dim fileLocation As String = Server.MapPath("\") + Request.Files("UploadFile").FileName

    '    Dim FileName As String = "QC_SpecificationListReport" & Path.GetExtension(UploadFile.PostedFile.FileName)
    '    Dim Extension As String = Path.GetExtension(UploadFile.PostedFile.FileName)
    '    Dim FolderPath As String = "Excel_Reports/"
    '    Dim fileLocation As String = Server.MapPath(FolderPath + FileName)

    '    If File.Exists(fileLocation) Then
    '        File.Delete(fileLocation)
    '    End If

    '    Request.Files("UploadFile").SaveAs(fileLocation)
    '    Dim strConn As String = ""

    '    Select Case fileExtension
    '        Case ".xls"
    '            'strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
    '            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
    '        Case ".xlsx"
    '            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 12.0 xml;HDR=Yes;IMEX=1"""
    '    End Select

    '    BindData(strConn)
    '    'File.Delete(fileLocation)
    'End Sub

    Protected Sub imgbtnupload_Click(sender As Object, e As EventArgs) Handles imgbtnupload.Click
        If Request.Files("UploadFile").ContentLength <= 0 Then
            Return
        End If
        Dim fileExtension As String = Path.GetExtension(Request.Files("UploadFile").FileName)
        Dim a As String = Request.Files("UploadFile").FileName
        If fileExtension <> ".xls" AndAlso fileExtension <> ".xlsx" Then
            Return
        End If
        'Dim fileLocation As String = Server.MapPath("\") + Request.Files("UploadFile").FileName

        Dim FileName As String = "QC_SpecificationListReport" & Path.GetExtension(UploadFile.PostedFile.FileName)
        Dim Extension As String = Path.GetExtension(UploadFile.PostedFile.FileName)
        Dim FolderPath As String = "Excel_Reports/"
        Dim fileLocation As String = Server.MapPath(FolderPath + FileName)

        If File.Exists(fileLocation) Then
            File.Delete(fileLocation)
        End If

        Request.Files("UploadFile").SaveAs(fileLocation)
        Dim strConn As String = ""

        Select Case fileExtension
            Case ".xls"
                'strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
            Case ".xlsx"
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 12.0 xml;HDR=Yes;IMEX=1"""
        End Select

        BindData(strConn)
        'File.Delete(fileLocation)
    End Sub

    Private Sub BindData(ByVal strConn As String)
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim objOCSpecificationclass As New OCSpecification
        Dim Auto_Id As Integer = 0
        Dim Created_user As String = userInfo.userIDEntity
        Dim dt As DataTable = Nothing
        Dim ColumnName As String
        Dim ColumnData As String

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim objOCSpecification As New OCSpecificationEntity
        Dim objParameter As New OCSPrmPrdEntity
        Dim objConn As OleDbConnection = New OleDbConnection(strConn)
        objConn.Open()
        dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        objConn.Close()

        sqlConn = VMS.DataAccess.DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()

        Dim RowsAffected As Integer
        Dim NoofRowsAffected As Integer
        Dim DeleteRow As Integer
        Try
            objOCSpecification.createduser = userInfo.userIDEntity
            objOCSpecification.activestatus = Constant.Common.ActiveStatus
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0

            Dim dtsheet_dtls As DataTable = New DataTable()
            Dim dsError As DataSet = New DataSet()
            Dim dsHaveError As DataSet = New DataSet()
            Dim dsSheetName As DataSet = New DataSet()
            Dim SheetName As String = String.Empty
            Dim clean As String
            Dim cleanNew As String

            dtsheet_dtls.Columns.Add("Id")
            dtsheet_dtls.Columns.Add("Specifications")
            dtsheet_dtls.Columns.Add("Specifications_Value")

            Dim dtSheetname As DataTable = New DataTable()

            dtSheetname.Columns.Add("SheetName")

            For Each row As DataRow In dt.Rows
                Dim dt_sheet As DataTable = Nothing
                dt_sheet = getSheetData(strConn, row("TABLE_NAME").ToString())

                Dim dr As DataRow = dtSheetname.NewRow()
                SheetName = row("TABLE_NAME").ToString()
                clean = SheetName.Replace("'", "")
                cleanNew = clean.Replace("$", "")
                dr("SheetName") = cleanNew
                dtSheetname.Rows.Add(dr)
                dtSheetname.AcceptChanges()
                ViewState("dtSheetName") = dtSheetname

                Dim query As IEnumerable(Of DataRow) = From dtrow In dt_sheet.AsEnumerable()
                                                       Where dtrow.Field(Of String)("VENDOR_CODE") <> String.Empty Select dtrow
                Dim dtsheet_new As DataTable = Nothing
                Try
                    dtsheet_new = query.CopyToDataTable()
                Catch
                End Try
                Dim qc_dtsheet As DataTable = CreateQCSpecificationTable()
                Try
                    For i = 0 To dtsheet_new.Rows.Count - 1
                        Dim VENDOR_CODE As String = Convert.ToString(dtsheet_new.Rows(i)("VENDOR_CODE"))
                        Dim PRODUCT As String = Convert.ToString(dtsheet_new.Rows(i)("PRODUCT"))
                        Dim PRODUCT_CODE As String = Convert.ToString(dtsheet_new.Rows(i)("PRODUCT_CODE"))
                        Dim BATCH_NO As String = Convert.ToString(dtsheet_new.Rows(i)("BATCH_NO"))
                        Dim BATCH_DATE As String = FormatDate(dtsheet_new.Rows(i)("BATCH_DATE"))

                        Dim Specifications As String = ""
                        Dim Specifications_Value As String = ""

                        For c = 5 To dtsheet_new.Columns.Count - 1
                            If dtsheet_new.Columns(c).ToString() <> "Remarks" AndAlso dtsheet_new.Columns(c).ToString() <> "field_name" Then
                                Specifications = dtsheet_new.Columns(c).ToString()
                                Specifications_Value = Convert.ToString(IIf(Convert.ToString(dtsheet_new.Rows(i)(c)) = "", "", Convert.ToString(dtsheet_new.Rows(i)(c))))
                                qc_dtsheet.Rows.Add(VENDOR_CODE, PRODUCT, PRODUCT_CODE, BATCH_NO, BATCH_DATE, Specifications, Specifications_Value)
                            End If

                        Next
                    Next
                Catch ex As Exception
                    lblValidationMessage.Text = "Invalid QC Specification file."
                    Exit Sub
                End Try
                qc_dtsheet.AcceptChanges()

                Dim dt1 As DataTable = New DataTable()
                Dim dtCopy As DataTable = New DataTable()

                Dim dsNew = New DataSet()
                If dtsheet_new.Rows.Count > 0 Then
                    dt1 = objOCSpecificationclass.QCspecification_validation(qc_dtsheet, userInfo.userBranchEntity).Tables(0)
                    dtCopy = dt1.Copy()
                    dtCopy.TableName = row("TABLE_NAME").ToString()
                    dsError.Tables.Add(dtCopy)
                    ViewState("dtError") = dsError

                    Dim dtonlyError As DataTable = dt1
                    Dim view As DataView = New DataView(dtonlyError)
                    view.RowFilter = "Isnull([remarks],'') <> ''"

                    dtonlyError = view.ToTable()

                    If (dtonlyError IsNot Nothing AndAlso dtonlyError.Rows.Count > 0) Then
                        dtonlyError.TableName = row("TABLE_NAME").ToString()
                        dsHaveError.Tables.Add(dtonlyError)
                        'lblPopMessage.Text = "Some Thing Went Wrong. !!"
                        'lblPopMessage.ForeColor = System.Drawing.Color.Red
                        'ModalPopupExtender1.Show()
                        'GoTo z
                    Else
                        For j = 0 To dtsheet_new.Rows.Count - 1
                            objOCSpecification.Auto_Id = Request.QueryString(Constant.SessionKeys.OCS_ID)
                            objOCSpecification.Vendor_Code = dtsheet_new.Rows(j)("VENDOR_CODE")
                            objOCSpecification.Product_Type = dtsheet_new.Rows(j)("PRODUCT")
                            objOCSpecification.Product_Code = dtsheet_new.Rows(j)("PRODUCT_CODE")
                            objOCSpecification.Batch_No = dtsheet_new.Rows(j)("BATCH_NO")
                            objOCSpecification.Batch_Date = FormatDate(dtsheet_new.Rows(j)("BATCH_DATE"))
                            RowsAffected = objOCSpecificationclass.OC_SpecificationInsertUpdate(objOCSpecification, sqlConn, sqlTrans)

                            If (RowsAffected > 0) Then
                                dtsheet_dtls.Rows.Clear()
                                For Each column As DataColumn In dtsheet_new.Columns
                                    If (column.ToString <> "VENDOR_CODE" AndAlso column.ToString <> "PRODUCT" AndAlso column.ToString <> "PRODUCT_CODE" AndAlso column.ToString <> "BATCH_NO" AndAlso column.ToString <> "BATCH_DATE") Then
                                        ColumnName = column.ColumnName
                                        ColumnData = dtsheet_new.Rows(j)(column).ToString()
                                        dtsheet_dtls.Rows.Add(RowsAffected, ColumnName, ColumnData)
                                    End If
                                Next
                                For z = 0 To dtsheet_dtls.Rows.Count - 1
                                    objParameter.Auto_Id = dtsheet_dtls.Rows(z)("Id")
                                    objParameter.Paramss = dtsheet_dtls.Rows(z)("Specifications")
                                    objParameter.ResultType = dtsheet_dtls.Rows(z)("Specifications_Value")
                                    objParameter.CreatedUser = userInfo.userIDEntity
                                    If (objParameter.ResultType <> "") Then
                                        NoofRowsAffected += objOCSpecificationclass.OC_SpecificationDtls(objParameter, sqlConn, sqlTrans)
                                    End If
                                Next
                            End If
                        Next
                        i += 1
                    End If
                End If
            Next
            If dsHaveError.Tables.Count > 0 Then
                lblPopMessage.Text = "Some Thing Went Wrong. !!"
                lblPopMessage.ForeColor = System.Drawing.Color.Red
                ModalPopupExtender1.Show()
            Else
                If (RowsAffected > 0) Then
                    sqlTrans.Commit()
                    lblConfirmMessage.Text = "QC Specification Insertion Successfull."
                    lblConfirmMessage.ForeColor = System.Drawing.Color.Red
                Else
z:                  sqlTrans.Rollback()
                End If
            End If


        Catch ex As Exception
            sqlTrans.Rollback()
        Finally
            If sqlConn IsNot Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
    Private Function getSheetData(ByVal strConn As String, ByVal sheet As String) As DataTable
        Dim query As String = "select * from [" & sheet & "]"
        Dim objConn As OleDbConnection
        Dim oleDA As OleDbDataAdapter
        Dim dt As DataTable = New DataTable()
        objConn = New OleDbConnection(strConn)
        objConn.Open()
        oleDA = New OleDbDataAdapter(query, objConn)
        oleDA.Fill(dt)
        objConn.Close()
        oleDA.Dispose()
        objConn.Dispose()
        Return dt
    End Function
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
#Region "Methods"
    Private Shared Function CreateQCSpecificationTable() As DataTable
        Dim estTable As DataTable = New DataTable("qcDetails")
        Dim dtColumn As DataColumn

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.[GetType]("System.String")
        dtColumn.ColumnName = "VENDOR_CODE"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.[GetType]("System.String")
        dtColumn.ColumnName = "PRODUCT"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.[GetType]("System.String")
        dtColumn.ColumnName = "PRODUCT_CODE"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.[GetType]("System.String")
        dtColumn.ColumnName = "BATCH_NO"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.[GetType]("System.String")
        dtColumn.ColumnName = "BATCH_DATE"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.[GetType]("System.String")
        dtColumn.ColumnName = "Specifications"
        estTable.Columns.Add(dtColumn)

        dtColumn = New DataColumn()
        dtColumn.DataType = System.Type.[GetType]("System.String")
        dtColumn.ColumnName = "Specifications_Value"
        estTable.Columns.Add(dtColumn)

        Return estTable
    End Function
#End Region

    Protected Sub btnDownloadForRectification_Click(sender As Object, e As EventArgs) Handles btnDownloadForRectification.Click
        Dim ds As DataSet = TryCast(ViewState("dtError"), DataSet)
        'Dim SheetDsName As DataSet = TryCast(ViewState("dtSheetName"), DataSet)
        Dim SheetDsName As DataTable = TryCast(ViewState("dtSheetName"), DataTable)

        pnlMessageBox.Visible = False
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            ExportToExcelSheet(ds, SheetDsName)
        End If
    End Sub
    Protected Sub ExportToExcelSheet(ByVal ds As DataSet, ByVal SheetDsName As DataTable)
        Dim objOCSpecification As New OCSpecification
        Dim ms As MemoryStream = New MemoryStream()
        Dim WorkBook As HSSFWorkbook = New HSSFWorkbook()

        For i As Integer = 0 To SheetDsName.Rows.Count - 1
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(i) Is Nothing) AndAlso ds.Tables(i).Rows.Count > 0) Then
                    If (ds.Tables(i).Rows.Count > 0) Then
                        Dim ReportSheet As HSSFSheet = WorkBook.CreateSheet(SheetDsName.Rows(i)("SheetName").ToString())
                        Dim Row As HSSFRow = Nothing
                        Dim Cell As HSSFCell = Nothing
                        Dim alignLeft As ICellStyle = WorkBook.CreateCellStyle()
                        Dim alignRight As ICellStyle = WorkBook.CreateCellStyle()
                        Dim alignCenter As ICellStyle = WorkBook.CreateCellStyle()

                        Dim bgWhite As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim alignCenterBoldText As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        alignCenterBoldText.FillForegroundColor = HSSFColor.LightYellow.Index
                        alignCenterBoldText.FillPattern = FillPattern.SolidForeground
                        Dim dataStyleLeft As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim dataStyleRight As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim dataStyleCenter As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim topHeaderStyle As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)
                        Dim topHeaderStyle1 As HSSFCellStyle = CType(WorkBook.CreateCellStyle(), HSSFCellStyle)

                        dataStyleCenter.Alignment = HorizontalAlignment.Center
                        dataStyleLeft.Alignment = HorizontalAlignment.Left
                        dataStyleRight.Alignment = HorizontalAlignment.Right
                        topHeaderStyle.Alignment = HorizontalAlignment.Left
                        topHeaderStyle.VerticalAlignment = VerticalAlignment.Center
                        topHeaderStyle.FillForegroundColor = HSSFColor.LightCornflowerBlue.Index
                        topHeaderStyle.FillPattern = FillPattern.SolidForeground

                        topHeaderStyle1.Alignment = HorizontalAlignment.Center
                        topHeaderStyle1.VerticalAlignment = VerticalAlignment.Center
                        topHeaderStyle1.FillForegroundColor = HSSFColor.LightGreen.Index
                        topHeaderStyle1.FillPattern = FillPattern.SolidForeground

                        alignLeft.Alignment = HorizontalAlignment.Left
                        alignRight.Alignment = HorizontalAlignment.Right
                        alignCenter.Alignment = HorizontalAlignment.Center
                        alignCenter.VerticalAlignment = VerticalAlignment.Center
                        alignCenterBoldText.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
                        alignCenterBoldText.VerticalAlignment = VerticalAlignment.Center
                        alignCenterBoldText.Alignment = HorizontalAlignment.Center

                        Dim headerfont = WorkBook.CreateFont()
                        headerfont.FontHeightInPoints = 11
                        headerfont.FontName = "Calibri"
                        headerfont.Boldweight = CShort(NPOI.SS.UserModel.FontBoldWeight.Bold)
                        topHeaderStyle.SetFont(headerfont)
                        topHeaderStyle1.SetFont(headerfont)
                        Dim font = WorkBook.CreateFont()
                        font.FontHeightInPoints = 11
                        font.FontName = "Calibri"
                        font.Boldweight = CShort(NPOI.SS.UserModel.FontBoldWeight.Bold)
                        alignCenterBoldText.SetFont(font)
                        alignCenterBoldText.IsLocked = True

                        Row = CType(ReportSheet.CreateRow(0), HSSFRow)
                        Dim SheetRowIndex As Integer = 1
                        Dim colIndex As Integer = 0
                        For j = 0 To ds.Tables(i).Columns.Count - 1
                            Cell = CType(Row.CreateCell(j), HSSFCell)
                            Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                            Cell.SetCellValue(ds.Tables(i).Columns(j).ColumnName)
                            Cell.CellStyle = alignCenterBoldText
                            Cell.CellStyle.Alignment = HorizontalAlignment.Center
                        Next

                        For l = 0 To ds.Tables(i).Rows.Count - 1
                            Row = CType(ReportSheet.CreateRow(SheetRowIndex), HSSFRow)
                            For k = 0 To ds.Tables(i).Columns.Count - 1
                                Cell = CType(Row.CreateCell(k), HSSFCell)
                                Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                                Cell.SetCellValue(ds.Tables(i).Rows(l)(k).ToString)
                                Cell.CellStyle = dataStyleCenter
                            Next
                            SheetRowIndex = SheetRowIndex + 1
                        Next
                        For m As Integer = 0 To ds.Tables(i).Columns.Count - 1
                            ReportSheet.AutoSizeColumn(m)
                        Next

                    End If
                End If
            End If
        Next
        Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"
        If Not (Directory.Exists(genReportPath)) Then
            Directory.CreateDirectory(genReportPath)
        End If
        Dim file_name As String = "QC_SpecificationErrorLogFormat" & DateString & ".xls"
        Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)
        WorkBook.Write(fl)
        fl.Close()

        Response.Clear()
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"

        Response.WriteFile(genReportPath & file_name)
        Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
    End Sub
    'Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
    '    Response.Redirect("OCSpecificationList.aspx")
    'End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As EventArgs) Handles ImageButton2.Click
        Response.Redirect("OCSpecificationList.aspx")
    End Sub
End Class
