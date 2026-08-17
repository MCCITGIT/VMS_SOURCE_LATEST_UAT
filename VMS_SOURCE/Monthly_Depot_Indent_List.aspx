<%@ Page Title="Monthly Depot Indent List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Monthly_Depot_Indent_List.aspx.vb" Inherits="Monthly_Depot_Indent_List" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <script type="text/javascript" src="Scripts/ValidationMonthlyDepotIndentList.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Monthly Indent List</h3>
                <p class="pageSubTitle">Depot indents summarised by month</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Region:</label>
                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Depot:</label>
                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Year:<span id="Span3" class="mandatory">* (yyyy)</span></label>
                                <asp:TextBox ID="txtFinYear" runat="server" Columns="50" CssClass="form-control" MaxLength="4" Rows="1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Month:<span id="Span" class="mandatory">* (mm)</span></label>
                                <asp:TextBox ID="txtMonth" runat="server" Columns="50" CssClass="form-control select2" MaxLength="2" Rows="1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Format:</label>
                                <asp:DropDownList ID="ddlPrntOptn" CssClass="form-control select2" runat="server" AppendDataBoundItems="True">
                                    <%--<asp:ListItem>Select Print Option</asp:ListItem>--%>
                                    <asp:ListItem Value="PdfFormat">PDF</asp:ListItem>
                                    <asp:ListItem Value="ExcelFormat" Selected="True">Excel</asp:ListItem>
                                    <%--<asp:ListItem Value="WordFormat">Word</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" TabIndex="31" runat="server" Text="Submit" />
                    <asp:Button ID="btnReset" CssClass="btn btn-danger btn-sm" TabIndex="32" runat="server" Text="Cancel" />
                </div>
            </div>
            <div id="tblrental" runat="server">
                <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            </div>
            <div id="divErrMsg1" class="errormsg"></div>
        </div>
    </div>
</asp:Content>
