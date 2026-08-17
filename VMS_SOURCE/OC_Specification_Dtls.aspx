<%@ Page Title="QC SPECIFICATION ADD" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="OC_Specification_Dtls.aspx.vb" Inherits="OC_Specification_Dtls" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var gvProductParameterId = '<%= gvProductParameter.ClientID %>';
    </script>
    <script type="text/javascript" language="javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/Messages.js"></script>
    <script type="text/javascript" src="Scripts/ValidateOC_SpecificationJS.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff")%>"></script>
    <script type="text/javascript">
        function isNumber(evt) {
            debugger;
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function isNumberKey(evt, txt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                  (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">var cal1 = new CalendarPopup();</script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">QC SPECIFICATION ADD</h3>
                <p class="pageSubTitle">Define quality control specification parameters</p>
            </div>
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
                                <label class="form-control-label">Vender:<span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlVender" ClientIDMode="Static" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Product:<span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlProduct" ClientIDMode="Static" CssClass="form-control select2" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Batch No.:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtBatchno" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Batch date:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtBatchDate" ClientIDMode="Static" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBatchDate" Format="dd/MM/yyyy" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Product Code:<span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlProductCode" ClientIDMode="Static" CssClass="form-control select2" runat="server">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvProductParameter" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Parameters">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParams" runat="server" Text='<%# Bind("Params") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Result">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtResult" CssClass="form-control" runat="server" Text='<%# Bind("Result") %>'></asp:TextBox>
                                        <asp:DropDownList ID="ddlresult" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frequency">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrequency" runat="server" Text='<%# Bind("FrequencyName") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnDropDownYN" Value='<%# Bind("IsDropdown") %>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hdnFrequncy" Value='<%# Bind("Frequency")%>' runat="server"></asp:HiddenField>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                        </div>
                    </div>
                    <asp:Label ID="lblErrMsg" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnOk" runat="server" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                        PopupControlID="Panel1" TargetControlID="hdnOk">
                    </asp:ModalPopupExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalPanel bootstrapModal" Style="display: none;">
                <%--<div style="background-color: #6699FF; height: 15px; text-align: center; padding: 2px;">
                    <asp:Label ID="Label2" runat="server" ForeColor="White" Font-Bold="true" Text="QC Specification"></asp:Label>
                </div>--%>
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
                                    <img src="images/success.gif" style="width: auto; height: 50px; margin: 0px 0px 20px 0px" alt="Img" />
                                        <asp:Label ID="lblPopMessage" runat="server" class="form-control-label" Style="font-size: 18px;"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnOK" CssClass="btn btn-primary" runat="server" Text="OK"/>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
