'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : IndentDetails.aspx.vb
'Created Date	: 20-December-2011
'Created By	    : Debayan Das
'Version	    : R02.00.00
'Description	: Code behind for Unitwise_SKU_Despatch.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes


Partial Class IndentDetails
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

#Region "PAGE LOAD"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            PopulateRegion()
            PopulateDepot()

            PopulateYear()
            PopulateMonth()
            LoadSearchCriteria()
            Dim DptDsptchdUntWise As New DepotDespatchUnitwiseApp
            btnSubmit.Attributes.Add("onClick", "return ValidateIndetDetailsYear('" + DptDsptchdUntWise.GetTopFinYear() + "','" + DptDsptchdUntWise.GetLastFinYear() + "');")
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
        Dim indentDetailsObject As New IndentDetailsClass
        Dim DepotSet As New DataSet

        DepotSet = indentDetailsObject.GetDepot(ddlRegion.SelectedValue, Constant.Common.ActiveStatus)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlLocation.DataSource = DepotSet.Tables(0)
            ddlLocation.DataTextField = "depot_name"
            ddlLocation.DataValueField = "depot_code"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.REGION) Then
            ddlLocation.SelectedValue = userInfo.userBranchEntity
            ddlLocation.Enabled = False
        End If
    End Sub
#End Region

#Region "Populate TxtProcessYear"
    Private Sub PopulateYear()
        CheckLogin()

        Dim DptDsptchdUntWise As New DepotDespatchUnitwiseApp
        Dim YearSet As New DataSet

        YearSet = DptDsptchdUntWise.GetYear(Constant.Common.ActiveStatus)
        If (Not (YearSet Is Nothing) AndAlso YearSet.Tables.Count > 0 AndAlso Not (YearSet.Tables(0) Is Nothing) AndAlso YearSet.Tables(0).Rows.Count > 0) Then
            'txtProcessYear.Text = "param_char_value"
            txtFinYear.Text = YearSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessYear.DataBind()
        End If
    End Sub
#End Region

#Region "Populate TxtProcessMonth"
    Private Sub PopulateMonth()
        CheckLogin()

        Dim DptDsptchdUntWise As New DepotDespatchUnitwiseApp
        Dim MonthSet As New DataSet

        MonthSet = DptDsptchdUntWise.GetMonth(Constant.Common.ActiveStatus)
        If (Not (MonthSet Is Nothing) AndAlso MonthSet.Tables.Count > 0 AndAlso Not (MonthSet.Tables(0) Is Nothing) AndAlso MonthSet.Tables(0).Rows.Count > 0) Then
            'txtProcessMonth.Text = MonthSet.Tables(0).Rows(0)
            'txtProcessMonth.Text = "param_char_value"
            txtMonth.Text = MonthSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessMonth.DataBind()
        End If
    End Sub
#End Region

#Region "Load Search Criteria"
    ' Loads the earlier search criteria when navigating back from a different screen
    Private Sub LoadSearchCriteria()
        If Not (Session(Constant.SessionKeys.DptDsptchUntWiseSearchInfo) Is Nothing) Then
            Dim DptDsptchUntWiseSearchInfo As New DepotDespatchUnitwiseSearchCriteria
            DptDsptchUntWiseSearchInfo = CType(Session(Constant.SessionKeys.DptDsptchUntWiseSearchInfo), DepotDespatchUnitwiseSearchCriteria)
            ddlRegion.SelectedValue = DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Region
            ddlLocation.SelectedValue = DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Depot

            txtMonth.Text = DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Month
            txtFinYear.Text = DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Finyear
            ddlPrntOptn.SelectedValue = DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_PrntOptn
        End If
    End Sub
#End Region

#Region "Save Search Criteria"
    Public Sub SaveSearchCriteria()
        CheckLogin()
        Dim DptDsptchUntWiseSearchInfo As New DepotDespatchUnitwiseSearchCriteria
        DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Region = ddlRegion.SelectedValue
        DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Depot = ddlLocation.SelectedValue

        DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Finyear = txtFinYear.Text.Trim
        DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_Month = txtMonth.Text.Trim
        DptDsptchUntWiseSearchInfo.Depot_Despatch_Unitwise_PrntOptn = ddlPrntOptn.SelectedValue
        Session(Constant.SessionKeys.DptDsptchUntWiseSearchInfo) = DptDsptchUntWiseSearchInfo
    End Sub
#End Region

#Region "btnSubmit Click Event Handeling"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()
        SaveSearchCriteria()
        lblErrMsg.Text = ""
        If ddlPrntOptn.SelectedValue = Constant.Common.ExcelFormat Then

            Dim indentDetailsObject As New IndentDetailsClass
            Dim ExcelSet As New DataSet

            ExcelSet = indentDetailsObject.GetExcelIndentDetails(ddlRegion.SelectedValue, ddlLocation.SelectedValue, txtFinYear.Text.Trim, txtMonth.Text.Trim)
            GridView1.DataSource = ExcelSet
            GridView1.DataBind()

            If (ExcelSet.Tables(0).Rows.Count > 0) Then
                lblErrMsg.Text = ""

                Dim rowcount, i As Integer
                rowcount = ExcelSet.Tables(0).Rows.Count
                ExcelSet.Tables(0).Columns.Add("Srl No.").SetOrdinal(0)
                For i = 0 To rowcount - 1
                    ExcelSet.Tables(0).Rows(i)("Srl No.") = i + 1
                Next
                Dim FileNme As String
                FileNme = Convert.ToString(userInfo.userIDEntity)
                FileNme = FileNme + "_" + "Indent_Details" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
                ExportToExcel(ExcelSet, Response, FileNme)

            Else
                lblErrMsg.Text = "No Records Found"
            End If

        Else
            'UNCOMMENT THIS IF YOU WANT TO SHOW IN REPORT VIEWER

            'Dim ReportViewer As New ReportViewer_DC

            'ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Depot_Despatch_Unitwise_Report
            'ReportViewer.ReportCase = Constant.ReportView.ReportCase.DptDsptchUntWiseRptCase

            'ReportViewer.Region = ddlRegion.SelectedValue
            'ReportViewer.DptDsptchdUntWiseDepot = ddlLocation.SelectedValue
            'ReportViewer.DptDsptchdUntWiseFinYear = txtFinYear.Text.Trim
            'ReportViewer.DptDsptchdUntWiseFinMonth = txtMonth.Text.Trim
            'ReportViewer.Active = Constant.Common.ActiveStatus

            'ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)
            lblErrMsg.Text = "Only Excel file is supported."

        End If
    End Sub
#End Region

#Region "Function to Export Dataset to Excel"
    Protected Sub ExportToExcel(ByVal dset As DataSet, ByVal Response As HttpResponse, ByVal filename As String)

        Try
            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "Indent-details" + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + "Process Year:" + txtFinYear.Text + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + "Process Month:" + txtMonth.Text + "</b></div><BR>")
            Response.Write("<b>" + "Company Name : " + userInfo.userCompanyEntity + "</b><BR>")
            'Response.Write("<img src='" + AppDomain.CurrentDomain.BaseDirectory + "/images/Berger.gif' /><BR>")
            'Response.Write("<div style='text-align:center;'><b>" + "From : " + fdate + "  to " + tdate + "</b></div><BR>")
            Response.Write("<div style='text-align:right;'><b>" + "Report Date : " + Format(Date.Today, "dd/MM/yyyy") + "</b></div><BR>")
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

#Region "BUTTON RESET CLICK"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("~/Home.aspx")
    End Sub
#End Region

#Region "REGION DROPDOWN SELECTED INDEX CHANGE"
    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
        'PopulateUnit()
    End Sub
#End Region

End Class
