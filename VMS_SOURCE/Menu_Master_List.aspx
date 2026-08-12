<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Menu_Master_List.aspx.vb" Inherits="Menu_Master_List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidationMenuMaster.js"></script>
    <script type="text/javascript" src="Scripts/Messages.js"></script>
    <script type="text/javascript" src="Scripts/RegEX.js"></script>
    <script type="text/javascript" src="Scripts/date.js"></script>
    <script type="text/javascript" src="Scripts/AjaxServices.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Form Menu Master</h3>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Parent Form:</label>
                        <asp:DropDownList ID="ddlParentForm" AutoPostBack="true" OnSelectedIndexChanged="ddlParentForm_SelectedIndexChanged" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <asp:Label ID="Label3" runat="server" Style="color: Red; font-size: small; font-weight: bold;"></asp:Label>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:Label ID="lblLOVCode" runat="server" Style="color: Red; font-size: small; font-weight: bold;"></asp:Label>
            <asp:Label ID="Label1" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div id="divErrorMessage"></div>

            <div class="table-responsive">
                <asp:GridView ID="gvMenuMaster" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                    Visible="true" ShowFooter="true" BorderWidth="1px" EmptyDataText="There are No Data..."
                    OnRowCancelingEdit="gvMenuMaster_RowCancelingEdit"  CssClass="table table-hover upgradDataGrid">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                         <asp:TemplateField HeaderText="Parent Form" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblParent" runat="server" Text='<%# Bind("parentFormName") %>'></asp:Label>
                             
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList CssClass="form-control" ID="ddlParent" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlParent_ftr" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Form Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFrmName" runat="server" Text='<%# Bind("fmm_name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                   <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("fmm_id") %>'></asp:HiddenField>
                                <asp:TextBox ID="txtFrmName" CssClass="form-control" runat="server" Text='<%# Bind("fmm_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFrmName_ftr" CssClass="form-control" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Form Link" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFrmLink" runat="server" Text='<%# Bind("fmm_link") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFrmLink" CssClass="form-control" runat="server" Text='<%# Bind("fmm_link") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFrmLink_ftr" CssClass="form-control" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sequence" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblFrmSeq" runat="server" Text='<%# Bind("fmm_sequence") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFrmSeq" CssClass="form-control" runat="server" Text='<%# Bind("fmm_sequence") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtFrmSeq_ftr" CssClass="form-control" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblActive" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList CssClass="form-control" ID="ddlActive" runat="server">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlActive_ftr" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" CommandName="edit" runat="server" CssClass="btn btn-primary gridBtn">Edit</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" CommandName="update" runat="server" CssClass="btn btn-success gridBtn">Save</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" CommandName="cancel" runat="server" CssClass="btn btn-secondary gridBtn">Cancel</asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="btnInsert" CommandName="insert" runat="server" CssClass="btn btn-success gridBtn">Save</asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
