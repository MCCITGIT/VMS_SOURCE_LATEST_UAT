<%@ Page Title="Estimation Data Upload" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EstimationDataUpload.aspx.vb" Inherits="EstimationDataUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="Scripts/ValidateEstimationUpload.js" type="text/javascript"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Estimation Data Upload</h3>
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
                                <label class="form-control-label">Process Year:</label>
                                <asp:HiddenField ID="hdnFileName" runat="server" />
                                <asp:Button ID="btnBoth" runat="server" Style="display: none" />
                                <asp:Label ID="lblYear" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Month:</label>
                                <asp:Label ID="lblMonth" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="form-control-label">Upload File:</label>
                                <div class="d-flex">
                                    <asp:FileUpload ID="Upload_File" runat="server" CssClass="form-control" />
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary btn-sm ml-2" Text="Upload" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card" id="tabGrid" runat="server">
                <div class="card-body">
                    <div class="dflexCSb">
                        <div class="form-group row ddlFinYear">
                            <label for="ddlPageSize" class="col-auto form-control-label">Select Region:</label>
                            <div class="col-auto">
                                <asp:DropDownList ID="ddlAll" runat="server" CssClass="form-control select2" AutoPostBack="True">
                                    <asp:ListItem Selected="True">ALL</asp:ListItem>
                                    <asp:ListItem>Error</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group row ddlPageSize">
                            <label for="ddlPageSize" class="col-auto form-control-label">
                                <asp:Label ID="Label4" runat="server" Text="Results Per Page: "></asp:Label>
                            </label>
                            <div class="col-auto">
                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvEstimationDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No" ItemStyle-HorizontalAlign="Left"
                                                                                                ControlStyle-Width="5%">
                                                                                                <ControlStyle Width="5%"></ControlStyle>
                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNo" runat="server" Width="94%" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Depot" DataField="est_depot" ControlStyle-Width="10%">
                                    <ControlStyle Width="10%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="SKU Code" DataField="est_sku_code" ControlStyle-Width="10%">
                                    <ControlStyle Width="10%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Average NOP" DataField="est_average_nop" ControlStyle-Width="10%">
                                    <ControlStyle Width="10%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Estimate NOP" DataField="est_estimate_nop" ControlStyle-Width="10%">
                                    <ControlStyle Width="10%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Invalid SKU" DataField="est_no_sku" ControlStyle-Width="5%">
                                    <ControlStyle Width="5%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Invalid Depot" DataField="est_no_depot" ControlStyle-Width="5%">
                                    <ControlStyle Width="5%"></ControlStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Created User" DataField="created_user" ControlStyle-Width="10%">
                                                                                                <ControlStyle Width="10%"></ControlStyle>
                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                                                HeaderText="Created Date" DataField="created_date" ControlStyle-Width="10%">
                                                                                                <ControlStyle Width="10%"></ControlStyle>
                                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                            </asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="LabelErr" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="card" id="tabSummery" runat="server" visible="false">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process Start Time:</label>
                                <asp:Label ID="lblStartTime" runat="server" CssClass="labelDataPoint"></asp:Label>
                                <asp:HiddenField ID="hdnStart" runat="server" />
                                <asp:HiddenField ID="hdnStockAsOn" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Process End Time:</label>
                                <asp:Label ID="lblEndTime" runat="server" CssClass="labelDataPoint"></asp:Label>
                                <asp:HiddenField ID="hdnEnd" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Elapsed Time:</label>
                                <asp:Label ID="lblEclapsedTime" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Total Records:</label>
                                <asp:Label ID="lblTotalRecords" runat="server" CssClass="labelDataPoint"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </div>
            </div>


            <div runat="server" id="divPopup" ClientIDMode="Static" style="display: none; position: absolute; background-color: #eeeeee; width: 300px; height: 100px; z-index: 9002;">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" style="font-size: 16px">File with same name already exists.Do you want to Delete and Poceed?
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="but4" Width="40px" />
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="but4" Width="40px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="blanket" style="display: none;"></div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
