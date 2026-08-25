<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RmpDashboard.aspx.vb" Inherits="RmpDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="includes/rmp-dashboard.css?v=1.0.0.<%= DateTime.Now.Ticks.ToString() %>" rel="stylesheet" type="text/css" />

    <div class="rmpDashboardWrap">

        <%--<!-- ================= PAGE HEADER ================= -->--%>
        <div class="breadcrumbs">
            <div class="leftFung">
                <a href="Home.aspx" title="Home"><i class="fa fa-home"></i></a>
                <span class="diveider">/</span>
                <div class="pageTitleWrap">
                    <p class="pageTitle">RM Purchase Dashboard</p>
                    <p class="pageSubTitle">Raw material procurement &amp; production usage overview</p>
                </div>
            </div>
        </div>

        <%-- ================= SEARCH FILTERS ================= --%>
        <div class="newCard rmp-filter-card">
            <div class="card-body">
                <div class="rmp-filter-stats-row">
                    <div class="rmp-filter-fields">
                        <div class="row">
                            <div id="divVendor" class="col-md-3" runat="server">
                                <div class="form-group">
                                    <label class="form-control-label">Vendor:</label>
                                    <%--<asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control">
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlvendor" ClientIDMode="Static" CssClass="form-control select2" TabIndex="1" runat="server"></asp:DropDownList>

                                </div>
                            </div>
                            <div id="divDuration" class="col-md-2" runat="server">
                                <div class="form-group">
                                    <label class="form-control-label">Duration:</label>
                                    <asp:DropDownList ID="ddlDurationType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDurationType_SelectedIndexChanged">
                                        <asp:ListItem Text="Select Duration" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Monthly" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Yearly" Value="Y"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divYear" class="col-md-2" runat="server">
                                <div class="form-group">
                                    <label class="form-control-label">Year:</label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="monthDiv" runat="server">
                                <div class="form-group">
                                    <label class="form-control-label">Month:</label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="rmp-filter-actions">
                                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm rmp-btn-icon" ToolTip="Search" OnClick="btnSearch_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--<!-- ================= CHARTS ================= -->--%>
        <div id="divCharts" class="rmp-chart-row" runat="server" visible="false">

            <%--<!-- Purchased vs Used bar chart -->--%>
            <asp:HiddenField ID="hdnRmChartData" runat="server" />
            <div class="newCard">
                <div class="rmp-panel-head">
                    <div>
                        <h4 class="newHeadTitle">Quantity Purchased vs Used</h4>
                        <p class="rmp-panel-sub">Raw material purchased vs used</p>
                    </div>
                    <div class="rmp-legend">
                        <div class="rmp-legend-item"><span class="rmp-legend-dot" style="background: #1b5a8c"></span>Purchased</div>
                        <div class="rmp-legend-item"><span class="rmp-legend-dot" style="background: #188038"></span>Used</div>
                    </div>
                </div>

                <div class="rmp-bars-wrap">
                    <div class="rmp-bars-scroll" id="rmpBarsScroll">
                        <div class="rmp-bars-inner" id="rmpBarsInner">
                            <!-- Dynamic bars -->
                            <div class="rmp-bars" id="rmpBars">
                            </div>

                            <!-- Dynamic labels -->
                            <div class="rmp-bar-labels" id="rmpBarLabels">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <%--<!-- ================= RM VENDORS TABLE ================= -->--%>
        <div class="table-responsive rm-grid-scroll">
            <asp:GridView
                ID="gvRmPendingList"
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
                    <asp:TemplateField HeaderText="SR. NO.">
                        <ItemTemplate>
                            <asp:Label ID="lblSrl" runat="server"
                                Text='<%# (gvRmPendingList.PageIndex * gvRmPendingList.PageSize) + Container.DataItemIndex + 1 %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="REQUEST ID">
                        <ItemTemplate>
                            <span class="request-id-wrap">
                                <i class="fas fa-file-alt"></i>
                                <asp:Label ID="lblReqId" runat="server" CssClass="request-id-text" Text='<%# Eval("req_id") %>'></asp:Label>
                            </span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="16%" />
                        <ItemStyle HorizontalAlign="Left" Width="16%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VENDOR">
                        <ItemTemplate>
                            <div class="vendor-cell">
                                <div>
                                    <div class="vendor-name">
                                        <asp:Label ID="lblVendor" runat="server" Text='<%# Eval("RawMaterialVendorCode") %>'></asp:Label>
                                    </div>
                                    <div class="vendor-sub">
                                        Vendor
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="42%" />
                        <ItemStyle HorizontalAlign="Left" Width="42%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <span class='<%# GetStatusCss(Eval("Status").ToString()) %>'>
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left" Width="18%" />
                        <ItemStyle HorizontalAlign="Left" Width="18%" />

                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>
    <script type="text/javascript">

        function loadRmChart() {

            var hiddenField =
                document.getElementById('<%= hdnRmChartData.ClientID %>');

            if (!hiddenField)
                return;

            var json = hiddenField.value;

            if (!json)
                return;

            var data;

            try {
                data = JSON.parse(json);
            }
            catch (e) {
                console.error("Invalid RM chart JSON", e);
                return;
            }

            renderRmChart(data);
        }


        var RMP_CHART_BAR_AREA = 150;


        function getRmChartLayout() {

            var root =
                document.querySelector(".rmpDashboardWrap");

            var styles = root
                ? window.getComputedStyle(root)
                : null;

            var groupWidth = 76;
            var groupGap = 25;

            if (styles) {
                groupWidth =
                    parseFloat(styles.getPropertyValue("--rmp-bar-group-width")) || groupWidth;

                groupGap =
                    parseFloat(styles.getPropertyValue("--rmp-bar-group-gap")) || groupGap;
            }

            return {
                groupWidth: groupWidth,
                groupGap: groupGap
            };
        }


        function updateRmChartScrollWidth(itemCount) {

            var barsInner =
                document.getElementById("rmpBarsInner");

            if (!barsInner || itemCount <= 0) {
                if (barsInner) {
                    barsInner.style.minWidth = "";
                }
                return;
            }

            var layout = getRmChartLayout();
            var totalWidth =
                (itemCount * layout.groupWidth) +
                (Math.max(0, itemCount - 1) * layout.groupGap);

            barsInner.style.minWidth = totalWidth + "px";
        }


        function renderRmChart(data) {

            var barsContainer =
                document.getElementById("rmpBars");

            var labelsContainer =
                document.getElementById("rmpBarLabels");


            barsContainer.innerHTML = "";
            labelsContainer.innerHTML = "";


            if (!data || data.length === 0) {

                updateRmChartScrollWidth(0);

                barsContainer.innerHTML =
                    "<div class='rmp-no-data'>No data available</div>";

                return;
            }


            //-----------------------------------------
            // Find maximum quantity
            //-----------------------------------------

            var maxValue = 0;

            data.forEach(function (item) {

                var purchased =
                    parseFloat(item.PurchasedQty) || 0;

                var used =
                    parseFloat(item.UsedQty) || 0;


                if (purchased > maxValue)
                    maxValue = purchased;

                if (used > maxValue)
                    maxValue = used;

            });


            if (maxValue <= 0)
                maxValue = 1;


            updateRmChartScrollWidth(data.length);


            //-----------------------------------------
            // Create bars
            //-----------------------------------------

            data.forEach(function (item) {

                var purchasedQty =
                    parseFloat(item.PurchasedQty) || 0;

                var usedQty =
                    parseFloat(item.UsedQty) || 0;


                var purchasedHeight =
                    Math.round((purchasedQty / maxValue) * RMP_CHART_BAR_AREA);

                var usedHeight =
                    Math.round((usedQty / maxValue) * RMP_CHART_BAR_AREA);

                if (purchasedQty > 0 && purchasedHeight < 3)
                    purchasedHeight = 3;

                if (usedQty > 0 && usedHeight < 3)
                    usedHeight = 3;


                //---------------------------------
                // Bar Group
                //---------------------------------

                var barGroup =
                    document.createElement("div");

                barGroup.className =
                    "rmp-bar-group";


                //---------------------------------
                // Purchased
                //---------------------------------

                var purchasedWrapper =
                    document.createElement("div");

                purchasedWrapper.className =
                    "rmp-bar-item";


                var purchasedValue =
                    document.createElement("span");

                purchasedValue.className =
                    "rmp-bar-value";

                purchasedValue.innerText =
                    formatQuantity(purchasedQty);


                var purchasedBar =
                    document.createElement("div");

                purchasedBar.className =
                    "rmp-bar purchased";

                purchasedBar.style.height =
                    purchasedHeight + "px";

                purchasedBar.title =
                    "Purchased: " +
                    purchasedQty.toLocaleString();


                purchasedWrapper.appendChild(
                    purchasedValue
                );

                purchasedWrapper.appendChild(
                    purchasedBar
                );


                //---------------------------------
                // Used
                //---------------------------------

                var usedWrapper =
                    document.createElement("div");

                usedWrapper.className =
                    "rmp-bar-item";


                var usedValue =
                    document.createElement("span");

                usedValue.className =
                    "rmp-bar-value";

                usedValue.innerText =
                    formatQuantity(usedQty);


                var usedBar =
                    document.createElement("div");

                usedBar.className =
                    "rmp-bar used";

                usedBar.style.height =
                    usedHeight + "px";

                usedBar.title =
                    "Used: " +
                    usedQty.toLocaleString();


                usedWrapper.appendChild(
                    usedValue
                );

                usedWrapper.appendChild(
                    usedBar
                );


                //---------------------------------
                // Add both bars
                //---------------------------------

                barGroup.appendChild(
                    purchasedWrapper
                );

                barGroup.appendChild(
                    usedWrapper
                );

                barsContainer.appendChild(
                    barGroup
                );


                //---------------------------------
                // Raw material label
                //---------------------------------

                var label =
                    document.createElement("span");

                label.innerText =
                    item.RawMaterialCode;

                label.title =
                    item.RawMaterialCode;

                labelsContainer.appendChild(
                    label
                );

            });

        }


        function formatQuantity(value) {

            if (value >= 1000000)
                return (value / 1000000).toFixed(1) + "M";

            if (value >= 1000)
                return (value / 1000).toFixed(1) + "K";

            return value.toFixed(0);
        }


        function initRmChart() {
            loadRmChart();
        }

        document.addEventListener("DOMContentLoaded", initRmChart);

        if (typeof Sys !== "undefined" &&
            Sys.WebForms &&
            Sys.WebForms.PageRequestManager) {

            Sys.WebForms.PageRequestManager
                .getInstance()
                .add_endRequest(initRmChart);
        }

    </script>

</asp:Content>
