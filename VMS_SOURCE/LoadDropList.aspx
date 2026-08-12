<%@ Page Title="Load Drop List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LoadDropList.aspx.vb" Inherits="LoadDropList" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {  // button Add (F7 keypress)	    		    
                __doPostBack(document.getElementById('ImgbtnAdd').name, '');
            }
            else if (event.keyCode == 119) { // button Search (F8 keypress)

                __doPostBack(document.getElementById('ImgbtnSearch').name, '');
            }
        }
        //-->
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <h3 class="pageTitle">Load Drop List</h3>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Depot:</label>
                        <asp:DropDownList ID="ddlBranch" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Vendor:</label>
                        <asp:DropDownList ID="ddlVendor" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">SKU Code:</label>
                        <asp:TextBox ID="txtSearchUserName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3 form-btn-mt">
                    <div class="form-group">
                        <%--<asp:ImageButton CssClass="btn btn-primary btn-sm" ImageUrl="images/ic_search.gif" ID="ImgbtnSearch" runat="server" />
                        <asp:ImageButton CssClass="btn btn-success btn-sm" ImageUrl="images/ic_add.gif" ID="ImgbtnAdd" PostBackUrl="~/LoadDropAddUpdate.aspx" runat="server" />--%>
                        <asp:LinkButton CssClass="btn btn-primary btn-sm" ID="ImgbtnSearch" runat="server" OnClick="ImgbtnSearch_Click">Search</asp:LinkButton>
                        <asp:LinkButton CssClass="btn btn-success btn-sm" ID="ImgbtnAdd" PostBackUrl="~/LoadDropAddUpdate.aspx" runat="server">Add</asp:LinkButton>
                    </div>
                </div>

            </div>
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
                <asp:GridView ID="gvLoadDrop" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                    Visible="true" OnRowDataBound="gvLoadDrop_RowDataBound" BorderWidth="1" CssClass="table table-hover upgradDataGrid" OnPageIndexChanging="gvLoadDrop_IndexChanging">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" HeaderText="S.No" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Vendor Unit" DataField="vendor_name"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Depot" DataField="depot_name"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="SKU Code" DataField="sku_code"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="SKU Description" DataField="SkuDescription"></asp:BoundField>
                    </Columns>
                </asp:GridView>
                <div id="Div_User_List_Grid" runat="server" visible="false">
                    <table border="1" class="table table-hover upgradDataGrid">
                        <tr class="headerGrid">
                            <th style="text-align: center;">Sl.No.</th>
                            <th style="text-align: center;">Vendor Unit</th>
                            <th style="text-align: center;">Depot</th>
                            <th style="text-align: center;">SKU Code</th>
                            <th style="text-align: center;">SKU Description</th>
                        </tr>
                        <tr class="tlrowlight">
                            <td colspan="7">No Records Found</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
