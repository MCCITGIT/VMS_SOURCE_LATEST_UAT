Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class CreateLoadMasterClass
#Region "Get Screen Details"

    Function GetSCreenDetails() As DataSet

        Dim DetailsDS As System.Data.DataSet

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Load_Master_Get_Screen_Details]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region

    '#Region "Check Stock Master"

    '    Function CheckStockMaster() As DataSet

    '        Dim DetailsDS As System.Data.DataSet

    '        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("Load_Master_Check_Stock_Master", Data.CommandType.StoredProcedure)

    '        Return DetailsDS
    '    End Function
    '#End Region
#Region "Check Stock Master and Estimate Data"

    Function CheckStockAndEstimate() As DataSet

        Dim DetailsDS As System.Data.DataSet

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Load_Master_Check_Stock_And_Estimate]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region
    '#Region "Check Estimation Data"

    '    Function CheckEstimationData() As DataSet

    '        Dim DetailsDS As System.Data.DataSet

    '        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("Load_Master_Check_Estimation_Data", Data.CommandType.StoredProcedure)

    '        Return DetailsDS
    '    End Function
    '#End Region

#Region "Crerate Load Master"
    Public Function CreateLoadMaster(ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction, ByVal userid As String) As Integer

        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@userid"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = userid



            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlconn
            sqlCmd.Transaction = sqltrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            'sqlCmd.CommandText = "[Load_Master_Create_Cursor_Vr2]"
            sqlCmd.CommandText = "[Load_Master_Create_Cursor]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception

            Throw ex
        End Try

        Return numRowsAffected
    End Function

#End Region

#Region "Get Not Found Details "
    Public Function GetNotFoundCount(ByVal userid As String) As DataSet
        Dim DetailsDS As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid



        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Load_Master_Get_Not_FoundDetails]", Data.CommandType.StoredProcedure, sqlParams)
        Return DetailsDS
    End Function
#End Region

#Region "Get Unmapped Vendor"
    Public Function GetUnmappedVendor(ByVal userid As String) As DataSet
        Dim DetailsDS As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid



        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Load_Master_Get_Unmapped_Vendor]", Data.CommandType.StoredProcedure, sqlParams)
        Return DetailsDS
    End Function
#End Region


#Region "Insert Mapped Vendor SKU"
    Public Function Insert_Mapped_Vendor_SKU(ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction, ByVal Year As String, ByVal Month As String, ByVal DepotCode As String, ByVal VendorCode As String, ByVal Remarks As String, ByVal SKU As String, ByVal userid As String) As Integer

        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@userid"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = userid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Year

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@Month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Month

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@DepotCode"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = DepotCode

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@VendorCode"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = VendorCode

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@Remarks"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = IIf(Remarks <> String.Empty, Remarks, DBNull.Value)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@SKU"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = SKU

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlconn
            sqlCmd.Transaction = sqltrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Load_Master_Get_Unmapped_Vendor_Insert]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception

            Throw ex
        End Try

        Return numRowsAffected
    End Function

#End Region

    'Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source
#Region "Get Load Master Data For Mail"
    Public Function GetLoadMasterDataForMail() As DataSet
        Dim ds As DataSet
        Try
            ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Load_Master_Data_For_Mail]", Data.CommandType.StoredProcedure)
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function

    Public Function GetLoadMasterMailIds() As DataSet
        Return DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Load_Master_Data_Recipient_MailId]", Data.CommandType.StoredProcedure)
    End Function
#End Region

End Class
