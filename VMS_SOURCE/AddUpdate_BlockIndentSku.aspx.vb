Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports VMS.Web
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.HSSF.Util
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Partial Class AddUpdate_BlockIndentSku
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        AddAttributes()
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub
#End Region
#Region "Check Login"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region
    Private Sub AddAttributes()
        btnSubmit.Attributes.Add("OnClick", "return validateSubmit();")
        imgbtnSearch.Attributes.Add("OnClick", "return validateSearch();")
    End Sub
    Protected Sub Populate_Sku()
        Try
            Dim ds As DataSet = New DataSet()
            Dim admixObj As BlockIndentClass = New BlockIndentClass()
            ds = admixObj.Get_Sku_Details(txtSkuCode.Text)

            If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                txtSkuDesc.Text = ds.Tables(0).Rows(0)("Sku_Desc").ToString()
            Else
                txtSkuDesc.Text = ""
                lblErrorMessage.Text = "No Record Found"
                lblErrorMessage.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorBindingGrid
            Throw ex
        End Try
    End Sub
    Protected Sub txtSkuCode_TextChanged(sender As Object, e As EventArgs)
        Populate_Sku()
    End Sub
    Protected Sub BindGrid()
        Try
            Dim ds As DataSet = New DataSet()
            Dim obj As BlockIndentClass = New BlockIndentClass()
            ds = obj.Get_BlockIndent_Sku_Details(txtSearchCode.Text)

            If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                gvSkucode.DataSource = ds.Tables(0)
                gvSkucode.DataBind()
            Else
                gvSkucode.DataSource = Nothing
                gvSkucode.DataBind()
                lblErrorMessage.Text = "No Record Found"
                lblErrorMessage.ForeColor = Drawing.Color.Red
                txtSearchCode.Text = ""
            End If

        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorBindingGrid
            Throw ex
        End Try
    End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs)
        If txtSearchCode.Text = "" Then
            lblErrorMessage.Text = "Enter SKU Code."
            Exit Sub
        End If
        BindGrid()
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim intnumberroweffect As Integer
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        sqlConn = CType(DBFactory.GetHelper().OpenConnection(), SqlConnection)
        sqlTrans = sqlConn.BeginTransaction()
        Dim updt As BlockIndentClass = New BlockIndentClass()

        If txtSkuCode.Text = "" Then
            lblErrorMessage.Text = "Enter SKU Code."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Return
        End If

        Try
            intnumberroweffect = updt.InsertUpdate_BlockSku_Details(txtSkuCode.Text, txtSkuDesc.Text, userInfo.userIDEntity, sqlConn, sqlTrans)

            If intnumberroweffect > 0 Then
                sqlTrans.Commit()
                ModalPopupExtender1.Show()
                lblMsg.Text = "Submitted Successfully."
                lblMsg.ForeColor = System.Drawing.Color.Green
                'lblErrorMessage.Text = "Submitted Successfully."
                'lblErrorMessage.ForeColor = System.Drawing.Color.Green
            Else
                lblErrorMessage.Text = "Error Occured."
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                sqlTrans.Rollback()
            End If

        Catch ex As Exception
            sqlTrans.Rollback()
            lblErrorMessage.Text = "Error Occured. Contact Administrator." & Environment.NewLine & "Error: " + ex.Message
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
        Finally

            If Not (sqlConn Is Nothing) Then
                sqlConn.Close()
                ClearData()
            End If
        End Try
    End Sub
    Protected Sub ClearData()
        txtSearchCode.Text = ""
        txtSkuCode.Text = ""
        txtSkuDesc.Text = ""
        BindGrid()
    End Sub
    Protected Sub gvSkucode_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSkucode.RowCommand
        If e.CommandName = "EditRow" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvSkucode.Rows(index)
            Dim lblSkuCode As Label = CType(row.FindControl("lblSkuCode"), Label)
            Dim lblSkuDesc As Label = CType(row.FindControl("lblSkuDesc"), Label)

            Dim intnumberroweffect As Integer
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            sqlConn = CType(DBFactory.GetHelper().OpenConnection(), SqlConnection)
            sqlTrans = sqlConn.BeginTransaction()
            Dim updt As BlockIndentClass = New BlockIndentClass()

            Try
                intnumberroweffect = updt.Delete_BlockSku_Details(lblSkuCode.Text, userInfo.userIDEntity, sqlConn, sqlTrans)

                If intnumberroweffect > 0 Then
                    sqlTrans.Commit()
                    ModalPopupExtender1.Show()
                    lblMsg.Text = "Deleted Successfully."
                    lblMsg.ForeColor = System.Drawing.Color.Green
                    'lblErrorMessage.Text = "Deleted Successfully."
                    'lblErrorMessage.ForeColor = System.Drawing.Color.Green
                Else
                    lblErrorMessage.Text = "Error Occured."
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    sqlTrans.Rollback()
                End If

            Catch ex As Exception
                sqlTrans.Rollback()
                lblErrorMessage.Text = "Error Occured. Contact Administrator." & Environment.NewLine & "Error: " + ex.Message
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
            Finally

                If Not (sqlConn Is Nothing) Then
                    sqlConn.Close()
                    ClearData()
                End If
            End Try
        End If
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx", True)
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ClearData()
        lblErrorMessage.Text = ""
    End Sub
End Class
