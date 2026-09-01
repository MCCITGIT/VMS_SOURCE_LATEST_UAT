
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports VMS.DataAccess
Imports VMS.Web

Partial Class BulkIndentEntry_HO
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

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("IndentsList_HO.aspx", False)
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Try
            lblMsg.Text = ""
            lblMsg.ForeColor = Drawing.Color.Red
            btnConfirm.Visible = False
            lbtnDwnloadFile.Visible = False

            If Not fupUploadFile.HasFile Then
                lblMsg.Text = "Please upload an Excel file."
                Exit Sub
            End If

            Dim indntMaster As New IndentMaster()

            Dim fileExt As String = Path.GetExtension(fupUploadFile.FileName).ToLower()
            If fileExt <> ".xlsx" AndAlso fileExt <> ".xls" Then
                lblMsg.Text = "Only Excel files (.xls/.xlsx) allowed."
                Exit Sub
            End If

            Dim dt As DataTable = ReadExcelToDT(fupUploadFile.FileContent, fileExt)

            Dim dsProductSKUCodes As DataSet

            dt.Columns("Depot").ColumnName = "depot_code"
            dt.Columns("SKU Code").ColumnName = "sku_code"
            dt.Columns("Indent NOP").ColumnName = "indent_nop"
            dt.Columns("Vendor").ColumnName = "vendor"
            dt.Columns("Reason").ColumnName = "reason"
            dt.Columns.Add("flag", GetType(String))

            dsProductSKUCodes = indntMaster.GetBulkIndentList(dt)

            If (Not (dsProductSKUCodes Is Nothing)) Then
                gvIndentSKUList.Visible = True

                Session("dtIndentList") = dsProductSKUCodes.Tables(0)

                gvIndentSKUList.DataSource = dsProductSKUCodes.Tables(0)
                gvIndentSKUList.DataBind()

                btnConfirm.Visible = True

                If (dsProductSKUCodes.Tables IsNot Nothing And dsProductSKUCodes.Tables.Count > 1 And dsProductSKUCodes.Tables(1).Rows.Count > 0) Then
                    Session("dtError") = dsProductSKUCodes.Tables(1)
                    lblMsg.Text = "There are some errors in the file. To download the same please click here ->"
                    lbtnDwnloadFile.Visible = True
                End If
            End If

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        Finally
            If Not (sqlConn Is Nothing) Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Private Function ReadExcelToDT(stream As Stream, ext As String) As DataTable
        Dim workbook As IWorkbook

        If ext = ".xls" Then
            workbook = New HSSFWorkbook(stream)
        Else
            workbook = New XSSFWorkbook(stream)
        End If

        Dim sheet As ISheet = workbook.GetSheetAt(0)
        Dim dt As New DataTable()

        Dim headerRow As IRow = sheet.GetRow(0)
        For i = 0 To headerRow.LastCellNum - 1
            dt.Columns.Add(headerRow.GetCell(i).ToString())
        Next

        For r = 1 To sheet.LastRowNum
            Dim row As IRow = sheet.GetRow(r)
            If row Is Nothing Then Continue For

            Dim dr As DataRow = dt.NewRow()
            For c = 0 To dt.Columns.Count - 1
                Dim cell As ICell = row.GetCell(c)
                dr(c) = If(cell IsNot Nothing, cell.ToString(), "")
            Next

            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Sub ExportToExcel(dt As DataTable)
        Dim workbook As IWorkbook = New HSSFWorkbook()
        Dim sheet As ISheet = workbook.CreateSheet("ExportData")

        Dim header As IRow = sheet.CreateRow(0)
        For i = 0 To dt.Columns.Count - 1
            header.CreateCell(i).SetCellValue(dt.Columns(i).ColumnName)
        Next

        For r = 0 To dt.Rows.Count - 1
            Dim row As IRow = sheet.CreateRow(r + 1)
            For c = 0 To dt.Columns.Count - 1
                row.CreateCell(c).SetCellValue(dt.Rows(r)(c).ToString())
            Next
        Next

        Dim ms As New MemoryStream()
        workbook.Write(ms)

        Response.Clear()
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("Content-Disposition", "attachment; filename=BulkIndentError.xls")
        Response.BinaryWrite(ms.ToArray())
        Response.End()
    End Sub

    Protected Sub lbtnDwnloadFile_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = TryCast(Session("dtError"), DataTable)
        If dt IsNot Nothing Then
            ExportToExcel(dt)
        End If
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs)
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        CheckLogin()
        Try
            Dim indntMaster As New IndentMaster()

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            Dim dt As DataTable = TryCast(Session("dtIndentList"), DataTable)
            If dt Is Nothing Then
                lblMsg.Text = "No indent data found. Please upload the file again."
                sqlTrans.Rollback()
                Exit Sub
            End If

            dt.Columns("depot").ColumnName = "depot_code"

            dt.Columns.Remove("depot_name")
            dt.Columns.Remove("vendor_name")
            dt.Columns.Remove("sku_name")

            Dim dtReturn As DataSet = indntMaster.InsertIndentDetails_HO_Bulk(dt, userInfo.userIDEntity, sqlConn, sqlTrans)

            If (dtReturn IsNot Nothing And dtReturn.Tables.Count > 0 And Convert.ToInt32(dtReturn.Tables(0).Rows(0)("InsertedCount")) > 0) Then
                sqlTrans.Commit()
                lblMsg.Text = "Submitted Successfully"
                lblMsg.ForeColor = Drawing.Color.Green
                lbtnDwnloadFile.Visible = False
                btnConfirm.Visible = False
            Else
                sqlTrans.Rollback()
                lblMsg.Text = "Unable to submit indent data. Please verify the uploaded file."
            End If

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        Finally
            If Not (sqlConn Is Nothing) Then
                sqlConn.Close()
            End If
        End Try

    End Sub
End Class
