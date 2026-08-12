
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.IO
Imports System.Security.Permissions
Imports Microsoft.Win32
Partial Class IndentPoDownload
    Inherits System.Web.UI.Page

#Region "Page_Load Event"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If Not IsPostBack Then

            AddAttributes()

            PopulateRegionDropdown()
            PopulateDepotDropdown()
            PopulateProductCategory()
            PopulateYear()
            PopulateMonth()

            If ddlyear.Items.FindByValue(DateTime.Now.Year.ToString()) IsNot Nothing Then
                ddlyear.SelectedValue = DateTime.Now.Year.ToString()
            End If


            If ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString("D2")) IsNot Nothing Then
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString("D2")
            End If

            'lblFinYear.Text = GetStandardParameter(Constant.Common.StandardParameter_ProcessYear)

            'lblFinMonth.Text = GetStandardParameter(Constant.Common.StandardParameter_ProcessMonth)

            If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                ddlStatus.SelectedValue = "E"
            End If

            RetrieveSearchCriteria()

            PopulateIndentList()

        End If

    End Sub

#End Region


#Region "Adding Attributes to Controls"
    Public Sub AddAttributes()
        'btnSubmit.Attributes.Add("onClick", "return validateForm()")
    End Sub
#End Region


#Region "Save Search Criteria."
    ' Saves the current search criteria in session
    Private Sub SaveSearchCriteria()
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Session(Constant.SessionKeys.IndentListSearchInfo) = Nothing

        Dim indentSearchInfo As New IndentListSearchCriteria
        indentSearchInfo.IndentRegion = ddlRegion.SelectedValue
        indentSearchInfo.IndentDepot = ddlDepot.SelectedValue

        If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
            indentSearchInfo.IndentStatus = "E"
        Else
            indentSearchInfo.IndentStatus = ddlStatus.SelectedValue
        End If

        Session(Constant.SessionKeys.IndentListSearchInfo) = indentSearchInfo

    End Sub

#End Region

#Region "Retrieve Search Criteria."

    ' Retrieve the existing search criteria in session
    Private Sub RetrieveSearchCriteria()

        If (Not (Session(Constant.SessionKeys.IndentListSearchInfo) Is Nothing)) Then

            Dim indentSearchInfo As New IndentListSearchCriteria

            indentSearchInfo = Session(Constant.SessionKeys.IndentListSearchInfo)
            ddlRegion.SelectedValue = indentSearchInfo.IndentRegion
            ddlDepot.SelectedValue = indentSearchInfo.IndentDepot
            ddlStatus.SelectedValue = indentSearchInfo.IndentStatus

        End If

        SaveSearchCriteria()

    End Sub

#End Region




#Region "Populate Region dropdown."

    Private Sub PopulateRegionDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnRegion As New Common()
        Dim dsRegion As DataSet

        Try

            dsRegion = cmnRegion.GetLovDetails(userInfo.userCompanyEntity, Constant.Common.REGION_TYPE, Constant.Common.ActiveStatus)

            If Not (dsRegion Is Nothing) Then

                If Not (dsRegion.Tables(0).Rows.Count = 0) Then

                    ddlRegion.DataSource = dsRegion
                    ddlRegion.DataTextField = "Lov_Value"
                    ddlRegion.DataValueField = "Lov_Code"
                    ddlRegion.DataBind()

                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.All, "", True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                        ddlRegion.SelectedValue = userInfo.userRegionEntity
                        ddlRegion.Enabled = False
                    End If

                Else
                    ddlRegion.Items.Insert(0, New ListItem(Constant.Common.Selec, "", True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Populate Depot dropdown."

    Private Sub PopulateDepotDropdown()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New Common()
        Dim dsDepot As DataSet

        ddlDepot.Items.Clear()

        Try

            dsDepot = cmnDepot.Getdepotname(ddlRegion.SelectedValue)

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlDepot.DataSource = dsDepot
                    ddlDepot.DataTextField = "depot_name"
                    ddlDepot.DataValueField = "depot_code"
                    ddlDepot.DataBind()

                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                    If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.REGION) Then
                        ddlDepot.SelectedValue = userInfo.userBranchEntity
                        ddlDepot.Enabled = False
                    End If

                Else
                    ddlDepot.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

#End Region

#Region "Get values for a particular Standard Parameter."

    Private Function GetStandardParameter(ByVal param_name As String) As String

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnStandardParameter As New Common()
        Dim dsStandardParameter As DataSet

        Dim result As String = String.Empty

        Try

            dsStandardParameter = cmnStandardParameter.GetStandardParameterValues(param_name)

            If Not (dsStandardParameter Is Nothing) Then

                If Not (dsStandardParameter.Tables(0).Rows.Count = 0) Then
                    result = dsStandardParameter.Tables(0).Rows(0)("param_char_value")
                Else
                    Dim returnUrl As String = "~/ExceptionPage.aspx"
                    Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
                    Server.Transfer(returnUrl)
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

        Return result

    End Function

#End Region
    Private Sub PopulateProductCategory()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim cmnDepot As New IndentMaster()
        Dim dsDepot As DataSet

        ddlproduct.Items.Clear()

        Try

            dsDepot = cmnDepot.GetProductCategory()

            If Not (dsDepot Is Nothing) Then

                If Not (dsDepot.Tables(0).Rows.Count = 0) Then

                    ddlproduct.DataSource = dsDepot
                    ddlproduct.DataTextField = "product_name"
                    ddlproduct.DataValueField = "product_name"
                    ddlproduct.DataBind()
                    'ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))

                Else
                    ' ddlproduct.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
                End If

            End If

        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)

        End Try

    End Sub

    Public Sub PopulateYear()
        Try
            Dim startYear As Int32 = 2019
            Dim currentYear As Int32 = DateTime.Now.Year

            ddlyear.Items.Clear()

            For year As Integer = startYear To currentYear
                ddlyear.Items.Add(New ListItem(year.ToString(), year.ToString(), True))
            Next

            ddlyear.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

    Public Sub PopulateMonth()
        Try
            ddlMonth.Items.Clear()

            For month As Integer = 1 To 12
                Dim monthValue As String = month.ToString("D2") ' Formats the month as "01", "02", etc.
                ddlMonth.Items.Add(New ListItem(monthValue, monthValue, True))
            Next

            ddlMonth.Items.Insert(0, New ListItem(Constant.Common.Selec, String.Empty, True))
        Catch ex As Exception
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Response.Redirect(returnUrl)
        End Try
    End Sub

#Region "Populate SKU Codes gridview in case of New Indent Entry."

    Private Sub PopulateIndentList()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If
        Dim Competitor As String = String.Empty
        Dim indIndentMaster As New IndentMaster()
        Dim dsIndentList As DataSet

        Dim indent_header As New IndentHeaderEntity()

        indent_header.IndentDepot = ddlDepot.SelectedValue
        indent_header.IndentFinYear = ddlyear.SelectedValue
        indent_header.IndentFinMonth = ddlMonth.SelectedValue
        indent_header.IndentStatus = ddlStatus.SelectedValue

        For Each lstitm As ListItem In ddlproduct.Items
            If lstitm.Selected Then
                Competitor = Competitor + lstitm.Value + ","

            End If
        Next

        dsIndentList = indIndentMaster.GetIndentPoDownloadList(indent_header, userInfo.userIDEntity, Competitor)
        If (Not (dsIndentList Is Nothing)) Then
            gvIndentList.Visible = True

            gvIndentList.DataSource = dsIndentList.Tables(0)

            'Dim primary(3) As String

            'primary(0) = "depot_code"
            'primary(1) = "fin_year"
            'primary(2) = "fin_month"
            'primary(3) = "indent_no"

            'gvIndentList.DataKeyNames = primary

            gvIndentList.DataBind()
        End If

        'If (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
        '    gvIndentList.Columns(0).Visible = False
        '    gvIndentList.Columns(1).Visible = True
        '    gvIndentList.Columns(2).Visible = True
        'Else
        '    gvIndentList.Columns(0).Visible = True
        '    gvIndentList.Columns(1).Visible = False
        '    gvIndentList.Columns(2).Visible = False
        'End If


    End Sub
#End Region


    'Protected Sub imgbtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAdd.Click
    '    SaveSearchCriteria()
    '    Response.Redirect("AddUpdateIndentEntry.aspx", True)
    'End Sub

    'Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click

    '    If String.IsNullOrEmpty(ddlyear.SelectedValue) Then
    '        lblErrorMessage.Text = "Year is mandatory."
    '        lblErrorMessage.ForeColor = System.Drawing.Color.Red
    '        ddlyear.BackColor = System.Drawing.Color.Yellow
    '        Return
    '    End If

    '    If String.IsNullOrEmpty(ddlDepot.SelectedValue) Then
    '        lblErrorMessage.Text = "Depot is mandatory."
    '        lblErrorMessage.ForeColor = System.Drawing.Color.Red
    '        ddlDepot.BackColor = System.Drawing.Color.Yellow
    '        Return
    '    End If

    '    ' Reset background color for valid selections
    '    ddlyear.BackColor = System.Drawing.Color.White
    '    ddlDepot.BackColor = System.Drawing.Color.White
    '    lblErrorMessage.Text = ""

    '    SaveSearchCriteria()
    '    PopulateIndentList()
    'End Sub

    Protected Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        If String.IsNullOrEmpty(ddlyear.SelectedValue) Then
            lblErrorMessage.Text = "Year is mandatory."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            ddlyear.BackColor = System.Drawing.Color.Yellow
            Return
        End If

        If String.IsNullOrEmpty(ddlDepot.SelectedValue) Then
            lblErrorMessage.Text = "Depot is mandatory."
            lblErrorMessage.ForeColor = System.Drawing.Color.Red
            ddlDepot.BackColor = System.Drawing.Color.Yellow
            Return
        End If

        ' Reset background color for valid selections
        ddlyear.BackColor = System.Drawing.Color.White
        ddlDepot.BackColor = System.Drawing.Color.White
        lblErrorMessage.Text = ""

        SaveSearchCriteria()
        PopulateIndentList()
    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        PopulateDepotDropdown()
    End Sub

    Protected Sub gvIndentList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIndentList.RowDataBound
        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)
        Else
            Response.Redirect("~/Login.aspx")
        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim hdndoc As HiddenField = CType(e.Row.FindControl("hdndoc"), HiddenField)
            Dim btndownload As ImageButton = CType(e.Row.FindControl("btndownload"), ImageButton)

            'Dim chkDelete As CheckBox = CType(e.Row.FindControl("chkDelete"), CheckBox)
            'chkDelete.Attributes.Add("onclick", "rwslctToggleSelect('" & chkDelete.ClientID & "');")
            'If rowView("created_user_depot").ToString <> rowView("depot_code").ToString Then
            '    chkDelete.Enabled = False
            'End If

            Dim lblApprvRejctStatus As Label = CType(e.Row.FindControl("lblApprvRejctStatus"), Label)

            If IsDBNull(rowView("approved_yn")) Then
                lblApprvRejctStatus.Text = "Entered"
            Else
                If rowView("approved_yn") = "Y" Then
                    e.Row.BackColor = Drawing.Color.LawnGreen
                    lblApprvRejctStatus.Text = "Approved"
                ElseIf rowView("approved_yn") = "N" Then
                    e.Row.BackColor = Drawing.Color.Pink
                    lblApprvRejctStatus.Text = "Rejected"
                End If

            End If

            If rowView("indh_inv_req_mail_yn") = "Y" Then
                e.Row.BackColor = Drawing.Color.Yellow
            End If



            Dim txtRemarks As TextBox = CType(e.Row.FindControl("txtRemarks"), TextBox)

            If Not (userInfo.userGroupCodeEntity = Constant.UserFormAccess.SYSADMIN Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOMARKETING Or userInfo.userGroupCodeEntity = Constant.UserFormAccess.HOACCOUNTS) Then
                txtRemarks.Enabled = False
            End If

            e.Row.Cells(6).ForeColor = Drawing.Color.Blue
            e.Row.Cells(7).ForeColor = Drawing.Color.Blue

            If (hdndoc.Value = String.Empty) Then
                btndownload.Visible = False
            Else
                btndownload.Visible = True
            End If

        End If

    End Sub


    Private Sub DownloadDocument(ByVal fileName As String)
        Try
            'Dim genReportPath As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & "\" & userInfo.userCompanyEntity & "\" & "Machine Scrap Selling" & "\"

            Dim genReportPath As String = ConfigurationManager.AppSettings.Get("UPLOAD_DOCS_FOLDER_ABS_PATH") & "Berger" & "\" & "Invoice_Docs" & "\"
            Dim appReceiptFileAbsolutePath = String.Concat(genReportPath, fileName)
            If File.Exists(appReceiptFileAbsolutePath) Then
                Response.Clear()
                Response.Charset = String.Empty
                Response.ContentType = GetMIMEType(appReceiptFileAbsolutePath)
                Response.AppendHeader("Content-Disposition", String.Concat("attachment; filename= """, fileName, """"))
                Response.TransmitFile(appReceiptFileAbsolutePath)
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Flush()
            Else
                'lblPopMessageShow.Text = "File not found."
                'lblPopMessageShow.ForeColor = Drawing.Color.Red
                'Button1.OnClientClick = "return RefreshScreen()"
                'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetMIMEType(ByVal filepath As String) As String
        Dim regPerm As RegistryPermission = New RegistryPermission(RegistryPermissionAccess.Read, "\\HKEY_CLASSES_ROOT")
        Dim classesRoot As RegistryKey = Registry.ClassesRoot
        Dim fi = New FileInfo(filepath)
        Dim dotExt As String = LCase(fi.Extension)
        Dim typeKey As RegistryKey = classesRoot.OpenSubKey("MIME\Database\Content Type")
        Dim keyname As String = String.Empty

        For Each keyname In typeKey.GetSubKeyNames()
            Dim curKey As RegistryKey = classesRoot.OpenSubKey(String.Concat("MIME\Database\Content Type\", keyname))
            If LCase(curKey.GetValue("Extension")) = dotExt Then
                'Debug.WriteLine("Content type was " + keyname)
                Return keyname
            End If
        Next
        Return keyname
    End Function


    Protected Sub gvIndentList_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            Dim indIndentMaster As New IndentMaster()
            If (String.Compare(e.CommandName, "download", StringComparison.CurrentCultureIgnoreCase) = 0) Then
                If String.IsNullOrEmpty(e.CommandArgument) Then
                    Exit Sub
                End If
                DownloadDocument(e.CommandArgument.ToString().Trim())
            End If
        Catch ex As Exception
            Dim returnUrl As String = "~/XP_ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Response.Redirect(returnUrl)
        End Try
    End Sub

End Class
