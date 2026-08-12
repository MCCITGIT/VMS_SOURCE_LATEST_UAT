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
Partial Class TokenVendorRequisitionAddUpdate
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
        AddAttributes()
        If Not IsPostBack Then

            If Not (String.IsNullOrEmpty(Request.QueryString("id"))) Then

            End If

            PopulateUnit()
            PopulateUnitApplicableSite()
            PopulateTokenVendor(ddlTokenVendor)
            gvRequisitionItemsList.PageIndex = 0
            BindGrid()
            If (userInfo.userGroupCodeEntity.Equals("UNIT")) Then
                ddlVendorUnit.Enabled = False
            Else
                ddlVendorUnit.Enabled = True
            End If
        End If

    End Sub

#End Region

#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        btnSubmit.OnClientClick = "return ValidateSubmit();"
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

#Region "Populate Site"
    Private Sub PopulateUnitApplicableSite()
        CheckLogin()
        Try
            Dim obj As New UnitApplicableProductAssignClass
            Dim dsUnitSet As New DataSet
            dsUnitSet = obj.GetUnitApplicableSites(ddlVendorUnit.SelectedValue, Constant.Common.ActiveStatus)
            ddlSite.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlSite.DataSource = dsUnitSet.Tables(0)
                ddlSite.DataTextField = "utas_site_name"
                ddlSite.DataValueField = "utas_site_code"
                ddlSite.DataBind()
                If (dsUnitSet.Tables(0).Rows.Count <> 1) Then
                    ddlSite.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
                End If
            Else
                ddlSite.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty))
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Try
            Dim obj As New TokenVendorRequisitionClass
            Dim dsProductSet As New DataSet
            If (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                dsProductSet = obj.GetProductList(ddlVendorUnit.SelectedValue, String.Empty, String.Empty, ddlTokenVendor.SelectedValue)
                lblReqId.ForeColor = Drawing.Color.Red
                btnSubmit.Style.Remove("display")
                ddlVendorUnit.Attributes.Remove("disabled")
            Else
                dsProductSet = obj.GetRequisitionItemsListByid(Convert.ToInt32(Request.QueryString("id")))
                txtDesc.Text = dsProductSet.Tables(0).Rows(0)("trh_desc").ToString()
                txtDesc.Enabled = False
                ddlTokenVendor.SelectedItem.Selected = False
                ddlTokenVendor.SelectedValue = dsProductSet.Tables(0).Rows(0)("tokenVendor").ToString()
                ddlTokenVendor.Enabled = False
                ddlSite.Enabled = False
                lblReqId.ForeColor = Drawing.Color.Black
                lblReqId.Text = dsProductSet.Tables(0).Rows(0)("trd_requisition_id").ToString()
                btnSubmit.Style.Add("display", "none")
                ddlVendorUnit.Attributes.Add("disabled", "true")
            End If

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0 AndAlso Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                gvRequisitionItemsList.DataSource = dsProductSet.Tables(0)
                gvRequisitionItemsList.DataBind()

            Else
                gvRequisitionItemsList.DataSource = Nothing
                gvRequisitionItemsList.DataBind()
                btnSubmit.Style.Add("display", "none")

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvRequisitionItemsList.PageIndexChanging
        gvRequisitionItemsList.PageIndex = e.NewPageIndex
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

            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region
    Protected Sub gvProductList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRequisitionItemsList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Dim ddl As DropDownList = CType(e.Row.FindControl("ddlTokenVendor"), DropDownList)
            Dim txtQty As TextBox = CType(e.Row.FindControl("txtQty"), TextBox)
            txtQty.Attributes.Add("onblur", "return validateQty('" & txtQty.ClientID & "');")
            If Not (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                txtQty.Enabled = False
            End If
            'Dim hdnStatus As HiddenField = CType(e.Row.FindControl("hdnActive"), HiddenField)
            'If (hdnStatus.Value.Equals("Y")) Then
            '    e.Row.Style.Add("background-color", "#bdffb5")
            'End If
            'Dim btn As ImageButton = CType(e.Row.FindControl("imgBtnSubmit"), ImageButton)
            'btn.OnClientClick = "return ValidateTokenVendorAssign('" & ddl.ClientID & "','" & btn.ClientID & "');"
        End If
    End Sub
    Protected Sub gvTokenVendorList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvRequisitionItemsList.RowCommand

    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        CheckLogin()
        lblErrorMessage.Text = ""
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New TokenVendorRequisitionClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False
        Try

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            Dim dt As New DataTable
            dt.Columns.Add("trd_id", GetType(Integer))
            dt.Columns.Add("trd_requisition_id", GetType(Integer))
            dt.Columns.Add("trd_sku", GetType(String))
            dt.Columns.Add("trd_qty", GetType(Integer))
            If (gvRequisitionItemsList.Rows.Count > 0) Then
                For Each gvrow As GridViewRow In gvRequisitionItemsList.Rows
                    Dim hdnUnit As HiddenField = CType(gvrow.FindControl("hdnUnit"), HiddenField)
                    Dim hdnProductId As HiddenField = CType(gvrow.FindControl("hdnProductId"), HiddenField)
                    Dim hdnPackSize As HiddenField = CType(gvrow.FindControl("hdnPackSize"), HiddenField)
                    'Dim hdnActive As HiddenField = CType(gvrow.FindControl("hdnActive"), HiddenField)
                    Dim hdnTokenVendor As HiddenField = CType(gvrow.FindControl("hdnTokenVendor"), HiddenField)
                    Dim txtQty As TextBox = CType(gvrow.FindControl("txtQty"), TextBox)
                    If Not (txtQty.Text.Equals(String.Empty)) Then
                        Dim dr As DataRow = dt.NewRow()
                        dr("trd_id") = gvrow.RowIndex + 1
                        dr("trd_requisition_id") = 0
                        dr("trd_sku") = hdnProductId.Value
                        dr("trd_qty") = txtQty.Text
                        dt.Rows.Add(dr)

                    End If

                Next
                dt.AcceptChanges()
                If (dt.Rows.Count > 0) Then
                    RecordInserted = obj.TokenRequisitionInsertUpdate(0, txtDesc.Text, ddlVendorUnit.SelectedValue, ddlSite.SelectedValue, userInfo.userIDEntity, Constant.Common.ActiveStatus, ddlTokenVendor.SelectedValue, dt, Constant.Common.Token_Req_Status_New, sqlConn, sqlTrans)
                    If (RecordInserted > 0) Then
                        sqlTrans.Commit()
                        SendMail(RecordInserted)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location.href='TokenRequisitionList.aspx';", True)
                        'SendMail(RecordInserted.ToString(), txtDesc.Text, ddlVendorUnit.SelectedValue.ToString(), ddlSite.SelectedValue.ToString(), ddlTokenVendor.SelectedValue)

                    Else
                        sqlTrans.Rollback()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                    End If
                Else
                    lblErrorMessage.Text = "Please fill at least one quantity."
                End If

            Else
                lblErrorMessage.Text = "No data available"
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
    Protected Sub ddlTokenVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTokenVendor.SelectedIndexChanged
        BindGrid()
    End Sub
    Protected Sub txtQty_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim multiplier As Decimal = GetStandardParameter("TokenMultiplier")
            Dim txt As TextBox = TryCast(sender, TextBox)
            Dim qty As Integer = 0
            If (multiplier > 0) Then

                If (Integer.TryParse(txt.Text, qty)) Then
                    If Not ((qty Mod multiplier) = 0) Then
                        txt.Text = String.Empty
                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "window.alert('" & "Despatch Quantity has be multiple of " & Convert.ToInt32(multiplier).ToString & "." & "');", True)
                    Else

                        'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "document.getElementById('" & lblErrorMessage.ClientID & "').innerText='';", True)
                    End If
                Else
                    txt.Text = String.Empty
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "document.getElementById('" & lblErrorMessage.ClientID & "').innerText='Invalid Quantity.';", True)

                End If

            Else
                txt.Text = String.Empty
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Notify", "document.getElementById('" & lblErrorMessage.ClientID & "').innerText='Invalid Multiplier.';", True)

            End If
        Catch ex As Exception
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally

        End Try

    End Sub
#Region "Get values for a particular Standard Parameter."

    Private Function GetStandardParameter(ByVal param_name As String) As Decimal

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnStandardParameter As New Common()
        Dim dsStandardParameter As DataSet

        Dim result As Decimal = 0

        Try

            dsStandardParameter = cmnStandardParameter.GetStandardParameterValues(param_name)

            If Not (dsStandardParameter Is Nothing) Then

                If Not (dsStandardParameter.Tables(0).Rows.Count = 0) Then
                    result = Decimal.Parse(dsStandardParameter.Tables(0).Rows(0)("param_decimal_value").ToString)
                Else
                    Dim returnUrl As String = "~/ExceptionPage.aspx"
                    Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    Server.Transfer(returnUrl)
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

        Return result

    End Function

#End Region
    Protected Sub gvRequisitionItemsList_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvRequisitionItemsList.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim txtDespatchQty As TextBox = CType(e.Row.FindControl("txtQty"), TextBox)
            AddHandler txtDespatchQty.TextChanged, AddressOf txtQty_TextChanged

        End If
    End Sub
    Protected Sub ddlVendorUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendorUnit.SelectedIndexChanged
        PopulateUnitApplicableSite()
        BindGrid()
    End Sub

    Private Sub SendMail(ByVal requisitionId As Integer)
        Dim ds As DataSet
        Dim mstr As New TokenVendorRequisitionClass

        ds = mstr.Get_Mail_Details(requisitionId)

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            Try
                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim mailobj As New EmailSMSsender
                Dim mailEntity As New MailEntity
                mailEntity.ToAddress = ds.Tables(0).Rows(0)("toId").ToString
                'mailEntity.CCAddress = ds.Tables(0).Rows(0)("ccID").ToString
                mailEntity.BCCAddress = "automailer@mccit.co.in"
                mailEntity.MailSubject = "NEW TOKEN REQUISITION FROM - " & ds.Tables(1).Rows(0)("trh_unit").ToString

                Dim MailBody As String = String.Empty

                '-----------------------------------------------------------------------------------------------
                Dim bodyBuilder As StringBuilder = New StringBuilder()

                bodyBuilder.Append("<div>Please find below the token requisition details.</div>")
                bodyBuilder.Append("<table cellpadding='2' style='text-align: Left; width:95%; border: 1px solid black;'>")

                Dim rowcount As Integer = 0

                For Each dr As DataRow In ds.Tables(1).Rows
                    rowcount += 1
                    Dim styleStr As String

                    If rowcount Mod 2 = 0 Then
                        styleStr = " background-color: #CDF7F2;"
                    Else
                        styleStr = ""
                    End If

                    'bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;'>")
                    'bodyBuilder.Append("<td style='width:25%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>#</td>")
                    'bodyBuilder.Append("<td>" & rowcount.ToString() & "</td>")
                    'bodyBuilder.Append("</tr>")
                    bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
                    bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Requisition ID:-</td>")
                    bodyBuilder.Append("<td>" & dr("trh_id").ToString() & "</td>")
                    bodyBuilder.Append("</tr>")
                    bodyBuilder.Append("</br>")
                    bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
                    bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Unit:-</td>")
                    bodyBuilder.Append("<td>" & dr("trh_unit").ToString() & "</td>")
                    bodyBuilder.Append("</tr>")
                    bodyBuilder.Append("</br>")
                    bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
                    bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Site Name:-</td>")
                    bodyBuilder.Append("<td>" & dr("trh_site").ToString() & "</td>")
                    bodyBuilder.Append("</tr>")
                    bodyBuilder.Append("</br>")
                    'bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
                    'bodyBuilder.Append("<td style='width:50%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;text-align:right;padding:5px;'>Token vender:-</td>")
                    'bodyBuilder.Append("<td>" & dr("trh_token_vendor").ToString() & "</td>")
                    'bodyBuilder.Append("</tr>")
                    bodyBuilder.Append("</br>")
                    'bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;  '>")
                    'bodyBuilder.Append("<td style='width:25%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>Description:-</td>")
                    'bodyBuilder.Append("<td>" & dr("trh_desc").ToString() & "</td>")
                    'bodyBuilder.Append("</tr>")
                    'bodyBuilder.Append("</br>")
                    'bodyBuilder.Append("<tr style='font-size: small;font-family: Arial; '>")
                    'bodyBuilder.Append("<td style='width:25%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>Description:-</td>")
                    'bodyBuilder.Append("<td>" & dr("cmp_description").ToString() & "</td>")
                    'bodyBuilder.Append("</tr>")
                    'bodyBuilder.Append("<tr>")
                    'bodyBuilder.Append("<td style='width:25%;background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>Description:-</td>")
                    'bodyBuilder.Append("<td>" & dr("cmp_description").ToString() & "</td>")
                    bodyBuilder.Append("</tr>")

                Next

                'bodyBuilder.Append("<tr style=' background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>")
                'bodyBuilder.Append("<td colspan='4'>This is a System Generated Mail. Please Do not Reply.</td>")
                'bodyBuilder.Append("</tr>")
                bodyBuilder.Append("<tr>")
                bodyBuilder.Append("<td colspan='4'>")
                bodyBuilder.Append("<table cellpadding='8' style='text-align: Left; width:100%;border:1px solid grey;'>")
                bodyBuilder.Append("<tr style=' background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;'>")
                'bodyBuilder.Append("<td style='width:10%;'>#</td>")
                bodyBuilder.Append("<td style='width:15%;border:1px solid grey;text-align:center;'>Product</td>")
                bodyBuilder.Append("<td style='width:35%;border:1px solid grey;text-align:center;'> Name</td>")
                bodyBuilder.Append("<td style='width:15%;border:1px solid grey;text-align:center;'>Pack Size</td>")
                bodyBuilder.Append("<td style='width:15%;border:1px solid grey;text-align:center;'>Denomination</td>")
                bodyBuilder.Append("<td style='width:8%;border:1px solid grey;text-align:center;'>Requisition Qty</td>")
                bodyBuilder.Append("</tr>")
                Dim rowcount1 As Integer = 0
                For Each dr As DataRow In ds.Tables(2).Rows
                    rowcount1 += 1
                    Dim styleStr As String

                    If rowcount Mod 2 = 0 Then
                        styleStr = " background-color: #CDF7F2;"
                    Else
                        styleStr = ""
                    End If

                    bodyBuilder.Append("<tr style='font-size: small;font-family: Arial;'>")
                    bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("sku_new_code").ToString() & "</td>")
                    bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("sku_desc").ToString() & "</td>")
                    bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("sku_volume").ToString() & "</td>")
                    bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("uap_denomination").ToString() & "</td>")
                    bodyBuilder.Append("<td style='border:1px solid grey;text-align:center;'>" & dr("qty").ToString() & "</td>")
                    bodyBuilder.Append("</tr>")
                Next
                bodyBuilder.Append("<tr style=' background-color: #99CCFF; font-size: small;font-family: Arial; font-weight: bold;align:left;'>")
                bodyBuilder.Append("<td colspan='5' style='text-align:center;'>This is a System Generated Mail. Please Do not Reply.</td>")
                bodyBuilder.Append("</tr>")
                bodyBuilder.Append("</table>")
                bodyBuilder.Append("</td>")
                bodyBuilder.Append("</tr>")
                bodyBuilder.Append("</table>")
                'mailobj.mailBody = bodyBuilder.ToString()

                mailEntity.MailBody = bodyBuilder.ToString()
                '------------------------------------------------------------------------------------------------
                'Dim recipNo As String = mailobj.sendMailHTML(mailEntity)
                mailEntity.Sender_Task = "SendMail_TokenVendorRequisition"
                mailobj.sendMail(mailEntity)
                'Next
            Catch ex As Exception
                Throw ex
            End Try

        End If


    End Sub

End Class
