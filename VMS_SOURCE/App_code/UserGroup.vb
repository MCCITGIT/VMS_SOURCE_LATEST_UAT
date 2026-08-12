'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/UserGroup.vb
'Created Date	: 24-November-2007
'Created By	    : Arun
'Version	    : R02.00.00
'Description	: Code behind file for UserGroup Class

'Modified By       Modified On       Version         Reason

'*************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Namespace VMS.Web

    Public Class UserGroup

#Region "Get UserGroup List"

        Function GetUserGroupList(ByVal Company As String, ByVal UserGroup As String) As DataSet

            Dim UserGroupDetails As System.Data.DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usergroup"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserGroup



            UserGroupDetails = DBFactory.GetHelper().ExecuteDataSet("User_Group_List", Data.CommandType.StoredProcedure, sqlParams)

            Return UserGroupDetails
        End Function
#End Region

#Region "Insert UserGroup"
        Function InsertUsrGrp(ByVal company As String, ByVal UsrCode As String, ByVal UsrDesc As String, ByVal UsrType As String, ByVal UsrLevel As String, ByVal Status As String, ByVal IDFlag As String, ByVal BrFlag As String, ByVal ComFlag As String, ByVal DeptFlag As String, ByVal UserID As String) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(10) As SqlParameter
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = company

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@usercode"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = UsrCode

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@userdesc"
                sqlParams(2).DbType = DbType.String
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = UsrDesc

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@usertype"
                sqlParams(3).DbType = DbType.String
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = UsrType

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@userlevel"
                sqlParams(4).DbType = DbType.String
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = UsrLevel

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@status"
                sqlParams(5).DbType = DbType.String
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = Status

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@idflag"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = IDFlag

                sqlParams(7) = New SqlParameter()
                sqlParams(7).ParameterName = "@brflag"
                sqlParams(7).DbType = DbType.String
                sqlParams(7).Direction = Data.ParameterDirection.Input
                sqlParams(7).Value = BrFlag

                sqlParams(8) = New SqlParameter()
                sqlParams(8).ParameterName = "@comflag"
                sqlParams(8).DbType = DbType.String
                sqlParams(8).Direction = Data.ParameterDirection.Input
                sqlParams(8).Value = ComFlag

                sqlParams(9) = New SqlParameter()
                sqlParams(9).ParameterName = "@deptflag"
                sqlParams(9).DbType = DbType.String
                sqlParams(9).Direction = Data.ParameterDirection.Input
                sqlParams(9).Value = DeptFlag

                sqlParams(10) = New SqlParameter()
                sqlParams(10).ParameterName = "@userid"
                sqlParams(10).DbType = DbType.String
                sqlParams(10).Direction = Data.ParameterDirection.Input
                sqlParams(10).Value = UserID


                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "User_Group_Insert"

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

#Region "Get UserGroup"

        Function GetUserGroup(ByVal Company As String, ByVal UserGroup As String) As DataSet

            Dim UserGroupDetails As System.Data.DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usergroup"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserGroup



            UserGroupDetails = DBFactory.GetHelper().ExecuteDataSet("Get_User_Group", Data.CommandType.StoredProcedure, sqlParams)

            Return UserGroupDetails
        End Function
#End Region

#Region "Update UserGroup"
        Function UpdateUsrGrp(ByVal company As String, ByVal UsrCode As String, ByVal UsrDesc As String, ByVal UsrType As String, ByVal UsrLevel As String, ByVal Status As String, ByVal IDFlag As String, ByVal BrFlag As String, ByVal ComFlag As String, ByVal DeptFlag As String, ByVal UserID As String) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(10) As SqlParameter
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = company

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@usercode"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = UsrCode

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@userdesc"
                sqlParams(2).DbType = DbType.String
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = UsrDesc

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@usertype"
                sqlParams(3).DbType = DbType.String
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = UsrType

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@userlevel"
                sqlParams(4).DbType = DbType.String
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = UsrLevel

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@status"
                sqlParams(5).DbType = DbType.String
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = Status

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@idflag"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = IDFlag

                sqlParams(7) = New SqlParameter()
                sqlParams(7).ParameterName = "@brflag"
                sqlParams(7).DbType = DbType.String
                sqlParams(7).Direction = Data.ParameterDirection.Input
                sqlParams(7).Value = BrFlag

                sqlParams(8) = New SqlParameter()
                sqlParams(8).ParameterName = "@comflag"
                sqlParams(8).DbType = DbType.String
                sqlParams(8).Direction = Data.ParameterDirection.Input
                sqlParams(8).Value = ComFlag

                sqlParams(9) = New SqlParameter()
                sqlParams(9).ParameterName = "@deptflag"
                sqlParams(9).DbType = DbType.String
                sqlParams(9).Direction = Data.ParameterDirection.Input
                sqlParams(9).Value = DeptFlag

                sqlParams(10) = New SqlParameter()
                sqlParams(10).ParameterName = "@userid"
                sqlParams(10).DbType = DbType.String
                sqlParams(10).Direction = Data.ParameterDirection.Input
                sqlParams(10).Value = UserID


                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "User_Group_Update"

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

#Region "UserGroup Already Exists"
        Function GetUserGroupExist(ByVal Company As String, ByVal UserGroup As String) As DataSet
            Dim UsrGrp As DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@UserGroup"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserGroup


            UsrGrp = DBFactory.GetHelper().ExecuteDataSet("UserGroup_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

            Return UsrGrp
        End Function
#End Region

    End Class
End Namespace
