<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dispatch_List.aspx.vb" Inherits="Dispatch_List" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server" id="head">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <title>Dispatch List</title>

    <!-- Existing CSS -->
    <link href="includes/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="includes/style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />

    <link type="text/css" rel="stylesheet" href="includes/select2.min.css" />
    <link type="text/css" rel="stylesheet" href="includes/select2-bootstrap4.min.css" />
    <link href="includes/sumoselect.css" rel="stylesheet" />

    <link href="includes/upgrad-style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />

    <!-- Font Awesome -->
    <link rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />

    <!-- jQuery -->
    <script type="text/javascript"
        src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <!-- Bootstrap -->
    <script type="text/javascript"
        src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

    <!-- Select2 -->
    <script type="text/javascript" src="Scripts/select2.full.min.js"></script>

    <style type="text/css">
        /* ==========================================================
       PAGE
    ========================================================== */

        html,
        body {
            margin: 0;
            padding: 0;
            width: 100%;
            min-height: 100%;
        }

        body {
            background: #ffffff;
            font-family: Arial, Helvetica, sans-serif;
            color: #343a40;
        }

        .contentMainBody {
            margin: 0 !important;
            padding: 14px 16px 30px !important;
            width: 100% !important;
            max-width: 100% !important;
            min-height: 100vh;
            box-sizing: border-box;
            background: #ffffff;
        }


        /* ==========================================================
       TOP HEADER / BREADCRUMB
       Matches reference screenshot
    ========================================================== */

        .standalone-breadcrumbs {
            position: relative;
            width: 100%;
            min-height: 62px;
            padding: 8px 18px 8px 22px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            box-sizing: border-box;
            background: #f8fbfe;
            border: 1px solid #dde7f0;
            border-radius: 16px;
            margin-bottom: 13px;
            box-shadow: 0 2px 5px rgba(31, 55, 78, 0.08), 0 5px 12px rgba(31, 55, 78, 0.04);
        }


            /* Left vertical navy line */

            .standalone-breadcrumbs::before {
                content: "";
                position: absolute;
                left: 9px;
                top: 13px;
                bottom: 13px;
                width: 4px;
                background: #154872;
                border-radius: 4px;
            }


            .standalone-breadcrumbs .leftFung {
                display: flex;
                align-items: center;
                min-width: 0;
            }


        /* Home icon box */

        .home-link {
            width: 44px;
            height: 44px;
            min-width: 44px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #eef4fa;
            border: 1px solid #d6e3ef;
            border-radius: 12px;
            color: #154872 !important;
            font-size: 17px;
            text-decoration: none !important;
            transition: all 0.2s ease;
        }

            .home-link:hover {
                background: #e6eff8;
                border-color: #cbdbea;
                color: #10385a !important;
            }


        .diveider {
            margin: 0 10px;
            color: #b5c3d0;
            font-size: 17px;
            font-weight: 400;
        }


        .pageTitleWrap {
            display: flex;
            flex-direction: column;
            justify-content: center;
        }


        .pageTitle {
            margin: 0 0 2px 0;
            color: #153d60;
            font-size: 15px;
            line-height: 19px;
            font-weight: 700;
        }


        .pageSubTitle {
            margin: 0;
            color: #61758f;
            font-size: 11px;
            line-height: 15px;
            font-weight: 400;
        }


        /* Vendor label on right side */

        .rm-vendor-label {
            color: #154872;
            font-size: 12px;
            font-weight: 600;
        }



        /* ==========================================================
       CARD
    ========================================================== */

        .contentMainBody .card {
            width: 100%;
            margin-bottom: 13px;
            background: #ffffff;
            border: 1px solid #dcdcdc;
            border-radius: 16px;
            overflow: hidden;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.14), 0 1px 2px rgba(0, 0, 0, 0.08);
        }


        .contentMainBody .card-body {
            padding: 13px 16px;
        }



        /* ==========================================================
       PANEL HEADER
    ========================================================== */

        .mst-panel-header {
            padding: 16px 16px 8px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            background: #ffffff;
            border-bottom: 0;
        }


        .mst-panel-header-left {
            display: flex;
            align-items: center;
        }


        .mst-panel-icon {
            width: 40px;
            height: 40px;
            min-width: 40px;
            margin-right: 12px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #eaf1ff;
            border-radius: 11px;
            color: #154872;
            font-size: 17px;
        }


        .mst-panel-title {
            margin: 0 0 2px 0;
            color: #414141;
            font-size: 14px;
            font-weight: 600;
        }


        .mst-panel-subtitle {
            margin: 0;
            color: #61758f;
            font-size: 11px;
            font-weight: 400;
        }



        /* ==========================================================
       FILTER FORM
    ========================================================== */

        .filter-row {
            align-items: flex-end;
        }


        .form-group {
            margin-bottom: 0;
        }


        .form-control-label {
            display: block;
            margin-bottom: 3px;
            color: #3e3e3e;
            font-size: 11px;
            font-weight: 500;
        }


        .form-control {
            height: 34px !important;
            padding: 5px 10px;
            background: #ffffff;
            border: 1px solid #aeb4ba;
            border-radius: 13px;
            color: #3d3d3d;
            font-size: 12px;
            box-shadow: none !important;
        }


            .form-control:focus {
                border-color: #7899b8;
                box-shadow: 0 0 0 2px rgba(21, 72, 114, 0.07) !important;
            }



        /* ==========================================================
       SELECT2
    ========================================================== */

        .select2-container {
            width: 100% !important;
        }


        .select2-container--default
        .select2-selection--single {
            height: 34px !important;
            background: #ffffff !important;
            border: 1px solid #aeb4ba !important;
            border-radius: 13px !important;
            outline: none;
        }


            .select2-container--default
            .select2-selection--single
            .select2-selection__rendered {
                height: 32px;
                line-height: 32px !important;
                padding-left: 10px !important;
                padding-right: 30px !important;
                color: #3d3d3d;
                font-size: 12px;
            }


            .select2-container--default
            .select2-selection--single
            .select2-selection__arrow {
                height: 32px !important;
                top: 0 !important;
                right: 5px !important;
            }


        .select2-container--default.select2-container--focus
        .select2-selection--single,
        .select2-container--default.select2-container--open
        .select2-selection--single {
            border-color: #7899b8 !important;
        }



        /* ==========================================================
       SEARCH BUTTON
    ========================================================== */

        .form-btn-mt {
            display: flex;
            align-items: flex-end;
        }


        .btn-search {
            height: 32px;
            min-width: 82px;
            padding: 0 15px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #154872 !important;
            border: 1px solid #154872 !important;
            border-radius: 18px !important;
            color: #ffffff !important;
            font-size: 12px;
            font-weight: 500;
            box-shadow: none !important;
            text-decoration: none !important;
        }


            .btn-search:hover,
            .btn-search:focus {
                background: #10385a !important;
                border-color: #10385a !important;
                color: #ffffff !important;
                text-decoration: none !important;
            }


        .search-icon {
            margin-right: 6px;
        }



        /* ==========================================================
       GRID TABLE
    ========================================================== */

        .table-responsive {
            width: 100%;
            overflow-x: auto;
        }


        .upgradDataGrid {
            width: 100% !important;
            margin-bottom: 12px !important;
            border-collapse: collapse !important;
            border: 1px solid #d9d9d9 !important;
            font-size: 12px;
            color: #333333;
            background: #ffffff;
        }


            .upgradDataGrid th {
                padding: 7px 9px !important;
                background: #eff2f5 !important;
                color: #4e4e56 !important;
                border: 1px solid #d9d9d9 !important;
                font-size: 11px !important;
                font-weight: 600 !important;
                text-transform: none;
                vertical-align: middle !important;
                white-space: nowrap;
            }


            .upgradDataGrid td {
                padding: 4px 9px !important;
                background: #ffffff;
                color: #222222;
                border: 1px solid #dddddd !important;
                font-size: 11px;
                line-height: 1.2;
                vertical-align: middle !important;
            }


            .upgradDataGrid tr:hover td {
                background: #f9fbfd !important;
            }


        .request-id-text {
            color: #222222;
            font-weight: 400;
        }



        /* ==========================================================
       ACTION EYE ICON
    ========================================================== */

        .grid-action {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: transparent !important;
            border: 0 !important;
            color: #087cf0 !important;
            font-size: 13px !important;
            padding: 2px 5px;
            width: auto;
            height: auto;
            text-decoration: none !important;
        }


            .grid-action:hover {
                color: #005fca !important;
                background: transparent !important;
                text-decoration: none !important;
            }



        /* ==========================================================
       COMPLETED CHECK
    ========================================================== */

        .status-complete {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: transparent;
            color: #28a745 !important;
            border: 0;
            font-size: 13px;
        }



        /* ==========================================================
       PAGER
    ========================================================== */

        .PagerGrid {
            background: #ffffff !important;
        }


            .PagerGrid table {
                margin: 6px 5px 3px auto;
            }


            .PagerGrid td {
                border: 0 !important;
                padding: 2px !important;
            }


            .PagerGrid a,
            .PagerGrid span {
                min-width: 26px;
                height: 26px;
                padding: 3px 8px;
                display: inline-flex;
                align-items: center;
                justify-content: center;
                border: 1px solid #d8dee5;
                border-radius: 5px;
                background: #ffffff;
                color: #154872;
                font-size: 11px;
                text-decoration: none;
            }


            .PagerGrid span {
                background: #154872;
                border-color: #154872;
                color: #ffffff;
            }


            .PagerGrid a:hover {
                background: #eef4fa;
                color: #10385a;
            }



        /* ==========================================================
       EMPTY DATA
    ========================================================== */

        .upgradDataGrid td[colspan] {
            padding: 20px !important;
            text-align: center;
            color: #7a8793;
        }



        /* ==========================================================
       RESPONSIVE
    ========================================================== */

        @media (max-width: 767px) {

            .contentMainBody {
                padding: 10px !important;
            }


            .standalone-breadcrumbs {
                min-height: 60px;
                padding: 8px 12px 8px 20px;
                border-radius: 12px;
            }


            .home-link {
                width: 38px;
                height: 38px;
                min-width: 38px;
                border-radius: 10px;
            }


            .pageTitle {
                font-size: 14px;
            }


            .pageSubTitle {
                font-size: 10px;
            }


            .contentMainBody .card {
                border-radius: 12px;
            }


            .contentMainBody .card-body {
                padding: 12px;
            }


            .filter-row > div {
                margin-bottom: 10px;
            }


            .form-btn-mt {
                margin-bottom: 0 !important;
            }


            .btn-search {
                width: 100%;
            }


            .mst-panel-header {
                padding: 12px 12px 7px;
            }


            .mst-panel-icon {
                width: 36px;
                height: 36px;
                min-width: 36px;
            }
        }

        /* ==========================================================
   STANDALONE PAGE - PROPORTIONATELY LARGER TEXT
   Only font sizes are overridden
========================================================== */

        /* Page Header */
        .pageTitle {
            font-size: 18px !important;
        }

        .pageSubTitle {
            font-size: 13px !important;
        }

        .rm-vendor-label {
            font-size: 14px !important;
        }


        /* Panel / Card Header */
        .mst-panel-title {
            font-size: 16px !important;
        }

        .mst-panel-subtitle {
            font-size: 13px !important;
        }


        /* Form Labels */
        .form-control-label {
            font-size: 13px !important;
        }


        /* Normal Inputs / Dropdowns */
        .form-control {
            font-size: 14px !important;
        }


        /* Select2 */
        .select2-container--default
        .select2-selection--single
        .select2-selection__rendered {
            font-size: 14px !important;
        }

        .select2-results__option {
            font-size: 14px !important;
        }


        /* Search Button */
        .btn-search {
            font-size: 14px !important;
        }


        /* Grid */
        .upgradDataGrid {
            font-size: 14px !important;
        }

            .upgradDataGrid th {
                font-size: 13px !important;
            }

            .upgradDataGrid td {
                font-size: 13px !important;
            }


        /* Request ID */
        .request-id-text {
            font-size: 13px !important;
        }


        /* Action Icons / Status */
        .grid-action {
            font-size: 15px !important;
        }

        .status-complete {
            font-size: 15px !important;
        }


        /* Pager */
        .PagerGrid a,
        .PagerGrid span {
            font-size: 13px !important;
        }


        /* Mobile */
        @media (max-width: 767px) {

            .pageTitle {
                font-size: 17px !important;
            }

            .pageSubTitle {
                font-size: 12px !important;
            }

            .mst-panel-title {
                font-size: 15px !important;
            }

            .mst-panel-subtitle {
                font-size: 12px !important;
            }

            .form-control-label {
                font-size: 13px !important;
            }

            .form-control {
                font-size: 14px !important;
            }

            .select2-container--default
            .select2-selection--single
            .select2-selection__rendered {
                font-size: 14px !important;
            }

            .btn-search {
                font-size: 14px !important;
            }

            .upgradDataGrid th {
                font-size: 12px !important;
            }

            .upgradDataGrid td {
                font-size: 13px !important;
            }
        }

        .rightFung {
            display: flex;
            align-items: center;
            gap: 5px;
        }

        .welcome-text {
            color: #154872;
            font-size: 16px;
            font-weight: 700;
        }

        .rm-vendor-label {
            color: #154872;
            font-size: 16px !important;
            font-weight: 700 !important;
        }
    </style>

</head>

<body>

    <form id="form1" runat="server">

        <div class="contentMainBody">


            <!-- ======================================================
                 PAGE HEADER
            ======================================================= -->

            <div class="standalone-breadcrumbs">

                <div class="leftFung">

                    <div class="pageTitleWrap">

                        <h3 class="pageTitle">Dispatch Request List
                        </h3>

                        <p class="pageSubTitle">
                            Review and process raw material dispatch requests
                        </p>

                    </div>

                </div>


                <div class="rightFung">
                    <span class="welcome-text">Welcome: </span>
                    <asp:Label ID="lblRmVendor"
                        runat="server"
                        CssClass="rm-vendor-label">
                    </asp:Label>

                </div>

            </div>



            <!-- ======================================================
                 SEARCH / FILTER CARD
            ======================================================= -->

            <div class="card">

                <div class="mst-panel-header">

                    <div class="mst-panel-header-left">

                        <span class="mst-panel-icon">
                            <i class="fas fa-filter"></i>
                        </span>

                        <div>
                            <h5 class="mst-panel-title">Search Dispatch Requests
                            </h5>

                            <p class="mst-panel-subtitle">
                                Filter requests by vendor and dispatch status
                            </p>
                        </div>

                    </div>

                </div>


                <div class="card-body">

                    <div class="row filter-row">


                        <div class="col-md-3" hidden="hidden">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Depot:
                                </label>

                                <asp:DropDownList
                                    ID="ddlDepot"
                                    CssClass="form-control select2"
                                    runat="server">
                                </asp:DropDownList>

                            </div>

                        </div>

                        <div id="divVendor"
                            runat="server"
                            class="col-md-4">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Vendor:
                                </label>

                                <asp:DropDownList
                                    ID="ddlVendor"
                                    CssClass="form-control select2"
                                    runat="server">
                                </asp:DropDownList>

                            </div>

                        </div>


                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Status:
                                </label>

                                <asp:DropDownList
                                    ID="ddlStatus"
                                    CssClass="form-control select2"
                                    runat="server">
                                </asp:DropDownList>

                            </div>

                        </div>



                        <div class="col-md-2 form-btn-mt">

                            <div class="form-group">

                                <asp:LinkButton
                                    ID="btnSearch"
                                    runat="server"
                                    CssClass="btn btn-search"
                                    OnClick="btnSearch_Click">

                                    <i class="fas fa-search search-icon"></i>
                                    Search

                                </asp:LinkButton>

                            </div>

                        </div>


                    </div>

                </div>

            </div>



            <!-- ======================================================
                 DISPATCH LIST CARD
            ======================================================= -->

            <div class="card">

                <div class="mst-panel-header">

                    <div class="mst-panel-header-left">

                        <span class="mst-panel-icon">
                            <i class="fas fa-list"></i>
                        </span>

                        <div>

                            <h5 class="mst-panel-title">Dispatch Request List
                            </h5>

                            <p class="mst-panel-subtitle">
                                Dispatch requests matching the selected search criteria
                            </p>

                        </div>

                    </div>

                </div>



                <div class="card-body">

                    <div class="table-responsive">

                        <asp:GridView
                            ID="gvDispatchList"
                            runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="10"
                            Visible="true"
                            BorderWidth="1"
                            CssClass="table table-hover upgradDataGrid"
                            EmptyDataText="No dispatch requests found.">

                            <RowStyle CssClass="tlrowlight" />

                            <PagerStyle
                                CssClass="PagerGrid"
                                HorizontalAlign="Right" />

                            <HeaderStyle CssClass="headerGrid" />

                            <FooterStyle CssClass="footerGrid" />


                            <Columns>

                                <asp:TemplateField
                                    HeaderText="Srl No."
                                    HeaderStyle-HorizontalAlign="Center">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblSrl"
                                            runat="server"
                                            Text='<%# Container.DataItemIndex + 1 %>'>
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="7%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="7%" />

                                </asp:TemplateField>





                                <asp:TemplateField
                                    HeaderText="Request ID"
                                    HeaderStyle-HorizontalAlign="Center">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblReqId"
                                            runat="server"
                                            CssClass="request-id-text"
                                            Text='<%# Bind("orh_Id") %>'>
                                        </asp:Label>

                                        <asp:HiddenField
                                            runat="server"
                                            ID="hdnReqId"
                                            Value='<%# Bind("orh_Id") %>' />

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="15%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="15%" />

                                </asp:TemplateField>





                                <asp:TemplateField
                                    HeaderText="Vendor"
                                    HeaderStyle-HorizontalAlign="Center">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblVendor"
                                            runat="server"
                                            Text='<%# Bind("unit_name") %>'>
                                        </asp:Label>

                                        <asp:HiddenField
                                            runat="server"
                                            ID="hdnVendorCode"
                                            Value='<%# Bind("orh_vendor_code") %>' />

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="45%" />

                                    <ItemStyle
                                        HorizontalAlign="Left"
                                        Width="45%" />

                                </asp:TemplateField>





                                <asp:TemplateField
                                    HeaderText="Request Date"
                                    HeaderStyle-HorizontalAlign="Center">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblReqDate"
                                            runat="server"
                                            Text='<%# Bind("created_date") %>'>
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="20%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="20%" />

                                </asp:TemplateField>





                                <asp:TemplateField
                                    HeaderText="Action"
                                    HeaderStyle-HorizontalAlign="Center">

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

                                                <i class="fas fa-eye"></i>

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


                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="10%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="10%" />

                                </asp:TemplateField>


                            </Columns>


                        </asp:GridView>

                    </div>

                </div>

            </div>


        </div>

    </form>



    <!-- ==========================================================
         SELECT2 INITIALIZATION
    =========================================================== -->

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
