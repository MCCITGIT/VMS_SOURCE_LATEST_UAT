Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes


Public Class VendorDispatchClass

    Public Function GetVendorDispatchList(ByVal ddrh_vendor_id As String, ByVal month As String, ByVal year As String, ByVal status As String) As DataSet
        'Dim DepotDS As New DataSet
        'Dim sqlParams(1) As SqlParameter

        'sqlParams(0) = New SqlParameter()
        'sqlParams(0).ParameterName = "@Region"
        'sqlParams(0).DbType = DbType.String
        'sqlParams(0).Direction = Data.ParameterDirection.Input
        'sqlParams(0).Value = IIf(Region <> String.Empty, Region, DBNull.Value)


        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@Active"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = Active

        'DepotDS = DBFactory.GetHelper().ExecuteDataSet("[Estimation_Data_Get_Depot]", Data.CommandType.StoredProcedure, sqlParams)
        Dim DS As New DataSet
        'Return DepotDS
        Try




            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@ddrh_vendor_id"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = System.Data.ParameterDirection.Input
            sqlParams(0).Value = ddrh_vendor_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@month"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = System.Data.ParameterDirection.Input
            sqlParams(1).Value = month

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@year"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = System.Data.ParameterDirection.Input
            sqlParams(2).Value = year

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@status"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = System.Data.ParameterDirection.Input
            sqlParams(3).Value = status

            DS = DBFactory.GetHelper().ExecuteDataSet("[VendorDispatch_getVendorDispatchList]", Data.CommandType.StoredProcedure, sqlParams)
            Return DS
        Catch ex As Exception
            Return DS
        End Try
    End Function



    Public Function GetVendorDispatchAssignDetailsList(ByVal ddrd_hdr_req_id As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams As SqlParameter() = New SqlParameter(0) {}
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@ddrd_hdr_req_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = System.Data.ParameterDirection.Input
        sqlParams(0).Value = ddrd_hdr_req_id
        DS = DBFactory.GetHelper().ExecuteDataSet("VendorDispatch_getDispatchAssignedDetails_vr1", System.Data.CommandType.StoredProcedure, sqlParams)
        'DS = DBFactory.GetHelper().ExecuteDataSet("VendorDispatch_getDispatchAssignedDetails", System.Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Public Function UpdateVendorDispatchRequestStatus(ByVal ddah_req_hdr_id As String, ByVal ddah_transporter_name As String, ByVal ddah_vehicle_no As String, ByVal ddah_vendor_invoice_no As String, ByVal ddah_vendor_invoice_date As String, ByVal ddah_waybill_no As String, ByVal created_user As String, ByVal Doc_Path As String, ByVal FileName As String, ByVal OrgFileName As String, ByVal ValidUpTo As String, ByVal EWayDate As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(11) As SqlParameter
            '  sqlConn = CType(DBFactory.GetHelper().OpenConnection(), SqlConnection)

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@ddah_req_hdr_id"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = System.Data.ParameterDirection.Input
            sqlParams(0).Value = ddah_req_hdr_id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@ddah_transporter_name"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = System.Data.ParameterDirection.Input
            sqlParams(1).Value = ddah_transporter_name

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@ddah_vehicle_no"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = System.Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(ddah_vehicle_no <> String.Empty, ddah_vehicle_no, DBNull.Value)

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@ddah_vendor_invoice_no"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = System.Data.ParameterDirection.Input
            sqlParams(3).Value = ddah_vendor_invoice_no

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@ddah_vendor_invoice_date"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = System.Data.ParameterDirection.Input
            sqlParams(4).Value = ddah_vendor_invoice_date

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@ddah_waybill_no"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = System.Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(ddah_waybill_no <> String.Empty, ddah_waybill_no, DBNull.Value)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@created_user"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = System.Data.ParameterDirection.Input
            sqlParams(6).Value = created_user

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@Doc_Path"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = System.Data.ParameterDirection.Input
            sqlParams(7).Value = Doc_Path

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@FileName"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = System.Data.ParameterDirection.Input
            sqlParams(8).Value = FileName

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@OrgFileName"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = System.Data.ParameterDirection.Input
            sqlParams(9).Value = OrgFileName

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@ValidUpTo"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = System.Data.ParameterDirection.Input
            sqlParams(10).Value = IIf(ValidUpTo <> String.Empty, ValidUpTo, DBNull.Value)

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@EWayDate"
            sqlParams(11).DbType = DbType.String
            sqlParams(11).Direction = System.Data.ParameterDirection.Input
            sqlParams(11).Value = IIf(EWayDate <> String.Empty, EWayDate, DBNull.Value)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            'sqlCmd.CommandText = "Vendor_Despatch_Request_Status_Update"
            sqlCmd.CommandText = "Vendor_Despatch_Request_Status_Update_vr1"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function

    'Public Function UpdateVendorDispatchRequestStatus(ByVal ddah_req_hdr_id As String, ByVal ddah_transporter_name As String, ByVal ddah_vehicle_no As String, ByVal ddah_vendor_invoice_no As String, ByVal ddah_vendor_invoice_date As String, ByVal ddah_waybill_no As String, ByVal created_user As String) As Integer
    '    Dim sqlConn As SqlConnection = Nothing
    '    Dim sqlTrans As SqlTransaction = Nothing
    '    Dim numRowsAffected As Integer
    '    Dim sqlParams As SqlParameter() = New SqlParameter(6) {}

    '    Try
    '        sqlConn = CType(DBFactory.GetHelper().OpenConnection(), SqlConnection)
    '        sqlTrans = sqlConn.BeginTransaction()
    '        sqlParams(0) = New SqlParameter()
    '        sqlParams(0).ParameterName = "@ddah_req_hdr_id"
    '        sqlParams(0).DbType = DbType.String
    '        sqlParams(0).Direction = System.Data.ParameterDirection.Input
    '        sqlParams(0).Value = ddah_req_hdr_id
    '        sqlParams(1) = New SqlParameter()
    '        sqlParams(1).ParameterName = "@ddah_transporter_name"
    '        sqlParams(1).DbType = DbType.String
    '        sqlParams(1).Direction = System.Data.ParameterDirection.Input
    '        sqlParams(1).Value = ddah_transporter_name
    '        sqlParams(2) = New SqlParameter()
    '        sqlParams(2).ParameterName = "@ddah_vehicle_no"
    '        sqlParams(2).DbType = DbType.String
    '        sqlParams(2).Direction = System.Data.ParameterDirection.Input
    '        sqlParams(2).Value = ddah_vehicle_no
    '        sqlParams(3) = New SqlParameter()
    '        sqlParams(3).ParameterName = "@ddah_vendor_invoice_no"
    '        sqlParams(3).DbType = DbType.String
    '        sqlParams(3).Direction = System.Data.ParameterDirection.Input
    '        sqlParams(3).Value = ddah_vendor_invoice_no
    '        sqlParams(4) = New SqlParameter()
    '        sqlParams(4).ParameterName = "@ddah_vendor_invoice_date"
    '        sqlParams(4).DbType = DbType.String
    '        sqlParams(4).Direction = System.Data.ParameterDirection.Input
    '        sqlParams(4).Value = ddah_vendor_invoice_date
    '        sqlParams(5) = New SqlParameter()
    '        sqlParams(5).ParameterName = "@ddah_waybill_no"
    '        sqlParams(5).DbType = DbType.String
    '        sqlParams(5).Direction = System.Data.ParameterDirection.Input
    '        sqlParams(5).Value = ddah_waybill_no
    '        sqlParams(6) = New SqlParameter()
    '        sqlParams(6).ParameterName = "@created_user"
    '        sqlParams(6).DbType = DbType.String
    '        sqlParams(6).Direction = System.Data.ParameterDirection.Input
    '        sqlParams(6).Value = created_user
    '        Dim sqlCmd As SqlCommand = New SqlCommand()
    '        sqlCmd.Connection = sqlConn
    '        sqlCmd.Transaction = sqlTrans
    '        sqlCmd.CommandType = CommandType.StoredProcedure
    '        sqlCmd.CommandText = "Vendor_Despatch_Request_Status_Update"
    '        sqlCmd.Parameters.AddRange(sqlParams)
    '        numRowsAffected = sqlCmd.ExecuteNonQuery()
    '        sqlTrans.Commit()
    '    Catch ex As Exception
    '        If Not (sqlTrans Is Nothing) Then sqlTrans.Rollback()
    '        Throw ex
    '    Finally
    '        If Not (sqlConn Is Nothing) Then sqlConn.Close()
    '    End Try

    '    Return numRowsAffected
    'End Function

    Public Function getVirtualOrgEmail(ByVal Req_Id As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams As SqlParameter() = New SqlParameter(0) {}
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Req_Id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = System.Data.ParameterDirection.Input
        sqlParams(0).Value = Req_Id
        DS = DBFactory.GetHelper().ExecuteDataSet("getVirtualOrgEmail", System.Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

End Class
