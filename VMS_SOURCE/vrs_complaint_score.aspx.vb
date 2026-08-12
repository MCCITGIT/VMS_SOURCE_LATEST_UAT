
Imports System.Data
Imports VMS.Web

Partial Class vrs_complaint_score
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
            PopulateQuarter()
            PopulateVendor()
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

#Region "Populate Dropdown"
    Private Sub PopulateQuarter()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New vrsComplaintClass()
        Dim ds As DataSet
        Try
            ds = obj.GetQuarterDetails(userInfo.userIDEntity)
            If Not (ds Is Nothing) Then
                If Not (ds.Tables(0).Rows.Count = 0) Then
                    ddlQuarter.DataSource = ds
                    ddlQuarter.DataTextField = "qm_quarter_short_code"
                    ddlQuarter.DataValueField = "qm_id"
                    ddlQuarter.DataBind()
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                Else
                    ddlQuarter.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
    Private Sub PopulateVendor()
        CheckLogin()
        Try
            Dim obj As New vrsComplaintClass
            Dim dsUnitSet As New DataSet
            ddlVendor.Items.Clear()
            dsUnitSet = obj.GetVendor(userInfo.userIDEntity)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendor.DataSource = dsUnitSet.Tables(0)
                ddlVendor.DataTextField = "vendor_name"
                ddlVendor.DataValueField = "vendor_code"
                ddlVendor.DataBind()
                ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                If dsUnitSet.Tables(0).Rows.Count = 1 Then
                    ddlVendor.SelectedIndex = 1
                    ddlVendor.Enabled = False
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        div2.Visible = True
        bindGrid()
    End Sub

#Region "Bind Grid"
    Private Sub bindGrid()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()

        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlVendor.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor.');", True)
            Exit Sub
        End If

        Dim obj As New vrsComplaintClass
        Dim ds As New DataSet
        ds = obj.GetComplaintDetails(ddlQuarter.SelectedValue, ddlVendor.SelectedValue)
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                gvComplaintDetails.DataSource = ds.Tables(0)
                gvComplaintDetails.DataBind()

                ddlQuarter.Enabled = False
                ddlVendor.Enabled = False
            Else
                gvComplaintDetails.DataSource = Nothing
                gvComplaintDetails.DataBind()
            End If
        End If
    End Sub
#End Region

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim path = "~/vrs_complaint_score.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx", True)
    End Sub

    Protected Sub gvComplaintDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "ViewDetails" Then
            Dim args As String() = e.CommandArgument.ToString().Split("|"c)

            If args.Length = 2 Then
                Dim vendorId As String = args(0)
                Dim quarterId As String = args(1)

                Dim obj As New vrsComplaintClass
                Dim ds As DataSet = obj.GetVendorComplaintDetails(vendorId, quarterId)

                gvVendorDetails.DataSource = ds.Tables(0)
                gvVendorDetails.DataBind()
                pnlVendorDetails.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnClosePopup_Click(sender As Object, e As EventArgs)
        pnlVendorDetails.Visible = False
    End Sub
End Class
