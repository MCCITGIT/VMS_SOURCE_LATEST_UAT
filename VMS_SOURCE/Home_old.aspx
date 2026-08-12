<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Home_old.aspx.vb" Inherits="Home_old" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:: Welcome to Berger Color Bank Inventory Management::</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="robots" content="noindex">
    <meta name="googlebot" content="noindex">
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function fnWindowSpan(strUrl, strtarget) {
            window.open(strUrl, strtarget, "status=no,toolbar=no,menubar=no,location=no,scrollbars=no,modal=no,resizable=no");
        }
        function newwindow(strUrl, strtarget) {
            var docwindow = window.open(strUrl, strtarget, 'width=600,height=400,toolbar=no,location=yes,directories=yes,status=yes,menubar=no,scrollbars=yes,copyhistory=yes,resizable = yes');

        }

        function tdhide() {
            document.getElementById("tblguide").style.display = 'none';
            blink = 1;
        }
        var blink;
        function tdshow() {

            if (blink == 1) {
                document.getElementById("tblguide").style.display = 'block';
                blink = 2;
            }
            else if (blink == 2) {
                document.getElementById("tblguide").style.display = 'none';
                blink = 1;
            }

        }
    </script>
    <style type="text/css">
        .box {
            width: 30%;
            height: 60px;            
            background-color: white;
            margin: 25px auto 15px;
            border-radius: 5px;
            padding-top:1px;
        }

            .box h3 a {
                font-family: 'Didact Gothic', sans-serif;
                font-weight: normal;
                text-align: center;
                padding-top: 10px;
                color: #07466d;
                font-size:10pt;
            }

        .box3 {
            background-color: #9EEBA1;
        }

        .shadow3 {
            position: relative;
        }

        .shadow3 {
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.3), 0 0 20px rgba(0, 0, 0, 0.1) inset;
        }
            /*****************************************************************dashed border
****************************************************************/
            .shadow3 h3 {
                width: 87%;
                margin-left: 6%;
                /*border: 2px dashed #F7EEEE;*/
                border-radius: 5px;
            }
            /****************************************************************
*styling shadows
****************************************************************/
            .shadow3:before, .shadow3:after {
                content: "";
                position: absolute;
                bottom: 0;
                top: 2px;
                left: 15px;
                right: 15px;
                z-index: -1;
                border-radius: 100px/30px;
                -webkit-box-shadow: 0 0 30px 2px #479F41;
                -moz-box-shadow: 0 0 30px 2px #479F41;
                box-shadow: 0 0 30px 2px #479F41;
            }
    </style>
</head>
<body style="background-color: #ffd0d0;" onload="tdhide();">
    <form id="form1" method="post" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" height="462">
                                    <!--Top Logo header starts here -->

                                    <!--Top Logo header ends here -->
                                    <tr>
                                        <td align="left" valign="top" height="367" width="321">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="height: 1px; background-color: #40c7ff"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bl">
                                                                        <tr>
                                                                            <td style="height: 30px; background-color: #005783">&nbsp;&nbsp<img alt="" src="images/welcome.gif" width="228" height="21" /></td>
                                                                            <td style="width: 25px; background-image: url('images/left_top.gif')">&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="background-color: #004060; height: 7px"></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align="center" valign="top">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="table1">
                                                            <tr>
                                                                <td align="left" valign="top">
                                                                    <table width="232" border="0" cellspacing="0" cellpadding="0" height="100%" id="table2">
                                                                        <tr>
                                                                            <td align="left" style="width: 10%; height: 22px"></td>
                                                                            <td style="width: 90%; height: 22px; color: #000000;" colspan="2" valign="top">
                                                                                <script type="text/javascript" src="Scripts/MENU_OCF.js">
																				//function IMG1_onclick() {
                                                                                </script>
                                                                                <asp:Literal ID="_clientScript" runat="server"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td align="left" valign="top" height="367" width="478">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="left" valign="top"><%--<img alt="" src="images/inner_logo.jpg" width="478" height="74"/>--%></td>
                                                </tr>
                                                <%--<tr>
													<td style="background-color:#004060; height:3px"></td>
												</tr>--%>
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bl">
                                                            <tr>
                                                                <td align="left">
                                                                    <img alt="" src="images/flash_news.jpg" width="160"
                                                                        height="20" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 1; background-color: #ffffff"></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bl">
                                                                        <tr>
                                                                            <td style="width: 1%">
                                                                                <img alt="" src="images/news_left.jpg" height="106px" /></td>
                                                                            <td style="background-image: url('images/news_bg.gif'); background-repeat: repeat-x; height: 106px; width: 98%">

                                                                                <span id="news_marquee_scroll" runat="server" class="bl" style="margin-left: 10px;">&nbsp;</span>
                                                                            </td>
                                                                            <td style="width: 1%">
                                                                                <img alt="" src="images/news_right.jpg" height="106px" /></td>
                                                                        </tr>
                                                                        <!--tr>															    
																<td style="background-image:url(images/news_bg.gif); height:53px">
																    <div style="MARGIN-LEFT:10px;"><strong>Today's Closed Deals:</strong></div>
																    <span id="closed_marquee_scroll" runat="server" style="MARGIN-LEFT:150px">&nbsp;</span>
																</td>
															</tr-->
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="height: 10px"></td>
                                                </tr>
                                                <tr>
                                                    <!--td><asp:AdRotator id="headrotator" runat="server" width="478" height="150" AdvertisementFile="headrotator.xml"></asp:AdRotator></td-->
                                                </tr>
                                                <tr>
                                                    <td style="height: 5px"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="478px" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 229px" align="left" valign="top">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <img alt="" src="images/quick_links.jpg" width="110" height="24" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="1"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="1" bgcolor="#f9f9f9;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="1"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="tdQuickLink" runat="server" align="left" height="8" class="mt" style="background-color: #f9f9f9; color: #000000;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top" style="background-color: #537392">
                                                                                <table width="95%" border="0" cellpadding="0" cellspacing="0" class="chi">
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="background-color: #ffffff" height="1"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                                <td width="20">&nbsp;</td>
                                                                <td width="229" align="left" valign="top">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <img alt="" src="images/action_req.jpg" width="118" height="24" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="1"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="1" bgcolor="#ffffff"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" height="1" style="background-color: #f9f9f9"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="tdActionReq" runat="server" align="left" class="chi" style="background-color: #f9f9f9;" height="8"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" valign="top" style="background-color: #f9f9f9;">
                                                                                <table width="95%" border="0" cellpadding="0" cellspacing="0" class="bl">
                                                                                    <tr>
                                                                                        <td height="1" align="left"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="1" align="left" style="background-color: #f9f9f9;"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="background-color: #ffffff" height="1"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblComplainRegistrationLink" runat="server">
                                                <tr>
                                                    <td>
                                                        <div class="wrap" style="text-align:center;">
                                                            <div class="box box3 shadow3">
                                                                <h3><a href="https://bpilsharepoint1.bergerindia.com:97" target="_blank" title="For Product Complaint Click Here">Complain/BTC</a></h3>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 10px"></td>
                                        <td align="left" valign="top" height="367" width="402">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <!--tr>
													<td><img src="images/house2.gif" width="253" height="38" alt="" /></td>
												</tr>
												<tr-->

                                                <td align="left" valign="top">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="height: 1px; background-color: #00ec54"></td>
                                                        </tr>
                                                        <tr>

                                                            <td align="left" valign="top" style="background-color: #02a33c">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 16px; height: 35px; background-image: url('images/green_bg.gif'); background-color: #02a33c" align="left"></td>
                                                                        <td align="center">

                                                                            <img alt="" title="Click to view System Help Guide" src="images/icon_user_guide.gif" width="30" height="30" style="vertical-align: middle; cursor: hand" onclick=" newwindow('bstracker help menu/user_help.html','blank')" /></td>

                                                                        <td style="width: 2px">
                                                                            <img alt="" src="images/v_line.gif" width="2" height="11" /></td>

                                                                        <td align="center">
                                                                            <img alt="" src="images/arrow2.gif" width="3" height="5" style="vertical-align: middle" />
                                                                            <span style="color: White; cursor: hand; font-size: 9pt" onclick=" fnWindowSpan('Contacts.html','blank')"><strong>Contact</strong></span></td>
                                                                        <td style="width: 2px">
                                                                            <img alt="" src="images/v_line.gif" width="2" height="11" /></td>
                                                                        <td align="center">
                                                                            <img alt="" src="images/arrow2.gif" width="3" height="5" style="vertical-align: middle" />
                                                                            <span style="color: White; cursor: hand; font-size: 8pt" onclick=" fnWindowSpan('Faqs_info.html','blank')"><strong>FAQ's</strong></span></td>
                                                                        <td style="width: 2px">
                                                                            <img alt="" src="images/v_line.gif" width="2" height="11" /></td>
                                                                        <td align="center">
                                                                            <img alt="" src="images/arrow2.gif" width="3" height="5" style="vertical-align: middle" />
                                                                            <a href="Logout.aspx" class="bl" target="_parent"><strong>Signout</strong></a></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table width="90%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <!--td align="left" valign="top">
																    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bl">
																		<!--tr>
																			<td align="left"><img alt="" src="images/today_reg.gif" width="162px" height="19px"/></td>
																		</tr>
																		<tr>
																			<td align="left" style="height:1px"></td>
																		</tr>
																		<tr>
																			<td align="left" style="height:1px; background-color:#ffffff"></td>
																		</tr>
																		<tr>
																			<td align="left" style="height:1px"></td>
																		</tr>
																		<tr>
																			<td style="height:60px; background-color:#537392;" align="left">																			
																			    <span id="reg_marquee_scroll" runat="server" style="MARGIN-LEFT:5px">&nbsp;</span>
																			</td>
																		</tr-->
                                            </table>
                                        </td>
                                    </tr>
                                    <%--<tr>
																<td style="height:8px"></td>
															</tr>--%>
                                    <tr>
                                        <td align="left" valign="top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="bl">
                                                <tr>
                                                    <td align="center" style="background-color: #d7d7d7; border-bottom: 1px solid #999999; vertical-align: middle;">
                                                        <span style="font-family: Verdana; font-weight: bold; font-size: 12px; height: 25px; color: #000000;">STOCK AS ON :</span>
                                                    </td>
                                                </tr>
                                                <tr align="center" valign="top">
                                                    <td style="background-color: #f9f9f9; height: 50px; vertical-align: middle;">
                                                        <asp:Label ID="lblLastStockUpdateDate" runat="server" Font-Names="Verdana"
                                                            Font-Size="12px" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td onclick="tdshow();" style="cursor: hand">
                                            <img alt="" src="images/m_guildlines.gif" width="231" height="21" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" width="100%">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="1">
                                                <tr>
                                                    <td>
                                                        <table id="tblguide" border="0" cellspacing="2" cellpadding="2" width="100%" align="left" style="background-color: #4F97D1">
                                                            <%--<tr align="left"><td align="left"  bgcolor="#e1e1e1" width="100%" style="font-family:Arial;font-size:9pt"><a href="includes/Mc_Booking_format.xlsx">Machine Booking Format</a></td></tr>--%>
                                                            <%--<tr align="left"><td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('includes/Mc_Booking_format.xlsx','blank')">How to create a Purchase Order</td></tr>--%>
                                                            <%--<tr align="left"><td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/depot_receipt_from Thirdparty.htm','blank')">How do I receive the stocks in 3rd Party location against Purchase Order</td></tr>--%>
                                                            <%--<tr>
																				<td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/IDT-TO-Despatch.htm','blank')">How do I despatch  the gift items to Depots - IDT (Consolidated Despatch)</td>
																			</tr>
																			<tr align="left"><td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/Depot_despatch_advice.htm','blank')">How do I despatch  the gift items to Depots - (Third Party)</td></tr>
																			<tr align="left"><td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/Purchase Order.htm','blank')">How do I adjust my stock incase of Physical Discrepancy</td></tr>--%>
                                                            <%--<tr>
																				<td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/depot_receipt_from Thirdparty.htm','blank')">How do I receive stocks from 3rd Party Despatch Locations  </td>
																			</tr>
																			<tr>
																				<td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/IDT-TO-Receive.htm','blank')">How do I receive stocks from regional depot </td>
																			</tr>--%>
                                                            <%--<tr align="left"><td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/depot_receipt_from Vendor.htm','blank')">How do I receive stocks from Vendor against a PO </td></tr>
																			<tr align="left"><td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/Dealer advice.htm','blank')">How do I create Dealer Despacth Advice</td></tr>
																			<tr align="left"><td  class="info_tddark" align="left"  bgcolor="#4F97D1" wrap width="100%" style="font-family:Arial;font-size:9pt" onclick=" newwindow('bstracker help menu/Dealer advice.htm','blank')">How do i Acknowledge Dealer Confirmation</td></tr>--%>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <td height="20" width="321">&nbsp;</td>
                        <td style="width: 10px"></td>

                        <td height="19" width="478">&nbsp;</td>

                        <td style="width: 10px"></td>

                        <td height="19" width="402">&nbsp;</td>

                    </table>
                </td>
            </tr>
        </table>
        </td>
						</tr>
						<tr>
                            <td style="height: 8px"></td>
                        </tr>

        <tr>
            <td style="height: 25px" align="center"><%--<span class="mt">--%>
                <%--<div style="text-align: center; padding: 2px 0px 0px 0px; width:100%;">--%>
                <table border="0" cellpadding="0" cellspacing="0" class="mt" style="background-image: url('images/Friday-BottomOuterBnr.gif'); background-repeat: no-repeat; height: 35px"
                    width="100%">
                    <tr>
                        <td align="center">Best viewed in IE 6.0 or higher with monitor resolution set to 1024 x 768 pixels.
                            <br />
                            © Copyright 2012 <a class="dd" href="http://www.mccit.co.in">www.mccit.co.in</a></td>
                    </tr>
                </table>
                <%--</div>--%>

                <%--</span>--%></td>
        </tr>
        </table>
				<%--</td>--%>
        <%--</tr>
		</table>
        --%>
        <map name="Map" id="CEO_desk_Map">
            <area href="Score_Card.aspx" id="imgScoreCard" runat="server" visible="false" shape="RECT" coords="291,1,376,25" alt="CEO ScoreCard" />
            <area href="CEO_desk.aspx" id="imgDashBoard" runat="server" visible="false" shape="RECT" coords="391,1,476,25" alt="CEO DashBoard" />
        </map>
        </TD></TR></TABLE>
    </form>
</body>
</html>
