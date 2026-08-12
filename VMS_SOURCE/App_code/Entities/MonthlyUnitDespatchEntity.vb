Imports Microsoft.VisualBasic

Public Class MonthlyUnitDespatchEntity
    Private MonthlyUnitDespatchEntity_Region As String
    Private MonthlyUnitDespatchEntity_Depot As String
    Private MonthlyUnitDespatchEntity_Unit As String
    Private MonthlyUnitDespatchEntity_ProcessYr As String
    Private MonthlyUnitDespatchEntity_ProcessMnth As String
    Private Report_Format As String

    Public Sub New()
        MonthlyUnitDespatchEntity_Region = String.Empty
        MonthlyUnitDespatchEntity_Depot = String.Empty
        MonthlyUnitDespatchEntity_Unit = String.Empty
        MonthlyUnitDespatchEntity_ProcessYr = String.Empty
        MonthlyUnitDespatchEntity_ProcessMnth = String.Empty
        Report_Format = String.Empty
    End Sub

    Public Property Region() As String
        Get
            Return MonthlyUnitDespatchEntity_Region
        End Get
        Set(ByVal value As String)
            MonthlyUnitDespatchEntity_Region = value
        End Set
    End Property

    Public Property Depot() As String
        Get
            Return MonthlyUnitDespatchEntity_Depot
        End Get
        Set(ByVal value As String)
            MonthlyUnitDespatchEntity_Depot = value
        End Set
    End Property

    Public Property Unit() As String
        Get
            Return MonthlyUnitDespatchEntity_Unit
        End Get
        Set(ByVal value As String)
            MonthlyUnitDespatchEntity_Unit = value
        End Set
    End Property

    Public Property ProcessYr() As String
        Get
            Return MonthlyUnitDespatchEntity_ProcessYr
        End Get
        Set(ByVal value As String)
            MonthlyUnitDespatchEntity_ProcessYr = value
        End Set
    End Property

    Public Property ProcessMnth() As String
        Get
            Return MonthlyUnitDespatchEntity_ProcessMnth
        End Get
        Set(ByVal value As String)
            MonthlyUnitDespatchEntity_ProcessMnth = value
        End Set
    End Property

    Public Property ReportFormat() As String
        Get
            Return Report_Format
        End Get
        Set(ByVal value As String)
            Report_Format = value
        End Set
    End Property
End Class
