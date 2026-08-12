Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class StockUpdateClass
#Region "Get Screen Details"

    Function GetSCreenDetails() As DataSet

        Dim DetailsDS As System.Data.DataSet

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Stock_Update_Get_Screen_Details]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region
#Region "Update Stock Master"
    Function UpdateStock(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As Integer


        Dim sqlCmd As New SqlCommand()
        sqlCmd.Connection = sqlConn
        sqlCmd.Transaction = sqlTrans
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[Stock_Update_Old_Record_Update]"
        sqlCmd.CommandTimeout = 0
        numRowsAffected = sqlCmd.ExecuteNonQuery()
        Return numRowsAffected

    End Function
#End Region
#Region "Inserting Stock master"
    Public Function InsertStockMaster(ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction, ByVal A As String()) As Integer

        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@stk_ason_date"
            sqlParams(0).DbType = DbType.Date
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = CType((FormatDate(A(4).Trim)), Date)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@stk_depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = A(1).Trim

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@stk_sku_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = A(2).Trim

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@stk_stock_nop"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = A(3).Trim

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@created_user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = A(5).Trim

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_date"
            sqlParams(5).DbType = DbType.DateTime
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = FormatDate(A(6).Trim)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@active"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = A(7).Trim

           
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlconn
            sqlCmd.Transaction = sqltrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Stock_Update_Insert_Stock]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception

            Throw ex
        End Try

        Return numRowsAffected
    End Function

#End Region
#Region "Update Stock as on date "
    Public Function DeleteStockMaster(ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction, ByVal A As String()) As Integer

        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@stk_ason_date"
            sqlParams(0).DbType = DbType.Date
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = CType((FormatDate(A(4).Trim)), Date)



            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlconn
            sqlCmd.Transaction = sqltrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Stock_Update_As_On_Date_Update]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception

            Throw ex
        End Try

        Return numRowsAffected
    End Function

#End Region
#Region "Update Load Master"
    Function UpdateLoadMaster(ByVal usrid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As Integer
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = usrid

        Dim sqlCmd As New SqlCommand()
        sqlCmd.Connection = sqlConn
        sqlCmd.Transaction = sqlTrans
        sqlCmd.CommandTimeout = 0
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[Stock_Update_Update_Load_Mstr]"
        sqlCmd.Parameters.AddRange(sqlParams)
        numRowsAffected = sqlCmd.ExecuteNonQuery()
        Return numRowsAffected

    End Function
#End Region
#Region "Get Screen Details"
    Function GetStockDetails() As DataSet
        Dim DetailsDS As System.Data.DataSet
        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Stock_Update_Stock_Details]", Data.CommandType.StoredProcedure)
        Return DetailsDS
    End Function
#End Region
#Region "Get Screen Details Err"
    Function GetStockDetailsErr() As DataSet
        Dim DetailsDS As System.Data.DataSet
        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Stock_Update_Stock_DetailsErr]", Data.CommandType.StoredProcedure)
        Return DetailsDS
    End Function
#End Region
#Region "Update Load Master using Cursor"
    Function UpdateLoadMasterCursor(ByVal usrid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As Integer
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = usrid


        Dim sqlCmd As New SqlCommand()
        sqlCmd.Connection = sqlConn
        sqlCmd.Transaction = sqlTrans
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[Stock_Update_Update_Load_Mstr_Cursor]"
        sqlCmd.Parameters.AddRange(sqlParams)
        sqlCmd.CommandTimeout = 0
        numRowsAffected = sqlCmd.ExecuteNonQuery()
        Return numRowsAffected

    End Function
#End Region
#Region "Update Load Master Calculating Auto Indent"
    Function CalculateAutoIndent(ByVal usrid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer

        Dim numRowsAffected As Integer
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = usrid


        Dim sqlCmd As New SqlCommand()
        sqlCmd.Connection = sqlConn
        sqlCmd.Transaction = sqlTrans
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[Stock_Update_Calculate_AutoIndent]"
        sqlCmd.Parameters.AddRange(sqlParams)
        sqlCmd.CommandTimeout = 0
        numRowsAffected = sqlCmd.ExecuteNonQuery()
        Return numRowsAffected

    End Function
#End Region
#Region "Get Error Count"

    Function GetErrorNo() As DataSet

        Dim DetailsDS As System.Data.DataSet

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Stock_Update_Get_Error_Status]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region
#Region "Update Stock as on date "
    Public Function GetStockCountAsOn(ByVal A As String()) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@stk_ason_date"
        sqlParams(0).DbType = DbType.Date
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = CType((FormatDate(A(4).Trim)), Date)

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Stock_Update_Get_Count_AsOnDate]", Data.CommandType.StoredProcedure, sqlParams)



        Return DetailsDS
    End Function

#End Region
#Region "Get Row Count "
    Public Function GetRowCount() As DataSet

        Dim DetailsDS As System.Data.DataSet


        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Stock_Update_Get_No_Of_Row_Inserted]", Data.CommandType.StoredProcedure)



        Return DetailsDS
    End Function

#End Region
#Region "Date Format"
    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime
        If Not (stringdate = String.Empty) Then
            Dim ddate As String() = stringdate.Split("/")
            Dim arrlist As New ArrayList
            Dim index As Integer = 0

            While index <= ddate.Length - 1
                arrlist.Add(ddate(index))
                System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
            End While
            Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(0))
            Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
            Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(2))

            Dim dt As DateTime = New DateTime(yyyy, mm, dd)
            dt = FormatDateTime(dt, DateFormat.LongDate)

            Return dt
        End If
    End Function
#End Region
End Class
