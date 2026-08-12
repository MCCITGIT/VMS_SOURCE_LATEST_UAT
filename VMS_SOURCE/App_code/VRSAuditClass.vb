Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class VRSAuditClass
#Region "Get vendor details"
    Public Function GetVendorDetails(ByVal userid As String) As DataSet

        Dim ds As DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid
        ds = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[tc_vendor_list]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function
#End Region

#Region "Get quarter details"
    Public Function GetQuarterDetails(ByVal user As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@user_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = user

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_Quarter_List]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function
#End Region

    '#Region "Get audit type details"
    '    Public Function GetTypeDetails() As DataSet

    '        Dim ds As DataSet

    '        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Type]", Data.CommandType.StoredProcedure)

    '        Return ds

    '    End Function
    '#End Region

#Region "Get audit type details"
    'Public Function GetParameterDetails(ByVal type As String) As DataSet

    '    Dim ds As DataSet

    '    Dim sqlParams(0) As SqlParameter

    '    sqlParams(0) = New SqlParameter()
    '    sqlParams(0).ParameterName = "@type"
    '    sqlParams(0).DbType = DbType.String
    '    sqlParams(0).Direction = Data.ParameterDirection.Input
    '    sqlParams(0).Value = type

    '    ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Audit_Parameters]", Data.CommandType.StoredProcedure, sqlParams)
    '    Return ds

    'End Function
#End Region

    '#Region "Get total score"
    '    Public Function GetTotalScore(ByVal param As String) As DataSet

    '        Dim ds As DataSet

    '        Dim sqlParams(0) As SqlParameter

    '        sqlParams(0) = New SqlParameter()
    '        sqlParams(0).ParameterName = "@param"
    '        sqlParams(0).DbType = DbType.String
    '        sqlParams(0).Direction = Data.ParameterDirection.Input
    '        sqlParams(0).Value = param

    '        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Total_Score]", Data.CommandType.StoredProcedure, sqlParams)
    '        Return ds

    '    End Function
    '#End Region

#Region "Get audit details"
    Public Function GetAuditDetails(ByVal vendor As String,
                                    ByVal quarter As Integer) As DataSet

        Dim ds As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = quarter

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Audit_Details]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function
#End Region

    Public Function SubmitAuditDetails(ByVal quarter As Integer,
                                     ByVal vendor As String,
                                     ByVal user As String,
                                     ByVal check As String,
                                     ByVal dt As DataTable,
                                     ByVal sqlConn As SqlConnection,
                                     ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim output As Integer = 0
        Dim result As Integer = 0
        Dim sqlParams(4) As SqlParameter
        Try
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@quarter"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = quarter

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor_id"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = vendor

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@user_id"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = user

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@AuditDetails"
            sqlParams(3).SqlDbType = SqlDbType.Structured
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = dt

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@check"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = check


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[Insert_Audit_Details]"

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

    Public Function GetVendorAuditAcknowledge(ByVal userid As String, ByVal quarter As String) As DataSet

        Dim ds As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Convert.ToString(quarter)

        ds = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[tc_vendor_list_audit_acknowledge]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#Region "Get audit details Report"
    Public Function GetAuditDetailsReport(ByVal vendor As String,
                                    ByVal quarter As String) As DataSet

        Dim ds As DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@quarter"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(quarter <> String.Empty, quarter, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[vrs_get_audit_details_report]", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function
#End Region



End Class
