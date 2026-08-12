Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess
Namespace VMS.Web
    Public Class OCSpecificationEntity
        Private Id As Integer
        Private VendorCode As String
        Private ProductCode As String
        Private BatchNo As String
        Private BatchDate As SqlDateTime
        Private Specifications As String
        Private SpecificationsValue As String
        Private created_user As String
        Private created_date As Date
        Private modified_user As String
        Private modified_date As Date
        Private deleted_user As String
        Private deleted_date As Date
        Private active As String
        Private ProductType As String
        Private ConfirmYN As String
        Private ConfirmedBy As String
        Private VenderName As String
        Public Sub New()
            Id = Integer.MinValue
            VendorCode = String.Empty
            ProductCode = String.Empty
            BatchNo = String.Empty
            BatchDate = SqlDateTime.Null
            Specifications = String.Empty
            SpecificationsValue = String.Empty

            created_user = String.Empty
            created_date = Date.MinValue
            modified_user = String.Empty
            modified_date = Date.MinValue
            deleted_user = String.Empty
            deleted_date = Date.MinValue
            active = String.Empty
            ProductType = String.Empty
            ConfirmYN = String.Empty
            ConfirmedBy = String.Empty
            VenderName = String.Empty
        End Sub
        Public Property Vendor_Code() As String
            Get
                Return VendorCode
            End Get
            Set(value As String)
                VendorCode = value
            End Set
        End Property
        Public Property Product_Code() As String
            Get
                Return ProductCode
            End Get
            Set(value As String)
                ProductCode = value
            End Set
        End Property
        Public Property Batch_No() As String
            Get
                Return BatchNo
            End Get
            Set(value As String)
                BatchNo = value
            End Set
        End Property
        Public Property Batch_Date() As SqlDateTime
            Get
                Return BatchDate
            End Get
            Set(ByVal value As SqlDateTime)
                BatchDate = value
            End Set
        End Property
        Public Property SpecificationsDtls() As String
            Get
                Return SpecificationsDtls
            End Get
            Set(ByVal value As String)
                SpecificationsDtls = value
            End Set
        End Property
        Public Property Specifications_Value() As String
            Get
                Return SpecificationsValue
            End Get
            Set(ByVal value As String)
                SpecificationsValue = value
            End Set
        End Property
        Public Property createduser() As String
            Get
                Return created_user
            End Get
            Set(ByVal value As String)
                created_user = value
            End Set
        End Property
        Public Property modifieduser() As String
            Get
                Return modifieduser
            End Get
            Set(ByVal value As String)
                modifieduser = value
            End Set
        End Property
        Public Property deleteduser() As String
            Get
                Return deleteduser
            End Get
            Set(ByVal value As String)
                deleteduser = value
            End Set
        End Property
        Public Property createddate() As Date
            Get
                Return createddate
            End Get
            Set(ByVal value As Date)
                createddate = value
            End Set
        End Property
        Public Property modifieddate() As Date
            Get
                Return modifieddate
            End Get
            Set(ByVal value As Date)
                modifieddate = value
            End Set
        End Property
        Public Property activestatus() As String
            Get
                Return active
            End Get
            Set(ByVal value As String)
                active = value
            End Set
        End Property
        Public Property Auto_Id() As Integer
            Get
                Return Id
            End Get
            Set(ByVal value As Integer)
                Id = value
            End Set
        End Property
        Public Property Product_Type() As String
            Get
                Return ProductType
            End Get
            Set(ByVal value As String)
                ProductType = value
            End Set
        End Property
        Public Property confirm_YN() As String
            Get
                Return ConfirmYN
            End Get
            Set(value As String)
                ConfirmYN = value
            End Set
        End Property
        Public Property confirmed_by() As String
            Get
                Return ConfirmedBy
            End Get
            Set(value As String)
                ConfirmedBy = value
            End Set
        End Property
        Public Property Vender_Name() As String
            Get
                Return VenderName
            End Get
            Set(value As String)
                VenderName = value
            End Set
        End Property
    End Class
End Namespace
