Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class VendorMaster

    '#Region "Get Vendor Unit "
    '    Public Function GetVendorUnit(ByVal active As String,ByVal  ) As DataSet
    '        Dim PrjectList As DataSet
    '        Dim sqlParams(1) As SqlParameter
    '        sqlParams(0) = New SqlParameter()
    '        sqlParams(0).ParameterName = "@"
    '        sqlParams(0).DbType = DbType.String
    '        sqlParams(0).Direction = Data.ParameterDirection.Input
    '        sqlParams(0).Value = 

    '        sqlParams(1) = New SqlParameter()
    '        sqlParams(1).ParameterName = "@active"
    '        sqlParams(1).DbType = DbType.String
    '        sqlParams(1).Direction = Data.ParameterDirection.Input
    '        sqlParams(1).Value = active
    '        PrjectList = DBFactory.GetHelper().ExecuteDataSet("VendorMaster_GetUnitName", Data.CommandType.StoredProcedure, sqlParams)
    '        Return PrjectList

    '    End Function
    '#End Region
#Region "Get Vendor Unit "
    Public Function GetUnitName(ByVal active As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("VendorMaster_GetUnitName", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
#Region "Vendor SKU Search Result"

    Function GetVendorList(ByVal VendorUnit As String, ByVal SKUCode As String) As DataSet
        Dim VendorSKUDetails As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@VendorUnit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(VendorUnit <> String.Empty, VendorUnit, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@SKUCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(SKUCode <> String.Empty, SKUCode, DBNull.Value)

        VendorSKUDetails = DBFactory.GetHelper().ExecuteDataSet("VendorMaster_SearchList", Data.CommandType.StoredProcedure, sqlParams)
        Return VendorSKUDetails
    End Function
#End Region
#Region "Vendor SKU Search Result for SKU Code"
    Function GetVendorListForSKUCode(ByVal SKUCode As String) As DataSet
        Dim VendorSKUDetails As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@SKUCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(SKUCode <> String.Empty, SKUCode, DBNull.Value)

        VendorSKUDetails = DBFactory.GetHelper().ExecuteDataSet("VendorMaster_SearchResult", Data.CommandType.StoredProcedure, sqlParams)

        Return VendorSKUDetails
    End Function
#End Region
#Region "Get Vendor Details"
    Function VendorDetails(ByVal v_sku_code As String, ByVal VendorUnit As String) As DataSet

        Dim dsVendorDetails As System.Data.DataSet
        Dim GetVendorDetails As New VendorMasterEntity
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@VendorUnit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(VendorUnit <> String.Empty, VendorUnit, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@v_sku_code"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(v_sku_code <> String.Empty, v_sku_code, DBNull.Value)

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@DepotCode"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = IIf(DepotCode <> String.Empty, DepotCode, DBNull.Value)

        dsVendorDetails = DBFactory.GetHelper().ExecuteDataSet("VendorMaster_GetVendorDetails", Data.CommandType.StoredProcedure, sqlParams)

        'GetVendorDetails.VendorRegion = dsVendorDetails.Tables(0).Rows(0)("depot_regn").ToString.Trim
        'GetVendorDetails.VendorDepot = dsVendorDetails.Tables(0).Rows(0)("depot_code").ToString.Trim
        'GetVendorDetails.VendorName = dsVendorDetails.Tables(0).Rows(0)("depot_name").ToString.Trim
        'GetVendorDetails.VendorTSL = dsVendorDetails.Tables(0).Rows(0)("v_tsl_factor").ToString.Trim
        'GetVendorDetails.VendorPA = dsVendorDetails.Tables(0).Rows(0)("v_primary_secondary").ToString.Trim
        Return dsVendorDetails
    End Function
#End Region
#Region "Populate Vendor SKU For Region"

    Function GetVendorListRegion(ByVal VendorRegion As String, ByVal VendorSku As String, ByVal VendorUnit As String) As DataSet
        Dim VendorSKUDetails As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@VendorRegion"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(VendorRegion <> String.Empty, VendorRegion, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@v_sku_code"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(VendorSku <> String.Empty, VendorSku, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@vendor_unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(VendorUnit <> String.Empty, VendorUnit, DBNull.Value)

        VendorSKUDetails = DBFactory.GetHelper().ExecuteDataSet("VendorMaster_SearchListForRegion", Data.CommandType.StoredProcedure, sqlParams)

        Return VendorSKUDetails
    End Function
#End Region
#Region "Insert Vendor Details"
    Public Function InsrtVendorDetails(ByVal depotCode As String, ByVal VendorUnit As String, ByVal tsl As Decimal, ByVal Ps As String, ByVal created_user As String, ByVal active As String, ByVal SkuCode As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As New Integer
        Try
            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depotCode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = depotCode

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@VendorUnit"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(VendorUnit <> String.Empty, VendorUnit, DBNull.Value)


            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@tsl"
            sqlParams(2).DbType = DbType.Decimal
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(tsl <> Decimal.MinValue, tsl, DBNull.Value)


            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@Ps"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = IIf(Ps <> String.Empty, Ps, DBNull.Value)

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@created_user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = created_user


            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@active"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = active

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@SkuCode"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = SkuCode

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "VendorSkuMaster_Insert"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            Return numRowsAffected
        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region
#Region "Delete Vendor Sku Master Details"
    Public Function DeleteVendorSkuMaster(ByVal sku_code As String, ByVal vendor_unit As String, ByVal active As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As New Integer
        Try
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@sku_code"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = sku_code

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor_unit"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = vendor_unit

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@active"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = active

            
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[VendorSkuMaster_Delete]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
        Return numRowsAffected
    End Function
#End Region
#Region "Update Vendor Master Details"
    Public Function UpdateVendorDetails(ByVal VendorUnit As String, ByVal Tsl As Decimal, ByVal Ps As String, ByVal ModifiedUsr As String, ByVal active As String, ByVal skuCode As String, ByVal depotCode As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As New Integer
        Try
            Dim sqlParams(6) As SqlParameter
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@VendorUnit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(VendorUnit <> String.Empty, VendorUnit, DBNull.Value)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@tsl"
            sqlParams(1).DbType = DbType.Decimal
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(Tsl <> Decimal.MinValue, Tsl, DBNull.Value)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@Ps"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(Ps <> String.Empty, Ps, DBNull.Value)

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ModifiedUsr"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = ModifiedUsr

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = active

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@SkuCode"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(skuCode <> String.Empty, skuCode, DBNull.Value)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@depotCode"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = IIf(depotCode <> String.Empty, depotCode, DBNull.Value)


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[VendorSkuMaster_Update]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
        Return numRowsAffected
    End Function
#End Region
#Region "Delete Vendor Sku Master Rows "
    Public Function DeleteVendorSkuMasterOneRow(ByVal depotCode As String, ByVal sku_code As String, ByVal vendor_unit As String, ByVal active As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As New Integer
        Try
            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@sku_code"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = sku_code

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor_unit"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = vendor_unit

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@active"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = active

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@depotCode"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = depotCode


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "VendorSkuMaster_DeleteOneRows"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
        Return numRowsAffected
    End Function
#End Region
End Class
