<%@ Page Title="Dashboard" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  start chart--%>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <style>
        #chart-container {
            width: 100%;
            max-width: 100%;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Literal ID="litPending" runat="server" Visible="false"></asp:Literal>
            <asp:Literal ID="litDespatch" runat="server" Visible="false">></asp:Literal>

            <div class="row">
                <div class="col-md-12">
                    <div class="flashComplainBTCCard">
                        <div class="newCard w100">
                            <div class="newCardHead">
                                <h3 class="newHeadTitle">Flash News</h3>
                            </div>
                            <div class="newCardBody">
                                <div class="noRecordFnew" id="news_marquee_scroll" runat="server">No new updates at the moment.</div>
                            </div>
                        </div>
                        <a class="complainBTCCard" id="tblComplainRegistrationLink" runat="server" href="https://bpilsharepoint1.bergerindia.com:97" target="_blank" title="For Product Complaint Click Here">
                            <div class="newCard">
                                <i class="fas fa-comment-dots cbtcImg"></i>
                                <p>Complain/BTC</p>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <%--Modified-by MUKESH BHAGAT on 20-08-2026 : restored from old UAT source (Action Required panel and Last Stock Update Date)--%>
            <div class="row">
                <div class="col-md-8">
                    <div class="newCard w100">
                        <div class="newCardHead">
                            <h3 class="newHeadTitle">Action Required</h3>
                        </div>
                        <div class="newCardBody">
                            <div id="tdActionReq" runat="server"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="newCard w100">
                        <div class="newCardHead">
                            <h3 class="newHeadTitle">Stock As On</h3>
                        </div>
                        <div class="newCardBody">
                            <asp:Label ID="lblLastStockUpdateDate" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>



            <div class="row" runat="server" id="divHo">
                <div class="col-md-8">
                    <div class="dbQuikCardList">
                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-box-open quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Total Despatch (Vol)</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblTotalDespatch">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkViewDetails" runat="server" CssClass="quikLink" OnClick="lnkViewDetails_Click">
                                   View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-hourglass-start quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Pending load (Vol)</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblPendingLoad">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkPendingLoadDetails" runat="server" CssClass="quikLink" OnClick="lnkPendingLoadDetails_Click">
                                 View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-vector-square quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Vendor Complaints</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblVendorComplaints">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkVendorComplaintDetails" runat="server" CssClass="quikLink" OnClick="lnkVendorComplaintDetails_Click">
    View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars" style="display: none;">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-campground quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">New Documents</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblNewDoc">0</asp:Label>
                                </div>
                            </div>
                            <a class="quikLink">View Details <i class="fas fa-chevron-right"></i></a>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-folder-minus quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Expired Documents</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblExpDoc">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkExpieredDoc" runat="server" CssClass="quikLink" OnClick="lnkExpieredDoc_Click">
    View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-campground quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Unapproved indent</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblUnapprovedIndent">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkUnapproveIndent" runat="server" CssClass="quikLink" OnClick="lnkUnapproveIndent_Click">
                                  View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-shekel-sign quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Unapproved Despatch Challans</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblUnapprovedDespatch">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkUnapprovedDespatch" runat="server" CssClass="quikLink" OnClick="lnkUnapprovedDespatch_Click">
                                  View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-weight-hanging quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Unapproved Legal/Statutory</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblUnapprovedLegal">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkUnApprovedDoc" runat="server" CssClass="quikLink" OnClick="lnkUnApprovedDoc_Click">
    View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-stopwatch-20 quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Audited Vendor</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblAuditedVendorCount">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkAuditCount" runat="server" CssClass="quikLink" OnClick="lnkAuditCount_Click">
    View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>

                        <div class="loopQuikCars">
                            <div class="quikFungView">
                                <div class="quikImgView">
                                    <i class="fas fa-universal-access quikImg" style="font-size: 25px;"></i>
                                </div>
                                <div class="quikDtls">
                                    <h3 class="quikName">Sample Tested Vendor</h3>
                                    <asp:Label class="quikNo" runat="server" ID="lblSampleTestedVendorCount">0</asp:Label>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkSampleTestedCount" runat="server" CssClass="quikLink" OnClick="lnkSampleTestedCount_Click">
    View Details <i class="fas fa-chevron-right"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="newCard">
                        <div class="newCardHead">
                            <h3 class="newHeadTitle">2025-2026 Despatch and Pending load</h3>
                        </div>
                        <div class="newCardBody">
                            <div id="chart-container">
                                <canvas id="salesChart" style="width: 100% !important; height: 238px !important;"></canvas>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="newCard">
                        <div class="newCardHead">
                            <h3 class="newHeadTitle">Quick Links</h3>
                        </div>
                        <div class="newCardBody">
                            <div class="menu" role="menu" aria-label="Quick menu" id="tblQuickMenu" runat="server"></div>

                            <ul class="qukLinkNav" id="tdQuickLink" runat="server" style="display: none"></ul>
                        </div>
                    </div>
                    <div class="newCard">
                        <div class="newCardHead">
                            <h3 class="newHeadTitle">Top 4 Vendor(TY)</h3>
                        </div>
                        <div class="newCardBody">
                            <div class="table-responsive tvlGridHt">
                                <asp:GridView ID="gvTopvendor" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" CssClass="upgradDataGrid m-0 custGvTopvendorGrid" CellSpacing="0" CellPadding="0">
                                    <RowStyle CssClass="tlrowlight" />
                                    <SelectedRowStyle />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vendor">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_vendor_name" runat="server" Text='<%# Bind("ty_vendor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Position">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_obtain_weightage" runat="server" Text='<%# Bind("ty_vendor_rank") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="newCard">
                        <div class="newCardHead">
                            <h3 class="newHeadTitle">Top 4 Vendor(LY)</h3>
                        </div>
                        <div class="newCardBody">
                            <div class="table-responsive tvlGridHt">
                                <asp:GridView ID="gvTop3Vend" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" CssClass="upgradDataGrid m-0 custGvTopvendorGrid" CellSpacing="0" CellPadding="0">
                                    <RowStyle CssClass="tlrowlight" />
                                    <SelectedRowStyle />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vendor">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_vendor_name" runat="server" Text='<%# Bind("ly_vendor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Position">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_obtain_weightage" runat="server" Text='<%# Bind("ly_vendor_rank") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="newCard">
                        <div class="newCardHead">
                            <h3 class="newHeadTitle">Vendor Level Wsie Despatch</h3>
                        </div>
                        <div class="newCardBody">
                            <div class="table-responsive tvlGridHt">
                                <asp:GridView ID="gvVendorDespatch" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" CssClass="upgradDataGrid m-0 custGvTopvendorGrid" CellSpacing="0" CellPadding="0">
                                    <RowStyle CssClass="tlrowlight" />
                                    <SelectedRowStyle />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vendor Level">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_vendor_lvl" runat="server" Text='<%# Bind("vld_level") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Despatch Vol">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_despatch_vol" runat="server" Text='<%# Bind("vld_vol") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" runat="server" id="divUnit">
            </div>
            <div class="row" runat="server" id="divDepot">
            </div>


            <%--<div class="row">
                <div class="col-md-12">
                    <div class="newCard">
                        <div class="newCardBody">
                            <div class="lastDateView">
                                <h3>
                                    <asp:Label ID="lblDayNumber" runat="server"></asp:Label>
                                </h3>
                                <p>
                                    <asp:Label ID="lblMonthName" runat="server"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server"></asp:Label>
                                    <asp:Label ID="lblDayName" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>

            <%--            <map name="Map" id="CEO_desk_Map" style="display: none;">
                <area href="Score_Card.aspx" id="imgScoreCard" runat="server" visible="false" shape="RECT" coords="291,1,376,25" alt="CEO ScoreCard" />
                <area href="CEO_desk.aspx" id="imgDashBoard" runat="server" visible="false" shape="RECT" coords="391,1,476,25" alt="CEO DashBoard" />
            </map>--%>

            <%--Start Total Despatch (Vol)--%>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:ModalPopupExtender ID="mp1" runat="server"
                PopupControlID="Panel3" TargetControlID="HiddenField1">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel3" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="modalPanel bootstrapModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Details</h5>
                            <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>--%>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                        <asp:GridView ID="gvDtls" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid">
                                            <RowStyle CssClass="tlrowlight" />
                                            <SelectedRowStyle />
                                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <FooterStyle CssClass="footerGrid" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Unit Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" Text='<%# Bind("vendor") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="60%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Depot Despatch">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepotDespatch" Text='<%# Bind("depot_vol") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Direct Despatch">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDirectDespatch" Text='<%# Bind("direct_vol") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="20%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnmp1ClosePopup" OnClick="btnmp1ClosePopup_Click" runat="server" CssClass="btn btn-secondary" Text="Close" />

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnmp1ClosePopup" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <%--End Total Despatch (Vol)--%>


            <%--Start Pending Load--%>
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:ModalPopupExtender ID="mpPendingLoad" runat="server"
                PopupControlID="pnlPendingLoad" TargetControlID="HiddenField2">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlPendingLoad" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="modalPanel bootstrapModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Pending Load - Level 1</h5>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                        <asp:GridView ID="gvPendingLoad" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid">
                                            <RowStyle CssClass="tlrowlight" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Vendor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVendor" runat="server" Text='<%# Eval("vendor") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="60%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pending Load">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPendingLoad" runat="server" Text='<%# Eval("pending_load", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" Width="30%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnClosePendingLoad" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnClosePendingLoad_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnClosePendingLoad" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <%--End Pending Load--%>


            <asp:HiddenField ID="HiddenField_Complaints" runat="server" />
            <asp:ModalPopupExtender ID="mpComplaints" runat="server"
                PopupControlID="Panel_Complaints" TargetControlID="HiddenField_Complaints">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel_Complaints" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="modalPanel bootstrapModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Monthly Complaints Summary</h5>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="divComplaintscount">
                                        <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                            <asp:GridView ID="gvComplaints" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid" OnRowCommand="gvComplaints_RowCommand">
                                                <RowStyle CssClass="tlrowlight" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Vendor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendor" Text='<%# Eval("vendor") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of Complaints">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblComplaints" Text='<%# Eval("noOfcomplaints") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnView" runat="server" CommandName="ViewComplaints" CommandArgument='<%# Eval("unit_code") %>'
                                                                Text="View" CssClass="btn btn-info btn-sm tableBtnXs" />
                                                        </ItemTemplate>
                                                        <ControlStyle></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>

                                    <div id="panel_Comaplaints_details" runat="server">
                                        <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">

                                            <asp:GridView ID="gvCoplaintsDtl" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid" OnRowCommand="gvComplaints_RowCommand">
                                                <RowStyle CssClass="tlrowlight" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SKU">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSKu" Text='<%# Eval("sku") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complaint ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcomID" Text='<%# Eval("vc_complaints_id") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complaint Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcomdate" Text='<%# Eval("complaints_date") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complaint Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcomdate" Text='<%# Eval("vc_remarks") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complaint Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcomdate" Text='<%# Eval("vc_status") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <%--<div class="row form-btn-mt">
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnComplaintBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm" OnClick="btnComplaintBack_Click" />
                                            </div>
                                        </div>--%>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnCloseComplaints" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnCloseComplaints_Click" />
                                    <asp:Button ID="btnComplaintBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm" OnClick="btnComplaintBack_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCloseComplaints" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnComplaintBack" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:HiddenField ID="HiddenField_LegalDocs" runat="server" />
            <asp:ModalPopupExtender ID="mpLegalDocs" runat="server"
                PopupControlID="Panel_LegalDocs" TargetControlID="HiddenField_LegalDocs">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel_LegalDocs" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="modalPanel bootstrapModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Legal Documents (Expired & To be Expire in Next 10 Days)</h5>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="divExpireDoc">
                                        <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                            <asp:GridView ID="gvLegalDocs" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid"
                                                OnRowCommand="gvLegalDocs_RowCommand">
                                                <RowStyle CssClass="tlrowlight" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Vendor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendor" Text='<%# Eval("vendor") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocCount" Text='<%# Eval("expire_doc_count") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnView" runat="server" CommandName="ViewExpireDoc" CommandArgument='<%# Eval("unit_code") %>'
                                                                Text="View" CssClass="btn btn-info btn-sm tableBtnXs" />
                                                        </ItemTemplate>
                                                        <ControlStyle></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                    <div id="divExpiredocdtls" runat="server">
                                        <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                            <asp:GridView ID="gvExpiredocDtl" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid">
                                                <RowStyle CssClass="tlrowlight" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Parameter Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblParam" Text='<%# Eval("vlm_param_name") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valid From">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvalidFrom" Text='<%# Eval("validfrom") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Valid Till">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValidTill" Text='<%# Eval("validtill") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <%-- <div class="row form-btn-mt">
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnLegalExpireBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm" OnClick="btnLegalExpireBack_Click" />
                                            </div>
                                        </div>--%>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnCloseLegalDocs" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnCloseLegalDocs_Click" />
                                    <asp:Button ID="btnLegalExpireBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm" OnClick="btnLegalExpireBack_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCloseLegalDocs" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:HiddenField ID="HiddenUnApproved" runat="server" />
            <asp:ModalPopupExtender ID="mpUnApprovedDoc" runat="server"
                PopupControlID="PanelUnApprobvedDoc" TargetControlID="HiddenUnApproved">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelUnApprobvedDoc" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="modalPanel bootstrapModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Unapproved Legal/Statutory Documents</h5>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="divLegalApprove">
                                        <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                            <asp:GridView ID="gvUnApprovedDoc" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid" OnRowCommand="gvUnApprovedDoc_RowCommand">
                                                <RowStyle CssClass="tlrowlight" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Vendor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendor" Text='<%# Eval("vendor") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocCount" Text='<%# Eval("unapproved_legal_doc_count") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnView" runat="server" CommandName="ViewLegalApprove" CommandArgument='<%# Eval("unit_code") %>'
                                                                Text="View" CssClass="btn btn-info btn-sm tableBtnXs" />
                                                        </ItemTemplate>
                                                        <ControlStyle></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>

                                    <div id="divLegalApproveDtl" runat="server">
                                        <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                            <asp:GridView ID="gvLegalApproveDtl" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid">
                                                <RowStyle CssClass="tlrowlight" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Parameter Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllegalApproveParam" Text='<%# Eval("vlm_param_name") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblegalStatus" Text='<%# Eval("status") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <%--<div class="row form-btn-mt">
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnLegalApproveBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm" OnClick="btnLegalApproveBack_Click" />
                                            </div>
                                        </div>--%>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnUnApprovedDoc" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnUnApprovedDoc_Click" />
                                    <asp:Button ID="btnLegalApproveBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm" OnClick="btnLegalApproveBack_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnUnApprovedDoc" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:HiddenField ID="HiddenAuditCount" runat="server" />
            <asp:ModalPopupExtender ID="mpAuditCount" runat="server"
                PopupControlID="PanelAuditCount" TargetControlID="HiddenAuditCount">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelAuditCount" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="modalPanel bootstrapModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Audited Vendor Count</h5>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                        <asp:GridView ID="gvAuditCount" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid">
                                            <RowStyle CssClass="tlrowlight" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Unit Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAuditUnitCode" Text='<%# Eval("unit_code") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vendor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVendor" Text='<%# Eval("vendor") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="90%" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnAuditCount" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnAuditCount_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAuditCount" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:HiddenField ID="HiddenSampleTestedCount" runat="server" />
            <asp:ModalPopupExtender ID="mpSampleTestedCount" runat="server"
                PopupControlID="PanelSampleTestedCount" TargetControlID="HiddenSampleTestedCount">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PanelSampleTestedCount" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="modalPanel bootstrapModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Sample Tested Vendor Count</h5>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" style="overflow-y: auto; max-height: 300px;">
                                        <asp:GridView ID="gvSampleCount" runat="server" AutoGenerateColumns="false" CssClass="upgradDataGrid">
                                            <RowStyle CssClass="tlrowlight" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Unit Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitCode" Text='<%# Eval("unit_code") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vendor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVendor" Text='<%# Eval("vendor") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="90%" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSampleTestedClose" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnSampleTestedClose_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSampleTestedClose" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <script>
                const ctx = document.getElementById('salesChart').getContext('2d');
                const pendingData = [<%= litPending.Text %>];
                const despatchData = [<%= litDespatch.Text %>];
                const salesChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ["Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar"],
                        datasets: [
                          {
                              label: "Pending Loads",
                              //data: [300, 310, 305, 320, 295, 285, 275, 280, 290, 300, 310, 305],
                              data: pendingData,
                              borderColor: "red",
                              backgroundColor: "rgba(255,0,0,0.1)",
                              tension: 0.3,
                              pointRadius: 4,
                              pointBackgroundColor: "red"
                          },
                          {
                              label: "Total Despatch",
                              //data: [310, 320, 330, 310, 300, 305, 295, 300, 315, 325, 335, 345],
                              data: despatchData,
                              borderColor: "blue",
                              backgroundColor: "rgba(0,0,255,0.1)",
                              tension: 0.3,
                              pointRadius: 4,
                              pointBackgroundColor: "blue"
                          }
                        ]
                    },
                    options: {
                        responsive: true,
                        //plugins: {
                        //    title: {
                        //        display: true,
                        //        text: "2025-2026 Despatch and Pending load",
                        //        font: {
                        //            size: 18
                        //        }
                        //    },
                        //    legend: {
                        //        position: "top"
                        //    }
                        //},
                        scales: {
                            y: {
                                beginAtZero: false,
                                //min: 260,
                                //max: 360,
                                ticks: {
                                    stepSize: 10
                                },
                                title: {
                                    display: true,
                                    text: "Loads (Volume)"
                                }
                            },
                            x: {
                                title: {
                                    display: true,
                                    text: "Months"
                                }
                            }
                        }
                    }
                });
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
