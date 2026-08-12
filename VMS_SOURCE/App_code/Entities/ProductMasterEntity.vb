Imports Microsoft.VisualBasic

Public Class ProductMasterEntity
    Private ProductID As Integer
    Private ProductName As String
    Private created_user As String
    Private created_date As DateTime
    Private modified_user As String
    Private modified_date As DateTime
    Private deleted_user As String
    Private deleted_date As DateTime
    Private Tran_type As Integer
    Private active As String

    Public Sub New()
        ProductID = Integer.MinValue
        ProductName = String.Empty
        created_user = String.Empty
        created_date = DateTime.MinValue
        modified_user = String.Empty
        modified_date = DateTime.MinValue
        deleted_user = String.Empty
        deleted_date = DateTime.MinValue
        Tran_type = Integer.MinValue
        active = String.Empty
    End Sub

    Public Property PName() As String
        Get
            Return ProductName
        End Get
        Set(ByVal value As String)
            ProductName = value
        End Set
    End Property

    Public Property PID() As Integer
        Get
            Return ProductID
        End Get
        Set(ByVal value As Integer)
            ProductID = value
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

    Public Property Trantype() As String
        Get
            Return Tran_type
        End Get
        Set(ByVal value As String)
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
End Class
