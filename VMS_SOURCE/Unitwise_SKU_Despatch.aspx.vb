'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Unitwise_SKU_Despatch.aspx.vb
'Created Date	: 20-December-2011
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for Unitwise_SKU_Despatch.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class Unitwise_SKU_Despatch
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
            'PopulateRegion()
            PopulateUnit()
            PopulateYear()
            PopulateMonth()
            Dim SKUDsptchdUntWise As New UnitwiseSKUDespatch_App
            btnSubmit.Attributes.Add("onClick", "return ValidateDptDsptchUntWise('" + SKUDsptchdUntWise.GetTopFinYear() + "','" + SKUDsptchdUntWise.GetLastFinYear() + "');")
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

    '#Region "Populate Region"
    '    Private Sub PopulateRegion()
    '        CheckLogin()

    '        Dim ObjDocumentType As New Common
    '        Dim OccupationTypeSet As New DataSet
    '        Dim LovType As String = Constant.Common.REGION_TYPE
    '        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
    '        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
    '            ddlRegion.DataSource = OccupationTypeSet.Tables(0)
    '            ddlRegion.DataTextField = "lov_value"
    '            ddlRegion.DataValueField = "lov_code"
    '            ddlRegion.DataBind()
    '            ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
    '        End If
    '        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
    '            ddlRegion.SelectedValue = userInfo.userRegionEntity
    '            ddlRegion.Enabled = False
    '        End If

    '    End Sub
    '#End Region

#Region "Populate Unit"
    Private Sub PopulateUnit()
        CheckLogin()

        Dim SKUDsptchdUntWise As New UnitwiseSKUDespatch_App
        Dim UnitSet As New DataSet

        UnitSet = SKUDsptchdUntWise.GetUnit(Constant.Common.ActiveStatus)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlDsptchdUnit.DataSource = UnitSet.Tables(0)
            ddlDsptchdUnit.DataTextField = "unit_name"
            ddlDsptchdUnit.DataValueField = "unit_code"
            ddlDsptchdUnit.DataBind()
            ddlDsptchdUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlDsptchdUnit.SelectedValue = userInfo.userUnitEntity
        '    ddlDsptchdUnit.Enabled = False
        'End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlDsptchdUnit.SelectedValue = userInfo.userBranchEntity
            ddlDsptchdUnit.Enabled = False
        End If
    End Sub
#End Region

#Region "Populate TxtProcessYear"
    Private Sub PopulateYear()
        CheckLogin()

        Dim SKUDsptchdUntWise As New UnitwiseSKUDespatch_App
        Dim YearSet As New DataSet

        YearSet = SKUDsptchdUntWise.GetYear(Constant.Common.ActiveStatus)
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

        Dim SKUDsptchdUntWise As New UnitwiseSKUDespatch_App
        Dim MonthSet As New DataSet

        MonthSet = SKUDsptchdUntWise.GetMonth(Constant.Common.ActiveStatus)
        If (Not (MonthSet Is Nothing) AndAlso MonthSet.Tables.Count > 0 AndAlso Not (MonthSet.Tables(0) Is Nothing) AndAlso MonthSet.Tables(0).Rows.Count > 0) Then
            'txtProcessMonth.Text = MonthSet.Tables(0).Rows(0)
            'txtProcessMonth.Text = "param_char_value"
            txtMonth.Text = MonthSet.Tables(0).Rows(0)("param_char_value")
            'txtProcessMonth.DataBind()
        End If
    End Sub
#End Region

    'Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
    '    PopulateUnit()
    'End Sub

#Region "btnSubmit Click Event Handeling"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()

        If rdbDptWise.Checked = True Then

            If ddlPrntOptn.SelectedValue = Constant.Common.ExcelFormat Then

                Dim UntwisSKUDsptch As New UnitwiseSKUDespatch_App
                Dim UntwisExcelSet As New DataSet

                UntwisExcelSet = UntwisSKUDsptch.GetExcelUntWiseUntDsptchRpt(ddlDsptchdUnit.SelectedValue, txtFinYear.Text.Trim, txtMonth.Text.Trim, Constant.Common.ActiveStatus, IIf(userInfo.userGroupCodeEntity = "DEPOT", userInfo.userBranchEntity, String.Empty))
                If (UntwisExcelSet.Tables(0).Rows.Count > 0) Then
                    lblErrMsg.Visible = False
                    Dim rowcount, i As Integer
                    rowcount = UntwisExcelSet.Tables(0).Rows.Count


                    For i = 0 To rowcount - 1

                        UntwisExcelSet.Tables(0).Rows(i)("Srl No.") = i + 1
                        UntwisExcelSet.Tables(0).Rows(i)("LTR") = IIf(UntwisExcelSet.Tables(0).Rows(i)("LTR") <> 0.0, CType(UntwisExcelSet.Tables(0).Rows(i)("LTR"), Decimal), DBNull.Value)
                        UntwisExcelSet.Tables(0).Rows(i)("KG") = IIf(UntwisExcelSet.Tables(0).Rows(i)("KG") <> 0.0, CType(UntwisExcelSet.Tables(0).Rows(i)("KG"), Decimal), DBNull.Value)

                    Next
                    Dim FileNme As String
                    FileNme = Convert.ToString(userInfo.userIDEntity)
                    FileNme = FileNme + "_" + "Unitwise_SKU_Despatch" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
                    ExportToExcelUnitwise(UntwisExcelSet, Response, FileNme)
                Else
                    lblErrMsg.Visible = True
                    lblErrMsg.Text = "No Records Found"
                End If

            Else

                Dim ReportViewer As New ReportViewer_DC

                ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Unitwise_SKU_Despatched_Report
                ReportViewer.ReportCase = Constant.ReportView.ReportCase.UntWisSKUDsptchRptCase

                If userInfo.userGroupCodeEntity = "DEPOT" Then
                    ReportViewer.Depot = userInfo.userBranchEntity
                Else
                    ReportViewer.Depot = String.Empty
                End If

                ReportViewer.UnitwiseSKUDsptch_Unit = ddlDsptchdUnit.SelectedValue
                ReportViewer.UnitwiseSKUDsptch_FinYear = txtFinYear.Text.Trim
                ReportViewer.UnitwiseSKUDsptch_Month = txtMonth.Text.Trim
                ReportViewer.Active = Constant.Common.ActiveStatus

                ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)

            End If
        ElseIf rdbSummary.Checked = True Then

            If ddlPrntOptn.SelectedValue = Constant.Common.ExcelFormat Then

                Dim UntwisSKUDsptch As New UnitwiseSKUDespatch_App
                Dim SmmryExcelSet As New DataSet

                SmmryExcelSet = UntwisSKUDsptch.GetExcelUntWiseUntDsptchSmmryRpt(ddlDsptchdUnit.SelectedValue, txtFinYear.Text.Trim, txtMonth.Text.Trim, Constant.Common.ActiveStatus, IIf(userInfo.userGroupCodeEntity = "DEPOT", userInfo.userBranchEntity, String.Empty))
                If (SmmryExcelSet.Tables(0).Rows.Count > 0) Then
                    lblErrMsg.Visible = False
                    Dim rowcount, i As Integer
                    rowcount = SmmryExcelSet.Tables(0).Rows.Count


                    For i = 0 To rowcount - 1

                        'SmmryExcelSet.Tables(0).Rows(i)("Srl No.") = i + 1
                        SmmryExcelSet.Tables(0).Rows(i)("LTR") = IIf(SmmryExcelSet.Tables(0).Rows(i)("LTR") <> 0.0, CType(SmmryExcelSet.Tables(0).Rows(i)("LTR"), Decimal), DBNull.Value)
                        SmmryExcelSet.Tables(0).Rows(i)("KG") = IIf(SmmryExcelSet.Tables(0).Rows(i)("KG") <> 0.0, CType(SmmryExcelSet.Tables(0).Rows(i)("KG"), Decimal), DBNull.Value)

                    Next
                    Dim FileNme As String
                    FileNme = Convert.ToString(userInfo.userIDEntity)
                    FileNme = FileNme + "_" + "Unitwise_SKU_Despatch_Summary" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
                    ExportToExcelSummary(SmmryExcelSet, Response, FileNme)
                Else
                    lblErrMsg.Visible = True
                    lblErrMsg.Text = "No Records Found"
                End If

            Else

                Dim ReportViewer As New ReportViewer_DC

                ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Unitwise_SKU_Despatch_Summary_Report
                ReportViewer.ReportCase = Constant.ReportView.ReportCase.UntWisSKUDsptchSmmryRptCase

                If userInfo.userGroupCodeEntity = "DEPOT" Then
                    ReportViewer.Depot = userInfo.userBranchEntity
                Else
                    ReportViewer.Depot = String.Empty
                End If
                ReportViewer.UnitwiseSKUDsptch_Unit = ddlDsptchdUnit.SelectedValue
                ReportViewer.UnitwiseSKUDsptch_FinYear = txtFinYear.Text.Trim
                ReportViewer.UnitwiseSKUDsptch_Month = txtMonth.Text.Trim
                ReportViewer.Active = Constant.Common.ActiveStatus

                ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)

            End If
        End If
    End Sub
#End Region

#Region "Function to Export Dataset to Excel For Unitwise"
    Protected Sub ExportToExcelUnitwise(ByVal dset As DataSet, ByVal Response As HttpResponse, ByVal filename As String)

        Try
            Dim Year As String = txtFinYear.Text
            Dim Month As String = txtMonth.Text
            Response.Clear()
            Response.Charset = ""

            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "Sourcewise SKU Despatch" + "</b></div><BR>")
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

#Region "Function to Export Dataset to Excel For Summary"
    Protected Sub ExportToExcelSummary(ByVal dset As DataSet, ByVal Response As HttpResponse, ByVal filename As String)

        Try
            Dim Year As String = txtFinYear.Text
            Dim Month As String = txtMonth.Text
            Response.Clear()
            Response.Charset = ""

            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "Sourcewise SKU Despatch Summary" + "</b></div><BR>")
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

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("~/Home.aspx")
    End Sub
End Class
