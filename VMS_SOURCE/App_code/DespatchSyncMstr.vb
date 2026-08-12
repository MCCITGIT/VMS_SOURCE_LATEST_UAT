Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess
Public Class DespatchSyncMstr

#Region "Get Despatch Details"
    Function GetDespatchDetails() As DataSet

        Dim DetailsDS As System.Data.DataSet
        'Dim sqlParams(2) As SqlParameter

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[DespatchSink_getDetails]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region

#Region "Update Despatch sink Details"
    Function UpdateDespatchSyncDetails(ByVal Unit As String, ByVal Depot As String, ByVal ChallanNo As Integer, ByVal challan_fin_year As String, ByVal despd_srl As Int32) As Integer
        Dim numRowsAffected As Integer
        Dim sqlParams(4) As SqlParameter

        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Depot

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@ChallanNo"
            sqlParams(2).DbType = DbType.Int32
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = ChallanNo

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@challan_fin_year"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = challan_fin_year

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@despd_srl"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = despd_srl

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "DespatchSink_UpdateDetails"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            'SqlTrans is set to commit to save the transaction
            sqlTrans.Commit()

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Throw ex
        Finally
            If Not (sqlConn Is Nothing) Then
                'sqlConn is set to close state after completing the transaction
                sqlConn.Close()
            End If
        End Try

        Return numRowsAffected
    End Function
#End Region

End Class
