'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/DataAccess/SqlHelper.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.00.00
'Description	: Code behind file for Database connection 

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports System
Imports System.Text
Imports System.Collections.Generic


Namespace VMS.DataAccess

    Public NotInheritable Class DBFactory

        Public Shared Function GetHelper() As DBHelper

            Dim lDBHelper As SqlHelper = New SqlHelper() 'SqlHelper to define lDBHelper
            Return lDBHelper
        End Function
    End Class
End Namespace