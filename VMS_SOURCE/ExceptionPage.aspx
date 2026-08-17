<%@ Page Title="Exception Page" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ExceptionPage.aspx.vb" Inherits="ExceptionPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationFlashMaster.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Exception</h3>
                <p class="pageSubTitle">Details of the error that occurred</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div id="frmException" runat="server" class="excpPageView">
                <img src="images/exception.png" class="excpImg" alt="Error" />
                <asp:Label ID="lblErr" runat="server" class="excpTx">An error has ocurred.Contact Administrator.</asp:Label>
                <asp:Button ID="btnBack" CssClass="btn btn-secondary btn-sm" runat="server" Text="Back" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

