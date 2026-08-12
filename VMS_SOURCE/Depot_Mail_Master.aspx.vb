Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class Depot_Mail_Master
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.MaintainScrollPositionOnPostBack = True
        CheckLogin()
        If (Not IsPostBack) Then
            PopulateDepot(ddlDepot, String.Empty)
            PopulateDepot(ddldepot_norc, String.Empty)
            PopulateRegion(ddlrgn_norc)
            DepotMailMstrList()
        End If
        AddAttributes()
    End Sub
#End Region
#Region "Check Login"
    Private Sub CheckLogin()

        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

    End Sub
#End Region
#Region "AddAttributes"
    Private Sub AddAttributes()
        btnInsert.Attributes.Add("onClick", "return ValidateDepotMail('" & ddlrgn_norc.ClientID & "', '" & ddldepot_norc.ClientID & "', '" & txtemail.ClientID & "', '" & btnInsert.ClientID & "');")
    End Sub
#End Region
#Region "DepotMailMstrList"
    Private Sub DepotMailMstrList()
        Dim depotmailOBJ As New DepotMailMstrClass
        Dim ds As DataSet
        ds = depotmailOBJ.GetDEpotMailMstrList(ddlDepot.SelectedValue)

        If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
            If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                gvDepotMail.DataSource = ds
                gvDepotMail.DataBind()
                Div_Lov_Mstr_Grid.Visible = False
            Else
                gvDepotMail.DataSource = Nothing
                gvDepotMail.DataBind()
                Div_Lov_Mstr_Grid.Visible = True
            End If
        End If
    End Sub
#End Region
#Region "DepotList"
    Private Sub PopulateDepot(ByVal ddl As DropDownList, ByVal regn As String)
        Dim depotmailOBJ As New DepotMailMstrClass
        Dim ds As DataSet
        ddl.Items.Clear()
        ds = depotmailOBJ.GetDepotList(regn)
        Try
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddl.DataSource = ds
                    ddl.DataTextField = "Depot_Name"
                    ddl.DataValueField = "Depot_code"
                    ddl.DataBind()
                    ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                Else
                    ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try
    End Sub
#End Region

#Region "RegionList"
    Private Sub PopulateRegion(ByVal rgn As DropDownList)
        Dim depotmailOBJ As New DepotMailMstrClass
        Dim ds As DataSet
        rgn.Items.Clear()
        ds = depotmailOBJ.GetRegionList()
        Try
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    rgn.DataSource = ds
                    rgn.DataTextField = "depot_regn"
                    rgn.DataValueField = "depot_regn"
                    rgn.DataBind()
                    rgn.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                Else
                    rgn.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If
            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try
    End Sub
#End Region
    Protected Sub ddlDepot_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepot.SelectedIndexChanged
        DepotMailMstrList()
    End Sub
    Protected Sub gvDepotMail_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvDepotMail.RowCancelingEdit
        Try
            gvDepotMail.EditIndex = -1
            DepotMailMstrList()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gvDepotMail_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvDepotMail.RowEditing
        gvDepotMail.EditIndex = e.NewEditIndex
        DepotMailMstrList()
    End Sub
    Protected Sub btnInsert_Click(sender As Object, e As ImageClickEventArgs) Handles btnInsert.Click

        Dim Numrowsaffected As Integer
        Dim depotmailOBJ As New DepotMailMstrClass
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            Numrowsaffected = depotmailOBJ.Insert_DepotMail(ddlrgn_norc.SelectedValue, ddldepot_norc.SelectedValue, txtemail.Text, userInfo.userIDEntity, sqlConn, sqlTrans)

            If Numrowsaffected > 0 Then
                sqlTrans.Commit()
                DepotMailMstrList()
            Else
                sqlTrans.Rollback()
                lblErrorMessage.Text = "Error inserting data!"
                lblErrorMessage.Visible = True
            End If

        Catch ex As Exception
            sqlTrans.Rollback()
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally

            sqlConn.Close()
        End Try

    End Sub

    Protected Sub gvDepotMail_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvDepotMail.PageIndex = e.NewPageIndex
        DepotMailMstrList()
    End Sub
    Protected Sub gvDepotMail_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim Numrowsaffected As Integer
        Dim depotmailOBJ As New DepotMailMstrClass

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        CheckLogin()

        If e.CommandName = "insert" Then

            Try

                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                Dim footerRow As GridViewRow = gvDepotMail.FooterRow

                Dim region As String = CType(footerRow.FindControl("ddlregion_ftr"), DropDownList).SelectedValue
                Dim depotCode As String = CType(footerRow.FindControl("ddldepot_ftr"), DropDownList).SelectedValue
                Dim mailId As String = CType(footerRow.FindControl("txtemail"), TextBox).Text


                Numrowsaffected = depotmailOBJ.Insert_DepotMail(region, depotCode, mailId, userInfo.userIDEntity, sqlConn, sqlTrans)

                If Numrowsaffected > 0 Then
                    sqlTrans.Commit()
                    DepotMailMstrList()
                Else
                    sqlTrans.Rollback()
                    lblErrorMessage.Text = "Error inserting data!"
                    lblErrorMessage.Visible = True
                End If
            Catch ex As Exception
                sqlTrans.Rollback()
                Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
                HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")

            Finally

                sqlConn.Close()
            End Try


        End If

        If e.CommandName = "edit" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            gvDepotMail.EditIndex = index
            DepotMailMstrList()
        End If
    End Sub

    Protected Sub gvDepotMail_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim Numrowsaffected As Integer
        Dim depotmailOBJ As New DepotMailMstrClass
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            Dim row As GridViewRow = gvDepotMail.Rows(e.RowIndex)

            Dim region As String = CType(row.FindControl("lblRegion"), Label).Text
            Dim depotCode As String = CType(row.FindControl("hdndepot"), HiddenField).Value.ToString()
            Dim mailId As String = CType(row.FindControl("txtemail"), TextBox).Text

            Numrowsaffected = depotmailOBJ.Insert_DepotMail(region, depotCode, mailId, userInfo.userIDEntity, sqlConn, sqlTrans)

            If Numrowsaffected > 0 Then
                sqlTrans.Commit()
                gvDepotMail.EditIndex = -1
                DepotMailMstrList()
            Else
                sqlTrans.Rollback()
                lblErrorMessage.Text = "Error updating data!"
                lblErrorMessage.Visible = True
            End If
        Catch ex As Exception
            sqlTrans.Rollback()
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally

            sqlConn.Close()
        End Try

    End Sub

    Protected Sub gvDepotMail_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ddlRegion As DropDownList = CType(e.Row.FindControl("ddlregion"), DropDownList)
            Dim lblRegion As Label = CType(e.Row.FindControl("lblRegion"), Label)
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            If ddlRegion IsNot Nothing Then
                PopulateRegion(ddlRegion)
                ddlRegion.SelectedValue = rowView("Region")
            End If

            Dim ddlDepot As DropDownList = CType(e.Row.FindControl("ddldepot_grd"), DropDownList)
            Dim lblDepot As Label = CType(e.Row.FindControl("lblDepotCode"), Label)
            If ddlDepot IsNot Nothing Then
                PopulateDepot(ddlDepot, String.Empty)
                ddlDepot.SelectedValue = rowView("Depot_Code")
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            Dim ddlRegionftr As DropDownList = CType(e.Row.FindControl("ddlregion_ftr"), DropDownList)

            If ddlRegionftr IsNot Nothing Then
                PopulateRegion(ddlRegionftr)
            End If

            Dim ddlDepotftr As DropDownList = CType(e.Row.FindControl("ddldepot_ftr"), DropDownList)

            If ddlDepotftr IsNot Nothing Then
                PopulateDepot(ddlDepotftr, String.Empty)
            End If

            Dim txtEmail As TextBox = CType(e.Row.FindControl("txtemail"), TextBox)
            Dim btninsert As LinkButton = CType(e.Row.FindControl("btnInsert"), LinkButton)

            Dim script As String = "return ValidateDepotMail('" & ddlRegionftr.ClientID & "', '" & ddlDepotftr.ClientID & "', '" & txtEmail.ClientID & "', '" & btninsert.ClientID & "');"

            btninsert.Attributes.Add("onclick", script)
        End If
    End Sub
    Protected Sub ddlregion_ftr_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim footerRow As GridViewRow = gvDepotMail.FooterRow

        Dim ddlDepot As DropDownList = CType(footerRow.FindControl("ddldepot_ftr"), DropDownList)

        Dim selectedRegion As String = CType(sender, DropDownList).SelectedValue

        PopulateDepot(ddlDepot, selectedRegion)
    End Sub
    Protected Sub ddlrgn_norc_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateDepot(ddldepot_norc, ddlrgn_norc.SelectedValue)
    End Sub
End Class
