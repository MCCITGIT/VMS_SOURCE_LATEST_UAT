<%@ Page Title="Vendor SKU Linking Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Vendor_SKU_Depot_Linking_Master_DD.aspx.vb" Inherits="Vendor_SKU_Depot_Linking_Master_DD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript" src="Scripts/ValidationDDVendorSKUDepotMaster.js"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 119) { // button Search (F8 keypress)
                __doPostBack(document.getElementById('imgbtnSearch').name, '');
            }
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

            var text = e.get_text();
            debugger;

            document.getElementById('<%=hdnSKUCode.ClientID%>').value = value;
            // getSKUDetails();
        }

        function HideLoading1() {
            var loading_icon = document.getElementById("loading");
            loading_icon.style.visibility = 'hidden';
            //loading_icon.style.visibility = (loading_icon.style.visibility == 'visible') ? 'hidden' : 'visible';
        }

        function ShowLoading1() {
            var loading_icon = document.getElementById("loading");
            loading_icon.style.visibility = 'visible';
            //loading_icon.style.visibility = (loading_icon.style.visibility == 'visible') ? 'hidden' : 'visible';
        }

        function aceSelected2(sender, e) {
            var value = e.get_value();

            var text = e.get_text();
            debugger;

            document.getElementById('<%=hdnskucode1.ClientID%>').value = value;
            // getSKUDetails();
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Vendor SKU Linking Direct</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">SKU:<span style="color: red;">*</span></label>
                                <asp:HiddenField ID="hdnskucode1" runat="server" />
                                <div class="flexInputDiv">
                                    <asp:TextBox ID="txtSkuCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                    TargetControlID="txtSkuCode"
                                    ServiceMethod="SKUSearch"
                                    MinimumPrefixLength="3"
                                    EnableCaching="false"
                                    CompletionListCssClass="autoCompleteFlyout"
                                    CompletionListItemCssClass="autoCompleteFlyoutItem"
                                    CompletionListHighlightedItemCssClass="autoCompleteFlyoutHighLightedItem"
                                    OnClientItemSelected="aceSelected2"
                                    OnClientPopulated="HideLoading1"
                                    OnClientPopulating="ShowLoading1"
                                    BehaviorID="AutoCompleteEx1"
                                    CompletionListElementID="Panel2"
                                    FirstRowSelected="true"
                                    OnClientHidden="HideLoading1"
                                    OnClientHiding="HideLoading1">
                                    <Animations>
                                    <OnShow>
                                        <Sequence>
                                            <OpacityAction Opacity="0" />
                                            <HideAction Visible="true" />
                                            <ScriptAction Script="
                                                var behavior = $find('AutoCompleteEx1');
                                                if (!behavior._height) {
                                                    var target = behavior.get_completionList();
                                                    behavior._height = target.offsetHeight - 2;
                                                    target.style.height = '0px';
                                                }" />
                                            <Parallel Duration=".4">
                                                <FadeIn />
                                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx1')._height" />
                                            </Parallel>
                                        </Sequence>
                                    </OnShow>
                                    <OnHide>
                                        <Parallel Duration=".4">
                                            <FadeOut />
                                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx1')._height" EndValue="0" />
                                        </Parallel>
                                    </OnHide>
                                    </Animations>
                                </asp:AutoCompleteExtender>
                                <img alt="Loading..." src="images/progress.gif" id="loading" class="inputLoading" />
                                <asp:Panel ID="Panel2" runat="server" Style="overflow-y: scroll; position: absolute; left: 0; top: 0; text-align: left;">
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Vendor Source:</label>
                                <asp:DropDownList ID="ddlVendor" CssClass="form-control select2" TabIndex="4" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 form-btn-mt">
                            <div class="form-group">
                                <%--<asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="btn btn-primary btn-sm" ImageUrl="~/images/ic_search.gif" />--%>
                                <asp:LinkButton ID="imgbtnSearch" runat="server" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="card">
        <div class="card-body">
            <div class="form-group row ddlPageSize">
                <label for="ddlPageSize" class="col-auto form-control-label">
                    <asp:Label ID="lblResultspPage" runat="server" Text="Results Per Page:"></asp:Label>
                </label>
                <div class="col-md-1">
                    <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnSKUCode" runat="server" />
                        <asp:HiddenField ID="hdnNewVendorName" runat="server" />
                        <asp:GridView ID="gvVendorSKUList" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" ShowFooter="true" EmptyDataText="There are No Data..."
                            OnRowCancelingEdit="gvVendorSKUList_RowCancelingEdit" OnRowEditing="gvVendorSKUList_RowEditing">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SKU Description" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblskudes" runat="server" Text='<%# Bind("SkuDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbleditskudes" runat="server" Text='<%# Bind("SkuDescription") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="lblftrskudes" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtftrSKUdsec_OnTextChanged" Enabled="true"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="lblftrskudes"
                                            ServiceMethod="SKUSearch" MinimumPrefixLength="3" EnableCaching="false"
                                            CompletionListCssClass="autoCompleteFlyout" CompletionListItemCssClass="autoCompleteFlyoutItem"
                                            CompletionListHighlightedItemCssClass="autoCompleteFlyoutHighLightedItem" OnClientItemSelected="aceSelected"
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

                                        <img alt="Loading..." src="images/progress.gif" id="loading" class="inputLoading" />
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="150" Width="400"
                                            Style="overflow-y: scroll; position: absolute; left: 0; top: 0; text-align: left;">
                                        </asp:Panel>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="21%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="21%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="21%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SKU" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSKU" runat="server" Text='<%# Bind("v_sku_code") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnskucode1" runat="server" Value='<%# Bind("v_sku_code") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbleditSKU" runat="server" Text='<%# Bind("v_sku_code") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtftrSKU" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="11%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="11%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="11%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Current Vendor" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentVendor" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnvendor" runat="server" Value='<%# Bind("v_vendor_unit") %>' />
                                        <asp:HiddenField ID="hdnvendorname" runat="server" Value='<%# Bind("vendor_name") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbleditVendor" runat="server" Text='<%# Bind("vendor_name") %>'></asp:Label>

                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblftrVendor" runat="server" Visible="true"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New Vendor" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlVendor" CssClass="form-control" TabIndex="4" Visible="true" OnSelectedIndexChanged="ddlNewVendor_SelectedIndexChanged" AutoPostBack="true"
                                            runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddleditVendor" CssClass="form-control" TabIndex="4" Visible="true" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlftrVendor" CssClass="form-control" TabIndex="1" runat="server">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Active" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--<asp:DropDownList ID="ddlActive" runat="server" Width="50px"
                                                SelectedValue='<%# Bind("active") %>'>
                                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control" Width="80px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control" Width="80px">
                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnactive" runat="server" Value='<%# Bind("active") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control" Width="80px">
                                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--<asp:ImageButton ID="btnEdit" Visible="false" CommandName="edit" runat="server" ImageUrl="~/Images/edit.jpg" />
                                        <asp:ImageButton ID="btnChange" CommandName="change" runat="server" ImageUrl="~/Images/b_save.gif" />--%>
                                        <asp:LinkButton ID="btnEdit" Visible="false" CommandName="edit" CssClass="btn btn-primary gridBtn" runat="server">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="btnChange" CommandName="change" CssClass="btn btn-success gridBtn" runat="server">Save</asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%--<asp:ImageButton ID="btnUpdate" CommandName="update" runat="server" ImageUrl="~/Images/b_save.gif" />
                                        <asp:ImageButton ID="btnCancel" CommandName="cancel" runat="server" ImageUrl="~/Images/b_cancel.gif" />--%>
                                        <asp:LinkButton ID="btnUpdate" CommandName="update" CssClass="btn btn-success gridBtn" runat="server"><i class="fa fa-check-circle" style="color:#00e731;"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" CommandName="cancel" CssClass="btn btn-primary gridBtn" runat="server"><i class="fa fa-times-circle" style="color:#e70000;"></i></asp:LinkButton>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <%--<asp:ImageButton ID="btnInsert" CommandName="insert" runat="server" ImageUrl="~/Images/b_save.gif" />--%>
                                        <asp:LinkButton ID="btnInsert" CommandName="insert" CssClass="btn btn-success gridBtn" runat="server">Save</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblErrorMessage" CssClass="errormsg" runat="server" Font-Size="10px" Font-Bold="true"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger
                            ControlID="imgbtnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPageSize"
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
