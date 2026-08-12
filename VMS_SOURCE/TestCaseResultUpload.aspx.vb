Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports ClosedXML.Excel
Imports System.Data.OleDb
Imports NPOI.SS.Util
Imports AjaxControlToolkit
Imports System.Globalization
Imports NPOI.SS.Formula.Functions

Partial Class TestCaseResultUpload
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            AddAttributes()
            PopulateVendor()
            'PopulateVendorBrand(String.Empty)
        End If

    End Sub

#End Region

#Region "Adding Attributes To Controls"
    Public Sub AddAttributes()

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

#Region "Populate Dropdown"
    Private Sub PopulateVendor()
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet
            ddlVendor.Items.Clear()
            dsUnitSet = obj.GetVendor(userInfo.userIDEntity)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendor.DataSource = dsUnitSet.Tables(0)
                ddlVendor.DataTextField = "vendor_name"
                ddlVendor.DataValueField = "vendor_code"
                ddlVendor.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
                ddlVendor_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
    Private Sub PopulateVendorBrand(ByVal vendorCode As String)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetVendorBrand(vendorCode, userInfo.userIDEntity)
            ddlBrand.Items.Clear()
            ddlProduct.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlBrand.DataSource = dsUnitSet.Tables(0)
                ddlBrand.DataTextField = "brand_name"
                ddlBrand.DataValueField = "brand_id"
                ddlBrand.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                ElseIf (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    PopulateVendorBrandProduct(ddlVendor.SelectedValue, ddlBrand.SelectedValue)
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub

    Private Sub PopulateVendorBrandProduct(ByVal vendorCode As String, ByVal brandId As String)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetVendorBrandProduct(vendorCode, brandId, userInfo.userIDEntity)
            ddlProduct.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlProduct.DataSource = dsUnitSet.Tables(0)
                ddlProduct.DataTextField = "prd_desc"
                ddlProduct.DataValueField = "prd_code"
                ddlProduct.DataBind()
                ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                If (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlProduct_SelectedIndexChanged(ddlProduct, New EventArgs)
                End If

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub

    Private Sub CheckExportMsg(ByVal vendorCode As String, ByVal brandId As String, ByVal productcode As String)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.CheckExportMsg(vendorCode, brandId, productcode)



            If (dsUnitSet.Tables(0).Rows(0)("msg") <> "") Then
                Dim s As String = dsUnitSet.Tables(0).Rows(0)("msg").ToString
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" + s + "');", True)
            Else

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub

#End Region

#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsProductSet As New DataSet
            Dim RequisitionId As Integer = 0
            'dsProductSet = obj.GetTestList(ddlVendor.SelectedValue, ddlBrand.SelectedValue, txtTestName.Text.Trim(), userInfo.userIDEntity)

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvTestList.DataSource = dsProductSet.Tables(0)
                gvTestList.DataBind()
            Else
                gvTestList.DataSource = Nothing
                gvTestList.DataBind()
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTestList.PageIndexChanging
        gvTestList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    Protected Sub gvTokenVendorList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTestList.RowCommand
        CheckLogin()
        If (e.CommandName.Equals("EditTest")) Then
            Response.Redirect("TestCaseTestMasterAddUpdate.aspx?id=" & e.CommandArgument.ToString)
        End If
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        DownloadQCForm()
    End Sub
    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
        PopulateVendorBrand(ddlVendor.SelectedValue)
    End Sub

    Protected Sub ddlBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBrand.SelectedIndexChanged

        PopulateVendorBrandProduct(ddlVendor.SelectedValue, ddlBrand.SelectedValue)

        'CheckExportMsg(ddlVendor.SelectedValue, ddlBrand.SelectedValue, ddlProduct.SelectedValue)
    End Sub
    Private Sub DownloadQCForm()
        CheckLogin()
        Dim VendorCode As String = ddlVendor.SelectedValue
        Dim BrandId As Int64 = Val(ddlBrand.SelectedValue)
        Dim ProductCode As String = ddlProduct.SelectedValue

        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetQcFormData(VendorCode, Val(ddlBrand.SelectedValue), userInfo.userIDEntity, ProductCode)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ExportToExcel(dsUnitSet)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub
    Private Sub ExportToExcel(ds As DataSet)

        If ds.Tables.Count > 2 AndAlso (ds.Tables(0).Rows.Count > 0) Then

            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\QC_Upload_Template.xls", FileMode.Open, FileAccess.Read)

            Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)
            Dim ReportSheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")
            'Dim templateWorkbook1 As HSSFWorkbook = New HSSFWorkbook(fs, True)
            Dim HiddenSheet As HSSFSheet = templateWorkbook.GetSheet("HiddenSheet")
            ' Create named range for dropdown list
            templateWorkbook.SetSheetHidden(templateWorkbook.GetSheetIndex(HiddenSheet), SheetState.Hidden)

            Dim optionlength = 3

            Dim font1 As IFont = templateWorkbook.CreateFont()
            font1.Color = NPOI.HSSF.Util.HSSFColor.Black.Index
            font1.FontName = "Calibri"
            font1.FontHeightInPoints = 10
            font1.Boldweight = True

            Dim font3 As IFont = templateWorkbook.CreateFont()
            font3.Color = NPOI.HSSF.Util.HSSFColor.Black.Index
            font3.FontName = "Calibri"
            font3.FontHeightInPoints = 10

            'Dim style1 As ICellStyle = templateWorkbook.CreateCellStyle()
            'style1.Alignment = HorizontalAlignment.RIGHT
            'style1.SetFont(font3)
            'Dim currency As Short = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")
            'style1.DataFormat = currency


            Dim styleH1Center As ICellStyle = templateWorkbook.CreateCellStyle()
            styleH1Center.VerticalAlignment = VerticalAlignment.Center
            styleH1Center.Alignment = HorizontalAlignment.Center
            styleH1Center.SetFont(font1)
            styleH1Center.BorderRight = BorderStyle.Thin
            styleH1Center.BorderBottom = BorderStyle.Thin
            styleH1Center.BorderTop = BorderStyle.Thin
            styleH1Center.BorderLeft = BorderStyle.Thin
            styleH1Center.WrapText = True
            styleH1Center.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Gold.Index
            styleH1Center.FillPattern = FillPattern.SolidForeground

            Dim styleH2Center As ICellStyle = templateWorkbook.CreateCellStyle()
            styleH2Center.VerticalAlignment = VerticalAlignment.Center
            styleH2Center.Alignment = HorizontalAlignment.Center
            styleH2Center.SetFont(font1)
            styleH2Center.BorderRight = BorderStyle.Thin
            styleH2Center.BorderBottom = BorderStyle.Thin
            styleH2Center.BorderTop = BorderStyle.Thin
            styleH2Center.BorderLeft = BorderStyle.Thin
            styleH2Center.WrapText = True
            styleH2Center.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            styleH2Center.FillPattern = FillPattern.SolidForeground


            Dim styleH3Center As ICellStyle = templateWorkbook.CreateCellStyle()
            styleH3Center.VerticalAlignment = VerticalAlignment.Center
            styleH3Center.Alignment = HorizontalAlignment.Center
            styleH3Center.SetFont(font3)
            styleH3Center.BorderRight = BorderStyle.Thin
            styleH3Center.BorderBottom = BorderStyle.Thin
            styleH3Center.BorderTop = BorderStyle.Thin
            styleH3Center.BorderLeft = BorderStyle.Thin
            styleH3Center.WrapText = True
            styleH3Center.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index
            styleH3Center.FillPattern = FillPattern.SolidForeground

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

            Dim styleHLeft As ICellStyle = templateWorkbook.CreateCellStyle()
            styleHLeft.VerticalAlignment = VerticalAlignment.Center
            styleHLeft.Alignment = HorizontalAlignment.Left
            styleHLeft.SetFont(font1)
            styleHLeft.BorderRight = BorderStyle.Thin
            styleHLeft.BorderBottom = BorderStyle.Thin
            styleHLeft.BorderTop = BorderStyle.Thin
            styleHLeft.BorderLeft = BorderStyle.Thin
            styleHLeft.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index
            styleHLeft.FillPattern = FillPattern.SolidForeground


            Dim styleLeftError As ICellStyle = templateWorkbook.CreateCellStyle()
            styleLeftError.VerticalAlignment = VerticalAlignment.Center
            styleLeftError.Alignment = HorizontalAlignment.Left
            styleLeftError.SetFont(font3)
            styleLeftError.BorderRight = BorderStyle.Thin
            styleLeftError.BorderBottom = BorderStyle.Thin
            styleLeftError.BorderTop = BorderStyle.Thin
            styleLeftError.BorderLeft = BorderStyle.Thin
            styleLeftError.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BlueGrey.Red.Index

            Dim styleRight As ICellStyle = templateWorkbook.CreateCellStyle()
            styleRight.VerticalAlignment = VerticalAlignment.Center
            styleRight.Alignment = HorizontalAlignment.Right
            styleRight.SetFont(font3)
            styleRight.BorderRight = BorderStyle.Thin
            styleRight.BorderBottom = BorderStyle.Thin
            styleRight.BorderTop = BorderStyle.Thin
            styleRight.BorderLeft = BorderStyle.Thin

            'Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
            'styleValue.SetFont(font3)
            'styleValue.BorderRight = BorderStyle.THIN
            'styleValue.BorderBottom = BorderStyle.THIN
            'styleValue.BorderTop = BorderStyle.THIN
            'styleValue.BorderLeft = BorderStyle.THIN
            'styleValue.VerticalAlignment = VerticalAlignment.CENTER
            'styleValue.Alignment = HorizontalAlignment.RIGHT
            'styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")

            Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValue.BorderRight = BorderStyle.Thin
            styleValue.BorderBottom = BorderStyle.Thin
            styleValue.BorderTop = BorderStyle.Thin
            styleValue.BorderLeft = BorderStyle.Thin
            styleValue.VerticalAlignment = VerticalAlignment.Center
            styleValue.Alignment = HorizontalAlignment.Right
            styleValue.SetFont(font3)
            'Dim currency As Short = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")
            'styleValue.DataFormat = currency

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

            Dim cnt_test As Int32 = ds.Tables(1).Columns.Count
            Dim Row As HSSFRow
            Dim Cell As HSSFCell
            Dim CellHeader As HSSFCell

            Dim RowHidden As HSSFRow
            Dim CellHidden As HSSFCell

            ReportSheet.AddMergedRegion(New CellRangeAddress(0, 0, 0, (3 + cnt_test)))
            Row = ReportSheet.GetRow(0)
            CellHeader = Row.GetCell(0)
            CellHeader.SetCellValue("UNIT : " & ds.Tables(0).Rows(0)("vendor_name").ToString().ToUpper())
            CellHeader.CellStyle = styleHLeft

            ReportSheet.AddMergedRegion(New CellRangeAddress(1, 1, 0, (3 + cnt_test)))
            Row = ReportSheet.GetRow(1)
            Cell = Row.GetCell(0)
            Cell.SetCellValue("Brand : " & ds.Tables(0).Rows(0)("brand_name").ToString())
            Cell.CellStyle = styleLeft

            RowHidden = HiddenSheet.CreateRow(0)
            CellHidden = RowHidden.CreateCell(0)
            CellHidden.SetCellValue(ddlVendor.SelectedValue.ToString() & "|" & ddlBrand.SelectedValue.ToString() & "|" & ddlProduct.SelectedValue.ToString())
            CellHidden.CellStyle = styleLeft

            RowHidden = HiddenSheet.CreateRow(1)
            CellHidden = RowHidden.CreateCell(0)
            CellHidden.SetCellValue(ddlBrand.SelectedValue)
            CellHidden.CellStyle = styleLeft

            For i As Int32 = 0 To cnt_test - 1
                ReportSheet.SetColumnWidth(4 + i, 5000)

                Row = ReportSheet.GetRow(2)
                Dim CellTestName As ICell = Row.CreateCell(4 + i)
                CellTestName.SetCellValue(ds.Tables(1).Rows(0)(i).ToString())
                CellTestName.CellStyle = styleH1Center

                Row = ReportSheet.GetRow(3)
                Dim CellTestId As ICell = Row.CreateCell(4 + i)
                CellTestId.SetCellValue(ds.Tables(1).Columns(i).ToString())
                CellTestId.CellStyle = styleCenter
                Row.ZeroHeight = True

                Row = ReportSheet.GetRow(4)
                Dim CellFrequency As ICell = Row.CreateCell(4 + i)
                CellFrequency.SetCellValue(ds.Tables(1).Rows(1)(i).ToString())
                CellFrequency.CellStyle = styleH2Center

                Row = ReportSheet.GetRow(5)
                Dim CellRefVal As ICell = Row.CreateCell(4 + i)
                CellRefVal.SetCellValue(ds.Tables(1).Rows(2)(i).ToString())
                CellRefVal.CellStyle = styleH3Center

                If ds.Tables(1).Rows(3)(i).ToString() = "TT02" Then

                    Dim strOptions = ds.Tables(1).Rows(2)(i).ToString().Split({"/"c})
                    For j As Int32 = 0 To strOptions.Length - 1
                        Dim hiddenRow As IRow = HiddenSheet.CreateRow(optionlength + j)
                        Dim hiddenCell As ICell = hiddenRow.CreateCell(0)
                        hiddenCell.SetCellValue(strOptions(j).ToString())
                    Next
                    Dim name1 As IName = templateWorkbook.CreateName()
                    name1.NameName = "DropdownOptions_" + (4 + i).ToString()
                    name1.RefersToFormula = "HiddenSheet!$A$" & (optionlength + 1).ToString() & ":$A$" & (optionlength + strOptions.Length).ToString()
                    optionlength = optionlength + strOptions.Length + 1

                End If

            Next

            '' Create a hidden sheet to store dropdown list items (optional)
            'For r As Integer = 0 To ds.Tables(2).Rows.Count - 1
            '    Dim hiddenRow As IRow = HiddenSheet.CreateRow(optionlength + r)
            '    Dim hiddenCell As ICell = hiddenRow.CreateCell(0)
            '    hiddenCell.SetCellValue(ds.Tables(2).Rows(r)("prd_desc").ToString())
            'Next

            'Dim name As IName = templateWorkbook.CreateName()
            'name.NameName = "DropdownProducts"
            'name.RefersToFormula = "HiddenSheet!$A$" & (optionlength + 1) & ":$A$" & optionlength + ds.Tables(2).Rows.Count
            'optionlength = optionlength + ds.Tables(2).Rows.Count + 1

            Dim SheetRowIndex As Integer = 6
            Dim colIndex As Integer = 0
            For i As Int32 = 0 To 56 - 1
                Row = ReportSheet.CreateRow(SheetRowIndex)
                colIndex = 0

                Dim productValue As String = ds.Tables(2).Rows(0)("prd_desc").ToString()

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue(productValue)
                Cell.CellStyle = styleLeft

                ' Create data validation for the specific cell
                Dim dvHelper As HSSFDataValidationHelper = New HSSFDataValidationHelper(ReportSheet)
                Dim cellRangeAddress As New NPOI.SS.Util.CellRangeAddressList(SheetRowIndex, SheetRowIndex, colIndex, colIndex)
                Dim dvConstraint As HSSFDataValidation = dvHelper.CreateValidation(dvHelper.CreateFormulaListConstraint("""" & productValue & """"), cellRangeAddress)
                'Dim dvConstraint As HSSFDataValidation = dvHelper.CreateValidation(dvHelper.CreateFormulaListConstraint("DropdownProducts"), cellRangeAddress)
                dvConstraint.ShowErrorBox = True
                ReportSheet.AddValidationData(dvConstraint)

                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue("")
                Cell.CellStyle = styleLeft
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue("")
                Cell.CellStyle = styleLeft
                colIndex += 1

                Cell = Row.CreateCell(colIndex)
                Cell.SetCellValue("")
                Cell.CellStyle = styleDate
                Cell.CellStyle.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("@")
                colIndex += 1

                For j As Int32 = 0 To cnt_test - 1

                    Cell = Row.CreateCell(colIndex + j)

                    If ds.Tables(1).Rows(3)(j) = "TT01" Then
                        Cell.SetCellValue("")
                        Cell.CellStyle = styleValue
                    ElseIf ds.Tables(1).Rows(3)(j) = "TT02" Then
                        Cell.SetCellValue("Select an option")
                        Cell.CellStyle = styleLeft

                        ' Create data validation for the specific cell
                        Dim dvHelper1 As HSSFDataValidationHelper = New HSSFDataValidationHelper(ReportSheet)
                        Dim cellRangeAddress1 As New NPOI.SS.Util.CellRangeAddressList(SheetRowIndex, SheetRowIndex, colIndex + j, colIndex + j)
                        Dim dvConstraint1 As HSSFDataValidation = dvHelper1.CreateValidation(dvHelper1.CreateFormulaListConstraint("DropdownOptions_" + (colIndex + j).ToString()), cellRangeAddress1)
                        dvConstraint1.ShowErrorBox = True
                        ReportSheet.AddValidationData(dvConstraint1)

                    Else
                        Cell.SetCellValue("")
                        Cell.CellStyle = styleLeft
                    End If
                Next
                colIndex += 1



                SheetRowIndex += 1
            Next

            For i As Integer = 56 To ReportSheet.LastRowNum
                Dim row1 As IRow = ReportSheet.GetRow(i)
                If row1 IsNot Nothing Then
                    row1.ZeroHeight = True
                End If
            Next

            'Dim lastColumnIndex As Integer = ReportSheet.GetRow(0).LastCellNum - 1 ' Get the last column index (0-based)
            'For col As Integer = 7 To lastColumnIndex
            '    ReportSheet.SetColumnWidth(col, 0) ' Set column width to 0 to hide it
            'Next

            'If ds.Tables.Count > 3 AndAlso Not ds.Tables(4) Is Nothing AndAlso ds.Tables(4).Rows.Count > 0 Then
            '    Dim dtBatch As DataTable = ds.Tables(4).DefaultView.ToTable(True, "batch_no")

            '    SheetRowIndex = 6
            '    colIndex = 0
            '    For i As Int32 = 0 To dtBatch.Rows.Count - 1

            '        Row = ReportSheet.GetRow(SheetRowIndex)
            '        colIndex = 0

            '        Dim batchNo As String = dtBatch.Rows(i)(0).ToString()  ' Assuming batch_no is a string, adjust data type if necessary
            '        Dim dtTable As DataTable = ds.Tables(4).Select("batch_no = '" & batchNo & "'").CopyToDataTable()

            '        Cell = Row.GetCell(colIndex)
            '        Cell.SetCellValue(dtTable.Rows(0)("product_name").ToString())
            '        Cell.CellStyle = styleLeftError
            '        colIndex += 1

            '        Cell = Row.GetCell(colIndex)
            '        Cell.SetCellValue(dtTable.Rows(0)("shade_code").ToString())
            '        Cell.CellStyle = styleLeftError
            '        colIndex += 1

            '        Cell = Row.GetCell(colIndex)
            '        Cell.SetCellValue(dtTable.Rows(0)("batch_no").ToString())
            '        Cell.CellStyle = styleLeftError
            '        colIndex += 1

            '        Cell = Row.GetCell(colIndex)
            '        Cell.SetCellValue(dtTable.Rows(0)("batch_date").ToString())
            '        Cell.CellStyle = styleLeftError
            '        colIndex += 1

            '        For j As Int32 = 0 To dtTable.Rows.Count - 1

            '            For tindx = 0 To cnt_test - 1
            '                Dim TestId As String = ReportSheet.GetRow(3).GetCell(tindx + 4).ToString()
            '                If TestId = dtTable.Rows(j)("test_name") Then
            '                    Cell = Row.GetCell(colIndex)
            '                    Cell.SetCellValue(dtTable.Rows(j)("test_name").ToString())
            '                    Cell.CellStyle = styleLeft

            '                    If dtTable.Rows(j)("valid_yn").ToString() = "N" Then

            '                        Cell = Row.CreateCell(cnt_test + 3 + 1)
            '                        Cell.SetCellValue(dtTable.Rows(j)("remarks").ToString())
            '                        Cell.CellStyle = styleLeft
            '                        'colIndex += 1

            '                    End If

            '                    Exit For
            '                End If
            '            Next

            '            colIndex += 1
            '        Next


            '        SheetRowIndex += 1
            '    Next

            'End If


            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If

            Dim DateString As String = "_" & Format(DateTime.Now, "dd-MM-yyyy_HH-mm-ss")
            Dim file_name As String = "QC_Form_" + DateString + ".xls"

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

            'Response.End()

        End If
    End Sub

    Protected Sub ImportExcel(sender As Object, e As EventArgs) Handles btnUpload.Click

        divFileError.Visible = False
        hdnErrorFilePath.Value = String.Empty
        ViewState("dtErrorFileData") = Nothing

        Try

            Dim DOC_ABS_PATH As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH")
            Dim FolderPath As String = "QC_Form_Excel/"
            Dim FileName As String = "QC_TestReport_" & DateTime.UtcNow.ToString("yyyyMMdd") & "_" & Guid.NewGuid().ToString() & Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim fileLocation As String = FolderPath & FileName
            If Not (Directory.Exists(DOC_ABS_PATH & FolderPath)) Then
                Directory.CreateDirectory(DOC_ABS_PATH & FolderPath)
            End If
            Dim fileInfo = New FileInfo(DOC_ABS_PATH & fileLocation)
            Dim fileExtension As String = fileInfo.Extension
            FileUpload1.SaveAs(DOC_ABS_PATH & fileLocation)


            If String.IsNullOrEmpty(ddlVendor.SelectedValue.ToString()) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor.');", True)
                Exit Sub
            End If
            If Val(ddlBrand.SelectedValue.ToString()) = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a brand.');", True)
                Exit Sub
            End If

            ddlVendor.Enabled = False
            ddlBrand.Enabled = False
            ddlProduct.Enabled = False

            If fileExtension <> ".xls" AndAlso fileExtension <> ".xlsx" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid file. Please choose .xls or .xlsx file.');", True)
                Return
            End If

            Dim strConn As String = ""

            Select Case fileExtension
                Case ".xls"
                    'strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & DOC_ABS_PATH & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                Case ".xlsx"
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & DOC_ABS_PATH & fileLocation & ";Extended Properties=""Excel 12.0 xml;HDR=Yes;IMEX=1"""
            End Select

            BindData(strConn, fileLocation)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid file. Please choose valid file.');", True)
        End Try
    End Sub
    Private Sub BindData(ByVal strConn As String, ByVal filePath As String)
        CheckLogin()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim objOCSpecificationclass As New OCSpecification
        Dim Auto_Id As Integer = 0
        Dim Created_user As String = userInfo.userIDEntity

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Try

            Dim dt_hidden_sheet As DataTable = Nothing
            dt_hidden_sheet = getSheetData(strConn, "HiddenSheet$")
            Dim VendorId As String
            Dim BrandId As Int64
            Dim ProductCode As String
            Dim strVB = dt_hidden_sheet.Columns(0).ToString().Split({"|"c})
            VendorId = strVB(0).ToString()
            BrandId = Val(strVB(1).ToString())
            ProductCode = strVB(2).ToString()
            If Not VendorId = ddlVendor.SelectedValue.ToString() Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid vendor selection. \nPlease download new file for this vendor.');", True)
                Exit Sub
            End If
            If Not BrandId = Val(ddlBrand.SelectedValue.ToString()) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid brand selection. \nPlease download new file for this brand.');", True)
                Exit Sub
            End If
            If Not ProductCode = ddlProduct.SelectedValue.ToString() Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid product selection. \nPlease download new file for this product.');", True)
                Exit Sub
            End If

            Dim dt_sheet As DataTable = Nothing
            dt_sheet = getSheetData(strConn, "Sheet1$")

            Dim dt As New DataTable
            dt.Columns.Add("slno")
            dt.Columns.Add("brand_name")
            dt.Columns.Add("product_name")
            dt.Columns.Add("shade_code")
            dt.Columns.Add("batch_no")
            dt.Columns.Add("batch_date")
            dt.Columns.Add("test_name")
            dt.Columns.Add("result_value")

            'Dim brand_name As String = dt_sheet.Rows(0)(1).ToString()
            'Dim product_name As String = dt_sheet.Rows(0)(3).ToString()
            'Dim shade_code As String = dt_sheet.Rows(0)(6).ToString()
            'Dim slno As String = Val(dt_sheet.Rows(1)(0).ToString())
            'Dim batch_no As String = dt_sheet.Rows(1)(1).ToString()
            'Dim batch_date As String = dt_sheet.Rows(1)(3).ToString()

            For i As Integer = 5 To dt_sheet.Rows.Count - 1
                If Not String.IsNullOrEmpty(dt_sheet.Rows(i)(2).ToString()) Then
                    For j As Int32 = 1 To dt_sheet.Columns.Count - 1
                        If Not String.IsNullOrEmpty(dt_sheet.Rows(2)(j).ToString()) Then
                            Dim newRow As DataRow = dt.NewRow()
                            newRow("slno") = (i - 3)
                            'newRow("brand_name") = brand_name
                            newRow("product_name") = dt_sheet.Rows(i)(0).ToString.Trim()
                            newRow("shade_code") = dt_sheet.Rows(i)(1).ToString.Trim()
                            newRow("batch_no") = dt_sheet.Rows(i)(2).ToString.Trim()
                            'newRow("batch_date") = CType(dt_sheet.Rows(i)(3), DateTime).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                            Dim cellBatchDate = dt_sheet.Rows(i)(3)
                            If Not IsDBNull(cellBatchDate) Then
                                If TypeOf cellBatchDate Is DateTime Then
                                    newRow("batch_date") = CType(cellBatchDate, DateTime).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                ElseIf TypeOf cellBatchDate Is String Then
                                    newRow("batch_date") = cellBatchDate.ToString()
                                End If
                            Else
                                newRow("batch_date") = ""
                            End If
                            newRow("test_name") = dt_sheet.Rows(2)(j).ToString()
                            newRow("result_value") = dt_sheet.Rows(i)(j).ToString.Trim()
                            dt.Rows.Add(newRow)
                        End If
                    Next
                End If
            Next i
            dt.AcceptChanges()


            Dim obj As New QualityControlClass
            Dim dsProductSet As New DataSet
            dsProductSet = obj.UploadQcFormData(ddlVendor.SelectedValue, ddlBrand.SelectedValue, dt, userInfo.userIDEntity, filePath)

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then

                If dsProductSet.Tables.Count > 1 AndAlso dsProductSet.Tables(1).Rows(0)("valid_yn") = "N" Then
                    'ExportToExcel(dsProductSet)
                    hdnErrorFilePath.Value = filePath
                    ViewState("dtErrorFileData") = dsProductSet
                    divFileError.Visible = True
                    'ExportErrorExcel(dsProductSet, filePath)
                Else
                    SubmitBulkData(dt, filePath)
                End If

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid file. Please choose valid file.');", True)
            'Dim returnUrl As String = "~/ExceptionPage.aspx"
            'Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            'Server.Transfer(returnUrl)
        End Try

    End Sub
    Private Sub SubmitBulkData(ByVal dt As DataTable, ByVal filePath As String)
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New QualityControlClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            RecordInserted = obj.TestResultEntryBulkInsert(ddlVendor.SelectedValue, ddlBrand.SelectedValue, dt, userInfo.userIDEntity, filePath, sqlConn, sqlTrans)
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
    Private Sub ExportErrorExcel(ByVal ds As DataSet, ByVal filePath As String)

        If Not String.IsNullOrEmpty(filePath) Then

            Dim DOC_ABS_PATH As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH")
            Dim fs As FileStream = New FileStream(DOC_ABS_PATH & filePath, FileMode.Open, FileAccess.Read)
            Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)
            Dim ReportSheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")


            Dim font1 As IFont = templateWorkbook.CreateFont()
            font1.Color = NPOI.HSSF.Util.HSSFColor.Black.Index
            font1.FontName = "Calibri"
            font1.FontHeightInPoints = 10
            font1.Boldweight = True

            Dim font2 As IFont = templateWorkbook.CreateFont()
            font2.Color = NPOI.HSSF.Util.HSSFColor.Black.Index
            font2.FontName = "Calibri"
            font2.FontHeightInPoints = 10
            font2.Boldweight = False
            font2.Color = NPOI.HSSF.Util.HSSFColor.White.Index

            Dim styleLeft As ICellStyle = templateWorkbook.CreateCellStyle()
            styleLeft.VerticalAlignment = VerticalAlignment.Center
            styleLeft.Alignment = HorizontalAlignment.Left
            styleLeft.SetFont(font1)
            styleLeft.BorderRight = BorderStyle.Thin
            styleLeft.BorderBottom = BorderStyle.Thin
            styleLeft.BorderTop = BorderStyle.Thin
            styleLeft.BorderLeft = BorderStyle.Thin

            Dim styleLeftError As ICellStyle = templateWorkbook.CreateCellStyle()
            styleLeftError.VerticalAlignment = VerticalAlignment.Center
            styleLeftError.Alignment = HorizontalAlignment.Left
            styleLeftError.SetFont(font1)
            styleLeftError.BorderRight = BorderStyle.Thin
            styleLeftError.BorderBottom = BorderStyle.Thin
            styleLeftError.BorderTop = BorderStyle.Thin
            styleLeftError.BorderLeft = BorderStyle.Thin
            styleLeftError.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index
            styleLeftError.FillPattern = FillPattern.SolidForeground

            Dim style1LeftError As ICellStyle = templateWorkbook.CreateCellStyle()
            style1LeftError.VerticalAlignment = VerticalAlignment.Center
            style1LeftError.Alignment = HorizontalAlignment.Left
            style1LeftError.SetFont(font1)
            style1LeftError.BorderRight = BorderStyle.Thin
            style1LeftError.BorderBottom = BorderStyle.Thin
            style1LeftError.BorderTop = BorderStyle.Thin
            style1LeftError.BorderLeft = BorderStyle.Thin
            style1LeftError.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            style1LeftError.FillPattern = FillPattern.SolidForeground

            Dim style2LeftError As ICellStyle = templateWorkbook.CreateCellStyle()
            style2LeftError.VerticalAlignment = VerticalAlignment.Center
            style2LeftError.Alignment = HorizontalAlignment.Center
            style2LeftError.SetFont(font1)
            style2LeftError.BorderRight = BorderStyle.Thin
            style2LeftError.BorderBottom = BorderStyle.Thin
            style2LeftError.BorderTop = BorderStyle.Thin
            style2LeftError.BorderLeft = BorderStyle.Thin
            style2LeftError.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Gold.Index
            style2LeftError.FillPattern = FillPattern.SolidForeground

            Dim style3LeftError As ICellStyle = templateWorkbook.CreateCellStyle()
            style3LeftError.VerticalAlignment = VerticalAlignment.Center
            style3LeftError.Alignment = HorizontalAlignment.Left
            style3LeftError.SetFont(font1)
            style3LeftError.BorderRight = BorderStyle.Thin
            style3LeftError.BorderBottom = BorderStyle.Thin
            style3LeftError.BorderTop = BorderStyle.Thin
            style3LeftError.BorderLeft = BorderStyle.Thin
            style3LeftError.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index
            style3LeftError.FillPattern = FillPattern.SolidForeground

            Dim style4LeftError As ICellStyle = templateWorkbook.CreateCellStyle()
            style4LeftError.VerticalAlignment = VerticalAlignment.Center
            style4LeftError.Alignment = HorizontalAlignment.Left
            style4LeftError.SetFont(font2)
            style4LeftError.BorderRight = BorderStyle.Thin
            style4LeftError.BorderBottom = BorderStyle.Thin
            style4LeftError.BorderTop = BorderStyle.Thin
            style4LeftError.BorderLeft = BorderStyle.Thin
            style4LeftError.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index
            style4LeftError.FillPattern = FillPattern.SolidForeground

            Dim Row As HSSFRow
            Dim Cell As HSSFCell
            Dim SheetRowIndex As Integer = 0
            Dim colIndex As Integer = 0
            Dim cnt_test As Integer = Val(ds.Tables(0).Rows(0)("test_cnt")) + 4
            colIndex = Val(ds.Tables(0).Rows(0)("test_cnt")) + 4

            Row = ReportSheet.GetRow(0)
            Cell = Row.CreateCell(colIndex)
            Cell.CellStyle = styleLeftError

            Row = ReportSheet.GetRow(1)
            Cell = Row.CreateCell(colIndex)
            Cell.CellStyle = style1LeftError

            ReportSheet.AddMergedRegion(New CellRangeAddress(2, 5, (2 + cnt_test), (2 + cnt_test)))
            Row = ReportSheet.GetRow(2)
            Cell = Row.CreateCell(colIndex)
            Cell.SetCellValue("Reasons")
            Cell.CellStyle = style2LeftError

            Row = ReportSheet.GetRow(3)
            Cell = Row.CreateCell(colIndex)
            Cell.CellStyle = style2LeftError

            Row = ReportSheet.GetRow(4)
            Cell = Row.CreateCell(colIndex)
            Cell.CellStyle = style1LeftError

            Row = ReportSheet.GetRow(5)
            Cell = Row.CreateCell(colIndex)
            Cell.CellStyle = style3LeftError

            ReportSheet.SetColumnWidth(colIndex, 10000)

            SheetRowIndex = 6
            For i As Int32 = 0 To 56 - 1
                Try
                    Row = ReportSheet.GetRow(SheetRowIndex)
                    Cell = Row.CreateCell(colIndex)
                    Cell.CellStyle = styleLeft

                    Dim batchNo As String = Row.GetCell(2).ToString().Trim()

                    If ds.Tables(0).Select("batch_no = '" & batchNo & "'").Length > 0 Then
                        Dim dtTable As DataTable = ds.Tables(0).Select("batch_no = '" & batchNo & "'").CopyToDataTable()

                        If dtTable.Rows.Count > 0 AndAlso dtTable.Rows(0)("valid_yn").ToString() = "N" Then
                            Cell.SetCellValue(dtTable.Rows(0)("remarks").ToString())

                            For j As Int32 = 0 To Val(ds.Tables(0).Rows(0)("test_cnt")) + 4
                                Cell = Row.GetCell(j)
                                Cell.CellStyle = style4LeftError
                            Next

                        End If
                    End If
                Catch ex As Exception

                End Try

                SheetRowIndex += 1
            Next

            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If

            Dim DateString As String = "_" & Format(DateTime.Now, "dd-MM-yyyy_HH-mm-ss")
            Dim file_name As String = "QC_Form_" + DateString + ".xls"

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

            'Response.End()

        End If
    End Sub
    Private Function getSheetData(ByVal strConn As String, ByVal sheet As String) As DataTable
        Dim query As String = "select * from [" & sheet & "]"
        Dim dt As DataTable = New DataTable()
        Dim formats() As String = {"dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy"}
        Dim provider As CultureInfo = CultureInfo.InvariantCulture
        Dim parsedDate As DateTime

        Using conn As New OleDbConnection(strConn)
            conn.Open()

            Using cmd As New OleDbCommand(query, conn)
                Using reader As OleDbDataReader = cmd.ExecuteReader()

                    ' Build DataTable schema manually based on reader's inferred types
                    For i As Integer = 0 To reader.FieldCount - 1
                        Dim colName As String = reader.GetName(i)
                        Dim colType As Type = reader.GetFieldType(i)
                        If colType = GetType(DateTime) Then
                            dt.Columns.Add(colName, GetType(String))
                        Else
                            dt.Columns.Add(colName, colType)
                        End If
                    Next

                    While reader.Read()
                        Dim row As DataRow = dt.NewRow()
                        For i As Integer = 0 To reader.FieldCount - 1
                            If reader(i) IsNot DBNull.Value Then

                                If DateTime.TryParseExact(reader(i), formats, provider, DateTimeStyles.None, parsedDate) Then
                                    row(i) = parsedDate.ToString("dd/MM/yyyy")
                                Else
                                    row(i) = reader(i)
                                End If
                            Else
                                row(i) = DBNull.Value
                            End If
                        Next
                        dt.Rows.Add(row)
                    End While

                End Using
            End Using
        End Using

        Return dt

        'Dim objConn As OleDbConnection
        'Dim oleDA As OleDbDataAdapter
        'objConn = New OleDbConnection(strConn)
        'objConn.Open()
        'oleDA = New OleDbDataAdapter(query, objConn)
        'oleDA.Fill(dt)
        'objConn.Close()
        'oleDA.Dispose()
        'objConn.Dispose()
        Return dt
    End Function
    Protected Sub gvTestList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTestList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowview As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim pnlValid As Panel = CType(e.Row.FindControl("pnlValid"), Panel)
            Dim pnlInvalid As Panel = CType(e.Row.FindControl("pnlInvalid"), Panel)
            pnlValid.Visible = False
            pnlInvalid.Visible = False
            If rowview("valid_result_yn").ToString() <> "Y" Then
                e.Row.BackColor = Drawing.Color.Tomato
                pnlInvalid.Visible = True
            Else
                pnlValid.Visible = True
                btnSubmit.Visible = True
            End If
        End If
    End Sub

#Region "Date Format"
    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime

        If (stringdate.Equals(String.Empty)) Then
            Return SqlDateTime.MinValue
        End If

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
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ddlVendor.Enabled = True
        ddlBrand.Enabled = True
        ddlProduct.Enabled = True
    End Sub

    Protected Sub btnDownloadErrorFile_Click(sender As Object, e As EventArgs) Handles btnDownloadErrorFile.Click
        Try
            Dim ds As DataSet = CType(ViewState("dtErrorFileData"), DataSet)
            ExportErrorExcel(ds, hdnErrorFilePath.Value.ToString())
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim productCode As String = ddlProduct.SelectedValue
        If Not String.IsNullOrEmpty(productCode) Then
            CheckExportMsg(ddlVendor.SelectedValue, ddlBrand.SelectedValue, ddlProduct.SelectedValue)
        End If
    End Sub
End Class
