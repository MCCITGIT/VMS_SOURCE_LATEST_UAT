Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class VendorStockEntry
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        Page.MaintainScrollPositionOnPostBack = True
        'txtAsOndate.Attributes = DateTime.Now.ToString("yyyy-MM-dd")
        If (Not IsPostBack) Then
            txtAsOndate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now)
            AddAttributes()
            'btnSubmit.Enabled = False
            populateVendor()
            ddlVendor_SelectedIndexChanged(sender, e)

        End If
    End Sub

#End Region

#Region "AddAttributes"

    Private Sub AddAttributes()
        btnSubmit.Attributes.Add("onclick", "return validateVendorStockEntry();")
    End Sub
#End Region

#Region "CheckLogin"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region


#Region "PopulateVendor"
    Public Sub populateVendor()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet
        DS = Obj.GetVendor(userInfo.userIDEntity)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlVendor.DataSource = DS.Tables(0)
            ddlVendor.DataTextField = "vendor_name"
            ddlVendor.DataValueField = "vendor_code"
            ddlVendor.DataBind()
            ddlVendor.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If DS.Tables(0).Rows.Count = 1 Then
                ddlVendor.SelectedIndex = 1
                ddlVendor.Enabled = False
            End If
        End If

    End Sub
#End Region

    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
        VendorStockListEntryLoad()
        'btnSubmit.Enabled = True
        lblErrorMessage.Text = ""
    End Sub


    Private Sub VendorStockListEntryLoad()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        Dim QualityControlClass As New QualityControlClass()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim StockList As DataSet

        Dim AsOnDate As String
        Dim AsOnDate1 As String
        AsOnDate = String.Format("{0:yyyy-MM-dd}", txtAsOndate.Text)
        'If AsOnDate = "" Then
        AsOnDate1 = String.Format("{0:yyyy-MM-dd}", DateTime.Now)
        'End If
        If AsOnDate <> AsOnDate1 Then
            btnSubmit.Visible = False
        Else
            btnSubmit.Visible = True
        End If

        Dim VendorId As String = ddlVendor.SelectedValue
        StockList = QualityControlClass.GetVendorStockEntryList(VendorId, AsOnDate)

        If (Not (StockList Is Nothing) AndAlso StockList.Tables.Count > 0) Then
            If (Not (StockList.Tables(0) Is Nothing) AndAlso StockList.Tables(0).Rows.Count > 0) Then
                grdStockEntry.DataSource = StockList
                grdStockEntry.DataBind()
                'If StockList.Tables(0).Rows(0)("vssm_vol").ToString <> "0.00" Then
                '    btnSubmit.Enabled = False
                'Else
                '    btnSubmit.Enabled = True
                'End If
                Me.CalculateGrandTotal()
                'grdStockEntry.Columns[1].Visible = False
            Else
                grdStockEntry.DataSource = Nothing
                grdStockEntry.DataBind()
                'gvVendorStockDetails.Visible = True
            End If
        End If

    End Sub

    Protected Sub txtNop_TextChanged(sender As Object, e As EventArgs)
        Dim qt As Double
        Dim qcost As Double
        Dim row As GridViewRow = TryCast((TryCast(sender, TextBox)).NamingContainer, GridViewRow)
        Dim kk As Integer = row.RowIndex
        qt = Convert.ToDouble((CType(grdStockEntry.Rows(kk).FindControl("txtNop"), TextBox)).Text)
        qcost = Convert.ToDouble((CType(grdStockEntry.Rows(kk).FindControl("txtPacksize"), Label)).Text)
        CType(grdStockEntry.Rows(kk).FindControl("txtValue"), TextBox).Text = (qt * qcost).ToString()
        CType(grdStockEntry.Rows(kk).FindControl("lblValue"), Label).Text = (qt * qcost).ToString()
        'CType(grdStockEntry.Rows(kk).FindControl("lbltotal"), Label).Text = (qt * qcost).ToString()
        Me.CalculateGrandTotal()
    End Sub

    Private Sub CalculateGrandTotal()
        Dim total As Double = 0
        For Each row As GridViewRow In grdStockEntry.Rows
            Dim subtotal As String = (CType(row.FindControl("txtValue"), TextBox)).Text
            total += If(Not String.IsNullOrEmpty(subtotal), Convert.ToDouble(subtotal), 0)
        Next
        TryCast(grdStockEntry.FooterRow.FindControl("lbltotal"), Label).Text = total.ToString()
    End Sub

#Region "Insert Vendor Stock Entry"
    Private Sub InsertVendorStockEntry()
        Dim numRowsDeleted As Integer
        Dim numRowsAffected As Integer
        Dim VendorStockE As New VendorStockEntryEntity
        Dim obj As New QualityControlClass
        Dim RowIndex As Integer = 0

        Try
            'sqlConn = DBFactory.GetHelper.OpenConnection()
            'sqlTrans = sqlConn.BeginTransaction()
            Dim dt1 As DataTable = New DataTable()
            Dim dr1 As DataRow
            dt1.Columns.Add(New System.Data.DataColumn("vssm_sku_code", GetType(String)))
            dt1.Columns.Add(New System.Data.DataColumn("vssm_nop", GetType(Decimal)))
            dt1.Columns.Add(New System.Data.DataColumn("vssm_vol", GetType(Decimal)))
            For RowIndex = 0 To grdStockEntry.Rows.Count - 1
                'TrnstDyEntity.vendor_unit = ddlUnit.SelectedValue
                Dim lblSkuCode As Label = grdStockEntry.Rows(RowIndex).FindControl("lblSkuCode")
                Dim txtNop As TextBox = grdStockEntry.Rows(RowIndex).FindControl("txtNop")
                Dim lblValue As Label = grdStockEntry.Rows(RowIndex).FindControl("lblValue")

                dr1 = dt1.NewRow()

                If txtNop.Text <> "0.00" Then
                    dr1.Item("vssm_sku_code") = Convert.ToString(lblSkuCode.Text)
                    dr1.Item("vssm_nop") = Convert.ToString(txtNop.Text.ToString().Split("|")(0))
                End If
                If lblValue.Text <> "0.00" Then
                    dr1.Item("vssm_vol") = Convert.ToString(lblValue.Text.ToString().Split("|")(0))
                    dt1.Rows.Add(dr1)
                End If


                'If Not (Trim(txtTransit.Text) = String.Empty) Then
                '    TrnstDyEntity.transit_days = CType(txtTransit.Text, Integer)
                'End If


            Next

            dt1.AcceptChanges()
            If dt1.Rows.Count > 0 Then
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Enter atleast one Stock."
                'Return
            End If

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                VendorStockE.CreatedUser = userInfo.userIDEntity
                VendorStockE.date = String.Format("{0:yyyy-MM-dd}", txtAsOndate.Text)
                VendorStockE.vendor_id = ddlVendor.SelectedValue
                numRowsAffected = obj.InsertVendorStock(VendorStockE, dt1)
                If numRowsAffected > 0 Then
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green
                    lblErrorMessage.Text = "Submitted Successfully"
                    ddlVendor.ClearSelection()
                    btnSubmit.Visible = False
                Else
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    lblErrorMessage.Text = "Something went wrong. Try again."

                End If
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Enter atleast one Stock ."

            End If

        Catch ex As Exception
            Dim returnUrl As String = "ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Server.Transfer(returnUrl)
        Finally
            VendorStockListEntryLoad()
        End Try
    End Sub
#End Region

#Region "Submit Click Event Handelling"
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        CheckLogin()
        InsertVendorStockEntry()
    End Sub
#End Region

#Region "gvTransitDays Event Handelling"
    Protected Sub grdStockEntry_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStockEntry.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            If rowView("vssm_vol") <> "0.00" Then
                e.Row.BackColor = Drawing.Color.LawnGreen
                e.Row.ForeColor = Drawing.Color.Black
                'btnSubmit.Enabled = False
            End If
        End If
    End Sub
#End Region

#Region "Search Button Click Event Handelling"
    Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgbtnSearch.Click
        VendorStockListEntryLoad()
    End Sub
#End Region


    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx", True)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Dim path = "~/VendorStockList.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub
    Protected Sub RangeValidator2_Init(sender As Object, e As EventArgs)
        Dim Validator As RangeValidator = DirectCast(sender, RangeValidator)
        Dim Today As Date = Date.Today
        Validator.MaximumValue = Today.ToString("yyyy/MM/dd")
        Validator.MinimumValue = Today.AddDays(-1).ToString("yyyy/MM/dd")
    End Sub
End Class
