<%@ Page Title="Token Vendor Requisition List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TokenRequisitionList.aspx.vb" Inherits="TokenRequisitionList" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <script type="text/javascript" src="Scripts/ValidateUnitApplicableVendorAssign.js?key=<%= DateTime.Now.ToString %>"></script>

    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                __doPostBack(document.getElementById('imgbtnAdd').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('imgbtnSearch').name, '');
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
            <h3 class="pageTitle">Token Requisition List</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Unit Name:</label>
                        <asp:DropDownList ID="ddlVendorUnit" CssClass="form-control select2" runat="server" AutoPostBack="True"></asp:DropDownList>
                        <asp:Label runat="server" ID="lblTokenVendor"></asp:Label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Vendor Name:</label>
                        <asp:DropDownList ID="ddlTokenVendor" CssClass="form-control select2" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Requisition Id:</label>
                        <asp:DropDownList ID="ddlVendorRequisition" CssClass="form-control select2" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Status:</label>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server" AutoPostBack="false"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12 text-center">
                    <div class="form-group">
                        <%--<asp:ImageButton CssClass="btn btn-primary btn-sm" ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" />
                        <asp:ImageButton CssClass="btn btn-success btn-sm" ImageUrl="~/images/ic_add.gif" ID="imgbtnAdd" runat="server" />--%>
                        <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-success btn-sm" ID="imgbtnAdd" runat="server" OnClick="imgbtnAdd_Click">Add</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvRequistionList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" BorderStyle="None" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Requisition Id" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequistionId" Text='<%# Bind("trh_id") %>' runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Name" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" Text='<%# Bind("trh_unit") %>' runat="server" />
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor Name" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendor" Text='<%# Bind("trh_token_vendor")%>' runat="server" />
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site Name" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiteName" Text='<%# Bind("trh_site_name") %>' runat="server" />
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" Text='<%# Bind("trh_desc") %>' runat="server" />
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of items" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoOfitems" Text='<%# Bind("items") %>' runat="server" />
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Qty." ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalQty" Text='<%# Bind("totalQty") %>' runat="server" />
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# Bind("trh_status") %>' runat="server" />
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ControlStyle-Width="100%">
                                    <HeaderTemplate>
                                        <span>View</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgBtnSubmit" CommandArgument='<%# Bind("trh_id") %>' CommandName="EditRequisition" ToolTip="View" runat="server"><i class="fa fa-eye"></i></asp:LinkButton>
                                        <%--<asp:ImageButton ID="imgBtnSubmit" ImageUrl="~/images/ic_view.gif" CommandArgument='<%# Bind("trh_id") %>' CommandName="EditRequisition" Style="width: 25%" ToolTip="View" runat="server" />--%>
                                    </ItemTemplate>
                                    <ControlStyle Width="100%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ControlStyle-Width="100%">
                                    <HeaderTemplate>
                                        <span>Reject</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnStatus" Value='<%# Bind("trh_status") %>' />
                                        <asp:LinkButton ID="imgBtnReject" CommandArgument='<%# Bind("trh_id") %>' CommandName="RejectRequisition" ToolTip="Reject" runat="server"><i class="fa fa-times-circle" style="color:#FF0000;"></i></asp:LinkButton>
                                        <%--<asp:ImageButton ID="imgBtnReject" ImageUrl="~/images/ic_delete.gif" CommandArgument='<%# Bind("trh_id") %>' CommandName="RejectRequisition" Style="width: 25%" ToolTip="Reject" runat="server" />--%>
                                    </ItemTemplate>

                                    <ControlStyle Width="100%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        <asp:PostBackTrigger ControlID="gvRequistionList" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
