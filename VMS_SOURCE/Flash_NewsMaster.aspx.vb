'**************************************************
'Copyright	    : PROTECTON, MCC, KOLKATA
'Source	        : Flash_NewsMaster.aspx.vb
'Created Date	: 29-November-2007
'Created By	    : Saravanan 
'Version	    : R02.00.00
'Description	: Code behind for Flash News Master Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes

Partial Class Flash_NewsMaster
    Inherits System.Web.UI.Page

#Region "Page_Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            AddAttributes()
            PopulateUserNames()
            PopulateFlashNews()
           

        End If

    End Sub

#End Region

#Region "AddAttributes"

    Private Sub AddAttributes()
        btnSubmit.Attributes.Add("onClick", "return ValidateFlashNewsMasterControls();")

        txtMsg1.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE1',this.id,'txtDoExp1','HiddenField1');")
        txtMsg2.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE2',this.id,'txtDoExp2','HiddenField2');")
        txtMsg3.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE3',this.id,'txtDoExp3','HiddenField3');")
        txtMsg4.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE4',this.id,'txtDoExp4','HiddenField4');")
        txtMsg5.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE5',this.id,'txtDoExp5','HiddenField5');")
        txtMsg6.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE6',this.id,'txtDoExp6','HiddenField6');")
        txtMsg7.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE7',this.id,'txtDoExp7','HiddenField7');")
        txtMsg8.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE8',this.id,'txtDoExp8','HiddenField8');")
        txtMsg9.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE9',this.id,'txtDoExp9','HiddenField9');")
        txtMsg10.Attributes.Add("OnBlur", "return DisplayCurrentDate('txtDoE10',this.id,'txtDoExp10','HiddenField10');")
    End Sub
#End Region

#Region "Populate UserNames and UserIds"
    Public Sub PopulateUserNames()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New UserProfile
        Dim UserIdsSet As New DataSet
        UserIdsSet = ObjDocumentType.GetUserIds(userInfo.userCompanyEntity)
        If (Not (UserIdsSet Is Nothing) AndAlso UserIdsSet.Tables.Count > 0 AndAlso Not (UserIdsSet.Tables(0) Is Nothing) AndAlso UserIdsSet.Tables(0).Rows.Count > 0) Then
            ddlUserName.DataSource = UserIdsSet.Tables(0)
            ddlUserName.DataTextField = "usp_username"
            ddlUserName.DataValueField = "usp_user_id"
            ddlUserName.DataBind()

            ddlUserName.Items.Insert(0, New ListItem(Constant.Common.All, "All", True))
        End If

    End Sub
#End Region

#Region "Populate Flash News Details"

    Public Sub PopulateFlashNews()

        ClearFields()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjFlashNews As New FlashNews
        Dim FlashNewsSet As New DataSet
        FlashNewsSet = ObjFlashNews.GetFalshNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue)
        If (Not (FlashNewsSet Is Nothing) AndAlso FlashNewsSet.Tables.Count > 0) Then
            If (Not (FlashNewsSet.Tables(0) Is Nothing) AndAlso FlashNewsSet.Tables(0).Rows.Count > 0) Then

                Dim i As Integer = 1

                For Each FlashNewsListRow As System.Data.DataRow In FlashNewsSet.Tables(0).Rows

                    Select Case i

                        Case 1
                            HiddenField1.Value = FlashNewsListRow("flash_from")
                            txtMsg1.Text = FlashNewsListRow("flash_msg")
                            txtDoE1.Text = FlashNewsListRow("flash_from")
                            txtDoExp1.Text = FlashNewsListRow("flash_till")


                        Case 2
                            HiddenField2.Value = FlashNewsListRow("flash_from")
                            txtMsg2.Text = FlashNewsListRow("flash_msg")
                            txtDoE2.Text = FlashNewsListRow("flash_from")
                            txtDoExp2.Text = FlashNewsListRow("flash_till")
                        Case 3
                            HiddenField3.Value = FlashNewsListRow("flash_from")
                            txtMsg3.Text = FlashNewsListRow("flash_msg")
                            txtDoE3.Text = FlashNewsListRow("flash_from")
                            txtDoExp3.Text = FlashNewsListRow("flash_till")
                        Case 4
                            HiddenField4.Value = FlashNewsListRow("flash_from")
                            txtMsg4.Text = FlashNewsListRow("flash_msg")
                            txtDoE4.Text = FlashNewsListRow("flash_from")
                            txtDoExp4.Text = FlashNewsListRow("flash_till")

                        Case 5
                            HiddenField5.Value = FlashNewsListRow("flash_from")
                            txtMsg5.Text = FlashNewsListRow("flash_msg")
                            txtDoE5.Text = FlashNewsListRow("flash_from")
                            txtDoExp5.Text = FlashNewsListRow("flash_till")

                        Case 6
                            HiddenField6.Value = FlashNewsListRow("flash_from")
                            txtMsg6.Text = FlashNewsListRow("flash_msg")
                            txtDoE6.Text = FlashNewsListRow("flash_from")
                            txtDoExp6.Text = FlashNewsListRow("flash_till")

                        Case 7
                            HiddenField7.Value = FlashNewsListRow("flash_from")
                            txtMsg7.Text = FlashNewsListRow("flash_msg")
                            txtDoE7.Text = FlashNewsListRow("flash_from")
                            txtDoExp7.Text = FlashNewsListRow("flash_till")

                        Case 8
                            HiddenField8.Value = FlashNewsListRow("flash_from")
                            txtMsg8.Text = FlashNewsListRow("flash_msg")
                            txtDoE8.Text = FlashNewsListRow("flash_from")
                            txtDoExp8.Text = FlashNewsListRow("flash_till")

                        Case 9
                            HiddenField9.Value = FlashNewsListRow("flash_from")
                            txtMsg9.Text = FlashNewsListRow("flash_msg")
                            txtDoE9.Text = FlashNewsListRow("flash_from")
                            txtDoExp9.Text = FlashNewsListRow("flash_till")

                        Case 10
                            HiddenField10.Value = FlashNewsListRow("flash_from")
                            txtMsg10.Text = FlashNewsListRow("flash_msg")
                            txtDoE10.Text = FlashNewsListRow("flash_from")
                            txtDoExp10.Text = FlashNewsListRow("flash_till")
                    End Select

                    i = i + 1
                Next

            End If
        End If

    End Sub
#End Region

#Region "Button Submit Events"

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim DeleteFlashNews As New FlashNews
        Dim numRowsAffected As Integer
        numRowsAffected = DeleteFlashNews.DeleteFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue)

        Dim i As Integer

        For i = 1 To 10

            Select Case i

                Case 1
                    If Not (txtMsg1.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg1.Text, FormatDate(HiddenField1.Value), FormatDate(txtDoExp1.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If
                Case 2
                    If Not (txtMsg2.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg2.Text, FormatDate(HiddenField2.Value), FormatDate(txtDoExp2.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If
                Case 3
                    If Not (txtMsg3.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg3.Text, FormatDate(HiddenField3.Value), FormatDate(txtDoExp3.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If
                Case 4
                    If Not (txtMsg4.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg4.Text, FormatDate(HiddenField4.Value), FormatDate(txtDoExp4.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If
                Case 5
                    If Not (txtMsg5.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg5.Text, FormatDate(HiddenField5.Value), FormatDate(txtDoExp5.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If

                Case 6
                    If Not (txtMsg6.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg6.Text, FormatDate(HiddenField6.Value), FormatDate(txtDoExp6.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If

                Case 7
                    If Not (txtMsg7.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg7.Text, FormatDate(HiddenField7.Value), FormatDate(txtDoExp7.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If

                Case 8
                    If Not (txtMsg8.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg8.Text, FormatDate(HiddenField8.Value), FormatDate(txtDoExp8.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If

                Case 9
                    If Not (txtMsg9.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg9.Text, FormatDate(HiddenField9.Value), FormatDate(txtDoExp9.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If

                Case 10
                    If Not (txtMsg10.Text = String.Empty) Then
                        Dim InsertFlashNews As New FlashNews
                        numRowsAffected = InsertFlashNews.InsertFlashNews(userInfo.userCompanyEntity, ddlUserName.SelectedValue, txtMsg10.Text, FormatDate(HiddenField10.Value), FormatDate(txtDoExp10.Text), Constant.Common.ActiveStatus, userInfo.userIDEntity)
                    End If
            End Select
        Next

        PopulateFlashNews()

    End Sub
#End Region

#Region "Clear Fields"

    Public Sub ClearFields()

        Dim i As Integer = 1

        For i = 1 To 10

            Select Case i

                Case 1
                    txtMsg1.Text = String.Empty
                    txtDoE1.Text = String.Empty
                    txtDoExp1.Text = String.Empty
                Case 2
                    txtMsg2.Text = String.Empty
                    txtDoE2.Text = String.Empty
                    txtDoExp2.Text = String.Empty
                Case 3
                    txtMsg3.Text = String.Empty
                    txtDoE3.Text = String.Empty
                    txtDoExp3.Text = String.Empty

                Case 4
                    txtMsg4.Text = String.Empty
                    txtDoE4.Text = String.Empty
                    txtDoExp4.Text = String.Empty

                Case 5
                    txtMsg5.Text = String.Empty
                    txtDoE5.Text = String.Empty
                    txtDoExp5.Text = String.Empty

                Case 6
                    txtMsg6.Text = String.Empty
                    txtDoE6.Text = String.Empty
                    txtDoExp6.Text = String.Empty

                Case 7
                    txtMsg7.Text = String.Empty
                    txtDoE7.Text = String.Empty
                    txtDoExp7.Text = String.Empty

                Case 8
                    txtMsg8.Text = String.Empty
                    txtDoE8.Text = String.Empty
                    txtDoExp8.Text = String.Empty

                Case 9
                    txtMsg9.Text = String.Empty
                    txtDoE9.Text = String.Empty
                    txtDoExp9.Text = String.Empty

                Case 10
                    txtMsg10.Text = String.Empty
                    txtDoE10.Text = String.Empty
                    txtDoExp10.Text = String.Empty
            End Select
        Next

    End Sub

#End Region

#Region "Date Format"

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

#End Region

#Region "Dropdown User List Events"

    Protected Sub ddlUserName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserName.SelectedIndexChanged
        PopulateFlashNews()
    End Sub

#End Region

#Region "Reset Events"

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        PopulateFlashNews()
    End Sub

#End Region

#Region "Cancel Button Events"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("~/Home.aspx")
    End Sub

#End Region

End Class
