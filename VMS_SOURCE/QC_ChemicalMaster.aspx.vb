
Imports System.Data
Imports VMS.Web

Partial Class QC_ChemicalMaster
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim ChemicalID As Int64
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            ChemicalDetailsList()
            'If Not (Request.QueryString.Count = 0) Then
            '    txtBrandName.Text = Request.QueryString("BrandName").ToString()
            '    btnSubmit.Text = "Update"
            'End If
            'btnSubmit.Attributes.Add("onclick", "return validateBrandListAdd();")
        End If

    End Sub

    Private Sub ChemicalDetailsList()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim QualityControlClass As New QualityControlClass()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ChemicalMasterList As DataSet
        ChemicalMasterList = QualityControlClass.GetChemicalMasterList()

        If (Not (ChemicalMasterList Is Nothing) AndAlso ChemicalMasterList.Tables.Count > 0) Then
            If (Not (ChemicalMasterList.Tables(0) Is Nothing) AndAlso ChemicalMasterList.Tables(0).Rows.Count > 0) Then
                gvChemicalList.DataSource = ChemicalMasterList
                gvChemicalList.DataBind()
                'di.Visible = False
            Else
                gvChemicalList.DataSource = Nothing
                gvChemicalList.DataBind()
                'Div_Lov_Details_Grid.Visible = True
            End If
        End If

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        If String.IsNullOrEmpty(txtChemicalName.Text) Then
            lblErrorMessage.Text = "Please enter Chemical Name"
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            lblErrorMessage.Font.Size = 10
            Return
        End If
        Dim QualityControlClass As New QualityControlClass()

        Dim RowsAffected As Integer
        Dim MsgID As Integer

        Try
            'Checking Access For Submit Button 
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            If btnSubmit.Text = Constant.GeneralMessages.btnSubmit Then

                MsgID = QualityControlClass.InsertUpdateChemicalMasterDtls(txtChemicalName.Text, userInfo.userIDEntity, 1, 0)
                If MsgID = 1 Then
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green
                    lblErrorMessage.Text = "Chmecial Master Save Succssfully."
                    'btnSubmit.Enabled = False
                    txtChemicalName.Text = ""
                    ChemicalDetailsList()
                ElseIf MsgID = 2 Then
                    lblErrorMessage.ForeColor = System.Drawing.Color.Yellow
                    lblErrorMessage.Text = "Chmecial With Samename Already Present."
                Else
                    lblErrorMessage.Text = "Chmecial Master Not Save."
                End If
            ElseIf btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then

                MsgID = QualityControlClass.InsertUpdateChemicalMasterDtls(txtChemicalName.Text, userInfo.userIDEntity, 2, hdnChemicalid.Value)
                If MsgID = 1 Then
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green
                    lblErrorMessage.Text = "Chemical Master Updated Succssfully."
                    txtChemicalName.Text = ""
                    hdnChemicalid.Value = ""
                    btnSubmit.Text = Constant.GeneralMessages.btnSubmit
                    ChemicalDetailsList()

                Else
                    lblErrorMessage.Text = "Chemical Master Not Save."
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

        If RowsAffected > 0 Then
            Response.Redirect("~/OC_ChemicalMaster.aspx", True)
        End If
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Dim path = "~/QC_ChemicalMaster.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Home.aspx", True)
    End Sub
    Protected Sub gvChemicalList_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            Dim QualityControlClass As New QualityControlClass()
            Dim gv_row As GridViewRow = Nothing
            Dim index As Integer = Nothing
            Dim DS As New DataSet
            If e.CommandName = "EditRow" Then
                ChemicalID = Convert.ToString(e.CommandArgument)

                DS = QualityControlClass.GetChemicalMasterById(ChemicalID)
                If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0) Then
                    For Each row As DataRow In DS.Tables(0).Rows
                        txtChemicalName.Text = row.Item("chemical_name")
                        hdnChemicalid.Value = ChemicalID
                        btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                        ChemicalDetailsList()
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
