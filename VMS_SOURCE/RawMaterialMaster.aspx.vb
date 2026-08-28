Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class RawMaterialMaster
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        btnSubmit.Attributes.Add("onclick", "return validateInputs();")
        If Not IsPostBack Then
            BindData()
        End If
    End Sub
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
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
            Dim searchText As String = prefixText.Trim()
            Dim ds As DataSet = obj.GetRawMaterial_SearchList(prefixText)

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
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim RawmatObj As New RawMaterialMasterEntity()
        Dim obj As New OPC_VendorClass()
        Dim RowsAffected As Integer
        Dim MsgID As Integer

        Try
            If String.IsNullOrEmpty(txtSearchText.Text.Trim()) Then
                ShowInlineValidation("Please enter Raw Material name.")
                Return
            End If

            If String.IsNullOrEmpty(txtrawmatid.Value.Trim()) Then
                ShowInlineValidation("Please select Raw Material from the list.")
                Return
            End If

            If btnSubmit.Text = Constant.GeneralMessages.btnSubmit Then
                RawmatObj.RawMatCode = txtrawmatid.Value
                RawmatObj.CreatedUser = userInfo.userIDEntity
                RawmatObj.Trantype = 1
                RawmatObj.ActiveStatus = "Y"

                MsgID = obj.InsertUpdateRawMatMasterDtls(RawmatObj)

                If MsgID = 1 Then
                    ClearInlineValidation()
                    lblErrorMessage.Text = ""
                    txtSearchText.Text = ""
                    txtrawmatid.Value = String.Empty
                    gvrawMatDetails.PageIndex = 0
                    BindData()
                    RmActionPopup.ShowSuccess(Me, "Raw Material Saved Successfully.")
                ElseIf MsgID = 2 Then
                    ShowInlineValidation("Raw Material with the same name already exists.")
                Else
                    ShowInlineValidation("Raw Material not saved.")
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

        If RowsAffected > 0 Then
            Response.Redirect("~/RawMaterialMaster.aspx", True)
        End If
    End Sub
    Private Sub BindData()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        ds = obj.GetRawmaterialMstrList()

        Dim table As DataTable = RmGridHelper.GetTable(ds)
        RmGridHelper.BindPaged(gvrawMatDetails, table)
        UpdateSummary(table)
    End Sub

    Private Sub UpdateSummary(ByVal sourceTable As DataTable)
        Dim totalCount As Integer = 0
        Dim activeCount As Integer = 0
        Dim inactiveCount As Integer = 0

        If sourceTable IsNot Nothing Then
            totalCount = sourceTable.Rows.Count
            For Each row As DataRow In sourceTable.Rows
                If NormalizeActiveValue(Convert.ToString(row("active"))) = "Y" Then
                    activeCount += 1
                Else
                    inactiveCount += 1
                End If
            Next
        End If

        lblTotalCount.Text = totalCount.ToString()
        lblActiveCount.Text = activeCount.ToString()
        lblInactiveCount.Text = inactiveCount.ToString()
    End Sub

    Protected Sub gvrawMatDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvrawMatDetails.PageIndexChanging
        gvrawMatDetails.EditIndex = -1
        gvrawMatDetails.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub gvrawMatDetails_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvrawMatDetails.RowEditing
        gvrawMatDetails.EditIndex = e.NewEditIndex
        BindData()
    End Sub
    Protected Sub gvrawMatDetails_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvrawMatDetails.RowUpdating
        Dim Index As Integer = gvrawMatDetails.EditIndex
        Dim btn2 As LinkButton
        Dim ddl As DropDownList
        Dim hdn As HiddenField

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        sqlConn = CType(DBFactory.GetHelper().OpenConnection(), SqlConnection)
        sqlTrans = sqlConn.BeginTransaction()

        Dim obj As New OPC_VendorClass()
        Dim RawmatObj As New RawMaterialMasterEntity()
        Dim MsgID As Integer

        btn2 = CType(gvrawMatDetails.Rows(Index).FindControl("btnUpdate"), LinkButton)

        If btn2.CommandName = "Update" Then
            ddl = CType(gvrawMatDetails.Rows(Index).FindControl("ddlactive"), DropDownList)
            Dim active As String = ddl.SelectedValue
            hdn = CType(gvrawMatDetails.Rows(Index).FindControl("hdnrawmatid"), HiddenField)

            RawmatObj.RawMatCode = hdn.Value
            RawmatObj.ActiveStatus = active
            RawmatObj.CreatedUser = userInfo.userIDEntity
            RawmatObj.Trantype = 2

            MsgID = obj.InsertUpdateRawMatMasterDtls(RawmatObj)

            If MsgID = 1 Then
                lblErrorMessage.Text = ""
                txtSearchText.Text = ""
                btnSubmit.Text = Constant.GeneralMessages.btnSubmit
                gvrawMatDetails.EditIndex = -1
                BindData()
                RmActionPopup.ShowSuccess(Me, "Raw Material Updated Successfully.")
            Else
                lblErrorMessage.Text = "Unable to update raw material."
            End If
        End If
    End Sub
    Private Function NormalizeActiveValue(ByVal dbValue As String) As String
        Dim value As String = Convert.ToString(dbValue).Trim().ToUpper()

        If value = "Y" OrElse value = "YES" OrElse value = "1" OrElse value = "TRUE" Then
            Return "Y"
        End If

        Return "N"
    End Function
    Protected Sub gvrawMatDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvrawMatDetails.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then Exit Sub

        Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)
        If rowView Is Nothing Then Exit Sub

        Dim activeValue As String = NormalizeActiveValue(Convert.ToString(rowView("active")))
        Dim ddl As DropDownList = CType(e.Row.FindControl("ddlactive"), DropDownList)
        If Not ddl Is Nothing Then
            If ddl.Items.FindByValue(activeValue) IsNot Nothing Then
                ddl.SelectedValue = activeValue
            End If
            ddl.Enabled = True
        End If

        If activeValue = "N" Then
            e.Row.Style("background-color") = "#ffe8ea"
        End If
    End Sub
    Protected Sub gvrawMatDetails_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvrawMatDetails.RowCancelingEdit
        gvrawMatDetails.EditIndex = -1
        BindData()
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Dim path = "~/RawMaterialMaster.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub

    Private Sub ClearInlineValidation()
        txtSearchText.CssClass = "form-control"
        valSearchText.Text = String.Empty
    End Sub

    Private Sub ShowInlineValidation(ByVal message As String)
        ClearInlineValidation()
        txtSearchText.CssClass = "form-control field-invalid"
        valSearchText.Text = message
    End Sub
End Class
