
Imports System.Data
Imports System.IO
Imports VMS.Web

Partial Class Dispatch_Details
    Inherits System.Web.UI.Page
    Dim DOC_ABS_PATH As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH")
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                Dim orhId As Integer
                Dim vendorCode As String = String.Empty
                Dim dispatchStatus As String = String.Empty

                vendorCode = If(Request.QueryString("orh_vendor_code"), String.Empty).Trim()
                dispatchStatus = If(Request.QueryString("dispatch_status"), String.Empty).Trim()

                ' Validate Request ID
                If Not Integer.TryParse(Request.QueryString("orh_id"), orhId) OrElse orhId <= 0 Then
                    Response.Redirect("Dispatch_List.aspx", False)
                    Context.ApplicationInstance.CompleteRequest()
                    Return
                End If

                ' Validate Vendor Code
                If String.IsNullOrWhiteSpace(vendorCode) Then
                    Response.Redirect("Dispatch_List.aspx", False)
                    Context.ApplicationInstance.CompleteRequest()
                    Return
                End If

                ' Validate Dispatch Status
                If String.IsNullOrWhiteSpace(dispatchStatus) Then
                    Response.Redirect("Dispatch_List.aspx", False)
                    Context.ApplicationInstance.CompleteRequest()
                    Return
                End If

                populateDeliveryType()
                ' Decide which method to call
                BindDetailsByStatus(orhId, vendorCode, dispatchStatus)


            Catch ex As Exception
                Throw
            End Try
        End If

    End Sub

    Private Sub BindDetailsByStatus(ByVal orhId As Integer,
                                ByVal vendorCode As String,
                                ByVal dispatchStatus As String)

        Dim status As String = dispatchStatus.Trim().ToUpper()

        If status = "PENDING" Then
            gvMaterials.Columns(4).Visible = True
            gvMaterials.Columns(6).Visible = True
            BindRequestDetails(orhId, vendorCode)

        ElseIf status = "DISPATCHED" Then
            btnUploadInvoice.Visible = False
            gvMaterials.Columns(4).Visible = False
            gvMaterials.Columns(6).Visible = False
            BindDispatchDetails(orhId, vendorCode)

        Else
            Response.Redirect("Dispatch_List.aspx", False)
            Context.ApplicationInstance.CompleteRequest()
            Return
        End If

    End Sub

    Public Sub populateDeliveryType()
        Dim Obj As New POLinkingRequestClass
        Dim DS As New DataSet
        DS = Obj.GetLovDetails("DELIVERY_TYPE", Constant.Common.ActiveStatus)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlDelType.DataSource = DS.Tables(0)
            ddlDelType.DataTextField = "lov_value"
            ddlDelType.DataValueField = "lov_code"
            ddlDelType.DataBind()
            ddlDelType.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If DS.Tables(0).Rows.Count = 1 Then
                ddlDelType.SelectedIndex = 1
                ddlDelType.Enabled = False
            End If
        End If

    End Sub

    Private Sub BindRequestDetails(ByVal orhId As Integer, ByVal vendorCode As String)

        Dim obj As POLinkingRequestClass = New POLinkingRequestClass()

        ' Clear Grid
        gvMaterials.DataSource = Nothing
        gvMaterials.DataBind()

        Dim ds As DataSet = New DataSet()

        ds = obj.GetRequestDetails(orhId, vendorCode)

        If ds Is Nothing Then
            Return
        End If


        '==================================================
        ' TABLE 0 : HEADER DETAILS
        '==================================================
        If ds.Tables.Count > 0 AndAlso
           ds.Tables(0) IsNot Nothing AndAlso
           ds.Tables(0).Rows.Count > 0 Then

            Dim dr As DataRow = ds.Tables(0).Rows(0)

            lblRequestID.Text = dr("orh_Id").ToString()
            lblVendorCode.Text = dr("orh_vendor_code").ToString()
            lblVendorName.Text = dr("unit_name").ToString()
            lblRequestDate.Text = dr("created_date").ToString()
            hdnRawMaterialVendorCode.Value = ds.Tables(0).Rows(0)("orh_rawmaterial_vender_code").ToString()
        End If


        '==================================================
        ' TABLE 1 : REQUEST DETAILS LIST
        '==================================================
        If ds.Tables.Count > 1 AndAlso
           ds.Tables(1) IsNot Nothing AndAlso
           ds.Tables(1).Rows.Count > 0 Then

            gvMaterials.DataSource = ds.Tables(1)
            gvMaterials.DataBind()

        End If

    End Sub

    Private Sub BindDispatchDetails(ByVal odhId As Integer,
                                ByVal vendorCode As String)

        Try
            Dim obj As New POLinkingRequestClass()
            Dim ds As DataSet = obj.GetDispatchDetails(odhId, vendorCode)

            If ds Is Nothing OrElse
           ds.Tables.Count = 0 OrElse
           ds.Tables(0).Rows.Count = 0 Then

                Response.Redirect("Dispatch_List.aspx", False)
                Context.ApplicationInstance.CompleteRequest()
                Return
            End If

            '========================================================
            ' HEADER DETAILS
            '========================================================
            Dim dr As DataRow = ds.Tables(0).Rows(0)

            lblRequestID.Text = Convert.ToString(dr("orh_Id"))
            lblRequestDate.Text = Convert.ToString(dr("created_date"))
            lblVendorCode.Text = Convert.ToString(dr("orh_vendor_code"))
            lblVendorName.Text = Convert.ToString(dr("unit_name"))

            hdnRawMaterialVendorCode.Value =
            Convert.ToString(dr("orh_rawmaterial_vender_code"))


            '========================================================
            ' DELIVERY TYPE
            '========================================================
            Dim deliveryType As String =
            Convert.ToString(dr("odh_del_type")).Trim()

            If Not String.IsNullOrWhiteSpace(deliveryType) Then

                Dim item As ListItem =
                ddlDelType.Items.FindByValue(deliveryType)

                'Fallback in case DB stores text instead of value
                If item Is Nothing Then
                    item = ddlDelType.Items.FindByText(deliveryType)
                End If

                If item IsNot Nothing Then
                    ddlDelType.ClearSelection()
                    item.Selected = True
                End If

            End If


            '========================================================
            ' VERY IMPORTANT
            ' Setup Courier / Transport UI first
            '========================================================
            DisplayCourierInfo()


            '========================================================
            ' INVOICE DETAILS
            '========================================================
            txtInvNo.Text = Convert.ToString(dr("odh_inv_no"))
            txtInvDate.Text = Convert.ToString(dr("odh_inv_date"))


            '========================================================
            ' COURIER / TRANSPORT DETAILS
            '========================================================
            If ddlDelType.SelectedIndex > 0 Then

                Dim selectedText As String =
                ddlDelType.SelectedItem.Text.Trim().ToLowerInvariant()


                '----------------------------------------------------
                ' COURIER
                '----------------------------------------------------
                If selectedText.Contains("courier") Then
                    txtCouNo.Text =
                    Convert.ToString(dr("odh_courier_id"))

                    txtTranName.Text =
                    Convert.ToString(dr("odh_trans_name"))

                    'For Courier use Courier Date
                    txtLRDate.Text =
                    Convert.ToString(dr("odh_courier_date"))

                    'Not applicable for Courier
                    txtLRNo.Text = String.Empty
                    txtVehNo.Text = String.Empty


                    '----------------------------------------------------
                    ' TRANSPORT
                    '----------------------------------------------------
                ElseIf selectedText.Contains("transport") Then
                    txtCouNo.Text =
                    Convert.ToString(dr("odh_courier_id"))

                    txtTranName.Text =
                    Convert.ToString(dr("odh_trans_name"))

                    txtLRNo.Text =
                    Convert.ToString(dr("odh_lr_no"))

                    txtLRDate.Text =
                    Convert.ToString(dr("odh_lr_date"))

                    txtVehNo.Text =
                    Convert.ToString(dr("odh_vehicle_no"))

                End If

            End If


            '========================================================
            ' MATERIAL DETAILS
            '========================================================
            If ds.Tables.Count > 1 AndAlso
           ds.Tables(1) IsNot Nothing AndAlso
           ds.Tables(1).Rows.Count > 0 Then

                gvMaterials.DataSource = ds.Tables(1)
                gvMaterials.DataBind()

            Else

                gvMaterials.DataSource = Nothing
                gvMaterials.DataBind()

            End If


            '========================================================
            ' DISABLE ALL EDITABLE CONTROLS
            '========================================================
            SetDispatchDetailsReadOnly()

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub SetDispatchDetailsReadOnly()

        ddlDelType.Enabled = False

        txtCouNo.Enabled = False
        txtTranName.Enabled = False
        txtLRNo.Enabled = False
        txtLRDate.Enabled = False
        txtVehNo.Enabled = False

        txtInvNo.Enabled = False
        txtInvDate.Enabled = False

        fuLrDoc.Enabled = False
        fuInv.Enabled = False

        calLRDate.Enabled = False
        calInvDate.Enabled = False

        For Each row As GridViewRow In gvMaterials.Rows

            If row.RowType = DataControlRowType.DataRow Then

                Dim txtQty As TextBox =
                TryCast(row.FindControl("txtQtyToDispatch"), TextBox)

                If txtQty IsNot Nothing Then
                    txtQty.Enabled = False
                End If

            End If

        Next

        btnSubmit.Visible = False

    End Sub

    Public Function GetFormattedDate(ByVal value As Object) As String

        If value Is Nothing OrElse value Is DBNull.Value Then
            Return ""
        End If

        Dim dt As DateTime

        If DateTime.TryParse(value.ToString(), dt) Then
            Return dt.ToString("dd-MM-yyyy")
        End If

        Return value.ToString()

    End Function

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Dispatch_List.aspx")
    End Sub


    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

    '    Dim dispatchEntity As New DistpacthDetailsEntity()
    '    Dim dispatchClass As New POLinkingRequestClass()

    '    Dim MsgID As Integer

    '    Try
    '        ' -------- Build header fields from the form --------
    '        dispatchEntity.ReqID = Convert.ToInt32(lblRequestID.Text)
    '        dispatchEntity.CourierId = txtCouNo.Text.Trim()
    '        dispatchEntity.InvoiceNo = txtInvNo.Text.Trim()
    '        dispatchEntity.InvoiceDate = ParseDateOrMin(txtInvDate.Text)
    '        dispatchEntity.TransporterName = txtTranName.Text.Trim()
    '        dispatchEntity.LRNumber = txtLRNo.Text.Trim()
    '        dispatchEntity.LRDt = ParseDateOrMin(txtLRDate.Text)
    '        dispatchEntity.VehicleNumber = txtVehNo.Text.Trim()
    '        dispatchEntity.DeliveryType = txtDelType.Text.Trim()
    '        dispatchEntity.CreatedUser = "murthy"

    '        ' -------- Handle LR Doc / Invoice Doc uploads --------
    '        'If fuLrDoc.HasFile Then
    '        '    Dim lrDocPath As String = SaveDoc(fuLrDoc)
    '        '    If Not String.IsNullOrEmpty(lrDocPath) Then
    '        '        dispatchEntity.LRDocument = lrDocPath
    '        '    Else
    '        '        dispatchEntity.LRDocument = String.Empty
    '        '    End If
    '        'Else
    '        '    dispatchEntity.LRDocument = String.Empty
    '        'End If
    '        If fuLrDoc.HasFile Then
    '            Dim lrDocPath As String = SaveDoc(fuLrDoc)
    '            If Not String.IsNullOrEmpty(lrDocPath) Then
    '                dispatchEntity.LRDocument = lrDocPath
    '                dispatchEntity.DocFileName = Path.GetFileName(lrDocPath)
    '                dispatchEntity.DocPath = lrDocPath
    '            Else
    '                dispatchEntity.LRDocument = String.Empty
    '                dispatchEntity.DocFileName = String.Empty
    '                dispatchEntity.DocPath = String.Empty
    '            End If
    '        Else
    '            dispatchEntity.LRDocument = String.Empty
    '            dispatchEntity.DocFileName = String.Empty
    '            dispatchEntity.DocPath = String.Empty
    '        End If

    '        ' -------- Build detail rows DataTable from the grid --------
    '        Dim dtDetails As New DataTable()
    '        dtDetails.Columns.Add("ord_id", GetType(Integer))
    '        dtDetails.Columns.Add("rawmatcode", GetType(String))
    '        dtDetails.Columns.Add("requested_qty", GetType(Decimal))
    '        dtDetails.Columns.Add("already_dispatched_qty", GetType(Decimal))
    '        dtDetails.Columns.Add("qty_to_dispatch", GetType(Decimal))

    '        For Each row As GridViewRow In gvMaterials.Rows

    '            Dim hdnOrdID As HiddenField = CType(row.FindControl("hdnOrdID"), HiddenField)
    '            Dim lblRmCode As Label = CType(row.FindControl("lblRmCode"), Label)
    '            Dim lblRequestedQty As Label = CType(row.FindControl("lblRequestedQty"), Label)
    '            Dim lblDispatchQty As Label = CType(row.FindControl("lblDispatchQty"), Label)
    '            Dim txtQty As TextBox = CType(row.FindControl("txtQtyToDispatch"), TextBox)

    '            Dim qty As Decimal = 0
    '            Decimal.TryParse(txtQty.Text, qty)

    '            If qty > 0 Then
    '                dtDetails.Rows.Add(
    '                Convert.ToInt32(hdnOrdID.Value),
    '                lblRmCode.Text,
    '                Convert.ToDecimal(lblRequestedQty.Text),
    '                Convert.ToDecimal(lblDispatchQty.Text),
    '                qty
    '            )
    '            End If

    '        Next

    '        If dtDetails.Rows.Count = 0 Then
    '            lblMessage.Text = "Please enter a quantity to dispatch for at least one item."
    '            pnlMessage.Visible = True
    '            Return
    '        End If

    '        ' -------- Call the BLL / SP --------
    '        MsgID = dispatchClass.InsertDispatchDetails(dispatchEntity, dtDetails)

    '        If MsgID = 1 Then
    '            'pnlMessage.Visible = True
    '            'lblMessage.CssClass = "alert alert-success d-block"
    '            'lblMessage.Text = "Dispatch submitted successfully."
    '            '' Optionally: rebind gvMaterials, clear fields, etc.
    '            mpeSuccess.Show()
    '        Else
    '            pnlMessage.Visible = True
    '            lblMessage.CssClass = "alert alert-danger d-block"
    '            lblMessage.Text = If(String.IsNullOrEmpty(dispatchEntity.Message), "Dispatch not saved.", dispatchEntity.Message)
    '        End If

    '    Catch ex As Exception
    '        Dim returnUrl As String = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '        Server.Transfer(returnUrl)
    '    End Try

    'End Sub

    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim dispatchEntity As New DistpacthDetailsEntity()
    '    Dim dispatchClass As New POLinkingRequestClass()

    '    Dim MsgID As Integer

    '    Try

    '        '===========================================
    '        ' SERVER SIDE VALIDATION
    '        '===========================================

    '        Dim validationErrors As List(Of String) =
    '        ValidateDispatchDetails()

    '        If validationErrors.Count > 0 Then

    '            ShowValidationPopup(validationErrors)

    '            Return

    '        End If


    '        '===========================================
    '        ' Build Header
    '        '===========================================

    '        dispatchEntity.ReqID =
    '        Convert.ToInt32(lblRequestID.Text)

    '        dispatchEntity.CourierId =
    '        txtCouNo.Text.Trim()

    '        dispatchEntity.InvoiceNo =
    '        txtInvNo.Text.Trim()

    '        dispatchEntity.InvoiceDate =
    '        ParseDateOrMin(txtInvDate.Text)

    '        dispatchEntity.TransporterName =
    '        txtTranName.Text.Trim()

    '        dispatchEntity.LRNumber =
    '        txtLRNo.Text.Trim()

    '        dispatchEntity.LRDt =
    '        ParseDateOrMin(txtLRDate.Text)

    '        dispatchEntity.VehicleNumber =
    '        txtVehNo.Text.Trim()

    '        'dispatchEntity.DeliveryType =
    '        'txtDelType.Text.Trim()

    '        dispatchEntity.CreatedUser =
    '        "murthy"


    '        '===========================================
    '        ' LR Document
    '        '===========================================

    '        If fuLrDoc.HasFile Then

    '            Dim lrDocPath As String =
    '            SaveDoc(fuLrDoc)

    '            If Not String.IsNullOrEmpty(lrDocPath) Then

    '                dispatchEntity.LRDocument =
    '                lrDocPath

    '                dispatchEntity.DocFileName =
    '                Path.GetFileName(lrDocPath)

    '                dispatchEntity.DocPath =
    '                lrDocPath

    '            Else

    '                dispatchEntity.LRDocument =
    '                String.Empty

    '                dispatchEntity.DocFileName =
    '                String.Empty

    '                dispatchEntity.DocPath =
    '                String.Empty

    '            End If

    '        End If


    '        '===========================================
    '        ' Detail DataTable
    '        '===========================================

    '        Dim dtDetails As New DataTable()

    '        dtDetails.Columns.Add(
    '        "ord_id",
    '        GetType(Integer)
    '    )

    '        dtDetails.Columns.Add(
    '        "rawmatcode",
    '        GetType(String)
    '    )

    '        dtDetails.Columns.Add(
    '        "requested_qty",
    '        GetType(Decimal)
    '    )

    '        dtDetails.Columns.Add(
    '        "already_dispatched_qty",
    '        GetType(Decimal)
    '    )

    '        dtDetails.Columns.Add(
    '        "qty_to_dispatch",
    '        GetType(Decimal)
    '    )


    '        For Each row As GridViewRow In gvMaterials.Rows

    '            Dim hdnOrdID As HiddenField =
    '            CType(
    '                row.FindControl("hdnOrdID"),
    '                HiddenField
    '            )

    '            Dim lblRmCode As Label =
    '            CType(
    '                row.FindControl("lblRmCode"),
    '                Label
    '            )

    '            Dim lblRequestedQty As Label =
    '            CType(
    '                row.FindControl("lblRequestedQty"),
    '                Label
    '            )

    '            Dim lblDispatchQty As Label =
    '            CType(
    '                row.FindControl("lblDispatchQty"),
    '                Label
    '            )

    '            Dim txtQty As TextBox =
    '            CType(
    '                row.FindControl("txtQtyToDispatch"),
    '                TextBox
    '            )


    '            Dim qty As Decimal = 0D

    '            Decimal.TryParse(
    '            txtQty.Text.Trim(),
    '            qty
    '        )


    '            If qty > 0 Then

    '                dtDetails.Rows.Add(
    '                Convert.ToInt32(
    '                    hdnOrdID.Value
    '                ),
    '                lblRmCode.Text.Trim(),
    '                Convert.ToDecimal(
    '                    lblRequestedQty.Text
    '                ),
    '                Convert.ToDecimal(
    '                    lblDispatchQty.Text
    '                ),
    '                qty
    '            )

    '            End If

    '        Next


    '        '===========================================
    '        ' Save
    '        '===========================================

    '        MsgID =
    '        dispatchClass.InsertDispatchDetails(
    '            dispatchEntity,
    '            dtDetails
    '        )


    '        If MsgID = 1 Then

    '            mpeSuccess.Show()

    '        Else

    '            pnlMessage.Visible = True

    '            lblMessage.CssClass =
    '            "alert alert-danger d-block"

    '            lblMessage.Text =
    '            If(
    '                String.IsNullOrEmpty(
    '                    dispatchEntity.Message
    '                ),
    '                "Dispatch not saved.",
    '                dispatchEntity.Message
    '            )

    '        End If


    '    Catch ex As Exception

    '        Dim returnUrl As String =
    '        "~/ExceptionPage.aspx"

    '        Session(
    '        Constant.SessionKeys.ErrMessage
    '    ) =
    '        Constant.ErrorMessages.GeneralError

    '        Server.Transfer(returnUrl)

    '    End Try

    'End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim dispatchEntity As New DistpacthDetailsEntity()
        Dim dispatchClass As New POLinkingRequestClass()

        Dim MsgID As Integer

        Try

            '===========================================
            ' SERVER SIDE VALIDATION
            '===========================================

            Dim validationErrors As List(Of String) =
            ValidateDispatchDetails()

            If validationErrors.Count > 0 Then

                ShowValidationPopup(validationErrors)

                Return

            End If


            '===========================================
            ' Build Header
            '===========================================

            dispatchEntity.ReqID =
            Convert.ToInt32(lblRequestID.Text)

            dispatchEntity.rmVendorCode =
            hdnRawMaterialVendorCode.Value.Trim()

            dispatchEntity.CourierId =
            txtCouNo.Text.Trim()

            dispatchEntity.InvoiceNo =
            txtInvNo.Text.Trim()

            dispatchEntity.InvoiceDate =
            ParseDateOrMin(txtInvDate.Text)

            dispatchEntity.TransporterName =
            txtTranName.Text.Trim()

            dispatchEntity.LRNumber =
            txtLRNo.Text.Trim()

            Dim ddltype As String = ddlDelType.SelectedItem.Text.Trim().ToLowerInvariant()

            If ddltype.Contains("courier") Then
                dispatchEntity.CourDt =
                ParseDateOrMin(txtLRDate.Text)
            ElseIf ddltype.Contains("transport") Then
                dispatchEntity.LRDt =
                ParseDateOrMin(txtLRDate.Text)
            End If


            dispatchEntity.VehicleNumber =
            txtVehNo.Text.Trim()

            dispatchEntity.DeliveryType =
            ddlDelType.SelectedValue

            dispatchEntity.CreatedUser =
            hdnRawMaterialVendorCode.Value.Trim()


            '===========================================
            ' LR Document
            '===========================================

            If fuLrDoc.HasFile Then
                Dim lrDocPath As String = SaveDoc(fuLrDoc, Constant.Common.LRDoc)
                If Not String.IsNullOrEmpty(lrDocPath) Then
                    dispatchEntity.LRDocFileName = Path.GetFileName(lrDocPath)
                    dispatchEntity.LRDocPath = lrDocPath
                Else
                    dispatchEntity.LRDocFileName = String.Empty
                    dispatchEntity.LRDocPath = String.Empty
                End If
            End If

            '===========================================
            ' Invoice Document
            '===========================================

            If fuInv.HasFile Then
                Dim invDocPath As String = SaveDoc(fuInv, Constant.Common.InvoiceDoc)
                If Not String.IsNullOrEmpty(invDocPath) Then
                    dispatchEntity.InvDocFileName = Path.GetFileName(invDocPath)
                    dispatchEntity.InvDocPath = invDocPath
                Else
                    dispatchEntity.InvDocFileName = String.Empty
                    dispatchEntity.InvDocPath = String.Empty
                End If
            End If

            '===========================================
            ' Detail DataTable
            '===========================================

            Dim dtDetails As New DataTable()

            dtDetails.Columns.Add(
            "ord_id",
            GetType(Integer)
        )

            dtDetails.Columns.Add(
            "rawmatcode",
            GetType(String)
        )

            dtDetails.Columns.Add(
            "requested_qty",
            GetType(Decimal)
        )

            dtDetails.Columns.Add(
            "already_dispatched_qty",
            GetType(Decimal)
        )

            dtDetails.Columns.Add(
            "qty_to_dispatch",
            GetType(Decimal)
        )


            For Each row As GridViewRow In gvMaterials.Rows

                Dim hdnOrdID As HiddenField =
                CType(
                    row.FindControl("hdnOrdID"),
                    HiddenField
                )

                Dim lblRmCode As Label =
                CType(
                    row.FindControl("lblRmCode"),
                    Label
                )

                Dim lblRequestedQty As Label =
                CType(
                    row.FindControl("lblRequestedQty"),
                    Label
                )

                Dim lblDispatchQty As Label =
                CType(
                    row.FindControl("lblDispatchQty"),
                    Label
                )

                Dim txtQty As TextBox =
                CType(
                    row.FindControl("txtQtyToDispatch"),
                    TextBox
                )


                Dim qty As Decimal = 0D

                Decimal.TryParse(
                txtQty.Text.Trim(),
                qty
            )


                If qty > 0 Then

                    dtDetails.Rows.Add(
                    Convert.ToInt32(
                        hdnOrdID.Value
                    ),
                    lblRmCode.Text.Trim(),
                    Convert.ToDecimal(
                        lblRequestedQty.Text
                    ),
                    Convert.ToDecimal(
                        lblDispatchQty.Text
                    ),
                    qty
                )

                End If

            Next


            '===========================================
            ' Save
            '===========================================

            MsgID =
            dispatchClass.InsertDispatchDetails(
                dispatchEntity,
                dtDetails
            )


            If MsgID = 1 Then

                mpeSuccess.Show()

            Else

                pnlMessage.Visible = True

                lblMessage.CssClass =
                "alert alert-danger d-block"

                lblMessage.Text =
                If(
                    String.IsNullOrEmpty(
                        dispatchEntity.Message
                    ),
                    "Dispatch not saved.",
                    dispatchEntity.Message
                )

            End If


        Catch ex As Exception

            Dim returnUrl As String =
            "~/ExceptionPage.aspx"

            Session(
            Constant.SessionKeys.ErrMessage
        ) =
            Constant.ErrorMessages.GeneralError

            Server.Transfer(returnUrl)

        End Try

    End Sub

    Private Function SaveDoc(ByVal fileUpload As FileUpload, ByVal docTypeFolder As String) As String
        Dim response As String = ""
        Try
            If fileUpload Is Nothing OrElse Not fileUpload.HasFile Then
                Return ""
            End If
            Dim guidObj As Guid = Guid.NewGuid()
            'Get original file extension
            Dim extension As String = Path.GetExtension(fileUpload.FileName)
            'Generate unique filename
            Dim fileName As String = guidObj.ToString() & extension
            'Folder: LRDoc/InvoiceDoc  ->  yyyy_MM_dd
            Dim directoryPath As String = docTypeFolder & "/" & DateTime.Now.ToString("yyyy_MM_dd")
            Dim fullDirectoryPath As String = Path.Combine(DOC_ABS_PATH, docTypeFolder, DateTime.Now.ToString("yyyy_MM_dd"))
            'Create directory if not available
            If Not Directory.Exists(fullDirectoryPath) Then
                Directory.CreateDirectory(fullDirectoryPath)
            End If
            Dim fullFilePath As String = Path.Combine(fullDirectoryPath, fileName)
            'Save uploaded file
            fileUpload.SaveAs(fullFilePath)
            'Return relative path
            response = directoryPath & "/" & fileName
        Catch ex As Exception
            response = ""
        End Try
        Return response
    End Function

    'Private Function ParseDateOrMin(ByVal dateText As String) As DateTime
    '    Dim result As DateTime
    '    If DateTime.TryParseExact(dateText, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, result) Then
    '        Return result
    '    End If
    '    Return DateTime.MinValue
    'End Function
    Private Function ParseDateOrMin(ByVal dateText As String) As DateTime

        If String.IsNullOrWhiteSpace(dateText) Then
            Return DateTime.MinValue
        End If

        dateText = dateText.Trim()

        Dim result As DateTime

        ' Accept both dd-MM-yyyy (typed via calendar extender) and
        ' dd/MM/yyyy (from the invoice OCR extract, which swaps '-' to '/')
        Dim acceptedFormats() As String = {"dd-MM-yyyy", "dd/MM/yyyy"}

        If DateTime.TryParseExact(dateText, acceptedFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, result) Then
            Return result
        End If

        Return DateTime.MinValue

    End Function

    'Private Function ValidateDispatchDetails() As List(Of String)

    '    Dim errors As New List(Of String)()


    '    '---------------------------------------
    '    ' Delivery Type
    '    '---------------------------------------
    '    'If String.IsNullOrWhiteSpace(txtDelType.Text) Then
    '    '    errors.Add("Delivery Type is required.")
    '    'End If


    '    '---------------------------------------
    '    ' Courier No
    '    '---------------------------------------
    '    If String.IsNullOrWhiteSpace(txtCouNo.Text) Then
    '        errors.Add("Courier No is required.")
    '    End If


    '    '---------------------------------------
    '    ' Transporter Name
    '    '---------------------------------------
    '    If String.IsNullOrWhiteSpace(txtTranName.Text) Then
    '        errors.Add("Transporter Name is required.")
    '    End If


    '    '---------------------------------------
    '    ' LR / Consignment No
    '    '---------------------------------------
    '    If String.IsNullOrWhiteSpace(txtLRNo.Text) Then
    '        errors.Add("LR / Consignment No is required.")
    '    End If


    '    '---------------------------------------
    '    ' LR Date
    '    '---------------------------------------
    '    If String.IsNullOrWhiteSpace(txtLRDate.Text) Then

    '        errors.Add("LR Date is required.")

    '    Else

    '        Dim lrDateValue As DateTime

    '        If Not TryParseDispatchDate(
    '            txtLRDate.Text.Trim(),
    '            lrDateValue
    '        ) Then

    '            errors.Add("Please enter a valid LR Date.")

    '        End If

    '    End If


    '    '---------------------------------------
    '    ' Vehicle Number
    '    '---------------------------------------
    '    If String.IsNullOrWhiteSpace(txtVehNo.Text) Then
    '        errors.Add("Vehicle No is required.")
    '    End If


    '    '---------------------------------------
    '    ' LR Document
    '    '---------------------------------------
    '    'If Not fuLrDoc.HasFile Then

    '    '    errors.Add("LR Document is required.")

    '    'Else

    '    '    Dim extension As String =
    '    '        System.IO.Path.GetExtension(
    '    '            fuLrDoc.FileName
    '    '        )

    '    '    If Not extension.Equals(
    '    '        ".pdf",
    '    '        StringComparison.OrdinalIgnoreCase
    '    '    ) Then

    '    '        errors.Add(
    '    '            "LR Document must be a PDF file."
    '    '        )

    '    '    End If

    '    'End If


    '    '---------------------------------------
    '    ' Invoice Number
    '    '---------------------------------------
    '    If String.IsNullOrWhiteSpace(txtInvNo.Text) Then
    '        errors.Add("Invoice No is required.")
    '    End If


    '    '---------------------------------------
    '    ' Invoice Date
    '    '---------------------------------------
    '    If String.IsNullOrWhiteSpace(txtInvDate.Text) Then

    '        errors.Add("Invoice Date is required.")

    '    Else

    '        Dim invoiceDateValue As DateTime

    '        If Not TryParseDispatchDate(
    '            txtInvDate.Text.Trim(),
    '            invoiceDateValue
    '        ) Then

    '            errors.Add(
    '                "Please enter a valid Invoice Date."
    '            )

    '        End If

    '    End If


    '    '---------------------------------------
    '    ' Invoice Document
    '    '---------------------------------------

    '    ' fuInv is intentionally NOT validated.


    '    '---------------------------------------
    '    ' Quantity Validation
    '    '---------------------------------------

    '    Dim hasDispatchQty As Boolean = False

    '    Dim rowNumber As Integer = 0

    '    For Each row As GridViewRow In gvMaterials.Rows

    '        rowNumber += 1

    '        Dim txtQty As TextBox =
    '            CType(
    '                row.FindControl("txtQtyToDispatch"),
    '                TextBox
    '            )

    '        Dim lblPendingQty As Label =
    '            CType(
    '                row.FindControl("lblPendingQty"),
    '                Label
    '            )


    '        Dim qty As Decimal = 0D
    '        Dim pendingQty As Decimal = 0D


    '        If Not Decimal.TryParse(
    '            txtQty.Text.Trim(),
    '            qty
    '        ) Then

    '            errors.Add(
    '                "Please enter a valid dispatch quantity at row " &
    '                rowNumber.ToString() & "."
    '            )

    '            Continue For

    '        End If


    '        Decimal.TryParse(
    '            lblPendingQty.Text.Trim(),
    '            pendingQty
    '        )


    '        If qty < 0 Then

    '            errors.Add(
    '                "Dispatch quantity cannot be negative at row " &
    '                rowNumber.ToString() & "."
    '            )

    '        End If


    '        If qty > 0 Then

    '            hasDispatchQty = True

    '            If qty > pendingQty Then

    '                errors.Add(
    '                    "Dispatch quantity cannot exceed pending quantity at row " &
    '                    rowNumber.ToString() & "."
    '                )

    '            End If

    '        End If

    '    Next


    '    If Not hasDispatchQty Then

    '        errors.Add(
    '            "Please enter quantity to dispatch for at least one material."
    '        )

    '    End If


    '    Return errors

    'End Function

    Private Function ValidateDispatchDetails() As List(Of String)

        Dim errors As New List(Of String)()

        '---------------------------------------
        ' Delivery Type
        '---------------------------------------
        If ddlDelType.SelectedIndex <= 0 OrElse
       ddlDelType.SelectedItem Is Nothing OrElse
       String.IsNullOrWhiteSpace(ddlDelType.SelectedItem.Text) Then

            errors.Add("Delivery Type is required.")

        Else

            Dim selectedDeliveryText As String =
            ddlDelType.SelectedItem.Text.Trim().ToLowerInvariant()


            '---------------------------------------
            ' Courier / POD / Transport No
            ' Mandatory for Courier and Transport
            '---------------------------------------
            If String.IsNullOrWhiteSpace(txtCouNo.Text) Then

                If selectedDeliveryText.Contains("courier") Then

                    errors.Add("POD No is required.")

                ElseIf selectedDeliveryText.Contains("transport") Then

                    errors.Add("Transport No is required.")

                Else

                    errors.Add("Courier / Transport No is required.")

                End If

            End If


            '---------------------------------------
            ' Courier Name / Transporter Name
            ' Mandatory for Courier and Transport
            '---------------------------------------
            If String.IsNullOrWhiteSpace(txtTranName.Text) Then

                If selectedDeliveryText.Contains("courier") Then

                    errors.Add("Courier Name is required.")

                ElseIf selectedDeliveryText.Contains("transport") Then

                    errors.Add("Transporter Name is required.")

                Else

                    errors.Add("Courier / Transporter Name is required.")

                End If

            End If


            '---------------------------------------
            ' LR / Consignment No
            ' Mandatory ONLY for Transport
            '---------------------------------------
            If selectedDeliveryText.Contains("transport") Then

                If String.IsNullOrWhiteSpace(txtLRNo.Text) Then

                    errors.Add("LR / Consignment No is required.")

                End If

            End If


            '---------------------------------------
            ' LR Date / Courier Date
            ' Mandatory for Courier and Transport
            '---------------------------------------
            If String.IsNullOrWhiteSpace(txtLRDate.Text) Then

                If selectedDeliveryText.Contains("courier") Then

                    errors.Add("Courier Date is required.")

                ElseIf selectedDeliveryText.Contains("transport") Then

                    errors.Add("LR Date is required.")

                Else

                    errors.Add("Delivery Date is required.")

                End If

            Else

                Dim lrDateValue As DateTime

                If Not TryParseDispatchDate(
                txtLRDate.Text.Trim(),
                lrDateValue
            ) Then

                    If selectedDeliveryText.Contains("courier") Then

                        errors.Add(
                        "Please enter a valid Courier Date."
                    )

                    ElseIf selectedDeliveryText.Contains("transport") Then

                        errors.Add(
                        "Please enter a valid LR Date."
                    )

                    Else

                        errors.Add(
                        "Please enter a valid Delivery Date."
                    )

                    End If

                End If

            End If


            '---------------------------------------
            ' Vehicle Number
            ' Mandatory ONLY for Transport
            '---------------------------------------
            If selectedDeliveryText.Contains("transport") Then

                If String.IsNullOrWhiteSpace(txtVehNo.Text) Then

                    errors.Add("Vehicle No is required.")

                End If

            End If

        End If


        '---------------------------------------
        ' LR Document
        ' NOT Mandatory
        ' Validate PDF only if uploaded
        '---------------------------------------
        If fuLrDoc.HasFile Then

            Dim extension As String =
            System.IO.Path.GetExtension(
                fuLrDoc.FileName
            )

            If Not extension.Equals(
            ".pdf",
            StringComparison.OrdinalIgnoreCase
        ) Then

                errors.Add(
                "LR Document must be a PDF file."
            )

            End If

        End If


        '---------------------------------------
        ' Invoice Number
        '---------------------------------------
        If String.IsNullOrWhiteSpace(txtInvNo.Text) Then

            errors.Add("Invoice No is required.")

        End If


        '---------------------------------------
        ' Invoice Date
        '---------------------------------------
        If String.IsNullOrWhiteSpace(txtInvDate.Text) Then

            errors.Add("Invoice Date is required.")

        Else

            Dim invoiceDateValue As DateTime

            If Not TryParseDispatchDate(
            txtInvDate.Text.Trim(),
            invoiceDateValue
        ) Then

                errors.Add(
                "Please enter a valid Invoice Date."
            )

            End If

        End If


        '---------------------------------------
        ' Invoice Document
        ' NOT Mandatory
        ' Validate PDF only if uploaded
        '---------------------------------------
        If fuInv.HasFile Then

            Dim extension As String =
            System.IO.Path.GetExtension(
                fuInv.FileName
            )

            If Not extension.Equals(
            ".pdf",
            StringComparison.OrdinalIgnoreCase
        ) Then

                errors.Add(
                "Invoice Document must be a PDF file."
            )

            End If

        End If


        '---------------------------------------
        ' Quantity Validation
        '---------------------------------------
        Dim hasDispatchQty As Boolean = False

        Dim rowNumber As Integer = 0

        For Each row As GridViewRow In gvMaterials.Rows

            rowNumber += 1

            Dim txtQty As TextBox =
            CType(
                row.FindControl("txtQtyToDispatch"),
                TextBox
            )

            Dim lblPendingQty As Label =
            CType(
                row.FindControl("lblPendingQty"),
                Label
            )


            Dim qty As Decimal = 0D
            Dim pendingQty As Decimal = 0D


            '---------------------------------------
            ' Invalid Quantity
            '---------------------------------------
            If txtQty Is Nothing OrElse
           Not Decimal.TryParse(
               txtQty.Text.Trim(),
               qty
           ) Then

                errors.Add(
                "Please enter a valid dispatch quantity at row " &
                rowNumber.ToString() & "."
            )

                Continue For

            End If


            '---------------------------------------
            ' Pending Quantity
            '---------------------------------------
            If lblPendingQty IsNot Nothing Then

                Decimal.TryParse(
                lblPendingQty.Text.Trim(),
                pendingQty
            )

            End If


            '---------------------------------------
            ' Negative Quantity
            '---------------------------------------
            If qty < 0 Then

                errors.Add(
                "Dispatch quantity cannot be negative at row " &
                rowNumber.ToString() & "."
            )

                Continue For

            End If


            '---------------------------------------
            ' Quantity > 0
            '---------------------------------------
            If qty > 0 Then

                hasDispatchQty = True

                If qty > pendingQty Then

                    errors.Add(
                    "Dispatch quantity cannot exceed pending quantity at row " &
                    rowNumber.ToString() & "."
                )

                End If

            End If

        Next


        '---------------------------------------
        ' At least one material required
        '---------------------------------------
        If Not hasDispatchQty Then

            errors.Add(
            "Please enter quantity to dispatch for at least one material."
        )

        End If


        Return errors

    End Function

    Private Function TryParseDispatchDate(
    ByVal dateText As String,
    ByRef parsedDate As DateTime
) As Boolean

        Dim formats() As String = {
            "dd-MM-yyyy",
            "dd/MM/yyyy"
        }

        Return DateTime.TryParseExact(
            dateText,
            formats,
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            parsedDate
        )

    End Function

    Private Sub ShowValidationPopup(
    ByVal errors As List(Of String)
)

        Dim sb As New System.Text.StringBuilder()

        sb.Append("<ul>")

        For Each err As String In errors

            sb.Append("<li>")
            sb.Append(Server.HtmlEncode(err))
            sb.Append("</li>")

        Next

        sb.Append("</ul>")

        lblValidationMessage.Text = sb.ToString()

        mpeValidation.Show()

    End Sub

    Protected Sub ddlDelType_SelectedIndexChanged(sender As Object, e As EventArgs)
        DisplayCourierInfo()
    End Sub

    Private Sub DisplayCourierInfo()
        If ddlDelType.SelectedIndex <= 0 OrElse String.IsNullOrWhiteSpace(ddlDelType.SelectedItem.Text) Then
            pnlCourierCard.Visible = False
            Return
        End If

        Dim selectedText As String = ddlDelType.SelectedItem.Text.Trim().ToLowerInvariant()

        pnlCourierCard.Visible = True

        If selectedText.Contains("courier") Then
            'lblCourierCardHeader.Text = "Courier Information"
            'lblCourierNoLabel.Text = "Courier No:"
            'lblTranNameLabel.Text = "Courier Name:"
            divLrNo.Visible = False
            divVehNo.Visible = False
            lblCourierCardHeader.Text = "Courier Information"
            lblCourierNoLabel.Text = "POD No:"
            txtCouNo.Attributes("placeholder") = "Enter POD No."
            lblTranNameLabel.Text = "Courier Name:"
            txtTranName.Attributes("placeholder") = "Enter Courier Name."
            lblLrDate.Text = "Courier Date: "
            txtLRDate.Attributes("placeholder") = "Select Courier Date."
        ElseIf selectedText.Contains("transport") Then
            lblCourierCardHeader.Text = "Transport Information"
            lblCourierNoLabel.Text = "Transport No:"
            txtCouNo.Attributes("placeholder") = "Enter Transport No."
            lblTranNameLabel.Text = "Transporter Name:"
            txtTranName.Attributes("placeholder") = "Enter Transport Name."
            lblLrDate.Text = "LR Date: "
            txtLRDate.Attributes("placeholder") = "Select LR Date."
            divLrNo.Visible = True
            divVehNo.Visible = True
        Else
            pnlCourierCard.Visible = False
        End If
    End Sub
End Class
