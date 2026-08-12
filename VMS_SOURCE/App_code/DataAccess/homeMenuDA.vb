Imports System.Data.SqlClient
Imports System.Data

Namespace VMS.DataAccess

    Public Class homeMenuDA
        Private cmdhMenu As SqlCommand
        Private connhMenu As SqlConnection
        Private dahMenu As SqlDataAdapter

        Public Sub New()
            dahMenu = New SqlDataAdapter
            Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader
            connhMenu = New SqlConnection(configurationAppSettings.GetValue("ConStr", GetType(System.String)))
            'connhMenu = New SqlConnection(System.Configuration.ConfigurationSettings.AppSettings.Get(0))
        End Sub
        Public Function getAccessForms(ByVal prmgrpid As String, ByVal prmuserid As String) As DataSet
            Dim objDS As New DataSet

            ' dahMenu.TableMappings.Add("Table", "FORM_MSTR")
            cmdhMenu = New SqlCommand("AGRO_sp_GetAccessForms", connhMenu)
            cmdhMenu.CommandType = CommandType.StoredProcedure
            cmdhMenu.Parameters.Add("@prmgrpid", SqlDbType.VarChar, 15)
            cmdhMenu.Parameters("@prmgrpid").Value = prmgrpid
            cmdhMenu.Parameters.Add("@prmuserid", SqlDbType.VarChar, 50)
            cmdhMenu.Parameters("@prmuserid").Value = prmuserid
            dahMenu.SelectCommand = cmdhMenu

            Try
                dahMenu.Fill(objDS)
                Return objDS
            Catch ex As Exception
                'Throw New EProcException("Error getting User Applicable Forms - Data Access", ex)
                'Catch exG As Exception
                'Throw New EProcException("Error getting User Applicable Forms - Data Access", exG)
            Finally
                With dahMenu
                    If Not .SelectCommand Is Nothing Then
                        If Not .SelectCommand.Connection Is Nothing Then
                            .SelectCommand.Connection.Dispose()
                        End If
                        .SelectCommand.Dispose()
                    End If
                    .Dispose()
                End With
            End Try
        End Function

    End Class
End Namespace

