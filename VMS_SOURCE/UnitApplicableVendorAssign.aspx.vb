'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : UnitApplicableVendorAssign.aspx.vb
'Created Date	: 13-09-2018
'Created By	    : Debayan Das
'Version	    : R01.00.00
'Description	: Code behind for Unit Applicable Product Assign Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class UnitApplicableVendorAssign
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then

            AddAttributes()
            PopulateUnit()
            PopulateVendorUnitProduct()
            gvTokenVendorList.PageIndex = 0
            BindGrid()
        End If

    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()

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

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            'Server.Transfer(returnUrl)
            Response.Redirect(returnUrl)
        End Try

    End Sub
#End Region

#Region "Populate Product dropdown."

    Private Sub PopulateVendorUnitProduct()

        CheckLogin()
        Try
            Dim obj As New UnitApplicableProductAssignClass
            Dim dsProductSet As New DataSet

            dsProductSet = obj.GetProductNameFromUnit(ddlVendorUnit.SelectedValue)
            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                ddlVendorProduct.DataSource = dsProductSet.Tables(0)
                ddlVendorProduct.DataTextField = "sku_prd_desc"
                ddlVendorProduct.DataValueField = "sku_new_code"
                ddlVendorProduct.DataBind()

                If Not (dsProductSet.Tables(0).Rows.Count = 1) Then
                    ddlVendorProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            'Server.Transfer(returnUrl)
            Response.Redirect(returnUrl)
        End Try
    End Sub

#End Region
#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim obj As New UnitApplicableVendorAssignClass
            Dim dsProductSet As New DataSet

            dsProductSet = obj.GetProductList(ddlVendorUnit.SelectedValue, ddlVendorProduct.SelectedValue, ddlActive.SelectedValue)
            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvTokenVendorList.DataSource = dsProductSet.Tables(0)
                gvTokenVendorList.DataBind()
            Else
                gvTokenVendorList.DataSource = Nothing
                gvTokenVendorList.DataBind()
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            'Server.Transfer(returnUrl)
            Response.Redirect(returnUrl)
        End Try

    End Sub
#End Region


    Protected Sub ddlVendorUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorUnit.SelectedIndexChanged
        PopulateVendorUnitProduct()
    End Sub
    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTokenVendorList.PageIndexChanging
        gvTokenVendorList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    'Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
    '    BindGrid()
    'End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        BindGrid()
    End Sub



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
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            'Server.Transfer(returnUrl)
            Response.Redirect(returnUrl)
        End Try

    End Sub
#End Region
    Protected Sub gvProductList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTokenVendorList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlTokenVendor"), DropDownList)
            Dim hdnTokenVendor As HiddenField = CType(e.Row.FindControl("hdnTokenVendor"), HiddenField)
            PopulateTokenVendor(ddl)
            If (ddl.Items.Count > 0 And Not (hdnTokenVendor.Value.Equals(String.Empty))) Then
                ddl.SelectedItem.Selected = False
                ddl.SelectedValue = hdnTokenVendor.Value
            End If
            Dim hdnStatus As HiddenField = CType(e.Row.FindControl("hdnActive"), HiddenField)
            If (hdnStatus.Value.Equals("Y")) Then
                e.Row.Style.Add("background-color", "#bdffb5")
            End If
            Dim btn As ImageButton = CType(e.Row.FindControl("imgBtnSubmit"), ImageButton)
            btn.OnClientClick = "return ValidateTokenVendorAssign('" & ddl.ClientID & "','" & btn.ClientID & "');"
        End If
    End Sub
    Protected Sub gvTokenVendorList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTokenVendorList.RowCommand
        CheckLogin()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New UnitApplicableVendorAssignClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Try
            Dim gvrow As GridViewRow = CType(CType(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            Dim hdnUnit As HiddenField = CType(gvrow.FindControl("hdnUnit"), HiddenField)
            'Dim hdnProductId As HiddenField = CType(gvrow.FindControl("hdnProductId"), HiddenField)
            'Dim hdnPackSize As HiddenField = CType(gvrow.FindControl("hdnPackSize"), HiddenField)
            Dim hdnskuCode As HiddenField = CType(gvrow.FindControl("hdnskuCode"), HiddenField)
            Dim hdnActive As HiddenField = CType(gvrow.FindControl("hdnActive"), HiddenField)
            Dim ddlTokenVendor As DropDownList = CType(gvrow.FindControl("ddlTokenVendor"), DropDownList)
            'If (Not (hdnUnit.Value.Equals(String.Empty)) And Not (hdnProductId.Value.Equals(String.Empty)) And Not (hdnPackSize.Value.Equals(String.Empty)) And Not (ddlTokenVendor.SelectedValue.Equals(String.Empty))) Then
            If (Not (hdnUnit.Value.Equals(String.Empty)) And Not (hdnskuCode.Value.Equals(String.Empty)) And Not (ddlTokenVendor.SelectedValue.Equals(String.Empty))) Then
                'RecordInserted = obj.AssignTokenVendor(hdnUnit.Value, hdnProductId.Value, hdnPackSize.Value, ddlTokenVendor.SelectedValue, userInfo.userIDEntity, Constant.Common.ActiveStatus, sqlConn, sqlTrans)
                RecordInserted = obj.AssignTokenVendor(hdnUnit.Value, hdnskuCode.Value, ddlTokenVendor.SelectedValue, userInfo.userIDEntity, Constant.Common.ActiveStatus, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    sqlTrans.Commit()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');", True)
                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                End If
            Else
                lblErrorMessage.Text = "Required field can't be blank..."
            End If
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
            BindGrid()
        End Try
    End Sub
End Class
