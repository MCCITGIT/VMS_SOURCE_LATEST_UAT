Imports Microsoft.VisualBasic

Public Class OCSPrmPrdEntity
    Private PrmPrdId As String
    Private ProductCode As String
    Private Params As String
    Private Frequency As String
    Private created_user As String
    Private modified_user As String
    Private deleted_user As String
    Private active As String
    Private NumericYN As String
    Private DropdownYN As String
    Private ProductType As String
    Private Result As String
    Private Id As Integer
    Private DropdownParam As String
    Private MinValue As String
    Private MaxValue As String
    Sub New()
        PrmPrdId = String.Empty
        ProductCode = String.Empty
        Params = String.Empty
        Frequency = String.Empty
        created_user = String.Empty
        modified_user = String.Empty
        deleted_user = String.Empty
        active = String.Empty
        NumericYN = String.Empty
        DropdownYN = String.Empty
        ProductType = String.Empty
        Result = String.Empty
        Id = Integer.MinValue
        DropdownParam = String.Empty
        MinValue = String.Empty
        MaxValue = String.Empty
    End Sub
    Public Property Min_Value() As String
        Get
            Return MinValue
        End Get
        Set(ByVal value As String)
            MinValue = value
        End Set
    End Property
    Public Property Max_Value() As String
        Get
            Return MaxValue
        End Get
        Set(ByVal value As String)
            MaxValue = value
        End Set
    End Property
    Public Property PrmPrd_Id() As String
        Get
            Return PrmPrdId
        End Get
        Set(ByVal value As String)
            PrmPrdId = value
        End Set
    End Property
    Public Property Numeric_YN() As String
        Get
            Return NumericYN
        End Get
        Set(ByVal value As String)
            NumericYN = value
        End Set
    End Property
    Public Property Dropdown_YN() As String
        Get
            Return DropdownYN
        End Get
        Set(ByVal value As String)
            DropdownYN = value
        End Set
    End Property
    Public Property Product_Code() As String
        Get
            Return ProductCode
        End Get
        Set(ByVal value As String)
            ProductCode = value
        End Set
    End Property
    Public Property Paramss() As String
        Get
            Return Params
        End Get
        Set(ByVal value As String)
            Params = value
        End Set
    End Property
    Public Property PFrequency() As String
        Get
            Return Frequency
        End Get
        Set(ByVal value As String)
            Frequency = value
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
    Public Property modifieduser() As String
        Get
            Return modified_user
        End Get
        Set(ByVal value As String)
            modified_user = value
        End Set
    End Property
    Public Property deleteduser() As String
        Get
            Return deleted_user
        End Get
        Set(ByVal value As String)
            deleted_user = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return active
        End Get
        Set(ByVal value As String)
            active = value
        End Set
    End Property
    Public Property Product_Type() As String
        Get
            Return ProductType
        End Get
        Set(ByVal value As String)
            ProductType = value
        End Set
    End Property
    Public Property ResultType() As String
        Get
            Return Result
        End Get
        Set(ByVal value As String)
            Result = value
        End Set
    End Property
    Public Property Auto_Id() As Integer
        Get
            Return Id
        End Get
        Set(ByVal value As Integer)
            Id = value
        End Set
    End Property
    Public Property DropDown_Param() As String
        Get
            Return DropdownParam
        End Get
        Set(ByVal value As String)
            DropdownParam = value
        End Set
    End Property
End Class
