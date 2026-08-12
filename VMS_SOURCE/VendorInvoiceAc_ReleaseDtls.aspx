<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VendorInvoiceAc_ReleaseDtls.aspx.vb" Inherits="VendorInvoiceAc_ReleaseDtls" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Vendor Invoice Account Release Details::</title>
    <link href="includes/style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/date.js" type="text/javascript"></script>
    <script src="Scripts/calendarpopup.js" type="text/javascript"></script>
    <script src="Scripts/popupwindow.js" type="text/javascript"></script>
    <script type="text/javascript">var cal1 = new CalendarPopup();</script>
    <script src="Scripts/FunctionValidator.js" type="text/javascript"></script>
    <script src="Scripts/anchorposition.js" type="text/javascript"></script>


    <link type="text/css" rel="Stylesheet" href="includes/select2.min.css" />
    <link type="text/css" rel="Stylesheet" href="includes/select2-bootstrap4.min.css" />
    <style type="text/css">
        .dot1 {
            height: 16px;
            width: 16px;
            background-color: #3bff07;
            border-radius: 50%;
            display: inline-block;
        }

        .dot4 {
            height: 16px;
            width: 16px;
            background-color: #ffff00;
            border-radius: 50%;
            display: inline-block;
        }

        .t {
            position: relative;
            top: -6px;
            margin-left: 8px;
            font-size: smaller;
        }

        .btnDownload {
            margin-bottom: 20px;
            position: relative;
            background-color: #CD7F32;
            border: none;
            font-size: 12px;
            color: #FFFFFF;
            padding: 5px;
            width: 70px;
            text-align: center;
            transition-duration: 0.4s;
            text-decoration: none;
            overflow: hidden;
            cursor: pointer;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left" valign="top" style="width: 100%;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="top" style="background-color: #ffffff;">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="bottom" style="width: 90%;">
                                            <asp:Image ID="Image2" ImageUrl="~/images/inner_tag.jpg" runat="server" />
                                        </td>
                                        <td style="width: 10%; height: 65px;" align="center" valign="bottom">
                                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Home" ImageUrl="~/images/home_new.png"
                                                PostBackUrl="~/Home.aspx" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 1006px">&nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" align="center" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="ss">
                                    <tr>
                                        <td align="left" style="font-family: BodoniPS; font-size: 24px; text-align: center;">Vendor Invoice Account Release Details
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; margin-top: 20px" class="updatePanelView">
                                                <tr>
                                                    <td style="width: 25%; text-align: left; vertical-align: baseline;">
                                                        <div class="formCtrl">
                                                            <label class="txLabel">Vendor:</label>
                                                            <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="select2 formField" TabIndex="2"></asp:DropDownList>
                                                        </div>
                                                    </td>

                                                    <td style="width: 25%; text-align: left; vertical-align: baseline;">
                                                        <div class="formCtrl">
                                                            <label class="txLabel">Depot:</label>
                                                            <asp:DropDownList ID="ddldepot" runat="server" CssClass="select2 formField" TabIndex="2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td style="width: 10%; text-align: left; vertical-align: baseline;">
                                                        <div class="formCtrl">
                                                            <label class="txLabel">Status:</label>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2 formField" TabIndex="2">
                                                                <asp:ListItem Text="Select" Value="" />
                                                                <asp:ListItem Text="Paid" Value="Paid" />
                                                                <asp:ListItem Text="Due" Value="Due" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td style="width: 10%; text-align: left; vertical-align: baseline;">
                                                        <div class="formCtrl">
                                                            <label class="txLabel">Type:</label>
                                                            <asp:DropDownList ID="ddltype" runat="server" CssClass="select2 formField" TabIndex="2">
                                                                <asp:ListItem Text="Select" Value="" />
                                                                <asp:ListItem Text="Depot Despatch" Value="Depot Despatch" />
                                                                <asp:ListItem Text="Direct Despatch" Value="Direct Despatch" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td style="width: 10%; text-align: left; vertical-align: baseline;">
                                                        <div class="formCtrl">
                                                            <label class="txLabel">From Date:</label>
                                                            <asp:TextBox ID="txtFromDate" CssClass="formField h18 wAuto" runat="server" Width="90px" MaxLength="10"></asp:TextBox>
                                                            <%--<a href="javascript:cal1.select(document.forms[0].txtFromDate,'FromDt','dd/MM/yyyy');">
                                                                <img src="images/date_icon.gif" id="FromDt" runat="server" alt="Calender" style="border: 0" />
                                                            </a>--%>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                                                        </div>
                                                    </td>
                                                    <td style="width: 10%; text-align: left; vertical-align: baseline;">
                                                        <div class="formCtrl">
                                                            <label class="txLabel">To Date:</label>
                                                            <asp:TextBox ID="txtTodate" CssClass="formField h18 wAuto" runat="server" Width="90px" MaxLength="10"></asp:TextBox>
                                                           <%-- <a href="javascript:cal1.select(document.forms[0].txtTodate,'ToDt','dd/MM/yyyy');">
                                                                <img src="images/date_icon.gif" id="ToDt" runat="server" alt="Calender" style="border: 0" />
                                                            </a>--%>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="dd/MM/yyyy" />
                                                        </div>
                                                    </td>
                                                    <td style="width: 2%; text-align: left; vertical-align: middle; padding-bottom: 20px;">
                                                        <div class="formCtrlBtn">
                                                            <asp:ImageButton CssClass="mr5" ID="ImgbtnSearch" runat="server" ImageUrl="images/ic_search.gif" ToolTip="Search" />
                                                        </div>
                                                    </td>
                                                    <td style="width: 5%; text-align: left;">
                                                        <div class="formCtrlBtn">
                                                            <asp:LinkButton CssClass="btnDownload" ID="btndownload" runat="server" OnClick="btndownload_Click" Text="Download" ToolTip="Download" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="margin: 0px auto; text-align: center;">
                                        <td style="margin: 0px auto; text-align: center;">
                                            <table style="width: 100%; margin: 0px auto; text-align: center" class="mt">
                                                <tr>
                                                    <td style="margin: 0px auto; text-align: center;">
                                                        <table style="width: 100%; margin: 0px auto;">
                                                            <tr>
                                                                <%--<td style="text-align: right;">
                                                                    <asp:Label ID="Label4" runat="server" Text="Results Per Page:"></asp:Label>
                                                                    <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropDown" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </td>--%>
                                                                <td style="text-align: right">&nbsp;  &nbsp;  &nbsp;
                                                                    <%--<span class="dot1"></span><span class="t">Auto</span>--%>
                                                                    <span class="dot4"></span><span class="t">Manual</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="margin: 0px auto; text-align: center;">
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="gvVendorInvoiceDtls" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnRowDataBound="gvVendorInvoiceDtls_RowDataBound"
                                                                            Visible="true" PageSize="30"
                                                                            BorderWidth="1" CssClass="table table-hover upgradDataGrid" EmptyDataText="No Record Found">
                                                                            <RowStyle CssClass="tlrowlight" />
                                                                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                                                            <HeaderStyle CssClass="headerGrid" />
                                                                            <FooterStyle CssClass="footerGrid" />
                                                                            <Columns>
                                                                                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                            </asp:BoundField>--%>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Type" DataField="Type">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Depot" DataField="depot_name">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Despatched Date" DataField="InvoiceUploadDate">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Invoice No" DataField="Invoice_No">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="Invoice Date" DataField="Invoice_Date">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Invoice Value" DataField="Invoice_Value">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Release No" DataField="Release_No" ControlStyle-Width="6%">
                                                                                    <ControlStyle Width="10%"></ControlStyle>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Release Date" DataField="Release_Date">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="GRN No" DataField="GRN_No">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="GRN Date" DataField="GRN_Date">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Voucher No" DataField="Voucher_No">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Amount Paid" DataField="Payment_Status">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Amount Due" DataField="PendingAmount">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td style="height: 19px">
                                            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label><div
                                                id="divErrorMessage">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 20px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 39px; width: 100%;" align="center">
                    <span class="mt">
                        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
                    </span>
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>
    <script type="text/javascript" language="javascript" src="Scripts/select2.full.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.select2').select2();
        });
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_endRequest(function () {
        //    $('.select2').select2();
        //});

    </script>
</body>
</html>
