Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class LoadDropAddUpdateClass

#Region "Get product list"

    Function GetProductList(ByVal unitCode As String, ByVal Region As String, ByVal DepotCode As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unitCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = unitCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Region"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(Region.Equals(String.Empty), DBNull.Value, Region)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@DepotCode"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(DepotCode.Equals(String.Empty), DBNull.Value, DepotCode)

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[LoadDropAdd_getProduct]", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region


#Region "Get SKU Details"
    Function GetSKUDetails(ByVal unit As String, ByVal productCode As String, ByVal ProcessYear As String, ByVal ProcessMonth As String, ByVal Region As String, ByVal DepotCode As String) As DataSet
        Dim sqlParams(5) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@productCode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(productCode.Equals(String.Empty), DBNull.Value, productCode)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@ProcessYear"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = ProcessYear

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ProcessMonth"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = ProcessMonth

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@Region"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = IIf(Region.Equals(String.Empty), DBNull.Value, Region)

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@DepotCode"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(DepotCode.Equals(String.Empty), DBNull.Value, DepotCode)

            Return DBFactory.GetHelper().ExecuteDataSet("[LoadDropAdd_getSKUList]", Data.CommandType.StoredProcedure, sqlParams)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "Get SKU Details Drop DepotWise"
    Function GetSKUDetailsDropDepotWise(ByVal DepotCode As String, ByVal Vendor As String) As DataSet
        Dim sqlParams(1) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@DepotCode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = DepotCode

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Vendor



            Return DBFactory.GetHelper().ExecuteDataSet("[LoadDropDetial_getDepotWise]", Data.CommandType.StoredProcedure, sqlParams)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Insert Load Drop Header"
    Function InsertLoadDroppHeader(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByVal Unit As String, ByVal Year As String, ByVal Month As String, ByVal Created_user As String) As Integer

        Dim numRowsAffected As Integer
        'Dim challanNo As Integer
        'challanNo = -1
        Try
            Dim sqlParams(4) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@ldh_vend_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@ldh_hdr_req_id"
            sqlParams(1).DbType = DbType.Int64
            sqlParams(1).Direction = Data.ParameterDirection.Output

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@ldh_year"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Year

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ldh_month"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Month

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@created_user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Created_user

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[LoadDropAdd_insertHdr]"
            sqlCmd.Parameters.AddRange(sqlParams)
            sqlCmd.ExecuteNonQuery()

            numRowsAffected = sqlParams(1).Value

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region

#Region "Insert despatch Detail"
    Function InsertDespatchDetail(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByVal hdrReqId As Int32, ByVal DepotCode As String, ByVal SkuCode As String, ByVal ReqQty As Int32, ByVal Created_User As String, ByVal ProcessYear As String, ByVal ProcessMonth As String, ByVal UnitCode As String) As Integer

        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(7) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@hdrReqId"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = hdrReqId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@DepotCode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = DepotCode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@SkuCode"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = SkuCode

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ReqQty"
            sqlParams(3).DbType = DbType.Int64
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = ReqQty

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@Created_User"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Created_User

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@ProcessYear"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = ProcessYear

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@ProcessMonth"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = ProcessMonth

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@UnitCode"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = UnitCode

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[LoadDropAdd_insertDtl]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()



        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region

#Region "Get Drop Load list"

    Function GetDropLoadList(ByVal Depot As String, ByVal SKUCode As String, ByVal Vendor As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Depot.Equals(String.Empty), DBNull.Value, Depot)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@SKUCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(SKUCode.Equals(String.Empty), DBNull.Value, SKUCode)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@Vendor"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(Vendor.Equals(String.Empty), DBNull.Value, Vendor)


        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[LoadDrop_getList]", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region

End Class
