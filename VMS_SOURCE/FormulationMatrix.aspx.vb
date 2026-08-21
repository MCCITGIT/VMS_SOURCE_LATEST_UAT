Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports VMS.Web

Partial Class FormulationMatrix
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not txtProductSearch.Enabled OrElse String.Equals(txtProductSearch.Attributes("readonly"), "readonly", StringComparison.OrdinalIgnoreCase) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "lockProductSearch", "syncProductResetButtonState();", True)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()

        If Not IsPostBack Then
            ClearGrid()
            If Not String.IsNullOrWhiteSpace(Request.QueryString("producode")) Then
                hdnProductCode.Value = Convert.ToString(Request.QueryString("producode")).Trim()
                hdnEditHeaderId.Value = Convert.ToString(Request.QueryString("id")).Trim()
                If hdnSkucode.Value = "" Then
                    hdnSkucode.Value = hdnProductCode.Value
                End If
                If hdnProductName.Value <> "" Then
                    txtProductSearch.Text = hdnProductName.Value
                Else
                    txtProductSearch.Text = hdnProductCode.Value
                End If
                ApplyEditMode()
                Binddata()
            End If
        End If
    End Sub

    Private Sub CheckLogin()
        If Not (Session(Constant.SessionKeys.UserInfo) Is Nothing) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    <System.Web.Script.Services.ScriptMethod(),
    System.Web.Services.WebMethod()>
    Public Shared Function ProductSearch(ByVal prefixText As String) As String()
        Dim productDetails As List(Of String) = New List(Of String)()

        If String.IsNullOrWhiteSpace(prefixText) OrElse prefixText.Trim().Length < 3 Then
            Return productDetails.ToArray()
        End If

        Try
            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet = obj.GetProduct(prefixText.Trim())

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso Not ds.Tables(0) Is Nothing Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim productCode As String = Convert.ToString(dr("product_code")).Trim()
                    Dim productName As String = Convert.ToString(dr("product_name")).Trim()
                    Dim skuCode As String = If(dr.Table.Columns.Contains("sku_code"), Convert.ToString(dr("sku_code")).Trim(), String.Empty)

                    If productName <> "" AndAlso productCode <> "" Then
                        Dim itemValue As String = If(skuCode <> "", productCode & "|" & skuCode, productCode)
                        productDetails.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(productName, itemValue))
                    End If
                Next
            End If
        Catch
        End Try

        Return productDetails.ToArray()
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If String.IsNullOrWhiteSpace(hdnProductCode.Value) AndAlso String.IsNullOrWhiteSpace(hdnSkucode.Value) Then
            ShowValidation("Please select Product.")
            ClearGrid()
            Exit Sub
        End If

        txtProductSearch.Attributes("readonly") = "readonly"
        Binddata()
    End Sub

    Protected Sub btnResetProductPostback_Click(sender As Object, e As EventArgs)
        If IsEditMode() Then
            Exit Sub
        End If
        lblErrorMessage.Text = ""
        ClearGrid()
    End Sub

    Private Function IsEditMode() As Boolean
        Return ParseInteger(hdnEditHeaderId.Value) > 0
    End Function

    Private Sub ApplyEditMode()
        txtProductSearch.Attributes("readonly") = "readonly"
        If IsEditMode() Then
            btnSearch.Visible = False
        End If
    End Sub

    Private Sub Binddata()
        lblErrorMessage.Text = ""

        Dim productCode As String = hdnProductCode.Value.Trim()
        If productCode = "" Then
            productCode = hdnSkucode.Value.Trim()
        End If

        If productCode = "" Then
            ShowValidation("Please select Product.")
            ClearGrid()
            Return
        End If

        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetFormulation_MatrixBindList(productCode)

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim table As DataTable = ds.Tables(0)
            Dim headerId As Integer = ParseInteger(hdnEditHeaderId.Value)
            If headerId > 0 AndAlso table.Columns.Contains("header_id") Then
                Dim view As New DataView(table)
                view.RowFilter = "CONVERT(header_id, 'System.String') = '" & headerId.ToString() & "'"
                table = view.ToTable()
            End If

            If table.Rows.Count = 0 Then
                ClearGrid()
                ShowValidation("No formulation details found for the selected product.")
                Return
            End If

            If table.Columns.Contains("product_name") Then
                Dim productName As String = Convert.ToString(table.Rows(0)("product_name")).Trim()
                If productName <> "" Then
                    txtProductSearch.Text = productName
                    hdnProductName.Value = productName
                End If
            End If

            gvFormulationMatrix.DataSource = table
            gvFormulationMatrix.DataBind()
            btnSubmit.Visible = True
            SetSubmitButtonText()
        Else
            ClearGrid()
            ShowValidation("No formulation details found for the selected product.")
        End If
    End Sub

    Private Sub ClearGrid()
        gvFormulationMatrix.DataSource = Nothing
        gvFormulationMatrix.DataBind()
        btnSubmit.Visible = False
        btnSubmit.Text = "Submit"
    End Sub

    Protected Sub gvFormulationMatrix_PreRender(sender As Object, e As EventArgs) Handles gvFormulationMatrix.PreRender
        MergeFormulaSetColumn()
    End Sub

    Private Sub MergeFormulaSetColumn()
        Const formulaSetCol As Integer = 1

        For Each row As GridViewRow In gvFormulationMatrix.Rows
            If row.RowType <> DataControlRowType.DataRow OrElse row.Cells.Count <= formulaSetCol Then
                Continue For
            End If
            row.Cells(formulaSetCol).Visible = True
            row.Cells(formulaSetCol).RowSpan = 1
        Next

        If gvFormulationMatrix.Rows.Count < 2 Then
            Return
        End If

        Dim startIndex As Integer = 0
        Dim spanCount As Integer = 1

        For i As Integer = 1 To gvFormulationMatrix.Rows.Count - 1
            Dim currentText As String = GetFormulaSetText(gvFormulationMatrix.Rows(i))
            Dim previousText As String = GetFormulaSetText(gvFormulationMatrix.Rows(i - 1))

            If currentText <> "" AndAlso String.Equals(currentText, previousText, StringComparison.OrdinalIgnoreCase) Then
                spanCount += 1
                gvFormulationMatrix.Rows(i).Cells(formulaSetCol).Visible = False
                gvFormulationMatrix.Rows(startIndex).Cells(formulaSetCol).RowSpan = spanCount
            Else
                startIndex = i
                spanCount = 1
            End If
        Next
    End Sub

    Private Shared Function GetFormulaSetText(ByVal row As GridViewRow) As String
        If row Is Nothing Then
            Return String.Empty
        End If

        Dim lblFormulaSet As Label = CType(row.FindControl("lblFormulaSet"), Label)
        If lblFormulaSet IsNot Nothing Then
            Return Convert.ToString(lblFormulaSet.Text).Trim()
        End If

        Return String.Empty
    End Function

    Private Sub SetSubmitButtonText()
        If IsEditMode() Then
            btnSubmit.Text = "Update"
            Return
        End If

        Dim hasExisting As Boolean = False
        For Each gridRow As GridViewRow In gvFormulationMatrix.Rows
            Dim hdnId As HiddenField = CType(gridRow.FindControl("hdnId"), HiddenField)
            If hdnId IsNot Nothing AndAlso ParseInteger(hdnId.Value) > 0 Then
                hasExisting = True
                Exit For
            End If
        Next
        btnSubmit.Text = If(hasExisting, "Update", "Submit")
    End Sub

    Private Function CreateSaveTable() As DataTable
        Dim saveTable As DataTable = New DataTable()
        saveTable.Columns.Add("id", GetType(Integer))
        saveTable.Columns.Add("header_id", GetType(Integer))
        saveTable.Columns.Add("brand_code", GetType(String))
        saveTable.Columns.Add("vendor_code", GetType(String))
        saveTable.Columns.Add("product_code", GetType(String))
        saveTable.Columns.Add("rawmat_code", GetType(String))
        saveTable.Columns.Add("ratio", GetType(String))
        saveTable.Columns.Add("rate", GetType(String))
        Return saveTable
    End Function

    Private Function TryAddGridRowToSaveTable(ByVal gridRow As GridViewRow, ByVal saveTable As DataTable, ByRef errorMessage As String) As Boolean
        errorMessage = String.Empty

        Dim txtRate As TextBox = CType(gridRow.FindControl("txtRate"), TextBox)
        Dim hdnId As HiddenField = CType(gridRow.FindControl("hdnId"), HiddenField)
        Dim hdnHeaderId As HiddenField = CType(gridRow.FindControl("hdnHeaderId"), HiddenField)
        Dim hdnBrandCode As HiddenField = CType(gridRow.FindControl("hdnBrandCode"), HiddenField)
        Dim hdnVendorCode As HiddenField = CType(gridRow.FindControl("hdnVendorCode"), HiddenField)
        Dim hdnGridProductCode As HiddenField = CType(gridRow.FindControl("hdnGridProductCode"), HiddenField)
        Dim hdnRawMatCode As HiddenField = CType(gridRow.FindControl("hdnRawMatCode"), HiddenField)
        Dim lblRatio As Label = CType(gridRow.FindControl("lblRatio"), Label)

        Dim rateText As String = If(txtRate Is Nothing, String.Empty, Convert.ToString(txtRate.Text).Trim())
        Dim numericRate As Decimal = 0D
        If Not TryParseRate(rateText, numericRate) Then
            errorMessage = "Please enter a valid Rate greater than 0."
            Return False
        End If

        Dim rawMatCode As String = If(hdnRawMatCode Is Nothing, String.Empty, Convert.ToString(hdnRawMatCode.Value).Trim())
        If String.IsNullOrWhiteSpace(rawMatCode) Then
            errorMessage = "Raw material details are missing. Please select the product again."
            Return False
        End If

        Dim productCode As String = If(hdnGridProductCode Is Nothing, String.Empty, Convert.ToString(hdnGridProductCode.Value).Trim())
        If productCode = "" Then
            productCode = hdnProductCode.Value.Trim()
        End If
        If productCode = "" Then
            productCode = hdnSkucode.Value.Trim()
        End If

        Dim dr As DataRow = saveTable.NewRow()
        dr("id") = ParseInteger(If(hdnId Is Nothing, String.Empty, hdnId.Value))
        dr("header_id") = ParseInteger(If(hdnHeaderId Is Nothing, String.Empty, hdnHeaderId.Value))
        dr("brand_code") = If(hdnBrandCode Is Nothing, String.Empty, Convert.ToString(hdnBrandCode.Value).Trim())
        dr("vendor_code") = If(hdnVendorCode Is Nothing, String.Empty, Convert.ToString(hdnVendorCode.Value).Trim())
        dr("product_code") = productCode
        dr("rawmat_code") = rawMatCode
        dr("ratio") = If(lblRatio Is Nothing, String.Empty, Convert.ToString(lblRatio.Text).Trim())
        dr("rate") = numericRate.ToString("0.00", CultureInfo.InvariantCulture)
        saveTable.Rows.Add(dr)
        Return True
    End Function

    Private Function SaveFormulationMatrix(ByVal saveTable As DataTable) As Integer
        If saveTable Is Nothing OrElse saveTable.Rows.Count = 0 Then
            Return 0
        End If

        Dim obj As New OPC_VendorClass()
        Return obj.InsertFormulationMatrix(userInfo.userIDEntity, saveTable)
    End Function

    Private Shared Function TryParseRate(ByVal value As String, ByRef numericRate As Decimal) As Boolean
        numericRate = 0D
        If String.IsNullOrWhiteSpace(value) Then
            Return False
        End If

        Dim rateText As String = value.Trim()
        If Decimal.TryParse(rateText, NumberStyles.Number, CultureInfo.InvariantCulture, numericRate) OrElse
           Decimal.TryParse(rateText, NumberStyles.Number, CultureInfo.CurrentCulture, numericRate) Then
            Return numericRate > 0D
        End If

        Return False
    End Function

    Private Shared Function ParseInteger(ByVal value As String) As Integer
        Dim intValue As Integer = 0
        If Integer.TryParse(value, intValue) Then
            Return intValue
        End If

        Dim decimalValue As Decimal = 0D
        If Decimal.TryParse(value, decimalValue) Then
            Return CInt(decimalValue)
        End If

        Return 0
    End Function

    Protected Sub gvFormulationMatrix_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFormulationMatrix.RowCommand
        If e.CommandName <> "SaveRow" AndAlso e.CommandName <> "UpdateRow" Then
            Exit Sub
        End If

        Dim rowIndex As Integer = 0
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) Then
            Exit Sub
        End If
        If rowIndex < 0 OrElse rowIndex >= gvFormulationMatrix.Rows.Count Then
            Exit Sub
        End If

        Dim saveTable As DataTable = CreateSaveTable()
        Dim errorMessage As String = String.Empty
        If Not TryAddGridRowToSaveTable(gvFormulationMatrix.Rows(rowIndex), saveTable, errorMessage) Then
            ShowValidation(errorMessage)
            Exit Sub
        End If

        Try
            Dim rowsAffected As Integer = SaveFormulationMatrix(saveTable)
            If rowsAffected > 0 Then
                Binddata()
                Dim matrixId As Integer = ParseInteger(Convert.ToString(saveTable.Rows(0)("id")))
                Dim successMessage As String = If(matrixId > 0, "Record updated successfully.", "Submitted Successfully.")
                RmActionPopup.ShowSuccess(Me, successMessage)
            Else
                ShowValidation("Unable to save formulation matrix. Please try again.")
            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect("~/ExceptionPage.aspx")
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrWhiteSpace(hdnProductCode.Value) Then
            ShowValidation("Please select Product.")
            Exit Sub
        End If

        If gvFormulationMatrix.Rows.Count = 0 Then
            ShowValidation("No formulation details found for the selected product.")
            Exit Sub
        End If

        Dim saveTable As DataTable = CreateSaveTable()
        For Each gridRow As GridViewRow In gvFormulationMatrix.Rows
            If gridRow.RowType <> DataControlRowType.DataRow Then
                Continue For
            End If

            Dim errorMessage As String = String.Empty
            If Not TryAddGridRowToSaveTable(gridRow, saveTable, errorMessage) Then
                If errorMessage = "Please enter a valid Rate greater than 0." Then
                    ShowValidation("Please enter a valid Rate greater than 0 for all raw materials.")
                Else
                    ShowValidation(errorMessage)
                End If
                Exit Sub
            End If
        Next

        If saveTable.Rows.Count = 0 Then
            ShowValidation("No formulation details found for the selected product.")
            Exit Sub
        End If

        Try
            Dim rowsAffected As Integer = SaveFormulationMatrix(saveTable)
            If rowsAffected > 0 Then
                lblErrorMessage.Text = ""
                Dim successMessage As String = If(btnSubmit.Text = "Update", "Updated Successfully.", "Submitted Successfully.")
                RmActionPopup.ShowSuccess(Me, successMessage, "FormulationMatrixList.aspx")
            Else
                ShowValidation("Unable to save formulation matrix. Please try again.")
            End If
        Catch ex As Exception
            ShowValidation("Unable to save formulation matrix. Please try again.")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/FormulationMatrixList.aspx")
    End Sub

    Private Sub ShowValidation(ByVal message As String)
        lblErrorMessage.Text = ""
        RmActionPopup.ShowError(Me, message)
    End Sub
End Class
