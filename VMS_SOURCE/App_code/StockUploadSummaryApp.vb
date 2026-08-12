'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/StockUploadSummaryApp.vb
'Created Date	: 13-December-2011
'Created By	    : Debayan Biswas
'Version	    : R01.00.00
'Description	: Code behind file for StockUploadSummaryApp Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Public Class StockUploadSummaryApp
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

    Public Function GetTopFinYear() As String
        Dim noRowsAffected As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Constant.Common.ActiveStatus

        noRowsAffected = DBFactory.GetHelper().ExecuteDataSet("Stock_Upload_Get_TopFinYear", Data.CommandType.StoredProcedure, sqlParams)
        Dim TopFinYr As String
        TopFinYr = noRowsAffected.Tables(0).Rows(0)("fin_year")
        Return TopFinYr
    End Function

    Public Function GetLastFinYear() As String
        Dim noRowsAffected As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Constant.Common.ActiveStatus

        noRowsAffected = DBFactory.GetHelper().ExecuteDataSet("Stock_Upload_Get_LastFinYear", Data.CommandType.StoredProcedure, sqlParams)
        Dim LastFinYr As String
        LastFinYr = noRowsAffected.Tables(0).Rows(0)("fin_year")
        Return LastFinYr
    End Function
End Class
