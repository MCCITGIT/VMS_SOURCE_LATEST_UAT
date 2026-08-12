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

Partial Class DeviationsFGQuality
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
            PopulateQuarter()
            PopulateVendor()
            btnSubmit.Visible = False
            btnConsubmit.Visible = False
            'btnApprove.Visible = False
            'btnReject.Visible = False
            ''ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            'PopulateVendorBrand(String.Empty)
            'PopulateVendorBrandProduct(String.Empty, String.Empty)
            If Not (String.IsNullOrEmpty(Request.QueryString("vendorid")) AndAlso String.IsNullOrEmpty(Request.QueryString("brandid")) AndAlso String.IsNullOrEmpty(Request.QueryString("quarter")) AndAlso String.IsNullOrEmpty(Request.QueryString("product")) AndAlso String.IsNullOrEmpty(Request.QueryString("skucode"))) Then
                Dim vendorId As String = Request.QueryString("vendorid")
                Dim brandID As String = Request.QueryString("brandid")
                Dim Quarter As String = Request.QueryString("quarter")
                Dim Productcode As String = Request.QueryString("product")
                Dim skucode As String = Request.QueryString("skucode")

                ddlQuarter.SelectedValue = Quarter
                ddlVendor.SelectedValue = vendorId
                ddlBrand.SelectedValue = brandID
                PopulateVendorBrandProduct(vendorId, brandID)
                ddlProduct.SelectedValue = Productcode
                PopulateProductWiseSku(vendorId, Productcode)
                ddlSku.SelectedValue = skucode
                bindGrid()

                gvTestList.PageIndex = 0
                'bindGrid(id)

            Else
                btnBack.PostBackUrl = "Home.aspx"
            End If
        End If

    End Sub

    Public Sub loadData(ByVal Id As Int64)
        CheckLogin()
        Try
            Dim obj As New DeviationsFGQualityClass
            Dim dsProductSet As New DataSet
            Dim RequisitionId As Integer = 0
            dsProductSet = obj.GetTestResultHdrById(Id)

            If (Not (dsProductSet Is Nothing) AndAlso dsProductSet.Tables.Count > 0) Then
                If (Not (dsProductSet.Tables(0) Is Nothing) AndAlso dsProductSet.Tables(0).Rows.Count > 0) Then
                    hdnId.Value = dsProductSet.Tables(0).Rows(0)("hdr_id")
                    ddlVendor.SelectedValue = dsProductSet.Tables(0).Rows(0)("vendor_id")
                    ddlVendor_SelectedIndexChanged(Nothing, Nothing)
                    ddlBrand.SelectedValue = dsProductSet.Tables(0).Rows(0)("brand_id")
                    'ddlBrand_SelectedIndexChanged(Nothing, Nothing)
                    'ddlProduct.SelectedValue = dsProductSet.Tables(0).Rows(0)("product_id")
                    'txtShade.Text = dsProductSet.Tables(0).Rows(0)("shade")
                    'txtBatchNo.Text = dsProductSet.Tables(0).Rows(0)("batch_no")
                    'txtBatchDate.Text = dsProductSet.Tables(0).Rows(0)("batch_date")

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
    Private Sub PopulateQuarter()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New VRSAuditClass()
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
                    If (ds.Tables(0).Rows.Count > 0) Then
                        For Each row As DataRow In ds.Tables(0).Rows
                            Dim currentquarter As String = row("qm_current_quarter").ToString()
                            'If currentquarter = "Y" Then
                            '    ddlQuarter.SelectedValue = row("qm_id").ToString()
                            '    ddlQuarter.Enabled = False
                            'End If
                        Next
                    End If
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
            Dim obj As New DeviationsFGQualityClass
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
            Dim obj As New DeviationsFGQualityClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetVendorBrand(vendorCode, userInfo.userIDEntity)
            ddlBrand.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlBrand.DataSource = dsUnitSet.Tables(0)
                ddlBrand.DataTextField = "brand_name"
                ddlBrand.DataValueField = "brand_id"
                ddlBrand.DataBind()
                ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            Else
                ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                'ddlBrand_SelectedIndexChanged(Nothing, Nothing)
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

    Private Sub PopulateProductWiseSku(ByVal vendorCode As String, ByVal ProductCode As String)
        CheckLogin()
        Try
            Dim obj As New QualityControlClass
            Dim dsUnitSet As New DataSet

            dsUnitSet = obj.GetProductWiseSku(vendorCode, ProductCode)
            ddlSku.Items.Clear()
            If (Not (dsUnitSet Is Nothing) AndAlso dsUnitSet.Tables.Count > 0 AndAlso Not (dsUnitSet.Tables(0) Is Nothing) AndAlso dsUnitSet.Tables(0).Rows.Count > 0) Then
                ddlSku.DataSource = dsUnitSet.Tables(0)
                ddlSku.DataTextField = "sku_desc"
                ddlSku.DataValueField = "sku_code"
                ddlSku.DataBind()
                If (dsUnitSet.Tables(0).Rows.Count = 1) Then
                    ddlSku.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
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
        Dim userGroup As String
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
            userGroup = userInfo.userGroupCodeEntity
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlVendor.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor.');", True)
            Exit Sub
        End If
        If Val(ddlBrand.SelectedValue.ToString()) = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a brand.');", True)
            Exit Sub
        End If
        If (ddlProduct.SelectedValue.ToString()) = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a product.');", True)
            Exit Sub
        End If
        Dim LovDetailsGet As New DeviationsFGQualityClass
        Dim LovDetailsList As DataSet
        Dim lovtype As String
        lovtype = ddlBrand.SelectedValue
        txtBatchNo.Text = ""


        LovDetailsList = LovDetailsGet.GetBrandTestDtlsList(Val(ddlBrand.SelectedValue), ddlQuarter.SelectedValue, ddlVendor.SelectedValue, userInfo.userIDEntity, ddlProduct.SelectedValue, ddlSku.SelectedValue)
        If (Not (LovDetailsList Is Nothing) AndAlso LovDetailsList.Tables.Count > 0) Then
            If (Not (LovDetailsList.Tables(0) Is Nothing) AndAlso LovDetailsList.Tables(0).Rows.Count > 0 AndAlso Not (LovDetailsList.Tables(1) Is Nothing) AndAlso LovDetailsList.Tables(1).Rows.Count > 0) Then
                gvTestList.DataSource = LovDetailsList.Tables(0)
                gvTestList.DataBind()
                gvExteriorTestList.DataSource = LovDetailsList.Tables(1)
                gvExteriorTestList.DataBind()

                If (Not String.IsNullOrEmpty(Convert.ToString(LovDetailsList.Tables(0).Rows(0)("product_code"))) AndAlso Not String.IsNullOrEmpty(Convert.ToString(LovDetailsList.Tables(0).Rows(0)("batch_no")))) Then

                    ddlProduct.SelectedValue = Convert.ToString(LovDetailsList.Tables(0).Rows(0)("product_code"))
                    txtBatchNo.Text = Convert.ToString(LovDetailsList.Tables(0).Rows(0)("batch_no"))
                End If


                If Convert.ToString(LovDetailsList.Tables(0).Rows(0)("dfq_confirm_status")) = "Y" Then
                    btnSubmit.Visible = False
                    btnConsubmit.Visible = False
                    ddlProduct.Enabled = False
                    txtBatchNo.Enabled = False
                Else
                    'btnSubmit.Visible = True
                    'btnConsubmit.Visible = True
                End If

                'If (userGroup = "HO" Or userGroup = "HO-MARKETING") Then
                '    btnSubmit.Visible = False
                '    btnConsubmit.Visible = False
                '    If Convert.ToString(LovDetailsList.Tables(0).Rows(0)("dfq_approve_yn")) <> "P" Then
                '        btnApprove.Visible = False
                '        btnReject.Visible = False
                '    Else
                '        btnApprove.Visible = True
                '        btnReject.Visible = True
                '    End If
                'Else
                '    btnApprove.Visible = False
                '    btnReject.Visible = False
                'End If

                'For Each row As DataRow In LovDetailsList.Tables(0).Rows
                '    If row("dfq_confirm_status").ToString() = "Y" Then
                '        btnSubmit.Visible = False
                '        btnConsubmit.Visible = False
                '        Exit For
                '    Else
                '        btnSubmit.Visible = True
                '        btnConsubmit.Visible = True
                '        Exit For
                '    End If
                'Next
                ddlQuarter.Enabled = False
                ddlVendor.Enabled = False
                ddlBrand.Enabled = False

            Else
                gvTestList.DataSource = Nothing
                gvTestList.DataBind()
                gvExteriorTestList.DataSource = Nothing
                gvExteriorTestList.DataBind()
            End If
        End If
    End Sub
#End Region


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindGrid()
    End Sub

    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
        PopulateVendorBrand(ddlVendor.SelectedValue)
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        CheckLogin()
        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If
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

        If String.IsNullOrEmpty(txtBatchNo.Text.Trim()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid batch no.');", True)
            Exit Sub
        End If


        Dim quarter As String = Convert.ToString(ddlQuarter.SelectedValue)
        Dim vendorId As String = Convert.ToString(ddlVendor.SelectedValue)
        Dim brandId As Int64 = Val(Convert.ToString(ddlBrand.SelectedValue))

        Dim dt As New DataTable
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next

        For Each row As GridViewRow In gvExteriorTestList.Rows
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next
        If dt.Rows.Count > 0 Then
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            Dim obj As New DeviationsFGQualityClass

            Dim RecordInserted As Integer = 0
            Dim status As String = String.Empty
            Dim flag As Boolean = False
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                Dim check As String = "S"
                'RecordInserted = obj.DeviationFGQualityInsertUpdate(quarter, vendorId, brandId, userInfo.userIDEntity, check, ddlProduct.SelectedValue, "", txtBatchNo.Text, dt, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    'sqlTrans.Rollback()
                    sqlTrans.Commit()
                    bindGrid()
                    If Val(hdnId.Value) > 0 Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updated successfully.');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted successfully.');", True)
                    End If

                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                End If
            Catch ex As Exception
                'If (sqlTrans IsNot Nothing) Then
                '    sqlTrans.Rollback()
                'End If
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
        End If
    End Sub

    Protected Sub btnConsubmit_Click(sender As Object, e As EventArgs) Handles btnConsubmit.Click
        CheckLogin()
        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If
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

        If String.IsNullOrEmpty(txtBatchNo.Text.Trim()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Invalid batch no.');", True)
            Exit Sub
        End If

        Dim quarter As String = Convert.ToString(ddlQuarter.SelectedValue)
        Dim vendorId As String = Convert.ToString(ddlVendor.SelectedValue)
        Dim brandId As Int64 = Val(Convert.ToString(ddlBrand.SelectedValue))

        Dim dt As New DataTable
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next

        For Each row As GridViewRow In gvExteriorTestList.Rows
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next
        If dt.Rows.Count > 0 Then
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            Dim obj As New DeviationsFGQualityClass

            Dim RecordInserted As Integer = 0
            Dim status As String = String.Empty
            Dim flag As Boolean = False
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                Dim check As String = "C"
                'RecordInserted = obj.DeviationFGQualityInsertUpdate(quarter, vendorId, brandId, userInfo.userIDEntity, check, ddlProduct.SelectedValue, "", txtBatchNo.Text, dt, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    'sqlTrans.Rollback()
                    sqlTrans.Commit()
                    bindGrid()
                    If Val(hdnId.Value) > 0 Then
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updated successfully.');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted successfully.');", True)
                    End If

                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                End If
            Catch ex As Exception
                'If (sqlTrans IsNot Nothing) Then
                '    sqlTrans.Rollback()
                'End If
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
        End If
    End Sub

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

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        Dim path = "~/DeviationsFGQuality.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub


    Protected Sub gvTestList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTestList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)
            Dim rowIndex As Int32 = e.Row.RowIndex
            Dim ddlResultValue As DropDownList = CType(e.Row.FindControl("ddlResultValue"), DropDownList)
            Dim txtResultValue As TextBox = CType(e.Row.FindControl("txtResultValue"), TextBox)
            Dim hdnResultValue As HiddenField = CType(e.Row.FindControl("hdnResultValue"), HiddenField)
            Dim hdnStatus As HiddenField = CType(e.Row.FindControl("hdnStatus"), HiddenField)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)

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
            If hdnStatus IsNot Nothing AndAlso Not String.IsNullOrEmpty(hdnStatus.Value) Then
                If Convert.ToString(hdnStatus.Value).Equals("Yes", StringComparison.InvariantCulture) Then
                    lblStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0f9906")
                Else
                    lblStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ed0b0b")
                End If
            End If


        End If
    End Sub
    Protected Sub gvExteriorTestList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvExteriorTestList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim rowView As DataRowView = TryCast(e.Row.DataItem, DataRowView)
            Dim rowIndex As Int32 = e.Row.RowIndex
            Dim ddlResultValue As DropDownList = CType(e.Row.FindControl("ddlResultValue"), DropDownList)
            Dim txtResultValue As TextBox = CType(e.Row.FindControl("txtResultValue"), TextBox)
            Dim hdnResultValue As HiddenField = CType(e.Row.FindControl("hdnResultValue"), HiddenField)
            Dim hdnStatus As HiddenField = CType(e.Row.FindControl("hdnStatus"), HiddenField)
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)

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
            If hdnStatus IsNot Nothing AndAlso Not String.IsNullOrEmpty(hdnStatus.Value) Then
                If Convert.ToString(hdnStatus.Value).Equals("Yes", StringComparison.InvariantCulture) Then
                    lblStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0f9906")
                Else
                    lblStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ed0b0b")
                End If
            End If
        End If
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        CheckLogin()
        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlVendor.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor.');", True)
            Exit Sub
        End If
        If Val(ddlBrand.SelectedValue.ToString()) = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a brand.');", True)
            Exit Sub
        End If


        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New DeviationsFGQualityClass

        Dim quarter As String = Convert.ToString(ddlQuarter.SelectedValue)
        Dim vendorId As String = Convert.ToString(ddlVendor.SelectedValue)
        Dim brandId As Int64 = Val(Convert.ToString(ddlBrand.SelectedValue))
        Dim productCode As String = (Convert.ToString(ddlProduct.SelectedValue))

        Dim dt As New DataTable
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next

        For Each row As GridViewRow In gvExteriorTestList.Rows
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next

        Dim RecordInserted As Integer
        If dt.Rows.Count > 0 Then
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                RecordInserted = obj.DeviationApproveRejectInsert("Y", userInfo.userIDEntity, quarter, vendorId, brandId, productCode, "", dt, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    'sqlTrans.Rollback()
                    sqlTrans.Commit()
                    bindGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approved successfully.');", True)

                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record approve Failed!');", True)
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
        End If
    End Sub

    'Protected Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
    '    CheckLogin()
    '    If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
    '        Exit Sub
    '    End If
    '    If String.IsNullOrEmpty(ddlVendor.SelectedValue.ToString()) Then
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor.');", True)
    '        Exit Sub
    '    End If
    '    If Val(ddlBrand.SelectedValue.ToString()) = 0 Then
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a brand.');", True)
    '        Exit Sub
    '    End If


    '    Dim sqlConn As SqlConnection = Nothing
    '    Dim sqlTrans As SqlTransaction = Nothing
    '    Dim obj As New DeviationsFGQualityClass

    '    Dim quarter As String = Convert.ToString(ddlQuarter.SelectedValue)
    '    Dim vendorId As String = Convert.ToString(ddlVendor.SelectedValue)
    '    Dim brandId As Int64 = Val(Convert.ToString(ddlBrand.SelectedValue))
    '    Dim dt As New DataTable
    '    dt.Columns.Add("test_id", GetType(Int64))
    '    dt.Columns.Add("result_value", GetType(String))

    '    For Each row As GridViewRow In gvTestList.Rows
    '        Dim slno As Label = CType(row.FindControl("lblSlno"), Label)
    '        Dim hdnTestId As HiddenField = CType(row.FindControl("hdnTestId"), HiddenField)
    '        Dim hdnTestType As HiddenField = CType(row.FindControl("hdnTestType"), HiddenField)
    '        Dim ddlResultValue As DropDownList = CType(row.FindControl("ddlResultValue"), DropDownList)
    '        Dim txtResultValue As TextBox = CType(row.FindControl("txtResultValue"), TextBox)
    '        Dim resultValue As String = String.Empty
    '        If hdnTestType.Value = "TT02" Then
    '            resultValue = ddlResultValue.SelectedValue.ToString()
    '        Else
    '            resultValue = txtResultValue.Text.ToString()
    '        End If
    '        If Val(hdnTestId.Value) > 0 Then
    '            Dim newRow As DataRow = dt.NewRow()
    '            newRow("test_id") = Val(hdnTestId.Value)
    '            newRow("result_value") = resultValue
    '            dt.Rows.Add(newRow)
    '        End If
    '    Next

    '    For Each row As GridViewRow In gvExteriorTestList.Rows
    '        Dim slno As Label = CType(row.FindControl("lblSlno"), Label)
    '        Dim hdnTestId As HiddenField = CType(row.FindControl("hdnTestId"), HiddenField)
    '        Dim hdnTestType As HiddenField = CType(row.FindControl("hdnTestType"), HiddenField)
    '        Dim ddlResultValue As DropDownList = CType(row.FindControl("ddlResultValue"), DropDownList)
    '        Dim txtResultValue As TextBox = CType(row.FindControl("txtResultValue"), TextBox)
    '        Dim resultValue As String = String.Empty
    '        If hdnTestType.Value = "TT02" Then
    '            resultValue = ddlResultValue.SelectedValue.ToString()
    '        Else
    '            resultValue = txtResultValue.Text.ToString()
    '        End If
    '        If Val(hdnTestId.Value) > 0 Then
    '            Dim newRow As DataRow = dt.NewRow()
    '            newRow("test_id") = Val(hdnTestId.Value)
    '            newRow("result_value") = resultValue
    '            dt.Rows.Add(newRow)
    '        End If
    '    Next

    '    Dim RecordInserted As Integer
    '    If dt.Rows.Count > 0 Then
    '        Try
    '            sqlConn = DBFactory.GetHelper.OpenConnection()
    '            sqlTrans = sqlConn.BeginTransaction()
    '            RecordInserted = obj.DeviationApproveRejectInsert("N", userInfo.userIDEntity, quarter, vendorId, brandId, dt, sqlConn, sqlTrans)
    '            If (RecordInserted > 0) Then
    '                'sqlTrans.Rollback()
    '                sqlTrans.Commit()
    '                bindGrid()
    '                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record rejected successfully.');", True)

    '            Else
    '                sqlTrans.Rollback()
    '                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record reject Failed!');", True)
    '            End If
    '        Catch ex As Exception
    '            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
    '        Finally
    '            If (sqlConn IsNot Nothing) Then
    '                sqlConn.Close()
    '            End If
    '        End Try
    '    Else
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
    '    End If
    'End Sub

    Protected Sub btnSubmitRemarks_Click(sender As Object, e As EventArgs) Handles btnSubmitRemarks.Click
        If String.IsNullOrWhiteSpace(txtRejectRemarks.Text) Then
            lblPopupError.Text = "Remarks are required."
            lblPopupError.Visible = True
            ' ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowPopup", "showRejectPopup();", True)
            Exit Sub
        End If
        ProcessRejection()

    End Sub

    Private Sub ProcessRejection()
        CheckLogin()
        If String.IsNullOrEmpty(ddlQuarter.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select quarter.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlVendor.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a vendor.');", True)
            Exit Sub
        End If
        If Val(ddlBrand.SelectedValue.ToString()) = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a brand.');", True)
            Exit Sub
        End If


        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New DeviationsFGQualityClass

        Dim quarter As String = Convert.ToString(ddlQuarter.SelectedValue)
        Dim vendorId As String = Convert.ToString(ddlVendor.SelectedValue)
        Dim brandId As Int64 = Val(Convert.ToString(ddlBrand.SelectedValue))
        Dim productCode As String = (Convert.ToString(ddlProduct.SelectedValue))
        Dim dt As New DataTable
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next

        For Each row As GridViewRow In gvExteriorTestList.Rows
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
                newRow("test_id") = Val(hdnTestId.Value)
                newRow("result_value") = resultValue
                dt.Rows.Add(newRow)
            End If
        Next

        Dim RecordInserted As Integer
        If dt.Rows.Count > 0 Then
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                RecordInserted = obj.DeviationApproveRejectInsert("N", userInfo.userIDEntity, quarter, vendorId, brandId, productCode, txtRejectRemarks.Text, dt, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    'sqlTrans.Rollback()
                    sqlTrans.Commit()
                    bindGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record rejected successfully.');", True)
                    txtRejectRemarks.Text = String.Empty
                    lblPopupError.Text = String.Empty
                    lblPopupError.Visible = False
                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record reject Failed!');", True)
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
        End If
    End Sub

    Protected Sub ddlBrand_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateVendorBrandProduct(ddlVendor.SelectedValue, ddlBrand.SelectedValue)
        'bindGrid()
    End Sub
    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Dim path = "~/vrs_FGQualityList.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If
    End Sub
    Protected Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateProductWiseSku(ddlVendor.SelectedValue, ddlProduct.SelectedValue)
    End Sub
End Class

