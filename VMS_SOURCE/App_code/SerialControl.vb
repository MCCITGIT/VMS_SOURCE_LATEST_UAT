'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/SerialConvert.vb
'Created Date	: 25-November-2007
'Created By	    : Arun
'Version	    : R02.00.00
'Description	: Code behind file for SerialConvert Class

'Modified By       Modified On       Version         Reason

'*************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Public Class SerialControl

#Region "Get UserGroup List"

    Function GetSerialNoControlList(ByVal Company As String, ByVal finyear As String) As DataSet

        Dim SerialControlDetails As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@finyear"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = finyear



        SerialControlDetails = DBFactory.GetHelper().ExecuteDataSet("Serial_No_Control_List", Data.CommandType.StoredProcedure, sqlParams)

        Return SerialControlDetails
    End Function
#End Region

#Region "Get SerialCtrl"

    Function GetSerialCtrl(ByVal Company As String, ByVal Year As String, ByVal DocType As String, ByVal srlid As Integer) As DataSet

        Dim ExtConvertDetails As System.Data.DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Year

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@doctype"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = DocType

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@srlid"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = srlid

        ExtConvertDetails = DBFactory.GetHelper().ExecuteDataSet("Srl_Convert_Get", Data.CommandType.StoredProcedure, sqlParams)

        Return ExtConvertDetails
    End Function
#End Region

#Region "Update SrlCntrl"
    Function UpdateSrlCntrl(ByVal company As String, ByVal Year As String, ByVal Doc As String, ByVal Loc As String, ByVal Dept As String, ByVal Prefix As String, ByVal No As Integer, ByVal Status As String, ByVal UserID As String, ByVal Incr As Integer, ByVal srlid As Integer) As Integer
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
            sqlParams(1).ParameterName = "@year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Year

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@doc"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Doc

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@loc"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Loc

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@dept"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Dept

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@prefix"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Prefix

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@no"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = No

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@status"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Status

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@userid"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = UserID

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@incr"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = Incr

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@srlid"
            sqlParams(10).DbType = DbType.Int32
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = srlid

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Srl_Ctrl_Update"

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

#Region "Insert SrlCntrl"
    Function InsertSrlCntrl(ByVal company As String, ByVal Year As String, ByVal Doc As String, ByVal Loc As String, ByVal Dept As String, ByVal Prefix As String, ByVal No As Integer, ByVal Status As String, ByVal UserID As String, ByVal Incr As Integer, ByVal srlid As Integer) As Integer
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
            sqlParams(1).ParameterName = "@year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Year

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@doc"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Doc

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@loc"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Loc

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@dept"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Dept

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@prefix"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Prefix

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@no"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = No


            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@status"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Status

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@userid"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = UserID

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@incr"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = Incr

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Srl_Ctrl_Insert"

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

#Region "Get SrlCntrl"

    Function GetSrlCntrlExist(ByVal Company As String, ByVal Year As String, ByVal Doc As String, ByVal screenStatus As String, ByVal srlid As String, ByVal srlloc As String) As DataSet

        Dim ExtConvertDetails As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Year

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@doc"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Doc

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@screenstatus"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = screenStatus

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@srlid"
        sqlParams(4).DbType = DbType.Int32
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = srlid

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@srl_loc"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = srlloc

        ExtConvertDetails = DBFactory.GetHelper().ExecuteDataSet("Srl_Cntrl_Exists", Data.CommandType.StoredProcedure, sqlParams)

        Return ExtConvertDetails
    End Function
#End Region

End Class
