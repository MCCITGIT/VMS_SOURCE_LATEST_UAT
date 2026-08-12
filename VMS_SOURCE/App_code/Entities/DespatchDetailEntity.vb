Imports Microsoft.VisualBasic

Public Class DespatchDetailEntity
    Private despd_desp_unit As String
    Private despd_desp_depot As String
    Private despd_challan_fin_year As String
    Private despd_challan_no As Integer
    Private despd_challan_date As DateTime
    Private despd_srl As Integer
    Private despd_sku_code As String
    Private despd_sku_uom As String
    Private despd_desp_nop As Integer
    Private despd_sku_vol As Decimal
    Private despd_auto_indent As Integer
    Private despd_depot_indent As Integer
    Private despd_indent_total As Integer
    Private despd_despatch_to_date As Integer
    Private despd_pending_load As Integer
    Private created_user As String
    Private created_date As DateTime
    Private modified_user As String
    Private modified_date As DateTime
    Private deleted_user As String
    Private deleted_date As DateTime
    Private active As String
    Private despd_process_month As String
    Private despd_transit_till As DateTime

    Private despd_lot_no As String
    Private despd_line_num As Integer
    Private despd_po_rate As Decimal
    Private despd_sku_gst As Decimal

    Public Sub New()
        despd_desp_unit = String.Empty
        despd_desp_depot = String.Empty
        despd_challan_fin_year = String.Empty
        despd_challan_no = Integer.MinValue
        despd_challan_date = DateTime.MinValue
        despd_srl = Integer.MinValue
        despd_sku_code = String.Empty
        despd_sku_uom = String.Empty
        despd_desp_nop = Integer.MinValue
        despd_sku_vol = Decimal.MinValue
        despd_auto_indent = Integer.MinValue
        despd_depot_indent = Integer.MinValue
        despd_indent_total = Integer.MinValue
        despd_despatch_to_date = Integer.MinValue
        despd_pending_load = Integer.MinValue
        created_user = String.Empty
        created_date = DateTime.MinValue
        modified_user = String.Empty
        modified_date = DateTime.MinValue
        deleted_user = String.Empty
        deleted_date = DateTime.MinValue
        active = String.Empty
        despd_process_month = String.Empty
        despd_transit_till = DateTime.MinValue
        despd_lot_no = String.Empty
        despd_line_num = Integer.MinValue
        despd_po_rate = Decimal.MinValue
        despd_sku_gst = Decimal.MinValue

    End Sub
    Public Property DespUnit() As String
        Get
            Return despd_desp_unit
        End Get
        Set(ByVal value As String)
            despd_desp_unit = value
        End Set
    End Property
    Public Property DespDepot() As String
        Get
            Return despd_desp_depot
        End Get
        Set(ByVal value As String)
            despd_desp_depot = value
        End Set
    End Property
    Public Property ChallanFinYear() As String
        Get
            Return despd_challan_fin_year
        End Get
        Set(ByVal value As String)
            despd_challan_fin_year = value
        End Set
    End Property
    Public Property ChallanNo() As Integer
        Get
            Return despd_challan_no
        End Get
        Set(ByVal value As Integer)
            despd_challan_no = value
        End Set
    End Property
    Public Property ChallanDate() As DateTime
        Get
            Return despd_challan_date
        End Get
        Set(ByVal value As DateTime)
            despd_challan_date = value
        End Set
    End Property
    Public Property Srl() As Integer
        Get
            Return despd_srl
        End Get
        Set(ByVal value As Integer)
            despd_srl = value
        End Set
    End Property
    Public Property SkuCode() As String
        Get
            Return despd_sku_code
        End Get
        Set(ByVal value As String)
            despd_sku_code = value
        End Set
    End Property
    Public Property SkuUom() As String
        Get
            Return despd_sku_uom
        End Get
        Set(ByVal value As String)
            despd_sku_uom = value
        End Set
    End Property
    Public Property DespNop() As Integer
        Get
            Return despd_desp_nop
        End Get
        Set(ByVal value As Integer)
            despd_desp_nop = value
        End Set
    End Property
    Public Property SkuVol() As Decimal
        Get
            Return despd_sku_vol
        End Get
        Set(ByVal value As Decimal)
            despd_sku_vol = value
        End Set
    End Property
    Public Property AutoIndent() As Integer
        Get
            Return despd_auto_indent
        End Get
        Set(ByVal value As Integer)
            despd_auto_indent = value
        End Set
    End Property
    Public Property DepotIndent() As Integer
        Get
            Return despd_depot_indent
        End Get
        Set(ByVal value As Integer)
            despd_depot_indent = value
        End Set
    End Property
    Public Property IndentTotal() As Integer
        Get
            Return despd_indent_total
        End Get
        Set(ByVal value As Integer)
            despd_indent_total = value
        End Set
    End Property
    Public Property DespatchToDate() As Integer
        Get
            Return despd_despatch_to_date
        End Get
        Set(ByVal value As Integer)
            despd_despatch_to_date = value
        End Set
    End Property
    Public Property PendingLoad() As Integer
        Get
            Return despd_pending_load
        End Get
        Set(ByVal value As Integer)
            despd_pending_load = value
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
    Public Property ModifiedUser() As String
        Get
            Return modified_user
        End Get
        Set(ByVal value As String)
            modified_user = value
        End Set
    End Property
    Public Property ModifiedDate() As DateTime
        Get
            Return modified_date
        End Get
        Set(ByVal value As DateTime)
            modified_date = value
        End Set
    End Property
    Public Property DeletedUser() As String
        Get
            Return deleted_user
        End Get
        Set(ByVal value As String)
            deleted_user = value
        End Set
    End Property
    Public Property DeletedDate() As DateTime
        Get
            Return deleted_date
        End Get
        Set(ByVal value As DateTime)
            deleted_date = value
        End Set
    End Property
    Public Property ProcessMonth() As String
        Get
            Return despd_process_month
        End Get
        Set(ByVal value As String)
            despd_process_month = value
        End Set
    End Property
    Public Property ActiveStatus() As String
        Get
            Return Active
        End Get
        Set(ByVal value As String)
            active = value
        End Set
    End Property
    Public Property TransitTill() As DateTime
        Get
            Return despd_transit_till
        End Get
        Set(ByVal value As DateTime)
            despd_transit_till = value
        End Set
    End Property


    Public Property lot_no() As String
        Get
            Return despd_lot_no
        End Get
        Set(ByVal value As String)
            despd_lot_no = value
        End Set
    End Property
    Public Property LineNum() As Integer
        Get
            Return despd_line_num
        End Get
        Set(ByVal value As Integer)
            despd_line_num = value
        End Set
    End Property
    Public Property Po_Rate() As Decimal
        Get
            Return despd_po_rate
        End Get
        Set(ByVal value As Decimal)
            despd_po_rate = value
        End Set
    End Property
    Public Property Sku_Gst() As Decimal
        Get
            Return despd_sku_gst
        End Get
        Set(ByVal value As Decimal)
            despd_sku_gst = value
        End Set
    End Property

End Class

