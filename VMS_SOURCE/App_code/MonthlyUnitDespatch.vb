Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Imports NPOI.SS.Formula.Functions

Public Class MonthlyUnitDespatch

    '#Region "Get Process Year "
    '    Public Function GetProcessYr(ByVal active As String) As DataSet
    '        Dim PrjectList As DataSet

    '        Dim sqlParams(0) As SqlParameter

    '        sqlParams(0) = New SqlParameter()
    '        sqlParams(0).ParameterName = "@active"
    '        sqlParams(0).DbType = DbType.String
    '        sqlParams(0).Direction = Data.ParameterDirection.Input
    '        sqlParams(0).Value = active

    '        PrjectList = DBFactory.GetHelper().ExecuteDataSet("MonthlyUnitDespatch_GetProcessYr", Data.CommandType.StoredProcedure, sqlParams)
    '        Return PrjectList

    '    End Function
    '#End Region
#Region "Get Vendor Unit "
    Public Function GetUnitName(ByVal active As String, ByVal region As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@region"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = IIf(region <> String.Empty, region, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("MonthlyUnitDespatch_GetUnitName", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region

#Region "Get Report for Dealer Machine Return List  Report"
    Public Function GetMonthlyUnitDespatchReport(ByVal active As String, ByVal region As String, ByVal depot As String, ByVal ProcessYr As String, ByVal ProcessMnth As String, ByVal unit As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(5) As SqlParameter

        'sqlParams(0) = New SqlParameter()
        'sqlParams(0).ParameterName = "@company"
        'sqlParams(0).DbType = DbType.String
        'sqlParams(0).Direction = Data.ParameterDirection.Input
        'sqlParams(0).Value = company

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@region"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@depot"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

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

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@unit"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        'sqlParams(5).Value = unit
        sqlParams(5).Value = IIf(unit <> String.Empty, unit, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("MonthlyUnitDespatch_Report_vr1", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function

    Public Function GetMonthlyUnitDespatchReportVr2(ByVal region As String, ByVal depot As String, ByVal unit As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal active As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@region"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(unit <> String.Empty, unit, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@FromDate"
        sqlParams(3).DbType = DbType.Date
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = FromDate

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@ToDate"
        sqlParams(4).DbType = DbType.Date
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = ToDate

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@active"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = active

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("MonthlyUnitDespatch_Report_vr6", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
#End Region

#Region "Get Year , Months from Standard params "
    Public Function GetMnthsYr(ByVal active As String) As DataSet
        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@active"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = active

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@region"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = IIf(region <> String.Empty, region, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("MonthlyUnitDespatch_GetStandardParams", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList

    End Function
#End Region
    Public Function GetMonthYearWiseSKUDespatchReport(ByVal year As String, ByVal month As String, ByVal userid As String, ByVal product_category As String, ByVal Productcode As String, ByVal region As String, ByVal depot As String, ByVal vendor As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(7) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@year"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(year <> String.Empty, year, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@month"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(month <> String.Empty, month, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@UserId"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = userid

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@product_category"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(product_category <> String.Empty, product_category, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@sku_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(Productcode <> String.Empty, Productcode, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@region"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@depot_code"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(7) = New SqlParameter()
        sqlParams(7).ParameterName = "@vendor"
        sqlParams(7).DbType = DbType.String
        sqlParams(7).Direction = Data.ParameterDirection.Input
        sqlParams(7).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Unit_Month_Year_Wise_SKU_Despatch_Report", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
    Public Function GetRegionDepotWiseSKUDespatchReport(ByVal year As String, ByVal month As String, ByVal userid As String, ByVal product_category As String, ByVal Productcode As String, ByVal region As String, ByVal depot As String, ByVal vendor As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(7) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@year"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(year <> String.Empty, year, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@month"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(month <> String.Empty, month, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@UserId"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = userid

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@product_category"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(product_category <> String.Empty, product_category, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@sku_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(Productcode <> String.Empty, Productcode, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@region"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@depot_code"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(7) = New SqlParameter()
        sqlParams(7).ParameterName = "@vendor"
        sqlParams(7).DbType = DbType.String
        sqlParams(7).Direction = Data.ParameterDirection.Input
        sqlParams(7).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Region_Depot_Wise_SKU_Despatch_Report", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
    Public Function GetVendorWiseDespatchReport(ByVal year As String, ByVal month As String, ByVal userid As String, ByVal product_category As String, ByVal Productcode As String, ByVal region As String, ByVal depot As String, ByVal vendor As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(7) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@year"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(year <> String.Empty, year, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@month"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(month <> String.Empty, month, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@UserId"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = userid

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@product_category"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(product_category <> String.Empty, product_category, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@sku_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(Productcode <> String.Empty, Productcode, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@region"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@depot_code"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(7) = New SqlParameter()
        sqlParams(7).ParameterName = "@vendor"
        sqlParams(7).DbType = DbType.String
        sqlParams(7).Direction = Data.ParameterDirection.Input
        sqlParams(7).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[Unit_Wise_Depot_Despatch_Report]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
    Public Function GetFactoryWiseDespatchReport(ByVal year As String, ByVal month As String, ByVal userid As String, ByVal product_category As String, ByVal Productcode As String, ByVal region As String, ByVal depot As String, ByVal vendor As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(7) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@year"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(year <> String.Empty, year, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@month"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(month <> String.Empty, month, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@UserId"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = userid

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@product_category"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(product_category <> String.Empty, product_category, DBNull.Value)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@sku_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(Productcode <> String.Empty, Productcode, DBNull.Value)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@region"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@depot_code"
        sqlParams(6).DbType = DbType.String
        sqlParams(6).Direction = Data.ParameterDirection.Input
        sqlParams(6).Value = IIf(depot <> String.Empty, depot, DBNull.Value)

        sqlParams(7) = New SqlParameter()
        sqlParams(7).ParameterName = "@vendor"
        sqlParams(7).DbType = DbType.String
        sqlParams(7).Direction = Data.ParameterDirection.Input
        sqlParams(7).Value = IIf(vendor <> String.Empty, vendor, DBNull.Value)

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[Unit_Wise_Factory_Despatch_Report]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
    Public Function GetProductCategoryWiseSKUDtls(ByVal product_category As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@product_category"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = product_category

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[Get_Product_Category_Wise_SKU_dtl]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
    Public Function GetFactryList(ByVal userid As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@UserId"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userid

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("[Factory_List_Get]", Data.CommandType.StoredProcedure, sqlParams)
        Return PrjectList
    End Function
End Class
