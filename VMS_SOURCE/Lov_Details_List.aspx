<%@ Page Title="Lov Details List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Lov_Details_List.aspx.vb" Inherits="Lov_Details" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/ValidationLovDetails.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Lov Details List</h3>
                <p class="pageSubTitle">Values maintained under each list of values</p>
            </div>
        </div>
        <div class="rightFung">
            <a href="Lov_Master_List.aspx" title="LOV Details" class="btn btn-success btn-sm">LOV Master</a>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">LOV:</label>
                        <asp:DropDownList ID="ddlLOV" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblLOVCode" runat="server" Style="color: Red; font-size: small; font-weight: bold;"></asp:Label>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvLovDetails" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="true" ShowFooter="true"
                    OnRowCancelingEdit="gvLovDetails_RowCancelingEdit" OnRowEditing="gvLovDetails_RowEditing" EmptyDataText="There are No Data..."
                    OnRowDataBound="gvLovDetails_RowDataBound" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="LOV Code" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblLovCode" runat="server" Text='<%# Bind("lov_code") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" Text='<%# Bind("lov_code") %>'></asp:TextBox>
                                <asp:HiddenField ID="hdntxtCode" runat="server" Value='<%# Bind("lov_code") %>' />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOV Desc" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblLovDesc" runat="server" Text='<%# Bind("lov_shrt_desc") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" Text='<%# Bind("lov_shrt_desc") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOV Value" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblLovValue" runat="server" Text='<%# Bind("lov_value") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" CssClass="form-control" Text='<%# Bind("lov_value") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtValue" runat="server" CssClass="form-control"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOV Seq" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblLovSeq" runat="server" Text='<%# Bind("lov_value_seq") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSeq" runat="server" CssClass="form-control" Text='<%# Bind("lov_value_seq") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSeq" runat="server" CssClass="form-control"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field1 Type" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblField1" runat="server" Text='<%# Bind("lov_field1_value") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtField1" runat="server" CssClass="form-control" Text='<%# Bind("lov_field1_value") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtField1" runat="server" CssClass="form-control"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field2 Type" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblField2" runat="server" Text='<%# Bind("lov_field2_value") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtField2" runat="server" CssClass="form-control" Text='<%# Bind("lov_field2_value") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtField2" runat="server" CssClass="form-control"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Field3 Type" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblField3" runat="server" Text='<%# Bind("lov_field3_value") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtField3" runat="server" CssClass="form-control" Text='<%# Bind("lov_field3_value") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtField3" runat="server" CssClass="form-control"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblActive" runat="server" Text='<%# Bind("active") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control" DataTextField='<%# Bind("active") %>' DataValueField='<%# Bind("active") %>'>
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
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

                <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                <div id="divErrorMessage"></div>

                <div id="Div_Lov_Details_Grid" runat="server" visible="false">
                    <table class="table table-hover upgradDataGrid" border="1">
                        <tbody>
                            <tr class="headerGrid">
                                <th style="text-align: center;">LOV Code</th>
                                <th style="text-align: center;">LOV Desc</th>
                                <th style="text-align: center;">LOV Value</th>
                                <th style="text-align: center;">LOV Seq</th>
                                <th style="text-align: center;">Field1 Type </th>
                                <th style="text-align: center;">Field2 Type</th>
                                <th style="text-align: center;">Field3 Type</th>
                                <th style="text-align: center;">Active</th>
                                <th style="text-align: center;">Action</th>
                            </tr>
                            <tr class="tlrowlight">
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtType" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtValue" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtSeq" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtField1" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtField2" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:TextBox ID="txtField3" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: center;">
                                    <asp:LinkButton ID="btnInsert" runat="server" CssClass="btn btn-success gridBtn">Save</asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="tlrowlight">
                                <td style="text-align: center;" colspan="9">
                                    <asp:Label ID="lblErrorMessage1" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                                    <div id="divErrorMessage1"></div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
