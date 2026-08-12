Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient
Public Class BlockIndentClass
    Public Function Get_Sku_Details(ByVal skucode As String) As DataSet
        Dim DepotDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@skucode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = skucode

        DepotDS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_SkuDesc]", Data.CommandType.StoredProcedure, sqlParams)
        Return DepotDS
    End Function
    Public Function Get_BlockIndent_Sku_Details(ByVal skucode As String) As DataSet
        Dim DepotDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@skucode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(skucode), DBNull.Value, skucode)

        DepotDS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_BlockIndentSkuMstr_List]", Data.CommandType.StoredProcedure, sqlParams)
        Return DepotDS
    End Function
    Public Function InsertUpdate_BlockSku_Details(ByVal skucode As String, ByVal skuDesc As String, ByVal userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim intds As Integer

        Try
            Dim sqlparams(2) As SqlParameter

            sqlparams(0) = New SqlParameter()
            sqlparams(0).ParameterName = "@skucode"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = ParameterDirection.Input
            sqlparams(0).Value = skucode

            sqlparams(1) = New SqlParameter()
            sqlparams(1).ParameterName = "@skuname"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = ParameterDirection.Input
            sqlparams(1).Value = skuDesc

            sqlparams(2) = New SqlParameter()
            sqlparams(2).ParameterName = "@created_user"
            sqlparams(2).DbType = DbType.String
            sqlparams(2).Direction = ParameterDirection.Input
            sqlparams(2).Value = userid

            Dim sqlcmd As SqlCommand = New SqlCommand()
            sqlcmd.CommandText = "[dbo].[Insert_BlockIndentSku_Mstr]"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Connection = sqlConn
            sqlcmd.Transaction = sqlTrans
            sqlcmd.Parameters.AddRange(sqlparams)
            intds = sqlcmd.ExecuteNonQuery()
            Return intds

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete_BlockSku_Details(ByVal skucode As String, ByVal userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim intds As Integer

        Try
            Dim sqlparams(1) As SqlParameter

            sqlparams(0) = New SqlParameter()
            sqlparams(0).ParameterName = "@skucode"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = ParameterDirection.Input
            sqlparams(0).Value = skucode

            sqlparams(1) = New SqlParameter()
            sqlparams(1).ParameterName = "@created_user"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = ParameterDirection.Input
            sqlparams(1).Value = userid

            Dim sqlcmd As SqlCommand = New SqlCommand()
            sqlcmd.CommandText = "[dbo].[Delete_BlockIndent]"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Connection = sqlConn
            sqlcmd.Transaction = sqlTrans
            sqlcmd.Parameters.AddRange(sqlparams)
            intds = sqlcmd.ExecuteNonQuery()
            Return intds

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
