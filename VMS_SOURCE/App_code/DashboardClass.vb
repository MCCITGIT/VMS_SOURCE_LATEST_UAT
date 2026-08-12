Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class DashboardClass
#Region "Crerate Load Master"
    Public Function CreateDashboardFile(ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction, ByVal userid As String, ByVal year As String, ByVal month As String) As Integer

        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@active"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Constant.Common.ActiveStatus

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@userid"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = userid

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@year"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = year

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@month"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = month

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlconn
            sqlCmd.Transaction = sqltrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[VMS_Dashboard_Creation]"
            sqlCmd.CommandTimeout = 300

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception

            Throw ex
        End Try

        Return numRowsAffected
    End Function

#End Region


#Region "Get Screen Details"

    Function GetSCreenDetails(ByVal unitCode As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Dashboard_Get_Screen_Details]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region

#Region "Get Unit"
    Public Function GetUnit(ByVal Active As String) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Active

        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[Dashboard_Get_Unit]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

#End Region
#Region "Get product list"

    Function GetProductList(ByVal unitCode As String, ByVal active As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unitCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unitCode <> String.Empty, unitCode, DBNull.Value)


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@active"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = active



        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Dashboard_Get_Product_List]", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region
#Region "Get Unit wise summery"
    Public Function GetUnitWiseSummery(ByVal unit As String, ByVal Year As String, ByVal month As String, ByVal depot As String, ByVal region As String) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit <> String.Empty, unit, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Year

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = month


        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@depot"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@region"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(region <> String.Empty, region, DBNull.Value)

        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[Dashboard_Get_Unit_Summery]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

#End Region

#Region "Get Depot wise summery"
    Public Function GetDepotWiseSummery(ByVal unit As String, ByVal Year As String, ByVal month As String, ByVal product As String, ByVal depot As String, ByVal region As String) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit <> String.Empty, unit, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Year

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = month

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@product"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(product <> String.Empty, product, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@depot"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@region"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(region <> String.Empty, region, DBNull.Value)

        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[Dashboard_Get_Depot_Summery]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

#End Region

End Class
