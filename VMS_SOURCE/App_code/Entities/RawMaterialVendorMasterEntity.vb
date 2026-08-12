Imports Microsoft.VisualBasic

Public Class RawMaterialVendorMasterEntity
    Private vendor_code As String
    Private vendor_name As String
    Private gst_registration_number As String
    Private vendor_address As String
    Private contact_person As String
    Private mobile_number As String
    Private email_address As String
    Private created_user As String
    Private created_date As DateTime
    Private modified_user As String
    Private modified_date As DateTime
    Private deleted_user As String
    Private deleted_date As DateTime
    Private Tran_type As Integer
    Private active As String
    Private city As String
    Private state As String
    Private pincode As String

    Public Sub New()
        vendor_code = String.Empty
        vendor_name = String.Empty
        gst_registration_number = String.Empty
        vendor_address = String.Empty
        contact_person = String.Empty
        mobile_number = String.Empty
        email_address = String.Empty
        created_user = String.Empty
        created_date = DateTime.MinValue
        modified_user = String.Empty
        modified_date = DateTime.MinValue
        deleted_user = String.Empty
        deleted_date = DateTime.MinValue
        Tran_type = Integer.MinValue
        active = String.Empty
        city = String.Empty
        state = String.Empty
        pincode = String.Empty
    End Sub

    Public Property VendorCode() As String
        Get
            Return vendor_code
        End Get
        Set(ByVal value As String)
            vendor_code = value
        End Set
    End Property

    Public Property VendorName() As String
        Get
            Return vendor_name
        End Get
        Set(ByVal value As String)
            vendor_name = value
        End Set
    End Property

    Public Property GstRegistrationNumber() As String
        Get
            Return gst_registration_number
        End Get
        Set(ByVal value As String)
            gst_registration_number = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return vendor_address
        End Get
        Set(ByVal value As String)
            vendor_address = value
        End Set
    End Property

    Public Property ContactPerson() As String
        Get
            Return contact_person
        End Get
        Set(ByVal value As String)
            contact_person = value
        End Set
    End Property

    Public Property MobileNumber() As String
        Get
            Return mobile_number
        End Get
        Set(ByVal value As String)
            mobile_number = value
        End Set
    End Property

    Public Property EmailAddress() As String
        Get
            Return email_address
        End Get
        Set(ByVal value As String)
            email_address = value
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

    Public Property Trantype() As Integer
        Get
            Return Tran_type
        End Get
        Set(ByVal value As Integer)
            Tran_type = value
        End Set
    End Property

    Public Property ActiveStatus() As String
        Get
            Return active
        End Get
        Set(ByVal value As String)
            active = value
        End Set
    End Property

    Public Property Vendor_City() As String
        Get
            Return city
        End Get
        Set(ByVal value As String)
            city = value
        End Set
    End Property
    Public Property Vendor_State() As String
        Get
            Return state
        End Get
        Set(ByVal value As String)
            state = value
        End Set
    End Property
    Public Property Vendor_PinCode() As String
        Get
            Return pincode
        End Get
        Set(ByVal value As String)
            pincode = value
        End Set
    End Property
End Class
