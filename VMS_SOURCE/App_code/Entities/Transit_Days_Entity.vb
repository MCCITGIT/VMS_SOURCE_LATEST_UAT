'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Transit_Days_AddUpdate.aspx.vb
'Created Date	: 14-January-2012
'Created By	    : Debayan Biswas
'Version	    : R02.00.00
'Description	: Code behind for Transit_Days_AddUpdate.aspx Page

'Modified By       Modified On       Version         Reason

'****************************************************************


Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes

Namespace VMS.Web
    Public Class Transit_Days_Entity
        Private t_vendor_unit As String
        Private t_depot As String
        Private t_transit_days As Integer
        Private created_user As String
        Private created_date As DateTime

        Public Sub New()
            t_vendor_unit = String.Empty
            t_depot = String.Empty
            t_transit_days = Integer.MinValue
            created_user = String.Empty
            created_date = DateTime.MinValue
        End Sub

        Public Property vendor_unit() As String
            Get
                Return t_vendor_unit
            End Get
            Set(ByVal value As String)
                t_vendor_unit = value
            End Set
        End Property
        Public Property depot() As String
            Get
                Return t_depot
            End Get
            Set(ByVal value As String)
                t_depot = value
            End Set
        End Property
        Public Property transit_days() As Integer
            Get
                Return t_transit_days
            End Get
            Set(ByVal value As Integer)
                t_transit_days = value
            End Set
        End Property
        Public Property CreatedUser() As String
            Get
                Return created_user
            End Get
            Set(ByVal value As String)
                created_user = value
            End Set
        End Property
        Public Property CreatedDate() As DateTime
            Get
                Return created_date
            End Get
            Set(ByVal value As DateTime)
                created_date = value
            End Set
        End Property
    End Class
End Namespace

