'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : PendingDespatches.aspx.vb
'Created Date	: 01-Feb-2012
'Created By	    : Deepak Yadav
'Version	    : R02.00.00
'Description	: Code behind for PendingDespatches.aspx Page

'****************************************************************
Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

'Added for Excel Report using dll <Begin>
Imports System.Web.Security
Imports System.Security.Principal
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Excel
Imports System.Reflection
Imports System.Runtime.InteropServices.Marshal
'<End>

Partial Class MonthLoadVsDespatches
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Dim LOGON32_LOGON_INTERACTIVE As Integer = 2
    Dim LOGON32_PROVIDER_DEFAULT As Integer = 0

    Dim impersonationContext As WindowsImpersonationContext

    Declare Function LogonUserA Lib "advapi32.dll" (ByVal lpszUsername As String, _
                            ByVal lpszDomain As String, _
                            ByVal lpszPassword As String, _
                            ByVal dwLogonType As Integer, _
                            ByVal dwLogonProvider As Integer, _
                            ByRef phToken As IntPtr) As Integer

    Declare Auto Function DuplicateToken Lib "advapi32.dll" ( _
                            ByVal ExistingTokenHandle As IntPtr, _
                            ByVal ImpersonationLevel As Integer, _
                            ByRef DuplicateTokenHandle As IntPtr) As Integer

    Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long
    Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long


    Dim kgTotal As Decimal = 0.0
    Dim LtrTotal As Decimal = 0.0
    Dim Nop As Integer = 0
#Region "Page Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        CheckLogin()
        If Not IsPostBack Then
            PopulateProcessYr()
            PopulateRegion()
            PopulateDepot()
            PopulateUnit()
            LoadSearchCriteria()
            'ddlProcessYr.Enabled = False
            'ddlProcessMnth.Enabled = False
            ddlReportFormat.Enabled = False
            txtAsOnDate.Text = Format(Date.Today, "dd/MM/yyyy")
            txtAsOnDate.Enabled = False
        End If
    End Sub
#End Region
#Region "Populate Process Year"
    Private Sub PopulateProcessYr()
        CheckLogin()

        Dim ProcessYr As New Common
        Dim StandrdParams As New MonthlyUnitDespatch
        Dim YearSet As New DataSet
        Dim StandardYrMnth As New DataSet

        YearSet = ProcessYr.GetFinYrDetails(Constant.Common.Company, Constant.Common.ActiveStatus)
        StandardYrMnth = StandrdParams.GetMnthsYr(Constant.Common.ActiveStatus)
        If (Not (YearSet Is Nothing) AndAlso YearSet.Tables.Count > 0 AndAlso Not (YearSet.Tables(0) Is Nothing) AndAlso YearSet.Tables(0).Rows.Count > 0) Then
            ddlProcessYr.DataSource = YearSet.Tables(0)
            ddlProcessYr.DataTextField = "fin_year"
            ddlProcessYr.DataValueField = "fin_year"
            ddlProcessYr.DataBind()
            'ddlProcessYr.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            'ddlProcessYr.Items.Insert(0, New ListItem("2011", String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlProcessYr.SelectedValue = userInfo.currentFinancialYearEntity
        '    ddlProcessYr.Enabled = False
        'End If


        If (Not (StandardYrMnth Is Nothing) AndAlso StandardYrMnth.Tables.Count > 0 AndAlso Not (StandardYrMnth.Tables(0) Is Nothing) AndAlso StandardYrMnth.Tables(0).Rows.Count > 0) Then
            ddlProcessYr.SelectedValue = StandardYrMnth.Tables(0).Rows(0)("param_char_value")
            ddlProcessMnth.SelectedValue = StandardYrMnth.Tables(0).Rows(1)("param_char_value")
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

        Dim GeDepot As New Common
        Dim DepotSet As New DataSet

        DepotSet = GeDepot.Getdepotname(ddlRegion.SelectedValue)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = DepotSet.Tables(0)
            ddlDepot.DataTextField = "depot_name"
            ddlDepot.DataValueField = "depot_code"
            ddlDepot.DataBind()
            'ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
            ddlDepot.SelectedValue = userInfo.userBranchEntity
            ddlDepot.Enabled = False
        Else
            ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
    End Sub
#End Region
#Region "Populate Unit"
    Private Sub PopulateUnit()
        CheckLogin()

        Dim UnitDespatch As New MonthLoadVsDespatchesClass
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, ddlRegion.SelectedValue)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            'ddlUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlUnit.SelectedValue = userInfo.userUnitEntity
        '    ddlUnit.Enabled = False
        'End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlUnit.SelectedValue = userInfo.userBranchEntity
            ddlUnit.Enabled = False
        End If
    End Sub
#End Region
#Region "Populate Depot ,Unit for Selected Region"
    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        'SaveSearchCriteria()
        PopulateDepot()
        'PopulateUnit()
    End Sub
#End Region
#Region "Button Submit For Report"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()
        Try
            SaveSearchCriteria()
            lblErrMsg.Text = String.Empty
            If ddlReportFormat.SelectedValue = Constant.Common.ExcelFormat Then

                Dim UserId As String = ConfigurationManager.AppSettings.Get("ServerUserID")
                Dim Domain As String = ConfigurationManager.AppSettings.Get("ServerDomain")
                Dim Password As String = ConfigurationManager.AppSettings.Get("ServerPassword")
                If impersonateValidUser(UserId, Domain, Password) Then
                    'ExportToExcelSheet(dsDailyReportCollection_Due, Response)


                    Dim company As String = userInfo.userCompanyEntity
                    Dim region As String = ddlRegion.SelectedValue
                    Dim depot As String = ddlDepot.SelectedValue
                    Dim unit As String = ddlUnit.SelectedValue
                    Dim ProcessYr As String = ddlProcessYr.SelectedValue
                    Dim ProcessMnth As String = ddlProcessMnth.SelectedValue
                    Dim OrderBy_Depot_Sku As String = ddlOrderBy.SelectedValue

                    'Dim fromdate As DateTime = IIf(txtFromDate.Text.Trim <> String.Empty, FormatDate(txtFromDate.Text), DateTime.MinValue)
                    'Dim todate As DateTime = IIf(txtToDate.Text.Trim <> String.Empty, FormatDate(txtToDate.Text), DateTime.MinValue)
                    'Dim todaydate As DateTime = Date.Today
                    Dim active As String = Constant.Common.ActiveStatus

                    Dim reportObj As New MonthLoadVsDespatchesClass
                    Dim ExcelSet As DataSet
                    ExcelSet = reportObj.PendingDespatch_Report(active, region, depot, ProcessYr, ProcessMnth, unit, OrderBy_Depot_Sku)
                    If (ExcelSet.Tables(0).Rows.Count > 0) Then
                        'ExcelSet.Tables(0).Columns.Add("DT. OF SENDING")
                        'ExcelSet.Tables(0).Columns.Add("CN85")
                        Dim rowcount, i As Integer
                        rowcount = ExcelSet.Tables(0).Rows.Count


                        ' For i = 0 To rowcount - 1

                        'ExcelSet.Tables(0).Rows(i)("Srl") = i + 1
                        'Nop = Nop + ExcelSet.Tables(0).Rows(i)("NOP")
                        'kgTotal = kgTotal + ExcelSet.Tables(0).Rows(i)("LTR")
                        'LtrTotal = LtrTotal + ExcelSet.Tables(0).Rows(i)("KG")

                        'ExcelSet.Tables(0).Rows(i)("LTR") = IIf(ExcelSet.Tables(0).Rows(i)("LTR") <> "0.00", ExcelSet.Tables(0).Rows(i)("LTR"), String.Empty)
                        'ExcelSet.Tables(0).Rows(i)("KG") = IIf(ExcelSet.Tables(0).Rows(i)("KG") <> "0.00", ExcelSet.Tables(0).Rows(i)("KG"), String.Empty)


                        'If ExcelSet.Tables(0).Rows(i)("Approved Y/N") = "Y" Then
                        '    ExcelSet.Tables(0).Rows(i)("Approved Y/N") = "Yes"
                        'End If
                        'If ExcelSet.Tables(0).Rows(i)("Approved Y/N") = "N" Then
                        '    ExcelSet.Tables(0).Rows(i)("Approved Y/N") = "No"
                        'End If

                        'Next

                        Dim FileNme As String
                        FileNme = Convert.ToString(userInfo.userCompanyEntity)
                        FileNme = FileNme + "_" + "Pending Despatches" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
                        'ExportToExcel(ExcelSet, Response, FileNme)
                        ExportToExcelSheet(ExcelSet, Response)
                    Else
                        lblErrMsg.Text = "No Data Found"
                    End If

                    undoImpersonation()
                End If
                    'Else



                    'If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
                    '    userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
                    'Else
                    '    Response.Redirect("~/Login.aspx")
                    'End If

                    ''SaveSearchCriteria()

                    'Dim ReportViewer As New ReportViewer_DC

                    'ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.MonthlyUnitDespatch
                    'ReportViewer.ReportCase = Constant.ReportView.ReportCase.MonthlyUnitDespatchReportCase
                    ''End If
                    'ReportViewer.Company = Constant.Common.Company
                    'ReportViewer.Active = Constant.Common.ActiveStatus
                    'ReportViewer.Region = ddlRegion.SelectedValue
                    'ReportViewer.Depot = ddlDepot.SelectedValue
                    'ReportViewer.Unit = ddlUnit.SelectedValue
                    'ReportViewer.ProcessYr = ddlProcessYr.SelectedValue
                    'ReportViewer.ProcessMnth = ddlProcessMnth.SelectedValue
                    'ReportViewer.ReportType = ddlReportFormat.SelectedValue


                    'ClientScript.RegisterStartupScript(Me.GetType(), "ShowReport", "<script language='javascript'>fnNewWindow('ReportViewer.aspx')</script>")
                End If
        Catch ex As Exception

        End Try

    End Sub
#End Region
#Region "Function to Export Dataset to Excel"
    Protected Sub ExportToExcel(ByVal dset As DataSet, ByVal Response As HttpResponse, ByVal filename As String)

        'Dim fdate As String = (txtFromDate.Text)
        'Dim tdate As String = (txtToDate.Text)
        Dim processyr As String = dset.Tables(0).Rows(0)("Process Year")
        Dim processMnths As String = dset.Tables(0).Rows(0)("Process Month")
        Dim unit As String = dset.Tables(0).Rows(0)("Source")
        Try
            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "Pending Despatches" + "</b></div>")
            'Response.Write("<div style='text-align:right;'><b>" + "Report Date:" + Format(Date.Today, "dd/MM/yyyy") + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + "As on: " + Format(DateTime.Now, "dd/MM/yyyy") + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + " Process Year: " + processyr + "</b></div>")
            Response.Write("<div style='text-align:center;'><b>" + "Process Month: " + processMnths + "</b></div>")
            Response.Write("<div style='text-align:left;'><b>" + "Source: " + unit + "</b></div>")

            'Response.Write("<b>" + "Company Name : " + userInfo.userCompanyEntity + "</b><BR>")
            'Response.Write("<div style='text-align:left;'><b>" + "From Date: " + fdate + "</b></div>")
            'Response.Write("<div style='text-align:left;'><b>" + "  To Date: " + tdate + "</b></div>")


            'Response.Write("<BR>")
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename + ".xls")
            ''Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Dim stringwrite As New System.IO.StringWriter
            Dim htmlwrite As New System.Web.UI.HtmlTextWriter(stringwrite)

            dset.Tables(0).Columns.Remove("Process Year")
            dset.Tables(0).Columns.Remove("Process Month")
            dset.Tables(0).Columns.Remove("Source")
            Dim dg As New GridView

            'AddHandler dg.RowDataBound, AddressOf dg_RowDataBound

            ''dg.ShowFooter = True
            'AddHandler dg.RowCreated, New GridViewRowEventHandler(AddressOf GridUpdate)
            dg.DataSource = dset.Tables(0)
            dg.DataBind()

            dg.RenderControl(htmlwrite)

            Dim str As String = stringwrite.ToString


            Response.Write(stringwrite.ToString)
            'Response.Write("<div style='text-align:right;'><b>" + "Total:" + temp + "</b></div><BR>")
            'Response.Write("<BR>")
            'Response.Write("<div style='text-align:right;'><b>" + " Total&nbsp;&nbsp;&nbsp;&nbsp;: &nbsp;&nbsp;&nbsp;&nbsp;" + CType(temp, String) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CType(temp1, String) + "</b></div><BR>")

            'Response.Write("<div style='text-align:left;'><b>" + "All Total" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CType(Nop, String) + " &nbsp;" + CType(kgTotal, String) + "&nbsp;" + CType(LtrTotal, String) + "</b></div><BR>")

            'Response.Write("<div style='text-align:left;'><b>" + "All Total" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CType(Bank_Charge_Total, String) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CType(Amount_Total, String) + "</b></div><BR>")

            Response.End()
        Catch ex As Exception
            'Dim returnUrl As String = "~/ExceptionPage.aspx"
            'Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorExporttoExcel
            'Server.Transfer(returnUrl)
            'MsgBox(ex.Message)
        End Try


    End Sub
#End Region
#Region "Date Format"

    Public Function FormatDate(ByVal stringdate As String) As DateTime
        If Not (stringdate = String.Empty) Then

            Dim ddate As String() = stringdate.Split("/")
            Dim arrlist As New ArrayList
            Dim index As Integer = 0

            While index <= ddate.Length - 1
                arrlist.Add(ddate(index))
                System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
            End While
            Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(0))
            Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
            Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(2))

            Dim dt As DateTime = New DateTime(yyyy, mm, dd)
            dt = FormatDateTime(dt, DateFormat.LongDate)

            Return dt
        End If
    End Function
#End Region
#Region "Button Cacel Event"
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub
#End Region

#Region "Save Search Criteria"

    ' Saves the current search criteria in session
    Private Sub SaveSearchCriteria()

        Dim SearchInfo As New MonthLoadVsDespatchesEntity

        SearchInfo.Region = ddlRegion.SelectedValue
        SearchInfo.Depot = ddlDepot.SelectedValue
        SearchInfo.Unit = ddlUnit.SelectedValue
        SearchInfo.ProcessYr = ddlProcessYr.SelectedValue
        SearchInfo.ProcessMnth = ddlProcessMnth.SelectedValue
        SearchInfo.ReportFormat = ddlReportFormat.SelectedValue
        SearchInfo.OrderBy = ddlOrderBy.SelectedValue
        Session(Constant.SessionKeys.MonthLoadVsDespatchSearchInfo) = SearchInfo
    End Sub
#End Region

#Region "Load Search Criteria"

    '' Loads the earlier search criteria when navigating back from a different screen
    Private Sub LoadSearchCriteria()

        If Not (Session(Constant.SessionKeys.MonthLoadVsDespatchSearchInfo) Is Nothing) Then
            Dim SearchInfo As New MonthLoadVsDespatchesEntity
            SearchInfo = CType(Session(Constant.SessionKeys.MonthLoadVsDespatchSearchInfo), MonthLoadVsDespatchesEntity)

            ddlRegion.SelectedValue = SearchInfo.Region
            ddlDepot.SelectedValue = SearchInfo.Depot
            ddlUnit.SelectedValue = SearchInfo.Unit
            ddlProcessYr.SelectedValue = SearchInfo.ProcessYr
            ddlProcessMnth.SelectedValue = SearchInfo.ProcessMnth
            ddlReportFormat.SelectedValue = SearchInfo.ReportFormat
            ddlOrderBy.SelectedValue = SearchInfo.OrderBy


        End If

    End Sub

#End Region
#Region "Export to Excel using Dll"
    Protected Sub ExportToExcelSheet(ByVal dset As DataSet, ByVal Response As HttpResponse)

        Dim Rows As Integer
        Dim oExcel As New Microsoft.Office.Interop.Excel.Application
        Dim oBooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim oBook As Microsoft.Office.Interop.Excel.Workbook
        Dim oSheets As Microsoft.Office.Interop.Excel.Sheets
        Dim oSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oCells As Microsoft.Office.Interop.Excel.Range
        Dim sFile As String
        Dim sTemplate As String
        Dim DateString As String = "_" + CType(DateTime.Now.Day, String) + "_" + CType(DateTime.Now.Month, String) + "_" + CType(DateTime.Now.Year, String)
        Dim Datagrd As New DataGrid

        Datagrd.DataSource = dset.Tables(0)
        Datagrd.DataBind()


        'for Excel Format <begin>

        If Datagrd.Items.Count > 0 Then
            sFile = Server.MapPath(Request.ApplicationPath) & "\Excel_Reports\Month_Load_Vs_Despatches" + DateString + ".xls"
            sTemplate = Server.MapPath(Request.ApplicationPath) & "\Templates\Month_Load_Vs_Despatches.xls"
            oExcel.Visible = False
            oExcel.DisplayAlerts = False

            oBooks = oExcel.Workbooks
            oBooks.Open(Server.MapPath(Request.ApplicationPath) & "\Templates\Month_Load_Vs_Despatches.xls") 'Load colorful template with chart 
            oBook = oBooks.Item(1)
            oSheets = oBook.Worksheets
            oSheet = CType(oSheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)
            oSheet.Name = "Monthly_Load_Vs_Despatch"
            oCells = oSheet.Cells
            Rows = 8

            oSheet.Range("H2").Value = txtAsOnDate.Text
            oSheet.Range("H3").Value = ddlProcessYr.SelectedValue
            oSheet.Range("H4").Value = ddlProcessMnth.SelectedValue
            oSheet.Range("H5").Value = ddlOrderBy.SelectedItem.Text

            Dim Sku As String = Nothing
            Dim Product As String = Nothing
            Dim Depot As String = Nothing
            Dim V_TOT_1 As Double
            Dim V_TOT_2 As Double
            Dim V_TOT_3 As Double
            Dim V_TOT_4 As Double
            Dim V_TOT_5 As Double
            'Dim V_TOT_6 As Double

            Dim V_TOT_11 As Double
            Dim V_TOT_21 As Double
            Dim V_TOT_31 As Double
            Dim V_TOT_41 As Double
            Dim V_TOT_51 As Double

            Dim V_TOT_G_1 As Double
            Dim V_TOT_G_2 As Double
            Dim V_TOT_G_3 As Double
            Dim V_TOT_G_4 As Double
            'Dim V_TOT_G_5 As Double
            'Dim V_TOT_G_6 As Double
            Dim ServiceLvl As Double
            Dim Load As Double
            Dim Despatch As Double
            Dim count As Integer = 0
            If ddlOrderBy.SelectedValue = "D" Then
                For i = 0 To Datagrd.Items.Count - 1
                    count = count + 1

                    'MsgBox(Datagrd.Items(i).Cells(5).Text)
                    If Depot <> "" And Depot <> Datagrd.Items(i).Cells(5).Text Then

                        oSheet.Range("D" & Rows).Value = Depot & " Total :"
                        oSheet.Range("J" & Rows).Value = V_TOT_1
                        oSheet.Range("K" & Rows).Value = V_TOT_2
                        oSheet.Range("L" & Rows).Value = V_TOT_3
                        oSheet.Range("M" & Rows).Value = V_TOT_4
                        'MsgBox(V_TOT_5 / i)
                        oSheet.Range("N" & Rows).Value = (V_TOT_3 + (V_TOT_4 / 2.8)) / (V_TOT_1 + (V_TOT_2 / 2.8)) * 100
                        'oSheet.Range("K" & Rows).Value = V_TOT_6


                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "11"
                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(255, 0, 255)

                        V_TOT_G_1 = V_TOT_G_1 + V_TOT_1
                        V_TOT_G_2 = V_TOT_G_2 + V_TOT_2
                        V_TOT_G_3 = V_TOT_G_3 + V_TOT_3
                        V_TOT_G_4 = V_TOT_G_4 + V_TOT_4
                        ' V_TOT_G_5 = V_TOT_G_5 + V_TOT_5
                        'V_TOT_G_6 = V_TOT_G_6 + V_TOT_6



                        V_TOT_1 = 0
                        V_TOT_2 = 0
                        V_TOT_3 = 0
                        V_TOT_4 = 0
                        ' V_TOT_5 = 0
                        'V_TOT_6 = 0


                        Rows = Rows + 1

                    End If

                    Depot = Datagrd.Items(i).Cells(5).Text


                    oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "10"
                    oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = False
                    oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(0, 0, 0)




                    oSheet.Range("A" & Rows).Value = i + 1
                    oSheet.Range("B" & Rows).Value = Datagrd.Items(i).Cells(4).Text
                    oSheet.Range("C" & Rows).Value = Datagrd.Items(i).Cells(5).Text

                    oSheet.Range("D" & Rows).Value = Datagrd.Items(i).Cells(6).Text
                    oSheet.Range("E" & Rows).Value = Datagrd.Items(i).Cells(7).Text
                    oSheet.Range("F" & Rows).Value = Datagrd.Items(i).Cells(8).Text
                    oSheet.Range("G" & Rows).Value = Datagrd.Items(i).Cells(9).Text
                    oSheet.Range("H" & Rows).Value = Datagrd.Items(i).Cells(10).Text
                    oSheet.Range("I" & Rows).Value = Datagrd.Items(i).Cells(11).Text

                    oSheet.Range("J" & Rows).Value = Datagrd.Items(i).Cells(12).Text

                    oSheet.Range("K" & Rows).Value = Datagrd.Items(i).Cells(13).Text
                    oSheet.Range("L" & Rows).Value = Datagrd.Items(i).Cells(14).Text
                    oSheet.Range("M" & Rows).Value = Datagrd.Items(i).Cells(15).Text

                    Despatch = (Val(Datagrd.Items(i).Cells(14).Text) + (Val(Datagrd.Items(i).Cells(15).Text) / 2.8))
                    Load = (Val(Datagrd.Items(i).Cells(12).Text) + (Val(Datagrd.Items(i).Cells(13).Text) / 2.8))
                    If Load > 0 Then
                        ServiceLvl = (Despatch / Load) * 100
                        oSheet.Range("N" & Rows).Value = ServiceLvl
                    Else
                        ServiceLvl = 0
                        oSheet.Range("N" & Rows).Value = ServiceLvl
                    End If



                    V_TOT_1 = V_TOT_1 + Datagrd.Items(i).Cells(12).Text
                    V_TOT_2 = V_TOT_2 + Datagrd.Items(i).Cells(13).Text
                    V_TOT_3 = V_TOT_3 + Datagrd.Items(i).Cells(14).Text
                    V_TOT_4 = V_TOT_4 + Datagrd.Items(i).Cells(15).Text
                    V_TOT_5 = (V_TOT_3 + (V_TOT_4 / 2.8)) / (V_TOT_1 + (V_TOT_2 / 2.8)) * 100
                    'V_TOT_6 = V_TOT_6 + Datagrd.Items(i).Cells(10).Text
                    'V_TOT_11 = V_TOT_11 + Datagrd.Items(i).Cells(6).Text
                    'V_TOT_21 = V_TOT_21 + Datagrd.Items(i).Cells(7).Text
                    'V_TOT_31 = V_TOT_31 + Datagrd.Items(i).Cells(8).Text
                    'V_TOT_41 = V_TOT_41 + Datagrd.Items(i).Cells(9).Text



                    Rows = Rows + 1

                Next
                'Rows = Rows + 1
                '- ------
                oSheet.Range("D" & Rows).Value = Depot & " Total :"
                oSheet.Range("J" & Rows).Value = V_TOT_1
                oSheet.Range("K" & Rows).Value = V_TOT_2
                oSheet.Range("L" & Rows).Value = V_TOT_3
                oSheet.Range("M" & Rows).Value = V_TOT_4
                oSheet.Range("N" & Rows).Value = (V_TOT_3 + (V_TOT_4 / 2.8)) / (V_TOT_1 + (V_TOT_2 / 2.8)) * 100
                ' oSheet.Range("K" & Rows).Value = V_TOT_6
                '---------


                oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "11"
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(255, 0, 255)

                V_TOT_G_1 = V_TOT_G_1 + V_TOT_1
                V_TOT_G_2 = V_TOT_G_2 + V_TOT_2
                V_TOT_G_3 = V_TOT_G_3 + V_TOT_3
                V_TOT_G_4 = V_TOT_G_4 + V_TOT_4
                '  V_TOT_G_5 = V_TOT_G_5 + V_TOT_5
                ' V_TOT_G_6 = V_TOT_G_6 + V_TOT_6


                Rows = Rows + 1

                oSheet.Range("B" & Rows).Value = "All Total :"
                'MsgBox((V_TOT_G_3 + (V_TOT_G_4 / 2.8)) / (V_TOT_G_1 + (V_TOT_G_2 / 2.8)))
                oSheet.Range("J" & Rows).Value = V_TOT_G_1
                oSheet.Range("K" & Rows).Value = V_TOT_G_2
                oSheet.Range("L" & Rows).Value = V_TOT_G_3
                oSheet.Range("M" & Rows).Value = V_TOT_G_4
                oSheet.Range("N" & Rows).Value = (V_TOT_G_3 + (V_TOT_G_4 / 2.8)) / (V_TOT_G_1 + (V_TOT_G_2 / 2.8)) * 100

                'oSheet.Range("K" & Rows).Value = V_TOT_G_6

                oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "14"
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(255, 0, 0)

                'Else
                '    Try

                '        'strScript = "<script>"
                '        'strScript &= "alert('Please Select The Srf');"
                '        'strScript &= "</script>"
                '        'Page.RegisterStartupScript("ClientSideScript", strScript)
                '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "message", "alert('Please Select ');", True)
                '    Catch ex As Exception
                '        Response.Write(ex.Message & "<br>" & ex.StackTrace)
                '    End Try
                '    Exit Sub
            End If
            'End If




            If ddlOrderBy.SelectedValue = "S" Then

                For i = 0 To Datagrd.Items.Count - 1
                    count = count + 1
                    'MsgBox(Datagrd.Items(i).Cells(5).Text)



                    If Sku <> "" And Sku <> Datagrd.Items(i).Cells(7).Text Then

                        oSheet.Range("E" & Rows).Value = Sku & " Total :"
                        oSheet.Range("J" & Rows).Value = V_TOT_11
                        oSheet.Range("K" & Rows).Value = V_TOT_21
                        oSheet.Range("L" & Rows).Value = V_TOT_31
                        oSheet.Range("M" & Rows).Value = V_TOT_41
                        oSheet.Range("N" & Rows).Value = (V_TOT_31 + (V_TOT_41 / 2.8)) / (V_TOT_11 + (V_TOT_21 / 2.8)) * 100
                        'oSheet.Range("K" & Rows).Value = V_TOT_6


                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "11"
                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(255, 0, 255)

                        'V_TOT_G_1 = V_TOT_G_1 + V_TOT_1
                        'V_TOT_G_2 = V_TOT_G_2 + V_TOT_2
                        'V_TOT_G_3 = V_TOT_G_3 + V_TOT_3
                        'V_TOT_G_4 = V_TOT_G_4 + V_TOT_4
                        'V_TOT_G_5 = V_TOT_G_5 + V_TOT_5
                        'V_TOT_G_6 = V_TOT_G_6 + V_TOT_6



                        V_TOT_11 = 0
                        V_TOT_21 = 0
                        V_TOT_31 = 0
                        V_TOT_41 = 0
                        V_TOT_5 = 0
                        'V_TOT_6 = 0


                        Rows = Rows + 1

                    End If


                    Sku = Datagrd.Items(i).Cells(7).Text


                    If Product <> "" And Product <> Datagrd.Items(i).Cells(8).Text Then

                        oSheet.Range("F" & Rows).Value = Product & " Total :"
                        oSheet.Range("J" & Rows).Value = V_TOT_1
                        oSheet.Range("K" & Rows).Value = V_TOT_2
                        oSheet.Range("L" & Rows).Value = V_TOT_3
                        oSheet.Range("M" & Rows).Value = V_TOT_4
                        oSheet.Range("N" & Rows).Value = (V_TOT_3 + (V_TOT_4 / 2.8)) / (V_TOT_1 + (V_TOT_2 / 2.8)) * 100
                        'oSheet.Range("K" & Rows).Value = V_TOT_6


                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "11"
                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                        oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(154, 88, 204)

                        V_TOT_G_1 = V_TOT_G_1 + V_TOT_1
                        V_TOT_G_2 = V_TOT_G_2 + V_TOT_2
                        V_TOT_G_3 = V_TOT_G_3 + V_TOT_3
                        V_TOT_G_4 = V_TOT_G_4 + V_TOT_4
                        'V_TOT_G_5 = V_TOT_G_5 + V_TOT_5
                        'V_TOT_G_6 = V_TOT_G_6 + V_TOT_6



                        V_TOT_1 = 0
                        V_TOT_2 = 0
                        V_TOT_3 = 0
                        V_TOT_4 = 0
                        'V_TOT_5 = 0
                        'V_TOT_6 = 0


                        Rows = Rows + 1

                    End If
                    Product = Datagrd.Items(i).Cells(8).Text


                    oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "10"
                    oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = False
                    oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(0, 0, 0)


                    oSheet.Range("A" & Rows).Value = i + 1
                    oSheet.Range("B" & Rows).Value = Datagrd.Items(i).Cells(4).Text
                    oSheet.Range("C" & Rows).Value = Datagrd.Items(i).Cells(5).Text

                    oSheet.Range("D" & Rows).Value = Datagrd.Items(i).Cells(6).Text
                    oSheet.Range("E" & Rows).Value = Datagrd.Items(i).Cells(7).Text
                    oSheet.Range("F" & Rows).Value = Datagrd.Items(i).Cells(8).Text
                    oSheet.Range("G" & Rows).Value = Datagrd.Items(i).Cells(9).Text
                    oSheet.Range("H" & Rows).Value = Datagrd.Items(i).Cells(10).Text
                    oSheet.Range("I" & Rows).Value = Datagrd.Items(i).Cells(11).Text

                    oSheet.Range("J" & Rows).Value = Datagrd.Items(i).Cells(12).Text

                    oSheet.Range("K" & Rows).Value = Datagrd.Items(i).Cells(13).Text
                    oSheet.Range("L" & Rows).Value = Datagrd.Items(i).Cells(14).Text
                    oSheet.Range("M" & Rows).Value = Datagrd.Items(i).Cells(15).Text


                    Despatch = (Val(Datagrd.Items(i).Cells(14).Text) + (Val(Datagrd.Items(i).Cells(15).Text) / 2.8))
                    Load = (Val(Datagrd.Items(i).Cells(12).Text) + (Val(Datagrd.Items(i).Cells(13).Text) / 2.8))
                    If Load > 0 Then
                        ServiceLvl = (Despatch / Load) * 100
                        oSheet.Range("N" & Rows).Value = ServiceLvl
                    Else
                        ServiceLvl = 0
                        oSheet.Range("N" & Rows).Value = ServiceLvl
                    End If


                    V_TOT_11 = V_TOT_11 + Datagrd.Items(i).Cells(12).Text
                    V_TOT_21 = V_TOT_21 + Datagrd.Items(i).Cells(13).Text
                    V_TOT_31 = V_TOT_31 + Datagrd.Items(i).Cells(14).Text
                    V_TOT_41 = V_TOT_41 + Datagrd.Items(i).Cells(15).Text
                    'V_TOT_51 = (V_TOT_3 + (V_TOT_4 / 2.8)) / (V_TOT_1 + (V_TOT_2 / 2.8))*100




                    V_TOT_1 = V_TOT_1 + Datagrd.Items(i).Cells(12).Text
                    V_TOT_2 = V_TOT_2 + Datagrd.Items(i).Cells(13).Text
                    V_TOT_3 = V_TOT_3 + Datagrd.Items(i).Cells(14).Text
                    V_TOT_4 = V_TOT_4 + Datagrd.Items(i).Cells(15).Text
                    'V_TOT_5 = (V_TOT_3 + (V_TOT_4 / 2.8)) / (V_TOT_1 + (V_TOT_2 / 2.8)) * 100
                    'V_TOT_6 = V_TOT_6 + Datagrd.Items(i).Cells(10).Text



                    Rows = Rows + 1


                Next

                oSheet.Range("E" & Rows).Value = Sku & " Total :"
                oSheet.Range("J" & Rows).Value = V_TOT_11
                oSheet.Range("K" & Rows).Value = V_TOT_21
                oSheet.Range("L" & Rows).Value = V_TOT_31
                oSheet.Range("M" & Rows).Value = V_TOT_41
                oSheet.Range("N" & Rows).Value = (V_TOT_31 + (V_TOT_41 / 2.8)) / (V_TOT_11 + (V_TOT_21 / 2.8)) * 100
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "11"
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(255, 0, 255)


                'Rows = Rows + 1
                '- ------



                ' V_TOT_G_5 = V_TOT_G_5 + V_TOT_5
                ' V_TOT_G_6 = V_TOT_G_6 + V_TOT_6


                Rows = Rows + 1

                oSheet.Range("F" & Rows).Value = Product & " Total :"
                oSheet.Range("J" & Rows).Value = V_TOT_1
                oSheet.Range("K" & Rows).Value = V_TOT_2
                oSheet.Range("L" & Rows).Value = V_TOT_3
                oSheet.Range("M" & Rows).Value = V_TOT_4
                oSheet.Range("N" & Rows).Value = (V_TOT_3 + (V_TOT_4 / 2.8)) / (V_TOT_1 + (V_TOT_2 / 2.8)) * 100
                ' oSheet.Range("K" & Rows).Value = V_TOT_6
                '---------


                oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "11"
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(154, 88, 204)


                Rows = Rows + 1

                V_TOT_G_1 = V_TOT_G_1 + V_TOT_1
                V_TOT_G_2 = V_TOT_G_2 + V_TOT_2
                V_TOT_G_3 = V_TOT_G_3 + V_TOT_3
                V_TOT_G_4 = V_TOT_G_4 + V_TOT_4
                'V_TOT_G_5 = (V_TOT_G_3 + (V_TOT_G_4 / 2.8)) / (V_TOT_G_1 + (V_TOT_G_2 / 2.8))

                oSheet.Range("B" & Rows).Value = "All Total :"

                oSheet.Range("J" & Rows).Value = V_TOT_G_1
                oSheet.Range("K" & Rows).Value = V_TOT_G_2
                oSheet.Range("L" & Rows).Value = V_TOT_G_3
                oSheet.Range("M" & Rows).Value = V_TOT_G_4
                oSheet.Range("N" & Rows).Value = (V_TOT_G_3 + (V_TOT_G_4 / 2.8)) / (V_TOT_G_1 + (V_TOT_G_2 / 2.8)) * 100
                'oSheet.Range("K" & Rows).Value = V_TOT_G_6

                oSheet.Range("A" & Rows & ":N" & Rows).Font.Size = "14"
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Bold = True
                oSheet.Range("A" & Rows & ":N" & Rows).Font.Color = RGB(255, 0, 0)


            End If

        Else
            Try

                'strScript = "<script>"
                'strScript &= "alert('Please Select The Srf');"
                'strScript &= "</script>"
                'Page.RegisterStartupScript("ClientSideScript", strScript)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "message", "alert('Please Select ');", True)
            Catch ex As Exception
                Response.Write(ex.Message & "<br>" & ex.StackTrace)
            End Try
            Exit Sub

        End If
        '<end>



        oSheet.Columns("A:N").AutoFit()
        oSheet.SaveAs(sFile) 'Save in a temporary file 
        oBook.Close()
        oExcel.Quit()
        ReleaseComObject(oCells)
        ReleaseComObject(oSheet)
        ReleaseComObject(oSheets)
        ReleaseComObject(oBook)
        ReleaseComObject(oBooks)
        ReleaseComObject(oExcel)
        oCells = Nothing
        oSheet = Nothing
        oSheets = Nothing
        oBook = Nothing
        oBooks = Nothing
        oExcel = Nothing
        System.GC.Collect()

        'Response.Redirect("http://localhost:4891/SNOOK_SOURCE/Excel_Reports/Daily_Collection_Due_Report" + DateString + ".xls")
        Dim ServerIpAddress As String = ConfigurationManager.AppSettings.Get("ServerIPAddress")
        'Dim ServerIpAddress As String = "http://localhost:2727/VMS_SOURCE/Excel_Reports/"
        Response.Redirect(ServerIpAddress + "Month_Load_Vs_Despatches" + DateString + ".xls")
    End Sub
#End Region

    Private Function impersonateValidUser(ByVal userName As String, _
ByVal domain As String, ByVal password As String) As Boolean

        Dim tempWindowsIdentity As WindowsIdentity
        Dim token As IntPtr = IntPtr.Zero
        Dim tokenDuplicate As IntPtr = IntPtr.Zero
        impersonateValidUser = False

        If RevertToSelf() Then
            If LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
                If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                    tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                    impersonationContext = tempWindowsIdentity.Impersonate()
                    If Not impersonationContext Is Nothing Then
                        impersonateValidUser = True
                    End If
                End If
            End If
        End If
        If Not tokenDuplicate.Equals(IntPtr.Zero) Then
            CloseHandle(tokenDuplicate)
        End If
        If Not token.Equals(IntPtr.Zero) Then
            CloseHandle(token)
        End If
    End Function

    Private Sub undoImpersonation()
        impersonationContext.Undo()
    End Sub
End Class
