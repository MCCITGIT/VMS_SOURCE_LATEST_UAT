Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class TokenVendorAddUpdate
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

            If Not (String.IsNullOrEmpty(Request.QueryString("vendorCode"))) Then
                'If Not (Request.QueryString(Constant.SessionKeys.ID) = Nothing) Then
                btnSubmit.CommandName = Constant.GeneralMessages.btnUpdate
                btnSubmit.Text = Constant.GeneralMessages.btnUpdate

                Dim TVMCode As String = Request.QueryString("vendorCode")

                PopulateTokenVendor(TVMCode)
                btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                lblVendorId.ForeColor = Drawing.Color.Black
            Else
                lblVendorId.ForeColor = Drawing.Color.Red
                btnSubmit.Text = Constant.GeneralMessages.Submit
            End If
        End If

        AddAttributes()
    End Sub

#End Region

#Region "Function to populate Vendor Details for further modification"
    Private Sub PopulateTokenVendor(ByVal tvm_code As String)

        CheckLogin()
        Dim obj As New TokenVendorListClass
        Dim VendorProfile As New VendorMasterEntity
        Dim VendorDB As New VendorMaster
        Dim VendorDs As New DataSet
        VendorDs = obj.GetTokenVendorGetBy_tvm_code(tvm_code)


        If (Not (VendorDs Is Nothing) AndAlso VendorDs.Tables.Count > 0 AndAlso Not (VendorDs.Tables(0) Is Nothing) AndAlso VendorDs.Tables(0).Rows.Count > 0) Then
            trActive.Visible = True

            lblVendorId.Text = VendorDs.Tables(0).Rows(0)("tvm_code")
            lblVendorId.Enabled = False
            'txtTokenVendorName.Text = IIf(VendorDs.Tables(0).Rows(0)("tvm_name") = DBNull.Value, String.Empty, VendorDs.Tables(0).Rows(0)("tvm_name"))
            'txtTokenVendorEmail.Text = IIf(VendorDs.Tables(0).Rows(0)("tvm_email") = DBNull.Value, String.Empty, VendorDs.Tables(0).Rows(0)("tvm_email"))
            'txtAddress.Text = IIf(VendorDs.Tables(0).Rows(0)("tvm_address") = DBNull.Value, String.Empty, VendorDs.Tables(0).Rows(0)("tvm_address"))
            'txtMobile.Text = IIf(VendorDs.Tables(0).Rows(0)("tvm_mobile") = DBNull.Value, String.Empty, VendorDs.Tables(0).Rows(0)("tvm_mobile"))
            'txtCity.Text = IIf(VendorDs.Tables(0).Rows(0)("tvm_city") = DBNull.Value, String.Empty, VendorDs.Tables(0).Rows(0)("tvm_city"))
            'txtState.Text = IIf(VendorDs.Tables(0).Rows(0)("tvm_state") = DBNull.Value, String.Empty, VendorDs.Tables(0).Rows(0)("tvm_state"))
            'txtZip.Text = IIf(VendorDs.Tables(0).Rows(0)("tvm_zip") = DBNull.Value, String.Empty, VendorDs.Tables(0).Rows(0)("tvm_zip"))

            txtTokenVendorName.Text = Convert.ToString(VendorDs.Tables(0).Rows(0)("tvm_name"))
            txtTokenVendorEmail.Text = Convert.ToString(VendorDs.Tables(0).Rows(0)("tvm_email"))
            txtAddress.Text = Convert.ToString(VendorDs.Tables(0).Rows(0)("tvm_address"))
            txtMobile.Text = Convert.ToString(VendorDs.Tables(0).Rows(0)("tvm_mobile"))
            txtCity.Text = Convert.ToString(VendorDs.Tables(0).Rows(0)("tvm_city"))
            txtState.Text = Convert.ToString(VendorDs.Tables(0).Rows(0)("tvm_state"))
            txtZip.Text = Convert.ToString(VendorDs.Tables(0).Rows(0)("tvm_zip"))

            ddlActive.SelectedValue = VendorDs.Tables(0).Rows(0)("active")

        Else

            'ddlVendor.SelectedValue = VendorDs.Tables(0).Rows(0)("v_vendor_unit")
            'txtSKU.Text = VendorDs.Tables(0).Rows(0)("v_sku_code")
            'txtDesc.Text = (VendorDs.Tables(0).Rows(0)("SkuDescription"))

            'gvVendorAdd.Visible = True
            'gvVendorAdd.DataSource = VendorDs.Tables(0)
            'gvVendorAdd.DataBind()
        End If
        'ddlEngDepot.SelectedValue = EngProfile.EngLocation_code
        'ddlEngClassifi.SelectedValue = EngProfile.Engclassification
        'txtbxLongName.Text = EngProfile.EngLong_name
        'txtbxShortName.Text = EngProfile.EngShortName
        'txtBxmobile.Text = EngProfile.EngmobileNo
    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()

        btnSubmit.OnClientClick = "return ValidateSubmit('" & btnSubmit.Text & "');"
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

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        CheckLogin()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New TokenVendorListClass
        Dim list As New List(Of Integer)
        Dim RecordUpdated As Integer
        Dim status As String = String.Empty
        Try
            If (Not (txtTokenVendorName.Text.Equals(String.Empty)) And Not (txtTokenVendorEmail.Text.Equals(String.Empty)) And Not (txtAddress.Text.Equals(String.Empty)) And Not (txtMobile.Text.Equals(String.Empty)) And Not (txtCity.Text.Equals(String.Empty)) And Not (txtState.Equals(String.Empty)) And Not (txtZip.Text.Equals(String.Empty))) Then
                If IsNumeric(txtMobile.Text.Trim()) And IsNumeric(txtZip.Text.Trim()) Then


                    sqlConn = DBFactory.GetHelper.OpenConnection()
                    sqlTrans = sqlConn.BeginTransaction()
                    If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
                        If Not (ddlActive.SelectedValue.Equals(String.Empty)) Then
                            Dim MobileNo As Integer = Integer.MinValue
                            If Not (txtMobile.Text.Trim() = String.Empty) Then
                                MobileNo = CType(txtMobile.Text.Trim(), Integer)
                            Else
                                MobileNo = Integer.MinValue
                            End If
                            Dim ZipNo As Integer = Integer.MinValue
                            If Not (txtZip.Text.Trim() = String.Empty) Then
                                ZipNo = CType(txtZip.Text.Trim(), Integer)
                            Else
                                ZipNo = Integer.MinValue
                            End If
                            RecordUpdated = obj.TokenVendorInsertUpdate(lblVendorId.Text.Trim(), txtTokenVendorName.Text.Trim(), txtTokenVendorEmail.Text.Trim(), MobileNo, txtAddress.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), ZipNo, userInfo.userIDEntity, ddlActive.SelectedValue, sqlConn, sqlTrans)
                            If (RecordUpdated > 0) Then
                                sqlTrans.Commit()
                                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updated Successfully.');window.location.href='TokenVendorList.aspx';", True)

                            ElseIf (RecordUpdated = 0) Then
                                sqlTrans.Rollback()
                                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updation Failed!');", True)
                            ElseIf (RecordUpdated = -1) Then
                                sqlTrans.Rollback()
                                Throw New Exception
                            End If
                        Else
                            lblErrorMessage.Text = "Please select a status."
                        End If

                    Else

                        RecordUpdated = obj.TokenVendorInsertUpdate("0", txtTokenVendorName.Text.Trim(), txtTokenVendorEmail.Text.Trim(), txtMobile.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtZip.Text.Trim(), userInfo.userIDEntity, Constant.Common.ActiveStatus, sqlConn, sqlTrans)

                        list.Add(RecordUpdated)
                        If (RecordUpdated > 0) Then
                            sqlTrans.Commit()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location.href='TokenVendorList.aspx';", True)

                        ElseIf (RecordUpdated = 0) Then
                            sqlTrans.Rollback()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                        ElseIf (RecordUpdated = -1) Then
                            sqlTrans.Rollback()
                            Throw New Exception
                        End If
                    End If
                Else
                    lblErrorMessage.Text = "Mobile no and zip code should be numeric."

                End If
            Else
                lblErrorMessage.Text = "Required field's can't be blank."
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

        End Try
    End Sub
   
    'Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
    '    CheckLogin()
    '    lblErrorMessage.Text = ""
    '    Dim sqlConn As SqlConnection = Nothing
    '    Dim sqlTrans As SqlTransaction = Nothing
    '    Dim obj As New TokenVendorListClass



    '    Dim RecordInserted As Integer
    '    Dim status As String = String.Empty
    '    Dim flag As Boolean = False
    '    Try

    '        sqlConn = DBFactory.GetHelper.OpenConnection()
    '        sqlTrans = sqlConn.BeginTransaction()

    '        'obj.TokenVendorInsertUpdate("0", txtTokenVendorName.Text, txtTokenVendorEmail.Text, txtMobile.Text, txtAddress.Text, txtCity.Text, txtState.Text,txtZip.Text,"Y","" userInfo.userIDEntity, sqlConn, sqlTrans)
    '            If (RecordInserted > 0) Then
    '                sqlTrans.Commit()
    '                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');window.location.href='TokenRequisitionList.aspx';", True)

    '            Else
    '                sqlTrans.Rollback()
    '                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
    '            End If





    '    Catch ex As Exception
    '        If (sqlTrans IsNot Nothing) Then
    '            sqlTrans.Rollback()
    '        End If

    '        Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
    '        HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

    '    Finally
    '        If (sqlConn IsNot Nothing) Then
    '            sqlConn.Close()
    '        End If

    '    End Try
    'End Sub
   
End Class
