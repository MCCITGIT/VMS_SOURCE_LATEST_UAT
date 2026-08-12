Imports Microsoft.VisualBasic
Namespace VMS.Web

    Public Class ExportExcelEntity

        Private Ex_Table_Name As String
        Private Ex_Selected_Field_Name As String
        Private Ex_Heading As String
        Private Ex_FieldOrder As Integer
        Private Ex_Total_YN As String
        Private Ex_DataType As String
        Private Ex_OrderBy As String
        Private Ex_Report_Name As String
        Private Ex_GeneratedQuery As String
        Private tx_field_1 As Integer
        Private tx_field_2 As String
        Private created_user As String
        Private modified_user As String

        Public Sub New()
            Ex_Table_Name = String.Empty
            Ex_Selected_Field_Name = String.Empty
            Ex_Heading = String.Empty
            Ex_FieldOrder = Integer.MinValue
            Ex_Total_YN = String.Empty
            Ex_DataType = String.Empty
            Ex_OrderBy = String.Empty
            Ex_Report_Name = String.Empty
            Ex_GeneratedQuery = String.Empty
            tx_field_1 = Integer.MinValue
            tx_field_2 = String.Empty
            created_user = String.Empty
            modified_user = String.Empty

        End Sub

        Public Property ExTableName() As String
            Get
                Return Ex_Table_Name
            End Get
            Set(ByVal value As String)
                Ex_Table_Name = value
            End Set
        End Property
        Public Property ExSelectedFieldName() As String
            Get
                Return Ex_Selected_Field_Name
            End Get
            Set(ByVal value As String)
                Ex_Selected_Field_Name = value
            End Set
        End Property
        Public Property ExHeading() As String
            Get
                Return Ex_Heading
            End Get
            Set(ByVal value As String)
                Ex_Heading = value
            End Set
        End Property
        Public Property ExFieldOrder() As Integer
            Get
                Return Ex_FieldOrder
            End Get
            Set(ByVal value As Integer)
                Ex_FieldOrder = value
            End Set
        End Property
        Public Property ExTotalYN() As String
            Get
                Return Ex_Total_YN
            End Get
            Set(ByVal value As String)
                Ex_Total_YN = value
            End Set
        End Property
        Public Property ExDataType() As String
            Get
                Return Ex_DataType
            End Get
            Set(ByVal value As String)
                Ex_DataType = value
            End Set
        End Property
        Public Property ExOrderBy() As String
            Get
                Return Ex_OrderBy
            End Get
            Set(ByVal value As String)
                Ex_OrderBy = value
            End Set
        End Property
        Public Property ExReportName() As String
            Get
                Return Ex_Report_Name
            End Get
            Set(ByVal value As String)
                Ex_Report_Name = value
            End Set
        End Property
        Public Property ExGeneratedQuery() As String
            Get
                Return Ex_GeneratedQuery
            End Get
            Set(ByVal value As String)
                Ex_GeneratedQuery = value
            End Set
        End Property
        Public Property txfield1() As Integer
            Get
                Return tx_field_1
            End Get
            Set(ByVal value As Integer)
                tx_field_1 = value
            End Set
        End Property
        Public Property txfield2() As String
            Get
                Return tx_field_2
            End Get
            Set(ByVal value As String)
                tx_field_2 = value
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
                Return modified_user
            End Get
            Set(ByVal value As String)
                modified_user = value
            End Set
        End Property

    End Class
End Namespace
