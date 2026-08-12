'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/UserGroup.vb
'Created Date	: 3-Decemkber-2007
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
Public Class UserFormAccess

#Region "UserGroup_Get"
    Public Function UserID_Get(ByVal Company As String, ByVal UsrGrp As String) As DataSet
        Dim UserGroupSet As New DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@usrgrp"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = UsrGrp

        UserGroupSet = DBFactory.GetHelper().ExecuteDataSet("UserGrp_ID_get", Data.CommandType.StoredProcedure, sqlParams)
        Return UserGroupSet
    End Function
#End Region

#Region "UserForms_Get"
    Public Function UserForms_Get(ByVal Company As String, ByVal UsrGrp As String, ByVal UsrID As String) As DataSet
        Dim UserGroupSet As New DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@usrgrp"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = UsrGrp

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@usrid"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = UsrID

        UserGroupSet = DBFactory.GetHelper().ExecuteDataSet("UserAvlbForms_get", Data.CommandType.StoredProcedure, sqlParams)
        Return UserGroupSet
    End Function
#End Region

#Region "UserApplForms_Get"
    Public Function UserApplForms_Get(ByVal Company As String, ByVal UsrGrp As String, ByVal UsrID As String) As DataSet
        Dim UserGroupSet As New DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@usrgrp"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = UsrGrp

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@usrid"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = UsrID

        UserGroupSet = DBFactory.GetHelper().ExecuteDataSet("UserApplForms_get", Data.CommandType.StoredProcedure, sqlParams)
        Return UserGroupSet
    End Function
#End Region

#Region "Insert UserForm"
    Function InsertUsrFrm(ByVal company As String, ByVal Desc As String, ByVal Code As String, ByVal User As String, ByVal GroupCode As String, ByVal UserID As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(5) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@desc"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Desc

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Code

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@user"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = User

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@groupcode"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = GroupCode

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@userid"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = UserID


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "User_Form_Access_Insert"

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

#Region "User Access Forms and Details_Get"

    Public Function User_Privileges_Get(ByVal Company As String, ByVal UsrGrp As String) As DataSet

        Dim User_PrivilegesSet As New DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@usr_group_code"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = UsrGrp

        User_PrivilegesSet = DBFactory.GetHelper().ExecuteDataSet("User_Privileges_Get", Data.CommandType.StoredProcedure, sqlParams)

        Return User_PrivilegesSet

    End Function

#End Region

#Region "User Privileges Details Update"

    Public Function UserPrivileges_Update(ByVal Company As String, ByVal UsrGrpCode As String, ByVal UsrAccessType As String, ByVal QuickLink As String, ByVal ModifiedUsr As String, ByVal hdnFormCode As String) As Integer

        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(5) As SqlParameter

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usr_group_code"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UsrGrpCode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@usr_access_type"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = UsrAccessType

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@quick_link"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = QuickLink

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@modified_user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = ModifiedUsr

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@hdnFormCode"
            sqlParams(5).DbType = DbType.Int32
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = hdnFormCode

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "User_Privileges_Update"

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

#Region "Delete UserForm"
    Function DeleteUsrFrm(ByVal company As String, ByVal formcode As Integer, ByVal GroupCode As String, ByVal UserID As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsDeleted As Integer
        Dim sqlParams(3) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@formcode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = formcode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@groupcode"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = GroupCode

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@userid"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = UserID


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "User_Form_Access_Delete"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsDeleted = sqlCmd.ExecuteNonQuery()

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

        Return numRowsDeleted

    End Function
#End Region

End Class
