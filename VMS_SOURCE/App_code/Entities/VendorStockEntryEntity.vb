Imports Microsoft.VisualBasic

Public Class VendorStockEntryEntity
    Private vssm_vendor_id As String
    Private vssm_ason_date As String
    Private vssm_sku_code As String
    Private vssm_nop As Double
    Private vssm_vol As Double
    Private created_user As String
    Private created_date As DateTime

    Public Sub New()
        vssm_vendor_id = String.Empty
        vssm_ason_date = String.Empty
        vssm_sku_code = String.Empty
        vssm_nop = Double.MinValue
        vssm_vol = Double.MinValue
        created_user = String.Empty
        created_date = DateTime.MinValue
    End Sub

    Public Property vendor_id() As String
        Get
            Return vssm_vendor_id
        End Get
        Set(ByVal value As String)
            vssm_vendor_id = value
        End Set
    End Property
    Public Property [date]() As String
        Get
            Return vssm_ason_date
        End Get
        Set(ByVal value As String)
            vssm_ason_date = value
        End Set
    End Property
    Public Property sku_code() As Integer
        Get
            Return vssm_sku_code
        End Get
        Set(ByVal value As Integer)
            vssm_sku_code = value
        End Set
    End Property

    Public Property vsm_nop() As Integer
        Get
            Return [vssm_nop]
        End Get
        Set(ByVal value As Integer)
            vssm_nop = value
        End Set
    End Property

    Public Property vsm_vol() As Integer
        Get
            Return vssm_vol
        End Get
        Set(ByVal value As Integer)
            vssm_vol = value
        End Set
    End Property
    Public Property CreatedUser() As String
        Get
            Return created_user
        End Get
        Set(ByVal value As String)
            created_user = value
        End Set
    End Property
    Public Property CreatedDate() As DateTime
        Get
            Return created_date
        End Get
        Set(ByVal value As DateTime)
            created_date = value
        End Set
    End Property
End Class
