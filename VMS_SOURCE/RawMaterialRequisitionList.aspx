<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RawMaterialRequisitionList.aspx.vb" Inherits="RawMaterialRequisitionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateRawMaterialRequisitionDtls.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {
                __doPostBack(document.getElementById('ImgbtnAdd').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('btnCancel').name, '');
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Raw Material Requisition List</h3>
                <p class="pageSubTitle">Track raw material requisitions</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanelFilter" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon"><i class="fas fa-search"></i></span>
                        <div>
                            <h5 class="mst-panel-title">Search Requisition</h5>
                            <p class="mst-panel-subtitle">Filter raw material requisitions by vendor and status</p>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group pb-0">
                                <label class="form-control-label">Vendor:</label>
                                <asp:DropDownList ID="ddlvendor" ClientIDMode="Static" CssClass="form-control select2" TabIndex="1" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group pb-0">
                                <label class="form-control-label">Raw Material Vendor:</label>
                                <asp:DropDownList ID="ddlRawMatvendor" ClientIDMode="Static" CssClass="form-control select2" TabIndex="2" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group pb-0">
                                <label class="form-control-label">Approval Status:</label>
                                <asp:DropDownList ID="ddlApprovalstatus" ClientIDMode="Static" CssClass="form-control select2" TabIndex="3" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group pb-0">
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" ClientIDMode="Static" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>
                                <asp:LinkButton ID="ImgbtnAdd" runat="server" CssClass="btn btn-success btn-sm" ClientIDMode="Static" OnClick="ImgbtnAdd_Click">Add</asp:LinkButton>
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning btn-sm" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="card">
        <div class="mst-panel-header">
            <div class="mst-panel-header-left">
                <span class="mst-panel-icon"><i class="fas fa-list"></i></span>
                <div>
                    <h5 class="mst-panel-title">Requisition List</h5>
                    <p class="mst-panel-subtitle">All raw material requisitions and their approval status</p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive" style="overflow-y: auto; max-height: calc(100vh - 290px);">
                <asp:UpdatePanel ID="UpdatePanelGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView BorderWidth="1" CssClass="table table-hover upgradDataGrid" CellSpacing="0" CellPadding="0"
                            ID="gvRequisition" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" AllowPaging="false" Visible="true"
                            ShowFooter="false" GridLines="both" EmptyDataText="No records found"
                            OnRowCommand="gvRequisition_RowCommand" OnRowDataBound="gvRequisition_RowDataBound">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSlNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnVendorCode" runat="server" Value='<%# Bind("vendor_code") %>' />
                                        <asp:HiddenField ID="hdnRequestId" runat="server" Value='<%# Bind("request_id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="16%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="16%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RM Vendor Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRawMatVendorName" runat="server" Text='<%# Bind("rawmat_vendor_name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnrmVendorcode" runat="server" Value='<%# Bind("rawmat_vendor_code") %>' />
                                        <asp:HiddenField ID="hdnrmVendoremail" runat="server" Value='<%# Bind("rawmat_vendor_email") %>' />
                                        <asp:HiddenField ID="hdnccemail" runat="server" Value='<%# Bind("CCAddress") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="16%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="16%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Raw Material List">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRawmaterialList" runat="server" Text='<%# Bind("RawmaterialList") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="36%" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="36%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approval Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Bind("approval_status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="display: flex; align-items: center; justify-content: center">
                                            <asp:LinkButton ID="btnView" runat="server" Visible="true" Text="View" CommandName="ViewRequisition" CommandArgument='<%# Container.DataItemIndex %>' ToolTip="View" CssClass="text-primary"><i class="fa fa-eye"></i></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                        <asp:PostBackTrigger ControlID="gvRequisition" />
                        <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success btn-sm"
                        OnClientClick="return validateRawMaterialRequisitionApprove();"
                        CausesValidation="false" UseSubmitBehavior="true" />
                    <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
