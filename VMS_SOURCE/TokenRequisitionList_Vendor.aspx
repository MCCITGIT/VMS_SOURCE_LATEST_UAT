<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TokenRequisitionList_Vendor.aspx.vb" Inherits="TokenRequisitionList_Vendor" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <title>Token Requisition List (Vendor)</title>


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
                                <h2 style="font-size: 14px; font-weight: bold; color: #6694e2; margin: 0px; font-family: Verdana; text-decoration: underline;">Token Despatch List (Vendor)</h2>
                            </td>
                        </tr>


                    </table>

                    <table style="width: 85%;" border="0" cellspacing="0" cellpadding="0">

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
                                                    <td style="width: 20%;">Requisition Id
                                                    </td>
                                                    <td style="width: 20%;">Vendor Name
                                                    </td>
                                                    <td style="width: 20%;">Unit Name</td>


                                                    <td style="width: 20%;">Despatch Id
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlVendorRequisition" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlTokenVendor" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlVendorUnit" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    </td>

                                                    <td>&nbsp;<asp:DropDownList ID="ddlDespatchId" Font-Names="Verdana" Font-Size="11px" runat="server">
                                                    </asp:DropDownList>
                                                    </td>


                                                </tr>
                                            </table>
                                            <%-- </ContentTemplate></asp:UpdatePanel>--%>
                                        </td>
                                        <td style="width: 10%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ImageUrl="images/ic_search.gif" ID="imgbtnSearch" ToolTip="Search" runat="server" />
                                                        &nbsp;
                                            &nbsp;
                                            <asp:ImageButton ImageUrl="~/images/ic_add.gif" Visible="false" ToolTip="Make a new despatch" ID="imgbtnAdd" runat="server" />
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

                    <table style="width: 85%;" border="0" cellspacing="0" cellpadding="0">

                        <tr>
                            <td style="text-align: center; border: solid 1px #d7d7d7; padding: 5px; background-color: #f9f9f9; width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvRequistionList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="true" OnPageIndexChanging="gvProductList_PageIndexChanging" OnRowCommand="gvTokenVendorList_RowCommand" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                                                <RowStyle CssClass="tlrowlight" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <FooterStyle CssClass="footerGrid" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Requisition Id" ControlStyle-Width="70%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" Text='<%# Bind("tdh_requisition_id")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Despatch Id" ControlStyle-Width="70%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequistionId" Text='<%# Bind("tdh_despatch_id")%>' runat="server" />

                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Qty." ControlStyle-Width="90%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalDespatchQty" Text='<%# Bind("total_despatch_qty")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Name" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" Text='<%# Bind("unit_name")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="9%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Courrier Name" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoOfitems" Text='<%# Bind("tdh_transporter")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <%--                   <asp:TemplateField HeaderText="Truck No." ControlStyle-Width="90%" >
                                            <ItemTemplate>
                                                 <asp:Label ID="lblTotalQty" Text='<%# Bind("tdh_truck_no")%>' runat="server" />
                                            </ItemTemplate>

                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Challan No." ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_vendor_challan_no" Text='<%# Bind("tdh_vendor_challan_no")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Challan Date." ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_vendor_challan_date" Text='<%# Bind("tdh_vendor_challan_date")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Road Permit." ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltdh_road_permit" Text='<%# Bind("tdh_road_permit")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" ControlStyle-Width="100%">
                                                        <HeaderTemplate>
                                                            <span>View</span>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnSubmit" ImageUrl="~/images/ic_view.gif" CommandArgument='<%# Bind("tdh_despatch_id")%>' CommandName="EditRequisition" Style="width: 35%" ToolTip="View" runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Width="100%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print" ControlStyle-Width="100%">
                                                        <HeaderTemplate>
                                                            <span>Print</span>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnPrint" ImageUrl="~/images/printButton.png" CommandName="Print" CommandArgument='<%# Bind("tdh_despatch_id")%>' Style="width: 35%" ToolTip="Print" runat="server" />
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
                                        <asp:PostBackTrigger ControlID="gvRequistionList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                    </table>



                </td>

            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>

                            <td style="text-align: center">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    BackColor="#99CCFF" ForeColor="Black" Font-Bold="true" PostBackUrl="~/TokenVendorRequisitionList.aspx" /></td>
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
