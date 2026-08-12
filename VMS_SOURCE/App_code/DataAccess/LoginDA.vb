Imports VMS.Common
Imports System.Data.SqlClient
Imports System.Data

Namespace VMS.DataAccess
    Public Class LoginDA
        Private cmdLogin As SqlCommand
        Private connLogin As SqlConnection
        Private daLogin As SqlDataAdapter
        Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader

        Public Sub New()
            daLogin = New SqlDataAdapter
            'connLogin = New SqlConnection(System.Configuration.ConfigurationSettings.AppSettings.Get("ConStr"))
            Dim str As String = configurationAppSettings.GetValue("ConStr", GetType(System.String))
            connLogin = New SqlConnection(str)
        End Sub
        Public Function chkLoginUser(ByVal prmUid As String, ByVal prmPwd As String) As String
            Dim dtLogin As New DataSet
            Try
                cmdLogin = New SqlCommand("AGRO_Sp_chkLoginUser", connLogin)
                cmdLogin.CommandType = CommandType.StoredProcedure
                cmdLogin.Parameters.Add(New SqlParameter("@prmUid", SqlDbType.VarChar, 50, ParameterDirection.Input))
                cmdLogin.Parameters.Add(New SqlParameter("@prmPwd", SqlDbType.VarChar, 15, ParameterDirection.Input))
                'cmdLogin.Parameters.Add(New SqlParameter("@prmValid", SqlDbType.VarChar, 20, ParameterDirection.Output))
                'Dim prmValid = New SqlParameter
                'prmValid.SqlDbType = SqlDbType.VarChar
                'prmValid.Size = 15
                'prmValid.Direction = ParameterDirection.Output
                'prmValid.ParameterName = "@prmValid"
                'cmdLogin.Parameters.Add(prmValid)

                cmdLogin.Parameters("@prmUid").Value = prmUid
                cmdLogin.Parameters("@prmPwd").Value = prmPwd

                daLogin.SelectCommand = cmdLogin
                connLogin.Open()
                daLogin.Fill(dtLogin)
                'cmdLogin.ExecuteNonQuery()
                connLogin.Close()

                Return dtLogin.Tables(0).Rows(0)("prmValid")

                'Return cmdLogin.Parameters("@prmValid").Value
            Catch exG As Exception
                'Throw New OTBSException("Error in D.Access---chkLoginUser", exG)
            Finally
                If Not cmdLogin Is Nothing Then
                    If Not cmdLogin.Connection Is Nothing Then
                        cmdLogin.Connection.Dispose()
                    End If
                    cmdLogin.Dispose()
                End If
            End Try
        End Function
        Public Function getLoginDetails(ByVal prmUid As String) As DataSet
            'Dim dtLogin As New loginData
            Dim dtLogin As New DataSet
            Try
                ' daLogin.TableMappings.Add("Table", dtLogin.PMS_USER_PROFILE_TABLE)
                'daLogin.TableMappings.Add("Table1", dtLogin.PMS_USER_GROUP_TABLE)
                'daLogin.TableMappings.Add("Table2", dtLogin.PMS_PMS_FIN_YEAR_TABLE)

                cmdLogin = New SqlCommand("AGRO_Sp_GetLoginDetails", connLogin)
                cmdLogin.CommandType = CommandType.StoredProcedure
                cmdLogin.Parameters.Add(New SqlParameter("@prmUid", SqlDbType.VarChar, 50))

                cmdLogin.Parameters("@prmUid").Value = prmUid
                daLogin.SelectCommand = cmdLogin
                connLogin.Open()
                daLogin.Fill(dtLogin)
                connLogin.Close()
                Return dtLogin

            Catch exG As Exception
                ' Throw New EProcException("Error in D.Access---getLoginDetails", exG)
            Finally
                With daLogin
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

