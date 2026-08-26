<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RMProdutwiseConsumption.aspx.vb" Inherits="RMProdutwiseConsumption" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RM Consumption Product Wise</title>
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <link href="includes/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="includes/upgrad-style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Scripts/anchorposition.js"></script>
    <script type="text/javascript" src="Scripts/popupwindow.js"></script>
    <script type="text/javascript" src="Scripts/calendarpopup.js"></script>
    <script type="text/javascript" src="Scripts/date.js"></script>
    <script type="text/javascript" src="Scripts/Currency.js"></script>
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/Messages.js"></script>
    <script type="text/javascript" src="Scripts/RegEX.js"></script>
    <script type="text/javascript" src="Scripts/AjaxServices.js"></script>
    <script type="text/javascript" src="Scripts/Autocomplete.js"></script>
    <script>
        //document.addEventListener("DOMContentLoaded", function () {
        //    const headers = document.querySelectorAll(".p-hide-all-header .headerGrid");
        //    const headerCells = document.querySelectorAll(".p-hide-all-header .headerGrid th");
        //    headers.forEach(function (el, index) {
        //        if (index > 0) { // skip first element
        //            el.style.display = "none";
        //        }
        //    });
        //    headerCells.forEach(function (th) {
        //        th.style.position = "sticky";
        //        th.style.top = "0";
        //        th.style.zIndex = "10";
        //    });
        //});
        document.addEventListener("DOMContentLoaded", function () {

            const headers = document.querySelectorAll(".p-hide-all-header .headerGrid");

            // Hide all header rows except the first one
            headers.forEach(function (el, index) {
                if (index > 0) {
                    el.style.display = "none";
                }
            });
        });
        document.addEventListener("DOMContentLoaded", function () {
            const headers = document.querySelectorAll(".p-hide-all-header .p-products-heading");
            headers.forEach(function (el, index) {
                if (index > 0) { // skip first element
                    el.style.display = "none";
                }
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
        <div class="header">
            <div class="container">
                <div class="headerContainer">
                    <div class="logoSection">
                        <img class="logo" src="images/berger-paints-logo.png" alt="logo" />
                        <h3 class="ModuleName">Vendor Management Software</h3>
                    </div>
                    <a href="Home.aspx" title="Home">
                        <img class="homeIcon" src="images/3d-house.png" alt="Home" />
                    </a>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="breadcrumbs">
                <div class="leftFung">
                    <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
                    <div class="diveider">/</div>
                    <h3 class="pageTitle">RM Consumtion Product Wise</h3>
                </div>
                <div class="rightFung"></div>
            </div>
        </div>

        <div class="container">
            <div class="card">
                <div class="row">
                    <div class="col-md-12">

                        <!-- Header Section -->
                        <asp:UpdatePanel ID="updtl" runat="server">
                            <ContentTemplate>
                                <div class="header-section">
                                    <div class="row align-items-center">
                                        <!-- Product Image -->
                                        <div class="col-auto">
                                            <div class="product-image-wrapper">
                                                <img src="./images/product-placeholder.png" alt="Product Image" class="product-image">
                                            </div>
                                        </div>
                                        <!-- Product Info -->
                                        <div class="col">
                                            <div class="product-info">
                                                <span class="quarterly-badge" id="rmquarter" runat="server">Quarterly: Q4</span>
                                                <h2 class="dealer-name" id="lbvendor" runat="server">Santanu Nag Rahul</h2>
                                                <div class="product-details">
                                                    <%--<div class="detail-item">
                                                        <i class="fas fa-box-open"></i>
                                                        <span>Product:</span>
                                                        <strong id="lbproduct" runat="server">Bison Putty</strong>
                                                    </div>
                                                    <div class="detail-item">
                                                        <i class="fas fa-tag"></i>
                                                        <span>Brand:</span>
                                                        <strong>Bison Putty</strong>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- Total Dispatch Badge -->
                                        <div class="col-auto">
                                            <div class="dispatch-badge">
                                                <span class="dispatch-label">TOTAL DISPATCH</span>
                                                <span class="dispatch-value" id="dispatchvol" runat="server">100 MT</span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="stats-section">
                            <div class="row">
                                <!-- Allocation Card -->
                                <div class="col-md-3 mb-3">
                                    <div class="stat-card allocation-card allocationBColr" style="position: relative">
                                        <%--<img class="right-arrow" src="images/allocation-supply-arrow.png" alt="arrow" />--%>
                                        <div class="fade-arrow right-arrow">
                                            <div class="fade-arrow__shaft fa1-shaft"></div>
                                            <div class="fade-arrow__head fa1-head"></div>
                                        </div>

                                        <div class="stat-card-header">
                                            <div class="stat-icon allocation-icon">
                                                <i class="fas fa-chart-pie"></i>
                                            </div>
                                            <h5 class="stat-title">Allocation</h5>
                                            <div class="stat-decoration">
                                                <i class="fas fa-truck"></i>
                                            </div>
                                        </div>
                                        <div class="stat-card-body">
                                            <asp:UpdatePanel ID="upAllocation" runat="server">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="rptAllocation" runat="server">
                                                        <ItemTemplate>
                                                            <div class="stat-row">
                                                                <div class="stat-label">
                                                                    <span class="label-name"><%# Eval("tc_chemical_name") %></span>

                                                                </div>
                                                                <span class="stat-value"><%# Eval("alloted") %></span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <!-- Supply Card -->
                                <div class="col-md-3 mb-3">
                                    <div class="stat-card supply-card supplyBColr" style="position: relative">
                                        <%--<img class="right-arrow" src="images/remaining-arrow.png" alt="arrow" />--%>
                                        <div class="fade-arrow right-arrow">
                                            <div class="fade-arrow__shaft fa2-shaft"></div>
                                            <div class="fade-arrow__head fa2-head"></div>
                                        </div>
                                        <div class="stat-card-header">
                                            <div class="stat-icon supply-icon">
                                                <i class="fas fa-dolly"></i>
                                            </div>
                                            <h5 class="stat-title">Supply</h5>
                                            <div class="stat-decoration">
                                                <i class="fas fa-shopping-cart"></i>
                                            </div>
                                        </div>

                                        <div class="stat-card-body">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="rptSupply" runat="server">
                                                        <ItemTemplate>
                                                            <div class="stat-row">
                                                                <div class="stat-label">
                                                                    <span class="label-name"><%# Eval("tc_chemical_name") %></span>

                                                                </div>
                                                                <span class="stat-value"><%# Eval("supply") %></span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <!-- Consumption Card -->
                                <div class="col-md-3 mb-3">
                                    <div class="stat-card consumption-card consumptionBColr" style="position: relative">
                                        <%--<img class="right-arrow" src="images/consumption-arrow.png" alt="arrow" />--%>
                                        <div class="fade-arrow right-arrow">
                                            <div class="fade-arrow__shaft fa3-shaft"></div>
                                            <div class="fade-arrow__head fa3-head"></div>
                                        </div>
                                        <div class="stat-card-header">
                                            <div class="stat-icon consumption-icon">
                                                <i class="fas fa-tint"></i>
                                            </div>
                                            <h5 class="stat-title">Consumption</h5>
                                            <div class="stat-decoration">
                                                <i class="fas fa-seedling"></i>
                                            </div>
                                        </div>
                                        <div class="stat-card-body">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="rptConsumption" runat="server">
                                                        <ItemTemplate>
                                                            <div class="stat-row">
                                                                <div class="stat-label">
                                                                    <span class="label-name"><%# Eval("tc_chemical_name") %></span>

                                                                </div>
                                                                <span class="stat-value"><%# Eval("consumption") %></span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <!-- Remaining Card -->
                                <div class="col-md-3 mb-3">
                                    <div class="stat-card remaining-card remainingBColr">
                                        <div class="stat-card-header">
                                            <div class="stat-icon remaining-icon">
                                                <i class="fas fa-layer-group"></i>
                                            </div>
                                            <h5 class="stat-title">Remaining</h5>
                                            <div class="stat-decoration">
                                                <i class="fas fa-hourglass-half"></i>
                                            </div>
                                        </div>
                                        <div class="stat-card-body">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="rptRemaining" runat="server">
                                                        <ItemTemplate>
                                                            <div class="stat-row">
                                                                <div class="stat-label">
                                                                    <span class="label-name"><%# Eval("tc_chemical_name") %></span>

                                                                </div>
                                                                <span class="stat-value"><%# Eval("remaning") %></span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <%--Product wise Consumption--%>
                            <div class="container-fluid">

    <!-- HEADER ROW -->
      <div style="position: sticky; top: 0; z-index: 999;">
        <div class="row headerGrid">
            <div class="col-md-3" style="padding-right:0;">
                <div class="p-products-heading">
                    <p>📋 PRODUCT</p>
                </div>
            </div>
            <div class="col-md-9" style="padding-left:0;">
                <table class="table p-upgradDataGrid">
                    <tr class="headerGrid">
                        <th style="width:18%">📦 VOLUME</th>
                        <th style="width:20%">⚗️ RAW MATERIALS</th>
                        <th style="width:12%">% DOSAGE</th>
                        <th style="width:20%">📊 CONSUMPTION</th>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <!-- REPEATER -->
    <asp:Repeater ID="rptcusumeProduct" runat="server" 
        OnItemDataBound="rptcusumeProduct_ItemDataBound">

        <ItemTemplate>

            <div class="row align-items-start">

                <!-- PRODUCT COLUMN -->
                <div class="col-md-3" style="padding-right:0;">
                    <div style="border:1px solid #ededed;border-right:0;">
                        
                        <div class="prod-label">
                            <span class="label-name">
                                <%# Eval("productname") %>
                            </span>

                            <asp:HiddenField runat="server"
                                ID="hdnVendor"
                                Value='<%# Eval("vendorid") %>' />

                            <asp:HiddenField runat="server"
                                ID="hdnProductCode"
                                Value='<%# Eval("productcode") %>' />
                        </div>

                    </div>
                </div>


                <!-- GRID COLUMN -->
                <div class="col-md-9" style="padding-left:0;">
                    <div class="table-responsive tvlGridHt">
                        <asp:UpdatePanel ID="updetails" runat="server">
                            <ContentTemplate>

                                <asp:GridView
                                    ID="gvConsumption"
                                    runat="server"
                                    CssClass="p-upgradDataGrid"
                                    AutoGenerateColumns="false"
                                    ShowHeader="false"
                                    GridLines="Both"
                                    BorderWidth="0"
                                    CellSpacing="0"
                                    CellPadding="0"
                                    OnRowDataBound="gvConsumption_RowDataBound">

                                    <RowStyle CssClass="tlrowlight" />
                                    <AlternatingRowStyle CssClass="tlrowdark" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lbVolume"
                                                    runat="server"
                                                    CssClass="lbl-volume"
                                                    Text='<%# Bind("total_despatch_production_yield") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="18%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRawMaterial"
                                                    runat="server"
                                                    Text='<%# Bind("tc_chemical_name") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDosage"
                                                    runat="server"
                                                    CssClass="lbl-dosage"
                                                    Text='<%# Bind("tc_rm_dosage") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblConsumption"
                                                    runat="server"
                                                    CssClass="lbl-consumption"
                                                    Text='<%# Bind("consumption") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>

            </div>

        </ItemTemplate>

    </asp:Repeater>

</div>
                           <%-- <div class="row" style="margin-bottom: 4px">
                                <div class="col-md-12 p-hide-all-header">
                                    <asp:Repeater ID="rptcusumeProduct" runat="server" OnItemDataBound="rptcusumeProduct_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="row align-items-start">
                                                <div class="col-md-3" style="padding-right: 0;">
                                                    <div style="border: 1px solid #ededed; border-right: 0;">
                                                        <div class="p-products-heading">
                                                            <p>&#x1F4CB PRODUCT</p>
                                                        </div>
                                                        <div class="prod-label">
                                                            <span class="label-name"><%# Eval("productname") %></span>
                                                            <asp:HiddenField runat="server" ID="hdnVendor" Value='<%# Eval("vendorid") %>' />
                                                            <asp:HiddenField runat="server" ID="hdnProductCode" Value='<%# Eval("productcode") %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-9" style="padding-left: 0;">
                                                    <div class="table-responsive tvlGridHt">
                                                        <asp:UpdatePanel ID="updetails" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView
                                                                    CssClass="p-upgradDataGrid"
                                                                    ID="gvConsumption"
                                                                    runat="server"
                                                                    AutoGenerateColumns="false"
                                                                    AllowPaging="false"
                                                                    Visible="true"
                                                                    GridLines="Both"
                                                                    OnRowDataBound="gvConsumption_RowDataBound"
                                                                    BorderWidth="0"
                                                                    CellSpacing="0"
                                                                    CellPadding="0" ShowHeader="false">

                                                                    <HeaderStyle CssClass="headerGrid" HorizontalAlign="Center" />
                                                                    <RowStyle CssClass="tlrowlight" />
                                                                    <AlternatingRowStyle CssClass="tlrowdark" />
                                                                    <FooterStyle CssClass="footerGrid" HorizontalAlign="Center" />
                                                                    <PagerStyle HorizontalAlign="Center" />

                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="&#x1F4E6; VOLUME">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbVolume" runat="server"
                                                                                    CssClass="lbl-volume"
                                                                                    Text='<%# Bind("total_despatch_production_yield") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="&#x2697;&#xFE0F; RAW MATERIALS">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRawMaterial" runat="server"
                                                                                    Text='<%# Bind("tc_chemical_name") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="% DOSAGE">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDosage" runat="server"
                                                                                    CssClass="lbl-dosage"
                                                                                    Text='<%# Bind("tc_rm_dosage") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="&#x1F4CA; CONSUMPTION">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblConsumption" runat="server"
                                                                                    CssClass="lbl-consumption"
                                                                                    Text='<%# Bind("consumption") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                     
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>--%>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
