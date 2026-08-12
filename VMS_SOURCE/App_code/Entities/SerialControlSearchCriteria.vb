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
    Public Class SerialControlSearchCriteria
        Private page_Index As Integer
        Private Fin_year As String
        Public Sub New()
            Fin_year = String.Empty
            page_Index = Integer.MinValue
        End Sub
        Public Property Finyear() As String

            Get
                Return Fin_year
            End Get
            Set(ByVal value As String)
                Fin_year = value
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


