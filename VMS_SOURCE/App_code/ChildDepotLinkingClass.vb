Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Public Class ChildDepotLinkingClass
#Region "Get parent depot details"
    Public Function GetParentdepotname(ByVal parent_depot As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@parent_depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(parent_depot = String.Empty, DBNull.Value, parent_depot)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[CHILD_DEPOT_GET_UNLNKD_PARENT_DEPOT]", Data.CommandType.StoredProcedure, sqlParams)

        Return PrjectList

    End Function
#End Region
#Region "Get child depot details"
    Public Function GetChildDepotList(ByVal parent_depot As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@parent_depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(parent_depot = String.Empty, DBNull.Value, parent_depot)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[CHILD_DEPOT_GET_UNLNKD_DEPOT]", Data.CommandType.StoredProcedure, sqlParams)

        Return PrjectList

    End Function
#End Region
#Region "Insert Update Child Depot Code"
    Function InsertUpdateChildDepotLinking(ByVal parent_depot As String, ByVal child_depot As String, ByVal created_user As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim sqlParams(2) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@parent_depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = parent_depot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@child_depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = child_depot

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@userid"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = created_user


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[CHILD_DEPOT_INSERT_UPDATE]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()


        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Throw ex
        End Try
        Return numRowsAffected
    End Function

    Function DeleteLinkedChildDetails(ByVal parent_depot As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim sqlParams(0) As SqlParameter
        Try

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@parent_depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = parent_depot

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[CHILD_DEPOT_LINKED_DELETE]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Throw ex
        End Try
        Return numRowsAffected
    End Function
#End Region
End Class
