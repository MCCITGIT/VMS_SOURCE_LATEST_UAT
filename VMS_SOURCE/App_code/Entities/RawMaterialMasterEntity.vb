Imports Microsoft.VisualBasic

Public Class RawMaterialMasterEntity
    Private rawmat_code As String
    Private created_user As String
    Private created_date As DateTime
    Private active As String
    Private Tran_type As Integer
    Public Sub New()
        rawmat_code = String.Empty
        created_user = String.Empty
        created_date = DateTime.MinValue
        active = String.Empty
        Tran_type = Integer.MinValue
    End Sub

    Public Property RawMatCode() As String
        Get
            Return rawmat_code
        End Get
        Set(ByVal value As String)
            rawmat_code = value
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

    Public Property ActiveStatus() As String
        Get
            Return active
        End Get
        Set(ByVal value As String)
            active = value
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
End Class
