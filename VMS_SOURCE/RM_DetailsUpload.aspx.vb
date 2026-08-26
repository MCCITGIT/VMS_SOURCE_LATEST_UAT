
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.XSSF.UserModel
Imports VMS.DataAccess
Imports VMS.Web

Partial Class RM_DetailsUpload
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private ReadOnly ConnStr As String =
        "YOUR_CONNECTION_STRING_HERE"

#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            'AddAttributes()

            Populate_Quarter()
            'PopulateVendorBrand(String.Empty)
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

#Region "Populate Quarter"
    Private Sub Populate_Quarter()
        Try
            Dim obj As New QualityControlClass
            Dim ds As New DataSet
            ddlQuarter.Items.Clear()
            ds = obj.Get_QuarterList(userInfo.userIDEntity)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlQuarter.DataSource = ds.Tables(0)
                ddlQuarter.DataTextField = "qm_quarter_short_code"
                ddlQuarter.DataValueField = "qm_id"
                ddlQuarter.DataBind()

                If Not (ds.Tables(0).Rows.Count = 1) Then
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Try
            Dim obj As New QualityControlClass
            Dim dsRmDetails As New DataSet

            dsRmDetails = obj.GetSupplierData()
            If (Not (dsRmDetails Is Nothing) AndAlso dsRmDetails.Tables.Count > 0 AndAlso Not (dsRmDetails.Tables(0) Is Nothing) AndAlso dsRmDetails.Tables(0).Rows.Count > 0) Then
                ExportToExcel(dsRmDetails)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub

    Private Sub ExportToExcel(ds As DataSet)

        If ds Is Nothing OrElse ds.Tables.Count < 3 Then Exit Sub

        Dim workbook As New HSSFWorkbook()
        Dim sheet As HSSFSheet = workbook.CreateSheet("SupplierDetails")
        Dim hiddenSheet As HSSFSheet = workbook.CreateSheet("HiddenSheet")

        workbook.SetSheetHidden(workbook.GetSheetIndex(hiddenSheet), True)

        '==============================
        ' HEADER
        '==============================
        Dim headerRow As HSSFRow = sheet.CreateRow(0)

        headerRow.CreateCell(0).SetCellValue("Supplier")
        headerRow.CreateCell(1).SetCellValue("Chemical")
        headerRow.CreateCell(2).SetCellValue("Vendor")
        headerRow.CreateCell(3).SetCellValue("Billed Qty")
        headerRow.CreateCell(4).SetCellValue("Supplier_ID")

        sheet.SetColumnHidden(4, True)

        sheet.SetColumnWidth(0, 6000)
        sheet.SetColumnWidth(1, 6000)
        sheet.SetColumnWidth(2, 9000)

        Dim pointer As Integer = 0

        '==============================
        ' SUPPLIER MASTER
        '==============================
        Dim supplierTable As DataTable = ds.Tables(0)
        Dim supplierStart As Integer = pointer

        For i As Integer = 0 To supplierTable.Rows.Count - 1

            Dim rowHidden = hiddenSheet.CreateRow(pointer)

            rowHidden.CreateCell(0).SetCellValue(
            supplierTable.Rows(i)("supplier_name").ToString())

            rowHidden.CreateCell(1).SetCellValue(
            supplierTable.Rows(i)("supplier_id").ToString())

            pointer += 1
        Next

        Dim supplierEnd As Integer = pointer - 1

        Dim supplierNameRange As IName = workbook.CreateName()
        supplierNameRange.NameName = "SupplierList"
        supplierNameRange.RefersToFormula =
        "HiddenSheet!$A$" & (supplierStart + 1) &
        ":$A$" & (supplierEnd + 1)

        pointer += 2

        '==============================
        ' CHEMICAL MASTER
        '==============================
        Dim chemicalTable As DataTable = ds.Tables(1)
        Dim chemicalStart As Integer = pointer

        For i As Integer = 0 To chemicalTable.Rows.Count - 1

            Dim rowHidden = hiddenSheet.CreateRow(pointer)

            rowHidden.CreateCell(0).SetCellValue(
            chemicalTable.Rows(i)("chemical_name").ToString())

            rowHidden.CreateCell(1).SetCellValue(
            chemicalTable.Rows(i)("chemical_id").ToString())

            pointer += 1
        Next

        Dim chemicalEnd As Integer = pointer - 1

        Dim chemicalNameRange As IName = workbook.CreateName()
        chemicalNameRange.NameName = "ChemicalList"
        chemicalNameRange.RefersToFormula =
        "HiddenSheet!$A$" & (chemicalStart + 1) &
        ":$A$" & (chemicalEnd + 1)

        pointer += 2

        '==============================
        ' VENDOR MASTER
        '==============================
        Dim vendorTable As DataTable = ds.Tables(2)
        Dim vendorStart As Integer = pointer

        For i As Integer = 0 To vendorTable.Rows.Count - 1

            Dim rowHidden = hiddenSheet.CreateRow(pointer)

            rowHidden.CreateCell(0).SetCellValue(
            vendorTable.Rows(i)("vendor_name").ToString())

            rowHidden.CreateCell(1).SetCellValue(
            vendorTable.Rows(i)("vendor_id").ToString())

            pointer += 1
        Next

        Dim vendorEnd As Integer = pointer - 1

        Dim vendorNameRange As IName = workbook.CreateName()
        vendorNameRange.NameName = "VendorList"
        vendorNameRange.RefersToFormula =
        "HiddenSheet!$A$" & (vendorStart + 1) &
        ":$A$" & (vendorEnd + 1)

        '==============================
        ' APPLY DROPDOWN (Rows 2–101)
        '==============================
        Dim dvHelper As New HSSFDataValidationHelper(sheet)

        ' Supplier dropdown (Column 0 → A)
        Dim supplierRange As New CellRangeAddressList(1, 100, 0, 0)
        Dim supplierValidation = dvHelper.CreateValidation(
        DVConstraint.CreateFormulaListConstraint("SupplierList"),
        supplierRange)
        sheet.AddValidationData(supplierValidation)

        ' Chemical dropdown (Column 1 → B)
        Dim chemicalRange As New CellRangeAddressList(1, 100, 1, 1)
        Dim chemicalValidation = dvHelper.CreateValidation(
        DVConstraint.CreateFormulaListConstraint("ChemicalList"),
        chemicalRange)
        sheet.AddValidationData(chemicalValidation)

        ' Vendor dropdown (Column 2 → C)
        Dim vendorRange As New CellRangeAddressList(1, 100, 2, 2)
        Dim vendorValidation = dvHelper.CreateValidation(
        DVConstraint.CreateFormulaListConstraint("VendorList"),
        vendorRange)
        sheet.AddValidationData(vendorValidation)

        '==============================
        ' BILLED QTY VALIDATION (Rows 2–101, Column D)
        '==============================
        ' Block strings explicitly: must be a number AND > 0
        ' Note: use a relative formula anchored to the first cell in the range (D2).
        Dim billedQtyRange As New CellRangeAddressList(1, 100, 3, 3)
        Dim billedQtyConstraint As DVConstraint =
            DVConstraint.CreateNumericConstraint(ValidationType.DECIMAL, OperatorType.GREATER_THAN, "0", Nothing)
        Dim billedQtyValidation = dvHelper.CreateValidation(billedQtyConstraint, billedQtyRange)
        billedQtyValidation.ErrorStyle = ERRORSTYLE.STOP
        billedQtyValidation.CreateErrorBox("Invalid Billed Qty", "Billed Qty must be a numeric value greater than 0. Text is not allowed.")
        billedQtyValidation.ShowErrorBox = True
        billedQtyValidation.EmptyCellAllowed = False
        sheet.AddValidationData(billedQtyValidation)

        '==============================
        ' AUTO SUPPLIER_ID MAPPING
        '==============================
        For rowIndex As Integer = 1 To 100

            Dim row = sheet.GetRow(rowIndex)
            If row Is Nothing Then
                row = sheet.CreateRow(rowIndex)
            End If

            Dim idCell = row.CreateCell(4)

            Dim excelRow As Integer = rowIndex + 1

            ' Supplier is in Column A now
            Dim formula As String =
            "IFERROR(VLOOKUP(A" & excelRow &
            ",HiddenSheet!A:B,2,FALSE),"""")"

            idCell.CellFormula = formula

        Next

        '==============================
        ' SAVE FILE
        '==============================
        Dim path As String = Server.MapPath("~/Excel_Reports/")
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        Dim fileName As String =
        "Supplier_Details_" &
        DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls"

        Using fs As New FileStream(path & fileName, FileMode.Create)
            workbook.Write(fs)
        End Using

        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("content-disposition",
                          "attachment;filename=" & fileName)
        Response.WriteFile(path & fileName)
        Response.End()

    End Sub

    'Private Sub ExportToExcel(ds As DataSet)

    '    Dim workbook As New HSSFWorkbook()
    '    Dim sheet As HSSFSheet = workbook.CreateSheet("SupplierDetails")
    '    Dim hiddenSheet As HSSFSheet = workbook.CreateSheet("HiddenSheet")

    '    ' Hide master sheet
    '    workbook.SetSheetHidden(workbook.GetSheetIndex(hiddenSheet), True)

    '    '====================================
    '    ' CREATE HEADER
    '    '====================================
    '    'Dim headerRow As HSSFRow = sheet.CreateRow(0)
    '    'headerRow.CreateCell(0).SetCellValue("Quarter")
    '    'headerRow.CreateCell(1).SetCellValue("Supplier")
    '    'headerRow.CreateCell(2).SetCellValue("Chemical")
    '    'headerRow.CreateCell(3).SetCellValue("Vendor")
    '    'headerRow.CreateCell(4).SetCellValue("Billed Qty")

    '    'sheet.SetColumnWidth(0, 4000)
    '    'sheet.SetColumnWidth(1, 6000)
    '    'sheet.SetColumnWidth(2, 6000)
    '    'sheet.SetColumnWidth(3, 9000)
    '    'sheet.SetColumnWidth(4, 4000)

    '    Dim pointer As Integer = 0

    '    '====================================
    '    ' SUPPLIER MASTER
    '    '====================================
    '    Dim suppliers As String() = {
    '    "Sup 1",
    '    "Sup 2",
    '    "Sup 3",
    '    "Sup 4"
    '}

    '    Dim supplierStart = pointer

    '    For i As Integer = 0 To suppliers.Length - 1
    '        hiddenSheet.CreateRow(pointer).CreateCell(0).SetCellValue(suppliers(i))
    '        pointer += 1
    '    Next

    '    Dim supplierEnd = pointer - 1

    '    Dim nameSupplier As IName = workbook.CreateName()
    '    nameSupplier.NameName = "SupplierList"
    '    nameSupplier.RefersToFormula = "HiddenSheet!$A$" & (supplierStart + 1) &
    '                               ":$A$" & (supplierEnd + 1)

    '    pointer += 2

    '    '====================================
    '    ' CHEMICAL MASTER
    '    '====================================
    '    Dim chemicals As String() = {
    '    "chem tes",
    '    "RNA",
    '    "CEMENT"
    '}

    '    Dim chemicalStart = pointer

    '    For i As Integer = 0 To chemicals.Length - 1
    '        hiddenSheet.CreateRow(pointer).CreateCell(0).SetCellValue(chemicals(i))
    '        pointer += 1
    '    Next

    '    Dim chemicalEnd = pointer - 1

    '    Dim nameChemical As IName = workbook.CreateName()
    '    nameChemical.NameName = "ChemicalList"
    '    nameChemical.RefersToFormula = "HiddenSheet!$A$" & (chemicalStart + 1) &
    '                               ":$A$" & (chemicalEnd + 1)

    '    pointer += 2

    '    '====================================
    '    ' VENDOR MASTER
    '    '====================================
    '    Dim vendors As String() = {
    '    "Carborundum Universal Limited-(U87)",
    '    "CREATIVE UDYOG-KALYANI-(U07)",
    '    "Promoz Products Pvt. Ltd.-(V36)"
    '}

    '    Dim vendorStart = pointer

    '    For i As Integer = 0 To vendors.Length - 1
    '        hiddenSheet.CreateRow(pointer).CreateCell(0).SetCellValue(vendors(i))
    '        pointer += 1
    '    Next

    '    Dim vendorEnd = pointer - 1

    '    Dim nameVendor As IName = workbook.CreateName()
    '    nameVendor.NameName = "VendorList"
    '    nameVendor.RefersToFormula = "HiddenSheet!$A$" & (vendorStart + 1) &
    '                             ":$A$" & (vendorEnd + 1)

    '    '====================================
    '    ' APPLY DROPDOWN VALIDATION (Rows 1–100)
    '    '====================================
    '    Dim dvHelper As New HSSFDataValidationHelper(sheet)

    '    ' Supplier Dropdown (Column 1)
    '    Dim supplierRange As New CellRangeAddressList(1, 100, 1, 1)
    '    Dim supplierValidation = dvHelper.CreateValidation(
    '    DVConstraint.CreateFormulaListConstraint("SupplierList"),
    '    supplierRange)
    '    sheet.AddValidationData(supplierValidation)

    '    ' Chemical Dropdown (Column 2)
    '    Dim chemicalRange As New CellRangeAddressList(1, 100, 2, 2)
    '    Dim chemicalValidation = dvHelper.CreateValidation(
    '    DVConstraint.CreateFormulaListConstraint("ChemicalList"),
    '    chemicalRange)
    '    sheet.AddValidationData(chemicalValidation)

    '    ' Vendor Dropdown (Column 3)
    '    Dim vendorRange As New CellRangeAddressList(1, 100, 3, 3)
    '    Dim vendorValidation = dvHelper.CreateValidation(
    '    DVConstraint.CreateFormulaListConstraint("VendorList"),
    '    vendorRange)
    '    sheet.AddValidationData(vendorValidation)

    '    '====================================
    '    ' SAVE FILE
    '    '====================================
    '    Dim path As String = Server.MapPath("~/Excel_Reports/")
    '    If Not Directory.Exists(path) Then
    '        Directory.CreateDirectory(path)
    '    End If

    '    Dim fileName As String = "Supplier_Details_" &
    '                         DateTime.Now.ToString("ddMMyyyyHHmmss") & ".xls"

    '    Using fs As New FileStream(path & fileName, FileMode.Create)
    '        workbook.Write(fs)
    '    End Using

    '    '====================================
    '    ' DOWNLOAD
    '    '====================================
    '    Response.Clear()
    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AppendHeader("content-disposition", "attachment;filename=" & fileName)
    '    Response.WriteFile(path & fileName)
    '    Response.End()

    'End Sub



    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)

        If Not fuExcel.HasFile Then

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select an Excel file.');", True)
            Exit Sub
        End If

        Dim ext As String = Path.GetExtension(fuExcel.FileName).ToLower()
        If ext <> ".xlsx" AndAlso ext <> ".xls" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Only Excel files allowed.');", True)
            Exit Sub
        End If

        Dim uploadDir As String = Server.MapPath("~/Uploads/")
        If Not Directory.Exists(uploadDir) Then
            Directory.CreateDirectory(uploadDir)
        End If

        Dim filePath As String =
            uploadDir & Guid.NewGuid().ToString() & ext

        fuExcel.SaveAs(filePath)

        Dim dt As DataTable = ImportExcelWithIDs(filePath)

        If dt.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('No valid data found.');", True)
            Exit Sub
        End If

        ' Validate Billed Qty: must be numeric and > 0, strings not allowed
        Dim invalidRows As New List(Of Integer)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim billedVal As Decimal
            Dim rawQty As String = Convert.ToString(dt.Rows(i)("Billed Qty")).Trim()
            If String.IsNullOrEmpty(rawQty) OrElse Not Decimal.TryParse(rawQty, billedVal) OrElse billedVal <= 0 Then
                invalidRows.Add(i + 2) ' +2 = Excel row (1-based + header)
            End If
        Next

        If invalidRows.Count > 0 Then
            Dim rowList As String = String.Join(", ", invalidRows)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert",
                "alert('Billed Qty must be a valid number greater than 0. Invalid rows: " & rowList & "');", True)
            Exit Sub
        End If

        SubmitBulkData(dt)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Excel uploaded successfully');", True)
        ' lblMsg.Text = "Excel uploaded successfully"

        File.Delete(filePath)
    End Sub

    Public Shared Function ImportExcelWithIDs(filePath As String) As DataTable

        Dim dt As New DataTable()
        dt.Columns.Add("SupplierID", GetType(Integer))
        dt.Columns.Add("ChemicalID", GetType(Integer))
        dt.Columns.Add("VendorID", GetType(String))
        dt.Columns.Add("Billed Qty", GetType(String))

        Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)

            Dim workbook As New HSSFWorkbook(fs) ' For .xls
            Dim hiddenSheet As ISheet = workbook.GetSheet("HiddenSheet")

            Dim supplierDict As New Dictionary(Of String, Integer)
            Dim chemicalDict As New Dictionary(Of String, Integer)
            Dim vendorDict As New Dictionary(Of String, String)

            ' =========================
            ' READ HIDDEN SHEET
            ' =========================
            Dim sectionIndex As Integer = 0  ' 0=Supplier, 1=Chemical, 2=Vendor

            For i As Integer = 0 To hiddenSheet.LastRowNum

                Dim row = hiddenSheet.GetRow(i)

                ' If row is blank → move to next section
                If row Is Nothing OrElse row.GetCell(0) Is Nothing _
                OrElse String.IsNullOrWhiteSpace(row.GetCell(0).ToString()) Then

                    sectionIndex += 1
                    Continue For
                End If

                Dim name As String = row.GetCell(0).ToString().Trim()
                Dim id As String = row.GetCell(1).ToString().Trim()

                Select Case sectionIndex

                    Case 0 ' Supplier
                        supplierDict(name) = Convert.ToInt32(id)

                    Case 2 ' Chemical
                        chemicalDict(name) = Convert.ToInt32(id)

                    Case 4 ' Vendor
                        vendorDict(name) = id

                End Select

            Next
            'For i As Integer = 0 To hiddenSheet.LastRowNum

            '    Dim row = hiddenSheet.GetRow(i)
            '    If row Is Nothing Then Continue For

            '    Dim nameCell = row.GetCell(0)
            '    Dim idCell = row.GetCell(1)

            '    If nameCell Is Nothing OrElse idCell Is Nothing Then Continue For

            '    Dim name As String = nameCell.ToString().Trim()
            '    Dim id As String = idCell.ToString().Trim()

            '    If String.IsNullOrEmpty(name) Then Continue For

            '    ' Supplier section (Row 0–3)
            '    If i <= 3 Then
            '        supplierDict(name) = Convert.ToInt32(id)

            '        ' Chemical section (Row 6–8)
            '    ElseIf i >= 6 AndAlso i <= 8 Then
            '        chemicalDict(name) = Convert.ToInt32(id)

            '        ' Vendor section (Row >= 11)
            '    ElseIf i >= 11 Then
            '        vendorDict(name) = id
            '    End If

            'Next

            ' =========================
            ' READ MAIN SHEET
            ' =========================
            Dim sheet As ISheet = workbook.GetSheet("SupplierDetails")

            For rowIndex As Integer = 1 To sheet.LastRowNum

                Dim row = sheet.GetRow(rowIndex)
                If row Is Nothing Then Continue For

                Dim supplierName As String = GetCellValue(row.GetCell(0))
                Dim chemicalName As String = GetCellValue(row.GetCell(1))
                Dim vendorName As String = GetCellValue(row.GetCell(2))
                Dim billedqty As String = GetCellValue(row.GetCell(3))

                If supplierDict.ContainsKey(supplierName) AndAlso
                   chemicalDict.ContainsKey(chemicalName) AndAlso
                   vendorDict.ContainsKey(vendorName) Then

                    dt.Rows.Add(
                        supplierDict(supplierName),
                        chemicalDict(chemicalName),
                        vendorDict(vendorName),
                        billedqty
                    )

                End If

            Next

        End Using

        Return dt

    End Function


    Private Shared Function GetCellValue(cell As ICell) As String

        If cell Is Nothing Then Return ""

        Select Case cell.CellType
            Case CellType.String
                Return cell.StringCellValue.Trim()
            Case CellType.Numeric
                Return cell.NumericCellValue.ToString().Trim()
            Case CellType.Formula
                Return cell.ToString().Trim()
            Case Else
                Return ""
        End Select

    End Function

    Private Sub SubmitBulkData(ByVal dt As DataTable)
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New QualityControlClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            RecordInserted = obj.InsertSupplierDetails(ddlQuarter.SelectedValue, userInfo.userIDEntity, dt)
            If (RecordInserted > 0) Then
                sqlTrans.Commit()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');", True)
            Else
                sqlTrans.Rollback()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
            End If
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
    End Sub



    'Private Function ReadExcelUsingNPOI(filePath As String) As DataTable

    '    Dim dt As New DataTable
    '    dt.Columns.Add("Supplier", GetType(String))
    '    dt.Columns.Add("Chemical", GetType(String))
    '    dt.Columns.Add("Vendor", GetType(String))
    '    dt.Columns.Add("Billed Qty", GetType(String))

    '    Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
    '        Dim wb As IWorkbook

    '        If Path.GetExtension(filePath).ToLower() = ".xlsx" Then
    '            wb = New XSSFWorkbook(fs)
    '        Else
    '            wb = New HSSFWorkbook(fs)
    '        End If

    '        Dim sheet As ISheet = wb.GetSheetAt(0)

    '        ' Header validation
    '        Dim header = sheet.GetRow(0)
    '        If header Is Nothing OrElse
    '           header.GetCell(0).ToString() <> "Supplier" OrElse
    '           header.GetCell(1).ToString() <> "Chemical" Then

    '            Throw New Exception("Invalid Excel format")
    '        End If

    '        For rowIndex As Integer = 1 To sheet.LastRowNum
    '            Dim row = sheet.GetRow(rowIndex)
    '            If row Is Nothing Then Continue For

    '            Dim name As String = row.GetCell(0).ToString().Trim()
    '            Dim scoreCell = row.GetCell(1)

    '            If String.IsNullOrEmpty(name) Then Continue For
    '            If scoreCell Is Nothing OrElse scoreCell.CellType <> CellType.Numeric Then Continue For

    '            dt.Rows.Add(name, Convert.ToInt32(scoreCell.NumericCellValue))
    '        Next
    '    End Using

    '    Return dt

    'End Function

End Class
