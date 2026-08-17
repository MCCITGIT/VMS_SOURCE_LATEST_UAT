Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Collections.Generic
Imports System.Text

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
        btnApprove.OnClientClick = "return validateRawMaterialRequisitionApprove();"
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
        lblErrorMessage.Text = ""
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

    Protected Sub gvRequisition_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            Dim lblApprovalStatus As Label = CType(e.Row.FindControl("lblApprovalStatus"), Label)
            If chkSelect IsNot Nothing AndAlso lblApprovalStatus IsNot Nothing Then
                If String.Equals(lblApprovalStatus.Text.Trim(), "Approved", StringComparison.OrdinalIgnoreCase) Then
                    chkSelect.Enabled = False
                End If
            End If
        End If
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            lblErrorMessage.Text = String.Empty

            Dim dtApprove As New DataTable()
            dtApprove.Columns.Add("request_id", GetType(Integer))
            dtApprove.Columns.Add("approval_status", GetType(String))

            Dim mailRows As New List(Of Dictionary(Of String, String))()

            For Each row As GridViewRow In gvRequisition.Rows
                Dim chkSelect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                Dim hdnRequestId As HiddenField = CType(row.FindControl("hdnRequestId"), HiddenField)

                If chkSelect IsNot Nothing AndAlso chkSelect.Enabled AndAlso chkSelect.Checked Then
                    Dim requestId As Integer
                    If hdnRequestId IsNot Nothing AndAlso Integer.TryParse(hdnRequestId.Value, requestId) Then
                        dtApprove.Rows.Add(requestId, "A")

                        Dim hdnrmVendorcode As HiddenField = CType(row.FindControl("hdnrmVendorcode"), HiddenField)
                        Dim hdnrmVendoremail As HiddenField = CType(row.FindControl("hdnrmVendoremail"), HiddenField)
                        Dim lblVendorName As Label = CType(row.FindControl("lblVendorName"), Label)
                        Dim lblRawMatVendorName As Label = CType(row.FindControl("lblRawMatVendorName"), Label)
                        Dim lblRawmaterialList As Label = CType(row.FindControl("lblRawmaterialList"), Label)
                        Dim hdnccemail As HiddenField = CType(row.FindControl("hdnccemail"), HiddenField)

                        Dim mailInfo As New Dictionary(Of String, String)()
                        mailInfo("request_id") = requestId.ToString()
                        mailInfo("rm_vendor_code") = If(hdnrmVendorcode IsNot Nothing, Convert.ToString(hdnrmVendorcode.Value).Trim(), String.Empty)
                        mailInfo("rm_vendor_email") = If(hdnrmVendoremail IsNot Nothing, Convert.ToString(hdnrmVendoremail.Value).Trim(), String.Empty)
                        mailInfo("vendor_name") = If(lblVendorName IsNot Nothing, Convert.ToString(lblVendorName.Text).Trim(), String.Empty)
                        mailInfo("rm_vendor_name") = If(lblRawMatVendorName IsNot Nothing, Convert.ToString(lblRawMatVendorName.Text).Trim(), String.Empty)
                        mailInfo("raw_material_list") = If(lblRawmaterialList IsNot Nothing, Convert.ToString(lblRawmaterialList.Text).Trim(), String.Empty)
                        mailInfo("hdnccemail") = If(hdnccemail IsNot Nothing, Convert.ToString(hdnccemail.Value).Trim(), String.Empty)
                        mailRows.Add(mailInfo)
                    End If
                End If
            Next

            If dtApprove.Rows.Count = 0 Then
                lblErrorMessage.ForeColor = Drawing.Color.Red
                lblErrorMessage.Text = "Please select at least one pending requisition to approve."
                Return
            End If

            Dim obj As New OPC_VendorClass()
            Dim approvedCount As Integer = obj.ApproveRawMaterialRequest(userInfo.userIDEntity, dtApprove)

            If approvedCount > 0 Then
                Dim mailSentCount As Integer = 0
                Dim mailFailedCount As Integer = 0
                Dim mailMissingCount As Integer = 0

                For Each mailInfo As Dictionary(Of String, String) In mailRows
                    Dim mailResult As String = SendRawMaterialApprovalMail(mailInfo)
                    If mailResult = "Email Sent Successfully" Then
                        mailSentCount += 1
                    ElseIf mailResult = "Mail ID not found" Then
                        mailMissingCount += 1
                    Else
                        mailFailedCount += 1
                    End If
                Next

                lblErrorMessage.ForeColor = Drawing.Color.Green
                lblErrorMessage.Text = approvedCount.ToString() & " requisition(s) approved successfully."
                If mailSentCount > 0 OrElse mailFailedCount > 0 OrElse mailMissingCount > 0 Then
                    lblErrorMessage.Text &= " Mail sent: " & mailSentCount.ToString() & "."
                    If mailMissingCount > 0 Then
                        lblErrorMessage.Text &= " Mail ID not found: " & mailMissingCount.ToString() & "."
                    End If
                    If mailFailedCount > 0 Then
                        lblErrorMessage.Text &= " Mail failed: " & mailFailedCount.ToString() & "."
                    End If
                End If
                BindData()
            Else
                lblErrorMessage.ForeColor = Drawing.Color.Red
                lblErrorMessage.Text = "Unable to approve the selected requisition(s)."
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Private Function BuildDispatchListUrl(ByVal rmVendorCode As String) As String
        Dim redirectUrl As String = "Dispatch_List.aspx?rmvendor_code=" & Server.UrlEncode(rmVendorCode)
        Dim baseUrl As String = "https://bpilweb.bergerindia.com/vms/"
        Return New Uri(New Uri(baseUrl), redirectUrl).ToString()
    End Function

    Private Function SendRawMaterialApprovalMail(ByVal mailInfo As Dictionary(Of String, String)) As String
        Try
            Dim toAddress As String = String.Empty
            If mailInfo IsNot Nothing AndAlso mailInfo.ContainsKey("rm_vendor_email") Then
                toAddress = Convert.ToString(mailInfo("rm_vendor_email")).Trim()
            End If
            Dim ccAddress As String = String.Empty
            If mailInfo IsNot Nothing AndAlso mailInfo.ContainsKey("hdnccemail") Then
                ccAddress = Convert.ToString(mailInfo("hdnccemail")).Trim()
            End If

            If String.IsNullOrWhiteSpace(toAddress) Then
                Return "Mail ID not found"
            End If

            Dim requestId As String = If(mailInfo.ContainsKey("request_id"), Convert.ToString(mailInfo("request_id")), String.Empty)
            Dim rmVendorCode As String = If(mailInfo.ContainsKey("rm_vendor_code"), Convert.ToString(mailInfo("rm_vendor_code")), String.Empty)
            Dim vendorName As String = If(mailInfo.ContainsKey("vendor_name"), Convert.ToString(mailInfo("vendor_name")), String.Empty)
            Dim rmVendorName As String = If(mailInfo.ContainsKey("rm_vendor_name"), Convert.ToString(mailInfo("rm_vendor_name")), String.Empty)
            Dim rawMaterialList As String = If(mailInfo.ContainsKey("raw_material_list"), Convert.ToString(mailInfo("raw_material_list")), String.Empty)

            If String.IsNullOrWhiteSpace(rmVendorCode) Then
                Return "Mail ID not found"
            End If

            Dim dispatchUrl As String = BuildDispatchListUrl(rmVendorCode)

            Dim mailobj As New EmailSMSsender()
            Dim mailEntity As New MailEntity()
            Dim mailBody As New StringBuilder()

            mailEntity.ToAddress = toAddress
            mailEntity.CCAddress = ccAddress
            mailEntity.MailSubject = "Raw Material Requisition Approved - " & requestId

            mailBody.Append("<div style='font-family:Arial;font-size:13px;'>")
            mailBody.Append("<p>Dear Sir/Madam,</p>")
            mailBody.Append("<p>A raw material requisition has been approved. Please find the details below.</p>")
            mailBody.Append("<h3 style='color:#009933;'>Raw Material Requisition Details</h3>")

            mailBody.Append("<table style='border-collapse:collapse;width:100%;'>")
            mailBody.Append("<tr style='background:#009933;color:#fff;'>")
            mailBody.Append("<th style='border:1px solid #ccc;padding:8px;'>Request ID</th>")
            mailBody.Append("<th style='border:1px solid #ccc;padding:8px;'>Vendor Name</th>")
            'mailBody.Append("<th style='border:1px solid #ccc;padding:8px;'>RM Vendor Code</th>")
            mailBody.Append("<th style='border:1px solid #ccc;padding:8px;'>RM Vendor Name</th>")
            mailBody.Append("<th style='border:1px solid #ccc;padding:8px;'>Raw Material List</th>")
            mailBody.Append("<th style='border:1px solid #ccc;padding:8px;'>Status</th>")
            mailBody.Append("</tr>")

            mailBody.Append("<tr>")
            mailBody.Append("<td style='border:1px solid #ccc;padding:6px;text-align:center;'>" & Server.HtmlEncode(requestId) & "</td>")
            mailBody.Append("<td style='border:1px solid #ccc;padding:6px;'>" & Server.HtmlEncode(vendorName) & "</td>")
            'mailBody.Append("<td style='border:1px solid #ccc;padding:6px;text-align:center;'>" & Server.HtmlEncode(rmVendorCode) & "</td>")
            mailBody.Append("<td style='border:1px solid #ccc;padding:6px;'>" & Server.HtmlEncode(rmVendorName) & "</td>")
            mailBody.Append("<td style='border:1px solid #ccc;padding:6px;'>" & Server.HtmlEncode(rawMaterialList) & "</td>")
            mailBody.Append("<td style='border:1px solid #ccc;padding:6px;text-align:center;'>Approved</td>")
            mailBody.Append("</tr>")
            mailBody.Append("</table>")

            If Not String.IsNullOrWhiteSpace(dispatchUrl) Then
                mailBody.Append("<br/><br/>")
                mailBody.Append("<div style='text-align:center;'>")
                mailBody.Append("<a href='" & dispatchUrl & "' target='_blank' ")
                mailBody.Append("style='background:#007BFF;color:#FFFFFF;padding:10px 20px;")
                mailBody.Append("text-decoration:none;border-radius:5px;font-weight:bold;'>")
                mailBody.Append("View Dispatch List")
                mailBody.Append("</a>")
                mailBody.Append("</div>")
            End If

            mailBody.Append("<br/><hr/>")
            mailBody.Append("<div style='font-size:12px;color:#666;'>")
            mailBody.Append("<b>Note:</b> This is a system generated email. Please do not reply.")
            mailBody.Append("</div>")
            mailBody.Append("</div>")

            mailEntity.MailBody = mailBody.ToString()
            mailEntity.Sender_Task = "RawMaterialRequisition_ApprovalMail"

            If mailobj.sendMail(mailEntity) <= 0 Then
                Return "Email Sent Failed"
            End If

            Return "Email Sent Successfully"
        Catch ex As Exception
            Return "Email Sent Failed"
        End Try
    End Function

    Private Sub BindData()
        Try
            Dim obj As New OPC_VendorClass()
            If ddlApprovalstatus.SelectedValue = "A" Then
                btnApprove.Visible = False
            Else
                btnApprove.Visible = True
            End If
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
