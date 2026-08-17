<%@ Page Title="VMS Dashboard" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="netchartdir" Namespace="ChartDirector" TagPrefix="ChartDirector" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="includes/GlassBlackGridView.css" rel="stylesheet" type="text/css" />

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
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2" TabIndex="2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Region:</label>
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Depot:</label>
                                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
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
                        <div class="col-md-2">
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
                        <div class="col-md-1 form-btn-mt">
                            <div class="form-group">
                                <%--<asp:ImageButton ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" AlternateText="Home" ImageUrl="~/images/search.png" />--%>
                                <asp:LinkButton ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" AlternateText="Home" OnClick="btnSearch_Click">Search</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <h6 class="card-title m-0">Source Summary</h6>
                </div>
                <div class="card-body">
                    <div class="dflexCSb">
                        <div class="form-group row ddlFinYear">
                            <label for="ddlPageSize" class="col-auto form-control-label">
                                <asp:Label ID="Label4" runat="server" Text="Results Per Page:"></asp:Label>
                            </label>
                            <div class="col-auto">
                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row ddlPageSize">
                            <label for="ddlPageSize" class="col-auto form-control-label">Last Update Stock As On:</label>
                            <asp:Label ID="lblLaststok" runat="server" CssClass="col-auto font-weight-bold" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvUnitSummery" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                            BorderWidth="1" CssClass="table table-hover upgradDataGrid" ShowFooter="true" EmptyDataText="No Record Found">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="UNIT" DataField="unit" FooterText="Grand Total">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">AUTO INDENT
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblAutoindent_Kl" runat="server" Text='<%# Bind("autoindent_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblAutoindent_Mt" runat="server" Text='<%# Bind("autoindent_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblAutoindent_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblAutoindent_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">DEPOT INDENT
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDepotindent_Kl" runat="server" Text='<%# Bind("depotindent_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDepotindent_Mt" runat="server" Text='<%# Bind("depotindent_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDepotindent_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDepotindent_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">TRANSIT STOCK
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblTransit_Kl" runat="server" Text='<%# Bind("transit_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTransit_Mt" runat="server" Text='<%# Bind("transit_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblTransit_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTransit_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">PENDING LOAD
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblPendingLoad_Kl" runat="server" Text='<%# Bind("pending_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPendingLoad_Mt" runat="server" Text='<%# Bind("pending_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblPendingLoad_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPendingLoad_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">MONTH LOAD
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblMonthLoad_Kl" runat="server" Text='<%# Bind("monthload_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblMonthLoad_Mt" runat="server" Text='<%# Bind("monthload_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblMonthLoad_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblMonthLoad_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">DESPATCH-TO-DATE
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDespatch_Kl" runat="server" Text='<%# Bind("despatch_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDespatch_Mt" runat="server" Text='<%# Bind("despatch_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDespatch_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDespatch_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="% DESPACHED">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalDespatch" runat="server" Text='<%# Bind("despatchedPercent") %>'></asp:Label>
                                        &nbsp; &nbsp;
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalDespatch_Ftr" runat="server"></asp:Label>&nbsp; &nbsp;
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="7%" />
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="7%" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="% PENDING">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalSKU" runat="server" Text='<%# Bind("pendingPercent") %>'></asp:Label>&nbsp; &nbsp;
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTotalSKU_Ftr" runat="server"></asp:Label> &nbsp; &nbsp;
                                                                            </FooterTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="7%" />
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="7%" />
                                                                        </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:Label ID="lblUnit" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Product:</label>
                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <h6 class="card-title m-0">Depot Wise Break-Up</h6>
                </div>
                <div class="card-body">
                    <div class="form-group row ddlPageSize">
                        <label for="ddlPageSize" class="col-auto form-control-label">
                            <span>Results Per Page:</span>
                        </label>
                        <div class="col-md-1">
                            <asp:DropDownList ID="ddlPageSize0" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvDepotSummery" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                            BorderWidth="1" CssClass="table table-hover upgradDataGrid" GridLines="None" ShowFooter="true" EmptyDataText="No Record Found">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="DEPOT" DataField="depot" FooterText="Grand Total">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">DEPOT STOCK
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblStock_Kl" runat="server" Text='<%# Bind("stock_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStock_Mt" runat="server" Text='<%# Bind("stock_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblStock_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">&nbsp;
                                                                                                    <asp:Label ID="lblStock_Mt_Ftr" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">AUTO INDENT
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblAutoindent_Kl0" runat="server" Text='<%# Bind("autoindent_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblAutoindent_Mt0" runat="server" Text='<%# Bind("autoindent_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblAutoindent_Kl_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblAutoindent_Mt_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">DEPOT INDENT
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDepotindent_Kl0" runat="server" Text='<%# Bind("depotindent_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDepotindent_Mt0" runat="server" Text='<%# Bind("depotindent_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDepotindent_Kl_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDepotindent_Mt_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">PENDING LOAD
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblPendingLoad_Kl0" runat="server" Text='<%# Bind("pending_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPendingLoad_Mt0" runat="server" Text='<%# Bind("pending_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblPendingLoad_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPendingLoad_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">TRANSIT STOCK
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblTransit_Kl0" runat="server" Text='<%# Bind("transit_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTransit_Mt0" runat="server" Text='<%# Bind("transit_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblTransit_Kl_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTransit_Mt_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">MONTH LOAD
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000;">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblMonthLoad_Kl" runat="server" Text='<%# Bind("monthload_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblMonthLoad_Mt" runat="server" Text='<%# Bind("monthload_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblMonthLoad_Kl_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblMonthLoad_Mt_Ftr" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <HeaderTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr>
                                                <td colspan="2" style="width: 100%; border-bottom: 1px solid #000000;">DESPATCH-TO-DATE
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%; border-right: 1px solid #000000;">KL
                                                </td>
                                                <td style="width: 50%">MT
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDespatch_Kl0" runat="server" Text='<%# Bind("despatch_kl") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDespatch_Mt0" runat="server" Text='<%# Bind("despatch_mt") %>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%" cellspacing="0">
                                            <tr style="height: 100%;">
                                                <td style="width: 50%; border-right: 1px solid #000000; text-align: right">
                                                    <asp:Label ID="lblDespatch_Kl_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDespatch_Mt_Ftr0" runat="server"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table table-hover upgradDataGrid">
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit4" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit5" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit6" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit7" runat="server" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit8" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit9" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit10" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit11" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit12" runat="server" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ChartDirector:WebChartViewer ID="cv_unit13" runat="server" /></td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage"></div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
