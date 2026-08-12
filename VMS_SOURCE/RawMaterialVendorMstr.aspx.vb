Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Partial Class RawMaterialVendorMstr
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()

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
            If String.IsNullOrWhiteSpace(txtUnitName.Text.Trim()) Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please enter Vendor Name."
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
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Vendor Code already exists."
                btnSubmit.Enabled = True
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Vendor Not Saved."
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
            lblErrorMessage.ForeColor = System.Drawing.Color.Green
            lblErrorMessage.Text = saveMessage
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

    Private Function GetRowValue(ByVal row As DataRow, ParamArray columnNames As String()) As String
        For Each columnName As String In columnNames
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return Convert.ToString(row(columnName)).Trim()
            End If
        Next
        Return String.Empty
    End Function
End Class
