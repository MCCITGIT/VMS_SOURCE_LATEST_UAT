<%@ Page Title="Change Password" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ChangePasswordLink.aspx.vb" Inherits="ChangePasswordLink" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidateChngPwd.js"></script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Change Password</h3>
                <p class="pageSubTitle">Set a new password for your account</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <asp:HiddenField ID="hdnOldPwd" runat="server" />
                    <asp:Label ID="lblPwdErrMsg" runat="server"></asp:Label>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">UserName:<span class="mandatory" id="span3">*</span></label>
                                <asp:TextBox ID="txtUserName" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Current Password:<span class="mandatory" id="spanOldPwd">*</span></label>
                                <asp:TextBox ID="txtOldPwd" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">New Password:<span class="mandatory" id="span1">*</span></label>
                                <asp:TextBox ID="txtNewPwd" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="form-control-label">Confirm Password:<span class="mandatory" id="span2">*</span></label>
                                <asp:TextBox ID="txtConPwd" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12 text-center form-btn-mt">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                                <asp:Button ID="btnReset" CssClass="btn btn-danger btn-sm" runat="server" Text="Reset" />
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                    <div id="divErrorMessage"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
