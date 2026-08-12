Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class DeviationsFGQualityClass

    Public Function GetBrandTestDtlsList(ByVal brand_id As Int64, ByVal quarter As String, ByVal vendor As String, ByVal user_id As String, ByVal product_code As String, ByVal sku_code As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brand_id"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = brand_id

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@vendor_id"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = vendor

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@user_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = user_id

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@product_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(product_code Is "", DBNull.Value, product_code)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@sku_code"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(sku_code Is "", DBNull.Value, sku_code)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_brand_test_dtls_get]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function

    Public Function GetFGQualityList(ByVal brand_id As String, ByVal quarter As String, ByVal vendor As String) As DataSet
        Dim PrjectList As DataSet
        Dim sqlParams(2) As SqlParameter



        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = quarter

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendorid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(vendor), DBNull.Value, vendor)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@barndid"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(String.IsNullOrEmpty(brand_id), DBNull.Value, brand_id)



        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_fgquality_List]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function

    Public Function DeviationFGQualityInsertUpdate(ByVal quarter_id As String,
                                        ByVal vendor_id As String,
                                        ByVal brand_id As Int64,
                                        ByVal userid As String,
                                        ByVal check As String,
                                        ByVal productcode As String,
                                        ByVal skucode As String,
                                        ByVal batchno As String,
                                        ByVal tbl As DataTable,
                                        ByVal sqlConn As SqlConnection,
                                        ByVal sqlTrans As SqlTransaction) As String()
        Dim numRowsAffected As Integer
        ' Dim outputparam() As String
        Dim outputparam As New List(Of String)
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(10) As SqlParameter
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

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@check"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = check

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@product_code"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = productcode

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@batch_code"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = batchno

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@sku_code"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = skucode

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@out_msg"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Output
            sqlParams(10).Size = -1

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "dbo.vrs_deviations_fg_quality_insert_update_VR1"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            numRowsAffected = Convert.ToInt32(sqlParams(5).Value)
            outputparam.Add(sqlParams(5).Value)
            outputparam.Add(sqlParams(10).Value)

        Catch ex As Exception
            Throw ex

        End Try

        Return outputparam.ToArray()

    End Function

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

    Function GetAcknowledgeVendor(ByVal userid As String, ByVal quarter As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_vendor_list_quality_acknowledge]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
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

    Function GetVendorBrand(ByVal vendorCode As String, ByVal userId As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_code"
        sqlParams(0).DbType = DbType.AnsiString
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(vendorCode), DBNull.Value, vendorCode)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[tc_get_vendor_brand_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Public Function DeviationApproveRejectInsert(
                                        ByVal approve_yn As String,
                                        ByVal approve_by As String,
                                        ByVal quarter_id As String,
                                        ByVal vendor_id As String,
                                        ByVal brand_id As Int64,
                                        ByVal product_code As String,
                                        ByVal remarks As String,
                                        ByVal tbl As DataTable,
                                        ByVal sqlConn As SqlConnection,
                                        ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(7) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@approve_yn"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = approve_yn

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@approve_by"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = approve_by

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@quarter"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = quarter_id

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@vendor_id"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = vendor_id

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@test_res"
            sqlParams(4).SqlDbType = SqlDbType.Structured
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = tbl

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@brand_id"
            sqlParams(5).DbType = DbType.Int64
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = brand_id

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@remarks"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = IIf(remarks <> String.Empty, remarks, DBNull.Value)

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@product_code"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = IIf(product_code <> String.Empty, product_code, DBNull.Value)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[insert_approve_reject_yn]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            'numRowsAffected = Convert.ToInt32(sqlParams(5).Value)
        Catch ex As Exception
            Throw ex

        End Try

        Return numRowsAffected

    End Function

    Function GetFgqualityDtlsReport(ByVal brandid As Int64, ByVal quarter As String, ByVal vendorCode As String, ByVal userId As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@brand_id"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(String.IsNullOrEmpty(brandid), DBNull.Value, brandid)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.AnsiString
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(String.IsNullOrEmpty(quarter), DBNull.Value, quarter)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@vendor_id"
        sqlParams(2).DbType = DbType.AnsiString
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(String.IsNullOrEmpty(vendorCode), DBNull.Value, vendorCode)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@user_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = userId

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_fgquality_dtls_report]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

End Class
