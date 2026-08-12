<%@ Page Title="Depot Mail Master List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Depot_Mail_Master.aspx.vb" Inherits="Depot_Mail_Master" MaintainScrollPositionOnPostback="true" %>



<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidateDepotManagerMail.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Depot Manager Mail Master</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Depot:</label>
                        <asp:DropDownList ID="ddlDepot" runat="server" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblDmId" runat="server" Style="color: Red; font-size: small; font-weight: bold;"></asp:Label>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvDepotMail" runat="server" AutoGenerateColumns="false" OnRowCommand="gvDepotMail_RowCommand" OnRowUpdating="gvDepotMail_RowUpdating"
                    AllowPaging="true" PageSize="15" OnPageIndexChanging="gvDepotMail_PageIndexChanging" OnRowDataBound="gvDepotMail_RowDataBound"
                    Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" ShowFooter="true" EmptyDataText="There are No Data...">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Region" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("Region") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("Region") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlregion_ftr" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlregion_ftr_SelectedIndexChanged"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Depot Code" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDepotCode" runat="server" Text='<%# Bind("Depot_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblDepotCode" runat="server" Text='<%# Bind("Depot_Name") %>'></asp:Label>
                                <asp:HiddenField runat="server" ID="hdndepot" Value='<%# Bind("Depot_Code") %>' />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddldepot_ftr" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mail-ID" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblMailid" runat="server" Text='<%# Bind("MailId") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" Text='<%# Bind("MailId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtemail" CssClass="form-control" runat="server" Text='<%# Bind("MailId") %>'></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%--<asp:ImageButton ID="btnEdit" CommandName="edit" CommandArgument='<%# Container.DataItemIndex %>' runat="server" ImageUrl="~/Images/edit.jpg" />--%>
                                <asp:LinkButton CssClass="btn btn-primary gridBtn" ID="btnEdit" CommandName="edit" CommandArgument='<%# Container.DataItemIndex %>' runat="server">Edit</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%--<asp:ImageButton ID="btnUpdate" CommandName="update" CommandArgument='<%# Container.DataItemIndex %>' runat="server" ImageUrl="~/Images/b_save.gif" />
                                <asp:ImageButton ID="btnCancel" CommandName="cancel" CommandArgument='<%# Container.DataItemIndex %>' runat="server" ImageUrl="~/Images/b_cancel.gif" />--%>
                                <asp:LinkButton CssClass="btn btn-success gridBtn" ID="btnUpdate" CommandName="update" CommandArgument='<%# Container.DataItemIndex %>' runat="server">Save</asp:LinkButton>
                                <asp:LinkButton CssClass="btn btn-secondary gridBtn" ID="btnCancel" CommandName="cancel" CommandArgument='<%# Container.DataItemIndex %>' runat="server">Cancel</asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <%--<asp:ImageButton ID="btnInsert" CommandName="insert" runat="server" ImageUrl="~/Images/b_save.gif" />--%>
                                <asp:LinkButton CssClass="btn btn-success gridBtn" ID="btnInsert" CommandName="insert" runat="server">Save</asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div id="Div_Lov_Mstr_Grid" runat="server" visible="false">
                    <table border="1" class="table table-hover upgradDataGrid">
                        <tr class="headerGrid">
                            <th style="text-align: center;">Region</th>
                            <th style="text-align: center;">Depot Code</th>
                            <th style="text-align: center;">Mail-ID</th>
                            <th style="text-align: center;"></th>
                        </tr>
                        <tr class="tlrowlight">
                            <td style="text-align: center;">
                                <asp:DropDownList ID="ddlrgn_norc" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlrgn_norc_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="text-align: center;">
                                <asp:DropDownList ID="ddldepot_norc" CssClass="form-control select2" runat="server"></asp:DropDownList>
                            </td>
                            <td style="text-align: center;">
                                <asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: center;">
                                <asp:ImageButton ID="btnInsert" runat="server" ImageUrl="~/Images/b_save.gif" />
                            </td>
                        </tr>
                        <tr class="tlrowlight">
                            <td colspan="4">
                                <asp:Label ID="lblErrorMessage1" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                                <div id="divErrorMessage1"></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div id="divErrorMessage"></div>
        </div>
    </div>
</asp:Content>
