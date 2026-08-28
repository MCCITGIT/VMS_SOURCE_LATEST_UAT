
Imports System.Data
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports VMS.Web

Partial Class RMConsumptionSupplyDetails
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
            'AddAttributes()

            Populate_Quarter()
            PopulateVendor()
            'PopulateVendorBrand(String.Empty)
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

#Region "Populate Quarter"
    Private Sub Populate_Quarter()
        Try
            Dim obj As New QualityControlClass
            Dim ds As New DataSet
            ddlQuarter.Items.Clear()
            ds = obj.Get_QuarterList(userInfo.userIDEntity)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlQuarter.DataSource = ds.Tables(0)
                ddlQuarter.DataTextField = "qm_quarter_short_code"
                ddlQuarter.DataValueField = "qm_id"
                ddlQuarter.DataBind()

                If Not (ds.Tables(0).Rows.Count = 1) Then
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region


#Region "Populate Vendor"
    Private Sub PopulateVendor()
        CheckLogin()
        Try
            'Dim obj As New vrs_legalscore_class
            Dim Obj As New QualityControlClass
            Dim ds As New DataSet
            ddlVendor.Items.Clear()
            ' ds = obj.GetVendor_DataList()
            ds = Obj.GetVendor(userInfo.userIDEntity)
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlVendor.DataSource = ds.Tables(0)
                ddlVendor.DataTextField = "vendor_name"
                ddlVendor.DataValueField = "vendor_code"
                ddlVendor.DataBind()
                ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'If Not (ds.Tables(0).Rows.Count = 1) Then
                '    ddlvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'End If

                If ds.Tables(0).Rows.Count = 1 Then
                    ddlVendor.SelectedIndex = 1
                    ddlVendor.Enabled = False
                End If

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim QualityControlClass As New QualityControlClass()

        If String.IsNullOrEmpty(ddlQuarter.Text) Then
            lblErrorMessage.Text = "Please Select Quartor."
            ddlQuarter.Focus()
            Exit Sub
        End If
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim dtCnsumeProd As DataSet
        dtCnsumeProd = QualityControlClass.GetRmConsumtionSupplyProductList(ddlVendor.SelectedValue, txtProduct.Text, userInfo.userIDEntity.ToString())

        If (Not (dtCnsumeProd Is Nothing) AndAlso dtCnsumeProd.Tables.Count > 0) Then
            If (Not (dtCnsumeProd.Tables(0) Is Nothing) AndAlso dtCnsumeProd.Tables(0).Rows.Count > 0) Then
                gvConsumption.DataSource = dtCnsumeProd
                gvConsumption.DataBind()
                ' MergeGridRows(gvConsumption)

            Else
                gvConsumption.DataSource = Nothing
                gvConsumption.DataBind()

            End If
        End If

    End Sub


    Protected Sub gvConsumption_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "ViewDetails" Then
            Dim args As String() = e.CommandArgument.ToString().Split("|"c)
            Dim vendorId As String = args(0)
            Dim productCode As String = args(1)

            Response.Redirect("RMProdutwiseConsumption.aspx?vendorid=" & vendorId & "&productCode=" & productCode)

            Dim obj As New QualityControlClass
            Dim ds As New DataSet
            ds = obj.GetRmConsumtionSupplyProductDetails(vendorId, productCode, userInfo.userIDEntity.ToString())
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                rptAllocation.DataSource = ds.Tables(0)
                rptAllocation.DataBind()
                rptSupply.DataSource = ds.Tables(0)
                rptSupply.DataBind()
                rptConsumption.DataSource = ds.Tables(0)
                rptConsumption.DataBind()
                rptRemaining.DataSource = ds.Tables(0)
                rptRemaining.DataBind()
                'gvDtls.DataSource = ds.Tables(0)
                'gvDtls.DataBind()
                'gvSupply.DataSource = ds.Tables(0)
                'gvSupply.DataBind()
                'gvConsumeDtl.DataSource = ds.Tables(0)
                'gvConsumeDtl.DataBind()
                'gvRemain.DataSource = ds.Tables(0)
                'gvRemain.DataBind()
                lbvendor.InnerHtml = Convert.ToString(ds.Tables(0).Rows(0)("vendorname"))
                lbproduct.InnerHtml = Convert.ToString(ds.Tables(0).Rows(0)("productname"))
                dispatchvol.InnerHtml = Convert.ToString(ds.Tables(0).Rows(0)("total_despatch_production_yield"))
                rmquarter.InnerHtml = Convert.ToString(ddlQuarter.SelectedItem)
                mpRmConsumtion.Show()
            Else
                'gvLyTyDetails.DataSource = Nothing
                'gvLyTyDetails.DataBind()
                'mpLYTyDetails.Hide()
                mpRmConsumtion.Hide()
            End If
        End If
    End Sub
    Protected Sub btnSampleTestedClose_Click(sender As Object, e As EventArgs)
        mpRmConsumtion.Hide()
    End Sub
    Protected Sub btnExportConsumption_Click(sender As Object, e As EventArgs)
        Dim obj As New QualityControlClass()
        Dim dsComsumption As DataSet
        dsComsumption = obj.GetRmConsumtionDetails(ddlVendor.SelectedValue, txtProduct.Text, ddlQuarter.SelectedValue)
        If (Not (dsComsumption Is Nothing) AndAlso dsComsumption.Tables.Count > 0) Then
            ExportConsumptionExcelSheet(dsComsumption)
        End If
    End Sub

    Private Sub ExportConsumptionExcelSheet(ByVal dset As DataSet)
        Try
            'Opening the Excel template...
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\RM_Consumption_Report.xlsx", FileMode.Open, FileAccess.Read)

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

            Dim sheet As XSSFSheet = templateWorkbook.GetSheet("ConsumptionDetails")
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
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vendorname")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("tc_chemical_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("production")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("consumption")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("supply")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("remaning")))
                cell.CellStyle = styleValue
                colIndex += 1


                RowsIndex = RowsIndex + 1

            Next

            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "RM_Consumption_Report" & DateString & ".xlsx"
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

    Protected Sub btnExportAllocation_Click(sender As Object, e As EventArgs)
        Dim obj As New QualityControlClass()
        Dim dsAllocation As DataSet
        dsAllocation = obj.GetRmVariationReport(ddlVendor.SelectedValue, txtProduct.Text, ddlQuarter.SelectedValue)
        If (Not (dsAllocation Is Nothing) AndAlso dsAllocation.Tables.Count > 0) Then
            ExportallocationExcelSheet(dsAllocation)
        End If
    End Sub

    Private Sub ExportallocationExcelSheet(ByVal dset As DataSet)
        Try
            'Opening the Excel template...
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\RM_AllocationVariation_Report.xlsx", FileMode.Open, FileAccess.Read)

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

            Dim sheet As XSSFSheet = templateWorkbook.GetSheet("AllocationVariation")
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
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vendorname")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("chemical_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("supplier_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("tc_alloc_qty")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("tc_alloc_price")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("srb_billed_qty")))
                cell.CellStyle = styleValue
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("remainingqty")))
                cell.CellStyle = styleValue
                colIndex += 1


                RowsIndex = RowsIndex + 1

            Next

            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "RM_AllocationVariation_Report" & DateString & ".xlsx"
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

    'Protected Sub gvConsumption_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    For i As Integer = gvConsumption.Rows.Count - 1 To 1 Step -1

    '        Dim currentRow As GridViewRow = gvConsumption.Rows(i)
    '        Dim previousRow As GridViewRow = gvConsumption.Rows(i - 1)

    '        'Check Vendor column
    '        If currentRow.Cells(1).Text = previousRow.Cells(1).Text Then

    '            'Merge SlNo
    '            If previousRow.Cells(0).RowSpan = 0 Then
    '                previousRow.Cells(0).RowSpan = 2
    '            Else
    '                previousRow.Cells(0).RowSpan += 1
    '            End If
    '            currentRow.Cells(0).Visible = False

    '            'Merge Vendor
    '            If previousRow.Cells(1).RowSpan = 0 Then
    '                previousRow.Cells(1).RowSpan = 2
    '            Else
    '                previousRow.Cells(1).RowSpan += 1
    '            End If
    '            currentRow.Cells(1).Visible = False

    '            'Merge VIEW button column (assume column index 6)
    '            If previousRow.Cells(6).RowSpan = 0 Then
    '                previousRow.Cells(6).RowSpan = 2
    '            Else
    '                previousRow.Cells(6).RowSpan += 1
    '            End If
    '            currentRow.Cells(6).Visible = False

    '        End If

    '    Next
    'End Sub



    'Protected Sub gvConsumption_RowDataBound(sender As Object, e As GridViewRowEventArgs)

    '    For i As Integer = gvConsumption.Rows.Count - 1 To 1 Step -1

    '        Dim currentRow As GridViewRow = gvConsumption.Rows(i)
    '        Dim previousRow As GridViewRow = gvConsumption.Rows(i - 1)

    '        'Check Vendor column
    '        If currentRow.Cells(1).Text = previousRow.Cells(1).Text Then

    '            'Merge SlNo
    '            If previousRow.Cells(0).RowSpan = 0 Then
    '                previousRow.Cells(0).RowSpan = 2
    '            Else
    '                previousRow.Cells(0).RowSpan += 1
    '            End If
    '            currentRow.Cells(0).Visible = False

    '            'Merge Vendor
    '            If previousRow.Cells(1).RowSpan = 0 Then
    '                previousRow.Cells(1).RowSpan = 2
    '            Else
    '                previousRow.Cells(1).RowSpan += 1
    '            End If
    '            currentRow.Cells(1).Visible = False

    '            'Merge VIEW button column (assume column index 6)
    '            If previousRow.Cells(6).RowSpan = 0 Then
    '                previousRow.Cells(6).RowSpan = 2
    '            Else
    '                previousRow.Cells(6).RowSpan += 1
    '            End If
    '            currentRow.Cells(6).Visible = False

    '        End If

    '    Next

    'End Sub
    ''' <summary>
    ''' Extracts display text from a TableCell - works with Label, Button, LiteralControl inside TemplateFields.
    ''' TableCell.Text is empty when cell contains controls; we must read from the nested control.
    ''' </summary>
    Private Function GetCellText(cell As TableCell) As String
        For Each ctrl As Control In cell.Controls
            If TypeOf ctrl Is System.Web.UI.WebControls.Label Then
                Return CType(ctrl, System.Web.UI.WebControls.Label).Text.Trim()
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.Button OrElse TypeOf ctrl Is System.Web.UI.WebControls.LinkButton Then
                Return CType(ctrl, System.Web.UI.WebControls.IButtonControl).Text.Trim()
            ElseIf TypeOf ctrl Is System.Web.UI.LiteralControl Then
                Dim litText As String = CType(ctrl, System.Web.UI.LiteralControl).Text
                If Not String.IsNullOrWhiteSpace(litText) Then Return litText.Trim()
            End If
        Next
        Return If(cell.Text, "").Trim()
    End Function

    ''' <summary>
    ''' Merges adjacent GridView cells by vendor group. Uses groupColumn (e.g. Vendor) as the grouping key -
    ''' merges mergeColumns only when consecutive rows belong to the same vendor group.
    ''' </summary>
    ''' <param name="gv">The GridView to merge</param>
    ''' <param name="mergeColumns">Column indices to merge (e.g. SlNo, Vendor, View button)</param>
    ''' <param name="groupColumn">Column index that defines the group (e.g. 1 for Vendor). Merge only when this matches.</param>
    Public Sub MergeGridViewRows(ByVal gv As GridView, ByVal mergeColumns As Integer(), ByVal groupColumn As Integer)
        If gv.Rows.Count < 2 Then Return
        If groupColumn >= gv.Rows(0).Cells.Count Then Return

        For rowIndex As Integer = gv.Rows.Count - 2 To 0 Step -1
            Dim row As GridViewRow = gv.Rows(rowIndex)
            Dim nextRow As GridViewRow = gv.Rows(rowIndex + 1)

            ' Merge only when grouping column (Vendor) matches - ensures SlNo and View merge by vendor group
            If GetCellText(row.Cells(groupColumn)) <> GetCellText(nextRow.Cells(groupColumn)) Then Continue For

            For Each col As Integer In mergeColumns
                If col >= row.Cells.Count OrElse col >= nextRow.Cells.Count Then Continue For

                Dim nextCell As TableCell = nextRow.Cells(col)
                If nextCell.RowSpan < 2 Then
                    row.Cells(col).RowSpan = 2
                Else
                    row.Cells(col).RowSpan = nextCell.RowSpan + 1
                End If
                nextCell.Visible = False
            Next
        Next
    End Sub

    Protected Sub gvConsumption_DataBound(sender As Object, e As EventArgs)
        ' Set SlNo by vendor group (1, 2, 3... per vendor) so it merges correctly
        Dim groupIndex As Integer = 0
        Dim prevVendor As String = ""
        For Each gvRow As GridViewRow In gvConsumption.Rows
            Dim vendorText As String = GetCellText(gvRow.Cells(1))
            If vendorText <> prevVendor Then
                groupIndex += 1
                prevVendor = vendorText
            End If
            Dim lblSlNo As Label = TryCast(gvRow.FindControl("lblbrandid"), Label)
            If lblSlNo IsNot Nothing Then lblSlNo.Text = groupIndex.ToString()
        Next

        ' Merge SlNo, Vendor, View by vendor group (groupColumn 1 = Vendor)
        MergeGridViewRows(gvConsumption, New Integer() {0, 1, 6}, 1)

    End Sub
End Class
