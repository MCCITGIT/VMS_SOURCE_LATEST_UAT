<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dispatch_List.aspx.vb" Inherits="Dispatch_List" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server" id="head">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <title>Dispatch List</title>

    <link href="includes/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="includes/style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />

    <link type="text/css" rel="stylesheet" href="includes/select2.min.css" />
    <link type="text/css" rel="stylesheet" href="includes/select2-bootstrap4.min.css" />
    <link href="includes/sumoselect.css" rel="stylesheet" />

    <link href="includes/upgrad-style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />
    <link href="includes/rm-procurement.css?v=<%= DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />

    <link rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap" rel="stylesheet" />

    <script type="text/javascript"
        src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script type="text/javascript"
        src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

    <script type="text/javascript" src="Scripts/select2.full.min.js"></script>

    <style type="text/css">
        html,
        body {
            margin: 0;
            padding: 0;
            width: 100%;
            min-height: 100%;
        }

        body {
            background: #f4efe8 !important;
            font-family: "Inter", Arial, Helvetica, sans-serif;
            color: #1f2937;
        }

        .contentMainBody {
            margin: 0 !important;
            padding: 22px 28px 40px !important;
            width: 100% !important;
            max-width: 100% !important;
            min-height: 100vh;
            box-sizing: border-box;
            background: #f4efe8 !important;
        }

        .rm-page-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 16px;
            margin-bottom: 18px;
        }

        .rm-page-header-left {
            display: flex;
            align-items: center;
            min-width: 0;
        }

        .rm-brand-icon {
            width: 46px;
            height: 46px;
            min-width: 46px;
            margin-right: 14px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            color: #2f6fed;
            font-size: 28px;
        }

        .rm-page-title {
            margin: 0 0 4px 0;
            color: #2f6fed;
            font-size: 28px;
            line-height: 1.15;
            font-weight: 800;
            letter-spacing: -0.03em;
        }

        .rm-page-subtitle {
            margin: 0;
            color: #8a93a3;
            font-size: 13px;
            line-height: 1.45;
            font-weight: 400;
            max-width: 640px;
        }

        .rm-header-pills {
            display: flex;
            align-items: center;
            gap: 10px;
            flex-shrink: 0;
        }

        .rm-date-pill,
        .rm-vendor-pill {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            padding: 8px 14px;
            background: #ffffff;
            border: 1px solid #e8e2d8;
            border-radius: 999px;
            color: #2f6fed;
            font-size: 13px;
            font-weight: 600;
            box-shadow: 0 4px 14px rgba(31, 55, 78, 0.05);
        }

        .rm-vendor-pill:has(.rm-vendor-label:empty) {
            display: none;
        }

        .rm-vendor-label {
            color: #1f2937;
            font-size: 13px !important;
            font-weight: 600 !important;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            max-width: 200px;
        }

        .rm-stat-row {
            display: grid;
            grid-template-columns: repeat(3, minmax(0, 1fr));
            gap: 8px;
            margin-bottom: 10px;
        }

        .rm-stat-card {
            background: #ffffff;
            border: 1px solid #eee8de;
            border-radius: 12px;
            padding: 8px 12px;
            box-shadow: 0 8px 22px rgba(80, 60, 30, 0.06);
            min-height: 0;
            display: flex;
            align-items: center;
            gap: 10px;
            transition: box-shadow 0.22s ease, border-color 0.22s ease, transform 0.22s ease;
        }

        .rm-stat-icon {
            width: 18px;
            height: 18px;
            margin-bottom: 0;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 16px;
            flex-shrink: 0;
        }

        .rm-stat-icon.is-blue { color: #2f6fed; }
        .rm-stat-icon.is-orange { color: #e7a23a; }
        .rm-stat-icon.is-green { color: #22a35a; }

        .rm-stat-label {
            margin: 0;
            color: #9aa3b2;
            font-size: 10px;
            font-weight: 700;
            letter-spacing: 0.08em;
            text-transform: uppercase;
        }

        .rm-stat-value {
            margin: 0;
            color: #111827;
            font-size: 20px;
            line-height: 1;
            font-weight: 800;
        }

        .rm-list-card {
            background: #ffffff;
            border: 1px solid #eee8de;
            border-radius: 22px;
            box-shadow: 0 10px 28px rgba(80, 60, 30, 0.06);
            overflow: hidden;
            transition: box-shadow 0.22s ease, border-color 0.22s ease;
        }

        .rm-list-card-head {
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            gap: 16px;
            padding: 22px 24px 8px;
        }

        .rm-list-title {
            margin: 0 0 4px 0;
            color: #111827;
            font-size: 20px;
            font-weight: 800;
        }

        .rm-list-subtitle {
            margin: 0;
            color: #8a93a3;
            font-size: 13px;
        }

        .rm-filter-wrap {
            display: flex;
            align-items: center;
            gap: 10px;
            flex-wrap: wrap;
            justify-content: flex-end;
        }

        .rm-filter-wrap .form-group {
            margin: 0;
            min-width: 160px;
        }

        .form-control-label {
            display: none;
        }

        .form-control {
            height: 38px !important;
            padding: 6px 12px;
            background: #ffffff;
            border: 1px solid #d7dde6;
            border-radius: 12px;
            color: #334155;
            font-size: 13px;
            box-shadow: none !important;
            transition: background-color 0.2s ease, border-color 0.2s ease, box-shadow 0.2s ease;
        }

        .form-control:focus {
            border-color: #8fb0ea;
            box-shadow: 0 0 0 3px rgba(47, 111, 237, 0.12) !important;
        }

        .select2-container {
            width: 170px !important;
        }

        .select2-container--default .select2-selection--single {
            height: 38px !important;
            background: #ffffff !important;
            border: 1px solid #d7dde6 !important;
            border-radius: 12px !important;
            outline: none;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            height: 36px;
            line-height: 36px !important;
            padding-left: 12px !important;
            padding-right: 28px !important;
            color: #334155;
            font-size: 13px;
            font-weight: 600;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 36px !important;
            top: 0 !important;
            right: 4px !important;
        }

        .select2-container--default.select2-container--focus .select2-selection--single,
        .select2-container--default.select2-container--open .select2-selection--single {
            border-color: #8fb0ea !important;
        }

        .btn-search {
            height: 38px;
            width: 38px;
            min-width: 38px;
            padding: 0;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #2f6fed !important;
            border: 1px solid #2f6fed !important;
            border-radius: 12px !important;
            color: #ffffff !important;
            font-size: 13px;
            font-weight: 700;
            box-shadow: none !important;
            text-decoration: none !important;
            cursor: pointer;
            transition: background-color 0.18s ease, border-color 0.18s ease, color 0.18s ease, box-shadow 0.18s ease, transform 0.18s ease;
        }

        .btn-search:hover,
        .btn-search:focus {
            background: #1f5bd6 !important;
            border-color: #1f5bd6 !important;
            color: #ffffff !important;
            text-decoration: none !important;
            transform: translateY(-1px);
            box-shadow: 0 8px 18px rgba(47, 111, 237, 0.28) !important;
        }

        .search-icon {
            margin-right: 0;
            font-size: 13px;
        }

        .rm-list-body .table-responsive,
        .rm-grid-scroll {
            max-height: min(460px, calc(100vh - 280px));
            overflow-y: auto;
            overflow-x: hidden;
        }

        .rm-grid-scroll .upgradDataGrid thead th,
        .rm-grid-scroll .headerGrid th {
            position: sticky;
            top: 0;
            z-index: 2;
            background: #ffffff !important;
        }

        .rm-grid-scroll .PagerGrid {
            position: sticky;
            bottom: 0;
            z-index: 2;
            background: #ffffff !important;
        }

        .rm-list-body {
            padding: 8px 10px 18px;
        }

        .table-responsive {
            width: 100%;
            overflow-x: auto;
        }

        .upgradDataGrid {
            width: 100% !important;
            margin-bottom: 8px !important;
            border-collapse: collapse !important;
            border: 0 !important;
            font-size: 13px;
            color: #1f2937;
            background: transparent;
        }

        .upgradDataGrid th {
            padding: 12px 16px !important;
            background: transparent !important;
            color: #9aa3b2 !important;
            border: 0 !important;
            border-bottom: 1px solid #eef1f5 !important;
            font-size: 11px !important;
            font-weight: 700 !important;
            letter-spacing: 0.08em;
            text-transform: uppercase !important;
            vertical-align: middle !important;
            white-space: nowrap;
        }

        .upgradDataGrid td {
            padding: 14px 16px !important;
            background: transparent;
            color: #1f2937;
            border: 0 !important;
            border-bottom: 1px solid #f1f4f8 !important;
            font-size: 13px;
            line-height: 1.35;
            vertical-align: middle !important;
            transition: background-color 0.18s ease;
        }

        .upgradDataGrid tr:hover td {
            background: #fafbfd !important;
        }

        .upgradDataGrid tr:last-child td {
            border-bottom: 0 !important;
        }

        .request-id-wrap {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            color: #2f6fed;
            font-weight: 700;
        }

        .request-id-wrap i {
            font-size: 14px;
        }

        .request-id-text {
            color: #2f6fed;
            font-weight: 700;
        }

        .vendor-cell {
            display: flex;
            align-items: center;
            gap: 12px;
        }

        .vendor-avatar {
            width: 38px;
            height: 38px;
            min-width: 38px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #e8edf5;
            border-radius: 50%;
            color: #5b6b80;
            font-size: 12px;
            font-weight: 800;
        }

        .vendor-name {
            color: #111827;
            font-size: 13px;
            font-weight: 700;
            line-height: 1.2;
        }

        .vendor-sub {
            color: #9aa3b2;
            font-size: 11px;
            font-weight: 500;
        }

        .grid-action {
            width: 34px;
            height: 34px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #e8f0ff !important;
            border: 0 !important;
            border-radius: 50% !important;
            color: #2f6fed !important;
            font-size: 13px !important;
            text-decoration: none !important;
            transition: background-color 0.18s ease, color 0.18s ease, box-shadow 0.18s ease, transform 0.18s ease;
        }

        .grid-action:hover {
            background: #d7e6ff !important;
            color: #1f5bd6 !important;
            text-decoration: none !important;
            transform: translateY(-1px);
            box-shadow: 0 6px 14px rgba(47, 111, 237, 0.18);
        }

        .status-complete {
            width: 34px;
            height: 34px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #e8f8ee;
            color: #22a35a !important;
            border: 0;
            border-radius: 50%;
            font-size: 13px;
        }

        .PagerGrid {
            background: transparent !important;
        }

        .PagerGrid table {
            margin: 10px 12px 4px auto;
        }

        .PagerGrid td {
            border: 0 !important;
            padding: 2px !important;
        }

        .PagerGrid a,
        .PagerGrid span {
            min-width: 28px;
            height: 28px;
            padding: 3px 8px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            background: #ffffff;
            color: #2f6fed;
            font-size: 12px;
            text-decoration: none;
            transition: background-color 0.18s ease, color 0.18s ease, box-shadow 0.18s ease, transform 0.18s ease;
        }

        .PagerGrid span {
            background: #2f6fed;
            border-color: #2f6fed;
            color: #ffffff;
        }

        .PagerGrid a:hover {
            background: #eef4ff;
            color: #1f5bd6;
            transform: translateY(-1px);
            box-shadow: 0 4px 10px rgba(47, 111, 237, 0.16);
        }

        .upgradDataGrid td[colspan] {
            padding: 28px !important;
            text-align: center;
            color: #8a93a3;
        }

        @media (max-width: 991px) {
            .rm-stat-row {
                grid-template-columns: 1fr;
            }

            .rm-list-card-head {
                flex-direction: column;
            }

            .rm-filter-wrap {
                width: 100%;
                justify-content: stretch;
            }

            .select2-container,
            .rm-filter-wrap .form-group {
                width: 100% !important;
                min-width: 0;
            }

            .btn-search {
                width: 38px;
            }
        }

        @media (max-width: 767px) {
            .contentMainBody {
                padding: 14px !important;
            }

            .standalone-breadcrumbs {
                flex-direction: column;
                align-items: flex-start;
            }

            .standalone-breadcrumbs .pageTitle {
                font-size: 18px !important;
            }

            .standalone-breadcrumbs .rightFung {
                width: 100%;
                justify-content: flex-start;
            }
        }
    </style>
</head>

<body class="rm-module">
    <form id="form1" runat="server">
        <div class="contentMainBody">

            <div class="breadcrumbs standalone-breadcrumbs">
                <div class="leftFung">
                    <div class="pageTitleWrap">
                        <h3 class="pageTitle">Dispatch List</h3>
                        <p class="pageSubTitle">
                            Review incoming dispatch requests, monitor vendor status and open individual requests for further processing.
                        </p>
                    </div>
                </div>
                <div class="rightFung">
                    <span class="rm-date-pill">
                        <i class="far fa-calendar-alt"></i>
                        <%= DateTime.Now.ToString("dd MMMM yyyy") %>
                    </span>
                    <span class="rm-vendor-pill">
                        <asp:Label ID="lblRmVendor" runat="server" CssClass="rm-vendor-label"></asp:Label>
                    </span>
                </div>
            </div>

            <div class="rm-stat-row">
                <div class="rm-stat-card">
                    <div class="rm-stat-icon is-blue"><i class="fas fa-file-alt"></i></div>
                    <div>
                        <p class="rm-stat-label">Total Requests</p>
                        <p class="rm-stat-value">
                            <asp:Label ID="lblTotalRequests" runat="server" Text="0"></asp:Label>
                        </p>
                    </div>
                </div>
                <div class="rm-stat-card">
                    <div class="rm-stat-icon is-orange"><i class="far fa-clock"></i></div>
                    <div>
                        <p class="rm-stat-label">Pending</p>
                        <p class="rm-stat-value">
                            <asp:Label ID="lblPendingRequests" runat="server" Text="0"></asp:Label>
                        </p>
                    </div>
                </div>
                <div class="rm-stat-card">
                    <div class="rm-stat-icon is-green"><i class="fas fa-check-circle"></i></div>
                    <div>
                        <p class="rm-stat-label">Completed</p>
                        <p class="rm-stat-value">
                            <asp:Label ID="lblCompletedRequests" runat="server" Text="0"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>

            <div class="rm-list-card">
                <div class="rm-list-card-head">
                    <div>
                        <h5 class="rm-list-title">Recent Dispatch Requests</h5>
                        <p class="rm-list-subtitle">Select a vendor and status to quickly find the requests you need.</p>
                    </div>

                    <div class="rm-filter-wrap">
                        <div class="form-group" hidden="hidden">
                            <label class="form-control-label">Depot:</label>
                            <asp:DropDownList ID="ddlDepot" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>

                        <div id="divVendor" runat="server" class="form-group">
                            <label class="form-control-label">Vendor:</label>
                            <asp:DropDownList ID="ddlVendor" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="form-control-label">Status:</label>
                            <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>

                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-search" OnClick="btnSearch_Click" ToolTip="Search">
                            <i class="fas fa-search search-icon"></i>
                        </asp:LinkButton>
                    </div>
                </div>

                <div class="rm-list-body">
                    <div class="table-responsive rm-grid-scroll">
                        <asp:GridView
                            ID="gvDispatchList"
                            runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="10"
                            Visible="true"
                            BorderWidth="0"
                            CssClass="table table-hover upgradDataGrid"
                            EmptyDataText="No dispatch requests found."
                            PagerSettings-Mode="NumericFirstLast"
                            PagerSettings-PageButtonCount="5"
                            PagerSettings-FirstPageText="First"
                            PagerSettings-LastPageText="Last">

                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />

                            <Columns>
                                <asp:TemplateField HeaderText="SR. NO." HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrl" runat="server" Text='<%# (gvDispatchList.PageIndex * gvDispatchList.PageSize) + Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="REQUEST ID" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span class="request-id-wrap">
                                            <i class="fas fa-file-alt"></i>
                                            #<asp:Label ID="lblReqId" runat="server" CssClass="request-id-text" Text='<%# Bind("orh_Id") %>'></asp:Label>
                                        </span>
                                        <asp:HiddenField runat="server" ID="hdnReqId" Value='<%# Bind("orh_Id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="16%" />
                                    <ItemStyle HorizontalAlign="Left" Width="16%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="VENDOR" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div class="vendor-cell">
                                            <span class="vendor-avatar"><%# GetVendorInitials(Eval("unit_name")) %></span>
                                            <div>
                                                <div class="vendor-name">
                                                    <asp:Label ID="lblVendor" runat="server" Text='<%# Bind("unit_name") %>'></asp:Label>
                                                </div>
                                                <div class="vendor-sub">Vendor</div>
                                            </div>
                                        </div>
                                        <asp:HiddenField runat="server" ID="hdnVendorCode" Value='<%# Bind("orh_vendor_code") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="42%" />
                                    <ItemStyle HorizontalAlign="Left" Width="42%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="REQUEST DATE" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Bind("created_date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                    <ItemStyle HorizontalAlign="Left" Width="18%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ACTION" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="display: flex; align-items: center; justify-content: center;">
                                            <asp:LinkButton
                                                runat="server"
                                                ID="lbtnDetails"
                                                Text=""
                                                OnClick="lbtnDetails_Click"
                                                CommandName="Details"
                                                ToolTip="View Details"
                                                CssClass="grid-action">
                                                <i class="fas fa-arrow-right"></i>
                                            </asp:LinkButton>

                                            <asp:Label
                                                runat="server"
                                                ID="lblcheck"
                                                CssClass="status-complete"
                                                ToolTip="Completed"
                                                Visible="false">
                                                <i class="fas fa-check"></i>
                                            </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" Width="14%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            initializeSelect2();
        });

        function initializeSelect2() {
            $('.select2').each(function () {
                if (!$(this).hasClass('select2-hidden-accessible')) {
                    $(this).select2({
                        width: '100%'
                    });
                }
            });
        }
    </script>
</body>
</html>
