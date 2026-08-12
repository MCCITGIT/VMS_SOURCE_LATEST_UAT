
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Reporting
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Partial Class ReportViewer
    Inherits System.Web.UI.Page



    'added by Rohan on trial basis to crystal report blank screen problem on 24/06/2011   ---  start
    Dim report As ReportDocument
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        report = New ReportDocument
    End Sub
    'added by Rohan on trial basis to crystal report blank screen problem on 24/06/2011   ---  end

    'Protected Sub CrystalReportViewer1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load



    'End Sub
    'Protected Sub CrystalReportViewer1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Load



    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DatasetClear As Boolean

            Dim Ds As Reports = New Reports
            Dim Reportviewer As New ReportViewer_DC

            'commented by Rohan on trial basis to crystal report blank screen problem on 24/06/2011
            'Dim report As New ReportDocument
            report.FileName = Reportviewer.ReportFileName
            ' Dim ReportDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            ' ReportDoc.Load(Reportviewer.ReportFileName)

            'Dim report As New ReportDocument
            'report.FileName = Reportviewer.ReportFileName
            ' Dim ReportDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            ' ReportDoc.Load(Reportviewer.ReportFileName)



            Dim DataSource As String = ConfigurationManager.AppSettings("DBServerName").ToString()
            Dim InitialCatalog As String = ConfigurationManager.AppSettings("DBName").ToString()
            Dim User As String = ConfigurationManager.AppSettings("DBUserName").ToString()
            Dim Password As String = ConfigurationManager.AppSettings("DBPassword").ToString()

            Dim Conn As SqlConnection = New SqlConnection("Data Source=" + DataSource + ";Initial Catalog=" + InitialCatalog + ";User ID=" + User + ";Password=" + Password + "")

            Select Case Reportviewer.ReportCase


                'Added by Debayan Biswas on 07-11-2011 For Estimated_Data_Despatched_Status_Report
                'Start
                Case Constant.ReportView.ReportCase.EstmtnDataDsptchdStatRptCase

                    Conn.Open()

                    Dim EstmtnDataDsptchdStat As New SqlDataAdapter(Constant.StoreProcedures.Estimation_Data_Get_Details_Report, Conn)
                    EstmtnDataDsptchdStat.SelectCommand.CommandType = CommandType.StoredProcedure

                    EstmtnDataDsptchdStat.SelectCommand.Parameters.Add(New SqlParameter("@Region", IIf(Reportviewer.Region <> String.Empty, Reportviewer.Region, DBNull.Value)))
                    EstmtnDataDsptchdStat.SelectCommand.Parameters.Add(New SqlParameter("@Depot", IIf(Reportviewer.EstmtdDataDepot <> String.Empty, Reportviewer.EstmtdDataDepot, DBNull.Value)))
                    EstmtnDataDsptchdStat.SelectCommand.Parameters.Add(New SqlParameter("@Unit", IIf(Reportviewer.EstmtdDataUnit <> String.Empty, Reportviewer.EstmtdDataUnit, DBNull.Value)))
                    EstmtnDataDsptchdStat.SelectCommand.Parameters.Add(New SqlParameter("@SkuCode", IIf(Reportviewer.EstmtdDataSKUCode <> String.Empty, Reportviewer.EstmtdDataSKUCode, DBNull.Value)))
                    EstmtnDataDsptchdStat.SelectCommand.Parameters.Add(New SqlParameter("@FinYear", Reportviewer.EstmtdDataFinYear))
                    EstmtnDataDsptchdStat.SelectCommand.Parameters.Add(New SqlParameter("@Month", Reportviewer.EstmtdDataMonth))
                    EstmtnDataDsptchdStat.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))

                    EstmtnDataDsptchdStat.Fill(Ds, Constant.ReportDatasetTableName.Estimation_Data_Despatched_Status)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.Estimation_Data_Despatched_Status).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\Estimation_Data_Despatched_Status.aspx?NoData=" + Constant.Common.Yes)
                    End If
                    'End


                    'Added by Debayan Biswas on 10-11-2011 For Depot_Despatch_Unitwise_Report
                    'Start

                Case Constant.ReportView.ReportCase.DptDsptchUntWiseRptCase

                    Conn.Open()

                    Dim DepDsptchUntWise As New SqlDataAdapter(Constant.StoreProcedures.Depot_Despatch_Unitwise_Report, Conn)
                    DepDsptchUntWise.SelectCommand.CommandType = CommandType.StoredProcedure

                    DepDsptchUntWise.SelectCommand.Parameters.Add(New SqlParameter("@Region", IIf(Reportviewer.Region <> String.Empty, Reportviewer.Region, DBNull.Value)))
                    DepDsptchUntWise.SelectCommand.Parameters.Add(New SqlParameter("@Depot", IIf(Reportviewer.DptDsptchdUntWiseDepot <> String.Empty, Reportviewer.DptDsptchdUntWiseDepot, DBNull.Value)))
                    DepDsptchUntWise.SelectCommand.Parameters.Add(New SqlParameter("@Unit", IIf(Reportviewer.DptDsptchdUntWiseUnit <> String.Empty, Reportviewer.DptDsptchdUntWiseUnit, DBNull.Value)))
                    DepDsptchUntWise.SelectCommand.Parameters.Add(New SqlParameter("@Year", IIf(Reportviewer.DptDsptchdUntWiseFinYear <> String.Empty, Reportviewer.DptDsptchdUntWiseFinYear, DBNull.Value)))
                    DepDsptchUntWise.SelectCommand.Parameters.Add(New SqlParameter("@Month", IIf(Reportviewer.DptDsptchdUntWiseFinMonth <> String.Empty, Reportviewer.DptDsptchdUntWiseFinMonth, DBNull.Value)))
                    DepDsptchUntWise.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))

                    DepDsptchUntWise.Fill(Ds, Constant.ReportDatasetTableName.Depot_Despatched_Unitwise_Rpt)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.Depot_Despatched_Unitwise_Rpt).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\Depot_Despatches_Unitwise.aspx?NoData=" + Constant.Common.Yes)
                    End If

                    'End

                    'Added by Debayan Biswas on 10-11-2011 For StockUploadSummary_Report
                    'Start

                Case Constant.ReportView.ReportCase.StockUploadSummaryRptCase

                    Conn.Open()

                    Dim StckUpldSmry As New SqlDataAdapter(Constant.StoreProcedures.StockUploadSummary_Report, Conn)
                    StckUpldSmry.SelectCommand.CommandType = CommandType.StoredProcedure

                    StckUpldSmry.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))
                    StckUpldSmry.SelectCommand.Parameters.Add(New SqlParameter("@Year", Reportviewer.StckUpldProcessYear))
                    StckUpldSmry.SelectCommand.Parameters.Add(New SqlParameter("@Month", Reportviewer.StckUpldProcessMonth))
                    StckUpldSmry.SelectCommand.Parameters.Add(New SqlParameter("@Unit", IIf(Reportviewer.Unit <> String.Empty, Reportviewer.Unit, DBNull.Value)))

                    StckUpldSmry.Fill(Ds, Constant.ReportDatasetTableName.Stock_Upload_Summary_Report)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.Stock_Upload_Summary_Report).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\Stock_Upload_Summary.aspx?NoData=" + Constant.Common.Yes)
                    End If

                    'End

                    'Added by Debayan Biswas on 19-11-2011 For Despatched_Advice_Report
                    'Start

                Case Constant.ReportView.ReportCase.DespatchedAdviceRptCase

                    Conn.Open()

                    Dim DsptchdAdvc As New SqlDataAdapter(Constant.StoreProcedures.Despatched_Advice_Report, Conn)
                    DsptchdAdvc.SelectCommand.CommandType = CommandType.StoredProcedure

                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@Unit", Reportviewer.DsptchdAdviceUnit))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@Depot", Reportviewer.DsptchdAdviceDepot))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@FinYear", Reportviewer.DsptchdAdviceFinYear))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@ChallanNo", Reportviewer.DsptchdAdviceChlnNo))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))

                    DsptchdAdvc.Fill(Ds, Constant.ReportDatasetTableName.Despatched_Advice_Rpt_Tbl)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.Despatched_Advice_Rpt_Tbl).Rows.Count = 0) Then
                        DatasetClear = True
                    End If

                    'End

                Case Constant.ReportView.ReportCase.TokenDespatchedAdviceRptCase

                    Conn.Open()

                    Dim DsptchdAdvc As New SqlDataAdapter(Constant.StoreProcedures.GetToken_requisition_dtlsForVendorDespatch, Conn)
                    DsptchdAdvc.SelectCommand.CommandType = CommandType.StoredProcedure

                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@despatchId", Integer.Parse(Reportviewer.DsptchId)))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@requisitionId", 0))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@unit", String.Empty))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@trh_token_vendor", String.Empty))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@productId", DBNull.Value))
                    DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@packsize", DBNull.Value))
                    'DsptchdAdvc.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))

                    DsptchdAdvc.Fill(Ds, Constant.ReportDatasetTableName.GetToken_requisition_dtlsForVendorDespatch)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.GetToken_requisition_dtlsForVendorDespatch).Rows.Count = 0) Then
                        DatasetClear = True
                    Else
                        Dim dt As DataTable = Ds.Tables(Constant.ReportDatasetTableName.GetToken_requisition_dtlsForVendorDespatch).AsEnumerable().Where(Function(s) s.Field(Of Integer)("despatch_qty") > 0).CopyToDataTable
                        dt.TableName = Constant.ReportDatasetTableName.GetToken_requisition_dtlsForVendorDespatch
                        Ds.Tables.Remove(Constant.ReportDatasetTableName.GetToken_requisition_dtlsForVendorDespatch)
                        Ds.Tables.Add(dt)
                    End If


                Case Constant.ReportView.ReportCase.MonthlyUnitDespatchReportCase

                    Conn.Open()

                    Dim MonthlyDespatchReport As New SqlDataAdapter(Constant.StoreProcedures.MonthlyUnitDespatch_Report, Conn)

                    MonthlyDespatchReport.SelectCommand.CommandType = CommandType.StoredProcedure
                    MonthlyDespatchReport.SelectCommand.Parameters.Add(New SqlParameter("@region", IIf(Reportviewer.Region <> String.Empty, Reportviewer.Region, DBNull.Value)))
                    MonthlyDespatchReport.SelectCommand.Parameters.Add(New SqlParameter("@depot", IIf(Reportviewer.Depot <> String.Empty, Reportviewer.Depot, DBNull.Value)))
                    MonthlyDespatchReport.SelectCommand.Parameters.Add(New SqlParameter("@unit ", Reportviewer.Unit))
                    MonthlyDespatchReport.SelectCommand.Parameters.Add(New SqlParameter("@ProcessYr", Reportviewer.ProcessYr))
                    MonthlyDespatchReport.SelectCommand.Parameters.Add(New SqlParameter("@ProcessMnth", Reportviewer.ProcessMnth))
                    MonthlyDespatchReport.SelectCommand.Parameters.Add(New SqlParameter("@active", Reportviewer.Active))
                    MonthlyDespatchReport.Fill(Ds, Constant.ReportDatasetTableName.MonthlyUnitDespatchDataSet)

                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.MonthlyUnitDespatchDataSet).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\Monthly_Unit_Despatch.aspx?NoData=" + Constant.Common.Yes)
                    End If

                    'Added by Debayan Biswas on 21-12-2011 For Unitwise_SKU_Despatched_Report
                    'Start

                Case Constant.ReportView.ReportCase.UntWisSKUDsptchRptCase

                    Conn.Open()

                    Dim UntWisSKUDsptch As New SqlDataAdapter(Constant.StoreProcedures.Unitwise_SKU_Despatch_Report, Conn)
                    UntWisSKUDsptch.SelectCommand.CommandType = CommandType.StoredProcedure

                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Unit", IIf(Reportviewer.UnitwiseSKUDsptch_Unit <> String.Empty, Reportviewer.UnitwiseSKUDsptch_Unit, DBNull.Value)))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Year", Reportviewer.UnitwiseSKUDsptch_FinYear))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Month", Reportviewer.UnitwiseSKUDsptch_Month))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Depot", IIf(Reportviewer.Depot <> String.Empty, Reportviewer.Depot, DBNull.Value)))

                    UntWisSKUDsptch.Fill(Ds, Constant.ReportDatasetTableName.UnitWise_SKU_Despatch_Rpt_Tbl)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.UnitWise_SKU_Despatch_Rpt_Tbl).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\Unitwise_SKU_Despatch.aspx?NoData=" + Constant.Common.Yes)
                    End If

                Case Constant.ReportView.ReportCase.UntWisSKUDsptchSmmryRptCase

                    Conn.Open()

                    Dim UntWisSKUDsptch As New SqlDataAdapter(Constant.StoreProcedures.Unitwise_SKU_Despatch_Report_For_Summary, Conn)
                    UntWisSKUDsptch.SelectCommand.CommandType = CommandType.StoredProcedure

                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Unit", IIf(Reportviewer.UnitwiseSKUDsptch_Unit <> String.Empty, Reportviewer.UnitwiseSKUDsptch_Unit, DBNull.Value)))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Year", Reportviewer.UnitwiseSKUDsptch_FinYear))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Month", Reportviewer.UnitwiseSKUDsptch_Month))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))
                    UntWisSKUDsptch.SelectCommand.Parameters.Add(New SqlParameter("@Depot", IIf(Reportviewer.Depot <> String.Empty, Reportviewer.Depot, DBNull.Value)))

                    UntWisSKUDsptch.Fill(Ds, Constant.ReportDatasetTableName.UnitWise_SKU_Despatch_Summary_Rpt_Tbl)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.UnitWise_SKU_Despatch_Summary_Rpt_Tbl).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\Unitwise_SKU_Despatch.aspx?NoData=" + Constant.Common.Yes)
                    End If

                    'End


                    'Added by Debayan Biswas on 15-11-2011 For Monthly_Depot_Indent_List_Report
                    'Start

                Case Constant.ReportView.ReportCase.MonthlyDepotIndentListRptCase

                    Conn.Open()

                    Dim MnthlyDptIndntLst As New SqlDataAdapter(Constant.StoreProcedures.Mnthly_Dpt_Indent_List_Report, Conn)
                    MnthlyDptIndntLst.SelectCommand.CommandType = CommandType.StoredProcedure

                    MnthlyDptIndntLst.SelectCommand.Parameters.Add(New SqlParameter("@Region", IIf(Reportviewer.MnthlyDptIndntLstRptRegion <> String.Empty, Reportviewer.MnthlyDptIndntLstRptRegion, DBNull.Value)))
                    MnthlyDptIndntLst.SelectCommand.Parameters.Add(New SqlParameter("@Depot", IIf(Reportviewer.MnthlyDptIndntLstRptDepot <> String.Empty, Reportviewer.MnthlyDptIndntLstRptDepot, DBNull.Value)))
                    MnthlyDptIndntLst.SelectCommand.Parameters.Add(New SqlParameter("@Year", Reportviewer.MnthlyDptIndntLstRptFinYear))
                    MnthlyDptIndntLst.SelectCommand.Parameters.Add(New SqlParameter("@Month", Reportviewer.MnthlyDptIndntLstRptMonth))
                    MnthlyDptIndntLst.SelectCommand.Parameters.Add(New SqlParameter("@Active", Reportviewer.Active))

                    MnthlyDptIndntLst.Fill(Ds, Constant.ReportDatasetTableName.Monthly_Dpt_Indent_List_Rpt_Tbl)
                    Conn.Close()

                    If (Ds.Tables(Constant.ReportDatasetTableName.Monthly_Dpt_Indent_List_Rpt_Tbl).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\Monthly_Depot_Indent_List.aspx?NoData=" + Constant.Common.Yes)
                    End If

                    'End

                    'Added by Debayan Biswas on 07-01-2012 For User_Profile_List_Report
                    'Start
                Case Constant.ReportView.ReportCase.UserProfileReportCase

                    Conn.Open()
                    Dim UpRptDS As New SqlDataAdapter(Constant.StoreProcedures.UserProfile_List_Report, Conn)


                    UpRptDS.SelectCommand.CommandType = CommandType.StoredProcedure
                    UpRptDS.SelectCommand.Parameters.Add(New SqlParameter("@Company", Reportviewer.up_Company))
                    UpRptDS.SelectCommand.Parameters.Add(New SqlParameter("@region", IIf(Reportviewer.up_Region <> String.Empty, Reportviewer.up_Region, DBNull.Value)))
                    UpRptDS.SelectCommand.Parameters.Add(New SqlParameter("@depot", IIf(Reportviewer.up_Depot <> String.Empty, Reportviewer.up_Depot, DBNull.Value)))


                    UpRptDS.Fill(Ds, Constant.ReportDatasetTableName.UserProfileReport_DT)
                    Conn.Close()
                    If (Ds.Tables(Constant.ReportDatasetTableName.UserProfileReport_DT).Rows.Count = 0) Then
                        DatasetClear = True
                        Response.Redirect("~\User_Profile_List_Report.aspx?NoData=" + "Yes")
                    End If
                    'End


            End Select



            If (Not (DatasetClear)) Then
                ' ReportDoc.SetDataSource(Ds)
                'CrystalReportViewer1.ReportSource = ReportDoc
                'CrystalReportViewer1.RefreshReport()
                report.SetDataSource(Ds)
                'report = ReportDoc
                report.Refresh()
                'report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "")
                If (Reportviewer.ReportType = Constant.Common.PdfFormat Or Reportviewer.ReportType = String.Empty) Then
                    report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "")
                ElseIf (Reportviewer.ReportType = Constant.Common.ExcelFormat) Then
                    report.ExportToHttpResponse(ExportFormatType.Excel, Response, True, "")

                ElseIf (Reportviewer.ReportType = Constant.Common.WordFormat) Then
                    report.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "")
                End If
            End If


        Catch ex As Exception
            Label1.Text = ex.ToString()
        End Try

    End Sub


    'added by Rohan on trial basis to crystal report blank screen problem on 24/06/2011   ---  start
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        report.Dispose()
    End Sub
    'added by Rohan on trial basis to crystal report blank screen problem on 24/06/2011   ---  end
End Class
