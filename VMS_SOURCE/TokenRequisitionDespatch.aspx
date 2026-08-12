<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TokenRequisitionDespatch.aspx.vb" Inherits="TokenRequisitionDespatch" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <title>Token Vendor Requisition Despatch (Vendor)</title>


    <script type="text/javascript" src="Scripts/anchorposition.js"></script>
    <script type="text/javascript" src="Scripts/popupwindow.js"></script>
    <script type="text/javascript" src="Scripts/calendarpopup.js"></script>
    <script type="text/javascript" src="Scripts/date.js"></script>
    <script type="text/javascript" src="Scripts/Currency.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/Messages.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/RegEX.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/date.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/AjaxServices.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/Autocomplete.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/ValidateTokenVendorDespatchAddUpdate.js?key=&<%= DateTime.Now.ToString %>"></script>
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
        <script type="text/javascript">var cal1 = new CalendarPopup();</script>


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
                                <h2 style="font-size: 14px; font-weight: bold; color: #6694e2; margin: 0px; font-family: Verdana; text-decoration: underline;">Token Vendor Requisition Despatch (Vendor)</h2>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 100%;">&nbsp;</td>
                        </tr>


                    </table>


                    <table border="0" style="width: 85%" cellpadding="2" cellspacing="1" style="background-color: #ffffff" class="mt">
                        <tr style="text-align: left;">
                            <td>
                                <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                                <div style="float: right">
                                    <div style="height: 12px; width: 12px; float: left; margin-right: 4px; margin-top: 1px; background: lightgreen;">
                                    </div>
                                    <span style="float: right">Full Despatched</span>
                                </div>
                            </td>



                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <div style="height: 20px; background-color: #66CCFF; font-family: Georgia; font-size: 11pt; color: #fff"
                                    align="center">
                                    Token Despatch Details
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%; text-align: center; margin: 0px auto;">



                                    <tr>
                                        <td align="left" style="width: 50%" valign="top">

                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" style="width: 100%" cellspacing="1" class="mt">
                                                        <tr class="tdfloat" align="left">
                                                            <td class="style5" style="width: 50%">
                                                                <span style="color: black">Vendor Name :</span>
                                                            </td>
                                                            <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                                <asp:DropDownList ID="ddlTokenVendor" Enabled="false" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />


                                                            </td>
                                                        </tr>


                                                        <tr class="tdfloat" align="left">
                                                            <td class="style5" style="width: 50%">
                                                                <span style="color: black">Unit Name :</span>
                                                            </td>
                                                            <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                                <asp:DropDownList ID="ddlVendorUnit" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />

                                                            </td>
                                                        </tr>
                                                        <tr class="tdfloat" align="left">
                                                            <td class="style5" style="width: 50%">
                                                                <span style="color: black">Site Name :</span>
                                                            </td>
                                                            <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                                <asp:Label ID="lblSite" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />

                                                            </td>
                                                        </tr>
                                                        <tr class="tdfloat" align="left">
                                                            <td class="style5" style="width: 50%">
                                                                <span style="color: black">Requisition Id :</span>
                                                            </td>
                                                            <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                                <asp:DropDownList ID="ddlVendorRequisition" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />

                                                            </td>
                                                        </tr>
                                                        <tr class="tdfloat" runat="server" visible="false" align="left">
                                                            <td class="style5" style="width: 50%">
                                                                <span style="color: black">Product Name :</span>
                                                            </td>
                                                            <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                                <asp:DropDownList ID="ddlProduct" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />

                                                            </td>
                                                        </tr>
                                                        <tr class="tdfloat" style="display: none" align="left">
                                                            <td class="style5" style="width: 50%">
                                                                <span style="color: black">Pack size (Kl.) :</span>
                                                            </td>
                                                            <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                                <asp:DropDownList ID="ddlPack_Size" Font-Names="Verdana" Font-Size="11px" Visible="false" runat="server" AutoPostBack="True" />

                                                            </td>
                                                        </tr>
                                                        <tr class="tdfloat" align="left">
                                                            <td class="style5" style="width: 50%">
                                                                <span style="color: black">Courrier Name :</span>
                                                            </td>
                                                            <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                                <asp:TextBox ID="txt_transporter" Style="width: 200px" runat="server"></asp:TextBox>
                                                                <span id="Span3" class="mandatory">*</span>
                                                            </td>
                                                        </tr>


                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 50%;">
                                            <table style="width: 100%;">
                                                <tr class="tdfloat" align="left">
                                                    <td class="style5" style="width: 50%">
                                                        <span style="color: black">Despatch Id :</span>
                                                    </td>
                                                    <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                        <asp:Label runat="server" ID="lblReqId">**Autogenerated**

                                                        </asp:Label>

                                                    </td>
                                                </tr>

                                                <%--    <tr class="tdfloat" align="left">
                                                                                            <td class="style5" style="width:50%">
                                                                                                <span style="color:black">Truck No :</span>
                                                                                            </td>
                                                                                            <td style="background-color: #FFFFFF; height: 30px;text-align:left; font-size:11px;  font-weight:bold; font-family:Verdana; height:15px;" class="clsTDbg" align="left">
                                                                                                <asp:TextBox ID="txt_truck_no"  Style="width:200px" runat="server"></asp:TextBox>
                                                                                                  <span id="Span4" class="mandatory"> *</span> 
                                                                                            </td>
                                                                                        </tr>--%>
                                                <tr class="tdfloat" align="left">
                                                    <td class="style5" style="width: 50%">
                                                        <span style="color: black">Vendor Challan No :</span>
                                                    </td>
                                                    <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                        <asp:TextBox ID="txt_vendor_challan_no" Style="width: 200px" runat="server"></asp:TextBox>
                                                        <span id="Span5" class="mandatory">*</span>
                                                    </td>
                                                </tr>

                                                <tr class="tdfloat" align="left">
                                                    <td class="style5" style="width: 50%">
                                                        <span style="color: black">Vendor Challan Date :</span>
                                                    </td>
                                                    <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                        <asp:TextBox ID="txtChallanDate" CssClass="txtBox" Style="width: 70px; cursor: not-allowed" MaxLength="10" TabIndex="28" runat="server"></asp:TextBox>
                                                        <span class="mandatory" id="spanDtSprtn">*</span>
                                                        <a id="aCalendar" runat="server" href="javascript:cal1.select(document.forms[0].txtChallanDate,'ChallanDate','dd/MM/yyyy');">
                                                            <img src="images/date_icon.gif" id="ChallanDate" alt="Date" style="border: 0;" />
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr class="tdfloat" align="left">
                                                    <td class="style5" style="width: 50%">
                                                        <span style="color: black">Road Permit :</span>
                                                    </td>
                                                    <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                        <asp:TextBox ID="txt_road_permit" Style="width: 200px" runat="server"></asp:TextBox>
                                                        <span id="Span6" class="mandatory">*</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>

                    </table>
                    <table style="width: 85%;" border="0" cellspacing="0" cellpadding="0">

                        <tr>
                            <td style="text-align: center; border: solid 1px #d7d7d7; padding: 5px; background-color: #f9f9f9; width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvRequisitionItemsList" runat="server" OnRowCreated="gvRequisitionItemsList_RowCreated" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="false" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
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
                                                            <asp:Label ID="lblSku" runat="server" Text='<%# Bind("sku_new_code") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnProductId" Value='<%# Bind("sku_new_code") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnUnit" Value='<%# Bind("unit") %>' runat="server" />

                                                            <asp:HiddenField ID="hdnTokenVendor" Value='<%# Bind("tokenVendor") %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("sku_desc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>

                                                    <%--  <asp:TemplateField HeaderText="Description">
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

                                                    <%--  <asp:TemplateField HeaderText="Unit" ControlStyle-Width="90%" >
                                            <ItemTemplate>
                                               <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("v_vendor_unit") %>'></asp:Label>
                                            </ItemTemplate>

                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:TemplateField>--%>
                                                    <%--               <asp:TemplateField HeaderText="Token Vendor" ControlStyle-Width="90%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblTokenVendor" Text='<%# Bind("tokenVendorName") %>' runat="server" />
                                                <asp:DropDownList ID="ddlTokenVendor" Enabled="false" style="cursor:not-allowed" Visible="false" runat="server"></asp:DropDownList>
                                            </ItemTemplate>

                                            <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Total Req. Qty" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtQty" Enabled="false" placeholder="0" Text='<%# Bind("qty") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pending Qty" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPendingQty" Enabled="false" ReadOnly="true" placeholder="0" Text='<%# Bind("pending_qty") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Despatched Qty" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDespatched" Enabled="false" ReadOnly="true" placeholder="0" Text='<%# Bind("tdd_despatch_qty") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty to be Despatched" ControlStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDespatchQty" AutoPostBack="true" OnTextChanged="txtDespatchQty_TextChanged" placeholder="0" Text='<%# Bind("pending_qty") %>' runat="server"></asp:TextBox>
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Action" ControlStyle-Width="100%" >
                                                      <HeaderTemplate>
                                                          <span>Action</span>
                                                         
                                                      </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnSubmit" ImageUrl="~/images/b_save.gif" CommandName="AssignUnitVendor" Style="width:65%" ToolTip="Save" runat="server" />
                                            </ItemTemplate>

                                            <ControlStyle  Width="100%"></ControlStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                                        </asp:TemplateField> --%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvRequisitionItemsList" />
                                    </Triggers>
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
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                                BackColor="#99CCFF" ForeColor="Black" Font-Bold="true" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                                BackColor="#99CCFF" ForeColor="Black" Font-Bold="true" /></td>
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
