<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dispatch_Details.aspx.vb" Inherits="Dispatch_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!doctype html>
<html lang="en">
<head runat="server" id="head">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Request Details</title>

    <!-- CSS -->
    <link href="includes/style.css?v=<%= DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />

    <link href="includes/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <link href="includes/upgrad-style.css?v=<%= DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />

    <style type="text/css">
        /* ==========================================================
           STANDALONE PAGE - SAME UI FAMILY AS DISPATCH LIST
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
           PAGE HEADER
        ========================================================== */

        .standalone-breadcrumbs {
            position: relative;
            width: 100%;
            min-height: 68px;
            padding: 9px 20px 9px 24px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            box-sizing: border-box;
            background: #f8fbfe;
            border: 1px solid #dde7f0;
            border-radius: 16px;
            margin-bottom: 14px;
            box-shadow: 0 2px 5px rgba(31, 55, 78, 0.08), 0 5px 12px rgba(31, 55, 78, 0.04);
        }

            .standalone-breadcrumbs::before {
                content: "";
                position: absolute;
                left: 9px;
                top: 14px;
                bottom: 14px;
                width: 4px;
                background: #154872;
                border-radius: 4px;
            }

            .standalone-breadcrumbs .leftFung {
                display: flex;
                align-items: center;
                min-width: 0;
            }

        .home-link {
            width: 46px;
            height: 46px;
            min-width: 46px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #eef4fa;
            border: 1px solid #d6e3ef;
            border-radius: 12px;
            color: #154872 !important;
            font-size: 18px;
            text-decoration: none !important;
            transition: all 0.2s ease;
        }

            .home-link:hover {
                background: #e6eff8;
                border-color: #cbdbea;
                color: #10385a !important;
            }

        .diveider {
            margin: 0 11px;
            color: #b5c3d0;
            font-size: 18px;
            font-weight: 400;
        }

        .pageTitleWrap {
            display: flex;
            flex-direction: column;
            justify-content: center;
        }

        .pageTitle {
            margin: 0 0 3px 0;
            color: #153d60;
            font-size: 18px !important;
            line-height: 22px;
            font-weight: 700;
        }

        .pageSubTitle {
            margin: 0;
            color: #61758f;
            font-size: 13px !important;
            line-height: 17px;
            font-weight: 400;
        }

        .rightFung {
            display: flex;
            align-items: center;
            gap: 5px;
            white-space: nowrap;
        }

        .welcome-text,
        .rm-vendor-label {
            color: #154872;
            font-size: 16px !important;
            font-weight: 700 !important;
        }

        /* ==========================================================
           CARDS
        ========================================================== */

        .contentMainBody .card,
        .contentMainBody .details-card {
            width: 100%;
            margin-bottom: 14px;
            background: #ffffff;
            border: 1px solid #dcdcdc;
            border-radius: 16px;
            /*overflow: hidden;*/
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.07);
        }

        .contentMainBody .card-body {
            padding: 16px 18px 18px;
        }

        /* ==========================================================
           SECTION HEADERS
        ========================================================== */

        .mst-panel-header {
            padding: 15px 16px 9px;
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
            width: 42px;
            height: 42px;
            min-width: 42px;
            margin-right: 12px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #eaf1ff;
            border-radius: 11px;
            color: #154872;
            font-size: 18px;
        }

        .mst-panel-title {
            margin: 0 0 2px 0;
            color: #414141;
            font-size: 16px !important;
            font-weight: 700;
        }

        .mst-panel-subtitle {
            margin: 0;
            color: #61758f;
            font-size: 13px !important;
            font-weight: 400;
        }

        /* Existing Bootstrap card-header fallback */
        .details-card .card-header {
            padding: 14px 18px;
            background: #ffffff;
            border-bottom: 1px solid #eef1f4;
        }

            .details-card .card-header h5 {
                margin: 0;
                color: #414141;
                font-size: 16px;
                font-weight: 700;
            }

        /* ==========================================================
           FORM
        ========================================================== */

        .form-group {
            margin-bottom: 0;
        }

        .row .form-group {
            margin-bottom: 4px;
        }

        .form-control-label {
            display: block;
            margin-bottom: 5px;
            color: #3e3e3e;
            font-size: 13px !important;
            font-weight: 600;
        }

        .form-control {
            min-height: 38px;
            padding: 7px 11px;
            background: #ffffff;
            border: 1px solid #aeb4ba;
            border-radius: 12px;
            color: #333333;
            font-size: 14px !important;
            box-shadow: none !important;
        }

            .form-control:focus {
                border-color: #7899b8;
                box-shadow: 0 0 0 2px rgba(21, 72, 114, 0.07) !important;
            }

        .detail-value {
            min-height: 40px;
            display: flex;
            align-items: center;
            background: #f8fbfe;
            border-color: #dbe3ea;
            color: #26394b;
            font-size: 14px !important;
            font-weight: 600;
        }

        input[type="file"].form-control {
            height: 40px;
            padding: 7px 10px;
        }

        /* ==========================================================
           BUTTONS
        ========================================================== */

        .btn {
            font-size: 14px !important;
            font-weight: 600;
        }

        .btn-primary {
            background: #154872 !important;
            border-color: #154872 !important;
            color: #ffffff !important;
            border-radius: 18px !important;
            padding: 6px 17px !important;
        }

            .btn-primary:hover,
            .btn-primary:focus {
                background: #10385a !important;
                border-color: #10385a !important;
                color: #ffffff !important;
            }

        .btn-secondary {
            background: #6c757d !important;
            border-color: #6c757d !important;
            color: #ffffff !important;
            border-radius: 18px !important;
            padding: 6px 17px !important;
        }

        .button-section {
            margin-top: 18px;
            text-align: center;
        }

            .button-section .btn {
                min-width: 92px;
                margin: 0 4px;
            }

        /* ==========================================================
           MATERIAL GRID
        ========================================================== */

        .table-responsive {
            width: 100%;
            overflow-x: auto;
        }

        .upgradDataGrid {
            width: 100% !important;
            margin-bottom: 0 !important;
            border-collapse: collapse !important;
            border: 1px solid #d9d9d9 !important;
            background: #ffffff;
            color: #333333;
            font-size: 14px !important;
        }

            .upgradDataGrid th {
                padding: 9px 9px !important;
                background: #eff2f5 !important;
                color: #4e4e56 !important;
                border: 1px solid #d9d9d9 !important;
                font-size: 13px !important;
                font-weight: 700 !important;
                vertical-align: middle !important;
                white-space: normal;
            }

            .upgradDataGrid td {
                padding: 8px 9px !important;
                background: #ffffff;
                color: #222222;
                border: 1px solid #dddddd !important;
                font-size: 13px !important;
                line-height: 1.35;
                font-weight: 500;
                vertical-align: middle !important;
            }

            .upgradDataGrid tr:hover td {
                background: #f9fbfd !important;
            }

        .materialGrid {
            width: 100%;
            table-layout: fixed;
        }

            .materialGrid th,
            .materialGrid td {
                vertical-align: middle !important;
                text-align: center;
                word-wrap: break-word;
            }

        .qty-value {
            color: #26394b;
            font-weight: 600;
        }

        .pending-qty {
            color: #b46b00;
            font-weight: 700;
        }

        .qtyDispatchBox {
            width: 105px !important;
            height: 38px;
            min-height: 38px;
            margin: 0 auto;
            text-align: center;
            padding: 5px 7px;
            font-weight: 600;
        }

        /* Chrome, Edge, Safari */
        .no-spinner::-webkit-outer-spin-button,
        .no-spinner::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        /* Firefox */
        .no-spinner {
            -moz-appearance: textfield;
        }

        /* ==========================================================
           MESSAGE
        ========================================================== */

        .message-box {
            margin-bottom: 14px;
        }

            .message-box .alert {
                margin-bottom: 0;
                border-radius: 12px;
                font-size: 14px;
            }

        #divMessage {
            font-size: 13px;
            font-weight: 600;
        }

        /* ==========================================================
           POPUPS
        ========================================================== */

        .modalBackground {
            background-color: #000;
            opacity: 0.6;
            filter: alpha(opacity=60);
        }

        .success-popup,
        .validation-popup {
            background-color: #fff;
            padding: 0;
            border-radius: 14px;
            overflow: hidden;
            box-shadow: 0 8px 28px rgba(0,0,0,0.28);
        }

        .success-popup {
            width: 360px;
            max-width: 90%;
        }

        .validation-popup {
            width: 450px;
            max-width: 90%;
        }

        .success-popup .success-popup-header {
            background-color: #28a745;
            color: #fff;
            padding: 13px 18px;
        }

            .success-popup .success-popup-header h5,
            .validation-popup-header h5 {
                margin: 0;
                font-size: 16px;
                font-weight: 700;
            }

        .success-popup .success-popup-body {
            padding: 22px 18px;
            text-align: center;
            font-size: 14px;
        }

        .success-popup .success-popup-footer,
        .validation-popup-footer {
            padding: 12px 18px;
            text-align: center;
            border-top: 1px solid #eee;
        }

        .validation-popup-header {
            background-color: #dc3545;
            color: #fff;
            padding: 13px 18px;
        }

        .validation-popup-body {
            padding: 20px 25px;
            text-align: left;
            max-height: 350px;
            overflow-y: auto;
            font-size: 14px;
        }

            .validation-popup-body ul {
                margin: 0;
                padding-left: 20px;
            }

            .validation-popup-body li {
                margin-bottom: 7px;
                color: #dc3545;
                font-weight: 500;
            }

        /* ==========================================================
           RESPONSIVE
        ========================================================== */

        @media (max-width: 991px) {
            .rightFung {
                max-width: 42%;
                white-space: normal;
                text-align: right;
            }
        }

        @media (max-width: 767px) {
            .contentMainBody {
                padding: 10px !important;
            }

            .standalone-breadcrumbs {
                min-height: 64px;
                padding: 8px 12px 8px 20px;
                border-radius: 12px;
            }

            .home-link {
                width: 40px;
                height: 40px;
                min-width: 40px;
                border-radius: 10px;
                font-size: 16px;
            }

            .pageTitle {
                font-size: 17px !important;
            }

            .pageSubTitle {
                font-size: 12px !important;
            }

            .rightFung {
                display: none;
            }

            .contentMainBody .card,
            .contentMainBody .details-card {
                border-radius: 12px;
            }

            .contentMainBody .card-body {
                padding: 13px;
            }

            .mst-panel-header {
                padding: 12px 12px 8px;
            }

            .mst-panel-icon {
                width: 38px;
                height: 38px;
                min-width: 38px;
                font-size: 16px;
            }

            .mst-panel-title {
                font-size: 15px !important;
            }

            .mst-panel-subtitle {
                font-size: 12px !important;
            }

            .row > [class*="col-md-"] {
                margin-bottom: 12px;
            }

            .button-section .btn {
                margin-bottom: 6px;
            }
        }
    </style>

</head>

<body>

    <form id="form1" runat="server" autocomplete="off">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div class="contentMainBody">

            <div class="standalone-breadcrumbs">
                <div class="leftFung">
                    <a href="Dispatch_List.aspx" class="home-link" title="Dispatch List">
                        <i class="fas fa-home"></i>
                    </a>

                    <div class="diveider">/</div>

                    <div class="pageTitleWrap">
                        <h3 class="pageTitle">Request Details</h3>
                        <p class="pageSubTitle">Review request information and complete dispatch details</p>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="message-box">
                <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block"></asp:Label>
            </asp:Panel>
            <asp:Button ID="btnPopupTarget" runat="server" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="mpeSuccess" runat="server" TargetControlID="btnPopupTarget" PopupControlID="pnlSuccessPopup" BackgroundCssClass="modalBackground" OkControlID="btnPopupOk"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel
                ID="pnlSuccessPopup"
                runat="server"
                CssClass="success-popup"
                Style="display: none;">

                <div class="success-popup-header">
                    <h5>Success</h5>
                </div>

                <div class="success-popup-body">
                    <p>Dispatch submitted successfully.</p>
                </div>

                <div class="success-popup-footer">
                    <asp:Button
                        ID="btnPopupOk"
                        runat="server"
                        Text="OK"
                        CssClass="btn btn-primary btn-sm"
                        OnClientClick="window.location.href='Dispatch_List.aspx'; return false;" />
                </div>

            </asp:Panel>

            <asp:Button
                ID="btnValidationPopupTarget"
                runat="server"
                Style="display: none;" />

            <ajaxToolkit:ModalPopupExtender
                ID="mpeValidation"
                runat="server"
                BehaviorID="mpeValidationBehavior"
                TargetControlID="btnValidationPopupTarget"
                PopupControlID="pnlValidationPopup"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel
                ID="pnlValidationPopup"
                runat="server"
                CssClass="validation-popup"
                Style="display: none;">

                <div class="validation-popup-header">
                    <h5>Validation</h5>
                </div>

                <div class="validation-popup-body">

                    <asp:Label
                        ID="lblValidationMessage"
                        runat="server">
                    </asp:Label>

                </div>

                <div class="validation-popup-footer">

                    <asp:Button
                        ID="btnValidationOk"
                        runat="server"
                        Text="OK"
                        CssClass="btn btn-primary btn-sm"
                        CausesValidation="false"
                        OnClientClick="$find('mpeValidationBehavior').hide(); return false;" />

                </div>

            </asp:Panel>

            <div class="card details-card">

                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon">
                            <i class="fas fa-file-alt"></i>
                        </span>
                        <div>
                            <h5 class="mst-panel-title">Request Information</h5>
                            <p class="mst-panel-subtitle">Basic request and vendor information</p>
                        </div>
                    </div>
                </div>


                <div class="card-body">

                    <div class="row">

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Request ID:
                               
                                </label>

                                <asp:Label
                                    ID="lblRequestID"
                                    runat="server"
                                    CssClass="form-control detail-value">
                                </asp:Label>
                                <asp:HiddenField ID="hdnRawMaterialVendorCode" runat="server" />

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Request Date:
                               
                                </label>

                                <asp:Label
                                    ID="lblRequestDate"
                                    runat="server"
                                    CssClass="form-control detail-value">
                                </asp:Label>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Vendor Code:
                               
                                </label>

                                <asp:Label
                                    ID="lblVendorCode"
                                    runat="server"
                                    CssClass="form-control detail-value">
                                </asp:Label>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Vendor Name:
                               
                                </label>

                                <asp:Label
                                    ID="lblVendorName"
                                    runat="server"
                                    CssClass="form-control detail-value">
                                </asp:Label>

                            </div>

                        </div>


                    </div>

                </div>

            </div>

            <div class="card details-card">
                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon">
                            <i class="fas fa-shipping-fast"></i>
                        </span>
                        <div>
                            <h5 class="mst-panel-title">Dispatch Information</h5>
                            <p class="mst-panel-subtitle">Select how the material will be dispatched</p>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    Delivery Type:
                               
                                </label>
                                <asp:DropDownList
                                    ID="ddlDelType"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control select2"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDelType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel
                ID="pnlCourierCard"
                runat="server"
                CssClass="card details-card"
                Visible="false">

                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon">
                            <i class="fas fa-truck"></i>
                        </span>
                        <div>
                            <h5 class="mst-panel-title">
                                <asp:Label ID="lblCourierCardHeader" runat="server" Text="Courier Information"></asp:Label>
                            </h5>
                            <p class="mst-panel-subtitle">Enter courier or transport dispatch information</p>
                        </div>
                    </div>
                </div>

                <div class="card-body">

                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    <asp:Label ID="lblCourierNoLabel" runat="server" Text="POD No:"></asp:Label>
                                </label>
                                <asp:TextBox
                                    ID="txtCouNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control"
                                    placeholder="Enter POD No.">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    <asp:Label ID="lblTranNameLabel" runat="server" Text="Courier Name:"></asp:Label>
                                </label>
                                <asp:TextBox
                                    ID="txtTranName"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control"
                                    placeholder="Enter Courier Name.">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div id="divLrNo" class="col-md-3" runat="server">
                            <div class="form-group">
                                <label class="form-control-label">
                                    LR/Consignment No:
                               
                                </label>
                                <asp:TextBox
                                    ID="txtLRNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control"
                                    placeholder="Enter LR/Consignment No.">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    <asp:Label ID="lblLrDate" runat="server" Text="Courier Date:"></asp:Label>
                                </label>
                                <asp:TextBox
                                    ID="txtLRDate"
                                    runat="server"
                                    CssClass="form-control"
                                    autocomplete="off"
                                    placeholder="Select Courier Date."
                                    onkeydown="return handleDateKeyDown(event, this);"
                                    onpaste="return true;"
                                    ondrop="return true;">
                                </asp:TextBox>
                                <ajaxToolkit:CalendarExtender
                                    ID="calLRDate"
                                    runat="server"
                                    TargetControlID="txtLRDate"
                                    Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>

                        <div id="divVehNo" class="col-md-3" runat="server">
                            <div class="form-group">
                                <label class="form-control-label">
                                    Vehicle No:
                               
                                </label>
                                <asp:TextBox
                                    ID="txtVehNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control"
                                    placeholder="Enter Vehicle No.">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    LR Doc:
                               
                                </label>
                                <asp:FileUpload
                                    ID="fuLrDoc"
                                    runat="server"
                                    ClientIDMode="Static"
                                    accept=".pdf,application/pdf"
                                    CssClass="form-control" />
                            </div>
                        </div>

                    </div>

                </div>

            </asp:Panel>

            <div class="card details-card">

                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon">
                            <i class="fas fa-file-invoice"></i>
                        </span>
                        <div>
                            <h5 class="mst-panel-title">Invoice Information</h5>
                            <p class="mst-panel-subtitle">Upload the invoice and verify extracted invoice details</p>
                        </div>
                    </div>
                </div>


                <div class="card-body">

                    <div class="row">

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Invoice Doc:
                               
                                </label>

                                <asp:FileUpload
                                    ID="fuInv"
                                    runat="server"
                                    ClientIDMode="Static"
                                    accept=".pdf,application/pdf"
                                    CssClass="form-control" />

                                <button type="button" id="btnUploadInvoice" class="btn btn-primary btn-sm mt-2" runat="server">
                                    Upload &amp; Extract
       
                               
                                </button>
                                <div id="divMessage" class="mt-2"></div>
                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Invoice No:
                               
                                </label>

                                <asp:TextBox
                                    ID="txtInvNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control"
                                    placeholder="Enter Invoice No.">
                                </asp:TextBox>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Invoice Date:
                               
                                </label>

                                <%--<asp:TextBox
                                    ID="txtInvDate"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>--%>
                                <asp:TextBox
                                    ID="txtInvDate"
                                    runat="server"
                                    CssClass="form-control"
                                    autocomplete="off"
                                    placeholder="Select Invoice Date."
                                    onkeydown="return handleDateKeyDown(event, this);"
                                    onpaste="return false;"
                                    ondrop="return false;">
                                </asp:TextBox>
                                <ajaxToolkit:CalendarExtender
                                    ID="calInvDate"
                                    runat="server"
                                    TargetControlID="txtInvDate"
                                    Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

            <div class="card details-card">

                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon">
                            <i class="fas fa-list"></i>
                        </span>
                        <div>
                            <h5 class="mst-panel-title">Material Request Details</h5>
                            <p class="mst-panel-subtitle">Review requested quantities and enter quantity to dispatch</p>
                        </div>
                    </div>
                </div>


                <div class="card-body">


                    <div class="table-responsive">

                        <asp:GridView
                            ID="gvMaterials"
                            runat="server"
                            AutoGenerateColumns="false"
                            BorderWidth="1"
                            CssClass="table table-hover upgradDataGrid materialGrid"
                            EmptyDataText="No request details found.">

                            <RowStyle CssClass="tlrowlight" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />

                            <Columns>
                                <asp:TemplateField HeaderText="Srl No.">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblSrl"
                                            runat="server"
                                            Text='<%# Container.DataItemIndex + 1 %>'>
                                        </asp:Label>

                                        <asp:HiddenField
                                            ID="hdnOrdID"
                                            runat="server"
                                            Value='<%# Eval("ord_id") %>' />

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="6%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="6%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Raw Material Code">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblRmCode"
                                            runat="server"
                                            Text='<%# Eval("ord_rawmaterial_code") %>'>
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="18%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="18%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Required Delivery Date">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblDeliveryDate"
                                            runat="server"
                                            Text='<%# Eval("ord_req_delivery_date", "{0:dd-MM-yyyy}") %>'>
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="16%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="16%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requested Quantity">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblRequestedQty"
                                            runat="server"
                                            CssClass="qty-value"
                                            Text='<%# Eval("ord_qty") %>'>
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="14%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="14%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Dispatched Quantity">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblDispatchQty"
                                            runat="server"
                                            CssClass="qty-value"
                                            Text='<%# Eval("dispatch_qty") %>'>
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="15%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="15%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pending Quantity">

                                    <ItemTemplate>

                                        <asp:Label
                                            ID="lblPendingQty"
                                            runat="server"
                                            CssClass="pending-qty"
                                            Text='<%# Eval("pending_qty") %>'>
                                        </asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="13%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="13%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity To Dispatch">

                                    <ItemTemplate>

                                        <asp:TextBox
                                            ID="txtQtyToDispatch"
                                            runat="server"
                                            CssClass="form-control qtyDispatchBox"
                                            Text="0"
                                            MaxLength="10"
                                            inputmode="decimal"
                                            onkeypress="return allowDecimal(this, event);"
                                            oninput="validateDecimal(this);">
                                        </asp:TextBox>

                                    </ItemTemplate>

                                    <HeaderStyle
                                        HorizontalAlign="Center"
                                        Width="18%" />

                                    <ItemStyle
                                        HorizontalAlign="Center"
                                        Width="18%" />

                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                    </div>

                    <div class="row button-section">

                        <div class="col-md-12">

                            <asp:Button
                                ID="btnSubmit"
                                runat="server"
                                Text="Submit"
                                OnClick="btnSubmit_Click"
                                OnClientClick="return validateDispatchForm();"
                                CssClass="btn btn-primary btn-sm"
                                CausesValidation="false" />

                            <asp:Button
                                ID="btnBack"
                                runat="server"
                                OnClick="btnBack_Click"
                                Text="Back"
                                CssClass="btn btn-secondary btn-sm"
                                CausesValidation="false" />

                        </div>

                    </div>


                </div>

            </div>


        </div>

    </form>

    <script type="text/javascript">

        function allowDecimal(control, evt) {

            var charCode = evt.which ? evt.which : evt.keyCode;

            // Allow Backspace, Tab, Delete and Arrow keys
            if (
                charCode === 8 ||
                charCode === 9 ||
                charCode === 37 ||
                charCode === 39 ||
                charCode === 46
            ) {
                // 46 can also represent decimal point
                if (charCode === 46) {
                    if (control.value.indexOf('.') !== -1) {
                        return false;
                    }
                }

                return true;
            }

            // Allow digits 0-9
            if (charCode >= 48 && charCode <= 57) {
                return true;
            }

            return false;
        }


        function validateDecimal(control) {

            // Remove everything except numbers and decimal point
            control.value = control.value.replace(/[^0-9.]/g, '');

            // Allow only one decimal point
            var parts = control.value.split('.');

            if (parts.length > 2) {
                control.value = parts[0] + '.' + parts.slice(1).join('');
            }

        }

    </script>
    <script type="text/javascript">

        // Bound on native DOMContentLoaded so it doesn't depend on any
        // jQuery ready() callback elsewhere on the page.
        document.addEventListener('DOMContentLoaded', function () {
            bindInvoiceUploadExtract();
        });

        function bindInvoiceUploadExtract() {
            var btn = document.getElementById('btnUploadInvoice');
            if (btn) {
                btn.onclick = triggerInvoiceOcrUpload;
            }
        }

        function triggerInvoiceOcrUpload() {

            var fileUpload = document.getElementById('fuInv');
            var msgDiv = document.getElementById('divMessage');
            var btn = document.getElementById('btnUploadInvoice');

            if (!fileUpload || !fileUpload.files || fileUpload.files.length === 0) {
                showInvoiceMessage(msgDiv, 'Please select an invoice PDF file.', 'danger');
                return;
            }

            var file = fileUpload.files[0];

            if (!/\.pdf$/i.test(file.name)) {
                showInvoiceMessage(msgDiv, 'Please upload a PDF invoice file.', 'danger');
                fileUpload.value = '';
                return;
            }

            var formData = new FormData();
            formData.append('file', file, file.name);

            var xhr = new XMLHttpRequest();
            xhr.open('POST', 'InvoiceOcrExtract.ashx', true);

            btn.disabled = true;
            showInvoiceMessage(msgDiv, 'Uploading and extracting invoice details...', 'info');

            xhr.onload = function () {

                btn.disabled = false;

                var result;
                try {
                    result = JSON.parse(xhr.responseText);
                } catch (e) {
                    showInvoiceMessage(msgDiv, 'Invoice OCR request failed.', 'danger');
                    fileUpload.value = '';
                    return;
                }

                if (xhr.status === 200 && result && result.success) {

                    if (result.invoice_no) {
                        document.getElementById('txtInvNo').value = result.invoice_no;
                    }

                    //if (result.invoice_date) {
                    //    document.getElementById('txtInvDate').value = result.invoice_date.replace(/-/g, '/');
                    //}
                    if (result.invoice_date) {

                        console.log("Invoice Date from API:", result.invoice_date);

                        var invoiceDate = result.invoice_date.trim();
                        invoiceDate = invoiceDate.replace(/\//g, '-');

                        var parts = invoiceDate.split('-');

                        if (parts.length === 3) {

                            var day;
                            var month;
                            var year;

                            if (parts[0].length === 4) {
                                // yyyy-MM-dd
                                year = parts[0];
                                month = parts[1];
                                day = parts[2];
                            } else {
                                // dd-MM-yyyy
                                day = parts[0];
                                month = parts[1];
                                year = parts[2];
                            }

                            document.getElementById('txtInvDate').value =
                                day.padStart(2, '0') + '-' +
                                month.padStart(2, '0') + '-' +
                                year;
                        }
                    }

                    showInvoiceMessage(msgDiv, 'Invoice details extracted successfully.', 'success');

                } else {
                    showInvoiceMessage(msgDiv, (result && result.message) || 'Unable to extract invoice details from the uploaded PDF.', 'danger');
                    fileUpload.value = '';
                }
            };

            xhr.onerror = function () {
                btn.disabled = false;
                showInvoiceMessage(msgDiv, 'Invoice OCR request failed.', 'danger');
                fileUpload.value = '';
            };

            xhr.send(formData);
        }

        function showInvoiceMessage(msgDiv, text, type) {

            if (!msgDiv) {
                return;
            }

            var cssClass = 'text-muted';

            if (type === 'danger') {
                cssClass = 'text-danger';
            } else if (type === 'success') {
                cssClass = 'text-success';
            }

            msgDiv.innerHTML = '<span class="' + cssClass + '">' + text + '</span>';
        }

    </script>
    <%--<script type="text/javascript">

        function validateDispatchForm() {

            var errors = [];

            // ========================================
            // Delivery Type
            // ========================================
            //var deliveryType = document.getElementById('txtDelType');

            //if (!deliveryType || deliveryType.value.trim() === '') {
            //    errors.push('Delivery Type is required.');
            //}

            var deliveryType = document.getElementById('ddlDelType');

            if (!deliveryType || deliveryType.value.trim() === '') {
                errors.push('Delivery Type is required.');
            }


            // ========================================
            // Courier No
            // ========================================
            var courierNo = document.getElementById('txtCouNo');

            if (!courierNo || courierNo.value.trim() === '') {
                errors.push('Courier No is required.');
            }


            // ========================================
            // Transporter Name
            // ========================================
            var transporterName = document.getElementById('txtTranName');

            if (!transporterName || transporterName.value.trim() === '') {
                errors.push('Transporter Name is required.');
            }


            // ========================================
            // LR / Consignment No
            // ========================================
            var lrNo = document.getElementById('txtLRNo');

            if (!lrNo || lrNo.value.trim() === '') {
                errors.push('LR / Consignment No is required.');
            }


            // ========================================
            // LR Date
            // ========================================
            var lrDate = document.getElementById('<%= txtLRDate.ClientID %>');

            if (!lrDate || lrDate.value.trim() === '') {

                errors.push('LR Date is required.');

            } else if (!isValidDispatchDate(lrDate.value.trim())) {

                errors.push('Please enter a valid LR Date.');

            }


            // ========================================
            // Vehicle No
            // ========================================
            var vehicleNo = document.getElementById('txtVehNo');

            if (!vehicleNo || vehicleNo.value.trim() === '') {
                errors.push('Vehicle No is required.');
            }


            // ========================================
            // LR Document
            // REQUIRED
            // ========================================
            //var lrDoc = document.getElementById('fuLrDoc');

            //if (!lrDoc ||
            //    !lrDoc.files ||
            //    lrDoc.files.length === 0) {

            //    errors.push('LR Document is required.');

            //} else {

            //    var lrFileName = lrDoc.files[0].name;

            //    if (!/\.pdf$/i.test(lrFileName)) {
            //        errors.push('LR Document must be a PDF file.');
            //    }
            //}


            // ========================================
            // Invoice No
            // ========================================
            var invoiceNo = document.getElementById('txtInvNo');

            if (!invoiceNo || invoiceNo.value.trim() === '') {
                errors.push('Invoice No is required.');
            }


            // ========================================
            // Invoice Date
            // ========================================
            var invoiceDate = document.getElementById('txtInvDate');

            if (!invoiceDate || invoiceDate.value.trim() === '') {

                errors.push('Invoice Date is required.');

            } else if (!isValidDispatchDate(invoiceDate.value.trim())) {

                errors.push('Please enter a valid Invoice Date.');

            }


            // ========================================
            // Invoice Document
            // NOT REQUIRED
            // ========================================

            // No validation for fuInv


            // ========================================
            // Quantity Validation
            // ========================================

            var qtyBoxes = document.querySelectorAll('.qtyDispatchBox');

            var hasDispatchQty = false;

            for (var i = 0; i < qtyBoxes.length; i++) {

                var qtyBox = qtyBoxes[i];

                var qtyText = qtyBox.value.trim();

                if (qtyText === '') {
                    qtyText = '0';
                }

                var qty = parseFloat(qtyText);

                if (isNaN(qty)) {

                    errors.push(
                        'Please enter a valid dispatch quantity at row ' +
                        (i + 1) + '.'
                    );

                    continue;
                }


                if (qty < 0) {

                    errors.push(
                        'Dispatch quantity cannot be negative at row ' +
                        (i + 1) + '.'
                    );

                    continue;
                }


                if (qty > 0) {

                    hasDispatchQty = true;

                    var row = qtyBox.closest('tr');

                    if (row) {

                        var pendingLabel =
                            row.querySelector('.pending-qty');

                        if (pendingLabel) {

                            var pendingQty =
                                parseFloat(
                                    pendingLabel.innerText ||
                                    pendingLabel.textContent ||
                                    '0'
                                );

                            if (!isNaN(pendingQty) &&
                                qty > pendingQty) {

                                errors.push(
                                    'Dispatch quantity cannot exceed pending quantity at row ' +
                                    (i + 1) + '.'
                                );
                            }
                        }
                    }
                }
            }


            if (!hasDispatchQty) {

                errors.push(
                    'Please enter quantity to dispatch for at least one material.'
                );
            }


            // ========================================
            // Show Modal
            // ========================================

            if (errors.length > 0) {

                var message =
                    '<ul>';

                for (var j = 0; j < errors.length; j++) {
                    message += '<li>' + errors[j] + '</li>';
                }

                message += '</ul>';

                document.getElementById(
                '<%= lblValidationMessage.ClientID %>'
                ).innerHTML = message;

                var popup =
                    $find('mpeValidationBehavior');

                if (popup) {
                    popup.show();
                }

                return false;
            }


            return true;
        }


        function isValidDispatchDate(value) {

            // Accept:
            // dd-MM-yyyy
            // dd/MM/yyyy

            var match =
                value.match(
                    /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/
                );

            if (!match) {
                return false;
            }

            var day = parseInt(match[1], 10);
            var month = parseInt(match[2], 10);
            var year = parseInt(match[3], 10);

            var date =
                new Date(
                    year,
                    month - 1,
                    day
                );

            return (
                date.getFullYear() === year &&
                date.getMonth() === month - 1 &&
                date.getDate() === day
            );
        }

    </script>--%>

    <script type="text/javascript">

        function validateDispatchForm() {

            var errors = [];

            // ========================================
            // Delivery Type
            // ========================================
            var deliveryType = document.getElementById('ddlDelType');
            var selectedDeliveryText = '';

            if (!deliveryType || deliveryType.value.trim() === '') {

                errors.push('Delivery Type is required.');

            } else {

                selectedDeliveryText =
                    deliveryType.options[deliveryType.selectedIndex]
                        .text
                        .trim()
                        .toLowerCase();


                // ========================================
                // Courier / POD / Transport No
                // Mandatory for Courier & Transport
                // ========================================
                var courierNo = document.getElementById('txtCouNo');

                if (!courierNo || courierNo.value.trim() === '') {

                    if (selectedDeliveryText.indexOf('courier') !== -1) {

                        errors.push('POD No is required.');

                    } else if (selectedDeliveryText.indexOf('transport') !== -1) {

                        errors.push('Transport No is required.');

                    } else {

                        errors.push('Courier / Transport No is required.');
                    }
                }


                // ========================================
                // Courier Name / Transporter Name
                // Mandatory for Courier & Transport
                // ========================================
                var transporterName = document.getElementById('txtTranName');

                if (!transporterName ||
                    transporterName.value.trim() === '') {

                    if (selectedDeliveryText.indexOf('courier') !== -1) {

                        errors.push('Courier Name is required.');

                    } else if (selectedDeliveryText.indexOf('transport') !== -1) {

                        errors.push('Transporter Name is required.');

                    } else {

                        errors.push('Courier / Transporter Name is required.');
                    }
                }


                // ========================================
                // LR / Consignment No
                // Mandatory ONLY for Transport
                // ========================================
                if (selectedDeliveryText.indexOf('transport') !== -1) {

                    var lrNo = document.getElementById('txtLRNo');

                    if (!lrNo || lrNo.value.trim() === '') {

                        errors.push('LR / Consignment No is required.');
                    }
                }


                // ========================================
                // LR Date / Courier Date
                // Mandatory for Courier & Transport
                // ========================================
                var lrDate =
                    document.getElementById('<%= txtLRDate.ClientID %>');

                if (!lrDate || lrDate.value.trim() === '') {

                    if (selectedDeliveryText.indexOf('courier') !== -1) {

                        errors.push('Courier Date is required.');

                    } else if (selectedDeliveryText.indexOf('transport') !== -1) {

                        errors.push('LR Date is required.');

                    } else {

                        errors.push('Delivery Date is required.');
                    }

                } else if (!isValidDispatchDate(lrDate.value.trim())) {

                    if (selectedDeliveryText.indexOf('courier') !== -1) {

                        errors.push('Please enter a valid Courier Date.');

                    } else if (selectedDeliveryText.indexOf('transport') !== -1) {

                        errors.push('Please enter a valid LR Date.');

                    } else {

                        errors.push('Please enter a valid Delivery Date.');
                    }
                }


                // ========================================
                // Vehicle No
                // Mandatory for Courier & Transport
                // ========================================
                if (selectedDeliveryText.indexOf('transport') !== -1) {

                    var vehicleNo = document.getElementById('txtVehNo');

                    if (!vehicleNo || vehicleNo.value.trim() === '') {
                        errors.push('Vehicle No is required.');
                    }
                }

            }


            // ========================================
            // LR Document
            // NOT MANDATORY
            // ========================================
            // No mandatory validation for fuLrDoc.
            //
            // Optional:
            // If user uploads a file, validate that it is PDF.
            // ========================================

            var lrDoc = document.getElementById('fuLrDoc');

            if (lrDoc &&
                lrDoc.files &&
                lrDoc.files.length > 0) {

                var lrFileName = lrDoc.files[0].name;

                if (!/\.pdf$/i.test(lrFileName)) {

                    errors.push('LR Document must be a PDF file.');
                }
            }


            // ========================================
            // Invoice No
            // Mandatory
            // ========================================
            var invoiceNo = document.getElementById('txtInvNo');

            if (!invoiceNo ||
                invoiceNo.value.trim() === '') {

                errors.push('Invoice No is required.');
            }


            // ========================================
            // Invoice Date
            // Mandatory
            // ========================================
            var invoiceDate = document.getElementById('txtInvDate');

            if (!invoiceDate ||
                invoiceDate.value.trim() === '') {

                errors.push('Invoice Date is required.');

            } else if (!isValidDispatchDate(invoiceDate.value.trim())) {

                errors.push('Please enter a valid Invoice Date.');
            }


            // ========================================
            // Invoice Document
            // NOT REQUIRED
            // ========================================
            // No mandatory validation for fuInv.
            //
            // Optional:
            // Validate PDF only if file is selected.
            // ========================================

            var invDoc = document.getElementById('fuInv');

            if (invDoc &&
                invDoc.files &&
                invDoc.files.length > 0) {

                var invFileName = invDoc.files[0].name;

                if (!/\.pdf$/i.test(invFileName)) {

                    errors.push('Invoice Document must be a PDF file.');
                }
            }


            // ========================================
            // Quantity Validation
            // ========================================
            var qtyBoxes =
                document.querySelectorAll('.qtyDispatchBox');

            var hasDispatchQty = false;

            for (var i = 0; i < qtyBoxes.length; i++) {

                var qtyBox = qtyBoxes[i];

                var qtyText = qtyBox.value.trim();

                if (qtyText === '') {
                    qtyText = '0';
                }

                var qty = parseFloat(qtyText);


                // ========================================
                // Invalid Quantity
                // ========================================
                if (isNaN(qty)) {

                    errors.push(
                        'Please enter a valid dispatch quantity at row ' +
                        (i + 1) + '.'
                    );

                    continue;
                }


                // ========================================
                // Negative Quantity
                // ========================================
                if (qty < 0) {

                    errors.push(
                        'Dispatch quantity cannot be negative at row ' +
                        (i + 1) + '.'
                    );

                    continue;
                }


                // ========================================
                // Quantity > 0
                // ========================================
                if (qty > 0) {

                    hasDispatchQty = true;

                    var row = qtyBox.closest('tr');

                    if (row) {

                        var pendingLabel =
                            row.querySelector('.pending-qty');

                        if (pendingLabel) {

                            var pendingText =
                                pendingLabel.innerText ||
                                pendingLabel.textContent ||
                                '0';

                            var pendingQty =
                                parseFloat(pendingText);


                            // ========================================
                            // Cannot exceed pending quantity
                            // ========================================
                            if (!isNaN(pendingQty) &&
                                qty > pendingQty) {

                                errors.push(
                                    'Dispatch quantity cannot exceed pending quantity at row ' +
                                    (i + 1) + '.'
                                );
                            }
                        }
                    }
                }
            }


            // ========================================
            // At least one dispatch quantity required
            // ========================================
            if (!hasDispatchQty) {

                errors.push(
                    'Please enter quantity to dispatch for at least one material.'
                );
            }


            // ========================================
            // Show Validation Modal
            // ========================================
            if (errors.length > 0) {

                var message = '<ul>';

                for (var j = 0; j < errors.length; j++) {

                    message +=
                        '<li>' +
                        errors[j] +
                        '</li>';
                }

                message += '</ul>';


                document.getElementById(
                '<%= lblValidationMessage.ClientID %>'
                ).innerHTML = message;


                var popup =
                    $find('mpeValidationBehavior');

                if (popup) {
                    popup.show();
                }

                return false;
            }


            // ========================================
            // All validations passed
            // ========================================
            return true;
        }



        // ============================================================
        // Date Validation
        //
        // Accepted:
        // dd-MM-yyyy
        // dd/MM/yyyy
        //
        // Examples:
        // 17-08-2026
        // 17/08/2026
        // ============================================================
        function isValidDispatchDate(value) {

            if (!value) {
                return false;
            }

            var match =
                value.match(
                    /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/
                );

            if (!match) {
                return false;
            }


            var day =
                parseInt(match[1], 10);

            var month =
                parseInt(match[2], 10);

            var year =
                parseInt(match[3], 10);


            var date =
                new Date(
                    year,
                    month - 1,
                    day
                );


            return (
                date.getFullYear() === year &&
                date.getMonth() === month - 1 &&
                date.getDate() === day
            );
        }

    </script>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var vendorName = document.getElementById('<%= lblVendorName.ClientID %>');
            var welcomeVendorName = document.getElementById('welcomeVendorName');

            if (vendorName && welcomeVendorName) {
                welcomeVendorName.textContent =
                    (vendorName.innerText || vendorName.textContent || '').trim();
            }
        });
    </script>

    <script type="text/javascript">
        function handleDateKeyDown(event, textbox) {

            // Allow Tab for navigation
            if (event.key === "Tab") {
                return true;
            }

            // Backspace / Delete clears the complete date
            if (event.key === "Backspace" || event.key === "Delete") {
                textbox.value = "";
                return false;
            }

            // Block all other keyboard input
            return false;
        }
</script>

</body>
</html>
