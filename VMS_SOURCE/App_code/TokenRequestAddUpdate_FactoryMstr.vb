Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Public Class TokenRequestAddUpdate_FactoryMstr

#Region "Get Denomination value"

    Public Function GetProductDenominationValue(ByVal Product As String, ByVal Packsize As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Product"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Product

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Packsize"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Packsize

        ds = DBFactory.GetHelper().ExecuteDataSet("[TOKEN_GENERATION_BERGER_DB].[dbo].[TokenRequestAdd_getDenominationValue]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region

#Region "Get Karton Capacity List "
    Public Function GetKartonCapacity(ByVal ParamName As String) As DataSet
        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@ParamName"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = ParamName

        ds = DBFactory.GetHelper().ExecuteDataSet("[TOKEN_GENERATION_BERGER_DB].[dbo].[TokenRequestAdd_getKartonCapacity]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds
    End Function
#End Region

#Region "Get Token Month "
    Public Function GetTokenMonth(ByVal CurrentMonth As String) As DataSet
        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@CurrentMonth"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = CurrentMonth

        ds = DBFactory.GetHelper().ExecuteDataSet("[TOKEN_GENERATION_BERGER_DB].[dbo].[TokenRequestAdd_getTokenMonth]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds
    End Function
#End Region


#Region "Get Token Month "
    Public Function GetMailIds(ByVal ParamName As String) As DataSet
        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@ParamName"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = ParamName

        ds = DBFactory.GetHelper().ExecuteDataSet("[TokenRequestAdd_getmailId]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds
    End Function
#End Region


End Class
