Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes

Public Class DespatchHeaderEntity
    Private desph_desp_unit As String
    Private desph_desp_depot As String
    Private desph_challan_fin_year As String
    Private desph_challan_no As Integer
    Private desph_challan_date As SqlDateTime
    Private desph_total_ltr As Decimal
    Private desph_total_kg As Decimal
    Private desph_transporter_name As String
    Private desph_truck_no As String
    Private desph_excise_gp_no As String
    Private desph_excise_gp_dt As SqlDateTime
    Private desph_approved_yn As String
    Private desph_approved_date As SqlDateTime
    Private created_user As String
    Private created_date As DateTime
    Private modified_user As String
    Private modified_date As DateTime
    Private deleted_user As String
    Private deleted_date As DateTime
    Private active As String
    Private desph_process_month As String
    Private desph_road_permit_no As String
    Private desph_po_no As String
    'Private desph_lot_no As String
    Private desph_site_name As String
    Private desph_delivery_depot As String
    Private desph_site_id As Long
    Private desph_transpoter_id As Int32
    Private desph_invoice_value As Decimal

    Private desph_eway_bill_no As String
    Private desph_eway_bill_dt As SqlDateTime
    Private desph_valid_upto_dt As SqlDateTime
    'Modified-by MUKESH BHAGAT on 20-08-2026 : restored Indent feature from old UAT source
    Private desph_third_party_indent_yn As String
    Private desph_third_party_indent As String

    Public Sub New()
        desph_desp_unit = String.Empty
        desph_desp_depot = String.Empty
        desph_challan_fin_year = String.Empty
        desph_challan_no = Integer.MinValue
        desph_challan_date = SqlDateTime.MinValue
        desph_total_ltr = Decimal.MinValue
        desph_total_kg = Decimal.MinValue
        desph_transporter_name = String.Empty
        desph_truck_no = String.Empty
        desph_excise_gp_no = String.Empty
        desph_excise_gp_dt = SqlDateTime.MinValue
        desph_approved_yn = String.Empty
        desph_approved_date = SqlDateTime.MinValue
        created_user = String.Empty
        created_date = DateTime.MinValue
        modified_user = String.Empty
        modified_date = DateTime.MinValue
        deleted_user = String.Empty
        deleted_date = DateTime.MinValue
        active = String.Empty
        desph_process_month = String.Empty
        desph_road_permit_no = String.Empty

        desph_po_no = String.Empty
        'desph_lot_no = String.Empty
        desph_site_name = String.Empty
        desph_delivery_depot = String.Empty
        desph_site_id = Integer.MinValue
        desph_transpoter_id = Integer.MinValue
        desph_invoice_value = Decimal.MinValue

        desph_eway_bill_no = String.Empty
        desph_eway_bill_dt = SqlDateTime.MinValue
        desph_valid_upto_dt = SqlDateTime.MinValue
        'Modified-by MUKESH BHAGAT on 20-08-2026 : restored Indent feature from old UAT source
        desph_third_party_indent = String.Empty
        desph_third_party_indent_yn = String.Empty

    End Sub
    Public Property DespUnit() As String
        Get
            Return desph_desp_unit
        End Get
        Set(ByVal value As String)
            desph_desp_unit = value
        End Set
    End Property
    Public Property DespDepot() As String
        Get
            Return desph_desp_depot
        End Get
        Set(ByVal value As String)
            desph_desp_depot = value
        End Set
    End Property
    Public Property ChallanFinYear() As String
        Get
            Return desph_challan_fin_year
        End Get
        Set(ByVal value As String)
            desph_challan_fin_year = value
        End Set
    End Property
    Public Property ChallanNo() As Integer
        Get
            Return desph_challan_no
        End Get
        Set(ByVal value As Integer)
            desph_challan_no = value
        End Set
    End Property
    Public Property ChallanDate() As SqlDateTime
        Get
            Return desph_challan_date
        End Get
        Set(ByVal value As SqlDateTime)
            desph_challan_date = value
        End Set
    End Property
    Public Property TotalLtr() As Decimal
        Get
            Return desph_total_ltr
        End Get
        Set(ByVal value As Decimal)
            desph_total_ltr = value
        End Set
    End Property
    Public Property TotalKg() As Decimal
        Get
            Return desph_total_kg
        End Get
        Set(ByVal value As Decimal)
            desph_total_kg = value
        End Set
    End Property
    Public Property TransporterName() As String
        Get
            Return desph_transporter_name
        End Get
        Set(ByVal value As String)
            desph_transporter_name = value
        End Set
    End Property
    Public Property TruckNo() As String
        Get
            Return desph_truck_no
        End Get
        Set(ByVal value As String)
            desph_truck_no = value
        End Set
    End Property
    Public Property ExciseGpNo() As String
        Get
            Return desph_excise_gp_no
        End Get
        Set(ByVal value As String)
            desph_excise_gp_no = value
        End Set
    End Property
    Public Property ExciseGpDt() As SqlDateTime
        Get
            Return desph_excise_gp_dt
        End Get
        Set(ByVal value As SqlDateTime)
            desph_excise_gp_dt = value
        End Set
    End Property
    Public Property ApprovedYn() As String
        Get
            Return desph_approved_yn
        End Get
        Set(ByVal value As String)
            desph_approved_yn = value
        End Set
    End Property
    Public Property ApprovedDate() As DateTime
        Get
            Return desph_approved_date
        End Get
        Set(ByVal value As DateTime)
            desph_approved_date = value
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
            Return desph_process_month
        End Get
        Set(ByVal value As String)
            desph_process_month = value
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
    Public Property RoadPermitNo() As String
        Get
            Return desph_road_permit_no
        End Get
        Set(ByVal value As String)
            desph_road_permit_no = value
        End Set
    End Property

    Public Property po_no() As String
        Get
            Return desph_po_no
        End Get
        Set(ByVal value As String)
            desph_po_no = value
        End Set
    End Property

    'Public Property lot_no() As String
    '    Get
    '        Return desph_lot_no
    '    End Get
    '    Set(ByVal value As String)
    '        desph_lot_no = value
    '    End Set
    'End Property

    Public Property site_name() As String
        Get
            Return desph_site_name
        End Get
        Set(ByVal value As String)
            desph_site_name = value
        End Set
    End Property

    Public Property delivery_depot() As String
        Get
            Return desph_delivery_depot
        End Get
        Set(ByVal value As String)
            desph_delivery_depot = value
        End Set
    End Property
    Public Property SiteId() As Long
        Get
            Return desph_site_id
        End Get
        Set(ByVal value As Long)
            desph_site_id = value
        End Set
    End Property
    Public Property TranspoterId() As Int32
        Get
            Return desph_transpoter_id
        End Get
        Set(ByVal value As Int32)
            desph_transpoter_id = value
        End Set
    End Property
    Public Property InvoiceValue() As Decimal
        Get
            Return desph_invoice_value
        End Get
        Set(ByVal value As Decimal)
            desph_invoice_value = value
        End Set
    End Property

    Public Property EWayBillNo() As String
        Get
            Return desph_eway_bill_no
        End Get
        Set(ByVal value As String)
            desph_eway_bill_no = value
        End Set
    End Property

    Public Property EwayBillDt() As SqlDateTime
        Get
            Return desph_eway_bill_dt
        End Get
        Set(ByVal value As SqlDateTime)
            desph_eway_bill_dt = value
        End Set
    End Property

    Public Property ValidUptoDt() As SqlDateTime
        Get
            Return desph_valid_upto_dt
        End Get
        Set(ByVal value As SqlDateTime)
            desph_valid_upto_dt = value
        End Set
    End Property

    'Modified-by MUKESH BHAGAT on 20-08-2026 : restored Indent feature from old UAT source
    Public Property ThirdPartyIndentYn() As String
        Get
            Return desph_third_party_indent_yn
        End Get
        Set(ByVal value As String)
            desph_third_party_indent_yn = value
        End Set
    End Property

    Public Property ThirdPartyIndent() As String
        Get
            Return desph_third_party_indent
        End Get
        Set(ByVal value As String)
            desph_third_party_indent = value
        End Set
    End Property
End Class
