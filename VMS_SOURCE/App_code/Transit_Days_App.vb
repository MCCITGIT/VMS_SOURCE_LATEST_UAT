'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Transit_Days_AddUpdate.aspx.vb
'Created Date	: 13-January-2012
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for Transit_Days_AddUpdate.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Public Class Transit_Days_App
    Public Function GetUnit(ByVal Active As String) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Active

        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[Get_Unit_Details]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

    Public Function GetDetails(ByVal Unit As String) As DataSet
        Dim DetailsDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Unit

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Transit_Days_Get_Details]", Data.CommandType.StoredProcedure, sqlParams)
        Return DetailsDS
    End Function

    Public Function TransitDaysInsert(ByVal TrnstDyEntity As VMS.Web.Transit_Days_Entity, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
        Dim NumsRowAffected As New Integer

        Try
            Dim sqlparams(3) As SqlParameter

            sqlparams(0) = New SqlParameter
            sqlparams(0).ParameterName = "@t_vendor_unit"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = ParameterDirection.Input
            sqlparams(0).Value = TrnstDyEntity.vendor_unit

            sqlparams(1) = New SqlParameter
            sqlparams(1).ParameterName = "@t_depot"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = ParameterDirection.Input
            sqlparams(1).Value = TrnstDyEntity.depot

            sqlparams(2) = New SqlParameter
            sqlparams(2).ParameterName = "@t_transit_days"
            sqlparams(2).DbType = DbType.Int64
            sqlparams(2).Direction = ParameterDirection.Input
            sqlparams(2).Value = IIf(TrnstDyEntity.transit_days <> Integer.MinValue, TrnstDyEntity.transit_days, DBNull.Value)

            sqlparams(3) = New SqlParameter
            sqlparams(3).ParameterName = "@created_user"
            sqlparams(3).DbType = DbType.String
            sqlparams(3).Direction = ParameterDirection.Input
            sqlparams(3).Value = TrnstDyEntity.CreatedUser

            Dim sqlcmd As New SqlCommand
            sqlcmd.CommandText = "[Transit_Days_Insert]"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Connection = sqlconn
            sqlcmd.Transaction = sqltrans
            sqlcmd.Parameters.AddRange(sqlparams)
            NumsRowAffected = sqlcmd.ExecuteNonQuery
            Return NumsRowAffected
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function TransitDaysDelete(ByVal Unit As String, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
        Dim numsrowaffected As Integer

        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Unit

            Dim sqlcmd As New SqlCommand
            sqlcmd.CommandText = "[Transit_Days_Delete]"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Connection = sqlconn
            sqlcmd.Transaction = sqltrans
            sqlcmd.Parameters.AddRange(sqlParams)
            numsrowaffected = sqlcmd.ExecuteNonQuery
            Return numsrowaffected
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetExcelReport(ByVal Unit As String) As DataSet
        Dim DetailsDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Unit

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Transit_Days_Get_Report_For_Excel]", Data.CommandType.StoredProcedure, sqlParams)
        Return DetailsDS
    End Function
End Class
