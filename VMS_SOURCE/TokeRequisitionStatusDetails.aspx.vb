Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class TokeRequisitionStatusDetails
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If


        If Not IsPostBack Then

            PopulateUnit()
            'PopulateUnitApplicableSite()
            PopulateTokenVendor(ddlTokenVendor)
            gvRequisitionItemsList.PageIndex = 0
            If Not (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                BindGrid()
            End If

        End If
    End Sub


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        'btnSubmit.OnClientClick = "return ValidateSubmit();"
    End Sub
#End Region

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
        Try
            Dim obj As New UnitApplicableProductAssignClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetUnitName(Constant.Common.ActiveStatus)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendorUnit.DataSource = dsUnitSet.Tables(0)
                ddlVendorUnit.DataTextField = "unit_name"
                ddlVendorUnit.DataValueField = "unit_code"
                ddlVendorUnit.DataBind()
                If (userInfo.userGroupCodeEntity.Equals("UNIT")) Then
                    ddlVendorUnit.SelectedValue = userInfo.userIDEntity
                    ddlVendorUnit.Enabled = False
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    '#Region "Populate Site"
    '    Private Sub PopulateUnitApplicableSite()
    '        CheckLogin()
    '        Try
    '            Dim obj As New UnitApplicableProductAssignClass
    '            Dim dsUnitSet As New DataSet
    '            dsUnitSet = obj.GetUnitApplicableSites(ddlVendorUnit.SelectedValue, Constant.Common.ActiveStatus)
    '            ddlSite.Items.Clear()
    '            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
    '                ddlSite.DataSource = dsUnitSet.Tables(0)
    '                ddlSite.DataTextField = "utas_site_name"
    '                ddlSite.DataValueField = "utas_site_code"
    '                ddlSite.DataBind()
    '                If (dsUnitSet.Tables(0).Rows.Count <> 1) Then
    '                    ddlSite.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
    '                End If
    '            Else
    '                ddlSite.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
    '            End If
    '        Catch ex As Exception
    '            Dim returnUrl As String = "~/ExceptionPage.aspx"
    '            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '            Server.Transfer(returnUrl)
    '        End Try

    '    End Sub
    '#End Region

#Region "Populate Token Vendor List"
    Private Sub PopulateTokenVendor(ddl As DropDownList)
        CheckLogin()
        Try
            Dim obj As New UnitApplicableVendorAssignClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetTokenVendorList(String.Empty, Constant.Common.ActiveStatus)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then

                ddl.DataSource = dsUnitSet.Tables(0)
                ddl.DataTextField = "tvm_name"
                ddl.DataValueField = "tvm_code"
                ddl.DataBind()

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    'Protected Sub ddlVendorUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorUnit.SelectedIndexChanged
    '    PopulateUnitApplicableSite()
    'End Sub


#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim obj As New TokenVendorRequisitionClass
            Dim dsProductSet As New DataSet
            If (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                'dsProductSet = obj.GetRequisitionStatusDetailsByid(ddlVendorUnit.SelectedValue, String.Empty, String.Empty, ddlTokenVendor.SelectedValue)
                'lblReqId.ForeColor = Drawing.Color.Red

                'ddlVendorUnit.Attributes.Remove("disabled")
            Else
                dsProductSet = obj.GetRequisitionStatusDetailsByid(Convert.ToInt32(Request.QueryString("id")))
                txtDesc.Text = dsProductSet.Tables(0).Rows(0)("trh_desc").ToString()
                txtDesc.Enabled = False
                ddlTokenVendor.SelectedItem.Selected = False
                ddlTokenVendor.SelectedValue = dsProductSet.Tables(0).Rows(0)("tokenVendor").ToString()
                ddlTokenVendor.Enabled = False
                'ddlSite.Enabled = False
                lblReqId.ForeColor = Drawing.Color.Black
                lblReqId.Text = dsProductSet.Tables(0).Rows(0)("trd_requisition_id").ToString()
                ddlVendorUnit.Attributes.Add("disabled", "true")
            End If

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvRequisitionItemsList.DataSource = dsProductSet.Tables(0)
                gvRequisitionItemsList.DataBind()

            Else
                gvRequisitionItemsList.DataSource = Nothing
                gvRequisitionItemsList.DataBind()


            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub
End Class
