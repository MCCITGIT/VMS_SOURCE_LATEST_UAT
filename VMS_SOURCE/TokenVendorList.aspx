<%@ Page Title="Unit Applicable Product Assign" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TokenVendorList.aspx.vb" Inherits="TokenVendorList" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <script type="text/javascript" src="Scripts/ValidationIndentList_HO.js"></script>
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
            <h3 class="pageTitle">Token Vendor List</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-3" style="display: none">
                    <div class="form-group">
                        <label class="form-control-label">Unit:<span id="Span1" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlVendorUnit" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Partial Search:</label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Active:</label>
                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 form-btn-mt">
                    <div class="form-group">
                        <%--<asp:ImageButton ImageUrl="images/ic_search.gif" ID="imgbtnSearch" CssClass="btn btn-primary btn-sm" runat="server" />
                        <asp:ImageButton ImageUrl="~/images/ic_add.gif" ID="imgbtnAdd" CssClass="btn btn-success btn-sm" runat="server" />--%>
                        <asp:LinkButton ID="imgbtnSearch" CssClass="btn btn-primary btn-sm" runat="server">Search</asp:LinkButton>
                        <asp:LinkButton ID="imgbtnAdd" CssClass="btn btn-success btn-sm" runat="server" OnClick="imgbtnAdd_Click">Add</asp:LinkButton>
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
                        <asp:GridView ID="gvVendorList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" BorderStyle="None" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>

                                <asp:TemplateField HeaderText="Vendor Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvendorcode" runat="server" Text='<%# Bind("tvm_code") %>'></asp:Label>
                                        <%--<asp:HiddenField ID="hdnProductId" Value='<%# Bind("productId") %>' runat="server" />
                                                <asp:HiddenField ID="hdnUnit" Value='<%# Bind("unit") %>' runat="server" />
                                                <asp:HiddenField ID="hdnPackSize" Value='<%# Bind("packsize") %>' runat="server" />
                                                <asp:HiddenField ID="hdnActive" Value='<%# Bind("status") %>' runat="server" />--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("tvm_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("tvm_email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("tvm_mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Address" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbladdress" runat="server" Text='<%# Bind("tvm_address") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("tvm_city") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%# Bind("tvm_state") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActive" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                                    </ItemTemplate>

                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
