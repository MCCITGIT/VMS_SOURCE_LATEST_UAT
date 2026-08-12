Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class EstimationUploadClass
#Region "Get Screen Details"

    Function GetSCreenDetails() As DataSet

        Dim DetailsDS As System.Data.DataSet

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Estimation_Data_Get_Screen_Details]", Data.CommandType.StoredProcedure)

        Return DetailsDS
    End Function
#End Region
#Region "Get Estimation as on date "
    Public Function GetEstCountAsOn(ByVal A As String()) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@yyyy"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = A(2)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@mm"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = A(3)

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Estimation_Data_Get_Count_AsOnDate]", Data.CommandType.StoredProcedure, sqlParams)



        Return DetailsDS
    End Function

#End Region
#Region "Delete estimate Data as on date "
    Public Function DeleteEstimationAsOn(ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction, ByVal A As String()) As Integer

        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@yyyy"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = A(2)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@mm"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = A(3)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlconn
            sqlCmd.Transaction = sqltrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Estimation_Data_As_On_Date_Update]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception

            Throw ex
        End Try

        Return numRowsAffected
    End Function

#End Region
#Region "Inserting Estimate Data"
    Public Function InsertEstimateData(ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction, ByVal A As String()) As Integer

        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(8) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@est_depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = A(1).Trim

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@est_yyyy"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = A(2).Trim

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@est_mm"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = A(3).Trim

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@est_sku_code"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = A(4).Trim

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@est_average_nop"
            sqlParams(4).DbType = DbType.Int64
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = A(5).Trim

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@est_estimate_nop"
            sqlParams(5).DbType = DbType.Int64
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = A(6).Trim

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@created_user"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = A(7).Trim

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@created_date"
            sqlParams(7).DbType = DbType.DateTime
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = FormatDate(A(8).Trim)

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@active"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = A(9).Trim


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlconn
            sqlCmd.Transaction = sqltrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Estimation_Data_Insertion]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception

            Throw ex
        End Try

        Return numRowsAffected
    End Function

#End Region
#Region "Get Row Count "
    Public Function GetRowCount(ByVal year As String, ByVal month As String) As DataSet
        Dim DetailsDS As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@est_yyyy"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = year

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@est_mm"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = month

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Estimation_Data_Get_No_Of_Row_Inserted]", Data.CommandType.StoredProcedure, sqlParams)
        Return DetailsDS
    End Function
#End Region
#Region "Get Not Found Count "
    Public Function GetNotFoundCount(ByVal year As String, ByVal month As String) As DataSet
        Dim DetailsDS As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@yyyy"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = year

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@mm"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = month

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Estimation_Data_Get_Not_Found_Count]", Data.CommandType.StoredProcedure, sqlParams)
        Return DetailsDS
    End Function
#End Region
#Region "Get Estimation Details"
    Public Function GetEstimationDetails(ByVal active As String, ByVal ProcessYr As String, ByVal ProcessMonth As String) As DataSet
        Dim objEst As System.Data.DataSet
        Dim sqlparams(2) As SqlParameter

        sqlparams(0) = New SqlParameter()
        sqlparams(0).ParameterName = "@active"
        sqlparams(0).DbType = DbType.String
        sqlparams(0).Direction = ParameterDirection.Input
        sqlparams(0).Value = active

        sqlparams(1) = New SqlParameter()
        sqlparams(1).ParameterName = "@ProcessYr"
        sqlparams(1).DbType = DbType.String
        sqlparams(1).Direction = ParameterDirection.Input
        sqlparams(1).Value = ProcessYr

        sqlparams(2) = New SqlParameter()
        sqlparams(2).ParameterName = "@ProcessMonth"
        sqlparams(2).DbType = DbType.String
        sqlparams(2).Direction = ParameterDirection.Input
        sqlparams(2).Value = ProcessMonth


        objEst = DBFactory.GetHelper().ExecuteDataSet("EstimationDataUpload_GetDetails", CommandType.StoredProcedure, sqlparams)
        Return objEst
    End Function
#End Region
#Region "Get Estimation Error Details"
    Public Function GetEstimationDetailsError(ByVal active As String, ByVal ProcessYr As String, ByVal ProcessMonth As String) As DataSet
        Dim objEst As System.Data.DataSet
        Dim sqlparams(2) As SqlParameter

        sqlparams(0) = New SqlParameter()
        sqlparams(0).ParameterName = "@active"
        sqlparams(0).DbType = DbType.String
        sqlparams(0).Direction = ParameterDirection.Input
        sqlparams(0).Value = active

        sqlparams(1) = New SqlParameter()
        sqlparams(1).ParameterName = "@ProcessYr"
        sqlparams(1).DbType = DbType.String
        sqlparams(1).Direction = ParameterDirection.Input
        sqlparams(1).Value = ProcessYr

        sqlparams(2) = New SqlParameter()
        sqlparams(2).ParameterName = "@ProcessMonth"
        sqlparams(2).DbType = DbType.String
        sqlparams(2).Direction = ParameterDirection.Input
        sqlparams(2).Value = ProcessMonth


        objEst = DBFactory.GetHelper().ExecuteDataSet("EstimationDataUpload_GetDetailsError", CommandType.StoredProcedure, sqlparams)
        Return objEst
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
