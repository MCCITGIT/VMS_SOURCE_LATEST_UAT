Imports System.Data
Imports VMS.Web

Partial Class BulkRawMaterialReceiptList
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
            PopulateStatus()
            BindData()
        End If
    End Sub

    Private Sub PopulateStatus()
        'ddlStatus.Items.Clear()
        'ddlStatus.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        ddlStatus.Items.Insert(0, New ListItem("Pending", "P"))
        ddlStatus.Items.Insert(1, New ListItem("Received", "R"))
    End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs)
        lblErrorMessage.Text = String.Empty
        BindData()
    End Sub

    Protected Sub gvReceipt_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "ViewReceipt" Then
                Dim row As GridViewRow = Nothing
                Dim clickedControl As Control = TryCast(e.CommandSource, Control)
                If Not clickedControl Is Nothing Then
                    row = TryCast(clickedControl.NamingContainer, GridViewRow)
                End If

                If row Is Nothing Then
                    Dim rowIndex As Integer = 0
                    If Integer.TryParse(Convert.ToString(e.CommandArgument), rowIndex) AndAlso rowIndex >= 0 AndAlso rowIndex < gvReceipt.Rows.Count Then
                        row = gvReceipt.Rows(rowIndex)
                    End If
                End If

                If row Is Nothing Then
                    Throw New Exception("Unable to determine selected row.")
                End If

                Dim hdnDespatchId As HiddenField = CType(row.FindControl("hdnDespatchId"), HiddenField)
                Dim redirectUrl = "~/BulkRawMaterialReceiptDtls.aspx?despatch_id=" & Server.UrlEncode(hdnDespatchId.Value)
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
            Dim ds As DataSet = obj.GetRawMaterialReceiptList(ddlStatus.SelectedValue)

            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                gvReceipt.DataSource = ds.Tables(0)
                gvReceipt.DataBind()
            Else
                gvReceipt.DataSource = Nothing
                gvReceipt.DataBind()
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub
End Class
