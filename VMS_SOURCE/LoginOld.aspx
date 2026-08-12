<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginOld.aspx.vb" Inherits="LoginOld" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Vendor Management System</title>
    <meta name="robots" content="noindex">
    <meta name="googlebot" content="noindex">
    <script type="text/javascript">

      var _gaq = _gaq || [];
      _gaq.push(['_setAccount', 'UA-33450822-1']);
      _gaq.push(['_trackPageview']);

      (function() {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
      })();

    </script>
    <link href="includes/style_home.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" language="javascript" src="Scripts/Validate_support.js"></script>
	<script type="text/javascript" language="javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/ValidationLogin.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/Messages.js"></script>	
    <script type="text/javascript" language="javascript" >
        function newwindow(strUrl, strtarget)
        {
            var docwindow = window.open(strUrl, strtarget, 'width=600, height=400, toolbar=no, location=no, directories=yes , status=no, menubar=no, scrollbars=yes, copyhistory=yes, resizable=yes');
        }

        function fnWindowSpan(strUrl, strtarget)
        {
            window.open(strUrl, strtarget, "status=no,toolbar=no,menubar=no,location=no,scrollbars=no,modal=no,resizable=no");
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">     
    
    <table width="100%" cellpadding="0" cellspacing="0" id="page_content">
    
        <tr>
            <td class="header" style="border-bottom:1px solid #d8d8d8;">
                <table style="width:100%;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td id="logo" width="15%" style="vertical-align:middle; text-align:left;">                                
                            <img alt="Best Wall Paint Colors, House Painting Colors" title="Lewis Berger Paints" src="images/berger-paints-logo.png" />                                                                  
                        </td>
                        <td width="60%" style="vertical-align:middle; text-align:left;">
                            <span id="since">Berger Paints India Limited.</span>
                        </td>
                        <td width="25%" style="vertical-align:middle; text-align:right;font-family:Verdana, Geneva, sans-serif; font-size:10px; font-weight:bold; padding-right:10px;">
                            <a href="#" style="background-color:#cc0000; padding:5px 10px 5px 10px; color:#ffffff; text-decoration:none;" onclick="fnWindowSpan('Contacts.htm','blank')">Contact Us</a>
                            <a href="#" style="background-color:#cc0000; padding:5px 10px 5px 10px; color:#ffffff; text-decoration:none;">Help</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td id="tralert" runat="server" align="center" valign="top">                    
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
	                    <td><img alt="" src="images/warning_logo.jpg"/></td>
		                <td align="center"  style="background-color:#ffffff;color:#CC3300;font-size:11pt;font-weight:bold">
		                    <img alt="" src="images/ie_logo.jpg"/><br />
		                    <span style="background-color:#FFFFFF;color:#CC3300;font-size:11pt;font-weight:bold;">Use Internet Explorer 6 or Higher Version</span>
		                    <img alt="" src="images/mozilla_logo.jpg"/>&nbsp;&nbsp;<img alt="" src="images/opera_logo.png"/>
                            <img alt="" src="images/google-chrome_logo.jpg"/>
                            <img alt="" src="images/netscape_logo.png"/><br />
                            <span>If You Use Other Browsers Application may not work properly</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#edefee;">
                    <tr>
                        <td style=" text-align:center;">
                            <img alt="globe" src="images/globe.jpg" />
                        </td>
                        <td style="width:45%;">
                            <h3 style="font-family:Arial Rounded MT Bold">VENDOR MANAGEMENT SYSTEM</h3>
                            <p>
                                <span style="font-family:Arial Rounded MT; font-size:14px; color:Blue; ">This platform facilitates supply management of Vendor produced FG products.</span><br />
                                
                            </p>
                        </td>
                        <td>
                            <table id="register">
                                <tr>
                                    <td>             
                                        <form name="form1" id="form2" action="#" style="text-align:left;">
                                            <%--<table width="100%">
                                                <tr style="text-align:center;">
                                                    <td><h2 style="text-decoration:underline;">Welcome to the <span style="font-style:italic; color:#CC0000; font-family:Verdana; font-weight:bold; font-size:20px;">VMS</span> Software.</h2></td>
                                                </tr>
                                            </table>--%>
                                            
                                            <fieldset>
                                                <table>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label"><span>Username: </span><span class="mandatory">&bull;</span></td>
                                                        <td><asp:textbox id="txtUserId" MaxLength="20" Width="150px" Runat="server"></asp:textbox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label"><span>Password: </span><span class="mandatory">&bull;</span></td>
                                                        <td><asp:textbox id="txtPassword" MaxLength="20" Width="150" Runat="server" TextMode="Password" ></asp:textbox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td><span id="UsrErrMsg" style="color:Red;"></span><asp:Label ID="lblErrorMessage" Visible="false" runat="server" ForeColor="Red"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label" colspan="2" style="text-align:center;">
                                                            <%--<a href="#">Forgot Password ?</a> |--%>
                                                             <a href="ChangePasswordLink.aspx">Change Password.</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label" colspan="2" style="text-align:center;"><asp:HiddenField ID="hdnNavgr" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="imgbtnLogin" runat="server" BackColor="#478af8" Width="341px" Font-Bold="true" ForeColor="White" Text="Login" />
                                                        </td>
                                                    </tr>                            
                                                </table>
                                            </fieldset>
                                        </form>
                                    </td>
                                </tr>    
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td  style="background-color:#edefee;">
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>                
    
        <tr>                    
            <td style="border-top:1px solid #d8d8d8;">
                <table style="width:100%; margin-bottom:5px;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td id="notice" style="padding-left:5px; padding-right:5px;">
                            <ul id="notice_nav">
                                <li><a id="current" href="#" >Disclaimer</a></li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left:5px; padding-right:5px;">
                            <div id="notice_content" >
                                Use of this system is restricted to Authorized Users only. This computer system is a private property of the company and may only be used those individuals authorized by the company management in accordance 
                                with Management and Computer Consultants, system policies. Unauthorized, illegal or improper use of this system may result in disciplinary action against the violators and may also lead to criminal prosecution. All the users 
                                of this computer system should be aware that any information placed in the system is subject to close monitoring and is not subject to any expectation of privacy. By accessing this system, the User Agrees to The Terms &amp; Conditions of the firm.
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td id="footer">
                <table style="width:100%;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="background-color:#478af8; border:#478af8 1px solid; padding:5px; color:#ffffff; width:120px; text-align:center;">Last Modified Date :</td>
                        <td style="padding:5px; color:#666666; text-align:left;  border-top:#478af8 1px solid; border-bottom:#478af8 1px solid;">February 02, 2012</td>
                        <td style="padding:5px; color:blue; text-align:right;  border-top:#478af8 1px solid; border-bottom:#478af8 1px solid;">© Copyright 2012 Management and Computer Consultant. <a href="#">www.mccit.co.in</a></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            
    </form>
</body>
</html>
