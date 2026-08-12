Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class TokenVendorRequisitionListClass


#Region "Get Requistion List For Vendor "
    Public Function GetRequistionListForVendor(ByVal userId As String, ByVal userGroup As String, ByVal unit As String, ByVal token_vendor As String, ByVal trh_id As Integer, ByVal fromDate As SqlDateTime, ByVal todate As SqlDateTime) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(6) As SqlParameter

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
        sqlParams(2).Value = IIf(unit.Equals(""), DBNull.Value, unit)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@trh_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(trh_id.Equals(0), DBNull.Value, trh_id)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@trh_token_vendor"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(token_vendor.Equals(""), DBNull.Value, token_vendor)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@fromDate"
        sqlParams(5).SqlDbType = SqlDbType.DateTime
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(fromDate = SqlDateTime.MinValue, DBNull.Value, fromDate)

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@toDate"
        sqlParams(6).SqlDbType = SqlDbType.DateTime
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(todate = SqlDateTime.MinValue, DBNull.Value, todate)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Token_vendor_Requisition_list_ForVendor", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Requistion List For Vendor "
    Public Function GetRequistionListForVendor_despatch(ByVal userId As String, ByVal userGroup As String, ByVal unit As String, ByVal token_vendor As String, ByVal trh_id As Integer, ByVal fromDate As SqlDateTime, ByVal todate As SqlDateTime) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(6) As SqlParameter

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
        sqlParams(2).Value = IIf(unit.Equals(""), DBNull.Value, unit)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@trh_id"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(trh_id.Equals(0), DBNull.Value, trh_id)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@trh_token_vendor"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(token_vendor.Equals(""), DBNull.Value, token_vendor)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@fromDate"
        sqlParams(5).SqlDbType = SqlDbType.DateTime
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(fromDate = SqlDateTime.MinValue, DBNull.Value, fromDate)

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@toDate"
        sqlParams(6).SqlDbType = SqlDbType.DateTime
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(todate = SqlDateTime.MinValue, DBNull.Value, todate)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Token_vendor_Requisition_list_ForDespatch", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Unit List"
    Public Function GetUnitList(ByVal tokenVendor As String, ByVal status As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@tokenvendor"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = tokenVendor

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@case"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = status

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Get_Vendor_Unit_List", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

End Class
