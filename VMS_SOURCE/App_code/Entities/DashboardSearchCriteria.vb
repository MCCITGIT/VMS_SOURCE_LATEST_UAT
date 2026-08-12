Imports Microsoft.VisualBasic

Public Class DashboardSearchCriteria
    Private DUnit As String
    Private DRegion As String
    Private DDepot As String
    Private DYear As String
    Private DMonth As String
    Private DProduct As String
    Private DUnitPageSize As String
    Private DUnitPageIndex As String
    Private DDepotPageSize As String
    Private DDepotPageIndex As String
    Public Sub New()
        DUnit = String.Empty
        DRegion = String.Empty
        DDepot = String.Empty
        DYear = String.Empty
        DMonth = String.Empty
        DProduct = String.Empty
        DUnitPageSize = Integer.MinValue
        DUnitPageIndex = Integer.MinValue
        DDepotPageSize = Integer.MinValue
        DDepotPageIndex = Integer.MinValue

    End Sub
    Public Property Unit() As String
        Get
            Return DUnit
        End Get
        Set(ByVal value As String)
            DUnit = value
        End Set
    End Property
    Public Property Region() As String
        Get
            Return DRegion
        End Get
        Set(ByVal value As String)
            DRegion = value
        End Set
    End Property
    Public Property Depot() As String
        Get
            Return DDepot
        End Get
        Set(ByVal value As String)
            DDepot = value
        End Set
    End Property
    Public Property Year() As String
        Get
            Return DYear
        End Get
        Set(ByVal value As String)
            DYear = value
        End Set
    End Property
    Public Property Month() As String
        Get
            Return DMonth
        End Get
        Set(ByVal value As String)
            DMonth = value
        End Set
    End Property
    Public Property Product() As String
        Get
            Return DProduct
        End Get
        Set(ByVal value As String)
            DProduct = value
        End Set
    End Property
    Public Property UnitPageSize() As Integer
        Get
            Return DUnitPageSize
        End Get
        Set(ByVal value As Integer)
            DUnitPageSize = value
        End Set
    End Property
    Public Property UnitPageIndex() As Integer
        Get
            Return DUnitPageIndex
        End Get
        Set(ByVal value As Integer)
            DUnitPageIndex = value
        End Set
    End Property
    Public Property DepotPageSize() As Integer
        Get
            Return DDepotPageSize
        End Get
        Set(ByVal value As Integer)
            DDepotPageSize = value
        End Set
    End Property
    Public Property DepotPageIndex() As Integer
        Get
            Return DDepotPageIndex
        End Get
        Set(ByVal value As Integer)
            DDepotPageIndex = value
        End Set
    End Property
End Class
