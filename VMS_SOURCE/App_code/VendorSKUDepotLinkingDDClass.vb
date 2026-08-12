Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class VendorSKUDepotLinkingDDClass
#Region "Update VendorSKUMstr"
    Function VendorSKUMstrInsertUpdate(ByVal Vendor As String, ByVal OldVendor As String, ByVal SKU As String, ByVal Active As String, ByVal UserID As String, ByVal Action As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(6) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@v_vendor_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Vendor

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@v_sku_code"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = SKU

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@active"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Active

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@userid"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = UserID

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@action"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Action

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@oldVendor"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(OldVendor <> String.Empty, OldVendor, DBNull.Value)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@output_code"
            sqlParams(6).DbType = DbType.Int32
            sqlParams(6).Direction = Data.ParameterDirection.Output
            sqlParams(6).Size = 100

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "VMS.[dbo].[DD_Vendor_SKU_Mstr_Insert_Update]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            numRowsAffected = Convert.ToInt32(sqlParams(6).Value.ToString)

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
#Region "Get VendorSKUMstr List"

    Function GetVendorSKUDetailsList(ByVal Vendor As String, ByVal SKU As String) As DataSet

        Dim VendorSKUDetails As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@v_vendor_unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Vendor <> String.Empty, Vendor, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@v_sku_code"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(SKU <> String.Empty, SKU, DBNull.Value)

        VendorSKUDetails = DBFactory.GetHelper().ExecuteDataSet("[dbo].[DD_Vendor_SKU_Details_List]", Data.CommandType.StoredProcedure, sqlParams)

        Return VendorSKUDetails
    End Function
#End Region

    Function GetSkuPartialSearch(ByVal desc As String) As DataSet

        Dim DS As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@desc"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = desc

        DS = DBFactory.GetHelper().ExecuteDataSet("ProductDetailAdd_Get_SKU_Partial_Search", Data.CommandType.StoredProcedure, sqlParams)

        Return DS

    End Function
End Class
