
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.Util
Imports VMS.DataAccess
Imports VMS.Web
Imports NPOI.HSSF.UserModel

Partial Class VRSComplaintsAcknowledge
    Inherits System.Web.UI.Page
#Region "Page_Load Event"
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        CheckLogin()
        If Not IsPostBack Then
            'PopulateQuarter()
            PopulateFinYear()
            PopulateVendor()
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

#Region "Populate Fin Year"
    Private Sub PopulateFinYear()
        CheckLogin()
        Try
            Dim Obj As New vrs_legalscore_class
            Dim ds As New DataSet
            ddlFinYear.Items.Clear()
            ds = Obj.GetFinYear(userInfo.userIDEntity)
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlFinYear.DataSource = ds.Tables(0)
                ddlFinYear.DataTextField = "fin_year_text"
                ddlFinYear.DataValueField = "fin_year"
                ddlFinYear.DataBind()
                ddlFinYear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'If Not (ds.Tables(0).Rows.Count = 1) Then
                '    ddlvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'End If

                If ds.Tables(0).Rows.Count = 1 Then
                    ddlFinYear.SelectedIndex = 1
                    ddlFinYear.Enabled = False
                    PopulateQuarter()
                End If

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub
#End Region

#Region "Populate Vendor dropdown."

    Private Sub PopulateVendor()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New vrsComplaintClass()
        Dim ds As DataSet

        Try

            ds = obj.GetComplaintsVendorPopulateDropdown(userInfo.userIDEntity, ddlQuarter.SelectedValue)

            If Not (ds Is Nothing) Then

                If Not (ds.Tables(0).Rows.Count = 0) Then

                    ddlVendor.DataSource = ds
                    ddlVendor.DataTextField = "vendor_name"
                    ddlVendor.DataValueField = "vendor_code"
                    ddlVendor.DataBind()

                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))

                    If ds.Tables(0).Rows.Count = 1 Then
                        ddlVendor.SelectedIndex = 1
                        ddlVendor.Enabled = False
                        BindGrid()
                    End If

                Else
                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate Quarter dropdown."

    Private Sub PopulateQuarter()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New vrs_legalscore_class()
        Dim ds As DataSet

        Try
            'ds = obj.GetQuarterDetails(userInfo.userIDEntity)
            ds = obj.Get_QuarterList_vr1(userInfo.userIDEntity, ddlFinYear.SelectedValue)

            If Not (ds Is Nothing) Then

                If Not (ds.Tables(0).Rows.Count = 0) Then

                    ddlQuarter.DataSource = ds
                    ddlQuarter.DataTextField = "qm_quarter_short_code"
                    ddlQuarter.DataValueField = "qm_id"
                    ddlQuarter.DataBind()
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                    If (ds.Tables(0).Rows.Count > 0) Then
                        For Each row As DataRow In ds.Tables(0).Rows
                            Dim currentquarter As String = row("qm_current_quarter").ToString()
                            'If currentquarter = "Y" Then
                            '    ddlQuarter.SelectedValue = row("qm_id").ToString()
                            '    PopulateVendor()
                            '    ddlQuarter.Enabled = False
                            'End If
                        Next
                    End If
                Else
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub
    Private Sub BindGrid()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New vrsComplaintClass()
        Dim ds As DataSet
        'If String.IsNullOrEmpty(ddlVendor.SelectedValue) Then
        '    lblErrorMessage.Text = "Please select a vendor"
        '    lblErrorMessage.ForeColor = System.Drawing.Color.Red
        '    lblErrorMessage.Font.Size = 10
        'End If

        If String.IsNullOrEmpty(ddlQuarter.SelectedValue) Then
            lblErrorMessage.Text = "Please select a quarter"
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Font.Size = 10
        End If

        If Not String.IsNullOrEmpty(ddlQuarter.SelectedValue) Then
            ds = obj.GetComplaintsDetails(ddlVendor.SelectedValue, Convert.ToInt64(ddlQuarter.SelectedValue))
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    gvAuditList.DataSource = ds
                    gvAuditList.DataBind()
                    ddlQuarter.Enabled = False
                    ddlVendor.Enabled = False
                    lblErrorMessage.Text = String.Empty
                Else
                    gvAuditList.DataSource = Nothing
                    gvAuditList.DataBind()
                    lblErrorMessage.Text = String.Empty
                End If
            End If

        End If

    End Sub

#End Region

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Home.aspx")
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid()
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim path = "~/VRSComplaintsAcknowledge.aspx"
        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub
    Protected Sub ddlQuarter_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim quarter As String = Convert.ToString(ddlQuarter.SelectedValue)
        If quarter IsNot Nothing AndAlso Not String.IsNullOrEmpty(quarter) Then
            PopulateVendor()
        End If
    End Sub

    Protected Sub btnComplaintsClosePopup_Click(sender As Object, e As EventArgs)
        mpComplaints.Hide()
    End Sub
    Protected Sub gvAuditList_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "ViewDetails" Then
            Dim row As GridViewRow = CType((CType(e.CommandSource, Control)).NamingContainer, GridViewRow)
            Dim rowIndex As Integer = row.RowIndex
            Dim UnitCode As String = (TryCast(gvAuditList.Rows(rowIndex).FindControl("hdnUnit"), HiddenField)).Value
            Dim QuarterId As String = (TryCast(gvAuditList.Rows(rowIndex).FindControl("hdnQuarter"), HiddenField)).Value
            If Not String.IsNullOrEmpty(UnitCode) AndAlso Not String.IsNullOrEmpty(QuarterId) Then
                Dim obj As New vrsComplaintClass()
                Dim ds As DataSet
                ds = obj.GetComplaintsIndivisualDetails(UnitCode, Convert.ToInt64(QuarterId))
                If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                    If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                        gvComplaintsDtls.DataSource = ds
                        gvComplaintsDtls.DataBind()
                        mpComplaints.Show()
                    Else
                        gvComplaintsDtls.DataSource = Nothing
                        gvComplaintsDtls.DataBind()
                    End If
                End If
            End If
        End If
    End Sub


    Protected Sub gvAuditList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim totalComplaint As Int32 = Convert.ToInt32(drv("vcd_total_complaints"))
            Dim btnView As LinkButton = CType(e.Row.FindControl("btnView"), LinkButton)
            If totalComplaint = 0 Then
                btnView.Visible = False
            End If
        End If
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Dim obj As New vrsComplaintClass()
        Dim dsAudit As DataSet
        dsAudit = obj.GetComplaintsDetails(ddlVendor.SelectedValue, ddlQuarter.SelectedValue)
        If (Not (dsAudit Is Nothing) AndAlso dsAudit.Tables.Count > 0) Then
            ExportToExcelSheet(dsAudit)
        End If

    End Sub

    Private Sub ExportToExcelSheet(ByVal dset As DataSet)
        Try
            'Opening the Excel template...
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\Complaints_Detail_Report.xlsx", FileMode.Open, FileAccess.Read)

            'Getting the complete workbook...
            Dim templateWorkbook As XSSFWorkbook = New XSSFWorkbook(fs)

            Dim font3 As IFont = templateWorkbook.CreateFont()
            font3.Color = HSSFColor.Black.Index
            font3.FontName = "Calibri"
            font3.FontHeightInPoints = 10

            Dim headerStyle As ICellStyle = templateWorkbook.CreateCellStyle()
            headerStyle.SetFont(font3)
            headerStyle.FillPattern = FillPattern.SolidForeground
            headerStyle.FillForegroundColor = IndexedColors.BlueGrey.Index ' or any other color
            headerStyle.Alignment = HorizontalAlignment.Center
            headerStyle.VerticalAlignment = VerticalAlignment.Center



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
            styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")


            Dim styleValue1 As ICellStyle = templateWorkbook.CreateCellStyle()
            styleValue1.SetFont(font3)
            styleValue1.BorderRight = BorderStyle.Thin
            styleValue1.BorderBottom = BorderStyle.Thin
            styleValue1.BorderTop = BorderStyle.Thin
            styleValue1.BorderLeft = BorderStyle.Thin
            styleValue1.VerticalAlignment = VerticalAlignment.Center
            styleValue1.Alignment = HorizontalAlignment.Right
            styleValue1.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0 ;_ * -#,##0_ ;_ * ""-""??_ ;_ @_ ")




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

            Dim sheet As XSSFSheet = templateWorkbook.GetSheet("ComplaintsDetails")
            Dim RowsIndex As Integer
            Dim DateString As String = "_" & DateTime.Today.ToString("dd_MM_yyyy")

            Dim row As XSSFRow
            Dim cell As XSSFCell


            row = sheet.GetRow(0)
            cell = row.GetCell(0)
            cell.SetCellValue("Report as on - " + DateTime.Today.ToString("dd/MM/yyyy"))
            'cell.CellStyle = headerStyle

            RowsIndex = 2
            Dim count = 0
            Dim colIndex As Integer = 0

            Dim dt As DataTable = dset.Tables(0)

            For i = 0 To dset.Tables(0).Rows.Count - 1


                row = sheet.CreateRow(RowsIndex)
                colIndex = 0

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("srl_no")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("unit_name")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vcd_monthly_avg_vol")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vcd_total_complaints")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vcd_total_justified_complaints")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("unjustified_complaints")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vch_complaint_tendency_ratio")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vch_total_max_score")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vch_total_obtain_score")))
                cell.CellStyle = styleValue
                colIndex += 1

                RowsIndex = RowsIndex + 1

            Next

            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "Complaint_details_Report" & DateString & ".xlsx"
            Dim fl As FileStream = New FileStream(genReportPath & file_name, FileMode.Create)
            templateWorkbook.Write(fl)
            fl.Close()
            Response.Clear()
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



    Protected Sub ddlFinYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(ddlFinYear.SelectedValue) Then
            ddlQuarter.Items.Clear()
        Else
            PopulateQuarter()
        End If
    End Sub
End Class
