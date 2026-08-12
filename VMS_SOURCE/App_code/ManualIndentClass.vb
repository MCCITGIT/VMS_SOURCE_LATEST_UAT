Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class ManualIndentClass
    Public Function GetMonthlyIndentReport(ByVal unit As String, ByVal depot As String, ByVal ProcessYr As String, ByVal ProcessMnth As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit <> String.Empty, unit, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(ProcessYr <> String.Empty, ProcessYr, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(ProcessMnth <> String.Empty, ProcessMnth, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[depot_wise_manual_indent_details_get_vr2]", Data.CommandType.StoredProcedure, sqlParams)
        'ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[depot_wise_manual_indent_details_get]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds
    End Function
End Class
