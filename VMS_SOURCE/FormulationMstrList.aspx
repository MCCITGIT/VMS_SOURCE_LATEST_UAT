<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FormulationMstrList.aspx.vb" Inherits="FormulationMstrList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function onProductSelected(sender, e) {
            var value = e.get_value();
            var text = e.get_text();
            document.getElementById('<%=hdnProductCode.ClientID%>').value = value;
            document.getElementById('<%=txtProductSearch.ClientID%>').value = text + " (" + value + ")";
            sender.get_element().value = text + " (" + value + ")";
        }

        function clearProductSelection() {
            document.getElementById('<%=hdnProductCode.ClientID%>').value = '';
        }

        function resetProductField() {
            document.getElementById('<%=txtProductSearch.ClientID%>').value = '';
            document.getElementById('<%=hdnProductCode.ClientID%>').value = '';
        }
    </script>
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Product Formulation</h3>
                <p class="pageSubTitle">Browse product formulation records</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon"><i class="fas fa-search"></i></span>
                        <div>
                            <h5 class="mst-panel-title">Search Brand</h5>
                            <p class="mst-panel-subtitle">Search a brand name to show it to the master list</p>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Brand:</label>
                                <asp:DropDownList ID="ddlBrand" ClientIDMode="Static" CssClass="form-control select2" TabIndex="1" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" visible="false">
                            <div class="form-group">
                                <label class="form-control-label">Raw Material:</label>
                                <asp:DropDownList ID="ddlRawMat" ClientIDMode="Static" CssClass="form-control select2" TabIndex="4" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Product:</label>
                                <div class="input-group product-search-group">
                                    <asp:TextBox ID="txtProductSearch" ClientIDMode="Static" CssClass="form-control" TabIndex="2" runat="server" AutoComplete="Off" Placeholder="Enter Product" onkeyup="clearProductSelection();"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn btn-outline-secondary product-reset-btn" onclick="resetProductField(); return false;" title="Reset SKU"><i class="fas fa-sync-alt fa-xs"></i></button>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnProductCode" ClientIDMode="Static" runat="server" />
                                <asp:AutoCompleteExtender ID="aceProductSearch" runat="server"
                                    TargetControlID="txtProductSearch"
                                    ServiceMethod="ProductSearch"
                                    CompletionInterval="200"
                                    EnableCaching="false"
                                    CompletionSetCount="20"
                                    FirstRowSelected="true"
                                    OnClientItemSelected="onProductSelected"
                                    CompletionListCssClass="vmsAutoComplete"
                                    CompletionListItemCssClass="vmsAutoCompleteItem"
                                    CompletionListHighlightedItemCssClass="vmsAutoCompleteItemHighlight">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-md-2 form-btn-mt">
                            <div class="form-group">
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" ClientIDMode="Static">Search</asp:LinkButton>
                                <asp:LinkButton ID="ImgbtnAdd" runat="server" CssClass="btn btn-success btn-sm" ClientIDMode="Static">Add</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card" runat="server" id="tr1">
                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon"><i class="fas fa-list"></i></span>
                        <div>
                            <h5 class="mst-panel-title">Brand List</h5>
                            <p class="mst-panel-subtitle">All brands currently available for product mapping</p>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive" style="overflow-y: auto; max-height: calc(100vh - 290px);">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvFormulationList" runat="server" AutoGenerateColumns="False" BorderWidth="1"
                                    CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found">
                                    <RowStyle CssClass="tlrowlight" />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Srl No" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrl" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbrand" Text='<%# Bind("Brand_Name") %>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnbrandcode" Value='<%# Bind("Brand_Code") %>' />
                                                <asp:HiddenField runat="server" ID="hdnid" Value='<%# Bind("fh_id") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Raw Material" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblraw" Text='<%# Bind("Raw_Mat_Name")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnRawCode" Value='<%# Bind("Raw_Mat_Code") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Sku" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSku" Text='<%# Bind("Sku_Desc")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnSkucode" Value='<%# Bind("Sku_Code") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="6%" />
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
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                                <asp:PostBackTrigger ControlID="gvFormulationList" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

