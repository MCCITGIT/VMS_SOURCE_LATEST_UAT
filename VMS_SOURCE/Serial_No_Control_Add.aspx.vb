'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Serial_No_Control_Add.aspx.vb
'Created Date	: 26-November-2007
'Created By	    : Arun 
'Version	    : R02.00.00
'Description	: Code behind for SerialNoControlAdd Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class Serial_No_Control_Add
    Inherits System.Web.UI.Page

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            AddAttributes()
            populateFinYear()
            populateDepartment()
            'populateBranch()
            LoadDepotName()
            populateDocType()
            If (Not Request.QueryString(Constant.SessionKeys.ID) Is Nothing) Then
                Dim srlid As Integer = CInt(Request.QueryString(Constant.SessionKeys.ID))
                btnSubmit.Text = Constant.GeneralMessages.btnUpdate
                'ddlTypeDoc.Attributes.Add("onChange", "return fnChangeFromToExists('Update','" + srlid.ToString + "');")
                ddlLocation.Attributes.Add("onChange", "return fnChangeFromToExists('Update','" + srlid.ToString + "');")
                PopulateSrlCntrl()
            Else
                'ddlTypeDoc.Attributes.Add("onChange", "return fnChangeFromToExists('Insert','0');")
                ddlLocation.Attributes.Add("onChange", "return fnChangeFromToExists('Insert','0');")
                btnSubmit.Text = Constant.GeneralMessages.Submit
                rdActiveYes.Checked = True
            End If

        End If
    End Sub
#End Region

#Region "AddAttributes"

    Private Sub AddAttributes()

        btnSubmit.Attributes.Add("onClick", "return ValidateSNCAControls();")
        txtNo.Attributes.Add("onKeyPress", "KeyPressNumeric()")
        txtIncrement.Attributes.Add("onKeyPress", "KeyPressNumeric()")
        ddlFinYear.Attributes.Add("onChange", "return fnChangeFromTo();")
        'ddlTypeDoc.Attributes.Add("onChange", "return fnChangeFromToExists();")



    End Sub
#End Region

#Region "Populate Fin Year"
    Public Sub populateFinYear()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim StatusTypeSet As New DataSet
        StatusTypeSet = ObjDocumentType.GetFinYrDetails(userInfo.userCompanyEntity, Constant.Common.ActiveStatus)
        If (Not (StatusTypeSet Is Nothing) AndAlso StatusTypeSet.Tables.Count > 0 AndAlso Not (StatusTypeSet.Tables(0) Is Nothing) AndAlso StatusTypeSet.Tables(0).Rows.Count > 0) Then
            ddlFinYear.DataSource = StatusTypeSet.Tables(0)
            ddlFinYear.DataTextField = "dis_fin_year"
            ddlFinYear.DataValueField = "fin_year"
            ddlFinYear.DataBind()
            Dim i As Integer
            For i = 0 To StatusTypeSet.Tables(0).Rows.Count - 1
                If (Convert.ToString(StatusTypeSet.Tables(0).Rows(i)("fin_current")) = Constant.Common.ActiveStatus) Then
                    Dim k As String = StatusTypeSet.Tables(0).Rows(i)("fin_year")
                    ddlFinYear.SelectedValue = k
                    Exit For
                End If
            Next
            ddlFinYear.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
#End Region

#Region "Populate DocType"
    Public Sub populateDocType()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim StatusTypeSet As New DataSet
        StatusTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, Constant.Common.Doc_Type, Constant.Common.ActiveStatus)
        If (Not (StatusTypeSet Is Nothing) AndAlso StatusTypeSet.Tables.Count > 0 AndAlso Not (StatusTypeSet.Tables(0) Is Nothing) AndAlso StatusTypeSet.Tables(0).Rows.Count > 0) Then
            ddlTypeDoc.DataSource = StatusTypeSet.Tables(0)
            ddlTypeDoc.DataTextField = "lov_value"
            ddlTypeDoc.DataValueField = "lov_code"
            ddlTypeDoc.DataBind()

            ddlTypeDoc.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
#End Region

#Region "populateDepartment"
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
            ddlDept.DataSource = OccupationTypeSet.Tables(0)
            ddlDept.DataMember = "lov_value"
            ddlDept.DataValueField = "lov_value"
            ddlDept.DataBind()
            ddlDept.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))

        End If
    End Sub
#End Region

#Region "PopulateLocation"
    Public Sub populateBranch()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim VendorTypeSet As New DataSet
        Dim LovType As String = Constant.Common.Lov_Vend_Branch
        VendorTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (VendorTypeSet Is Nothing) AndAlso VendorTypeSet.Tables.Count > 0 AndAlso Not (VendorTypeSet.Tables(0) Is Nothing) AndAlso VendorTypeSet.Tables(0).Rows.Count > 0) Then
            ddlLocation.DataSource = VendorTypeSet.Tables(0)
            ddlLocation.DataMember = "lov_code"
            ddlLocation.DataValueField = "lov_code"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))

        End If
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
            ddlLocation.DataSource = dataSortview
            ddlLocation.DataTextField = "depot_name"
            ddlLocation.DataValueField = "depot_code"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
            ddlLocation.SelectedValue = userInfo.userBranchEntity
        End If
        If Not userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Then
            ddlLocation.Enabled = False
        End If
    End Sub
#End Region

#Region "PopulateSrlCntrl"
    Public Sub PopulateSrlCntrl()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim PopulateSrlCtrl As New DataSet
        Dim SrlCtrl As New SerialControl
        Dim Year As String
        Year = Request.QueryString(Constant.SessionKeys.CurrentYear)
        Dim DocType As String
        DocType = Request.QueryString(Constant.SessionKeys.DOC)
        Dim srlid As String
        srlid = Request.QueryString(Constant.SessionKeys.ID)
        PopulateSrlCtrl = SrlCtrl.GetSerialCtrl(userInfo.userCompanyEntity, Year, DocType, srlid)
        If (Not (PopulateSrlCtrl Is Nothing) AndAlso PopulateSrlCtrl.Tables.Count > 0 AndAlso Not (PopulateSrlCtrl.Tables(0) Is Nothing) AndAlso PopulateSrlCtrl.Tables(0).Rows.Count > 0) Then

            ddlFinYear.SelectedValue = IIf(PopulateSrlCtrl.Tables(0).Rows(0)("srl_fin_year").Equals(DBNull.Value), String.Empty, PopulateSrlCtrl.Tables(0).Rows(0)("srl_fin_year"))
            ddlTypeDoc.SelectedValue = IIf(PopulateSrlCtrl.Tables(0).Rows(0)("srl_doc_type").Equals(DBNull.Value), String.Empty, PopulateSrlCtrl.Tables(0).Rows(0)("srl_doc_type"))
            ddlLocation.SelectedValue = IIf(PopulateSrlCtrl.Tables(0).Rows(0)("srl_branch").Equals(DBNull.Value), String.Empty, PopulateSrlCtrl.Tables(0).Rows(0)("srl_branch"))
            ddlDept.SelectedValue = IIf(PopulateSrlCtrl.Tables(0).Rows(0)("srl_dept").Equals(DBNull.Value), String.Empty, PopulateSrlCtrl.Tables(0).Rows(0)("srl_dept"))
            txtPrefix.Text = IIf(PopulateSrlCtrl.Tables(0).Rows(0)("srl_prefix").Equals(DBNull.Value), String.Empty, PopulateSrlCtrl.Tables(0).Rows(0)("srl_prefix"))
            txtNo.Text = IIf(PopulateSrlCtrl.Tables(0).Rows(0)("srl_no").Equals(DBNull.Value), String.Empty, PopulateSrlCtrl.Tables(0).Rows(0)("srl_no"))
            txtIncrement.Text = IIf(PopulateSrlCtrl.Tables(0).Rows(0)("srl_increment").Equals(DBNull.Value), String.Empty, PopulateSrlCtrl.Tables(0).Rows(0)("srl_increment"))

            If PopulateSrlCtrl.Tables(0).Rows(0)("active").Equals(DBNull.Value) Then
                rdActiveYes.Checked = True
                rdInActiveNo.Checked = False
            ElseIf PopulateSrlCtrl.Tables(0).Rows(0)("active").ToString.Trim = Constant.Common.ActiveStatus Then
                rdActiveYes.Checked = True
                rdInActiveNo.Checked = False
            ElseIf PopulateSrlCtrl.Tables(0).Rows(0)("active").ToString.Trim = Constant.Common.InActiveStatus Then
                rdInActiveNo.Checked = True
                rdActiveYes.Checked = False
            End If

        End If

    End Sub
#End Region

#Region "btnSubmit_Click"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim Year As String
        Year = ddlFinYear.SelectedValue
        Dim Doc As String
        Doc = ddlTypeDoc.SelectedValue
        Dim Loc As String
        Loc = ddlLocation.SelectedValue
        Dim Dept As String
        Dept = ddlDept.SelectedValue
        Dim Prefix As String
        Prefix = txtPrefix.Text
        Dim No As Integer
        No = txtNo.Text
        Dim Incr As Integer
        Incr = txtIncrement.Text
        Dim Status As String
        If (rdActiveYes.Checked = True) Then
            Status = Constant.Common.ActiveStatus
        Else
            Status = Constant.Common.InActiveStatus
        End If
        Dim srlid As String
        srlid = Request.QueryString(Constant.SessionKeys.ID)

        Dim Numrowsaffected As Integer

        'UPDATE 
        If (Not Request.QueryString(Constant.SessionKeys.ID) Is Nothing) Then
            Dim SrlCntrlUpdate As New SerialControl()
            Numrowsaffected = SrlCntrlUpdate.UpdateSrlCntrl(userInfo.userCompanyEntity, Year, Doc, Loc, Dept, Prefix, No, Status, userInfo.userIDEntity, Incr, srlid)
            'INSERT
        Else
            Dim SrlCntrlAdd As New SerialControl()
            Numrowsaffected = SrlCntrlAdd.InsertSrlCntrl(userInfo.userCompanyEntity, Year, Doc, Loc, Dept, Prefix, No, Status, userInfo.userIDEntity, Incr, srlid)
        End If

        If (Numrowsaffected > 0) Then
            Response.Redirect("~/Serial_No_Control_List.aspx")
        End If
    End Sub
#End Region

#Region "btnCancel_Click"
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Serial_No_Control_List.aspx")
    End Sub
#End Region

#Region "Reset button Click"

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If (Not Request.QueryString(Constant.SessionKeys.CurrentYear) Is Nothing) Then
            PopulateSrlCntrl()
        Else
            Response.Redirect("~/Serial_No_Control_Add.aspx")
        End If


    End Sub

#End Region

End Class
