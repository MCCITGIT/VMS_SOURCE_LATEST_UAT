<%@ Page Title="Token Requisition Summary" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TokenRequisitionSummary.aspx.vb" Inherits="TokenRequisitionSummary" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Token Requisition Summary</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-12 text-center mt-2">
                    <div class="form-group">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" TabIndex="31" runat="server" Text="Export To Excel" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-danger btn-sm" TabIndex="32" runat="server" Text="Cancel" />
                    </div>
                </div>
            </div>
            <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
        </div>
    </div>

    <script type="text/javascript">
        function clearNotification() {
            document.getElementById("<%=lblErrMsg.ClientID%>").innerText = "";
        }
    </script>
</asp:Content>
