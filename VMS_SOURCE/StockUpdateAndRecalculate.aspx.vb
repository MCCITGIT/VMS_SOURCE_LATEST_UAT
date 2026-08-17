'***************************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : User_Form_Access.aspx.vb
'Created Date	: 08/12/2011
'Created By	    : Riddhikusal Datta 
'Version	    : R01.00.00
'Description	: Code behind for StockUpdateAndRecalculate

'Modified By       Modified On       Version         Reason

'****************************************************************
Imports Microsoft.VisualBasic

Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient



Partial Class StockUpdateAndRecalculate
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
#Region "Get Screen Details"
    Private Sub GetScreenDetails()
        Dim ScreenDS As DataSet
        Dim StockObj As New StockUpdateClass
        ScreenDS = StockObj.GetSCreenDetails
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            lblYear.Text = ScreenDS.Tables(0).Rows(0)("year").ToString
            lblMonth.Text = ScreenDS.Tables(0).Rows(0)("month").ToString
        End If
    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckLogin()
            GetScreenDetails()
            rbtnBoth.Checked = True
            tabGrid.Visible = False
            btnUpload.Attributes.Add("onClick", "return ValidateUpload()")
        End If

    End Sub
#Region "Save File"
    Private Sub SaveFile()
        btnUpload.Enabled = False

        'D = Date.Now.ToLongTimeString
        'filesavestrt = "File Saving strt: " + D

        'Dim upload_filename As String = Upload_File.PostedFile.FileName.ToString
        'Dim fn As String
        'Dim sysdate As String = Format(Date.Now, "dd-MM-yyyy")
        'If Not Upload_File.PostedFile Is Nothing And Upload_File.PostedFile.ContentLength > 0 Then
        '    fn = System.IO.Path.GetFileName(Upload_File.PostedFile.FileName)
        'End If
        'Dim extension As String = String.Empty
        'If (fn.LastIndexOf(".") >= 0) Then
        '    extension = fn.Substring(fn.LastIndexOf(".") + 1)
        'End If

        'fn = "Stock_Update" + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString + "." + extension
        'Dim saveLocation As String

        'saveLocation = Server.MapPath("Documents") & "\Stock_Master" & "\" & sysdate & "\" & fn

        'Dim savefolder As String = Server.MapPath("Documents") & "\Stock_Master" & "\" & sysdate

        'If Not Directory.Exists(savefolder) Then
        '    Directory.CreateDirectory(savefolder)
        'End If

        'Upload_File.PostedFile.SaveAs(saveLocation)

        'D = Date.Now.ToLongTimeString
        'filesaveend = "File Saving end: " + D
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()


        Dim StockObj As New StockUpdateClass

        'D = Date.Now.ToLongTimeString
        'tmprdmfilestrt = "Temp. Redemption insert strt: " + D

        Try
            StockObj.UpdateStock(sqlConn, sqlTrans)
            numRowsAffected = Readfile(userInfo.userIDEntity, hdnFileName.Value, sqlConn, sqlTrans)
            If numRowsAffected > 0 Then
                sqlTrans.Commit()
                'CreateMessageAlert("Upload Complete", "alertKey")

            Else
                sqlTrans.Rollback()
            End If
            'D = Date.Now.ToLongTimeString
            'tmprdmfileend = "Temp. Redemption insert end: " + D
        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                'SqlTrans is set to Rollback to go back to the beginning of the transaction
                sqlTrans.Rollback()

            End If
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = ex.Message
            Server.Transfer(returnUrl)

        Finally
            If Not (sqlConn Is Nothing) Then
                'sqlConn is set to close state after completing the transaction
                sqlConn.Close()
                btnUpload.Enabled = True
                'lblupload.Visible = False

                'Server.Transfer("~/Dealer_Upload.aspx")

            End If
        End Try

      
    End Sub
#End Region

#Region "Read Stock text file"
    Public Function Readfile(ByVal user As String, ByVal filename As String, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim A(25) As String
        Dim data As String
        Dim k As Integer = 1
        'Dim sqlConn As New SqlConnection
        'Dim sqlTrans As SqlTransaction

        Dim lineNo As Integer = 0
        Try
            sr = File.OpenText(filename)




            'sqlConn = DBFactory.GetHelper.OpenConnection()

            'sqlTrans = sqlConn.BeginTransaction()


            Do While sr.EndOfStream = False
                linerd = sr.ReadLine
                linerd = linerd & "|"
                For i As Integer = 1 To Len(linerd)
                    If Mid(linerd, i, 1) = "|" Then
                        A(k) = data
                        k = k + 1
                        data = ""
                    Else
                        data = data & Mid(linerd, i, 1)
                    End If
                Next
                Dim StockObj As New StockUpdateClass

                If lineNo = 0 Then
                    'Dim ds As DataSet
                    'ds = StockObj.GetStockCountAsOn(A)
                    numRowsAffected = StockObj.DeleteStockMaster(sqlconn, sqltrans, A)
                End If

                numRowsAffected = StockObj.InsertStockMaster(sqlconn, sqltrans, A)


                'ProgressBar1.PerformStep()
                data = ""
                k = 1
                lineNo += 1
            Loop
            'sr.Close()
            'If numRowsAffected > 0 Then
            '    sqltrans.Commit()
            '    CreateMessageAlert("Upload Complete", "alertKey")

            '    Dim strScript As String = "<script language=JavaScript>document.getElementById('progressbar').style.display = 'none'; document.getElementById('lblupload').innerHTML = ""; document.getElementById('btnUpload').disabled = false;</script>"
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alertKey", strScript)
            'Else
            '    sqltrans.Rollback()

            'End If
        Catch ex As Exception

            Dim returnUrl As String = "~/ExceptionPage.aspx"
            'Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Session(Constant.SessionKeys.ErrMessage) = "Error at Line No. " + lineNo.ToString + Environment.NewLine + ex.Message
            Server.Transfer(returnUrl)
        Finally
            'sqlConn.Close()
            btnUpload.Enabled = True
        End Try

        Return numRowsAffected


    End Function
#End Region

#Region "Display Stock Details after Upload"
    Private Sub DisplayStockDetails()
        PageSizeDropdown()
        BindGrid()

    End Sub
#End Region

#Region "Bind Grid"
    Private Sub BindGrid()
        CheckLogin()
        Dim ScreenDS As DataSet
        Dim StockObj As New StockUpdateClass
        ScreenDS = StockObj.GetStockDetails
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            gvStockDetails.DataSource = ScreenDS
            gvStockDetails.DataBind()
            tabGrid.Visible = True
            '--modified by deepak on 18/01/2012
            gvStockDetails.Visible = True
            ddlPageSize.Visible = True
            Label4.Visible = True
            ddlAll.Visible = True
        Else
            'tabGrid.Visible = False
            '--modified by deepak on 18/01/2012
            tabGrid.Visible = True
            ddlAll.Visible = True
            gvStockDetails.Visible = False
            ddlPageSize.Visible = False
            Label4.Visible = False


        End If
    End Sub
#End Region

#Region "Bind Grid For Error"
    Private Sub BindGridErr()
        CheckLogin()
        Dim ScreenDS As DataSet
        Dim StockObj As New StockUpdateClass
        ScreenDS = StockObj.GetStockDetailsErr
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            gvStockDetails.DataSource = ScreenDS
            gvStockDetails.DataBind()
            tabGrid.Visible = True
        Else
            tabGrid.Visible = False
        End If
    End Sub
#End Region

#Region "Populate page size dropdown"

    ' Populates the page size dropdown
    Private Sub PageSizeDropdown()
        ddlPageSize.Items.Clear()
        'Gets the page size from the web.config file
        Dim configPagesize As String = ConfigurationManager.AppSettings.Get("PlotPageSize")
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
        gvStockDetails.PageSize = ddlPageSize.SelectedValue
    End Sub

#End Region
#Region "Update Load"
    Private Sub UpdateLoad()
        CheckLogin()
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()


        Dim StockObj As New StockUpdateClass

        numRowsAffected = StockObj.UpdateLoadMaster(userInfo.userIDEntity, sqlConn, sqlTrans)
        If numRowsAffected > 0 Then
            sqlTrans.Commit()
        Else
            sqlTrans.Rollback()
        End If
    End Sub
#End Region

#Region "Process"
    Private Sub Process()
        CheckLogin()
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction
        D1 = Date.Now.ToLongTimeString
        hdnStart.Value = Format(DateTime.Now, "hh:mm:ss:fff")
        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()
        numRowsAffected = ProcessLoadMaster(sqlConn, sqlTrans)
        If numRowsAffected > 0 Then
            numRowsAffected += AutoIndentCalculation(sqlConn, sqlTrans)
            If numRowsAffected > 0 Then
                sqlTrans.Commit()
            Else
                sqlTrans.Rollback()
            End If
        Else
            sqlTrans.Rollback()
        End If
        hdnEnd.Value = Format(DateTime.Now, "hh:mm:ss:fff")
        D2 = Date.Now.ToLongTimeString
        lblEclapsedTime.Text = (D2 - D1).ToString
    End Sub
#End Region

#Region "Process Load Master"
    Function ProcessLoadMaster(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        CheckLogin()
        Dim numRowsAffected As Integer

        Dim StockObj As New StockUpdateClass

        numRowsAffected = StockObj.UpdateLoadMasterCursor(userInfo.userIDEntity, sqlConn, sqlTrans)

        Return numRowsAffected
    End Function
#End Region
#Region "Calculate Auto Indent in Load Master"
    Function AutoIndentCalculation(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        CheckLogin()
        Dim numRowsAffected As Integer

        Dim StockObj As New StockUpdateClass

        numRowsAffected = StockObj.CalculateAutoIndent(userInfo.userIDEntity, sqlConn, sqlTrans)
        Return numRowsAffected
    End Function
#End Region
#Region "Get Error Count"
    Private Sub GetErrorCount()
        Dim ScreenDS As DataSet
        Dim StockObj As New StockUpdateClass
        ScreenDS = StockObj.GetErrorNo
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            Dim errcount As Integer = ScreenDS.Tables(0).Rows(0)("ErrCount")
            If errcount > 0 Then
                LabelErr.Text = "Error : " & errcount.ToString & " SKU(s) are not found in Vendor Master"
            End If
        End If
    End Sub
#End Region


#Region "Read File Before save"
    Private Sub CheckFile()
        Dim fn As String
        Dim A(25) As String
        Dim data As String
        Dim k As Integer = 1
        Dim sysdate As String = Format(Date.Now, "dd-MM-yyyy")

        If Not Upload_File.PostedFile Is Nothing AndAlso Upload_File.PostedFile.ContentLength > 0 Then
            fn = System.IO.Path.GetFileName(Upload_File.PostedFile.FileName)
        End If
        Dim extension As String = String.Empty
        If (fn.LastIndexOf(".") >= 0) Then
            extension = fn.Substring(fn.LastIndexOf(".") + 1)
        End If

        fn = "Stock_Update" + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString + "." + extension

        saveLocation = Server.MapPath("Documents") & "\Stock_Master" & "\" & sysdate & "\" & fn
        hdnFileName.Value = saveLocation
        Dim savefolder As String = Server.MapPath("Documents") & "\Stock_Master" & "\" & sysdate

        If Not Directory.Exists(savefolder) Then
            Directory.CreateDirectory(savefolder)
        End If

        Upload_File.PostedFile.SaveAs(saveLocation)
        sr = File.OpenText(saveLocation)


        Dim cnt As Integer


        Dim lineNo As Integer = 0

        Do While sr.EndOfStream = False
            linerd = sr.ReadLine
            linerd = linerd & "|"
            For i As Integer = 1 To Len(linerd)
                If Mid(linerd, i, 1) = "|" Then
                    A(k) = data
                    k = k + 1
                    data = ""
                Else
                    data = data & Mid(linerd, i, 1)
                End If
            Next
            Dim StockObj As New StockUpdateClass

            If lineNo = 0 Then
                Dim ds As DataSet

                ds = StockObj.GetStockCountAsOn(A)
                cnt = ds.Tables(0).Rows(0)(0)
                hdnStockAsOn.Value = A(4)

            End If
            'MsgBox(Mid(A(4), 7, 4))
            If Mid(A(4), 4, 2) <> Trim(lblMonth.Text) Or Mid(A(4), 7, 4) <> Trim(lblYear.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> popup('divPopup');</script>", False)
            End If


            GoTo Z
        Loop
Z:      sr.Close()

        If cnt > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> popup('divPopup');</script>", False)
        Else
            If rbtnBoth.Checked = True Then
                SaveFile()
                UpdateLoad()
                DisplayStockDetails()
                Process()
                GetErrorCount()
                DisplaySummery()
            ElseIf rbtnUpload.Checked Then
                SaveFile()
            End If

        End If


    End Sub
#End Region

#Region "Display Summery"
    Private Sub DisplaySummery()
        Dim ScreenDS As DataSet
        Dim StockObj As New StockUpdateClass
        ScreenDS = StockObj.GetRowCount
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            Dim errcount As Integer = ScreenDS.Tables(0).Rows(0)("RowCnt")

            lblTotalRecords.Text = errcount.ToString

        End If
        lblStartTime.Text = hdnStart.Value
        lblEndTime.Text = hdnEnd.Value
        'lblStockAsOn.Text = hdnStockAsOn.Value
        tabSummery.Visible = True
        DisplayStockDetails()
    End Sub
#End Region
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        'If rbtnBoth.Checked Then
        '    SaveFile()
        '    UpdateLoad()
        '    DisplayStockDetails()
        'End If
        CheckFile()
    End Sub

    Protected Sub gvStockDetails_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvStockDetails.PageIndexChanging
        gvStockDetails.PageIndex = e.NewPageIndex
        'comment by deepak 
        'BindGrid()
        If ddlAll.SelectedValue = "ALL" Then
            'PageSizeDropdown()
            BindGrid()
        Else
            'PageSizeDropdown()
            BindGridErr()
        End If
    End Sub

    Protected Sub gvStockDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvStockDetails.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim pageIdx As Integer = gvStockDetails.PageIndex * ddlPageSize.SelectedValue
            e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)
            If rowView("stk_no_sku") = "Y" Then
                e.Row.BackColor = Drawing.Color.Red
                e.Row.ForeColor = Drawing.Color.White
            End If
        End If

        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            'row = e.Row.Controls(0).Controls(0).Controls(0)
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

    

    Protected Sub ddlPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        gvStockDetails.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue)
        'commented by Deepak
        'BindGrid()
        If ddlAll.SelectedValue = "ALL" Then
            'PageSizeDropdown()
            BindGrid()
        Else
            'PageSizeDropdown()
            BindGridErr()
        End If
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Process()
        GetErrorCount()
        DisplaySummery()
    End Sub

    Protected Sub gvStockDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvStockDetails.SelectedIndexChanged

    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        SaveFile()
        UpdateLoad()
        'DisplayStockDetails()
        Process()
        GetErrorCount()
        DisplaySummery()
    End Sub

    Protected Sub rbtnUpdate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnUpdate.CheckedChanged
        If rbtnUpdate.Checked = True Then
            UpdateLoad()
            DisplayStockDetails()
            Upload_File.Enabled = False
            btnUpload.Visible = False
            Process()
            GetErrorCount()
            DisplaySummery()
        End If
    End Sub

   
    Protected Sub rbtnUpload_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnUpload.CheckedChanged
        If rbtnUpload.Checked Then
            Upload_File.Enabled = True
            btnUpload.Enabled = True
            tabGrid.Visible = False
            tabSummery.Visible = False
            btnUpload.Visible = True
            LabelErr.Text = ""
        End If

    End Sub

   
    Protected Sub rbtnBoth_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnBoth.CheckedChanged
        If rbtnBoth.Checked Then
            Upload_File.Enabled = True
            btnUpload.Enabled = True
            tabGrid.Visible = False
            LabelErr.Text = ""
            tabSummery.Visible = False
            btnUpload.Visible = True
        End If

    End Sub

    Protected Sub btnBoth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBoth.Click
        Upload_File.Enabled = True
        btnUpload.Enabled = True
        tabGrid.Visible = False
        LabelErr.Text = ""
        tabSummery.Visible = False
        btnUpload.Visible = True
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

    Protected Sub ddlAll_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAll.SelectedIndexChanged
        If ddlAll.SelectedValue = "ALL" Then
            PageSizeDropdown()
            BindGrid()
        Else
            PageSizeDropdown()
            BindGridErr()
        End If
    End Sub


End Class
