<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="VendorRawMaterialLink.aspx.vb" Inherits="VendorRawMaterialLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/VendorRawMaterialLinking.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>

    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                if (document.getElementById('btnSubmit').disabled == true)
                    return false;
                else {
                    // button Add (F7 keypress)
                    validateVendorRawMaterialLinkAdd();
                }
                //__doPostBack(document.getElementById('btnSubmit').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('btnCancel').name, '');
            }
        }

        function disableBackButton() {
            window.history.forward(1);
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

        function resetProductField() {
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

        function allowRateTwoDecimal(evt, control) {
            var charCode = evt.which ? evt.which : evt.keyCode;

            if (charCode === 8 || charCode === 9 || charCode === 13 || charCode === 37 || charCode === 39 || charCode === 46) {
                return true;
            }

            var charValue = String.fromCharCode(charCode);
            if (!/[0-9.]/.test(charValue)) {
                return false;
            }

            var value = control.value || "";
            if (charValue === ".") {
                return value.indexOf(".") === -1;
            }

            var dotIndex = value.indexOf(".");
            if (dotIndex !== -1) {
                var decimals = value.substring(dotIndex + 1);
                var hasSelection = control.selectionStart !== control.selectionEnd;
                if (!hasSelection && control.selectionStart > dotIndex && decimals.length >= 2) {
                    return false;
                }
            }

            return true;
        }

        function sanitizeRateTwoDecimal(control) {
            var value = control.value || "";
            value = value.replace(/[^0-9.]/g, "");

            if (value.indexOf(".") !== -1) {
                var parts = value.split(".");
                value = parts[0] + "." + parts.slice(1).join("");
            }

            var dotIndex = value.indexOf(".");
            if (dotIndex !== -1) {
                var intPart = value.substring(0, dotIndex);
                var decPart = value.substring(dotIndex + 1, dotIndex + 3);
                value = intPart + "." + decPart;
            }

            control.value = value;
        }

        function formatRateTwoDecimal(control) {
            var value = (control.value || "").trim();
            if (value === "") {
                return;
            }

            var numValue = parseFloat(value);
            if (isNaN(numValue)) {
                control.value = "";
                return;
            }

            control.value = numValue.toFixed(2);
        }
    </script>
    <script type="text/javascript">
        function onProductSelected(sender, e) {
            var value = e.get_value();
            var text = e.get_text();
            document.getElementById('<%=hdnVendorCode.ClientID%>').value = value;
            document.getElementById('<%=txtVendorSearch.ClientID%>').value = text + " (" + value + ")";
            sender.get_element().value = text + " (" + value + ")";
        }

        function clearProductSelection() {
            document.getElementById('<%=hdnVendorCode.ClientID%>').value = '';
        }

        function resetProductField() {
            document.getElementById('<%=txtVendorSearch.ClientID%>').value = '';
         document.getElementById('<%=hdnVendorCode.ClientID%>').value = '';
     }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Vendor Raw Material Linking</h3>
                <p class="pageSubTitle">Link raw materials to their vendors</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon"><i class="fas fa-link"></i></span>
                        <div>
                            <h5 class="mst-panel-title">Link Raw Material</h5>
                            <p class="mst-panel-subtitle">Search a raw material and link it to the selected vendor</p>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <%--<div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Vendor:<span id="Span1" class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlVendor" ClientIDMode="Static" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>--%>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Vendor:</label>
                                <div class="input-group product-search-group">
                                    <asp:TextBox ID="txtVendorSearch" ClientIDMode="Static" CssClass="form-control" TabIndex="2" runat="server" AutoComplete="Off" Placeholder="Enter Vendor" onkeyup="clearProductSelection();"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn btn-outline-secondary product-reset-btn" onclick="resetProductField(); return false;" title="Reset SKU"><i class="fas fa-sync-alt fa-xs"></i></button>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnVendorCode" ClientIDMode="Static" runat="server" />
                                <asp:AutoCompleteExtender ID="aceVendorSearch" runat="server"
                                    TargetControlID="txtVendorSearch"
                                    ServiceMethod="VendorSearch"
                                    CompletionInterval="200"
                                    EnableCaching="false"
                                    CompletionSetCount="20"
                                    FirstRowSelected="true"
                                    OnClientItemSelected="onProductSelected">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Search Raw Material:<span id="Span2" class="mandatory">*</span></label>
                                <div class="input-group product-search-group">
                                    <asp:TextBox ID="txtSearchText" ClientIDMode="Static" class="form-control" runat="server" AutoComplete="Off" onkeyup="clearRawMaterialSelection();" Placeholder="Enter Here"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn btn-outline-secondary product-reset-btn" onclick="resetProductField(); return false;" title="Reset SKU"><i class="fas fa-sync-alt fa-xs"></i></button>
                                    </div>
                                </div>
                                <asp:HiddenField ID="txtrawmatid" ClientIDMode="Static" runat="server" />
                                <asp:AutoCompleteExtender ID="aceRawMaterialSearch" runat="server"
                                    TargetControlID="txtSearchText"
                                    ServiceMethod="RawMaterialSearch"
                                    CompletionInterval="200"
                                    EnableCaching="false"
                                    CompletionSetCount="20"
                                    FirstRowSelected="true"
                                    OnClientItemSelected="onRawMaterialSelected"
                                    CompletionListCssClass="vmsAutoComplete"
                                    CompletionListItemCssClass="vmsAutoCompleteItem"
                                    CompletionListHighlightedItemCssClass="vmsAutoCompleteItemHighlight">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-md-2 form-btn-mt">
                            <div class="form-group">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-sm" />
                            </div>
                        </div>
                    </div>

                    <div class="row form-btn-mt">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvVendorRawMat" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" CssClass="table table-hover upgradDataGrid"
                                    EmptyDataText="No records added." GridLines="both">
                                    <RowStyle CssClass="tlrowlight" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vendor Name">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("id") %>' />
                                                <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnVendorCode" runat="server" Value='<%# Bind("vendor_code") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="rawmat_code" HeaderText="Raw Material Code" ReadOnly="true" />
                                        <asp:BoundField DataField="rawmat_name" HeaderText="Raw Material Name" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRate" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("rate") %>' AutoComplete="Off"
                                                    onkeypress="return allowRateTwoDecimal(event, this);"
                                                    oninput="sanitizeRateTwoDecimal(this);"
                                                    onblur="formatRateTwoDecimal(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblactiveText" runat="server" Text='<%# IIf(UCase(Trim(CStr(Eval("active")))) = "Y", "Yes", "No") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlactive" CssClass="form-control form-control-sm" runat="server">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" CommandName="Edit" runat="server" CssClass="text-info mr-1" ToolTip="Edit"
                                                    Visible='<%# (Not String.IsNullOrWhiteSpace(Convert.ToString(Eval("id")))) %>'><i class="fas fa-edit"></i></asp:LinkButton>
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
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" ClientIDMode="Static" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm" />

                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

