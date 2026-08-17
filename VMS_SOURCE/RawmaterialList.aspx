<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RawmaterialList.aspx.vb" Inherits="RawmaterialList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Vendor Raw Material Linking</h3>
                <p class="pageSubTitle">Raw materials linked to each vendor</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="mst-panel-header">
            <div class="mst-panel-header-left">
                <span class="mst-panel-icon"><i class="fas fa-plus"></i></span>
                <div>
                    <h5 class="mst-panel-title">Add Vendor Raw Material</h5>
                    <p class="mst-panel-subtitle">Enter a Vendor Raw Material name to add it to the master list</p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-control-label">Vendor:</label>
                        <asp:DropDownList ID="ddlVendor" ClientIDMode="Static" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 form-btn-mt">
                    <div class="form-group">
                        <asp:LinkButton ID="ImgbtnAdd" runat="server" CssClass="btn btn-success btn-sm" ClientIDMode="Static" OnClick="ImgbtnAdd_Click">Add</asp:LinkButton>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="card">
        <div class="mst-panel-header">
            <div class="mst-panel-header-left">
                <span class="mst-panel-icon"><i class="fas fa-list"></i></span>
                <div>
                    <h5 class="mst-panel-title">Raw Material List</h5>
                    <p class="mst-panel-subtitle">Raw materials linked to the selected vendor</p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView BorderWidth="1" CssClass="table table-hover upgradDataGrid" CellSpacing="0" CellPadding="0"
                    ID="gvRawMatList" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="true"
                    ShowFooter="false" GridLines="both">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor Code" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorCode" runat="server" Text='<%# Bind("vendor_code") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="28%" />
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="28%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>
                                <asp:HiddenField ID="hdnVendorCode" runat="server" Value='<%# Bind("vendor_code") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="28%" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="28%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GST No" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblvendor_gstno" runat="server" Text='<%# Bind("gst_no") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="34%" />
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="34%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblmobile_gstno" runat="server" Text='<%# Bind("mobile_no") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="34%" />
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="34%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblemail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="34%" />
                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Width="34%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div style="display: flex; align-items: center; justify-content: center">
                                    <asp:LinkButton ID="btnView" runat="server" Visible="true" Text="View" CommandName="View" CommandArgument='<%# Container.DataItemIndex %>' ToolTip="View" CssClass="text-primary"><i class="fa fa-eye"></i></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <ControlStyle></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
