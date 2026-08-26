<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="QC_RMAllocation.aspx.vb" Inherits="QC_RMAllocation" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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


    <asp:UpdateProgress ID="updProgress" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="pageLoader">
                <div class="innerLoader">
                    <img class="loaderImg" alt="progress" src="images/ajax-loader.gif" />
                    <p class="loaderTx">Processing... Please Wait.</p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

   

    <div class="container">
        <div class="breadcrumbs">
            <div class="leftFung">
                <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
                <div class="diveider">/</div>
                <h3 class="pageTitle">RM Allocation Upload</h3>
            </div>
            <div class="rightFung"></div>
        </div>
    </div>

    <div class="container">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <label class="form-control-label">Quarter:</label>
                                    <asp:DropDownList ID="ddlQuarter" class="form-control form-control-sm select2" runat="server" AutoPostBack="true" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <%-- <div class="col-md-3">
                            <div class="form-group">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                    <ContentTemplate>
                                        <label class="form-control-label">Vendor:</label>
                                        <asp:DropDownList ID="ddlVendor" class="form-control form-control-sm select2" runat="server" AutoPostBack="true" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>--%>

                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <label class="form-control-label">Upload File:</label>
                                    <%--<asp:AsyncFileUpload runat="server" ID="AsyncFileUpload1" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" class="form-control form-control-sm" />--%>
                                    <asp:FileUpload runat="server" class="form-control form-control-sm" ID="fuExcel" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-md-3 form-btn-mt">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <asp:Button ID="btnUpload" runat="server" ToolTip="Click to Upload File" Text="Upload" CssClass="btn btn-primary btn-sm" OnClick="btnUpload_Click" OnClientClick="showProgress()" />
                                <asp:Button ID="btnDownload" runat="server" ToolTip="Click to Download File" Text="Download" CssClass="btn btn-success btn-sm" OnClick="btnDownload_Click" />
                                <asp:Button ID="btnReset" runat="server" ToolTip="Click to Reset" Text="Reset" CssClass="btn btn-warning btn-sm" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnDownload" />
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
    </div>

</asp:Content>
