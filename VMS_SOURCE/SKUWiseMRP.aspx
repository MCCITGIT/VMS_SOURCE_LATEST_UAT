<%@ Page Title="SKU Wise M.R.P." Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SKUWiseMRP.aspx.vb" Inherits="SKUWiseMRP" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">SKU wise MRP Export </h3>
                <p class="pageSubTitle">Export maximum retail price by SKU</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12 text-center py-4">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" TabIndex="31" runat="server" Text="Export To Excel" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" TabIndex="32" runat="server" Text="Cancel" />
                    <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function clearNotification() {
            document.getElementById("<%=lblErrMsg.ClientID%>").innerText = "";
        }
    </script>
</asp:Content>
