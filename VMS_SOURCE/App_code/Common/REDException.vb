Imports System
Imports System.Runtime.Serialization
Imports Microsoft.ApplicationBlocks.ExceptionManagement

Namespace VMS.Common
    <Serializable()> Public Class AGROException
        Inherits BaseApplicationException

        Public Layer As String
        Public Method As String
        Public Timestamp As String
        'Default constructor
        Public Sub New()
            MyBase.New()
        End Sub
        'Constructor with exception message
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
        'Constructor with message and inner exception
        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub
        'Protected constructor to de-serialize data
        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub
    End Class
End Namespace
