<%@ Page Title="Product Vendor Linking" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ProductVendorLinking.aspx.vb" Inherits="ProductVendorLinking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/validateProductVendorLinking.js"></script>
    <script type="text/javascript">
        function checkAllProduct(checkbox) {
            var cbl = document.getElementById('<%=chkbxListApplProducts.ClientID%>').getElementsByTagName("input");
            for (i = 0; i < cbl.length; i++) cbl[i].checked = checkbox.checked;
        }
    </script>

    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                if (document.getElementById('btnSubmit').disabled == true)
                    return false;
                else {
                    // button Add (F7 keypress)
                    validateBrandProductLinkAdd();
                }
                //__doPostBack(document.getElementById('btnSubmit').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('btnCancel').name, '');
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }
    </script>
    <script type="text/javascript" src="Scripts/validateProductVendorLinking.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Product Vendor Linking</h3>
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
                                <label class="form-control-label">Vendor:</label>
                                <asp:DropDownList ID="ddlVendor" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="form-group">
                                <label class="form-control-label">Search Text:</label>
                                <input type="text" class="form-control" id="searchInput" placeholder="Type to search..." oninput="searchText()">
                            </div>
                        </div>
                    </div>
                    <label class="form-control-label">Select Products:</label>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="CheckBoxList">
                                    <asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" CssClass="checkAll" Text="Select All" onclick="checkAllProduct(this)" />
                                    <asp:CheckBoxList ID="chkbxListApplProducts" Enabled="false" runat="server" TabIndex="14" RepeatColumns="4"
                                        RepeatDirection="Horizontal" Width="100%" AutoPostBack="False">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Submit" CssClass="btn btn-primary btn-sm" />
                            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary btn-sm" />
                            <asp:Button ID="btnReset" runat="server" Visible="false" Text="Reset" CssClass="btn btn-warning btn-sm" />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
