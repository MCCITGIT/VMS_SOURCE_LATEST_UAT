Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Imports VMS.Web


Public Class UserProfile_List_ReportClass
#Region "Populate Depots"
    Public Function GeDepots(ByVal region As String, ByVal active As String) As DataSet
        Dim noRowsAffected As New DataSet

        Dim sqlParams(1) As SqlParameter
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Region"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Active"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = active

        noRowsAffected = DBFactory.GetHelper().ExecuteDataSet("UserProfile_Report_get_Depot", Data.CommandType.StoredProcedure, sqlParams)
        Return noRowsAffected
    End Function
#End Region

#Region "Populate Excel Report"
    Public Function GeReportDetails(ByVal company As String, ByVal region As String, ByVal depot As String) As DataSet
        Dim noRowsAffected As New DataSet

        Dim sqlParams(2) As SqlParameter
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = company

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

        noRowsAffected = DBFactory.GetHelper().ExecuteDataSet("UserProfile_List_Report", Data.CommandType.StoredProcedure, sqlParams)
        Return noRowsAffected
    End Function
#End Region

End Class
