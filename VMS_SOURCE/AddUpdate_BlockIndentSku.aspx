<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="AddUpdate_BlockIndentSku.aspx.vb" Inherits="AddUpdate_BlockIndentSku" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/Validate_BlockSku.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
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
    <style>
        .gapOr {
            width: 2px;
            border-right: 2px solid #c2c2c2;
            height: 60px;
            margin: auto;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 0px;
        }

            .gapOr p {
                font-size: 15px;
                position: absolute;
                font-weight: bold;
                line-height: 18px;
                top: 20px;
                color: #10385a;
                background: #FFF;
                width: 100%;
                display: flex;
                align-items: center;
                justify-content: center;
                margin: auto;
                padding: 2px 0px;
            }
    </style>
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Block Indent Sku Master</h3>
                <p class="pageSubTitle">Block SKUs from being indented</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">                    
                    <div class="row align-items-end">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">SKU Code</label>
                                <asp:TextBox runat="server" ID="txtSearchCode"
                                    CssClass="form-control"
                                    AutoComplete="Off"
                                    placeholder="Search by SKU Code"
                                    TabIndex="1">
                                </asp:TextBox>                                
                            </div>
                        </div>

                        <!-- Search Button -->
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton CssClass="btn btn-primary btn-sm mt-4"
                                    ID="imgbtnSearch"
                                    runat="server"
                                    TabIndex="2" OnClick="imgbtnSearch_Click">
                                            Search
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="gapOr">
                                <p>OR</p>
                            </div>
                        </div>

                        <!-- SKU Code -->
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">SKU Code</label>
                                <asp:TextBox runat="server" ID="txtSkuCode"
                                    CssClass="form-control"
                                    AutoComplete="Off"
                                    TabIndex="3"
                                    placeholder="SKU Code"
                                    AutoPostBack="True"
                                    OnTextChanged="txtSkuCode_TextChanged">
                                </asp:TextBox>
                            </div>
                        </div>

                        <!-- SKU Description -->
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">SKU Description</label>
                                <asp:TextBox runat="server" ID="txtSkuDesc"
                                    CssClass="form-control"
                                    AutoComplete="Off"
                                    TabIndex="4"
                                    placeholder="SKU Description"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </div>
                        </div>

                        <!-- Buttons -->
                        <div class="col-md-12" runat="server" id="div_upload_button">
                            <div class="form-group mt-4 d-flex justify-content-center" style="gap: 5px;">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm"
                                    runat="server" Text="Submit" />

                                <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm"
                                    runat="server" Text="Cancle" />

                                <asp:Button ID="btnReset" CssClass="btn btn-warning btn-sm"
                                    runat="server" Text="Reset" />
                            </div>
                        </div>
                        <div>
                            <div>
                                <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server" style="padding-left: 8px;"></asp:Label>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="card" runat="server" id="tr1">
                <div class="card-body">
                    <div class="table-responsive" style="overflow-y: auto; max-height: calc(100vh - 290px);">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvSkucode" runat="server" AutoGenerateColumns="False" BorderWidth="1" CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found">
                                    <RowStyle CssClass="tlrowlight" />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Srl No" ControlStyle-Width="90%">
                                        <ItemTemplate>
                                            <asp:Label ID="ldlSrlNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>

                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sku Code" ControlStyle-Width="90%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSkuCode" Text='<%# Bind("Sku_code") %>' runat="server" />
                                        </ItemTemplate>
                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sku Description" ControlStyle-Width="90%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSkuDesc" Text='<%# Bind("Sku_Desc")%>' runat="server" />
                                        </ItemTemplate>
                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Active" ControlStyle-Width="90%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActive" Text='<%# Bind("Active_Status")%>' runat="server" />
                                        </ItemTemplate>
                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" CommandName="EditRow" Visible="true" runat="server" OnClientClick="return confirm('Are you sure to delete?');"
                                                CssClass="btn btn-info gridBtn" Text="Delete" title="Delete" ToolTip="Click To Delete"
                                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>'></asp:Button>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                           <%-- <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                                <asp:PostBackTrigger ControlID="gv" />
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                        </div>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hdnTargetID1" runat="server" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnOk" PopupControlID="PnlOk"
                TargetControlID="hdnTargetID1" CancelControlID="btnOk" BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>

            <asp:Panel ID="PnlOk" runat="server" CssClass="modalPanel bootstrapModal" Style="display: none;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Message</h5>
                                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>--%>
                                </div>

                                <div class="modal-body text-center">
                                    <%--<div class="table-responsive" style="max-height: 350px; overflow-y: auto;"></div>--%>
                                    <img src="images/success.gif" style="width: auto; height: 100px; margin: 0px 0px 20px 0px" alt="Img" />
                                    <asp:Label ID="lblMsg" runat="server" class="form-control-label" Style="font-size: 18px;" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnOk" CssClass="btn btn-primary" runat="server" Text="OK" />
                                </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>


