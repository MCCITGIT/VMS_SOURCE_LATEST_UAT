Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Partial Class VendorRawMaterialLink
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private Const GridTableKey As String = "VendorRawMatGridTable"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        Page.MaintainScrollPositionOnPostBack = True

        If (Not IsPostBack) Then
            AddAttributes()
            PopulateVendor()
            InitializeGridTable()
            If Not String.IsNullOrWhiteSpace(Request.QueryString("vendorcode")) Then
                Dim queryVendorCode As String = Convert.ToString(Request.QueryString("vendorcode")).Trim()
                If ddlVendor.Items.FindByValue(queryVendorCode) IsNot Nothing Then
                    ddlVendor.SelectedValue = queryVendorCode
                End If

                Binddata()
            End If

        End If
    End Sub
#End Region
#Region "AddAttributes"
    Private Sub AddAttributes()
        btnSubmit.Attributes.Add("onclick", "return validateVendorRawMaterialLinkAdd();")
        btnAdd.Attributes.Add("onclick", "return validateAddRawmaterial();")
    End Sub
#End Region
#Region "PopulateVendor"
    Public Sub PopulateVendor()
        Dim obj As New OPC_VendorClass()
        Dim ds As New DataSet()
        ds = obj.GetRawMaterialVendorList()

        ddlVendor.Items.Clear()
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlVendor.DataSource = ds.Tables(0)
            ddlVendor.DataTextField = "vendor_name"
            ddlVendor.DataValueField = "vendor_code"
            ddlVendor.DataBind()
        End If
        ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub
#End Region
    Private Sub InitializeGridTable()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add(New DataColumn("id", GetType(String)))
        dt.Columns.Add(New DataColumn("vendor_code", GetType(String)))
        dt.Columns.Add(New DataColumn("vendor_name", GetType(String)))
        dt.Columns.Add(New DataColumn("rawmat_code", GetType(String)))
        dt.Columns.Add(New DataColumn("rawmat_name", GetType(String)))
        dt.Columns.Add(New DataColumn("rate", GetType(String)))
        dt.Columns.Add(New DataColumn("active", GetType(String)))
        ViewState(GridTableKey) = dt
    End Sub
    Private Function GetGridTable() As DataTable
        Dim dt As DataTable = TryCast(ViewState(GridTableKey), DataTable)
        If dt Is Nothing Then
            InitializeGridTable()
            dt = TryCast(ViewState(GridTableKey), DataTable)
        End If
        Return dt
    End Function
    Private Sub BindRawMatGrid()
        'If gvVendorRawMat.Columns.Count > 4 Then
        '    gvVendorRawMat.Columns(4).Visible = (gvVendorRawMat.EditIndex >= 0)
        'End If
        gvVendorRawMat.DataSource = GetGridTable()
        gvVendorRawMat.DataBind()
    End Sub
    Private Sub CaptureGridRates()
        Dim dt As DataTable = GetGridTable()
        For i As Integer = 0 To gvVendorRawMat.Rows.Count - 1
            Dim txtRate As TextBox = CType(gvVendorRawMat.Rows(i).FindControl("txtRate"), TextBox)
            If i < dt.Rows.Count AndAlso Not txtRate Is Nothing Then
                dt.Rows(i)("rate") = Convert.ToString(txtRate.Text).Trim()
            End If
        Next
        ViewState(GridTableKey) = dt
    End Sub
    <System.Web.Script.Services.ScriptMethod(),
    System.Web.Services.WebMethod()>
    Public Shared Function RawMaterialSearch(ByVal prefixText As String) As String()
        Dim rawMaterialDetails As List(Of String) = New List(Of String)()

        If String.IsNullOrWhiteSpace(prefixText) OrElse prefixText.Trim().Length < 3 Then
            Return rawMaterialDetails.ToArray()
        End If

        Try
            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet = obj.GetRawMatList(prefixText.Trim())

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0) Is Nothing Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim rawMaterialId As String = Convert.ToString(dr("Raw_Mat_Code")).Trim()
                    Dim rawMaterialName As String = Convert.ToString(dr("Raw_Mat_Name")).Trim()

                    If rawMaterialName <> "" AndAlso rawMaterialId <> "" Then
                        rawMaterialDetails.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rawMaterialName, rawMaterialId))
                    End If
                Next
            End If
        Catch ex As Exception
            ' Keep autocomplete resilient; return whatever is already collected.
        End Try

        Return rawMaterialDetails.ToArray()
    End Function
    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtSearchText.Text = String.Empty
        txtrawmatid.Value = String.Empty
        lblErrorMessage.Text = ""
        'gvVendorRawMat.EditIndex = -1
        'Binddata()
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' If a row is being edited, auto-cancel edit mode before adding a new row.
        If gvVendorRawMat.EditIndex >= 0 Then
            gvVendorRawMat.EditIndex = -1
        End If

        CaptureGridRates()
        btnSubmit.Visible = True

        If ddlVendor.SelectedIndex <= 0 Then
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "Please select Vendor."
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtrawmatid.Value) Then
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Text = "Please enter Raw Material."
            Exit Sub
        End If

        Dim dt As DataTable = GetGridTable()
        Dim selectedRawMatCode As String = txtrawmatid.Value.Trim()
        Dim selectedVendorCode As String = ddlVendor.SelectedValue.Trim()

        For Each row As DataRow In dt.Rows
            If Convert.ToString(row("vendor_code")).Trim().Equals(selectedVendorCode, StringComparison.OrdinalIgnoreCase) AndAlso
               Convert.ToString(row("rawmat_code")).Trim().Equals(selectedRawMatCode, StringComparison.OrdinalIgnoreCase) Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Selected Raw Material already added."
                Exit Sub
            End If
        Next

        Dim rawMatName As String = txtSearchText.Text.Trim()
        If rawMatName.Contains("(") Then
            rawMatName = rawMatName.Substring(0, rawMatName.LastIndexOf("("c)).Trim()
        End If

        Dim dr As DataRow = dt.NewRow()
        dr("id") = String.Empty
        dr("vendor_code") = ddlVendor.SelectedValue
        dr("vendor_name") = ddlVendor.SelectedItem.Text
        dr("rawmat_code") = selectedRawMatCode
        dr("rawmat_name") = rawMatName
        dr("rate") = String.Empty
        dr("active") = "Y"
        dt.Rows.Add(dr)
        ViewState(GridTableKey) = dt

        gvVendorRawMat.EditIndex = -1
        BindRawMatGrid()
        txtSearchText.Text = String.Empty
        txtrawmatid.Value = String.Empty
        lblErrorMessage.Text = ""
    End Sub
    Protected Sub gvVendorRawMat_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvVendorRawMat.RowCommand
        If e.CommandName <> "DeleteRow" Then
            Exit Sub
        End If

        CaptureGridRates()
        'btnSubmit.Visible = False

        Dim rowIndex As Integer = 0
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) Then
            Exit Sub
        End If

        Dim dt As DataTable = GetGridTable()
        If rowIndex >= 0 AndAlso rowIndex < dt.Rows.Count Then
            Dim linkId As Integer = 0
            Integer.TryParse(Convert.ToString(dt.Rows(rowIndex)("id")), linkId)
            If linkId > 0 Then
                Dim obj As New OPC_VendorClass()
                obj.UpdateVendorRawMaterialLink(linkId, "N", userInfo.userIDEntity)
                Binddata()
                lblErrorMessage.ForeColor = System.Drawing.Color.Green
                lblErrorMessage.Text = "Record updated successfully."
                Exit Sub
            Else
                dt.Rows.RemoveAt(rowIndex)
                ViewState(GridTableKey) = dt
            End If
        End If
        BindRawMatGrid()
    End Sub
    Protected Sub gvVendorRawMat_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvVendorRawMat.RowEditing
        CaptureGridRates()
        gvVendorRawMat.EditIndex = e.NewEditIndex
        BindRawMatGrid()
    End Sub
    Protected Sub gvVendorRawMat_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvVendorRawMat.RowCancelingEdit
        gvVendorRawMat.EditIndex = -1
        BindRawMatGrid()
    End Sub
    Protected Sub gvVendorRawMat_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvVendorRawMat.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then Exit Sub

        Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)
        If rowView Is Nothing Then Exit Sub

        Dim txtRate As TextBox = CType(e.Row.FindControl("txtRate"), TextBox)
        Dim hdnId As HiddenField = CType(e.Row.FindControl("hdnId"), HiddenField)
        Dim rowId As String = String.Empty
        If Not hdnId Is Nothing Then
            rowId = Convert.ToString(hdnId.Value).Trim()
        End If

        ' Existing DB rows should not allow rate edit; only active status can be changed in edit mode.
        If Not txtRate Is Nothing AndAlso rowId <> "" Then
            txtRate.ReadOnly = True
            txtRate.Enabled = False
        End If

        If (e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit Then
            Dim ddlactive As DropDownList = CType(e.Row.FindControl("ddlactive"), DropDownList)
            If Not ddlactive Is Nothing Then
                Dim activeValue As String = NormalizeActiveValue(Convert.ToString(rowView("active")))
                If ddlactive.Items.FindByValue(activeValue) IsNot Nothing Then
                    ddlactive.SelectedValue = activeValue
                Else
                    ddlactive.SelectedValue = "N"
                End If
            End If
        End If
    End Sub
    Protected Sub gvVendorRawMat_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvVendorRawMat.RowUpdating
        CaptureGridRates()
        Dim row As GridViewRow = gvVendorRawMat.Rows(e.RowIndex)
        Dim hdnId As HiddenField = CType(row.FindControl("hdnId"), HiddenField)
        Dim ddlactive As DropDownList = CType(row.FindControl("ddlactive"), DropDownList)
        Dim txtRate As TextBox = CType(row.FindControl("txtRate"), TextBox)

        Dim newActive As String = "N"
        If Not ddlactive Is Nothing Then
            newActive = NormalizeActiveValue(ddlactive.SelectedValue)
        End If

        Dim dt As DataTable = GetGridTable()
        If e.RowIndex >= 0 AndAlso e.RowIndex < dt.Rows.Count Then
            dt.Rows(e.RowIndex)("active") = newActive
            If Not txtRate Is Nothing Then
                dt.Rows(e.RowIndex)("rate") = Convert.ToString(txtRate.Text).Trim()
            End If
            ViewState(GridTableKey) = dt
        End If

        Dim linkId As Integer = 0
        Integer.TryParse(Convert.ToString(If(hdnId Is Nothing, String.Empty, hdnId.Value)), linkId)

        If linkId > 0 Then
            Dim obj As New OPC_VendorClass()
            Dim rowsAffected As Integer = obj.UpdateVendorRawMaterialLink(linkId, newActive, userInfo.userIDEntity)
            If rowsAffected > 0 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Green
                lblErrorMessage.Text = "Record updated successfully."
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Unable to update record."
            End If
        End If

        gvVendorRawMat.EditIndex = -1
        If linkId > 0 Then
            Binddata()
        Else
            BindRawMatGrid()
        End If
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/RawmaterialList.aspx", True)
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim path = "~/VendorRawMaterialLink.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim RowsAffectedMstr As Integer
        Dim obj As New OPC_VendorClass
        ''---------------------
        Try
            Dim Vendor As String = String.Empty
            If ddlVendor.SelectedIndex > 0 Then
                Vendor = Convert.ToString(ddlVendor.SelectedValue)
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please select Vendor."
                Return
            End If

            CaptureGridRates()
            Dim dtGrid As DataTable = GetGridTable()
            Dim dt1 As DataTable = New DataTable()

            dt1.Columns.Add(New System.Data.DataColumn("vendor_code", GetType(String)))
            dt1.Columns.Add(New System.Data.DataColumn("rawmat_code", GetType(String)))
            dt1.Columns.Add(New System.Data.DataColumn("rate", GetType(String)))

            For Each gridRow As DataRow In dtGrid.Rows
                Dim existingId As Integer = 0
                Integer.TryParse(Convert.ToString(gridRow("id")), existingId)
                If existingId <= 0 Then
                    Dim dr1 As DataRow = dt1.NewRow()
                    dr1.Item("vendor_code") = Convert.ToString(gridRow("vendor_code")).Trim()
                    dr1.Item("rawmat_code") = Convert.ToString(gridRow("rawmat_code")).Trim()
                    dr1.Item("rate") = Convert.ToString(gridRow("rate")).Trim()
                    dt1.Rows.Add(dr1)
                End If
            Next
            dt1.AcceptChanges()
            If dt1.Rows.Count <= 0 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please add at least one new Raw Material."
                Return
            End If

            RowsAffectedMstr = obj.InsertVendorRawmaterialLink(userInfo.userIDEntity, dt1)

            If RowsAffectedMstr > 0 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Green
                lblErrorMessage.Text = "Submitted Successfully"
                ddlVendor.SelectedIndex = -1
                txtSearchText.Text = String.Empty
                txtrawmatid.Value = String.Empty
                gvVendorRawMat.DataSource = Nothing
                gvVendorRawMat.Visible = False

            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Something went wrong. Try again."
            End If

        Catch ex As SqlException
            If ex.Number = 2627 OrElse ex.Number = 2601 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Same Vendor and Raw Material already exists."
                gvVendorRawMat.EditIndex = -1
                Binddata()
                Return
            End If

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        Finally

        End Try
    End Sub
    Private Sub Binddata()
        Dim selectedVendorCode As String = String.Empty
        If ddlVendor.SelectedIndex > 0 Then
            selectedVendorCode = Convert.ToString(ddlVendor.SelectedValue).Trim()
        ElseIf Not String.IsNullOrWhiteSpace(Request.QueryString("vendorcode")) Then
            selectedVendorCode = Convert.ToString(Request.QueryString("vendorcode")).Trim()
            If selectedVendorCode <> "" AndAlso ddlVendor.Items.FindByValue(selectedVendorCode) IsNot Nothing Then
                ddlVendor.SelectedValue = selectedVendorCode
            End If
        End If
        ddlVendor.Enabled = False
        If selectedVendorCode = "" Then
            InitializeGridTable()
            BindRawMatGrid()
            Exit Sub
        End If

        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetVendorRawMatEditList(selectedVendorCode)
        Dim dt As DataTable = GetGridTable()
        dt.Rows.Clear()

        If Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) Then
            For Each dbRow As DataRow In ds.Tables(0).Rows
                Dim dr As DataRow = dt.NewRow()
                dr("id") = Convert.ToString(dbRow("id"))
                dr("vendor_code") = Convert.ToString(dbRow("vendor_code"))
                dr("vendor_name") = Convert.ToString(dbRow("vendor_name"))
                dr("rawmat_code") = Convert.ToString(dbRow("rawmat_code"))
                dr("rawmat_name") = Convert.ToString(dbRow("rawmat_name"))
                dr("rate") = Convert.ToString(dbRow("rate"))
                dr("active") = NormalizeActiveValue(dbRow("active"))
                dt.Rows.Add(dr)
            Next

            'If dt.Rows.Count > 0 AndAlso String.IsNullOrWhiteSpace(txtrawmatid.Value) AndAlso String.IsNullOrWhiteSpace(txtSearchText.Text) Then
            '    Dim firstRawCode As String = Convert.ToString(dt.Rows(0)("rawmat_code")).Trim()
            '    Dim firstRawName As String = Convert.ToString(dt.Rows(0)("rawmat_name")).Trim()
            '    txtrawmatid.Value = firstRawCode
            '    If firstRawName <> "" AndAlso firstRawCode <> "" Then
            '        txtSearchText.Text = firstRawName & " (" & firstRawCode & ")"
            '    End If
            'End If
        End If

        ViewState(GridTableKey) = dt
        BindRawMatGrid()
    End Sub
    Private Function NormalizeActiveValue(ByVal dbValue As String) As String
        Dim activeText As String = Convert.ToString(dbValue).Trim().ToUpper()
        If activeText = "Y" OrElse activeText = "YES" OrElse activeText = "1" OrElse activeText = "TRUE" Then
            Return "Y"
        End If
        Return "N"
    End Function
End Class
