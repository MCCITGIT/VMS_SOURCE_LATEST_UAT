
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports VMS.Web

Partial Class Load_Mstr_Unmapped_Vendor_SKU
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim sr As StreamReader
    Dim linerd As Char()
    Dim filesavestrt As String
    Dim filesaveend As String
    Dim saveLocation As String
    Dim D1, D2 As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckLogin()
            'GetScreenDetails()
            'PageSizeDropdown()
            'EstimationDataAndStockCheck()
            GetUnmappedSKUDetails()
        End If
    End Sub
#Region "Login Check"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region

#Region "Get Unmapped Vendor Details"
    Private Sub GetUnmappedSKUDetails()
        CheckLogin()
        Dim ScreenDS As DataSet
        Dim LoadObj As New CreateLoadMasterClass
        ScreenDS = LoadObj.GetUnmappedVendor(userInfo.userIDEntity)
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            gvNotFound.DataSource = ScreenDS
            gvNotFound.DataBind()
            'Label4.Visible = True
            'ddlPageSize.Visible = True
            'lblNotFound.Text = "Vendor Unit not Found for Following Cases"
        Else
            gvNotFound.Visible = False
            'Label4.Visible = False
            'ddlPageSize.Visible = False
            'lblNotFound.Text = ""
        End If
    End Sub
#End Region

    Protected Sub gvNotFound_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Find the dropdown
            Dim ddlVendor As DropDownList = CType(e.Row.FindControl("ddlVendor"), DropDownList)
            Dim hdnDepotCode As HiddenField = CType(e.Row.FindControl("hdnDepotCode"), HiddenField)


            Dim ScreenDS As DataSet
            Dim LoadObj As New IndentMaster
            ScreenDS = LoadObj.GetVendorList(hdnDepotCode.Value)
            If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
                ddlVendor.DataSource = ScreenDS.Tables(0)
                ddlVendor.DataTextField = "vendor_name"
                ddlVendor.DataValueField = "vendor_code"
                ddlVendor.DataBind()
            Else
                ddlVendor.Enabled = False
                'gvNotFound.Visible = False
                'Label4.Visible = False
                'ddlPageSize.Visible = False
                'lblNotFound.Text = ""
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        CheckLogin()
        Dim LoadObj As New CreateLoadMasterClass
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction
        For Each row As GridViewRow In gvNotFound.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim lblYear As Label = CType(row.FindControl("lblYear"), Label)
                Dim lblMonth As Label = CType(row.FindControl("lblMonth"), Label)
                Dim lblDepot As Label = CType(row.FindControl("lblDepot"), Label)
                Dim hdnDepotCode As HiddenField = CType(row.FindControl("hdnDepotCode"), HiddenField)
                Dim lblSKU As Label = CType(row.FindControl("lblSKU"), Label)
                Dim lblAvg As Label = CType(row.FindControl("lblAvg"), Label)
                Dim lblEst As Label = CType(row.FindControl("lblEst"), Label)
                Dim ddlVendor As DropDownList = CType(row.FindControl("ddlVendor"), DropDownList)
                Dim txtRemarks As TextBox = CType(row.FindControl("txtRemarks"), TextBox)

                If Not String.IsNullOrEmpty(ddlVendor.SelectedValue) AndAlso
                   Not String.IsNullOrEmpty(txtRemarks.Text) Then
                    numRowsAffected = LoadObj.Insert_Mapped_Vendor_SKU(sqlConn, sqlTrans, lblYear.Text, lblMonth.Text, hdnDepotCode.Value, ddlVendor.SelectedValue, txtRemarks.Text, lblSKU.Text, userInfo.userIDEntity)
                End If
            End If
        Next

    End Sub
End Class
