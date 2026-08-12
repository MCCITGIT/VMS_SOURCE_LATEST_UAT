Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient

Partial Class MaxDespatchLimit
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Login Check"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region
#Region "Bind Grid"
    Private Sub BindGrid()
        Dim DespatchDS As DataSet
        Dim DespatchObj As New MaxDespLimitClass
        DespatchDS = DespatchObj.GetDespLimitDetail()
        If (Not (DespatchDS Is Nothing) AndAlso DespatchDS.Tables.Count > 0 AndAlso Not (DespatchDS.Tables(0) Is Nothing) AndAlso DespatchDS.Tables(0).Rows.Count > 0) Then
            gvDespDtl.DataSource = DespatchDS
            gvDespDtl.DataBind()
        Else
            gvDespDtl.DataSource = DespatchDS
            gvDespDtl.DataBind()
        End If
    End Sub
#End Region
#Region "Add Attribute"
    Private Sub AddAttribute()
        btnSubmit.Attributes.Add("onclick", "return ValidateSubmit();")
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MaintainScrollPositionOnPostBack = True
            CheckLogin()
            BindGrid()
            AddAttribute()
            If Request.QueryString("mode") = "error" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Error Occured');</script>", False)
            ElseIf Request.QueryString("mode") = "success" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Submitted Succesfully');</script>", False)
            End If
        End If
    End Sub

    Protected Sub gvDespDtl_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDespDtl.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Text = (e.Row.RowIndex + 1)
            Dim txtLimit As TextBox = e.Row.FindControl("txtLimit")
            txtLimit.Attributes.Add("onkeypress", "KeyPressDecimal();")
        End If
    End Sub

#Region "Update Limit"
    Private Sub UpdateLimit()
        Dim redirectMode As String
        CheckLogin()
        Dim DespatchObj As New MaxDespLimitClass
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()
        Try
            Dim lblUnitCode As Label
            Dim txtLimit As TextBox

            For i As Integer = 0 To gvDespDtl.Rows.Count - 1
                lblUnitCode = gvDespDtl.Rows(i).FindControl("lblUnitCode")
                txtLimit = gvDespDtl.Rows(i).FindControl("txtLimit")

                numRowsAffected = DespatchObj.UpdateMaxLimit(lblUnitCode.Text.Trim, Convert.ToDecimal(txtLimit.Text.Trim), userInfo.userIDEntity, sqlConn, sqlTrans)
                If Not numRowsAffected > 0 Then
                    sqlTrans.Rollback()
                    redirectMode = "error"
                    GoTo z
                End If
            Next
            sqlTrans.Commit()
            redirectMode = "success"
        Catch ex As Exception
            sqlTrans.Rollback()
            redirectMode = "error"
        End Try
z:      sqlConn.Close()
        Response.Redirect("MaxDespatchLimit.aspx?mode=" + redirectMode)
    End Sub
#End Region

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        UpdateLimit()
    End Sub
End Class
