<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Top.aspx.vb" Inherits="Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
		<link href="includes/style.css" rel="stylesheet" type="text/css" />
		
		<base target="main" />
		
     
	</head>
	<body style="margin-top:0px; margin-left:0px">
    <form id="form1" runat="server">
    
		<table width="100%" border="0" style="height:100px; vertical-align:top; margin-top:0px; background-color:#07466D" class="bl">
            <tr align="left" valign="top">
                <td>
                    <table width="100%" border="0" style="height:150px; vertical-align:top; margin-top:0px;">
                    <tr align="left" valign="top">
                    
                        <td style="width:70px;"><strong>Company : </strong></td>
                        
                        <td id="tdCompany" style="width:170px; text-align:left" runat="server"></td>
                        <td style="width:50px;"><strong>Region : </strong></td>
                        
                        <td id="tdRegion" style="width:170px; text-align:left" runat="server"></td>
                        
                        <td style="width:40px; text-align:left"><strong>Depot : </strong></td>
                        
                        <td id="tdBranch" style="width:190px; text-align:left" runat="server"></td>
                        
                        <td style="width:60px; text-align:left"><strong>User ID : </strong></td>
                        
                        <td id="tdUid" style="width:180px; text-align:left" runat="server"></td>
                        
                        <td style="width:70px; text-align:left"><strong>Department : </strong></td>
                        
                        <td id="tdDept" style="width:170px; text-align:left" runat="server"></td>
                        
                        <td align="right" style="width:120px; text-align:left">
                            <a href="Logout.aspx" class="bl" target="_parent"><strong>Logout</strong></a>
                        </td>
                        
                    </tr>
                    </table>
                </td>
             </tr>   
        </table>
    </form>
	</body>
</html>

