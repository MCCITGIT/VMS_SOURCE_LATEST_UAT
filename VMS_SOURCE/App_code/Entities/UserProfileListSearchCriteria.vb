Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes
Namespace VMS.Web
    Public Class UserProfileListSearchCriteria
        Private UPBranch As String
        Private UPDepartment As String
        Private UPUserGroup As String
        Private UPPagination As Integer
        Private UPUserName As String
        Public Sub New()
            UPBranch = String.Empty
            UPDepartment = String.Empty
            UPUserGroup = String.Empty
            UPUserName = String.Empty
            UPPagination = Integer.MinValue
        End Sub
        Public Property UserBranch() As String
            Get
                Return UPBranch
            End Get
            Set(ByVal value As String)
                UPBranch = value
            End Set
        End Property
        Public Property UserDepartment() As String
            Get
                Return UPDepartment
            End Get
            Set(ByVal value As String)
                UPDepartment = value
            End Set
        End Property
        Public Property UserUserName() As String
            Get
                Return UPUserName
            End Get
            Set(ByVal value As String)
                UPUserName = value
            End Set
        End Property
        Public Property UserUserGroup() As String
            Get
                Return UPUserGroup
            End Get
            Set(ByVal value As String)
                UPUserGroup = value
            End Set
        End Property
        Public Property UserPagination() As Integer
            Get
                Return UPPagination
            End Get
            Set(ByVal value As Integer)
                UPPagination = value
            End Set
        End Property
    End Class
End Namespace