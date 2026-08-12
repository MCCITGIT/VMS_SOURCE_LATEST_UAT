Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class DirectDespatch_OrderClass
    Public Function DirectDespatch_OrderDetailsReport(ByVal FromDate As SqlDateTime, ByVal ToDate As SqlDateTime, ByVal UserId As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@fromDate"
        sqlParams(0).DbType = DbType.Date
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = FromDate

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@toDate"
        sqlParams(1).DbType = DbType.Date
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = ToDate

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@UserId"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = UserId

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vmd_dd_order_details_report_get]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
End Class
