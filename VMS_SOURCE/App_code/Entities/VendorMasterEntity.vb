Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes
Public Class VendorMasterEntity
    Private Vendor_Region As String
    Private Vendor_Depot As String
    Private Vendor_Name As String
    Private Vendor_TSL As String
    Private Vendor_PA As String
    Private Vendor_Unit As String
    Private page_Size As String
    Private Vendor_Sku_Code As String
    Private Page_Number As Integer
    Public Sub New()

        Vendor_Region = String.Empty
        Vendor_Depot = String.Empty
        Vendor_Name = String.Empty
        Vendor_TSL = String.Empty
        Vendor_PA = String.Empty
        Vendor_Unit = String.Empty
        page_Size = String.Empty
        Vendor_Sku_Code = String.Empty
        Page_Number = Integer.MinValue
    End Sub

    Public Property PageNumber() As Integer
        Get
            Return Page_Number
        End Get
        Set(ByVal value As Integer)
            Page_Number = value
        End Set
    End Property




    Public Property VendorSku_Code() As String
        Get
            Return Vendor_Sku_Code
        End Get
        Set(ByVal value As String)
            Vendor_Sku_Code = value
        End Set
    End Property




    Public Property pageSize() As String
        Get
            Return page_Size
        End Get
        Set(ByVal value As String)
            page_Size = value
        End Set
    End Property


    Public Property VendorRegion() As String
        Get
            Return Vendor_Region
        End Get
        Set(ByVal value As String)
            Vendor_Region = value
        End Set
    End Property

    Public Property VendorUnit() As String
        Get
            Return Vendor_Unit
        End Get
        Set(ByVal value As String)
            Vendor_Unit = value
        End Set
    End Property




    Public Property VendorDepot() As String
        Get
            Return Vendor_Depot
        End Get
        Set(ByVal value As String)
            Vendor_Depot = Trim(value)
        End Set
    End Property

    Public Property VendorName() As String
        Get
            Return Vendor_Name
        End Get
        Set(ByVal value As String)
            Vendor_Name = Trim(value)
        End Set
    End Property

    Public Property VendorTSL() As String
        Get
            Return Vendor_TSL
        End Get
        Set(ByVal value As String)
            Vendor_TSL = Trim(value)
        End Set
    End Property
    Public Property VendorPA() As String
        Get
            Return Vendor_PA
        End Get
        Set(ByVal value As String)
            Vendor_PA = Trim(value)
        End Set
    End Property
    

End Class
