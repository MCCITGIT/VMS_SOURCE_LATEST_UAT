Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Public Class DepotMstrClass
#Region "Region Populate"
    Function GetRegionDetails() As DataSet
        Dim ds As System.Data.DataSet
        ds = DBFactory.GetHelper().ExecuteDataSet("Get_RegionDetails", Data.CommandType.StoredProcedure)
        Return ds

    End Function
#End Region
#Region "Depot Populate"
    Function GetDepotDetails(ByVal Region As String) As DataSet
        Dim ds As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Region"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Region <> String.Empty, Region, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("Get_RegionWise_DepotList", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function
#End Region
#Region "Depot Details"
    Function GetDepot_DataList(ByVal Depot As String) As DataSet
        Dim ds As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Depot <> String.Empty, Depot, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("Get_DepotDetails", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function
#End Region
End Class
