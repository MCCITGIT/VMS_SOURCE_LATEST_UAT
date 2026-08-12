Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class vrs_Serviceability_class
    Function Get_ServiceabilityDepotDispatch(ByVal vendorid As String, ByVal quarterid As Int32) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter_id"
        sqlParams(1).DbType = DbType.Int32
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarterid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_serviceability_final]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_ServiceabilityDirectDispatch(ByVal vendorid As String, ByVal quarterid As Int32) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter_id"
        sqlParams(1).DbType = DbType.Int32
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarterid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_complint_serviceability_direct_dispatch]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function



End Class
