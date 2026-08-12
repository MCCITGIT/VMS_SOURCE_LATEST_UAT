'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : User_Profile_List_Report.aspx.vb
'Created Date	: 07-January-2012
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for User_Profile_List_Report.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Partial Class User_Profile_List_Report
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("NoData") = "Yes") Then
            lblErrMsg.Text = "No record found!"
        Else
            lblErrMsg.Text = String.Empty
        End If
        If Not IsPostBack Then
            CheckLogin()
            PopulateRegion()
            PopulateDepot()
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
        Dim tmpDepotDS As New DataSet
        Dim ReportObj As New UserProfile_List_ReportClass
        Dim region As String
        region = IIf(ddlRegion.SelectedValue <> "0", ddlRegion.SelectedValue, String.Empty)
        ddlDepot.Items.Clear()
        tmpDepotDS = ReportObj.GeDepots(region, Constant.Common.ActiveStatus)
        If (Not (tmpDepotDS Is Nothing) AndAlso tmpDepotDS.Tables.Count > 0 AndAlso Not (tmpDepotDS.Tables(0) Is Nothing) AndAlso tmpDepotDS.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = tmpDepotDS
            ddlDepot.DataTextField = "depotname"
            ddlDepot.DataValueField = "depot_code"
            ddlDepot.DataBind()
            ddlDepot.Items.Insert(0, New ListItem("All", String.Empty, True))
        Else
            ddlDepot.Items.Insert(0, New ListItem("Select", String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlDepot.SelectedValue = userInfo.userRegionEntity
            ddlDepot.Enabled = False
        End If

    End Sub
#End Region

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
    End Sub

#Region "Function to Export Dataset to Excel"

    Protected Sub ExportToExcel(ByVal dset As DataSet, ByVal Response As HttpResponse, ByVal filename As String)


        Try
            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "USER PROFILE" + "</b></div><BR>")
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
            'dg.ShowFooter = True
            'AddHandler dg.RowCreated, New GridViewRowEventHandler(AddressOf insertfooter)
            dg.DataSource = dset.Tables(0)
            dg.DataBind()

            dg.RenderControl(htmlwrite)

            Response.Write(stringwrite.ToString)
            'Response.Write("<div style='text-align:right;'><b>" + "Total:" + temp + "</b></div><BR>")
            'Response.Write("<BR>")
            'Response.Write("<div style='text-align:right;'><b>" + " Total&nbsp;&nbsp;&nbsp;&nbsp;: &nbsp;&nbsp;&nbsp;&nbsp;" + CType(temp, String) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CType(temp1, String) + "</b></div><BR>")

            Response.End()
        Catch ex As Exception
            'Dim returnUrl As String = "~/ExceptionPage.aspx"
            'Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorExporttoExcel
            'Server.Transfer(returnUrl)
            'MsgBox(ex.Message)
        End Try


    End Sub
#End Region

#Region "Submit Button Click Event Handeling"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()
        If ddlReportFormat.SelectedValue = Constant.Common.ExcelFormat Then
            Dim UsrProfobj As New UserProfile_List_ReportClass
            Dim ExcelSet As DataSet
            Dim company As String = userInfo.userCompanyEntity
            Dim region As String = ddlRegion.SelectedValue
            Dim depo As String = ddlDepot.SelectedValue

            'Dim userid as String =
            ExcelSet = UsrProfobj.GeReportDetails(company, region, depo)
            If (ExcelSet.Tables(0).Rows.Count > 0) Then
                Dim x As Integer
                Dim i As Integer = ExcelSet.Tables(0).Rows.Count

                For x = 0 To i - 1
                    ExcelSet.Tables(0).Rows(x)("Srl No.") = x + 1
                    If Trim(ExcelSet.Tables(0).Rows(x)("Status").ToString) = "Y" Then
                        ExcelSet.Tables(0).Rows(x)("Status") = "Active"
                    Else
                        ExcelSet.Tables(0).Rows(x)("Status") = "Inactive"
                    End If
                Next x
                Dim FileNme As String
                FileNme = Convert.ToString(userInfo.userIDEntity)
                FileNme = FileNme + "_" + "User_Profile" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
                ExportToExcel(ExcelSet, Response, FileNme)

            Else
                lblErrMsg.Text = "No Data Found"
            End If
        Else
            Dim ReportViewer As New ReportViewer_DC
            ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.UserProfileReport
            ReportViewer.ReportCase = Constant.ReportView.ReportCase.UserProfileReportCase
            ReportViewer.up_Company = userInfo.userCompanyEntity

            ReportViewer.up_Depot = ddlDepot.SelectedValue
            ReportViewer.up_Region = ddlRegion.SelectedValue

            ReportViewer.ReportType = ddlReportFormat.SelectedValue

            ClientScript.RegisterStartupScript(Me.GetType(), "ShowReport", "<script type='text/javascript' language='javascript'>fnNewWindow('ReportViewer.aspx','__BLANK')</script>")
        End If
    End Sub
#End Region

    'Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '    Response.Redirect("~/User_Profile_List.aspx")
    'End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/User_Profile_List.aspx")
    End Sub
End Class
