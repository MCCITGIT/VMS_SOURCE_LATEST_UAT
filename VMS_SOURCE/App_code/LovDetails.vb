'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/LovDetails.vb
'Created Date	: 24-November-2007
'Created By	    : Arun
'Version	    : R02.00.00
'Description	: Code behind file for LovDetails Class

'Modified By       Modified On       Version         Reason

'*************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class LovDetails

#Region "Get LovDetails List"

    Function GetLovDetailsList(ByVal Company As String, ByVal lovtype As String) As DataSet

        Dim LovDetailsDetails As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@lovtype"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = lovtype



        LovDetailsDetails = DBFactory.GetHelper().ExecuteDataSet("Lov_Details_List", Data.CommandType.StoredProcedure, sqlParams)

        Return LovDetailsDetails
    End Function
#End Region

#Region "Insert LovDetails"
    Function InsertLovDetails(ByVal company As String, ByVal Type As String, ByVal Desc As String, ByVal Value As String, ByVal Seq As Integer, ByVal Field1 As String, ByVal Field2 As String, ByVal Field3 As String, ByVal Active As String, ByVal UserID As String, ByVal Code As String) As Integer
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
            sqlParams(1).ParameterName = "@type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Type

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@desc"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Desc

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@value"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Value

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@seq"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Seq

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@field1"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Field1

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@field2"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = Field2

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@field3"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Field3

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@active"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = Active

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@userid"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = UserID

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@code"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = Code


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Lov_Details_Insert"

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

#Region "Update LovDetails"
    Function LovDetailsUpdate(ByVal company As String, ByVal Type As String, ByVal Desc As String, ByVal Value As String, ByVal Seq As Integer, ByVal Field1 As String, ByVal Field2 As String, ByVal Field3 As String, ByVal Active As String, ByVal UserID As String, ByVal Code As String, ByVal hdnCode As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(11) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Type

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@desc"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Desc

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@value"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Value

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@seq"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Seq

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@field1"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Field1

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@field2"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = Field2

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@field3"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Field3

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@active"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = Active

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@userid"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = UserID

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@code"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = Code

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@hdncode"
            sqlParams(11).DbType = DbType.String
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = hdnCode


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Lov_Details_Update"

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

#Region "Get LovMstr List"

    Function GetLovMstrList(ByVal Company As String) As DataSet

        Dim LovDetailsDetails As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        


        LovDetailsDetails = DBFactory.GetHelper().ExecuteDataSet("Lov_Mstr_List", Data.CommandType.StoredProcedure, sqlParams)

        Return LovDetailsDetails
    End Function
#End Region

#Region "Insert LovMstr"
    Function InsertLovMstr(ByVal company As String, ByVal Type As String, ByVal Desc As String, ByVal Value As String, ByVal Seq As Integer, ByVal Field1 As String, ByVal Field2 As String, ByVal Field3 As String, ByVal Active As String, ByVal UserID As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(9) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Type

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@desc"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Desc

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@value"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Value

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@seq"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Seq

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@field1"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Field1

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@field2"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = Field2

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@field3"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Field3

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@active"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = Active

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@userid"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = UserID

            


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Lov_Mstr_Insert"

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

#Region "Update LovMstr"
    Function LovMstrUpdate(ByVal company As String, ByVal Type As String, ByVal Desc As String, ByVal Value As String, ByVal Seq As Integer, ByVal Field1 As String, ByVal Field2 As String, ByVal Field3 As String, ByVal Active As String, ByVal UserID As String, ByVal hdnType As String) As Integer
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
            sqlParams(1).ParameterName = "@type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Type

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@desc"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Desc

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@value"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Value

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@seq"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Seq

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@field1"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Field1

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@field2"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = Field2

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@field3"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Field3

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@active"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = Active

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@userid"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = UserID

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@hdntype"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = hdnType


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Lov_Mstr_Update"

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

#Region "Get MenuMstr List"

    Function GetMenuMstrList(ByVal parentid As Int64) As DataSet

        Dim LovDetailsDetails As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@fmm_parent_id"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = parentid

        LovDetailsDetails = DBFactory.GetHelper().ExecuteDataSet("[dbo].[FormMenu_GetList]", Data.CommandType.StoredProcedure, sqlParams)

        Return LovDetailsDetails
    End Function
#End Region

#Region "Insert LovMstr"
    Function InsertMenuMstr(ByVal parentid As Int64, ByVal frmName As String, ByVal frmlink As String, ByVal Seq As Integer, ByVal Active As String, ByVal UserID As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(5) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@fmm_name"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = frmName

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fmm_link"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = frmlink

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fmm_parent_id"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = parentid

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@fmm_sequence"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Seq

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Active

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = UserID

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[FormMenu_Mstr_Insert]"

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

#Region "Update MenuMstr"
    Function MenuMstrUpdate(ByVal frmid As Int64, ByVal parentid As Int64, ByVal frmName As String, ByVal frmlink As String, ByVal Seq As Integer, ByVal Active As String, ByVal UserID As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(6) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@fmm_id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = frmid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fmm_name"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = frmName

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fmm_link"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = frmlink

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@fmm_sequence"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Seq

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Active

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = UserID

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@fmm_parent_id"
            sqlParams(6).DbType = DbType.Int64
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = parentid


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[FormMenu_Update]"

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

#Region "AJAX MenuCode Exists"

    Function GetMenuMstrExist(ByVal Company As String, ByVal Mcode As String, ByVal Hcode As String) As DataSet

        Dim MenuCodeSet As System.Data.DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@mcode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Mcode

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@hcode"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Hcode

        MenuCodeSet = DBFactory.GetHelper().ExecuteDataSet("Menu_Mstr_Code_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

        Return MenuCodeSet

    End Function
#End Region

#Region "AJAX LovDetCode Exists"

    Function GetLovDetCodeExist(ByVal Company As String, ByVal type As String, ByVal Lcode As String, ByVal hcode As String) As DataSet

        Dim LovDetSet As System.Data.DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@type"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = type

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@Lcode"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Lcode

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@hcode"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = hcode

        LovDetSet = DBFactory.GetHelper().ExecuteDataSet("Lov_Details_Code_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

        Return LovDetSet

    End Function
#End Region

#Region "AJAX LovMstrType Exists"

    Function GetLovMstrTypeExist(ByVal Company As String, ByVal type As String, ByVal htype As String) As DataSet

        Dim MstrTypeSet As System.Data.DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@type"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = type

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@htype"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = htype

        MstrTypeSet = DBFactory.GetHelper().ExecuteDataSet("Lov_Mstr_Type_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

        Return MstrTypeSet

    End Function
#End Region

#Region "Get Parent Menu List"

    Function GetParentMenuList() As DataSet
        Dim ParentMenuLit As System.Data.DataSet
        ParentMenuLit = DBFactory.GetHelper().ExecuteDataSet("[dbo].[FormMenu_GetParentFormList]", Data.CommandType.StoredProcedure)
        Return ParentMenuLit
    End Function
#End Region

End Class
