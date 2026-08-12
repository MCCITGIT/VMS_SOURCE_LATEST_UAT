'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : DespatchReceiveList.aspx.vb
'Created Date	: 06-Apr-2013
'Created By	    : Rohan Mazumdar
'Version	    : R02.00.00
'Description	: Code behind for PendingDespatches.aspx Page

'****************************************************************
Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Partial Class DespatchReceiveList
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CheckLogin()

        If Not IsPostBack Then

            PopulateUnit()
            PopulateRegion()
            PopulateDepot()
            PopulateProcessYearMonth()

            PopulateDespatchReceiptGrid()

            'LoadSearchCriteria()
            txtChallanNo.Attributes.Add("onkeypress", "KeyPressNumeric()")
            btnSubmit.Attributes.Add("onclick", "return validateGrid();")

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

        Dim mstr As New PendingDespatchesClass
        Dim dsUnitSet As New DataSet

        dsUnitSet = mstr.GetUnitName(Constant.Common.ActiveStatus, ddlRegion.SelectedValue)
        If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
            ddlSource.DataSource = dsUnitSet.Tables(0)
            ddlSource.DataTextField = "unit_name"
            ddlSource.DataValueField = "unit_code"
            ddlSource.DataBind()
            ddlSource.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If

    End Sub
#End Region

#Region "Populate Region"
    Private Sub PopulateRegion()

        Dim mstr As New Common
        Dim dsRegion As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE

        dsRegion = mstr.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)

        If (Not (dsRegion Is Nothing) AndAlso dsRegion.Tables.Count > 0 AndAlso Not (dsRegion.Tables(0) Is Nothing) AndAlso dsRegion.Tables(0).Rows.Count > 0) Then
            ddlRegion.DataSource = dsRegion.Tables(0)
            ddlRegion.DataTextField = "lov_value"
            ddlRegion.DataValueField = "lov_code"
            ddlRegion.DataBind()
            'ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If

        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlRegion.SelectedValue = userInfo.userRegionEntity
            ddlRegion.Enabled = False
        End If

    End Sub
#End Region

#Region "Populate Depot"
    Private Sub PopulateDepot()

        Dim mstr As New Common
        Dim dsDepot As New DataSet

        dsDepot = mstr.Getdepotname(ddlRegion.SelectedValue)

        If (Not (dsDepot Is Nothing) AndAlso dsDepot.Tables.Count > 0 AndAlso Not (dsDepot.Tables(0) Is Nothing) AndAlso dsDepot.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = dsDepot.Tables(0)
            ddlDepot.DataTextField = "depot_name"
            ddlDepot.DataValueField = "depot_code"
            ddlDepot.DataBind()
        End If

        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
            ddlDepot.SelectedValue = userInfo.userBranchEntity
            ddlDepot.Enabled = False
            'ElseIf (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
            'ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If

    End Sub
#End Region

#Region "Populate Process Year"
    Private Sub PopulateProcessYearMonth()

        Dim mstr As New Common
        Dim StandrdParams As New MonthlyUnitDespatch
        Dim dsYearSet As New DataSet
        Dim StandardYrMnth As New DataSet

        dsYearSet = mstr.GetFinYrDetails(Constant.Common.Company, Constant.Common.ActiveStatus)

        StandardYrMnth = StandrdParams.GetMnthsYr(Constant.Common.ActiveStatus)
        If (Not (dsYearSet Is Nothing) AndAlso dsYearSet.Tables.Count > 0 AndAlso Not (dsYearSet.Tables(0) Is Nothing) AndAlso dsYearSet.Tables(0).Rows.Count > 0) Then
            ddlProcessYear.DataSource = dsYearSet.Tables(0)
            ddlProcessYear.DataTextField = "fin_year"
            ddlProcessYear.DataValueField = "fin_year"
            ddlProcessYear.DataBind()
        End If

        If (Not (StandardYrMnth Is Nothing) AndAlso StandardYrMnth.Tables.Count > 0 AndAlso Not (StandardYrMnth.Tables(0) Is Nothing) AndAlso StandardYrMnth.Tables(0).Rows.Count > 0) Then
            ddlProcessYear.SelectedValue = StandardYrMnth.Tables(0).Rows(0)("param_char_value")
            ddlProcessMonth.SelectedValue = StandardYrMnth.Tables(0).Rows(1)("param_char_value")
        End If

    End Sub
#End Region

#Region "Populate Despatch Receipt Grid"

    Private Sub PopulateDespatchReceiptGrid()

        Dim mstr As New DespatchReceipt()
        Dim dsDespatchReceiptList As DataSet

        Dim challan_no As Integer

        If (txtChallanNo.Text.Trim().Equals(String.Empty)) Then
            challan_no = Integer.MinValue
        Else
            challan_no = CType(txtChallanNo.Text.Trim(), Integer)
        End If

        dsDespatchReceiptList = mstr.Despatch_Receipt_Get_List(ddlSource.SelectedValue, ddlDepot.SelectedValue, ddlProcessYear.SelectedValue, ddlProcessMonth.SelectedValue, ddlStatus.SelectedValue, challan_no)
        If (Not (dsDespatchReceiptList Is Nothing)) Then

            gvDespatchRecvList.DataSource = dsDespatchReceiptList.Tables(0)

            Dim primary(2) As String

            primary(0) = "unit"
            primary(1) = "process_year"
            primary(2) = "challan_no"

            gvDespatchRecvList.DataKeyNames = primary

            gvDespatchRecvList.DataBind()

        End If

        btnSubmit.Enabled = False

        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then

        '    If (ddlStatus.SelectedValue.Equals("C")) Then
        '        btnSubmit.Enabled = False
        '    ElseIf (ddlStatus.SelectedValue.Equals("P")) Then
        '        If (dsDespatchReceiptList.Tables(0).Rows.Count > 0) Then
        '            btnSubmit.Enabled = True
        '        Else
        '            btnSubmit.Enabled = False
        '        End If
        '    End If

        'End If

    End Sub
#End Region

    'Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
    '    PopulateDespatchReceiptGrid()
    'End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        PopulateDespatchReceiptGrid()
    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
    End Sub

#Region "Gridview Events"

    Protected Sub gvDespatchRecvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDespatchRecvList.RowDataBound

        Dim Calender As HtmlAnchor = Nothing
        Dim txtRecvDate As TextBox = Nothing
        Dim txtRecvLtr As TextBox = Nothing
        Dim txtRecvKg As TextBox = Nothing
        Dim chkSelect As CheckBox = Nothing
        Dim hdnLtr As HiddenField = Nothing
        Dim hdnKg As HiddenField = Nothing

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            chkSelect = CType(e.Row.Cells(0).FindControl("chkSelect"), CheckBox)

            hdnLtr = CType(e.Row.Cells(0).FindControl("hdnLtr"), HiddenField)
            hdnKg = CType(e.Row.Cells(0).FindControl("hdnKg"), HiddenField)

            txtRecvLtr = CType(e.Row.Cells(10).FindControl("txtRecvLtr"), TextBox)
            txtRecvKg = CType(e.Row.Cells(11).FindControl("txtRecvKg"), TextBox)

            Calender = CType(e.Row.Cells(12).FindControl("Calender"), HtmlAnchor)
            txtRecvDate = CType(e.Row.Cells(12).FindControl("txtRecvDate"), TextBox)

            chkSelect.Attributes.Add("onclick", "rwslctToggleSelect('" & chkSelect.ClientID _
                                                                        & "', '" & hdnLtr.ClientID _
                                                                        & "', '" & hdnKg.ClientID _
                                                                        & "', '" & txtRecvLtr.ClientID _
                                                                        & "', '" & txtRecvKg.ClientID _
                                                                        & "', '" & txtRecvDate.ClientID _
                                                                        & "');")

            If Not Calender Is Nothing Then
                Calender.HRef = "javascript:cal1.select(document.forms[0]." & txtRecvDate.ClientID & ",'" & txtRecvDate.ClientID & "','dd/MM/yyyy');"
            End If

            txtRecvLtr.Enabled = False
            txtRecvKg.Enabled = False
            txtRecvDate.Enabled = False

            txtRecvLtr.Attributes.Add("onkeypress", "KeyPressDecimal()")
            txtRecvKg.Attributes.Add("onkeypress", "KeyPressDecimal()")
            txtRecvLtr.Attributes.Add("onblur", "validateReceiptValue('" + txtRecvLtr.ClientID + "')")
            txtRecvKg.Attributes.Add("onblur", "validateReceiptValue('" + txtRecvKg.ClientID + "')")

            If (ddlStatus.SelectedValue.Equals("C")) Then
                Calender.Visible = False
                chkSelect.Visible = False
            Else
                Calender.Visible = True
                chkSelect.Visible = True
            End If

        End If

    End Sub

#End Region


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim ms As DespatchReceipt = New DespatchReceipt()

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Dim RowsAffected As Integer

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()


            For RowIndex As Integer = 0 To gvDespatchRecvList.Rows.Count - 1

                Dim gvRow As GridViewRow = gvDespatchRecvList.Rows(RowIndex)

                Dim chkSelect As CheckBox = CType(gvRow.FindControl("chkSelect"), CheckBox)
                Dim unit As String = gvDespatchRecvList.DataKeys.Item(RowIndex).Item("unit").ToString()
                Dim process_year As String = gvDespatchRecvList.DataKeys.Item(RowIndex).Item("process_year").ToString()
                Dim challan_no As Integer = CType(gvDespatchRecvList.DataKeys.Item(RowIndex).Item("challan_no").ToString(), Integer)
                Dim txtRecvLtr As TextBox = CType(gvRow.FindControl("txtRecvLtr"), TextBox)
                Dim txtRecvKg As TextBox = CType(gvRow.FindControl("txtRecvKg"), TextBox)
                Dim txtRecvDate As TextBox = CType(gvRow.FindControl("txtRecvDate"), TextBox)

                If (chkSelect.Checked = True) Then

                    Dim despatch_receive_record As DespatchReceiveDetailsEntity = New DespatchReceiveDetailsEntity()
                    despatch_receive_record.VUnit = unit
                    despatch_receive_record.ProcessYear = process_year
                    despatch_receive_record.ChallanNo = challan_no
                    despatch_receive_record.ReceiveTotalLtr = CType(txtRecvLtr.Text.Trim(), Decimal)
                    despatch_receive_record.ReceiveTotalKg = CType(txtRecvKg.Text.Trim(), Decimal)
                    despatch_receive_record.ReceiveDate = FormatDate(txtRecvDate.Text.Trim())
                    despatch_receive_record.CreatedUser = userInfo.userIDEntity
                    despatch_receive_record.AdditionalYN = "N"
                    RowsAffected += ms.Despatch_Receipt_Insert(despatch_receive_record, sqlConn, sqlTrans)

                End If

            Next


            If RowsAffected > 0 Then
                sqlTrans.Commit()

                PopulateDespatchReceiptGrid()

                lblErrorMessage.Text = "Records updated successfully."
                lblErrorMessage.ForeColor = System.Drawing.Color.Green
                'CreateMessageAlert("Records updated successfully.", "alertKey")
            Else
                sqlTrans.Rollback()
                lblErrorMessage.Text = Constant.ErrorMessages.GeneralError
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                'CreateMessageAlert(Constant.ErrorMessages.GeneralError, "alertKey")
            End If

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        Finally
            If Not (sqlConn Is Nothing) Then
                'sqlConn is set to close state after completing the transaction
                sqlConn.Close()
            End If
        End Try

    End Sub

    '#Region "Alert Message"
    '    Public Sub CreateMessageAlert(ByVal alertMsg As String, ByVal alertKey As String)
    '        Dim strScript As String
    '        strScript = "<script language=JavaScript>alert('" + alertMsg + "'); </script>"
    '        If Not (ClientScript.IsStartupScriptRegistered(alertKey)) Then
    '            ClientScript.RegisterStartupScript(Me.GetType(), alertKey, strScript)
    '        End If
    '    End Sub
    '#End Region

#Region "Date Format"
    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime

        If (stringdate.Equals(String.Empty)) Then
            Return SqlDateTime.MinValue
        End If

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

    'Protected Sub imgbtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAdd.Click
    '    Response.Redirect("AddAdditionalDespatchReceipt.aspx", True)
    'End Sub

    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        Response.Redirect("AddAdditionalDespatchReceipt.aspx", True)
    End Sub
End Class
