var firstErrorControl;
var errMsg;

function allowRateTwoDecimal(evt, control) {
    var charCode = evt.which ? evt.which : evt.keyCode;

    if (charCode === 8 || charCode === 9 || charCode === 13 || charCode === 37 || charCode === 39 || charCode === 46) {
        return true;
    }

    var charValue = String.fromCharCode(charCode);
    if (!/[0-9.]/.test(charValue)) {
        return false;
    }

    var value = control.value || "";
    if (charValue === ".") {
        return value.indexOf(".") === -1;
    }

    var dotIndex = value.indexOf(".");
    if (dotIndex !== -1) {
        var decimals = value.substring(dotIndex + 1);
        var hasSelection = control.selectionStart !== control.selectionEnd;
        if (!hasSelection && control.selectionStart > dotIndex && decimals.length >= 2) {
            return false;
        }
    }

    return true;
}

function sanitizeRateTwoDecimal(control) {
    var value = control.value || "";
    value = value.replace(/[^0-9.]/g, "");

    if (value.indexOf(".") !== -1) {
        var parts = value.split(".");
        value = parts[0] + "." + parts.slice(1).join("");
    }

    var dotIndex = value.indexOf(".");
    if (dotIndex !== -1) {
        var intPart = value.substring(0, dotIndex);
        var decPart = value.substring(dotIndex + 1, dotIndex + 3);
        value = intPart + "." + decPart;
    }

    control.value = value;
}

function formatRateTwoDecimal(control) {
    var value = (control.value || "").trim();
    if (value === "") {
        return;
    }

    var numValue = parseFloat(value);
    if (isNaN(numValue)) {
        control.value = "";
        return;
    }

    control.value = numValue.toFixed(2);
}

function validateRawMaterialRequisitionSubmit() {
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown1("ddlVendor", "Please select RM Vendor.");

    var grid = document.getElementById("gvVendorRawMat");
    var hasDataRow = false;
    var hasValidRow = false;
    var hasMissingDateError = false;
    var hasMissingQtyError = false;

    if (grid) {
        var rows = grid.querySelectorAll("tr");
        hasDataRow = rows.length > 1;

        for (var i = 1; i < rows.length; i++) {
            var row = rows[i];
            var qtyControl = row.querySelector("input[id$='txtQuantity']");
            var dateControl = row.querySelector("input[id$='txtReqDate']");

            if (!qtyControl || !dateControl) {
                continue;
            }

            formatRateTwoDecimal(qtyControl);
            var qtyValue = (qtyControl.value || "").trim();
            var dateValue = (dateControl.value || "").trim();
            var qtyNumber = parseFloat(qtyValue);
            var hasQty = qtyValue !== "" && !isNaN(qtyNumber) && qtyNumber > 0;
            var hasDate = dateValue !== "";

            if (!hasQty && !hasDate) {
                SetErrorColor(dateControl.id, true);
                SetErrorColor(qtyControl.id, true);
                continue;
            }

            if (hasQty && hasDate) {
                hasValidRow = true;
                SetErrorColor(dateControl.id, true);
                SetErrorColor(qtyControl.id, true);
            } else if (hasQty && !hasDate) {
                if (firstErrorControl === "") {
                    firstErrorControl = dateControl.id;
                }
                if (!hasMissingDateError) {
                    errMsg += GetErrorRow(dateControl.id, "Please enter Requisition Date.");
                    hasMissingDateError = true;
                }
                SetErrorColor(dateControl.id, false);
                SetErrorColor(qtyControl.id, true);
            } else if (hasDate && !hasQty) {
                if (firstErrorControl === "") {
                    firstErrorControl = qtyControl.id;
                }
                if (!hasMissingQtyError) {
                    errMsg += GetErrorRow(qtyControl.id, "Please enter Quantity.");
                    hasMissingQtyError = true;
                }
                SetErrorColor(qtyControl.id, false);
                SetErrorColor(dateControl.id, true);
            }
        }
    }

    if (!hasDataRow) {
        if (firstErrorControl === "") {
            firstErrorControl = "gvVendorRawMat";
        }
        errMsg += GetErrorRow("gvVendorRawMat", "Please search and load Raw Material details.");
    } else if (!hasValidRow) {
        if (firstErrorControl === "") {
            firstErrorControl = "gvVendorRawMat";
            errMsg += GetErrorRow("gvVendorRawMat", "Please enter Quantity and Requisition Date for at least one Raw Material.");
        }
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }

    document.getElementById("lblErrorMessage").innerHTML = "";
    if (confirm("Are you sure to submit?")) {
        return true;
    }

    return false;
}

function validateRawMaterialRequisitionApprove() {
    firstErrorControl = "";
    errMsg = "";

    var grid = document.getElementById("gvRequisition");
    if (!grid) {
        grid = document.querySelector("[id$='gvRequisition']");
    }

    var hasSelected = false;
    if (grid) {
        var checkboxes = grid.querySelectorAll("input[type='checkbox'][id$='chkSelect']");
        for (var i = 0; i < checkboxes.length; i++) {
            if (!checkboxes[i].disabled && checkboxes[i].checked) {
                hasSelected = true;
                break;
            }
        }
    }

    if (!hasSelected) {
        firstErrorControl = "gvRequisition";
        errMsg += GetErrorRow("gvRequisition", "Please select at least one pending requisition to approve.");
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }

    document.getElementById("lblErrorMessage").innerHTML = "";
    if (confirm("Are you sure you want to approve the selected requisition(s)?")) {
        return true;
    }

    return false;
}
