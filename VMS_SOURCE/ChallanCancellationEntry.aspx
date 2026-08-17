<%@ Page Title="Despatched Challan Cancellation" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ChallanCancellationEntry.aspx.vb" Inherits="ChallanCancellationEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">var cal1 = new CalendarPopup();</script>
    <script src="Scripts/ValidateEstimationUpload.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {

            if (event.keyCode == 118) {  // button Add (F7 keypress)
                document.getElementById('btnSubmit').click()
            }
            else if (event.keyCode == 119) { // button Search (F8 keypress)

                document.getElementById('btnCancel').click()
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }
        //-->
    </script>
    <script src="Scripts/ValidateUnitDespatchAddUpdate.js?time=<%=  DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>" type="text/javascript"></script>
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Despatched Challan Cancellation</h3>
                <p class="pageSubTitle">Request cancellation of a despatched challan</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <asp:Label ID="lblChallanNo" runat="server" Style="text-align: right; display: block; font-weight: 700; color: #006fd0; font-size: 13px;"></asp:Label>
                    <asp:HiddenField ID="hdnChallanno" runat="server" />
                    <asp:HiddenField ID="hdnNoMaster" runat="server" />
                    <asp:HiddenField ID="hdnMaxDespLimit" runat="server" />
                    <asp:HiddenField ID="hdnLotNo" runat="server" />
                    <asp:HiddenField ID="hdnUnitOracleId" runat="server" />
                    <asp:HiddenField ID="hdnUnitCode" runat="server" />
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Challan Date:</label>
                                <asp:TextBox ID="txtChallanDt" CssClass="form-control" Enabled="false" MaxLength="10" TabIndex="1" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Region:</label>
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Depot:<span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:Label ID="lblUnit" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Year:</label>
                                <asp:Label ID="lblYear" runat="server" CssClass="labelDataPoint"></asp:Label>
                                <asp:HiddenField ID="hdnYear" runat="server" ClientIDMode="Static"></asp:HiddenField>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Month:</label>
                                <asp:Label ID="lblmonth" runat="server" CssClass="labelDataPoint"></asp:Label>
                                <asp:HiddenField ID="hdnMonth" runat="server" ClientIDMode="Static"></asp:HiddenField>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Transporter:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtTransporter" CssClass="form-control" TabIndex="6" runat="server" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Truck No.:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtTruckNo" CssClass="form-control" TabIndex="7" runat="server" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Vendor Challan No.:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtCenvatNo" CssClass="form-control" TabIndex="8" runat="server" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Vendor Challan Date:</label>
                                <asp:TextBox ID="txtCenvatDt" CssClass="form-control" MaxLength="10" TabIndex="9" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Road Permit No.:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtRoadPermitNo" CssClass="form-control" TabIndex="7" runat="server" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Invoicing Depot:<span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlDeliveryDepot" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="form-group row ddlPageSize">
                        <label for="ddlPageSize" class="col-auto form-control-label">
                            <asp:Label ID="Label4" runat="server" Text="Results Per Page:"></asp:Label>
                        </label>
                        <div class="col-md-1">
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvSKUDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" GridLines="Vertical"
                            EmptyDataText="No SKU Found">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:BoundField>
                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                            <asp:HiddenField ID="hdnUom" runat="server" Value='<%# Bind("skuUom") %>' />
                                                                                            <asp:HiddenField ID="hdnVol" runat="server" Value='<%# Bind("skuVol") %>' />
                                                                                            <asp:HiddenField ID="hdnTransitDay" runat="server" Value='<%# Bind("transitDays") %>' />
                                                                                            <asp:HiddenField ID="hdnSKUCode" runat="server" Value='<%# Bind("load_sku_code") %>' />
                                                                                            <asp:HiddenField ID="hdnLineNum" runat="server" Value='<%# Bind("line_num") %>' />
                                                                                            <asp:HiddenField ID="hdnSkuDesc" runat="server" Value='<%# Bind("skuDesc") %>' />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                                                                    </asp:TemplateField>--%>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="SKU Code" DataField="load_sku_code">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="14%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Description" DataField="skuDesc">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="23%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="23%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="23%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="Auto Indent" DataField="calculatedAuto">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Depot Indent" DataField="load_depot_indent_nop_pending">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Despatch Till Date" DataField="calculatedDespatch" ControlStyle-Width="10%">
                                    <ControlStyle Width="10%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderText="Pending Load" DataField="pendingLoad">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                        </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="This Despatch" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingLoad" runat="server" Text='<%# Bind("pendingLoad") %>'
                                            Width="90%"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="This Despatch" HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtThisDesp" CssClass="txtBox" runat="server" Text='<%# Bind("pendingLoad") %>'
                                                                                                Width="50px" MaxLength="30" Enabled="False"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                                                                    </asp:TemplateField>--%>

                                <%-- <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                 <asp:Button ID="btnGo" CommandName="ShowQuantity" runat="server" CssClass="but2" Text="Go" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                        </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="LOT" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%-- <asp:TextBox ID="txtLOT" CssClass="txtBox" runat="server" Text='<%# Bind("despd_lot_no") %>'
                                                                                                Width="170px" Enabled="False"></asp:TextBox>--%>
                                        <asp:Label ID="lblLOT" runat="server" Text='<%# Bind("despd_lot_no")%>'
                                            Width="90%"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-sm" Text="Submit" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" PostBackUrl="~/ChallanCancellationList.aspx" />
                        </div>
                    </div>
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </div>
            </div>

            <asp:HiddenField ID="hdnTargetID_Quantity" runat="server" />
            <asp:HiddenField ID="hdnTargetID1" runat="server" />
            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="btnCance" PopupControlID="pnlQuantity" TargetControlID="hdnTargetID_Quantity" CancelControlID="btnCance" BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>
            <%-- <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlQuantity"
                TargetControlID="hdnTargetID_Quantity" BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>--%>
            <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" OkControlID="btnOk" PopupControlID="PnlOk" TargetControlID="hdnTargetID1" CancelControlID="btnOk" BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>

            <div id="divDespatchQuantity" style="text-align: center;" runat="server">
                <asp:Panel ID="pnlQuantity" runat="server" Width="500px" Height="450px" CssClass="popup"
                    HorizontalAlign="Center">
                    <div style="text-align: left; padding: 2px; border: none; background-color: #66CCFF; border: #999999;">
                        <asp:Label ID="lblSpPopupHdr" runat="server" Font-Bold="True"></asp:Label>
                    </div>
                    <br />
                    <table style="width: 99%; border: 1px solid #66CCFF">
                        <tr>
                            <td style="background-color: #E6F5FB; width: 30%; text-align: right; font-weight: bold; border-bottom: 1px solid #66CCFF;">SKU
                            </td>
                            <td align="left" style="border-bottom: 1px solid #66CCFF;">
                                <asp:Label ID="lblSKU" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnDespChallanNo" runat="server" />
                                <asp:HiddenField ID="hdnFinYear" runat="server" />
                                <asp:HiddenField ID="hdnDespatchUnit" runat="server" />
                                <asp:HiddenField ID="hdnDepotCode" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #E6F5FB; width: 30%; text-align: right; font-weight: bold; border-bottom: 1px solid #66CCFF;">SKU Description
                            </td>
                            <td align="left" style="border-bottom: 1px solid #66CCFF;">
                                <asp:Label ID="lblSKUDescription" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td style="background-color: #E6F5FB; width: 30%; text-align: right; font-weight: bold;">Total Quantity
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTotalDespatchQuantity" runat="server"></asp:Label>
                            </td>
                        </tr>

                    </table>
                    <table style="width: 100%; border: 1px solid #66CCFF">
                        <tr>
                            <th colspan="3" style="width: 5%; border-bottom: 1px solid #66CCFF; border-right: 1px solid #66CCFF; background-color: #E6F5FB;">Details
                            </th>
                            <th style="width: 20%; border-bottom: 1px solid #66CCFF; background-color: #E6F5FB;">Quantity
                            </th>
                        </tr>
                        <tr>
                            <td style="width: 10%">IND
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblPONo1" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 30%">
                                <asp:TextBox ID="txtDate1" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity1" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tlrowdark">
                            <td>IND
                            </td>
                            <td>
                                <asp:Label ID="lblPONo2" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate2" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity2" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>IND
                            </td>
                            <td>
                                <asp:Label ID="lblPONo3" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate3" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity3" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tlrowdark">
                            <td>IND
                            </td>
                            <td>
                                <asp:Label ID="lblPONo4" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate4" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity4" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>IND
                            </td>
                            <td>
                                <asp:Label ID="lblPONo5" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate5" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity5" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                    <br />


                    <br />
                    <asp:Button ID="btnAddSP" CssClass="but2" runat="server" Text="Add" Width="165px" />
                    &nbsp;<asp:Button ID="btnCance" runat="server" CssClass="but2" Text="Cancel" Width="165px" />
                </asp:Panel>
            </div>

            <asp:Panel ID="PnlOk" runat="server" CssClass="popup" Height="170px" HorizontalAlign="Center">
                <div style="background-color: #66CCFF; border: #999999; height: 15px; text-align: left; padding: 2px;">
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Font-Bold="true" Text="Message"></asp:Label>
                </div>
                <br />
                <div style="text-align: center; padding: 10px; height: 70px; overflow: scroll;">
                    <asp:Label ID="lblPopMessage" runat="server" ForeColor="#7f0037" Font-Bold="true" Text=""></asp:Label>
                </div>
                <br />
                <asp:Button ID="btnOk" CssClass="but2" Font-Bold="true"
                    runat="server" Text="Ok" Width="40px" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
