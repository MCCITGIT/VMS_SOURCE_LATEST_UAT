'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/Entities/DocUpload_Entity.vb
'Created Date	: 31-December-2011
'Created By	    : Debayan Biswas
'Version	    : R01.00.00
'Description	: Code behind file for DocUpload_Entity Class

'Modified By       Modified On       Version         Reason

'*************************************************************


Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes

Namespace VMS.Web
    Public Class DocUpload_Entity
        Private sdocs_from_depot As String
        Private sdocs_to_depot As String
        Private sdocs_gen_id As Integer
        Private sdocs_doc_catg As String
        Private sdocs_doc_title As String
        Private sdocs_doc_no As String
        Private sdocs_doc_date As DateTime
        Private sdocs_remarks As String
        Private sdocs_file_name As String
        Private sdocs_fin_year As String
        Private created_user As String
        Private created_date As DateTime
        Private modified_user As String
        Private modified_date As DateTime
        Private deleted_user As String
        Private deleted_date As DateTime
        Private active As String

        Public Sub New()
            sdocs_from_depot = String.Empty
            sdocs_to_depot = String.Empty
            sdocs_gen_id = Integer.MinValue
            sdocs_doc_catg = String.Empty
            sdocs_doc_title = String.Empty
            sdocs_doc_no = String.Empty
            sdocs_doc_date = DateTime.MinValue
            sdocs_remarks = String.Empty
            sdocs_file_name = String.Empty
            sdocs_fin_year = String.Empty
            created_user = String.Empty
            created_date = DateTime.MinValue
            modified_user = String.Empty
            modified_date = DateTime.MinValue
            deleted_user = String.Empty
            deleted_date = DateTime.MinValue
            active = String.Empty
        End Sub

        Public Property DocsFromDepot() As String
            Get
                Return sdocs_from_depot
            End Get
            Set(ByVal value As String)
                sdocs_from_depot = value
            End Set
        End Property
        Public Property DocsToDepot() As String
            Get
                Return sdocs_to_depot
            End Get
            Set(ByVal value As String)
                sdocs_to_depot = value
            End Set
        End Property
        Public Property DocsGenId() As Integer
            Get
                Return sdocs_gen_id
            End Get
            Set(ByVal value As Integer)
                sdocs_gen_id = value
            End Set
        End Property
        Public Property DocsDocCatg() As String
            Get
                Return sdocs_doc_catg
            End Get
            Set(ByVal value As String)
                sdocs_doc_catg = value
            End Set
        End Property
        Public Property DocsDocTitle() As String
            Get
                Return sdocs_doc_title
            End Get
            Set(ByVal value As String)
                sdocs_doc_title = value
            End Set
        End Property
        Public Property DocsDocNo() As String
            Get
                Return sdocs_doc_no
            End Get
            Set(ByVal value As String)
                sdocs_doc_no = value
            End Set
        End Property
        Public Property DocsDocDate() As DateTime
            Get
                Return sdocs_doc_date
            End Get
            Set(ByVal value As DateTime)
                sdocs_doc_date = value
            End Set
        End Property
        Public Property DocsRemarks() As String
            Get
                Return sdocs_remarks
            End Get
            Set(ByVal value As String)
                sdocs_remarks = value
            End Set
        End Property
        Public Property DocsFileName() As String
            Get
                Return sdocs_file_name
            End Get
            Set(ByVal value As String)
                sdocs_file_name = value
            End Set
        End Property
        Public Property DocsFinYear() As String
            Get
                Return sdocs_fin_year
            End Get
            Set(ByVal value As String)
                sdocs_fin_year = value
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
        Public Property ModifiedUser() As String
            Get
                Return modified_user
            End Get
            Set(ByVal value As String)
                modified_user = value
            End Set
        End Property
        Public Property ModifiedDate() As DateTime
            Get
                Return modified_date
            End Get
            Set(ByVal value As DateTime)
                modified_date = value
            End Set
        End Property
        Public Property DeletedUser() As String
            Get
                Return deleted_user
            End Get
            Set(ByVal value As String)
                deleted_user = value
            End Set
        End Property
        Public Property DeletedDate() As DateTime
            Get
                Return deleted_date
            End Get
            Set(ByVal value As DateTime)
                deleted_date = value
            End Set
        End Property
        Public Property DocActive() As String
            Get
                Return active
            End Get
            Set(ByVal value As String)
                active = value
            End Set
        End Property
    End Class
End Namespace

