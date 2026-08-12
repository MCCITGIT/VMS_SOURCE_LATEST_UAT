Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions
Imports System.Security.Permissions
Imports Microsoft.Win32
Imports System.IO
Imports System.Globalization
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.Util
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.XSSF.UserModel

Partial Class OCSpecificationReport
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture())
            txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture())
            PopulateVender()
            PopulateProducts()
            ''BindGrid(ddlVender.SelectedValue, txtFromDate.Text, txtTodate.Text, ddlproduct.SelectedValue)
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
    Public Sub PopulateVender()
        Dim ds As DataSet
        Try
            Dim StockObj As New UnitDespatchClass
            ds = StockObj.GetUnit(String.Empty, Constant.Common.ActiveStatus)
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlVender.DataSource = ds.Tables(0)
                ddlVender.DataTextField = "unit_name"
                ddlVender.DataValueField = "unit_code"
                ddlVender.DataBind()
                ddlVender.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            Else
                ddlVender.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
            If (userInfo.userGroupCodeEntity = "UNIT") Then
                ddlVender.SelectedValue = userInfo.userBranchEntity
                ddlVender.Enabled = False
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub
    Public Sub PopulateProducts()
        Dim mstr As New OCSpecification
        Dim ds As New DataSet
        Dim LovType As String = "OCS_PRODUCTS"
        ds = mstr.GetProdDetails(userInfo.userIDEntity)
        Try
            ds = mstr.GetProdDetails(userInfo.userIDEntity)
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlproduct.DataSource = ds.Tables(0)
                ddlproduct.DataTextField = "lov_value"
                ddlproduct.DataValueField = "lov_code"
                ddlproduct.DataBind()
                ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            Else
                ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
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
    'Protected Sub GetReportData(ByVal vender As String, ByVal FromDate As String, ByVal Todate As String, ByVal Product As String)
    '    Dim Fdate As SqlDateTime
    '    Dim Tdate As SqlDateTime
    '    Fdate = FormatDate(FromDate)
    '    Tdate = FormatDate(Todate)
    '    CheckLogin()
    '    Try
    '        Dim ocspecificationds As New DataSet
    '        Dim objOCSpecification As New OCSpecification
    '        ocspecificationds = objOCSpecification.OCSpecificationReport(vender, Fdate, Tdate, Product)
    '        If (Not (ocspecificationds Is Nothing) AndAlso ocspecificationds.Tables.Count > 0 AndAlso Not (ocspecificationds.Tables(0) Is Nothing) AndAlso ocspecificationds.Tables(0).Rows.Count > 0) Then

    '        Else

    '        End If
    '    Catch ex As Exception
    '        Dim returnUrl As String = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorBindingGrid
    '        Server.Transfer(returnUrl)
    '    End Try
    'End Sub


    'Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
    '    Try
    '        Dim ds As DataSet
    '        Dim ocspecificationds As New DataSet
    '        Dim Obj As Common = New Common()
    '        ds = Obj.GetLovDetails("BERGER", "OCS_PRODUCTS", "Y")
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

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        Try
            Dim ds As DataSet
            Dim ocspecificationds As New DataSet
            Dim Obj As Common = New Common()
            ds = Obj.GetLovDetails("BERGER", "OCS_PRODUCTS", "Y")
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

    Public Function FormatDateToString(ByVal stringdate As String) As String
        Dim FromDt As String
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
            Dim dd As String = arrlist.Item(0)
            Dim mm As String = arrlist.Item(1)
            Dim yyyy As String = arrlist.Item(2)
            FromDt = yyyy.ToString() + "-" + mm.ToString() + "-" + dd.ToString()

            'Dim dt As DateTime = New DateTime(yyyy, mm, dd)
            'dt = FormatDateTime(dt, DateFormat.LongDate)
            Return FromDt
        End If
    End Function
    Private Sub ExportToExcel(ds As DataSet)
        Dim objOCSpecification As New OCSpecification
        Dim ms As MemoryStream = New MemoryStream()
        Dim WorkBook As HSSFWorkbook = New HSSFWorkbook()
        Dim dsresult As DataSet
        Dim FromDate As String = FormatDateToString(txtFromDate.Text)
        Dim Todate As String = FormatDateToString(txtTodate.Text)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim ProductCode As String = ds.Tables(0).Rows(i)("lov_code").ToString()
            dsresult = objOCSpecification.OCSpecificationReport(ddlVender.SelectedValue.ToString(), FromDate, Todate, ProductCode)

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

                        dataStyleCenter.Alignment = HorizontalAlignment.CENTER
                        dataStyleLeft.Alignment = HorizontalAlignment.LEFT
                        dataStyleRight.Alignment = HorizontalAlignment.RIGHT
                        topHeaderStyle.Alignment = HorizontalAlignment.LEFT
                        topHeaderStyle.VerticalAlignment = VerticalAlignment.Center
                        topHeaderStyle.FillForegroundColor = HSSFColor.LightCornflowerBlue.Index
                        topHeaderStyle.FillPattern = FillPattern.SolidForeground

                        topHeaderStyle1.Alignment = HorizontalAlignment.CENTER
                        topHeaderStyle1.VerticalAlignment = VerticalAlignment.Center
                        topHeaderStyle1.FillForegroundColor = HSSFColor.LightGreen.Index
                        topHeaderStyle1.FillPattern = FillPattern.SolidForeground

                        alignLeft.Alignment = HorizontalAlignment.LEFT
                        alignRight.Alignment = HorizontalAlignment.RIGHT
                        alignCenter.Alignment = HorizontalAlignment.CENTER
                        alignCenter.VerticalAlignment = VerticalAlignment.CENTER
                        alignCenterBoldText.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN
                        alignCenterBoldText.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN
                        alignCenterBoldText.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN
                        alignCenterBoldText.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN
                        alignCenterBoldText.VerticalAlignment = VerticalAlignment.CENTER
                        alignCenterBoldText.Alignment = HorizontalAlignment.CENTER

                        Dim headerfont = WorkBook.CreateFont()
                        headerfont.FontHeightInPoints = 11
                        headerfont.FontName = "Calibri"
                        headerfont.Boldweight = CShort(NPOI.SS.UserModel.FontBoldWeight.BOLD)
                        topHeaderStyle.SetFont(headerfont)
                        topHeaderStyle1.SetFont(headerfont)
                        Dim font = WorkBook.CreateFont()
                        font.FontHeightInPoints = 11
                        font.FontName = "Calibri"
                        font.Boldweight = CShort(NPOI.SS.UserModel.FontBoldWeight.BOLD)
                        alignCenterBoldText.SetFont(font)
                        alignCenterBoldText.IsLocked = True

                        Row = CType(ReportSheet.CreateRow(0), HSSFRow)
                        Cell = CType(Row.CreateCell(0), HSSFCell)
                        Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                        Cell.SetCellValue("REPORT AS ON - " & DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                        Cell.CellStyle = topHeaderStyle

                        'Cell = CType(Row.CreateCell(1), HSSFCell)
                        'Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                        'Cell.SetCellValue("")
                        'Cell.CellStyle = topHeaderStyle

                        Cell = CType(Row.CreateCell(2), HSSFCell)
                        Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                        Cell.SetCellValue("QC Specification Report")
                        Cell.CellStyle = topHeaderStyle1

                        Dim lastColoum As Int16 = dsresult.Tables(0).Columns.Count - 1
                        Dim cra = New NPOI.SS.Util.CellRangeAddress(0, 0, 0, 1)
                        Dim cra1 = New NPOI.SS.Util.CellRangeAddress(0, 0, 2, lastColoum)
                        ReportSheet.AddMergedRegion(cra)
                        ReportSheet.AddMergedRegion(cra1)

                        Row = CType(ReportSheet.CreateRow(1), HSSFRow)
                        Dim SheetRowIndex As Integer = 2
                        Dim colIndex As Integer = 0
                        For j = 0 To dsresult.Tables(0).Columns.Count - 1
                            Cell = CType(Row.CreateCell(j), HSSFCell)
                            Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@")
                            Cell.SetCellValue(dsresult.Tables(0).Columns(j).ColumnName)
                            Cell.CellStyle = alignCenterBoldText
                            Cell.CellStyle.Alignment = HorizontalAlignment.CENTER
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
        Dim file_name As String = "QC_SpecificationListReport" & DateString & ".xls"
        Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)
        WorkBook.Write(fl)
        fl.Close()

        Response.Clear()
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"

        Response.WriteFile(genReportPath & file_name)
        Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
    End Sub

End Class
