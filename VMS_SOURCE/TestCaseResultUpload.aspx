<%@ Page Title="Test Result Upload" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TestCaseResultUpload.aspx.vb" Inherits="TestCaseResultUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<%--    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateUnitApplicableVendorAssign.js?key=<%= DateTime.Now.ToString %>"></script>--%>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                __doPostBack(document.getElementById('imgbtnAdd').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('imgbtnSearch').name, '');
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }
    </script>

    <script type="text/javascript">
        function showProgress() {
            var updateProgress = document.getElementById('updProgress');
            updateProgress.style.display = "block";
        }

        function checkValidation() {
            const ddlVendor = document.getElementById("ddlVendor").value;
            const ddlBrand = document.getElementById("ddlBrand").value;
            const ddlProduct = document.getElementById("ddlProduct").value;

            if (ddlVendor === "") {
                alert("Please select vendor");
                return false;
            }
            else if (ddlBrand === "") {
                alert("Please select brand");
                return false;
            }
            else if (ddlProduct === "") {
                alert("Please select product");
                return false;
            }
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Test Result Upload</h3>
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
                                <label class="form-control-label">Vendor:</label>
                                <asp:DropDownList ID="ddlVendor" ClientIDMode="Static" CssClass="form-control select2" runat="server" AutoPostBack="true" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Brand:</label>
                                <asp:DropDownList ID="ddlBrand" ClientIDMode="Static" CssClass="form-control select2" runat="server" AutoPostBack="True" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Product:</label>
                                <asp:DropDownList ID="ddlProduct" ClientIDMode="Static" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Upload File:</label>
                                <%--<asp:AsyncFileUpload runat="server" ID="AsyncFileUpload1" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" class="form-control" />--%>
                                <asp:FileUpload runat="server" CssClass="form-control" ClientIDMode="Static" ID="FileUpload1" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" />
                            </div>
                        </div>
                        <div class="col-md-12 form-btn-mt text-center">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <asp:Button ID="btnUpload" runat="server" ClientIDMode="Static" ToolTip="Click to Upload File" Text="Upload" CssClass="btn btn-primary btn-sm" OnClientClick="showProgress()" />
                                    <asp:Button ID="imgbtnAdd" runat="server" ClientIDMode="Static" ToolTip="Click to Download File" Text="Download" CssClass="btn btn-success btn-sm" OnClientClick="return checkValidation()" />
                                    <asp:Button ID="btnReset" runat="server" ClientIDMode="Static" ToolTip="Click to Reset" Text="Reset" CssClass="btn btn-warning btn-sm" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="imgbtnAdd" />
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <%--<div class="col-md-12 form-btn-mt text-center" runat="server">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="progressBarContainer">
                                        <div id="progressBar" runat="server"></div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>--%>
                        <div class="col-md-12 form-btn-mt text-center" runat="server">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                <ContentTemplate>
                                    <div runat="server" id="divFileError" visible="false">
                                        <asp:HiddenField runat="server" ID="hdnErrorFilePath" Value="" />
                                        <span class="text-danger">There are some error found in file. Please download the file and review. </span>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnDownloadErrorFile" runat="server" ToolTip="Click to Download File" Text="Download File" CssClass="btn btn-primary btn-sm" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnDownloadErrorFile" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <%-- </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </div>
            </div>

            <div class="card" style="display: none;">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="form-control-label">Product:</label>
                                <%--<asp:DropDownList ID="ddlProduct" class="form-control" runat="server" AutoPostBack="true" />--%>
                                <asp:TextBox ID="txtProduct" runat="server" class="form-control"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdnProduct" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Shade:</label>
                                <asp:TextBox ID="txtShade" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Batch No:</label>
                                <asp:TextBox ID="txtBatchNo" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Batch Date:</label>
                                <asp:TextBox ID="txtBatchDate" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvTestList" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" AllowPaging="true" PageSize="20" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                                            <RowStyle CssClass="tlrowlight" />
                                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <FooterStyle CssClass="footerGrid" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Slno" ControlStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlno" Text='<%# Bind("slno") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Name" ControlStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTestName" Text='<%# Bind("test_name") %>' runat="server" />
                                                        <asp:HiddenField runat="server" ID="hdnTestId" Value='<%# Bind("th_test_id") %>' />
                                                    </ItemTemplate>
                                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Frequency" ControlStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFrequency" Text='<%# Bind("frequency")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Min Value" ControlStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinValue" Text='<%# Bind("min_value")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Max Value" ControlStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaxValue" Text='<%# Bind("max_value")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" ControlStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" Text='<%# Bind("uom")%>' runat="server" />
                                                    </ItemTemplate>
                                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual" ControlStyle-Width="90%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActual" Text='<%# Bind("result_value") %>' runat="server" />
                                                        <asp:HiddenField runat="server" ID="hdnResultValue" Value='<%# Bind("result_value") %>' />
                                                    </ItemTemplate>
                                                    <ControlStyle Height="90%" Width="90%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ControlStyle-Width="100%">
                                                    <HeaderTemplate>
                                                        <span>View</span>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Panel runat="server" ID="pnlValid" Visible="false"><i class="fas fa-check-circle checkIcon"></i></asp:Panel>
                                                        <asp:Panel runat="server" ID="pnlInvalid" Visible="false"><i class="fas fa-times-circle crossIcon"></i></asp:Panel>
                                                        <asp:HiddenField runat="server" ID="hdnValidYN" Value='<%# Bind("valid_result_yn") %>' />
                                                        <%--<asp:Button ID="imgBtnSubmit" Visible="true" runat="server" CssClass="btn btn-info gridBtn" Text="View" title="View" ToolTip="View" CommandName="EditTest"></asp:Button>--%>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="100%"></ControlStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="btnUpload" EventName="Click" />--%>
                                    <asp:PostBackTrigger ControlID="gvTestList" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" ToolTip="Submit" Text="Submit" CssClass="btn btn-success btn-sm" />
                        </div>
                    </div>
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
