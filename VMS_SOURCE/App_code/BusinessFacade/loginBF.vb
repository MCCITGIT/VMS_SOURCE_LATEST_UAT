Imports VMS.Common
Imports VMS.DataAccess
Imports System.Data

Namespace VMS.BusinessFacade
    Public Class loginBF
        Public Function chkValidUser(ByVal usrid As String, ByVal pwd As String) As String
            'Cerating the Object of DA
            Dim objDALogin As New LoginDA

            'Try
            'Intializing to the variable of type GetFinDeatils function returns
            Return objDALogin.chkLoginUser(usrid, pwd)
            'Catch ex As Exception
            ' Throw New EProcException("Error in B.Facade--Login", ex)
            'End Try

        End Function
        Public Function getUsrdetails(ByVal usrid As String) As DataSet
            Dim objDAlogin As New LoginDA
            Dim dsSource_BF As DataSet

            Try
                dsSource_BF = objDAlogin.getLoginDetails(usrid)
                Return dsSource_BF
            Catch ex As Exception
                'Throw New EProcException("Error getting User Details - Business Facade", ex)
            End Try

        End Function
    End Class

End Namespace
