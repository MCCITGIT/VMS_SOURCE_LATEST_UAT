<%@ Page Title="Serial Number Control List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Serial_No_Control_List.aspx.vb" Inherits="Serial_No_Control_List" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {  // button Add (F7 keypress)	    		    
                __doPostBack(document.getElementById('ImgbtnAdd').name, '');
            }
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Serial Number Control</h3>
                <p class="pageSubTitle">Browse serial number control records</p>
            </div>
        </div>
        <div class="rightFung">
            <asp:LinkButton ID="ImgbtnAdd" runat="server" CssClass="btn btn-success btn-sm" OnClick="ImgbtnAdd_Click">Add</asp:LinkButton>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <div class="dflexCSb">
                <div class="form-group row ddlPageSize">
                    <label for="ddlPageSize" class="col-auto form-control-label">
                        <asp:Label ID="lblPageSize" runat="server" Text="Results Per Page:"></asp:Label>
                    </label>
                    <div class="col-auto">
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row ddlFinYear">
                    <label for="ddlFinYear" class="col-auto form-control-label">Fin Year:</label>
                    <div class="col-auto">
                        <asp:DropDownList ID="ddlFinYear" CssClass="form-control select2" AutoPostBack="true" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="gvSerialNoControl" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                    Visible="true" OnPageIndexChanging="gvSerialNoControl_PageIndexChanging" BorderWidth="1" CssClass="table table-hover upgradDataGrid">
                    <RowStyle CssClass="tlrowlight" />
                    <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                    <HeaderStyle CssClass="headerGrid" />
                    <FooterStyle CssClass="footerGrid" />
                    <Columns>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Select"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="DOC Type" DataField="srl_doc_type"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Branch" DataField="srl_branch"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Department" DataField="srl_dept"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Prefix" DataField="srl_prefix"></asp:BoundField>
                        <asp:BoundField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Number" DataField="srl_no"></asp:BoundField>
                    </Columns>
                </asp:GridView>

                <div id="Div_Serial_No_Control_Grid" runat="server" visible="false">
                    <table border="1" class="table table-hover upgradDataGrid">
                        <tbody>
                            <tr class="headerGrid">
                                <th style="text-align: center;">Select</th>
                                <th style="text-align: center;">DOC Type</th>
                                <th style="text-align: center;">Company</th>
                                <th style="text-align: center;">Branch</th>
                                <th style="text-align: center;">Department </th>
                                <th style="text-align: center;">Prefix</th>
                                <th style="text-align: center;">Number</th>
                            </tr>
                            <tr class="tlrowlight">
                                <td colspan="7">No Records Found</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
