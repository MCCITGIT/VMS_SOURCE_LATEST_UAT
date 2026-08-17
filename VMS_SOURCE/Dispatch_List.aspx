<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dispatch_List.aspx.vb" Inherits="Dispatch_List" %>

<!doctype html>
<html lang="en">
<head runat="server" id="head">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Dispatch List</title>
    <link href="includes/style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />
    <link href="includes/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <link type="text/css" rel="Stylesheet" href="includes/select2.min.css" />
    <link type="text/css" rel="Stylesheet" href="includes/select2-bootstrap4.min.css" />
    <link href="includes/sumoselect.css" rel="stylesheet" />

    <link href="includes/upgrad-style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />

    <!-- JS -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript" src="Scripts/select2.full.min.js"></script>

    <style type="text/css">
        .upgradDataGrid th,
        .upgradDataGrid td {
            padding: 12px 16px !important;
            vertical-align: middle !important;
        }

        .upgradDataGrid td {
            line-height: 1.4;
        }

            .upgradDataGrid th:first-child,
            .upgradDataGrid td:first-child,
            .upgradDataGrid th:last-child,
            .upgradDataGrid td:last-child {
                padding: 12px 8px !important;
            }

        /* Override sidebar/navbar offsets from upgrad-style.css since this page has neither */
        .contentMainBody {
            margin: 0 !important;
            padding: 20px !important;
            width: 100% !important;
            max-width: 100% !important;
        }

        html, body {
            margin: 0;
            padding: 0;
        }

        .rm-vendor-label {
            color: #ffffff;
            font-size: 15px;
            font-weight: bold;
            letter-spacing: 0.3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="contentMainBody">

            <div class="breadcrumbs">
                <div class="leftFung">
                    <asp:Label ID="lblRmVendor" runat="server" CssClass="rm-vendor-label"></asp:Label>
                </div>
                <div class="rightFung"></div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3" hidden="hidden">
                            <div class="form-group">
                                <label class="form-control-label">Depot:</label>
                                <asp:DropDownList ID="ddlDepot" class="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div id="divVendor" runat="server" class="col-md-4">
                            <div class="form-group">
                                <label class="form-control-label">Vendor:</label>
                                <asp:DropDownList ID="ddlVendor" class="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="form-control-label">Status:</label>
                                <asp:DropDownList ID="ddlStatus" class="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 form-btn-mt">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDispatchList" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                            Visible="true" BorderWidth="1" CssClass="table table-hover upgradDataGrid" EmptyDataText="No records found">
                            <RowStyle CssClass="tlrowlight" />
                            <PagerStyle CssClass="PagerGrid" HorizontalAlign="Right" />
                            <HeaderStyle CssClass="headerGrid" />
                            <FooterStyle CssClass="footerGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Srl No." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Request ID" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqId" runat="server" Text='<%# Bind("orh_Id") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnReqId" Value='<%# Bind("orh_Id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendor" runat="server" Text='<%# Bind("unit_name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnVendorCode" Value='<%# Bind("orh_vendor_code") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Request Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqDate" runat="server" Text='<%# Bind("created_date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnDetails" Text=""
                                            OnClick="lbtnDetails_Click" CommandName="Details"
                                            Style="font-size: 14px">
                                            <i class="fa fa-eye"></i>
                                        </asp:LinkButton>
                                        <asp:Label runat="server" ID="lblcheck" CssClass="text-success" Visible="false"><i class="fa fa-check" aria-hidden="true"></i></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.select2').select2();
        });
    </script>
</body>
</html>
