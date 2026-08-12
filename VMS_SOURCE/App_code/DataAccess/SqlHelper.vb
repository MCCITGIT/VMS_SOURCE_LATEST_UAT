'**********************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/DataAccess/SqlHelper.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.00.00
'Description	: Code behind file for Database connection 

'Modified By       Modified On       Version         Reason

'************************************************************

Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.SessionState

Namespace VMS.DataAccess

#Region "Function"
    'Function GetConnectionString gets the connection String of the Sim database
    'Function OpenConnection helps to open the connection with the database
    'Function CloseConnection helps to close the connection with the database
    'Function ExecuteDataSet helps to execute the stored procedure for update and retrieval operation
#End Region

    Public Class SqlHelper
        Inherits DBHelper
        Public Shared DatabaseException As Boolean
        Dim returnUrl As String = HttpContext.Current.Request.QueryString("ReturnUrl")
        Dim crConnectionInfo As New SqlConnectionStringBuilder
        Dim configurationAppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader
        Dim sqlEx As System.Exception

        'Gets the connection String of the Sim database
        Protected Overrides Function GetConnectionString() As String
            'Dim str As String = configurationAppSettings.GetValue("ConStr", GetType(System.String))
            Dim crConnectionInfo As New SqlConnectionStringBuilder
            With crConnectionInfo
                .DataSource = configurationAppSettings.GetValue("DBServerName", GetType(System.String))
                .InitialCatalog = configurationAppSettings.GetValue("DBName", GetType(System.String))
                .UserID = configurationAppSettings.GetValue("DBUserName", GetType(System.String))
                .Password = configurationAppSettings.GetValue("DBPassword", GetType(System.String))
                .ConnectTimeout = 30
            End With
            Return crConnectionInfo.ToString()
        End Function
        'Helps to open the connection with the database
        Public Overrides Function OpenConnection() As System.Data.Common.DbConnection
            Dim connectionObject As New SqlConnection()
            Try
                connectionObject.ConnectionString = GetConnectionString()
                connectionObject.Open()
            Catch ex As Exception
                'validateException()
            End Try
            Return connectionObject
        End Function
        'Helps to close the connection with the database
        Public Overrides Function CloseConnection(ByRef connectionObject As System.Data.Common.DbConnection) As Boolean
            Dim connectionClosed As Boolean
            Try
                If Not (connectionObject Is Nothing) Then
                    If (connectionObject.State <> ConnectionState.Broken AndAlso connectionObject.State <> ConnectionState.Closed) Then
                        connectionObject.Close()
                        connectionClosed = True
                    Else
                        connectionClosed = False
                    End If
                Else
                    connectionClosed = False
                End If
            Catch ex As Exception
                'validateException()
            End Try
            Return connectionClosed
        End Function
        'Helps to execute the stored procedure for update and retrieval operation

        Public Overloads Overrides Function ExecuteDataSet(ByVal commandText As String, ByVal commandType As System.Data.CommandType, ByVal ParamArray parameter As System.Data.IDbDataParameter()) As System.Data.DataSet
            Return ExecuteDataSet(commandText, commandType, 0, -1, parameter)
        End Function

        'Helps to execute the stored procedure for update and retrieval operation
        Public Overloads Function ExecuteDataSet(ByVal commandText As String, ByVal commandType As System.Data.CommandType, ByVal startRecordNum As Integer, ByVal maxRecordsToFetch As Integer, ByVal ParamArray parameters As System.Data.IDbDataParameter()) As DataSet
            Dim connectionObject As SqlConnection = Nothing
            Dim commandObject As SqlCommand
            Dim adapterObject As SqlDataAdapter
            Dim resultSet As DataSet = Nothing
            ' Try
            connectionObject = New SqlConnection
            commandObject = New SqlCommand
            connectionObject.ConnectionString = GetConnectionString()
            commandObject.Connection = connectionObject
            commandObject.CommandText = commandText
            commandObject.CommandType = commandType
            commandObject.CommandTimeout = 0
            commandObject.Parameters.AddRange(parameters)
            Try
                connectionObject.Open()
            Catch sqlEx As Exception

                returnUrl = "~/ExceptionPage.aspx"
                HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.DatabaseConnectionError
                HttpContext.Current.Server.Transfer(returnUrl)

            End Try

            adapterObject = New SqlDataAdapter
            adapterObject.SelectCommand = commandObject
            resultSet = New DataSet

            Try
                If Not (maxRecordsToFetch = -1) Then
                    adapterObject.Fill(resultSet, startRecordNum, maxRecordsToFetch, "Table1")
                Else
                    adapterObject.Fill(resultSet)
                End If
            Catch sqlEx As Exception
                DatabaseException = True
                Dim Str As String
                'Str assigns the first 22 characters of the sqlEx 
                Str = Mid(sqlEx.Message.ToString, 1, 23)
                'If str equals the transport level error message string then the condition validates 
                If Str = Constant.ExpModules.TransportError Then
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.DatabaseConnectionBreak
                    HttpContext.Current.Server.Transfer(returnUrl)
                Else
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    HttpContext.Current.Server.Transfer(returnUrl)
                End If
            Finally
                If Not (connectionObject Is Nothing) AndAlso (Not (connectionObject.State = ConnectionState.Broken) OrElse Not (connectionObject.State = ConnectionState.Closed)) Then
                    connectionObject.Close()
                End If
            End Try
            Return resultSet
        End Function

        'Helps to execute the stored procedure for insert operation
        Public Overloads Overrides Function ExecuteNonQuery(ByVal commandText As String, ByVal commandType As System.Data.CommandType, ByVal ParamArray parameters As System.Data.IDbDataParameter()) As Integer
            Dim connectionObject As SqlConnection = Nothing
            Dim commandObject As SqlCommand
            Dim numRowsAffected As Integer = 0
            'Try
            connectionObject = New SqlConnection
            commandObject = New SqlCommand
            connectionObject.ConnectionString = GetConnectionString()
            commandObject.Connection = connectionObject
            commandObject.CommandText = commandText
            commandObject.CommandType = commandType
            commandObject.Parameters.AddRange(parameters)
            Try
                connectionObject.Open()
            Catch sqlEx As Exception
                DatabaseException = True
                Dim Str As String
                'Str assigns the first 22 characters of the sqlEx 
                Str = Mid(sqlEx.Message.ToString, 1, 23)
                'If str equals the transport level error message string then the condition validates 
                If Str = Constant.ExpModules.TransportError Then
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.DatabaseConnectionBreak
                    HttpContext.Current.Server.Transfer(returnUrl)
                Else
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    HttpContext.Current.Server.Transfer(returnUrl)
                End If
            End Try
            Try
                numRowsAffected = commandObject.ExecuteNonQuery
            Catch sqlEx As Exception
                DatabaseException = True
                Dim Str As String
                'Str assigns the first 22 characters of the sqlEx 
                Str = Mid(sqlEx.Message.ToString, 1, 23)
                'If str equals the transport level error message string then the condition validates 
                If Str = Constant.ExpModules.TransportError Then
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.DatabaseConnectionBreak
                    HttpContext.Current.Server.Transfer(returnUrl)
                Else
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    HttpContext.Current.Server.Transfer(returnUrl)
                End If
            Finally
                If Not (connectionObject Is Nothing) AndAlso (Not (connectionObject.State = ConnectionState.Broken) OrElse Not (connectionObject.State = ConnectionState.Closed)) Then
                    connectionObject.Close()
                End If
            End Try
            Return numRowsAffected
        End Function
        'Helps to execute the stored procedure for insert operation
        Public Overloads Overrides Function ExecuteNonQuery(ByRef connectionObject As Data.Common.DbConnection, ByVal commandText As String, ByVal commandType As System.Data.CommandType, ByVal ParamArray parameters As System.Data.IDbDataParameter()) As Integer

            Dim commandObject As SqlCommand
            Dim numRowsAffected As Integer = 0
            Try
                connectionObject = CType(connectionObject, SqlConnection)
                commandObject = New SqlCommand
                commandObject.Connection = connectionObject
                commandObject.CommandText = commandText
                commandObject.CommandType = commandType
                commandObject.Parameters.AddRange(parameters)

                numRowsAffected = commandObject.ExecuteNonQuery
            Catch lException As Exception

                DatabaseException = True
                Dim Str As String
                'Str assigns the first 22 characters of the sqlEx 
                Str = Mid(lException.Message.ToString, 1, 23)
                'If str equals the transport level error message string then the condition validates 
                If Str = Constant.ExpModules.TransportError Then
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.DatabaseConnectionBreak
                    HttpContext.Current.Server.Transfer(returnUrl)
                Else
                    returnUrl = "~/ExceptionPage.aspx"
                    HttpContext.Current.Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    HttpContext.Current.Server.Transfer(returnUrl)
                End If
            End Try
            Return numRowsAffected
        End Function


        'Created by Rajesh Daniel on 10/11/2008
        'Function to Insert/Update/Delete values in Database
        Public Overloads Overrides Function ExecuteNonQuery(ByVal connectionObject As SqlConnection, ByVal sqlTrans As SqlTransaction, ByVal commandText As String, ByVal commandType As CommandType, ByVal OutputParameter As Boolean, ByVal ParamArray parameters As IDbDataParameter()) As Integer
            Dim commandObject As New SqlCommand()
            Dim numRowsAffected As Integer = 0
            Try
                'SqlConnection connectionObject = new SqlConnection();


                'connectionObject.ConnectionString = GetConnectionString();
                commandObject.Connection = connectionObject
                commandObject.Transaction = sqlTrans
                commandObject.CommandText = commandText
                commandObject.CommandType = commandType
                commandObject.Parameters.AddRange(parameters)

                'connectionObject.Open();

                numRowsAffected = commandObject.ExecuteNonQuery()

                If (OutputParameter) Then
                    If numRowsAffected > 0 Then
                        If parameters(0) IsNot Nothing Then
                            numRowsAffected = Convert.ToInt32(parameters(0).Value)
                        Else
                            numRowsAffected = Int32.MinValue
                        End If
                    End If
                End If

            Catch ex As Exception
                'Logger.LogException(ex);
                Throw ex

            Finally
                'if (connectionObject != null && (connectionObject.State != ConnectionState.Broken || connectionObject.State != ConnectionState.Closed))
                ' connectionObject.Close();
            End Try

            Return numRowsAffected
        End Function
    End Class
End Namespace