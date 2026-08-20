Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class AddProductMaster
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        btnSubmit.Attributes.Add("onclick", "return validateInputs();")
        If Not IsPostBack Then
            BrandDetailsListLoad()
        End If
    End Sub
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim ProdMstrEntity As New ProductMasterEntity()
        Dim obj As New OPC_VendorClass()
        Dim RowsAffected As Integer
        Dim MsgID As Integer

        Try
            'Checking Access For Submit Button 
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            If Not String.IsNullOrEmpty(txtBrandName.Text.Trim()) Then
                If btnSubmit.Text = Constant.GeneralMessages.btnSubmit Then
                    ProdMstrEntity.PName = txtBrandName.Text
                    ProdMstrEntity.CreatedUser = userInfo.userIDEntity
                    ProdMstrEntity.Trantype = 1
                    ProdMstrEntity.ActiveStatus = "Y"

                    MsgID = obj.InsertUpdateBrandMasterDtls(ProdMstrEntity)

                    If MsgID = 1 Then
                        lblErrorMessage.Text = ""
                        txtBrandName.Text = ""
                        gvbrandDetails.PageIndex = 0
                        BrandDetailsListLoad()
                        RmActionPopup.ShowSuccess(Me, "Brand Saved Successfully.")
                    ElseIf MsgID = 2 Then
                        lblErrorMessage.Text = ""
                        RmActionPopup.ShowError(Me, "Brand with the same name already exists.")
                    Else
                        lblErrorMessage.Text = ""
                        RmActionPopup.ShowError(Me, "Brand not saved.")
                    End If
                End If
            Else
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Please enter brand name.")
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

        If RowsAffected > 0 Then
            Response.Redirect("~/AddProductMaster.aspx", True)
        End If
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Dim path = "~/AddProductMaster.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub
    Private Sub BrandDetailsListLoad()
        Dim ds As DataSet
        Dim obj As New OPC_VendorClass()
        ds = obj.GetBrandMasterList()

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                Dim rowCount As Integer = ds.Tables(0).Rows.Count
                Dim pageCount As Integer = CInt(Math.Ceiling(rowCount / CDbl(gvbrandDetails.PageSize)))
                If gvbrandDetails.PageIndex >= pageCount Then
                    gvbrandDetails.PageIndex = Math.Max(pageCount - 1, 0)
                End If
                gvbrandDetails.DataSource = ds
                gvbrandDetails.DataBind()
                UpdateBrandSummary(ds.Tables(0))
            Else
                gvbrandDetails.PageIndex = 0
                gvbrandDetails.DataSource = Nothing
                gvbrandDetails.DataBind()
                UpdateBrandSummary(Nothing)
            End If
        Else
            gvbrandDetails.PageIndex = 0
            UpdateBrandSummary(Nothing)
        End If
    End Sub

    Private Sub UpdateBrandSummary(ByVal brandTable As DataTable)
        Dim totalCount As Integer = 0
        Dim activeCount As Integer = 0
        Dim inactiveCount As Integer = 0

        If brandTable IsNot Nothing Then
            totalCount = brandTable.Rows.Count
            For Each row As DataRow In brandTable.Rows
                If NormalizeActiveValue(Convert.ToString(row("active"))) = "Y" Then
                    activeCount += 1
                Else
                    inactiveCount += 1
                End If
            Next
        End If

        lblTotalBrands.Text = totalCount.ToString()
        lblActiveBrands.Text = activeCount.ToString()
        lblInactiveBrands.Text = inactiveCount.ToString()
    End Sub
    Protected Sub gvbrandDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvbrandDetails.PageIndexChanging
        gvbrandDetails.EditIndex = -1
        gvbrandDetails.PageIndex = e.NewPageIndex
        BrandDetailsListLoad()
    End Sub

    Protected Sub gvbrandDetails_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvbrandDetails.RowEditing
        gvbrandDetails.EditIndex = e.NewEditIndex
        BrandDetailsListLoad()
    End Sub
    Protected Sub gvbrandDetails_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvbrandDetails.RowUpdating
        Dim Index As Integer = gvbrandDetails.EditIndex
        Dim btn2 As LinkButton
        Dim ddl As DropDownList
        Dim hdn As HiddenField
        Dim lbl As Label
        Dim brandId As Integer = 0

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        sqlConn = CType(DBFactory.GetHelper().OpenConnection(), SqlConnection)
        sqlTrans = sqlConn.BeginTransaction()

        Dim obj As New OPC_VendorClass()
        Dim ProdMstrEntity As New ProductMasterEntity()
        Dim MsgID As Integer

        btn2 = CType(gvbrandDetails.Rows(Index).FindControl("btnUpdate"), LinkButton)

        If btn2.CommandName = "Update" Then
            ddl = CType(gvbrandDetails.Rows(Index).FindControl("ddlactive"), DropDownList)
            Dim active As String = ddl.SelectedValue
            hdn = CType(gvbrandDetails.Rows(Index).FindControl("hdnBrandId"), HiddenField)
            lbl = CType(gvbrandDetails.Rows(Index).FindControl("lblbrandname"), Label)

            ProdMstrEntity.PID = hdn.Value
            ProdMstrEntity.PName = lbl.Text
            ProdMstrEntity.ActiveStatus = active
            ProdMstrEntity.CreatedUser = userInfo.userIDEntity
            ProdMstrEntity.Trantype = 2

            MsgID = obj.InsertUpdateBrandMasterDtls(ProdMstrEntity)

            If MsgID = 1 Then
                lblErrorMessage.Text = ""
                txtBrandName.Text = ""
                txtBrandId.Value = ""
                btnSubmit.Text = Constant.GeneralMessages.btnSubmit
                gvbrandDetails.EditIndex = -1
                BrandDetailsListLoad()
                RmActionPopup.ShowSuccess(Me, "Brand Updated Successfully.")
            Else
                lblErrorMessage.Text = ""
                RmActionPopup.ShowError(Me, "Unable to update brand.")
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
    Protected Sub gvbrandDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvbrandDetails.RowDataBound
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
    Protected Sub gvbrandDetails_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvbrandDetails.RowCancelingEdit
        gvbrandDetails.EditIndex = -1
        BrandDetailsListLoad()
    End Sub
End Class
