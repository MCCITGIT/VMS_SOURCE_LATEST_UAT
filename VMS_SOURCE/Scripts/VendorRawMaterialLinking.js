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
        return rmFailValidation(errMsg);
    }
    else {
        document.getElementById("lblErrorMessage").innerHTML = '';
        return rmConfirmPostback("btnAdd", "add");
    }
}
function validateVendorRawMaterialLinkAdd() {

    firstErrorControl = "";
    errMsg = "";
    //ValidateDropDown1("ddlVendor", "Please Select Vendor.");
    //ValidateRequired("txtVendorSearch", "Please enter Vendor.");
    var grid = document.getElementById("gvVendorRawMat");
    var hasDataRow = false;
    if (grid) {
        var bodyRows = grid.querySelectorAll("tr");
        hasDataRow = bodyRows.length > 1;
    }

    if (!hasDataRow) {
        if (firstErrorControl == "") {
            firstErrorControl = "gvVendorRawMat";
        }
        errMsg += GetErrorRow("gvVendorRawMat", "Please add at least one Raw Material.");
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        return rmFailValidation(errMsg);
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = '';
        return rmConfirmPostback("btnSubmit", "submit");
    }
}