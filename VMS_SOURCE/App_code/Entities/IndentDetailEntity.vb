Imports Microsoft.VisualBasic

Public Class IndentDetailEntity

    Private depot As String
    Private fin_year As String
    Private fin_month As String
    Private vendor_unit As String
    Private sku_code As String
    Private created_user As String
    Private sku_nop As Integer
    Private sku_vol As Decimal
    Private sku_uom As String
    Private remarks As String
    Private pending_load As Integer
    Private indent_to_date As Integer
    Private desp_to_date As Integer
    Private indent_id As Integer
    Private Priority As String

    Public Sub New()

        depot = String.Empty
        fin_year = String.Empty
        fin_month = String.Empty
        vendor_unit = String.Empty
        sku_code = String.Empty
        created_user = String.Empty
        sku_nop = Integer.MinValue
        sku_vol = Decimal.MinValue
        sku_uom = String.Empty
        remarks = String.Empty
        pending_load = Integer.MinValue
        indent_to_date = Integer.MinValue
        desp_to_date = Integer.MinValue
        indent_id = Integer.MinValue
        Priority = String.Empty
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

    Public Property IndentSKUCode() As String
        Get
            Return sku_code
        End Get
        Set(ByVal value As String)
            sku_code = value
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

    Public Property IndentSKUNOP() As Integer
        Get
            Return sku_nop
        End Get
        Set(ByVal value As Integer)
            sku_nop = value
        End Set
    End Property

    Public Property IndentSKUVol() As Decimal
        Get
            Return sku_vol
        End Get
        Set(ByVal value As Decimal)
            sku_vol = value
        End Set
    End Property

    Public Property IndentSKUUOM() As String
        Get
            Return sku_uom
        End Get
        Set(ByVal value As String)
            sku_uom = value
        End Set
    End Property

    Public Property IndentSKURemarks() As String
        Get
            Return remarks
        End Get
        Set(ByVal value As String)
            remarks = value
        End Set
    End Property

    Public Property IndentSKUPendingLoad() As Integer
        Get
            Return pending_load
        End Get
        Set(ByVal value As Integer)
            pending_load = value
        End Set
    End Property

    Public Property IndentSKUIndentToDate() As Integer
        Get
            Return indent_to_date
        End Get
        Set(ByVal value As Integer)
            indent_to_date = value
        End Set
    End Property

    Public Property IndentSKUDespatchToDate() As Integer
        Get
            Return desp_to_date
        End Get
        Set(ByVal value As Integer)
            desp_to_date = value
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
    Public Property IndentPriority() As String
        Get
            Return Priority
        End Get
        Set(ByVal value As String)
            Priority = value
        End Set
    End Property
End Class
