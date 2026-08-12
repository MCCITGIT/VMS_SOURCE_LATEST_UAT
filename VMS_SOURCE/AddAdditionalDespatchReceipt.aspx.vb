'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : AddAditionalDespatchReceiptList.aspx.vb
'Created Date	: 18-Apr-2013
'Created By	    : Rohan Mazumdar
'Version	    : R02.00.00
'Description	: Code behind for PendingDespatches.aspx Page

'****************************************************************
Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Partial Class AddAdditionalDespatchReceipt
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CheckLogin()

        If Not IsPostBack Then

            PopulateUnit()
            PopulateRegion()
            'PopulateDepot()
            PopulateProcessYearMonth()

            txtReceivedLtr.Attributes.Add("onkeypress", "KeyPressDecimal()")
            txtReceivedKg.Attributes.Add("onkeypress", "KeyPressDecimal()")

            btnSubmit.Attributes.Add("onclick", "return ValidateAdditionalReceiptDetails();")

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
            ddlSource.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
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
            ddlRegion.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If

        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlRegion.SelectedValue = userInfo.userRegionEntity
            ddlRegion.Enabled = False
        End If

    End Sub
#End Region

    '#Region "Populate Depot"
    '    Private Sub PopulateDepot()

    '        Dim mstr As New Common
    '        Dim dsDepot As New DataSet

    '        dsDepot = mstr.Getdepotname(ddlRegion.SelectedValue)

    '        If (Not (dsDepot Is Nothing) AndAlso dsDepot.Tables.Count > 0 AndAlso Not (dsDepot.Tables(0) Is Nothing) AndAlso dsDepot.Tables(0).Rows.Count > 0) Then
    '            ddlDepot.DataSource = dsDepot.Tables(0)
    '            ddlDepot.DataTextField = "depot_name"
    '            ddlDepot.DataValueField = "depot_code"
    '            ddlDepot.DataBind()
    '        End If

    '        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
    '            ddlDepot.SelectedValue = userInfo.userBranchEntity
    '            ddlDepot.Enabled = False
    '            'ElseIf (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
    '            'ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
    '        End If

    '    End Sub
    '#End Region

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

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim ms As DespatchReceipt = New DespatchReceipt()

        Dim ChallanNo As Integer

        Try

            Dim despatch_receive_record As DespatchReceiveDetailsEntity = New DespatchReceiveDetailsEntity()

            despatch_receive_record.VUnit = ddlSource.SelectedValue
            despatch_receive_record.ProcessYear = ddlProcessYear.SelectedValue
            despatch_receive_record.VDepot = userInfo.userIDEntity.Substring(2)
            despatch_receive_record.ProcessMonth = ddlProcessMonth.SelectedValue
            despatch_receive_record.ChallanDate = FormatDate(txtChallanDate.Text.Trim())
            despatch_receive_record.ReceiveTotalLtr = CType(txtReceivedLtr.Text.Trim(), Decimal)
            despatch_receive_record.ReceiveTotalKg = CType(txtReceivedKg.Text.Trim(), Decimal)
            despatch_receive_record.TransporterName = txtTransporterName.Text.Trim()
            despatch_receive_record.PermitNo = txtRoadPermitNo.Text.Trim()
            despatch_receive_record.TruckNo = txtTruckNo.Text.Trim()
            despatch_receive_record.GPNo = txtCenvatNo.Text.Trim()
            despatch_receive_record.GPDate = FormatDate(txtCenvatDate.Text.Trim())
            despatch_receive_record.ReceiveDate = FormatDate(txtReceiptDate.Text.Trim())
            despatch_receive_record.CreatedUser = userInfo.userIDEntity
            'despatch_receive_record.AdditionalYN = "Y"

            ChallanNo = ms.DespatchReceiptAdditionalEntry_Insert(despatch_receive_record)

            If ChallanNo > 0 Then
                lblChallanNo.Text = ChallanNo.ToString()
                lblErrorMessage.Text = "Records updated successfully."
                lblErrorMessage.ForeColor = System.Drawing.Color.Green
                btnSubmit.Enabled = False
            Else
                lblErrorMessage.Text = Constant.ErrorMessages.GeneralError
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Response.Redirect("DespatchReceiveList.aspx", True)

    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateUnit()
    End Sub

End Class
