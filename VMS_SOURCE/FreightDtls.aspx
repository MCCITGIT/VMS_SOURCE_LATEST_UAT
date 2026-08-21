<%@ Page Title="Freight Details List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FreightDtls.aspx.vb" Inherits="FreightDtls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" src="Scripts/ValidationIndentList_HO.js"></script>--%>
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

    <script type="text/javascript">
        function isNumber(evt, element) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;

            // Allow: backspace (8), tab (9), delete (46), arrows (37&#8211;40)
            if (charCode == 8 || charCode == 9 || charCode == 46 || (charCode >= 37 && charCode <= 40)) {
                return true;
            }

            // Allow decimal point (.) only once
            if (charCode == 46) {
                if (element.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            }

            // Allow digits 0&#8211;9
            if (charCode < 48 || charCode > 57) {
                return false;
            }

            return true;
        }
        function validateFileUpload() {
            var fileUpload = document.getElementById('<%= uploadBulkExcel.ClientID %>');

            // Check if a file is selected
            if (fileUpload.value === "") {
                alert("Please select a file before proceeding.");
                return false;
            }
            var confirmUpload = confirm("Are you sure you want to upload this file?");
            if (!confirmUpload) {
                return false; // cancel upload
            }

            return true; // proceed with upload
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Freight Details List</h3>
                <p class="pageSubTitle">Freight charges by route and vendor</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Unit Name:</label>
                                <asp:DropDownList ID="ddlVendorUnit" CssClass="form-control select2" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Freight Value:</label>
                                <asp:DropDownList ID="ddlFreight" CssClass="form-control select2" runat="server">
                                    <asp:ListItem Value="1">Greater Than 0</asp:ListItem>
                                    <asp:ListItem Value="0">Blank</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 form-btn-mt">
                            <div class="form-group">
                                <%--<asp:ImageButton CssClass="btn btn-primary btn-sm" ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" />--%>
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-success btn-sm" ID="imgbtnDownload" runat="server">Download</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" id="div_upload">
                            <div class="form-group">
                                <label class="form-control-label">Upload File:</label>
                                <asp:FileUpload ID="uploadBulkExcel" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-1 form-btn-mt" runat="server" id="div_upload_button">
                            <div class="form-group">
                                <asp:LinkButton CssClass="btn btn-info btn-sm" ID="btnUpload" runat="server" OnClientClick="return validateFileUpload();">Upload</asp:LinkButton>
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
                                <asp:GridView ID="gvRequistionList" runat="server" AutoGenerateColumns="False" BorderWidth="1" CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found">
                                    <RowStyle CssClass="tlrowlight" />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Srl No" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrl" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnFreightID" Value='<%# Bind("udfd_id") %>' />
                                            </ItemTemplate>

                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Source" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" Text='<%# Bind("unit_name") %>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnUnitCode" Value='<%# Bind("v_vendor_unit") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Depot" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepot" Text='<%# Bind("depot_name")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnDepotCode" Value='<%# Bind("v_depot") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Freight/kg (Rs.)" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblFreight" Text='<%# Bind("udfd_freight_dtls") %>' runat="server" />--%>
                                                <asp:TextBox runat="server" ID="txtFreight" Text='<%# Bind("udfd_freight_dtls") %>' onkeypress="return isNumber(event, this)" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                                <asp:PostBackTrigger ControlID="gvRequistionList" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return confirm('Are you sure you want to submit?');"/>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card" runat="server" id="tr2" visible="false">
                <div class="card-body">
                    <div class="table-responsive" style="overflow-y: auto; max-height: calc(100vh - 290px);">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gdvPreview" runat="server" AutoGenerateColumns="False" BorderWidth="1"
                                    CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found">
                                    <RowStyle CssClass="tlrowlight" />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Source" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" Text='<%# Bind("Unit") %>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnUnitCode" Value='<%# Bind("UnitCode") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Depot" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepot" Text='<%# Bind("Depot")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hdnDepot" Value='<%# Bind("DepotCode") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Freight/kg (Rs.)" ControlStyle-Width="90%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFreight" Text='<%# Bind("Freight")%>' runat="server" />
                                            </ItemTemplate>
                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <%-- <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                                <asp:PostBackTrigger ControlID="gvRequistionList" />
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" CssClass="btn btn-success btn-sm" runat="server" Text="Confirm" Visible="false" OnClientClick="return confirm('Are you sure you want to submit?');" />
                            <asp:Button ID="btnReset" CssClass="btn btn-success btn-sm" runat="server" Text="Reset" Visible="false" />
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
                                    <img src="images/success.gif" style="width:auto;height:100px;margin:0px 0px 20px 0px" alt="Img"/>
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

            <%--<asp:Panel ID="PnlOk" runat="server" CssClass="popup" Height="170px" HorizontalAlign="Center">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div style="background-color: #66CCFF; border: #999999; height: 15px; text-align: left; padding: 2px;">
                            <asp:Label ID="Label1" runat="server" ForeColor="White" Font-Bold="true" Text="Message"></asp:Label>
                        </div>
                        <br />
                        <div style="text-align: center; padding: 10px; height: 70px; overflow: scroll;">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="#7f0037" Font-Bold="true" Text=""></asp:Label>
                        </div>
                        <br />
                        <asp:Button ID="btnOk" CssClass="but2" Font-Bold="true"
                            runat="server" Text="Ok" Width="40px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>--%>


            <%--<asp:Panel ID="Panel1" runat="server" CssClass="popup" Height="170px" HorizontalAlign="Center">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div style="background-color: #66CCFF; border: #999999; height: 15px; text-align: left; padding: 2px;">
                            <asp:Label ID="Label2" runat="server" ForeColor="White" Font-Bold="true" Text="Message"></asp:Label>
                        </div>
                        <br />
                        <div style="text-align: center; padding: 10px; height: 70px; overflow: scroll;">
                            <asp:Label ID="lbl_Msg" runat="server" ForeColor="#7f0037" Font-Bold="true" Text=""></asp:Label>s
                        </div>
                        <br />
                        <asp:Button ID="Button1" CssClass="but2" Font-Bold="true"
                            runat="server" Text="Ok" Width="40px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnDownload" />
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel1"
        TargetControlID="HiddenField1" BackgroundCssClass="popupBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPanel bootstrapModal" Style="display: none;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Message</h5>
                            <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>--%>
                        </div>
                        <div class="modal-body text-center">
                            <%--<div class="table-responsive" style="max-height: 350px; overflow-y: auto;"></div>--%>
                            <img src="images/success.gif" style="width: auto; height: 100px; margin: 0px 0px 20px 0px" alt="Img" />
                            <asp:Label ID="lbl_Msg" runat="server" class="form-control-label" Style="font-size: 18px;"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btn_ok" CssClass="btn btn-primary" runat="server" Text="OK" OnClick="btn_ok_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
