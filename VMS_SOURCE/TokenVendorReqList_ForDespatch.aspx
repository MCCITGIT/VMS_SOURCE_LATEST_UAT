<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TokenVendorReqList_ForDespatch.aspx.vb" Inherits="TokenVendorReqList_ForDespatch" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <title>Token Vendor Requisition List</title>


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
    <script type="text/javascript" language="javascript" src="Scripts/ValidateUnitApplicableVendorAssign.js?key=<%= DateTime.Now.ToString %>"></script>

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


        <script type="text/javascript">
            var cal1 = new CalendarPopup();
        </script>
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
                                <h2 style="font-size: 14px; font-weight: bold; color: #6694e2; margin: 0px; font-family: Verdana; text-decoration: underline;">Token Requisition List Of Pending Despatches (Vendor)</h2>

                            </td>
                        </tr>


                    </table>

                    <table style="width: 70%;" border="0" cellspacing="0" cellpadding="0">

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
                                                    <td style="width: 15%;">Vendor Name
                                                    </td>
                                                    <td style="width: 25%;">Unit Name</td>

                                                    <td style="width: 10%;">Requisition Id
                                                    </td>

                                                    <td style="width: 25%;">From Date
                                                    </td>

                                                    <td style="width: 35%;">To Date
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td>
                                                        <asp:DropDownList ID="ddlTokenVendor" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />

                                                    </td>
                                                    <td>


                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlVendorUnit" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>


                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlVendorRequisition" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlVendorUnit" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>


                                                    </td>
                                                    <td>

                                                        <asp:TextBox ID="txtFromDate" CssClass="txtBox" runat="server" ReadOnly="true" Style="cursor: not-allowed" Width="90px" MaxLength="10"></asp:TextBox>
                                                        <a href="javascript:cal1.select(document.forms[0].txtFromDate,'RequisitionFromDate','dd/MM/yyyy');">
                                                            <img src="images/date_icon.gif" id="RequisitionFromDate" alt="Calender" style="border: 0; margin-top: -4px; position: absolute; margin-left: 5px" />
                                                        </a>
                                                    </td>
                                                    <td>

                                                        <asp:TextBox ID="txtTodate" CssClass="txtBox" runat="server" Style="cursor: not-allowed" ReadOnly="true" Width="90px" MaxLength="10"></asp:TextBox>
                                                        <a href="javascript:cal1.select(document.forms[0].txtTodate,'RequisitionToDate','dd/MM/yyyy');">
                                                            <img src="images/date_icon.gif" id="RequisitionToDate" alt="Calender" style="border: 0; margin-top: -4px; position: absolute; margin-left: 5px" />
                                                        </a>
                                                    </td>
                                                    <td></td>


                                                </tr>
                                            </table>
                                            <%-- </ContentTemplate></asp:UpdatePanel>--%>
                                        </td>
                                        <td style="width: 6%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" />
                                                        &nbsp;
                                                          &nbsp;
                                                          
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

                    <table style="width: 70%;" border="0" cellspacing="0" cellpadding="0">

                        <tr>
                            <td style="text-align: center; border: solid 1px #d7d7d7; padding: 5px; background-color: #f9f9f9; width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvRequistionList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                                                <RowStyle CssClass="tlrowlight" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <FooterStyle CssClass="footerGrid" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Requisition Id" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequistionId" Text='<%# Bind("trh_id") %>' runat="server" />

                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requisition Date" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcreated_date" Text='<%# Bind("created_date")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Name" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" Text='<%# Bind("trh_unit") %>' runat="server" />
                                                            <asp:HiddenField runat="server" ID="hdnUnit" Value='<%# Bind("unit_code") %>' />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor Name" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendor" Text='<%# Bind("trh_token_vendor")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site Name" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSiteName" Text='<%# Bind("trh_site_name") %>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" Text='<%# Bind("trh_desc") %>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of items" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoOfitems" Text='<%# Bind("items") %>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Qty." ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalQty" Text='<%# Bind("totalQty") %>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" Visible="false" ControlStyle-Width="100%">
                                                        <HeaderTemplate>
                                                            <span>Action</span>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnSubmit" ImageUrl="~/images/ic_view.gif" CommandArgument='<%# Bind("trh_id") %>' CommandName="EditRequisition" Style="width: 25%" ToolTip="View" runat="server" />
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
                                        <asp:PostBackTrigger ControlID="gvRequistionList" />
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
