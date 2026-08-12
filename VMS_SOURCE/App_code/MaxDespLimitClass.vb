Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess
Public Class MaxDespLimitClass
#Region "Get Screen Details"

    Function GetDespLimitDetail() As DataSet

        Dim DetailsDS As System.Data.DataSet
        'Dim sqlParams(0) As SqlParameter

        'sqlParams(0) = New SqlParameter()
        'sqlParams(0).ParameterName = "@unitCode"
        'sqlParams(0).DbType = DbType.String
        'sqlParams(0).Direction = Data.ParameterDirection.Input
        'sqlParams(0).Value = unitCode
        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[MaxDespLimit_Get_Detail]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region
#Region "Update Max Limit"
    Function UpdateMaxLimit(ByVal unit As String, ByVal limit As Decimal, ByVal userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@mdl_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@mdl_limit"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = limit

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@modified_user"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = userid

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "MaxDespLimit_Update_Limit"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region
End Class
