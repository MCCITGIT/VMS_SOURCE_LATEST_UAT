Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class AddBrandMaster
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        btnSubmit.Attributes.Add("onclick", "return validateBrandListAdd();")
        If Not IsPostBack Then
            BrandDetailsListLoad()
        End If

    End Sub
#Region "Global Variable"
    Dim BrandId As Int64
#End Region
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim BrandMasterEntity As New BrandMasterEntity()

        Dim QualityControlClass As New QualityControlClass()

        Dim RowsAffected As Integer
        Dim MsgID As Integer

        Try
            'Checking Access For Submit Button 
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            If Not String.IsNullOrEmpty(txtBrandName.Text.Trim()) Then
                If btnSubmit.Text = Constant.GeneralMessages.btnSubmit Then

                    BrandMasterEntity.BName = txtBrandName.Text
                    BrandMasterEntity.CreatedUser = userInfo.userIDEntity
                    BrandMasterEntity.Trantype = 1

                    MsgID = QualityControlClass.InsertUpdateBrandMasterDtls(BrandMasterEntity)
                    If MsgID = 1 Then
                        lblErrorMessage.ForeColor = System.Drawing.Color.Green
                        lblErrorMessage.Text = "Brand Save Succssfully."
                        'btnSubmit.Enabled = False
                        txtBrandName.Text = ""
                        BrandDetailsListLoad()
                    ElseIf MsgID = 2 Then
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red
                        lblErrorMessage.Text = "Brand With Samename Already Present."
                    Else
                        lblErrorMessage.Text = "Brand Not Save."
                    End If
                ElseIf btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then

                    BrandMasterEntity.BID = txtBrandId.Value
                    BrandMasterEntity.BName = txtBrandName.Text
                    BrandMasterEntity.CreatedUser = userInfo.userIDEntity
                    BrandMasterEntity.Trantype = 2
                    MsgID = QualityControlClass.InsertUpdateBrandMasterDtls(BrandMasterEntity)
                    If MsgID = 1 Then
                        lblErrorMessage.ForeColor = System.Drawing.Color.Green
                        lblErrorMessage.Text = "Brand Updated Succssfully."
                        txtBrandName.Text = ""
                        txtBrandId.Value = ""
                        btnSubmit.Text = Constant.GeneralMessages.btnSubmit
                        BrandDetailsListLoad()

                    Else
                        lblErrorMessage.Text = "Brand Not Save."
                    End If
                End If
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please enter brand name."
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

        If RowsAffected > 0 Then
            Response.Redirect("~/AddBrandMaster.aspx", True)
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx", True)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Dim path = "~/AddBrandMaster.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub
    Private Sub BrandDetailsListLoad()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim QualityControlClass As New QualityControlClass()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim BrandMasterList As DataSet


        BrandMasterList = QualityControlClass.GetBrandMasterList()

        If (Not (BrandMasterList Is Nothing) AndAlso BrandMasterList.Tables.Count > 0) Then
            If (Not (BrandMasterList.Tables(0) Is Nothing) AndAlso BrandMasterList.Tables(0).Rows.Count > 0) Then
                gvbrandDetails.DataSource = BrandMasterList
                gvbrandDetails.DataBind()
                'di.Visible = False
            Else
                gvbrandDetails.DataSource = Nothing
                gvbrandDetails.DataBind()
                'Div_Lov_Details_Grid.Visible = True
            End If
        End If

    End Sub


    Protected Sub gvbrandDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvbrandDetails.SelectedIndexChanged

    End Sub


    Protected Sub gvbrandDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvbrandDetails.RowCommand
        Try
            Dim QualityControlClass As New QualityControlClass()
            Dim gv_row As GridViewRow = Nothing
            Dim index As Integer = Nothing
            Dim DS As New DataSet
            If e.CommandName = "EditRow" Then
                BrandId = Convert.ToString(e.CommandArgument)

                DS = QualityControlClass.GetBrandMasterByBrandId(BrandId)
                If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0) Then
                    For Each row As DataRow In DS.Tables(0).Rows
                        txtBrandName.Text = row.Item("brand_name")
                        txtBrandId.Value = BrandId
                        btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                        BrandDetailsListLoad()
                    Next
                Else
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    lblErrorMessage.Text = "Something went wrong. Try again."
                End If
            End If
        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        Finally
        End Try
    End Sub
End Class
