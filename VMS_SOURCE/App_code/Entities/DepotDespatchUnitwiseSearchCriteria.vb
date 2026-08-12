'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/DepotDespatchUnitwiseSearchCriteria.vb
'Created Date	: 14-December-2011
'Created By	    : Debayan Biswas
'Version	    : R01.00.00
'Description	: Code behind file for DepotDespatchUnitwiseSearchCriteria Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Public Class DepotDespatchUnitwiseSearchCriteria
    Private DepotDespatchUnitwiseDepot As String
    Private DepotDespatchUnitwiseRegion As String
    Private DepotDespatchUnitwiseUnit As String
    Private DepotDespatchUnitwiseFinyear As String
    Private DepotDespatchUnitwiseMonth As String
    Private DepotDespatchUnitwisePrntOptn As String
    Public Sub New()
        DepotDespatchUnitwiseDepot = String.Empty
        DepotDespatchUnitwiseRegion = String.Empty
        DepotDespatchUnitwiseUnit = String.Empty
        DepotDespatchUnitwiseFinyear = String.Empty
        DepotDespatchUnitwiseMonth = String.Empty
        DepotDespatchUnitwisePrntOptn = String.Empty
    End Sub
    Public Property Depot_Despatch_Unitwise_Depot() As String
        Get
            Return DepotDespatchUnitwiseDepot
        End Get
        Set(ByVal value As String)
            DepotDespatchUnitwiseDepot = value
        End Set
    End Property
    Public Property Depot_Despatch_Unitwise_Region() As String
        Get
            Return DepotDespatchUnitwiseRegion
        End Get
        Set(ByVal value As String)
            DepotDespatchUnitwiseRegion = value
        End Set
    End Property
    Public Property Depot_Despatch_Unitwise_Unit() As String
        Get
            Return DepotDespatchUnitwiseUnit
        End Get
        Set(ByVal value As String)
            DepotDespatchUnitwiseUnit = value
        End Set
    End Property
    Public Property Depot_Despatch_Unitwise_Finyear() As String
        Get
            Return DepotDespatchUnitwiseFinyear
        End Get
        Set(ByVal value As String)
            DepotDespatchUnitwiseFinyear = value
        End Set
    End Property
    Public Property Depot_Despatch_Unitwise_Month() As String
        Get
            Return DepotDespatchUnitwiseMonth
        End Get
        Set(ByVal value As String)
            DepotDespatchUnitwiseMonth = value
        End Set
    End Property
    Public Property Depot_Despatch_Unitwise_PrntOptn() As String
        Get
            Return DepotDespatchUnitwisePrntOptn
        End Get
        Set(ByVal value As String)
            DepotDespatchUnitwisePrntOptn = value
        End Set
    End Property
End Class
