
Imports System.Data
Imports VMS.Web

Partial Class RmpDashboard
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            BindDashboardDetails()
            'PopulateVendor()
            PopulateDuration()
        End If
    End Sub

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub PopulateDuration()
        Dim Obj As New POLinkingRequestClass
        Dim DS As New DataSet
        DS = Obj.GetLovDetails("DURATION", Constant.Common.ActiveStatus)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlDurationType.DataSource = DS.Tables(0)
            ddlDurationType.DataTextField = "lov_value"
            ddlDurationType.DataValueField = "lov_code"
            ddlDurationType.DataBind()
            ddlDurationType.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If DS.Tables(0).Rows.Count = 1 Then
                ddlDurationType.SelectedIndex = 1
                ddlDurationType.Enabled = False
            End If
        End If
    End Sub

    Private Sub PopulateMonth()
        Dim Obj As New POLinkingRequestClass
        Dim DS As New DataSet
        DS = Obj.GetLovDetails("MONTH", Constant.Common.ActiveStatus)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlMonth.DataSource = DS.Tables(0)
            ddlMonth.DataTextField = "lov_value"
            ddlMonth.DataValueField = "lov_code"
            ddlMonth.DataBind()
            ddlMonth.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If DS.Tables(0).Rows.Count = 1 Then
                ddlMonth.SelectedIndex = 1
                ddlMonth.Enabled = False
            End If
        End If
    End Sub

    Private Sub PopulateFinYear()
        Dim Obj As New POLinkingRequestClass
        Dim DS As New DataSet
        DS = Obj.GetLovDetails("YEAR", Constant.Common.ActiveStatus)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlYear.DataSource = DS.Tables(0)
            ddlYear.DataTextField = "lov_value"
            ddlYear.DataValueField = "lov_code"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If DS.Tables(0).Rows.Count = 1 Then
                ddlYear.SelectedIndex = 1
                ddlYear.Enabled = False
            End If
        End If
    End Sub

    Private Sub BindDashboardDetails()
        'divKpiCards.Visible = False
        divCharts.Visible = False
        gvRmPendingList.Visible = False
        If String.Equals(userInfo.userDepartmentEntity.ToString(), Constant.Common.userDept, StringComparison.OrdinalIgnoreCase) Then
            monthDiv.Visible = False
            divYear.Visible = False
            divDuration.Visible = False
            PopulateUnit()
        Else
            divVendor.Visible = False
            monthDiv.Visible = False
            divYear.Visible = False
            divDuration.Visible = False
        End If
        'BindDashboardData()
    End Sub

    Private Sub PopulateUnit()
        Dim UnitDespatch As New PendingDespatchesClass
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, String.Empty)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlVendor.DataSource = UnitSet.Tables(0)
            ddlVendor.DataTextField = "unit_name"
            ddlVendor.DataValueField = "unit_code"
            ddlVendor.DataBind()
            ddlVendor.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlVendor.SelectedValue = userInfo.userBranchEntity
            ddlVendor.Enabled = False
        End If
    End Sub

    Private Sub BindDashboardData(ByVal vendorCode As String)

        If String.IsNullOrWhiteSpace(vendorCode) OrElse vendorCode = "0" Then

            'divKpiCards.Visible = False
            divCharts.Visible = False
            gvRmPendingList.Visible = False

            hdnRmChartData.Value = "[]"

            Exit Sub

        End If

        'divKpiCards.Visible = True
        divCharts.Visible = True
        gvRmPendingList.Visible = True

        PopulateRmChart(vendorCode)

    End Sub

    Private Sub PopulateRmChart(ByVal userId As String)
        Dim obj As POLinkingRequestClass = New POLinkingRequestClass()
        Dim ds As DataSet = obj.GetRmConsumedDetails(userId)
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim chartData As New List(Of Object)()
            For Each row As DataRow In ds.Tables(0).Rows
                chartData.Add(New With {
                .RawMaterialCode = row("fd_rawmaterial_code").ToString(),
                .PurchasedQty = If(
                    IsDBNull(row("total_recv_qty")),
                    0D,
                    Convert.ToDecimal(row("total_recv_qty"))
                ),
                .UsedQty = If(
                    IsDBNull(row("total_qty_consumed")),
                    0D,
                    Convert.ToDecimal(row("total_qty_consumed"))
                )
            })
            Next
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            hdnRmChartData.Value = serializer.Serialize(chartData)
        Else
            hdnRmChartData.Value = "[]"
        End If
    End Sub

    Private Sub BindPendingDispatchTable(ByVal userId As String)
        userId = "U02"
        Dim obj As POLinkingRequestClass = New POLinkingRequestClass()
        Dim ds As DataSet = obj.GetPendingDispatchData(userId)
        Dim table As DataTable = RmGridHelper.GetTable(ds)
        RmGridHelper.BindPaged(gvRmPendingList, table)
    End Sub

    Protected Sub ddlDurationType_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlDurationType.SelectedValue = "MONTHLY" Then
            divYear.Visible = True
            monthDiv.Visible = True
            PopulateMonth()
            PopulateFinYear()
        ElseIf ddlDurationType.SelectedValue = "YEARLY" Then
            divYear.Visible = True
            monthDiv.Visible = False
            PopulateFinYear()
        Else
            divYear.Visible = False
            monthDiv.Visible = False
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        BindDashboardData(ddlvendor.SelectedValue)
        BindPendingDispatchTable(ddlvendor.SelectedValue)
    End Sub

    Protected Function GetStatusCss(status As String) As String

        If String.IsNullOrWhiteSpace(status) Then
            Return "rmp-status pending"
        End If

        Select Case status.Trim().ToLower()

            Case "transit"
                Return "rmp-status transit"

            Case "pending"
                Return "rmp-status pending"

            Case Else
                Return "rmp-status pending"

        End Select

    End Function
End Class
