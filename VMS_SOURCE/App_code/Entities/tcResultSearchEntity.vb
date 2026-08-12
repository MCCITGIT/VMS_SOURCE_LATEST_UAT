Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes

Namespace VMS.Web

    Public Class tcResultSearchEntity

        Private iVendor As String
        Private iBrand As String
        Private iProduct As String
        Private iBatchNo As String
        Private iFromDate As String
        Private iToDate As String

        Public Sub New()
            iVendor = String.Empty
            iBrand = String.Empty
            iProduct = String.Empty
            iBatchNo = String.Empty
            iFromDate = String.Empty
            iToDate = String.Empty
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

        Public Property BatchNo() As String
            Get
                Return iBatchNo
            End Get
            Set(ByVal value As String)
                iBatchNo = value
            End Set
        End Property

        Public Property FromDate() As String
            Get
                Return iFromDate
            End Get
            Set(ByVal value As String)
                iFromDate = value
            End Set
        End Property

        Public Property ToDate() As String
            Get
                Return iToDate
            End Get
            Set(ByVal value As String)
                iToDate = value
            End Set
        End Property

    End Class

End Namespace

