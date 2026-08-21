<%@ Page Title="Unit Applicable Vendor Assign" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UnitApplicableVendorAssign.aspx.vb" Inherits="UnitApplicableVendorAssign" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateUnitApplicableVendorAssign.js?key="&<%= DateTime.Now.ToString %> ></script>

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
                <h3 class="pageTitle">Unit Applicable Vendor Assign (HO)</h3>
                <p class="pageSubTitle">Browse and manage user profiles</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="form-control-label">Unit:<span id="Span1" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlVendorUnit" CssClass="form-control select2" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="form-control-label">Sku Code:</label>
                        <asp:DropDownList ID="ddlVendorProduct" runat="server" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <label class="form-control-label">Active:</label>
                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control select2">
                            <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 form-btn-mt">
                    <div class="form-group">
                        <asp:ImageButton ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click" CssClass="btn btn-primary btn-sm" />
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
                        <div class="table-responsive">
                            <asp:GridView ID="gvTokenVendorList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="true" PageSize="20" CssClass="table table-hover upgradDataGrid">
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
                                    <asp:TemplateField HeaderText="Product">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSku" runat="server" Text='<%# Bind("sku_new_code")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnskuCode" Value='<%# Bind("sku_new_code")%>' runat="server" />
                                            <asp:HiddenField ID="hdnUnit" Value='<%# Bind("unit") %>' runat="server" />
                                            <asp:HiddenField ID="hdnActive" Value='<%# Bind("status") %>' runat="server" />

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("sku_prd_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                    </asp:TemplateField>

                                    <%--      <asp:TemplateField HeaderText="Description">
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

                                    <asp:TemplateField HeaderText="Unit" ControlStyle-Width="90%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("v_vendor_unit") %>'></asp:Label>
                                        </ItemTemplate>

                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" ControlStyle-Width="90%">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnTokenVendor" Value='<%# Bind("tokenVendor") %>' runat="server" />
                                            <asp:DropDownList ID="ddlTokenVendor" runat="server"></asp:DropDownList>
                                        </ItemTemplate>

                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ControlStyle-Width="100%">
                                        <HeaderTemplate>
                                            <span>Action</span>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnSubmit" ImageUrl="~/images/b_save.gif" CommandName="AssignUnitVendor" Style="width: 65%" ToolTip="Save" runat="server" />
                                        </ItemTemplate>

                                        <ControlStyle Width="100%"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        <asp:PostBackTrigger ControlID="gvTokenVendorList" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
