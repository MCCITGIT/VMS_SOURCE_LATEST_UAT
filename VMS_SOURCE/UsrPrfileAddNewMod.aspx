<%@ Page Title="User Profile Add" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UsrPrfileAddNewMod.aspx.vb" Inherits="UsrPrfileAddNewMod" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/FunctionValidator.js"></script>
    <script type="text/javascript" src="Scripts/ValidationUserProfile.js"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {

            if (event.keyCode == 118) {  // button Add (F7 keypress)
                if (document.getElementById('btnSubmit').disabled == false)
                    ValidateUPAControls();
                else
                    return false;
            }
            else if (event.keyCode == 119) { // button Search (F8 keypress)    		    	        
                __doPostBack(document.getElementById('btnCancel').name, '');
            }
            else if (event.keyCode == 120) { // button Reset (F9 keypress)
                __doPostBack(document.getElementById('btnReset').name, '');
            }
            else if (event.keyCode == 113) { // button Search (F2 keypress)    		    	        
                __doPostBack(document.getElementById('btnShowPassword').name, '');
            }
            //	    else if(event.keyCode == 123){// button Pending (F12 keypress)
            //		    __doPostBack(document.getElementById('btnPending').name,'');
            //		    //alert("Pending");
            //	    }
        }
        //-->
    </script>
    <script type="text/javascript">var cal1 = new CalendarPopup();</script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">User Profile Add</h3>
                <p class="pageSubTitle">Create a user profile and assign access</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:Label ID="lblSaveConfirmMsg" Style="color: Red; font-size: small; font-weight: bold;" runat="server"></asp:Label>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label ID="lblPassword" Style="color: Red; font-size: small; font-weight: bold;" runat="server" Visible="false"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnShowPassword" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">User ID:<span id="Span1" class="mandatory">*</span></label>
                        <asp:TextBox ID="txtUserID" ClientIDMode="Static" CssClass="form-control" MaxLength="20" TabIndex="1" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Residential Address:</label>
                        <asp:TextBox ID="txtResAddress" ClientIDMode="Static" CssClass="form-control" TabIndex="17" TextMode="MultiLine" MaxLength="300" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Depot:<span id="Span6" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlBranch" ClientIDMode="Static" CssClass="form-control select2" TabIndex="2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Department:<span id="Span7" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlDepartment" ClientIDMode="Static" CssClass="form-control select2" TabIndex="3" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">First Name:<span id="Span2" class="mandatory">*</span></label>
                        <asp:TextBox ID="txtFirstName" ClientIDMode="Static" CssClass="form-control" MaxLength="40" TabIndex="4" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Last Name:</label>
                        <asp:TextBox ID="txtLastName" ClientIDMode="Static" CssClass="form-control" MaxLength="30" TabIndex="5" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Office Phone No.:</label>
                        <asp:TextBox ID="txtOfficePhoneNo" ClientIDMode="Static" CssClass="form-control" MaxLength="50" TabIndex="18" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Extension:</label>
                        <asp:TextBox ID="txtExtension" ClientIDMode="Static" CssClass="form-control" MaxLength="10" TabIndex="19" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Short Name/ Initials:<span class="mandatory" id="spanInitial">*</span></label>
                        <asp:TextBox ID="txtShortName" ClientIDMode="Static" CssClass="form-control" MaxLength="8" TabIndex="6" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Residential Phone No.:</label>
                        <asp:TextBox ID="txtResPhoneNo" ClientIDMode="Static" CssClass="form-control" MaxLength="50" TabIndex="20" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">User Group:<span id="lblGroup3" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlUserGroup" ClientIDMode="Static" CssClass="form-control select2" TabIndex="7" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Mobile Phone No.:</label>
                        <asp:TextBox ID="txtMobilePhoneNo" ClientIDMode="Static" MaxLength="40" TabIndex="21" CssClass="form-control" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Permanent / Temporary:<span id="Span8" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlPrmtTmpy" ClientIDMode="Static" CssClass="form-control select2" TabIndex="8" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">E-mail:</label>
                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="form-control" MaxLength="100" TabIndex="22" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Designation:<span id="Span9" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlDesignation" ClientIDMode="Static" CssClass="form-control select2" TabIndex="9" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Date of Birth:</label>
                        <asp:TextBox ID="txtDOB" ClientIDMode="Static" CssClass="form-control" MaxLength="10" TabIndex="23" runat="server" placeholder="dd/mm/yyyy"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy" />
                        
                        
                        <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtDOB,'DOB','dd/MM/yyyy');">
                            <img src="images/date_icon.gif" id="DOB" alt="Calender" style="border: 0" />
                        </a>--%>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Reporting To(First):<span id="Span10" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlReportingTo1" ClientIDMode="Static" CssClass="form-control select2" TabIndex="10" AutoPostBack="true" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Reporting To(Second):</label>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlReportingTo2" ClientIDMode="Static" CssClass="form-control select2" TabIndex="11" runat="server"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlReportingTo1" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Date of Joining:<span class="clstrbg"></span></label>
                        <asp:TextBox ID="txtDOJ" ClientIDMode="Static" CssClass="form-control" MaxLength="10" TabIndex="24" runat="server" placeholder="dd/mm/yyyy"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOJ" Format="dd/MM/yyyy" />
                        <%--<a class="formCalndIcon" href="javascript:cal1.select(document.forms[0].txtDOJ,'DOJ','dd/MM/yyyy');">
                            <img src="images/date_icon.gif" id="DOJ" alt="Calender" style="border: 0" />
                        </a>--%>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Region:<span id="Span11" class="mandatory">*</span></label>
                        <asp:DropDownList ID="ddlRegion" ClientIDMode="Static" CssClass="form-control select2" runat="server" TabIndex="12" Enabled="False"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Blood Group:<span class="clstrbg"></span></label>
                        <asp:TextBox ID="txtBloodGroup" ClientIDMode="Static" MaxLength="20" CssClass="form-control" TabIndex="25" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Incentive Applicable:<span id="Span3" class="mandatory">*</span></label>
                        <div class="checkRadioGroup">
                            <asp:RadioButton ID="rbtnIncApplicableY" GroupName="Applicable" Text="Yes" TabIndex="14" runat="server" />
                            <asp:RadioButton ID="rbtnIncApplicableN" GroupName="Applicable" Text="No" TabIndex="14" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Total Past Experience[Year(s)]:</label>
                        <asp:TextBox ID="txtTotalExpYears" CssClass="form-control" MaxLength="3" TabIndex="26" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Total Past Experience[Month(s)]:</label>
                        <asp:TextBox ID="txtTotalExpMonths" MaxLength="2" CssClass="form-control" TabIndex="27" runat="server" placeholder="Enter here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Initialize Password:<span id="Span4" class="mandatory">*</span></label>
                        <div class="checkRadioGroup">
                            <asp:RadioButton ID="rbtnIntPasswordY" GroupName="Password" Text="Yes" TabIndex="15" runat="server" />
                            <asp:RadioButton ID="rbtnIntPasswordN" GroupName="Password" Text="No" TabIndex="15" runat="server" />
                        </div>
                        <asp:HiddenField ID="hdnpwd" runat="server" />
                        <asp:HiddenField ID="hdnpwd1" runat="server" />
                        <asp:HiddenField ID="hdnpwd2" runat="server" />
                        <asp:HiddenField ID="hdnpwd3" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Reason for Seperation:</label>
                        <asp:DropDownList ID="ddlReasonForSeperation" ClientIDMode="Static" CssClass="form-control select2" TabIndex="29" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Active:<span id="Span5" class="mandatory">*</span></label>
                        <div class="checkRadioGroup">
                            <asp:RadioButton ID="rbtnActiveY" GroupName="active" Text="Yes" TabIndex="16" runat="server" />
                            <asp:RadioButton ID="rbtnActiveN" GroupName="active" Text="No" TabIndex="16" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Date of Seperation:<span class="mandatory" id="spanDtSprtn"></span></label>
                        <asp:TextBox ID="txtDateOfSeperation" CssClass="form-control" MaxLength="10" TabIndex="28" runat="server" placeholder="dd/mm/yyyy"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateOfSeperation" Format="dd/MM/yyyy" />
                        
                         <%--<a class="formCalndIcon" id="aCalendar" href="javascript:cal1.select(document.forms[0].txtDateOfSeperation,'DateOfSeperation','dd/MM/yyyy');">
                            <img src="images/date_icon.gif" id="DateOfSeperation" alt="Date" style="border: 0" />
                        </a>--%>
                    </div>
                </div>
                <div class="col-md-12 mt-3">
                    <div class="form-group text-center">
                        <asp:Label ID="lblErrorMessage" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
                        <div id="divErrorMessage"></div>
                        <asp:Button ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                        <asp:Button ID="btnCancel" ClientIDMode="Static" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                        <asp:Button ID="btnReset" ClientIDMode="Static" CssClass="btn btn-danger btn-sm" runat="server" Text="Reset" />
                        <asp:Button ID="btnShowPassword" ClientIDMode="Static" runat="server" CssClass="btn btn-info btn-sm" Style="display: none;" Text="Show Pass" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">fnReasonSeperation(document.getElementById("ddlReasonForSeperation").value);</script>
</asp:Content>
