'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/EstmtdDataDsptchdStatSearchCriteria.vb
'Created Date	: 07-December-2011
'Created By	    : Debayan Biswas
'Version	    : R01.00.00
'Description	: Code behind file for EstmtdDataDsptchdStatSearchCriteria Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Public Class EstmtdDataDsptchdStatSearchCriteria
    Private EstmtdDataDsptchdStatDepot As String
    Private EstmtdDataDsptchdStatRegion As String
    Private EstmtdDataDsptchdStatUnit As String
    Private EstmtdDataDsptchdStatSKU As String
    Private EstmtdDataDsptchdStatFinyr As String
    Private EstmtdDataDsptchdStatMonth As String
    Private EstmtdDataDsptchdStatPrntOptn As String
    Public Sub New()
        EstmtdDataDsptchdStatDepot = String.Empty
        EstmtdDataDsptchdStatRegion = String.Empty
        EstmtdDataDsptchdStatUnit = String.Empty
        EstmtdDataDsptchdStatSKU = String.Empty
        EstmtdDataDsptchdStatFinyr = String.Empty
        EstmtdDataDsptchdStatMonth = String.Empty
        EstmtdDataDsptchdStatPrntOptn = String.Empty
    End Sub
    Public Property Estmtd_Data_Dsptchd_Stat_Depot() As String
        Get
            Return EstmtdDataDsptchdStatDepot
        End Get
        Set(ByVal value As String)
            EstmtdDataDsptchdStatDepot = value
        End Set
    End Property
    Public Property Estmtd_Data_Dsptchd_Stat_Region() As String
        Get
            Return EstmtdDataDsptchdStatRegion
        End Get
        Set(ByVal value As String)
            EstmtdDataDsptchdStatRegion = value
        End Set
    End Property
    Public Property Estmtd_Data_Dsptchd_Stat_Unit() As String
        Get
            Return EstmtdDataDsptchdStatUnit
        End Get
        Set(ByVal value As String)
            EstmtdDataDsptchdStatUnit = value
        End Set
    End Property
    Public Property Estmtd_Data_Dsptchd_Stat_SKU() As String
        Get
            Return EstmtdDataDsptchdStatSKU
        End Get
        Set(ByVal value As String)
            EstmtdDataDsptchdStatSKU = value
        End Set
    End Property
    Public Property Estmtd_Data_Dsptchd_Stat_Finyr() As String
        Get
            Return EstmtdDataDsptchdStatFinyr
        End Get
        Set(ByVal value As String)
            EstmtdDataDsptchdStatFinyr = value
        End Set
    End Property
    Public Property Estmtd_Data_Dsptchd_Stat_Month() As String
        Get
            Return EstmtdDataDsptchdStatMonth
        End Get
        Set(ByVal value As String)
            EstmtdDataDsptchdStatMonth = value
        End Set
    End Property
    Public Property Estmtd_Data_Dsptchd_Stat_PrntOptn() As String
        Get
            Return EstmtdDataDsptchdStatPrntOptn
        End Get
        Set(ByVal value As String)
            EstmtdDataDsptchdStatPrntOptn = value
        End Set
    End Property
End Class
