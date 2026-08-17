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

    <style type="text/css">
        html,
        body {
            margin: 0;
            padding: 0;
        }

        .contentMainBody {
            margin: 0 !important;
            padding: 20px !important;
            width: 100% !important;
            max-width: 100% !important;
        }

        .pageTitle {
            font-weight: 600;
            margin-bottom: 0;
        }

        .details-card {
            margin-bottom: 20px;
        }

            .details-card .card-header {
                padding: 12px 18px;
            }

                .details-card .card-header h5 {
                    margin: 0;
                    font-weight: 600;
                }

        .form-control-label {
            font-weight: 600;
            margin-bottom: 5px;
        }

        .detail-value {
            min-height: 38px;
            display: flex;
            align-items: center;
            background-color: #f8f9fa;
            font-weight: 600;
            color: #333;
        }

        .upgradDataGrid {
            width: 100%;
            margin-bottom: 0;
        }

            .upgradDataGrid th,
            .upgradDataGrid td {
                padding: 9px 12px !important;
                vertical-align: middle !important;
            }

            .upgradDataGrid th {
                font-weight: 600;
                white-space: nowrap;
            }

            .upgradDataGrid td {
                line-height: 1.4;
                font-weight: 500;
            }

                .upgradDataGrid th:first-child,
                .upgradDataGrid td:first-child {
                    padding-left: 6px !important;
                    padding-right: 6px !important;
                }

        .qty-value {
            font-weight: 600;
        }

        .pending-qty {
            font-weight: 700;
        }

        .message-box {
            margin-bottom: 15px;
        }

        .button-section {
            margin-top: 20px;
            text-align: center;
        }

        .materialGrid {
            width: 100%;
            table-layout: fixed;
        }

            .materialGrid th,
            .materialGrid td {
                padding: 8px 6px !important;
                vertical-align: middle !important;
                text-align: center;
                word-wrap: break-word;
            }

            .materialGrid th {
                white-space: normal;
                font-weight: 600;
            }

        .qtyDispatchBox {
            width: 90px !important;
            height: 34px;
            margin: 0 auto;
            text-align: center;
            padding: 4px 6px;
        }

        .qty-value,
        .pending-qty {
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

        .modalBackground {
            background-color: #000;
            opacity: 0.6;
            filter: alpha(opacity=60);
        }

        .success-popup {
            background-color: #fff;
            width: 320px;
            padding: 0;
            border-radius: 6px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.3);
        }

            .success-popup .success-popup-header {
                background-color: #28a745;
                color: #fff;
                padding: 12px 18px;
                border-radius: 6px 6px 0 0;
            }

                .success-popup .success-popup-header h5 {
                    margin: 0;
                    font-weight: 600;
                }

            .success-popup .success-popup-body {
                padding: 20px 18px;
                text-align: center;
            }

            .success-popup .success-popup-footer {
                padding: 12px 18px;
                text-align: center;
                border-top: 1px solid #eee;
            }

        .validation-popup {
            background-color: #fff;
            width: 420px;
            max-width: 90%;
            padding: 0;
            border-radius: 6px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.3);
        }

        .validation-popup-header {
            background-color: #dc3545;
            color: #fff;
            padding: 12px 18px;
            border-radius: 6px 6px 0 0;
        }

            .validation-popup-header h5 {
                margin: 0;
                font-weight: 600;
            }

        .validation-popup-body {
            padding: 20px 25px;
            text-align: left;
            max-height: 350px;
            overflow-y: auto;
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

        .validation-popup-footer {
            padding: 12px 18px;
            text-align: center;
            border-top: 1px solid #eee;
        }
    </style>

</head>

<body>

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div class="contentMainBody">

            <div class="breadcrumbs">
                <div class="leftFung">
                    <h3 class="pageTitle">Request Details</h3>
                </div>
                <div class="rightFung"></div>
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

                <div class="card-header">

                    <h5>Request Information
                    </h5>

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

            <%--<div class="card details-card">

                <div class="card-header">

                    <h5>Courier Information
                    </h5>

                </div>


                <div class="card-body">

                    <div class="row">

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Delivery Type:
                                </label>
                                <asp:DropDownList ID="ddlDelType" class="form-control select2" runat="server"></asp:DropDownList>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Courier No:
                                </label>

                                <asp:TextBox
                                    ID="txtCouNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Transporter Name:
                                </label>

                                <asp:TextBox
                                    ID="txtTranName"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    LR/Consignment No::
                                </label>

                                <asp:TextBox
                                    ID="txtLRNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    LR Date:
                                </label>

                                <asp:TextBox
                                    ID="txtLRDate"
                                    runat="server"
                                    CssClass="form-control"
                                    autocomplete="off">
                                </asp:TextBox>

                                <ajaxToolkit:CalendarExtender
                                    ID="calLRDate"
                                    runat="server"
                                    TargetControlID="txtLRDate"
                                    Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>

                            </div>

                        </div>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Vehicle No:
                                </label>

                                <asp:TextBox
                                    ID="txtVehNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
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

            </div>--%>

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

            <asp:Panel
                ID="pnlCourierCard"
                runat="server"
                CssClass="card details-card"
                Visible="false">

                <div class="card-header">
                    <h5>
                        <asp:Label ID="lblCourierCardHeader" runat="server" Text="Courier Information"></asp:Label>
                    </h5>
                </div>

                <div class="card-body">

                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    <asp:Label ID="lblCourierNoLabel" runat="server" Text="Courier No:"></asp:Label>
                                </label>
                                <asp:TextBox
                                    ID="txtCouNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    <asp:Label ID="lblTranNameLabel" runat="server" Text="Transporter Name:"></asp:Label>
                                </label>
                                <asp:TextBox
                                    ID="txtTranName"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    LR/Consignment No::
                                </label>
                                <asp:TextBox
                                    ID="txtLRNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    LR Date:
                                </label>
                                <asp:TextBox
                                    ID="txtLRDate"
                                    runat="server"
                                    CssClass="form-control"
                                    autocomplete="off">
                                </asp:TextBox>
                                <ajaxToolkit:CalendarExtender
                                    ID="calLRDate"
                                    runat="server"
                                    TargetControlID="txtLRDate"
                                    Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">
                                    Vehicle No:
                                </label>
                                <asp:TextBox
                                    ID="txtVehNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
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

                <div class="card-header">

                    <h5>Invoice Information
                    </h5>

                </div>


                <div class="card-body">

                    <div class="row">

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Invoice No:
                                </label>

                                <asp:TextBox
                                    ID="txtInvNo"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>

                        <%--<div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Invoice Amount:
                                </label>

                                <asp:TextBox
                                    ID="txtInvAmt"
                                    runat="server"
                                    CssClass="form-control"
                                    autocomplete="off">
                                </asp:TextBox>

                                <ajaxToolkit:CalendarExtender
                                    ID="CalendarExtender1"
                                    runat="server"
                                    TargetControlID="txtLRDate"
                                    Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>

                            </div>

                        </div>--%>

                        <div class="col-md-3">

                            <div class="form-group">

                                <label class="form-control-label">
                                    Invoice Date:
                                </label>

                                <asp:TextBox
                                    ID="txtInvDate"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control">
                                </asp:TextBox>

                            </div>

                        </div>

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

                                <button type="button" id="btnUploadInvoice" class="btn btn-primary btn-sm mt-2">
                                    Upload &amp; Extract
                               
                                </button>
                                <div id="divMessage" class="mt-2"></div>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

            <div class="card details-card">

                <div class="card-header">

                    <h5>Material Request Details
                    </h5>

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

                    if (result.invoice_date) {
                        document.getElementById('txtInvDate').value = result.invoice_date.replace(/-/g, '/');
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
    <script type="text/javascript">

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
            var lrDoc = document.getElementById('fuLrDoc');

            if (!lrDoc ||
                !lrDoc.files ||
                lrDoc.files.length === 0) {

                errors.push('LR Document is required.');

            } else {

                var lrFileName = lrDoc.files[0].name;

                if (!/\.pdf$/i.test(lrFileName)) {
                    errors.push('LR Document must be a PDF file.');
                }
            }


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

    </script>

    <%--<script type="text/javascript">

        function toggleCourierCard() {

            var ddl = document.getElementById('ddlDelType');
            var card = document.getElementById('divCourierCard');
            var cardHeader = document.getElementById('lblCourierCardHeader');
            var courierNoLabel = document.getElementById('lblCourierNoLabel');
            var tranNameLabel = document.getElementById('lblTranNameLabel');

            if (!ddl || !card) {
                return;
            }

            var selectedOption = ddl.options[ddl.selectedIndex];
            var selectedText = selectedOption ? selectedOption.text.trim().toLowerCase() : '';
            var selectedValue = ddl.value ? ddl.value.trim() : '';

            if (selectedValue === '' || selectedText === '') {
                card.style.display = 'none';
                return;
            }

            card.style.display = 'block';

            if (selectedText.indexOf('courier') !== -1) {

                if (cardHeader) { cardHeader.textContent = 'Courier Information'; }
                if (courierNoLabel) { courierNoLabel.textContent = 'Courier No:'; }
                if (tranNameLabel) { tranNameLabel.textContent = 'Courier Name:'; }

            } else if (selectedText.indexOf('transport') !== -1) {

                if (cardHeader) { cardHeader.textContent = 'Transport Information'; }
                if (courierNoLabel) { courierNoLabel.textContent = 'Transport No:'; }
                if (tranNameLabel) { tranNameLabel.textContent = 'Transporter Name:'; }

            } else {

                if (cardHeader) { cardHeader.textContent = 'Courier Information'; }
                if (courierNoLabel) { courierNoLabel.textContent = 'Courier No:'; }
                if (tranNameLabel) { tranNameLabel.textContent = 'Transporter Name:'; }

            }
        }

        // Re-apply state on load (covers postbacks that keep the selected value)
        document.addEventListener('DOMContentLoaded', function () {
            toggleCourierCard();
        });

    </script>--%>
</body>
</html>
