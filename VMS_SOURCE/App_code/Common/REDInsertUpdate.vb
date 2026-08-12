Imports Microsoft.VisualBasic
Imports System.Reflection
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess

Namespace RedInsertUpdateCommon

    Public Class REDInsertUpdate
        Private dbfact As New DBFactory()
        Private sqlConn As New SqlConnection()
        Private sqlTrans As SqlTransaction = Nothing
        Private SqlCmd As New SqlCommand()

        Public Function SqlParamsCollection(ByVal EntityClassObj As Object, ByVal OutputParameter As Boolean) As System.Data.SqlClient.SqlParameter()
            'Dim sqlParams As SqlParameterCollection() = New SqlParameter(EntityClassObj.GetType().GetProperties().Length)
            Dim MaxParmas As Integer = EntityClassObj.GetType().GetProperties().Length - 1
            Dim sqlParams(MaxParmas) As SqlParameter
            Dim Index As Integer = 0
            For Each finfo As PropertyInfo In EntityClassObj.[GetType]().GetProperties()
                sqlParams(Index) = New SqlParameter()
                sqlParams(Index).ParameterName = "@" + finfo.Name
                sqlParams(Index).DbType = Me.DTYPE(finfo.PropertyType.Name)
                If ((sqlParams(Index).ParameterName = Constant.Parameters.OutParameter1) OrElse (sqlParams(Index).ParameterName = Constant.Parameters.OutParameter2)) AndAlso (OutputParameter) Then
                    sqlParams(Index).Direction = ParameterDirection.Output
                Else
                    sqlParams(Index).Direction = ParameterDirection.Input
                    sqlParams(Index).Value = finfo.GetValue(EntityClassObj, Nothing)
                    If sqlParams(Index).Value.ToString() = Constant.MinimunValues.MinDataTimeValue OrElse sqlParams(Index).Value.ToString() = Constant.MinimunValues.MinDataTimeValueAlt OrElse sqlParams(Index).Value.ToString() = Constant.MinimunValues.MinInt32Value OrElse sqlParams(Index).Value.ToString() = Constant.MinimunValues.MinInt32ValueAlt Then
                        sqlParams(Index).Value = DBNull.Value
                    End If
                End If
                Index += 1
            Next
            Return sqlParams
        End Function
        Public Function DTYPE(ByVal T As String) As DbType
            Select Case T
                Case Constant.DatabaseDbTypes.Int16
                    Return DbType.Int16

                Case Constant.DatabaseDbTypes.Int32
                    Return DbType.Int32

                Case Constant.DatabaseDbTypes.Int64
                    Return DbType.Int64

                Case Constant.DatabaseDbTypes.[String]
                    Return DbType.[String]

                Case Constant.DatabaseDbTypes.[Double]
                    Return DbType.[Double]

                Case Constant.DatabaseDbTypes.[Decimal]
                    Return DbType.[Decimal]

                Case Constant.DatabaseDbTypes.UInt16
                    Return DbType.UInt16

                Case Constant.DatabaseDbTypes.UInt32
                    Return DbType.UInt32

                Case Constant.DatabaseDbTypes.UInt64
                    Return DbType.UInt64

                Case Constant.DatabaseDbTypes.DateTime
                    Return DbType.DateTime
                Case Constant.DatabaseDbTypes.SqlDateTime
                    Return DbType.DateTime

                Case Else

                    Return DbType.[String]
            End Select
        End Function
        'Created by Rajesh Daniel on 10/11/2008
        'funciton that helps to insert/update/Delete Records in Database
        Public Function Table_InsertUpdate(ByVal AgroEntity As Object, ByVal commit As Boolean, ByVal SPName As String, ByVal OutputParameter As Boolean) As Int32
            Dim Insertval As Integer = Int32.MinValue
            Try
                If sqlConn.State <> ConnectionState.Open Then
                    sqlConn = DirectCast(DBFactory.GetHelper.OpenConnection(), SqlConnection)
                    sqlTrans = sqlConn.BeginTransaction()
                End If
                Dim sqlParams As SqlParameter() = SqlParamsCollection(AgroEntity, OutputParameter)
                SqlCmd.CommandText = SPName
                Insertval = DBFactory.GetHelper.ExecuteNonQuery(sqlConn, sqlTrans, SqlCmd.CommandText, CommandType.StoredProcedure, OutputParameter, sqlParams)
                If commit Then
                    sqlTrans.Commit()
                End If
            Catch ex As Exception
                If sqlTrans Is Nothing Then
                    sqlTrans.Rollback()
                End If
                Throw ex
            Finally
                If commit Then
                    If sqlConn IsNot Nothing Then
                        sqlConn.Close()
                    End If
                End If
            End Try
            Return Insertval
        End Function


        'Created by Rajesh Daniel on 10/11/2008
        'funciton that helps to generate Parameters Array

        Public Function DataSetSqlParamsCollection(ByVal DatasetParamsValue As SortedList) As System.Data.IDbDataParameter()
            Dim sqlParams(DatasetParamsValue.Count) As SqlParameter
            Dim Index As Integer = DatasetParamsValue.Count - 1
            Dim Type As String = String.Empty
            Dim valueType As String = String.Empty
            For Each key As DictionaryEntry In DatasetParamsValue
                Dim a As Int32 = Int32.MinValue
                sqlParams(Index) = New SqlParameter()
                sqlParams(Index).ParameterName = key.Key.ToString()
                Type = key.Value.[GetType]().ToString()
                valueType = Type.Remove(0, 7)
                sqlParams(Index).DbType = Me.DTYPE(valueType)
                sqlParams(Index).Direction = ParameterDirection.Input
                sqlParams(Index).Value = key.Value.ToString()
                If sqlParams(Index).Value.ToString() = Constant.MinimunValues.MinDataTimeValue OrElse sqlParams(Index).Value.ToString() = Constant.MinimunValues.MinDataTimeValueAlt OrElse sqlParams(Index).Value.ToString() = [String].Empty OrElse sqlParams(Index).Value.ToString() = Constant.MinimunValues.MinInt32Value Then
                    sqlParams(Index).Value = DBNull.Value
                End If

                Index -= 1
            Next
            Return sqlParams
        End Function


    End Class
End Namespace
