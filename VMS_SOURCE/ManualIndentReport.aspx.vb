Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports System.Globalization

Partial Class ManualIndentReport
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Request.QueryString("NoData") = "Yes") Then
            lblErrMsg.Text = "No record found!"
        Else
            lblErrMsg.Text = String.Empty
        End If

        CheckLogin()
        AddAttributes()
        If Not IsPostBack Then
            PopulateProcessYr()
            PopulateDepot()
            PopulateUnit()
        End If
    End Sub
    Private Sub AddAttributes()

        btnSubmit.OnClientClick = "return validate_yearmonth();"
    End Sub
#Region "Populate Process Year"
    Private Sub PopulateProcessYr()
        Dim ProcessYr As New Common
        Dim StandrdParams As New MonthlyUnitDespatch
        Dim YearSet As New DataSet
        Dim StandardYrMnth As New DataSet

        YearSet = ProcessYr.GetFinYrDetails(Constant.Common.Company, Constant.Common.ActiveStatus)
        StandardYrMnth = StandrdParams.GetMnthsYr(Constant.Common.ActiveStatus)
        If (Not (YearSet Is Nothing) AndAlso YearSet.Tables.Count > 0 AndAlso Not (YearSet.Tables(0) Is Nothing) AndAlso YearSet.Tables(0).Rows.Count > 0) Then
            ddlProcessYr.DataSource = YearSet.Tables(0)
            ddlProcessYr.DataTextField = "fin_year"
            ddlProcessYr.DataValueField = "fin_year"
            ddlProcessYr.DataBind()
        End If

        If (Not (StandardYrMnth Is Nothing) AndAlso StandardYrMnth.Tables.Count > 0 AndAlso Not (StandardYrMnth.Tables(0) Is Nothing) AndAlso StandardYrMnth.Tables(0).Rows.Count > 0) Then
            ddlProcessYr.SelectedValue = StandardYrMnth.Tables(0).Rows(0)("param_char_value")
            ddlProcessMnth.SelectedValue = StandardYrMnth.Tables(0).Rows(1)("param_char_value")
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
#Region "Populate Depot"
    Private Sub PopulateDepot()
        Dim GeDepot As New Common
        Dim DepotSet As New DataSet

        DepotSet = GeDepot.Getdepotname(String.Empty)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = DepotSet.Tables(0)
            ddlDepot.DataTextField = "depot_name"
            ddlDepot.DataValueField = "depot_code"
            ddlDepot.DataBind()
            ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
    End Sub
#End Region
#Region "Populate Unit"
    Private Sub PopulateUnit()
        Dim UnitDespatch As New MonthlyUnitDespatch
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, String.Empty)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If

    End Sub
#End Region
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        If ddlProcessYr.SelectedValue.Trim = String.Empty Then
            lblErrMsg.Text = "Please Select a Year."
            ddlDepot.Focus()
            Exit Sub
        End If

        If ddlProcessMnth.SelectedValue.Trim = String.Empty Then
            lblErrMsg.Text = "Please Select a Month."
            ddlDepot.Focus()
            Exit Sub
        End If

        Dim depot As String = ddlDepot.SelectedValue
        Dim unit As String = ddlUnit.SelectedValue
        Dim ProcessYr As String = ddlProcessYr.SelectedValue
        Dim ProcessMnth As String = ddlProcessMnth.SelectedValue

        Dim reportObj As New ManualIndentClass
        Dim ExcelSet As DataSet
        ExcelSet = reportObj.GetMonthlyIndentReport(unit, depot, ProcessYr, ProcessMnth)
        If (ExcelSet.Tables(0).Rows.Count > 0) Then
            ExportToExcelSheet(ExcelSet)
        End If
    End Sub
#Region "Export to Excel using Dll"
    Protected Sub ExportToExcelSheet(ByVal dset As DataSet)
        Try
            Dim datatime As String = System.DateTime.Now.Ticks
            'Opening the Excel template...
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\ManualIndentReport.xls", FileMode.Open, FileAccess.Read)
            'Getting the complete workbook...
            Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)
            'Getting the worksheet by its name...
            Dim sheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")
            Dim RowsIndex As Integer
            Dim DateString As String = (DateTime.Now.Ticks.ToString()).Substring(13) + "_" & Format(Now, "yyyyMMdd")
            'Styles

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

            Dim style2 As ICellStyle = templateWorkbook.CreateCellStyle()
            style2.VerticalAlignment = VerticalAlignment.CENTER
            'style2.Alignment = HorizontalAlignment.CENTER

            ''style2.FillPattern = FillPattern.SolidForeground
            ''style2.FillForegroundColor = HSSFColor.BLACK.index
            style2.SetFont(font1)

            Dim style3 As ICellStyle = templateWorkbook.CreateCellStyle()
            style3.VerticalAlignment = VerticalAlignment.CENTER
            style3.Alignment = HorizontalAlignment.CENTER

            ''style2.FillPattern = FillPattern.SolidForeground
            style3.FillForegroundColor = HSSFColor.RED.index
            style3.SetFont(font1)

            Dim style4 As ICellStyle = templateWorkbook.CreateCellStyle()
            style4.VerticalAlignment = VerticalAlignment.CENTER
            style4.Alignment = HorizontalAlignment.CENTER

            ''style2.FillPattern = FillPattern.SolidForeground
            style4.FillForegroundColor = HSSFColor.BLUE.index
            style4.SetFont(font1)

            Dim style5 As ICellStyle = templateWorkbook.CreateCellStyle()
            style5.VerticalAlignment = VerticalAlignment.CENTER
            style5.SetFont(font3)
            'style5.FillForegroundColor = HSSFColor.YELLOW.index
            style5.FillPattern = FillPattern.SolidForeground
            style5.BorderTop = BorderStyle.THIN
            style5.BorderRight = BorderStyle.THIN
            style5.BorderBottom = BorderStyle.THIN
            style5.BorderLeft = BorderStyle.THIN

            Dim stylecenter As ICellStyle = templateWorkbook.CreateCellStyle()
            stylecenter.VerticalAlignment = VerticalAlignment.CENTER
            stylecenter.Alignment = HorizontalAlignment.CENTER

            ''style2.FillPattern = FillPattern.SolidForeground
            ''style2.FillForegroundColor = HSSFColor.BLACK.index
            stylecenter.SetFont(font1)
            stylecenter.BorderTop = BorderStyle.THIN
            stylecenter.BorderRight = BorderStyle.THIN
            stylecenter.BorderBottom = BorderStyle.THIN
            stylecenter.BorderLeft = BorderStyle.THIN

            Dim style6 As ICellStyle = templateWorkbook.CreateCellStyle()
            style6.VerticalAlignment = VerticalAlignment.CENTER
            style6.Alignment = HorizontalAlignment.RIGHT

            Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValue.SetFont(font1)
            styleValue.VerticalAlignment = VerticalAlignment.CENTER
            styleValue.Alignment = HorizontalAlignment.RIGHT
            styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00 ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")

            ''style2.FillPattern = FillPattern.SolidForeground
            ''style2.FillForegroundColor = HSSFColor.BLACK.index
            style6.SetFont(font1)

            Dim style7 As ICellStyle = templateWorkbook.CreateCellStyle()
            style7.VerticalAlignment = VerticalAlignment.CENTER
            style7.Alignment = HorizontalAlignment.Right

            style7.FillPattern = FillPattern.SolidForeground
            style7.FillForegroundColor = HSSFColor.LightOrange.Index
            style7.SetFont(font2)

            Dim styleleft As ICellStyle = templateWorkbook.CreateCellStyle()
            styleleft.VerticalAlignment = VerticalAlignment.CENTER
            styleleft.Alignment = HorizontalAlignment.LEFT
            styleleft.SetFont(font2)

            styleleft.BorderTop = BorderStyle.THIN
            styleleft.BorderRight = BorderStyle.THIN
            styleleft.BorderBottom = BorderStyle.THIN
            styleleft.BorderLeft = BorderStyle.THIN

            Dim styleright As ICellStyle = templateWorkbook.CreateCellStyle()
            styleright.VerticalAlignment = VerticalAlignment.CENTER
            styleright.Alignment = HorizontalAlignment.RIGHT
            styleright.SetFont(font2)

            styleright.BorderTop = BorderStyle.THIN
            styleright.BorderRight = BorderStyle.THIN
            styleright.BorderBottom = BorderStyle.THIN
            styleright.BorderLeft = BorderStyle.THIN

            Dim style8 As ICellStyle = templateWorkbook.CreateCellStyle()
            style8.VerticalAlignment = VerticalAlignment.CENTER
            style8.Alignment = HorizontalAlignment.Right

            style8.FillPattern = FillPattern.SolidForeground
            style8.FillForegroundColor = HSSFColor.LightBlue.Index
            style8.SetFont(font2)

            Dim style10 As ICellStyle = templateWorkbook.CreateCellStyle()
            style10.VerticalAlignment = VerticalAlignment.CENTER
            style10.Alignment = HorizontalAlignment.Center

            style10.FillPattern = FillPattern.SolidForeground
            style10.FillForegroundColor = HSSFColor.LightBlue.Index
            style10.SetFont(font2)

            Dim style11 As ICellStyle = templateWorkbook.CreateCellStyle()
            style11.VerticalAlignment = VerticalAlignment.CENTER
            style11.Alignment = HorizontalAlignment.Right

            style11.FillPattern = FillPattern.SolidForeground
            style11.FillForegroundColor = HSSFColor.LightYellow.Index
            style11.SetFont(font2)

            Dim style12 As ICellStyle = templateWorkbook.CreateCellStyle()
            style12.VerticalAlignment = VerticalAlignment.CENTER
            style12.Alignment = HorizontalAlignment.LEFT

            Dim style33 As ICellStyle = templateWorkbook.CreateCellStyle()
            style33.VerticalAlignment = VerticalAlignment.CENTER
            style33.SetFont(font4)
            style33.FillForegroundColor = HSSFColor.AUTOMATIC.index
            style33.BorderTop = BorderStyle.THIN
            style33.BorderRight = BorderStyle.THIN
            style33.BorderBottom = BorderStyle.THIN
            style33.BorderLeft = BorderStyle.THIN

            Dim style35 As ICellStyle = templateWorkbook.CreateCellStyle()
            style35.VerticalAlignment = VerticalAlignment.CENTER
            style35.Alignment = HorizontalAlignment.CENTER
            style35.SetFont(font4)
            style35.FillForegroundColor = HSSFColor.AUTOMATIC.index

            Dim style36 As ICellStyle = templateWorkbook.CreateCellStyle()
            style36.VerticalAlignment = VerticalAlignment.CENTER
            style36.SetFont(font5)
            style36.FillForegroundColor = HSSFColor.AUTOMATIC.index
            style36.BorderTop = BorderStyle.THIN
            style36.BorderRight = BorderStyle.THIN
            style36.BorderBottom = BorderStyle.THIN
            style36.BorderLeft = BorderStyle.THIN

            Dim style37 As ICellStyle = templateWorkbook.CreateCellStyle()
            style37.VerticalAlignment = VerticalAlignment.CENTER
            style37.Alignment = HorizontalAlignment.CENTER
            style37.SetFont(font5)
            style37.FillForegroundColor = HSSFColor.AUTOMATIC.index
            style37.BorderTop = BorderStyle.THIN
            style37.BorderRight = BorderStyle.THIN
            style37.BorderBottom = BorderStyle.THIN
            style37.BorderLeft = BorderStyle.THIN

            Dim font = templateWorkbook.CreateFont()
            font.FontHeightInPoints = 10
            font.FontName = "Calibri"

            Dim styleDate As ICellStyle = templateWorkbook.CreateCellStyle()
            styleDate.VerticalAlignment = VerticalAlignment.CENTER
            styleDate.Alignment = HorizontalAlignment.CENTER
            styleDate.SetFont(font)

            styleDate.BorderTop = BorderStyle.THIN
            styleDate.BorderRight = BorderStyle.THIN
            styleDate.BorderBottom = BorderStyle.THIN
            styleDate.BorderLeft = BorderStyle.THIN

            Dim formatIdDate = HSSFDataFormat.GetBuiltinFormat("dd/MM/yyyy")

            If formatIdDate = -1 Then
                Dim newDataFormat = templateWorkbook.CreateDataFormat()
                styleDate.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy")
            Else
                styleDate.DataFormat = formatIdDate
            End If

            Dim group1StartIndex As Integer
            Dim group2StartIndex As Integer
            Dim group3StartIndex As Integer
            Dim group4StartIndex As Integer
            Dim group5StartIndex As Integer

            Dim row As HSSFRow
            Dim cell As HSSFCell

            row = sheet.GetRow(0)
            cell = row.GetCell(0)
            cell.SetCellValue("Report As On- " & DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))

            Dim dt As DataTable = dset.Tables(0)

            RowsIndex = 2

            group1StartIndex = RowsIndex
            group2StartIndex = RowsIndex
            group3StartIndex = RowsIndex
            group4StartIndex = RowsIndex
            group5StartIndex = RowsIndex

            Dim colIndex As Integer = 0


            '==============Start Of Sheet1=========//
            For i = 0 To dt.Rows.Count - 1
                Dim TotalNop As Integer = 0

                If (Convert.ToString(dt.Rows(i)("indd_sku_code")).Equals("Z ALL Total") _
               And Convert.ToString(dt.Rows(i)("indh_depot")).Equals("ZZZ") _
               And Convert.ToString(dt.Rows(i)("depot_regn")).Equals("ZZ")) Then
                    TotalNop = 0
                    row = sheet.CreateRow(RowsIndex)

                    colIndex = 0
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue("Grand Total")
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("AutoLoad")))
                    cell.CellStyle = style5

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(Val(dt.Rows(i)("indd_sku_nop")))
                    'cell.CellStyle = style5

                    'TotalNop = Val(dt.Rows(i)("AutoLoad")) + Val(dt.Rows(i)("indd_sku_nop"))

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(TotalNop)
                    'cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("Total_Despatch")))
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    sheet.GroupRow(group1StartIndex, RowsIndex - 1)
                    group1StartIndex = RowsIndex + 1
                    group2StartIndex = group1StartIndex
                    group3StartIndex = group1StartIndex
                    group4StartIndex = group1StartIndex
                    group5StartIndex = group1StartIndex

                ElseIf (Convert.ToString(dt.Rows(i)("indd_sku_code")).Equals("Y Region Total") _
                     And Convert.ToString(dt.Rows(i)("indh_depot")).Equals("ZZZ") _
                     And Not Convert.ToString(dt.Rows(i)("depot_regn")).Equals("zz")
                  ) Then
                    TotalNop = 0
                    row = sheet.CreateRow(RowsIndex)
                    'Prod Group Total=================================
                    colIndex = 0
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_regn")) & " Total")
                    cell.CellStyle = style35

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("AutoLoad")))
                    cell.CellStyle = style33

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(Val(dt.Rows(i)("indd_sku_nop")))
                    'cell.CellStyle = style33

                    'TotalNop = Val(dt.Rows(i)("AutoLoad")) + Val(dt.Rows(i)("indd_sku_nop"))

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(TotalNop)
                    'cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("Total_Despatch")))
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    sheet.GroupRow(group4StartIndex, RowsIndex - 1)
                    group4StartIndex = RowsIndex + 1
                    RowsIndex = RowsIndex + 1

                ElseIf (Convert.ToString(dt.Rows(i)("indd_sku_code")).Equals("X Depot Total") _
                And Not Convert.ToString(dt.Rows(i)("indh_depot")).Equals("ZZZ") _
                And Not Convert.ToString(dt.Rows(i)("depot_regn")).Equals("ZZ")
             ) Then
                    TotalNop = 0
                    row = sheet.CreateRow(RowsIndex)
                    'Prod Group Total=================================
                    colIndex = 0
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_regn")))
                    cell.CellStyle = style37

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("indh_depot")) & " Total")
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("AutoLoad")))
                    cell.CellStyle = style36

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(Val(dt.Rows(i)("indd_sku_nop")))
                    'cell.CellStyle = style36

                    'TotalNop = Val(dt.Rows(i)("AutoLoad")) + Val(dt.Rows(i)("indd_sku_nop"))

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(TotalNop)
                    'cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("Total_Despatch")))
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellType(CellType.STRING)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style36

                    sheet.GroupRow(group4StartIndex, RowsIndex - 1)
                    group4StartIndex = RowsIndex + 1
                    RowsIndex = RowsIndex + 1
                Else
                    TotalNop = 0
                    row = sheet.CreateRow(RowsIndex)

                    colIndex = 0
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_regn")))
                    cell.CellStyle = stylecenter

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("indh_depot")))
                    cell.CellStyle = stylecenter

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_name")))
                    cell.CellStyle = styleleft

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("VendorCode")))
                    cell.CellStyle = stylecenter

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("unit_name")))
                    cell.CellStyle = styleleft

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("indd_sku_code")))
                    cell.CellStyle = stylecenter

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("sku_desc")))
                    cell.CellStyle = styleleft

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("IndentType")))
                    cell.CellStyle = styleleft

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("AutoLoad")))
                    cell.CellStyle = styleright

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(Val(dt.Rows(i)("indd_sku_nop")))
                    'cell.CellStyle = styleright

                    'TotalNop = Val(dt.Rows(i)("AutoLoad")) + Val(dt.Rows(i)("indd_sku_nop"))

                    'colIndex += 1
                    'cell = row.CreateCell(colIndex)
                    'cell.SetCellValue(TotalNop)
                    'cell.CellStyle = styleright

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    cell.SetCellValue(Val(dt.Rows(i)("Total_Despatch")))
                    cell.CellStyle = styleright

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    If Convert.ToString(dt.Rows(i)("indh_indent_no")) = String.Empty Or Convert.ToString(dt.Rows(i)("indh_indent_no")) = "999999" Then
                        cell.SetCellValue(String.Empty)
                    Else
                        cell.SetCellValue(Val(dt.Rows(i)("indh_indent_no")))
                    End If
                    cell.CellStyle = styleright

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    Try
                        cell.SetCellValue(Convert.ToDateTime(dt.Rows(i)("indh_indent_date")))
                    Catch ex As Exception
                        cell.SetCellValue("")
                    End Try
                    cell.CellStyle = styleDate

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    Try
                        cell.SetCellValue(Convert.ToDateTime(dt.Rows(i)("HOApprovedDate")))
                    Catch ex As Exception
                        cell.SetCellValue("")
                    End Try
                    cell.CellStyle = styleDate

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    If Convert.ToString(dt.Rows(i)("ChallanNo")) = String.Empty Then
                        cell.SetCellValue(String.Empty)
                    Else
                        cell.SetCellValue(Convert.ToString(dt.Rows(i)("ChallanNo")))
                    End If
                    cell.CellStyle = stylecenter

                    colIndex += 1
                    cell = row.CreateCell(colIndex)
                    If Convert.ToString(dt.Rows(i)("ChallanDate")) = String.Empty Or Convert.ToString(dt.Rows(i)("ChallanDate")).Contains("1900") Then
                        cell.SetCellValue(String.Empty)
                    Else
                        cell.SetCellValue(Convert.ToString(dt.Rows(i)("ChallanDate")))
                    End If
                    cell.CellStyle = stylecenter

                    RowsIndex = RowsIndex + 1
                End If
            Next


            '==============End Of Sheet1=========//
            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"
            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "ManualIndentReport" + ddlDepot.SelectedValue & "_" + UCase(ddlDepot.SelectedValue) + DateString + ".xls"
            Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)

            templateWorkbook.Write(fl)
            fl.Close()

            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.WriteFile(genReportPath & file_name)
            Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Catch ex As Exception
            Dim str = ex.Message.ToString()
        Finally

        End Try

    End Sub
    'Protected Sub ExportToExcelSheet(ByVal dset As DataSet)
    '    Try
    '        Dim datatime As String = System.DateTime.Now.Ticks
    '        'Opening the Excel template...
    '        Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\ManualIndentReport.xls", FileMode.Open, FileAccess.Read)
    '        'Getting the complete workbook...
    '        Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)
    '        'Getting the worksheet by its name...
    '        Dim sheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")
    '        Dim RowsIndex As Integer
    '        Dim DateString As String = (DateTime.Now.Ticks.ToString()).Substring(13) + "_" & Format(Now, "yyyyMMdd")
    '        'Styles

    '        Dim font1 As IFont = templateWorkbook.CreateFont()
    '        font1.Color = HSSFColor.BLACK.index
    '        font1.FontName = "Calibri"
    '        font1.FontHeightInPoints = 9

    '        Dim font2 As IFont = templateWorkbook.CreateFont()
    '        font2.Color = HSSFColor.BLACK.index
    '        font2.FontName = "Calibri"
    '        font2.FontHeightInPoints = 9

    '        Dim font3 As IFont = templateWorkbook.CreateFont()
    '        font3.Color = HSSFColor.YELLOW.index
    '        font3.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
    '        font3.FontName = "Calibri"
    '        font3.FontHeightInPoints = 9
    '        font3.IsItalic = True

    '        Dim font4 As IFont = templateWorkbook.CreateFont()
    '        font4.Color = HSSFColor.RED.index
    '        font4.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
    '        font4.FontName = "Calibri"

    '        font4.FontHeightInPoints = 9
    '        font4.IsItalic = True

    '        Dim font5 As IFont = templateWorkbook.CreateFont()
    '        font5.Color = HSSFColor.BLUE.index
    '        font5.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
    '        font5.FontName = "Calibri"
    '        font5.FontHeightInPoints = 9
    '        font5.IsItalic = True

    '        Dim style2 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style2.VerticalAlignment = VerticalAlignment.CENTER
    '        'style2.Alignment = HorizontalAlignment.CENTER

    '        ''style2.FillPattern = FillPattern.SolidForeground
    '        ''style2.FillForegroundColor = HSSFColor.BLACK.index
    '        style2.SetFont(font1)

    '        Dim style3 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style3.VerticalAlignment = VerticalAlignment.CENTER
    '        style3.Alignment = HorizontalAlignment.CENTER

    '        ''style2.FillPattern = FillPattern.SolidForeground
    '        style3.FillForegroundColor = HSSFColor.RED.index
    '        style3.SetFont(font1)

    '        Dim style4 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style4.VerticalAlignment = VerticalAlignment.CENTER
    '        style4.Alignment = HorizontalAlignment.CENTER

    '        ''style2.FillPattern = FillPattern.SolidForeground
    '        style4.FillForegroundColor = HSSFColor.BLUE.index
    '        style4.SetFont(font1)

    '        Dim style5 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style5.VerticalAlignment = VerticalAlignment.CENTER
    '        style5.SetFont(font3)
    '        'style5.FillForegroundColor = HSSFColor.YELLOW.index
    '        style5.FillPattern = FillPatternType.SOLID_FOREGROUND
    '        style5.BorderTop = BorderStyle.THIN
    '        style5.BorderRight = BorderStyle.THIN
    '        style5.BorderBottom = BorderStyle.THIN
    '        style5.BorderLeft = BorderStyle.THIN

    '        Dim stylecenter As ICellStyle = templateWorkbook.CreateCellStyle()
    '        stylecenter.VerticalAlignment = VerticalAlignment.CENTER
    '        stylecenter.Alignment = HorizontalAlignment.CENTER

    '        ''style2.FillPattern = FillPattern.SolidForeground
    '        ''style2.FillForegroundColor = HSSFColor.BLACK.index
    '        stylecenter.SetFont(font1)
    '        stylecenter.BorderTop = BorderStyle.THIN
    '        stylecenter.BorderRight = BorderStyle.THIN
    '        stylecenter.BorderBottom = BorderStyle.THIN
    '        stylecenter.BorderLeft = BorderStyle.THIN

    '        Dim style6 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style6.VerticalAlignment = VerticalAlignment.CENTER
    '        style6.Alignment = HorizontalAlignment.RIGHT

    '        Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
    '        styleValue.SetFont(font1)
    '        styleValue.VerticalAlignment = VerticalAlignment.CENTER
    '        styleValue.Alignment = HorizontalAlignment.RIGHT
    '        styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00 ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")

    '        ''style2.FillPattern = FillPattern.SolidForeground
    '        ''style2.FillForegroundColor = HSSFColor.BLACK.index
    '        style6.SetFont(font1)

    '        Dim style7 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style7.VerticalAlignment = VerticalAlignment.CENTER
    '        style7.Alignment = HorizontalAlignment.RIGHT

    '        style7.FillPattern = FillPatternType.SOLID_FOREGROUND
    '        style7.FillForegroundColor = HSSFColor.LIGHT_ORANGE.index
    '        style7.SetFont(font2)

    '        Dim styleleft As ICellStyle = templateWorkbook.CreateCellStyle()
    '        styleleft.VerticalAlignment = VerticalAlignment.CENTER
    '        styleleft.Alignment = HorizontalAlignment.LEFT
    '        styleleft.SetFont(font2)

    '        styleleft.BorderTop = BorderStyle.THIN
    '        styleleft.BorderRight = BorderStyle.THIN
    '        styleleft.BorderBottom = BorderStyle.THIN
    '        styleleft.BorderLeft = BorderStyle.THIN

    '        Dim styleright As ICellStyle = templateWorkbook.CreateCellStyle()
    '        styleright.VerticalAlignment = VerticalAlignment.CENTER
    '        styleright.Alignment = HorizontalAlignment.RIGHT
    '        styleright.SetFont(font2)

    '        styleright.BorderTop = BorderStyle.THIN
    '        styleright.BorderRight = BorderStyle.THIN
    '        styleright.BorderBottom = BorderStyle.THIN
    '        styleright.BorderLeft = BorderStyle.THIN

    '        Dim style8 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style8.VerticalAlignment = VerticalAlignment.CENTER
    '        style8.Alignment = HorizontalAlignment.RIGHT

    '        style8.FillPattern = FillPatternType.SOLID_FOREGROUND
    '        style8.FillForegroundColor = HSSFColor.LIGHT_BLUE.index
    '        style8.SetFont(font2)

    '        Dim style10 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style10.VerticalAlignment = VerticalAlignment.CENTER
    '        style10.Alignment = HorizontalAlignment.CENTER

    '        style10.FillPattern = FillPatternType.SOLID_FOREGROUND
    '        style10.FillForegroundColor = HSSFColor.LIGHT_BLUE.index
    '        style10.SetFont(font2)

    '        Dim style11 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style11.VerticalAlignment = VerticalAlignment.CENTER
    '        style11.Alignment = HorizontalAlignment.RIGHT

    '        style11.FillPattern = FillPatternType.SOLID_FOREGROUND
    '        style11.FillForegroundColor = HSSFColor.LIGHT_YELLOW.index
    '        style11.SetFont(font2)

    '        Dim style12 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style12.VerticalAlignment = VerticalAlignment.CENTER
    '        style12.Alignment = HorizontalAlignment.LEFT

    '        Dim style33 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style33.VerticalAlignment = VerticalAlignment.CENTER
    '        style33.SetFont(font4)
    '        style33.FillForegroundColor = HSSFColor.AUTOMATIC.index
    '        style33.BorderTop = BorderStyle.THIN
    '        style33.BorderRight = BorderStyle.THIN
    '        style33.BorderBottom = BorderStyle.THIN
    '        style33.BorderLeft = BorderStyle.THIN

    '        Dim style35 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style35.VerticalAlignment = VerticalAlignment.CENTER
    '        style35.Alignment = HorizontalAlignment.CENTER
    '        style35.SetFont(font4)
    '        style35.FillForegroundColor = HSSFColor.AUTOMATIC.index

    '        Dim style36 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style36.VerticalAlignment = VerticalAlignment.CENTER
    '        style36.SetFont(font5)
    '        style36.FillForegroundColor = HSSFColor.AUTOMATIC.index
    '        style36.BorderTop = BorderStyle.THIN
    '        style36.BorderRight = BorderStyle.THIN
    '        style36.BorderBottom = BorderStyle.THIN
    '        style36.BorderLeft = BorderStyle.THIN

    '        Dim style37 As ICellStyle = templateWorkbook.CreateCellStyle()
    '        style37.VerticalAlignment = VerticalAlignment.CENTER
    '        style37.Alignment = HorizontalAlignment.CENTER
    '        style37.SetFont(font5)
    '        style37.FillForegroundColor = HSSFColor.AUTOMATIC.index
    '        style37.BorderTop = BorderStyle.THIN
    '        style37.BorderRight = BorderStyle.THIN
    '        style37.BorderBottom = BorderStyle.THIN
    '        style37.BorderLeft = BorderStyle.THIN

    '        Dim font = templateWorkbook.CreateFont()
    '        font.FontHeightInPoints = 10
    '        font.FontName = "Calibri"

    '        Dim styleDate As ICellStyle = templateWorkbook.CreateCellStyle()
    '        styleDate.VerticalAlignment = VerticalAlignment.CENTER
    '        styleDate.Alignment = HorizontalAlignment.CENTER
    '        styleDate.SetFont(font)

    '        styleDate.BorderTop = BorderStyle.THIN
    '        styleDate.BorderRight = BorderStyle.THIN
    '        styleDate.BorderBottom = BorderStyle.THIN
    '        styleDate.BorderLeft = BorderStyle.THIN

    '        Dim formatIdDate = HSSFDataFormat.GetBuiltinFormat("dd/MM/yyyy")

    '        If formatIdDate = -1 Then
    '            Dim newDataFormat = templateWorkbook.CreateDataFormat()
    '            styleDate.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy")
    '        Else
    '            styleDate.DataFormat = formatIdDate
    '        End If

    '        Dim group1StartIndex As Integer
    '        Dim group2StartIndex As Integer
    '        Dim group3StartIndex As Integer
    '        Dim group4StartIndex As Integer
    '        Dim group5StartIndex As Integer

    '        Dim row As HSSFRow
    '        Dim cell As HSSFCell

    '        row = sheet.GetRow(0)
    '        cell = row.GetCell(0)
    '        cell.SetCellValue("Report As On- " & DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))

    '        Dim dt As DataTable = dset.Tables(0)

    '        RowsIndex = 2

    '        group1StartIndex = RowsIndex
    '        group2StartIndex = RowsIndex
    '        group3StartIndex = RowsIndex
    '        group4StartIndex = RowsIndex
    '        group5StartIndex = RowsIndex

    '        Dim colIndex As Integer = 0

    '        '==============Start Of Sheet1=========//
    '        For i = 0 To dt.Rows.Count - 1
    '            If (Convert.ToString(dt.Rows(i)("indd_sku_code")).Equals("Z ALL Total") _
    '           And Convert.ToString(dt.Rows(i)("indh_depot")).Equals("ZZZ") _
    '           And Convert.ToString(dt.Rows(i)("depot_regn")).Equals("ZZ")) Then

    '                row = sheet.CreateRow(RowsIndex)

    '                colIndex = 0
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue("Grand Total")
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.NUMERIC)
    '                cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("indd_sku_nop"))))
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style5

    '                sheet.GroupRow(group1StartIndex, RowsIndex - 1)
    '                group1StartIndex = RowsIndex + 1
    '                group2StartIndex = group1StartIndex
    '                group3StartIndex = group1StartIndex
    '                group4StartIndex = group1StartIndex
    '                group5StartIndex = group1StartIndex

    '            ElseIf (Convert.ToString(dt.Rows(i)("indd_sku_code")).Equals("Y Region Total") _
    '                 And Convert.ToString(dt.Rows(i)("indh_depot")).Equals("ZZZ") _
    '                 And Not Convert.ToString(dt.Rows(i)("depot_regn")).Equals("zz")
    '              ) Then

    '                row = sheet.CreateRow(RowsIndex)
    '                'Prod Group Total=================================
    '                colIndex = 0
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_regn")) & " Total")
    '                cell.CellStyle = style35

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.NUMERIC)
    '                cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("indd_sku_nop"))))
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style33

    '                sheet.GroupRow(group4StartIndex, RowsIndex - 1)
    '                group4StartIndex = RowsIndex + 1
    '                RowsIndex = RowsIndex + 1

    '            ElseIf (Convert.ToString(dt.Rows(i)("indd_sku_code")).Equals("X Depot Total") _
    '            And Not Convert.ToString(dt.Rows(i)("indh_depot")).Equals("ZZZ") _
    '            And Not Convert.ToString(dt.Rows(i)("depot_regn")).Equals("ZZ")
    '         ) Then

    '                row = sheet.CreateRow(RowsIndex)
    '                'Prod Group Total=================================
    '                colIndex = 0
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_regn")))
    '                cell.CellStyle = style37

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("indh_depot")) & " Total")
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.NUMERIC)
    '                cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("indd_sku_nop"))))
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellType(CellType.STRING)
    '                cell.SetCellValue(String.Empty)
    '                cell.CellStyle = style36

    '                sheet.GroupRow(group4StartIndex, RowsIndex - 1)
    '                group4StartIndex = RowsIndex + 1
    '                RowsIndex = RowsIndex + 1
    '            Else
    '                row = sheet.CreateRow(RowsIndex)

    '                colIndex = 0
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_regn")))
    '                cell.CellStyle = stylecenter

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("indh_depot")))
    '                cell.CellStyle = stylecenter

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("depot_name")))
    '                cell.CellStyle = styleleft

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("indd_sku_code")))
    '                cell.CellStyle = stylecenter

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToString(dt.Rows(i)("sku_desc")))
    '                cell.CellStyle = styleleft

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("indd_sku_nop")))
    '                cell.CellStyle = styleright

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("indh_indent_no")))
    '                cell.CellStyle = styleright

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                Try
    '                    cell.SetCellValue(Convert.ToDateTime(dt.Rows(i)("indh_indent_date")))
    '                Catch ex As Exception
    '                    cell.SetCellValue("")
    '                End Try
    '                cell.CellStyle = styleDate

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                Try
    '                    cell.SetCellValue(Convert.ToDateTime(dt.Rows(i)("HOApprovedDate")))
    '                Catch ex As Exception
    '                    cell.SetCellValue("")
    '                End Try
    '                cell.CellStyle = styleDate

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("AutoLoad")))
    '                cell.CellStyle = styleright

    '                colIndex += 1
    '                cell = row.CreateCell(colIndex)
    '                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("Total_Despatch")))
    '                cell.CellStyle = styleright

    '                RowsIndex = RowsIndex + 1
    '            End If
    '        Next


    '        '==============End Of Sheet1=========//
    '        Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"
    '        If Not (Directory.Exists(genReportPath)) Then
    '            Directory.CreateDirectory(genReportPath)
    '        End If
    '        Dim file_name As String = "ManualIndentReport" + ddlDepot.SelectedValue & "_" + UCase(ddlDepot.SelectedValue) + DateString + ".xls"
    '        Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)

    '        templateWorkbook.Write(fl)
    '        fl.Close()

    '        Response.Clear()
    '        Response.Charset = ""
    '        Response.ContentType = "application/vnd.ms-excel"
    '        Response.WriteFile(genReportPath & file_name)
    '        Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
    '        Response.Cache.SetCacheability(HttpCacheability.NoCache)

    '    Catch ex As Exception
    '        Dim str = ex.Message.ToString()
    '    Finally

    '    End Try

    'End Sub
#End Region
End Class
