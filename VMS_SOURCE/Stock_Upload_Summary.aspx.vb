'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Stock_Upload_Summary.aspx.vb
'Created Date	: 10-December-2011
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for Stock_Upload_Summary.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class Stock_Upload_Summary
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Request.QueryString("NoData") = "Yes") Then
            lblErrMsg.Text = "No record found!"
        Else
            lblErrMsg.Text = String.Empty
        End If

        CheckLogin()

        If Not IsPostBack Then
            PopulateYear()
            PopulateMonth()
            Dim StckUpld As New StockUploadSummaryApp
            btnSubmit.Attributes.Add("onClick", "return ValidateStckUpldSmmry('" + StckUpld.GetTopFinYear() + "','" + StckUpld.GetLastFinYear() + "');")
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

#Region "Populate TxtProcessYear"
    Private Sub PopulateYear()
        CheckLogin()

        Dim StckUpld As New StockUploadSummaryApp
        Dim YearSet As New DataSet

        YearSet = StckUpld.GetYear(Constant.Common.ActiveStatus)
        If (Not (YearSet Is Nothing) AndAlso YearSet.Tables.Count > 0 AndAlso Not (YearSet.Tables(0) Is Nothing) AndAlso YearSet.Tables(0).Rows.Count > 0) Then
            txtProcessYear.Text = YearSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessYear.DataBind()
        End If
    End Sub
#End Region

#Region "Populate TxtProcessMonth"
    Private Sub PopulateMonth()
        CheckLogin()

        Dim StckUpld As New StockUploadSummaryApp
        Dim MonthSet As New DataSet

        MonthSet = StckUpld.GetMonth(Constant.Common.ActiveStatus)
        If (Not (MonthSet Is Nothing) AndAlso MonthSet.Tables.Count > 0 AndAlso Not (MonthSet.Tables(0) Is Nothing) AndAlso MonthSet.Tables(0).Rows.Count > 0) Then
            txtProcessMonth.Text = MonthSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessMonth.DataBind()
        End If
    End Sub
#End Region

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        CheckLogin()

        Dim ReportViewer As New ReportViewer_DC

        ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Stock_Upload_Summary_Report
        ReportViewer.ReportCase = Constant.ReportView.ReportCase.StockUploadSummaryRptCase

        If userInfo.userGroupCodeEntity = "UNIT" Then
            ReportViewer.Unit = userInfo.userBranchEntity
        Else
            ReportViewer.Unit = String.Empty
        End If
        ReportViewer.Active = Constant.Common.ActiveStatus
        ReportViewer.StckUpldProcessYear = txtProcessYear.Text
        ReportViewer.StckUpldProcessMonth = txtProcessMonth.Text

        ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub
End Class
