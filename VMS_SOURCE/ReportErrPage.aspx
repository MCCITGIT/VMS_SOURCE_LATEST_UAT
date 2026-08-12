<%@ Page Title="Untitled Page" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ReportErrPage.aspx.vb" Inherits="ReportErrPage" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationFlashMaster.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Untitled Page</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            NO ITEMS ARE ENTERED.<br />
            PLEASE ENTER MACHINE TYPE/ACCESSORIE'S AND THEN TAKE A PRINTOUT.
            <asp:Button ID="btnOk" runat="server" Text="OK" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
