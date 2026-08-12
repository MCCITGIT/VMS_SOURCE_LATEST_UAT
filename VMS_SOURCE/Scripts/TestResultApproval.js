

var firstErrorControl;
var errMsg;
function TestresultApproval() {

    firstErrorControl = "";
    errMsg = "";
    //ValidateDropDown1("ddlVendor", "Please Select Vendor.");
    ValidateRequired("txtRemarks", "Please enter Remarks.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to Reject?")) {
            document.getElementById("btnReject").disabled = true;
            //document.getElementById("ctl00_ContentPlaceHolder1_btnReset").disabled = true;
            //document.getElementById(btnSave).click();
            __doPostBack(document.getElementById("btnReject").name, '');
            //document.getElementById(btnSave).disabled = true;
            return true;
        }
        else {
            return false;
        }
    }
}
