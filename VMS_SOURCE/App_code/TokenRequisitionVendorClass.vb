Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class TokenRequisitionVendorClass
#Region "Get Vendor Unit "
    Public Function GetUnitName(ByVal vendorId As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorId"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = vendorId

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Requisition_Unit_List_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
#Region "Get Requistion List "
    Public Function GetRequistionList(ByVal userId As String, ByVal userGroup As String, ByVal unit As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userid"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@userGroup"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userGroup

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Token_vendor_Requisition_list_Get", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
End Class
