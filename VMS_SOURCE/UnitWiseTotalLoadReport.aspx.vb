
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
Partial Class UnitWiseTotalLoadReport
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim kgTotal As Decimal = 0.0
    Dim LtrTotal As Decimal = 0.0
    Dim Nop As Integer = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            PopulateProcessYr()
            PopulateRegion()
            PopulateDepot()
            PopulateUnit()
            'LoadSearchCriteria()
            'ddlProcessYr.Enabled = False
            'ddlProcessMnth.Enabled = False
            'ddlReportFormat.Enabled = False
            'txtAsOnDate.Text = Format(Date.Today, "dd/MM/yyyy")
            'txtAsOnDate.Enabled = False
        End If
    End Sub


#Region "Populate Process Year"
    Private Sub PopulateProcessYr()
        CheckLogin()

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
            'ddlProcessYr.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            'ddlProcessYr.Items.Insert(0, New ListItem("2011", String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlProcessYr.SelectedValue = userInfo.currentFinancialYearEntity
        '    ddlProcessYr.Enabled = False
        'End If


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
#Region "Populate Region"
    Private Sub PopulateRegion()
        CheckLogin()

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlRegion.DataSource = OccupationTypeSet.Tables(0)
            ddlRegion.DataTextField = "lov_value"
            ddlRegion.DataValueField = "lov_code"
            ddlRegion.DataBind()
            ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HO Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlRegion.SelectedValue = userInfo.userRegionEntity
            ddlRegion.Enabled = False
        End If

    End Sub
#End Region
#Region "Populate Depot"
    Private Sub PopulateDepot()
        CheckLogin()

        Dim GeDepot As New Common
        Dim DepotSet As New DataSet

        DepotSet = GeDepot.Getdepotname(ddlRegion.SelectedValue)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = DepotSet.Tables(0)
            ddlDepot.DataTextField = "depot_name"
            ddlDepot.DataValueField = "depot_code"
            ddlDepot.DataBind()
            'ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
            ddlDepot.SelectedValue = userInfo.userBranchEntity
            ddlDepot.Enabled = False
        Else
            ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
    End Sub
#End Region
#Region "Populate Unit"
    Private Sub PopulateUnit()
        CheckLogin()

        Dim UnitDespatch As New PendingDespatchesClass
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, ddlRegion.SelectedValue)
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

    Private Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
    End Sub
#End Region
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim company As String = userInfo.userCompanyEntity
        Dim region As String = ddlRegion.SelectedValue
        Dim depot As String = ddlDepot.SelectedValue
        Dim unit As String = ddlUnit.SelectedValue
        Dim ProcessYr As String = ddlProcessYr.SelectedValue
        Dim ProcessMnth As String = ddlProcessMnth.SelectedValue

        'Dim reportObj As New PendingDespatchesClass
        'Dim ExcelSet As DataSet
        'ExcelSet = reportObj.Total_Load_Report(region, depot, ProcessYr, ProcessMnth, unit)
        'If (ExcelSet.Tables(0).Rows.Count > 0) Then

        '    Dim rowcount, i As Integer
        '    rowcount = ExcelSet.Tables(0).Rows.Count


        '    For i = 0 To rowcount - 1

        '        ExcelSet.Tables(0).Rows(i)("Srl") = i + 1


        '    Next

        '    Dim FileNme As String
        '    FileNme = Convert.ToString(userInfo.userCompanyEntity)
        '    FileNme = FileNme + "_" + "Pending Despatches" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
        '    ExportToExcel(ExcelSet, Response, FileNme)
        'End If

        Try
            Dim mst As PendingDespatchesClass = New PendingDespatchesClass()
            Dim ds As DataSet = Nothing

            ds = mst.Total_Load_Report(region, depot, ProcessYr, ProcessMnth, unit)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ExportToExcelSheet1(ds)
                Else
                    lblErrMsg.Text = "No data found."
                End If
            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect("~/ExceptionPage.aspx")
        End Try

    End Sub

#Region "Export to Excel using Dll"
    Private Sub ExportToExcelSheet1(ByVal dset As DataSet)
        'Opening the Excel template...
        Dim fs As FileStream = New FileStream(AppDomain.CurrentDomain.BaseDirectory & "Templates\UnitWiseLoarReport.xls", FileMode.Open, FileAccess.Read)

        'Getting the complete workbook...
        Dim templateWorkbook As HSSFWorkbook = New HSSFWorkbook(fs, True)

        'Getting the worksheet by its name...
        Dim sheet As HSSFSheet = templateWorkbook.GetSheet("Sheet1")


        Dim font1 As IFont = templateWorkbook.CreateFont()
        font1.Color = HSSFColor.BLACK.index
        font1.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
        font1.FontName = "Arial"
        font1.FontHeightInPoints = 9

        Dim font2 As IFont = templateWorkbook.CreateFont()
        font2.Color = HSSFColor.BLACK.index
        font2.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
        font2.FontName = "Arial"
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

        Dim style1 As ICellStyle = templateWorkbook.CreateCellStyle()
        style1.VerticalAlignment = VerticalAlignment.CENTER
        style1.BottomBorderColor = HSSFColor.BLACK.index
        style1.SetFont(font1)

        Dim style2 As ICellStyle = templateWorkbook.CreateCellStyle()
        style2.VerticalAlignment = VerticalAlignment.CENTER
        style2.SetFont(font2)
        style2.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_(* #,##0.00_);_(* (#,##0.00);_(* "" ""??_);_(@_)")

        Dim style11 As ICellStyle = templateWorkbook.CreateCellStyle()
        style11.VerticalAlignment = VerticalAlignment.CENTER
        style11.SetFont(font1)

        Dim style12 As ICellStyle = templateWorkbook.CreateCellStyle()
        style12.VerticalAlignment = VerticalAlignment.CENTER
        style12.SetFont(font2)

        Dim style13 As ICellStyle = templateWorkbook.CreateCellStyle()
        style13.VerticalAlignment = VerticalAlignment.CENTER
        style13.SetFont(font2)

        Dim style3 As ICellStyle = templateWorkbook.CreateCellStyle()
        style3.VerticalAlignment = VerticalAlignment.CENTER
        style3.SetFont(font4)
        style3.FillForegroundColor = HSSFColor.SkyBlue.Index
        'style3.FillBackgroundColor = HSSFColor.SKY_BLUE.index
        style3.FillPattern = FillPattern.SolidForeground
        'style3.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_(* #,##0.00_);_(* (#,##0.00);_(* "" ""??_);_(@_)")

        Dim style4 As ICellStyle = templateWorkbook.CreateCellStyle()
        style4.VerticalAlignment = VerticalAlignment.CENTER
        style4.SetFont(font3)
        style4.FillForegroundColor = HSSFColor.LightYellow.Index
        style4.FillPattern = FillPattern.SolidForeground


        Dim style5 As ICellStyle = templateWorkbook.CreateCellStyle()
        style5.VerticalAlignment = VerticalAlignment.CENTER
        style5.SetFont(font3)
        'style5.FillForegroundColor = HSSFColor.YELLOW.index
        style5.FillPattern = FillPattern.SolidForeground


        Dim style33 As ICellStyle = templateWorkbook.CreateCellStyle()
        style33.VerticalAlignment = VerticalAlignment.CENTER
        style33.SetFont(font4)
        style33.FillForegroundColor = HSSFColor.AUTOMATIC.index
        'style33.FillPattern = FillPatternType.SOLID_FOREGROUND

        Dim style35 As ICellStyle = templateWorkbook.CreateCellStyle()
        style35.VerticalAlignment = VerticalAlignment.CENTER
        style35.Alignment = HorizontalAlignment.CENTER
        style35.SetFont(font4)
        style35.FillForegroundColor = HSSFColor.AUTOMATIC.index
        'style33.FillPattern = FillPatternType.SOLID_FOREGROUND


        Dim style32 As ICellStyle = templateWorkbook.CreateCellStyle()
        style32.VerticalAlignment = VerticalAlignment.CENTER
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
        cell.SetCellValue("Report Month - " & GetFromMonth(Convert.ToInt32(ddlProcessMnth.SelectedValue)) & "'" & Right(Convert.ToString(ddlProcessYr.SelectedValue), 2))

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
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(7)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("load_estimate_nop"))))
                    cell.CellStyle = style5

                    cell = row.CreateCell(8)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("ltrVol"))))
                    cell.CellStyle = style5

                    cell = row.CreateCell(9)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("kgVol"))))
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
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style5

                    cell = row.CreateCell(7)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("load_estimate_nop"))))
                    cell.CellStyle = style5

                    cell = row.CreateCell(8)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("ltrVol"))))
                    cell.CellStyle = style5

                    cell = row.CreateCell(9)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("kgVol"))))
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
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("SkuDescription")) & " Total")
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
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style3

                    cell = row.CreateCell(7)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("load_estimate_nop"))))
                    cell.CellStyle = style3

                    cell = row.CreateCell(8)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("ltrVol"))))
                    cell.CellStyle = style3

                    cell = row.CreateCell(9)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("kgVol"))))
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
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(4)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("Region")) & " Total")
                    cell.CellStyle = style35

                    cell = row.CreateCell(5)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(6)
                    cell.SetCellValue(String.Empty)
                    cell.CellStyle = style33

                    cell = row.CreateCell(7)
                    'cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("load_estimate_nop"))))
                    cell.CellStyle = style33

                    cell = row.CreateCell(8)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("ltrVol"))))
                    cell.CellStyle = style33

                    cell = row.CreateCell(9)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("kgVol"))))
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
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("SkuDescription")))

                    cell = row.CreateCell(3)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("Po_Link")))

                    cell = row.CreateCell(4)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("Region")))


                    cell = row.CreateCell(5)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("DepotCode")))

                    cell = row.CreateCell(6)
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("DepotName")))

                    cell = row.CreateCell(7)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("load_estimate_nop"))))
                    cell.CellStyle = style32

                    cell = row.CreateCell(8)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("ltrVol"))))
                    cell.CellStyle = style32

                    cell = row.CreateCell(9)
                    cell.SetCellType(CellType.NUMERIC)
                    cell.SetCellValue(Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows(i)("kgVol"))))
                    cell.CellStyle = style32


                    RowIndex = RowIndex + 1

                End If

            Next
        End If


        Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"

        If Not (Directory.Exists(genReportPath)) Then
            Directory.CreateDirectory(genReportPath)
        End If

        Dim file_name As String = "UnitWiseTotalLoad" & DateString & ".xls"

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

    Private Function GetFromMonth(ByVal MonthCode As Integer) As String
        Dim MonthName As String = String.Empty
        If (MonthCode = 1) Then
            MonthName = "January"
        ElseIf (MonthCode = 2) Then
            MonthName = "February"
        ElseIf (MonthCode = 3) Then
            MonthName = "March"
        ElseIf (MonthCode = 4) Then
            MonthName = "April"
        ElseIf (MonthCode = 5) Then
            MonthName = "May"
        ElseIf (MonthCode = 6) Then
            MonthName = "June"
        ElseIf (MonthCode = 7) Then
            MonthName = "July"
        ElseIf (MonthCode = 8) Then
            MonthName = "August"
        ElseIf (MonthCode = 9) Then
            MonthName = "September"
        ElseIf (MonthCode = 10) Then
            MonthName = "October"
        ElseIf (MonthCode = 11) Then
            MonthName = "November"
        ElseIf (MonthCode = 12) Then
            MonthName = "December"
        End If
        Return MonthName.ToString
    End Function

End Class
