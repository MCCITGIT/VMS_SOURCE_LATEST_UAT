<%@ Page Title="Stock Upload Summary" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Upload_Summary.aspx.vb" Inherits="Stock_Upload_Summary" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationStockUploadSummary.js"></script>

    <script type="text/javascript">
        function fnNewWindow(url, target) {
            window.open(url, target);
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Load Summary Report</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Process Year:<span class="mandatory" id="span1">*</span></label>
                        <asp:TextBox ID="txtProcessYear" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Process Month:<span class="mandatory" id="spanTDate">*</span></label>
                        <asp:TextBox ID="txtProcessMonth" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Report Format:</label>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="form-control select2" Enabled="False">
                            <asp:ListItem Value="PdfFormat" Selected="True">PDF</asp:ListItem>
                            <asp:ListItem Value="ExcelFormat">EXCEL</asp:ListItem>
                            <asp:ListItem Value="WordFormat">WORD</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 form-btn-mt">
                    <div class="form-group">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                    </div>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
