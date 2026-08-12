'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/Entities/VMSUserEntity.vb
'Created Date	: 06-December-2007
'Created By	    : Saravanan
'Version	    : R01.00.00
'Description	: Code behind file for Red User Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Namespace VMS.Web

    Public Class VMSUserEntity

        Private userID As String
        Private userPWD As String
        Private userFirstName As String
        Private userLastName As String
        Private userGroupCode As String
        Private userEmail As String
        Private userDepartment As String
        Private userBranch As String
        Private userCompany As String
        Private userRegion As String
        Private userRptManager As String
        Private userStatus As String
        Private currentFinancialYear As String
        Private userUnit As String

        'Changes By Sumeet 26-02-2015 (Start)
        Private UserPasswordChangeDifference As Integer

        Public Sub New()

            userID = String.Empty
            userPWD = String.Empty
            userFirstName = String.Empty
            userLastName = String.Empty
            userGroupCode = String.Empty
            userEmail = String.Empty
            userDepartment = String.Empty
            userBranch = String.Empty
            userCompany = String.Empty
            userRegion = String.Empty
            userRptManager = String.Empty
            userStatus = String.Empty
            currentFinancialYear = String.Empty
            userUnit = String.Empty

            'Changes By Sumeet 26-02-2015 (Start)
            UserPasswordChangeDifference = Integer.MinValue
        End Sub

        'Login User ID
        Public Property userIDEntity() As String
            Get
                Return userID
            End Get
            Set(ByVal value As String)
                userID = value
            End Set
        End Property

        'Login User Password
        Public Property userPWDEntity() As String
            Get
                Return userPWD
            End Get
            Set(ByVal value As String)
                userPWD = value
            End Set
        End Property

        'Login User FirstName
        Public Property userFirstNameEntity() As String
            Get
                Return userFirstName
            End Get
            Set(ByVal value As String)
                userFirstName = value
            End Set
        End Property

        'Login User LastName
        Public Property userLastNameEntity() As String
            Get
                Return userLastName
            End Get
            Set(ByVal value As String)
                userLastName = value
            End Set
        End Property

        'Login User GroupCode
        Public Property userGroupCodeEntity() As String
            Get
                Return userGroupCode
            End Get
            Set(ByVal value As String)
                userGroupCode = value
            End Set
        End Property

        'Login User Email
        Public Property userEmailEntity() As String
            Get
                Return userEmail
            End Get
            Set(ByVal value As String)
                userEmail = value
            End Set
        End Property

        'Login User Department
        Public Property userDepartmentEntity() As String
            Get
                Return userDepartment
            End Get
            Set(ByVal value As String)
                userDepartment = value
            End Set
        End Property

        'Login User Branch
        Public Property userBranchEntity() As String
            Get
                Return userBranch
            End Get
            Set(ByVal value As String)
                userBranch = value
            End Set
        End Property

        'Login User Company
        Public Property userCompanyEntity() As String
            Get
                Return userCompany
            End Get
            Set(ByVal value As String)
                userCompany = value
            End Set
        End Property

        'Login User Team
        Public Property userRegionEntity() As String
            Get
                Return userRegion
            End Get
            Set(ByVal value As String)
                userRegion = value
            End Set
        End Property

        'Login User Reporting Manager
        Public Property userRptManagerEntity() As String
            Get
                Return userRptManager
            End Get
            Set(ByVal value As String)
                userRptManager = value
            End Set
        End Property

        'Login User Active Status
        Public Property userStatusEntity() As String
            Get
                Return userStatus
            End Get
            Set(ByVal value As String)
                userStatus = value
            End Set
        End Property

        'Current Financial Year
        Public Property currentFinancialYearEntity() As String
            Get
                Return currentFinancialYear
            End Get
            Set(ByVal value As String)
                currentFinancialYear = value
            End Set
        End Property

        'Login User Unit
        Public Property userUnitEntity() As String
            Get
                Return userUnit
            End Get
            Set(ByVal value As String)
                userUnit = value
            End Set
        End Property

        'Changes By Sumeet 26-02-2015 (Start)
        Public Property UserPasswordChangeDifferenceEntity() As Integer
            Get
                Return UserPasswordChangeDifference
            End Get
            Set(ByVal value As Integer)
                UserPasswordChangeDifference = value
            End Set
        End Property
    End Class

End Namespace
