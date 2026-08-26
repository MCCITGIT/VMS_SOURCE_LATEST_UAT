
Imports System.Data
Imports VMS.Web

Partial Class QC_ChemicalProductLiking
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then
            populateProduct()
            ddlproduct_SelectedIndexChanged(sender, e)
            AddAttributes()
            ' ChemicalDetailsList()
            'If Not (Request.QueryString.Count = 0) Then
            '    txtBrandName.Text = Request.QueryString("BrandName").ToString()
            '    btnSubmit.Text = "Update"
            'End If
            'btnSubmit.Attributes.Add("onclick", "return validateBrandListAdd();")
        End If

    End Sub
#Region "AddAttributes"
    Private Sub AddAttributes()
        btnSubmit.Attributes.Add("onclick", "return validateBrandProductLinkAdd();")
    End Sub
#End Region

#Region "PopulateProduct"
    Public Sub populateProduct()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet
        DS = Obj.GetChemicalProduct()
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlproduct.DataSource = DS.Tables(0)
            ddlproduct.DataTextField = "product_name"
            ddlproduct.DataValueField = "product_code"
            ddlproduct.DataBind()
            ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            If DS.Tables(0).Rows.Count = 1 Then
                ddlproduct.SelectedIndex = 1
                ddlproduct.Enabled = False
            End If
        End If
        'ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
    End Sub
#End Region



    Private Sub PopulateEditMode()
        ' CheckLogin()
        'chkbxListApplProducts.Items.Clear()
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet

        DS = Obj.GetProductWiseChemical(ddlproduct.SelectedValue)
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            gvChemicalList.DataSource = DS.Tables(0)
            gvChemicalList.DataBind()
        End If
        'chkbxListApplProducts.DataSource = DS.Tables(0)
        'chkbxListApplProducts.DataTextField = "product_name"
        'chkbxListApplProducts.DataValueField = "product_code"
        'chkbxListApplProducts.DataBind()
        'For Each row As DataRow In DS.Tables(0).Rows
        '    'Dim tmpMR_Product As String = row.Item("check")

        '    Dim tmpMR_Product() As String = DS.Tables(0).Rows(0)("check").ToString.Split(",")
        '    For Each lstitm As ListItem In chkbxListApplProducts.Items
        '        If lstitm.Value <> String.Empty AndAlso Array.IndexOf(tmpMR_Product,) = "Y" Then
        '            If lstitm.Selected = True Then
        '                Return
        '            Else
        '                lstitm.Selected = True
        '            End If
        '        End If
        '    Next
        'Next row


        'For Each lstitm As ListItem In chkbxListApplProducts.Items
        '    For Each row As DataRow In DS.Tables(0).Rows
        '        Dim tmpMR_Product As String = row.Item("check")
        '        Dim tmpMR_Productd As String = row.Item("product_code")
        '        'Dim tmpMR_Product As String = DS.Tables(0).Rows(0)("check").ToString.Split(",")

        '        If tmpMR_Product = "Y" And lstitm.Value = tmpMR_Productd Then
        '            If lstitm.Selected = True Then
        '                Return
        '            Else
        '                lstitm.Selected = True
        '            End If

        '        End If

        '    Next row
        'Next


        'Dim j As Integer = 0
        'For j = 0 To Me.chkbxListApplProducts.Items.Count - 1
        '    chkbxListApplProducts.Items.=
        '    For Each row As DataRow In DS.Tables(0).Rows
        '        Dim item = CType(Me.chkbxListApplProducts.Items(j))
        '        Me.chkbxListApplProducts.se
        '        If Array.IndexOf(chkbxListApplProducts.Items, j) = row.Item("product_name") And row.Item("check") = "Y" Then
        '            chkbxListApplProducts.
        '        End If
        '    Next row
        'Next

        'chkbxListLocation.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        ' End If

    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim RowsAffectedMstr As Integer
        Dim obj As New QualityControlClass
        ''---------------------
        Try
            Dim NoRowsAffected As Integer = 0
            Dim productcode As String = String.Empty
            Dim Dosage As Decimal = 0
            If ddlproduct.SelectedIndex > 0 Then
                productcode = Convert.ToString(ddlproduct.SelectedValue)
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "Please select Product."
                Return
            End If

            Dim dt1 As DataTable = New DataTable()
            Dim dr1 As DataRow
            dt1.Columns.Add(New System.Data.DataColumn("bpl_chemical_id", GetType(Int32)))
            dt1.Columns.Add(New System.Data.DataColumn("bpl_dosage", GetType(Decimal)))

            If (gvChemicalList.Rows.Count > 0) Then
                For index = 0 To gvChemicalList.Rows.Count - 1
                    Dim row As GridViewRow = gvChemicalList.Rows(index)
                    Dim checkselect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                    If (checkselect.Checked = True) Then
                        Dim hdnchecmicalid As HiddenField = CType(row.FindControl("hdnchecmicalid"), HiddenField)
                        Dim lblchemical As Label = CType(row.FindControl("lblchemical"), Label)
                        Dim txtDosage As TextBox = CType(row.FindControl("txtDosage"), TextBox)

                        If String.IsNullOrEmpty(txtDosage.Text) Then
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please Enter Dosage Value');", True)
                            Return
                        End If
                        dt1.Rows.Add(hdnchecmicalid.Value, txtDosage.Text)
                    End If
                Next
            End If


            'If txtDosage.Text <> "" Then
            '    Dosage = Convert.ToDecimal(txtDosage.Text)
            'Else
            '    lblErrorMessage.ForeColor = System.Drawing.Color.Red
            '    lblErrorMessage.Text = "Please Enter Dosage Value"
            '    Return
            'End If



            'For Each sku As ListItem In chkbxListApplProducts.Items
            '    If sku.Selected = True Then
            '        dr1 = dt1.NewRow()
            '        'dr1.Item("bpl_product_id") = Convert.ToString(Brand)
            '        dr1.Item("bpl_product_id") = Convert.ToString(sku.Value.ToString().Split("|")(0))
            '        dr1.Item("bpl_dosage") = Dosage
            '        dt1.Rows.Add(dr1)
            '    End If
            'Next
            dt1.AcceptChanges()
            If dt1.Rows.Count > 0 Then
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "select atleast one Chemical."
                Return
            End If

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then

                RowsAffectedMstr = obj.InsertChemicalproductLink(ddlproduct.SelectedValue, userInfo.userIDEntity, dt1)
                If RowsAffectedMstr > 0 Then
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green
                    lblErrorMessage.Text = "Submitted Successfully"

                Else
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red
                    lblErrorMessage.Text = "Something went wrong. Try again."

                End If
            Else
                lblErrorMessage.ForeColor = System.Drawing.Color.Red
                lblErrorMessage.Text = "select atleast one Brand and Product ."

            End If

        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            Response.Redirect(returnUrl)
        Finally

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx", True)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Dim path = "~/BrandProductLinking.aspx"

        If Not (Request.QueryString.Count = 0) Then
            path += Request.Url.Query
            Response.Redirect(path)
        Else
            Response.Redirect(path)
        End If

    End Sub
    Protected Sub ddlproduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateEditMode()
    End Sub
    Protected Sub gvChemicalList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim status As String = drv("check").ToString()
            Dim chkSelect As CheckBox = CType(e.Row.FindControl("chkSelect"), CheckBox)
            If (status = "Y") Then
                chkSelect.Enabled = False
                chkSelect.Checked = True
            End If
        End If
    End Sub
End Class
