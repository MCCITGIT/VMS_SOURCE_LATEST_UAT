'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : AddUpdateIndentEntry.aspx.vb
'Created Date	: 10-December-2011
'Created By	    : Rohan Mazumdar 
'Version	    : R02.00.00
'Description	: Code behind for AddUpdateIndentEntry Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO
Imports VMS.DataAccess

Partial Class AddUpdateIndentEntry_HO
    Inherits System.Web.UI.Page
    Dim totLtr As Integer = 0
    Dim totKg As Integer = 0
#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        'ScriptManager.GetCurrent(Me).RegisterPostBackControl(btnSubmit)


        If Not IsPostBack Then
            lblTotKg.Text = 0
            lblTotLtr.Text = 0
            PopulateRegionDropdown()
            PopulateDepotDropdown()

            'btnSubmit.Attributes.Add("onclick", "return validateSKUList();")
            btnSubmit.Attributes.Add("onclick", "return validateSKUList_vr1();")

            If Not (Request.QueryString.Count = 0) Then

                lblIndentNo.Text = Request.QueryString("IndentNo").ToString()
                lblIndentDate.Text = Request.QueryString("IndentDate").ToString()

                ddlRegion.SelectedValue = Request.QueryString("RegionCode").ToString()
                ddlRegion.Enabled = False

                ddlDepot.SelectedValue = Request.QueryString("DepotCode").ToString()
                ddlDepot.Enabled = False

                lblFinYear.Text = Request.QueryString("FinYear").ToString()
                lblFinMonth.Text = Request.QueryString("FinMonth").ToString()

                PopulateVendorUnit()
                'PopulateVendorUnitProduct()
                PopulateUnitProduct()
                ddlVendorUnit.Enabled = False
                'ddlVendorProduct.Enabled = False
                chkbxListproduct.Enabled = False

                PopulateGridEditMode(CType(lblIndentNo.Text, Integer))

                btnSubmit.Text = "Update"

                If Not (Request.QueryString("Approved").ToString() = String.Empty) Then
                    btnSubmit.Enabled = False
                    btnReset.Enabled = False
                End If

            Else

                lblIndentDate.Text = Format(DateTime.Now, "dd/MM/yyyy")

                lblFinYear.Text = GetStandardParameter(Constant.Common.StandardParameter_ProcessYear)
                lblFinMonth.Text = GetStandardParameter(Constant.Common.StandardParameter_ProcessMonth)

                PopulateVendorUnit()
                'PopulateVendorUnitProduct()
                PopulateUnitProduct()

            End If
            If ddlDepot.SelectedValue = "" Then
            Else
                PopulateAllActiveVendor()
            End If
        End If

    End Sub

#End Region
    Private Sub PopulateAllActiveVendor()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim vndprdIndentMaster As New IndentMaster()
        Dim dsVendorUnitProduct As DataSet

        ddlvndor.Items.Clear()

        Try

            dsVendorUnitProduct = vndprdIndentMaster.GetVendorList(ddlDepot.SelectedValue)

            If Not (dsVendorUnitProduct Is Nothing) Then

                If Not (dsVendorUnitProduct.Tables(0).Rows.Count = 0) Then

                    ddlvndor.DataSource = dsVendorUnitProduct
                    ddlvndor.DataTextField = "vendor_name"
                    ddlvndor.DataValueField = "vendor_code"
                    ddlvndor.DataBind()

                    'ddlVendorProduct.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
                    ddlvndor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                Else
                    ddlvndor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub
#Region "Populate Region dropdown."

    Private Sub PopulateRegionDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnRegion As New Common()
        Dim dsRegion As DataSet

        Try

            dsRegion = cmnRegion.GetLovDetails(userInfo.userCompanyEntity, Constant.Common.REGION_TYPE, Constant.Common.ActiveStatus)

            If Not (dsRegion Is Nothing) Then

                If Not (dsRegion.Tables(0).Rows.Count = 0) Then

                    ddlRegion.DataSource = dsRegion
                    ddlRegion.DataTextField = "Lov_Value"
                    ddlRegion.DataValueField = "Lov_Code"
                    ddlRegion.DataBind()

                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, "", True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                        ddlRegion.SelectedValue = userInfo.userRegionEntity
                        ddlRegion.Enabled = False
                    End If

                Else
                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate Depot dropdown."

    Private Sub PopulateDepotDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New Common()
        Dim dsDepot As DataSet

        ddlDepot.Items.Clear()

        Try

            dsDepot = cmnDepot.Getdepotname(ddlRegion.SelectedValue)

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlDepot.DataSource = dsDepot
                    ddlDepot.DataTextField = "depot_name"
                    ddlDepot.DataValueField = "depot_code"
                    ddlDepot.DataBind()

                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.REGION) Then
                        ddlDepot.SelectedValue = userInfo.userBranchEntity
                        ddlDepot.Enabled = False
                    End If

                Else
                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Get values for a particular Standard Parameter."

    Private Function GetStandardParameter(ByVal param_name As String) As String

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnStandardParameter As New Common()
        Dim dsStandardParameter As DataSet

        Dim result As String = String.Empty

        Try

            dsStandardParameter = cmnStandardParameter.GetStandardParameterValues(param_name)

            If Not (dsStandardParameter Is Nothing) Then

                If Not (dsStandardParameter.Tables(0).Rows.Count = 0) Then
                    result = dsStandardParameter.Tables(0).Rows(0)("param_char_value")
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

#Region "Populate Vendor Unit dropdown."

    Private Sub PopulateVendorUnit()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim vndIndentMaster As New IndentMaster()
        Dim dsVendorUnit As DataSet

        Dim indent_header As New IndentHeaderEntity()

        indent_header.IndentDepot = ddlDepot.SelectedValue

        ddlVendorUnit.Items.Clear()

        Try

            dsVendorUnit = vndIndentMaster.GetDstnctVendoratVndSkuMstr_HO()

            If Not (dsVendorUnit Is Nothing) Then

                If Not (dsVendorUnit.Tables(0).Rows.Count = 0) Then

                    ddlVendorUnit.DataSource = dsVendorUnit
                    ddlVendorUnit.DataTextField = "vendor_unit_name"
                    ddlVendorUnit.DataValueField = "vendor_unit_code"
                    ddlVendorUnit.DataBind()

                    ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                Else
                    ddlVendorUnit.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate Product dropdown."

    'Private Sub PopulateVendorUnitProduct()

    '    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '    If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '        userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
    '    Else
    '        Response.Redirect("~/Login.aspx")
    '    End If

    '    Dim vndprdIndentMaster As New IndentMaster()
    '    Dim dsVendorUnitProduct As DataSet

    '    Dim indent_header As New IndentHeaderEntity()
    '    indent_header.IndentDepot = ddlDepot.SelectedValue
    '    indent_header.IndentVendorUnit = ddlVendorUnit.SelectedValue

    '    'ddlVendorProduct.Items.Clear()

    '    Try

    '        dsVendorUnitProduct = vndprdIndentMaster.GetDstnctVendorProductatVndSkuMstr_HO()

    '        If Not (dsVendorUnitProduct Is Nothing) Then

    '            If Not (dsVendorUnitProduct.Tables(0).Rows.Count = 0) Then

    '                ddlVendorProduct.DataSource = dsVendorUnitProduct
    '                ddlVendorProduct.DataTextField = "prd_desc"
    '                ddlVendorProduct.DataValueField = "product_code"
    '                ddlVendorProduct.DataBind()

    '                'ddlVendorProduct.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
    '                ddlVendorProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

    '            Else
    '                ddlVendorProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    '            End If

    '        End If

    '    Catch ex As Exception

    '        Dim returnUrl As String = "~/ExceptionPage.aspx"
    '        Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
    '        Server.Transfer(returnUrl)

    '    End Try

    'End Sub

    Public Sub PopulateUnitProduct()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim indent_header As New IndentHeaderEntity()
        indent_header.IndentDepot = ddlDepot.SelectedValue
        indent_header.IndentVendorUnit = ddlVendorUnit.SelectedValue

        chkbxListproduct.Items.Clear()
        Dim vndprdIndentMaster As New IndentMaster()
        Dim dsVendorUnitProduct As DataSet

        dsVendorUnitProduct = vndprdIndentMaster.GetDstnctVendorProductatVndSkuMstr_HO()
        If (Not (dsVendorUnitProduct Is Nothing) AndAlso dsVendorUnitProduct.Tables.Count > 0 AndAlso Not (dsVendorUnitProduct.Tables(0) Is Nothing) AndAlso dsVendorUnitProduct.Tables(0).Rows.Count > 0) Then
            chkbxListproduct.DataSource = dsVendorUnitProduct.Tables(0)
            chkbxListproduct.DataTextField = "prd_desc"
            chkbxListproduct.DataValueField = "product_code"
            chkbxListproduct.DataBind()
            'chkbxListLocation.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If

    End Sub

#End Region

#Region "Populate SKU Codes gridview in case of New Indent Entry."

    Private Sub PopulateSKUCodes()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If Not chkbxListproduct.SelectedValue = String.Empty Then
            Dim vndprdskuIndentMaster As New IndentMaster()
            Dim dsProductSKUCodes As DataSet

            Dim strLocationCode As String = String.Empty

            For Each lstitm As ListItem In chkbxListproduct.Items
                If lstitm.Selected Then
                    If strLocationCode.Length = 0 Then
                        strLocationCode = lstitm.Value
                    Else
                        strLocationCode = lstitm.Value & "," & strLocationCode
                    End If
                End If
            Next

            Dim indent_header As New IndentHeaderEntity()

            indent_header.IndentDepot = ddlDepot.SelectedValue
            indent_header.IndentFinYear = lblFinYear.Text
            indent_header.IndentFinMonth = lblFinMonth.Text
            indent_header.IndentVendorUnit = ddlVendorUnit.SelectedValue
            indent_header.IndentProduct = strLocationCode

            Dim updateYN As String = String.Empty

            If Not (btnSubmit.Text = "Update") Then
                updateYN = Constant.Common.InActiveStatus
            Else
                updateYN = Constant.Common.ActiveStatus
            End If

            dsProductSKUCodes = vndprdskuIndentMaster.GetVendorProductSKUsatVndSkuMstr_HO(indent_header, updateYN)
            If (Not (dsProductSKUCodes Is Nothing)) Then
                gvIndentSKUList.Visible = True

                gvIndentSKUList.DataSource = dsProductSKUCodes.Tables(0)

                'Dim primary(0) As String

                'primary(0) = "bkd_booking_id"
                'gvMachineBooking.DataKeyNames = primary

                gvIndentSKUList.DataBind()
            End If
        Else
            gvIndentSKUList.Visible = True

            gvIndentSKUList.DataSource = Nothing

            gvIndentSKUList.DataBind()
        End If

    End Sub
#End Region

#Region "Populate Grid for edit mode."

    Private Sub PopulateGridEditMode(ByVal indent_id As Integer)

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim vndprdskuIndentMaster As New IndentMaster()
        Dim dsProductSKUCodes As DataSet

        Dim indent_header As New IndentHeaderEntity()

        indent_header.IndentDepot = ddlDepot.SelectedValue
        indent_header.IndentFinYear = lblFinYear.Text
        indent_header.IndentFinMonth = lblFinMonth.Text
        indent_header.IndentID = indent_id

        dsProductSKUCodes = vndprdskuIndentMaster.GetIndentDetails(indent_header)
        If (Not (dsProductSKUCodes Is Nothing)) Then
            gvIndentSKUList.Visible = True

            gvIndentSKUList.DataSource = dsProductSKUCodes.Tables(0)

            'Dim primary(0) As String

            'primary(0) = "bkd_booking_id"
            'gvMachineBooking.DataKeyNames = primary

            gvIndentSKUList.DataBind()


        End If
        ddlVendorUnit.SelectedValue = dsProductSKUCodes.Tables(0).Rows(0)("v_vendor_unit").ToString()
        'ddlVendorProduct.SelectedValue = dsProductSKUCodes.Tables(0).Rows(0)("product_code").ToString()

        For i = 0 To dsProductSKUCodes.Tables(1).Rows.Count - 1
            For Each lstitm As ListItem In chkbxListproduct.Items

                If Convert.ToString(dsProductSKUCodes.Tables(0).Rows(i)("product_code")) = lstitm.Value Then
                    lstitm.Selected = True
                End If
            Next
        Next

    End Sub

#End Region



#Region "Region dropdown SelectedIndexChanged Event"

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged

        PopulateDepotDropdown()
        PopulateVendorUnit()
        'PopulateVendorUnitProduct()
        PopulateUnitProduct()
        PopulateSKUCodes()

        lblErrorMessage.Text = ""

    End Sub

#End Region

#Region "Depot dropdown SelectedIndexChanged Event"

    Protected Sub ddlDepot_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepot.SelectedIndexChanged

        PopulateVendorUnit()
        'PopulateVendorUnitProduct()
        PopulateUnitProduct()
        PopulateSKUCodes()
        PopulateAllActiveVendor()
        lblErrorMessage.Text = ""

    End Sub

#End Region

#Region "Vendor unit dropdown SelectedIndexChanged Event"

    Protected Sub ddlVendorUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVendorUnit.SelectedIndexChanged

        'PopulateVendorUnitProduct()
        PopulateUnitProduct()
        PopulateSKUCodes()

        lblErrorMessage.Text = ""
    End Sub

#End Region

    '#Region "Vendor product dropdown SelectedIndexChanged Event"

    '    Protected Sub ddlVendorProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVendorProduct.SelectedIndexChanged

    '        PopulateSKUCodes()

    '        lblErrorMessage.Text = ""

    '    End Sub

    '#End Region

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim Extension As String = String.Empty ' Set the Response Content Type based on the file extension

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If ddlDepot.SelectedValue.Trim = String.Empty Then
            lblErrorMessage.Text = "Please Select a Depot."
            ddlDepot.Focus()
            Exit Sub
        ElseIf ddlVendorUnit.SelectedValue.Trim = String.Empty Then
            lblErrorMessage.Text = "Please Select Vendor Unit."
            ddlVendorUnit.Focus()
            Exit Sub
            'ElseIf ddlVendorProduct.SelectedValue.Trim = String.Empty Then
            '    lblErrorMessage.Text = "Please Select Product."
            '    ddlVendorProduct.Focus()
            '    Exit Sub
        End If


        Dim indntHeaderEntity As New IndentHeaderEntity()
        Dim indntDetailEntity As New IndentDetailEntity()

        Dim indntMaster As New IndentMaster()

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Dim RowsAffected As Integer
        Dim indent_id As Integer

        Try
            'Checking Access For Submit Button 
            ''''''''''''''''''''''''''''''''''''''''''''''''''
            If btnSubmit.Text = Constant.GeneralMessages.btnSubmit Then

                indntHeaderEntity.IndentDepot = ddlDepot.SelectedValue
                indntHeaderEntity.IndentFinYear = lblFinYear.Text
                indntHeaderEntity.IndentFinMonth = lblFinMonth.Text
                indntHeaderEntity.IndentVendorUnit = ddlVendorUnit.SelectedValue
                indntHeaderEntity.IndentCreatedUser = userInfo.userIDEntity

                Dim ds As DataSet = indntMaster.IsSerialMasterHasRecord(indntHeaderEntity)

                If (ds.Tables(0).Rows.Count > 0) Then
                    sqlConn = DBFactory.GetHelper.OpenConnection()
                    sqlTrans = sqlConn.BeginTransaction()

                    indent_id = indntMaster.InsertIndentHeader(indntHeaderEntity, sqlConn, sqlTrans)

                    If indent_id > 0 Then

                        For Each gvRow As GridViewRow In gvIndentSKUList.Rows

                            Dim hdnfldSKUCode As HiddenField
                            Dim hdnfldSKUUOM As HiddenField
                            Dim hdnfldSKUVol As HiddenField
                            Dim hdnrank As HiddenField

                            Dim lblPendingLoad As Label
                            Dim lblIndentToDate As Label
                            Dim lblDespatchToDate As Label

                            Dim txtNewLoad As TextBox
                            'Dim txtRemarks As TextBox
                            Dim ddlRemarks As DropDownList
                            Dim uploadAttachment As FileUpload

                            hdnfldSKUCode = gvRow.FindControl("hdnfldSKUCode")
                            hdnfldSKUUOM = gvRow.FindControl("hdnfldSKUUOM")
                            hdnfldSKUVol = gvRow.FindControl("hdnfldSKUVol")
                            hdnrank = gvRow.FindControl("hdnrank")

                            lblPendingLoad = gvRow.FindControl("lblPendingLoad")
                            lblIndentToDate = gvRow.FindControl("lblIndentToDate")
                            lblDespatchToDate = gvRow.FindControl("lblDespatchToDate")

                            txtNewLoad = gvRow.FindControl("txtNewLoad")
                            'txtRemarks = gvRow.FindControl("txtRemarks")
                            ddlRemarks = gvRow.FindControl("ddlReason")
                            uploadAttachment = gvRow.FindControl("upload_attachment")

                            Extension = GetFileExtension(uploadAttachment.FileName)

                            Dim DocsFileName As String = uploadAttachment.FileName
                            Dim DocPath As String = Format(Date.Now, "dd_MM_yyyy")
                            Dim DocCompletePath As String = DocPath + "/" + DocsFileName

                            If (CType(Trim(txtNewLoad.Text), Integer) > 0) Then
                                indntDetailEntity.IndentCreatedUser = userInfo.userIDEntity
                                indntDetailEntity.IndentDepot = indntHeaderEntity.IndentDepot
                                indntDetailEntity.IndentFinMonth = indntHeaderEntity.IndentFinMonth
                                indntDetailEntity.IndentFinYear = indntHeaderEntity.IndentFinYear
                                indntDetailEntity.IndentID = indent_id
                                indntDetailEntity.IndentSKUCode = Trim(Trim(hdnfldSKUCode.Value))
                                indntDetailEntity.IndentSKUDespatchToDate = CType(Trim(lblDespatchToDate.Text), Integer)
                                indntDetailEntity.IndentSKUIndentToDate = CType(Trim(lblIndentToDate.Text), Integer)
                                indntDetailEntity.IndentSKUNOP = CType(Trim(txtNewLoad.Text), Integer)
                                indntDetailEntity.IndentSKUPendingLoad = CType(Trim(lblPendingLoad.Text), Integer)
                                'indntDetailEntity.IndentSKURemarks = Trim(txtremarks.Text)
                                indntDetailEntity.IndentSKURemarks = Trim(ddlRemarks.SelectedValue)
                                indntDetailEntity.IndentSKUUOM = Trim(hdnfldSKUUOM.Value)
                                indntDetailEntity.IndentSKUVol = CType(Trim(hdnfldSKUVol.Value), Decimal)
                                indntDetailEntity.IndentVendorUnit = indntHeaderEntity.IndentVendorUnit
                                indntDetailEntity.IndentPriority = Trim(Trim(hdnrank.Value))

                                If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HO Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN) Then
                                    Dim sku_ds As DataSet = indntMaster.BlockIndent_SkuRecord(indntDetailEntity)
                                    Dim sku_count As Integer
                                    sku_count = Val(sku_ds.Tables(0).Rows(0)("sku_count").ToString())

                                    If (sku_count > 0) Then
                                        lblErrorMessage.Text = "Cannot raise indent as SKU is Slow Moving for All India."
                                        Exit Sub
                                    End If
                                End If

                                'RowsAffected += indntMaster.InsertIndentDetails(indntDetailEntity, sqlConn, sqlTrans)
                                RowsAffected += indntMaster.InsertIndentDetails_HO(indntDetailEntity, sqlConn, sqlTrans)

                                If (uploadAttachment.HasFile AndAlso uploadAttachment.PostedFile.ContentLength > 0) Then

                                    If (Path.GetExtension(uploadAttachment.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".img", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".jpg", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".jpeg", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".doc", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".gif", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".eml", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".msg", StringComparison.OrdinalIgnoreCase)) Then

                                        RowsAffected = indntMaster.Insert_Indent_Document(indent_id, DocsFileName, DocCompletePath, userInfo.userIDEntity, indntHeaderEntity.IndentDepot, lblFinYear.Text, lblFinMonth.Text, sqlConn, sqlTrans)
                                        If Not uploadAttachment.PostedFile Is Nothing And uploadAttachment.PostedFile.ContentLength > 0 Then

                                            Dim projectPath As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & userInfo.userCompanyEntity & "\" & "IndentHO_Docs" & "\" & DocPath
                                            Dim fn As String = System.IO.Path.GetFileName(uploadAttachment.PostedFile.FileName)
                                            Dim saveLocation As String = projectPath & "\" & fn
                                            Dim file As System.IO.FileInfo = New System.IO.FileInfo(saveLocation)

                                            If Not (Directory.Exists(projectPath)) Then
                                                Directory.CreateDirectory(projectPath)
                                            End If

                                            uploadAttachment.PostedFile.SaveAs(saveLocation)

                                        End If
                                    Else
                                        lblErrorMessage.Text = "Upload Correct File"
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next
                    End If

                    If RowsAffected > 0 Then
                        sqlTrans.Commit()
                    Else
                        sqlTrans.Rollback()
                    End If
                Else
                    lblErrorMessage.Text = "Indent No record for this depot is not present in the Serial Master. Please contact the administrator."
                End If

            ElseIf btnSubmit.Text = Constant.GeneralMessages.btnUpdate Then

                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                indntHeaderEntity.IndentDepot = ddlDepot.SelectedValue
                indntHeaderEntity.IndentFinYear = lblFinYear.Text
                indntHeaderEntity.IndentFinMonth = lblFinMonth.Text
                indntHeaderEntity.IndentVendorUnit = ddlVendorUnit.SelectedValue
                indntHeaderEntity.IndentID = CType(Trim(lblIndentNo.Text), Integer)
                indntHeaderEntity.IndentCreatedUser = userInfo.userIDEntity

                indntMaster.DeleteIndentHeaderandDetails(indntHeaderEntity, sqlConn, sqlTrans)

                indntMaster.DeleteIndent_Doc_dDetails(indntHeaderEntity, sqlConn, sqlTrans)

                RowsAffected = indntMaster.ModifyIndentHeader(indntHeaderEntity, sqlConn, sqlTrans)

                If (RowsAffected > 0) Then

                    For Each gvRow As GridViewRow In gvIndentSKUList.Rows
                        Dim hdnfldSKUCode As HiddenField
                        Dim hdnfldSKUUOM As HiddenField
                        Dim hdnfldSKUVol As HiddenField
                        Dim hdnrank As HiddenField

                        Dim lblPendingLoad As Label
                        Dim lblIndentToDate As Label
                        Dim lblDespatchToDate As Label

                        Dim txtNewLoad As TextBox
                        'Dim txtRemarks As TextBox
                        Dim ddlRemarks As DropDownList
                        Dim uploadAttachment As FileUpload

                        hdnfldSKUCode = gvRow.FindControl("hdnfldSKUCode")
                        hdnfldSKUUOM = gvRow.FindControl("hdnfldSKUUOM")
                        hdnfldSKUVol = gvRow.FindControl("hdnfldSKUVol")
                        hdnrank = gvRow.FindControl("hdnrank")

                        lblPendingLoad = gvRow.FindControl("lblPendingLoad")
                        lblIndentToDate = gvRow.FindControl("lblIndentToDate")
                        lblDespatchToDate = gvRow.FindControl("lblDespatchToDate")

                        txtNewLoad = gvRow.FindControl("txtNewLoad")
                        'txtRemarks = gvRow.FindControl("txtRemarks")
                        ddlRemarks = gvRow.FindControl("ddlReason")
                        uploadAttachment = gvRow.FindControl("upload_attachment")

                        Extension = GetFileExtension(uploadAttachment.FileName)

                        Dim DocsFileName As String = uploadAttachment.FileName
                        Dim DocPath As String = Format(Date.Now, "dd_MM_yyyy")
                        Dim DocCompletePath As String = DocPath + "/" + DocsFileName

                        If (CType(Trim(txtNewLoad.Text), Integer) > 0) Then
                            indntDetailEntity.IndentCreatedUser = userInfo.userIDEntity
                            indntDetailEntity.IndentDepot = indntHeaderEntity.IndentDepot
                            indntDetailEntity.IndentFinMonth = indntHeaderEntity.IndentFinMonth
                            indntDetailEntity.IndentFinYear = indntHeaderEntity.IndentFinYear
                            indntDetailEntity.IndentID = indntHeaderEntity.IndentID
                            indntDetailEntity.IndentSKUCode = Trim(hdnfldSKUCode.Value)
                            indntDetailEntity.IndentSKUDespatchToDate = CType(Trim(lblDespatchToDate.Text), Integer)
                            indntDetailEntity.IndentSKUIndentToDate = CType(Trim(lblIndentToDate.Text), Integer)
                            indntDetailEntity.IndentSKUNOP = CType(Trim(txtNewLoad.Text), Integer)
                            indntDetailEntity.IndentSKUPendingLoad = CType(Trim(lblPendingLoad.Text), Integer)
                            'indntDetailEntity.IndentSKURemarks = Trim(txtRemarks.Text)
                            indntDetailEntity.IndentSKURemarks = Trim(ddlRemarks.SelectedValue)
                            indntDetailEntity.IndentSKUUOM = Trim(hdnfldSKUUOM.Value)
                            indntDetailEntity.IndentSKUVol = CType(Trim(hdnfldSKUVol.Value), Decimal)
                            indntDetailEntity.IndentVendorUnit = indntHeaderEntity.IndentVendorUnit
                            indntDetailEntity.IndentPriority = Trim(Trim(hdnrank.Value))

                            'RowsAffected += indntMaster.InsertIndentDetails(indntDetailEntity, sqlConn, sqlTrans)
                            RowsAffected += indntMaster.InsertIndentDetails_HO(indntDetailEntity, sqlConn, sqlTrans)

                            If (uploadAttachment.HasFile AndAlso uploadAttachment.PostedFile.ContentLength > 0) Then

                                If (Path.GetExtension(uploadAttachment.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".img", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".jpg", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".jpeg", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".doc", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".gif", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".eml", StringComparison.OrdinalIgnoreCase)) OrElse
                                    (Path.GetExtension(uploadAttachment.FileName).Equals(".msg", StringComparison.OrdinalIgnoreCase)) Then

                                    RowsAffected = indntMaster.Insert_Indent_Document(indntHeaderEntity.IndentID, DocsFileName, DocCompletePath, userInfo.userIDEntity, indntHeaderEntity.IndentDepot, lblFinYear.Text, lblFinMonth.Text, sqlConn, sqlTrans)

                                    If Not uploadAttachment.PostedFile Is Nothing And uploadAttachment.PostedFile.ContentLength > 0 Then

                                        Dim projectPath As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & userInfo.userCompanyEntity & "\" & "IndentHO_Docs" & "\" & DocPath
                                        Dim fn As String = System.IO.Path.GetFileName(uploadAttachment.PostedFile.FileName)
                                        Dim saveLocation As String = projectPath & "\" & fn
                                        Dim file As System.IO.FileInfo = New System.IO.FileInfo(saveLocation)

                                        If Not (Directory.Exists(projectPath)) Then
                                            Directory.CreateDirectory(projectPath)
                                        End If

                                        uploadAttachment.PostedFile.SaveAs(saveLocation)

                                    End If
                                Else
                                    lblErrorMessage.Text = "Upload Correct File"
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                End If

                If RowsAffected > 0 Then
                    sqlTrans.Commit()
                Else
                    sqlTrans.Rollback()
                End If

            End If

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        Finally
            If Not (sqlConn Is Nothing) Then
                'sqlConn is set to close state after completing the transaction
                sqlConn.Close()
            End If
        End Try

        If RowsAffected > 0 Then
            Response.Redirect("~/IndentsList_HO.aspx", True)
        End If

    End Sub

    Protected Sub gvIndentSKUList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIndentSKUList.RowDataBound
        If (e.Row.RowType = DataControlRowType.Header) Then

            Dim userInfo As VMSUserEntity = New VMSUserEntity()
            If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
                userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

                Dim userDetailsObject As New UserLogin()
                Dim ds As DataSet
                Try
                    ds = userDetailsObject.GetLastStockUpdateDate()
                    If (ds.Tables(0).Rows.Count > 0) Then
                        e.Row.Cells(3).Text = "Stock as on <br /> <b style='color:red;'>" + ds.Tables(0).Rows(0)(0).ToString() + "</b>"
                    End If
                Catch ex As Exception
                    Dim returnUrl As String = "~/ExceptionPage.aspx"
                    Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    Server.Transfer(returnUrl)
                End Try

            End If

        ElseIf (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim txtNewLoad As TextBox = CType(e.Row.FindControl("txtNewLoad"), TextBox)
            'Dim txtRemarks As TextBox = CType(e.Row.FindControl("txtRemarks"), TextBox)
            Dim ddlReason As DropDownList = CType(e.Row.FindControl("ddlReason"), DropDownList)
            Dim hdnfldTSl As HiddenField = CType(e.Row.FindControl("hdnfldTSl"), HiddenField)
            Dim lblIndentToDate As Label = CType(e.Row.FindControl("lblIndentToDate"), Label)
            Dim lblPercentage As Label = CType(e.Row.FindControl("lblPercentage"), Label)

            Dim lblSKUCode As Label = CType(e.Row.FindControl("lblSKUCode"), Label)
            lblSKUCode.Text = "(" + rowView("v_sku_code") + ") <br />" + rowView("sku_desc")
            Dim hdnfldSKUUOM As HiddenField = e.Row.FindControl("hdnfldSKUUOM")
            Dim hdnfldSKUVol As HiddenField = e.Row.FindControl("hdnfldSKUVol")
            Dim hdnrank As HiddenField = e.Row.FindControl("hdnrank")
            Dim uploadAttachment As FileUpload = CType(e.Row.FindControl("upload_attachment"), FileUpload)

            'If ddlReason IsNot Nothing Then
            '    ScriptManager.GetCurrent(Page).RegisterPostBackControl(ddlReason)

            'End If

            Dim skucode As String
            skucode = rowView("v_sku_code")

            Dim sku_ds As DataSet
            Dim objIndent As New IndentMaster
            Dim Sku_Code As String = String.Empty

            sku_ds = objIndent.Applicable_SkuRecord(skucode)

            If sku_ds IsNot Nothing AndAlso sku_ds.Tables.Count > 0 AndAlso sku_ds.Tables(0).Rows.Count > 0 Then
                Sku_Code = sku_ds.Tables(0).Rows(0)("HasSku").ToString()
            End If

            If Sku_Code = "1" Then
                If ddlReason IsNot Nothing Then
                    LoadDDLDetails(ddlReason, "VENDOR_REASON_TYPE", Constant.Common.ActiveStatus)
                End If
            Else
                ddlReason.Visible = False
            End If

            Dim dsVendor As DataSet

            Dim VendorCode As String
            Dim vendorExists As Boolean = False
            Dim vendorPosition As String = String.Empty

            VendorCode = ddlVendorUnit.SelectedValue

            dsVendor = objIndent.Minimum_RateVendorList(ddlDepot.SelectedValue, skucode, VendorCode)

            If dsVendor IsNot Nothing AndAlso dsVendor.Tables.Count > 0 AndAlso dsVendor.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In dsVendor.Tables(0).Rows
                    If dr("vendor_code").ToString().Trim() = VendorCode.Trim() Then
                        vendorExists = True
                        vendorPosition = dr("rnk").ToString()
                        Exit For
                    End If
                Next
            End If

            If Not (Request.QueryString("Approved") Is Nothing) Then
                If (Request.QueryString("Approved").ToString() = String.Empty) Then
                    'txtNewLoad.Attributes.Add("onkeypress", "KeyPressNumeric();")
                    txtNewLoad.Attributes.Add("onblur", "return calculatePercentage('" + hdnfldTSl.Value + "', '" + lblIndentToDate.Text + "', '" + lblPercentage.ClientID + "', '" + txtNewLoad.ClientID + "');")
                End If
            End If


            If Not (Request.QueryString.Count = 0) Then

                txtNewLoad.Text = rowView("indd_sku_nop")
                'txtRemarks.Text = rowView("indd_remarks")

                'If (CType(hdnfldTSl.Value, Decimal) <> 0) Then
                '    lblPercentage.Text = Convert.ToInt32(((CType(rowView("indd_sku_nop"), Decimal) + CType(lblIndentToDate.Text, Decimal)) * 100) / CType(hdnfldTSl.Value, Decimal)).ToString + " %"
                'End If

                'Total Calculation By Riddhi------------------------------
                If rowView("sku_uom") = "K" Then
                    Dim packSize As Integer = Convert.ToInt64(rowView("sku_volume"))
                    totKg = totKg + (Convert.ToInt64(rowView("indd_sku_nop")) * packSize)
                    lblTotKg.Text = totKg
                End If
                If rowView("sku_uom") = "L" Then
                    Dim packSize As Integer = Convert.ToInt64(rowView("sku_volume"))
                    totLtr = totLtr + (Convert.ToInt64(rowView("indd_sku_nop")) * packSize)
                    lblTotLtr.Text = totLtr
                End If
                '---------------------------------------------------------
                If rowView("vendor_rank") <> "" Then
                    hdnrank.Value = rowView("vendor_rank")
                End If

                If rowView("indd_remarks").ToString() = "L1" Then
                    ddlReason.SelectedValue = "L1"
                    ddlReason.Enabled = False
                Else
                    ddlReason.Items.Remove("L1")
                    If Not String.IsNullOrEmpty(rowView("indd_remarks").ToString()) Then
                        ddlReason.SelectedValue = rowView("indd_remarks").ToString()
                    End If
                    ddlReason.Enabled = True
                End If

                If rowView("indd_remarks").ToString() = "AsPerPlan" Then
                    uploadAttachment.Visible = True
                Else
                    uploadAttachment.Visible = False
                End If
            Else
                If dsVendor IsNot Nothing AndAlso dsVendor.Tables.Count > 0 AndAlso dsVendor.Tables(0).Rows.Count > 0 Then
                    hdnrank.Value = dsVendor.Tables(0).Rows(0)("rnk").ToString()
                End If

                If vendorExists Then
                    If vendorPosition = "1" Then
                        ddlReason.SelectedValue = "L1"
                        ddlReason.Enabled = False


                    Else
                        ddlReason.Items.Remove("L1")
                        ddlReason.Enabled = True
                    End If
                Else
                    ddlReason.Items.Remove("L1")
                    ddlReason.Enabled = True
                End If
                uploadAttachment.Visible = False
                txtNewLoad.Text = "0"

                'txtNewLoad.Attributes.Add("onkeypress", "KeyPressNumeric();")
                txtNewLoad.Attributes.Add("onblur", "return calculatePercentage('" + hdnfldTSl.Value + "', '" + lblIndentToDate.Text + "', '" + lblPercentage.ClientID + "', '" + txtNewLoad.ClientID + "');")

            End If

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/IndentsList_HO.aspx", True)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Dim path = "~/AddUpdateIndentEntry_HO.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub





    'Private Sub GetPoNumber()
    '    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    '    If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
    '        userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
    '    Else
    '        Response.Redirect("~/Login.aspx")
    '    End If

    '    Dim ds As New DataSet()
    '    Dim obj As New IndentMaster()
    '    Dim dt As New DataTable
    '    Dim PoNumber As String = String.Empty

    '    Try
    '        ds = obj.GetPoNumber(ddlvndor.SelectedValue, ddlDepot.SelectedValue)
    '        If ((ds IsNot Nothing) AndAlso ds.Tables.Count > 0) Then
    '            If ((ds.Tables(0) IsNot Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then

    '                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '                    PoNumber = PoNumber + ds.Tables(0).Rows(i)("pm_po_no").ToString() + ","
    '                Next
    '                PoNumber = PoNumber.Remove(PoNumber.Length - 1, 1)
    '                hdnpo.Value = PoNumber
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim Msg As String = ex.Message
    '    End Try
    'End Sub
    Protected Sub lnkadd_Click(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        Dim row As DataRow
        lblMsg.Text = ""

        dt.Columns.Add("slno")
        dt.Columns.Add("vendorcode")
        dt.Columns.Add("vendorname")
        dt.Columns.Add("depotcode")
        dt.Columns.Add("depotname")
        dt.Columns.Add("skucode")
        dt.Columns.Add("skuname")
        dt.Columns.Add("remarks")

        Try
            Dim tbl As DataTable = New DataTable()
            If ViewState("table") IsNot Nothing Then
                tbl = ViewState("table")
            End If
            If tbl IsNot Nothing AndAlso tbl.Rows.Count > 0 Then
                Dim foundsku As DataRow() = tbl.Select("skuname = '" + txtsku.Text.Trim() + "'")
                If foundsku.Length <> 0 Then
                    txtsku.Text = ""
                    lblMsg.Text = "Already Added SKU in the below list."
                    lblMsg.ForeColor = Drawing.Color.Red
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalScript", "showmodal();", True)
                    Return
                End If
                dt = ViewState("table")
                row = dt.NewRow
                row("slno") = tbl.Rows.Count + 1
                row("vendorcode") = ddlvndor.SelectedValue
                row("vendorname") = ddlvndor.SelectedItem.Text.Trim() & "(" & ddlvndor.SelectedValue & ")"
                row("depotcode") = ddlDepot.SelectedValue
                row("depotname") = ddlDepot.SelectedItem.Text.Trim()
                row("skucode") = hdnskucodes.Value
                row("skuname") = txtsku.Text.Trim()
                row("remarks") = txtremarks.Text.Trim()
            Else
                row = dt.NewRow
                row("slno") = 1
                row("vendorcode") = ddlvndor.SelectedValue
                row("vendorname") = ddlvndor.SelectedItem.Text.Trim() & "(" & ddlvndor.SelectedValue & ")"
                row("depotcode") = ddlDepot.SelectedValue
                row("depotname") = ddlDepot.SelectedItem.Text.Trim()
                row("skucode") = hdnskucodes.Value
                row("skuname") = txtsku.Text.Trim()
                row("remarks") = txtremarks.Text.Trim()
            End If
            dt.Rows.Add(row)
            dt.AcceptChanges()
            ViewState("table") = dt

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                btnaddsku.Visible = True
                skudetails.Visible = True
                gvskudtls.DataSource = dt
                gvskudtls.DataBind()
                txtremarks.Text = ""
                txtsku.Text = ""
            Else
                ViewState("table") = Nothing
                gvskudtls.DataSource = Nothing
                gvskudtls.DataBind()
                btnaddsku.Visible = False
            End If
            ddlvndor.Enabled = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalScript", "showmodal();", True)
        Catch ex As Exception
            Dim Message As String = ex.Message
        End Try
    End Sub
    Protected Sub gvskudtls_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvIndentSKUList.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim lnkdelete As LinkButton = CType(e.Row.FindControl("lnkdelete"), LinkButton)

            If Not (lnkdelete Is Nothing) Then
                lnkdelete.Attributes.Add("onclick", "return validateDelete( '" + lnkdelete.ClientID + "');")
            End If

        End If
    End Sub
    Protected Sub gvskudtls_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "itemdelete" Then

            Dim row As GridViewRow = CType((CType(e.CommandSource, Control)).NamingContainer, GridViewRow)
            Dim rowIndex As Integer = row.RowIndex
            Dim skuname As String = (TryCast(gvskudtls.Rows(rowIndex).FindControl("lblsku"), Label)).Text

            Dim dt As DataTable = ViewState("table")
            gvskudtls.DataSource = Nothing
            gvskudtls.DataBind()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim dr As DataRow() = dt.Select("skuname = '" + skuname + "'")
                If dr.Length <> 0 Then
                    For Each drow In dr
                        drow.Delete()
                    Next
                End If

                dt.AcceptChanges()

            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                btnaddsku.Visible = True
                skudetails.Visible = True
                gvskudtls.DataSource = dt
                gvskudtls.DataBind()
                txtremarks.Text = ""
                txtsku.Text = ""
            Else
                ViewState("table") = Nothing
                gvskudtls.DataSource = Nothing
                gvskudtls.DataBind()
                btnaddsku.Visible = False
                gvskudtls.Visible = False
                ddlvndor.Enabled = True
                ddlvndor.SelectedIndex = 0
            End If
            ViewState("table") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalScript", "showmodal();", True)
        End If
    End Sub
    <System.Web.Script.Services.ScriptMethod()>
    <System.Web.Services.WebMethod()>
    Public Shared Function SKUSearch(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Dim vendorcode As String = String.Empty
        Dim depotcode As String = String.Empty
        Dim SKUdetails As List(Of String) = New List(Of String)()

        If Not String.IsNullOrEmpty(contextKey) Then
            Dim parts As String() = contextKey.Split("|"c)
            If parts.Length > 0 Then
                vendorcode = parts(0)
            End If
            If parts.Length > 1 Then
                depotcode = parts(1)
            End If
        End If

        If prefixText.Length >= 3 Then
            Try
                Dim ms As IndentMaster = New IndentMaster()
                Dim ds As DataSet = ms.GetSKU(prefixText, vendorcode, depotcode)

                If ((ds IsNot Nothing) AndAlso ds.Tables.Count > 0) Then
                    If ((ds.Tables(0) IsNot Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            SKUdetails.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                                ds.Tables(0).Rows(i)("sku_desc").ToString(),
                                ds.Tables(0).Rows(i)("sku_code").ToString()))
                        Next
                    End If
                End If
            Catch ex As Exception

            End Try
        End If

        Return SKUdetails.ToArray()
    End Function

    <System.Web.Script.Services.ScriptMethod()>
    <System.Web.Services.WebMethod()>
    Public Shared Function SKUCodeSearch(ByVal skucode As String, ByVal vendorcode As String, ByVal depotcode As String) As List(Of Object)
        Dim ms As IndentMaster = New IndentMaster()
        Dim SKUDetails As List(Of Object) = New List(Of Object)()

        Try
            Dim ds As DataSet = ms.GetSKU(skucode, vendorcode, depotcode)

            If ((ds IsNot Nothing) AndAlso ds.Tables.Count > 0) Then

                If ((ds.Tables(0) IsNot Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        SKUDetails.Add(New Object() {ds.Tables(0).Rows(i)("sku_code"), ds.Tables(0).Rows(i)("sku_desc")})
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
        Return SKUDetails
    End Function
    Protected Sub btnaddsku_Click(sender As Object, e As EventArgs)
        Dim obj As New IndentMaster()
        Dim AffectedRow As Integer

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        dt1.Columns.Add("SKUName")
        dt1.Columns.Add("SKU")
        Dim tbl As DataTable = ViewState("table")
        'Dim dr As DataRow
        Dim row As DataRow

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()


            ''Dim PONoList As String() = hdnpo.Value.Split(", ")
            '' For Each pono As String In PONoList
            For i As Integer = 0 To tbl.Rows.Count - 1
                row = dt1.NewRow
                row("SKUName") = tbl.Rows(i)("skuname").ToString()
                row("SKU") = tbl.Rows(i)("skucode").ToString()
                dt1.Rows.Add(row)
            Next
            dt1.AcceptChanges()
            '' Next
            If dt1.Rows.Count > 0 Then
                For j As Integer = 0 To dt1.Rows.Count - 1
                    If dt1.Rows(j)("SKU").ToString() = "" Then
                        lblMsg.Visible = True
                        lblMsg.Text = "Please Add Valid SKU."
                        lblMsg.ForeColor = System.Drawing.Color.Red
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalScript", "showmodal();", True)
                        Return
                    End If
                Next
            End If
            AffectedRow = obj.insertAdditionalRequest(dt1, hdndepot.Value, ddlvndor.SelectedValue, txtremarks.Text, userInfo.userIDEntity, sqlConn, sqlTrans)



            If AffectedRow > 0 Then
                sqlTrans.Commit()
                lblMsg.Visible = True
                lblMsg.Text = "Additional Sku Information Inserted Successfully"
                lblMsg.ForeColor = Drawing.Color.Green
                SendMail(dt1, ddlDepot.SelectedItem.Text, ddlvndor.SelectedItem.Text)
                btnaddsku.Visible = False
                ddlvndor.SelectedIndex = 0
                ddlvndor.Enabled = True
            Else
                sqlTrans.Rollback()
                lblMsg.Visible = True
                lblMsg.Text = "Something Went Wrong !"
                lblMsg.ForeColor = Drawing.Color.Red
            End If
            txtremarks.Text = ""
            txtsku.Text = ""

            ViewState("table") = Nothing
            '' GetAdditionalSKUDetails()
            skudetails.Visible = False
            gvskudtls.DataSource = Nothing
            gvskudtls.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalScript", "showmodal();", True)

        Catch ex As Exception
            Dim Msg As String = ex.Message
        End Try
    End Sub
    Private Sub SendMail(ByVal dt As DataTable, ByVal DepotCode As String, ByVal VendorCode As String)

        Dim objemail As EmailSMSsender = New EmailSMSsender()
        Dim mailsubject As String = String.Empty
        Dim mailBody As String = String.Empty
        Dim ToAddress As String = String.Empty
        Dim CCAddress As String = String.Empty
        Dim BCCAddress As String = String.Empty

        Dim responseMail As String = String.Empty

        Dim siteId As String = String.Empty
        Dim PONO As String = String.Empty
        Dim Ds As DataSet = New DataSet()
        Dim Ds1 As DataSet = New DataSet()
        Dim obj As New IndentMaster()

        Ds = obj.GetSiteId(ddlDepot.SelectedValue, ddlvndor.SelectedValue)

        If (Not (Ds Is Nothing) AndAlso Ds.Tables.Count > 0 AndAlso Not (Ds.Tables(0) Is Nothing) AndAlso Ds.Tables(0).Rows.Count > 0) Then
            siteId = Ds.Tables(0).Rows(0)("site_name").ToString()
            PONO = Ds.Tables(0).Rows(0)("PONO").ToString()
        End If
        If dt.Rows.Count > 0 Then
            Dim obj1 As POLinkingRequestClass = New POLinkingRequestClass()
            Ds1 = obj1.GetToMailAddress()
            If (Not (Ds1 Is Nothing) AndAlso Ds1.Tables.Count > 0 AndAlso Not (Ds1.Tables(0) Is Nothing) AndAlso Ds1.Tables(0).Rows.Count > 0) Then

                ToAddress = Ds1.Tables(0).Rows(0)("TOAddr").ToString()
                CCAddress = Ds1.Tables(0).Rows(0)("CCAddr").ToString()
                BCCAddress = Ds1.Tables(0).Rows(0)("BCCAddr").ToString()
            End If

            ''ToAddress = "ranjitchakraborty@bergerindia.com,suranjanbasu@bergerindia.com,arijitdasgupta@bergerindia.com,souvikchakravarty@bergerindia.com,bijendarsingh@bergerindia.com,diptangshudey@bergerindia.com,rasenjitdas@bergerindia.com"
            'ToAddress = "ranjitchakraborty@bergerindia.com,suranjanbasu@bergerindia.com,arijitdasgupta@bergerindia.com,souvikchakravarty@bergerindia.com,bijendarsingh@bergerindia.com,diptangshudey@bergerindia.com,rasenjitdas@bergerindia.com,accountstrainee@bergerindia.com"
            'CCAddress = ""
            'BCCAddress = "benimadhab.samanta@mccit.co.in"

            mailBody = "<p>Please find below the SKU details which need to be linked with the PO."
            'mailBody += "<table style='border:1px solid black;'>"
            mailBody += "<table style='border:1px solid black;'>" &
                           "<thead>" &
                              "<th colspan='6' style='border:1px solid black;padding: 5px;'>PO Linking Request</th>" &
                          "</thead>"
            mailBody += "<tbody>" &
                          "<tr>" &
                           "<td style='border:1px solid black;padding: 5px;'>Depot Name</td>" &
                           "<td style='border:1px solid black;padding: 5px;'>" & DepotCode & "</td>" &
                          "</tr>" &
                          "<tr>" &
                           "<td style='border:1px solid black;padding: 5px;'>Vendor Site</td>" &
                           "<td style='border:1px solid black;padding: 5px;'>" & VendorCode & "</td>" &
                          "</tr>" &
                          "<tr>" &
                           "<td style='border:1px solid black;padding: 5px;'>Site Id</td>" &
                           "<td style='border:1px solid black;padding: 5px;'>" & siteId & "</td>" &
                          "</tr>" &
                          "<tr>" &
                          "<td style='border:1px solid black;padding: 5px;'>PO No</td>" &
                           "<td style='border:1px solid black;padding: 5px;'>" & PONO & "</td>" &
                           "</tr>" &
                       "</tbody>"

            mailBody += "<tbody>" &
                           "<th colspan='2' style='border:1px solid black;padding: 5px;'>SKU Code</th>" &
                       "</tbody>"

            For i As Integer = 0 To dt.Rows.Count - 1
                mailBody += "<tbody>" &
                           "<td colspan='2' style='border:1px solid black;padding: 5px;' align='center'>" & dt.Rows(i)("SKUName") & "</td>" &
                       "</tbody>"
            Next

            mailBody += "</table></table></p>"
            mailBody += "<br><br><h4 style='display:block;width:100%;text-align:center;color: darkred;'>**This is an auto-generated mail.Please do not reply to this mail**</h4>"
            mailsubject = "SKU Linking request against PO"

            Dim mailobj As EmailSMSsender = New EmailSMSsender()
            Dim mailEntity As MailEntity = New MailEntity()
            If ToAddress <> "" Then
                mailEntity.ToAddress = ToAddress
                mailEntity.CCAddress = CCAddress
                mailEntity.BCCAddress = BCCAddress
                mailEntity.MailSubject = mailsubject
                mailEntity.MailBody = mailBody
                mailEntity.Sender_Task = "MailSendToPO"
                Dim recipNo As Integer = mailobj.sendMail(mailEntity)
                If recipNo = 0 Then
                    responseMail = "Email Sent Failed"
                    lblMsg.Text = responseMail
                    lblMsg.ForeColor = System.Drawing.Color.Red
                Else
                    'lblMsg.Text = responseMail
                    'lblMsg.ForeColor = System.Drawing.Color.Green
                End If
            End If
            '' objemail.sendMail(Convert.ToString(projectid), mailsubject, mailBody, ToAddress, CCAddress, BCCAddress)
        End If
    End Sub
    Protected Sub chkbxListproduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateSKUCodes()

        lblErrorMessage.Text = ""
    End Sub
    Protected Sub ddlReason_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlReason As DropDownList = CType(sender, DropDownList)
        Dim row As GridViewRow = CType(ddlReason.NamingContainer, GridViewRow)
        Dim uploadAttachment As FileUpload = CType(row.FindControl("upload_attachment"), FileUpload)

        If ddlReason.SelectedValue = "AsPerPlan" Then
            uploadAttachment.Visible = True
        Else
            uploadAttachment.Visible = False
        End If
    End Sub
#Region "Get File Extension"

    ' Gets the File extension from the file Name
    Private Function GetFileExtension(ByVal fileName As String) As String
        Dim extension As String = String.Empty
        If (fileName.LastIndexOf(".") >= 0) Then
            extension = fileName.Substring(fileName.LastIndexOf(".") + 1)
        End If
        Return extension
    End Function

#End Region
    Private Sub LoadDDLDetails(ByVal ddlName As DropDownList, ByVal LovType As String, ByVal ActiveStatus As String)
        Dim ObjDocumentType As New Common
        Dim ds As New DataSet

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        ds = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, Constant.Common.VENDOR_REASON_TYPE, Constant.Common.ActiveStatus)

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlName.DataSource = ds.Tables(0)
            ddlName.DataTextField = "lov_value"
            ddlName.DataValueField = "lov_code"
            ddlName.DataBind()
            ddlName.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If
    End Sub
End Class
