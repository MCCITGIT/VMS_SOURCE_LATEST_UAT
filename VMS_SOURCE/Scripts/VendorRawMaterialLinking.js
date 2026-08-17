var firstErrorControl;
var errMsg;
function validateAddRawmaterial() {

    firstErrorControl = "";
    errMsg = "";
    //ValidateDropDown1("ddlVendor", "Please Select Vendor.");
    ValidateRequired("txtVendorSearch", "Please enter Vendor.");
    ValidateRequired("txtSearchText", "Please enter Raw Material.");

    var selectedVendorCode = "";
    var selectedRawMatCode = "";
    var vendorControl = document.getElementById("ddlVendor");
    if (vendorControl) {
        selectedVendorCode = (vendorControl.value || "").trim().toUpperCase();
    }
    var rawMatCodeControl = document.getElementById("txtrawmatid");
    if (rawMatCodeControl) {
        selectedRawMatCode = (rawMatCodeControl.value || "").trim().toUpperCase();
    }

    if (selectedVendorCode !== "" && selectedRawMatCode !== "") {
        var grid = document.getElementById("gvVendorRawMat");
        if (grid) {
            var rows = grid.querySelectorAll("tr");
            for (var i = 1; i < rows.length; i++) {
                var cells = rows[i].getElementsByTagName("td");
                if (cells.length > 1) {
                    var vendorCodeControl = rows[i].querySelector("input[id$='hdnVendorCode']");
                    var existingVendorCode = vendorCodeControl ? (vendorCodeControl.value || "").trim().toUpperCase() : "";
                    var existingRawMatCode = (cells[1].innerText || cells[1].textContent || "").trim().toUpperCase();
                    if (existingVendorCode === selectedVendorCode && existingRawMatCode === selectedRawMatCode) {
                        if (firstErrorControl == "") {
                            firstErrorControl = "txtSearchText";
                        }
                        errMsg += GetErrorRow("txtSearchText", "Selected Raw Material already added.");
                        break;
                    }
                }
            }
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
        if (confirm("Are you sure to add?")) {
            document.getElementById("btnAdd").disabled = true;
            __doPostBack(document.getElementById("btnAdd").name, '');
            return true;
        }
        else {
            return false;
        }
    }
}
function validateVendorRawMaterialLinkAdd() {

    firstErrorControl = "";
    errMsg = "";
    //ValidateDropDown1("ddlVendor", "Please Select Vendor.");
    //ValidateRequired("txtVendorSearch", "Please enter Vendor.");
    var grid = document.getElementById("gvVendorRawMat");
    var hasDataRow = false;
    var rateInputs = [];
    if (grid) {
        var bodyRows = grid.querySelectorAll("tr");
        hasDataRow = bodyRows.length > 1;
        rateInputs = grid.querySelectorAll("input[id$='txtRate']");
    }

    if (!hasDataRow) {
        if (firstErrorControl == "") {
            firstErrorControl = "gvVendorRawMat";
        }
        errMsg += GetErrorRow("gvVendorRawMat", "Please add at least one Raw Material.");
    }

    if (hasDataRow) {
        for (var i = 0; i < rateInputs.length; i++) {
            var rateValue = (rateInputs[i].value || "").trim();
            if (rateValue === "") {
                if (firstErrorControl == "") {
                    firstErrorControl = rateInputs[i].id;
                }
                errMsg += GetErrorRow(rateInputs[i].id, "Please enter Rate.");
                break;
            }
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