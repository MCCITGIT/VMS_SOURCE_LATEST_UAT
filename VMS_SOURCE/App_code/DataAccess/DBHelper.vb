'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/DataAccess/SqlHelper.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.00.00
'Description	: Code behind file for Database connection 

'Modified By       Modified On       Version         Reason

'**********************************************************


Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Collections.Generic
Imports System.Data.SqlClient

Namespace VMS.DataAccess

    Public MustInherit Class DBHelper
        Protected MustOverride Function GetConnectionString() As String
        Public MustOverride Function OpenConnection() As DbConnection
        Public MustOverride Function CloseConnection(ByRef connectionObject As DbConnection) As Boolean
        Public MustOverride Function ExecuteDataSet(ByVal commandText As String, ByVal commandType As CommandType, ByVal ParamArray parameters As IDbDataParameter()) As DataSet

        Public MustOverride Overloads Function ExecuteNonQuery(ByVal commandText As String, ByVal commandType As CommandType, ByVal ParamArray parameters As IDbDataParameter()) As Integer
        Public MustOverride Overloads Function ExecuteNonQuery(ByRef connectionObject As DbConnection, ByVal commandText As String, ByVal commandType As System.Data.CommandType, ByVal ParamArray parameters As System.Data.IDbDataParameter()) As Integer

        'Added by Rajesh Daniel on 11/10/2008
        Public MustOverride Overloads Function ExecuteNonQuery(ByVal connectionObject As SqlConnection, ByVal sqlTrans As SqlTransaction, ByVal commandText As String, ByVal commandType As CommandType, ByVal OutputParamter As Boolean, ByVal ParamArray parameters As IDbDataParameter()) As Integer

        'Public MustOverride Function ExecuteScalar(ByVal commandText As String, ByVal commandType As CommandType, ByVal ParamArray parameters As IDbDataParameter()) As Object

        'Public MustOverride Function ExecuteReader(ByVal commandText As String, ByVal commandType As CommandType, ByVal ParamArray parameters As IDbDataParameter()) As IDataReader
    End Class
End Namespace