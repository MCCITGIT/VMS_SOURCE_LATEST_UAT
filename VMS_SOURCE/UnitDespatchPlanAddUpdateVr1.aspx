<%@ Page Title="Add/Update Despatch Challan" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UnitDespatchPlanAddUpdateVr1.aspx.vb" Inherits="UnitDespatchPlanAddUpdateVr1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="Scripts/Validation.js" type="text/javascript"></script>
    <script src="Scripts/ValidateEstimationUpload.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {

            if (event.keyCode == 118) {  // button Add (F7 keypress)
                document.getElementById('btnSubmit').click()
            }
            else if (event.keyCode == 119) { // button Search (F8 keypress)

                document.getElementById('btnCancel').click()
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }
        //-->
    </script>

    <script type="text/javascript">
        function HideLoading() {
            var loading_icon = document.getElementById("loading");
            loading_icon.style.visibility = 'hidden';
            //loading_icon.style.visibility = (loading_icon.style.visibility == 'visible') ? 'hidden' : 'visible';
        }

        function ShowLoading() {

            var loading_icon = document.getElementById("loading");
            loading_icon.style.visibility = 'visible';
            //loading_icon.style.visibility = (loading_icon.style.visibility == 'visible') ? 'hidden' : 'visible';
        }

        function aceSelected(sender, e) {
            var value = e.get_value();
            //var text = e._item.innerText;
            var text = e.get_text();

            document.getElementById('<%=hdnTranspoterId.ClientID%>').value = value;
            document.getElementById('<%=txtTransporter.ClientID%>').value = text;

            //sender.get_element().value = text;
            //document.getElementById("btnShowdealerDetails").click();
        }
    </script>

    <script type="text/javascript">
        function regex(e) {
            // var regex = new RegExp("^[a-zA-Z0-9_]*$");
            var regex = new RegExp("^\\s+$");

            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (regex.test(str)) {
                e.preventDefault();
                // alert('Please Enter Alphabet');
                return false;
            }
            else {
                return true;
            }
        }
    </script>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var currentDate = new Date();
            currentDate.setDate(currentDate.getDate());
            document.getElementById('txtCenvatDt').setAttribute('max', currentDate.toISOString().split('T')[0]);
        });

        document.addEventListener("DOMContentLoaded", function () {
            var currentDate = new Date();
            currentDate.setDate(currentDate.getDate());
            document.getElementById('txtChallanDt').setAttribute('max', currentDate.toISOString().split('T')[0]);
        });
    </script>
    <script src="Scripts/ValidateUnitDespatchAddUpdate.js?time=<%=  DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>" type="text/javascript"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Add/Update Despatch Challan</h3>
                <p class="pageSubTitle">Create and update despatch challans</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:Label ID="lblChallanNo" runat="server" CssClass="text-right mb-2 d-flex justify-content-end font-weight-bold"></asp:Label>
    <asp:HiddenField ID="hdnChallanno" runat="server" />
    <asp:HiddenField ID="hdnNoMaster" runat="server" />
    <asp:HiddenField ID="hdnMaxDespLimit" runat="server" />
    <asp:HiddenField ID="hdnLotNo" runat="server" />
    <asp:HiddenField ID="hdnUnitOracleId" runat="server" />
    <%-- Modified-by MUKESH BHAGAT on 20-08-2026 : restored Indent feature from old UAT source --%>
    <asp:HiddenField ID="hdnindentyn" runat="server" />
    <asp:HiddenField ID="hdnUnitCode" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Challan Date:<span class="mandatory">*</span></label>
                                <asp:TextBox ID="txtChallanDt" CssClass="form-control" MaxLength="10" TabIndex="1" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtChallanDt" Format="dd/MM/yyyy" />
                                <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtChallanDt,'ChallanDt','dd/MM/yyyy');">
                                    <img src="images/date_icon.gif" id="ChallanDt" runat="server" alt="Calender" style="border: 0" />
                                </a>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Region:</label>
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" CssClass="form-control select2" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Depot:<span class="mandatory">*</span></label>
                                <asp:ListBox ID="chkbxListLocation" runat="server" CssClass="form-control" SelectionMode="Multiple" placeholder="Select" TabIndex="26" AutoPostBack="true" OnSelectedIndexChanged="chkbxListLocation_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Source:</label>
                                <asp:Label ID="lblUnit" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Product:<span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" CssClass="form-control select2" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" TabIndex="4">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">All SKU:<span class="mandatory">*</span></label>
                                <asp:DropDownList ID="ddlAllSku" runat="server" AutoPostBack="True" CssClass="form-control select2" TabIndex="5">
                                    <asp:ListItem Text="Yes" Value="Y" />
                                    <asp:ListItem Text="No" Value="N" Selected="True" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Year:</label>
                                <asp:Label ID="lblYear" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Month:</label>
                                <asp:Label ID="lblmonth" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Invoicing Depot:<span class="mandatory">*</span></label>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDeliveryDepot" runat="server" AutoPostBack="True" CssClass="form-control select2"
                                            TabIndex="3">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <%-- Modified-by MUKESH BHAGAT on 20-08-2026 : restored Indent feature from old UAT source --%>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Indent:<span class="mandatory">*</span></label>
                                <asp:UpdatePanel ID="UpdatePanel_Indent" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIndent" runat="server" CssClass="form-control select2" AutoPostBack="true" Enabled="false">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Site Name:<span class="mandatory">*</span></label>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="true" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">PO No.:<span class="mandatory">*</span></label>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="true" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Berger Approved Transporter Y/N:</label>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" class="mt-2">
                            <ContentTemplate>
                                <asp:CheckBox ID="chkApprovedTranspoterYN" CssClass="checkRadioGroup" runat="server" OnCheckedChanged="chkApprovedTranspoterYN_CheckedChanged" AutoPostBack="true" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <label class="form-control-label">Transpoter Name/Id:<span class="mandatory" style="font-size: 9px;">* (Atleast 3 characters)</span></label>
                                <div class="flexInputDiv">
                                    <asp:TextBox ID="txtTranspoterName" runat="server" CssClass="form-control" placeholder="Search By Traspoter Name/Id"></asp:TextBox>
                                    <asp:LinkButton ID="btnResetdealerDetails" runat="server" Text="Reset" CssClass="refreshIcon">
                                       <i class="fas fa-sync"></i>
                                    </asp:LinkButton>
                                </div>
                                <asp:HiddenField ID="hdnTranspoterId" runat="server" />

                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtTranspoterName"
                                    ServiceMethod="TranspoterSearch" MinimumPrefixLength="3" EnableCaching="false"
                                    CompletionListCssClass="vmsAutoComplete" CompletionListItemCssClass="vmsAutoCompleteItem"
                                    CompletionListHighlightedItemCssClass="vmsAutoCompleteItemHighlight" OnClientItemSelected="aceSelected"
                                    OnClientPopulated="HideLoading" BehaviorID="AutoCompleteEx" CompletionListElementID="Panel1"
                                    OnClientPopulating="ShowLoading" FirstRowSelected="true" OnClientHidden="HideLoading"
                                    OnClientHiding="HideLoading">
                                    <Animations>
                                                    <OnShow>
                                                        <Sequence>
                                                            <OpacityAction Opacity="0" />
                                                            <HideAction Visible="true" />
                                                            <ScriptAction Script="
                                                                // Cache the size and setup the initial size
                                                                var behavior = $find('AutoCompleteEx');
                                                                if (!behavior._height) {
                                                                    var target = behavior.get_completionList();
                                                                    behavior._height = target.offsetHeight - 2;
                                                                    target.style.height = '0px';
                                                                }" />
                                
                                                            <Parallel Duration=".4">
                                                                <FadeIn />
                                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnShow>
                                                    <OnHide>
                            
                                                        <Parallel Duration=".4">
                                                            <FadeOut />
                                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                                        </Parallel>
                                                    </OnHide>
                                    </Animations>
                                </asp:AutoCompleteExtender>

                                <img alt="Loading..." src="images/ajax-loader.gif" id="loading" class="inputLoading" />
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Style="overflow-y: scroll; position: absolute; left: 0; top: 0">
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Transporter:<span class="mandatory">*</span></label>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtTransporter" CssClass="form-control" TabIndex="6" runat="server" MaxLength="500"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Truck No:<span class="mandatory">*</span></label>
                        <asp:TextBox ID="txtTruckNo" CssClass="form-control" TabIndex="7" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Vendor Invoice No:<span class="mandatory">*</span></label>
                        <asp:TextBox ID="txtCenvatNo" CssClass="form-control" TabIndex="8" runat="server" MaxLength="24" onkeypress="return regex(event);" AutoComplete="off" onpaste="return false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Vendor Invoice Date:<span class="mandatory">*</span></label>
                        <asp:TextBox ID="txtCenvatDt" CssClass="form-control" MaxLength="10" TabIndex="9" runat="server" onfocus="setMaxDate()"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCenvatDt" Format="dd/MM/yyyy" />
                        <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtCenvatDt,'cenvatDt','dd/MM/yyyy');">
                            <img src="images/date_icon.gif" id="cenvatDt" alt="Calendar" style="border: 0" />
                        </a>--%>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Road Permit No:<span class="mandatory">*</span></label>
                        <asp:TextBox ID="txtRoadPermitNo" CssClass="form-control" TabIndex="7" runat="server" MaxLength="30"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">E-Way Bill No:</label>
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtEwayBillNo" CssClass="form-control" TabIndex="7" runat="server" MaxLength="30"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">E-Way Bill Date:</label>
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtEwayBillDate" CssClass="form-control" MaxLength="10" TabIndex="9" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEwayBillDate" Format="dd/MM/yyyy" />
                                <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtEwayBillDate,'ewayDt','dd/MM/yyyy');">
                                    <img src="images/date_icon.gif" id="ewayDt" alt="Calender" style="border: 0" />
                                </a>--%>
                                <%--<asp:TextBox ID="txtEwayBillDate" runat="server" class="form-control" MaxLength="10" TextMode="Date"></asp:TextBox>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Valid Upto:</label>
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtValidUpto" CssClass="form-control" MaxLength="10" TabIndex="9" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtValidUpto" Format="dd/MM/yyyy" />
                                <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtValidUpto,'validUptoDt','dd/MM/yyyy');">
                                    <img src="images/date_icon.gif" id="validUptoDt" alt="Calender" style="border: 0" />
                                </a>--%>
                                <%--<asp:TextBox ID="txtValidUpto" runat="server" class="form-control" MaxLength="10" TextMode="Date"></asp:TextBox>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Final Invoice Value (After Tax):<span id="Span1" class="mandatory" runat="server">*</span></label>
                        <asp:TextBox ID="txtFinalInvoiceValue" CssClass="form-control" MaxLength="10" TextMode="Number" TabIndex="10" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Upload Actual Invoice Copy:<span class="mandatory">*</span><span id="Span4" class="mandatory" runat="server"></span></label>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:FileUpload ID="sch_fld1" runat="server" CssClass="form-control" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdnFileName" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
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
                        <asp:GridView ID="gvSKUDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" ShowFooter="true"
                            EmptyDataText="No SKU Found">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                        <asp:HiddenField ID="hdnUom" runat="server" Value='<%# Bind("skuUom") %>' />
                                        <asp:HiddenField ID="hdnVol" runat="server" Value='<%# Bind("skuVol") %>' />
                                        <asp:HiddenField ID="hdnTransitDay" runat="server" Value='<%# Bind("transitDays") %>' />
                                        <asp:HiddenField ID="hdnSKUCode" runat="server" Value='<%# Bind("load_sku_code") %>' />
                                        <asp:HiddenField ID="hdnLineNum" runat="server" Value='<%# Bind("line_num") %>' />
                                        <asp:HiddenField ID="hdnSkuDesc" runat="server" Value='<%# Bind("skuDesc") %>' />
                                        <asp:HiddenField ID="hdnSkuRate" runat="server" Value='<%# Bind("SkuRate") %>' />
                                        <asp:HiddenField ID="hdnSkuGST" runat="server" Value='<%# Bind("SkuGST") %>' />
                                        <asp:HiddenField ID="hdnDepotCode" runat="server" Value='<%# Bind("load_depot") %>' />
                                        <asp:HiddenField ID="hdnCurrSkuStatus" runat="server" Value='<%# Bind("CurrSkuStatus") %>' />

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                                </asp:TemplateField>

                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Depot" DataField="DepotName">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                                </asp:BoundField>

                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="SKU Code" DataField="load_sku_code">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="9%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Description" DataField="skuDesc">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Rate (per unit)" DataField="skuRate">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="Auto Indent" DataField="calculatedAuto">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Depot Indent" DataField="load_depot_indent_nop_pending">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Despatch Till Date" DataField="calculatedDespatch" ControlStyle-Width="10%">
                                    <ControlStyle Width="10%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                HeaderText="Pending Load" DataField="pendingLoad">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                            </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="This Despatch" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingLoad" runat="server" Text='<%# Bind("pendingLoad") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblftrThisDesp1" runat="server" Text='Total'></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="This Despatch" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtThisDesp" CssClass="txtBox" runat="server" Text='<%# Bind("pendingLoad") %>' TextMode="Number"
                                            Width="45px" MaxLength="30" Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblftrThisDesp" runat="server" Text=''></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Rate (Inc. GST)" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalRate" runat="server" Text=''></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblftrTotalRate" runat="server" Text=''></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                        <asp:Button ID="btnGo" CommandName="ShowQuantity" runat="server" CssClass="but2" Text="Go" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                            </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="LOT" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLOT" CssClass="txtBox" runat="server" Text='<%# Bind("despd_lot_no") %>'
                                            Width="170px" Enabled="False"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnDelete" CssClass="btn btn-danger btn-sm" runat="server" Text="Delete" />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-sm" Text="Submit" />
                            <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" PostBackUrl="~/UnitDespatchPlanListVr1.aspx" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlAllSku"
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkbxListLocation"
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlProduct"
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlRegion"
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlDeliveryDepot"
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnTargetID_Quantity" runat="server" />
            <asp:HiddenField ID="hdnTargetID1" runat="server" />
            <%-- <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnCance"
    PopupControlID="pnlQuantity" TargetControlID="hdnTargetID1" CancelControlID="btnCance"
    BackgroundCssClass="popupBackground">
</asp:ModalPopupExtender>--%>
            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="btnCance" PopupControlID="pnlQuantity" TargetControlID="hdnTargetID_Quantity" CancelControlID="btnCance" BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>
            <%-- <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlQuantity"
    TargetControlID="hdnTargetID_Quantity" BackgroundCssClass="popupBackground">
</asp:ModalPopupExtender>--%>
            <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" OkControlID="btnOk" PopupControlID="PnlOk" TargetControlID="hdnTargetID1" CancelControlID="btnOk" BackgroundCssClass="popupBackground">
            </asp:ModalPopupExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divDespatchQuantity" runat="server">
        <asp:Panel ID="pnlQuantity" runat="server" CssClass="modalPanel bootstrapModal" Style="display: none;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h5 class="modal-title">
                                    <asp:Label ID="lblSpPopupHdr" runat="server" Font-Bold="True"></asp:Label></h5>
                                <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>--%>
                            </div>
                            <div class="modal-body">
                                <table style="width: 99%; border: 1px solid #66CCFF">
                                    <tr>
                                        <td style="background-color: #E6F5FB; width: 30%; text-align: right; font-weight: bold; border-bottom: 1px solid #66CCFF;">SKU
                                        </td>
                                        <td align="left" style="border-bottom: 1px solid #66CCFF;">
                                            <asp:Label ID="lblSKU" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnDespChallanNo" runat="server" />
                                            <asp:HiddenField ID="hdnFinYear" runat="server" />
                                            <asp:HiddenField ID="hdnDespatchUnit" runat="server" />
                                            <asp:HiddenField ID="hdnDepotCode" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #E6F5FB; width: 30%; text-align: right; font-weight: bold; border-bottom: 1px solid #66CCFF;">SKU Description
                                        </td>
                                        <td align="left" style="border-bottom: 1px solid #66CCFF;">
                                            <asp:Label ID="lblSKUDescription" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="background-color: #E6F5FB; width: 30%; text-align: right; font-weight: bold;">Total Quantity
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblTotalDespatchQuantity" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                                <table style="width: 100%; border: 1px solid #66CCFF">
                                    <tr>
                                        <th colspan="3" style="width: 5%; border-bottom: 1px solid #66CCFF; border-right: 1px solid #66CCFF; background-color: #E6F5FB;">Details
                                        </th>
                                        <th style="width: 20%; border-bottom: 1px solid #66CCFF; background-color: #E6F5FB;">Quantity
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">IND
                                        </td>
                                        <td style="width: 20%">
                                            <asp:Label ID="lblPONo1" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 30%">
                                            <asp:TextBox ID="txtDate1" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity1" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="tlrowdark">
                                        <td>IND
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPONo2" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate2" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity2" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>IND
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPONo3" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate3" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity3" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="tlrowdark">
                                        <td>IND
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPONo4" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate4" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity4" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>IND
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPONo5" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate5" runat="server" MaxLength="10" Width="130px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity5" runat="server" MaxLength="10" Width="64px" Style="background-color: inherit;"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnAddSP" runat="server" Text="Add" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCance" runat="server" Text="Cancel" CssClass="btn btn-secondary" />
                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
    </div>

    <asp:Panel ID="PnlOk" runat="server" CssClass="modalPanel bootstrapModal" Style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h5 class="modal-title">
                                <asp:Label ID="Label1" runat="server" ForeColor="White" Font-Bold="true" Text="Message"></asp:Label>
                            </h5>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblPopMessage" runat="server" ForeColor="#7f0037" Font-Bold="true" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="btn btn-primary" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript" src="Scripts/jquery.sumoselect.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.select2').select2();
            $(<%=chkbxListLocation.ClientID%>).SumoSelect({
                selectAll: true,
                search: true,
                csvDispCount: 2,
                searchText: 'Search...',
            });

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('.select2').select2();
            $(<%=chkbxListLocation.ClientID%>).SumoSelect({
                selectAll: true,
                search: true,
                csvDispCount: 2,
                searchText: 'Search...',
            });

        });

    </script>
</asp:Content>
