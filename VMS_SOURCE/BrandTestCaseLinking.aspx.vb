Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess

Partial Class BrandTestCaseLinking
    Inherits System.Web.UI.Page

#Region "Page Load Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.MaintainScrollPositionOnPostBack = True

        If (Not IsPostBack) Then
            AddAttributes()
            populateBrandList()
            'populateTestList(ddlTest)
            'RetrieveSearchCriteria()
            BrandTestLinkingListLoad()
        End If
    End Sub
#End Region

#Region "AddAttributes"
    Private Sub AddAttributes()

        btnInsert.Attributes.Add("onClick", "return ValidateLDLdivControls();")
        txtSeq.Attributes.Add("OnKeyPress", "KeyPressNumeric()")

    End Sub
#End Region

#Region "Save Search Criteria"
    ' Saves the current search criteria in session
    Private Sub SaveSearchCriteria()
        Dim LovDetailsSearchInfo As New LovDetailsSearchCriteria
        LovDetailsSearchInfo.LovType = ddlBrand.SelectedValue

        Session(Constant.SessionKeys.LovDetailsSearchInfo) = LovDetailsSearchInfo


    End Sub
    Private Sub RetrieveSearchCriteria()
        If (Not (Session(Constant.SessionKeys.LovDetailsSearchInfo) Is Nothing)) Then
            Dim LovDetailsSearchInfo As New LovDetailsSearchCriteria
            LovDetailsSearchInfo = Session(Constant.SessionKeys.LovDetailsSearchInfo)
            ddlBrand.SelectedValue = LovDetailsSearchInfo.LovType
        End If

    End Sub
#End Region

#Region "Populate Dropdown"
    Public Sub populateBrandList()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet
        DS = Obj.Getbrand()
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlBrand.DataSource = DS.Tables(0)
            ddlBrand.DataTextField = "brand_name"
            ddlBrand.DataValueField = "brand_id"
            ddlBrand.DataBind()
            If Not DS.Tables(0).Rows.Count = 1 Then
                ddlBrand.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        End If
    End Sub
    Public Sub populateTestList(ByRef ddl As DropDownList)
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet
        DS = Obj.GetTest(Val(ddlBrand.SelectedValue), userInfo.userIDEntity, ddlProduct.SelectedValue)
        ddl.Items.Clear()
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddl.DataSource = DS.Tables(0)
            ddl.DataTextField = "test_name"
            ddl.DataValueField = "test_id"
            ddl.DataBind()
            If Not DS.Tables(0).Rows.Count = 1 Then
                ddl.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If

            ViewState("dt_Test") = DS.Tables(0)
        End If
    End Sub

    Public Sub populateBrandwiseProductList()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim Obj As New QualityControlClass
        Dim DS As New DataSet
        DS = Obj.GetBrandWiseProduct(Val(ddlBrand.SelectedValue))
        If (Not (DS Is Nothing) AndAlso DS.Tables.Count > 0 AndAlso Not (DS.Tables(0) Is Nothing) AndAlso DS.Tables(0).Rows.Count > 0) Then
            ddlProduct.DataSource = DS.Tables(0)
            ddlProduct.DataTextField = "prd_desc"
            ddlProduct.DataValueField = "prd_code"
            ddlProduct.DataBind()
            If Not DS.Tables(0).Rows.Count = 1 Then
                ddlProduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
            End If
        End If
    End Sub
#End Region

    Private Sub BrandTestLinkingListLoad()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim LovDetailsGet As New QualityControlClass
        Dim LovDetailsList As DataSet
        Dim lovtype As String
        lovtype = ddlBrand.SelectedValue

        LovDetailsList = LovDetailsGet.GetBrandTestLinkingList(Val(ddlBrand.SelectedValue), userInfo.userCompanyEntity, ddlProduct.SelectedValue)
        If (Not (LovDetailsList Is Nothing) AndAlso LovDetailsList.Tables.Count > 0) Then
            If (Not (LovDetailsList.Tables(0) Is Nothing) AndAlso LovDetailsList.Tables(0).Rows.Count > 0) Then
                gvDetails.DataSource = LovDetailsList
                gvDetails.DataBind()
                Div_Lov_Details_Grid.Visible = True
                trTableHeader.Visible = False
            Else
                gvDetails.DataSource = Nothing
                gvDetails.DataBind()
                Div_Lov_Details_Grid.Visible = True
                trTableHeader.Visible = True
                ddlTest.SelectedIndex = 0
                txtSeq.Text = ""
            End If
        End If

    End Sub

    Public Function FormatDate(ByVal stringdate As String) As SqlDateTime

        If Not (stringdate = String.Empty) Then

            Dim ddate As String() = stringdate.Split("/")
            Dim arrlist As New ArrayList
            Dim index As Integer = 0

            While index <= ddate.Length - 1
                arrlist.Add(ddate(index))
                System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
            End While
            Dim dd As Integer = System.Convert.ToInt32(arrlist.Item(0))
            Dim mm As Integer = System.Convert.ToInt32(arrlist.Item(1))
            Dim yyyy As Integer = System.Convert.ToInt32(arrlist.Item(2))

            Dim dt As DateTime = New DateTime(yyyy, mm, dd)
            dt = FormatDateTime(dt, DateFormat.LongDate)

            Return dt
        End If

    End Function

    Protected Sub ddlBrandList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBrand.SelectedIndexChanged
        'SaveSearchCriteria()
        'populateTestList(ddlTest)
        'BrandTestLinkingListLoad()
        populateBrandwiseProductList()
    End Sub

    Protected Sub gvDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetails.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Dim pageIdx As Integer = gvDetails.PageIndex * ddlPageSize.SelectedValue
            'e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
            'Dim imagebttn As ImageButton = e.Row.Cells(7).FindControl("btnUpdate")
            'If Not (imagebttn Is Nothing) Then
            '    Dim txtBox As TextBox = e.Row.Cells(0).FindControl("txtCode")
            '    Dim hdnBox As HiddenField = e.Row.Cells(0).FindControl("hdntxtCode")
            '    'Dim type As String = ddlLOV.SelectedValue
            '    If Not (txtBox Is Nothing) Then
            '        txtBox.Attributes.Add("onBlur", "return fnCompareLovDetailsCode(this.value,'" + hdnBox.Value + "','" + ddlBrand.SelectedValue + "');")
            '    End If
            '    imagebttn.Attributes.Add("onclick", "return fnValidateForgvDetails('" + e.Row.RowIndex.ToString + "');")
            'End If

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim ddlGvTest As DropDownList = e.Row.FindControl("ddlTest")
            If Not ddlGvTest Is Nothing Then
                populateTestList(ddlGvTest)
            End If
        End If

        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim ddlGvTest As DropDownList = e.Row.FindControl("ddlTest")
            If Not ddlGvTest Is Nothing Then
                populateTestList(ddlGvTest)
            End If
        End If

    End Sub

    Protected Sub gvDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetails.RowCommand

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        If e.CommandName = "insert" Then

            If String.IsNullOrEmpty(ddlBrand.SelectedValue.ToString()) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select brand.');", True)
                Exit Sub
            End If
            If String.IsNullOrEmpty(ddlProduct.SelectedValue.ToString()) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select product.');", True)
                Exit Sub
            End If

            Dim Type As String = ddlBrand.SelectedValue
            Dim ddlCode As DropDownList = gvDetails.FooterRow.FindControl("ddlTest")
            Dim txtSeq As TextBox = gvDetails.FooterRow.FindControl("txtSeq")
            Dim ddlActive As DropDownList = gvDetails.FooterRow.FindControl("ddlActive")

            lblErrorMessage.Text = ""
            Dim sqlConn As SqlConnection = Nothing
            Dim sqlTrans As SqlTransaction = Nothing
            Dim obj As New QualityControlClass

            Dim RecordInserted As Integer
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                RecordInserted = obj.BrandTestLinkingInsertUpdate(0, Val(ddlBrand.SelectedValue), Val(ddlTest.SelectedValue), Val(txtSeq.Text), ddlActiveYn.SelectedValue, ddlProduct.SelectedValue, userInfo.userIDEntity, sqlConn, sqlTrans)
                If (RecordInserted > 0) Then
                    sqlTrans.Commit()
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');", True)
                    BrandTestLinkingListLoad()
                Else
                    sqlTrans.Rollback()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
                End If
            Catch ex As Exception
                If (sqlTrans IsNot Nothing) Then
                    sqlTrans.Rollback()
                End If
                Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
                HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
            Finally
                If (sqlConn IsNot Nothing) Then
                    sqlConn.Close()
                End If
            End Try

        End If

    End Sub

    Protected Sub gvDetails_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles gvDetails.RowCancelingEdit
        Try
            gvDetails.EditIndex = -1
            BrandTestLinkingListLoad()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvDetails_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetails.RowEditing
        gvDetails.EditIndex = e.NewEditIndex
        BrandTestLinkingListLoad()
    End Sub

    Protected Sub gvDetails_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvDetails.RowUpdating

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If String.IsNullOrEmpty(ddlBrand.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select brand.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlProduct.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select product.');", True)
            Exit Sub
        End If

        lblErrorMessage.Text = ""
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New QualityControlClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False

        Dim index As Integer = gvDetails.EditIndex
        Dim row As GridViewRow = gvDetails.Rows(index)
        Try

            Dim Type As String = ddlBrand.SelectedValue
            Dim hdnLinkId As HiddenField = row.FindControl("hdnLinkId")
            Dim LinkId As Integer = hdnLinkId.Value
            Dim txtCode As HiddenField = row.FindControl("hdnTestCode")
            Dim Code As String = txtCode.Value
            Dim txtSeq As TextBox = row.FindControl("txtSeq")
            If Val(txtSeq.Text) <= 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please enter sequence.');", True)
                Exit Sub
            End If
            Dim Seq As Integer = txtSeq.Text
            Dim ddlActive As DropDownList = row.FindControl("ddlActiveYn")
            Dim Active As String = ddlActive.SelectedValue



            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            RecordInserted = obj.BrandTestLinkingInsertUpdate(LinkId, Val(ddlBrand.SelectedValue), Val(Code), Val(txtSeq.Text), Active, ddlProduct.SelectedValue, userInfo.userIDEntity, sqlConn, sqlTrans)
            If (RecordInserted > 0) Then
                sqlTrans.Commit()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updated Successfully.');", True)
                Try
                    gvDetails.EditIndex = -1
                    BrandTestLinkingListLoad()
                Catch ex As Exception
                End Try
            Else
                sqlTrans.Rollback()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record updation Failed!');", True)
            End If
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try

    End Sub

    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsert.Click

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        If String.IsNullOrEmpty(ddlBrand.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select brand.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlProduct.SelectedValue.ToString()) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select product.');", True)
            Exit Sub
        End If
        If String.IsNullOrEmpty(Convert.ToString(ddlTest.SelectedValue)) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please select test.');", True)
            Exit Sub
        End If
        If Val(txtSeq.Text) <= 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Please enter sequence.');", True)
            Exit Sub
        End If
        lblErrorMessage.Text = ""
        Dim sqlConn As SqlConnection = Nothing
        Dim sqlTrans As SqlTransaction = Nothing
        Dim obj As New QualityControlClass

        Dim RecordInserted As Integer
        Dim status As String = String.Empty
        Dim flag As Boolean = False
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()
            RecordInserted = obj.BrandTestLinkingInsertUpdate(0, Val(ddlBrand.SelectedValue), Val(ddlTest.SelectedValue), Val(txtSeq.Text), ddlActiveYn.SelectedValue, ddlProduct.SelectedValue, userInfo.userIDEntity, sqlConn, sqlTrans)
            If (RecordInserted > 0) Then
                sqlTrans.Commit()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record inserted Successfully.');", True)
                BrandTestLinkingListLoad()
            Else
                sqlTrans.Rollback()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Record insertion Failed!');", True)
            End If
        Catch ex As Exception
            If (sqlTrans IsNot Nothing) Then
                sqlTrans.Rollback()
            End If
            Session(Constant.SessionKeys.ErrMessage) = ex.ToString()
            HttpContext.Current.Server.Transfer("~/ExceptionPage.aspx")
        Finally
            If (sqlConn IsNot Nothing) Then
                sqlConn.Close()
            End If
        End Try
    End Sub


    Protected Sub ddlTest_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTest.SelectedIndexChanged
        Dim dt_Test As DataTable
        Try
            dt_Test = CType(ViewState("dt_Test"), DataTable)
            If dt_Test.Rows.Count > 0 Then
                Dim dt_new As DataTable = dt_Test.Select("test_id = '" & ddlTest.SelectedValue.ToString() & "'").CopyToDataTable()

                lblRefValue.Text = dt_new.Rows(0)("ref_value").ToString()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub ddlProduct_SelectedIndexChanged(sender As Object, e As EventArgs)

        populateTestList(ddlTest)
        BrandTestLinkingListLoad()

    End Sub
End Class
