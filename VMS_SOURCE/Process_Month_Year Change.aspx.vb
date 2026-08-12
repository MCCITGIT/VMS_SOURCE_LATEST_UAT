'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Process_Month_Year_Change.aspx.vb
'Created Date	: 01-March-2012
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for Process_Month_Year_Change.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Partial Class Process_Month_Year_Change
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckLogin()
            AddAttributes()
            PopulateYear()
            PopulateMonth()
        End If
    End Sub

#Region "Check Login"
    Private Sub CheckLogin()

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

    End Sub
#End Region

#Region "Add Attributes"
    Private Sub AddAttributes()
        txtProcessYear.Attributes.Add("onkeypress", "return ValidateIntegerFields('Process Year');")
        txtProcessMonth.Attributes.Add("onkeypress", "return ValidateIntegerFields('Process Month');")

        'txtProcessMonth.Attributes.Add("onblur", "return ValidateSystemMonth('" & txtProcessMonth.ClientID & "');")
        'txtProcessYear.Attributes.Add("onblur", "return ValidateSystemYear('" & txtProcessYear.ClientID & "');")

        btnSubmit.Attributes.Add("onClick", "return ValidateSubmit()")
    End Sub
#End Region

#Region "Populate TxtProcessYear"
    Private Sub PopulateYear()
        CheckLogin()

        Dim PrcssMntChng As New Process_Month_Year_Change_App
        Dim YearSet As New DataSet

        YearSet = PrcssMntChng.GetYear(Constant.Common.ActiveStatus)
        If (Not (YearSet Is Nothing) AndAlso YearSet.Tables.Count > 0 AndAlso Not (YearSet.Tables(0) Is Nothing) AndAlso YearSet.Tables(0).Rows.Count > 0) Then
            txtProcessYear.Text = YearSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessYear.DataBind()
        End If
    End Sub
#End Region

#Region "Populate TxtProcessMonth"
    Private Sub PopulateMonth()
        CheckLogin()

        Dim PrcssMntChng As New Process_Month_Year_Change_App
        Dim MonthSet As New DataSet

        MonthSet = PrcssMntChng.GetMonth(Constant.Common.ActiveStatus)
        If (Not (MonthSet Is Nothing) AndAlso MonthSet.Tables.Count > 0 AndAlso Not (MonthSet.Tables(0) Is Nothing) AndAlso MonthSet.Tables(0).Rows.Count > 0) Then
            txtProcessMonth.Text = MonthSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessMonth.DataBind()
        End If
    End Sub
#End Region

#Region "Update Process Year and Month"
    Private Function UpdateYrMnth() As Integer
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim PrcssMntChng As New Process_Month_Year_Change_App

        Dim Company As String = userInfo.userCompanyEntity
        Dim ProcessYear As String = txtProcessYear.Text.Trim
        Dim ProcessMonth As String = txtProcessMonth.Text.Trim

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            numRowsAffected = PrcssMntChng.UpdateYearMonth(Company, ProcessYear, ProcessMonth, sqlConn, sqlTrans)

            If numRowsAffected > 0 Then
                sqlTrans.Commit()
            Else
                sqlTrans.Rollback()
            End If
        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Server.Transfer(returnUrl)
        Finally
            If Not (sqlConn Is Nothing) Then
                'sqlConn is set to close state after completing the transaction
                sqlConn.Close()
                lblErrMsg.Text = "&bull; Record Inserted Successfully"
            End If
        End Try
    End Function
#End Region

#Region "Submit Button Click Event Handelling"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()
        UpdateYrMnth()
    End Sub
#End Region

#Region "Cancel Button Click Event Handelling"
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub
#End Region

End Class
