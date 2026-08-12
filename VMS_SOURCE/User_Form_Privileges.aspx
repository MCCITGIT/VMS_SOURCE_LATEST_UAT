<%@ Page Language="VB" AutoEventWireup="false" CodeFile="User_Form_Privileges.aspx.vb" Inherits="User_Form_Access" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: User Form Privileges ::</title>
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left" valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="top" style="background-color: #ffffff">
                                <!-- Table for Logo Starts -->
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <!--<td style="width:180" align="center">
                <asp:Image ID="Image1" ImageUrl="~/images/inner_logo.gif" Width="149px" Height="74px" runat="server" />
            </td>-->
                                        <td valign="bottom" style="width: 90%">
                                            <asp:Image ID="Image2" ImageUrl="~/images/inner_tag.jpg" runat="server" />
                                        </td>
                                        <td style="width: 10%" align="center" valign="bottom">
                                            <asp:ImageButton ID="ImageButton1" AlternateText="Home" ImageUrl="~/images/home_new.png" PostBackUrl="~/Home.aspx" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- Table for Logo Ends -->
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 1"></td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 380" align="center" valign="top">
                                <table width="70%" border="0" cellpadding="0" cellspacing="0" class="ss" style="border-collapse: collapse; border-color: #111111">
                                    <tr>
                                        <td style="height: 25" align="left" colspan="2">User Form Access </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px; background-color: #339999"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="0" style="height: 50px;">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top">
                                            <table width="800px" border="0" cellpadding="0" cellspacing="0" class="mt" style="border-collapse: collapse; border-color: #111111">

                                                <tr style="height: 25px">
                                                    <td style="width: 400px;">&nbsp;UserGroup:&nbsp;<asp:DropDownList ID="ddlUsrGrp" Width="150px" CssClass="dropDown" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                    </td>
                                                    <td colspan="3" style="width: 400px;" align="right"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top" colspan="2">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvUsrFrmAccess" runat="server" AutoGenerateColumns="false" AllowPaging="False" ShowFooter="true"
                                                    Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid"
                                                    OnRowCancelingEdit="gvUsrFrmAccess_RowCancelingEdit" OnRowEditing="gvUsrFrmAccess_RowEditing">
                                                    <RowStyle CssClass="tlrowlight" />
                                                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                    <HeaderStyle CssClass="headerGrid" />
                                                    <FooterStyle CssClass="footerGrid" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sl.No." HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNo" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Form name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFormName" runat="server" Text='<%# Bind("FORM_DESC") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnFormCode" runat="server" Value='<%# Bind("FORM_CODE") %>' />
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtFormName" CssClass="txtBox" runat="server" Width="75px" Text='<%# Bind("FORM_DESC") %>'></asp:TextBox>
                                </EditItemTemplate>   --%>                         
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Read" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkRead" runat="server" Width="75px" Enabled="false" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="ChkRead" runat="server" Width="75px" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Add" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkAdd" runat="server" Width="75px" Enabled="false" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="ChkAdd" runat="server" Width="75px" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkEdit" runat="server" Width="75px" Enabled="false" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="ChkEdit" runat="server" Width="75px" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkDelete" runat="server" Width="75px" Enabled="false" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="ChkDelete" runat="server" Width="75px" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkPrint" runat="server" Width="75px" Enabled="false" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="ChkPrint" runat="server" Width="75px" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Approval" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkApproval" runat="server" Width="75px" Enabled="false" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="ChkApproval" runat="server" Width="75px" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Quick Link" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlQuickLink" runat="server" Width="75px" Enabled="false" DataValueField='<%# Bind("QUICK_LINK") %>'>
                                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlQuickLink" runat="server" Width="75px" DataValueField='<%# Bind("QUICK_LINK") %>'>
                                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" CommandName="edit" runat="server" ImageUrl="~/Images/edit.jpg" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ID="btnUpdate" CommandName="update" runat="server" ImageUrl="~/Images/b_save.gif" />
                                                                <asp:ImageButton ID="btnCancel" CommandName="cancel" runat="server" ImageUrl="~/Images/b_cancel.gif" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div id="Div_Usr_Frm_Access_Grid" runat="server" visible="false">
                                    <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color: #7BD1FC" width="800px">
                                        <tr style="height: 20px" class="gridList_Norecord">
                                            <td style="width: 114px; height: 20px;" align="center">Sl No</td>
                                            <td style="width: 114px; height: 20px;" align="center">Form Name</td>
                                            <td style="width: 114px; height: 20px;" align="center">Read</td>
                                            <td style="width: 114px; height: 20px;" align="center">Add</td>
                                            <td style="width: 114px; height: 20px;" align="center">Edit </td>
                                            <td style="width: 114px; height: 20px;" align="center">Delete</td>
                                            <td style="width: 114px; height: 20px;" align="center">Print</td>
                                            <td style="width: 114px; height: 20px;" align="center">Approval</td>
                                            <td style="width: 114px; height: 20px;" align="center">QuickLink</td>
                                        </tr>
                                        <tr style="height: 20px">
                                            <td colspan="9">No Records Found</td>
                                        </tr>
                                    </table>
                                </div>


                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="height: 25" align="center"><span class="mt">
                                <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
                            </span></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
