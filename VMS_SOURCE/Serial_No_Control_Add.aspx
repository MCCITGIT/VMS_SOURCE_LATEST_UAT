<%@ Page Title="Serial Number Control Add" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Serial_No_Control_Add.aspx.vb" Inherits="Serial_No_Control_Add" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="Scripts/ValidationSrlControl.js"></script>
    <script type="text/javascript">
        document.onkeydown = checkValue;
        function checkValue() {
            if (event.keyCode == 118) {  // button Add (F7 keypress)	  
                if (ValidateSNCAControls)
                    __doPostBack(document.getElementById('btnSubmit').name, '');
                else
                    return false;
            }
            else if (event.keyCode == 119) { // button Search (F8 keypress)

                __doPostBack(document.getElementById('btnCancel').name, '');
            }
            else if (event.keyCode == 120) { // button Search (F9 keypress)

                __doPostBack(document.getElementById('btnReset').name, '');
            }
            //	    else if(event.keyCode == 123){// button Pending (F12 keypress)
            //		    __doPostBack(document.getElementById('btnPending').name,'');
            //		    //alert("Pending");
            //	    }
        }
    </script>

    <div class="breadcrumbs">
        <div class="leftFung">
            <a href="Home.aspx" title="Home"><i class="fas fa-home"></i></a>
            <div class="diveider">/</div>
            <div class="pageTitleWrap">
                <h3 class="pageTitle">Serial Number Control - Add</h3>
                <p class="pageSubTitle">Define serial number ranges and controls</p>
            </div>
        </div>
        <div class="rightFung"></div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:Label ID="lblPwdErrMsg" runat="server" Style="color: Red; font-size: small; font-weight: bold;"></asp:Label>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Financial Year:<span class="mandatory" id="spanYr">*</span></label>
                        <asp:DropDownList ID="ddlFinYear" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Type of Document:<span class="mandatory" id="span1">*</span></label>
                        <asp:DropDownList ID="ddlTypeDoc" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Location:<span class="mandatory" id="span4">*</span></label>
                        <asp:DropDownList ID="ddlLocation" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Department:</label>
                        <asp:DropDownList ID="ddlDept" CssClass="form-control select2" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Prefix:</label>
                        <asp:TextBox ID="txtPrefix" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Number:<span class="mandatory" id="span2">*</span></label>
                        <asp:TextBox ID="txtNo" CssClass="form-control" MaxLength="9" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Increment:<span class="mandatory" id="span3">*</span></label>
                        <asp:TextBox ID="txtIncrement" CssClass="form-control" MaxLength="9" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label class="form-control-label">Active:</label>
                        <div class="checkRadioGroup">
                            <asp:RadioButton ID="rdActiveYes" runat="server" GroupName="Act" Text="Yes" />
                            <asp:RadioButton ID="rdInActiveNo" runat="server" GroupName="Act" Text="No" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblErrMsg" CssClass="errormsg" Visible="true" runat="server"></asp:Label>
            <div id="divErrorMessage"></div>

            <div class="row mt-3">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-success btn-sm" runat="server" Text="Submit" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-secondary btn-sm" runat="server" Text="Cancel" />
                    <asp:Button ID="btnReset" CssClass="btn btn-danger btn-sm" runat="server" Text="Reset" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
