'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/Process_Month_Year_Change_App.vb
'Created Date	: 01-March-2012
'Created By	    : Debayan Biswas
'Version	    : R01.00.00
'Description	: Code behind file for Process_Month_Year_Change_App Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Public Class Process_Month_Year_Change_App
    Public Function GetYear(ByVal Active As String) As DataSet
        Dim YearDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Active

        YearDS = DBFactory.GetHelper().ExecuteDataSet("[StockUploadSummary_Populate_Year]", Data.CommandType.StoredProcedure, sqlParams)
        Return YearDS
    End Function

    Public Function GetMonth(ByVal Active As String) As DataSet
        Dim MonthDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Active

        MonthDS = DBFactory.GetHelper().ExecuteDataSet("[StockUploadSummary_Populate_Month]", Data.CommandType.StoredProcedure, sqlParams)
        Return MonthDS
    End Function

    Public Function UpdateYearMonth(ByVal Company As String, ByVal ProcessYear As String, ByVal ProcessMonth As String, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
        Dim NumsRowAffected As New Integer

        Try
            Dim sqlparams(2) As SqlParameter

            sqlparams(0) = New SqlParameter
            sqlparams(0).ParameterName = "@Company"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = ParameterDirection.Input
            sqlparams(0).Value = Company

            sqlparams(1) = New SqlParameter
            sqlparams(1).ParameterName = "@param_char_year"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = ParameterDirection.Input
            sqlparams(1).Value = ProcessYear

            sqlparams(2) = New SqlParameter
            sqlparams(2).ParameterName = "@param_char_month"
            sqlparams(2).DbType = DbType.String
            sqlparams(2).Direction = ParameterDirection.Input
            sqlparams(2).Value = ProcessMonth

            Dim sqlcmd As New SqlCommand
            sqlcmd.CommandText = "[Process_Month_Year_Update]"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Connection = sqlconn
            sqlcmd.Transaction = sqltrans
            sqlcmd.Parameters.AddRange(sqlparams)
            NumsRowAffected = sqlcmd.ExecuteNonQuery
        Catch ex As Exception
            Throw ex
        End Try
        Return NumsRowAffected
    End Function
End Class
