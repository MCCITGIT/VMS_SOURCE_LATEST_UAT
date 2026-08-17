<%@ Page Title="Maximum Despatch Limit Set Up" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="MaxDespatchLimit.aspx.vb" Inherits="MaxDespatchLimit" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="Scripts/ValidateMaxDespLimit.js" type="text/javascript"></script>
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward(1);
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Maximum Despatch Limit</h3>
                <p class="pageSubTitle">Set maximum despatch limits per vendor</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvDespDtl" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                    Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" EmptyDataText="No Record Found">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitCode" runat="server" Text='<%# Bind("mdl_unit") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Limit">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLimit" runat="server" Text='<%# Bind("mdl_limit") %>' CssClass="form-control" Style="text-align: right;"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" PostBackUrl="~/Home.aspx" />
                </div>
            </div>
            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div id="divErrorMessage"></div>
        </div>
    </div>
</asp:Content>
