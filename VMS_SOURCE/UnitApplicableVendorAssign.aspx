<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UnitApplicableVendorAssign.aspx.vb" Inherits="UnitApplicableVendorAssign" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <title>Unit Applicable Vendor Assign</title>


    <script type="text/javascript" src="Scripts/anchorposition.js"></script>
    <script type="text/javascript" src="Scripts/popupwindow.js"></script>
    <script type="text/javascript" src="Scripts/calendarpopup.js"></script>
    <script type="text/javascript" src="Scripts/date.js"></script>
    <script type="text/javascript" src="Scripts/Currency.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/Messages.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/RegEX.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/AjaxServices.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/Autocomplete.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/ValidateUnitApplicableVendorAssign.js?key="&<%= DateTime.Now.ToString %> ></script>

    <script language="javascript" type="text/javascript">

        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                __doPostBack(document.getElementById('imgbtnAdd').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('imgbtnSearch').name, '');
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }

    </script>

</head>

<body onload="disableBackButton();">
    <form id="form1" runat="server" submitdisabledcontrols="true">



        <table style="width: 100%; margin: 0px;" border="0" cellspacing="0" cellpadding="0">

            <%-- Header Row --%>
            <tr>
                <td style="background-color: #f9f9f9; width: 100%;" align="center">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width: 5%;"></td>
                            <td style="width: 10%;">
                                <img src="images/berger-paints-logo.png" alt=""
                                    style="height: 81px; width: 119px" />
                            </td>
                            <td style="width: 75%; vertical-align: middle; padding-left: 20px; font-family: Verdana; font-size: 16px; font-weight: bold;">VENDOR MANAGEMENT SOFTWARE
                            </td>
                            <td style="width: 10%; text-align: center;">
                                <a href="Home.aspx">
                                    <img src="images/home_new.png" alt="Home" width="56px"
                                        height="58px" style="border: 0px;" /></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td style="width: 100%;">&nbsp;</td>
            </tr>

            <%-- Content Row --%>
            <tr>
                <td style="width: 100%;" align="center">

                    <table style="width: 85%;" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="height: 15px; width: 100%; text-align: center;">
                                <h2 style="font-size: 14px; font-weight: bold; color: #6694e2; margin: 0px; font-family: Verdana; text-decoration: underline;">Unit Applicable Vendor Assign (HO)</h2>
                            </td>
                        </tr>



                        <tr>
                            <td style="width: 100%;">&nbsp;</td>
                        </tr>

                    </table>

                    <table style="width: 75%;" border="0" cellspacing="0" cellpadding="0">

                        <tr style="text-align: left;">
                            <td>
                                <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-family: BodoniPS; font-size: 16px; text-align: center;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 85%">
                                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>--%>
                                            <table style="width: 100%; text-align: center;" class="mt">
                                                <tr style="background-color: #E6F5FB; height: 20px;">
                                                    <td style="width: 20%;">Unit
                                                    </td>
                                                    <td style="width: 40%;">Sku Code</td>

                                                    <td style="width: 20%;">Active
                                                    </td>



                                                </tr>
                                                <tr>

                                                    <td>&nbsp;<asp:DropDownList ID="ddlVendorUnit" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                        <span id="Span1" class="mandatory">*</span>
                                                    </td>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlVendorProduct" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>&nbsp;
                                            
                                                    </td>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlActive" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                        <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>

                                                    </asp:DropDownList>&nbsp;
                                                    </td>



                                                </tr>
                                            </table>
                                            <%-- </ContentTemplate></asp:UpdatePanel>--%>
                                        </td>
                                        <td style="width: 10%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" />
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>


                        <tr>
                            <td style="width: 100%;">&nbsp;</td>
                        </tr>

                    </table>

                    <table style="width: 75%;" border="0" cellspacing="0" cellpadding="0">

                        <tr>
                            <td style="text-align: center; border: solid 1px #d7d7d7; padding: 5px; background-color: #f9f9f9; width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTokenVendorList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                                                <RowStyle CssClass="tlrowlight" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <FooterStyle CssClass="footerGrid" />
                                                <Columns>
                                                    <%-- <asp:TemplateField HeaderText="Depot" ControlStyle-Width="90%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepot" runat="server" Text='<%# Bind("v_depot") %>'></asp:Label>
                                            </ItemTemplate>

                                            <ControlStyle Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="6%" Height="50px" />
                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Product">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSku" runat="server" Text='<%# Bind("sku_new_code")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnskuCode" Value='<%# Bind("sku_new_code")%>' runat="server" />
                                                            <asp:HiddenField ID="hdnUnit" Value='<%# Bind("unit") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnActive" Value='<%# Bind("status") %>' runat="server" />

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("sku_prd_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="12%" />
                                                    </asp:TemplateField>

                                                    <%--      <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductDesc" runat="server" Text='<%# Bind("sku_desc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Pack size (Kl.)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPackSize" runat="server" Text='<%# Bind("sku_volume") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("v_vendor_unit") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnTokenVendor" Value='<%# Bind("tokenVendor") %>' runat="server" />
                                                            <asp:DropDownList ID="ddlTokenVendor" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ControlStyle-Width="100%">
                                                        <HeaderTemplate>
                                                            <span>Action</span>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnSubmit" ImageUrl="~/images/b_save.gif" CommandName="AssignUnitVendor" Style="width: 65%" ToolTip="Save" runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Width="100%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
                                        <asp:PostBackTrigger ControlID="gvTokenVendorList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                    </table>



                </td>
            </tr>

            <tr>
                <td style="width: 100%;">&nbsp;</td>
            </tr>

            <%-- Footer Row --%>
            <tr>
                <td>
                    <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
                </td>
            </tr>
        </table>
    </form>
</body>

</html>
