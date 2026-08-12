Imports Microsoft.VisualBasic

Public Class TokenReceiveEntity
    Private trhVendorCode As String
    Private trhFactoryCode As String
    Private createdUser As String
    Private dtlTokenReceiveList As List(Of DtlTokenReceive)

    Public Sub New()
        trhVendorCode = String.Empty
        trhFactoryCode = String.Empty
        createdUser = String.Empty
        dtlTokenReceiveList = Nothing
    End Sub

    Public Property trh_vendor_code() As String
        Get
            Return trhVendorCode
        End Get
        Set(ByVal value As String)
            trhVendorCode = value
        End Set
    End Property
    Public Property trh_factory_code() As String
        Get
            Return trhFactoryCode
        End Get
        Set(ByVal value As String)
            trhFactoryCode = value
        End Set
    End Property
    Public Property created_user() As String
        Get
            Return createdUser
        End Get
        Set(ByVal value As String)
            createdUser = value
        End Set
    End Property
    Public Property dtlTokenReceive() As List(Of DtlTokenReceive)
        Get
            Return dtlTokenReceiveList
        End Get
        Set(ByVal value As List(Of DtlTokenReceive))
            dtlTokenReceiveList = value
        End Set
    End Property
End Class

Public Class DtlTokenReceive
    Private trdCartonId As Int32
    Private trdSessionId As Int32
    Private trdTokenMonth As String
    Private trdTokenYear As String
    Private trdQty As Int32
    Private trdReceiveQty As Int32

    Public Sub New()
        trdCartonId = Integer.MinValue
        trdSessionId = Integer.MinValue
        trdTokenMonth = String.Empty
        trdTokenYear = String.Empty
        trdQty = Integer.MinValue
        trdReceiveQty = Integer.MinValue
    End Sub

    Public Property trd_carton_id() As Int32
        Get
            Return trdCartonId
        End Get
        Set(ByVal value As Int32)
            trdCartonId = value
        End Set
    End Property
    Public Property trd_session_id() As Int32
        Get
            Return trdSessionId
        End Get
        Set(ByVal value As Int32)
            trdSessionId = value
        End Set
    End Property
    Public Property trd_token_month() As String
        Get
            Return trdTokenMonth
        End Get
        Set(ByVal value As String)
            trdTokenMonth = value
        End Set
    End Property
    Public Property trd_token_year() As String
        Get
            Return trdTokenYear
        End Get
        Set(ByVal value As String)
            trdTokenYear = value
        End Set
    End Property
    Public Property trd_qty() As Int32
        Get
            Return trdQty
        End Get
        Set(ByVal value As Int32)
            trdQty = value
        End Set
    End Property
    Public Property trd_receive_qty() As Int32
        Get
            Return trdReceiveQty
        End Get
        Set(ByVal value As Int32)
            trdReceiveQty = value
        End Set
    End Property
End Class
