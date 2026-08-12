Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web
Imports VMS.DataAccess
Imports VMS.Web

Public Class TokenReceiveClass
    Public Shared Function DBNullValueorStringIfNotNull(ByVal value As String) As Object
        Dim o As Object

        If (value = String.Empty Or value Is Nothing) Then
            o = DBNull.Value
        Else
            o = value
        End If

        Return o
    End Function

    Public Shared Function DBNullValueIfZero(ByVal value As Integer) As Object
        Dim o As Object

        If (value = 0) Then
            o = DBNull.Value
        Else
            o = value
        End If

        Return o
    End Function
    Public Function GetFactory(ByVal user As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@user"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(user.Equals(String.Empty), DBNull.Value, user)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_receive_getFactory]", System.Data.CommandType.StoredProcedure, sqlParams)
        Catch ex As Exception
            Throw ex
        End Try

        Return DS
    End Function

    Public Function GetVendor(ByVal Factory As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(Factory.Equals(String.Empty), DBNull.Value, Factory)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_receive_getVendor]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetDespatchedCartonList(ByVal factory As String, ByVal vendor As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_receive_getDespatchedcartonList]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetDetailsForEdit(ByVal trh_id As Int32) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@trh_id"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = trh_id

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_receive_getDetails]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function
    Public Function GetList(ByVal factory As String, ByVal vendor As String, ByVal User As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@User"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(User.Equals(String.Empty), DBNull.Value, User)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_receive_getList]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function Insert(ByVal entity As TokenReceiveEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Int32
        Dim returnResult As Int32 = 0
        Try

            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(entity.trh_factory_code.Equals(String.Empty), DBNull.Value, entity.trh_factory_code)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(entity.trh_vendor_code.Equals(String.Empty), DBNull.Value, entity.trh_vendor_code)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@created_user"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = entity.created_user

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@tbl1"
            sqlParams(3).SqlDbType = SqlDbType.Structured
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Common.ConvertToDataTable(entity.dtlTokenReceive)

            Dim sqlCmd As SqlCommand = New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "value_token_receive_Insert"
            sqlCmd.Parameters.AddRange(sqlParams)
            returnResult = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try
        Return returnResult
    End Function

    'Public Function GetTokenDespatchList(ByVal factory As String, ByVal vendor As String, ByVal user_id As String) As DataSet
    '    Dim DS As System.Data.DataSet = New System.Data.DataSet()
    '    Try
    '        Dim sqlParams(2) As SqlParameter

    '        sqlParams(0) = New SqlParameter()
    '        sqlParams(0).ParameterName = "@factory"
    '        sqlParams(0).DbType = DbType.String
    '        sqlParams(0).Direction = Data.ParameterDirection.Input
    '        sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)

    '        sqlParams(1) = New SqlParameter()
    '        sqlParams(1).ParameterName = "@vendor"
    '        sqlParams(1).DbType = DbType.String
    '        sqlParams(1).Direction = Data.ParameterDirection.Input
    '        sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

    '        sqlParams(2) = New SqlParameter()
    '        sqlParams(2).ParameterName = "@user_id"
    '        sqlParams(2).DbType = DbType.String
    '        sqlParams(2).Direction = Data.ParameterDirection.Input
    '        sqlParams(2).Value = IIf(user_id.Equals(String.Empty), DBNull.Value, user_id)

    '        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VMS_TokenDespatchListGet]", System.Data.CommandType.StoredProcedure, sqlParams)

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Return DS
    'End Function

    Public Function GetTokenDespatchList(ByVal factory As String, ByVal vendor As String, ByVal user_id As String, ByVal status As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@user_id"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(user_id.Equals(String.Empty), DBNull.Value, user_id)

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@status"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = IIf(status.Equals(String.Empty), DBNull.Value, status)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VMS_TokenDespatchListGet]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetTokenDespatchDetails(ByVal tdh_id As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@tdh_id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = tdh_id

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VMS_getDespatchedDetails]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function
End Class
