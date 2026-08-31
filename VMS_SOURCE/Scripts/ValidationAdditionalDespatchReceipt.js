

var firstErrorControl;
var errMsg;

function ValidateAdditionalReceiptDetails() {
    firstErrorControl = "";
    errMsg = "";

    if (ValidateRequired("txtChallanDate", "Please enter Challan Date.")) {
        CheckDateFormat("txtChallanDate", "Invalid Challan Date.");
    }
    
    ValidateDropDown("ddlRegion", "Please select a Region.");
    ValidateDropDown("ddlSource", "Please select Source.");
    //ValidateDropDown("ddlProcessYear", "Please select Process Year.");

    ValidateRequired("txtTransporterName", "Please enter Transporter Name.");
    ValidateRequired("txtRoadPermitNo", "Please enter Road Permit No.");
    ValidateRequired("txtTruckNo", "Please enter Truck No.");
    ValidateRequired("txtCenvatNo", "Please enter Vendor Challan No.");
    
    if (ValidateRequired("txtCenvatDate", "Please enter Vendor Challan Date.")) {
        CheckDateFormat("txtCenvatDate", "Invalid Vendor Challan Date.");
    }
    
    ValidateRequired("txtReceivedLtr", "Please enter Received Ltr.");
    ValidateRequired("txtReceivedKg", "Please enter Received Kg.");

    if (ValidateRequired("txtReceiptDate", "Please enter Receipt Date.")) {
        if (CheckDateFormat("txtReceiptDate", "Invalid Receipt Date.")) {
            ValidateGThanSystemDate("txtReceiptDate", "Receipt Date cannot be greater than Today's Date.");
        }
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to submit?")) {
            document.getElementById("btnSubmit").disabled = true;
            //document.getElementById("ctl00_ContentPlaceHolder1_btnReset").disabled = true;
            //document.getElementById(btnSave).click();
            __doPostBack(document.getElementById("btnSubmit").name, '');
            //document.getElementById(btnSave).disabled = true;
            return true;
        }
        else {
            return false;
        }
    }
}