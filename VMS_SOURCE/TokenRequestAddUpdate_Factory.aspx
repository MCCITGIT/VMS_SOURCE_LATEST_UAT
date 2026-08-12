<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TokenRequestAddUpdate_Factory.aspx.vb" Inherits="TokenRequestAddUpdate_Factory" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <title>Token Vendor Add/Update</title>


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
    <script type="text/javascript" language="javascript" src="Scripts/ValidateTokenVendorAddUpdate.js?key="&<%= DateTime.Now.ToString %> ></script>
    <script src="Scripts/ValidateTokenRequestAddUpdate_Factory.js?time=&<%= DateTime.Now.ToString %>" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function disableBackButton() {
            window.history.forward(1);
        }
        //-->
        function RedirectToListScreen() {
            window.location.href = "TokenRequestList_Factory.aspx";
            return false;
        }
    </script>
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
                            <%-- <td style="width: 75%; vertical-align: middle; padding-left: 20px; font-family: Verdana; font-size: 16px; font-weight: bold;">VENDOR MANAGEMENT SOFTWARE
                            </td>--%>
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

                    <table style="width: 65%;" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="height: 15px; width: 100%; text-align: center;">
                                <%--<h2 style="font-size: 14px; font-weight: bold; color: #6694e2; margin: 0px; font-family: Verdana; text-decoration: underline;">Token Vendor Add/Update</h2>--%>
                            </td>
                        </tr>



                    </table>


                    <table border="0" style="width: 65%" cellpadding="2" cellspacing="1" style="background-color: #ffffff"
                        class="mt">
                        <tr style="text-align: left;">
                            <td>
                                <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <div style="height: 20px; background-color: #66CCFF; font-family: Georgia; font-size: 11pt; color: #fff"
                                    align="center">
                                    Token Requisition Add/Update
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table border="0" style="width: 100%" cellspacing="1" class="mt">


                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Factory Name :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlFactory" runat="server" CssClass="dropDown" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnCartonCapacity" runat="server" />
                                                            <span id="Span8" runat="server" class="mandatory">*</span>
                                                            <asp:HiddenField ID="hdnSessionId" runat="server" />
                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>


                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Vendor Name :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="dropDown" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <span id="Span9" runat="server" class="mandatory">*</span>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlFactory" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>

                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Requisition Month :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlRequisitionMonth" runat="server" CssClass="dropDown">
                                                            </asp:DropDownList>
                                                            <span id="Span10" runat="server" class="mandatory">*</span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>

                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Requisition Year :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlRequisitionYear" runat="server" CssClass="dropDown">
                                                            </asp:DropDownList>
                                                            <span id="Span11" runat="server" class="mandatory">*</span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Token Type :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlTokenType" runat="server" CssClass="dropDown">
                                                            </asp:DropDownList>
                                                            <span id="Span2" runat="server" class="mandatory">*</span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Token Month :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropDown">
                                                    </asp:DropDownList>
                                                    <span id="Span1" runat="server" class="mandatory">*</span>
                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Token Year :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropDown">
                                                    </asp:DropDownList>
                                                    <span id="Span5" runat="server" class="mandatory">*</span>
                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Product Name :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="dropDown" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <span id="Span6" runat="server" class="mandatory">*</span>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlVendor" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Pack Size Name :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlPackSize" runat="server" CssClass="dropDown" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                            <span id="Span7" runat="server" class="mandatory">*</span>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlProduct" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Denomination Name:</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlValue" runat="server" CssClass="dropDown">
                                                            </asp:DropDownList>
                                                            <span id="Span4" runat="server" class="mandatory">*</span>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 50%">
                                                    <span style="color: black">Total Quantity :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:TextBox ID="txtQuantity" runat="server" Width="100px"></asp:TextBox>
                                                    <span id="Span12" runat="server" class="mandatory">*</span>
                                                </td>
                                            </tr>

                                            </tr>

                                            <tr>
                                                <td colspan="2" style="text-align: left; padding-left: 10px;">
                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblNB" runat="server" ForeColor="Red" Text="* Total quantity against requisition can not exceed more than 1 lac.  "></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>

                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                    </table>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="text-align: center;" colspan="2">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add"
                                                BackColor="#99CCFF" ForeColor="Black" Font-Bold="true" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnBack" runat="server" Text="Back"
                                                BackColor="#99CCFF" ForeColor="Black" Font-Bold="true" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left; padding-left: 10px;">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblValidationMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table style="width: 95%; margin: 0px auto; border: 1px solid #bde2e5">
                        <tr>
                            <td style="border: thin solid black; text-align: center; background-color: #66CCFF;" class="tlheader_1">Requisition Details 
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTokenDetails" runat="server" AutoGenerateColumns="False" BorderWidth="1" CssClass="table table-hover upgradDataGrid" EmptyDataText="No record(s) found." AllowPaging="False" ShowFooter="False">
                                                <RowStyle CssClass="tlrowlight" />
                                                <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <FooterStyle CssClass="footerGrid" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Factory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFactory" runat="server" Text='<%# Eval("factory_code") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnFactoryCode" runat="server" Value='<%#Eval("tm_factory_code")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendor" runat="server" Text='<%# Eval("vendor_code") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnVendorCode" runat="server" Value='<%#Eval("tm_vendor_code")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Token Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTokenType" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnTokenType" runat="server" Value='<%#Eval("tm_type")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Token Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("token_month") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnTokenMonth" runat="server" Value='<%#Eval("tm_token_month")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Token Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("token_year") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnTokenYear" runat="server" Value='<%#Eval("tm_token_year")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproduct" runat="server" Text='<%# Eval("product") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnProduct" runat="server" Value='<%#Eval("tm_product")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pack Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPacksize" runat="server" Text='<%# Eval("pack") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnPackSize" runat="server" Value='<%#Eval("tm_pack")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Denomination">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("denomination") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnTokenValue" runat="server" Value='<%#Eval("tm_denomination")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnQuantity" runat="server" Value='<%#Eval("tm_qty")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requisition Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequisitionMonth" runat="server" Text='<%# Eval("requisition_month") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnRequisitionMonth" runat="server" Value='<%#Eval("tm_requisition_month")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requisition Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequisitionYear" runat="server" Text='<%# Eval("requisition_year") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnRequisitionYear" runat="server" Value='<%#Eval("tm_requisition_year")%>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnCmdRemove" runat="server" Text="Remove" title="Remove" BackColor="Red"
                                                                CommandName="CmdRemove" OnClientClick="return confirm('Are you sure to remove?')"
                                                                CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td align="center">
                                <asp:Button ID="btnSubmit" CssClass="but1" runat="server" Text="Submit" Width="100px" />
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" BackColor="#99CCFF" ForeColor="Black" runat="server" Text="Submit" Width="100px" />
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
