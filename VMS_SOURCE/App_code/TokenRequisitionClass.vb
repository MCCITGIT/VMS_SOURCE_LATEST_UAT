Imports BERGER_VALUE_TOKEN.MsSqlDataAccess
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web
Imports VMS.DataAccess

Public Class TokenRequisitionClass
    Public Shared Function DBNullValueorStringIfNotNull(ByVal value As String) As Object
        Dim o As Object

        If (value = String.Empty Or value Is Nothing) Then
            o = DBNull.Value
        Else
            o = value
        End If

        Return o
    End Function

    Public Shared Function DBNullValueIfZero(ByVal value As Integer) As Object
        Dim o As Object

        If (value = 0) Then
            o = DBNull.Value
        Else
            o = value
        End If

        Return o
    End Function
    Public Function GetFactory(ByVal user As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@user"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(user.Equals(String.Empty), DBNull.Value, user)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_receive_getFactory]", System.Data.CommandType.StoredProcedure, sqlParams)
        Catch ex As Exception
            Throw ex
        End Try

        Return DS
    End Function

    Public Function GetLovDetails(ByVal lov_type As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@lov_type"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(lov_type.Equals(String.Empty), DBNull.Value, lov_type)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_getLovDetails]", System.Data.CommandType.StoredProcedure, sqlParams)
            Return DS

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPackSize(ByVal Factory As String, ByVal Vendor As String, ByVal Product As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

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
            sqlParams(2).ParameterName = "@Product"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(Product.Equals(String.Empty), DBNull.Value, Product)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_getPack]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetProduct(ByVal Factory As String, ByVal Vendor As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

            Dim sqlParams(1) As SqlParameter

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

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_getProduct]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetProductForNewReq(ByVal Factory As String, ByVal Vendor As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

            Dim sqlParams(1) As SqlParameter

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

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_getProduct_newReq]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetVendor(ByVal Factory As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(Factory.Equals(String.Empty), DBNull.Value, Factory)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_getvendor]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetSite(ByVal Factory As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(Factory.Equals(String.Empty), DBNull.Value, Factory)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_getsite]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetList(ByVal factory As String, ByVal vendor As String, ByVal ts_session_month As String, ByVal ts_session_year As String, ByVal requisition_id As Int32, ByVal user_id As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

            Dim sqlParams(5) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@ts_session_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(ts_session_month.Equals(String.Empty), DBNull.Value, ts_session_month)

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ts_session_year"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = IIf(ts_session_year.Equals(String.Empty), DBNull.Value, ts_session_year)

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@requisition_id"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = IIf(requisition_id = 0, DBNull.Value, requisition_id)

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@user_id"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(user_id.Equals(String.Empty), DBNull.Value, user_id)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_List]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetList(ByVal factory As String, ByVal site As String, ByVal vendor As String, ByVal ts_session_month As String, ByVal ts_session_year As String, ByVal requisition_id As Int32, ByVal user_id As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try

            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)


            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@site"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(site.Equals(String.Empty), DBNull.Value, site)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@vendor"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ts_session_month"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = IIf(ts_session_month.Equals(String.Empty), DBNull.Value, ts_session_month)

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@ts_session_year"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = IIf(ts_session_year.Equals(String.Empty), DBNull.Value, ts_session_year)

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@requisition_id"
            sqlParams(5).DbType = DbType.Int32
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(requisition_id = 0, DBNull.Value, requisition_id)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@user_id"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = IIf(user_id.Equals(String.Empty), DBNull.Value, user_id)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_GetList]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function GetDetailsForEdit(ByVal ts_session_id As Int32) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@ts_session_id"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = ts_session_id

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_fetchDetailsForEditNew]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function

    Public Function Insert(ByVal factory As String, ByVal vendor As String, ByVal type As String, ByVal ts_session_month As String, ByVal ts_session_year As String, ByVal created_user As String, ByVal dt As DataTable, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Int32
        Dim returnResult As Int32 = 0
        Try

            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@type"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(type.Equals(String.Empty), DBNull.Value, type)

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ts_session_month"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = IIf(ts_session_month.Equals(String.Empty), DBNull.Value, ts_session_month)

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@ts_session_year"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = IIf(ts_session_year.Equals(String.Empty), DBNull.Value, ts_session_year)

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(created_user.Equals(String.Empty), DBNull.Value, created_user)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tbl1"
            sqlParams(6).SqlDbType = SqlDbType.Structured
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = dt

            Dim sqlCmd As SqlCommand = New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "value_token_requisition_Insert"
            'sqlCmd.CommandText = "value_token_requisition_Insert_DEMO"
            sqlCmd.Parameters.AddRange(sqlParams)
            returnResult = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try
        Return returnResult
    End Function

    Public Function Update(ByVal RequisitionId As Int32, ByVal factory As String, ByVal vendor As String, ByVal type As String, ByVal ts_session_month As String, ByVal ts_session_year As String, ByVal created_user As String, ByVal dt As DataTable, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Int32
        Dim returnResult As Int32 = 0
        Try
            Dim sqlParams(7) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@factory"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(factory.Equals(String.Empty), DBNull.Value, factory)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@type"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(type.Equals(String.Empty), DBNull.Value, type)

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ts_session_month"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = IIf(ts_session_month.Equals(String.Empty), DBNull.Value, ts_session_month)

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@ts_session_year"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = IIf(ts_session_year.Equals(String.Empty), DBNull.Value, ts_session_year)

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(created_user.Equals(String.Empty), DBNull.Value, created_user)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tbl1"
            sqlParams(6).SqlDbType = SqlDbType.Structured
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = dt

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@RequisitionId"
            sqlParams(7).DbType = DbType.Int32
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = RequisitionId

            Dim sqlCmd As SqlCommand = New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "value_token_requisition_update"
            'sqlCmd.CommandText = "value_token_requisition_update_demo"
            sqlCmd.Parameters.AddRange(sqlParams)
            returnResult = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
        Return returnResult
    End Function

    Public Function GetProductDenomination(ByVal ProductCode As String, ByVal PackSizeCode As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@ProductCode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(ProductCode.Equals(String.Empty), DBNull.Value, ProductCode)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@PackSizeCode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(PackSizeCode.Equals(String.Empty), DBNull.Value, PackSizeCode)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[value_token_requisition_getDenomination]", System.Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return DS
    End Function
End Class
