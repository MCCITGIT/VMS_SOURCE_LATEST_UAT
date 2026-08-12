'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/ChangePwd.vb
'Created Date	: 23-November-2007
'Created By	    : Arun
'Version	    : R02.00.00
'Description	: Code behind file for ChangePassword Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Namespace VMS.Web

    Public Class ChangePassword


#Region "Update Password"
        Function UpdatePassword(ByVal company As String, ByVal UserID As String, ByVal NewPassword As String, ByVal OldPassword As String) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(4) As SqlParameter
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
                sqlParams(1).Value = UserID

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@NewPassword"
                sqlParams(2).DbType = DbType.String
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = NewPassword

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@OldPassword"
                sqlParams(3).DbType = DbType.String
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = OldPassword

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@out_status"
                sqlParams(4).DbType = DbType.Int32
                sqlParams(4).Direction = Data.ParameterDirection.Output
                'sqlParams(4).Value = out_status


                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "Change_Password_Update"

                sqlCmd.Parameters.AddRange(sqlParams)
                sqlCmd.ExecuteNonQuery()

                'SqlTrans is set to commit to save the transaction
                sqlTrans.Commit()
                numRowsAffected = sqlParams(4).Value
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

#Region "Password Already Exists"
        Function GetPasswordExist(ByVal Company As String, ByVal UserID As String, ByVal ConPwd As String) As DataSet
            Dim UsrPwd As DataSet

            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@UserId"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserID

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@ConPwd"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = ConPwd



            UsrPwd = DBFactory.GetHelper().ExecuteDataSet("Password_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

            Return UsrPwd
        End Function
#End Region

#Region "Change Password"
        Function ChangePassword(ByVal UserID As String, ByVal Pwd As String, ByVal ConPwd As String) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(2) As SqlParameter
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@userid"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = UserID

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@pwd"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = Pwd

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@ConPwd"
                sqlParams(2).DbType = DbType.String
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = ConPwd



                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "Change_Password_Link_Update"

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

#Region "PasswordLink Already Exists"
        Function GetPasswordLinkExist(ByVal UserID As String, ByVal ConPwd As String) As DataSet
            Dim UsrPwd As DataSet

            Dim sqlParams(1) As SqlParameter


            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@UserId"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = UserID

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@ConPwd"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = ConPwd



            UsrPwd = DBFactory.GetHelper().ExecuteDataSet("Password_Link_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

            Return UsrPwd
        End Function
#End Region



    End Class
End Namespace
