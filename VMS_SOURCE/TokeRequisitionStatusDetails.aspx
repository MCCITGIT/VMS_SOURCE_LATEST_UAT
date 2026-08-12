<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TokeRequisitionStatusDetails.aspx.vb" Inherits="TokeRequisitionStatusDetails" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <title>Token Requisition Received Add/Update</title>


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
    <%--<script type="text/javascript" language="javascript" src="Scripts/ValidateTokenReceiveAddUpdate.js?key=&<%= DateTime.Now.ToString %>" ></script>--%>

    <script language="javascript" type="text/javascript">

        //document.onkeydown = checkValue;
        //function checkValue() {
        //    if (event.keyCode == 118) { // button Add (F7 keypress)
        //        __doPostBack(document.getElementById('imgbtnAdd').name, '');
        //    }
        //    else if (event.keyCode == 119) {
        //        __doPostBack(document.getElementById('imgbtnSearch').name, '');
        //    }
        //}

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
                                <h2 style="font-size: 14px; font-weight: bold; color: #6694e2; margin: 0px; font-family: Verdana; text-decoration: underline;">Token Requisition Status Details</h2>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 100%;">&nbsp;</td>
                        </tr>


                    </table>


                    <table border="0" style="width: 55%" cellpadding="2" cellspacing="1" style="background-color: #ffffff"
                        class="mt">
                        <tr style="text-align: left;">
                            <td>
                                <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                                <%-- <div style="float:right"><div style="    height: 12px;
    width: 12px;
    float: left;
    margin-right: 4px;
    margin-top: 1px;
    background: lightgreen;"></div><span style="float:right">Full Received</span> </div>  --%>                         
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <div style="height: 20px; background-color: #66CCFF; font-family: Georgia; font-size: 11pt; color: #fff"
                                    align="center">
                                    Token Requisition Details
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table border="0" style="width: 100%" cellspacing="1" class="mt">
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 20%">
                                                    <span style="color: black">Requisition Id :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:Label runat="server" ID="lblReqId">Autogenerated

                                                    </asp:Label>

                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 20%">
                                                    <span style="color: black">Unit Name :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:DropDownList ID="ddlVendorUnit" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />

                                                </td>
                                            </tr>
                                            <%-- <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 20%">
                                                    <span style="color: black">Site Name :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:DropDownList ID="ddlSite" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />

                                                </td>
                                            </tr>--%>

                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 20%">
                                                    <span style="color: black">Vendor Name :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:DropDownList ID="ddlTokenVendor" Font-Names="Verdana" Font-Size="11px" runat="server" AutoPostBack="True" />



                                                </td>
                                            </tr>
                                            <tr class="tdfloat" align="left">
                                                <td class="style5" style="width: 20%">
                                                    <span style="color: black">Description :</span>
                                                </td>
                                                <td style="background-color: #FFFFFF; height: 30px; text-align: left; font-size: 11px; font-weight: bold; font-family: Verdana; height: 15px;" class="clsTDbg" align="left">
                                                    <asp:TextBox ID="txtDesc" TextMode="MultiLine" Style="height: 70px; width: 250px" runat="server"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="ddlDespatch" />
                                                                                    <asp:PostBackTrigger ControlID="ddlRequisition" />
                                                                                    <asp:PostBackTrigger ControlID="ddlTokenVendor" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                    </table>
                    <table style="width: 55%;" border="0" cellspacing="0" cellpadding="0">

                        <tr>
                            <td style="text-align: center; border: solid 1px #d7d7d7; padding: 5px; background-color: #f9f9f9; width: 100%;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvRequisitionItemsList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="false" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
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
                                                            <%-- <asp:HiddenField ID="hdnProductId" Value='<%# Bind("productId") %>' runat="server" />
                                                <asp:HiddenField ID="hdnUnit" Value='<%# Bind("unit") %>' runat="server" />     
                                                <asp:HiddenField ID="hdnTokenVendor" Value='<%# Bind("tokenVendor") %>' runat="server" />
                                                  <asp:HiddenField ID="hdnDespatchId" Value='<%# Bind("tdd_despatch_id") %>' runat="server" />--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("sku_desc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="13%" />
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

                                                    <asp:TemplateField HeaderText="Req Qty" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReqQty" placeholder="0" Text='<%# Bind("qty") %>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hdnReqQty" Value='<%# Bind("qty") %>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Despatched Qty" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDespatchedQty" placeholder="0" Text='<%# Bind("DespatchQty")%>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hdnDespatchedQty" Value='<%# Bind("DespatchQty")%>' runat="server" />
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt Qty" ControlStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceivedQty" placeholder="0" Text='<%# Bind("ReceivedQty")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>

                                                        <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
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
                                            <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                                BackColor="#99CCFF" ForeColor="Black" Font-Bold="true" />--%>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" PostBackUrl="~/TokenRequisitionStatusList.aspx"
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
