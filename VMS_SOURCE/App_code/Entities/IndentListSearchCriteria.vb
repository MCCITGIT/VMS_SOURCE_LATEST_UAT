Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes

Namespace VMS.Web

    Public Class IndentListSearchCriteria

        Private ilRegion As String
        Private ilDepot As String
        Private ilStatus As String

        Public Sub New()
            ilRegion = String.Empty
            ilDepot = String.Empty
            ilStatus = String.Empty
        End Sub

        Public Property IndentRegion() As String
            Get
                Return ilRegion
            End Get
            Set(ByVal value As String)
                ilRegion = value
            End Set
        End Property
        Public Property IndentDepot() As String
            Get
                Return ilDepot
            End Get
            Set(ByVal value As String)
                ilDepot = value
            End Set
        End Property
        Public Property IndentStatus() As String
            Get
                Return ilStatus
            End Get
            Set(ByVal value As String)
                ilStatus = value
            End Set
        End Property

    End Class

End Namespace

