
Imports System.Data
Imports VMS.Web

Partial Class Dispatch_List
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If (Not IsPostBack) Then
            'Dim rmVendorCode As String = String.Empty
            'If Request.QueryString("rmvendor_code") IsNot Nothing Then
            '    rmVendorCode = Request.QueryString("rmvendor_code").ToString()
            'End If
            Dim rmVendorCode As String = "5023412"
            ViewState("RmVendorCode") = rmVendorCode
            divVendor.Visible = False
            populateStatus()
        End If
        PopulateList(rmVendorCode)
    End Sub

    Private Property RmVendorCode As String
        Get
            If ViewState("RmVendorCode") Is Nothing Then
                Return String.Empty
            End If
            Return ViewState("RmVendorCode").ToString()
        End Get
        Set(value As String)
            ViewState("RmVendorCode") = value
        End Set
    End Property

#End Region

#Region "PopulateVendor"
    Public Sub populateVendor()
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet
        DS = Obj.GetVendor(userInfo.userIDEntity)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlVendor.DataSource = DS.Tables(0)
            ddlVendor.DataTextField = "vendor_name"
            ddlVendor.DataValueField = "vendor_code"
            ddlVendor.DataBind()
            ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If DS.Tables(0).Rows.Count = 1 Then
                ddlVendor.SelectedIndex = 1
                ddlVendor.Enabled = False
            End If
        End If

    End Sub
#End Region

    Public Sub populateStatus()
        Dim Obj As New POLinkingRequestClass
        Dim DS As New DataSet
        DS = Obj.GetLovDetails("DISPATCH_STATUS", Constant.Common.ActiveStatus)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlStatus.DataSource = DS.Tables(0)
            ddlStatus.DataTextField = "lov_value"
            ddlStatus.DataValueField = "lov_code"
            ddlStatus.DataBind()
            'ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            ddlStatus.SelectedIndex = 0
            If DS.Tables(0).Rows.Count = 1 Then
                ddlStatus.SelectedIndex = 1
                ddlStatus.Enabled = False
            End If
        End If

    End Sub

    Private Sub PopulateList(ByVal rmVendorCode As String)
        Dim obj As POLinkingRequestClass = New POLinkingRequestClass()
        Dim ds As DataSet = obj.GetDispatchList(rmVendorCode, ddlStatus.SelectedValue)

        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim vendorName As String = ds.Tables(0).Rows(0)("rvm_vendor_name").ToString()
            lblRmVendor.Text = vendorName ' or wherever you want to display it
        End If

        If (ds IsNot Nothing AndAlso ds.Tables.Count > 1 AndAlso ds.Tables(1) IsNot Nothing) Then
            RmGridHelper.BindPaged(gvDispatchList, ds.Tables(1))
        Else
            RmGridHelper.BindPaged(gvDispatchList, Nothing)
        End If

        BindSummaryCounts(rmVendorCode, ddlStatus.SelectedValue, ds)
    End Sub

    Protected Function GetVendorInitials(ByVal nameObj As Object) As String
        Dim name As String = If(nameObj Is Nothing, String.Empty, nameObj.ToString().Trim())
        If String.IsNullOrEmpty(name) Then
            Return "--"
        End If

        Dim parts() As String = name.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        If parts.Length = 1 Then
            Return parts(0).Substring(0, Math.Min(2, parts(0).Length)).ToUpper()
        End If

        Return (parts(0).Substring(0, 1) & parts(1).Substring(0, 1)).ToUpper()
    End Function

    Private Sub BindSummaryCounts(ByVal rmVendorCode As String, ByVal selectedStatus As String, ByVal currentDs As DataSet)
        Dim currentCount As Integer = 0
        If currentDs IsNot Nothing AndAlso currentDs.Tables.Count > 1 AndAlso currentDs.Tables(1) IsNot Nothing Then
            currentCount = currentDs.Tables(1).Rows.Count
        End If

        Dim pendingCount As Integer = 0
        Dim completedCount As Integer = 0
        Dim statusValue As String = If(selectedStatus, String.Empty).Trim().ToUpper()

        If statusValue = "PENDING" Then
            pendingCount = currentCount
            completedCount = GetDispatchCount(rmVendorCode, "DISPATCHED")
        ElseIf statusValue = "DISPATCHED" Then
            completedCount = currentCount
            pendingCount = GetDispatchCount(rmVendorCode, "PENDING")
        Else
            pendingCount = GetDispatchCount(rmVendorCode, "PENDING")
            completedCount = GetDispatchCount(rmVendorCode, "DISPATCHED")
        End If

        lblPendingRequests.Text = pendingCount.ToString()
        lblCompletedRequests.Text = completedCount.ToString()
        lblTotalRequests.Text = (pendingCount + completedCount).ToString()
    End Sub

    Private Function GetDispatchCount(ByVal rmVendorCode As String, ByVal dispatchStatus As String) As Integer
        Try
            Dim obj As POLinkingRequestClass = New POLinkingRequestClass()
            Dim ds As DataSet = obj.GetDispatchList(rmVendorCode, dispatchStatus)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 1 AndAlso ds.Tables(1) IsNot Nothing Then
                Return ds.Tables(1).Rows.Count
            End If
        Catch
        End Try
        Return 0
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        gvDispatchList.PageIndex = 0
        PopulateList(RmVendorCode)
    End Sub

    Protected Sub gvDispatchList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDispatchList.PageIndexChanging
        gvDispatchList.PageIndex = e.NewPageIndex
        PopulateList(RmVendorCode)
    End Sub

    Protected Sub lbtnDetails_Click(sender As Object, e As EventArgs)

        Dim lbtn As LinkButton = CType(sender, LinkButton)

        Dim row As GridViewRow = CType(lbtn.NamingContainer, GridViewRow)

        Dim hdnReqId As HiddenField = CType(row.FindControl("hdnReqId"), HiddenField)
        Dim hdnVendorCode As HiddenField = CType(row.FindControl("hdnVendorCode"), HiddenField)

        Dim orhId As String = hdnReqId.Value
        Dim orhVendorCode As String = hdnVendorCode.Value

        'Get selected status
        Dim dispatchStatus As String = ddlStatus.SelectedValue

        If Not String.IsNullOrEmpty(orhId) AndAlso
           Not String.IsNullOrEmpty(orhVendorCode) AndAlso
           Not String.IsNullOrEmpty(dispatchStatus) Then

            Dim url As String =
                "Dispatch_Details.aspx?orh_id=" & Server.UrlEncode(orhId) &
                "&orh_vendor_code=" & Server.UrlEncode(orhVendorCode) &
                "&dispatch_status=" & Server.UrlEncode(dispatchStatus)

            Response.Redirect(url)

        End If

    End Sub
End Class
