'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/WeekMaster.vb
'Created Date	: 25-November-2007
'Created By	    : Arun
'Version	    : R02.00.00
'Description	: Code behind file for WeekMaster Class

'Modified By       Modified On       Version         Reason

'*************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Namespace VMS.Web

    Public Class WeekMaster

#Region "Get WeekMaster List"

        Function GetWeekMasterList(ByVal Company As String) As DataSet

            Dim WeekMasterDetails As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company


            WeekMasterDetails = DBFactory.GetHelper().ExecuteDataSet("Week_Master_List", Data.CommandType.StoredProcedure, sqlParams)

            Return WeekMasterDetails
        End Function
#End Region

#Region "Get WeekMaster List for selected Financial Year"

        Function GetWeekMasterFinYearList(ByVal Company As String, ByVal FinYear As Integer) As DataSet

            Dim WeekMasterDetails As System.Data.DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@finYear"
            sqlParams(1).DbType = DbType.Int32
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = FinYear

            WeekMasterDetails = DBFactory.GetHelper().ExecuteDataSet("Week_Master_FinYear_List", Data.CommandType.StoredProcedure, sqlParams)

            Return WeekMasterDetails

        End Function
#End Region

#Region "Get FinYearMaster List"

        Function GetFinYearMasterList(ByVal Company As String) As DataSet

            Dim FinYearMasterDetails As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company


            FinYearMasterDetails = DBFactory.GetHelper().ExecuteDataSet("Fin_Year_Master_List", Data.CommandType.StoredProcedure, sqlParams)

            Return FinYearMasterDetails
        End Function
#End Region

#Region "FinYear Already Exists"
        Function GetUserGroupExist(ByVal Company As String, ByVal FinYear As String) As DataSet
            Dim FinYr As DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@finyear"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = FinYear


            FinYr = DBFactory.GetHelper().ExecuteDataSet("FinYr_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

            Return FinYr
        End Function
#End Region

#Region "Insert FinYear"
        Function InsertFinYear(ByVal company As String, ByVal FinYear As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal NoWeek As String, ByVal Status As String, ByVal UserID As String) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(6) As SqlParameter
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = company

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@finyear"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = FinYear

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@startdate"
                sqlParams(2).DbType = DbType.Date
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = StartDate

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@enddate"
                sqlParams(3).DbType = DbType.Date
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = EndDate

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@noweek"
                sqlParams(4).DbType = DbType.String
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = NoWeek

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@status"
                sqlParams(5).DbType = DbType.String
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = Status

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@userid"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = UserID


                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "Fin_Year_Master_Insert"

                sqlCmd.Parameters.AddRange(sqlParams)
                numRowsAffected = sqlCmd.ExecuteNonQuery()

                'SqlTrans is set to commit to save the transaction
                sqlTrans.Commit()

            Catch ex As Exception
                If Not (sqlTrans Is Nothing) Then
                    'SqlTrans is set to Rollback to go back to the beginning of the transaction
                    sqlTrans.Rollback()
                End If
                Throw ex
            Finally
                If Not (sqlConn Is Nothing) Then
                    'sqlConn is set to close state after completing the transaction
                    sqlConn.Close()
                End If
            End Try

            Return numRowsAffected

        End Function
#End Region

#Region "Update FinYear"
        Function UpdateFinYear(ByVal company As String, ByVal FinYear As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal NoWeek As String, ByVal Status As String, ByVal UserID As String) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(6) As SqlParameter
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = company

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@finyear"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = FinYear

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@startdate"
                sqlParams(2).DbType = DbType.Date
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = StartDate

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@enddate"
                sqlParams(3).DbType = DbType.Date
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = EndDate

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@noweek"
                sqlParams(4).DbType = DbType.String
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = NoWeek

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@status"
                sqlParams(5).DbType = DbType.String
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = Status

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@userid"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = UserID


                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "Fin_Year_Master_Update"

                sqlCmd.Parameters.AddRange(sqlParams)
                numRowsAffected = sqlCmd.ExecuteNonQuery()

                'SqlTrans is set to commit to save the transaction
                sqlTrans.Commit()

            Catch ex As Exception
                If Not (sqlTrans Is Nothing) Then
                    'SqlTrans is set to Rollback to go back to the beginning of the transaction
                    sqlTrans.Rollback()
                End If
                Throw ex
            Finally
                If Not (sqlConn Is Nothing) Then
                    'sqlConn is set to close state after completing the transaction
                    sqlConn.Close()
                End If
            End Try

            Return numRowsAffected

        End Function
#End Region

#Region "Get FinYearMaster"

        Function GetFinYearMaster(ByVal Company As String, ByVal FinYear As String) As DataSet

            Dim UserGroupDetails As System.Data.DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@finyear"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = FinYear

            UserGroupDetails = DBFactory.GetHelper().ExecuteDataSet("Get_Fin_Year_Master", Data.CommandType.StoredProcedure, sqlParams)

            Return UserGroupDetails
        End Function
#End Region

#Region "Insert a New Record in Week Master"

        Function InsertWeekMaster(ByRef WeekMstrEntity As VMS.Web.WeekMasterEntity) As Integer

            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(7) As SqlParameter

            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@fin_company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = WeekMstrEntity.PropertyCompany

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@fin_year"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = WeekMstrEntity.PropertyFinYear

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@fin_week"
                sqlParams(2).DbType = DbType.Int32
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = WeekMstrEntity.PropertyFinWeek

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@fin_week_start_date"
                sqlParams(3).DbType = DbType.DateTime
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = WeekMstrEntity.PropertyWeekStartDate

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@fin_week_end_date"
                sqlParams(4).DbType = DbType.DateTime
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = WeekMstrEntity.PropertyWeekEndDate

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@fin_month_no"
                sqlParams(5).DbType = DbType.Int32
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = WeekMstrEntity.PropertyMonthNo

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@created_user"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = WeekMstrEntity.PropertyCreatedUser

                sqlParams(7) = New SqlParameter()
                sqlParams(7).ParameterName = "@active"
                sqlParams(7).DbType = DbType.String
                sqlParams(7).Direction = Data.ParameterDirection.Input
                sqlParams(7).Value = WeekMstrEntity.PropertyActiveStatus

                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "Week_Master_Insert"

                sqlCmd.Parameters.AddRange(sqlParams)
                numRowsAffected = sqlCmd.ExecuteNonQuery()

                'SqlTrans is set to commit to save the transaction
                sqlTrans.Commit()

            Catch ex As Exception
                If Not (sqlTrans Is Nothing) Then
                    'SqlTrans is set to Rollback to go back to the beginning of the transaction
                    sqlTrans.Rollback()
                End If
                Throw ex
            Finally
                If Not (sqlConn Is Nothing) Then
                    'sqlConn is set to close state after completing the transaction
                    sqlConn.Close()
                End If
            End Try

            Return numRowsAffected

        End Function

#End Region

#Region "Update a Record in Week Master"

        Function UpdateWeekMaster(ByRef WeekMstrEntity As VMS.Web.WeekMasterEntity) As Integer

            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(8) As SqlParameter

            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@fin_company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = WeekMstrEntity.PropertyCompany

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@fin_year"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = WeekMstrEntity.PropertyFinYear

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@fin_week"
                sqlParams(2).DbType = DbType.Int32
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = WeekMstrEntity.PropertyFinWeek

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@fin_week_start_date"
                sqlParams(3).DbType = DbType.DateTime
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = WeekMstrEntity.PropertyWeekStartDate

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@fin_week_end_date"
                sqlParams(4).DbType = DbType.DateTime
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = WeekMstrEntity.PropertyWeekEndDate

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@fin_month_no"
                sqlParams(5).DbType = DbType.Int32
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = WeekMstrEntity.PropertyMonthNo

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@modified_user"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = WeekMstrEntity.PropertyModifiedUser

                sqlParams(7) = New SqlParameter()
                sqlParams(7).ParameterName = "@active"
                sqlParams(7).DbType = DbType.String
                sqlParams(7).Direction = Data.ParameterDirection.Input
                sqlParams(7).Value = WeekMstrEntity.PropertyActiveStatus

                sqlParams(8) = New SqlParameter()
                sqlParams(8).ParameterName = "@hdnweekno"
                sqlParams(8).DbType = DbType.Int32
                sqlParams(8).Direction = Data.ParameterDirection.Input
                sqlParams(8).Value = WeekMstrEntity.PropertyHdnFinWeek

                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "Week_Master_Update"

                sqlCmd.Parameters.AddRange(sqlParams)
                numRowsAffected = sqlCmd.ExecuteNonQuery()

                'SqlTrans is set to commit to save the transaction
                sqlTrans.Commit()

            Catch ex As Exception
                If Not (sqlTrans Is Nothing) Then
                    'SqlTrans is set to Rollback to go back to the beginning of the transaction
                    sqlTrans.Rollback()
                End If
                Throw ex
            Finally
                If Not (sqlConn Is Nothing) Then
                    'sqlConn is set to close state after completing the transaction
                    sqlConn.Close()
                End If
            End Try

            Return numRowsAffected

        End Function

#End Region

#Region "AJAX YearWeek Exists"

        Function GetYearWeekExist(ByVal Company As String, ByVal year As String, ByVal week As String, ByVal hweek As String) As DataSet

            Dim YearWeekSet As System.Data.DataSet

            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = year

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@week"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = week

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@hweek"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = hweek

            YearWeekSet = DBFactory.GetHelper().ExecuteDataSet("Week_Mstr_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

            Return YearWeekSet

        End Function
#End Region

    End Class
End Namespace

