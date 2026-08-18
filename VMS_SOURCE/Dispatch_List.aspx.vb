
Imports System.Data
Imports VMS.Web

Partial Class Dispatch_List
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If (Not IsPostBack) Then
            Dim rmVendorCode As String = String.Empty
            If Request.QueryString("rmvendor_code") IsNot Nothing Then
                rmVendorCode = Request.QueryString("rmvendor_code").ToString()
            End If
            'Dim rmVendorCode As String = "RM001"
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
        gvDispatchList.DataSource = Nothing
        gvDispatchList.DataBind()

        Dim ds As DataSet = obj.GetDispatchList(rmVendorCode, ddlStatus.SelectedValue)

        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim vendorName As String = ds.Tables(0).Rows(0)("rvm_vendor_name").ToString()
            lblRmVendor.Text = vendorName ' or wherever you want to display it
        End If

        If (ds IsNot Nothing AndAlso ds.Tables.Count > 1 AndAlso ds.Tables(1) IsNot Nothing) Then
            gvDispatchList.DataSource = ds.Tables(1)
            gvDispatchList.DataBind()
        Else
            gvDispatchList.DataSource = Nothing
            gvDispatchList.DataBind()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        'Dim rmVendorCode As String = String.Empty
        'If Request.QueryString("rmvendor_code") IsNot Nothing Then
        '    rmVendorCode = Request.QueryString("rmvendor_code").ToString()
        'End If

        PopulateList(rmVendorCode)
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
