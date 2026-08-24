Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Partial Class IndentWise_Vendorrating
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Request.QueryString("NoData") = "Yes") Then
            lblErrMsg.Text = "No record found!"
        Else
            lblErrMsg.Text = String.Empty
        End If

        CheckLogin()

        If Not IsPostBack Then
            PopulateRegion()
            PopulateDepot()
            PopulateUnit()
            PopulateProcessYr()

            'Dim DptDsptchdUntWise As New DepotDespatchUnitwiseApp
            'btnSubmit.Attributes.Add("onClick", "return ValidateDptDsptchUntWise('" + DptDsptchdUntWise.GetTopFinYear() + "','" + DptDsptchdUntWise.GetLastFinYear() + "');")
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

#Region "Populate Region"
    Private Sub PopulateRegion()

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
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlRegion.SelectedValue = userInfo.userRegionEntity
            ddlRegion.Enabled = False
        End If

    End Sub
#End Region

#Region "Populate Depot"
    Private Sub PopulateDepot()
        Dim DptDsptchdUntWise As New Common
        Dim DepotSet As New DataSet

        DepotSet = DptDsptchdUntWise.Getdepotname(ddlRegion.SelectedValue)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlLocation.DataSource = DepotSet.Tables(0)
            ddlLocation.DataTextField = "depot_name"
            ddlLocation.DataValueField = "depot_code"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.REGION) Then
            ddlLocation.SelectedValue = userInfo.userBranchEntity
            ddlLocation.Enabled = False
        End If
    End Sub
#End Region

#Region "Populate Unit"
    Private Sub PopulateUnit()
        Dim DptDsptchdUntWise As New DepotDespatchUnitwiseApp
        Dim UnitSet As New DataSet

        UnitSet = DptDsptchdUntWise.GetUnit(Constant.Common.ActiveStatus)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlDsptchdUnit.DataSource = UnitSet.Tables(0)
            ddlDsptchdUnit.DataTextField = "unit_name"
            ddlDsptchdUnit.DataValueField = "unit_code"
            ddlDsptchdUnit.DataBind()
            ddlDsptchdUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlDsptchdUnit.SelectedValue = userInfo.userUnitEntity
        '    ddlDsptchdUnit.Enabled = False
        'End If
        'If (userInfo.userGroupCodeEntity = "UNIT") Then
        '    ddlDsptchdUnit.SelectedValue = userInfo.userBranchEntity
        '    ddlDsptchdUnit.Enabled = False
        'End If
    End Sub
#End Region
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
    Protected Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'Modified-by MUKESH BHAGAT on 20-08-2026 : Home_New.aspx does not exist in this project (Cancel gave 404); OLD redirected to Home.aspx
        Response.Redirect("~/Home.aspx")
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim cls As New VendorRankingReportClass
        Dim ExcelSet As New DataSet
        Try
            ExcelSet = cls.Get_VendorIndent_Ranklist(ddlLocation.SelectedValue, ddlDsptchdUnit.SelectedValue, userInfo.userIDEntity, ddlProcessYr.SelectedValue, ddlProcessMnth.SelectedValue)

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
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\Indent_Wise_Vendor_Rating_Template.xlsx", FileMode.Open, FileAccess.Read)

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

            Dim sheet As XSSFSheet = templateWorkbook.GetSheet("Sheet1")
            Dim RowsIndex As Integer

            Dim row As XSSFRow
            Dim cell As XSSFCell

            Dim DateString As String = "_" & DateTime.Today.ToString("dd_MM_yyyy")
            row = sheet.GetRow(0)
            cell = row.GetCell(0)
            cell.SetCellValue("Indent Wise Vendor Rating Report As On - " + Format(Now, "dd-MM-yyyy").ToString)

            RowsIndex = 2
            Dim count = 0
            Dim colIndex As Integer = 0

            For i = 0 To dset.Tables(0).Rows.Count - 1
                row = sheet.CreateRow(RowsIndex)
                colIndex = 0

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("Region")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("Depot")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("Vendor")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("indh_indent_no")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("indh_fin_year")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("indh_month")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("SKU")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("SKU_DESC")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToDouble(dset.Tables(0).Rows(i)("Volume")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToInt32(dset.Tables(0).Rows(i)("indd_sku_nop")))
                cell.CellStyle = styleRight
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToDouble(dset.Tables(0).Rows(i)("TotalVolume")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("Priority")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("Reason")))
                cell.CellStyle = styleLeft
                colIndex += 1

                RowsIndex = RowsIndex + 1

            Next

            Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"
            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "Indent_Wise_Vendor_Rating_Report_" & DateString & ".xlsx"
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
End Class
