Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class VRSFactoryClass
    Public Function GetFactoryDetails() As DataSet

        Dim ds As DataSet
        ds = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[get_factory_details]", Data.CommandType.StoredProcedure)
        Return ds

    End Function

    Public Function GetVendorDetails(ByVal factory As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@factory"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = factory
        ds = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[get_vendor_details]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function

    Public Function SubmitFactoryDetails(ByVal factory As String,
                                     ByVal user As String,
                                     ByVal dt As DataTable,
                                     ByVal sqlConn As SqlConnection,
                                     ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(2) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = factory

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@user"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = user

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@vendors"
            sqlParams(2).SqlDbType = SqlDbType.Structured
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = dt

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[VMS].[dbo].[submit_factory_appl_vendor]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function

End Class
