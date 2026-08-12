'**************************************************
'Copyright	    : BSTRACKER, MCC, Kolkata
'Source	        : DealerDespatchDtlEntity.vb
'Created Date	: 14 Nov 2010
'Created By	    : Neeraj
'Version	    : 1.00.00
'Description	: Code Entity file for DealerDespatchDtl table

'Modified By       Modified On       Version         Reason

'*************************************************************


Imports Microsoft.VisualBasic
Imports System.data.SqlTypes
Namespace VMS.Web
    Public Class LovDetailsSearchCriteria
        Private Lov_type As String
        Public Sub New()
            Lov_type = String.Empty

        End Sub

        Public Property LovType() As String
            'Gets and Sets Location
            Get
                Return Lov_type
            End Get
            Set(ByVal value As String)
                Lov_type = value
            End Set
        End Property
    End Class
End Namespace

