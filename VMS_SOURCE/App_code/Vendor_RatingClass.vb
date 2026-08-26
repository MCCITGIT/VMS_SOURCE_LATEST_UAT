Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Imports NPOI.SS.Formula.Eval
Imports CrystalDecisions.[Shared]
Imports System.IdentityModel.Protocols.WSTrust

Public Class Vendor_RatingClass
    Function Get_VendorRatingList(ByVal vendorcode As String, ByVal quartor As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quartor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@keywords"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(vendorcode), DBNull.Value, vendorcode)

        'DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetVendor_RatingList]", Data.CommandType.StoredProcedure, sqlParams)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_vendor_weightage_board]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_VendorRatingList_All(ByVal vendor As String, ByVal finyear As String, ByVal quartor As String, ByVal product As String, ByVal vendorgrp As String, ByVal productgrp As String, ByVal type As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(6) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quartor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(vendor), DBNull.Value, vendor)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@product"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(String.IsNullOrEmpty(product), DBNull.Value, product)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@vendor_group"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(String.IsNullOrEmpty(vendorgrp), DBNull.Value, vendorgrp)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@product_group"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(String.IsNullOrEmpty(productgrp), DBNull.Value, productgrp)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@type"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = type

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@finyear"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = finyear

        'DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetVendor_RatingList]", Data.CommandType.StoredProcedure, sqlParams)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_vendor_weightage_board_All_vr1]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_VendorRatingHeaderList(ByVal vendorcode As String, ByVal quartor As String, ByVal vendorid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quartor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@keywords"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(vendorcode), DBNull.Value, vendorcode)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@vendorid"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = vendorid

        'DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetVendor_RatingList]", Data.CommandType.StoredProcedure, sqlParams)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_vendor_weightage_board_vendor_wise]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_StatutorydtsByHdr(ByVal hdr_id As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@hdr_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = hdr_id
        'DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetVendor_RatingList]", Data.CommandType.StoredProcedure, sqlParams)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_Statutory_dtls_by_hdr]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_QualitydtsByHdr(ByVal quarter As String, ByVal vendorid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quarter

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendorid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = vendorid
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_Quality_dtls_by_hdr]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_AuditdtsByHdr(ByVal quarter As String, ByVal hrdid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quarter

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@hdrid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = hrdid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Audit_Details_Byhdr]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_complaintdtlsByHdr(ByVal quarter As String, ByVal hrdid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quarter

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@hdrid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = hrdid
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_complaint_details_by_hdr]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_VendorWiseProductList(ByVal vendorcode As String, ByVal userid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_code"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorcode), DBNull.Value, vendorcode)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(userid), DBNull.Value, userid)

        'DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetVendor_RatingList]", Data.CommandType.StoredProcedure, sqlParams)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_vendor_product_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_Servicedts(ByVal quarter As String, ByVal vendorid As String, ByVal brandid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quarter

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendorid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = vendorid

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@brandid"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = brandid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_service_score_dtls]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_GrpServicedts(ByVal quarter As String, ByVal vendorid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quarter

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = vendorid
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_vendor_grp_serviceeability]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_VendorWiseProdut(ByVal vendorcode As String, ByVal quarter As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_code"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorcode), DBNull.Value, vendorcode)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(quarter), DBNull.Value, quarter)

        'DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetVendor_RatingList]", Data.CommandType.StoredProcedure, sqlParams)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_vendor_product_list_with_volume]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_VendorGroupList() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_vendorgroup_list]", Data.CommandType.StoredProcedure)
        Return DS
    End Function

    Function Get_GroupWisevendorList(ByVal vendorgrp As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorgrpcode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorgrp), DBNull.Value, vendorgrp)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Grpwise_vendor_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_Total_Despatch() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_totaldespath_lvl1]", CommandType.StoredProcedure)
        Return DS
    End Function
    Function Get_Pending_Load() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_totalpendingload_lvl1]", CommandType.StoredProcedure)
        Return DS
    End Function
    Function Get_Pending_Complaint() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_Comaplaints_lvl1]", CommandType.StoredProcedure)
        Return DS
    End Function
    Function Get_Legal_Doc() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_legalexpiredoc_lvl1]", CommandType.StoredProcedure)
        Return DS
    End Function
    Function Get_UnApproved_Doc() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_unapproved_doc_lvl1]", CommandType.StoredProcedure)
        Return DS
    End Function
    Function Get_Audit_Count() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_auditedcount_lvl1]", CommandType.StoredProcedure)
        Return DS
    End Function
    Function Get_SampleTested_Count() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_sampletestedcount_lvl1]", CommandType.StoredProcedure)
        Return DS
    End Function

    Function Get_DashBoardInfo_Comaplaints_lvl2(ByVal vendorcode As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorcode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorcode), DBNull.Value, vendorcode)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_Comaplaints_lvl2]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_DashBoardInfo_LegalExpireDoc_lvl2(ByVal vendorcode As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorcode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorcode), DBNull.Value, vendorcode)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_legalexpiredoc_lvl2]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_DashBoardInfo_UnapproveLegalDoc_lvl2(ByVal vendorcode As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorcode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorcode), DBNull.Value, vendorcode)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_DashBoardInfo_unapproved_doc_lvl2]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_VendorLYTY_DETAILS(ByVal vendor As String, ByVal quartor As String, ByVal finyr As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quartor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(vendor), DBNull.Value, vendor)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@finyear"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = finyr

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_vendor_ly_ty_details]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function


End Class
