<%@ Page Title="QC Specification Report" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="OCSpecificationReport.aspx.vb" Inherits="OCSpecificationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            <%-- Modified-by MUKESH BHAGAT on 20-08-2026 : MasterPage mangles IDs and imgbtnSearch is now a LinkButton (no "name");
                 use UniqueID. F7 branch removed - imgbtnAdd does not exist on this page (getElementById(null).name threw a JS error). --%>
            if (event.keyCode == 119) {
                __doPostBack('<%= imgbtnSearch.UniqueID %>', '');
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }
    </script>
    <script type="text/javascript">var cal1 = new CalendarPopup();</script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">QC Specification Report</h3>
                <p class="pageSubTitle">Quality control specification report</p>
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
                                <label class="form-control-label">Vender:</label>
                                <asp:DropDownList ID="ddlVender" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">From Date:</label>
                                <asp:TextBox ID="txtFromDate" CssClass="form-control" MaxLength="10" TabIndex="23" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">To Date:</label>
                                <asp:TextBox ID="txtTodate" CssClass="form-control" MaxLength="10" TabIndex="24" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="dd/MM/yyyy" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Product:</label>
                                <asp:DropDownList ID="ddlproduct" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 form-btn-mt">
                            <div class="form-group">
                                <%-- <asp:ImageButton ImageUrl="images/ic_search.gif" CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" />--%>
                                <%--<asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click">Search</asp:LinkButton>--%>
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click">Download</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblPopMessage" runat="server"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbtnSearch" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
