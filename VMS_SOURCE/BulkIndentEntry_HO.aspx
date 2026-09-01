<%@ Page Title="Bulk Indent Entry (HO)" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BulkIndentEntry_HO.aspx.vb" Inherits="BulkIndentEntry_HO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidationBulkIndentEntry_HO.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {
                var btnSubmit = document.getElementById('btnSubmit');
                if (!btnSubmit || btnSubmit.disabled == true)
                    return false;
                else if (typeof validateBulkIndentSubmit === "function" && validateBulkIndentSubmit()) {
                    btnSubmit.click();
                }
            }
            else if (event.keyCode == 119) {
                var btnCancel = document.getElementById('btnCancel');
                if (btnCancel) {
                    btnCancel.click();
                }
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }
        window.onload = disableBackButton;
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Bulk Indent Entry (HO)</h3>
                <p class="pageSubTitle">Upload indent entries in bulk for head office processing</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="mst-panel-header">
            <div class="mst-panel-header-left">
                <span class="mst-panel-icon"><i class="fas fa-file-upload"></i></span>
                <div>
                    <h5 class="mst-panel-title">Upload File</h5>
                    <p class="mst-panel-subtitle">[F7 = Submit] [F8 = Cancel]</p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="form-control-label">Choose File:<span class="mandatory">*</span></label>
                        <asp:FileUpload runat="server" ID="fupUploadFile" ClientIDMode="Static" CssClass="form-control" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="form-control-label d-block">&nbsp;</label>
                        <a href="Templates/Bulk_Indent_Upload_Template.xls" class="btn btn-info btn-sm">
                            <i class="fas fa-download"></i> Download Format
                        </a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button runat="server" ID="btnSubmit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateBulkIndentSubmit();" UseSubmitBehavior="true" CssClass="btn btn-primary btn-sm" Text="Submit" />
                    <asp:Button runat="server" ID="btnCancel" ClientIDMode="Static" OnClick="btnCancel_Click" UseSubmitBehavior="true" CssClass="btn btn-secondary btn-sm" Text="Cancel" />
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 text-center">
                    <asp:Label runat="server" ID="lblMsg" ClientIDMode="Static" CssClass="errormsg" Text=""></asp:Label>
                    <asp:LinkButton runat="server" ID="lbtnDwnloadFile" OnClick="lbtnDwnloadFile_Click" Visible="false" CssClass="btn btn-link btn-sm">
                        <i class="fas fa-file-excel"></i> Download Error File
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="card mt-3">
        <div class="mst-panel-header">
            <div class="mst-panel-header-left">
                <span class="mst-panel-icon"><i class="fas fa-list"></i></span>
                <div>
                    <h5 class="mst-panel-title">Indent Preview</h5>
                    <p class="mst-panel-subtitle">Review uploaded indent records before confirmation</p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvIndentSKUList" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found" GridLines="both">
                    <RowStyle CssClass="tlrowlight" />
                    <HeaderStyle CssClass="headerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depot Code">
                            <ItemTemplate>
                                <asp:Label ID="lblDepotCode" runat="server" Text='<%# Bind("depot_name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="16%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="16%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SKU Code">
                            <ItemTemplate>
                                <asp:Label ID="lblSkuCode" runat="server" Text='<%# Bind("sku_name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="16%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent NOP">
                            <ItemTemplate>
                                <asp:Label ID="lblIndentNop" runat="server" Text='<%# Bind("indent_nop") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="lblReason" runat="server" Text='<%# Bind("reason") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="16%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row mt-3">
                <div class="col-md-12 text-center">
                    <asp:Button runat="server" ID="btnConfirm" ClientIDMode="Static" OnClick="btnConfirm_Click" OnClientClick="return validateBulkIndentConfirm();" UseSubmitBehavior="true" CssClass="btn btn-success btn-sm" Text="Confirm" Visible="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
