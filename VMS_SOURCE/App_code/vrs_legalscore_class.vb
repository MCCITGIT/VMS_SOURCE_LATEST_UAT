Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Imports NPOI.SS.Formula.Eval
Imports CrystalDecisions.[Shared]
Imports System.IdentityModel.Protocols.WSTrust


Public Class vrs_legalscore_class
#Region "Fin Year"
    Function GetFinYear(ByVal userid As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@user_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid
        DS = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[VRS_Get_Fin_Year]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
#End Region
    Function Get_QuarterList(ByVal userid As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@user_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_Quarter_List]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function Get_QuarterList_vr1(ByVal userid As String, ByVal finYear As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@user_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@selected_fin_year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = finYear

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_Quarter_List]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Function Get_GetLegal_Statutory_ParameterList() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetLegal_Statutory_ParameterList]", Data.CommandType.StoredProcedure)
        Return DS
    End Function
    Function GetVendor_ObligationList() As DataSet
        Dim DS As System.Data.DataSet
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VRS_GetVendor_Obligation]", Data.CommandType.StoredProcedure)
        Return DS
    End Function
    Public Function GetVendor_DataList() As DataSet
        Dim dsVendor As System.Data.DataSet
        dsVendor = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VendorList_Get]", System.Data.CommandType.StoredProcedure)
        Return dsVendor
    End Function
    Function Get_LegalScoreList(ByVal vendorcode As String, ByVal quartor As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorcode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorcode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quartor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quartor

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_bind_legal_scoredata]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Function Get_LegalScoreData(ByVal obligation As String, ByVal status As String) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@obligation"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = obligation

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@status"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = status

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Vrs_GetLegal_Score]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
    Public Function VRS_LegalScore_Insert(ByVal vendorid As String, ByVal quartor As String, ByVal totalScore As Decimal, ByVal tbl As DataTable,
                                                ByVal created_user As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(4) As SqlParameter

        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendorid"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = vendorid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@quartor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = quartor

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@totalScore"
            sqlParams(2).DbType = DbType.Decimal
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = totalScore

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@tbl"
            sqlParams(3).SqlDbType = SqlDbType.Structured
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = tbl

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@created_user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = created_user

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[Insert_LegalScore]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function
    Public Function VRS_LegalScore_Insert_vr1(ByVal vendorid As String, ByVal quartor As String, ByVal totalScore As Decimal,
                                              ByVal parameter_val As Int32, ByVal obligation As String, ByVal avilibility As String,
                                              ByVal max_score As Decimal, ByVal obtain_score As Decimal, ByVal validtill As SqlDateTime,
                                              ByVal issue_authority As String, ByVal created_user As String, ByVal file_path As String,
                                              ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(11) As SqlParameter

        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendorid"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = vendorid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@quartor"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = quartor

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@totalScore"
            sqlParams(2).DbType = DbType.Decimal
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = totalScore

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@parameter"
            sqlParams(3).SqlDbType = SqlDbType.Int
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = parameter_val

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@obligation"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = obligation

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@avilibility"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = avilibility

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@max_score"
            sqlParams(6).DbType = DbType.Decimal
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = max_score

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@obtain_score"
            sqlParams(7).DbType = DbType.Decimal
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = obtain_score

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@validtill"
            sqlParams(8).DbType = DbType.Date
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = validtill

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@issue_authority"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = issue_authority

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@created_user"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = created_user

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@file_path"
            sqlParams(11).DbType = DbType.String
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = file_path

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[Insert_LegalScore]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function

    Public Function VRS_LegalScore_Delete(ByVal headerid As Integer, ByVal dtlsid As Integer, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim sqlParams(1) As SqlParameter

        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@headerid"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = headerid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@dtlsid"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = dtlsid

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[Delete_LegalScore]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function

    Function Get_Legal_ScoreData_BYID(ByVal headerid As Integer, ByVal dtlsid As Integer) As DataSet

        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@headerid"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = headerid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@dtlsid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = dtlsid

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_LegalScoreDetails_ById]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Public Function VRS_LegalScore_Modify(ByVal headerid As Integer, ByVal created_user As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim sqlParams(1) As SqlParameter

        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@headerid"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = headerid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@created_user"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = created_user

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[Update_LegalScore]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function

    Public Function GetLegalScoreDetails(ByVal vendor As String,
                                         ByVal quarter As String) As DataSet
        Dim dsVendor As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@qrtr"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("[VMS].dbo.vrs_legal_score_dtls_vr1", System.Data.CommandType.StoredProcedure, sqlParams)
        Return dsVendor
    End Function

    Public Function SubmitAuditDetails(ByVal quartor As String,
                                        ByVal vendorid As String,
                                        ByVal created_user As String,
                                        ByVal check As String,
                                        ByVal tbl As DataTable,
                                        ByVal sqlConn As SqlConnection,
                                        ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(4) As SqlParameter

        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendor"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = vendorid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@quarter"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = quartor

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@user_id"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = created_user

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@check"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = check

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@LegalScoreDetails"
            sqlParams(4).SqlDbType = SqlDbType.Structured
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = tbl


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "dbo.vrs_insert_legal_score"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function

    Public Function GetLegalScoreApprRejDetails(ByVal vendor As String,
                                         ByVal quarter As String) As DataSet
        Dim dsVendor As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@qrtr"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[vrs_legal_score_appr_rej_dtls]", System.Data.CommandType.StoredProcedure, sqlParams)
        Return dsVendor
    End Function

    Public Function UpdateLegalScoreStatus(ByVal quartor As String,
                                        ByVal vendorid As String,
                                        ByVal approveastatus As String,
                                        ByVal user As String,
                                        ByVal dtapprovereject As DataTable,
                                        ByVal sqlConn As SqlConnection,
                                        ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Dim sqlParams(4) As SqlParameter

        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@vendor"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = vendorid

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@quarter"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = quartor

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@user_id"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = user

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@approvestatus"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = approveastatus

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@tbl"
            sqlParams(4).SqlDbType = SqlDbType.Structured
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = dtapprovereject

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[vrs_vendor_legal_score_approve_reject]"

            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If

        End Try

        Return numRowsAffected

    End Function

    Public Function GetTargetScore(ByVal obligation As String,
                                         ByVal availability As String) As DataSet
        Dim dsVendor As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@obligation"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = obligation

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@status"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = availability

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("dbo.getTargetScore", System.Data.CommandType.StoredProcedure, sqlParams)
        Return dsVendor
    End Function

    Function GetVendorListForApproval(ByVal status As String, ByVal quarter As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@status"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(status <> String.Empty, status, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(quarter <> String.Empty, quarter, DBNull.Value)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_approval_vendor_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Function GetLegal_score_DtlsReport(ByVal quarter As String, ByVal vendor As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@quarter"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(quarter <> String.Empty, quarter, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendorid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_leagal_details_report]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

End Class
