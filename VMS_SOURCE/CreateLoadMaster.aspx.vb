'***************************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : CreateLoadMaster.aspx.vb
'Created Date	: 14/12/2011
'Created By	    : Riddhikusal Datta 
'Version	    : R01.00.00
'Description	: Code behind for CReatLoadMaster

'Modified By       Modified On       Version         Reason

'****************************************************************
Imports Microsoft.VisualBasic
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient

Partial Class CreateLoadMaster
    Inherits System.Web.UI.Page
    Dim userInfo As VMSUserEntity = New VMSUserEntity()
    Dim sr As StreamReader
    Dim linerd As Char()
    Dim filesavestrt As String
    Dim filesaveend As String
    Dim saveLocation As String
    Dim D1, D2 As Date
#Region "Login Check"
    Private Sub CheckLogin()
        If (Not (Session(Constant.SessionKeys.UserInfo) Is Nothing)) Then
            userInfo = CType(Session(Constant.SessionKeys.UserInfo), VMSUserEntity)

        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub
#End Region

    '#Region "check Stock Master"
    '    Private Sub StockMasterCheck()
    '        CheckLogin()
    '        Dim ScreenDS As DataSet
    '        Dim LoadObj As New CreateLoadMasterClass
    '        ScreenDS = LoadObj.CheckStockMaster
    '        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
    '            If CType(ScreenDS.Tables(0).Rows(0)(0), Integer) = 0 Then
    '                btnProcess.Enabled = False
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Stock Master Is not Updated for current month and year')</script>", False)
    '            End If
    '        End If
    '    End Sub
    '#End Region

#Region "check Estimation Data"
    Private Sub EstimationDataAndStockCheck()
        CheckLogin()
        Dim ScreenDS As DataSet
        Dim LoadObj As New CreateLoadMasterClass
        ScreenDS = LoadObj.CheckStockAndEstimate
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            If CType(ScreenDS.Tables(0).Rows(0)(0), Integer) = 0 AndAlso CType(ScreenDS.Tables(0).Rows(0)(1), Integer) = 0 Then
                btnProcess.Enabled = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Estimation Data and Stock Master are not Updated for current month and year')</script>", False)
            ElseIf CType(ScreenDS.Tables(0).Rows(0)(0), Integer) = 0 Then
                btnProcess.Enabled = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Stock Master is not Updated for current month and year')</script>", False)
            ElseIf CType(ScreenDS.Tables(0).Rows(0)(1), Integer) = 0 Then
                btnProcess.Enabled = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> alert('Estimate Data is not Updated for current month and year')</script>", False)

            End If
        End If
    End Sub
#End Region

#Region "Get Screen Details"
    Private Sub GetScreenDetails()
        Dim ScreenDS As DataSet
        Dim LoadObj As New CreateLoadMasterClass
        ScreenDS = LoadObj.GetSCreenDetails
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            lblYear.Text = ScreenDS.Tables(0).Rows(0)("year").ToString
            lblMonth.Text = ScreenDS.Tables(0).Rows(0)("month").ToString
            lblStockAsOn.Text = ScreenDS.Tables(0).Rows(0)("load_stock_as_on").ToString
        End If
    End Sub
#End Region
#Region "Load Master Creation"
    Private Sub LoadMasterCreate()
        CheckLogin()
        Dim LoadObj As New CreateLoadMasterClass
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()
        D1 = Date.Now.ToLongTimeString
        hdnStart.Value = Format(DateTime.Now, "hh:mm:ss:fff")

        numRowsAffected = LoadObj.CreateLoadMaster(sqlConn, sqlTrans, userInfo.userIDEntity)
        If numRowsAffected > 0 Then
            sqlTrans.Commit()
            GetErrorDetails()
            hdnEnd.Value = Format(DateTime.Now, "hh:mm:ss:fff")
            D2 = Date.Now.ToLongTimeString
            lblStartTime.Text = hdnStart.Value
            lblEndTime.Text = hdnEnd.Value
            lblEclapsedTime.Text = (D2 - D1).ToString
            tabSummery.Visible = True
        Else
            sqlTrans.Rollback()

        End If
    End Sub
#End Region
#Region "Get Error Details"
    Private Sub GetErrorDetails()
        CheckLogin()
        Dim ScreenDS As DataSet
        Dim LoadObj As New CreateLoadMasterClass
        ScreenDS = LoadObj.GetNotFoundCount(userInfo.userIDEntity)
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            gvNotFound.DataSource = ScreenDS
            gvNotFound.DataBind()
            Label4.Visible = True
            ddlPageSize.Visible = True
            lblNotFound.Text = "Vendor Unit not Found for Following Cases"
        Else
            gvNotFound.Visible = False
            Label4.Visible = False
            ddlPageSize.Visible = False
            lblNotFound.Text = ""
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
        gvNotFound.PageSize = ddlPageSize.SelectedValue
    End Sub

#End Region

    Protected Sub ddlPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        gvNotFound.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue)
        GetErrorDetails()
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        LoadMasterCreate()
        btnProcess.Enabled = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckLogin()
            GetScreenDetails()
            PageSizeDropdown()
            EstimationDataAndStockCheck()

        End If
    End Sub

    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    '    CheckLogin()

    '    Dim ReportViewer As New ReportViewer_DC

    '    ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Stock_Upload_Summary_Report
    '    ReportViewer.ReportCase = Constant.ReportView.ReportCase.StockUploadSummaryRptCase

    '    ReportViewer.Active = Constant.Common.ActiveStatus
    '    ReportViewer.StckUpldProcessYear = lblYear.Text
    '    ReportViewer.StckUpldProcessMonth = lblMonth.Text

    '    ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)

    'End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As EventArgs) Handles ImageButton2.Click
        CheckLogin()

        Dim ReportViewer As New ReportViewer_DC

        ReportViewer.ReportFileName = AppDomain.CurrentDomain.BaseDirectory + Constant.ReportView.ReportFileLoc + Constant.ReportView.ReportName.Stock_Upload_Summary_Report
        ReportViewer.ReportCase = Constant.ReportView.ReportCase.StockUploadSummaryRptCase

        ReportViewer.Active = Constant.Common.ActiveStatus
        ReportViewer.StckUpldProcessYear = lblYear.Text
        ReportViewer.StckUpldProcessMonth = lblMonth.Text

        ClientScript.RegisterStartupScript(Me.GetType(), "Show Report", "<script language='javascript'>fnNewWindow('ReportViewer.aspx','_blank')</script>", False)
    End Sub

    Protected Sub gvNotFound_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvNotFound.PageIndexChanging
        gvNotFound.PageIndex = e.NewPageIndex
        GetErrorDetails()

    End Sub

    Protected Sub gvNotFound_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNotFound.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim pageIdx As Integer = gvNotFound.PageIndex * ddlPageSize.SelectedValue
            e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
            
        End If


        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            row = e.Row.Controls(0).Controls(0).Controls(0)
            For Each cell As TableCell In row.Cells
                Dim lb As Control = cell.Controls(0)


                If (TypeOf (lb) Is Label) Then

                    'CType(lb, Label).ForeColor = System.Drawing.Color.Red
                    CType(lb, Label).CssClass = "lblpager"
                    CType(lb, Label).Width = 20
                    CType(lb, Label).Height = 15
                    'set current pager

                ElseIf (TypeOf (lb) Is LinkButton) Then

                    CType(lb, LinkButton).CssClass = "lnkpager"
                    CType(lb, LinkButton).Width = 20
                    CType(lb, LinkButton).Height = 15
                    CType(lb, LinkButton).ForeColor = Drawing.Color.Black
                End If

            Next
        End If
    End Sub

End Class
