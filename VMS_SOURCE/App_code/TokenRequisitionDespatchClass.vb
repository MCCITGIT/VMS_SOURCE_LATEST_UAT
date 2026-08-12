Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class TokenRequisitionDespatchClass
#Region "Get Vendor Unit "
    Public Function GetUnitName(ByVal active As String, ByVal token_vendor As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@trh_token_vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = token_vendor

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("PendingDespatches_GetUnitNameForVendor", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Vendor Unit "
    Public Function GetVendorRequisition(ByVal active As String, ByVal token_vendor As String, ByVal unit As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@trh_token_vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = token_vendor

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@trh_unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = unit

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("GetVendorRequisitionForDespatches", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Product List For Assignment"
    Public Function GetProductList(ByVal requisitionId As Integer, ByVal unit As String, ByVal tokenVendor As String, ByVal despatchId As Integer, ByVal productId As String, ByVal packsize As String) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = requisitionId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = unit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@trh_token_vendor"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = tokenVendor

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@despatchId"
        sqlParams(3).DbType = DbType.Int32
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = despatchId

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@productId"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(productId.Equals(""), DBNull.Value, productId)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@packsize"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(packsize.Equals(""), DBNull.Value, packsize)

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[GetToken_requisition_dtlsForVendorDespatch]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region

#Region "Get Product List For Assignment"
    Public Function GetProductName(ByVal requisitionId As Integer, ByVal unit As String, ByVal tokenVendor As String, ByVal despatchId As Integer) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = requisitionId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = unit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@trh_token_vendor"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = tokenVendor

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@despatchId"
        sqlParams(3).DbType = DbType.Int32
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = despatchId

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[GetToken_requisition_dtlsForVendorDespatchForProduct]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region

#Region "Get Pack Size"
    Public Function GetProductPackSize(ByVal requisitionId As Integer, ByVal unit As String, ByVal tokenVendor As String, ByVal despatchId As Integer, ByVal productId As String) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = requisitionId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = unit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@trh_token_vendor"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = tokenVendor

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@despatchId"
        sqlParams(3).DbType = DbType.Int32
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = despatchId

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@productId"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = productId

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[GetToken_requisition_dtlsForVendorDespatchForPackSize]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region

#Region "Token_Despatch_Insert_Update"
    Public Function TokenDespatchInsertUpdate(ByVal despatch_id As Integer, ByVal tdh_requisition_id As Integer, ByVal tdh_unit_code As String, ByVal tdh_token_vendor As String, ByVal tdh_transporter As String, ByVal tdh_truck_no As String, ByVal tdh_vendor_challan_no As String, ByVal tdh_vendor_challan_date As SqlDateTime, ByVal tdh_road_permit As String, ByVal userid As String, ByVal active As String, ByVal status As String, ByVal tbl As DataTable, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(12) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@tdh_despatch_id"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = despatch_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@tdh_requisition_id"
            sqlParams(1).DbType = DbType.Int32
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = tdh_requisition_id

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@tdh_unit_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = tdh_unit_code

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@tdh_token_vendor "
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = tdh_token_vendor

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@tdh_transporter"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = tdh_transporter

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@tdh_truck_no "
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = tdh_truck_no

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tdh_vendor_challan_no"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = tdh_vendor_challan_no

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@tdh_vendor_challan_date"
            sqlParams(7).SqlDbType = SqlDbType.DateTime
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = tdh_vendor_challan_date

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@tdh_road_permit "
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = tdh_road_permit

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@active"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = active

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@userId"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = userid

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@tbl"
            sqlParams(11).SqlDbType = SqlDbType.Structured
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = tbl

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@status"
            sqlParams(12).DbType = DbType.String
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = status

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Token_Despatch_Insert_Update"
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

#Region "Get Despatch List For Assignment"
    Public Function GetDespatchList(ByVal token_vendor As String, ByVal requisitionId As Integer, ByVal unit As String, ByVal despatchId As Integer) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@token_vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = token_vendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@requisition_id"
        sqlParams(1).DbType = DbType.Int32
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(requisitionId = 0, DBNull.Value, requisitionId)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(unit.Equals(""), DBNull.Value, unit)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@desPatchId"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(despatchId = 0, DBNull.Value, despatchId)

        ProductList = DBFactory.GetHelper().ExecuteDataSet("Token_vendor_Despatch_list_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region
End Class
