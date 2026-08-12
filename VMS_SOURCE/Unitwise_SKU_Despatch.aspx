<%@ Page Title="Sourcewise SKU Despatch" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Unitwise_SKU_Despatch.aspx.vb" Inherits="Unitwise_SKU_Despatch" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" src="Scripts/ValidationDepotDespatchUnitwise.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Sourcewise SKU Despatch</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:DropDownList ID="ddlDsptchdUnit" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Process Year:<span id="Span03" class="mandatory">* (yyyy)</span></label>
                                <asp:TextBox ID="txtFinYear" runat="server" Columns="50" CssClass="form-control" MaxLength="4" Rows="1" TabIndex="5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Process Month:<span id="Span3" class="mandatory">* (mm)</span></label>
                                <asp:TextBox ID="txtMonth" runat="server" Columns="50" CssClass="form-control" MaxLength="2" Rows="1" TabIndex="5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Format:</label>
                                <asp:DropDownList ID="ddlPrntOptn" CssClass="form-control select2" runat="server" AppendDataBoundItems="True">
                                    <%--<asp:ListItem>Select Print Option</asp:ListItem>--%>
                                    <asp:ListItem Value="PdfFormat">PDF</asp:ListItem>
                                    <asp:ListItem Value="ExcelFormat" Selected="True">Excel</asp:ListItem>
                                    <asp:ListItem Value="WordFormat">Word</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">RDB:</label>
                                <div class="checkRadioGroup">
                                    <asp:RadioButton ID="rdbSummary" runat="server" GroupName="reg" Text="Summary" />
                                    <asp:RadioButton ID="rdbDptWise" runat="server" GroupName="reg" Checked="true" Text="Sourcewise" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="divErrMsg1" class="errormsg"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" TabIndex="31" runat="server" Text="Download" />
                            <asp:Button ID="btnReset" CssClass="btn btn-secondary btn-sm" TabIndex="32" runat="server" Text="Cancel" />
                        </div>
                    </div>
                    <div class="row" id="tblrental">
                        <div class="col-md-12">
                            <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
