Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class vrsComplaintClass
    Function GetVendor(ByVal userid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid
        DS = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[tc_vendor_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#Region "Get quarter details"
    Public Function GetQuarterDetails(ByVal user As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@user_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = user

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_Quarter_List]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function
#End Region
    Public Function GetComplaintDetails(ByVal quarter As String, ByVal vendor As String) As DataSet
        Dim ds As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        'ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_complaint_details]", Data.CommandType.StoredProcedure, sqlParams)
        ds = DBFactory.GetHelper().ExecuteDataSet("dbo.get_complaint_hdr_details", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function

    Public Function GetVendorComplaintDetails(ByVal vendor As String,
                                              ByVal quarter As String) As DataSet
        Dim ds As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        ds = DBFactory.GetHelper().ExecuteDataSet("dbo.get_vendor_complaint_dtls", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function

    Function GetComplaintsVendorPopulateDropdown(ByVal userid As String, ByVal quarter As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        DS = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[tc_vendor_list_complaints_acknowledge]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Public Function GetComplaintsDetails(ByVal vendor As String, ByVal quarter As String) As DataSet
        Dim ds As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Not String.IsNullOrEmpty(vendor), vendor, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_vendor_list_against_complain]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function

    Public Function GetComplaintsIndivisualDetails(ByVal vendor As String, ByVal quarter As String) As DataSet
        Dim ds As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_complaint_details_with_complaints]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function




End Class
