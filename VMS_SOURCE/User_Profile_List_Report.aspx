<%@ Page Title="User Profile List Report" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="User_Profile_List_Report.aspx.vb" Inherits="User_Profile_List_Report" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" src="Scripts/MessagesIII.js"></script>
    <script type="text/javascript" src="Scripts/ValidationFromToReportDates.js"></script>

    <script type="text/javascript">
        function fnNewWindow(url, target) {
            window.open(url, target);
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">User Profile Report</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Region:</label>
                                <asp:DropDownList ID="ddlRegion" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Depot:</label>
                                <asp:DropDownList ID="ddlDepot" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Report Format:</label>
                                <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Selected="True" Value="PdfFormat">PDF</asp:ListItem>
                                    <asp:ListItem Value="ExcelFormat">EXCEL</asp:ListItem>
                                    <asp:ListItem Value="WordFormat">WORD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                        </div>
                    </div>

                    <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>


        </div>
    </div>
</asp:Content>


