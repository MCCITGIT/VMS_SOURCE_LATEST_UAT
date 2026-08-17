<%@ Page Title="Welcome to Vendor Management System" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Menu.aspx.vb" Inherits="Menu" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationFlashMaster.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Logout Page</h3>
                <p class="pageSubTitle">Session sign out</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <frameset framespacing="0" border="0" rows="20,*" target="main" frameborder="0">
		<frame name="main" scrolling="no" noresize target="main" src="Top.aspx">
		<frame name="main" src="Home.aspx" scrolling="yes" noresize>
		<noframes>
			<body topmargin="0" leftmargin="0">
				<p>This page uses frames, but your browser doesn't support them.</p>
			</body>
		</noframes>
	</frameset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
