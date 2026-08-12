Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes

Public Class TokenGenerationEntity

    Private tg_srl_no As Integer
    Private tg_refsrl_no As Integer
    Private tg_barcode As String
    Private tg_type As String
    Private tg_product As String
    Private tg_pack As String
    Private tg_denomination As String
    Private tg_quantity As Integer
    Private tg_month As String
    Private tg_year As String
    Private created_user As String
    Private created_date As Date
    Private modified_user As String
    Private modified_date As Date
    Private active As String

    Private tg_factory_code As String
    Private tg_Vendor_code As String
    Private tg_Requisition_month As String
    Private tg_Requisition_year As String

    Public Sub New()

        tg_srl_no = Integer.MinValue
        tg_refsrl_no = Integer.MinValue
        tg_barcode = String.Empty
        tg_type = String.Empty
        tg_product = String.Empty
        tg_pack = String.Empty
        tg_denomination = String.Empty
        tg_quantity = Integer.MinValue
        tg_month = String.Empty
        tg_year = String.Empty
        created_user = String.Empty
        created_date = Date.MinValue
        modified_user = String.Empty
        modified_date = Date.MinValue
        active = String.Empty

        tg_factory_code = String.Empty
        tg_Vendor_code = String.Empty

        tg_Requisition_month = String.Empty
        tg_Requisition_year = String.Empty

    End Sub

    Public Property tgsrlno() As Integer
        Get
            Return tg_srl_no
        End Get
        Set(ByVal value As Integer)
            tg_srl_no = value
        End Set
    End Property

    Public Property tgrefsrlno() As Integer
        Get
            Return tg_refsrl_no
        End Get
        Set(ByVal value As Integer)
            tg_refsrl_no = value
        End Set
    End Property

    Public Property tgbarcode() As String
        Get
            Return tg_barcode
        End Get
        Set(ByVal value As String)
            tg_barcode = value
        End Set
    End Property

    Public Property tgtype() As String
        Get
            Return tg_type
        End Get
        Set(ByVal value As String)
            tg_type = value
        End Set
    End Property

    Public Property tgproduct() As String
        Get
            Return tg_product
        End Get
        Set(ByVal value As String)
            tg_product = value
        End Set
    End Property
    Public Property tgpack() As String
        Get
            Return tg_pack
        End Get
        Set(ByVal value As String)
            tg_pack = value
        End Set
    End Property

    Public Property tgdenomination() As String
        Get
            Return tg_denomination
        End Get
        Set(ByVal value As String)
            tg_denomination = value
        End Set
    End Property

    Public Property tgquantity() As String
        Get
            Return tg_quantity
        End Get
        Set(ByVal value As String)
            tg_quantity = value
        End Set
    End Property

    Public Property tgmonth() As String
        Get
            Return tg_month
        End Get
        Set(ByVal value As String)
            tg_month = value
        End Set
    End Property

    Public Property tgyear() As String
        Get
            Return tg_year
        End Get
        Set(ByVal value As String)
            tg_year = value
        End Set
    End Property

    Public Property createduser() As String
        Get
            Return created_user
        End Get
        Set(ByVal value As String)
            created_user = value
        End Set
    End Property

    Public Property createddate() As Date
        Get
            Return created_date
        End Get
        Set(ByVal value As Date)
            created_date = value
        End Set
    End Property

    Public Property modifieduser() As String
        Get
            Return modified_user
        End Get
        Set(ByVal value As String)
            modified_user = value
        End Set
    End Property

    Public Property modifieddate() As String
        Get
            Return modified_date
        End Get
        Set(ByVal value As String)
            modified_date = value
        End Set
    End Property

    Public Property tgactive() As String
        Get
            Return active
        End Get
        Set(ByVal value As String)
            active = value
        End Set
    End Property


    Public Property factory_code() As String
        Get
            Return tg_factory_code
        End Get
        Set(ByVal value As String)
            tg_factory_code = value
        End Set
    End Property

    Public Property Vendor_code() As String
        Get
            Return tg_Vendor_code
        End Get
        Set(ByVal value As String)
            tg_Vendor_code = value
        End Set
    End Property



    Public Property Requisition_month() As String
        Get
            Return tg_Requisition_month
        End Get
        Set(ByVal value As String)
            tg_Requisition_month = value
        End Set
    End Property

    Public Property Requisition_year() As String
        Get
            Return tg_Requisition_year
        End Get
        Set(ByVal value As String)
            tg_Requisition_year = value
        End Set
    End Property

End Class
