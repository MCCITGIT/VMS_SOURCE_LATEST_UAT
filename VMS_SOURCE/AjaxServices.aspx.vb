'****************************************************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : AjaxService.aspx.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.01.00
'Description	: Code behind file for Ajax Implementation
'
' Modified By       Modified On           Version               Reason
'****************************************************************************************

Imports System.Data
Imports VMS.Web
Partial Class AjaxServices
    Inherits System.Web.UI.Page
    Dim Company As String
    'Dim ObjProjCreation As New ProjectCreation
    Dim ObjDocumentType As New Common

#Region "Page Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim code As String = Request.QueryString("Code")
        Dim returnString As String = String.Empty

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
            Company = userInfo.userCompanyEntity
        Else
            Response.Redirect("~/Login.aspx")
        End If


        Select Case code
            Case Constant.AjaxServices.UserId
                Dim userId As String
                userId = Request.QueryString("prjCode")
                returnString = GetUserIdCheck(userId)
            Case Constant.AjaxServices.ChangePassword
                Dim password As String
                password = Request.QueryString("pwd")
                returnString = GetUserPasswordExist(password)
            Case Constant.AjaxServices.UserGroup
                Dim UserGroup As String
                UserGroup = Request.QueryString("UserGroup")
                returnString = GetUserGroupExist(UserGroup)
            Case Constant.AjaxServices.FinYear
                Dim FinYear As String
                FinYear = Request.QueryString("FinYear")
                returnString = GetFinYearExist(FinYear)
            Case Constant.AjaxServices.SerialControl
                Dim year As String
                Dim doc As String
                Dim screenStatus As String
                Dim srlid As Integer
                Dim srlloc As String
                year = Request.QueryString("year")
                doc = Request.QueryString("doc")
                screenStatus = Request.QueryString("screenStatus")
                srlid = CInt(Request.QueryString("srlid"))
                srlloc = Request.QueryString("srlloc")
                returnString = GetSrlCntrlCheck(year, doc, screenStatus, srlid, srlloc)
            Case Constant.AjaxServices.MenuCode
                Dim mcode As String
                Dim hcode As String
                mcode = Request.QueryString("mcode")
                hcode = Request.QueryString("hcode")
                returnString = GetMenuMstrCheck(mcode, hcode)
            Case Constant.AjaxServices.LovDetailsCode
                Dim type As String
                Dim Lcode As String
                Dim hcode As String
                type = Request.QueryString("lovtype")
                Lcode = Request.QueryString("Lcode")
                hcode = Request.QueryString("hcode")
                returnString = GetLovDetCodeCheck(type, Lcode, hcode)
            Case Constant.AjaxServices.LovMasterType
                Dim type As String
                Dim htype As String
                type = Request.QueryString("lovtype")
                htype = Request.QueryString("htype")
                returnString = GetLovMstrTypeCheck(type, htype)
            Case Constant.AjaxServices.ChangePasswordLink
                Dim Userid As String
                Dim password As String
                password = Request.QueryString("pwd")
                Userid = Request.QueryString("usrid")
                returnString = GetUserPasswordLinkExist(Userid, password)
           
            Case "DepotRegion"
                Dim Depot As String
                Depot = Request.QueryString("Depot")
                returnString = GetDepotDtls(Depot)

            Case Constant.AjaxServices.UserGroupId
                Dim Company As String
                Dim UserGroup As String
                Dim Status As String
                Company = Request.QueryString("company")
                UserGroup = Request.QueryString("userGroup")
                Status = Request.QueryString("status")
                returnString = GetUserId(Company, UserGroup, Status)
            Case "GetDepotRegion_break"
                Dim depot As String = Request.QueryString("depot")
                Dim company As String = Request.QueryString("Company")
                returnString = getregion_break(company, depot)
        End Select
        'Push info back to client
        Response.Clear()
        Response.ContentType = "text/xml"
        Response.Write(returnString)
        Response.End()
    End Sub

#End Region




#Region "get region"

    Private Function getregion_break(ByVal Company As String, ByVal depot_code As String) As String

        Dim scriptBuilder As New Text.StringBuilder

        Try

            Dim ExtentItemgrpSet As New DataSet
            Dim ObjCommon As New Common
            ExtentItemgrpSet = ObjCommon.Getregion_break(Company, depot_code)
            If (Not (ExtentItemgrpSet Is Nothing) AndAlso ExtentItemgrpSet.Tables.Count > 0 AndAlso Not (ExtentItemgrpSet.Tables(0) Is Nothing) AndAlso ExtentItemgrpSet.Tables(0).Rows.Count > 0) Then
                scriptBuilder.Append("[")
                For Each mchset As System.Data.DataRow In ExtentItemgrpSet.Tables(0).Rows

                    scriptBuilder.Append("{")
                    scriptBuilder.Append("""Region"":")
                    scriptBuilder.Append("""")
                    scriptBuilder.Append(IIf(mchset("depot_regn") Is DBNull.Value, String.Empty, mchset("depot_regn")))
                    scriptBuilder.Append("""")
                    scriptBuilder.Append("}")
                    scriptBuilder.Append(",")
                Next
                scriptBuilder.Append("]")
            End If
            Return scriptBuilder.ToString
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.ErrorPopulateExtentUOM
            Server.Transfer(returnUrl)
        End Try

    End Function


#End Region

#Region "Get Document Type Listing"

    Private Function GetDocumentTypeListing(ByVal type As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim DocListTypeSet As New DataSet
        Dim DocStatus As String = type
        DocListTypeSet = ObjDocumentType.GetLovDetails(Company, DocStatus, Constant.Common.ActiveStatus)
        If (Not (DocListTypeSet Is Nothing) AndAlso DocListTypeSet.Tables.Count > 0 AndAlso Not (DocListTypeSet.Tables(0) Is Nothing) AndAlso DocListTypeSet.Tables(0).Rows.Count > 0) Then
            scriptBuilder.Append("[")
            For Each DocTypeListRow As System.Data.DataRow In DocListTypeSet.Tables(0).Rows
                'Dim stateListRow As System.Data.DataRow = statelist.Tables(0).Rows(0)
                scriptBuilder.Append("{")
                scriptBuilder.Append("""DoctypeValue"":")
                scriptBuilder.Append("""")
                scriptBuilder.Append(IIf(DocTypeListRow("lov_code") Is DBNull.Value, String.Empty, DocTypeListRow("lov_code")))
                scriptBuilder.Append("""")
                scriptBuilder.Append(",")
                scriptBuilder.Append("""DoctypeDescription"":")
                scriptBuilder.Append("""")
                scriptBuilder.Append(IIf(DocTypeListRow("lov_value") Is DBNull.Value, String.Empty, DocTypeListRow("lov_value")))
                scriptBuilder.Append("""")
                scriptBuilder.Append("}")
                scriptBuilder.Append(",")
            Next
            scriptBuilder.Append("]")
        End If
        Return scriptBuilder.ToString
    End Function
#End Region
#Region "Get UserID Details"

    Private Function GetUserIdCheck(ByVal userId As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim UserIdSet As New DataSet
        Dim userIdExists As Boolean
        Dim ObjUserProfile As New UserProfile
        UserIdSet = ObjUserProfile.GetUserIdCheck(Company, userId)
        If Not (UserIdSet Is Nothing AndAlso UserIdSet.Tables.Count > 0) Then
            If (Not (UserIdSet.Tables(0) Is Nothing) AndAlso UserIdSet.Tables(0).Rows.Count > 0) Then
                userIdExists = True
            Else
                userIdExists = False
            End If
        End If
        Return userIdExists
    End Function

#End Region
#Region "Get Password Details"

    Private Function GetUserPasswordExist(ByVal password As String) As String

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim scriptBuilder As New Text.StringBuilder
        Dim UserIdSet As New DataSet
        Dim userpwdExists As Boolean
        Dim userdetails As New ChangePassword
        UserIdSet = userdetails.GetPasswordExist(userInfo.userCompanyEntity, userInfo.userIDEntity, password)
        If Not (UserIdSet Is Nothing AndAlso UserIdSet.Tables.Count > 0) Then
            If (Not (UserIdSet.Tables(0) Is Nothing) AndAlso UserIdSet.Tables(0).Rows.Count > 0) Then
                userpwdExists = True
            Else
                userpwdExists = False
            End If
        End If
        Return userpwdExists
    End Function

#End Region
#Region "Get UserGroup Details"

    Private Function GetUserGroupExist(ByVal UserGroup As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim UserIdSet As New DataSet
        Dim usergroupExists As Boolean
        Dim userdetails As New UserGroup
        UserIdSet = userdetails.GetUserGroupExist(Company, UserGroup)
        If Not (UserIdSet Is Nothing AndAlso UserIdSet.Tables.Count > 0) Then
            If (Not (UserIdSet.Tables(0) Is Nothing) AndAlso UserIdSet.Tables(0).Rows.Count > 0) Then
                usergroupExists = True
            Else
                usergroupExists = False
            End If
        End If
        Return usergroupExists
    End Function

#End Region
#Region "Get FinYear Details"

    Private Function GetFinYearExist(ByVal FinYear As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim finyrSet As New DataSet
        Dim finyearExists As Boolean
        Dim finyrdetails As New WeekMaster
        finyrSet = finyrdetails.GetUserGroupExist(Company, FinYear)
        If Not (finyrSet Is Nothing AndAlso finyrSet.Tables.Count > 0) Then
            If (Not (finyrSet.Tables(0) Is Nothing) AndAlso finyrSet.Tables(0).Rows.Count > 0) Then
                finyearExists = True
            Else
                finyearExists = False
            End If
        End If
        Return finyearExists
    End Function

#End Region
#Region "Get SrlCntrlCheck Details"

    Private Function GetSrlCntrlCheck(ByVal year As String, ByVal doc As String, ByVal screenStatus As String, ByVal srlid As Integer, ByVal srlloc As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim SrlCntrlSet As New DataSet
        Dim SrlCntrlExists As Boolean
        Dim SrlCntrldetails As New SerialControl
        SrlCntrlSet = SrlCntrldetails.GetSrlCntrlExist(Company, year, doc, screenStatus, srlid, srlloc)
        If Not (SrlCntrlSet Is Nothing AndAlso SrlCntrlSet.Tables.Count > 0) Then
            If (Not (SrlCntrlSet.Tables(0) Is Nothing) AndAlso SrlCntrlSet.Tables(0).Rows.Count > 0) Then
                SrlCntrlExists = True
            Else
                SrlCntrlExists = False
            End If
        End If
        Return SrlCntrlExists
    End Function

#End Region
#Region "Get MenuMstrCheck Details"

    Private Function GetMenuMstrCheck(ByVal mcode As String, ByVal hcode As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim MenuMstrSet As New DataSet
        Dim MenuMstrExists As Boolean
        Dim MenuMstrdetails As New LovDetails
        MenuMstrSet = MenuMstrdetails.GetMenuMstrExist(Company, mcode, hcode)
        If Not (MenuMstrSet Is Nothing AndAlso MenuMstrSet.Tables.Count > 0) Then
            If (Not (MenuMstrSet.Tables(0) Is Nothing) AndAlso MenuMstrSet.Tables(0).Rows.Count > 0) Then
                MenuMstrExists = True
            Else
                MenuMstrExists = False
            End If
        End If
        Return MenuMstrExists
    End Function

#End Region
#Region "Get LovDetCodeCheck Details"

    Private Function GetLovDetCodeCheck(ByVal type As String, ByVal Lcode As String, ByVal hcode As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim LovDetSet As New DataSet
        Dim LovDetExists As Boolean
        Dim LovDetdetails As New LovDetails
        LovDetSet = LovDetdetails.GetLovDetCodeExist(Company, type, Lcode, hcode)
        If Not (LovDetSet Is Nothing AndAlso LovDetSet.Tables.Count > 0) Then
            If (Not (LovDetSet.Tables(0) Is Nothing) AndAlso LovDetSet.Tables(0).Rows.Count > 0) Then
                LovDetExists = True
            Else
                LovDetExists = False
            End If
        End If
        Return LovDetExists
    End Function

#End Region
#Region "Get LovMstrTypeCheck Details"

    Private Function GetLovMstrTypeCheck(ByVal type As String, ByVal htype As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim LovMstrSet As New DataSet
        Dim LovMstrExists As Boolean
        Dim LovMstrdetails As New LovDetails
        LovMstrSet = LovMstrdetails.GetLovMstrTypeExist(Company, type, htype)
        If Not (LovMstrSet Is Nothing AndAlso LovMstrSet.Tables.Count > 0) Then
            If (Not (LovMstrSet.Tables(0) Is Nothing) AndAlso LovMstrSet.Tables(0).Rows.Count > 0) Then
                LovMstrExists = True
            Else
                LovMstrExists = False
            End If
        End If
        Return LovMstrExists
    End Function

#End Region
#Region "Get YearWeekCheck Details"

    Private Function GetYearWeekCheck(ByVal year As String, ByVal week As String, ByVal hweek As String) As String
        Dim scriptBuilder As New Text.StringBuilder
        Dim YearWeekSet As New DataSet
        Dim YearWeekExists As Boolean
        Dim YearWeekdetails As New WeekMaster
        YearWeekSet = YearWeekdetails.GetYearWeekExist(Company, year, week, hweek)
        If Not (YearWeekSet Is Nothing AndAlso YearWeekSet.Tables.Count > 0) Then
            If (Not (YearWeekSet.Tables(0) Is Nothing) AndAlso YearWeekSet.Tables(0).Rows.Count > 0) Then
                YearWeekExists = True
            Else
                YearWeekExists = False
            End If
        End If
        Return YearWeekExists
    End Function

#End Region
#Region "Get PasswordLink Details"

    Private Function GetUserPasswordLinkExist(ByVal userid As String, ByVal password As String) As String

        'Dim userInfo As VMSUserEntity = New VMSUserEntity()
        'If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
        '    userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        'Else
        '    Response.Redirect("~/Login.aspx")
        'End If

        Dim scriptBuilder As New Text.StringBuilder
        Dim UserIdSet As New DataSet
        Dim userpwdExists As Boolean
        Dim userdetails As New ChangePassword
        UserIdSet = userdetails.GetPasswordLinkExist(userid, password)
        If Not (UserIdSet Is Nothing AndAlso UserIdSet.Tables.Count > 0) Then
            If (Not (UserIdSet.Tables(0) Is Nothing) AndAlso UserIdSet.Tables(0).Rows.Count > 0) Then
                userpwdExists = True
            Else
                userpwdExists = False
            End If
        End If
        Return userpwdExists
    End Function

#End Region
#Region "Depot Details"
    Function GetDepotDtls(ByVal Depot As String)
        Dim scriptBuilder As New Text.StringBuilder
        Dim DepotDetailsSet As New DataSet
        Dim ObjDocumentType As New Common


        DepotDetailsSet = ObjDocumentType.getDepotRegndetails(Depot)


        If (Not (DepotDetailsSet Is Nothing) AndAlso DepotDetailsSet.Tables.Count > 0 AndAlso Not (DepotDetailsSet.Tables(0) Is Nothing) AndAlso DepotDetailsSet.Tables(0).Rows.Count > 0) Then
            scriptBuilder.Append("[")
            For Each DtlListRow As System.Data.DataRow In DepotDetailsSet.Tables(0).Rows
                scriptBuilder.Append("{")
                scriptBuilder.Append("""Depot_region"":")
                scriptBuilder.Append("""")
                scriptBuilder.Append(IIf(DtlListRow("depot_regn") Is DBNull.Value, String.Empty, DtlListRow("depot_regn").ToString.Trim))
                scriptBuilder.Append("""")
                scriptBuilder.Append("}")
                scriptBuilder.Append(",")
            Next
            scriptBuilder.Append("]")
        End If
        Return scriptBuilder.ToString
    End Function
#End Region
#Region "User Id for specific user group"
    Function GetUserId(ByVal Company As String, ByVal UserGroup As String, ByVal Status As String)
        Dim scriptBuilder As New Text.StringBuilder
        Dim UserGroupDetailsSet As New DataSet
        Dim ObjDocumentType As New Common

        UserGroupDetailsSet = ObjDocumentType.GetUserId(Company, UserGroup, Status)
        If (Not (UserGroupDetailsSet Is Nothing) AndAlso UserGroupDetailsSet.Tables.Count > 0 AndAlso Not (UserGroupDetailsSet.Tables(0) Is Nothing) AndAlso UserGroupDetailsSet.Tables(0).Rows.Count > 0) Then
            scriptBuilder.Append("[")
            For Each DtlListRow As System.Data.DataRow In UserGroupDetailsSet.Tables(0).Rows
                scriptBuilder.Append("{")
                scriptBuilder.Append("""user_id"":")
                scriptBuilder.Append("""")
                scriptBuilder.Append(IIf(DtlListRow("usp_user_id") Is DBNull.Value, String.Empty, DtlListRow("usp_user_id").ToString.Trim))
                scriptBuilder.Append("""")
                scriptBuilder.Append("}")
                scriptBuilder.Append(",")
            Next
            scriptBuilder.Append("]")
        End If
        Return scriptBuilder.ToString

    End Function
#End Region
End Class
