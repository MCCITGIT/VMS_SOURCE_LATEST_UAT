<%@ Page Title="Complain Details" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="vrs_complaint_score.aspx.vb" Inherits="vrs_complaint_score" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidateUnitApplicableVendorAssign.js?key=<%= DateTime.Now.ToString %>"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Complaint Score Details</h3>
                <p class="pageSubTitle">Complaint scores contributing to vendor rating</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                    <ContentTemplate>
                                        <label class="form-control-label">Quarter:</label>
                                        <asp:DropDownList ID="ddlQuarter" class="form-control select2" runat="server" AutoPostBack="true" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                    <ContentTemplate>
                                        <label class="form-control-label">Vendor:</label>
                                        <asp:DropDownList ID="ddlVendor" class="form-control select2" runat="server" AutoPostBack="true" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                <ContentTemplate>
                                    <asp:Button ID="btnSearch" runat="server" ToolTip="Click to Search" Text="Search" CssClass="btn btn-success btn-sm" />&nbsp;
                                   
                                    <asp:Button ID="btnReset" runat="server" ToolTip="Click to Reset" Text="Reset" CssClass="btn btn-warning btn-sm" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card" runat="server" id="div2" visible="false">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">
                                        <asp:GridView ID="gvComplaintDetails" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid" OnRowCommand="gvComplaintDetails_RowCommand">
                                            <RowStyle CssClass="tlrowlight" />
                                            <SelectedRowStyle />
                                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <FooterStyle CssClass="footerGrid" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Total Complaints">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalComp" Text='<%# Bind("vcd_total_complaints") %>' runat="server" />
                                                        <asp:HiddenField runat="server" ID="hdnVendorId" Value='<%# Bind("vch_vendor_id") %>' />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Justified Complaints">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJustComp" Text='<%# Bind("vcd_total_justified_complaints") %>' runat="server" />
                                                        <asp:HiddenField runat="server" ID="hdnHeaderId" Value='<%# Bind("vch_header_id") %>' />
                                                        <asp:HiddenField runat="server" ID="hdnDtlsId" Value='<%# Bind("vcd_vch_dtls_id") %>' />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Max Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalMaxScr" Text='<%# Bind("vch_total_max_score") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Obtain Score">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblObtScr" Text='<%# Bind("vch_total_obtain_score") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Complaint Tendency Ratio">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTendRatio" Text='<%# Bind("vch_complaint_tendency_ratio") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnView" runat="server" CommandName="ViewDetails" CommandArgument='<%# Eval("vch_vendor_id") & "|" & Eval("vch_quarter_id") %>'
                                                            Text="View" CssClass="btn btn-info btn-sm tableBtnXs" />
                                                    </ItemTemplate>
                                                    <ControlStyle></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="text-center mt-2">
                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" />&nbsp;&nbsp;  --%>
                                        <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="gvComplaintDetails" />
                                    <%--<asp:PostBackTrigger ControlID="btnSubmit" />--%>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                </Triggers>
                            </asp:UpdatePanel>

                            <asp:Panel ID="pnlVendorDetails" runat="server" CssClass="modal-popup" Visible="False">
                                <div class="modal-content-custom">
                                    <div class="modal-header-custom">
                                        <h5>Vendor Complaint Details</h5>
                                        <asp:Button ID="btnClosePopup" runat="server" Text="×" CssClass="close-btn" OnClick="btnClosePopup_Click" />
                                    </div>
                                    <div class="modal-body-custom">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvVendorDetails" runat="server" AutoGenerateColumns="True" CssClass="table table-hover upgradDataGrid m-0 gvVendorDtlsGrid" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
