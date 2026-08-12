<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FormulationMaster.aspx.vb" Inherits="FormulationMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .product-search-group {
            display: flex;
            align-items: stretch;
        }

        .product-search-group .form-control {
            border-top-right-radius: 0;
            border-bottom-right-radius: 0;
        }

        .product-search-group .product-reset-btn {
            border-top-left-radius: 0;
            border-bottom-left-radius: 0;
            min-width: 36px;
            display: flex;
            align-items: center;
            justify-content: center;
        }
    </style>
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateFormulationMstr.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
    <%-- <script type="text/javascript">
        function checkAllShade(checkbox) {
            var cbl = document.getElementById('<%=chkbxListApplShade.ClientID%>').getElementsByTagName("input");
            for (i = 0; i < cbl.length; i++) cbl[i].checked = checkbox.checked;
        }
    </script>--%>
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
            var value = e.get_value();
            var text = e.get_text();
            document.getElementById('<%=hdnProductCode.ClientID%>').value = value;
            document.getElementById('<%=txtProductSearch.ClientID%>').value = text + " (" + value + ")";
            sender.get_element().value = text + " (" + value + ")";
            __doPostBack('<%=btnLoadShade.UniqueID%>', '');
        }

        function clearProductSelection() {
            document.getElementById('<%=hdnProductCode.ClientID%>').value = '';
        }

        function resetProductField() {
            document.getElementById('<%=txtProductSearch.ClientID%>').value = '';
            document.getElementById('<%=hdnProductCode.ClientID%>').value = '';
            __doPostBack('<%=btnLoadShade.UniqueID%>', '');
        }

    </script>
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Product Formulation Master</h3>
        </div>
        <div class="rightFung"></div>
    </div>
    <div class="card">
        <div class="card-body" >
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Brand:<span id="Span1" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlBrand" ClientIDMode="Static" CssClass="form-control select2" TabIndex="1" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="hdnId" runat="server"/>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Raw Material:<span id="Span4" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlRawMat" ClientIDMode="Static" CssClass="form-control select2" TabIndex="4" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Product:<span id="Span2" class="mandatory">*</span></label>
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
                            OnClientItemSelected="onProductSelected">
                        </asp:AutoCompleteExtender>
                        <asp:LinkButton ID="btnLoadShade" runat="server" Style="display: none;"></asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="card" style="margin: 0 auto; width: 65%;">
        <div class="card-body" >
            <div class="table-responsive">
                <asp:GridView BorderWidth="1" CssClass="table table-hover upgradDataGrid" CellSpacing="0" CellPadding="0"
                    ID="gdShadedtls" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="true"
                    ShowFooter="true" GridLines="both">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblbrandid" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblshadename" runat="server" Text='<%# Bind("Shade_Desc") %>'></asp:Label>
                                <asp:HiddenField ID="hdnShadecode" runat="server" Value='<%# Bind("Shade_Code") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalText" runat="server" Text="Total"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40%" />
                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consumption Ratio" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtratio" runat="server" class="form-control" AutoComplete="Off" Placeholder="Enter Here" onkeypress="return allowOnlyIntegerKey(event);" oninput="sanitizeIntegerInput(this); updateConsumptionRatioTotal();" onchange="updateConsumptionRatioTotal();"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblRatioTotal" runat="server" ClientIDMode="Static" Text="0.00%"></asp:Label>
                                <br />
                                <asp:Label ID="lblRatioStatus" runat="server" ClientIDMode="Static" Text="Within 100%"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit of Measurement" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtmeasurement" runat="server" class="form-control" AutoComplete="Off" Placeholder="Enter Here" onkeypress="return allowOnlyTextKey(event);" oninput="sanitizeTextInput(this);"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%"/>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-12 mt-3">
                <div class="form-group text-center">
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server" ClientIDMode="Static"></asp:Label>
                    <div id="divErrorMessage"></div>
                    <asp:Button ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                    <asp:Button ID="btnCancel" ClientIDMode="Static" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                    <asp:Button ID="btnReset" ClientIDMode="Static" CssClass="btn btn-danger btn-sm" runat="server" Text="Reset" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

