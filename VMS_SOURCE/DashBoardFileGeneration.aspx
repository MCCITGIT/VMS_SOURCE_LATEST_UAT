<%@ Page Title="Dashboard File Generation" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DashBoardFileGeneration.aspx.vb" Inherits="DashBoardFileGeneration" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Dashboard File Generation</h3>
                <p class="pageSubTitle">Generate the dashboard data files</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Process Year:</label>
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="form-control select2" TabIndex="3">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Process Month:</label>
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="form-control select2" TabIndex="3">
                            <asp:ListItem>01</asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem>03</asp:ListItem>
                            <asp:ListItem>04</asp:ListItem>
                            <asp:ListItem>05</asp:ListItem>
                            <asp:ListItem>06</asp:ListItem>
                            <asp:ListItem>07</asp:ListItem>
                            <asp:ListItem>08</asp:ListItem>
                            <asp:ListItem>09</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 form-btn-mt">
                    <div class="form-group">
                        <asp:Button ID="Button1" runat="server" Text="Generate Dashboard File" CssClass="btn btn-primary btn-sm" />
                    </div>
                </div>
            </div>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
