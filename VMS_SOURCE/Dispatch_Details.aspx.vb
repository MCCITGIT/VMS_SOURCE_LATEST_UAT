
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
                vendorCode = Request.QueryString("orh_vendor_code").ToString().Trim()

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

                BindRequestDetails(orhId, vendorCode)
                populateDeliveryType()

            Catch ex As Exception
                Throw ex
            End Try

            'txtDelType.Text = "COURIER/TRANSPORT DELIVERY"
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

            dispatchEntity.LRDt =
            ParseDateOrMin(txtLRDate.Text)

            dispatchEntity.VehicleNumber =
            txtVehNo.Text.Trim()

            'dispatchEntity.DeliveryType =
            'txtDelType.Text.Trim()

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

    Private Function ValidateDispatchDetails() As List(Of String)

        Dim errors As New List(Of String)()


        '---------------------------------------
        ' Delivery Type
        '---------------------------------------
        'If String.IsNullOrWhiteSpace(txtDelType.Text) Then
        '    errors.Add("Delivery Type is required.")
        'End If


        '---------------------------------------
        ' Courier No
        '---------------------------------------
        If String.IsNullOrWhiteSpace(txtCouNo.Text) Then
            errors.Add("Courier No is required.")
        End If


        '---------------------------------------
        ' Transporter Name
        '---------------------------------------
        If String.IsNullOrWhiteSpace(txtTranName.Text) Then
            errors.Add("Transporter Name is required.")
        End If


        '---------------------------------------
        ' LR / Consignment No
        '---------------------------------------
        If String.IsNullOrWhiteSpace(txtLRNo.Text) Then
            errors.Add("LR / Consignment No is required.")
        End If


        '---------------------------------------
        ' LR Date
        '---------------------------------------
        If String.IsNullOrWhiteSpace(txtLRDate.Text) Then

            errors.Add("LR Date is required.")

        Else

            Dim lrDateValue As DateTime

            If Not TryParseDispatchDate(
                txtLRDate.Text.Trim(),
                lrDateValue
            ) Then

                errors.Add("Please enter a valid LR Date.")

            End If

        End If


        '---------------------------------------
        ' Vehicle Number
        '---------------------------------------
        If String.IsNullOrWhiteSpace(txtVehNo.Text) Then
            errors.Add("Vehicle No is required.")
        End If


        '---------------------------------------
        ' LR Document
        '---------------------------------------
        If Not fuLrDoc.HasFile Then

            errors.Add("LR Document is required.")

        Else

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
        '---------------------------------------

        ' fuInv is intentionally NOT validated.


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


            If Not Decimal.TryParse(
                txtQty.Text.Trim(),
                qty
            ) Then

                errors.Add(
                    "Please enter a valid dispatch quantity at row " &
                    rowNumber.ToString() & "."
                )

                Continue For

            End If


            Decimal.TryParse(
                lblPendingQty.Text.Trim(),
                pendingQty
            )


            If qty < 0 Then

                errors.Add(
                    "Dispatch quantity cannot be negative at row " &
                    rowNumber.ToString() & "."
                )

            End If


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
            lblCourierCardHeader.Text = "Courier Information"
            lblCourierNoLabel.Text = "Courier No:"
            lblTranNameLabel.Text = "Courier Name:"
        ElseIf selectedText.Contains("transport") Then
            lblCourierCardHeader.Text = "Transport Information"
            lblCourierNoLabel.Text = "Transport No:"
            lblTranNameLabel.Text = "Transporter Name:"
        Else
            pnlCourierCard.Visible = False
        End If
    End Sub
End Class
