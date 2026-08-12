Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class OC_Specification_Dtls
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Public OCS_ID As String
    Public Confirm As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        AddAttributes()
        CalendarExtender1.StartDate = DateTime.Now.AddDays(-7).Date
        CalendarExtender1.EndDate = DateTime.Now.Date
        MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            PopulateType()
            PopulateVender()
            'ddlProduct_SelectedIndexChanged(sender, e)
            If (Not (Request.QueryString(Constant.SessionKeys.OCS_ID) Is Nothing)) Then
                If (Not (Request.QueryString(Constant.SessionKeys.OCS_ID) = Constant.GeneralMessages.AddNew)) Then
                    btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                    Session("OCS_ID") = Request.QueryString(Constant.SessionKeys.OCS_ID)
                    Session("Action") = "Edit"
                    PopulateOCSpecification()
                Else
                    btnSubmit.Text = Constant.GeneralMessages.Submit
                    Session("Action") = "Insert"
                End If
            Else
                btnSubmit.Text = Constant.GeneralMessages.Submit
                Session("Action") = "Insert"
            End If
            btnSubmit.Attributes.Add("onclick", "return ValidateOcSpecification();")
        End If
    End Sub
#Region "AddAttributes"
    Private Sub AddAttributes()
        txtBatchDate.Attributes.Add("ReadOnly", "ReadOnly")

    End Sub
#End Region
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        CheckLogin()
        For i As Integer = 0 To gvProductParameter.Rows.Count - 1 - 1
            Dim lblFrequency As Label = CType(gvProductParameter.Rows(i).Cells(0).FindControl("lblFrequency"), Label)
            Dim hdnFrequncy As HiddenField = CType(gvProductParameter.Rows(i).Cells(0).FindControl("hdnFrequncy"), HiddenField)
            Dim txtResult As TextBox = CType(gvProductParameter.Rows(i).Cells(0).FindControl("txtResult"), TextBox)

            Dim frequency As String = hdnFrequncy.Value
            Dim Result As String = txtResult.Text
            If frequency = "Each" And Result = "" Then
                lblErrMsg.Text = "Entry cann't be blank for Each Frequency"
            End If
        Next

        Dim RowsAffected As Integer = InsertOcSpecification()
        If RowsAffected = 1 Then
            Response.Redirect("~/OCSpecificationList.aspx")
        End If
    End Sub
#Region "Date Format"
    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime
        If Not (stringdate = String.Empty) Then
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
        End If
    End Function
#End Region
    Public Function InsertOcSpecification() As Integer

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim objOCSpecification As New OCSpecificationEntity
        Dim objOCSpecificationclass As New OCSpecification
        Dim objParameter As New OCSPrmPrdEntity

        objOCSpecification.Auto_Id = Request.QueryString(Constant.SessionKeys.OCS_ID)
        objOCSpecification.Vendor_Code = ddlVender.SelectedValue.ToString()
        objOCSpecification.Product_Type = ddlProduct.SelectedValue.ToString()
        objOCSpecification.Product_Code = ddlProductCode.SelectedValue.ToString()
        objOCSpecification.Batch_No = txtBatchno.Text
        objOCSpecification.Batch_Date = FormatDate(txtBatchDate.Text)
        sqlConn = VMS.DataAccess.DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()

        Dim RowsAffected As Integer
        Dim NoofRowsAffected As Integer
        Dim DeleteRow As Integer
        Try

            If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
                objOCSpecification.createduser = userInfo.userIDEntity
                objOCSpecification.activestatus = Constant.Common.ActiveStatus
                RowsAffected = objOCSpecificationclass.OC_SpecificationInsertUpdate(objOCSpecification, sqlConn, sqlTrans)
                If (RowsAffected > 0) Then
                    DeleteRow = objOCSpecificationclass.OC_SpecificationDelete(RowsAffected, sqlConn, sqlTrans)
                    For Each gvRow As GridViewRow In gvProductParameter.Rows
                        Dim lblParams As Label
                        Dim txtResult As TextBox
                        Dim ddlresult As DropDownList
                        Dim lblFrequency As Label
                        Dim result As String
                        Dim hdnDropDownYN As HiddenField = gvRow.FindControl("hdnDropDownYN")
                        Dim hdnFrequncy As HiddenField = gvRow.FindControl("hdnFrequncy")

                        lblParams = gvRow.FindControl("lblParams")
                        If (hdnDropDownYN.Value = "Y") Then
                            ddlresult = gvRow.FindControl("ddlresult")
                            result = ddlresult.SelectedValue
                        Else
                            txtResult = gvRow.FindControl("txtResult")
                            result = txtResult.Text
                        End If
                        lblFrequency = gvRow.FindControl("lblFrequency")

                        objParameter.Auto_Id = RowsAffected
                        objParameter.Paramss = lblParams.Text
                        objParameter.ResultType = result
                        objParameter.PFrequency = hdnFrequncy.Value
                        objParameter.CreatedUser = userInfo.userIDEntity
                        If (objParameter.ResultType <> "") Then
                            NoofRowsAffected += objOCSpecificationclass.OC_SpecificationDtls(objParameter, sqlConn, sqlTrans)
                        End If
                    Next
                    If (NoofRowsAffected > 0) Then
                        sqlTrans.Commit()
                    Else
                        sqlTrans.Rollback()
                    End If

                Else
                    sqlTrans.Rollback()
                End If
            Else
                objOCSpecification.createduser = userInfo.userIDEntity
                RowsAffected = objOCSpecificationclass.OC_SpecificationInsertUpdate(objOCSpecification, sqlConn, sqlTrans)
                If (RowsAffected > 0) Then
                    For Each gvRow As GridViewRow In gvProductParameter.Rows
                        Dim lblParams As Label
                        Dim txtResult As TextBox
                        Dim ddlresult As DropDownList
                        Dim lblFrequency As Label
                        Dim result As String
                        Dim hdnDropDownYN As HiddenField = gvRow.FindControl("hdnDropDownYN")
                        Dim hdnFrequncy As HiddenField = gvRow.FindControl("hdnFrequncy")

                        lblParams = gvRow.FindControl("lblParams")
                        If (hdnDropDownYN.Value = "Y") Then
                            ddlresult = gvRow.FindControl("ddlresult")
                            result = ddlresult.SelectedValue
                        Else
                            txtResult = gvRow.FindControl("txtResult")
                            result = txtResult.Text
                        End If
                        lblFrequency = gvRow.FindControl("lblFrequency")

                        objParameter.Auto_Id = RowsAffected
                        objParameter.Paramss = lblParams.Text
                        objParameter.ResultType = result
                        objParameter.PFrequency = hdnFrequncy.Value
                        objParameter.CreatedUser = userInfo.userIDEntity
                        If (objParameter.ResultType <> "") Then
                            NoofRowsAffected += objOCSpecificationclass.OC_SpecificationDtls(objParameter, sqlConn, sqlTrans)

                        End If
                    Next
                    If (NoofRowsAffected > 0) Then
                        sqlTrans.Commit()
                    Else
                        sqlTrans.Rollback()
                    End If

                Else
                    sqlTrans.Rollback()
                End If

            End If
            Return NoofRowsAffected

        Catch ex As Exception
            sqlTrans.Rollback()
        Finally
            sqlConn.Close()
            gvProductParameter.EditIndex = -1
            'PopulateParameter()
            lblPopMessage.Text = "QC Specification Insertion Successfull."
            lblPopMessage.ForeColor = System.Drawing.Color.Green
            ModalPopupExtender1.Show()
        End Try
    End Function
#Region "Populate Region"
    Private Sub PopulateType()

        Dim mstr As New OCSpecification
        Dim dsLovDtls As New DataSet
        Dim LovType As String = "OCS_PRODUCTS"
        dsLovDtls = mstr.GetProdDetails(userInfo.userIDEntity)
        If (Not (dsLovDtls Is Nothing) AndAlso dsLovDtls.Tables.Count > 0 AndAlso Not (dsLovDtls.Tables(0) Is Nothing) AndAlso dsLovDtls.Tables(0).Rows.Count > 0) Then
            ddlProduct.DataSource = dsLovDtls.Tables(0)
            ddlProduct.DataTextField = "lov_value"
            ddlProduct.DataValueField = "lov_code"
            ddlProduct.DataBind()
            ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If

        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlProduct.SelectedValue = userInfo.userRegionEntity
            ddlProduct.Enabled = False
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
#Region "ProductCodeFetchByType"
    Protected Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduct.SelectedIndexChanged
        Dim objProduct As New OCSPrdPrmClass
        Dim ds As DataSet
        PopulateParameter()
        ddlProductCode.Items.Clear()
        ds = objProduct.GetProductCode(ddlProduct.SelectedValue, userInfo.userIDEntity)


        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlProductCode.DataSource = ds.Tables(0)
            ddlProductCode.DataTextField = "ProductName"
            ddlProductCode.DataValueField = "ProductCode"
            ddlProductCode.DataBind()

        End If
        ddlProductCode.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
        '    ddlProductCode.SelectedValue = userInfo.userRegionEntity
        '    ddlProductCode.Enabled = False
        'End If
    End Sub
#End Region
#Region "Product Frequencey Bind"
    Private Sub PopulateParameter()
        Dim objmstr As New OCSPrdPrmClass
        Dim ds As New DataSet
        ds = objmstr.GetProductParameter(ddlProduct.SelectedValue.ToString(), Session("OCS_ID"), Session("Action").ToString())
        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            gvProductParameter.DataSource = ds.Tables(0)
            gvProductParameter.DataBind()
        Else
            gvProductParameter.DataSource = Nothing
            gvProductParameter.DataBind()

        End If
    End Sub
#End Region

    Protected Sub gvProductParameter_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvProductParameter.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim lblParams As Label = CType(e.Row.FindControl("lblParams"), Label)
            Dim txtResult As TextBox = CType(e.Row.FindControl("txtResult"), TextBox)
            Dim ddlresult As DropDownList = CType(e.Row.FindControl("ddlresult"), DropDownList)
            Dim lblFrequency As Label = CType(e.Row.FindControl("lblFrequency"), Label)

            If rowView("IsDropdown") = "Y" Then
                txtResult.Visible = False
                ddlresult.Visible = True
                PopulateParams(ddlresult, rowView("DropdownParams"))
                ddlresult.SelectedValue = rowView("Result").ToString

            Else
                txtResult.Visible = True
                ddlresult.Visible = False
            End If
            If rowView("IsNumericValue") = "Y" Then
                'txtResult.Attributes.Add("onkeypress", "return isNumber(event);")
                txtResult.Attributes.Add("onkeypress", "return isNumberKey(event,this);")
            End If
            'If lblFrequency.Text = "Each" Then
            '    'txtResult.Attributes("onchange") = "validateBlank() (this);"
            '    txtResult.Text = "Value cann't be blank"
            '    txtResult.BackColor = System.Drawing.Color.Yellow
            '    txtResult.ForeColor = System.Drawing.Color.Red
            'End If

        End If
    End Sub
#Region "Populate Vender"
    Public Sub PopulateVender()
        Dim ds As DataSet
        Try
            Dim StockObj As New UnitDespatchClass
            ds = StockObj.GetUnit(String.Empty, Constant.Common.ActiveStatus)
            If ((Not (ds Is Nothing) And ds.Tables.Count > 0 And Not (ds.Tables(0) Is Nothing) And ds.Tables(0).Rows.Count > 0)) Then
                ddlVender.DataSource = ds.Tables(0)
                ddlVender.DataTextField = "unit_name"
                ddlVender.DataValueField = "unit_code"
                ddlVender.DataBind()
                ddlVender.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            Else
                ddlVender.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
            If (userInfo.userGroupCodeEntity = "UNIT") Then
                ddlVender.SelectedValue = userInfo.userBranchEntity
                ddlVender.Enabled = False
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub
#End Region
    Public Sub PopulateParams(ByVal ddl As DropDownList, ByVal params As String)
        Dim dt As New DataTable
        dt.Columns.Add("param_value", GetType(String))
        Try
            Dim paramList = params.Split(",")
            If (paramList.Length > 0) Then
                For i As Integer = 0 To paramList.Length - 1
                    Dim dr As DataRow = dt.NewRow()
                    dr("param_value") = paramList(i).ToString
                    dt.Rows.Add(dr)
                Next
                dt.AcceptChanges()
            Else
                ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If

            If ((Not (dt Is Nothing) And dt.Rows.Count > 0)) Then
                ddl.DataSource = dt
                ddl.DataTextField = "param_value"
                ddl.DataValueField = "param_value"
                ddl.DataBind()
                ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            Else
                ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        Catch ex As Exception
            Dim returnUrl = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/OCSpecificationList.aspx?Id=New")
    End Sub
#Region "OcSpecification Fetch"
    Public Sub PopulateOCSpecification()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim PopulateOCset As New DataSet
        Dim OcSpecificationclass As New OCSpecification
        PopulateOCset = OcSpecificationclass.Edit_OCSpecificationData(Convert.ToInt32(Request.QueryString(Constant.SessionKeys.OCS_ID)))

        If (Not (PopulateOCset Is Nothing) AndAlso PopulateOCset.Tables.Count > 0 AndAlso Not (PopulateOCset.Tables(0) Is Nothing) AndAlso PopulateOCset.Tables(0).Rows.Count > 0) Then
            Dim oUserProfile As New UserProfileEntity

            ddlVender.SelectedValue = IIf(PopulateOCset.Tables(0).Rows(0)("VendorCode").Equals(DBNull.Value), String.Empty, PopulateOCset.Tables(0).Rows(0)("VendorCode"))
            ddlProduct.SelectedValue = IIf(PopulateOCset.Tables(0).Rows(0)("ProductType").Equals(DBNull.Value), String.Empty, PopulateOCset.Tables(0).Rows(0)("ProductType"))
            txtBatchno.Text = IIf(PopulateOCset.Tables(0).Rows(0)("BatchNo").Equals(DBNull.Value), String.Empty, PopulateOCset.Tables(0).Rows(0)("BatchNo"))
            txtBatchDate.Text = IIf(PopulateOCset.Tables(0).Rows(0)("BatchDate").Equals(DBNull.Value), String.Empty, PopulateOCset.Tables(0).Rows(0)("BatchDate"))
            ddlProduct_SelectedIndexChanged(Nothing, Nothing)
            ddlProductCode.SelectedValue = IIf(PopulateOCset.Tables(0).Rows(0)("ProductCode").Equals(DBNull.Value), String.Empty, PopulateOCset.Tables(0).Rows(0)("ProductCode"))
            Confirm = IIf(PopulateOCset.Tables(0).Rows(0)("Confirm_YN").Equals(DBNull.Value), String.Empty, PopulateOCset.Tables(0).Rows(0)("Confirm_YN"))
            If Confirm = "Y" Then
                btnSubmit.Visible = False
            Else
                btnSubmit.Visible = True
            End If
            ddlVender.Enabled = False
            ddlProduct.Enabled = False
            ddlProductCode.Enabled = False
            txtBatchno.Enabled = False
            txtBatchDate.Enabled = False
        Else
            ddlVender.Enabled = True
            ddlProduct.Enabled = True
            ddlProductCode.Enabled = True
            txtBatchno.Enabled = True
            txtBatchDate.Enabled = True
        End If
    End Sub
#End Region

    Protected Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        If lblPopMessage.Text.Trim().Equals("QC Specification Insertion Successfull.") Then
            Response.Redirect("~/OCSpecificationList.aspx?ID=Add", False)
            ModalPopupExtender1.Hide()
        End If
    End Sub
End Class
