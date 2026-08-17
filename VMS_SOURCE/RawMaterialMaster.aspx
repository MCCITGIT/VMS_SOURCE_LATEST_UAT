<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RawMaterialMaster.aspx.vb" Inherits="RawMaterialMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .product-search-group {
            display: flex;
            align-items: stretch;
        }

            .product-search-group .form-control {
                border-top-right-radius: 0;
                border-bottom-right-radius: 0;
            }

            .product-search-group .product-reset-btn {
                border-top-left-radius: 0;
                border-bottom-left-radius: 0;
                min-width: 36px;
                display: flex;
                align-items: center;
                justify-content: center;
            }
        /* AutoComplete dropdown container */
        .raw-material-autocomplete {
            max-height: 250px;
            overflow-y: auto;
            overflow-x: hidden;
            background: #fff;
            border: 1px solid #ced4da;
            border-radius: 0 0 5px 5px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.12);
            z-index: 99999 !important;
            width: auto !important;
        }

            /* Individual autocomplete item */
            .raw-material-autocomplete div {
                padding: 9px 12px;
                font-size: 14px;
                color: #343a40;
                cursor: pointer;
                border-bottom: 1px solid #f1f1f1;
                background: #fff;
            }

                /* Hover / selected item */
                .raw-material-autocomplete div:hover,
                .raw-material-autocomplete .autocomplete_highlightedListItem {
                    background-color: #f0f6ff !important;
                    color: #007bff !important;
                }

                /* Last item */
                .raw-material-autocomplete div:last-child {
                    border-bottom: none;
                }
    </style>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) { // button Add (F7 keypress)
                if (document.getElementById('btnSubmit').disabled == true)
                    return false;
                else {
                    // button Add (F7 keypress)
                    validateSKUList();
                }
                //__doPostBack(document.getElementById('btnSubmit').name, '');
            }
            else if (event.keyCode == 119) {
                __doPostBack(document.getElementById('btnCancel').name, '');
            }
        }

        function disableBackButton() {
            window.history.forward(1);
        }

        function onRawMaterialSelected(sender, e) {
            var value = e.get_value();
            var text = e.get_text();
            document.getElementById('<%=txtrawmatid.ClientID%>').value = value;
            document.getElementById('<%=txtSearchText.ClientID%>').value = text + " (" + value + ")";
            sender.get_element().value = text + " (" + value + ")";
        }

        function clearRawMaterialSelection() {
            document.getElementById('<%=txtrawmatid.ClientID%>').value = '';
        }

        function resetProductField() {
            var rawMatText = document.getElementById('<%=txtSearchText.ClientID%>');
            var rawMatCode = document.getElementById('<%=txtrawmatid.ClientID%>');

            if (rawMatText) {
                rawMatText.value = '';
            }
            if (rawMatCode) {
                rawMatCode.value = '';
            }

            return false;
        }
    </script>
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidateRawMaterialMaster.js?time=<%= DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.fff") %>"></script>
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Raw Material Tracking Configuration</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group pb-0">
                        <label class="form-control-label">Search Raw Material:<span id="Span2" class="mandatory">*</span></label>
                        <div class="input-group product-search-group">
                            <asp:TextBox ID="txtSearchText" ClientIDMode="Static" class="form-control" runat="server" AutoComplete="Off" onkeyup="clearRawMaterialSelection();" Placeholder="Enter Here"></asp:TextBox>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-outline-secondary product-reset-btn" onclick="resetProductField(); return false;" title="Reset SKU"><i class="fas fa-sync-alt fa-xs"></i></button>
                            </div>
                        </div>
                        <asp:HiddenField ID="txtrawmatid" ClientIDMode="Static" runat="server" />
                        <asp:AutoCompleteExtender ID="aceRawMaterialSearch" runat="server"
                            TargetControlID="txtSearchText"
                            ServiceMethod="RawMaterialSearch"
                            CompletionInterval="200"
                            EnableCaching="false"
                            CompletionSetCount="20"
                            FirstRowSelected="true"
                            OnClientItemSelected="onRawMaterialSelected"
                            CompletionListCssClass="raw-material-autocomplete">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
                <div class="col-md-12 form-btn-mt text-center">
                    <asp:Button ID="btnSubmit" ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary btn-sm" OnClick="btnCancel_Click" />
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblErrorMessage" ClientIDMode="Static" CssClass="errormsg" Visible="true" runat="server" Style="text-align: left; font-size: 13px; font-weight: bold;color:red;" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView BorderWidth="1" CssClass="table table-hover upgradDataGrid" CellSpacing="0" CellPadding="0"
                    ID="gvrawMatDetails" runat="server" AutoGenerateColumns="false" AllowPaging="false" Visible="true"
                    ShowFooter="false" GridLines="both">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl No" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblrawmatdid" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Raw Material Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblrawmatname" runat="server" Text='<%# Bind("Raw_Mat_Name") %>'></asp:Label>
                                <asp:HiddenField ID="hdnrawmatid" runat="server" Value='<%# Bind("Raw_Mat_Code") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblactiveText" runat="server" Text='<%# IIf(UCase(Trim(CStr(Eval("active")))) = "Y", "Yes", "No") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlactive" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" CommandName="Edit" runat="server" CssClass="btn btn-info btn-sm gridBtn" ToolTip="Edit"><i class="fas fa-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" CommandName="Update" CssClass="btn btn-success btn-sm gridBtn mr-1" runat="server" ToolTip="Update" OnClientClick="return confirm('Are you sure you want to update this record?');"><i class="fas fa-check"></i></asp:LinkButton>
                                <asp:LinkButton ID="btncancel" CommandName="Cancel" CssClass="btn btn-danger btn-sm gridBtn" runat="server" ToolTip="Cancel"><i class="fas fa-times"></i></asp:LinkButton>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

