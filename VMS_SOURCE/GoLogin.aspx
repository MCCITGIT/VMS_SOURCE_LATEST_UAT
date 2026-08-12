<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GoLogin.aspx.vb" Inherits="GoLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Berger Apps Redirect to New Server Page</title>
    <link href="includes/style_home.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 7]>   
	<script language="JavaScript" type="text/javascript">   
	function fixPNG() // correctly handle PNG transparency in Win IE 5.5 & 6.   
	{   
	   var arVersion = navigator.appVersion.split("MSIE");   
	   var version = parseFloat(arVersion[1]);   
	   if ((version >= 5.5) && (document.body.filters))    
	   {   
	      for(var i=0; i<document.images.length; i++)   
	      {   
        	 var img = document.images[i];   
	         var imgName = img.src.toUpperCase();   
        	 if (imgName.substring(imgName.length-3, imgName.length) == "PNG")   
	         {   
        	    var imgID = (img.id) ? "id='" + img.id + "' " : "";  
	            var imgClass = (img.className) ? "class='" + img.className + "' " : "";  
        	    var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' ";
	            var imgStyle = "display:inline-block;" + img.style.cssText;
        	    if (img.align == "left") imgStyle = "float:left;" + imgStyle   
	            if (img.align == "right") imgStyle = "float:right;" + imgStyle   
        	    if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle   
	            var strNewHTML = "<span " + imgID + imgClass + imgTitle + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";" + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader" + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>";
        	    img.outerHTML = strNewHTML;   
	            i = i-1;
        	 }   
	      }   
	   }      
	}   
	window.attachEvent("onload", fixPNG);   
	</script>   
	<![endif]-->
</head>
<body style="background-color: #ffd0d0">
    <form id="form1" runat="server">
        <div style="text-align: left;">

            <table width="100%">
                <tr>
                    <td style="width: 10%">
                        <img alt="BERGER" src="images/berger-paints-logo.png" /></td>
                    <td style="text-align: center;">
                        <img alt="" src="images/berger_click.png" /></td>
                </tr>
            </table>

            <h2 style="border: 1px solid #000000; padding: 20px;">Please enter url as <a href="http://www.bergerapps.in" style="text-decoration: underline; color: Blue;">www.bergerapps.in</a> and then click on the required application's hyperlink to login.</h2>

            <table style="background-color: #f9ebae; width: 100%; border: 1px #000000 solid; font-family: Verdana; font-size: 10px; font-weight: bold; text-align: center;">
                <tr>
                    <td>Painter Meet</td>
                    <td>BS Tracker</td>
                    <td>Leadgen</td>
                </tr>
                <tr>
                    <td>
                        <img alt="" src="images/paintermeet.png" class="thumbnail" />
                    </td>
                    <td>
                        <img alt="" src="images/bstracker.png" class="thumbnail" />
                    </td>
                    <td>
                        <img alt="" src="images/leadegen.png" class="thumbnail" />
                    </td>
                </tr>
                <tr>
                    <td>Color Bank</td>
                    <td>Prolead</td>
                    <td>Protecton</td>
                </tr>
                <tr>
                    <td>
                        <img alt="" src="images/cbank.png" class="thumbnail" />
                    </td>
                    <td>
                        <img alt="" src="images/prolead.png" class="thumbnail" />
                    </td>
                    <td>
                        <img alt="" src="images/protecton.png" class="thumbnail" />
                    </td>
                </tr>
                <tr>
                    <td>VMS</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <img alt="" src="images/vms.png" class="thumbnail" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
