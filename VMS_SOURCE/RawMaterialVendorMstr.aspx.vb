Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Partial Class RawMaterialVendorMstr
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        btnSubmit.Attributes.Add("onclick", "return rmConfirmVendorStatusSubmit();")

        If Not IsPostBack Then
            ShowSavedMessage()

            If Not String.IsNullOrWhiteSpace(Request.QueryString(Constant.SessionKeys.UnitCode)) AndAlso
               Request.QueryString(Constant.SessionKeys.UnitCode) <> Constant.GeneralMessages.AddNew Then
                btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                BindVendorData(Request.QueryString(Constant.SessionKeys.UnitCode))
            Else
                btnSubmit.Text = Constant.GeneralMessages.btnSubmit
                GenerateNextVendorCode()
            End If
        End If
    End Sub

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim vendorEntity As New RawMaterialVendorMasterEntity()
        Dim obj As New OPC_VendorClass()
        Dim MsgID As Integer

        Try
            If Not ValidateVendorInputs() Then
                Exit Sub
            End If

            If btnSubmit.Text = Constant.GeneralMessages.btnSubmit AndAlso String.IsNullOrWhiteSpace(txtUnitCode.Text.Trim()) Then
                GenerateNextVendorCode()
            End If

            vendorEntity.VendorCode = txtUnitCode.Text.Trim()
            vendorEntity.VendorName = txtUnitName.Text.Trim()
            vendorEntity.GstRegistrationNumber = txtGstRegNo.Text.Trim()
            vendorEntity.Address = txtLine1.Text.Trim()
            vendorEntity.Vendor_City = txtCity.Text.Trim()
            vendorEntity.Vendor_State = txtState.Text.Trim()
            vendorEntity.Vendor_PinCode = txtPin.Text.Trim()
            vendorEntity.ContactPerson = txtContactPerson.Text.Trim()
            vendorEntity.MobileNumber = txtMobileNo.Text.Trim()
            vendorEntity.EmailAddress = txtEmail.Text.Trim()
            vendorEntity.CreatedUser = userInfo.userIDEntity
            vendorEntity.ActiveStatus = GetSelectedActiveStatus()

            If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
                vendorEntity.VendorCode = Convert.ToString(Request.QueryString(Constant.SessionKeys.UnitCode)).Trim()
                vendorEntity.ModifiedUser = userInfo.userIDEntity
                vendorEntity.Trantype = 2
            Else
                vendorEntity.Trantype = 1
            End If

            MsgID = obj.InsertUpdateRawMaterialVendorMasterDtls(vendorEntity)

            If MsgID = 1 Then
                If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
                    Session("RawMaterialVendorSaveMsg") = "Vendor Updated Successfully."
                    Response.Redirect("~/RawMaterialVendorMstr.aspx?" & Constant.SessionKeys.UnitCode & "=" & Server.UrlEncode(vendorEntity.VendorCode), False)
                Else
                    Session("RawMaterialVendorSaveMsg") = "Vendor Saved Successfully."
                    Response.Redirect("~/RawMaterialVendorMstr.aspx?" & Constant.SessionKeys.UnitCode & "=" & Constant.GeneralMessages.AddNew, False)
                End If
                Context.ApplicationInstance.CompleteRequest()
            ElseIf MsgID = 2 Then
                ShowInlineValidation("UnitName", "Vendor Code already exists.")
                btnSubmit.Enabled = True
            Else
                ShowInlineValidation("UnitName", "Vendor not saved.")
                btnSubmit.Enabled = True
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub ShowSavedMessage()
        Dim saveMessage As String = Convert.ToString(Session("RawMaterialVendorSaveMsg"))
        If Not String.IsNullOrWhiteSpace(saveMessage) Then
            lblErrorMessage.Text = ""
            RmActionPopup.ShowSuccess(Me, saveMessage)
            Session.Remove("RawMaterialVendorSaveMsg")
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/RawMaterialVendorMstrList.aspx", False)
        Context.ApplicationInstance.CompleteRequest()
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
            BindVendorData(Request.QueryString(Constant.SessionKeys.UnitCode))
        Else
            Response.Redirect("~/RawMaterialVendorMstr.aspx?UnitCode=New", False)
            Context.ApplicationInstance.CompleteRequest()
        End If
    End Sub

    Private Sub BindVendorData(ByVal vendorCode As String)
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetRawMaterialVendorMasterEdit(vendorCode)

        If ds Is Nothing OrElse ds.Tables.Count = 0 OrElse ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim row As DataRow = ds.Tables(0).Rows(0)

        txtUnitCode.Text = Convert.ToString(ds.Tables(0).Rows(0)("vendor_code"))
        txtUnitCode.ReadOnly = True
        txtUnitName.Text = Convert.ToString(ds.Tables(0).Rows(0)("vendor_name"))
        txtUnitName.ReadOnly = True
        txtGstRegNo.Text = Convert.ToString(ds.Tables(0).Rows(0)("gst_no"))
        txtGstRegNo.ReadOnly = True
        txtLine1.Text = Convert.ToString(ds.Tables(0).Rows(0)("address"))
        txtCity.Text = Convert.ToString(ds.Tables(0).Rows(0)("city"))
        txtState.Text = Convert.ToString(ds.Tables(0).Rows(0)("state"))
        txtPin.Text = Convert.ToString(ds.Tables(0).Rows(0)("pincode"))
        txtContactPerson.Text = Convert.ToString(ds.Tables(0).Rows(0)("contact_person"))
        txtMobileNo.Text = Convert.ToString(ds.Tables(0).Rows(0)("mobile_no"))
        txtEmail.Text = Convert.ToString(ds.Tables(0).Rows(0)("email"))

        Dim activeValue As String = GetRowValue(row, "active").ToUpper()
        If activeValue = "N" Then
            rbtnActiveY.Checked = False
            rbtnActiveN.Checked = True
        Else
            rbtnActiveY.Checked = True
            rbtnActiveN.Checked = False
        End If
    End Sub

    Private Function GetSelectedActiveStatus() As String
        If rbtnActiveN.Checked Then
            Return Constant.Common.InActiveStatus
        End If
        Return Constant.Common.ActiveStatus
    End Function

    Private Sub GenerateNextVendorCode()
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetNextRawMaterialVendorCode()

        If Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0 Then
            txtUnitCode.Text = Convert.ToString(ds.Tables(0).Rows(0)("vendor_code")).Trim()
            txtUnitCode.ReadOnly = True
        End If
    End Sub

    Private Sub ClearFields()
        txtUnitName.Text = String.Empty
        txtGstRegNo.Text = String.Empty
        txtLine1.Text = String.Empty
        txtCity.Text = String.Empty
        txtState.Text = String.Empty
        txtEmail.Text = String.Empty
        rbtnActiveY.Checked = True
        rbtnActiveN.Checked = False
    End Sub

    Private Function ValidateVendorInputs() As Boolean
        ClearInlineValidation()

        Dim isValid As Boolean = True

        If String.IsNullOrWhiteSpace(txtUnitName.Text.Trim()) Then
            AppendInlineValidation("UnitName", "Please enter Vendor Name.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtGstRegNo.Text.Trim()) Then
            AppendInlineValidation("GstRegNo", "Please enter GST Registration Number.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtLine1.Text.Trim()) Then
            AppendInlineValidation("Line1", "Please enter Address.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtCity.Text.Trim()) Then
            AppendInlineValidation("City", "Please enter City.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtState.Text.Trim()) Then
            AppendInlineValidation("State", "Please enter State.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtPin.Text.Trim()) Then
            AppendInlineValidation("Pin", "Please enter Pincode.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(txtContactPerson.Text.Trim()) Then
            AppendInlineValidation("ContactPerson", "Please enter Contact Person.")
            isValid = False
        End If

        Dim mobileNo As String = txtMobileNo.Text.Trim()
        If String.IsNullOrWhiteSpace(mobileNo) Then
            AppendInlineValidation("MobileNo", "Please enter Mobile No.")
            isValid = False
        ElseIf Not System.Text.RegularExpressions.Regex.IsMatch(mobileNo, "^\d{10}$") Then
            AppendInlineValidation("MobileNo", "Mobile No. must be exactly 10 digits.")
            isValid = False
        End If

        Dim emailAddress As String = txtEmail.Text.Trim()
        If String.IsNullOrWhiteSpace(emailAddress) Then
            AppendInlineValidation("Email", "Please enter E-mail.")
            isValid = False
        ElseIf Not IsValidEmail(emailAddress) Then
            AppendInlineValidation("Email", "Please enter valid E-mail.")
            isValid = False
        End If

        Return isValid
    End Function

    Private Shared Function IsValidEmail(ByVal email As String) As Boolean
        Try
            Dim addr As New System.Net.Mail.MailAddress(email)
            Return String.Equals(addr.Address, email, StringComparison.OrdinalIgnoreCase)
        Catch
            Return False
        End Try
    End Function

    Private Sub ClearInlineValidation()
        txtUnitName.CssClass = "form-control"
        txtGstRegNo.CssClass = "form-control"
        txtLine1.CssClass = "form-control"
        txtCity.CssClass = "form-control"
        txtState.CssClass = "form-control"
        txtPin.CssClass = "form-control"
        txtContactPerson.CssClass = "form-control"
        txtMobileNo.CssClass = "form-control"
        txtEmail.CssClass = "form-control"
        valUnitName.Text = String.Empty
        valGstRegNo.Text = String.Empty
        valLine1.Text = String.Empty
        valCity.Text = String.Empty
        valState.Text = String.Empty
        valPin.Text = String.Empty
        valContactPerson.Text = String.Empty
        valMobileNo.Text = String.Empty
        valEmail.Text = String.Empty
    End Sub

    Private Sub AppendInlineValidation(ByVal fieldKey As String, ByVal message As String)
        Select Case fieldKey
            Case "UnitName"
                txtUnitName.CssClass = "form-control field-invalid"
                valUnitName.Text = message
            Case "GstRegNo"
                txtGstRegNo.CssClass = "form-control field-invalid"
                valGstRegNo.Text = message
            Case "Line1"
                txtLine1.CssClass = "form-control field-invalid"
                valLine1.Text = message
            Case "City"
                txtCity.CssClass = "form-control field-invalid"
                valCity.Text = message
            Case "State"
                txtState.CssClass = "form-control field-invalid"
                valState.Text = message
            Case "Pin"
                txtPin.CssClass = "form-control field-invalid"
                valPin.Text = message
            Case "ContactPerson"
                txtContactPerson.CssClass = "form-control field-invalid"
                valContactPerson.Text = message
            Case "MobileNo"
                txtMobileNo.CssClass = "form-control field-invalid"
                valMobileNo.Text = message
            Case "Email"
                txtEmail.CssClass = "form-control field-invalid"
                valEmail.Text = message
        End Select
    End Sub

    Private Sub ShowInlineValidation(ByVal fieldKey As String, ByVal message As String)
        ClearInlineValidation()
        AppendInlineValidation(fieldKey, message)
    End Sub

    Private Function GetRowValue(ByVal row As DataRow, ParamArray columnNames As String()) As String
        For Each columnName As String In columnNames
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return Convert.ToString(row(columnName)).Trim()
            End If
        Next
        Return String.Empty
    End Function
End Class
