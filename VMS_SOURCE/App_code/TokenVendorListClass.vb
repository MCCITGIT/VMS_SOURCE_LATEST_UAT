
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class TokenVendorListClass
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
#Region "Get token vendor for Unit"
    Public Function GetTokenVendorList(ByVal unit As String, ByVal search As String, ByVal active As String) As DataSet
        Dim ProductList As DataSet

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
        sqlParams(1).Value = IIf(active.Equals(String.Empty), DBNull.Value, active)

        ProductList = DBFactory.GetHelper().ExecuteDataSet("Token_Vendor_List_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region

#Region "Token Vendor Insert Update"
    Public Function TokenVendorInsertUpdate(ByVal tvm_code As String, ByVal tvm_name As String, ByVal tvm_email As String, ByVal tvm_mobile As Int64, ByVal tvm_address As String, ByVal tvm_city As String, ByVal tvm_state As String, ByVal tvm_zip As Int64, ByVal userId As String, ByVal active As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(9) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@tvm_code"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = tvm_code

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@tvm_name"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = tvm_name

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@tvm_email"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = tvm_email

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@tvm_mobile"
            sqlParams(3).DbType = DbType.Int64
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = IIf(tvm_mobile.Equals(Integer.MinValue), DBNull.Value, tvm_mobile)

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@tvm_address"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = tvm_address

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@tvm_city"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = tvm_city

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tvm_state"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = tvm_state

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@tvm_zip"
            sqlParams(7).DbType = DbType.Int64
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = IIf(tvm_zip.Equals(Integer.MinValue), DBNull.Value, tvm_zip)

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@userId"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = userId

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@active"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = active

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Token_Vendor_Insert_Update"
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

#Region "Get Vendor  by tvm_code"
    Public Function GetTokenVendorGetBy_tvm_code(ByVal id As String) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@tvm_code"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = id

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[Token_Vendor_Get_By_tvm_code]", Data.CommandType.StoredProcedure, sqlParams)
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
        sqlParams(1).ParameterName = "@product"
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
    Public Function InsertApplicableProduct(ByVal unit As String, ByVal product As String, ByVal packsize As String, ByVal userId As String, ByVal status As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(5) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@product"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = product

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@packsize"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = packsize

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@userid"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = userId

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@status"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = status

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@output"
            sqlParams(5).DbType = DbType.Int32
            sqlParams(5).Direction = Data.ParameterDirection.Output

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Unit_Applicable_Product_Assign_Add_Update"
            sqlCmd.Parameters.AddRange(sqlParams)
            sqlCmd.ExecuteNonQuery()
            numRowsAffected = Convert.ToInt32(sqlParams(5).Value.ToString)
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function
#End Region

End Class

