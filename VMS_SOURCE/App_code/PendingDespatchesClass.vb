Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Public Class PendingDespatchesClass
#Region "Get Vendor Unit "
    Public Function GetUnitName(ByVal active As String, ByVal region As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@region"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = IIf(region <> String.Empty, region, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("PendingDespatches_GetUnitName", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
#Region "Get Report for Dealer Machine Return List  Report"
    Public Function PendingDespatch_Report(ByVal active As String, ByVal region As String, ByVal depot As String, ByVal ProcessYr As String, ByVal ProcessMnth As String, ByVal unit As String, ByVal OrderBy_Depot_Sku As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(6) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@region"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@depot"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@ProcessYr"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(ProcessYr <> String.Empty, ProcessYr, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@ProcessMnth"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(ProcessMnth <> String.Empty, ProcessMnth, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@unit"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(unit <> String.Empty, unit, DBNull.Value)


        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@OrderBy_Depot_Sku"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = OrderBy_Depot_Sku

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[PendingDespatches_Report_vr1]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
#End Region


#Region "Total_Load_Report"
    Public Function Total_Load_Report(ByVal region As String, ByVal depot As String, ByVal ProcessYr As String, ByVal ProcessMnth As String, ByVal unit As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@region"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@ProcessYr"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(ProcessYr <> String.Empty, ProcessYr, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@ProcessMnth"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(ProcessMnth <> String.Empty, ProcessMnth, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@unit"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        'PrjectList = DBFactory.GetHelper().ExecuteDataSet("[unitWiseTotalLoadReport_get]", Data.CommandType.StoredProcedure, sqlParams)
        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[unitWiseTotalLoadReport_get_vr1]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
#End Region


End Class
