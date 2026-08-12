Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Public Class ShareOfBusinessReportClass
    Public Function GetBusinessSharelist(ByVal Depot As String, ByVal UserId As String, ByVal Year As String, ByVal Month As String) As DataSet

        Dim ExcelDptDsptchdUntWiseDS As New DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Depot <> String.Empty, Depot, DBNull.Value)

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@Unit"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = IIf(Unit <> String.Empty, Unit, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@UserId"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(UserId <> String.Empty, UserId, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@Year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(Year <> String.Empty, Year, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@Month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(Month <> String.Empty, Month, DBNull.Value)

        ExcelDptDsptchdUntWiseDS = DBFactory.GetHelper().ExecuteDataSet("Get_ShareOfBusiness_Data_Merge", Data.CommandType.StoredProcedure, sqlParams)
        Return ExcelDptDsptchdUntWiseDS
    End Function
End Class
