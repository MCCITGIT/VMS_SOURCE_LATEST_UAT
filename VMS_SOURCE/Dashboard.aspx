<%@ Page Title="VMS Dashboard" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="netchartdir" Namespace="ChartDirector" TagPrefix="ChartDirector" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="includes/dashboard-cards.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /* Compact inner cards — Source Summary & Depot Wise Break-Up only */
        .vms-dashboard table.gv-cards > tbody {
            gap: 10px;
        }
        .vms-dashboard .ds-card,
        .vms-dashboard .ds-total {
            border-radius: 12px;
        }
        .vms-dashboard .ds-card:hover {
            transform: translateY(-2px);
        }
        .vms-dashboard .ds-card-head {
            padding: 8px 10px;
            gap: 8px;
        }
        .vms-dashboard .ds-identity {
            gap: 8px;
        }
        .vms-dashboard .ds-avatar {
            width: 30px;
            height: 30px;
            border-radius: 8px;
            font-size: 12px;
            box-shadow: 0 2px 6px rgba(27, 90, 140, 0.22);
        }
        .vms-dashboard .ds-kicker {
            margin-bottom: 2px;
            font-size: 9px;
            letter-spacing: 0.5px;
        }
        .vms-dashboard .ds-title {
            font-size: 13px;
        }
        .vms-dashboard .ds-pct {
            min-width: 64px;
            padding: 3px 8px;
            border-radius: 8px;
        }
        .vms-dashboard .ds-pct-value {
            font-size: 14px;
        }
        .vms-dashboard .ds-metrics {
            gap: 6px;
            padding: 8px;
        }
        .vms-dashboard .ds-metric {
            padding: 5px 7px;
            border-radius: 8px;
        }
        .vms-dashboard .ds-metric-label {
            gap: 4px;
            font-size: 9px;
            margin-bottom: 3px;
        }
        .vms-dashboard .ds-metric-label i {
            font-size: 10px;
            width: 14px;
        }
        .vms-dashboard .ds-metric-pair {
            gap: 4px;
        }
        .vms-dashboard .ds-kv-value {
            font-size: 12px;
        }
        .vms-dashboard .ds-total .ds-metrics {
            padding: 8px;
            gap: 6px;
        }
    </style>

    <div class="vms-dashboard">

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">VMS Dashboard</h3>
                <p class="pageSubTitle">Key despatch and indent indicators at a glance</p>
            </div>
        </div>
        <div class="rightFung">
            AS ON:
            <asp:Label ID="lblAson" runat="server"></asp:Label>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="dash-panel">
                <div class="dash-panel-body">
                    <div class="dash-filter-grid">
                        <div class="dash-field">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2" TabIndex="2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="dash-field">
                            <div class="form-group">
                                <label class="form-control-label">Region:</label>
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="dash-field">
                            <div class="form-group">
                                <label class="form-control-label">Depot:</label>
                                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="dash-field">
                            <div class="form-group">
                                <label class="form-control-label">Process Year:</label>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="3">
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                    <asp:ListItem>2024</asp:ListItem>
                                    <asp:ListItem>2025</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="dash-field">
                            <div class="form-group">
                                <label class="form-control-label">Process Month:</label>
                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="3">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="dash-field dash-field-action">
                            <div class="form-group">
                                <%--<asp:ImageButton ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" AlternateText="Home" ImageUrl="~/images/search.png" />--%>
                                <asp:LinkButton ID="btnSearch" CssClass="btn btn-primary btn-sm dash-search-btn" runat="server" AlternateText="Home" OnClick="btnSearch_Click"><i class="fas fa-search" aria-hidden="true"></i> Search</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="dash-panel dash-acc">
                <button type="button" id="dashSourceToggle" class="dash-panel-head dash-acc-toggle" data-toggle="collapse" data-target="#dashSourceBody" aria-expanded="true" aria-controls="dashSourceBody">
                    <h6 class="dash-panel-title"><i class="fas fa-industry"></i> Source Summary</h6>
                    <span class="dash-acc-caret" aria-hidden="true"><i class="fas fa-chevron-down"></i></span>
                </button>
                <div id="dashSourceBody" class="collapse show">
                <div class="dash-panel-body">
                    <div class="dash-toolbar">
                        <div class="form-group row ddlFinYear">
                            <label for="ddlPageSize" class="col-auto form-control-label">
                                <asp:Label ID="Label4" runat="server" Text="Results Per Page:"></asp:Label>
                            </label>
                            <div class="col-auto">
                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row ddlPageSize dash-stock-chip">
                            <label for="ddlPageSize" class="col-auto form-control-label">Last Update Stock As On:</label>
                            <asp:Label ID="lblLaststok" runat="server" CssClass="col-auto font-weight-bold" Text=""></asp:Label>
                        </div>
                    </div>
                    <asp:GridView ID="gvUnitSummery" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                        BorderWidth="0" GridLines="None" ShowHeader="False" CssClass="gv-cards gv-unit-cards" ShowFooter="true" EmptyDataText="No Record Found">
                        <RowStyle CssClass="tlrowlight" />
                        <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                        <HeaderStyle CssClass="headerGrid" />
                        <FooterStyle CssClass="footerGrid" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <article class="ds-card">
                                        <header class="ds-card-head">
                                            <div class="ds-identity">
                                                <span class="ds-avatar" aria-hidden="true"><i class="fas fa-industry"></i></span>
                                                <div>
                                                    <span class="ds-kicker">UNIT</span>
                                                    <span class="ds-title"><%# Eval("unit") %></span>
                                                </div>
                                            </div>
                                            <div class="ds-pct">
                                                <span class="ds-pct-value">
                                                    <asp:Label ID="lblTotalDespatch" runat="server" Text='<%# Bind("despatchedPercent") %>'></asp:Label>
                                                </span>
                                                <span class="ds-pct-label">% DESPACHED</span>
                                            </div>
                                        </header>
                                        <div class="ds-metrics">
                                            <div class="ds-metric ds-m-auto">
                                                <div class="ds-metric-label"><i class="fas fa-bolt"></i> AUTO INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Kl" runat="server" Text='<%# Bind("autoindent_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Mt" runat="server" Text='<%# Bind("autoindent_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-depot">
                                                <div class="ds-metric-label"><i class="fas fa-warehouse"></i> DEPOT INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Kl" runat="server" Text='<%# Bind("depotindent_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Mt" runat="server" Text='<%# Bind("depotindent_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-transit">
                                                <div class="ds-metric-label"><i class="fas fa-truck"></i> TRANSIT STOCK</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Kl" runat="server" Text='<%# Bind("transit_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Mt" runat="server" Text='<%# Bind("transit_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-pending">
                                                <div class="ds-metric-label"><i class="fas fa-clock"></i> PENDING LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Kl" runat="server" Text='<%# Bind("pending_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Mt" runat="server" Text='<%# Bind("pending_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-month">
                                                <div class="ds-metric-label"><i class="fas fa-calendar-alt"></i> MONTH LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Kl" runat="server" Text='<%# Bind("monthload_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Mt" runat="server" Text='<%# Bind("monthload_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-despatch">
                                                <div class="ds-metric-label"><i class="fas fa-shipping-fast"></i> DESPATCH-TO-DATE</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Kl" runat="server" Text='<%# Bind("despatch_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Mt" runat="server" Text='<%# Bind("despatch_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </article>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <article class="ds-total">
                                        <header class="ds-card-head">
                                            <div class="ds-identity">
                                                <span class="ds-avatar" aria-hidden="true"><i class="fas fa-layer-group"></i></span>
                                                <div>
                                                    <span class="ds-kicker">UNIT</span>
                                                    <span class="ds-title">Grand Total</span>
                                                </div>
                                            </div>
                                            <div class="ds-pct">
                                                <span class="ds-pct-value">
                                                    <asp:Label ID="lblTotalDespatch_Ftr" runat="server"></asp:Label>
                                                </span>
                                                <span class="ds-pct-label">% DESPACHED</span>
                                            </div>
                                        </header>
                                        <div class="ds-metrics">
                                            <div class="ds-metric ds-m-auto">
                                                <div class="ds-metric-label"><i class="fas fa-bolt"></i> AUTO INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-depot">
                                                <div class="ds-metric-label"><i class="fas fa-warehouse"></i> DEPOT INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-transit">
                                                <div class="ds-metric-label"><i class="fas fa-truck"></i> TRANSIT STOCK</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-pending">
                                                <div class="ds-metric-label"><i class="fas fa-clock"></i> PENDING LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-month">
                                                <div class="ds-metric-label"><i class="fas fa-calendar-alt"></i> MONTH LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-despatch">
                                                <div class="ds-metric-label"><i class="fas fa-shipping-fast"></i> DESPATCH-TO-DATE</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </article>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
            </div>

            <div class="dash-panel">
                <div class="dash-panel-body">
                    <div class="dash-filter-grid">
                        <div class="dash-field">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:Label ID="lblUnit" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="dash-field">
                            <div class="form-group">
                                <label class="form-control-label">Product:</label>
                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="dash-panel dash-acc">
                <button type="button" id="dashDepotToggle" class="dash-panel-head dash-acc-toggle" data-toggle="collapse" data-target="#dashDepotBody" aria-expanded="true" aria-controls="dashDepotBody">
                    <h6 class="dash-panel-title"><i class="fas fa-warehouse"></i> Depot Wise Break-Up</h6>
                    <span class="dash-acc-caret" aria-hidden="true"><i class="fas fa-chevron-down"></i></span>
                </button>
                <div id="dashDepotBody" class="collapse show">
                <div class="dash-panel-body">
                    <div class="dash-toolbar">
                        <div class="form-group row ddlPageSize">
                            <label for="ddlPageSize" class="col-auto form-control-label">
                                <span>Results Per Page:</span>
                            </label>
                            <div class="col-auto">
                                <asp:DropDownList ID="ddlPageSize0" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvDepotSummery" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                        BorderWidth="0" GridLines="None" ShowHeader="False" CssClass="gv-cards gv-depot-cards" ShowFooter="true" EmptyDataText="No Record Found">
                        <RowStyle CssClass="tlrowlight" />
                        <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                        <HeaderStyle CssClass="headerGrid" />
                        <FooterStyle CssClass="footerGrid" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <article class="ds-card">
                                        <header class="ds-card-head">
                                            <div class="ds-identity">
                                                <span class="ds-avatar is-depot" aria-hidden="true"><i class="fas fa-warehouse"></i></span>
                                                <div>
                                                    <span class="ds-kicker">DEPOT</span>
                                                    <span class="ds-title"><%# Eval("depot") %></span>
                                                </div>
                                            </div>
                                        </header>
                                        <div class="ds-metrics">
                                            <div class="ds-metric ds-m-stock">
                                                <div class="ds-metric-label"><i class="fas fa-boxes"></i> DEPOT STOCK</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblStock_Kl" runat="server" Text='<%# Bind("stock_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblStock_Mt" runat="server" Text='<%# Bind("stock_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-auto">
                                                <div class="ds-metric-label"><i class="fas fa-bolt"></i> AUTO INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Kl0" runat="server" Text='<%# Bind("autoindent_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Mt0" runat="server" Text='<%# Bind("autoindent_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-depot">
                                                <div class="ds-metric-label"><i class="fas fa-clipboard-list"></i> DEPOT INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Kl0" runat="server" Text='<%# Bind("depotindent_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Mt0" runat="server" Text='<%# Bind("depotindent_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-pending">
                                                <div class="ds-metric-label"><i class="fas fa-clock"></i> PENDING LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Kl0" runat="server" Text='<%# Bind("pending_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Mt0" runat="server" Text='<%# Bind("pending_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-transit">
                                                <div class="ds-metric-label"><i class="fas fa-truck"></i> TRANSIT STOCK</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Kl0" runat="server" Text='<%# Bind("transit_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Mt0" runat="server" Text='<%# Bind("transit_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-month">
                                                <div class="ds-metric-label"><i class="fas fa-calendar-alt"></i> MONTH LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Kl" runat="server" Text='<%# Bind("monthload_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Mt" runat="server" Text='<%# Bind("monthload_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-despatch">
                                                <div class="ds-metric-label"><i class="fas fa-shipping-fast"></i> DESPATCH-TO-DATE</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Kl0" runat="server" Text='<%# Bind("despatch_kl") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Mt0" runat="server" Text='<%# Bind("despatch_mt") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </article>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <article class="ds-total">
                                        <header class="ds-card-head">
                                            <div class="ds-identity">
                                                <span class="ds-avatar is-depot" aria-hidden="true"><i class="fas fa-layer-group"></i></span>
                                                <div>
                                                    <span class="ds-kicker">DEPOT</span>
                                                    <span class="ds-title">Grand Total</span>
                                                </div>
                                            </div>
                                        </header>
                                        <div class="ds-metrics">
                                            <div class="ds-metric ds-m-stock">
                                                <div class="ds-metric-label"><i class="fas fa-boxes"></i> DEPOT STOCK</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblStock_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblStock_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-auto">
                                                <div class="ds-metric-label"><i class="fas fa-bolt"></i> AUTO INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Kl_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblAutoindent_Mt_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-depot">
                                                <div class="ds-metric-label"><i class="fas fa-clipboard-list"></i> DEPOT INDENT</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Kl_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDepotindent_Mt_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-pending">
                                                <div class="ds-metric-label"><i class="fas fa-clock"></i> PENDING LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblPendingLoad_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-transit">
                                                <div class="ds-metric-label"><i class="fas fa-truck"></i> TRANSIT STOCK</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Kl_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblTransit_Mt_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-month">
                                                <div class="ds-metric-label"><i class="fas fa-calendar-alt"></i> MONTH LOAD</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Kl_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblMonthLoad_Mt_Ftr" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="ds-metric ds-m-despatch">
                                                <div class="ds-metric-label"><i class="fas fa-shipping-fast"></i> DESPATCH-TO-DATE</div>
                                                <div class="ds-metric-pair">
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">KL</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Kl_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="ds-kv">
                                                        <span class="ds-kv-unit">MT</span>
                                                        <span class="ds-kv-value">
                                                            <asp:Label ID="lblDespatch_Mt_Ftr0" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </article>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="dash-panel">
                        <div class="dash-panel-body">
                            <div class="dash-charts">
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit1" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit2" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit3" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit4" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit5" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit6" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit7" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit8" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit9" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit10" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit11" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit12" runat="server" /></div>
                                <div class="dash-chart-frame"><ChartDirector:WebChartViewer ID="cv_unit13" runat="server" /></div>
                            </div>
                            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage"></div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        (function () {
            var STORE = { source: 'vmsDashAcc_source', depot: 'vmsDashAcc_depot' };
            var bound = false;

            function isOpen(key) {
                try { return sessionStorage.getItem(key) !== '0'; } catch (e) { return true; }
            }
            function setOpen(key, open) {
                try { sessionStorage.setItem(key, open ? '1' : '0'); } catch (e) { }
            }
            function sync(toggleSel, bodySel, key) {
                var $body = $(bodySel);
                var $toggle = $(toggleSel);
                if (!$body.length) { return; }
                var open = isOpen(key);
                $body.toggleClass('show', open);
                $toggle.toggleClass('collapsed', !open);
                $toggle.attr('aria-expanded', open ? 'true' : 'false');
            }
            function bind() {
                if (!window.jQuery) { return; }
                $('#dashSourceBody').off('shown.bs.collapse.dash hidden.bs.collapse.dash')
                    .on('shown.bs.collapse.dash', function () { setOpen(STORE.source, true); })
                    .on('hidden.bs.collapse.dash', function () { setOpen(STORE.source, false); });
                $('#dashDepotBody').off('shown.bs.collapse.dash hidden.bs.collapse.dash')
                    .on('shown.bs.collapse.dash', function () { setOpen(STORE.depot, true); })
                    .on('hidden.bs.collapse.dash', function () { setOpen(STORE.depot, false); });
                sync('#dashSourceToggle', '#dashSourceBody', STORE.source);
                sync('#dashDepotToggle', '#dashDepotBody', STORE.depot);
            }
            function start() {
                if (!window.jQuery) { setTimeout(start, 30); return; }
                bind();
                if (!bound && window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bind);
                    bound = true;
                }
            }
            if (document.readyState === 'complete') { start(); }
            else { window.addEventListener('load', start); }
        })();
    </script>

    </div>
</asp:Content>
