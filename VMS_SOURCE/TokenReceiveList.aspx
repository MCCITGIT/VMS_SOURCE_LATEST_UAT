<%@ Page Title="Token Receive List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TokenReceiveList.aspx.vb" Inherits="TokenReceiveList" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

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

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Token Receive List</h3>
                <p class="pageSubTitle">Browse token receipt records</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Factory:</label>
                        <asp:DropDownList ID="ddlFactory" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Site:</label>
                        <asp:DropDownList ID="ddlSite" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Vendor Name:</label>
                        <asp:DropDownList ID="ddlVendor" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 form-btn-mt">
                    <div class="form-group">
                        <%--<asp:ImageButton CssClass="btn btn-primary btn-sm" ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" />
                        <asp:ImageButton CssClass="btn btn-success btn-sm" ImageUrl="~/images/ic_add.gif" ID="imgbtnAdd" runat="server" PostBackUrl="~/TokenReceiveEntry.aspx" />--%>
                        <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-success btn-sm" ID="imgbtnAdd" runat="server" PostBackUrl="~/TokenReceiveEntry.aspx">Add</asp:LinkButton>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTokenDespatchList" runat="server" AutoGenerateColumns="False" EmptyDataText="No record(s) found."
                            EnableModelValidation="True" AllowPaging="true" ShowFooter="false" BorderWidth="1" CssClass="table table-hover upgradDataGrid"
                            OnRowCommand="gvTokenDespatchList_RowCommand" PageSize="10" OnPageIndexChanging="gvTokenDespatchList_PageIndexChanging">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Receive Id" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiveId" runat="server" Text='<%# Bind("trh_id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Site" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactory" runat="server" Text='<%# Bind("depot_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Received By" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Bind("created_user") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Received Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Bind("created_date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewDtls" CommandName="ViewDetails" Width="90%" CssClass="btn btn-primary mr-2" runat="server" ToolTip="Click To View Details" Text="View" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        <asp:PostBackTrigger ControlID="gvTokenDespatchList" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
