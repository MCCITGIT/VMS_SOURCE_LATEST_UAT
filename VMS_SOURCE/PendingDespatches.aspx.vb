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
Partial Class PendingDespatches
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim kgTotal As Decimal = 0.0
    Dim LtrTotal As Decimal = 0.0
    Dim Nop As Integer = 0

#Region "Page Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("NoData") = "Yes") Then
            lblErrMsg.Text = "No record found!"
        Else
            lblErrMsg.Text = String.Empty
        End If
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

        Dim UnitDespatch As New PendingDespatchesClass
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, ddlRegion.SelectedValue)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
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
        'CheckLogin()
        Try
            SaveSearchCriteria()
            lblErrMsg.Text = " "
            If ddlReportFormat.SelectedValue = Constant.Common.ExcelFormat Then
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

                Dim reportObj As New PendingDespatchesClass
                Dim ExcelSet As DataSet
                ExcelSet = reportObj.PendingDespatch_Report(active, region, depot, ProcessYr, ProcessMnth, unit, OrderBy_Depot_Sku)
                If (ExcelSet.Tables(0).Rows.Count > 0) Then
                    'ExcelSet.Tables(0).Columns.Add("DT. OF SENDING")
                    'ExcelSet.Tables(0).Columns.Add("CN85")
                    Dim rowcount, i As Integer
                    rowcount = ExcelSet.Tables(0).Rows.Count


                    For i = 0 To rowcount - 1

                        ExcelSet.Tables(0).Rows(i)("Srl") = i + 1
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

                    Next

                    Dim FileNme As String
                    FileNme = Convert.ToString(userInfo.userCompanyEntity)
                    FileNme = FileNme + "_" + "Pending Despatches" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
                    ExportToExcel(ExcelSet, Response, FileNme)
                Else
                    lblErrMsg.Text = "No Data Found"
                End If

                ' End If
            Else



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
        ''''Dim unit As String = dset.Tables(0).Rows(0)("Source")
        Try
            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "Pending Despatches" + "</b></div>")
            'Response.Write("<div style='text-align:right;'><b>" + "Report Date:" + Format(Date.Today, "dd/MM/yyyy") + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + "As on: " + Format(DateTime.Now, "dd/MM/yyyy") + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + " Process Year: " + processyr + "</b></div>")
            Response.Write("<div style='text-align:center;'><b>" + "Process Month: " + processMnths + "</b></div>")
            ''''Response.Write("<div style='text-align:left;'><b>" + "Source: " + unit + "</b></div>")

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
            ''''dset.Tables(0).Columns.Remove("Source")
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

        Dim SearchInfo As New PendingDespatchEntity

        SearchInfo.Region = ddlRegion.SelectedValue
        SearchInfo.Depot = ddlDepot.SelectedValue
        SearchInfo.Unit = ddlUnit.SelectedValue
        SearchInfo.ProcessYr = ddlProcessYr.SelectedValue
        SearchInfo.ProcessMnth = ddlProcessMnth.SelectedValue
        SearchInfo.ReportFormat = ddlReportFormat.SelectedValue
        SearchInfo.OrderBy = ddlOrderBy.SelectedValue
        Session(Constant.SessionKeys.PendingDespatchSearchInfo) = SearchInfo

    End Sub

#End Region
#Region "Load Search Criteria"

    '' Loads the earlier search criteria when navigating back from a different screen
    Private Sub LoadSearchCriteria()

        If Not (Session(Constant.SessionKeys.PendingDespatchSearchInfo) Is Nothing) Then
            Dim SearchInfo As New PendingDespatchEntity
            SearchInfo = CType(Session(Constant.SessionKeys.PendingDespatchSearchInfo), PendingDespatchEntity)

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
End Class
