Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class VendorInvoice_ReleaseClass
#Region "Vendor invoice release"
    Public Function GetVendorInvoice_ReleaseList(ByVal vendor_id As String, ByVal status As String, ByVal fromdate As SqlDateTime, ByVal todate As SqlDateTime, ByVal depot As String, ByVal type As String) As DataSet
        Dim ds As New DataSet
        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(vendor_id <> String.Empty, vendor_id, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@status"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(status <> String.Empty, status, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fromDate"
        sqlParams(2).SqlDbType = SqlDbType.DateTime
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(fromdate = SqlDateTime.MinValue, DBNull.Value, fromdate)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@toDate"
        sqlParams(3).SqlDbType = SqlDbType.DateTime
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(todate = SqlDateTime.MinValue, DBNull.Value, todate)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@depot"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@type"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(type <> String.Empty, type, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VendorInvoiceAc_ReleaseDtls]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function
    Public Function GetVendorInvoice_ReleaseList_vr1(ByVal vendor_id As String, ByVal status As String, ByVal fromdate As SqlDateTime, ByVal todate As SqlDateTime, ByVal depot As String, ByVal type As String) As DataSet
        Dim ds As New DataSet
        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_id"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(vendor_id <> String.Empty, vendor_id, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@status"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(status <> String.Empty, status, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fromDate"
        sqlParams(2).SqlDbType = SqlDbType.DateTime
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(fromdate = SqlDateTime.MinValue, DBNull.Value, fromdate)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@toDate"
        sqlParams(3).SqlDbType = SqlDbType.DateTime
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(todate = SqlDateTime.MinValue, DBNull.Value, todate)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@depot"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@type"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(type <> String.Empty, type, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[VendorInvoiceAc_ReleaseDtls_vr1]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds

    End Function
#End Region

End Class
