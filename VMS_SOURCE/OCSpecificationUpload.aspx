<%@ Page Title="QC Specification Report Upload" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="OCSpecificationUpload.aspx.vb" Inherits="OCSpecificationUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">var cal1 = new CalendarPopup();</script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">QC Specification Report Upload</h3>
                <p class="pageSubTitle">Upload quality control specification reports</p>
            </div>
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
                                <label class="form-control-label">Upload File:</label>
                                <asp:FileUpload ID="UploadFile" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-4 form-btn-mt">
                            <div class="form-group">
                                <%--<asp:ImageButton ImageUrl="images/upload.gif" CssClass="btn btn-primary btn-sm" ID="imgbtnupload" runat="server" />
                                <asp:ImageButton ImageUrl="images/download.gif" CssClass="btn btn-success btn-sm" ID="imgbtndownload" runat="server" />
                                <asp:ImageButton ImageUrl="images/btnback.gif" CssClass="btn btn-secondary btn-sm" ID="ImageButton2" runat="server" />--%>
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnupload" runat="server" OnClick="imgbtnupload_Click">Upload</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-success btn-sm" ID="imgbtndownload" runat="server" OnClick="imgbtndownload_Click">Download</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-secondary btn-sm" ID="ImageButton2" runat="server" OnClick="ImageButton2_Click">Back</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblValidationMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Label ID="lblvalidmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Label ID="lblConfirmMessage" runat="server"></asp:Label>
                </div>
            </div>

            <asp:HiddenField ID="hdnTargetID" runat="server" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnCancel"
                PopupControlID="pnlMessageBox" TargetControlID="hdnTargetID" CancelControlID="btnCancel"
                BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlMessageBox" runat="server" CssClass="popup" Height="170px" Width="350px" HorizontalAlign="Center">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div style="background-color: teal; height: 15px; text-align: left; padding: 2px;">
                            <asp:Label ID="lblMessageHeader" runat="server" ForeColor="White" Font-Bold="true" Text="Message"></asp:Label>
                        </div>
                        <br />
                        <div style="text-align: center; padding: 10px; height: 70px;">
                            <asp:Label ID="lblPopMessage" runat="server" ForeColor="#7f0037" Font-Bold="true" Text=""></asp:Label>
                        </div>
                        <%--<br />--%>
                        <asp:Button ID="btnDownloadForRectification" ForeColor="#ffffff" CssClass="btn btn-info" BackColor="teal" Font-Bold="true" runat="server" Text="Download file" Width="100px" />
                        <%-- <br />--%>
                        <asp:Button ID="btnCancel" ForeColor="#ffffff" BackColor="teal" Font-Bold="true" runat="server" Text="Cancel" Width="70px" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDownloadForRectification" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="imgbtnupload" />
                <asp:PostBackTrigger ControlID="imgbtndownload" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>
