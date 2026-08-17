
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient
Partial Class LoadDropAddUpdate
    Inherits System.Web.UI.Page
    Dim Updatemode As Boolean = False
    Dim Aproved_yn As Boolean = False
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckLogin()
        AddAttribute()
        If Not IsPostBack Then
            PopulateRegion()
            PopulateDepot()
            PopulateUnit()
            PopulateProcessYr()
            PopulateProduct()

            BindGrid()

            If (Not (Request.QueryString(Constant.SessionKeys.DEPT) Is Nothing) AndAlso Not (Request.QueryString("Vendor") Is Nothing)) Then
                Dim Depot As String
                Dim Vendor As String
                Depot = Request.QueryString(Constant.SessionKeys.DEPT)
                Vendor = Request.QueryString("Vendor")
                BindGridDepotWise(Depot, Vendor)

            End If

        End If
    End Sub

    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#Region "AddAttribute"
    Private Sub AddAttribute()
        btnSubmit.Attributes.Add("onclick", "return ValidateSubmit();")
    End Sub
#End Region

#Region "Populate Region"
    Private Sub PopulateRegion()
        CheckLogin()

        Dim ObjDocumentType As New Common
        Dim OccupationTypeSet As New DataSet
        Dim LovType As String = Constant.Common.REGION_TYPE
        OccupationTypeSet = ObjDocumentType.GetLovDetails(userInfo.userCompanyEntity, LovType, Constant.Common.ActiveStatus)
        If (Not (OccupationTypeSet Is Nothing) AndAlso OccupationTypeSet.Tables.Count > 0 AndAlso Not (OccupationTypeSet.Tables(0) Is Nothing) AndAlso OccupationTypeSet.Tables(0).Rows.Count > 0) Then
            ddlRegion.DataSource = OccupationTypeSet.Tables(0)
            ddlRegion.DataTextField = "lov_value"
            ddlRegion.DataValueField = "lov_code"
            ddlRegion.DataBind()
            ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
            ddlRegion.SelectedValue = userInfo.userRegionEntity
            ddlRegion.Enabled = False
        End If

    End Sub
#End Region
#Region "Populate Depot"
    Private Sub PopulateDepot()
        CheckLogin()

        Dim GeDepot As New Common
        Dim DepotSet As New DataSet

        DepotSet = GeDepot.Getdepotname(ddlRegion.SelectedValue)
        If (Not (DepotSet Is Nothing) AndAlso DepotSet.Tables.Count > 0 AndAlso Not (DepotSet.Tables(0) Is Nothing) AndAlso DepotSet.Tables(0).Rows.Count > 0) Then
            ddlDepot.DataSource = DepotSet.Tables(0)
            ddlDepot.DataTextField = "depot_name"
            ddlDepot.DataValueField = "depot_code"
            ddlDepot.DataBind()
            'ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.UNIT) Then
        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
            ddlDepot.SelectedValue = userInfo.userBranchEntity
            ddlDepot.Enabled = False
        Else
            ddlDepot.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
    End Sub
#End Region

#Region "Populate Unit"
    Private Sub PopulateUnit()
        CheckLogin()

        Dim UnitDespatch As New PendingDespatchesClass
        Dim UnitSet As New DataSet

        UnitSet = UnitDespatch.GetUnitName(Constant.Common.ActiveStatus, String.Empty)
        If (Not (UnitSet Is Nothing) AndAlso UnitSet.Tables.Count > 0 AndAlso Not (UnitSet.Tables(0) Is Nothing) AndAlso UnitSet.Tables(0).Rows.Count > 0) Then
            ddlUnit.DataSource = UnitSet.Tables(0)
            ddlUnit.DataTextField = "unit_name"
            ddlUnit.DataValueField = "unit_code"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem(Constant.Common.All, String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlUnit.SelectedValue = userInfo.userUnitEntity
        '    ddlUnit.Enabled = False
        'End If
        If (userInfo.userGroupCodeEntity = "UNIT") Then
            ddlUnit.SelectedValue = userInfo.userBranchEntity
            ddlUnit.Enabled = False
        End If
    End Sub

    'Private Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
    '    PopulateDepot()
    'End Sub
#End Region

#Region "Populate Process Year"
    Private Sub PopulateProcessYr()
        CheckLogin()

        Dim ProcessYr As New Common
        Dim StandrdParams As New MonthlyUnitDespatch
        Dim YearSet As New DataSet
        Dim StandardYrMnth As New DataSet

        YearSet = ProcessYr.GetFinYrDetails(Constant.Common.Company, Constant.Common.ActiveStatus)
        StandardYrMnth = StandrdParams.GetMnthsYr(Constant.Common.ActiveStatus)
        If (Not (YearSet Is Nothing) AndAlso YearSet.Tables.Count > 0 AndAlso Not (YearSet.Tables(0) Is Nothing) AndAlso YearSet.Tables(0).Rows.Count > 0) Then
            ddlProcessYr.DataSource = YearSet.Tables(0)
            ddlProcessYr.DataTextField = "fin_year"
            ddlProcessYr.DataValueField = "fin_year"
            ddlProcessYr.DataBind()
            'ddlProcessYr.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            'ddlProcessYr.Items.Insert(0, New ListItem("2011", String.Empty, True))
        End If
        'If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.DEPOT) Then
        '    ddlProcessYr.SelectedValue = userInfo.currentFinancialYearEntity
        '    ddlProcessYr.Enabled = False
        'End If

        If (Not (StandardYrMnth Is Nothing) AndAlso StandardYrMnth.Tables.Count > 0 AndAlso Not (StandardYrMnth.Tables(0) Is Nothing) AndAlso StandardYrMnth.Tables(0).Rows.Count > 0) Then
            ddlProcessYr.SelectedValue = StandardYrMnth.Tables(0).Rows(0)("param_char_value")
            ddlProcessMnth.SelectedValue = StandardYrMnth.Tables(0).Rows(1)("param_char_value")
            ddlProcessYr.Enabled = False
            ddlProcessMnth.Enabled = False

        End If

    End Sub
#End Region

    Private Sub PopulateProduct()
        CheckLogin()
        Dim DespatchDS As DataSet
        Dim DespatchObj As New LoadDropAddUpdateClass

        Dim strLocationCode As String = String.Empty

        DespatchDS = DespatchObj.GetProductList(ddlUnit.SelectedValue, ddlRegion.SelectedValue, ddlDepot.SelectedValue)
        If (Not (DespatchDS Is Nothing) AndAlso DespatchDS.Tables.Count > 0 AndAlso Not (DespatchDS.Tables(0) Is Nothing) AndAlso DespatchDS.Tables(0).Rows.Count > 0) Then
            ddlProduct.DataSource = DespatchDS
            ddlProduct.DataTextField = "descript"
            ddlProduct.DataValueField = "product"
            ddlProduct.DataBind()
            ddlProduct.Items.Insert(0, New ListItem("All", String.Empty, True))
        Else
            ddlProduct.Items.Clear()
            ddlProduct.Items.Insert(0, New ListItem("Select", 0, True))
        End If


    End Sub
    Private Sub ddlUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnit.SelectedIndexChanged
        PopulateProduct()
        BindGrid()
    End Sub

    Private Sub BindGrid()
        CheckLogin()

        Dim DespatchDS As DataSet
        Dim DespatchObj As New LoadDropAddUpdateClass

        Dim strLocationCode As String = String.Empty

        DespatchDS = DespatchObj.GetSKUDetails(ddlUnit.SelectedValue, ddlProduct.SelectedValue, ddlProcessYr.SelectedValue, ddlProcessMnth.SelectedValue, ddlRegion.SelectedValue, ddlDepot.SelectedValue)
        If (Not (DespatchDS Is Nothing) AndAlso DespatchDS.Tables.Count > 0 AndAlso Not (DespatchDS.Tables(0) Is Nothing) AndAlso DespatchDS.Tables(0).Rows.Count > 0) Then
            gvSKUDetails.DataSource = DespatchDS
            gvSKUDetails.DataBind()
            btnSubmit.Enabled = True
            'hdnNoMaster.Value = DespatchDS.Tables(0).Rows(0)("nomaster")
        Else
            gvSKUDetails.DataSource = DespatchDS
            gvSKUDetails.DataBind()
            btnSubmit.Enabled = False
        End If
    End Sub
    Private Sub BindGridDepotWise(Depot As String, Vendor As String)
        CheckLogin()

        Dim DespatchDS As DataSet
        Dim DespatchObj As New LoadDropAddUpdateClass

        Dim strLocationCode As String = String.Empty

        DespatchDS = DespatchObj.GetSKUDetailsDropDepotWise(Depot, Vendor)
        If (Not (DespatchDS Is Nothing) AndAlso DespatchDS.Tables.Count > 0 AndAlso Not (DespatchDS.Tables(0) Is Nothing) AndAlso DespatchDS.Tables(0).Rows.Count > 0) Then
            gvSKUDetails.DataSource = DespatchDS
            gvSKUDetails.DataBind()
            btnSubmit.Enabled = False
            gvSKUDetails.Enabled = False
            ddlRegion.Enabled = False
            ddlDepot.Enabled = False
            ddlUnit.Enabled = False
            ddlProduct.Enabled = False
            ddlProcessYr.Enabled = False
            ddlProcessMnth.Enabled = False
            'hdnNoMaster.Value = DespatchDS.Tables(0).Rows(0)("nomaster")
        Else
            gvSKUDetails.DataSource = DespatchDS
            gvSKUDetails.DataBind()
            btnSubmit.Enabled = False
        End If
    End Sub

    Private Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProduct.SelectedIndexChanged
        BindGrid()
    End Sub

    Private Sub gvSKUDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSKUDetails.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Dim pageIdx As Integer = gvSKUDetails.PageIndex * ddlPageSize.SelectedValue
            e.Row.Cells(0).Text = (e.Row.RowIndex + 1)
            Dim chk As CheckBox = e.Row.FindControl("chkSelect")

            Dim lbl As Label = e.Row.FindControl("lblPendingLoad")
            Dim txtDropLoad As TextBox = e.Row.FindControl("txtDropLoad")


            chk.Attributes.Add("onchange", "return QTYLockUnlock('" + chk.ClientID + "','" + txtDropLoad.ClientID + "')")
            txtDropLoad.Attributes.Add("onkeypress", "return isIntegerNumberKey(this, event);")
            'txt.Attributes.Add("onKeyPress", "KeyPressNumeric();")
            'txt.Attributes.Add("onblur", "CheckMaxLimit('" + txt.ClientID + "','" + lbl.ClientID + "');")
            'chk.Attributes.Add("onClick", "RowCheck('" & chk.ClientID & "','" & txt.ClientID & "','" & txtLotNo.ClientID & "');")

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            'If rowView("desph_approved_yn").ToString = "Y" Then
            '    btnGo.Enabled = False
            'Else
            '    btnGo.Enabled = True
            'End If


            'If Updatemode = True Then
            '    If rowView("skuExist") = "Y" Then
            '        chk.Checked = True
            '        txt.Enabled = True
            '        txt.Text = rowView("this_Despatch").ToString
            '    End If
            '    If CType(rowView("pendingLoad"), Integer) < 0 Then
            '        txt.Text = "0"
            '        e.Row.Cells(7).Text = "0"
            '    End If
            'Else
            '    If CType(rowView("pendingLoad"), Integer) < 0 Then
            '        txt.Text = "0"
            '        e.Row.Cells(7).Text = "0"
            '    End If
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

    Private Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepot()
        PopulateProduct()
    End Sub

    Private Sub ddlDepot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepot.SelectedIndexChanged
        PopulateProduct()
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        btnSubmit.Enabled = False
        CheckLogin()
        Dim Obj As New LoadDropAddUpdateClass
        'Dim hdrEntity As New DespatchHeaderEntity
        Dim numRowsAffected, numRowsAffected1, GetChallanId As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction
        Try



            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            Dim UnitCode As String = ddlUnit.SelectedValue
            Dim ProcessMonth As String = ddlProcessMnth.SelectedValue
            Dim ProcessYear As String = ddlProcessYr.SelectedValue


            numRowsAffected = Obj.InsertLoadDroppHeader(sqlConn, sqlTrans, UnitCode, ProcessYear, ProcessMonth, userInfo.userIDEntity)

            If (numRowsAffected > 0) Then

                Dim hdnUom, hdnVol, hdnDay, hdnLineNum, hdnDepot, hdnUnitCode, hdnSKUCode As HiddenField
                Dim chk As CheckBox
                Dim txtLot, txtDropLoad As TextBox
                Dim total As Integer = 0

                For i As Integer = 0 To gvSKUDetails.Rows.Count - 1

                    chk = gvSKUDetails.Rows(i).FindControl("chkSelect")
                    If chk.Checked = True Then

                        hdnSKUCode = gvSKUDetails.Rows(i).FindControl("hdnSKUCode")
                        txtDropLoad = gvSKUDetails.Rows(i).FindControl("txtDropLoad")
                        ' hdnVol = gvSKUDetails.Rows(i).FindControl("hdnVol")
                        'hdnDay = gvSKUDetails.Rows(i).FindControl("hdnTransitDay")
                        'hdnLineNum = gvSKUDetails.Rows(i).FindControl("hdnLineNum")
                        hdnDepot = gvSKUDetails.Rows(i).FindControl("hdnDepotCode")
                        hdnUnitCode = gvSKUDetails.Rows(i).FindControl("hdnUnitCode")

                        Dim lblPendingLoad As Label = gvSKUDetails.Rows(i).FindControl("lblPendingLoad")

                        numRowsAffected1 = Obj.InsertDespatchDetail(sqlConn, sqlTrans, numRowsAffected, hdnDepot.Value, hdnSKUCode.Value, txtDropLoad.Text, userInfo.userIDEntity, ProcessYear, ProcessMonth, hdnUnitCode.Value)

                    End If

                Next
                If numRowsAffected1 > 0 Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "message", "alert('Records have been upload successfully.')", True)
                    sqlTrans.Commit()
                Else
                    sqlTrans.Rollback()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "message", "alert('Something went wrong.')", True)
                End If

            End If

        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            '  Server.Transfer(returnUrl)
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally
            If Not (sqlConn Is Nothing) Then
                'sqlConn is set to close state after completing the transaction
                sqlTrans.Dispose()
                sqlConn.Close()

            End If
            BindGrid()
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/LoadDropList.aspx")

    End Sub


End Class
