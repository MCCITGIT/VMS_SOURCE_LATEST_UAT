'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Indent_Detail_Class.VB
'Created Date	: 29-06-2018
'Created By	    : Debayan Das
'Version	    : 0.0
'Description	: Class for get data for IndentDetails.aspx page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class IndentDetailsClass

    Public Function GetDepot(ByVal Region As String, ByVal Active As String) As DataSet
        Dim DepotDS As New DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Region"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Region <> String.Empty, Region, DBNull.Value)


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Active"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Active

        DepotDS = DBFactory.GetHelper().ExecuteDataSet("[Estimation_Data_Get_Depot]", Data.CommandType.StoredProcedure, sqlParams)
        Return DepotDS
    End Function

#Region "GET YEAR & MONTH"
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
#End Region

#Region "STORE DATASET DATA TO EXCEL"
    Public Function GetExcelIndentDetails(ByVal region As String, ByVal depot As String, ByVal year As String, ByVal Month As String) As DataSet
        Dim ExcelDptDsptchdUntWiseDS As New DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Region"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(region = String.Empty, DBNull.Value, region)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(year = String.Empty, DBNull.Value, year)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@Month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(Month = String.Empty, DBNull.Value, Month)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@Depot"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(depot = String.Empty, DBNull.Value, depot)

        ExcelDptDsptchdUntWiseDS = DBFactory.GetHelper().ExecuteDataSet("[indent_Details]", Data.CommandType.StoredProcedure, sqlParams)
        Return ExcelDptDsptchdUntWiseDS
    End Function
#End Region

#Region "GET FINANCIAL YEAR"
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
#End Region

End Class
