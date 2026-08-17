<%@ Page Title="Brand Test Linking" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="VendorStockList.aspx.vb" Inherits="VendorStockList" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="Scripts/ValidateVendorUnitMaster.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {  // button Add (F7 keypress)	    		    
                __doPostBack(document.getElementById('ImgbtnAdd').name, '');
            }
            else if (event.keyCode == 119) { // button Search (F8 keypress)

                __doPostBack(document.getElementById('ImgbtnSearch').name, '');
            }
        }
        //-->
    </script>
    <script type="text/javascript">var cal1 = new CalendarPopup(); function scheme_effective_date_onclick() { }</script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Brand Test Linking</h3>
                <p class="pageSubTitle">Browse vendor stock positions</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Vendor:</label>
                                <asp:DropDownList ID="ddlVendor" CssClass="form-control select2" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Batch From Date:</label>
                                <asp:TextBox ID="txtAsOndate" runat="server" CssClass="form-control" MaxLength="10" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">To Date:</label>
                                <asp:TextBox ID="txtAsOndateTo" runat="server" CssClass="form-control" MaxLength="10" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4 form-btn-mt">
                            <asp:Button ID="ImgbtnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm" />
                            <asp:Button ID="imgbtnExport" runat="server" Text="Export" CssClass="btn btn-info btn-sm" />
                            <asp:Button ID="ImgbtnAdd" PostBackUrl="~/VendorStockEntry.aspx" runat="server" Text="Add" CssClass="btn btn-success btn-sm" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvVendorStockDetails" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
                            Visible="true" ShowFooter="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbrandid" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="As On Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("ason_date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Volume" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVolume" runat="server" Text='<%# Bind("volume") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="upnlBtn" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="btnExport" CommandName="EditRow" Visible="true" runat="server" Text="Export" title="Export" ToolTip="Click To Export" CommandArgument='<%# Bind("vendor_code")%>'></asp:LinkButton>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnExport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="card" id="Div_Vendor_Stock_List_Grid" runat="server" visible="false">
                <div class="card-body">
                    <table border="1" class="table table-hover upgradDataGrid">
                        <tr class="headerGrid">
                            <th style="width: 5%; text-align: center;">Sl.No.</th>
                            <th style="width: 60%; text-align: center;">Vendor Name</th>
                            <th style="width: 15%; text-align: center;">As On Date</th>
                            <th style="width: 10%; text-align: center;">Volume</th>
                            <th style="width: 10%; text-align: center;">Action</th>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-weight: bold;" colspan="5">No Records Found</td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
