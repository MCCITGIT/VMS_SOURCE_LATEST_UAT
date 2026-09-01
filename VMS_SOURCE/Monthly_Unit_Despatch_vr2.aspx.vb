'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Monthly_Unit_Despatch_vr2.aspx.vb
'Created Date	: 14-December-2011
'Created By	    : Deepak Yadav
'Version	    : R02.00.00
'Description	: Code behind for Monthly_Unit_Despatch_vr2.aspx Page
'****************************************************************
Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Partial Class Monthly_Unit_Despatch_vr2
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If (Request.QueryString("NoData") = "Yes") Then
            lblErrMsg.Text = "No record found!"
        Else
            lblErrMsg.Text = String.Empty
        End If

        If Not IsPostBack Then
            PopulateRegion()
            PopulateDepot()
            PopulateUnit()

            LoadSearchCriteria()
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
        Dim GeDepot As New Common
        Dim DepotSet As New DataSet

        DepotSet = GeDepot.Getdepotname(ddlRegion.SelectedValue)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = DepotSet.Tables(0)
            ddlDepot.DataTextField = "depot_name"
            ddlDepot.DataValueField = "depot_code"
            ddlDepot.DataBind()
            ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.REGION) Then
            ddlDepot.SelectedValue = userInfo.userBranchEntity
            ddlDepot.Enabled = False
        End If
    End Sub
#End Region
#Region "Populate Unit"
    Private Sub PopulateUnit()
        Dim UnitDespatch As New MonthlyUnitDespatch
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, ddlRegion.SelectedValue)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlUnit.SelectedValue = userInfo.userBranchEntity
            ddlUnit.Enabled = False
        End If
    End Sub
#End Region
#Region "Populate Depot ,Unit for Selected Region"
    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
    End Sub
#End Region
#Region "Button Submit For Report"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Try
            lblErrMsg.Text = " "

            If txtFromDate.Text.Trim = String.Empty Then
                lblErrMsg.Text = "Please enter From Date"
                Exit Sub
            End If
            If txtToDate.Text.Trim = String.Empty Then
                lblErrMsg.Text = "Please enter To Date"
                Exit Sub
            End If

            Dim fromdate As DateTime = FormatDate(txtFromDate.Text)
            Dim todate As DateTime = FormatDate(txtToDate.Text)

            If fromdate > todate Then
                lblErrMsg.Text = "From Date cannot be greater than To Date"
                Exit Sub
            End If

            SaveSearchCriteria()

            Dim region As String = ddlRegion.SelectedValue
            Dim depot As String = ddlDepot.SelectedValue
            Dim unit As String = ddlUnit.SelectedValue
            Dim active As String = Constant.Common.ActiveStatus

            Dim reportObj As New MonthlyUnitDespatch
            Dim ExcelSet As DataSet = reportObj.GetMonthlyUnitDespatchReportVr2(region, depot, unit, fromdate, todate, active)

            If ExcelSet IsNot Nothing AndAlso ExcelSet.Tables.Count > 0 AndAlso ExcelSet.Tables(0) IsNot Nothing AndAlso ExcelSet.Tables(0).Rows.Count > 0 Then
                MonthlyUnitDespatchExcelExport.PrepareDataForExcel(ExcelSet)
                MonthlyUnitDespatchExcelExport.ExportToExcelSheet(ExcelSet, txtFromDate.Text.Trim(), txtToDate.Text.Trim(), userInfo.userCompanyEntity, AppDomain.CurrentDomain.BaseDirectory, Response)
            Else
                lblErrMsg.Text = "No Data Found"
            End If
        Catch ex As System.Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorExporttoExcel
            Server.Transfer(returnUrl)
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

    Private Sub SaveSearchCriteria()

        Dim SearchInfo As New MonthlyUnitDespatchEntity

        SearchInfo.Region = ddlRegion.SelectedValue
        SearchInfo.Depot = ddlDepot.SelectedValue
        SearchInfo.Unit = ddlUnit.SelectedValue
        SearchInfo.FromDate = txtFromDate.Text.Trim
        SearchInfo.ToDate = txtToDate.Text.Trim
        SearchInfo.ReportFormat = ddlReportFormat.SelectedValue
        Session(Constant.SessionKeys.MonthlyUnitDespatchSearchInfo) = SearchInfo

    End Sub

#End Region
#Region "Load Search Criteria"

    Private Sub LoadSearchCriteria()

        If Not (Session(Constant.SessionKeys.MonthlyUnitDespatchSearchInfo) Is Nothing) Then
            Dim SearchInfo As New MonthlyUnitDespatchEntity
            SearchInfo = CType(Session(Constant.SessionKeys.MonthlyUnitDespatchSearchInfo), MonthlyUnitDespatchEntity)

            ddlRegion.SelectedValue = SearchInfo.Region
            ddlDepot.SelectedValue = SearchInfo.Depot
            ddlUnit.SelectedValue = SearchInfo.Unit
            txtFromDate.Text = SearchInfo.FromDate
            txtToDate.Text = SearchInfo.ToDate
            ddlReportFormat.SelectedValue = SearchInfo.ReportFormat

        End If

    End Sub

#End Region
End Class
