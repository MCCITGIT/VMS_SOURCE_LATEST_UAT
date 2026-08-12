Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Public Class UnitRequisitionReportClass
#Region "Get Report for Dealer Machine Return List  Report"
    Public Function UnitRequisition_Report(ByVal active As String, ByVal vendor As String, ByVal unit As String, ByVal ProcessYr As String, ByVal ProcessMnth As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(Unit <> String.Empty, Unit, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@ProcessYr"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(ProcessYr <> String.Empty, ProcessYr, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@ProcessMnth"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(ProcessMnth <> String.Empty, ProcessMnth, DBNull.Value)


        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[UnitRequisition_Report]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
#End Region

    Public Function GetRequisitionreport(ByVal unit As String, ByVal product As String, ByVal packSize As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@product"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(product.Equals(String.Empty), DBNull.Value, product)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@packSize"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(packSize.Equals(String.Empty), DBNull.Value, packSize)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[Token_Stock_Master_Get]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
    Public Function GetApplicablePackSize(ByVal unit As String, ByVal product As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(unit.Equals(String.Empty), DBNull.Value, unit)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@product"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = product

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[Get_Applicable_Packsize]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
End Class
