Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class UnitTokenReceivedClass
#Region "Get Vendor Unit "
    Public Function GetTokenVendorList(ByVal unit_code As String, ByVal active As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit_code"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit_code.Equals(String.Empty), DBNull.Value, unit_code)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@active"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = active

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Token_Received_List_ForUnit", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Despatch List "
    Public Function GetDespatchList(ByVal requisitionId As Integer, ByVal despatchId As Integer, ByVal receiveId As Integer) As DataSet
        Dim ds As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = requisitionId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@despatchId"
        sqlParams(1).DbType = DbType.Int32
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = despatchId

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@receiveId"
        sqlParams(2).DbType = DbType.Int32
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = receiveId

        ds = DBFactory.GetHelper().ExecuteDataSet("Get_Despatches_From_Requisition", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function
#End Region
#Region "Get Despatch List "
    Public Function GetDespatchId(ByVal requisitionId As Integer) As DataSet
        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@requisitionId"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = requisitionId

        ds = DBFactory.GetHelper().ExecuteDataSet("Despatch_Id_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function
#End Region
#Region "Token reeive Insert Update"
    Public Function TokenReceiveInsertUpdate(ByVal despatchId As Integer, ByVal requisitionId As Integer, ByVal unit As String, ByVal userid As String, ByVal active As String, ByVal tokenVendor As String, ByVal tbl As DataTable, ByVal status As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(7) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@treh_despatch_id"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = despatchId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@treh_requisition_id"
            sqlParams(1).DbType = DbType.Int32
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = requisitionId

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@treh_unit_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = unit

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@treh_token_vendor"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = tokenVendor

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = active

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@userId"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = userid

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@tbl"
            sqlParams(6).SqlDbType = SqlDbType.Structured
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = tbl

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@status"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = status

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Token_Receieve_Add_Update"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex

        End Try

        Return numRowsAffected

    End Function
#End Region
#Region "Get Vendor Unit "
    Public Function GetRequisitionList(ByVal unit_code As String, ByVal tokenVendor As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit_code.Equals(String.Empty), DBNull.Value, unit_code)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@token_vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = tokenVendor

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Requisition_List_For_Receive_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Despatch List for receive"
    Public Function GetDespatchListForReceive(ByVal unit_code As String, ByVal vendor As String, ByVal requisitionId As Integer, ByVal despatchId As Integer, ByVal status As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = unit_code

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(vendor.Equals(String.Empty), DBNull.Value, vendor)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@requisition_id"
        sqlParams(2).DbType = DbType.Int32
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(requisitionId = 0, DBNull.Value, requisitionId)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@despatch"
        sqlParams(3).DbType = DbType.Int32
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(despatchId = 0, DBNull.Value, despatchId)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@status"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(status.Equals(String.Empty), DBNull.Value, status)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Token_Received_List_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
End Class
