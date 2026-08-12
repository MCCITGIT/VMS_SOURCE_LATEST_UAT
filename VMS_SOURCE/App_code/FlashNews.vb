'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/Flashnews.vb
'Created Date	: 08-January-2008
'Created By	    : Saravanan
'Version	    : R02.00.00
'Description	: Code behind file for FlashNews Class

'Modified By       Modified On       Version         Reason

'*************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class FlashNews

#Region "Insert FlashNews"

    Function InsertFlashNews(ByVal company As String, ByVal UserId As String, ByVal Msg As String, ByVal date_from As Date, ByVal till_date As Date, ByVal active As String, ByVal loginuser As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(6) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@flash_company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@flash_userid"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserId

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@flash_msg"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Msg

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@flash_to"
            sqlParams(3).DbType = DbType.Date
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = date_from

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@flash_retain_till"
            sqlParams(4).DbType = DbType.Date
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = till_date

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@active"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = active

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@created_user"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = loginuser

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "FlashNews_Insert"

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

#Region "Delete FlashNews"

    Function DeleteFlashNews(ByVal company As String, ByVal UserId As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(1) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@UserId"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserId

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "FlashNews_Delete"

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

#Region "Get Flash News"

    'Get Today Closed
    Public Function GetFalshNews(ByVal Company As String, ByVal UserId As String) As DataSet
        Dim FlashNewsSet As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@flash_company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@flash_userid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = UserId

        FlashNewsSet = DBFactory.GetHelper().ExecuteDataSet("FlashNews_Get", Data.CommandType.StoredProcedure, sqlParams)

        Return FlashNewsSet

    End Function
#End Region

End Class
