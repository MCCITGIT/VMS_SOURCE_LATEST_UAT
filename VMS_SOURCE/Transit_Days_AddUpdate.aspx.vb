'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Transit_Days_AddUpdate.aspx.vb
'Created Date	: 13-January-2012
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for Transit_Days_AddUpdate.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class Transit_Days_AddUpdate
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckLogin()
            PopulateUnit()
            btnSubmit.Visible = False
            btnCancel.Visible = False
            'btnReset.Visible = False
            'divF7.Visible = False
            'divF8.Visible = True
            imgbtnSearch.Attributes.Add("onClick", "return ValidateTrnstSearch();")
            imgbtnPrint.Attributes.Add("onClick", "return ValidateTrnstSearch();")
            btnSubmit.Attributes.Add("onClick", "return ValidateTrnstDy();")
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

#Region "Populate Unit"
    Private Sub PopulateUnit()
        CheckLogin()

        Dim TrnstDays As New Transit_Days_App
        Dim UnitSet As New DataSet

        UnitSet = TrnstDays.GetUnit(Constant.Common.ActiveStatus)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlUnit.SelectedValue = userInfo.userBranchEntity
            ddlUnit.Enabled = False
        End If
    End Sub
#End Region

#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim TrnstDays As New Transit_Days_App
            Dim GridSet As New DataSet

            GridSet = TrnstDays.GetDetails(ddlUnit.SelectedValue)
            If (Not (GridSet Is Nothing) AndAlso GridSet.Tables.Count > 0 AndAlso Not (GridSet.Tables(0) Is Nothing) AndAlso GridSet.Tables(0).Rows.Count > 0) Then
                lblNoRecFnd.Visible = False
                gvTransitDays.Visible = True
                gvTransitDays.DataSource = GridSet.Tables(0)
                gvTransitDays.DataBind()
                btnSubmit.Visible = True
                btnCancel.Visible = True
                'btnReset.Visible = True
                'divF7.Visible = True
                'divF8.Visible = False
            Else
                gvTransitDays.Visible = False
                lblNoRecFnd.Visible = True
                lblNoRecFnd.Text = "No Records Found!!!"
                btnSubmit.Visible = False
                btnCancel.Visible = False
                'btnReset.Visible = False
                'divF7.Visible = False
                'divF8.Visible = True
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorBindingGrid
            Server.Transfer(returnUrl)
        End Try
    End Sub
#End Region

#Region "gvTransitDays Event Handelling"
    Protected Sub gvTransitDays_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTransitDays.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            If IsDBNull(rowView("t_transit_days")) Then
                e.Row.BackColor = Drawing.Color.Brown
                e.Row.ForeColor = Drawing.Color.White
            End If
        End If
    End Sub
#End Region

#Region "Search Button Click Event Handelling"
    'Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
    '    BindGrid()
    'End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs)
        BindGrid()
    End Sub
#End Region

#Region "ddlUnit Selected Index Changed Event Handelling"
    Protected Sub ddlUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnit.SelectedIndexChanged
        BindGrid()
    End Sub
#End Region

#Region "Insert Transit Days"
    Private Function InsertTransitDays() As Integer
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsDeleted As Integer
        Dim numRowsAffected As Integer
        Dim TrnstDyEntity As New VMS.Web.Transit_Days_Entity
        Dim TrnstDays As New Transit_Days_App
        Dim RowIndex As Integer = 0

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            numRowsDeleted = TrnstDays.TransitDaysDelete(ddlUnit.SelectedValue, sqlConn, sqlTrans)

            For RowIndex = 0 To gvTransitDays.Rows.Count - 1
                TrnstDyEntity.vendor_unit = ddlUnit.SelectedValue
                Dim lblDepot As Label = gvTransitDays.Rows(RowIndex).FindControl("lblDepotCode")
                TrnstDyEntity.depot = lblDepot.Text
                Dim txtTransit As TextBox = gvTransitDays.Rows(RowIndex).FindControl("txtDays")

                If Not (Trim(txtTransit.Text) = String.Empty) Then
                    TrnstDyEntity.transit_days = CType(txtTransit.Text, Integer)
                End If

                TrnstDyEntity.CreatedUser = userInfo.userIDEntity

                numRowsAffected = TrnstDays.TransitDaysInsert(TrnstDyEntity, sqlConn, sqlTrans)

            Next
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
                BindGrid()
            End If
        End Try
    End Function
#End Region

#Region "Submit Click Event Handelling"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()
        InsertTransitDays()
    End Sub
#End Region

#Region "Cancel Button Click Event Handeling"
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub
#End Region

#Region "Function to Export Dataset to Excel"
    Protected Sub ExportToExcel(ByVal dset As DataSet, ByVal Response As HttpResponse, ByVal filename As String)

        Try
            Dim Unit As String = ddlUnit.SelectedItem.Text
            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.Write("<div style='text-align:center;'><b>" + "Transit Days" + "</b></div><BR>")
            Response.Write("<div style='text-align:center;'><b>" + " Source : " + Unit + "</b></div><BR>")
            Response.Write("<b>" + "Company Name : " + userInfo.userCompanyEntity + "</b><BR>")
            'Response.Write("<div style='text-align:right;'><b>" + "Report Date:" + FormatDate(Date.Today) + "</b></div><BR>")
            'Response.Write("<img src='" + AppDomain.CurrentDomain.BaseDirectory + "/images/Berger.gif' /><BR>")
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

#Region "Print Button Click Event Handelling"
    'Protected Sub imgbtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnPrint.Click
    '    CheckLogin()

    '    Dim TrnstDays As New Transit_Days_App
    '    Dim ExcelSet As New DataSet

    '    ExcelSet = TrnstDays.GetExcelReport(ddlUnit.SelectedValue)
    '    If (ExcelSet.Tables(0).Rows.Count > 0) Then
    '        'Dim i As Integer = ExcelSet.Tables(0).Rows.Count
    '        Dim FileNme As String
    '        FileNme = Convert.ToString(userInfo.userIDEntity)
    '        FileNme = FileNme + "_" + "Transit_Days" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
    '        For i As Integer = 0 To ExcelSet.Tables(0).Rows.Count - 1
    '            ExcelSet.Tables(0).Rows(i)("Srl No.") = i + 1
    '        Next
    '        ExportToExcel(ExcelSet, Response, FileNme)
    '    End If
    'End Sub
#End Region

    Protected Sub imgbtnPrint_Click(sender As Object, e As EventArgs)
        CheckLogin()

        Dim TrnstDays As New Transit_Days_App
        Dim ExcelSet As New DataSet

        ExcelSet = TrnstDays.GetExcelReport(ddlUnit.SelectedValue)
        If (ExcelSet.Tables(0).Rows.Count > 0) Then
            'Dim i As Integer = ExcelSet.Tables(0).Rows.Count
            Dim FileNme As String
            FileNme = Convert.ToString(userInfo.userIDEntity)
            FileNme = FileNme + "_" + "Transit_Days" + "_" + Convert.ToString(Date.Today.Day) + "_" + Convert.ToString(Date.Today.Month) + "_" + Convert.ToString(Date.Today.Year)
            For i As Integer = 0 To ExcelSet.Tables(0).Rows.Count - 1
                ExcelSet.Tables(0).Rows(i)("Srl No.") = i + 1
            Next
            ExportToExcel(ExcelSet, Response, FileNme)
        End If
    End Sub
End Class
