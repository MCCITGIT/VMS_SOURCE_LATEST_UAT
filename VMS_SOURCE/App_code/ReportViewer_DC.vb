Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Configuration


Public Class ReportViewer_DC
    Shared GetSet_ReportCase As String

    Public Property ReportCase() As String
        Get
            Return GetSet_ReportCase
        End Get
        Set(ByVal value As String)
            GetSet_ReportCase = value
        End Set
    End Property
    Shared GetSet_ReportFileName As String
    Public Property ReportFileName() As String
        Get
            Return GetSet_ReportFileName
        End Get
        Set(ByVal value As String)
            GetSet_ReportFileName = value
        End Set
    End Property
    Shared GetSet_TableName As String

    Public Property TableName() As String
        Get
            Return GetSet_TableName
        End Get
        Set(ByVal value As String)
            GetSet_TableName = value
        End Set
    End Property

    Shared GetSet_Company As String
    Public Property Company() As String
        Get
            Return GetSet_Company
        End Get
        Set(ByVal value As String)
            GetSet_Company = value
        End Set
    End Property

    Shared GetSet_ReportType As String

    Public Property ReportType() As String
        Get
            Return GetSet_ReportType
        End Get
        Set(ByVal value As String)
            GetSet_ReportType = value
        End Set
    End Property

    'Shared GetSet_Active As String
    'Public Property Active() As String
    '    Get
    '        Return GetSet_Active
    '    End Get
    '    Set(ByVal value As String)
    '        GetSet_Active = value
    '    End Set
    'End Property

    'Added by Debayan Biswas on 07-11-2011 For Estimated_Data_Despatched_Status_Report
    'Start
    Shared Estmtd_Data_Depot As String
    Public Property EstmtdDataDepot() As String
        Get
            Return Estmtd_Data_Depot
        End Get
        Set(ByVal value As String)
            Estmtd_Data_Depot = value
        End Set
    End Property
    Shared Estmtd_Data_Unit As String
    Public Property EstmtdDataUnit() As String
        Get
            Return Estmtd_Data_Unit
        End Get
        Set(ByVal value As String)
            Estmtd_Data_Unit = value
        End Set
    End Property
    Shared Estmtd_Data_SKU_Code As String
    Public Property EstmtdDataSKUCode() As String
        Get
            Return Estmtd_Data_SKU_Code
        End Get
        Set(ByVal value As String)
            Estmtd_Data_SKU_Code = value
        End Set
    End Property
    Shared Estmtd_Data_Fin_Year As String
    Public Property EstmtdDataFinYear() As String
        Get
            Return Estmtd_Data_Fin_Year
        End Get
        Set(ByVal value As String)
            Estmtd_Data_Fin_Year = value
        End Set
    End Property
    Shared Estmtd_Data_Month As String
    Public Property EstmtdDataMonth() As String
        Get
            Return Estmtd_Data_Month
        End Get
        Set(ByVal value As String)
            Estmtd_Data_Month = value
        End Set
    End Property
    'End

    'Added by Debayan Biswas on 10-11-2011 For Depot_Despatch_Unitwise_Report
    'Start
    Shared Dpt_Dsptchd_UntWise_Depot As String
    Public Property DptDsptchdUntWiseDepot() As String
        Get
            Return Dpt_Dsptchd_UntWise_Depot
        End Get
        Set(ByVal value As String)
            Dpt_Dsptchd_UntWise_Depot = value
        End Set
    End Property
    Shared Dpt_Dsptchd_UntWise_Unit As String
    Public Property DptDsptchdUntWiseUnit() As String
        Get
            Return Dpt_Dsptchd_UntWise_Unit
        End Get
        Set(ByVal value As String)
            Dpt_Dsptchd_UntWise_Unit = value
        End Set
    End Property
    Shared Dpt_Dsptchd_UntWise_Fin_Year As String
    Public Property DptDsptchdUntWiseFinYear() As String
        Get
            Return Dpt_Dsptchd_UntWise_Fin_Year
        End Get
        Set(ByVal value As String)
            Dpt_Dsptchd_UntWise_Fin_Year = value
        End Set
    End Property
    Shared Dpt_Dsptchd_UntWise_Fin_Month As String
    Public Property DptDsptchdUntWiseFinMonth() As String
        Get
            Return Dpt_Dsptchd_UntWise_Fin_Month
        End Get
        Set(ByVal value As String)
            Dpt_Dsptchd_UntWise_Fin_Month = value
        End Set
    End Property
    'End

    'Added by Debayan Biswas on 10-11-2011 For StockUploadSummary_Report
    'Start
    Shared Stck_Upld_Process_Year As String
    Public Property StckUpldProcessYear() As String
        Get
            Return Stck_Upld_Process_Year
        End Get
        Set(ByVal value As String)
            Stck_Upld_Process_Year = value
        End Set
    End Property
    Shared Stck_Upld_Process_Month As String
    Public Property StckUpldProcessMonth() As String
        Get
            Return Stck_Upld_Process_Month
        End Get
        Set(ByVal value As String)
            Stck_Upld_Process_Month = value
        End Set
    End Property
    'End

    'Added by Debayan Biswas on 17-12-2011 For Despatched_Advice_Report
    'Start

    Shared Dsptchd_Advice_Depot As String
    Public Property DsptchdAdviceDepot() As String
        Get
            Return Dsptchd_Advice_Depot
        End Get
        Set(ByVal value As String)
            Dsptchd_Advice_Depot = value
        End Set
    End Property
    Shared Dsptchd_Advice_Unit As String
    Public Property DsptchdAdviceUnit() As String
        Get
            Return Dsptchd_Advice_Unit
        End Get
        Set(ByVal value As String)
            Dsptchd_Advice_Unit = value
        End Set
    End Property
    Shared Dsptchd_Advice_FinYear As String
    Public Property DsptchdAdviceFinYear() As String
        Get
            Return Dsptchd_Advice_FinYear
        End Get
        Set(ByVal value As String)
            Dsptchd_Advice_FinYear = value
        End Set
    End Property
    Shared Dsptchd_Advice_ChlnNo As Integer
    Public Property DsptchdAdviceChlnNo() As Integer
        Get
            Return Dsptchd_Advice_ChlnNo
        End Get
        Set(ByVal value As Integer)
            Dsptchd_Advice_ChlnNo = value
        End Set
    End Property
    Shared Dsptchd_ID As String
    Public Property DsptchId() As String
        Get
            Return Dsptchd_ID
        End Get
        Set(ByVal value As String)
            Dsptchd_ID = value
        End Set
    End Property

    'End

    'created by deeepak for MonthlyUnitDespatch
    Shared GetSet_Active As String

    Public Property Active() As String
        Get
            Return GetSet_Active
        End Get
        Set(ByVal value As String)
            GetSet_Active = value
        End Set
    End Property

    Shared GetSET_Region As String

    Public Property Region() As String
        Get
            Return GetSET_Region
        End Get
        Set(ByVal value As String)
            GetSET_Region = value
        End Set
    End Property

    Shared GetSET_Depot As String

    Public Property Depot() As String
        Get
            Return GetSET_Depot
        End Get
        Set(ByVal value As String)
            GetSET_Depot = value
        End Set
    End Property

    Shared GetSET_Unit As String
    Public Property Unit() As String
        Get
            Return GetSET_Unit
        End Get
        Set(ByVal value As String)
            GetSET_Unit = value
        End Set
    End Property

    Shared GetSET_ProcessYr As String
    Public Property ProcessYr() As String
        Get
            Return GetSET_ProcessYr
        End Get
        Set(ByVal value As String)
            GetSET_ProcessYr = value
        End Set
    End Property

    Shared GetSET_ProcessMnth As String
    Public Property ProcessMnth() As String
        Get
            Return GetSET_ProcessMnth
        End Get
        Set(ByVal value As String)
            GetSET_ProcessMnth = value
        End Set
    End Property

    'Added by Debayan Biswas on 21-12-2011 For Unitwise_SKU_Despatched_Report
    'Start
    Shared Unitwise_SKU_Dsptch_Unit As String
    Public Property UnitwiseSKUDsptch_Unit() As String
        Get
            Return Unitwise_SKU_Dsptch_Unit
        End Get
        Set(ByVal value As String)
            Unitwise_SKU_Dsptch_Unit = value
        End Set
    End Property
    Shared Unitwise_SKU_Dsptch_FinYear As String
    Public Property UnitwiseSKUDsptch_FinYear() As String
        Get
            Return Unitwise_SKU_Dsptch_FinYear
        End Get
        Set(ByVal value As String)
            Unitwise_SKU_Dsptch_FinYear = value
        End Set
    End Property
    Shared Unitwise_SKU_Dsptch_Month As String
    Public Property UnitwiseSKUDsptch_Month() As String
        Get
            Return Unitwise_SKU_Dsptch_Month
        End Get
        Set(ByVal value As String)
            Unitwise_SKU_Dsptch_Month = value
        End Set
    End Property
    'End

    'Added by Debayan Biswas on 15-11-2011 For Monthly_Depot_Indent_List_Report
    'Start
    Shared Mnthly_Dpt_Indnt_Lst_Rpt_Region As String
    Public Property MnthlyDptIndntLstRptRegion() As String
        Get
            Return Mnthly_Dpt_Indnt_Lst_Rpt_Region
        End Get
        Set(ByVal value As String)
            Mnthly_Dpt_Indnt_Lst_Rpt_Region = value
        End Set
    End Property
    Shared Mnthly_Dpt_Indnt_Lst_Rpt_Depot As String
    Public Property MnthlyDptIndntLstRptDepot() As String
        Get
            Return Mnthly_Dpt_Indnt_Lst_Rpt_Depot
        End Get
        Set(ByVal value As String)
            Mnthly_Dpt_Indnt_Lst_Rpt_Depot = value
        End Set
    End Property
    Shared Mnthly_Dpt_Indnt_Lst_Rpt_FinYear As String
    Public Property MnthlyDptIndntLstRptFinYear() As String
        Get
            Return Mnthly_Dpt_Indnt_Lst_Rpt_FinYear
        End Get
        Set(ByVal value As String)
            Mnthly_Dpt_Indnt_Lst_Rpt_FinYear = value
        End Set
    End Property
    Shared Mnthly_Dpt_Indnt_Lst_Rpt_Month As String
    Public Property MnthlyDptIndntLstRptMonth() As String
        Get
            Return Mnthly_Dpt_Indnt_Lst_Rpt_Month
        End Get
        Set(ByVal value As String)
            Mnthly_Dpt_Indnt_Lst_Rpt_Month = value
        End Set
    End Property
    'End

    'Added by Debayan Biswas on 07-01-2012 For User_Profile_List_Report
    'Start
    Shared _up_Company As String
    Public Property up_Company() As String
        Get
            Return _up_Company
        End Get
        Set(ByVal value As String)
            _up_Company = value
        End Set
    End Property
    Shared _up_Region As String
    Public Property up_Region() As String
        Get
            Return _up_Region
        End Get
        Set(ByVal value As String)
            _up_Region = value
        End Set
    End Property
    Shared _up_Depot As String
    Public Property up_Depot() As String
        Get
            Return _up_Depot
        End Get
        Set(ByVal value As String)
            _up_Depot = value
        End Set
    End Property
    'End

End Class


