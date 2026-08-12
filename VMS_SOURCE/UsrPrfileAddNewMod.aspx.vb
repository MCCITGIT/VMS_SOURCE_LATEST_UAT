Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class UsrPrfileAddNewMod
    Inherits System.Web.UI.Page
    Public UserId As String
    Dim UserFunctionclass As New UserProfile

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddAttributes()
        If Not IsPostBack Then
            'SeniorityddlIncrement()
            LoadDepotName()
            'populateBranch()
            populateDepartment()
            populateEmployee()
            populateDesignation()
            populateRegion()
            populateResonforseperation()
            populateUserGroupCode()
            'ddlReportingTo2.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
            'If ((Not (Request.QueryString(Constant.SessionKeys.UserId) Is Nothing)) AndAlso Request.QueryString(Constant.SessionKeys.UserId) = Constant.GeneralMessages.AddNew) Then
            If (Not (Request.QueryString(Constant.SessionKeys.UserId) Is Nothing)) Then
                If (Not (Request.QueryString(Constant.SessionKeys.UserId) = Constant.GeneralMessages.AddNew)) Then
                    btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                    UserId = Request.QueryString(Constant.SessionKeys.UserId)
                    PopulateUserProfile()
                Else
                    btnSubmit.Text = Constant.GeneralMessages.Submit
                    rbtnIncApplicableN.Checked = True
                    rbtnIntPasswordN.Checked = True
                    rbtnActiveY.Checked = True
                End If
            Else
                btnSubmit.Text = Constant.GeneralMessages.Submit
                rbtnIncApplicableN.Checked = True
                rbtnIntPasswordN.Checked = True
                rbtnActiveY.Checked = True
            End If

            'populateVendortype()
        End If
    End Sub

    '#Region "Seniority"
    '    Private Sub SeniorityddlIncrement()
    '        ddlSeniority.Items.Clear()
    '        'Gets the Seniority from the web.config file
    '        Dim configSeniority As String = ConfigurationManager.AppSettings.Get("Seniority")
    '        Dim index As Integer = 0

    '        While index <= configSeniority
    '            Try
    '                Dim size As Integer = Convert.ToInt32(index)
    '                'Adds the Seniority to drop down list
    '                ddlSeniority.Items.Add(New ListItem(size.ToString, size.ToString))
    '            Catch exp As Exception
    '                ddlSeniority.Items.Clear()
    '            End Try
    '            System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
    '        End While
    '    End Sub
    '#End Region

#Region "AddAttributes"

    Private Sub AddAttributes()

        btnSubmit.Attributes.Add("onClick", "return ValidateUPAControls();")
        txtTotalExpYears.Attributes.Add("onKeyPress", "KeyPressNumeric()")
        txtTotalExpMonths.Attributes.Add("onKeyPress", "KeyPressNumeric()")
        txtUserID.Attributes.Add("OnBlur", "compareUserID(this.value);")
        ddlReasonForSeperation.Attributes.Add("OnChange", "fnReasonSeperation(this.value);")
        ddlBranch.Attributes.Add("onChange", "return fnRegionGet(this.value);")
    End Sub

#End Region

#Region "Load Depot Code"
    Private Sub LoadDepotName()

        Dim commonobj As New Common

        Dim dsDepot As DataSet

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")

        End If
        Dim depotCode As String

        depotCode = String.Empty
        dsDepot = commonobj.GetDepotDetails(depotCode, Constant.Common.ActiveStatus)
        If (Not (dsDepot Is Nothing) AndAlso dsDepot.Tables.Count > 0 AndAlso Not (dsDepot.Tables(0) Is Nothing) AndAlso dsDepot.Tables(0).Rows.Count > 0) Then
            Dim dataSortview As DataView = New DataView(dsDepot.Tables(0))
            dataSortview.Sort = "depot_name asc"
            ddlBranch.DataSource = dataSortview
            ddlBranch.DataTextField = "depot_name"
            ddlBranch.DataValueField = "depot_code"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
            ddlBranch.SelectedValue = userInfo.userBranchEntity
        End If

        If Not userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Then
            ddlBranch.Enabled = False
        End If
    End Sub
#End Region

    Public Sub populateBranch()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim UserTypeSet As New DataSet
        Dim LovType As String = Constant.Common.Lov_Vend_Branch
        UserTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (UserTypeSet Is Nothing) AndAlso UserTypeSet.Tables.Count > 0 AndAlso Not (UserTypeSet.Tables(0) Is Nothing) AndAlso UserTypeSet.Tables(0).Rows.Count > 0) Then
            ddlBranch.DataSource = UserTypeSet.Tables(0)
            ddlBranch.DataMember = "lov_code"
            ddlBranch.DataValueField = "lov_code"
            ddlBranch.DataBind()
            ddlBranch.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
    Public Sub populateDepartment()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.Lov_Department
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlDepartment.DataSource = OccupationTypeSet.Tables(0)
            ddlDepartment.DataMember = "lov_value"
            ddlDepartment.DataValueField = "lov_value"
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))

            ddlReportingTo1.DataSource = OccupationTypeSet.Tables(0)
            ddlReportingTo1.DataMember = "lov_value"
            ddlReportingTo1.DataValueField = "lov_value"
            ddlReportingTo1.DataBind()
            ddlReportingTo1.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
    Public Sub PopulateReportingto2()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjUserProfile As New UserProfile
        Dim ReportingTypeSet As New DataSet
        'Dim LovType As String = Constant.Common.Lov_Employee_Type
        ddlReportingTo2.Items.Clear()
        'ReportingTypeSet = ObjUserProfile.User_Profile(Session("COMPANY"), ddlDepartment.SelectedValue)
        If (Not (Request.QueryString(Constant.SessionKeys.UserId) Is Nothing)) Then
            If (Not (Request.QueryString(Constant.SessionKeys.UserId) = Constant.GeneralMessages.AddNew)) Then
                'Update Mode
                ReportingTypeSet = ObjUserProfile.User_Profile(userInfo.userCompanyEntity, ddlReportingTo1.SelectedValue, Request.QueryString(Constant.SessionKeys.UserId))
            Else
                'Add Mode
                ReportingTypeSet = ObjUserProfile.User_Profile(userInfo.userCompanyEntity, ddlReportingTo1.SelectedValue, String.Empty)
            End If
        Else
            'Add Mode
            ReportingTypeSet = ObjUserProfile.User_Profile(userInfo.userCompanyEntity, ddlReportingTo1.SelectedValue, String.Empty)
        End If

        If (Not (ReportingTypeSet Is Nothing) AndAlso ReportingTypeSet.Tables.Count > 0 AndAlso Not (ReportingTypeSet.Tables(0) Is Nothing) AndAlso ReportingTypeSet.Tables(0).Rows.Count > 0) Then
            ddlReportingTo2.DataSource = ReportingTypeSet.Tables(0)
            ddlReportingTo2.DataMember = "usp_user_id"
            ddlReportingTo2.DataValueField = "usp_user_id"
            ddlReportingTo2.DataBind()
            ddlReportingTo2.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        Else
            ddlReportingTo2.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
    Public Sub populateEmployee()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.Lov_Employee_Type
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlPrmtTmpy.DataSource = OccupationTypeSet.Tables(0)
            ddlPrmtTmpy.DataMember = "lov_value"
            ddlPrmtTmpy.DataValueField = "lov_value"
            ddlPrmtTmpy.DataBind()
            ddlPrmtTmpy.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
    Public Sub populateDesignation()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.Lov_Designation
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlDesignation.DataSource = OccupationTypeSet.Tables(0)
            ddlDesignation.DataMember = "lov_value"
            ddlDesignation.DataValueField = "lov_value"
            ddlDesignation.DataBind()
            ddlDesignation.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
    Public Sub populateRegion()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlRegion.DataSource = OccupationTypeSet.Tables(0)
            ddlRegion.DataTextField = "lov_value"
            ddlRegion.DataValueField = "lov_code"
            ddlRegion.DataBind()
            ddlRegion.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
            'ddlRegion.SelectedValue = userInfo.userRegionEntity
        End If

    End Sub
    Public Sub populateResonforseperation()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.Lov_Sep_Reason
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlReasonForSeperation.DataSource = OccupationTypeSet.Tables(0)
            ddlReasonForSeperation.DataMember = "lov_value"
            ddlReasonForSeperation.DataValueField = "lov_value"
            ddlReasonForSeperation.DataBind()
            ddlReasonForSeperation.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
    Public Sub populateUserGroupCode()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New UserProfile
        Dim UserGroupCodeSet As New DataSet
        'Dim LovType As String = Constant.Common.Lov_Sep_Reason
        UserGroupCodeSet = ObjDocumentType.UserGroup_Get(userInfo.userCompanyEntity)
        If (Not (UserGroupCodeSet Is Nothing) AndAlso UserGroupCodeSet.Tables.Count > 0 AndAlso Not (UserGroupCodeSet.Tables(0) Is Nothing) AndAlso UserGroupCodeSet.Tables(0).Rows.Count > 0) Then
            ddlUserGroup.DataSource = UserGroupCodeSet.Tables(0)
            ddlUserGroup.DataMember = "grp_user_group_code"
            ddlUserGroup.DataValueField = "grp_user_group_code"
            ddlUserGroup.DataBind()
            ddlUserGroup.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
    Public Function InsertUserProfile() As Integer

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim oUserProfile As New UserProfileEntity
        oUserProfile.uspcompany = userInfo.userCompanyEntity
        oUserProfile.uspuserid = txtUserID.Text
        oUserProfile.uspfirstname = txtFirstName.Text
        oUserProfile.usplastname = txtLastName.Text
        oUserProfile.uspinitials = txtShortName.Text
        oUserProfile.uspgroupcode = ddlUserGroup.SelectedValue
        If btnSubmit.Text = Constant.GeneralMessages.btnUpdate And rbtnIntPasswordY.Checked Then
            oUserProfile.usppswd = "9999"
            If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
                If hdnpwd1.Value = String.Empty Then
                    oUserProfile.uspoldpswd1 = hdnpwd.Value
                    oUserProfile.uspoldpswd2 = String.Empty
                    oUserProfile.uspoldpswd3 = String.Empty
                End If
                If hdnpwd2.Value = String.Empty Then
                    oUserProfile.uspoldpswd2 = hdnpwd.Value
                    oUserProfile.uspoldpswd3 = String.Empty
                End If
                If hdnpwd3.Value = String.Empty Then
                    oUserProfile.uspoldpswd3 = hdnpwd.Value
                End If
            Else
                oUserProfile.uspoldpswd1 = String.Empty
                oUserProfile.uspoldpswd2 = String.Empty
                oUserProfile.uspoldpswd3 = String.Empty
            End If
        Else
            If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
                oUserProfile.usppswd = hdnpwd.Value
            Else
                oUserProfile.usppswd = "9999"
            End If
        End If
        oUserProfile.uspdesig = ddlDesignation.SelectedValue
        oUserProfile.uspbranch = ddlBranch.SelectedValue
        oUserProfile.uspdept = ddlDepartment.SelectedValue
        oUserProfile.uspmailid = txtEmail.Text
        oUserProfile.uspofficeno = txtOfficePhoneNo.Text
        oUserProfile.uspextension = txtExtension.Text
        oUserProfile.uspmobile = txtMobilePhoneNo.Text
        oUserProfile.uspdob = FormatDate(txtDOB.Text)
        oUserProfile.uspemptype = ddlPrmtTmpy.SelectedValue
        oUserProfile.uspdoj = FormatDate(txtDOJ.Text)
        If Not txtTotalExpYears.Text = String.Empty Then
            oUserProfile.uspexpyrs = txtTotalExpYears.Text
        Else
            oUserProfile.uspexpyrs = 0
        End If
        If Not txtTotalExpMonths.Text = String.Empty Then
            oUserProfile.uspexpmonths = txtTotalExpMonths.Text
        Else
            oUserProfile.uspexpmonths = 0
        End If
        oUserProfile.usplastaccesseddate = SqlDateTime.Null
        oUserProfile.uspRegion = ddlRegion.SelectedValue
        oUserProfile.uspreportingmanager = ddlReportingTo2.SelectedValue
        oUserProfile.uspnotimesused = Integer.MinValue
        oUserProfile.uspexitdate = FormatDate(txtDateOfSeperation.Text)
        oUserProfile.createduser = userInfo.userIDEntity
        oUserProfile.usphomeadd = txtResAddress.Text
        oUserProfile.usphomeno = txtResPhoneNo.Text
        oUserProfile.uspreason = ddlReasonForSeperation.SelectedValue
        oUserProfile.uspreportingusergroup = ddlReportingTo1.SelectedValue
        ' oUserProfile.uspseniority = ddlSeniority.SelectedValue
        oUserProfile.uspbloodgroup = txtBloodGroup.Text
        If rbtnIncApplicableY.Checked Then
            oUserProfile.uspincentiveyn = Constant.Common.ActiveStatus
        ElseIf rbtnIncApplicableN.Checked Then
            oUserProfile.uspincentiveyn = Constant.Common.InActiveStatus
        End If
        If rbtnActiveY.Checked Then
            oUserProfile.activestatus = Constant.Common.ActiveStatus
        ElseIf rbtnActiveN.Checked Then
            oUserProfile.activestatus = Constant.Common.InActiveStatus
        End If
        Dim RowsAffected As Integer
        If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
            oUserProfile.modifieduser = userInfo.userIDEntity
            oUserProfile.uspuserid = Request.QueryString(Constant.SessionKeys.UserId)
            'oCustEntity.activestatus = Constant.Common.ActiveStatus
            RowsAffected = UserFunctionclass.UserUpdate(oUserProfile)
        Else
            RowsAffected = UserFunctionclass.UserInsert(oUserProfile)
            'RowsAffected = CustomerFuncitonClass.CustomerInsert(oCustEntity)
        End If
        Return RowsAffected
    End Function

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

    Public Sub PopulateUserProfile()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim PopulateUserset As New DataSet
        Dim UserFunctionclass As New UserProfile
        PopulateUserset = UserFunctionclass.User_Edit_Get(userInfo.userCompanyEntity, Request.QueryString(Constant.SessionKeys.UserId))
        If (Not (PopulateUserset Is Nothing) AndAlso PopulateUserset.Tables.Count > 0 AndAlso Not (PopulateUserset.Tables(0) Is Nothing) AndAlso PopulateUserset.Tables(0).Rows.Count > 0) Then
            Dim oUserProfile As New UserProfileEntity
            lblPassword.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_pswd").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_pswd"))

            txtUserID.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_user_id").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_user_id"))
            txtUserID.Enabled = False
            txtFirstName.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_first_name").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_first_name"))
            txtLastName.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_last_name").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_last_name"))
            txtShortName.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_initials").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_initials"))
            ddlUserGroup.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_group_code").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_group_code"))
            ddlDesignation.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_desig").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_desig"))
            ddlBranch.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_branch").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_branch"))
            ddlDepartment.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_dept").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_dept"))
            txtEmail.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_mailid").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_mailid"))
            txtOfficePhoneNo.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_office_no").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_office_no"))
            txtExtension.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_extension").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_extension"))
            txtMobilePhoneNo.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_mobile").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_mobile"))
            'txtDOB.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_dob") = "01/01/1900", String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_dob"))
            txtDOB.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_dob").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_dob"))
            ddlPrmtTmpy.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_emp_type").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_emp_type"))
            'txtDOJ.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_doj") = "01/01/1900", String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_doj"))
            txtDOJ.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_doj").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_doj"))
            txtTotalExpYears.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_exp_yrs").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_exp_yrs"))
            txtTotalExpMonths.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_exp_months").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_exp_months"))
            ddlRegion.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_region").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_region"))
            ddlReportingTo1.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_reporting_usergroup").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_reporting_usergroup"))

            PopulateReportingto2()

            ddlReportingTo2.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_reporting_manager").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_reporting_manager"))
            'txtDateOfSeperation.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_exit_date") = "01/01/1900", String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_exit_date"))
            txtDateOfSeperation.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_exit_date").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_exit_date"))
            txtResAddress.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_home_add").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_home_add"))
            'txtShortName.Text = IIf(PopulateUserset.Tables(0).Rows(0)("").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)(""))
            txtResPhoneNo.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_home_no").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_home_no"))
            ddlReasonForSeperation.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_reason").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_reason"))
            'ddlUserGroup.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)(""))
            'ddlSeniority.SelectedValue = IIf(PopulateUserset.Tables(0).Rows(0)("usp_seniority").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_seniority"))
            txtBloodGroup.Text = IIf(PopulateUserset.Tables(0).Rows(0)("usp_blood_group").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_blood_group"))
            hdnpwd.Value = IIf(PopulateUserset.Tables(0).Rows(0)("usp_pswd").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_pswd"))
            hdnpwd1.Value = IIf(PopulateUserset.Tables(0).Rows(0)("usp_old_pswd1").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_old_pswd1"))
            hdnpwd2.Value = IIf(PopulateUserset.Tables(0).Rows(0)("usp_old_pswd2").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_old_pswd2"))
            hdnpwd3.Value = IIf(PopulateUserset.Tables(0).Rows(0)("usp_old_pswd3").Equals(DBNull.Value), String.Empty, PopulateUserset.Tables(0).Rows(0)("usp_old_pswd3"))
            If PopulateUserset.Tables(0).Rows(0)("usp_incentive_yn").Equals(DBNull.Value) Then
                rbtnIncApplicableN.Checked = True
            ElseIf PopulateUserset.Tables(0).Rows(0)("usp_incentive_yn").ToString.Trim = Constant.Common.ActiveStatus Then
                rbtnIncApplicableY.Checked = True
            ElseIf PopulateUserset.Tables(0).Rows(0)("usp_incentive_yn").ToString.Trim = Constant.Common.InActiveStatus Then
                rbtnIncApplicableN.Checked = True
            End If
            If PopulateUserset.Tables(0).Rows(0)("active").Equals(DBNull.Value) Then
                rbtnActiveY.Checked = True
            ElseIf PopulateUserset.Tables(0).Rows(0)("active") = Constant.Common.ActiveStatus Then
                rbtnActiveY.Checked = True
            ElseIf PopulateUserset.Tables(0).Rows(0)("active") = Constant.Common.InActiveStatus Then
                rbtnActiveN.Checked = True
            End If
            rbtnIntPasswordN.Checked = True
        End If
    End Sub
    Protected Sub ddlReportingTo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReportingTo1.SelectedIndexChanged
        PopulateReportingto2()
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim RowsAffected As Integer = InsertUserProfile()
        If RowsAffected = 1 Then

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert",
                "alert('Submitted Successfully'); window.location='UsrPrflListNewMod.aspx';",
                True)
            'Response.Redirect("~/UsrPrflListNewMod.aspx")
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then
            PopulateUserProfile()
        Else
            'Clear()
            Response.Redirect("~/UsrPrfileAddNewMod.aspx?UserId=New")
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/UsrPrflListNewMod.aspx?UserId=New")
    End Sub

    Protected Sub btnShowPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowPassword.Click
        lblPassword.Visible = True
    End Sub
End Class
