<%@ Page Title="Brand Test Linking" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BrandTestCaseLinking.aspx.vb" Inherits="BrandTestCaseLinking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<%--    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidationLovDetails.js"></script>--%>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Brand Test Linking</h3>
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
                                <label class="form-control-label">Brand:</label>
                                <asp:DropDownList ID="ddlBrand" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Product:</label>
                                <asp:DropDownList ID="ddlProduct" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView BorderWidth="1" CssClass="table table-hover upgradDataGrid" ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="true" ShowFooter="false" EmptyDataText="" GridLines="both">
                                    <RowStyle CssClass="tlrowlight" />
                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <FooterStyle CssClass="footerGrid" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Test Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnLinkId" Value='<%# Bind("link_id") %>' />
                                                <asp:Label ID="lblTestName" runat="server" Text='<%# Bind("test_name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdnTestCode" Value='<%# Bind("test_id") %>' />
                                                <asp:HiddenField runat="server" ID="hdnLinkId" Value='<%# Bind("link_id") %>' />
                                                <%-- <asp:DropDownList ID="ddlTest" runat="server" class="form-control">
                                            </asp:DropDownList>--%>
                                                <asp:Label ID="lblTestName" runat="server" Text='<%# Bind("test_name") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlTest" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref Value" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRefValue" runat="server" Text='<%# Bind("refvalue") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblRefValue" runat="server" Text='<%# Bind("refvalue") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblRefValue" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Test Seq." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("test_seq") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSeq" class="form-control" runat="server" Text='<%# Bind("test_seq") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtSeq" class="form-control" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActive" runat="server" Text='<%# Bind("active_yn") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlActiveYn" class="form-control" runat="server" DataValueField='<%# Bind("active_yn") %>'>
                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlActiveYn" runat="server" class="form-control">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" CommandName="edit" runat="server" CssClass="btn btn-info gridBtn" Text="Edit" title="Edit" ToolTip="Edit"></asp:Button>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" CommandName="update" runat="server" CssClass="btn btn-success gridBtn" Text="Save" title="Save" ToolTip="Save"></asp:Button>
                                                <asp:Button ID="btnCancel" CommandName="cancel" runat="server" CssClass="btn btn-secondary gridBtn" Text="Cancel" title="Cancel" ToolTip="Cancel"></asp:Button>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnInsert" CommandName="insert" runat="server" CssClass="btn btn-success gridBtn" Text="Save" title="Save" ToolTip="Save"></asp:Button>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="Div_Lov_Details_Grid" runat="server" visible="false">
                        <div class="col-md-12">
                            <table class="table table-hover upgradDataGrid" border="1">
                                <tr class="headerGrid" id="trTableHeader" runat="server">
                                    <th style="width: 20%; text-align: left;">Test Name</th>
                                    <th style="width: 40%; text-align: left;">Ref Value</th>
                                    <th style="width: 15%; text-align: center;">Test Seq.</th>
                                    <th style="width: 10%; text-align: center;">Active</th>
                                    <th style="width: 15%; text-align: center;">Action</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList class="form-control select2" ID="ddlTest" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td style="text-align: center; width: 40%;">
                                        <asp:Label ID="lblRefValue" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="text-align: center; width: 15%;">
                                        <asp:TextBox ID="txtSeq" class="form-control" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center; width: 10%;">
                                        <asp:DropDownList class="form-control select2" ID="ddlActiveYn" runat="server">
                                            <asp:ListItem Value="Y" Text="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: center; width: 15%;">
                                        <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-success gridBtn" Text="Save" title="Save" ToolTip="Save"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblErrorMessage1" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage1"></div>
                        </div>
                    </div>
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
