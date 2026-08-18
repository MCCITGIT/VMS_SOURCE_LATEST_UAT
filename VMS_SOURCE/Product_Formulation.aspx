<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_Formulation.aspx.vb" Inherits="Product_Formulation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateFormulationMstr.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {  // button Add (F7 keypress)
                if (document.getElementById('btnSubmit').disabled == false)
                    ValidateUPAControls();
                else
                    return false;
            }
            else if (event.keyCode == 119) { // button Search (F8 keypress)    		    	        
                __doPostBack(document.getElementById('btnCancel').name, '');
            }
            else if (event.keyCode == 120) { // button Reset (F9 keypress)
                __doPostBack(document.getElementById('btnReset').name, '');
            }
        }

        function onProductSelected(sender, e) {
            //debugger;
            var value = e.get_value();
            var text = e.get_text();

            // value = productCode|sku_code
            var values = value.split('|');

            var productCode = values[0];
            var skuCode = values[1];

            document.getElementById('<%=hdnProductCode.ClientID%>').value = productCode;
            document.getElementById('<%=txtProductSearch.ClientID%>').value = text + " (" + productCode + ")";
            document.getElementById('<%=hdnProductName.ClientID%>').value = text + " (" + productCode + ")";
            document.getElementById('<%=hdnSkucode.ClientID%>').value = skuCode
            // Disable Product textbox after selection
            <%--document.getElementById('<%=txtProductSearch.ClientID%>').readOnly = true;--%>


            sender.get_element().value = text + " (" + productCode + ")";
            //__doPostBack('<%=btnLoadShade.UniqueID%>', '');
            // Trigger ASP.NET TextChanged event
            __doPostBack('<%= txtProductSearch.UniqueID %>', '');

        }

        function clearProductSelection() {
            document.getElementById('<%=hdnProductCode.ClientID%>').value = '';
        }

        function resetProductField() {
            document.getElementById('<%=txtProductSearch.ClientID%>').value = '';
            document.getElementById('<%=hdnProductCode.ClientID%>').value = '';
            __doPostBack('<%=btnLoadShade.UniqueID%>', '');
        }
        function onRawMaterialSelected(sender, e) {
            var value = e.get_value();
            var text = e.get_text();
            document.getElementById('<%=txtrawmatid.ClientID%>').value = value;
            document.getElementById('<%=txtSearchText.ClientID%>').value = text + " (" + value + ")";
            sender.get_element().value = text + " (" + value + ")";
        }

        function clearRawMaterialSelection() {
            document.getElementById('<%=txtrawmatid.ClientID%>').value = '';
        }
        function resetRawMaterialField() {
            var rawMatText = document.getElementById('<%=txtSearchText.ClientID%>');
            var rawMatCode = document.getElementById('<%=txtrawmatid.ClientID%>');

            if (rawMatText) {
                rawMatText.value = '';
            }
            if (rawMatCode) {
                rawMatCode.value = '';
            }

            return false;
        }
        function validateRatioInput(input) {
            let value = input.value;

            // Allow only numbers and decimal point
            value = value.replace(/[^0-9.]/g, '');

            // Allow only one decimal point
            let parts = value.split('.');
            if (parts.length > 2) {
                value = parts[0] + '.' + parts[1];
            }

            // Allow maximum 2 digits after decimal
            if (parts.length === 2) {
                value = parts[0] + '.' + parts[1].substring(0, 2);
            }

            input.value = value;
        }
    </script>
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home">
                <i class="fas fa-home"></i>
            </a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Product Formulation Master</h3>
                <p class="pageSubTitle">Define the raw material formulation of a product</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="mst-panel-header">
            <div class="mst-panel-header-left">
                <span class="mst-panel-icon"><i class="fas fa-flask"></i></span>
                <div>
                    <h5 class="mst-panel-title">Add Formulation</h5>
                    <p class="mst-panel-subtitle">Select a product and raw material to define the consumption ratio</p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-control-label">Brand:<span id="Span1" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlBrand" ClientIDMode="Static" CssClass="form-control select2" TabIndex="1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList>
                        <asp:HiddenField ID="hdnId" runat="server" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-control-label">Product:<span class="mandatory">*</span></label>
                        <div class="input-group product-search-group">
                            <asp:TextBox ID="txtProductSearch" ClientIDMode="Static" CssClass="form-control" TabIndex="2" runat="server" AutoComplete="Off" Placeholder="Enter Product" onkeyup="clearProductSelection();" OnTextChanged="txtProductSearch_TextChanged" AutoPostBack="true">
                            </asp:TextBox>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-outline-secondary product-reset-btn" onclick="resetProductField(); return false;" title="Reset Product">
                                    <i class="fas fa-sync-alt fa-xs"></i>
                                </button>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnProductCode" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hdnProductName" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hdnSkucode" runat="server" ClientIDMode="Static" />
                        <asp:AutoCompleteExtender ID="aceProductSearch" runat="server" TargetControlID="txtProductSearch" ServiceMethod="ProductSearch" CompletionInterval="200" EnableCaching="false" CompletionSetCount="20" FirstRowSelected="true" OnClientItemSelected="onProductSelected"
                            CompletionListCssClass="vmsAutoComplete" CompletionListItemCssClass="vmsAutoCompleteItem" CompletionListHighlightedItemCssClass="vmsAutoCompleteItemHighlight">
                        </asp:AutoCompleteExtender>
                        <asp:LinkButton ID="btnLoadShade" runat="server" Style="display: none;"></asp:LinkButton>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-control-label">Search Raw Material:<span class="mandatory">*</span></label>
                        <div class="input-group product-search-group">
                            <asp:TextBox ID="txtSearchText" ClientIDMode="Static" CssClass="form-control" runat="server" AutoComplete="Off" onkeyup="clearRawMaterialSelection();"
                                Placeholder="Enter Raw Material">
                            </asp:TextBox>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-outline-secondary product-reset-btn" onclick="resetRawMaterialField(); return false;" title="Reset Raw Material">
                                    <i class="fas fa-sync-alt fa-xs"></i>
                                </button>
                            </div>
                        </div>
                        <asp:HiddenField ID="txtrawmatid" ClientIDMode="Static" runat="server" />
                        <asp:AutoCompleteExtender ID="aceRawMaterialSearch" runat="server" TargetControlID="txtSearchText" ServiceMethod="RawMaterialSearch" CompletionInterval="200"
                            EnableCaching="false" CompletionSetCount="20" FirstRowSelected="true" OnClientItemSelected="onRawMaterialSelected"
                            CompletionListCssClass="vmsAutoComplete" CompletionListItemCssClass="vmsAutoCompleteItem" CompletionListHighlightedItemCssClass="vmsAutoCompleteItemHighlight">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-control-label">Consumption Ratio:<span class="mandatory">*</span> </label>
                        <asp:TextBox ID="txtRatio" ClientIDMode="Static" CssClass="form-control" runat="server" AutoComplete="Off" Placeholder="Enter Consumption Ratio"
                            oninput="validateRatioInput(this);">
                        </asp:TextBox>
                    </div>
                </div>
                <!-- Unit of Measurement -->
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-control-label">Unit of Measurement:<span class="mandatory">*</span> </label>
                        <asp:TextBox ID="txtmeasurement" ClientIDMode="Static" CssClass="form-control" runat="server" AutoComplete="Off" Placeholder="Enter Unit of Measurement">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3" runat="server" visible="false">
                    <label class="form-control-label">Recipe:<span id="Span111" class="mandatory">*</span></label>
                    <asp:DropDownList ID="ddlRecipe" ClientIDMode="Static" CssClass="form-control select2" TabIndex="1" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRecipe_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1 form-btn-mt">
                    <div class="form-group">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-sm" OnClick="btnAdd_Click" />
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
                    <h5 class="mst-panel-title">Formulation List</h5>
                    <p class="mst-panel-subtitle">Raw materials and their consumption ratios for the selected product</p>
                </div>
            </div>
        </div>

        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvVendorRawMat" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" CssClass="table table-hover upgradDataGrid"
                    EmptyDataText="No records added." GridLines="both" ShowFooter="true">
                    <RowStyle CssClass="tlrowlight" />
                    <HeaderStyle CssClass="headerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate>
                                <%--<asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("id") %>' />--%>
                                <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("brand_name") %>'></asp:Label>
                                <asp:HiddenField ID="hdnBrandCode" runat="server" Value='<%# Bind("brand_code") %>' />
                                <asp:HiddenField ID="hdnProductCode" runat="server" Value='<%# Bind("product_code") %>' />
                                <asp:HiddenField ID="hdnRawMatCode" runat="server" Value='<%# Bind("rawmat_code") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="product_name" HeaderText="Product Name" ReadOnly="true" />

                        <asp:TemplateField HeaderText="Raw Material Name">
                            <ItemTemplate>
                                <asp:Label ID="lblRawMatName" runat="server" Text='<%# Bind("rawmat_name") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalText" runat="server" Text="Total"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consumption Ratio" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRatio" runat="server" Text='<%# Bind("ratio") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblRatioTotal" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblRatioStatus" runat="server" ClientIDMode="Static" Text="Within 100%"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit of Measurement" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("unit") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDeleteRow" runat="server" CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' CssClass="text-danger" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this row?');"><i class="fas fa-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" CommandName="Update" CssClass="text-success mr-1" runat="server" ToolTip="Update" OnClientClick="return confirm('Are you sure you want to update this record?');"><i class="fas fa-check"></i></asp:LinkButton>
                                <asp:LinkButton ID="btncancel" CommandName="Cancel" CssClass="text-danger" runat="server" ToolTip="Cancel"><i class="fas fa-times"></i></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" ClientIDMode="Static" Visible="false"
                        OnClientClick="return confirm('Are you sure you want to submit this record?');" />
                    <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm" OnClick="btnCancel_Click1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

