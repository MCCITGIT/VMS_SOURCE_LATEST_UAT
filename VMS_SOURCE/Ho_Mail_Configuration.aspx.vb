Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Partial Class Ho_Mail_Configuration
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.MaintainScrollPositionOnPostBack = True
        CheckLogin()
        If (Not IsPostBack) Then
            PopulateName(ddlName)
            HoMailMstrList()
            PopulateActive(ddlActive_norc)
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
        btnInsert.Attributes.Add("onClick", "return ValidateHoMail('" & txtName_norc.ClientID & "', '" & txtMialId_norc.ClientID & "', '" & ddlActive_norc.ClientID & "', '" & btnInsert.ClientID & "');")
    End Sub
#End Region
#Region "HoMailMstrList"
    Private Sub HoMailMstrList()
        Dim homailOBJ As New HoMailMstrClass
        Dim ds As DataSet
        ds = homailOBJ.GetHoMailMstrList(ddlName.SelectedValue)

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
#Region "NameList"
    Private Sub PopulateName(ByVal ddl As DropDownList)
        Dim depotmailOBJ As New HoMailMstrClass
        Dim ds As DataSet
        ddl.Items.Clear()
        ds = depotmailOBJ.GetNameList()
        Try
            If (Not (ds Is Nothing) AndAlso ds.Tables.Count > 0) Then
                If (Not (ds.Tables(0) Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0) Then
                    ddl.DataSource = ds
                    ddl.DataTextField = "ho_name"
                    ddl.DataValueField = "ho_name"
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
    Private Sub PopulateActive(ByVal actv As DropDownList)

        actv.Items.Clear()

        Try
            actv.Items.Add(New ListItem("Yes", "Y"))
            actv.Items.Add(New ListItem("No", "N"))

            'actv.SelectedValue = "Y"

            actv.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub

#End Region
    Protected Sub gvDepotMail_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvDepotMail.RowCancelingEdit
        Try
            gvDepotMail.EditIndex = -1
            HoMailMstrList()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gvDepotMail_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvDepotMail.RowEditing
        gvDepotMail.EditIndex = e.NewEditIndex
        HoMailMstrList()
    End Sub
    Protected Sub btnInsert_Click(sender As Object, e As ImageClickEventArgs) Handles btnInsert.Click

        Dim Numrowsaffected As Integer
        Dim depotmailOBJ As New HoMailMstrClass
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            Numrowsaffected = depotmailOBJ.Insert_HoMail(txtName_norc.Text, txtMialId_norc.Text, ddlActive_norc.SelectedValue, userInfo.userIDEntity, sqlConn, sqlTrans)

            If Numrowsaffected > 0 Then
                sqlTrans.Commit()
                HoMailMstrList()
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
        HoMailMstrList()
    End Sub
    Protected Sub gvDepotMail_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim Numrowsaffected As Integer
        Dim depotmailOBJ As New HoMailMstrClass

        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        CheckLogin()

        If e.CommandName = "insert" Then

            Try

                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                Dim footerRow As GridViewRow = gvDepotMail.FooterRow

                Dim name As String = CType(footerRow.FindControl("txtName_ftr"), TextBox).Text
                Dim mail As String = CType(footerRow.FindControl("txtMialId_ftr"), TextBox).Text
                Dim active As String = CType(footerRow.FindControl("ddlActive_ftr"), DropDownList).SelectedValue


                Numrowsaffected = depotmailOBJ.Insert_HoMail(name, mail, active, userInfo.userIDEntity, sqlConn, sqlTrans)

                If Numrowsaffected > 0 Then
                    sqlTrans.Commit()
                    HoMailMstrList()
                    lblErrorMessage.Visible = False
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
            HoMailMstrList()
        End If
    End Sub

    Protected Sub gvDepotMail_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim Numrowsaffected As Integer
        Dim depotmailOBJ As New HoMailMstrClass
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing

        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            Dim row As GridViewRow = gvDepotMail.Rows(e.RowIndex)

            Dim name As String = CType(row.FindControl("txtName"), TextBox).Text
            Dim email As String = CType(row.FindControl("txtMailId"), TextBox).Text
            Dim active As String = CType(row.FindControl("ddlActive"), DropDownList).SelectedValue

            Numrowsaffected = depotmailOBJ.Insert_HoMail(name, email, active, userInfo.userIDEntity, sqlConn, sqlTrans)

            If Numrowsaffected > 0 Then
                sqlTrans.Commit()
                gvDepotMail.EditIndex = -1
                HoMailMstrList()
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

            Dim ddlActive As DropDownList = CType(e.Row.FindControl("ddlActive"), DropDownList)
            'Dim lblRegion As Label = CType(e.Row.FindControl("lblRegion"), Label)
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            If ddlActive IsNot Nothing Then
                PopulateActive(ddlActive)
                ddlActive.SelectedValue = rowView("active_status")
            End If

            Dim name As TextBox = CType(e.Row.FindControl("txtName"), TextBox)
            Dim mail As TextBox = CType(e.Row.FindControl("txtMailId"), TextBox)
            Dim btninsert As LinkButton = CType(e.Row.FindControl("btnUpdate"), LinkButton)

            If ddlActive IsNot Nothing Then
                Dim script As String = "return ValidateHoMail('" & name.ClientID & "', '" & mail.ClientID & "', '" & ddlActive.ClientID & "', '" & btninsert.ClientID & "');"

                btninsert.Attributes.Add("onclick", script)
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            Dim ddlActiveftr As DropDownList = CType(e.Row.FindControl("ddlActive_ftr"), DropDownList)

            If ddlActiveftr IsNot Nothing Then
                PopulateActive(ddlActiveftr)
            End If

            Dim name As TextBox = CType(e.Row.FindControl("txtName_ftr"), TextBox)
            Dim mail As TextBox = CType(e.Row.FindControl("txtMialId_ftr"), TextBox)
            Dim active As DropDownList = CType(e.Row.FindControl("ddlActive_ftr"), DropDownList)
            Dim btninsert As LinkButton = CType(e.Row.FindControl("btnInsert"), LinkButton)

            Dim script As String = "return ValidateHoMail('" & name.ClientID & "', '" & mail.ClientID & "', '" & active.ClientID & "', '" & btninsert.ClientID & "');"

            btninsert.Attributes.Add("onclick", script)
        End If
    End Sub
    Protected Sub ddlName_SelectedIndexChanged(sender As Object, e As EventArgs)
        HoMailMstrList()
    End Sub
End Class
