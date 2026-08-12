'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : Serial_No_Control_List.aspx.vb
'Created Date	: 26-November-2007
'Created By	    : Arun 
'Version	    : R02.00.00
'Description	: Code behind for SerialNoControlList Page

'Modified By       Modified On       Version         Reason

'****************************************************************

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Partial Class Serial_No_Control_List
    Inherits System.Web.UI.Page

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            PageSizeDropdown()
            populateFinYear()
            RetrieveSearchCriteria()
            SaveSearchCriteria()
            SerialNoControlListLoad()

        End If

    End Sub
#End Region

#Region "Save Search Criteria"
    ' Saves the current search criteria in session
    Private Sub SaveSearchCriteria()
        Dim SerialControlSearchInfo As New SerialControlSearchCriteria
        SerialControlSearchInfo.Finyear = ddlFinYear.SelectedValue
        SerialControlSearchInfo.PageIndex = gvSerialNoControl.PageIndex
        Session(Constant.SessionKeys.SerialControlSearchInfo) = SerialControlSearchInfo


    End Sub
#End Region

#Region "Retrieve Search Criteria"
    ' Retrieve the existing search criteria in session
    Private Sub RetrieveSearchCriteria()
        If (Not (Session(Constant.SessionKeys.SerialControlSearchInfo) Is Nothing)) Then
            Dim SerialControlSearchInfo As New SerialControlSearchCriteria
            SerialControlSearchInfo = Session(Constant.SessionKeys.SerialControlSearchInfo)
            ddlFinYear.SelectedValue = SerialControlSearchInfo.Finyear
            gvSerialNoControl.PageIndex = SerialControlSearchInfo.PageIndex

        End If

    End Sub
#End Region


#Region "Populate Fin Year"
    Public Sub populateFinYear()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim ObjDocumentType As New Common
        Dim StatusTypeSet As New DataSet
        StatusTypeSet = ObjDocumentType.GetFinYrDetails(userInfo.userCompanyEntity, Constant.Common.ActiveStatus)
        If (Not (StatusTypeSet Is Nothing) AndAlso StatusTypeSet.Tables.Count > 0 AndAlso Not (StatusTypeSet.Tables(0) Is Nothing) AndAlso StatusTypeSet.Tables(0).Rows.Count > 0) Then
            ddlFinYear.DataSource = StatusTypeSet.Tables(0)
            ddlFinYear.DataTextField = "dis_fin_year"
            ddlFinYear.DataValueField = "fin_year"
            ddlFinYear.DataBind()
            Dim i As Integer
            For i = 0 To StatusTypeSet.Tables(0).Rows.Count - 1
                If (Convert.ToString(StatusTypeSet.Tables(0).Rows(i)("fin_current")) = Constant.Common.ActiveStatus) Then
                    Dim k As String = StatusTypeSet.Tables(0).Rows(i)("fin_year")
                    ddlFinYear.SelectedValue = k
                    Exit For
                End If
            Next
            ddlFinYear.Items.Insert(0, New ListItem(Constant.Common.Selec, "0", True))
        End If
    End Sub
#End Region

#Region "Populate page size dropdown"

    ' Populates the page size dropdown
    Private Sub PageSizeDropdown()
        ddlPageSize.Items.Clear()
        'Gets the page size from the web.config file
        Dim configPagesize As String = ConfigurationManager.AppSettings.Get("PageSize")
        Dim numbers As String() = configPagesize.Split(",")
        Dim index As Integer = 0

        While index <= numbers.Length - 1
            Try
                Dim size As Integer = Convert.ToInt32(numbers(index))
                'Adds the page size to drop down list
                ddlPageSize.Items.Add(New ListItem(size.ToString, size.ToString))
            Catch exp As Exception
                ddlPageSize.Items.Clear()
                'LoadDefaultPageSize()
            End Try
            System.Math.Min(System.Threading.Interlocked.Increment(index), index - 1)
        End While
        gvSerialNoControl.PageSize = ddlPageSize.SelectedValue
    End Sub

#End Region

#Region "Page Size Change Event Handler"

    Protected Sub ddlPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        gvSerialNoControl.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue)
        SerialNoControlListLoad()
    End Sub

#End Region

#Region "Load Default Page Size"
    ' Loads the default page size if the entry is missing in web.config or any incorrect entries are present
    Private Sub LoadDefaultPageSize()
        Dim index As Integer = 1
        While index <= 50
            ddlPageSize.Items.Add(New ListItem(index.ToString, index.ToString))
            index = index + 1
        End While
    End Sub

#End Region

#Region "SerialNoControlListLoad"

    Private Sub SerialNoControlListLoad()

        Dim userInfo As VMSUserEntity = New VMSUserEntity()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If

        Dim SerialControlGet As New SerialControl
        Dim SerialControlList As DataSet
        Dim finyear As String
        finyear = ddlFinYear.SelectedValue

        SerialControlList = SerialControlGet.GetSerialNoControlList(userInfo.userCompanyEntity, finyear)
        If (Not (SerialControlList Is Nothing) AndAlso SerialControlList.Tables.Count > 0) Then
            If (Not (SerialControlList.Tables(0) Is Nothing) AndAlso SerialControlList.Tables(0).Rows.Count > 0) Then
                gvSerialNoControl.DataSource = SerialControlList
                gvSerialNoControl.DataBind()
                Div_Serial_No_Control_Grid.Visible = False
                lblPageSize.Visible = True
                ddlPageSize.Visible = True

            Else
                gvSerialNoControl.DataSource = Nothing
                gvSerialNoControl.DataBind()
                Div_Serial_No_Control_Grid.Visible = True
                lblPageSize.Visible = False
                ddlPageSize.Visible = False

            End If
        End If

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

#Region "gvSerialNoControl_RowDataBound"

    Protected Sub gvSerialNoControl_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSerialNoControl.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim pageIdx As Integer = gvSerialNoControl.PageIndex * ddlPageSize.SelectedValue
            e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            'e.Row.Cells(0).Text = "<a href='Request_Add.aspx?" + Constant.SessionKeys.ID + "=" + rowView("req_id").ToString() + "'>" + e.Row.Cells(0).Text + "</a>"
            If Not (rowView("active").ToString = "Y") Then

                e.Row.BackColor = Drawing.Color.Red
                e.Row.ForeColor = Drawing.Color.White
                'e.Row.Cells(1).Text = "<a class='gridlink' href='User_Group_Add.aspx?" + Constant.SessionKeys.ID + "=" + rowView("grp_user_group_code").ToString() + "'>" + e.Row.Cells(1).Text + "</a>"
                e.Row.Cells(1).Text = "<a  href='Serial_No_Control_Add.aspx?" + Constant.SessionKeys.CurrentYear + "=" + rowView("srl_fin_year").ToString() + "&" + Constant.SessionKeys.DOC + "=" + rowView("srl_doc_type").ToString() + "&" + Constant.SessionKeys.ID + "=" + rowView("srl_id").ToString() + "'>" + e.Row.Cells(1).Text + "</a>"

                'e.Row.Cells(1).Text = "<a href='Serial_No_Control_Add.aspx?" + Constant.SessionKeys.ID + "=" + rowView("srl_doc_type").ToString() + "'>" + e.Row.Cells(1).Text + "</a>"
            Else
                e.Row.Cells(1).Text = "<a  href='Serial_No_Control_Add.aspx?" + Constant.SessionKeys.CurrentYear + "=" + rowView("srl_fin_year").ToString() + "&" + Constant.SessionKeys.DOC + "=" + rowView("srl_doc_type").ToString() + "&" + Constant.SessionKeys.ID + "=" + rowView("srl_id").ToString() + "'>" + e.Row.Cells(1).Text + "</a>"
            End If
        End If
        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            row = e.Row.Controls(0).Controls(0).Controls(0)
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
#End Region

#Region "gvSerialNoControl_PageIndexChanging"
    Protected Sub gvSerialNoControl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvSerialNoControl.PageIndexChanging
        gvSerialNoControl.PageIndex = e.NewPageIndex
        SaveSearchCriteria()
        SerialNoControlListLoad()
    End Sub
#End Region

#Region "ddlFinYear_SelectedIndexChanged"
    Protected Sub ddlFinYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFinYear.SelectedIndexChanged
        SerialNoControlListLoad()
    End Sub
#End Region


    'Protected Sub ImgbtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgbtnAdd.Click
    '    Response.Redirect("~/Serial_No_Control_Add.aspx")
    'End Sub
    Protected Sub ImgbtnAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Serial_No_Control_Add.aspx")
    End Sub
End Class
