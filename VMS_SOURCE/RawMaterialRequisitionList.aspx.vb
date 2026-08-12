Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Collections.Generic

Partial Class RawMaterialRequisitionList
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            BindDropDown()
            BindData()
        End If
    End Sub
    Private Sub BindDropDown()
        PopulateUnit()
        PopulateRawMatVendor()
        PopulateApprovalStatus()
    End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs)
        BindData()
    End Sub

    Protected Sub ImgbtnAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/RawMaterialRequisitionDtls.aspx", False)
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        ddlvendor.SelectedIndex = 0
        ddlRawMatvendor.SelectedIndex = 0
        ddlApprovalstatus.SelectedIndex = 0
        BindData()
    End Sub

    Protected Sub gvRequisition_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "ViewRequisition" Then
                Dim row As GridViewRow = Nothing
                Dim clickedControl As Control = TryCast(e.CommandSource, Control)
                If Not clickedControl Is Nothing Then
                    row = TryCast(clickedControl.NamingContainer, GridViewRow)
                End If

                If row Is Nothing Then
                    Dim rowIndex As Integer = 0
                    If Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) AndAlso rowIndex >= 0 AndAlso rowIndex < gvRequisition.Rows.Count Then
                        row = gvRequisition.Rows(rowIndex)
                    End If
                End If

                If row Is Nothing Then
                    Throw New Exception("Unable to determine selected row.")
                End If

                Dim hdnRequestId As HiddenField = CType(row.FindControl("hdnRequestId"), HiddenField)
                Dim redirectUrl = "~/RawMaterialRequisitionDtls.aspx?request_id=" & Server.UrlEncode(hdnRequestId.Value)
                Response.Redirect(redirectUrl, False)
                Context.ApplicationInstance.CompleteRequest()
            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub BindData()
        Try
            Dim obj As New OPC_VendorClass()
            Dim ds As DataSet = obj.GetRawMaterialRequestList(ddlvendor.SelectedValue, ddlRawMatvendor.SelectedValue, ddlApprovalstatus.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    gvRequisition.DataSource = ds.Tables(0)
                    gvRequisition.DataBind()
                Else
                    gvRequisition.DataSource = Nothing
                    gvRequisition.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub PopulateRawMatVendor()
        Dim obj As New OPC_VendorClass()
        Dim ds As DataSet = obj.GetRawMaterialVendorList()

        ddlRawMatvendor.Items.Clear()
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlRawMatvendor.DataSource = ds.Tables(0)
            ddlRawMatvendor.DataTextField = "vendor_name"
            ddlRawMatvendor.DataValueField = "vendor_code"
            ddlRawMatvendor.DataBind()
        End If
        ddlRawMatvendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub

    Private Sub PopulateApprovalStatus()
        'ddlApprovalstatus.Items.Clear()
        'ddlApprovalstatus.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        ddlApprovalstatus.Items.Insert(0, New ListItem("Pending", "P"))
        ddlApprovalstatus.Items.Insert(1, New ListItem("Approved", "A"))
    End Sub

#Region "Populate Unit"
    Private Sub PopulateUnit()
        Dim UnitDespatch As New PendingDespatchesClass
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, String.Empty)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlvendor.DataSource = UnitSet.Tables(0)
            ddlvendor.DataTextField = "unit_name"
            ddlvendor.DataValueField = "unit_code"
            ddlvendor.DataBind()
            ddlvendor.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlvendor.SelectedValue = userInfo.userBranchEntity
            ddlvendor.Enabled = False
        End If
    End Sub
#End Region
End Class
