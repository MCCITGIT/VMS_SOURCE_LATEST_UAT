Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class POLinkingRequestClass
    Public Function GetPOLinkingReqList(ByVal depotCode As String, ByVal vendorCode As String, ByVal status As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams As SqlParameter() = New SqlParameter(2) {}
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depotCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = System.Data.ParameterDirection.Input
        sqlParams(0).Value = If(depotCode = "", DBNull.Value, CObj(depotCode))

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendorCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = System.Data.ParameterDirection.Input
        sqlParams(1).Value = If(vendorCode = "", DBNull.Value, CObj(vendorCode))

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@status"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = System.Data.ParameterDirection.Input
        sqlParams(2).Value = If(status = "", DBNull.Value, CObj(status))
        DS = DBFactory.GetHelper().ExecuteDataSet("Get_POLinkingRequest_List", System.Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Public Function GetToMailAddress() As DataSet
        Dim DS As System.Data.DataSet

        DS = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[Get_To_Mail_Address]", System.Data.CommandType.StoredProcedure)
        Return DS
    End Function
    Public Function RejectPOLinking(ByVal hdrID As Long, ByVal userId As String) As Integer

        'sqlConn checks the status of Sql connection whether in open or close state
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(1) As SqlParameter
        Try

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@hdrID"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = hdrID

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@user"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = userId

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Reject_POLinking"
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
End Class
