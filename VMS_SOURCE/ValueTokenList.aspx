<%@ Page Title="Token Requisition List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ValueTokenList.aspx.vb" Inherits="ValueTokenList" %>


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
                <h3 class="pageTitle">Token Requisition List</h3>
                <p class="pageSubTitle">Track value token requisitions</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Factory:</label>
                        <asp:DropDownList ID="ddlFactory" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                        <asp:Label runat="server" ID="lblTokenVendor"></asp:Label>
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
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Req. Id:</label>
                        <asp:TextBox ID="txtRequisitionId" CssClass="form-control" runat="server" onkeypress="return isNumber(event);" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Month:</label>
                        <asp:DropDownList ID="ddlMonth" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Year:</label>
                        <asp:DropDownList ID="ddlYear" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 form-btn-mt">
                    <div class="form-group">
                        <%--<asp:ImageButton CssClass="btn btn-primary btn-sm" ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" />
                        <asp:ImageButton CssClass="btn btn-success btn-sm" ImageUrl="~/images/ic_add.gif" ID="imgbtnAdd" runat="server" PostBackUrl="~/ValueTokenEntry.aspx" />--%>
                        <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server">Search</asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-success btn-sm" ID="imgbtnAdd" runat="server" PostBackUrl="~/ValueTokenEntry.aspx">Add</asp:LinkButton>
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
                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" EmptyDataText="No record(s) found."
                            EnableModelValidation="True" AllowPaging="true" ShowFooter="False" BorderWidth="1" CssClass="table table-hover upgradDataGrid" OnRowCommand="gvList_RowCommand" PageSize="10" OnPageIndexChanging="gvList_PageIndexChanging">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Requisition Id" HeaderStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrl" runat="server" Text='<%# Bind("ts_session_id") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnfactory" runat="server" Value='<%# Bind("ts_factory_code") %>'></asp:HiddenField>
                                        <asp:HiddenField ID="hdnsite" runat="server" Value='<%# Bind("site_code")%>'></asp:HiddenField>
                                        <asp:HiddenField ID="hdnVendorCode" runat="server" Value='<%# Bind("ts_vendor_code") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Factory" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFactory" runat="server" Text='<%# Bind("depot_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Site" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSite" runat="server" Text='<%# Bind("site_name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("vm_vendor_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Month" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Bind("ts_session_year") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Token Generated" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBTokenGeneratedYN" runat="server" Text='<%# Bind("token_generated_desc") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Token Requisition Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBTokenRequisitionDt" runat="server" Text='<%# Bind("token_requisition_date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Token Generated Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBTokenGeneratedDt" runat="server" Text='<%# Bind("token_generated_date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditRow" CommandName="EditRow" Width="90%" BackColor="#99CCFF" ForeColor="Black" Font-Bold="true" runat="server" ToolTip="Click To Edit" Text="Edit" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        <asp:PostBackTrigger ControlID="gvList" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
