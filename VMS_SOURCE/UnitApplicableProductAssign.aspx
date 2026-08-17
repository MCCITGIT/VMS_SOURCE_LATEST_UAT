<%@ Page Title="Unit Applicable Product Assign" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UnitApplicableProductAssign.aspx.vb" Inherits="UnitApplicableProductAssign" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <script type="text/javascript" src="Scripts/ValidateUnitApplicableProductVendorAssign.js?key=<%= DateTime.Now.ToString("dd/MM/yyyy") %>"></script>
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

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <img alt="" src="images/ajax-loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Unit Applicable Product Assign (HO)</h3>
                <p class="pageSubTitle">Assign the products applicable to each unit</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Unit:</label>
                                <asp:DropDownList ID="ddlVendorUnit" CssClass="form-control select2" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Sku Code:</label>
                                <asp:DropDownList ID="ddlVendorProduct" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Active:</label>
                                <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="P">Pending</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group">
                                <%--<asp:ImageButton ImageUrl="images/ic_search.gif" ID="imgbtnSearch" CssClass="btn btn-primary btn-sm" runat="server" />--%>
                                <asp:LinkButton ID="imgbtnSearch" CssClass="btn btn-primary btn-sm" runat="server">Search</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvProductList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" BorderStyle="None" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="Depot" ControlStyle-Width="90%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepot" runat="server" Text='<%# Bind("v_depot") %>'></asp:Label>
                                            </ItemTemplate>

                                            <ControlStyle Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="6%" Height="50px" />
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Unit" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("v_vendor_unit") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSku" runat="server" Text='<%# Bind("sku_new_code")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnSkuCode" Value='<%# Bind("sku_new_code") %>' runat="server" />
                                        <asp:HiddenField ID="hdnUnit" Value='<%# Bind("unit") %>' runat="server" />
                                        <asp:HiddenField ID="hdnActive" Value='<%# Bind("status") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("sku_desc") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>

                                <%--  <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductDesc" runat="server" Text='<%# Bind("sku_desc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Pack size (Kl.)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPackSize" runat="server" Text='<%# Bind("sku_volume") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Denomination" ControlStyle-Width="90%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDenomination" CssClass="form-control" runat="server" Text='<%# Bind("denomination") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Vendor">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnTokenVendor" Value='<%# Bind("tokenVendor") %>' runat="server" />
                                        <asp:DropDownList ID="ddlTokenVendor" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" ControlStyle-Width="100%">
                                    <HeaderTemplate>
                                        <span>Action</span>
                                        <asp:CheckBox ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hdnStatus" Value='<%# Bind("status") %>' />
                                        <asp:CheckBox ID="chkAppl" Checked='<%# If(Eval("status").ToString.Equals("Y"), True, False) %>' OnCheckedChanged="chkAppl_CheckedChanged" AutoPostBack="true" runat="server" />
                                    </ItemTemplate>
                                    <ControlStyle Width="100%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
