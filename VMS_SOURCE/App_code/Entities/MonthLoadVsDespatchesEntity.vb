Imports Microsoft.VisualBasic

Public Class MonthLoadVsDespatchesEntity
    Private DespatchEntity_Region As String
    Private DespatchEntity_Depot As String
    Private DespatchEntity_Unit As String
    Private DespatchEntity_ProcessYr As String
    Private DespatchEntity_ProcessMnth As String
    Private DespatchEntity_OrderBy As String
    Private Report_Format As String

    Public Sub New()
        DespatchEntity_Region = String.Empty
        DespatchEntity_Depot = String.Empty
        DespatchEntity_Unit = String.Empty
        DespatchEntity_ProcessYr = String.Empty
        DespatchEntity_ProcessMnth = String.Empty
        Report_Format = String.Empty
        DespatchEntity_OrderBy = String.Empty
    End Sub

    Public Property OrderBy() As String
        Get
            Return DespatchEntity_OrderBy
        End Get
        Set(ByVal value As String)
            DespatchEntity_OrderBy = value
        End Set
    End Property



    Public Property Region() As String
        Get
            Return DespatchEntity_Region
        End Get
        Set(ByVal value As String)
            DespatchEntity_Region = value
        End Set
    End Property

    Public Property Depot() As String
        Get
            Return DespatchEntity_Depot
        End Get
        Set(ByVal value As String)
            DespatchEntity_Depot = value
        End Set
    End Property

    Public Property Unit() As String
        Get
            Return DespatchEntity_Unit
        End Get
        Set(ByVal value As String)
            DespatchEntity_Unit = value
        End Set
    End Property

    Public Property ProcessYr() As String
        Get
            Return DespatchEntity_ProcessYr
        End Get
        Set(ByVal value As String)
            DespatchEntity_ProcessYr = value
        End Set
    End Property

    Public Property ProcessMnth() As String
        Get
            Return DespatchEntity_ProcessMnth
        End Get
        Set(ByVal value As String)
            DespatchEntity_ProcessMnth = value
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
