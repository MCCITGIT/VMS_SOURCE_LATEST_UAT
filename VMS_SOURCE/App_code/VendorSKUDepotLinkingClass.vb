
Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient
Public Class VendorSKUDepotLinkingClass
#Region "Get VendorSKUMstr List"

    Function GetVendorSKUDetailsList(ByVal Vendor As String, ByVal Depot As String, ByVal SKU As String, ByVal Region As String) As DataSet

        Dim VendorSKUDetails As System.Data.DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@v_vendor_unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Vendor <> String.Empty, Vendor, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@v_depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(Depot <> String.Empty, Depot, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@v_sku_code"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(SKU <> String.Empty, SKU, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@region"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(Region <> String.Empty, Region, DBNull.Value)



        VendorSKUDetails = DBFactory.GetHelper().ExecuteDataSet("Vendor_SKU_Details_List", Data.CommandType.StoredProcedure, sqlParams)

        Return VendorSKUDetails
    End Function
#End Region
#Region "Update VendorSKUMstr"
    Function VendorSKUMstrInsertUpdate(ByVal Depot As String, ByVal Vendor As String, ByVal SKU As String, ByVal tsl_factor As Decimal, ByVal primary_secondary As String, ByVal Active As String, ByVal UserID As String, ByVal Action As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(7) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@v_depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Depot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@v_vendor_unit"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Vendor

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@v_sku_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = SKU

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@active"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Active

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@userid"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = UserID

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@action"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Action

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@v_tsl_factor"
            sqlParams(6).DbType = DbType.Decimal
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = tsl_factor

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@v_primary_secondary"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = primary_secondary



            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Vendor_SKU_Mstr_Insert_Update"

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
