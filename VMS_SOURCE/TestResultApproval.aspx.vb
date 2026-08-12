Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class TestResultApproval
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
            'AddAttributes()
            'PopulateFrequency()
            'PopulateResultType()

            If Not (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                Dim id As String = Request.QueryString("id")
                gvTestList.PageIndex = 0
                BindGrid(id)
            End If

        End If
        'BindGrid(ID)
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
        btnReject.Attributes.Add("onclick", "return TestresultApproval();")
    End Sub
#End Region

#Region "Bind Grid"
    Private Sub BindGrid(ByVal id As String)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsProductSet As New DataSet
            Dim RequisitionId As Integer = 0
            dsProductSet = obj.GetTestResultApprovalList(id)
            btnApprove.Visible = False
            btnReject.Visible = False
            txtRemarks.Visible = False
            txtVendor.Text = dsProductSet.Tables(0).Rows(0)("vendor_name")
            txtBrand.Text = dsProductSet.Tables(0).Rows(0)("brand_name")
            txtProduct.Text = dsProductSet.Tables(0).Rows(0)("prd_desc")
            txtShade.Text = dsProductSet.Tables(0).Rows(0)("shade")
            txtBatchNo.Text = dsProductSet.Tables(0).Rows(0)("batch_no")
            txtBdate.Text = dsProductSet.Tables(0).Rows(0)("batch_date")

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvTestList.DataSource = dsProductSet.Tables(1)
                gvTestList.DataBind()
                txtRemarks.Text = dsProductSet.Tables(0).Rows(0)("remarks").ToString
                If dsProductSet.Tables(0).Rows(0)("qulify_status") = "Pending" Or dsProductSet.Tables(0).Rows(0)("qulify_status") = "Rejected" Then
                    btnApprove.Enabled = True
                    'txtRemarks.Enabled = False
                Else
                    btnApprove.Enabled = False
                    'txtRemarks.Enabled = False
                End If
            Else
                gvTestList.DataSource = Nothing
                gvTestList.DataBind()
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region


#Region "gvTestList Event Handelling"
    Protected Sub gvTestList_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvTestList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            If rowView("status") <> "Yes" Then
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
                e.Row.Cells(4).Font.Bold = True
                'btnSubmit.Enabled = False
            Else
                e.Row.Cells(4).ForeColor = Drawing.Color.Green
                e.Row.Cells(4).Font.Bold = True
            End If
        End If
    End Sub
#End Region


#Region "Submit Click Event Handelling"
    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        CheckLogin()
        Dim Action As String = "Insert"
        ApproveTestResult(Action)
        Dim id As String = Request.QueryString("id")
        BindGrid(ID)
        'btnApprove.Enabled = False
    End Sub
#End Region

#Region "Reject Click Event Handelling"
    Protected Sub btnReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.Click
        CheckLogin()
        Dim Action As String = "Reject"
        ApproveTestResult(Action)
        Dim id As String = Request.QueryString("id")
        BindGrid(ID)
        'btnReject.Enabled = False
    End Sub
#End Region
#Region "Approve Test Result"
    Private Sub ApproveTestResult(ByVal Action As String)
        Dim hdr_id As String = Request.QueryString("id")
        Dim numRowsAffected As Integer
        Dim obj As New QualityControlClass
        Dim RowIndex As Integer = 0

        Try
            Dim status As String = "Y"
            Dim Msg As String
            Dim Remark As String
            If Action <> "Insert" Then
                status = "N"
                Remark = txtRemarks.Text
                Msg = "Test Result Rejected"
            Else
                status = "Y"
                Remark = ""
                Msg = "Test Result Approved"
            End If

            numRowsAffected = obj.TestResultApprovalandReject(hdr_id, Remark, status, userInfo.userIDEntity)
            If numRowsAffected > 0 Then
                lblErrorMessage.ForeColor = System.Drawing.Color.Green
                If True Then

                End If
                lblErrorMessage.Text = Msg
                btnApprove.Enabled = False
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Something went wrong. Try again."

            End If

        Catch ex As Exception
            Dim returnUrl As String = "ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Server.Transfer(returnUrl)
        End Try
    End Sub
#End Region

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Dim path = "~/TestCaseTestResultList.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub

End Class
