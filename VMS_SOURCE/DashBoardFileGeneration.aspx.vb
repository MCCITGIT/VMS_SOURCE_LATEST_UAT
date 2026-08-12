'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : DashBoardGeneration.aspx.vb
'Created Date	: 27/12/2011
'Created By	    : Riddhikusal Datta
'Version	    : R02.00.00
'Description	: Code behind for DashBoardGeneration.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports Microsoft.VisualBasic
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient

Partial Class DashBoardFileGeneration
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
#Region "Get Screen Details"
    Private Sub GetScreenDetails()
        Dim ScreenDS As System.Data.DataSet
        Dim DashObj As New DashboardClass
        ScreenDS = DashObj.GetSCreenDetails(userInfo.userBranchEntity)
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            ddlYear.SelectedValue = ScreenDS.Tables(0).Rows(0)("year").ToString
            ddlMonth.SelectedValue = ScreenDS.Tables(0).Rows(0)("month").ToString

        End If
    End Sub
#End Region
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        CheckLogin()
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()
        Dim dashObj As New DashboardClass
        Try
            numRowsAffected = dashObj.CreateDashboardFile(sqlConn, sqlTrans, userInfo.userIDEntity, ddlYear.SelectedValue, ddlMonth.SelectedValue)
            If numRowsAffected > 0 Then
                sqlTrans.Commit()
                lblMsg.Text = "File Generated Successfully"
            Else
                sqlTrans.Rollback()
            End If
        Catch ex As Exception
            sqlTrans.Rollback()
            lblMsg.Text = "<br/>File Generated Failed<br/>Error: " + ex.Message

        End Try


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetScreenDetails()
        End If
    End Sub
End Class
