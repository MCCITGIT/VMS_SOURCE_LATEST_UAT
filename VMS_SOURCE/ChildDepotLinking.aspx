<%@ Page Title="Child Depot Linking" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ChildDepotLinking.aspx.vb" Inherits="ChildDepotLinking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationChildDepotLinking.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>

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

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Child Depot Linking</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Depot:</label>
                                <asp:DropDownList ID="ddlDepot" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group">
                                <asp:ImageButton ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" CssClass="btn btn-primary btn-sm" />
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Font-Size="Medium" Visible="true" runat="server"></asp:Label>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <asp:GridView ID="gvParentDepotList" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False" BorderWidth="1" CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found">
                                    <RowStyle CssClass="tlrowlight" />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Region">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("depot_regn") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Depot">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="View" Visible="true" Text='<%# Bind("depot_name") %>' title="View Details" ForeColor="Blue"></asp:LinkButton>
                                                <%-- <asp:Label ID="lblDepotCode" runat="server" Text='<%# Bind("depot_name") %>'></asp:Label>--%>
                                                <asp:HiddenField ID="hdnParentDepotName" runat="server" Value='<%#Eval("depot_name")%>' />
                                                <asp:HiddenField ID="hdnDepotCode" runat="server" Value='<%#Eval("depot_code")%>' />
                                                <asp:HiddenField ID="hdnChildDepotCode" runat="server" Value='<%#Eval("child_depots_code")%>' />

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Child Depots">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChildDepots" runat="server" Text='<%# Bind("child_depots_name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="75%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:HiddenField ID="hdnTargetID" runat="server" />
                            <asp:HiddenField ID="hdnTargetID2" runat="server" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnOk"
                                PopupControlID="pnlMessageBox" TargetControlID="hdnTargetID" CancelControlID="btnOk"
                                BackgroundCssClass="popupBackground">
                            </asp:ModalPopupExtender>
                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="btnCancel"
                                PopupControlID="pnlChildDepot" TargetControlID="hdnTargetID2" CancelControlID="btnCancel"
                                BackgroundCssClass="popupBackground">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <asp:Panel ID="pnlMessageBox" runat="server" CssClass="popup" Height="170px" Width="350px" HorizontalAlign="Center">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div style="background-color: teal; height: 15px; text-align: left; padding: 2px;">
                            <asp:Label ID="lblMessageHeader" runat="server" ForeColor="White" Font-Bold="true"
                                Text="Message"></asp:Label>
                        </div>
                        <br />
                        <div style="text-align: center; padding: 10px; height: 70px; overflow: scroll;">
                            <asp:Label ID="lblPopMessage" runat="server" ForeColor="#7f0037" Font-Bold="true"
                                Text=""></asp:Label>
                        </div>
                        <br />
                        <asp:Button ID="btnOk" ForeColor="#ffffff" BackColor="teal" Font-Bold="true" runat="server"
                            Text="Ok" Width="40px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>

            <asp:Panel ID="pnlChildDepot" runat="server" Width="800px" Height="280px" CssClass="popup"
                HorizontalAlign="Center">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div style="text-align: left; padding: 2px; border: none; background-color: #66CCFF; border: #999999;">
                            Parent Depot :
                            <asp:Label ID="lblPopupParentDepotHdr" runat="server" Font-Bold="True"></asp:Label>
                            <asp:HiddenField ID="hdnParentDepot" runat="server" />
                        </div>
                        <br />
                        <table style="width: 99%; border: 1px solid #66CCFF">
                            <tr>
                                <td style="background-color: #E6F5FB; width: 15%; text-align: right; font-weight: bold; border-bottom: 1px solid #66CCFF;">Child Depot <span class="mandatory">*</span>
                                </td>
                                <td align="left" style="border-bottom: 1px solid #66CCFF;">
                                    <div style="overflow-y: auto; overflow-x: auto; width: 100%; height: 150px; text-align: left;">
                                        <asp:CheckBoxList ID="chkbxChildDepotList" runat="server" TabIndex="10" Font-Size="Small" RepeatColumns="3" RepeatDirection="Horizontal" Width="100%" AutoPostBack="False">
                                        </asp:CheckBoxList>
                                    </div>

                                </td>
                            </tr>

                        </table>

                        <br />


                        <br />
                        <asp:Button ID="btnSubmit" CssClass="but2" runat="server" Text="Submit" Width="165px" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="but2" Text="Cancel" Width="165px" />
                        <table>
                            <tr style="text-align: left;">
                                <td>
                                    <asp:Label ID="lblPopValidationMessage" CssClass="errormsg" Font-Size="Medium" Visible="true" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
