Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Partial Class RawMaterialVendorMstrList
    Inherits System.Web.UI.Page

    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            PopulateVendor()
            BindData()
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Home.aspx")
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        ddlVendor.SelectedIndex = 0
        BindData()
    End Sub

    Protected Sub ImgbtnAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/RawMaterialVendorMstr.aspx")
    End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs)
        BindData()
    End Sub


    Protected Sub gvRawMatVendorDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "EditVendor" Then
                Dim row As GridViewRow = Nothing
                Dim clickedControl As Control = TryCast(e.CommandSource, Control)
                If Not clickedControl Is Nothing Then
                    row = TryCast(clickedControl.NamingContainer, GridViewRow)
                End If

                If row Is Nothing Then
                    Dim rowIndex As Integer = 0
                    If Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) AndAlso rowIndex >= 0 AndAlso rowIndex < gvRawMatVendorDetails.Rows.Count Then
                        row = gvRawMatVendorDetails.Rows(rowIndex)
                    End If
                End If

                If row Is Nothing Then
                    Throw New Exception("Unable to determine selected row.")
                End If

                Dim vendorCodeLabel As Label = CType(row.FindControl("lblVendorCode"), Label)
                Dim redirectUrl = "~/RawMaterialVendorMstr.aspx?" & Constant.SessionKeys.UnitCode & "=" & Server.UrlEncode(vendorCodeLabel.Text)
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

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Private Sub BindData()
        Try
            Dim ds As DataSet
            Dim obj As New OPC_VendorClass()
            ds = obj.GetRawMaterialVendorMasterList(ddlVendor.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    gvRawMatVendorDetails.DataSource = ds.Tables(0)
                    gvRawMatVendorDetails.DataBind()
                Else
                    gvRawMatVendorDetails.DataSource = Nothing
                    gvRawMatVendorDetails.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Sub PopulateVendor()
        Dim obj As New OPC_VendorClass()
        Dim ds As New DataSet()
        ds = obj.GetRawMaterialVendorList()

        ddlVendor.Items.Clear()
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlVendor.DataSource = ds.Tables(0)
            ddlVendor.DataTextField = "vendor_name"
            ddlVendor.DataValueField = "vendor_code"
            ddlVendor.DataBind()
        End If
        ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub
End Class
