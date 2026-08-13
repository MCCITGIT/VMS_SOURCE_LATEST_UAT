<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BulkRawMaterialReceiptDtls.aspx.vb" Inherits="BulkRawMaterialReceiptDtls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateBulkRawMaterialReceipt.js"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {
                if (document.getElementById('btnSubmit').disabled == true)
                    return false;
                else if (typeof validateRawMaterialRequisitionSubmit === "function" && validateRawMaterialRequisitionSubmit()) {
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
            <h3 class="pageTitle">Bulk Raw Material Receipt</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Receipt No. (Autogenerate):</label>
                                <asp:TextBox ID="txtreceiptNo" ClientIDMode="Static" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Courier:</label>
                                <asp:TextBox ID="txtCourierno" ClientIDMode="Static" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="hdnDespatchId" runat="server" />
                                <asp:HiddenField ID="hdnRequisitionId" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h5 class="mb-2">Item Details</h5>
                            <div class="table-responsive">
                                <asp:GridView ID="gvVendorRawMat" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" CssClass="table table-hover upgradDataGrid"
                                    EmptyDataText="No pending raw material found for receipt." GridLines="both"
                                    OnRowCommand="gvVendorRawMat_RowCommand" OnRowDataBound="gvVendorRawMat_RowDataBound">
                                    <RowStyle CssClass="tlrowlight" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Checked="true" Style="display: none" />
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                <asp:HiddenField ID="hdnsubinvemtory" Value='<%# Bind("sub_inventory")%>' runat="server" />
                                                <asp:HiddenField ID="hdnlocator" Value='<%# Bind("locater")%>' runat="server" />
                                                <asp:HiddenField ID="hdnItemCode" runat="server" Value='<%# Bind("rawmat_code")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Raw Material">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRawMatName" runat="server" Text='<%# Eval("rawmat_name") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnRawMatCode" runat="server" Value='<%# Eval("rawmat_code") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty. to be received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDespQty" runat="server" Text='<%# Eval("despatch_qunt", "{0:0.##}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                            <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Request Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" Text='<%# Eval("reqst_Qty", "{0:0.##}") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                            <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecMatchQty" runat="server" CssClass="form-control" Text='<%# Eval("despatch_qunt", "{0:0.##}") %>' Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                            <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAdjustRow" runat="server" CssClass="btn btn-success btn-sm" CommandName="Adjustment" Text="Adjustment" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" ClientIDMode="Static" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning btn-sm" OnClick="btnReset_Click" />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <asp:HiddenField ID="hdnAdjustTarget" runat="server" />
            <asp:ModalPopupExtender ID="mpAdjust" runat="server" PopupControlID="pnlAdjustment" TargetControlID="hdnAdjustTarget"
                BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlAdjustment" runat="server" CssClass="modalPanel bootstrapModal" Style="display: none;">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="upAdjustPopup" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                        <div class="modal-header">
                            <h5 class="modal-title">Adjustment -
                                <asp:Label ID="lblHItemPop" runat="server"></asp:Label>
                                <span style="font-weight: normal;">(Qty -
                                    <asp:Label ID="lblQtyPop" runat="server" Text="0"></asp:Label>)</span>
                            </h5>
                            <asp:HiddenField ID="hdnItemCodePop" runat="server" />
                            <asp:HiddenField ID="hdnItemTypePop" runat="server" />
                            <asp:HiddenField ID="hdnRequestQtyPop" runat="server" />
                            <asp:HiddenField ID="hdnDespQtygv" runat="server" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-control-label">Sub Inventory:</label>
                                        <asp:DropDownList ID="ddlSubInventoryPop" ClientIDMode="Static" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubInventoryPop_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-control-label">Locator:</label>
                                        <asp:DropDownList ID="ddlLocatorPop" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="form-control-label">Balance Qty:</label>
                                        <div class="form-control" style="background-color: #e9ecef;">
                                            <asp:Label ID="lblDespopQty" ClientIDMode="Static" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="form-control-label">Recp Qty:</label>
                                        <asp:TextBox runat="server" ID="txtQtyPop" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="form-control-label">&nbsp;</label>
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-info btn-sm btn-block" Text="Add" ToolTip="Add" CausesValidation="false" OnClick="btnAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="lblmsg" ClientIDMode="Static" runat="server" CssClass="errormsg" ForeColor="Red" Text=""></asp:Label>
                            <div class="table-responsive mt-2">
                                <asp:GridView ID="gvAdjustDtls" runat="server" AutoGenerateColumns="false" Width="100%" AllowPaging="false" EmptyDataText="No record(s) found."
                                    CssClass="table table-hover upgradDataGrid" OnRowCommand="gvAdjustDtls_RowCommand">
                                    <RowStyle CssClass="tlrowlight" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Bind("item_name")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnitemcode" runat="server" Value='<%# Bind("item_code")%>' />
                                                <asp:HiddenField ID="hdntypecode" runat="server" Value='<%# Bind("item_type_code")%>' />
                                                <asp:Label ID="lbltype" runat="server" Text='<%# Bind("item_type")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="35%" />
                                            <ItemStyle HorizontalAlign="Left" Width="35%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Inventory">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsubinventory" runat="server" Text='<%# Bind("sub_inventory")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnsubinventorycode" runat="server" Value='<%# Bind("sub_inventory_code")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Locator">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllocator" runat="server" Text='<%# Bind("locator")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnlocatorcode" runat="server" Value='<%# Bind("locator_code")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtygv" runat="server" Text='<%# Bind("received_qty")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" CausesValidation="false" ToolTip="Remove">
                                                    <i class="fa fa-trash" style="font-size: large;color: #dc1818;"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:Label ID="lblAdjustError" ClientIDMode="Static" runat="server" CssClass="errormsg" ForeColor="Red" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnAdjustContinue" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false"
                                OnClick="btnAdjustContinue_Click">Continue</asp:LinkButton>
                            <asp:LinkButton ID="lbtnExit" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false"
                                OnClick="lbtnExit_Click">Cancel</asp:LinkButton>
                        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnAdjustContinue" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
