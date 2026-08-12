
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class TokenVendorRequisitionClass
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
#Region "Token Requisition Insert Update"
    Public Function TokenRequisitionInsertUpdate(ByVal requisitionId As Integer, ByVal desc As String, ByVal unit As String, ByVal site As String, ByVal userid As String, ByVal active As String, ByVal tokenVendor As String, ByVal tbl As DataTable, ByVal status As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(9) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@requisitionId"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = requisitionId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@desc"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = desc

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@unit"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = unit

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@userId"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = userid

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = active

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@tokenVendor"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = tokenVendor

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@site"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = site

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@tbl"
            sqlParams(7).SqlDbType = SqlDbType.Structured
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = tbl

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@status"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = status

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@output"
            sqlParams(9).DbType = DbType.Int32
            sqlParams(9).Direction = Data.ParameterDirection.Output


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Token_Requisition_Insert_Update"


            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            If (Integer.TryParse(sqlParams(9).Value, output)) Then
                result = Integer.Parse(sqlParams(9).Value)
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
#Region "Get Product List For Assignment"
    Public Function GetProductList(ByVal unit As String, ByVal product As String, ByVal status As String, ByVal vendor As String) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(3) As SqlParameter

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

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@vendor"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = vendor

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[Token_Requisition_Details_Get]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region

#Region "Get Requistion List "
    Public Function GetRequistionList(ByVal userId As String, ByVal userGroup As String, ByVal unit As String, ByVal token_vendor As String, ByVal trh_id As Integer, ByVal status As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@userGroup"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userGroup

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = unit

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@trh_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(trh_id.Equals(0), DBNull.Value, trh_id)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@trh_token_vendor"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(token_vendor.Equals(""), DBNull.Value, token_vendor)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@status"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(status.Equals(String.Empty), DBNull.Value, status)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Token_vendor_Requisition_list_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
#Region "Get Product List For Assignment by id"
    Public Function GetRequisitionItemsListByid(ByVal id As Integer) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = id

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[Token_Requisition_Details_Get_By_id]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region
#Region "Get Requisition "
    Public Function GetRequisitionForUnitByVendor(ByVal active As String, ByVal token_vendor As String, ByVal unit As String) As DataSet
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
        sqlParams(2).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("GetRequisitionForUnitByVendor", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
    Public Function GetRequisitionForUnitByVendor_Unit(ByVal active As String, ByVal token_vendor As String, ByVal unit As String) As DataSet
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
        sqlParams(2).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[GetRequisitionForUnitByVendor_unit]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
    Public Function GetRequisitionForUnitByVendorUnDespatched(ByVal active As String, ByVal token_vendor As String, ByVal unit As String) As DataSet
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
        sqlParams(2).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[GetRequisitionForUnitByVendor_UnDespatched]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
#Region "Token Requisition Reject"
    Public Function TokenRequisitionReject(ByVal requisitionId As Integer, ByVal status As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim sqlParams(1) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@requisitionId"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = requisitionId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@status"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = status

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Token_Requision_Reject"


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
#Region "Get Product List For Assignment by id"
    Public Function Get_Mail_Details(ByVal id As Integer) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = id

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[Token_Requisition_Details_Mail]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region

#Region "Get Token Status List "
    Public Function GetTokenStatusList(ByVal userId As String, ByVal userGroup As String, ByVal unit As String, ByVal token_vendor As String, ByVal trh_id As Integer, ByVal status As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@userGroup"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userGroup

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = unit

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@trh_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(trh_id.Equals(0), DBNull.Value, trh_id)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@trh_token_vendor"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(token_vendor.Equals(""), DBNull.Value, token_vendor)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@status"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(status.Equals(String.Empty), DBNull.Value, status)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("TokenRequisitionStatus_getList", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Token Requisition Status by id"
    Public Function GetRequisitionStatusDetailsByid(ByVal id As Integer) As DataSet
        Dim ProductList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = id

        ProductList = DBFactory.GetHelper().ExecuteDataSet("[TokenRequisitionStatus_getDetailsbyId]", Data.CommandType.StoredProcedure, sqlParams)
        Return ProductList

    End Function
#End Region

    Public Function GetUnitVendorFreightList(ByVal unit As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unitCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = unit

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("unit_vendor_freight_list", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function

    Public Function FreightInsertUpdate(ByVal unitCode As String, ByVal vendorCode As String, ByVal freighDtls As Decimal, ByVal userid As String, ByVal freightID As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(4) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@unitCode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unitCode

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendorCode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = vendorCode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@freightDtls"
            sqlParams(2).DbType = DbType.Decimal
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = freighDtls

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@freightID"
            sqlParams(3).DbType = DbType.Int64
            sqlParams(3).Direction = Data.ParameterDirection.Input
            If String.IsNullOrEmpty(freightID) Then
                sqlParams(3).Value = DBNull.Value
            Else
                sqlParams(3).Value = Convert.ToInt64(freightID)
            End If

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = userid

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "unit_vendor_freight_inser_update"


            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            If (Integer.TryParse(sqlParams(4).Value, output)) Then
                result = Integer.Parse(sqlParams(4).Value)
            End If
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function

    Public Function GetUnitVendorFreight_downloadList(ByVal unit As String, ByVal freightval As Int32) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unitCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit <> String.Empty, unit, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@freight_val"
        sqlParams(1).DbType = DbType.Int32
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = freightval

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[unit_vendor_freight_excellist_vr1]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function

    Public Function FreightExcel_InsertUpdate(ByVal unitCode As String, ByVal vendorCode As String, ByVal freighDtls As Decimal, ByVal userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(3) As SqlParameter

        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@unitCode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unitCode

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendorCode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = vendorCode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@freightDtls"
            sqlParams(2).DbType = DbType.Decimal
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = freighDtls

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@user"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = userid

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[unit_vendor_freight_excel_inser_update]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            'If (Integer.TryParse(sqlParams(3).Value, output)) Then
            '    result = Integer.Parse(sqlParams(4).Value)
            'End If
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function
    Public Function AccessableUserlist() As DataSet
        Dim PrjectList As DataSet
        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[dbo].[GetFreight_Dtls_AccessableUserlist]", Data.CommandType.StoredProcedure)
        Return PrjectList

    End Function
End Class
