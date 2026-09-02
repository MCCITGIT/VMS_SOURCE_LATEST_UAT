<%@ Page Title="Bulk Load Drop Upload" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BulkLoadDropUpload.aspx.vb" Inherits="BulkLoadDropUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidationBulkLoadDropUpload.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {
                var btnSubmit = document.getElementById('btnSubmit');
                if (btnSubmit && btnSubmit.disabled !== true) {
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
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Bulk Load Drop Upload</h3>
                <p class="pageSubTitle">Upload load drop requests in bulk from Excel</p>
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
                    <p class="mst-panel-subtitle">
                        Process Year: <asp:Label ID="lblProcessYear" runat="server" />
                        &nbsp;&nbsp;Process Month: <asp:Label ID="lblProcessMonth" runat="server" />
                        &nbsp;&nbsp;[F7 = Submit] [F8 = Cancel]
                    </p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="form-control-label">Choose File:<span class="mandatory">*</span></label>
                        <asp:FileUpload runat="server" ID="fupUploadFile" ClientIDMode="Static" CssClass="form-control" accept="application/vnd.ms-excel" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="form-control-label d-block">&nbsp;</label>
                        <asp:Button runat="server" ID="btnDownloadTemplate" OnClick="btnDownloadTemplate_Click"
                            CssClass="btn btn-info btn-sm" Text="Download Template" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button runat="server" ID="btnSubmit" ClientIDMode="Static" OnClick="btnSubmit_Click" UseSubmitBehavior="true" CssClass="btn btn-primary btn-sm" Text="Submit" />
                    <asp:Button runat="server" ID="btnCancel" ClientIDMode="Static" OnClick="btnCancel_Click" UseSubmitBehavior="true" CssClass="btn btn-secondary btn-sm" Text="Cancel" />
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-12 text-center">
                    <asp:Label runat="server" ID="lblMsg" ClientIDMode="Static" CssClass="errormsg" Text=""></asp:Label>
                    <asp:LinkButton runat="server" ID="lbtnDwnloadFile" OnClick="lbtnDwnloadFile_Click" Visible="false" CssClass="btn btn-link btn-sm">
                        <i class="fas fa-file-excel"></i> Download Errors
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="pnlErrorList" runat="server" Visible="false">
    <div class="card mt-3">
        <div class="mst-panel-header">
            <div class="mst-panel-header-left">
                <span class="mst-panel-icon"><i class="fas fa-list"></i></span>
                <div>
                    <h5 class="mst-panel-title">Validation Errors</h5>
                    <p class="mst-panel-subtitle">Correct the listed rows and upload the file again</p>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvLoadDropList" runat="server" AutoGenerateColumns="False" Width="100%"
                    CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found" GridLines="both">
                    <RowStyle CssClass="tlrowlight" />
                    <HeaderStyle CssClass="headerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Depot Code" DataField="depot_code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="SKU Code" DataField="sku_code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Unit Code" DataField="unit_code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Qty" DataField="qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Reason" DataField="reason" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </asp:Panel>
</asp:Content>
