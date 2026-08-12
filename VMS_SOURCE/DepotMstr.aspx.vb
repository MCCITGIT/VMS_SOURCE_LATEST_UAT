Imports System.Data
Imports VMS.Web
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Partial Class DepotMstr
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        AddAttributes()
        If Not IsPostBack Then
            PopulateRegion()
            PopulateDepot()
            div1.Visible = False
        End If
    End Sub
#Region "Check Login"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region
#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        'imgbtnSearch.OnClientClick = "return ValidateSubmit();"
    End Sub
#End Region
#Region "Populate Region"
    Private Sub PopulateRegion()
        Dim obj As New DepotMstrClass
        Dim ds As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE
        ds = obj.GetRegionDetails()

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlRegion.DataSource = ds.Tables(0)
            ddlRegion.DataTextField = "RegionName"
            ddlRegion.DataValueField = "RegionCode"
            ddlRegion.DataBind()
            ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If

    End Sub
#End Region
#Region "Populate Depot"
    Private Sub PopulateDepot()
        Dim obj As New DepotMstrClass
        Dim ds As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE
        ds = obj.GetDepotDetails(ddlRegion.SelectedValue.ToString())

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = ds.Tables(0)
            ddlDepot.DataTextField = "Depot_Name"
            ddlDepot.DataValueField = "Depot_Code"
            ddlDepot.DataBind()
            'ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
            ddlDepot.Items.Insert(0, New ListItem("Select", String.Empty, True))
        End If

    End Sub
#End Region
    Protected Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
        ClearSection()
        div1.Visible = False
        'If (ddlRegion.SelectedValue <> "") Then
        '    PopulateDepot()
        '    ClearSection()
        '    div1.Visible = False
        'Else
        '    PopulateDepot()
        '    ClearSection()
        '    div1.Visible = False
        'End If

    End Sub
#Region "Depot Details Populate"
    Private Sub PopulateDepot_Details()
        div1.Visible = True
        Dim obj As New DepotMstrClass
        Dim ds As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE
        ds = obj.GetDepot_DataList(ddlDepot.SelectedValue.ToString())

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0 AndAlso Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
            lblAddr1.InnerHtml = ds.Tables(0).Rows(0)("Address1").ToString()
            lblAddr2.InnerHtml = ds.Tables(0).Rows(0)("Address2").ToString()
            lblphno.InnerHtml = ds.Tables(0).Rows(0)("Phone_No").ToString()
            lblemail.InnerHtml = ds.Tables(0).Rows(0)("Email_Id").ToString()

            lblcity.InnerHtml = ds.Tables(0).Rows(0)("City").ToString()
            lblstate.InnerHtml = ds.Tables(0).Rows(0)("State").ToString()
            lblpin.InnerHtml = ds.Tables(0).Rows(0)("PinCode").ToString()
            lblgstn.InnerHtml = ds.Tables(0).Rows(0)("GSTN_No").ToString()
        End If

    End Sub
#End Region
    Protected Sub ddlDepot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepot.SelectedIndexChanged
        If ddlDepot.SelectedValue <> "" Then
            PopulateDepot_Details()

        Else
            ClearSection()
            div1.Visible = False
        End If
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As EventArgs) Handles imgbtnBack.Click
        Response.Redirect("~/Home.aspx")
    End Sub
    Private Sub ClearSection()
        lblAddr1.InnerHtml = ""
        lblAddr2.InnerHtml = ""
        lblphno.InnerHtml = ""
        lblemail.InnerHtml = ""

        lblcity.InnerHtml = ""
        lblstate.InnerHtml = ""
        lblpin.InnerHtml = ""
        lblgstn.InnerHtml = ""
    End Sub
End Class
