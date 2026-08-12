<%@ Page Title="Flash News" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Flash_NewsMaster.aspx.vb" Inherits="Flash_NewsMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationFlashMaster.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Flash News</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Specific User:</label>
                                <asp:DropDownList ID="ddlUserName" CssClass="form-control select2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="table-responsive flashNewsGridMaxh">
                        <table class="table table-hover upgradDataGrid" border="1">
                            <tbody>
                                <tr class="headerGrid">
                                    <th style="text-align: center; width: 10%;">Sl.No.</th>
                                    <th style="text-align: center; width: 50%;">Message</th>
                                    <th style="text-align: center; width: 20%;">Date of Entry</th>
                                    <th style="text-align: center; width: 20%;">Retain Till</th>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">1</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg1" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE1" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp1" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">2</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg2" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE2" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp2" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">3</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg3" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE3" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp3" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">4</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg4" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE4" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp4" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">5</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg5" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE5" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField5" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp5" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">6</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg6" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE6" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField6" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp6" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">7</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg7" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE7" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField7" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp7" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">8</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg8" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE8" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField8" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp8" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">9</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg9" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE9" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField9" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp9" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="tlrowlight">
                                    <td style="text-align: center;">10</td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtMsg10" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoE10" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField10" runat="server" />
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:TextBox ID="txtDoExp10" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                            <asp:Button ID="btnReset" CssClass="btn btn-danger btn-sm" runat="server" Text="Reset" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
