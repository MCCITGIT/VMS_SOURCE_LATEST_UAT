Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class SourceMatrixClass
    Public Function SourceMatrixDataList(ByRef depot As String, ByVal vendor As String, ByVal sku As String) As DataSet
        Dim ds As DataSet
        Try

            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot_code"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor_code"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@sku"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(sku <> String.Empty, sku, DBNull.Value)

            ds = DBFactory.GetHelper().ExecuteDataSet("[dbo].[GetSourceMatrixData]", Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try

        Return ds

    End Function
End Class
