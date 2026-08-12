'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/MonthlyDepotIndentList_App.vb
'Created Date	: 15-December-2011
'Created By	    : Debayan Biswas
'Version	    : R01.00.00
'Description	: Code behind file for MonthlyDepotIndentList_App Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Namespace VMS.Web
    Public Class MonthlyDepotIndentList_App
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

        Public Function GetExcelMnthlyDptIndntLstRpt(ByVal Region As String, ByVal Depot As String, ByVal FinYear As String, ByVal Month As String, ByVal Active As String) As DataSet
            Dim ExcelMnthlyDptIndntLstDS As New DataSet
            Dim sqlParams(4) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Region"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(Region <> String.Empty, Region, DBNull.Value)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(Depot <> String.Empty, Depot, DBNull.Value)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@Year"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = FinYear

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@Month"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Month

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@Active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Active

            ExcelMnthlyDptIndntLstDS = DBFactory.GetHelper().ExecuteDataSet("[Mnthly_Dpt_Indent_List_Report_ForExcel]", Data.CommandType.StoredProcedure, sqlParams)
            Return ExcelMnthlyDptIndntLstDS
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
End Namespace

