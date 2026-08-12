Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class UnitApplicableVendorAssignClass
#Region "Get Vendor Unit "
    Public Function GetTokenVendorList(ByVal search As String, ByVal active As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@search"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(search.Equals(String.Empty), DBNull.Value, search)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@active"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = active

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Token_Vendor_List_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
#Region "Get Product List For Assignment"
    Public Function AssignTokenVendor(ByVal unit As String, ByVal product As String, ByVal tokenVendor As String, ByVal userId As String, ByVal status As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(4) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@uav_unit_code"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@uav_sku"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = product

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@uav_token_vendor_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = tokenVendor

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@userId"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = userId

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = status

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Token_Vendor_To_Unit_Assign"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function
#End Region
#Region "Get Product List For Assignment"
    Public Function GetProductList(ByVal unit As String, ByVal product As String, ByVal status As String) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = unit

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@product"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(product.Equals(""), DBNull.Value, product)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@status"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(status.Equals(""), DBNull.Value, status)

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[Unit_Vendor_assign_Product_Vendor_List_Get]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region
End Class
