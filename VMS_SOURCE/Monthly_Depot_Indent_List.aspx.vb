'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Monthly_Depot_Indent_List.aspx.vb
'Created Date	: 15-December-2011
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for Monthly_Depot_Indent_List.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class Monthly_Depot_Indent_List
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
            PopulateRegion()
            PopulateDepot()
            PopulateYear()
            PopulateMonth()
            Dim MnthlyDptIndntLst As New MonthlyDepotIndentList_App
            btnSubmit.Attributes.Add("onClick", "return ValidateMnthlyDptIndntLst('" + MnthlyDptIndntLst.GetTopFinYear() + "','" + MnthlyDptIndntLst.GetLastFinYear() + "');")
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

#Region "Populate Region"
    Private Sub PopulateRegion()
        CheckLogin()

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlRegion.DataSource = OccupationTypeSet.Tables(0)
            ddlRegion.DataTextField = "lov_value"
            ddlRegion.DataValueField = "lov_code"
            ddlRegion.DataBind()
            ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlRegion.SelectedValue = userInfo.userRegionEntity
            ddlRegion.Enabled = False
        End If

    End Sub
#End Region

#Region "Populate Depot"
    Private Sub PopulateDepot()
        CheckLogin()

        Dim MnthlyDptIndntLst As New MonthlyDepotIndentList_App
        Dim DepotSet As New DataSet

        DepotSet = MnthlyDptIndntLst.GetDepot(ddlRegion.SelectedValue, Constant.Common.ActiveStatus)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlLocation.DataSource = DepotSet.Tables(0)
            ddlLocation.DataTextField = "depot_name"
            ddlLocation.DataValueField = "depot_code"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlLocation.SelectedValue = userInfo.userBranchEntity
            ddlLocation.Enabled = False
        End If
    End Sub
#End Region

#Region "Populate TxtProcessYear"
    Private Sub PopulateYear()
        CheckLogin()

        Dim MnthlyDptIndntLst As New MonthlyDepotIndentList_App
        Dim YearSet As New DataSet

        YearSet = MnthlyDptIndntLst.GetYear(Constant.Common.ActiveStatus)
        If (Not (YearSet Is Nothing) AndAlso YearSet.Tables.Count > 0 AndAlso Not (YearSet.Tables(0) Is Nothing) AndAlso YearSet.Tables(0).Rows.Count > 0) Then
            txtFinYear.Text = YearSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessYear.DataBind()
        End If
    End Sub
#End Region

#Region "Populate TxtProcessMonth"
    Private Sub PopulateMonth()
        CheckLogin()

        Dim MnthlyDptIndntLst As New MonthlyDepotIndentList_App
        Dim MonthSet As New DataSet

        MonthSet = MnthlyDptIndntLst.GetMonth(Constant.Common.ActiveStatus)
        If (Not (MonthSet Is Nothing) AndAlso MonthSet.Tables.Count > 0 AndAlso Not (MonthSet.Tables(0) Is Nothing) AndAlso MonthSet.Tables(0).Rows.Count > 0) Then
            txtMonth.Text = MonthSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessMonth.DataBind()
        End If
    End Sub
#End Region

#Region "btnSubmit Click Event Handeling"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()

        If ddlPrntOptn.SelectedValue = Constant.Common.ExcelFormat Then
            Dim MnthlyDptIndntLst As New MonthlyDepotIndentList_App
            Dim ExcelSet As New DataSet

            ExcelSet = MnthlyDptIndntLst.GetExcelMnthlyDptIndntLstRpt(ddlRegion.SelectedValue, ddlLocation.SelectedValue, txtFinYear.Text.Trim, txtMonth.Text.Trim, Constant.Common.ActiveStatus)
            If (ExcelSet.Tables(0).Rows.Count > 0) Then
                Dim i As Integer = ExcelSet.Tables(0).Rows.Count
                Dim FileNme As String
                FileNme = Convert.ToString(userInfo.userIDEntity)
                FileNme = FileNme + "_" + "Monthly_Depot_Indent_List" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
                ExportToExcel(ExcelSet, Response, FileNme)
            Else
                lblErrMsg.Text = "No Records Found"
            End If
        Else
            Dim ReportViewer As New ReportViewer_DC

            ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Monthly_Depot_Indent_List_Report
            ReportViewer.ReportCase = Constant.ReportView.ReportCase.MonthlyDepotIndentListRptCase

            ReportViewer.MnthlyDptIndntLstRptRegion = ddlRegion.SelectedValue
            ReportViewer.MnthlyDptIndntLstRptDepot = ddlLocation.SelectedValue
            ReportViewer.MnthlyDptIndntLstRptFinYear = txtFinYear.Text.Trim
            ReportViewer.MnthlyDptIndntLstRptMonth = txtMonth.Text.Trim
            ReportViewer.Active = Constant.Common.ActiveStatus

            ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)

        End If
    End Sub
#End Region

#Region "Function to Export Dataset to Excel"
    Protected Sub ExportToExcel(ByVal dset As DataSet, ByVal Response As HttpResponse, ByVal filename As String)

        Try
            Dim Year As String = txtFinYear.Text
            Dim Month As String = txtMonth.Text
            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "Monthly Depot Indent List" + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + " Process Year : " + Year + "</b><BR><b> Process Month : " + Month + "</b></div><BR>")
            Response.Write("<b>" + "Company Name : " + userInfo.userCompanyEntity + "</b><BR>")
            'Response.Write("<img src='" + AppDomain.CurrentDomain.BaseDirectory + "/images/Berger.gif' /><BR>")
            'Response.Write("<div style='text-align:right;'><b>" + "Report Date : " + Format(Date.Today, "dd/MM/yyyy") + "</b></div><BR>")
            Response.Write("<BR>")
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename + ".xls")
            ''Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Dim stringwrite As New System.IO.StringWriter
            Dim htmlwrite As New System.Web.UI.HtmlTextWriter(stringwrite)

            Dim dg As New GridView
            dg.DataSource = dset.Tables(0)
            dg.DataBind()

            dg.RenderControl(htmlwrite)

            Response.Write(stringwrite.ToString)

            Response.End()
        Catch ex As Exception
            'Dim returnUrl As String = "~/ExceptionPage.aspx"
            'Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorExporttoExcel
            'Server.Transfer(returnUrl)
        End Try
    End Sub
#End Region

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("~/Home.aspx")
    End Sub
End Class
