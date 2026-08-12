
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports CrystalDecisions.Web
'Imports Microsoft.Office.Interop.Excel
Imports System.Diagnostics
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports System.IO
Imports NPOI.SS.Formula.Functions
Imports NPOI.XSSF.UserModel

Partial Class MonthYearWiseUnitDespatch
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CheckLogin()

        If Not IsPostBack Then

            AddAttributes()
            PopulateRegionDropdown()
            PopulateDepotDropdown()
            PopulateProductCategory()
            'PopulateProducts()
            PopulateUnit()
            PopulateProcessYr()
            'Dim CurrentDate As String = DateTime.Now.ToString("dd/MM/yyyy")
            'txtfromDate.Text = CurrentDate
            'txtToDate.Text = CurrentDate
        End If

    End Sub
    Private Sub PopulateUnit()

        Dim UnitDespatch As New MonthlyUnitDespatch
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
#Region "Populate Region dropdown."

    Private Sub PopulateRegionDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnRegion As New Common()
        Dim dsRegion As DataSet

        Try

            dsRegion = cmnRegion.GetLovDetails(userInfo.userCompanyEntity, Constant.Common.REGION_TYPE, Constant.Common.ActiveStatus)

            If Not (dsRegion Is Nothing) Then

                If Not (dsRegion.Tables(0).Rows.Count = 0) Then

                    ddlRegion.DataSource = dsRegion
                    ddlRegion.DataTextField = "Lov_Value"
                    ddlRegion.DataValueField = "Lov_Code"
                    ddlRegion.DataBind()

                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, "", True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                        ddlRegion.SelectedValue = userInfo.userRegionEntity
                        ddlRegion.Enabled = False
                    End If

                Else
                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub


#End Region
#Region "Get values for a particular Standard Parameter."

    Private Function GetStandardParameter(ByVal param_name As String) As String

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnStandardParameter As New Common()
        Dim dsStandardParameter As DataSet

        Dim result As String = String.Empty

        Try

            dsStandardParameter = cmnStandardParameter.GetStandardParameterValues(param_name)

            If Not (dsStandardParameter Is Nothing) Then

                If Not (dsStandardParameter.Tables(0).Rows.Count = 0) Then
                    result = dsStandardParameter.Tables(0).Rows(0)("param_char_value")
                Else
                    Dim returnUrl As String = "~/ExceptionPage.aspx"
                    Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    Server.Transfer(returnUrl)
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

        Return result

    End Function

#End Region
#Region "Populate Depot dropdown."

    Private Sub PopulateDepotDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New Common()
        Dim dsDepot As DataSet

        ddlDepot.Items.Clear()

        Try

            dsDepot = cmnDepot.Getdepotname(ddlRegion.SelectedValue)

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlDepot.DataSource = dsDepot
                    ddlDepot.DataTextField = "depot_name"
                    ddlDepot.DataValueField = "depot_code"
                    ddlDepot.DataBind()

                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.REGION) Then
                        ddlDepot.SelectedValue = userInfo.userBranchEntity
                        ddlDepot.Enabled = False
                    End If

                Else
                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region
    Private Sub PopulateProductCategory()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New IndentMaster()
        Dim dsDepot As DataSet

        ddlproductcat.Items.Clear()

        Try

            dsDepot = cmnDepot.GetProductCategory()

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlproductcat.DataSource = dsDepot
                    ddlproductcat.DataTextField = "product_name"
                    ddlproductcat.DataValueField = "product_name"
                    ddlproductcat.DataBind()
                    ddlproductcat.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                Else
                    ddlproductcat.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub
    Private Sub PopulateProducts()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New MonthlyUnitDespatch()
        Dim dsDepot As DataSet

        ddlproductcode.Items.Clear()

        Try

            dsDepot = cmnDepot.GetProductCategoryWiseSKUDtls(ddlproductcat.SelectedValue)

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlproductcode.DataSource = dsDepot
                    ddlproductcode.DataTextField = "sku_desc"
                    ddlproductcode.DataValueField = "sku_code"
                    ddlproductcode.DataBind()
                    ddlproductcode.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                Else
                    ddlproductcode.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub
#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        'btndownload.Attributes.Add("onClick", "return validateForm()")
    End Sub
#End Region
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
    Private Sub CheckLogin()

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

    End Sub
    Protected Sub btndownload_Click(sender As Object, e As EventArgs)
        Try

            'Dim FromDate As String
            'Dim ToDate As String
            'FromDate = String.Format("{0:yyyy-MM-dd}", txtfromDate.Text)
            'ToDate = String.Format("{0:yyyy-MM-dd}", txtToDate.Text)
            Dim reportObj As New MonthlyUnitDespatch
            Dim ExcelSet As DataSet
            ExcelSet = reportObj.GetMonthYearWiseSKUDespatchReport(ddlProcessYr.SelectedValue, ddlProcessMnth.SelectedValue, userInfo.userIDEntity, ddlproductcat.SelectedValue, ddlproductcode.SelectedValue, ddlRegion.SelectedValue, ddlDepot.SelectedValue, ddlUnit.SelectedValue)
            If (ExcelSet.Tables(0).Rows.Count > 0) Then
                ExportToExcelSheet(ExcelSet)
            Else
                lblErrMsg.Text = "No Data Found"
            End If
        Catch ex As Exception
            Dim MSG As String = ex.Message
        End Try
    End Sub
    Private Sub ExportToExcelSheet(ByVal ds As DataSet)
        'Opening the Excel template...
        Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\Month_Year_Wise_Unit_Despatch_Report_Template.xlsx", FileMode.Open, FileAccess.Read)

        Dim templateWorkbook As XSSFWorkbook = New XSSFWorkbook(fs)
        Dim sheet1 As XSSFSheet = CType(templateWorkbook.GetSheet("Sheet1"), XSSFSheet)
        Dim font1 As IFont = templateWorkbook.CreateFont()
        font1.FontHeightInPoints = 9
        font1.FontName = "Calibri"

        Dim font2 As IFont = templateWorkbook.CreateFont()
        font2.FontHeightInPoints = 9
        font2.FontName = "Calibri"
        font2.Boldweight = FontBoldWeight.Bold

        Dim styleGTotal As ICellStyle = templateWorkbook.CreateCellStyle()
        styleGTotal.VerticalAlignment = VerticalAlignment.Center
        styleGTotal.Alignment = HorizontalAlignment.Left
        styleGTotal.SetFont(font2)
        styleGTotal.FillForegroundColor = IndexedColors.Coral.Index
        styleGTotal.FillPattern = FillPattern.SolidForeground
        styleGTotal.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleGTotal.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleGTotal.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        styleGTotal.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin

        Dim styleTotal As ICellStyle = templateWorkbook.CreateCellStyle()
        styleTotal.VerticalAlignment = VerticalAlignment.Center
        styleTotal.Alignment = HorizontalAlignment.Right
        styleTotal.SetFont(font2)
        styleTotal.FillForegroundColor = IndexedColors.Coral.Index
        styleTotal.FillPattern = FillPattern.SolidForeground
        styleTotal.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleTotal.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleTotal.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        styleTotal.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin

        Dim styleRight As ICellStyle = templateWorkbook.CreateCellStyle()
        styleRight.VerticalAlignment = VerticalAlignment.Center
        styleRight.Alignment = HorizontalAlignment.Right
        styleRight.SetFont(font1)
        styleRight.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleRight.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleRight.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        styleRight.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin

        Dim styleLeft As ICellStyle = templateWorkbook.CreateCellStyle()
        styleLeft.VerticalAlignment = VerticalAlignment.Center
        styleLeft.Alignment = HorizontalAlignment.Left
        styleLeft.SetFont(font1)
        styleLeft.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleLeft.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleLeft.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        styleLeft.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin

        Dim styleCenter As ICellStyle = templateWorkbook.CreateCellStyle()
        styleCenter.VerticalAlignment = VerticalAlignment.Center
        styleCenter.Alignment = HorizontalAlignment.Center
        styleCenter.SetFont(font1)
        styleCenter.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleCenter.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleCenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        styleCenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin

        Dim styleDate As ICellStyle = templateWorkbook.CreateCellStyle()
        styleDate.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleDate.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleDate.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        styleDate.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin
        styleDate.VerticalAlignment = VerticalAlignment.Center
        styleDate.Alignment = HorizontalAlignment.Center
        styleDate.SetFont(font1)

        Dim styleValue As ICellStyle = templateWorkbook.CreateCellStyle()
        styleValue.SetFont(font1)
        styleValue.Alignment = HorizontalAlignment.Right
        styleValue.VerticalAlignment = VerticalAlignment.Center
        styleValue.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin
        styleValue.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleValue.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleValue.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        Dim currency As Short = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")
        styleValue.DataFormat = currency

        Dim styleValue1 As ICellStyle = templateWorkbook.CreateCellStyle()
        styleValue1.SetFont(font1)
        styleValue1.Alignment = HorizontalAlignment.Right
        styleValue1.VerticalAlignment = VerticalAlignment.Center
        styleValue1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin
        styleValue1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin
        styleValue1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin
        styleValue1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin
        Dim currency1 As Short = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0_ ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")
        styleValue1.DataFormat = currency1
        Dim formatIdDate = HSSFDataFormat.GetBuiltinFormat("dd/MM/yyyy")

        If formatIdDate = -1 Then
            Dim newDataFormat = templateWorkbook.CreateDataFormat()
            styleDate.DataFormat = newDataFormat.GetFormat("dd/MM/yyyy")
        Else
            styleDate.DataFormat = formatIdDate
        End If

        Dim RowsIndex As Integer
        Dim DateString As String = "_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm")
        Dim row As XSSFRow
        Dim cell As XSSFCell
        Dim dt As DataTable = ds.Tables(0)
        Dim totalOPCDepotDespatch As Decimal = 0
        Dim TotalDD As Decimal = 0
        Dim TotalOpcDD As Decimal = 0
        Dim totalFactoryDD As Decimal = 0
        Dim totalFactoryDepotDespatch As Decimal = 0
        Dim TotalDespatches As Decimal = 0

        If dt.Rows.Count > 0 Then
            RowsIndex = 2
            row = CType(sheet1.GetRow(0), XSSFRow)
            cell = CType(row.GetCell(0), XSSFCell)
            cell.SetCellValue("SKU LEVEL DD Report - " & ddlProcessMnth.SelectedItem.Text & ddlProcessYr.SelectedValue)
            cell = CType(row.GetCell(0), XSSFCell)
            Dim CellIndex As Integer = 0

            For i = 0 To dt.Rows.Count - 1
                CellIndex = 0
                row = CType(sheet1.CreateRow(RowsIndex), XSSFRow)
                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToString(dt.Rows(i)("SKU")))
                cell.CellStyle = styleLeft

                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToString(dt.Rows(i)("Description")))
                cell.CellStyle = styleLeft

                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("OPC Depot Despatch")))
                cell.CellStyle = styleRight

                totalOPCDepotDespatch = totalOPCDepotDespatch + Convert.ToDecimal(dt.Rows(i)("OPC Depot Despatch"))

                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("OPC DD")))
                cell.CellStyle = styleRight

                TotalOpcDD = TotalOpcDD + Convert.ToDecimal(dt.Rows(i)("OPC DD"))

                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("Factory DD")))
                cell.CellStyle = styleRight

                totalFactoryDD = totalFactoryDD + Convert.ToDecimal(dt.Rows(i)("Factory DD"))

                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("Factory Depot Despatch")))
                cell.CellStyle = styleRight

                totalFactoryDepotDespatch = totalFactoryDepotDespatch + Convert.ToDecimal(dt.Rows(i)("Factory Depot Despatch"))

                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("Total Despatches")))
                cell.CellStyle = styleRight

                TotalDespatches = TotalDespatches + Convert.ToDecimal(dt.Rows(i)("Total Despatches"))

                cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
                cell.SetCellValue(Convert.ToDecimal(dt.Rows(i)("Total DD %")))
                cell.CellStyle = styleRight

                TotalDD = TotalDD + Convert.ToDecimal(dt.Rows(i)("Total DD %"))

                RowsIndex = RowsIndex + 1
            Next
            RowsIndex = RowsIndex
            CellIndex = 0
            row = CType(sheet1.CreateRow(RowsIndex), XSSFRow)
            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue("GRAND TOTAL")
            cell.CellStyle = styleGTotal

            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue("")
            cell.CellStyle = styleGTotal

            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue(totalOPCDepotDespatch)
            cell.CellStyle = styleTotal

            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue(TotalOpcDD)
            cell.CellStyle = styleTotal

            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue(totalFactoryDD)
            cell.CellStyle = styleTotal

            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue(totalFactoryDepotDespatch)
            cell.CellStyle = styleTotal

            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue(TotalDespatches)
            cell.CellStyle = styleTotal

            Dim PercentageDD As Decimal = 0
            PercentageDD = Math.Round(((TotalOpcDD + totalFactoryDepotDespatch) / (totalOPCDepotDespatch + TotalOpcDD + totalFactoryDD + totalFactoryDepotDespatch) * 100), 2)

            cell = CType(row.CreateCell(Math.Min(System.Threading.Interlocked.Increment(CellIndex), CellIndex - 1)), XSSFCell)
            cell.SetCellValue(PercentageDD)
            cell.CellStyle = styleTotal
        End If

        Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"
        If Not (Directory.Exists(genReportPath)) Then
            Directory.CreateDirectory(genReportPath)
        End If
        Dim file_name As String = "Month_Year_Wise_Unit_Despatch_Report" + DateString + ".xlsx"
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

    End Sub
    'Protected Sub ddlproductcat_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    PopulateProducts()
    'End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateDepotDropdown()
    End Sub
End Class
