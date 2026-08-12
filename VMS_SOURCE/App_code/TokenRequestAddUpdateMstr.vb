Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess

Public Class TokenRequestAddUpdateMstr

#Region "Get Factory Applicable Vendor List "

    Public Function GetFactoryApplicableVendorList(ByVal FactoryCode As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@FactoryCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = FactoryCode

        ds = DBFactory.GetHelper().ExecuteDataSet("[TokenRequestAdd_getFactoryApplVendor]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region

#Region "Get Applicable Product List "

    Public Function GetApplicableProductList(ByVal FactoryCode As String, ByVal VendorCode As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@FactoryCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = FactoryCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@VendorCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = VendorCode

        ds = DBFactory.GetHelper().ExecuteDataSet("[TokenRequestAdd_getApplProduct]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region

#Region "Get Applicable PACK SIZE List "

    Public Function GetApplicablePackSizetList(ByVal FactoryCode As String, ByVal VendorCode As String, ByVal Product As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@FactoryCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = FactoryCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@VendorCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = VendorCode

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@Product"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Product

        ds = DBFactory.GetHelper().ExecuteDataSet("[TokenRequestAdd_getApplPackSize]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region

    Public Function InsertTokenSession(ByVal UserId As String, ByVal Active As String, ByVal Factory As String, ByVal Vendor As String, ByVal ReqMonth As String, ByVal ReqYear As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As New Integer

        Dim sqlParams(6) As SqlParameter
        Try

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@UserId"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = UserId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Active

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@SessionId"
            sqlParams(2).DbType = DbType.Int64
            sqlParams(2).Direction = Data.ParameterDirection.Output

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@Factory"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Factory

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@Vendor"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Vendor

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@ReqMonth"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = ReqMonth

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@ReqYear"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = ReqYear

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[TokenRequestAdd_insertSession]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return sqlParams(2).Value
    End Function

    Public Function InsertTokenDetails(ByVal Entity As TokenGenerationEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As New Integer

        Dim sqlParams(13) As SqlParameter
        Try

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@tm_session_id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Entity.tgrefsrlno

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@tm_factory_code"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Entity.factory_code

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@tm_vendor_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Entity.Vendor_code

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@tm_type"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Entity.tgtype

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@tm_product"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Entity.tgproduct

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@tm_pack"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Entity.tgpack

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tm_denomination"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = Entity.tgdenomination

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@tm_qty"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Entity.tgquantity

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@UserId"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = Entity.createduser

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@tm_srl"
            sqlParams(9).DbType = DbType.Int64
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = Entity.tgsrlno

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@tm_token_month"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = Entity.tgmonth

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@tm_token_year"
            sqlParams(11).DbType = DbType.String
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = Entity.tgyear

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@tm_requisition_month"
            sqlParams(12).DbType = DbType.String
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = Entity.Requisition_month

            sqlParams(13) = New SqlParameter()
            sqlParams(13).ParameterName = "@tm_requisition_year"
            sqlParams(13).DbType = DbType.String
            sqlParams(13).Direction = Data.ParameterDirection.Input
            sqlParams(13).Value = Entity.Requisition_year

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "TokenRequestAdd_insertTokenDetails"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected
    End Function

    Public Function DeleteTokenDetails(ByVal SessionId As Int64, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As New Integer

        Dim sqlParams(0) As SqlParameter
        Try

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@SessionId"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = SessionId


            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "TokenRequestAdd_deleteTokenDetails"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected
    End Function

#Region "Get Token Details "
    Public Function GetTokenDetails(ByVal SessionId As Int64) As DataSet
        Dim ds As DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@SessionId"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = SessionId

        ds = DBFactory.GetHelper().ExecuteDataSet("[TokenRequestAdd_getTokenDetails]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region


    Function SubmitTokenData(ByVal SessionId As Integer, ByVal xml_hdr As String, ByVal xml_dtls As String) As Integer
        Dim noOfRowEffected As Integer
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@SessionId"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = SessionId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@xml_hdr"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = xml_hdr

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@xml_dtl"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = xml_dtls

        noOfRowEffected = DBFactory.GetHelper().ExecuteNonQuery("[TOKEN_GENERATION_BERGER_DB].[dbo].[TokenRequisition_insertTokenData]", Data.CommandType.StoredProcedure, sqlParams)
        Return noOfRowEffected
    End Function

    '=====================================Code For List Page===================================

#Region "Get Token Requisition Session Details "
    Public Function GetTokenRequisitionSessionDetails(ByVal Factory As String, ByVal Vendor As String, ByVal Status As String) As DataSet
        Dim ds As DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Factory"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Factory.Equals(String.Empty), DBNull.Value, Factory)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(Vendor.Equals(String.Empty), DBNull.Value, Vendor)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@Status"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(Status.Equals(String.Empty), DBNull.Value, Status)

        ds = DBFactory.GetHelper().ExecuteDataSet("[TokenRequestList_get]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region


    Public Function GenerateTokenBarcode(ByVal sessionId As Int64, ByVal userId As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As New Integer

        Dim sqlParams(1) As SqlParameter
        Try

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@sessionId"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = sessionId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@userId"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = userId


            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandTimeout = 0
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Token_Barcode_Generation_V1"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected
    End Function



End Class
