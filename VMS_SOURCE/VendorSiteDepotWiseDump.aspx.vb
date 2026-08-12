Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
'Imports NPOI.HSSF.UserModel
'Imports NPOI.SS.UserModel
'Imports ClosedXML.Excel
Imports System.Globalization
'Imports Microsoft.Office.Interop.Excel
Imports NPOI.Util.Collections
Imports NPOI.SS.Util
Imports System.Data.OleDb
Imports AjaxControlToolkit
Imports VMS.DataAccess
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Partial Class VendorSiteDepotWiseDump
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Check Login"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Request.QueryString("NoData") = "Yes") Then
            lblErrMsg.Text = "No record found!"
        Else
            lblErrMsg.Text = String.Empty
        End If
        CheckLogin()
        If Not IsPostBack Then
            PopulateVendor()
        End If
    End Sub
    Private Sub PopulateVendor()
        Dim UnitDespatch As New MonthlyUnitDespatch
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, String.Empty)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If ddlUnit.SelectedValue.Trim = String.Empty Then
            lblErrMsg.Text = "Please Select Vendor."
            lblErrMsg.ForeColor = System.Drawing.Color.Red
            ddlUnit.Focus()
            Exit Sub
        End If

        Dim VendorCode As String = ddlUnit.SelectedValue
        Dim Obj As New VendorSiteDepotWiseDumpClass
        Dim ExcelSet As DataSet
        ExcelSet = Obj.GetVendorSiteDepotWiseDumpReport(VendorCode, userInfo.userIDEntity)
        If (ExcelSet.Tables(0).Rows.Count > 0) Then
            ExportToExcel(ExcelSet)
        End If
    End Sub
    Private Sub ExportToExcel(dset As DataSet)

        'Opening the Excel template...
        Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\VendorSiteDepotWiseDumpReport.xlsx", FileMode.Open, FileAccess.Read)

        'Getting the complete workbook...
        Dim templateWorkbook As XSSFWorkbook = New XSSFWorkbook(fs)

        Dim font3 As IFont = templateWorkbook.CreateFont()
        font3.Color = HSSFColor.Black.Index
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

        Dim sheet As XSSFSheet = templateWorkbook.GetSheet("Sheet1")

        Dim row As XSSFRow
        Dim cell As XSSFCell

        row = sheet.GetRow(0)
        cell = row.GetCell(0)
        cell.SetCellValue("Report Date - " & DateTime.Today.ToString("dd/MM/yyyy"))

        row = sheet.GetRow(0)
        cell = row.GetCell(2)
        cell.SetCellValue("VENDOR SITE DEPOT WISE DUMP REPORT")
        Dim RowIndex As Integer = 0
        RowIndex = 2

        Dim dt As DataTable = dset.Tables(0)

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1

                row = sheet.CreateRow(RowIndex)

                cell = row.CreateCell(0)
                cell.SetCellValue(Convert.ToString(dt.Rows(i)("unit_code")))
                cell.CellStyle = styleCenter

                cell = row.CreateCell(1)
                If dt.Rows(i)("unit_name") Is DBNull.Value Then
                    cell.SetCellValue(String.Empty)
                Else
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("unit_name")))
                End If

                cell.CellStyle = styleLeft

                cell = row.CreateCell(2)
                If dt.Rows(i)("pd_vendor_site_id") Is DBNull.Value Then
                    cell.SetCellValue(String.Empty)
                Else
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("pd_vendor_site_id")))
                End If
                cell.CellStyle = styleCenter

                cell = row.CreateCell(3)
                If dt.Rows(i)("pd_vendor_site_code") Is DBNull.Value Then
                    cell.SetCellValue(String.Empty)
                Else
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("pd_vendor_site_code")))
                End If
                cell.CellStyle = styleLeft

                cell = row.CreateCell(4)
                If dt.Rows(i)("pd_org_code") Is DBNull.Value Then
                    cell.SetCellValue(String.Empty)
                Else
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("pd_org_code")))
                End If

                cell.CellStyle = styleCenter

                cell = row.CreateCell(5)
                If dt.Rows(i)("DepotName") Is DBNull.Value Then
                    cell.SetCellValue(String.Empty)
                Else
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("DepotName")))
                End If
                cell.CellStyle = styleLeft

                cell = row.CreateCell(6)
                If dt.Rows(i)("pd_sku") Is DBNull.Value Then
                    cell.SetCellValue(String.Empty)
                Else
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("pd_sku")))
                End If
                cell.CellStyle = styleCenter

                cell = row.CreateCell(7)
                If dt.Rows(i)("sku_desc") Is DBNull.Value Then
                    cell.SetCellValue(String.Empty)
                Else
                    cell.SetCellValue(Convert.ToString(dt.Rows(i)("sku_desc")))
                End If
                cell.CellStyle = styleLeft

                RowIndex = RowIndex + 1

            Next
        End If

        Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"
        If Not (Directory.Exists(genReportPath)) Then
            Directory.CreateDirectory(genReportPath)
        End If
        Dim DateString As String = "_" & Format(Now, "dd-MM-yyyy_HH-mm-ss")
        Dim file_name As String = "VendorSiteDepotWiseDumpReport" + DateString + ".xlsx"

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
    End Sub
End Class
