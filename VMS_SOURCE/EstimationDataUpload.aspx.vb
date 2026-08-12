'***************************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : User_Form_Access.aspx.vb
'Created Date	: 10/12/2011
'Created By	    : Riddhikusal Datta 
'Version	    : R01.00.00
'Description	: Code behind for EstimationDataUpload

'Modified By       Modified On       Version         Reason

'****************************************************************
Imports Microsoft.VisualBasic
Imports VMS.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.IO
Imports VMS.DataAccess
Imports System.Data.SqlClient

Partial Class EstimationDataUpload
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
        Dim EstObj As New EstimationUploadClass
        ScreenDS = EstObj.GetSCreenDetails
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            lblYear.Text = ScreenDS.Tables(0).Rows(0)("year").ToString
            lblMonth.Text = ScreenDS.Tables(0).Rows(0)("month").ToString
        End If
    End Sub
#End Region
#Region "Save File"
    Private Sub SaveFile()
        btnUpload.Enabled = False
        Dim numRowsAffected As Integer
        Dim sqlConn As New SqlConnection
        Dim sqlTrans As SqlTransaction

        sqlConn = DBFactory.GetHelper.OpenConnection()
        sqlTrans = sqlConn.BeginTransaction()
        D1 = Date.Now.ToLongTimeString
        hdnStart.Value = Format(DateTime.Now, "hh:mm:ss:fff")
        Dim EstObj As New EstimationUploadClass

        Try
            numRowsAffected = Readfile(userInfo.userIDEntity, hdnFileName.Value, sqlConn, sqlTrans)
            If numRowsAffected > 0 Then
                sqlTrans.Commit()
            Else
                sqlTrans.Rollback()
            End If
        Catch ex As Exception
            If Not (sqlTrans Is Nothing) Then
                sqlTrans.Rollback()
            End If
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        Finally
            If Not (sqlConn Is Nothing) Then
                sqlConn.Close()
                btnUpload.Enabled = True
            End If
        End Try
        hdnEnd.Value = Format(DateTime.Now, "hh:mm:ss:fff")
        D2 = Date.Now.ToLongTimeString
        lblEclapsedTime.Text = (D2 - D1).ToString
    End Sub
#End Region
#Region "Read Estimate Data file"
    Public Function Readfile(ByVal user As String, ByVal filename As String, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim A(25) As String
        Dim data As String
        Dim k As Integer = 1
        Dim lineNo As Integer = 0
        Try
            sr = File.OpenText(filename)

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
                Dim EstObj As New EstimationUploadClass
                If lineNo = 0 Then
                    numRowsAffected = EstObj.DeleteEstimationAsOn(sqlconn, sqltrans, A)
                End If
                numRowsAffected = EstObj.InsertEstimateData(sqlconn, sqltrans, A)
                data = ""
                k = 1
                lineNo += 1
            Loop
        Catch ex As Exception
            'MsgBox(lineNo.ToString)
            Dim returnUrl As String = "~/ExceptionPage.aspx"
            Session(Constant.SessionKeys.ErrMessage) = Constant.ErrorMessages.GeneralError
            Server.Transfer(returnUrl)
        Finally
            btnUpload.Enabled = True
        End Try
        Return numRowsAffected


    End Function
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

        fn = "Estimate_Data" + Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString + "." + extension

        saveLocation = Server.MapPath("Documents") & "\Estimate_Data" & "\" & sysdate & "\" & fn
        hdnFileName.Value = saveLocation
        Dim savefolder As String = Server.MapPath("Documents") & "\Estimate_Data" & "\" & sysdate

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
            Dim StockObj As New EstimationUploadClass

            If lineNo = 0 Then
                Dim ds As DataSet

                ds = StockObj.GetEstCountAsOn(A)
                cnt = ds.Tables(0).Rows(0)(0)
                hdnStockAsOn.Value = A(4)

            End If
            'MsgBox(Mid(A(4), 7, 4))
            If A(3).Trim <> Trim(lblMonth.Text) Or A(2).Trim <> Trim(lblYear.Text) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> popup('divPopup');</script>", False)
            End If


            GoTo Z
        Loop
Z:      sr.Close()

        If cnt > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Message", "<script type='text/javascript' language='javascript'> popup('divPopup');</script>", False)
        Else
            SaveFile()
            DisplaySummery()

        End If


    End Sub
#End Region
#Region "Display Summery"
    Private Sub DisplaySummery()
        Dim ScreenDS As DataSet
        Dim EstObj As New EstimationUploadClass
        ScreenDS = EstObj.GetRowCount(lblYear.Text.Trim, lblMonth.Text.Trim)
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
            Dim errcount As Integer = ScreenDS.Tables(0).Rows(0)("RowCnt")
            lblTotalRecords.Text = errcount.ToString
        End If
        ScreenDS = EstObj.GetNotFoundCount(lblYear.Text.Trim, lblMonth.Text.Trim)
        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then

            LabelErr.Text = "Depot Not Found : " & ScreenDS.Tables(0).Rows(0)("NoDepot") & "<BR>" & "SKU Not Found : " & ScreenDS.Tables(0).Rows(0)("NoSku")
        End If
        lblStartTime.Text = hdnStart.Value
        lblEndTime.Text = hdnEnd.Value
        'lblStockAsOn.Text = hdnStockAsOn.Value
        tabSummery.Visible = True
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
        gvEstimationDetails.PageSize = ddlPageSize.SelectedValue
    End Sub

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CheckLogin()
            GetScreenDetails()
            PageSizeDropdown()
            tabGrid.Visible = False
            btnUpload.Attributes.Add("onClick", "return ValidateUpload()")

        End If
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        CheckFile()
        PopulateEstimationDetails()
        PageSizeDropdown()
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        SaveFile()
        DisplaySummery()

    End Sub

#Region "Get Estimation Details"
    Private Sub PopulateEstimationDetails()
        Dim ScreenDs As DataSet
        Dim EstObj As New EstimationUploadClass
        Dim ProcessYr As String = lblYear.Text
        Dim ProcessMnths As String = lblMonth.Text

        ScreenDs = EstObj.GetEstimationDetails(Constant.Common.ActiveStatus, ProcessYr, ProcessMnths)
        If (Not (ScreenDs Is Nothing) AndAlso ScreenDs.Tables.Count > 0 AndAlso Not (ScreenDs.Tables(0) Is Nothing) AndAlso ScreenDs.Tables(0).Rows.Count > 0) Then
            gvEstimationDetails.DataSource = ScreenDs
            gvEstimationDetails.DataBind()
            tabGrid.Visible = True
            '--modified by deepak on 18/01/2012
            gvEstimationDetails.Visible = True
            ddlPageSize.Visible = True
            Label4.Visible = True
            ddlAll.Visible = True
        Else
            'tabGrid.Visible = False
            '--modified by deepak on 18/01/2012
            tabGrid.Visible = True
            ddlAll.Visible = True
            gvEstimationDetails.Visible = False
            ddlPageSize.Visible = False
            Label4.Visible = False

        End If


    End Sub
#End Region
#Region "Get Estimation Error Details"
    Private Sub PopulateEstimationErrorDetails()
        Dim ScreenDs As DataSet
        Dim EstObj As New EstimationUploadClass
        Dim ProcessYr As String = lblYear.Text
        Dim ProcessMnths As String = lblMonth.Text

        ScreenDs = EstObj.GetEstimationDetailsError(Constant.Common.ActiveStatus, ProcessYr, ProcessMnths)
        If (Not (ScreenDs Is Nothing) AndAlso ScreenDs.Tables.Count > 0 AndAlso Not (ScreenDs.Tables(0) Is Nothing) AndAlso ScreenDs.Tables(0).Rows.Count > 0) Then
            gvEstimationDetails.DataSource = ScreenDs
            gvEstimationDetails.DataBind()
            tabGrid.Visible = True
            gvEstimationDetails.Visible = True
            ddlPageSize.Visible = True
            Label4.Visible = True
            ddlAll.Visible = True

        Else
            'tabGrid.Visible = False
            '--modified by deepak on 18/01/2012
            tabGrid.Visible = True
            ddlAll.Visible = True
            gvEstimationDetails.Visible = False
            ddlPageSize.Visible = False
            Label4.Visible = False
        End If
       


    End Sub
#End Region
#Region "Gridview PageIndexChanging event handling."
    Protected Sub gvEstimationDetails_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvEstimationDetails.PageIndexChanging
        gvEstimationDetails.PageIndex = e.NewPageIndex
        'SaveSearchCriteria()
        'PopulateEstimationDetails()
        If ddlAll.SelectedValue = "ALL" Then
            PopulateEstimationDetails()
            'PageSizeDropdown()
        Else
            PopulateEstimationErrorDetails()
            'PageSizeDropdown()
        End If
    End Sub

#End Region
#Region "Gridview RowDataBound event handling."

    Protected Sub gvEstimationDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEstimationDetails.RowDataBound


        'If (e.Row.RowType = DataControlRowType.DataRow) Then
        '    'e.Row.Cells(0).Text = e.Row.RowIndex + 1
        '    'Dim pageIdx As Integer = gvEstimationDetails.PageIndex * ddlPageSize.SelectedValue
        '    'e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)
        '    Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

        '    ' e.Row.Cells(2).Text = "<a href='Vendor_SKU_AddUpdate.aspx?" + Constant.SessionKeys.SKUCode + "=" + rowView("v_sku_code") + "&" + Constant.SessionKeys.Unit + "=" + rowView("v_vendor_unit") + "'class='hl'>" + rowView("SkuDescription") + "</a>"

        'End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Dim pageIdx As Integer = gvEstimationDetails.PageIndex * ddlPageSize.SelectedValue
            'e.Row.Cells(0).Text = pageIdx + (e.Row.RowIndex + 1)

            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            If rowView("est_no_sku") = "Y" Then
                e.Row.BackColor = Drawing.Color.Red
                e.Row.ForeColor = Drawing.Color.White
            End If

            If rowView("est_no_depot") = "Y" Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.ForeColor = Drawing.Color.White
            End If

            If rowView("est_no_depot") = "Y" And rowView("est_no_sku") = "Y" Then
                e.Row.BackColor = Drawing.Color.Maroon
                e.Row.ForeColor = Drawing.Color.White
            End If
        End If


        If (e.Row.RowType = DataControlRowType.Pager) Then
            Dim row As TableRow = New TableRow
            row = e.Row.Controls(0).Controls(0).Controls(0)
            For Each cell As TableCell In row.Cells
                Dim lb As Control = cell.Controls(0)


                If (TypeOf (lb) Is Label) Then

                    CType(lb, Label).CssClass = "lblpager"
                    CType(lb, Label).Width = 20
                    CType(lb, Label).Height = 15

                ElseIf (TypeOf (lb) Is LinkButton) Then

                    CType(lb, LinkButton).CssClass = "lnkpager"
                    CType(lb, LinkButton).Width = 20
                    CType(lb, LinkButton).Height = 15
                    CType(lb, LinkButton).ForeColor = Drawing.Color.Black
                End If

            Next
        ElseIf (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim rowView As DataRowView = CType(e.Row.DataItem, DataRowView)

            'If (rowView("eng_left_yn").ToString.ToLower() = Constant.Common.ActiveStatus.ToLower()) Then
            '    e.Row.BackColor = Drawing.Color.Red
            '    e.Row.ForeColor = Drawing.Color.White
            '    e.Row.Cells(5).Text = "Yes"
            'Else
            '    e.Row.Cells(5).Text = "No"
            'End If
        End If
    End Sub

#End Region

    '#Region "Bind Grid"
    '    Private Sub BindGrid()
    '        CheckLogin()
    '        Dim ScreenDS As DataSet
    '        Dim StockObj As New StockUpdateClass
    '        ScreenDS = StockObj.GetStockDetails
    '        If (Not (ScreenDS Is Nothing) AndAlso ScreenDS.Tables.Count > 0 AndAlso Not (ScreenDS.Tables(0) Is Nothing) AndAlso ScreenDS.Tables(0).Rows.Count > 0) Then
    '            gvEstimationDetails.DataSource = ScreenDS
    '            gvEstimationDetails.DataBind()
    '            tabGrid.Visible = True
    '        Else
    '            tabGrid.Visible = False
    '        End If
    '    End Sub
    '#End Region
#Region "page size for grid view"
    Protected Sub ddlPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        gvEstimationDetails.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue)
        'PopulateEstimationDetails()
        If ddlAll.SelectedValue = "ALL" Then
            PopulateEstimationDetails()
            'PageSizeDropdown()
        Else
            PopulateEstimationErrorDetails()
            'PageSizeDropdown()
        End If
    End Sub
#End Region

    Protected Sub ddlAll_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAll.SelectedIndexChanged
        If ddlAll.SelectedValue = "ALL" Then
            PopulateEstimationDetails()
            PageSizeDropdown()
        Else
            PopulateEstimationErrorDetails()
            PageSizeDropdown()
        End If
    End Sub

    
End Class
