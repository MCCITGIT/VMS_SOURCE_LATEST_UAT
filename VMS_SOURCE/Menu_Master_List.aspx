<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Menu_Master_List.aspx.vb" Inherits="Menu_Master_List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <link href="includes/menu-master-cards.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidationMenuMaster.js"></script>
    <script type="text/javascript" src="Scripts/Messages.js"></script>
    <script type="text/javascript" src="Scripts/RegEX.js"></script>
    <script type="text/javascript" src="Scripts/date.js"></script>
    <script type="text/javascript" src="Scripts/AjaxServices.js"></script>

    <div class="mm-page">

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Form Menu Master</h3>
                <p class="pageSubTitle">Maintain application forms and menu entries</p>
            </div>
        </div>
    </div>

    <div class="card mm-panel">
        <div class="card-body mm-panel-body">
            <div class="mm-filter-grid">
                <div class="mm-field">
                    <div class="form-group">
                        <label class="form-control-label">Parent Form:</label>
                        <asp:DropDownList ID="ddlParentForm" AutoPostBack="true" OnSelectedIndexChanged="ddlParentForm_SelectedIndexChanged" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <asp:Label ID="Label3" runat="server" Style="color: Red; font-size: small; font-weight: bold;"></asp:Label>
        </div>
    </div>

    <div class="card mm-panel">
        <div class="card-body mm-panel-body">
            <asp:Label ID="lblLOVCode" runat="server" Style="color: Red; font-size: small; font-weight: bold;"></asp:Label>
            <asp:Label ID="Label1" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div id="divErrorMessage"></div>

            <asp:GridView ID="gvMenuMaster" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                Visible="true" ShowFooter="true" ShowHeader="false" BorderWidth="0" GridLines="None"
                EmptyDataText="There are No Data..."
                OnRowCancelingEdit="gvMenuMaster_RowCancelingEdit" CssClass="gv-cards">
                <RowStyle CssClass="tlrowlight" />
                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                <HeaderStyle CssClass="headerGrid" />
                <FooterStyle CssClass="footerGrid" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <article class="mm-card">
                                <header class="mm-card-head">
                                    <div class="mm-identity">
                                        <span class="mm-avatar" aria-hidden="true"><i class="fas fa-th-list"></i></span>
                                        <div>
                                            <span class="mm-title">
                                                <asp:Label ID="lblFrmName" runat="server" Text='<%# Bind("fmm_name") %>'></asp:Label>
                                            </span>
                                            <span class="mm-parent">
                                                <span class="mm-kicker">Parent Form</span>
                                                <asp:Label ID="lblParent" runat="server" Text='<%# Bind("parentFormName") %>'></asp:Label>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="mm-head-meta">
                                        <div class="mm-seq">
                                            <span class="mm-seq-label">Sequence</span>
                                            <span class="mm-seq-value">
                                                <asp:Label ID="lblFrmSeq" runat="server" Text='<%# Bind("fmm_sequence") %>'></asp:Label>
                                            </span>
                                        </div>
                                        <div class="mm-status" data-state='<%# Eval("active") %>'>
                                            <span class="mm-badge">
                                                <asp:Label ID="lblActive" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                                            </span>
                                        </div>
                                        <asp:LinkButton ID="btnEdit" CommandName="edit" runat="server" CssClass="btn btn-primary gridBtn">Edit</asp:LinkButton>
                                    </div>
                                </header>
                                <div class="mm-card-body">
                                    <div class="mm-link-tile">
                                        <span class="mm-field-label"><i class="fas fa-link"></i> Form Link</span>
                                        <span class="mm-link-value">
                                            <asp:Label ID="lblFrmLink" runat="server" Text='<%# Bind("fmm_link") %>'></asp:Label>
                                        </span>
                                    </div>
                                </div>
                            </article>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <article class="mm-card is-edit">
                                <div class="mm-edit-grid">
                                    <div class="mm-edit-field">
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Bind("fmm_id") %>'></asp:HiddenField>
                                        <label>Parent Form</label>
                                        <asp:DropDownList CssClass="form-control" ID="ddlParent" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="mm-edit-field">
                                        <label>Form Name</label>
                                        <asp:TextBox ID="txtFrmName" CssClass="form-control" runat="server" Text='<%# Bind("fmm_name") %>'></asp:TextBox>
                                    </div>
                                    <div class="mm-edit-field">
                                        <label>Form Link</label>
                                        <asp:TextBox ID="txtFrmLink" CssClass="form-control" runat="server" Text='<%# Bind("fmm_link") %>'></asp:TextBox>
                                    </div>
                                    <div class="mm-edit-row">
                                        <div class="mm-edit-field">
                                            <label>Sequence</label>
                                            <asp:TextBox ID="txtFrmSeq" CssClass="form-control" runat="server" Text='<%# Bind("fmm_sequence") %>'></asp:TextBox>
                                        </div>
                                        <div class="mm-edit-field">
                                            <label>Active</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlActive" runat="server">
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <footer class="mm-card-foot">
                                    <asp:LinkButton ID="btnUpdate" CommandName="update" runat="server" CssClass="btn btn-success gridBtn">Save</asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" CommandName="cancel" runat="server" CssClass="btn btn-secondary gridBtn">Cancel</asp:LinkButton>
                                </footer>
                            </article>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <article class="mm-card is-add">
                                <div class="mm-add-grid">
                                    <div class="mm-add-field">
                                        <label>Parent Form</label>
                                        <asp:DropDownList ID="ddlParent_ftr" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="mm-add-field">
                                        <label>Form Name</label>
                                        <asp:TextBox ID="txtFrmName_ftr" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="mm-add-field">
                                        <label>Form Link</label>
                                        <asp:TextBox ID="txtFrmLink_ftr" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="mm-add-field">
                                        <label>Sequence</label>
                                        <asp:TextBox ID="txtFrmSeq_ftr" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="mm-add-field">
                                        <label>Active</label>
                                        <asp:DropDownList ID="ddlActive_ftr" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="mm-add-actions">
                                        <asp:LinkButton ID="btnInsert" CommandName="insert" runat="server" CssClass="btn btn-success gridBtn">Save</asp:LinkButton>
                                    </div>
                                </div>
                            </article>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    </div>
</asp:Content>
