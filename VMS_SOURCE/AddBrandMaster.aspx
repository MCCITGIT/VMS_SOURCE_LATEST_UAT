<%@ Page Title="Add Brand Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="AddBrandMaster.aspx.vb" Inherits="AddBrandMaster" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                if (document.getElementById('btnSubmit').disabled == true)
                    return false;
                else {
                    // button Add (F7 keypress)
                    validateSKUList();
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
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateAddUpdateBrandMaster.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Add Brand Master</h3>
                <p class="pageSubTitle">Create and update brand records</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group pb-0">
                        <label class="form-control-label">Brand Name:<span id="Span12" class="mandatory">*</span></label>
                        <asp:TextBox ID="txtBrandName" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="txtBrandId" ClientIDMode="Static" runat="server" />
                    </div>
                </div>
                <div class="col-md-8 form-btn-mt">
                    <asp:Button ID="btnSubmit" ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm"  />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary btn-sm" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning btn-sm" />
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView BorderWidth="1" CssClass="table table-hover upgradDataGrid" CellSpacing="0" CellPadding="0" ID="gvbrandDetails" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="true" ShowFooter="true" GridLines="both">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblbrandid" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblbrandname" runat="server" Text='<%# Bind("brand_name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" Visible="true">
                            <ItemTemplate>
                                <asp:Button ID="btnEditRow" CommandName="EditRow" Visible="true" runat="server" CssClass="btn btn-info gridBtn" Text="Edit" title="Edit" ToolTip="Click To Edit" CommandArgument='<%# Bind("brand_id")%>'></asp:Button>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
