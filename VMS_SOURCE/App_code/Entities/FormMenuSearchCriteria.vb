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
    Public Class FormMenuSearchCriteria
        Private Form_type As String
        Private page_Index As Integer
        Public Sub New()
            Form_type = String.Empty
            page_Index = Integer.MinValue
        End Sub

        Public Property FormType() As String
            'Gets and Sets Location
            Get
                Return Form_type
            End Get
            Set(ByVal value As String)
                Form_type = value
            End Set
        End Property

        Public Property PageIndex() As Integer
            'Gets and Sets Location
            Get
                Return page_Index
            End Get
            Set(ByVal value As Integer)
                page_Index = value
            End Set
        End Property
    End Class
End Namespace


