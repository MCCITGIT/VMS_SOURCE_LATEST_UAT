Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient

Partial Class ChallanCancellationEntry
    Inherits System.Web.UI.Page
    Dim Updatemode As Boolean = False
    Dim Aproved_yn As Boolean = False
    Dim userInfo As VMSUserEntity = New VMSUserEntity()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckLogin()
        If Not IsPostBack Then
            PageSizeDropdown()
            If Not String.IsNullOrEmpty(Request.QueryString(Constant.SessionKeys.Challan_No)) AndAlso Not String.IsNullOrEmpty(Request.QueryString(Constant.SessionKeys.Process_Year)) AndAlso Not String.IsNullOrEmpty(Request.QueryString(Constant.SessionKeys.UnitCode)) Then
                PopulateUpdateMode(Request.QueryString(Constant.SessionKeys.Challan_No), Request.QueryString(Constant.SessionKeys.Process_Year), Request.QueryString(Constant.SessionKeys.UnitCode))
            Else
                'btnSubmit.Enabled = False

                'txtChallanDt.Text = Format(Date.Now, "dd/MM/yyyy")
                'txtCenvatDt.Text = Format(Date.Now, "dd/MM/yyyy")
                'GetScreenDetails()
                'PopulateRegion()
                'PopulateDepotName()
                'PopulateDeliveryDepotName()
                'PopulatePONo()
                'PopulateSiteDetails()
                'PageSizeDropdown()
                ''Dim CHDate As Date = FormatDate(txtChallanDt.Text)
                ''Dim CurrentDate As String = CHDate.ToString("yyyyMMdd")
                ''Dim lotNo As String = "IND/" + userInfo.userBranchEntity + "/" + CurrentDate
                ''hdnLotNo.Value = lotNo.ToString
                Response.Redirect("ChallanCancellationList.aspx")

            End If
            AddAttributes()
        End If
    End Sub


#Region "Add Attributes"
    Public Sub AddAttributes()
        btnSubmit.Attributes.Add("onClick", "return confirm('Are you sure to cancel this?');")
    End Sub
#End Region

#Region "Login Check"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region

    '#Region "Get Screen Details"
    '    Private Sub GetScreenDetails()
    '        CheckLogin()
    '        Dim ScreenDS As DataSet
    '        Dim StockObj As New UnitDespatchClass
    '        ScreenDS = StockObj.GetSCreenDetails(userInfo.userBranchEntity)
    '        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
    '            lblYear.Text = ScreenDS.Tables(0).Rows(0)("year").ToString
    '            hdnYear.Value = ScreenDS.Tables(0).Rows(0)("year").ToString
    '            lblmonth.Text = ScreenDS.Tables(0).Rows(0)("month").ToString
    '            hdnMonth.Value = ScreenDS.Tables(0).Rows(0)("month").ToString
    '            lblUnit.Text = ScreenDS.Tables(0).Rows(0)("unit").ToString
    '            hdnMaxDespLimit.Value = ScreenDS.Tables(0).Rows(0)("maxLimit").ToString
    '            hdnUnitOracleId.Value = ScreenDS.Tables(0).Rows(0)("unitOracleId").ToString
    '        End If
    '    End Sub
    '#End Region

    Private Sub PageSizeDropdown()
        ddlPageSize.Items.Clear()
        'Gets the page size from the web.config file
        Dim configPagesize As String = ConfigurationManager.AppSettings.Get("PageSize")
        Dim numbers As String() = configPagesize.Split(",")
        Dim index As Integer = 0

        While index <= numbers.Length - 1
            Try
                Dim size As Integer = Convert.ToInt32(numbers(index))
                'Adds the page size to drop down list
                ddlPageSize.Items.Add(New ListItem(size.ToString, size.ToString))
            Catch exp As Exception
                ddlPageSize.Items.Clear()
                'LoadDefaultPageSize()
            End Try
            System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
        End While
        ddlPageSize.Items.Insert(0, New ListItem("999", 999, True))
        gvSKUDetails.PageSize = ddlPageSize.SelectedValue
    End Sub

#Region "Populate Region Dropdown"
    Public Sub PopulateRegion()
        CheckLogin()
        Dim commonObj As New Common
        Dim RegionDS As New DataSet
        Dim RegiontypeDS As DataSet = commonObj.GetLovDetails(userInfo.userCompanyEntity, Constant.Common.REGION_TYPE, Constant.Common.ActiveStatus)
        If Not (RegiontypeDS Is Nothing) Then
            ddlRegion.DataSource = RegiontypeDS
            ddlRegion.DataTextField = "Lov_Value"
            ddlRegion.DataValueField = "Lov_Code"
            ddlRegion.DataBind()
            ddlRegion.Items.Insert(0, New ListItem("ALL", "", True))
        End If
    End Sub
#End Region

#Region "Populate Depot Dropdown"
    Public Sub PopulateDepotName()
        CheckLogin()
        ddlLocation.Items.Clear()
        Dim commonObj As New Common
        Dim DepotDS As New DataSet

        DepotDS = commonObj.Getdepotname(ddlRegion.SelectedValue)
        If (Not (DepotDS Is Nothing) AndAlso DepotDS.Tables.Count > 0 AndAlso Not (DepotDS.Tables(0) Is Nothing) AndAlso DepotDS.Tables(0).Rows.Count > 0) Then
            ddlLocation.DataSource = DepotDS.Tables(0)
            ddlLocation.DataTextField = "Depot_Name"
            ddlLocation.DataValueField = "depot_code"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If

    End Sub
#End Region

#Region "Populate Delivery Depot Dropdown"
    Public Sub PopulateDeliveryDepotName()
        CheckLogin()
        ddlDeliveryDepot.Items.Clear()
        Dim commonObj As New Common
        Dim DepotDS As New DataSet

        DepotDS = commonObj.Getdepotname(String.Empty)
        If (Not (DepotDS Is Nothing) AndAlso DepotDS.Tables.Count > 0 AndAlso Not (DepotDS.Tables(0) Is Nothing) AndAlso DepotDS.Tables(0).Rows.Count > 0) Then
            ddlDeliveryDepot.DataSource = DepotDS.Tables(0)
            ddlDeliveryDepot.DataTextField = "Depot_Name"
            ddlDeliveryDepot.DataValueField = "depot_code"
            ddlDeliveryDepot.DataBind()
            ddlDeliveryDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If

    End Sub
#End Region

    Protected Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepotName()
    End Sub

#Region "Populate Update Mode"
    Private Sub PopulateUpdateMode(ByVal challanNo As Integer, ByVal year As String, ByVal unit As String)
        CheckLogin()
        PopulateRegion()
        PopulateDepotName()
        PopulateDeliveryDepotName()

        'PageSizeDropdown()
        Updatemode = True
        'btnSubmit.Text = Constant.GeneralMessages.btnUpdate
        Dim DespatchObj As New UnitDespatchClass
        Dim DespatchDS As DataSet
        DespatchDS = DespatchObj.GetUpdateModeDetail(challanNo, year, unit)
        If (Not (DespatchDS Is Nothing) AndAlso DespatchDS.Tables.Count > 0 AndAlso Not (DespatchDS.Tables(0) Is Nothing) AndAlso DespatchDS.Tables(0).Rows.Count > 0) Then
            ddlRegion.SelectedValue = DespatchDS.Tables(0).Rows(0)("region").ToString
            ddlLocation.SelectedValue = DespatchDS.Tables(0).Rows(0)("desph_desp_depot").ToString

            txtCenvatNo.Text = DespatchDS.Tables(0).Rows(0)("desph_excise_gp_no").ToString
            txtCenvatDt.Text = DespatchDS.Tables(0).Rows(0)("desph_excise_gp_dt").ToString
            txtChallanDt.Text = DespatchDS.Tables(0).Rows(0)("desph_challan_date").ToString
            txtTransporter.Text = DespatchDS.Tables(0).Rows(0)("desph_transporter_name").ToString
            txtTruckNo.Text = DespatchDS.Tables(0).Rows(0)("desph_truck_no").ToString
            hdnChallanno.Value = CType(DespatchDS.Tables(0).Rows(0)("desph_challan_no").ToString, Integer)
            lblmonth.Text = DespatchDS.Tables(0).Rows(0)("desph_process_month").ToString
            hdnMonth.Value = DespatchDS.Tables(0).Rows(0)("desph_process_month").ToString
            lblUnit.Text = DespatchDS.Tables(0).Rows(0)("desph_desp_unit").ToString
            lblYear.Text = DespatchDS.Tables(0).Rows(0)("desph_challan_fin_year").ToString
            hdnYear.Value = DespatchDS.Tables(0).Rows(0)("desph_challan_fin_year").ToString
            ddlDeliveryDepot.SelectedValue = DespatchDS.Tables(0).Rows(0)("desph_delivery_depot").ToString

            'PopulateSiteDetails()
            'ddlSite.SelectedValue = DespatchDS.Tables(0).Rows(0)("desph_site_name").ToString
            'PopulatePONo()
            'ddlPONo.SelectedValue = Convert.ToString(DespatchDS.Tables(0).Rows(0)("desph_po_no"))

            hdnUnitOracleId.Value = Convert.ToString(DespatchDS.Tables(0).Rows(0)("UnitOracleId"))

            'ddlProduct.Items.Clear()
            'ddlProduct.Items.Insert(0, New ListItem("All", String.Empty, True))
            'ddlProduct.Enabled = False
            'ddlAllSku.SelectedValue = "Y"
            'ddlAllSku.Enabled = False
            txtChallanDt.Enabled = False
            'ddlPONo.Enabled = False
            'ddlSite.Enabled = False
            ddlDeliveryDepot.Enabled = False
            ddlLocation.Enabled = False
            'ddlProduct.Enabled = False
            ddlRegion.Enabled = False


            txtTransporter.ReadOnly = True
            txtTruckNo.ReadOnly = True
            txtCenvatNo.ReadOnly = True
            txtCenvatDt.ReadOnly = True
            txtRoadPermitNo.ReadOnly = True

            'ChallanDt.visible = False
            lblChallanNo.Text = "Challan No. : " & hdnChallanno.Value
            txtRoadPermitNo.Text = DespatchDS.Tables(0).Rows(0)("desph_road_permit_no").ToString
            If (DespatchDS.Tables(0).Rows(0)("desph_approved_yn").ToString = "R") Then
                Aproved_yn = True
                btnSubmit.Visible = False

                'gvSKUDetails.Columns(9).Visible = False
            Else
                btnSubmit.Visible = True
            End If
        End If
        BindGridForUpdateMode(challanNo, unit, year)
    End Sub
#End Region

#Region "Bind Grid for Update Mode"
    Private Sub BindGridForUpdateMode(ByVal challanNo As Integer, ByVal unitCode As String, ByVal year As String)
        Dim DespatchDS As DataSet
        Dim DespatchObj As New UnitDespatchClass
        DespatchDS = DespatchObj.GetSKUDetailsForUpdateMode(challanNo, year, Constant.Common.ActiveStatus, unitCode, ddlLocation.SelectedValue)
        If (Not (DespatchDS Is Nothing) AndAlso DespatchDS.Tables.Count > 0 AndAlso Not (DespatchDS.Tables(0) Is Nothing) AndAlso DespatchDS.Tables(0).Rows.Count > 0) Then
            gvSKUDetails.DataSource = DespatchDS
            gvSKUDetails.DataBind()
            hdnNoMaster.Value = DespatchDS.Tables(0).Rows(0)("nomaster")
        Else
            gvSKUDetails.DataSource = DespatchDS
            gvSKUDetails.DataBind()
        End If
    End Sub
#End Region



    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        CheckLogin()
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Try
            Dim challanNo As String = Request.QueryString("Challan_No")
            Dim unit As String = Request.QueryString("UnitCode")
            Dim output As Integer = 0

            If ((Not (String.IsNullOrEmpty(challanNo))) AndAlso (Not (String.IsNullOrEmpty(unit)))) AndAlso (Integer.TryParse(challanNo, output) And (Not String.IsNullOrEmpty(hdnYear.Value)) And (Not String.IsNullOrEmpty(hdnMonth.Value))) Then
                If Not (String.IsNullOrEmpty(ddlDeliveryDepot.SelectedValue)) Then
                    Dim numRowsAffected As Integer = 0
                    Dim DespatchObj As New UnitDespatchClass


                    sqlConn = DBFactory.GetHelper.OpenConnection()
                    sqlTrans = sqlConn.BeginTransaction()
                    numRowsAffected = DespatchObj.CancelChallanENtry(Convert.ToInt32(challanNo), unit, ddlDeliveryDepot.SelectedValue, userInfo.userIDEntity, hdnYear.Value, hdnMonth.Value, sqlConn, sqlTrans)
                    If numRowsAffected > 0 Then
                        sqlTrans.Commit()
                        lblPopMessage.Text = "Challan cancelled successfully."
                    Else
                        sqlTrans.Rollback()
                        lblPopMessage.Text = "Challan Cancellation failed."
                    End If
                Else
                    lblPopMessage.Text = "Invalid Depot."
                End If
            Else
                lblPopMessage.Text = "Invalid Challan No. or Unit."
            End If

        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
            ModalPopupExtender3.Show()
            If Not String.IsNullOrEmpty(Request.QueryString(Constant.SessionKeys.Challan_No)) AndAlso Not String.IsNullOrEmpty(Request.QueryString(Constant.SessionKeys.Process_Year)) AndAlso Not String.IsNullOrEmpty(Request.QueryString(Constant.SessionKeys.UnitCode)) Then
                PopulateUpdateMode(Request.QueryString(Constant.SessionKeys.Challan_No), Request.QueryString(Constant.SessionKeys.Process_Year), Request.QueryString(Constant.SessionKeys.UnitCode))
            Else
                Response.Redirect("ChallanCancellationList.aspx")
            End If
        End Try
    End Sub

    Protected Sub gvSKUDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSKUDetails.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim pageIdx As Integer = gvSKUDetails.PageIndex * ddlPageSize.SelectedValue
            e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
          

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            'If rowView("desph_approved_yn").ToString = "Y" Then
            '    btnGo.Enabled = False
            'Else
            '    btnGo.Enabled = True
            'End If


            
        End If

        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            'row = e.Row.Controls(0).Controls(0).Controls(0)
            For Each cell As TableCell In row.Cells
                Dim lb As Control = cell.Controls(0)


                If (TypeOf (lb) Is Label) Then

                    'CType(lb, Label).ForeColor = System.Drawing.Color.Red
                    CType(lb, Label).CssClass = "lblpager"
                    'CType(lb, Label).Width = 20
                    'CType(lb, Label).Height = 15
                    'set current pager

                ElseIf (TypeOf (lb) Is LinkButton) Then

                    CType(lb, LinkButton).CssClass = "lnkpager"
                    'CType(lb, LinkButton).Width = 20
                    'CType(lb, LinkButton).Height = 15
                    'CType(lb, LinkButton).ForeColor = Drawing.Color.Black
                End If

            Next
        End If
    End Sub
End Class
