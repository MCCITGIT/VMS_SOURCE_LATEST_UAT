
Imports System.Data
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports VMS.Web

Partial Class vrs_FGQualityList
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
            AddAttributes()
            PopulateQuarter()
            PopulateVendor()

            RetrieveSearchCriteria()
            'bindGrid()

            'If Not (String.IsNullOrEmpty(Request.QueryString("id"))) Then
            '    hdnId.Value = Request.QueryString("id")
            '    If Val(hdnId.Value) > 0 Then
            '        loadData(Val(hdnId.Value))
            '        btnBack.PostBackUrl = "TestCaseTestResultList.aspx"
            '    End If
            'Else
            '    btnBack.PostBackUrl = "Home.aspx"
            'End If
        End If

    End Sub

    Public Sub loadData(ByVal Id As Int64)
        CheckLogin()
        Try
            Dim obj As New DeviationsFGQualityClass
            Dim dsProductSet As New DataSet
            Dim RequisitionId As Integer = 0
            dsProductSet = obj.GetTestResultHdrById(Id)

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0) Then
                If (Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                    hdnId.Value = dsProductSet.Tables(0).Rows(0)("hdr_id")
                    ddlVendor.SelectedValue = dsProductSet.Tables(0).Rows(0)("vendor_id")
                    ddlVendor_SelectedIndexChanged(Nothing, Nothing)
                    ddlBrand.SelectedValue = dsProductSet.Tables(0).Rows(0)("brand_id")
                    'ddlBrand_SelectedIndexChanged(Nothing, Nothing)
                    'ddlProduct.SelectedValue = dsProductSet.Tables(0).Rows(0)("product_id")
                    'txtShade.Text = dsProductSet.Tables(0).Rows(0)("shade")
                    'txtBatchNo.Text = dsProductSet.Tables(0).Rows(0)("batch_no")
                    'txtBatchDate.Text = dsProductSet.Tables(0).Rows(0)("batch_date")

                    ddlVendor.Enabled = False
                    ddlBrand.Enabled = False
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub

#End Region

#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()

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

#Region "Populate Dropdown"
    Private Sub PopulateQuarter()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New VRSAuditClass()
        Dim ds As DataSet
        Try
            ds = obj.GetQuarterDetails(userInfo.userIDEntity)
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
    Private Sub PopulateVendor()
        CheckLogin()
        Try
            Dim obj As New DeviationsFGQualityClass
            Dim dsUnitSet As New DataSet
            ddlVendor.Items.Clear()
            dsUnitSet = obj.GetAcknowledgeVendor(userInfo.userIDEntity, ddlQuarter.SelectedValue)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendor.DataSource = dsUnitSet.Tables(0)
                ddlVendor.DataTextField = "vendor_name"
                ddlVendor.DataValueField = "vendor_code"
                ddlVendor.DataBind()
                ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                If dsUnitSet.Tables(0).Rows.Count = 1 Then
                    ddlVendor.SelectedIndex = 1
                    ddlVendor.Enabled = False
                End If

                ddlVendor_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
    Private Sub PopulateVendorBrand(ByVal vendorCode As String)
        CheckLogin()
        Try
            Dim obj As New DeviationsFGQualityClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetVendorBrand(vendorCode, userInfo.userIDEntity)
            ddlBrand.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlBrand.DataSource = dsUnitSet.Tables(0)
                ddlBrand.DataTextField = "brand_name"
                ddlBrand.DataValueField = "brand_id"
                ddlBrand.DataBind()
                ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            Else
                ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'ddlBrand_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub

    'Private Sub PopulateVendorBrandProduct(ByVal vendorCode As String, ByVal brandId As String)
    '    CheckLogin()
    '    Try
    '        Dim obj As New QualityControlClass
    '        Dim dsUnitSet As New DataSet

    '        dsUnitSet = obj.GetVendorBrandProduct(vendorCode, brandId, userInfo.userIDEntity)
    '        ddlProduct.Items.Clear()
    '        If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
    '            ddlProduct.DataSource = dsUnitSet.Tables(0)
    '            ddlProduct.DataTextField = "prd_desc"
    '            ddlProduct.DataValueField = "prd_code"
    '            ddlProduct.DataBind()
    '            If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
    '                ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim returnUrl As String = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '        Server.Transfer(returnUrl)
    '    End Try

    'End Sub

#End Region

#Region "Bind Grid"
    Private Sub bindGrid()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim userGroup As String
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
            userGroup = userInfo.userGroupCodeEntity
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If


        Dim LovDetailsGet As New DeviationsFGQualityClass
        Dim LovDetailsList As DataSet
        Dim lovtype As String
        lovtype = ddlBrand.SelectedValue
        LovDetailsList = LovDetailsGet.GetFGQualityList((ddlBrand.SelectedValue), ddlQuarter.SelectedValue, ddlVendor.SelectedValue)
        If (Not (LovDetailsList Is Nothing) AndAlso LovDetailsList.Tables.Count > 0) Then
            If (Not (LovDetailsList.Tables(0) Is Nothing) AndAlso LovDetailsList.Tables(0).Rows.Count > 0) Then
                gvTesthdrList.DataSource = LovDetailsList.Tables(0)
                gvTesthdrList.DataBind()

            Else
                gvTesthdrList.DataSource = Nothing
                gvTesthdrList.DataBind()

            End If
        End If
    End Sub
#End Region

    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateVendorBrand(ddlVendor.SelectedValue)
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        bindGrid()
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Home.aspx", True)
    End Sub
    Protected Sub gvTesthdrList_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim obj As New Vendor_RatingClass
        Dim ds As DataSet
        If e.CommandName = "ViewProductDetails" Then

            Dim args As String() = e.CommandArgument.ToString().Split("|"c)

            If args.Length = 5 Then
                Dim vendorId As String = args(0)
                Dim brandID As String = args(1)
                Dim Quarter As String = args(2)
                Dim Productcode As String = args(3)
                Dim skucode As String = args(4)
                'Response.Redirect("vrs_DeviationsFGQualityDtls.aspx?vendorid=" & vendorId&"")
                Response.Redirect("vrs_DeviationsFGQualityDtls.aspx?vendorid=" & vendorId & "&brandid=" & brandID & "&quarter=" & Quarter & "&product=" & Productcode & "&skucode=" & skucode)

            End If
        End If
    End Sub

    Private Sub SaveSearchCriteria()
        CheckLogin()

        Session(Constant.SessionKeys.tcfgQualitySearchInfo) = Nothing
        Dim tcfgQualitySearchInfo As New tcfgQualitySearchEntity
        tcfgQualitySearchInfo.Vendor = ddlVendor.SelectedValue
        tcfgQualitySearchInfo.Brand = ddlBrand.SelectedValue
        tcfgQualitySearchInfo.Quarter = ddlQuarter.SelectedValue


        Session(Constant.SessionKeys.tcfgQualitySearchInfo) = tcfgQualitySearchInfo

    End Sub

    Private Sub RetrieveSearchCriteria()

        If (Not (Session(Constant.SessionKeys.tcTestresultSearchInfo) Is Nothing)) Then

            Dim tcSearchInfo As New tcfgQualitySearchEntity

            tcSearchInfo = Session(Constant.SessionKeys.tcfgQualitySearchInfo)
            ddlVendor.SelectedValue = tcSearchInfo.Vendor
            ddlBrand.SelectedValue = tcSearchInfo.Brand
            ddlQuarter.SelectedValue = tcSearchInfo.Quarter



        End If

        SaveSearchCriteria()

    End Sub

    Protected Sub ddlQuarter_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateVendor()
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlBrand.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select brand.');", True)
            Exit Sub
        End If
        Dim obj As New DeviationsFGQualityClass()
        Dim dsquality As DataSet
        dsquality = obj.GetFgqualityDtlsReport(Val(ddlBrand.SelectedValue), ddlQuarter.SelectedValue, ddlVendor.SelectedValue, ddlQuarter.SelectedValue)
        If (Not (dsquality Is Nothing) AndAlso dsquality.Tables.Count > 0) Then
            ExportToExcelSheet(dsquality)
        End If
    End Sub

    Private Sub ExportToExcelSheet(ByVal dset As DataSet)
        Try
            'Opening the Excel template...
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\FGQuality_Detail_Report.xlsx", FileMode.Open, FileAccess.Read)

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

            Dim sheet As XSSFSheet = templateWorkbook.GetSheet("QualityDetails")
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
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("slno")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("dfq_quarter")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("vendorname")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("brandname")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("product_code")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("sku_code")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("batch_no")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("test_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("refvalue")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("result_value")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("status")))
                cell.CellStyle = styleLeft
                colIndex += 1

                RowsIndex = RowsIndex + 1

            Next

            Dim genReportPath As String = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\"

            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "FGQuality_Detail_Report" & DateString & ".xlsx"
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

    Protected Sub gvTesthdrList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvTesthdrList.PageIndex = e.NewPageIndex
        bindGrid()
    End Sub
End Class
