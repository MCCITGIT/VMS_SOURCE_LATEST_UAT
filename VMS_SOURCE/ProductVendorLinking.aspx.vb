Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class ProductVendorLinking
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()


#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckLogin()
        Page.MaintainScrollPositionOnPostBack = True

        If (Not IsPostBack) Then
            AddAttributes()
            PopulateVendor()
            ddlVendor_SelectedIndexChanged(sender, e)

        End If
    End Sub
#End Region
#Region "AddAttributes"

    Private Sub AddAttributes()
        btnSubmit.Attributes.Add("onclick", "return validateVendorProductLinkAdd();")
    End Sub
#End Region
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#Region "PopulateVendor"
    Public Sub PopulateVendor()
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
        'ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub
#End Region
    Public Sub PopulateProductNameByVendorId()
        CheckLogin()
        chkbxListApplProducts.Items.Clear()
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet

        DS = Obj.GetProductByVendorId(ddlVendor.SelectedValue)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            chkbxListApplProducts.DataSource = DS.Tables(0)
            chkbxListApplProducts.DataTextField = "product_name"
            chkbxListApplProducts.DataValueField = "product_code"
            chkbxListApplProducts.DataBind()
            'chkbxListLocation.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If

    End Sub
    Protected Sub ddlVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVendor.SelectedIndexChanged
        'PopulateProductNameByVendorId()
        PopulateEditMode()
        lblErrorMessage.Text = ""
    End Sub
#Region "Btn Submit Event"
#End Region
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim RowsAffectedMstr As Integer
        Dim obj As New QualityControlClass


        ''---------------------
        Try
            Dim NoRowsAffected As Integer = 0
            Dim Vendor As String = String.Empty
            If ddlVendor.SelectedIndex > 0 Then
                Vendor = Convert.ToString(ddlVendor.SelectedValue)
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please select Vendor."
                Return
            End If


            Dim dt1 As DataTable = New DataTable()
            Dim dr1 As DataRow
            dt1.Columns.Add(New System.Data.DataColumn("bpl_product_id", GetType(String)))
            For Each sku As ListItem In chkbxListApplProducts.Items
                If sku.Selected = True Then
                    dr1 = dt1.NewRow()
                    'dr1.Item("bpl_product_id") = Convert.ToString(Vendor)
                    dr1.Item("bpl_product_id") = Convert.ToString(sku.Value.ToString().Split("|")(0))
                    'dr1.Item("abr_product_desc") = Convert.ToString(sku.Value.ToString().Split("|")(0))
                    dt1.Rows.Add(dr1)
                End If
            Next
            dt1.AcceptChanges()
            If dt1.Rows.Count > 0 Then
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "select atleast one Product."
                Return
            End If


            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then

                RowsAffectedMstr = obj.InsertVendorproductLink(ddlVendor.SelectedValue, userInfo.userIDEntity, dt1)
                If RowsAffectedMstr > 0 Then
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green
                    lblErrorMessage.Text = "Submitted Successfully"
                    chkbxListApplProducts.ClearSelection()
                    ddlVendor.ClearSelection()
                Else
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    lblErrorMessage.Text = "Something went wrong. Try again."

                End If
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "select atleast one Product and Vendor ."

            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        Finally

        End Try
        ''----------------------
    End Sub

#Region "Populate Edit Mode Product"
    Private Sub PopulateEditMode()
        CheckLogin()
        chkbxListApplProducts.Items.Clear()
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet

        DS = Obj.GetProductByVendorId(ddlVendor.SelectedValue)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            chkbxListApplProducts.DataSource = DS.Tables(0)
            chkbxListApplProducts.DataTextField = "product_name"
            chkbxListApplProducts.DataValueField = "product_code"
            chkbxListApplProducts.DataBind()



            For Each lstitm As ListItem In chkbxListApplProducts.Items
                For Each row As DataRow In DS.Tables(0).Rows
                    Dim tmpMR_Product As String = row.Item("check")
                    Dim tmpMR_Productd As String = row.Item("product_code")
                    'Dim tmpMR_Product As String = DS.Tables(0).Rows(0)("check").ToString.Split(",")

                    If tmpMR_Product = "Y" And lstitm.Value = tmpMR_Productd Then
                        If lstitm.Selected = True Then
                            Return
                        Else
                            lstitm.Selected = True
                        End If

                    End If

                Next row
            Next

            'chkbxListLocation.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        End If

    End Sub
#End Region

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx", True)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Dim path = "~/ProductVendorLinking.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub

End Class
