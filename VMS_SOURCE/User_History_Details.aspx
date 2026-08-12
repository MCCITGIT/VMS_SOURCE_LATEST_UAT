<%@ Page Title="User Profile List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="User_History_Details.aspx.vb" Inherits="User_History" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationUserIdGroup.js"></script>
    <script type="text/javascript" src="Scripts/ValidationUser_history.js"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 119) { // button search (F8 keypress)
                if (validatedate()) {
                    __doPostBack(document.getElementById('btnSearch').name, '');
                }
            }
            if (event.keyCode == 120) { // button clear (F9 keypress)

                __doPostBack(document.getElementById('btnClear').name, '');
            }
        }
    </script>
    <script type="text/javascript">var cal1 = new CalendarPopup(); function FromDate_onclick() { }
        function ToDate_onclick() {
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">User Login History Details</h3>
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
                                <label class="form-control-label">User Group:</label>
                                <asp:DropDownList ID="ddlUserGroup" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                <asp:HiddenField ID="hdnUserGroup" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">User Id:</label>
                                <asp:DropDownList ID="ddlUserId" CssClass="form-control select2" runat="server">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdnUserId" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Period From:</label>
                                <asp:TextBox ID="txtFromDate" TextMode="Date" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtFromDate,'FromDate','dd/MM/yyyy');">
                                    <img src="images/date_icon.gif" id="FromDate" alt="Calender" style="vertical-align: middle; border: 0" onclick="return FromDate_onclick()" />
                                </a>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Period To:</label>
                                <asp:TextBox ID="txtToDate" TextMode="Date" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtToDate,'ToDate','dd/MM/yyyy');">
                                    <img src="images/date_icon.gif" id="ToDate" alt="Calender" style="vertical-align: middle; border: 0" onclick="return ToDate_onclick()" />
                                </a>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Search For:</label>
                                <asp:DropDownList ID="ddlSearchFor" CssClass="form-control select2" runat="server">
                                    <asp:ListItem>Detail</asp:ListItem>
                                    <asp:ListItem>Summary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group">
                                <%--<asp:ImageButton CssClass="btn btn-primary btn-sm" ImageUrl="images/ic_search.gif" ID="btnSearch" runat="server" />--%>
                                <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="btnSearch" runat="server" OnClick="btnSearch_Click">Search</asp:LinkButton>
                                <asp:Button ID="btnClear" CssClass="btn btn-secondary btn-sm" runat="server" Text="Clear" />
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="form-group row ddlPageSize">
                        <label for="ddlPageSize" class="col-auto form-control-label">
                            <asp:Label ID="Label4" runat="server" Text="Results Per Page:"></asp:Label>
                        </label>
                        <div class="col-md-1">
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvUserHistory" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" OnPageIndexChanging="gvUserProfile_PageIndexChanging">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:BoundField HeaderText="User Id" DataField="uh_userid">
                                    <ItemStyle HorizontalAlign="center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="User Group" DataField="uh_user_group">
                                    <ItemStyle HorizontalAlign="center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Logged Date" DataField="logdate">
                                    <ItemStyle HorizontalAlign="center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Logged Time" DataField="logtime">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>

                        <asp:GridView ID="gvUserHistoryCount" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" OnPageIndexChanging="gvUserHistoryCount_PageIndexChanging">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="User Id" DataField="uh_userid"></asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="From Date" DataField="fromDate"></asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="To Date" DataField="toDate"></asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Total Count" DataField="totalCount"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
