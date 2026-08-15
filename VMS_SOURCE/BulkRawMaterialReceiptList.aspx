<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BulkRawMaterialReceiptList.aspx.vb" Inherits="BulkRawMaterialReceiptList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {
                __doPostBack(document.getElementById('imgbtnSearch').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('btnReset').name, '');
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
            <h3 class="pageTitle">Bulk Raw Material Receipt List</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanelFilter" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;" Text=""></asp:Label>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group pb-0">
                                <label class="form-control-label">Status:</label>
                                <asp:DropDownList ID="ddlStatus" ClientIDMode="Static" CssClass="form-control select2" TabIndex="1" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group pb-0">
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" ClientIDMode="Static" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive" style="overflow-y: auto; max-height: calc(100vh - 290px);">
                <asp:UpdatePanel ID="UpdatePanelGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView BorderWidth="1" CssClass="table table-hover upgradDataGrid" CellSpacing="0" CellPadding="0"
                            ID="gvReceipt" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" AllowPaging="false" Visible="true"
                            ShowFooter="false" GridLines="both" EmptyDataText="No records found"
                            OnRowCommand="gvReceipt_RowCommand">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSlNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Request Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestId" runat="server" Text='<%# Bind("requisition_id") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Despatch Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDespatchId" runat="server" Text='<%# Bind("despatch_id") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnDespatchId" runat="server" Value='<%# Bind("despatch_id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Despatch Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDespatchDate" runat="server" Text='<%# Bind("despatch_date", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnRequisitionId" runat="server" Value='<%# Bind("requisition_id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Courier Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCourierId" runat="server" Text='<%# Bind("courier_id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>                                

                                <asp:TemplateField HeaderText="Request Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestQty" runat="server" Text='<%# Bind("request_qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Despatch Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDespatchQty" runat="server" Text='<%# Bind("despatch_qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Receive ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceivedId" runat="server" Text='<%# Bind("received_id") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnReceivedId" runat="server" Value='<%# Bind("received_id") %>' /> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Receive Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiveQty" runat="server" Text='<%# Bind("received_qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pending Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingQty" runat="server" Text='<%# Bind("pending_qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Invoice No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblinvno" runat="server" Text='<%# Bind("invoice_no") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Invoice Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblinvdate" runat="server" Text='<%# Bind("invoice_date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="12%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div style="display: flex; align-items: center; justify-content: center">
                                            <asp:LinkButton ID="btnView" runat="server" Visible="true" Text="View" CommandName="ViewReceipt"
                                                CommandArgument='<%# Container.DataItemIndex %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        <asp:PostBackTrigger ControlID="gvReceipt" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
