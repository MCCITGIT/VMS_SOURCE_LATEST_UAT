<%@ Page Title="Challan wise Source Despatches" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Monthly_Unit_Despatch_vr2.aspx.vb" Inherits="Monthly_Unit_Despatch_vr2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {
                document.getElementById('btnSubmit').click();
            }
        }

        function validateSubmit() {
            firstErrorControl = "";
            errMsg = "";
            var From = false;
            var To = false;

            if (ValidateRequired("txtFromDate", "Please Enter From Date"))
                if (CheckDateFormat("txtFromDate", "Invalid From Date"))
                    From = true;

            if (ValidateRequired("txtToDate", "Please Enter To Date"))
                if (CheckDateFormat("txtToDate", "Invalid To Date"))
                    To = true;

            if (From && To)
                ValidatetwoDates("txtFromDate", "txtToDate", "From Date Cannot Be Greater Than To Date");

            if (firstErrorControl != "") {
                SetControlFocus(firstErrorControl);
                errMsg = "<table>" + errMsg + "</table>";
                document.getElementById("divErrorMessage").innerHTML = errMsg;
                return false;
            }
            document.getElementById("divErrorMessage").innerHTML = "";
            return true;
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Challan wise Source Despatches</h3>
                <p class="pageSubTitle">Source unit despatches by date range [F7 = Submit]</p>
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
                                <label class="form-control-label">Region:</label>
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Depot:</label>
                                <asp:DropDownList ID="ddlDepot" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">From Date:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtFromDate" ClientIDMode="Static" CssClass="form-control" MaxLength="10" placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">To Date:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtToDate" ClientIDMode="Static" CssClass="form-control" MaxLength="10" placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Report Format:</label>
                                <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="ExcelFormat" Selected="True">EXCEL</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 form-btn-mt">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" OnClientClick="return validateSubmit();" UseSubmitBehavior="true" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlRegion" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
