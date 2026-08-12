Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class VendorSiteDepotWiseDumpClass
    Public Function GetVendorSiteDepotWiseDumpReport(ByVal VendorCode As String, ByVal Userid As String) As DataSet

        Dim ds As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(VendorCode <> String.Empty, VendorCode, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@userid"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Userid

        ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Vendor_Site_Depot_Wise_Dump_Report_Get]", Data.CommandType.StoredProcedure, sqlParams)
        Return ds
    End Function
End Class
