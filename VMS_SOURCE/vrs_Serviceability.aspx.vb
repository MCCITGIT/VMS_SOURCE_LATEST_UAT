
Imports System.Data
Imports VMS.Web

Partial Class vrs_Serviceability
    Inherits System.Web.UI.Page
#Region "Global Variable"
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#End Region

#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        'AddAttributes()
        If Not IsPostBack Then
            Populate_Quarter()
            txtVendorcode.Text = userInfo.userIDEntity

            If (userInfo.userGroupCodeEntity.Equals("HO-MARKETING") Or userInfo.userGroupCodeEntity.Equals("SYSADMIN") Or userInfo.userGroupCodeEntity.Equals("DEPOT")) Then
                ' divVendorRating.Visible = True
                ' divVendorDashboard.Visible = False
            End If
        End If
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
#Region "AddAttributes"
    Private Sub AddAttributes()
        btnsearch.Attributes.Add("onclick", "return Validate_VendorRate_Search();")
    End Sub
#End Region

#Region "Populate Quarter"
    Private Sub Populate_Quarter()
        Try
            Dim obj As New vrs_legalscore_class
            Dim ds As New DataSet
            ddlquartor.Items.Clear()
            ds = obj.Get_QuarterList(userInfo.userIDEntity)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                ddlquartor.DataSource = ds.Tables(0)
                ddlquartor.DataTextField = "qm_quarter_short_code"
                ddlquartor.DataValueField = "qm_id"
                ddlquartor.DataBind()

                If Not (ds.Tables(0).Rows.Count = 1) Then
                    ddlquartor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Populate Depot Dispatch Grid"
    Private Sub BindFinalServiceability()
        Try
            Dim obj As New vrs_Serviceability_class
            Dim ds As New DataSet

            ds = obj.Get_ServiceabilityDepotDispatch(txtVendorcode.Text, ddlquartor.SelectedValue.ToString())

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                gvServiceAbility.DataSource = ds
                gvServiceAbility.DataBind()

                lblError.Text = ""
            Else
                gvServiceAbility.DataSource = Nothing
                gvServiceAbility.DataBind()


            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub gvServiceAbility_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvServiceAbility.PageIndexChanging
        Try
            gvServiceAbility.PageIndex = e.NewPageIndex
            BindFinalServiceability()
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    '#Region "Populate Direct Dispatch Grid"
    '    Private Sub BindDirectDispatch()
    '        Try
    '            Dim obj As New vrs_Serviceability_class
    '            Dim ds As New DataSet

    '            ds = obj.Get_ServiceabilityDirectDispatch(txtVendorcode.Text, ddlquartor.SelectedValue.ToString())

    '            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
    '                gvDirectDispatch.DataSource = ds
    '                gvDirectDispatch.DataBind()

    '                lblError.Text = ""
    '            Else
    '                gvDirectDispatch.DataSource = Nothing
    '                gvDirectDispatch.DataBind()


    '            End If
    '        Catch ex As Exception
    '            Dim returnUrl As String = "~/ExceptionPage.aspx"
    '            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '            Server.Transfer(returnUrl)
    '        End Try

    '    End Sub
    '#End Region

    'Protected Sub gvDirectDispatch_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDirectDispatch.PageIndexChanging
    '    Try
    '        gvDirectDispatch.PageIndex = e.NewPageIndex
    '        BindDirectDispatch()
    '    Catch ex As Exception
    '        Dim returnUrl As String = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
    '        Response.Redirect(returnUrl)
    '    End Try
    'End Sub


    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        BindFinalServiceability()
        ' BindDirectDispatch()
    End Sub



    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        ddlquartor.SelectedValue = ""
    End Sub
End Class
