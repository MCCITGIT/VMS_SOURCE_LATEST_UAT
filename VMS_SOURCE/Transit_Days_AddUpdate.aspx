<%@ Page Title="Transit Days" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Transit_Days_AddUpdate.aspx.vb" Inherits="Transit_Days_AddUpdate" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <script type="text/javascript" src="Scripts/ValidationTransitDays.js"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                document.getElementById('btnSubmit').click();

                //__doPostBack(document.getElementById('btnSubmit').name, '');
            }
            else if (event.keyCode == 119) {
                document.getElementById('imgbtnSearch').click();
                //            __doPostBack(document.getElementById('imgbtnSearch').name, '');
            }
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Transit Days</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Vendor:<span id="Span1" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 form-btn-mt">
                    <div class="form-group">
                        <asp:LinkButton ID="imgbtnSearch" runat="server" CssClass="btn btn-primary btn-sm" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>
                        <asp:LinkButton ID="imgbtnPrint" runat="server" CssClass="btn btn-warning btn-sm" OnClick="imgbtnPrint_Click">Print</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive gvTransitDayGridMaxh">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTransitDays" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Srl No." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNo" runat="server" Width="94%" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("t_region") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depot Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepotCode" runat="server" Text='<%# Bind("t_depot") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depot Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepotName" runat="server" Text='<%# Bind("trans_depot") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Days" ControlStyle-Width="90%" ControlStyle-Height="90%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDays" runat="server" MaxLength="2" CssClass="form-control" Text='<%# Bind("t_transit_days") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlUnit" EventName="SelectedIndexChanged" />
                </Triggers>--%>
                </asp:UpdatePanel>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblNoRecFnd" CssClass="errormsg" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    <asp:Label ID="lblErrMsg" CssClass="errormsg" runat="server"></asp:Label>
                    <div class="form-group text-center">
                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-success btn-sm">Submit</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-secondary btn-sm">Cancel</asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
