Imports Microsoft.VisualBasic

Namespace VMS.Web

    Public Class WeekMasterEntity

        Private company As String
        Private finYear As Integer
        Private finWeek As Integer
        Private hdnfinWeek As Integer
        Private weekStartDate As Date
        Private weekEndDate As Date
        Private monthNo As Integer
        Private createdUser As String
        Private modifiedUser As String
        Private deleteUser As String
        Private active As String

        Public Sub New()
            'Initializes all the property procedure values of Week Master
            company = String.Empty
            finYear = Integer.MinValue
            finWeek = Integer.MinValue
            hdnfinWeek = Integer.MinValue
            weekStartDate = Date.MinValue
            weekEndDate = Date.MinValue
            monthNo = Integer.MinValue
            createdUser = String.Empty
            modifiedUser = String.Empty
            deleteUser = String.Empty
            active = String.Empty
        End Sub

        Public Property PropertyCompany() As String
            'Gets and Sets Company
            Get
                Return company
            End Get
            Set(ByVal value As String)
                company = value
            End Set
        End Property

        Public Property PropertyFinYear() As Integer
            'Gets and Sets Finanical Year
            Get
                Return finYear
            End Get
            Set(ByVal value As Integer)
                finYear = value
            End Set
        End Property

        Public Property PropertyFinWeek() As Integer
            'Gets and Sets Finanical Week
            Get
                Return finWeek
            End Get
            Set(ByVal value As Integer)
                finWeek = value
            End Set
        End Property

        Public Property PropertyHdnFinWeek() As Integer
            'Gets and Sets Finanical Week
            Get
                Return hdnfinWeek
            End Get
            Set(ByVal value As Integer)
                hdnfinWeek = value
            End Set
        End Property

        Public Property PropertyWeekStartDate() As Date
            'Gets and Sets Week Start Date
            Get
                Return weekStartDate
            End Get
            Set(ByVal value As Date)
                weekStartDate = value
            End Set
        End Property

        Public Property PropertyWeekEndDate() As Date
            'Gets and Sets Week End Date
            Get
                Return weekEndDate
            End Get
            Set(ByVal value As Date)
                weekEndDate = value
            End Set
        End Property

        Public Property PropertyMonthNo() As Integer
            'Gets and Sets Month No
            Get
                Return monthNo
            End Get
            Set(ByVal value As Integer)
                monthNo = value
            End Set
        End Property

        Public Property PropertyCreatedUser() As String
            'Gets and Sets Created User
            Get
                Return createdUser
            End Get
            Set(ByVal value As String)
                createdUser = value
            End Set
        End Property

        Public Property PropertyModifiedUser() As String
            'Gets and Sets Modified User
            Get
                Return modifiedUser
            End Get
            Set(ByVal value As String)
                modifiedUser = value
            End Set
        End Property

        Public Property PropertyActiveStatus() As String
            'Gets and Sets Active Status
            Get
                Return active
            End Get
            Set(ByVal value As String)
                active = value
            End Set
        End Property

    End Class

End Namespace

