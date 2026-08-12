Imports Microsoft.VisualBasic

Public Class tcfgQualitySearchEntity
    Private iVendor As String
    Private iBrand As String
    Private iProduct As String
    Private iquarter As String


    Public Sub New()
        iVendor = String.Empty
        iBrand = String.Empty
        iProduct = String.Empty
        iquarter = String.Empty

    End Sub

    Public Property Vendor() As String
        Get
            Return iVendor
        End Get
        Set(ByVal value As String)
            iVendor = value
        End Set
    End Property
    Public Property Brand() As String
        Get
            Return iBrand
        End Get
        Set(ByVal value As String)
            iBrand = value
        End Set
    End Property
    Public Property Product() As String
        Get
            Return iProduct
        End Get
        Set(ByVal value As String)
            iProduct = value
        End Set
    End Property

    Public Property Quarter() As String
        Get
            Return iquarter
        End Get
        Set(ByVal value As String)
            iquarter = value
        End Set
    End Property


End Class
