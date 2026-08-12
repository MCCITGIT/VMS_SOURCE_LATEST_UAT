<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePasswordLink_New.aspx.vb" Inherits="ChangePasswordLink" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="includes/Footer.ascx" %>

<!doctype html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Change Password</title>
    <link href="includes/style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />
    <link href="includes/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="includes/upgrad-style.css?v=@DateTime.Now.ToString()" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Scripts/ValidateChngPwd.js"></script>
    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/Messages.js"></script>
    <script type="text/javascript" src="Scripts/AjaxServices.js"></script>
    <script>
        function togglePasswordVisibility(inputId, iconContainer) {
            var input = document.getElementById(inputId);
            var eyeIcon = iconContainer.querySelector('.fa-eye');
            var eyeSlashIcon = iconContainer.querySelector('.fa-eye-slash');

            if (input.type === 'password') {
                input.type = 'text';
                eyeIcon.classList.add('d-none');
                eyeSlashIcon.classList.remove('d-none');
            } else {
                input.type = 'password';
                eyeIcon.classList.remove('d-none');
                eyeSlashIcon.classList.add('d-none');
            }
        }
    </script>
</head>

<body class="loginBg">
    <form id="form1" runat="server">
        <div class="loginFullBody" style="align-items: center;">
            <div class="loginCBox changePwPage">
                <div class="loginFormArea">
                    <div class="compyDtls">
                        <img id="logo" class="bLogo" src="images/h-b-logo.png" alt="Best Wall Paint Colors, House Painting Colors" title="Lewis Berger Paints" />
                        <h3 class="bTitle">Berger Paints India Limited</h3>
                        <h4 class="canhPwTx">Change Password</h4>
                    </div>
                    <asp:HiddenField ID="hdnOldPwd" runat="server" />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="form-control-label">Username:<span class="mandatory" id="span3">*</span></label>
                                <asp:TextBox ID="txtUserName" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server" placeholder="Enter here..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group pRelative">
                                <label class="form-control-label">Current Password:<span class="mandatory" id="spanOldPwd">*</span></label>
                                <asp:TextBox ID="txtOldPwd" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server" placeholder="Enter here..."></asp:TextBox>
                                <div class="pwShowHideIcon" onclick="togglePasswordVisibility('<%= txtOldPwd.ClientID %>', this)">
                                    <i class="fas fa-eye"></i>
                                    <i class="fas fa-eye-slash d-none"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="form-control-label">New Password:<span class="mandatory" id="span1">*</span></label>
                                <asp:TextBox ID="txtNewPwd" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server" placeholder="Enter here..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group pRelative">
                                <label class="form-control-label">Confirm Password:<span class="mandatory" id="span2">*</span></label>
                                <asp:TextBox ID="txtConPwd" CssClass="form-control" TextMode="Password" MaxLength="20" runat="server" placeholder="Enter here..."></asp:TextBox>
                                <div class="pwShowHideIcon" onclick="togglePasswordVisibility('<%= txtConPwd.ClientID %>', this)">
                                    <i class="fas fa-eye"></i>
                                    <i class="fas fa-eye-slash d-none"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 mt-2">
                            <div class="form-group text-center">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="Cancel" />
                                <asp:Button ID="btnReset" CssClass="btn btn-danger" runat="server" Text="Reset" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                            <div id="divErrorMessage"></div>
                        </div>
                    </div>
                    <asp:Label ID="lblPwdErrMsg" runat="server" CssClass="errormsg"></asp:Label>
                    <div style="display: none">
                        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
                    </div>
                    <p class="copyRight text-center">© Copyright 2012 Management and Computer Consultant. <a href="https://mccit.co.in/" target="_blank" title="Management and Computer Consultants">www.mccit.co.in</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
