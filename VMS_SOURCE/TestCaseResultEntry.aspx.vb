Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports ClosedXML.Excel
Imports System.Data.OleDb

Partial Class TestCaseResultEntry
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
            PopulateVendor()
            'PopulateVendorBrand(String.Empty)
            'PopulateVendorBrandProduct(String.Empty, String.Empty)
            If Not (String.IsNullOrEmpty(Request.QueryString("id"))) Then
                hdnId.Value = Request.QueryString("id")
                If Val(hdnId.Value) > 0 Then
                    loadData(Val(hdnId.Value))
                    btnBack.PostBackUrl = "TestCaseTestResultList.aspx"
                End If
            Else
                btnBack.PostBackUrl = "Home.aspx"
            End If
        End If

    End Sub

    Public Sub loadData(ByVal Id As Int64)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsProductSet As New DataSet
            Dim RequisitionId As Integer = 0
            dsProductSet = obj.GetTestResultHdrById(Id)

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0) Then
                If (Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                    hdnId.Value = dsProductSet.Tables(0).Rows(0)("hdr_id")
                    ddlVendor.SelectedValue = dsProductSet.Tables(0).Rows(0)("vendor_id")
                    ddlVendor_SelectedIndexChanged(Nothing, Nothing)
                    ddlBrand.SelectedValue = dsProductSet.Tables(0).Rows(0)("brand_id")
                    ddlBrand_SelectedIndexChanged(Nothing, Nothing)
                    ddlProduct.SelectedValue = dsProductSet.Tables(0).Rows(0)("product_id")
                    txtShade.Text = dsProductSet.Tables(0).Rows(0)("shade")
                    txtBatchNo.Text = dsProductSet.Tables(0).Rows(0)("batch_no")
                    txtBatchDate.Text = dsProductSet.Tables(0).Rows(0)("batch_date")

                    ddlVendor.Enabled = False
                    ddlBrand.Enabled = False
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
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

#Region "Populate Dropdown"
    Private Sub PopulateVendor()
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet
            ddlVendor.Items.Clear()
            dsUnitSet = obj.GetVendor(userInfo.userIDEntity)
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlVendor.DataSource = dsUnitSet.Tables(0)
                ddlVendor.DataTextField = "vendor_name"
                ddlVendor.DataValueField = "vendor_code"
                ddlVendor.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
                ddlVendor_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
    Private Sub PopulateVendorBrand(ByVal vendorCode As String)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetVendorBrand(vendorCode, userInfo.userIDEntity)
            ddlBrand.Items.Clear()
            ddlProduct.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlBrand.DataSource = dsUnitSet.Tables(0)
                ddlBrand.DataTextField = "brand_name"
                ddlBrand.DataValueField = "brand_id"
                ddlBrand.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
                ddlBrand_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
    Private Sub PopulateVendorBrandProduct(ByVal vendorCode As String, ByVal brandId As String)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetVendorBrandProduct(vendorCode, brandId, userInfo.userIDEntity)
            ddlProduct.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlProduct.DataSource = dsUnitSet.Tables(0)
                ddlProduct.DataTextField = "prd_desc"
                ddlProduct.DataValueField = "prd_code"
                ddlProduct.DataBind()
                If Not (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try

    End Sub
#End Region

#Region "Bind Grid"
    Private Sub bindGrid()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim LovDetailsGet As New QualityControlClass
        Dim LovDetailsList As DataSet
        Dim lovtype As String
        lovtype = ddlBrand.SelectedValue

        LovDetailsList = LovDetailsGet.GetBrandTestDtlsList(Val(ddlBrand.SelectedValue), userInfo.userCompanyEntity, Val(hdnId.Value), ddlProduct.SelectedValue)
        If (Not (LovDetailsList Is Nothing) AndAlso LovDetailsList.Tables.Count > 0) Then
            If (Not (LovDetailsList.Tables(0) Is Nothing) AndAlso LovDetailsList.Tables(0).Rows.Count > 0) Then
                gvTestList.DataSource = LovDetailsList
                gvTestList.DataBind()
            Else
                gvTestList.DataSource = Nothing
                gvTestList.DataBind()
            End If
        End If
    End Sub
#End Region

    Protected Sub ddlBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBrand.SelectedIndexChanged
        PopulateVendorBrandProduct(ddlVendor.SelectedValue, ddlBrand.SelectedValue)

        If ddlProduct.Items.Count = 1 Then
            bindGrid()
        End If
    End Sub

    Protected Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduct.SelectedIndexChanged
        bindGrid()
    End Sub
    Protected Sub gvProductList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTestList.PageIndexChanging
        gvTestList.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub
    Protected Sub gvTokenVendorList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTestList.RowCommand
        CheckLogin()
        If (e.CommandName.Equals("EditTest")) Then
            Response.Redirect("TestCaseTestMasterAddUpdate.aspx?id=" & e.CommandArgument.ToString)
        End If
    End Sub
    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
        PopulateVendorBrand(ddlVendor.SelectedValue)
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        If String.IsNullOrEmpty(ddlVendor.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor.');", True)
            Exit Sub
        End If
        If Val(ddlBrand.SelectedValue.ToString()) = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a brand.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlProduct.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid product');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtShade.Text.Trim()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid shade');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtBatchNo.Text.Trim()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid batch no.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtBatchDate.Text.Trim()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid batch date');", True)
            Exit Sub
        End If

        Dim batchDate As SqlDateTime = FormatDate(txtBatchDate.Text.Trim())
        Dim inputDate As Date = Date.Parse(batchDate)
        Dim minDate As Date = Date.Now.AddDays(-7)
        If minDate > batchDate Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Minimum date should be " & minDate.AddDays(1).ToString("dd/MM/yyyy") & ".');", True)
            Exit Sub
        End If


        Dim vendorId As String = ddlVendor.SelectedValue.ToString()
        Dim brandId As Int64 = Val(ddlBrand.SelectedValue.ToString())
        Dim productCode As String = ddlProduct.SelectedValue.ToString()
        Dim shadeCode As String = txtShade.Text.Trim()
        Dim batchNo As String = txtBatchNo.Text.Trim()
        'Dim batchDate As SqlDateTime = FormatDate(txtBatchDate.Text)

        Dim dt As New DataTable
        dt.Columns.Add("slno", GetType(Int32))
        dt.Columns.Add("test_id", GetType(Int64))
        dt.Columns.Add("result_value", GetType(String))
        For Each row As GridViewRow In gvTestList.Rows
            Dim slno As Label = CType(row.FindControl("lblSlno"), Label)
            Dim hdnTestId As HiddenField = CType(row.FindControl("hdnTestId"), HiddenField)
            Dim hdnTestType As HiddenField = CType(row.FindControl("hdnTestType"), HiddenField)
            Dim ddlResultValue As DropDownList = CType(row.FindControl("ddlResultValue"), DropDownList)
            Dim txtResultValue As TextBox = CType(row.FindControl("txtResultValue"), TextBox)
            Dim resultValue As String = String.Empty
            If hdnTestType.Value = "TT02" Then
                resultValue = ddlResultValue.SelectedValue.ToString()
            Else
                resultValue = txtResultValue.Text.ToString()
            End If
            If Val(hdnTestId.Value) > 0 Then
                Dim newRow As DataRow = dt.NewRow()
                newRow("slno") = Val(slno.Text)
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next
        If dt.Rows.Count > 0 Then
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            Dim obj As New QualityControlClass

            Dim RecordInserted As Integer
            Dim status As String = String.Empty
            Dim flag As Boolean = False
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                RecordInserted = obj.QCFormDataSubmit(vendorId, brandId, productCode, shadeCode, batchNo, batchDate, dt, String.Empty, userInfo.userIDEntity, hdnId.Value, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    sqlTrans.Commit()
                    If Val(hdnId.Value) > 0 Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updated successfully.');window.location.href='TestCaseTestResultList.aspx';", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted successfully.');window.location.href='TestCaseTestResultList.aspx';", True)
                        btnReset_Click(Nothing, Nothing)
                    End If
                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                End If
            Catch ex As Exception
                If (sqlTrans IsNot Nothing) Then
                    sqlTrans.Rollback()
                End If
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
                'Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
                'HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
            End Try

        End If


    End Sub
#Region "Date Format"
    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime

        If (stringdate.Equals(String.Empty)) Then
            Return SqlDateTime.MinValue
        End If

        If Not (stringdate = String.Empty) Then
            If stringdate.Contains("/") Then
                Dim ddate As String() = stringdate.Split("/")
                Dim arrlist As New ArrayList
                Dim index As Integer = 0

                While index <= ddate.Length - 1
                    arrlist.Add(ddate(index))
                    System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
                End While
                Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(0))
                Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
                Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(2))

                Dim dt As DateTime = New DateTime(yyyy, mm, dd)
                dt = FormatDateTime(dt, DateFormat.LongDate)
                Return dt
            ElseIf stringdate.Contains("-") Then
                Dim ddate As String() = stringdate.Split("-")
                Dim arrlist As New ArrayList
                Dim index As Integer = 0

                While index <= ddate.Length - 1
                    arrlist.Add(ddate(index))
                    System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
                End While
                Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(2))
                Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
                Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(0))

                Dim dt As DateTime = New DateTime(yyyy, mm, dd)
                dt = FormatDateTime(dt, DateFormat.LongDate)
                Return dt
            End If
        End If



    End Function
#End Region
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        hdnId.Value = String.Empty

        ddlVendor.Enabled = True
        ddlBrand.Enabled = True

        ddlVendor.SelectedIndex = 0
        ddlVendor_SelectedIndexChanged(Nothing, Nothing)
        ddlProduct.Items.Clear()
        txtShade.Text = ""
        txtBatchNo.Text = ""
        txtBatchDate.Text = ""
        gvTestList.DataSource = Nothing
        gvTestList.DataBind()

    End Sub


    Protected Sub gvTestList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTestList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)
            Dim rowIndex As Int32 = e.Row.RowIndex
            Dim ddlResultValue As DropDownList = CType(e.Row.FindControl("ddlResultValue"), DropDownList)
            Dim txtResultValue As TextBox = CType(e.Row.FindControl("txtResultValue"), TextBox)
            Dim hdnResultValue As HiddenField = CType(e.Row.FindControl("hdnResultValue"), HiddenField)

            ddlResultValue.Visible = False
            txtResultValue.Visible = False

            If rowView IsNot Nothing Then
                Dim row As DataRow = rowView.Row
                Dim test_type As String = row("test_type").ToString()
                If test_type = "TT01" Then
                    txtResultValue.Text = hdnResultValue.Value
                    txtResultValue.Attributes.Add("oninput", "return oninputDecimal(this);")
                    txtResultValue.Visible = True
                    txtResultValue.Style.Add("text-align", "right")
                ElseIf test_type = "TT02" Then
                    ddlResultValue.Visible = True
                    Dim refvalue As String = row("refvalue").ToString()
                    Dim Options As String() = refvalue.Split("/"c)
                    ddlResultValue.DataSource = Options
                    ddlResultValue.DataBind()
                    ddlResultValue.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                    ddlResultValue.SelectedValue = hdnResultValue.Value
                Else
                    txtResultValue.Visible = True
                    txtResultValue.Text = hdnResultValue.Value
                End If
            End If
        End If
    End Sub
End Class

