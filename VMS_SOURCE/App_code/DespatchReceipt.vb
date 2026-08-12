Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class DespatchReceipt

#Region "Get Despatch Receipt List"

    Function Despatch_Receipt_Get_List(ByVal unit As String, ByVal depot As String, ByVal process_year As String, ByVal process_month As String, ByVal status As String, ByVal challan_no As Integer) As DataSet

        Dim ds As System.Data.DataSet
        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@region"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = IIf(region.Equals(String.Empty), DBNull.Value, region)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(depot.Equals(String.Empty), DBNull.Value, depot)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@process_year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(process_year.Equals(String.Empty), DBNull.Value, process_year)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@process_month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(process_month.Equals(String.Empty), DBNull.Value, process_month)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@status"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = status

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@challan_no"
        sqlParams(5).DbType = DbType.Int64
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(challan_no.Equals(Integer.MinValue), DBNull.Value, challan_no)

        ds = DBFactory.GetHelper().ExecuteDataSet("Despatch_Receipt_Get_List", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region


    Function Despatch_Receipt_Insert(ByVal despatch_receive_record As DespatchReceiveDetailsEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As Integer
        Dim sqlParams(6) As SqlParameter

        Try

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = despatch_receive_record.VUnit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@process_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = despatch_receive_record.ProcessYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@challan_no"
            sqlParams(2).DbType = DbType.Int64
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = despatch_receive_record.ChallanNo

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@recv_total_ltr"
            sqlParams(3).DbType = DbType.Decimal
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = despatch_receive_record.ReceiveTotalLtr

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@recv_total_kg"
            sqlParams(4).DbType = DbType.Decimal
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = despatch_receive_record.ReceiveTotalKg

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@receive_date"
            sqlParams(5).DbType = DbType.DateTime
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = despatch_receive_record.ReceiveDate

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@created_user"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = despatch_receive_record.CreatedUser

            'sqlParams(7) = New SqlParameter()
            'sqlParams(7).ParameterName = "@additional_yn"
            'sqlParams(7).DbType = DbType.String
            'sqlParams(7).Direction = Data.ParameterDirection.Input
            'sqlParams(7).Value = despatch_receive_record.AdditionalYN


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Despatch_Receipt_Insert"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Throw ex
        End Try

        Return numRowsAffected

    End Function

    Function DespatchReceiptAdditionalEntry_Insert(ByVal despatch_receive_record As DespatchReceiveDetailsEntity) As Integer

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Dim numRowsAffected As Integer
        Dim sqlParams(14) As SqlParameter

        Try

            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@desphr_desp_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = despatch_receive_record.VUnit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@desphr_challan_fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = despatch_receive_record.ProcessYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@desphr_challan_no"
            sqlParams(2).DbType = DbType.Int64
            sqlParams(2).Direction = Data.ParameterDirection.Output

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@desphr_desp_depot"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = despatch_receive_record.VDepot

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@desphr_process_month"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = despatch_receive_record.ProcessMonth

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@desphr_challan_date"
            sqlParams(5).DbType = DbType.DateTime
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = despatch_receive_record.ChallanDate

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@desphr_recv_total_ltr"
            sqlParams(6).DbType = DbType.Decimal
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = despatch_receive_record.ReceiveTotalLtr

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@desphr_recv_total_kg"
            sqlParams(7).DbType = DbType.Decimal
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = despatch_receive_record.ReceiveTotalKg

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@desphr_transporter_name"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = despatch_receive_record.TransporterName

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@desphr_road_permit_no"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = despatch_receive_record.PermitNo

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@desphr_truck_no"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = despatch_receive_record.TruckNo

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@desphr_excise_gp_no"
            sqlParams(11).DbType = DbType.String
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = despatch_receive_record.GPNo

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@desphr_excise_gp_dt"
            sqlParams(12).DbType = DbType.DateTime
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = despatch_receive_record.GPDate

            sqlParams(13) = New SqlParameter()
            sqlParams(13).ParameterName = "@desphr_receive_date"
            sqlParams(13).DbType = DbType.DateTime
            sqlParams(13).Direction = Data.ParameterDirection.Input
            sqlParams(13).Value = despatch_receive_record.ReceiveDate

            sqlParams(14) = New SqlParameter()
            sqlParams(14).ParameterName = "@created_user"
            sqlParams(14).DbType = DbType.String
            sqlParams(14).Direction = Data.ParameterDirection.Input
            sqlParams(14).Value = despatch_receive_record.CreatedUser


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "DespatchReceiptAdditionalEntry_Insert"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            'SqlTrans is set to commit to save the transaction
            sqlTrans.Commit()

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            'Throw ex
        Finally
            If Not (sqlConn Is Nothing) Then
                'sqlConn is set to close state after completing the transaction
                sqlConn.Close()
            End If
        End Try

        Return CType(sqlParams(2).Value, Integer)

    End Function




End Class
