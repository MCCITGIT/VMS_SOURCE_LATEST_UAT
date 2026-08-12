Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports VMS.Web
Partial Class FactoryApplicableVendor
    Inherits System.Web.UI.Page
#Region "Page_Load Event"
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        CheckLogin()
        If Not IsPostBack Then
            PopulateFactory()
            PopulateVendor()
        End If
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

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("Home.aspx")
    End Sub

#Region "Populate Vendor dropdown."

    Private Sub PopulateFactory()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New VRSFactoryClass()
        Dim ds As DataSet

        Try

            ds = obj.GetFactoryDetails()

            If Not (ds Is Nothing) Then

                If Not (ds.Tables(0).Rows.Count = 0) Then

                    ddlFactory.DataSource = ds
                    ddlFactory.DataTextField = "factory_name"
                    ddlFactory.DataValueField = "factory_code"
                    ddlFactory.DataBind()

                    ddlFactory.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))

                    If ds.Tables(0).Rows.Count = 1 Then
                        ddlFactory.SelectedIndex = 1
                        ddlFactory.Enabled = False
                    End If

                Else
                    ddlFactory.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

    Protected Sub ddlFactory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFactory.SelectedIndexChanged
        PopulateVendor()
    End Sub

#Region "Populate Vendor dropdown."

    Private Sub PopulateVendor()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim obj As New VRSFactoryClass()
        Dim ds As DataSet

        Try
            ds = obj.GetVendorDetails(ddlFactory.SelectedValue)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ds.Tables(0).DefaultView.Sort = "is_assigned DESC"
                Dim sortedTable As DataTable = ds.Tables(0).DefaultView.ToTable()

                cblVendors.DataSource = sortedTable
                cblVendors.DataTextField = "vendor_name"
                cblVendors.DataValueField = "vendor_code"
                cblVendors.DataBind()

                For Each row As DataRow In ds.Tables(0).Rows
                    If row("is_assigned").ToString() = "1" Then
                        Dim item As ListItem = cblVendors.Items.FindByValue(row("vendor_code").ToString())
                        If item IsNot Nothing Then
                            item.Selected = True
                        End If
                    End If
                Next

                vendorRow.Visible = True

            Else
                cblVendors.Items.Clear()
                vendorRow.Visible = False
            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        End Try
    End Sub

#End Region

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If String.IsNullOrEmpty(ddlFactory.SelectedValue) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select a factory');", True)
            Return
        End If

        Dim isAnyChecked As Boolean = False
        For Each item As ListItem In cblVendors.Items
            If item.Selected Then
                isAnyChecked = True
                Exit For
            End If
        Next

        If Not isAnyChecked Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select at least one vendor');", True)
            Return
        End If
        Dim dt As New DataTable()
        Dim count As Integer = 0


        dt.Columns.Add("VendorId", GetType(String))

        For Each item As ListItem In cblVendors.Items
            If item.Selected Then
                Dim dr As DataRow = dt.NewRow()
                dr("VendorId") = item.Value
                dt.Rows.Add(dr)
            End If
        Next

        If dt.Rows.Count > 0 Then
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            Dim obj As New VRSFactoryClass

            Dim RecordInserted As Integer
            Dim status As String = String.Empty
            Dim flag As Boolean = False
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                RecordInserted = obj.SubmitFactoryDetails(ddlFactory.SelectedValue, userInfo.userIDEntity, dt, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    sqlTrans.Commit()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submitted successfully.');", True)
                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record submission Failed!');", True)
                End If
            Catch ex As Exception
                If (sqlTrans IsNot Nothing) Then
                    sqlTrans.Rollback()
                End If
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('" & ex.Message.ToString() & "');", True)
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
                PopulateVendor()
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('There is no parameter details');", True)
        End If


    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        PopulateVendor()
    End Sub

End Class
