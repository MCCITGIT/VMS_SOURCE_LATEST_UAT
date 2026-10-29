Imports System.Data
Imports VMS.Web

Partial Class OpcRawMaterialCreation
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private Const RmCodeKey As String = "RmCode"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CheckLogin()
            Page.MaintainScrollPositionOnPostBack = True

            If Not IsPostBack Then
                PopulateVendor()

                Dim rmCode As String = If(Request.QueryString(RmCodeKey), String.Empty).Trim()
                If Not String.IsNullOrWhiteSpace(rmCode) Then
                    ViewState("EditRmCode") = rmCode
                    btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                    SetEditFieldsVisible(True)
                    LoadSavedRecord(rmCode)
                Else
                    btnSubmit.Text = Constant.GeneralMessages.btnSubmit
                    SetEditFieldsVisible(False)
                    txtRmCode.Text = String.Empty
                    rbtnActiveY.Checked = True
                    rbtnActiveN.Checked = False
                End If
            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect("~/ExceptionPage.aspx")
        End Try
    End Sub

    Private ReadOnly Property IsEditMode As Boolean
        Get
            Return ViewState("EditRmCode") IsNot Nothing
        End Get
    End Property

    Private ReadOnly Property EditRmCode As String
        Get
            Return If(Convert.ToString(ViewState("EditRmCode")), String.Empty).Trim()
        End Get
    End Property

    Private Function IsUnitUser() As Boolean
        If userInfo Is Nothing Then
            Return False
        End If
        Return String.Equals(Convert.ToString(userInfo.userGroupCodeEntity).Trim(), Constant.UserFormAccess.UNIT, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function OwnUnitCode() As String
        If userInfo Is Nothing Then
            Return String.Empty
        End If
        Return Convert.ToString(userInfo.userBranchEntity).Trim()
    End Function

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub PopulateVendor()
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetUnitName(Constant.Common.ActiveStatus)

        ddlVendor.Items.Clear()
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            ddlVendor.DataSource = ds.Tables(0)
            ddlVendor.DataTextField = "unit_name"
            ddlVendor.DataValueField = "unit_code"
            ddlVendor.DataBind()
        End If
        ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

        If IsUnitUser() Then
            Dim own As String = OwnUnitCode()
            If Not String.IsNullOrWhiteSpace(own) AndAlso ddlVendor.Items.FindByValue(own) IsNot Nothing Then
                ddlVendor.ClearSelection()
                ddlVendor.Items.FindByValue(own).Selected = True
            End If
            ddlVendor.Enabled = False
        End If
    End Sub

    Private Sub LoadSavedRecord(ByVal rmCode As String)
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetOpcRawMaterialCreation(rmCode)

        If ds Is Nothing OrElse ds.Tables.Count = 0 OrElse ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
            valRmName.Text = "Raw material was not found."
            Return
        End If

        Dim row As DataRow = ds.Tables(0).Rows(0)
        Dim unitCode As String = Convert.ToString(row("unit_code")).Trim()

        If IsUnitUser() AndAlso Not String.Equals(unitCode, OwnUnitCode(), StringComparison.OrdinalIgnoreCase) Then
            Response.Redirect("~/OpcRawMaterialCreationList.aspx", False)
            Context.ApplicationInstance.CompleteRequest()
            Return
        End If

        txtRmCode.Text = Convert.ToString(row("rmm_code")).Trim()
        txtRmName.Text = Convert.ToString(row("rmm_material_name")).Trim()

        Dim vendorItem As ListItem = ddlVendor.Items.FindByValue(unitCode)
        If vendorItem Is Nothing AndAlso Not String.IsNullOrWhiteSpace(unitCode) Then
            vendorItem = New ListItem(Convert.ToString(row("vendor_name")).Trim(), unitCode)
            ddlVendor.Items.Add(vendorItem)
        End If
        If vendorItem IsNot Nothing Then
            ddlVendor.ClearSelection()
            vendorItem.Selected = True
        End If
        ddlVendor.Enabled = False

        Dim activeValue As String = Convert.ToString(row("active")).Trim().ToUpper()
        rbtnActiveY.Checked = (activeValue <> "N")
        rbtnActiveN.Checked = (activeValue = "N")
    End Sub

    Private Sub ClearFieldErrors()
        If ddlVendor.CssClass Is Nothing Then
            ddlVendor.CssClass = "form-control select2"
        Else
            ddlVendor.CssClass = ddlVendor.CssClass.Replace("field-invalid", "").Trim()
            If ddlVendor.CssClass.IndexOf("select2", StringComparison.OrdinalIgnoreCase) < 0 Then
                ddlVendor.CssClass = (ddlVendor.CssClass & " select2").Trim()
            End If
        End If
        txtRmName.CssClass = "form-control"
        valVendor.Text = String.Empty
        valRmName.Text = String.Empty
    End Sub

    Private Sub SetFieldError(ByVal control As WebControl, ByVal messageLabel As Label, ByVal message As String)
        If control IsNot Nothing Then
            Dim css As String = Convert.ToString(control.CssClass)
            If css.IndexOf("field-invalid", StringComparison.OrdinalIgnoreCase) < 0 Then
                control.CssClass = (css & " field-invalid").Trim()
            End If
        End If
        If messageLabel IsNot Nothing Then
            messageLabel.Text = message
        End If
    End Sub

    Private Sub ShowMessage(ByVal message As String, ByVal isSuccess As Boolean)
        lblMessage.Text = String.Empty
        RmActionPopup.Show(Me, message, isSuccess)
    End Sub

    Private Function GetSelectedUnitCode() As String
        If IsUnitUser() Then
            Return OwnUnitCode()
        End If
        Return Convert.ToString(ddlVendor.SelectedValue).Trim()
    End Function

    Private Sub SetEditFieldsVisible(ByVal visible As Boolean)
        pnlRmCode.Visible = visible
        pnlActive.Visible = visible
    End Sub

    Private Function GetSelectedActiveStatus() As String
        If Not IsEditMode Then
            Return "Y"
        End If
        If rbtnActiveN IsNot Nothing AndAlso rbtnActiveN.Checked Then
            Return "N"
        End If
        Return "Y"
    End Function

    Private Function ValidateInputs() As Boolean
        ClearFieldErrors()
        Dim isValid As Boolean = True
        Dim unitCode As String = GetSelectedUnitCode()
        Dim name As String = Convert.ToString(txtRmName.Text).Trim()

        If String.IsNullOrWhiteSpace(unitCode) Then
            SetFieldError(ddlVendor, valVendor, "Please select a vendor.")
            isValid = False
        End If

        If String.IsNullOrWhiteSpace(name) Then
            SetFieldError(txtRmName, valRmName, "Enter the raw material name.")
            isValid = False
        ElseIf name.Length < 3 Then
            SetFieldError(txtRmName, valRmName, "Raw material name must be at least 3 characters.")
            isValid = False
        End If

        Return isValid
    End Function

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs)
        CheckLogin()

        If Not ValidateInputs() Then
            Return
        End If

        Try
            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet
            If IsEditMode Then
                ds = obj.UpdateOpcRawMaterialCreation(EditRmCode, Convert.ToString(txtRmName.Text).Trim(), GetSelectedActiveStatus(), userInfo.userIDEntity)
            Else
                ds = obj.InsertOpcRawMaterialCreation(Convert.ToString(txtRmName.Text).Trim(), GetSelectedUnitCode(), userInfo.userIDEntity)
            End If

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim status As Integer = 0
                Integer.TryParse(Convert.ToString(ds.Tables(0).Rows(0)("status")), status)
                Dim message As String = Convert.ToString(ds.Tables(0).Rows(0)("message"))
                Dim savedCode As String = Convert.ToString(ds.Tables(0).Rows(0)("rmm_code")).Trim()

                If status = 1 Then
                    If IsEditMode Then
                        LoadSavedRecord(EditRmCode)
                    Else
                        txtRmName.Text = String.Empty
                        txtRmCode.Text = String.Empty
                        SetEditFieldsVisible(False)
                        If Not IsUnitUser() AndAlso ddlVendor.Items.Count > 0 Then
                            ddlVendor.ClearSelection()
                            ddlVendor.SelectedIndex = 0
                        End If
                        rbtnActiveY.Checked = True
                        rbtnActiveN.Checked = False
                    End If
                    If Not String.IsNullOrWhiteSpace(savedCode) AndAlso message.IndexOf(savedCode, StringComparison.OrdinalIgnoreCase) < 0 Then
                        message = message & " Code: " & savedCode
                    End If
                    ShowMessage(message, True)
                Else
                    SetFieldError(txtRmName, valRmName, message)
                    ShowMessage(message, False)
                End If
            Else
                ShowMessage("Something went wrong. Try again.", False)
            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect("~/ExceptionPage.aspx")
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        ClearFieldErrors()
        lblMessage.Text = String.Empty

        If IsEditMode Then
            LoadSavedRecord(EditRmCode)
        Else
            txtRmName.Text = String.Empty
            txtRmCode.Text = String.Empty
            SetEditFieldsVisible(False)
            If Not IsUnitUser() AndAlso ddlVendor.Items.Count > 0 Then
                ddlVendor.ClearSelection()
                ddlVendor.SelectedIndex = 0
            End If
            rbtnActiveY.Checked = True
            rbtnActiveN.Checked = False
        End If
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/OpcRawMaterialCreationList.aspx")
    End Sub
End Class
