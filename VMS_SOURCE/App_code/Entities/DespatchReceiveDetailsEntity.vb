Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes

Public Class DespatchReceiveDetailsEntity

    Private unit As String
    Private depot As String
    Private process_year As String
    Private process_month As String
    Private challan_no As Integer
    Private challan_date As SqlDateTime
    Private transporter_name As String
    Private permit_no As String
    Private truck_no As String
    Private gp_no As String
    Private gp_date As SqlDateTime
    Private recv_total_ltr As Decimal
    Private recv_total_kg As Decimal
    Private receive_date As SqlDateTime
    Private created_user As String
    Private additional_yn As String


    Public Sub New()

        unit = String.Empty
        depot = String.Empty
        permit_no = String.Empty
        truck_no = String.Empty
        transporter_name = String.Empty
        gp_no = String.Empty
        gp_date = SqlDateTime.MinValue
        process_year = String.Empty
        process_month = String.Empty
        challan_no = Integer.MinValue
        challan_date = SqlDateTime.MinValue
        recv_total_ltr = Decimal.MinValue
        recv_total_kg = Decimal.MinValue
        receive_date = SqlDateTime.MinValue
        created_user = String.Empty
        additional_yn = String.Empty

    End Sub

    Public Property VUnit() As String
        Get
            Return Unit
        End Get
        Set(ByVal value As String)
            unit = value
        End Set
    End Property

    Public Property VDepot() As String
        Get
            Return depot
        End Get
        Set(ByVal value As String)
            depot = value
        End Set
    End Property

    Public Property TransporterName() As String
        Get
            Return transporter_name
        End Get
        Set(ByVal value As String)
            transporter_name = value
        End Set
    End Property

    Public Property PermitNo() As String
        Get
            Return permit_no
        End Get
        Set(ByVal value As String)
            permit_no = value
        End Set
    End Property

    Public Property TruckNo() As String
        Get
            Return truck_no
        End Get
        Set(ByVal value As String)
            truck_no = value
        End Set
    End Property

    Public Property GPNo() As String
        Get
            Return gp_no
        End Get
        Set(ByVal value As String)
            gp_no = value
        End Set
    End Property

    Public Property GPDate() As SqlDateTime
        Get
            Return gp_date
        End Get
        Set(ByVal value As SqlDateTime)
            gp_date = value
        End Set
    End Property

    Public Property ProcessYear() As String
        Get
            Return process_year
        End Get
        Set(ByVal value As String)
            process_year = value
        End Set
    End Property

    Public Property ProcessMonth() As String
        Get
            Return process_month
        End Get
        Set(ByVal value As String)
            process_month = value
        End Set
    End Property

    Public Property ChallanNo() As Integer
        Get
            Return challan_no
        End Get
        Set(ByVal value As Integer)
            challan_no = value
        End Set
    End Property

    Public Property ChallanDate() As SqlDateTime
        Get
            Return challan_date
        End Get
        Set(ByVal value As SqlDateTime)
            challan_date = value
        End Set
    End Property

    Public Property ReceiveTotalLtr() As Decimal
        Get
            Return recv_total_ltr
        End Get
        Set(ByVal value As Decimal)
            recv_total_ltr = value
        End Set
    End Property

    Public Property ReceiveTotalKg() As Decimal
        Get
            Return recv_total_kg
        End Get
        Set(ByVal value As Decimal)
            recv_total_kg = value
        End Set
    End Property

    Public Property ReceiveDate() As SqlDateTime
        Get
            Return receive_date
        End Get
        Set(ByVal value As SqlDateTime)
            receive_date = value
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

    Public Property AdditionalYN() As String
        Get
            Return additional_yn
        End Get
        Set(ByVal value As String)
            additional_yn = value
        End Set
    End Property

End Class
