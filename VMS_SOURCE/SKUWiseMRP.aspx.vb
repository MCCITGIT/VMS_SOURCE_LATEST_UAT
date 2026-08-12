
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports VMS.Web

Partial Class SKUWiseMRP
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CheckLogin()
        btnSubmit.OnClientClick = "clearNotification();"
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
        Try
            If (userInfo.userGroupCodeEntity.Equals("HO-MARKETING") Or userInfo.userGroupCodeEntity.Equals("SYSADMIN") Or userInfo.userGroupCodeEntity.Equals("UNIT")) Then
                ds = GetDsForExcel()

                If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                    If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                        ExportToExcel(ds)
                    Else
                        lblErrMsg.Text = "No Data Found..."
                    End If
                Else
                    lblErrMsg.Text = "No Data Found..."
                End If
            Else
                lblErrMsg.Text = "You are not allowed to download this report."
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorExporttoExcel
            Server.Transfer(returnUrl)
        End Try


    End Sub

    Private Function GetDsForExcel() As DataSet
        Dim ds As New System.Data.DataSet
        Dim obj As New SKUWiseMRPClasss
        Try
            ds = obj.GetSkuWiseMRPList(userInfo.userGroupCodeEntity, userInfo.userIDEntity)
        Catch ex As Exception
            Console.Write(ex)
        End Try
        Return ds
    End Function

    Private Sub ExportToExcel(ds As DataSet)

        If (ds.Tables(0).Rows.Count > 0) Then


            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\SKU_WISE_MRP_Templete.xls", FileMode.Open, FileAccess.Read)

            Dim WorkBook As HSSFWorkbook = New HSSFWorkbook(fs, True)
            Dim ReportSheet As HSSFSheet = WorkBook.GetSheet("Sheet1")

            Dim Row As HSSFRow
            Dim Cell As HSSFCell

            Dim alignLeft As HSSFCellStyle = WorkBook.CreateCellStyle()
            Dim alignRight As HSSFCellStyle = WorkBook.CreateCellStyle()
            Dim alignCenter As HSSFCellStyle = WorkBook.CreateCellStyle()
            Dim bgWhite As HSSFCellStyle = WorkBook.CreateCellStyle()

            alignLeft.Alignment = 1
            alignRight.Alignment = 3
            alignCenter.Alignment = 2
            bgWhite.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            Row = ReportSheet.GetRow(0)
            Cell = Row.GetCell(0)

            Cell.SetCellValue("Report As On - " & DateTime.Today.ToString("dd/MM/yyyy"))
            Cell = Row.GetCell(1)

            Cell.SetCellValue("SKU Wise MRP Dump")


            If (userInfo.userGroupCodeEntity.Equals("UNIT")) Then
                Cell = Row.GetCell(3)
                Cell.SetCellValue("User: " & userInfo.userIDEntity)
                Cell.CellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index
            Else
                Cell.CellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            End If

            Dim SheetRowIndex As Integer = 2

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Row = ReportSheet.CreateRow(SheetRowIndex)

                Cell = Row.CreateCell(0)
                Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("General")
                Cell.SetCellValue(ds.Tables(0).Rows(i)("sku"))
                Cell.CellStyle = alignLeft

                Cell = Row.CreateCell(1)
                Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("General")
                Cell.SetCellValue(ds.Tables(0).Rows(i)("skudesc"))
                Cell.CellStyle = alignLeft

                Cell = Row.CreateCell(2)
                Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("General")
                Cell.SetCellValue(ds.Tables(0).Rows(i)("uom"))
                Cell.CellStyle = alignCenter

                Cell = Row.CreateCell(3)
                Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("General")
                Cell.SetCellValue(ds.Tables(0).Rows(i)("packSize"))
                Cell.CellStyle = alignCenter

                Cell = Row.CreateCell(4)
                Cell.CellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("General")
                Cell.SetCellValue(ds.Tables(0).Rows(i)("mrp"))
                Cell.CellStyle = alignRight

                SheetRowIndex = SheetRowIndex + 1
            Next
            For columnIndex As Integer = 0 To 6 - 1
                    ReportSheet.AutoSizeColumn(columnIndex)
                Next
                Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

                If Not (Directory.Exists(genReportPath)) Then
                    Directory.CreateDirectory(genReportPath)
                End If

                Dim DateString As String = "_" & Format(Now, "dd-MM-yyyy_HH-mm-ss")
                Dim file_name As String = "SkuWiseMrp_" + DateString + ".xls"

                Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)

                WorkBook.Write(fl)
                fl.Close()

                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"
                Response.WriteFile(genReportPath & file_name)
                Response.AppendHeader("content-disposition", "attachment; filename=" & file_name)
            'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Flush()
                ' REMOVE TEMP FILE AFTER DOWNLOAD
                If (File.Exists(genReportPath & file_name)) Then
                    File.Delete(genReportPath & file_name)
                End If

                Response.End()

            End If
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("Home.aspx")
    End Sub
End Class
