Imports Microsoft.VisualBasic

Public Class IndentHeaderEntity

    Private depot As String
    Private fin_year As String
    Private fin_month As String
    Private vendor_unit As String
    Private product As String
    Private created_user As String
    Private status As String
    Private indent_id As Integer
    Private approve_yn As String
    Private remarks As String


    Public Sub New()

        depot = String.Empty
        fin_year = String.Empty
        fin_month = String.Empty
        vendor_unit = String.Empty
        product = String.Empty
        created_user = String.Empty
        status = String.Empty
        indent_id = Integer.MinValue
        approve_yn = String.Empty
        remarks = String.Empty

    End Sub

    Public Property IndentDepot() As String
        Get
            Return depot
        End Get
        Set(ByVal value As String)
            depot = value
        End Set
    End Property

    Public Property IndentFinYear() As String
        Get
            Return fin_year
        End Get
        Set(ByVal value As String)
            fin_year = value
        End Set
    End Property

    Public Property IndentFinMonth() As String
        Get
            Return fin_month
        End Get
        Set(ByVal value As String)
            fin_month = value
        End Set
    End Property

    Public Property IndentVendorUnit() As String
        Get
            Return vendor_unit
        End Get
        Set(ByVal value As String)
            vendor_unit = value
        End Set
    End Property

    Public Property IndentProduct() As String
        Get
            Return product
        End Get
        Set(ByVal value As String)
            product = value
        End Set
    End Property

    Public Property IndentCreatedUser() As String
        Get
            Return created_user
        End Get
        Set(ByVal value As String)
            created_user = value
        End Set
    End Property

    Public Property IndentStatus() As String
        Get
            Return status
        End Get
        Set(ByVal value As String)
            status = value
        End Set
    End Property

    Public Property IndentID() As Integer
        Get
            Return indent_id
        End Get
        Set(ByVal value As Integer)
            indent_id = value
        End Set
    End Property

    Public Property IndentApproveYN() As String
        Get
            Return approve_yn
        End Get
        Set(ByVal value As String)
            approve_yn = value
        End Set
    End Property

    Public Property IndentRemarks() As String
        Get
            Return remarks
        End Get
        Set(ByVal value As String)
            remarks = value
        End Set
    End Property

End Class

