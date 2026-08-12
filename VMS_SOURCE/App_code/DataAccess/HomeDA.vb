Imports VMS.Common
Imports System.Data.SqlClient
Imports System.Data

Namespace VMS.DataAccess
    Public Class HomeDA

        Private cmdHome As SqlCommand
        Private connLogin As SqlConnection
        Private daHome As SqlDataAdapter
        Dim homeDt As DataTable

        Public Sub New()
            Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader
            connLogin = New SqlConnection(configurationAppSettings.GetValue("ConStr", GetType(System.String)))
            'connLogin = New SqlConnection(System.Configuration.ConfigurationSettings.AppSettings.Get(0))
        End Sub

        'Public Function GetToDayClosedDA(ByVal strcomp As String) As DataTable

        '    homeDt = New DataTable
        '    cmdHome = New SqlCommand("AGRO_SP_GETTODAYCLOSED", connLogin)
        '    cmdHome.CommandType = CommandType.StoredProcedure
        '    cmdHome.Parameters.Add("@COMP", SqlDbType.VarChar, 20)
        '    cmdHome.Parameters("@COMP").Value = strcomp
        '    daHome = New SqlDataAdapter
        '    daHome.SelectCommand = cmdHome
        '    Try
        '        daHome.Fill(homeDt)
        '        Return homeDt

        '    Catch ex As Exception
        '        'Throw New EProcException("Error getting User Applicable Forms - Data Access", ex)
        '    Catch exG As Exception
        '        'Throw New EProcException("Error getting User Applicable Forms - Data Access", exG)
        '    Finally
        '        With daHome
        '            If Not .SelectCommand Is Nothing Then
        '                If Not .SelectCommand.Connection Is Nothing Then
        '                    .SelectCommand.Connection.Dispose()
        '                End If
        '                .SelectCommand.Dispose()
        '            End If
        '            .Dispose()
        '        End With
        '    End Try

        'End Function

        'Public Function GetToDayRegDA(ByVal strCmp As String) As DataTable

        '    homeDt = New DataTable
        '    cmdHome = New SqlCommand("AGRO_SP_GETTODAYREG", connLogin)
        '    cmdHome.CommandType = CommandType.StoredProcedure
        '    daHome = New SqlDataAdapter(cmdHome)
        '    cmdHome.Parameters.Add("@COMP", SqlDbType.VarChar, 20)
        '    cmdHome.Parameters("@COMP").Value = strCmp
        '    daHome.SelectCommand = cmdHome
        '    Try
        '        daHome.Fill(homeDt)
        '        Return homeDt

        '    Catch ex As Exception
        '        'Throw New EProcException("Error getting User Applicable Forms - Data Access", ex)
        '    Catch exG As Exception
        '        'Throw New EProcException("Error getting User Applicable Forms - Data Access", exG)
        '    Finally
        '        With daHome
        '            If Not .SelectCommand Is Nothing Then
        '                If Not .SelectCommand.Connection Is Nothing Then
        '                    .SelectCommand.Connection.Dispose()
        '                End If
        '                .SelectCommand.Dispose()
        '            End If
        '            .Dispose()
        '        End With
        '    End Try

        'End Function

        'Public Function GetToDayProjDA(ByVal strCmp As String) As DataTable

        '    homeDt = New DataTable
        '    cmdHome = New SqlCommand("AGRO_SP_GETTODAYPROJ", connLogin)
        '    cmdHome.CommandType = CommandType.StoredProcedure
        '    daHome = New SqlDataAdapter(cmdHome)
        '    cmdHome.Parameters.Add("@COMP", SqlDbType.VarChar, 20)
        '    cmdHome.Parameters("@COMP").Value = strCmp
        '    daHome.SelectCommand = cmdHome
        '    Try
        '        daHome.Fill(homeDt)
        '        Return homeDt

        '    Catch ex As Exception
        '        'Throw New EProcException("Error getting User Applicable Forms - Data Access", ex)
        '    Catch exG As Exception
        '        'Throw New EProcException("Error getting User Applicable Forms - Data Access", exG)
        '    Finally
        '        With daHome
        '            If Not .SelectCommand Is Nothing Then
        '                If Not .SelectCommand.Connection Is Nothing Then
        '                    .SelectCommand.Connection.Dispose()
        '                End If
        '                .SelectCommand.Dispose()
        '            End If
        '            .Dispose()
        '        End With
        '    End Try

        'End Function
    End Class
End Namespace

