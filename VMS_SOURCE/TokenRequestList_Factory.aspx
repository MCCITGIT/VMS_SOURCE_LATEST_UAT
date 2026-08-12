<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TokenRequestList_Factory.aspx.vb" Inherits="TokenRequestList_Factory" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <title>FACTORY TOKEN REQUISITION LIST</title>
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
    <script type="text/javascript" language="javascript" src="Scripts/ValidationIndentList_HO.js"></script>

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
                                <asp:Image ID="Image2" ImageUrl="~/images/inner_tag.jpg" runat="server" />
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
                                <h2 style="font-size: 14px; font-weight: bold; color: #6694e2; margin: 0px; font-family: Verdana; text-decoration: underline;">FACTORY TOKEN REQUISITION LIST</h2>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 65%;" border="0" cellspacing="0" cellpadding="0">
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
                                                    <td style="width: 33%;">Factory :</td>
                                                    <td style="width: 33%;">Vendor :</td>
                                                    <td style="width: 33%;">Status :</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlFactory" Font-Names="Verdana" AutoPostBack="true" Font-Size="11px" runat="server">
                                                    </asp:DropDownList>
                                                        <span id="Span1" class="mandatory">*</span>
                                                    </td>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlVendor" runat="server" CssClass="dropDown"></asp:DropDownList>&nbsp;
                                            
                                                    </td>
                                                    <td>&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDown">
                                                        <asp:ListItem Value="">Select</asp:ListItem>
                                                        <asp:ListItem Value="Y">Generated</asp:ListItem>
                                                        <asp:ListItem Value="N">Not Generated</asp:ListItem>
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
                                                        <asp:ImageButton ImageUrl="images/ic_search.gif" ID="imgbtnSearch" runat="server" />
                                                        &nbsp;
                                                          &nbsp;
                                                                  <asp:ImageButton ImageUrl="~/images/ic_add.gif" ID="imgbtnAdd" runat="server" />
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
                                            <asp:GridView ID="gvTokenRequisitionList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                                                <RowStyle CssClass="tlrowlight" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <FooterStyle CssClass="footerGrid" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Factory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFactory" runat="server" Text='<%# Bind("FactoryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendor" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requisition Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSessionId" runat="server" Text='<%# Bind("ts_session_id") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnSessionId" runat="server" Value='<%# Bind("ts_session_id") %>'></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("TokenStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requisition Date" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequisitionDate" runat="server" Text='<%# Bind("RequisitionDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imgbtnSearch" EventName="Click" />
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
