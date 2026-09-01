<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="VprDashboard.aspx.vb" Inherits="VprDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="includes/rm-procurement.css?v=<%= DateTime.Now.Ticks %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .form-control.field-invalid {
            border-color: #dc3545 !important;
            box-shadow: 0 0 0 3px rgba(220, 53, 69, 0.12);
        }

        .dispatch-field-error {
            display: block;
            color: #dc3545;
            font-size: 12px;
            font-weight: 500;
            margin-top: 4px;
            line-height: 1.35;
        }

            .dispatch-field-error:empty {
                display: none;
            }
    </style>
    <div class="rm-module rm-compact rm-brand-master">
        <script type="text/javascript">
            document.onkeydown = checkValue;
            function checkValue() {
                if (event.keyCode == 118) { // button Add (F7 keypress)
                    if (document.getElementById('btnSubmit').disabled == true)
                        return false;
                    else {
                        // button Add (F7 keypress)
                        validateSKUList();
                    }
                    //__doPostBack(document.getElementById('btnSubmit').name, '');
                }
                else if (event.keyCode == 119) {
                    __doPostBack(document.getElementById('btnCancel').name, '');
                }
            }

            function disableBackButton() {
                window.history.forward(1);
            }
        </script>
        <script type="text/javascript" src="Scripts/ValidateAddUpdate_ProductMaster.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
        <style>
            .errormsg {
                font-size: 13px;
            }
        </style>
        <div class="breadcrumbs">
            <div class="leftFung">
                <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
                <div class="diveider">/</div>
                <div class="pageTitleWrap">
                    <h3 class="pageTitle">Vendor Payment Dashboard</h3>
                    <p class="pageSubTitle">Manage and evaluate your active supplier relationships.</p>
                </div>
            </div>
            <div class="rightFung"></div>
        </div>

        <div class="card">
            <div class="card-body">
                <div class="rm-add-stats-row">
                    <div class="rm-add-form">
                        <div class="form-group pb-0 mb-0">
                            <label class="form-control-label">Search Vendor:<span id="Span2" class="mandatory">*</span></label>
                            <div class="rm-add-form-controls">
                                <asp:TextBox ID="txtVendorName" ClientIDMode="Static" CssClass="form-control" runat="server" AutoComplete="Off" Placeholder="Enter Here"></asp:TextBox>
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date:"></asp:Label>
                                <asp:TextBox
                                    ID="txtFromDate"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control"
                                    autocomplete="off"
                                    placeholder="Select From Date."
                                    onkeydown="return datePickerOnly(event, this);"
                                    onpaste="return true;"
                                    ondrop="return true;">
                                </asp:TextBox>
                                <ajaxToolkit:CalendarExtender
                                    ID="calFromDate"
                                    runat="server"
                                    TargetControlID="txtFromDate"
                                    Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:Label ID="lblToDate" runat="server" Text="To Date:"></asp:Label>
                                <asp:TextBox
                                    ID="txtToDate"
                                    runat="server"
                                    ClientIDMode="Static"
                                    CssClass="form-control"
                                    autocomplete="off"
                                    placeholder="Select To Date."
                                    onkeydown="return datePickerOnly(event, this);"
                                    onpaste="return true;"
                                    ondrop="return true;">
                                </asp:TextBox>
                                <ajaxToolkit:CalendarExtender
                                    ID="calToDate"
                                    runat="server"
                                    TargetControlID="txtToDate"
                                    Format="dd-MM-yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:Button ID="btnSubmit" ClientIDMode="Static" runat="server" Text="Search" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-outline-danger btn-sm" OnClick="btnReset_Click" />
                            </div>
                            <asp:Label ID="valBrandName" runat="server" ClientIDMode="Static" CssClass="dispatch-field-error"></asp:Label>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 10px; font-weight: bold; color: red;" Text=""></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="card rm-list-fill">
            <div class="mst-panel-header">
                <div class="mst-panel-header-left">
                    <span class="mst-panel-icon"><i class="fas fa-list"></i></span>
                    <div>
                        <h5 class="mst-panel-title">Vendor List</h5>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="table-responsive rm-grid-scroll">
                    <asp:GridView CssClass="table table-hover upgradDataGrid" CellSpacing="0" CellPadding="0"
                        ID="gvFgVendorlist" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" Visible="true" OnPageIndexChanging="gvFgVendorlist_PageIndexChanging"
                        ShowFooter="false" PagerSettings-Mode="NumericFirstLast" PagerSettings-PageButtonCount="5"
                        PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last">
                        <RowStyle CssClass="tlrowlight" />
                        <PagerStyle CssClass="PagerGrid" HorizontalAlign="Left" />
                        <HeaderStyle CssClass="headerGrid" />
                        <FooterStyle CssClass="footerGrid" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl No">
                                <ItemTemplate>
                                    <asp:Label ID="lblbrandid" runat="server" Text='<%# (gvFgVendorlist.PageIndex * gvFgVendorlist.PageSize) + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblbrandname" runat="server" Text='<%# Bind("unit_name") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnBrandId" runat="server" Value='<%# Bind("unit_code") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60%" CssClass="text-left" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60%" CssClass="text-left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRN Not Done">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrnNotDone" runat="server" Text='<%# Bind("grn_not_done") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" CssClass="text-left" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" CssClass="text-left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual GRN">
                                <ItemTemplate>
                                    <asp:Label ID="lblmanualGrn" runat="server" Text='<%# Bind("manual_grn") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" CssClass="text-left" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" CssClass="text-left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PAID">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidStatus" runat="server" Text='<%# Bind("paid_status") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnInvAmt" runat="server" Value='<%# Bind("invoice_amount") %>' />
                                    <asp:HiddenField ID="hdnAmtPaid" runat="server" Value='<%# Bind("amount_paid") %>' />
                                    <asp:HiddenField ID="hfnBalAmt" runat="server" Value='<%# Bind("balance_amount") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" CssClass="text-left" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" CssClass="text-left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDetails" CommandName="Details" runat="server" CssClass="text-info" ToolTip="View Details"><i class="fas fa-eye"></i></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" CssClass="text-center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" CssClass="text-center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <script>
        function datePickerOnly(event, textbox) {

            // Allow Backspace/Delete to clear the field
            if (event.key === "Backspace" || event.key === "Delete") {
                textbox.value = "";
                return false;
            }

            // Allow Tab, Shift, Ctrl, Alt
            if (
                event.key === "Tab" ||
                event.key === "Shift" ||
                event.key === "Control" ||
                event.key === "Alt"
            ) {
                return true;
            }

            // Block all typing
            event.preventDefault();
            return false;
        }
    </script>
    <script type="text/javascript" src="Scripts/rm-status-confirm.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>

