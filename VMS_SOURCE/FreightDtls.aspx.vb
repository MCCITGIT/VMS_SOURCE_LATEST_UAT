
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports VMS.Web
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel

Partial Class FreightDtls
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim userInfo As VMSUserEntity = New VMSUserEntity()
        'If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
        '    userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        'Else
        '    Response.Redirect("~/Login.aspx")
        'End If
        CheckLogin()

        If Not IsPostBack Then

            AddAttributes()
            PopulateUnit()
            BindUser()
            gvRequistionList.PageIndex = 0
            BindGrid()
            If (userInfo.userGroupCodeEntity.Equals("VENDOR")) Then
                ddlVendorUnit.Visible = False
                'lblTokenVendor.Visible = True
            Else
                ddlVendorUnit.Visible = True
                'lblTokenVendor.Visible = False
            End If
        End If

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

#Region "Populate Unit"
    Private Sub PopulateUnit()
        'CheckLogin()
        Try
            Dim obj As New UnitApplicableProductAssignClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetUnitName(Constant.Common.ActiveStatus)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendorUnit.DataSource = dsUnitSet.Tables(0)
                ddlVendorUnit.DataTextField = "unit_name"
                ddlVendorUnit.DataValueField = "unit_code"
                ddlVendorUnit.DataBind()
                ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))

                If (userInfo.userGroupCodeEntity.Equals("UNIT")) Then
                    ddlVendorUnit.SelectedValue = userInfo.userIDEntity
                    ddlVendorUnit.Enabled = False
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region


#Region "Bind Grid"
    Private Sub BindGrid()
        'CheckLogin()
        Try
            Dim obj As New TokenVendorRequisitionClass
            Dim dsProductSet As New DataSet
            Dim RequisitionId As Integer = 0
            'dsProductSet = obj.GetUnitVendorFreightList(ddlVendorUnit.SelectedValue)
            dsProductSet = obj.GetUnitVendorFreight_downloadList(ddlVendorUnit.SelectedValue, ddlFreight.SelectedValue)
            lblErrorMessage.Text = ""

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                tr1.Visible = True
                'tr3.Visible = True

                tr2.Visible = False
                btnSave.Visible = False
                btnReset.Visible = False

                gvRequistionList.DataSource = dsProductSet.Tables(0)
                gvRequistionList.DataBind()
                btnSubmit.Visible = True
            Else
                gvRequistionList.DataSource = Nothing
                gvRequistionList.DataBind()
                btnSubmit.Visible = False

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Populate Token Vendor List"
    Private Sub PopulateTokenVendor(ddl As DropDownList)
        'CheckLogin()
        Try
            Dim obj As New UnitApplicableVendorAssignClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetTokenVendorList(String.Empty, Constant.Common.ActiveStatus)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then

                ddl.DataSource = dsUnitSet.Tables(0)
                ddl.DataTextField = "tvm_name"
                ddl.DataValueField = "tvm_code"
                ddl.DataBind()
                If (dsUnitSet.Tables(0).Rows.Count > 0) Then
                    ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    'Protected Sub ddlVendorRequisition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorRequisition.SelectedIndexChanged

    'End Sub


    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRequistionList.PageIndexChanging
        gvRequistionList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub gvProductList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRequistionList.RowDataBound
        'If (e.Row.RowType = DataControlRowType.DataRow) Then
        '    Dim hdnStatus As HiddenField = CType(e.Row.FindControl("hdnStatus"), HiddenField)
        '    If (hdnStatus IsNot Nothing) And (Not (hdnStatus.Value.Equals(String.Empty))) Then
        '        If (hdnStatus.Value.Equals(Constant.Common.Token_Req_Status_New)) Then
        '            CType(e.Row.Cells(e.Row.Cells.Count - 1).FindControl("imgBtnReject"), ImageButton).Visible = True
        '            CType(e.Row.Cells(e.Row.Cells.Count - 1).FindControl("imgBtnReject"), ImageButton).OnClientClick = "return confirm('Are you sure to reject this?');"
        '        ElseIf (hdnStatus.Value.Equals(Constant.Common.Token_Req_Status_Rejected)) Then
        '            CType(e.Row.Cells(e.Row.Cells.Count - 1).FindControl("imgBtnReject"), ImageButton).Visible = False
        '            e.Row.BackColor = Drawing.Color.LightCoral
        '        Else
        '            CType(e.Row.Cells(e.Row.Cells.Count - 1).FindControl("imgBtnReject"), ImageButton).Visible = False
        '        End If

        '    End If
        'End If
    End Sub
    Protected Sub gvTokenVendorList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRequistionList.RowCommand
        'CheckLogin()
        If (e.CommandName.Equals("EditRequisition")) Then
            'Dim gvrow As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Response.Redirect("TokenVendorRequisitionAddUpdate.aspx?id=" & e.CommandArgument.ToString)
        End If
        If (e.CommandName.Equals("RejectRequisition")) Then
            'Dim gvrow As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblErrorMessage.Text = ""
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            Dim obj As New TokenVendorRequisitionClass

            Dim RecordInserted As Integer = 0
            Try
                Dim requisitionId As Integer = Convert.ToInt32(e.CommandArgument.ToString)

                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                RecordInserted = obj.TokenRequisitionReject(requisitionId, Constant.Common.Token_Req_Status_Rejected, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    sqlTrans.Commit()
                Else
                    sqlTrans.Rollback()
                End If
            Catch ex As Exception
                If (sqlTrans IsNot Nothing) Then
                    sqlTrans.Rollback()
                End If
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
                BindGrid()
            End Try


        End If
    End Sub
    'Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
    '    BindGrid()
    'End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        BindGrid()
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        'CheckLogin()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Dim numRowsAffected As Integer
        Dim PGObj As New TokenVendorRequisitionClass

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()

        For Each row As GridViewRow In gvRequistionList.Rows
            Dim txtFreight As TextBox = CType(row.FindControl("txtFreight"), TextBox)
            If txtFreight IsNot Nothing AndAlso txtFreight.Text <> String.Empty Then
                Dim hdnFreightID As HiddenField = CType(row.FindControl("hdnFreightID"), HiddenField)
                Dim hdnUnitCode As HiddenField = CType(row.FindControl("hdnUnitCode"), HiddenField)
                Dim hdnDepotCode As HiddenField = CType(row.FindControl("hdnDepotCode"), HiddenField)

                numRowsAffected = PGObj.FreightInsertUpdate(hdnUnitCode.Value, hdnDepotCode.Value, Convert.ToDecimal(txtFreight.Text), userInfo.userIDEntity, hdnFreightID.Value, sqlConn, sqlTrans)
            End If
        Next
        If numRowsAffected > 0 Then
            sqlTrans.Commit()
            ModalPopupExtender1.Show()
            lblMsg.Text = "Submitted Successfully"
            lblMsg.ForeColor = Drawing.Color.Green
            BindGrid()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'>window.close()</script>", False)
        Else
            sqlTrans.Rollback()
            lblErrorMessage.Text = "Error Occured!!"
        End If
        sqlConn.Close()
    End Sub

    Protected Sub imgbtnDownload_Click(sender As Object, e As EventArgs) Handles imgbtnDownload.Click
        Dim obj As New TokenVendorRequisitionClass
        Dim dsProductSet As New DataSet
        Try
            'dsProductSet = obj.GetUnitVendorFreight_ExcelList(ddlVendorUnit.SelectedValue)
            dsProductSet = obj.GetUnitVendorFreight_downloadList(ddlVendorUnit.SelectedValue, ddlFreight.SelectedValue)

            If (dsProductSet.Tables(0).Rows.Count > 0) Then
                ExportToExcelSheet(dsProductSet)
            Else
                lblErrorMessage.Text = "No Records Found"
            End If
        Catch ex As Exception
            Dim str As String = ex.Message.ToString()
        End Try
    End Sub
    Private Sub ExportToExcelSheet(ByVal dset As DataSet)
        Try
            'Opening the Excel template......
            Dim fs As FileStream = New FileStream(Server.MapPath(Request.ApplicationPath) & "\Templates\FreightDetailsReportTemplate.xlsx", FileMode.Open, FileAccess.Read)

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
            'styleValue.DataFormat = templateWorkbook.CreateDataFormat().GetFormat("_ * #,##0.00_ ;_ * -#,##0.00_ ;_ * ""-""??_ ;_ @_ ")

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
            cell.SetCellValue("Freight Details List Report As On - " + Format(Now, "dd-MM-yyyy").ToString)

            RowsIndex = 2
            Dim count = 0
            Dim colIndex As Integer = 0

            For i = 0 To dset.Tables(0).Rows.Count - 1
                row = sheet.CreateRow(RowsIndex)
                colIndex = 0

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(i + 1)
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("v_vendor_unit")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("unit_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("v_depot")))
                cell.CellStyle = styleCenter
                colIndex += 1

                cell = row.CreateCell(colIndex)
                cell.SetCellValue(Convert.ToString(dset.Tables(0).Rows(i)("depot_name")))
                cell.CellStyle = styleLeft
                colIndex += 1

                cell = row.CreateCell(colIndex)
                If dset.Tables(0).Rows(i)("udfd_freight_dtls") Is DBNull.Value OrElse IsNothing(dset.Tables(0).Rows(i)("udfd_freight_dtls")) OrElse dset.Tables(0).Rows(i)("udfd_freight_dtls").ToString().Trim() = "" Then
                    cell.SetCellValue("")
                Else
                    cell.SetCellValue(Convert.ToDouble(dset.Tables(0).Rows(i)("udfd_freight_dtls")))
                End If

                cell.CellStyle = styleValue
                colIndex += 1

                RowsIndex = RowsIndex + 1

            Next

            Dim genReportPath As String = AppDomain.CurrentDomain.BaseDirectory & "Excel_Reports\"
            If Not (Directory.Exists(genReportPath)) Then
                Directory.CreateDirectory(genReportPath)
            End If
            Dim file_name As String = "FreightDetailsReportTemplate_" & DateString & ".xlsx"
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
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Dim noofRows As String
        Dim invalidNumericRows As New List(Of Integer)

        tr1.Visible = False
        'tr3.Visible = False

        tr2.Visible = True
        btnSave.Visible = True
        btnReset.Visible = True

        Try
            Dim dtUploadData As DataTable = New DataTable()
            dtUploadData.Columns.Add("UnitCode")
            dtUploadData.Columns.Add("Unit")
            dtUploadData.Columns.Add("DepotCode")
            dtUploadData.Columns.Add("Depot")
            dtUploadData.Columns.Add("Freight")

            Dim drUploadData As DataRow = Nothing
            Dim upload_filename As String = uploadBulkExcel.PostedFile.FileName
            Dim DocPath As String = Format(Date.Now, "dd_MM_yyyy")

            If (uploadBulkExcel.PostedFile.ContentLength = 0) Then
                'tr3.Visible = True
                ModalPopupExtender1.Show()
                lblMsg.Text = "Please select a correct excel file before proceeding."
                lblMsg.ForeColor = Drawing.Color.Red
                Return
            End If

            Dim fn As String
            Dim sysdate As String = Format(Date.Now, "dd-MM-yyyy")

            If Not uploadBulkExcel.PostedFile Is Nothing And uploadBulkExcel.PostedFile.ContentLength > 0 Then
                fn = System.IO.Path.GetFileName(uploadBulkExcel.PostedFile.FileName)
            End If
            Dim extension As String = String.Empty

            If (fn.LastIndexOf(".") >= 0) Then
                extension = fn.Substring(fn.LastIndexOf(".") + 1)
            End If

            If extension <> "xls" AndAlso extension <> "xlsx" Then
                'tr3.Visible = True
                ModalPopupExtender1.Show()
                lblMsg.Text = "Please select a correct excel file before proceeding."
                lblMsg.ForeColor = Drawing.Color.Red
                Return
            End If

            'fn = "Excel_Reports" + Now.Hour.ToString + "_" + Now.Minute.ToString + "_" + Now.Second.ToString + "." + extension
            'saveLocation = Server.MapPath("Excel_Reports") & "\" & sysdate & "\" & fn
            'If Not Directory.Exists(savefolder) Then
            '    Directory.CreateDirectory(savefolder)
            'End If
            Dim saveLocation As String
            Dim FilePath As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & userInfo.userCompanyEntity & "\" & "Freight_Docs" & "\" & DocPath
            saveLocation = FilePath & "\" & fn
            Dim savefolder As String = Server.MapPath("Excel_Reports") & "\" & sysdate

            If Not (Directory.Exists(FilePath)) Then
                Directory.CreateDirectory(FilePath)
            End If

            uploadBulkExcel.PostedFile.SaveAs(saveLocation)

            If (uploadBulkExcel.HasFile AndAlso uploadBulkExcel.PostedFile.ContentLength > 0 AndAlso (Path.GetExtension(uploadBulkExcel.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)) OrElse (Path.GetExtension(uploadBulkExcel.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))) Then

                Dim st As Stream = uploadBulkExcel.PostedFile.InputStream
                Dim fileName As String = uploadBulkExcel.FileName
                extension = Path.GetExtension(fileName)

                Dim b As BinaryReader = New BinaryReader(uploadBulkExcel.PostedFile.InputStream)
                Dim bindata As Byte() = b.ReadBytes(uploadBulkExcel.PostedFile.ContentLength)
                Dim st1 As Stream = New MemoryStream(bindata)
                Dim workbook As XSSFWorkbook = New XSSFWorkbook(st1)
                Dim sheet As XSSFSheet = TryCast(workbook.GetSheetAt(0), XSSFSheet)
                Dim cellCount As Integer = 0

                If (sheet Is Nothing) Then
                    'tr3.Visible = True
                    ModalPopupExtender1.Show()
                    lblMsg.Text = "No sheet found in Excel file."
                    lblMsg.ForeColor = Drawing.Color.Red
                    Return
                End If

                If (sheet IsNot Nothing) Then
                    Dim rowIndex As Integer = 0
                    For Each row As XSSFRow In sheet
                        If rowIndex > 1 Then
                            Dim UnitCode As String = Convert.ToString(row.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK)).Trim()
                            Dim Unit As String = Convert.ToString(row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK)).Trim()
                            Dim DepotCode As String = Convert.ToString(row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK)).Trim()
                            Dim Depot As String = Convert.ToString(row.GetCell(4, MissingCellPolicy.RETURN_NULL_AND_BLANK)).Trim()
                            Dim Freight As String = Convert.ToString(row.GetCell(5, MissingCellPolicy.RETURN_NULL_AND_BLANK)).Trim()

                            '  Ensure required fields are present 
                            'If (String.IsNullOrEmpty(Source) OrElse String.IsNullOrEmpty(DepotCode) OrElse String.IsNullOrEmpty(Freight)) Then
                            If (String.IsNullOrEmpty(UnitCode) OrElse String.IsNullOrEmpty(DepotCode)) Then
                                'tr3.Visible = True
                                ModalPopupExtender1.Show()
                                'lblMsg.Text = "Row " & (rowIndex + 1).ToString() & ": One or more required fields (Source or Depot or Freight) are empty."
                                lblMsg.Text = "One or more required fields (Source or Depot or Freight) are empty."
                                lblMsg.ForeColor = Drawing.Color.Red

                                tr2.Visible = False
                                btnSave.Visible = False
                                btnReset.Visible = False
                                btnSubmit.Visible = False

                                Return
                            End If

                            '  neumeric check present 
                            Dim freightValue As Decimal
                            If Not String.IsNullOrWhiteSpace(Freight) Then
                                If Not Decimal.TryParse(Freight, freightValue) Then
                                    'tr3.Visible = True
                                    ModalPopupExtender1.Show()
                                    lblMsg.Text = "Invalid Freight values found . Please enter numeric/decimal values only."
                                    lblMsg.ForeColor = Drawing.Color.Red

                                    tr2.Visible = False
                                    btnSave.Visible = False
                                    btnReset.Visible = False
                                    btnSubmit.Visible = False

                                    Return
                                End If
                            End If
                            If (UnitCode <> String.Empty AndAlso UnitCode <> "UnitCode" AndAlso DepotCode <> String.Empty AndAlso DepotCode <> "DepotCode" AndAlso Freight IsNot Nothing AndAlso Freight <> String.Empty) Then
                                drUploadData = dtUploadData.NewRow()

                                drUploadData(0) = UnitCode
                                drUploadData(1) = Unit
                                drUploadData(2) = DepotCode
                                drUploadData(3) = Depot
                                drUploadData(4) = Freight

                                dtUploadData.Rows.Add(drUploadData)
                                dtUploadData.AcceptChanges()
                            End If
                        End If
                        rowIndex += 1
                    Next
                End If


                If dtUploadData.Rows.Count > 0 Then
                    gdvPreview.DataSource = dtUploadData
                    gdvPreview.DataBind()
                End If
            End If

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Dim numRowsAffected As Integer
        Dim PGObj As New TokenVendorRequisitionClass

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()

        For Each row As GridViewRow In gdvPreview.Rows
            Dim lblFreight As Label = CType(row.FindControl("lblFreight"), Label)

            If lblFreight IsNot Nothing AndAlso lblFreight.Text <> String.Empty Then
                Dim hdnUnitCode As HiddenField = CType(row.FindControl("hdnUnitCode"), HiddenField)
                Dim hdnDepot As HiddenField = CType(row.FindControl("hdnDepot"), HiddenField)

                numRowsAffected = PGObj.FreightExcel_InsertUpdate(hdnUnitCode.Value, hdnDepot.Value, Convert.ToDecimal(lblFreight.Text), userInfo.userIDEntity, sqlConn, sqlTrans)
            End If
        Next

        If numRowsAffected > 0 Then
            sqlTrans.Commit()

            ModalPopupExtender2.Show()
            lbl_Msg.Text = "Submitted Successfully"
            lbl_Msg.ForeColor = Drawing.Color.Green
        Else
            sqlTrans.Rollback()
            lblErrorMessage.Text = "Error Occured!!"

            tr1.Visible = False
            'tr3.Visible = False

            tr2.Visible = True
            btnSave.Visible = True
            btnReset.Visible = True
        End If

        sqlConn.Close()
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        gdvPreview.DataSource = Nothing
        gdvPreview.DataBind()

        tr1.Visible = True
        '  tr3.Visible = True

        tr2.Visible = False
        btnSave.Visible = False
        btnReset.Visible = False
    End Sub

    Private Sub BindUser()

        Try
            Dim obj As New TokenVendorRequisitionClass
            Dim ds As New DataSet
            Dim userExists As Boolean = False

            ds = obj.AccessableUserlist()
            lblErrorMessage.Text = ""

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr("userid").ToString().Trim() = userInfo.userIDEntity Then
                        userExists = True
                        Exit For
                    End If
                Next
            End If
            If userExists = True Then
                'btnUpload.Visible = True
                'uploadBulkExcel.Visible = True
                div_upload.Visible = True
                div_upload_button.Visible = True
                btnSubmit.Visible = True
            Else
                'btnUpload.Visible = False
                'uploadBulkExcel.Visible = False
                div_upload.Visible = False
                div_upload_button.Visible = False
                btnSubmit.Visible = False
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub
    Protected Sub btn_ok_Click(sender As Object, e As EventArgs)
        If lbl_Msg.Text.Contains("Successfully") Then
            ModalPopupExtender2.Hide()
            'tr1.Visible = True
            'tr3.Visible = True

            'tr2.Visible = False
            'btnSave.Visible = False

            'BindGrid()
            Response.Redirect("FreightDtls.aspx")
        Else
            ModalPopupExtender2.Hide()
        End If
    End Sub
End Class
