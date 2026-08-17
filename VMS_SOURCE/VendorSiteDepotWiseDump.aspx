<%@ Page Title="Vendor Site Depot Wise Dump Report" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="VendorSiteDepotWiseDump.aspx.vb" Inherits="VendorSiteDepotWiseDump" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Vendor Site Depot Wise Dump Report</h3>
                <p class="pageSubTitle">Vendor site and depot data extract</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Vendor:</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Download" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
