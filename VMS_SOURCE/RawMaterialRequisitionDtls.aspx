<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RawMaterialRequisitionDtls.aspx.vb" Inherits="RawMaterialRequisitionDtls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateRawMaterialRequisitionDtls.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {
                if (document.getElementById('btnSubmit').disabled == true)
                    return false;
                else if (validateRawMaterialRequisitionSubmit()) {
                    document.getElementById('btnSubmit').disabled = true;
                    __doPostBack(document.getElementById('btnSubmit').name, '');
                }
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('btnCancel').name, '');
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
                <h3 class="pageTitle">Raw Material Requisition Details</h3>
                <p class="pageSubTitle">Line details of the raw material requisition</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="mst-panel-header">
                    <div class="mst-panel-header-left">
                        <span class="mst-panel-icon"><i class="fas fa-plus"></i></span>
                        <div>
                            <h5 class="mst-panel-title">Add Requisition</h5>
                            <p class="mst-panel-subtitle">Select an RM vendor and enter raw material requisition details</p>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Vendor Name:</label>
                                <asp:TextBox ID="txtreqVendor" ClientIDMode="Static" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">RM Vendor:<span id="Span" class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlVendor" ClientIDMode="Static" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 form-btn-mt">
                            <div class="form-group">
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" ClientIDMode="Static" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
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
                                                <asp:HiddenField ID="hdnRate" runat="server" Value='<%# Bind("rate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="rawmat_code" HeaderText="Raw Material Code" ReadOnly="true" />
                                        <asp:BoundField DataField="rawmat_name" HeaderText="Raw Material Name" ReadOnly="true" />                                        
                                        <asp:TemplateField HeaderText="Quantity Required">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control form-control-sm" AutoComplete="Off"
                                                    onkeypress="return allowRateTwoDecimal(event, this);"
                                                    oninput="sanitizeRateTwoDecimal(this);"
                                                    onblur="formatRateTwoDecimal(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Required Delivery Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReqDate" runat="server" CssClass="form-control form-control-sm" TextMode="Date" AutoComplete="Off"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm" AutoComplete="Off"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" ClientIDMode="Static" Visible="false"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning btn-sm" />
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

