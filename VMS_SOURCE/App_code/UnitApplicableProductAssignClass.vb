Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class UnitApplicableProductAssignClass
#Region "Get Vendor Unit "
    Public Function GetUnitName(ByVal active As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("PendingDespatches_GetUnitName", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
#Region "Get Product Unit for Unit"
    Public Function GetProductNameFromUnit(ByVal unit As String) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit.Equals(""), DBNull.Value, unit)

        ProductList = DBFactory.GetHelper().ExecuteDataSet("Product_List_from_Unit_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

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
        sqlParams(1).ParameterName = "@skuCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(product.Equals(""), DBNull.Value, product)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@status"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(status.Equals(""), DBNull.Value, status)

        ProductList = DBFactory.GetHelper().ExecuteDataSet("Unit_Product_assign_ProductList_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region
#Region "Insert Applicable Product"
    Public Function InsertApplicableProduct(ByVal unit As String, ByVal skucode As String, ByVal denomination As Decimal, ByVal tokenVendor As String, ByVal userId As String, ByVal status As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@skucode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = skucode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@uap_denomination"
            sqlParams(2).DbType = DbType.Decimal
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = denomination

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@uap_token_vendor"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = tokenVendor

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@userid"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = userId

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@status"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = status

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@output"
            sqlParams(6).DbType = DbType.Int32
            sqlParams(6).Direction = Data.ParameterDirection.Output

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Unit_Applicable_Product_Assign_Add_Update"
            sqlCmd.Parameters.AddRange(sqlParams)
            sqlCmd.ExecuteNonQuery()

            numRowsAffected = Convert.ToInt32(sqlParams(6).Value.ToString)
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function
#End Region
#Region "Get Unit applicable site "
    Public Function GetUnitApplicableSites(ByVal unit As String, ByVal active As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@utas_unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = unit

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@active"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(active.Equals(String.Empty), DBNull.Value, active)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("GET_UNIT_APPLICABLE_SITE_TOKEN", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
End Class
