<%@ Page Title="Load Master Unmapped Vendor-SKU" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Load_Mstr_Unmapped_Vendor_SKU.aspx.vb" Inherits="Load_Mstr_Unmapped_Vendor_SKU" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Depot Indents List (HO)</h3>
                <p class="pageSubTitle">Vendor SKUs not yet mapped to a load master</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="gvNotFound" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnRowDataBound="gvNotFound_RowDataBound"
                            BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Bind("luvs_year") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("luvs_month") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depot">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepot" runat="server" Text='<%# Bind("tmp_depot") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnDepotCode" runat="server" Value='<%# Bind("luvs_depot") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SKU">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSKU" runat="server" Text='<%# Bind("luvs_sku") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Average">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAvg" runat="server" Text='<%# Bind("luvs_avg") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Average">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEst" runat="server" Text='<%# Bind("luvs_est") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlVendor" CssClass="select2" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRemarks"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                HeaderText="Estimated NOP" DataField="tmp_est" ControlStyle-Width="10%">
                                                                <ControlStyle Width="10%"></ControlStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" Text="Save" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
