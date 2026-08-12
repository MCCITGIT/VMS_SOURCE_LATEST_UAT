Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class QualityControlClass


#Region "Brand Master Class"
    Function InsertUpdateBrandMasterDtls(ByRef BrandMasterEntity As BrandMasterEntity) As Integer
        Dim sqlConn As SqlConnection = Nothing
        sqlConn = DBFactory.GetHelper.OpenConnection()
        'sqlTrans = sqlConn.BeginTransaction
        Dim MsgID As Integer

        Dim sqlParams(4) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@bm_brand_name"
            sqlParams(0).SqlDbType = SqlDbType.VarChar
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = BrandMasterEntity.BName

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@user_id"
            sqlParams(1).SqlDbType = SqlDbType.VarChar
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = BrandMasterEntity.CreatedUser

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@trantype"
            sqlParams(2).SqlDbType = SqlDbType.Int
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = BrandMasterEntity.Trantype

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@bm_brand_id"
            sqlParams(3).SqlDbType = SqlDbType.Int
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = BrandMasterEntity.BID

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@outputCode"
            sqlParams(4).DbType = DbType.Int64
            sqlParams(4).Direction = Data.ParameterDirection.Output
            sqlParams(4).Size = 100



            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            'sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[tc_brand_master_insertupdate]"
            sqlCmd.Parameters.AddRange(sqlParams)
            sqlCmd.ExecuteNonQuery()
            MsgID = CType(sqlParams(4).Value, Integer)
            'sqlTrans.Commit()
        Catch ex As Exception
            'If (sqlTrans IsNot Nothing) Then
            '    sqlTrans.Rollback()
            'End If
            Throw ex
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
        Return MsgID
    End Function
#Region "Bind Grid View"
    Function GetBrandMasterList() As DataSet

        Dim DS As System.Data.DataSet

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_get_brand_master_list]", Data.CommandType.StoredProcedure)
        Return DS
    End Function
#End Region
#Region "Get Brand By ID"
    Function GetBrandMasterByBrandId(ByVal Brandid As Integer) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brand_id"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Brandid
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_get_brand_master_bybrandid]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#End Region
#End Region


#Region "Brand Product Linking"
    Function Getbrand() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_get_brand_master_list]", Data.CommandType.StoredProcedure)
        Return DS
    End Function
    Function GetProduct(ByVal brandid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brandid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = brandid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_product_list_for_brand_link]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#Region "Insert Brand Product Linking"
    Function InsertBrandproductLink(ByVal brandid As Int64, ByVal user_id As String, ByVal tbl As DataTable) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        sqlConn = DBFactory.GetHelper.OpenConnection
        sqlTrans = sqlConn.BeginTransaction


        Dim sqlParams(2) As SqlParameter
        Try



            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@brandid"
            sqlParams(0).SqlDbType = SqlDbType.BigInt
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = brandid


            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@userid"
            sqlParams(1).SqlDbType = SqlDbType.VarChar
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = user_id

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@tbl"
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = tbl

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_brand_product_link_insert]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            sqlTrans.Commit()
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Throw ex
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
        Return numRowsAffected
    End Function
#End Region
#End Region


#Region "Product Vendor Linking"
    Function GetVendor(ByVal userid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid
        DS = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[tc_vendor_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Function GetProductByVendorId(ByVal vendorid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_product_list_for_vendor_link]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#Region "Insert Vendor Product Linking"
    Function InsertVendorproductLink(ByVal vendorid As String, ByVal user_id As String, ByVal tbl As DataTable) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        sqlConn = DBFactory.GetHelper.OpenConnection
        sqlTrans = sqlConn.BeginTransaction


        Dim sqlParams(2) As SqlParameter
        Try



            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendorid"
            sqlParams(0).SqlDbType = SqlDbType.VarChar
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = vendorid


            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@userid"
            sqlParams(1).SqlDbType = SqlDbType.VarChar
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = user_id

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@tbl"
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = tbl

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_vendor_product_link_insert]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            sqlTrans.Commit()
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Throw ex
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
        Return numRowsAffected
    End Function
#End Region
#End Region


#Region "Test Case Master"
    Public Function GetTestList(ByVal frequency As String, ByVal resultType As String, ByVal testName As String, ByVal userId As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@frequency"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(frequency), DBNull.Value, frequency)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@result_type"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(resultType), DBNull.Value, resultType)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@test_name"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = testName

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@user_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = userId

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_case_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
    Public Function GetTestData(ByVal testId As Int64, ByVal userId As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@th_test_id"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = testId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_case_details_by_test_id]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
    Public Function TestCaseInsertUpdate(ByVal test_id As Int64, ByVal test_name As String, ByVal frequency As String, ByVal result_type As String, ByVal result_sub_type As String, ByVal tbl As DataTable, ByVal userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(8) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@th_test_id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = test_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@th_test_name"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = test_name

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@th_srl_no"
            sqlParams(2).DbType = DbType.Int32
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = 0

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@th_frequency"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = frequency

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@th_type"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = result_type

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@th_sub_type"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = result_sub_type

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tbl"
            sqlParams(6).SqlDbType = SqlDbType.Structured
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = tbl

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@created_user"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = userid

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@output"
            sqlParams(8).DbType = DbType.Int32
            sqlParams(8).Direction = Data.ParameterDirection.Output

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_test_case_insert_update]"
            sqlCmd.Parameters.AddRange(sqlParams)

            numRowsAffected = sqlCmd.ExecuteNonQuery()
            If (Integer.TryParse(sqlParams(8).Value, output)) Then
                result = Integer.Parse(sqlParams(8).Value)
            End If

        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return result

    End Function
#End Region

#Region "Brand Test Linking"
    Function GetTest(ByVal brand_id As Int64, ByVal userId As String, ByVal product_id As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brand_id"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = brand_id

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@product_id"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = product_id

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_get_test_master_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Public Function BrandTestLinkingInsertUpdate(ByVal link_id As Int64, ByVal brand_id As Int64, ByVal test_id As Int64, ByVal test_seq As Int64, ByVal active_yn As String, ByVal product_id As String, ByVal userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(6) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@link_id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = link_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@brand_id"
            sqlParams(1).DbType = DbType.Int64
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = brand_id

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@test_id"
            sqlParams(2).DbType = DbType.Int64
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = test_id

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@test_seq"
            sqlParams(3).DbType = DbType.Int64
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = test_seq

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active_yn"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = active_yn

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@user_id"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = userid

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@Product_id"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = product_id

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_brand_test_linking_insert_update]"

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
    Public Function GetBrandTestLinkingList(ByVal brand_id As Int64, ByVal userId As String, ByVal product_id As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brand_id"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = brand_id


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@product_id"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = product_id

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_brand_test_linking_get_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Test Result Upload"
    Function GetVendorBrand(ByVal vendorCode As String, ByVal userId As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_code"
        sqlParams(0).DbType = DbType.AnsiString
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_get_vendor_brand_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Function GetVendorBrandProduct(ByVal vendorCode As String, ByVal brandId As String, ByVal userId As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.AnsiString
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@brandid"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Val(brandId)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@user_id"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = userId

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_productlist_brand_vendor_wise]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function GetProductWiseSku(ByVal vendorCode As String, ByVal product As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorcode"
        sqlParams(0).DbType = DbType.AnsiString
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@productcode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = product



        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_productwiseSku]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function GetQcFormData(ByVal vendorCode As String, ByVal brandId As Int64, ByVal userId As String, ByVal productCode As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.AnsiString
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@brandid"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = brandId

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@user_id"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = userId

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@productcode"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = productCode

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_template_download_vr2]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Function UploadQcFormData(ByVal vendorCode As String, ByVal brandId As Int64, ByVal tbl As DataTable, ByVal userId As String, ByVal file_path As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.AnsiString
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@brandid"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = brandId

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@tbl"
        sqlParams(2).SqlDbType = SqlDbType.Structured
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = tbl

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@user_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = userId

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@file_path"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(String.IsNullOrEmpty(file_path), DBNull.Value, file_path)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_result_validation_v2]", Data.CommandType.StoredProcedure, sqlParams)
        'DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_result_validation_v2_test]", Data.CommandType.StoredProcedure, sqlParams)

        Return DS
    End Function
    Public Function QCFormDataSubmit(ByVal vendor_id As String, ByVal brand_id As Int64, ByVal product_code As String, ByVal shade_code As String, ByVal batch_no As String, ByVal batch_date As SqlDateTime, ByVal tbl As DataTable, ByVal file_path As String, ByVal userid As String, ByVal result_id As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(9) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendor_id"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = vendor_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@brand_id"
            sqlParams(1).DbType = DbType.Int64
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = brand_id

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@product_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = product_code

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@shade_code"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = shade_code

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@batch_no"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = batch_no

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@batch_date"
            sqlParams(5).SqlDbType = SqlDbType.DateTime
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = batch_date

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tbl"
            sqlParams(6).SqlDbType = SqlDbType.Structured
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = tbl

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@file_path"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = file_path

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@user_id"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = userid

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@result_id"
            sqlParams(9).DbType = DbType.Int64
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = Val(result_id)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_test_result_insert_update]"

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

    Public Function FGQualityDataSubmit(ByVal quarter_id As String,
                                        ByVal vendor_id As String,
                                        ByVal brand_id As Int64,
                                        ByVal userid As String,
                                        ByVal tbl As DataTable,
                                        ByVal sqlConn As SqlConnection,
                                        ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(5) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@quarter"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = quarter_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor_id"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = vendor_id

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@brand_id"
            sqlParams(2).DbType = DbType.Int64
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = brand_id

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@user_id"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = userid

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@test_res"
            sqlParams(4).SqlDbType = SqlDbType.Structured
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = tbl

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@out_prm"
            sqlParams(5).DbType = DbType.Int32
            sqlParams(5).Direction = Data.ParameterDirection.Output

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "dbo.vrs_deviations_fg_quality_insert_update"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            numRowsAffected = Convert.ToInt32(sqlParams(5).Value)
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function

    Public Function GetTestResultHdrById(ByVal id As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(0) As SqlParameter


        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@resultid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(id), DBNull.Value, id)


        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_result_hdr_details]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
    Public Function GetBrandTestDtlsList(ByVal brand_id As Int64, ByVal userId As String, ByVal result_id As Int64, ByVal product_code As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brand_id"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = brand_id

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@result_id"
        sqlParams(2).DbType = DbType.Int64
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Val(result_id)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@product_code"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = product_code

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_brand_test_dtls_get]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function


    Function CheckExportMsg(ByVal vendorCode As String, ByVal brandId As String, ByVal productcode As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.AnsiString
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@brandid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = brandId

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@productid"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = productcode

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_result_upload_msg_check]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#End Region


#Region "Vendor Stock List"
#Region "Bind Grid View"
    Function GetVendorStockList(ByVal vendorid As String, ByVal AsOndt As String, ByVal AsOndtto As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(vendorid Is "", DBNull.Value, vendorid)
        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@asondatefrom"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(AsOndt Is "", DBNull.Value, AsOndt)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@asondateto"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(AsOndtto Is "", DBNull.Value, AsOndtto)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_vendor_stock_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#End Region
#End Region


#Region "Vendor Stock Entry"
#Region "Bind Grid View"
    Function GetVendorStockEntryList(ByVal vendorid As String, ByVal AsOndt As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorid
        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@asondate"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(AsOndt Is "", DBNull.Value, AsOndt)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_vendor_sku_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#End Region
#Region "Insert Vendor Stock"
    Function InsertVendorStock(ByRef Vendorstock As VendorStockEntryEntity, ByVal tbl As DataTable) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        sqlConn = DBFactory.GetHelper.OpenConnection
        sqlTrans = sqlConn.BeginTransaction


        Dim sqlParams(3) As SqlParameter
        Try



            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendorid"
            sqlParams(0).SqlDbType = SqlDbType.VarChar
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Vendorstock.vendor_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@asondate"
            sqlParams(1).SqlDbType = SqlDbType.VarChar
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Vendorstock.date

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@userid"
            sqlParams(2).SqlDbType = SqlDbType.VarChar
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Vendorstock.CreatedUser

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@tbl"
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = tbl

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_vendor_sku_stock_insert_update]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            sqlTrans.Commit()
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Throw ex
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
        Return numRowsAffected
    End Function
#End Region
#End Region

#Region "Get Stock Report"
    Function GetStockReport(ByVal vendorid As String, ByVal AsOndt As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@asondate"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(AsOndt Is "", DBNull.Value, AsOndt)
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_vendor_stock_export]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#End Region

#Region "Get Stock Report Batch Wise"
    Function GetStockReportBatchWise(ByVal vendorid As String, ByVal AsOndtfrom As String, ByVal AsOndtto As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(vendorid Is "", DBNull.Value, vendorid)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@asondatefrom"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(AsOndtfrom Is "", DBNull.Value, AsOndtfrom)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@asondateto"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(AsOndtto Is "", DBNull.Value, AsOndtto)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_vendor_stock_export_datewise]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#End Region


#Region "Test Result List Page"
    Public Function GetTestResultList(ByVal vendorid As String, ByVal brandid As String, ByVal product As String, ByVal fromdate As String, ByVal todate As String, ByVal userId As String, ByVal batchno As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(6) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorid), DBNull.Value, vendorid)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@brandid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(brandid), DBNull.Value, brandid)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@product"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(String.IsNullOrEmpty(product), DBNull.Value, product)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@fromdate"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(String.IsNullOrEmpty(fromdate), DBNull.Value, fromdate)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@todate"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(String.IsNullOrEmpty(todate), DBNull.Value, todate)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@user_id"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = userId

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@batch_no"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(String.IsNullOrEmpty(batchno), DBNull.Value, batchno)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_result_upload_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function


    Public Function GetTestResultList_export(ByVal vendorid As String, ByVal brandid As String, ByVal product As String, ByVal fromdate As String, ByVal todate As String, ByVal userId As String, ByVal batchno As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(6) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorid), DBNull.Value, vendorid)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@brandid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(brandid), DBNull.Value, brandid)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@product"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(String.IsNullOrEmpty(product), DBNull.Value, product)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@fromdate"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(String.IsNullOrEmpty(fromdate), DBNull.Value, fromdate)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@todate"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(String.IsNullOrEmpty(todate), DBNull.Value, todate)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@user_id"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = userId

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@batch_no"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(String.IsNullOrEmpty(batchno), DBNull.Value, batchno)
        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_result_upload_list_export_vr1]", Data.CommandType.StoredProcedure, sqlParams)

        Return PrjectList

    End Function

#End Region

#Region "Test Result Approval Page"
    Public Function GetTestResultApprovalList(ByVal id As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(0) As SqlParameter


        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@resultid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(id), DBNull.Value, id)


        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_test_result_details_for_approval]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function

    ' "Approval"

    Function TestResultApprovalandReject(ByVal hdrid As String, ByVal remarks As String, ByVal status As String, ByVal userId As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        sqlConn = DBFactory.GetHelper.OpenConnection
        sqlTrans = sqlConn.BeginTransaction


        Dim sqlParams(3) As SqlParameter
        Try



            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@resultid"
            sqlParams(0).SqlDbType = SqlDbType.VarChar
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = hdrid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@remarks"
            sqlParams(1).SqlDbType = SqlDbType.VarChar
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(String.IsNullOrEmpty(remarks), DBNull.Value, remarks)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@status"
            sqlParams(2).SqlDbType = SqlDbType.VarChar
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = status

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@userid"
            sqlParams(3).SqlDbType = SqlDbType.VarChar
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = userId

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_test_result_details_approve_reject]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            sqlTrans.Commit()
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Throw ex
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
        Return numRowsAffected
    End Function

    Public Function TestResultEntryBulkInsert(ByVal vendorCode As String, ByVal brandId As Int64, ByVal tbl As DataTable, ByVal userId As String, ByVal file_path As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(4) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendorid"
            sqlParams(0).DbType = DbType.AnsiString
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = vendorCode

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@brandid"
            sqlParams(1).DbType = DbType.Int64
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = brandId

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@tbl"
            sqlParams(2).SqlDbType = SqlDbType.Structured
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = tbl

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@user_id"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = userId

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@file_path"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = IIf(String.IsNullOrEmpty(file_path), DBNull.Value, file_path)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[tc_test_result_bulk_insert_v1]"

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

    Function GetBrandWiseProduct(ByVal brandid As Int64) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brandid"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = brandid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[get_brandwise_productlist]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
End Class
