<%@ Page Title="Direct Despatch Order Details Report" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DirectDespatch_OrderDetails.aspx.vb" Inherits="DirectDespatch_OrderDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <script type="text/javascript" src="Scripts/ValidationUserProfile.js"></script>
    <script type="text/javascript">
        var cal1 = new CalendarPopup();

        // "fromDateBtn" only exists when the calendar icon markup below is
        // uncommented; without this guard the script threw on every page load.
        var fromDateBtn = document.getElementById("fromDateBtn");
        if (fromDateBtn) {
            fromDateBtn.addEventListener("click", function (e) {
                e.preventDefault();
                cal1.select(document.forms[0].txtFromDate, 'FromDate', 'dd/MM/yyyy');
            });
        }
    </script>


    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Direct Despatch Order Details</h3>
                <p class="pageSubTitle">Order level details for direct despatches</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">From Date:</label>
                                <asp:TextBox ID="txtFromDate" CssClass="form-control" MaxLength="10" TabIndex="23" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                               <%-- <a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtToDate,'ToDate','dd/MM/yyyy');">
                                    <img src="images/date_icon.gif" id="FromDate" alt="Calendar" />
                                </a>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">To Date:</label>
                                <asp:TextBox ID="txtToDate" CssClass="form-control"  MaxLength="10" TabIndex="23" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                                <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtToDate,'ToDate','dd/MM/yyyy');">
                                    <img src="images/date_icon.gif" id="ToDate" alt="Calender" />
                                </a>--%>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                                <div id="divErrorMessage"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
           <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
