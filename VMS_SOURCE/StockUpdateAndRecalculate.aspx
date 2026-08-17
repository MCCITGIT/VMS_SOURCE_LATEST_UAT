<%@ Page Title="Stock Update" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="StockUpdateAndRecalculate.aspx.vb" Inherits="StockUpdateAndRecalculate" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="Scripts/ValidateStockUpdate.js" type="text/javascript"></script>
  
    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Stock Update</h3>
                <p class="pageSubTitle">Update stock figures and recalculate balances</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

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
                        <label class="form-control-label">Process Type:</label>
                        <div class="checkRadioGroup">
                            <asp:RadioButton ID="rbtnUpload" runat="server" Text="Upload Only" GroupName="Process" AutoPostBack="True" />
                            <asp:RadioButton ID="rbtnUpdate" runat="server" Text="Update Only  " GroupName="Process" AutoPostBack="True" />
                            <asp:RadioButton ID="rbtnBoth" runat="server" AutoPostBack="True" GroupName="Process" Text="Both" />
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="form-control-label">Process Month:</label>
                        <div class="dFlexC">
                            <asp:FileUpload ID="Upload_File" runat="server" CssClass="form-control" />
                            <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate--%>
                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary btn-sm ml-2" Text="Upload" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:LinkButton CssClass="btn btn-info btn-sm" ID="ImageButton2" runat="server" AlternateText="Home" OnClick="ImageButton2_Click">Print</asp:LinkButton>
                    <%--<asp:ImageButton CssClass="btn btn-info btn-sm" ID="ImageButton2" runat="server" AlternateText="Home" ImageUrl="~/images/printButton.png" />--%>
                </div>
            </div>
        </div>
    </div>

    <div class="card" id="tabGrid" runat="server">
        <div class="card-body">
            <div class="dflexCSb">
                <div class="form-group row ddlFinYear">
                    <label for="ddlPageSize" class="col-auto form-control-label">Type</label>
                    <div class="col-auto">
                        <asp:DropDownList ID="ddlAll" runat="server" CssClass="form-control select2" AutoPostBack="True">
                            <asp:ListItem Selected="True">ALL</asp:ListItem>
                            <asp:ListItem>Error</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row ddlPageSize">
                    <label for="ddlPageSize" class="col-auto form-control-label">
                        <asp:Label ID="Label4" runat="server" Text="Results Per Page:"></asp:Label>
                    </label>
                    <div class="col-auto">
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="gvStockDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                    Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No" ItemStyle-HorizontalAlign="Left"
                            ControlStyle-Width="5%">
                            <ControlStyle Width="5%"></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Year" DataField="stk_fin_year" ControlStyle-Width="10%">
                            <ControlStyle Width="10%"></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Month" DataField="stk_fin_month" ControlStyle-Width="5%">
                            <ControlStyle Width="5%"></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="As on Date" DataField="stk_ason_date" ControlStyle-Width="10%">
                            <ControlStyle Width="10%"></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Depot" DataField="stk_depot" ControlStyle-Width="5%">
                            <ControlStyle Width="5%"></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="SKU Code" DataField="stk_sku_code" ControlStyle-Width="15%">
                            <ControlStyle Width="15%"></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="Stock NOP" DataField="stk_stock_nop" ControlStyle-Width="5%">
                            <ControlStyle Width="5%"></ControlStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
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
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnProcess" runat="server" CssClass="btn btn-primary btn-sm" Style="display: none;" Text="Process" />
                </div>
            </div>
            <asp:Label ID="LabelErr" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <table id="tabSummery" runat="server" visible="false" border="1" class="table table-hover upgradDataGrid">
                    <tr>
                        <td style="background-color: #E6F5FB; width: 50%; text-align: right">Process Start Time :
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblStartTime" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnStart" runat="server" />
                            <asp:HiddenField ID="hdnStockAsOn" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #E6F5FB; width: 50%; text-align: right">Process End Time :
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblEndTime" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnEnd" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #E6F5FB; width: 50%; text-align: right">Elapsed Time :
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblEclapsedTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #E6F5FB; width: 50%; text-align: right">Total Records :
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                                                                    <td style="background-color: #E6F5FB; width:50%; text-align:right ">
                                                                        Records Inserted :</td>
                                                                    <td style="text-align:left">
                                                                            <asp:Label ID="lblRecInserted" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="background-color: #E6F5FB; width:50%; text-align:right ">
                                                                        Records Updated :</td>
                                                                    <td style="text-align:left">
                                                                            <asp:Label ID="lblRecUpdated" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>--%>
                </table>
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
                    <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-success btn-sm" />
                </td>
                <td style="text-align: center">
                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-danger btn-sm" />
                </td>
            </tr>
        </table>
    </div>
    <div id="blanket" style="display: none;"></div>
</asp:Content>
