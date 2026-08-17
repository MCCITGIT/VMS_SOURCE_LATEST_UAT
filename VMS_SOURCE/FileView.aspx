<%@ Page Title="Untitled Page" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FileView.aspx.vb" Inherits="FileView" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationFlashMaster.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Untitled Page</h3>
                <p class="pageSubTitle">View the selected file</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div>Body</div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
