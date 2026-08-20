var firstErrorControl;
var errMsg;

function allowOnlyIntegerKey(evt) {
    var charCode = evt.which ? evt.which : evt.keyCode;
    if (charCode === 8 || charCode === 9 || charCode === 13 || charCode === 37 || charCode === 39 || charCode === 46) {
        return true;
    }
    return charCode >= 48 && charCode <= 57;
}

function sanitizeIntegerInput(control) {
    control.value = (control.value || "").replace(/[^0-9]/g, "");
}

function allowOnlyTextKey(evt) {
    var charCode = evt.which ? evt.which : evt.keyCode;
    if (charCode === 8 || charCode === 9 || charCode === 13 || charCode === 32 || charCode === 37 || charCode === 39 || charCode === 46) {
        return true;
    }
    return (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122);
}

function sanitizeTextInput(control) {
    control.value = (control.value || "").replace(/[^a-zA-Z ]/g, "");
}

function updateConsumptionRatioTotal() {
    var grid = document.getElementById("gdShadedtls");
    var ratioInputs = [];
    if (grid) {
        ratioInputs = grid.querySelectorAll("input[id$='txtratio']");
    } else {
        ratioInputs = document.querySelectorAll("input[id$='txtratio']");
    }
    var total = 0;

    for (var i = 0; i < ratioInputs.length; i++) {
        var rawValue = ratioInputs[i].value || "";
        var numValue = parseInt(rawValue.toString().replace(/[^0-9]/g, ""), 10);

        if (!isNaN(numValue)) {
            total += numValue;
        }
    }

    var totalLabel = document.getElementById("lblRatioTotal");
    var statusLabel = document.getElementById("lblRatioStatus");
    if (!totalLabel || !statusLabel) return;

    totalLabel.innerHTML = total + "%";
    if (total <= 100) {
        statusLabel.innerHTML = "Within 100%";
        statusLabel.style.color = "#28a745";
    } else {
        statusLabel.innerHTML = "Exceeds 100%";
        statusLabel.style.color = "#dc3545";
    }
}

function validateInputs() {
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown1("ddlBrand", "Please Select Brand.");
    if (document.getElementById("ddlvendor")) {
        ValidateDropDown1("ddlvendor", "Please select Vendor.");
    }
    ValidateDropDown1("ddlRawMat", "Please Select Raw Material.");
    if (!document.getElementById("hdnProductCode") || (document.getElementById("hdnProductCode").value || "").trim() === "") {
        if (firstErrorControl == "") {
            firstErrorControl = "txtProductSearch";
        }
        errMsg += GetErrorRow("txtProductSearch", "Please Select Product.");
    }

    var ratioInputs = document.querySelectorAll("#gdShadedtls input[id$='txtratio']");
    var measurementInputs = document.querySelectorAll("#gdShadedtls input[id$='txtmeasurement']");
    var hasAtLeastOneRecord = false;
    var totalRatio = 0;

    for (var i = 0; i < ratioInputs.length; i++) {
        var ratioValue = (ratioInputs[i].value || "").trim();
        var measurementValue = "";

        if (measurementInputs.length > i) {
            measurementValue = (measurementInputs[i].value || "").trim();
        }

        if (ratioValue !== "" || measurementValue !== "") {
            hasAtLeastOneRecord = true;

            if (ratioValue === "") {
                if (firstErrorControl == "") {
                    firstErrorControl = ratioInputs[i].id;
                }
                errMsg += GetErrorRow(ratioInputs[i].id, "Please enter Consumption Ratio.");
            }

            if (measurementValue === "") {
                if (firstErrorControl == "") {
                    firstErrorControl = measurementInputs.length > i ? measurementInputs[i].id : "gdShadedtls";
                }
                errMsg += GetErrorRow(measurementInputs.length > i ? measurementInputs[i].id : "gdShadedtls", "Please enter Unit of Measurement.");
            }

            if (ratioValue !== "") {
                var numericRatio = parseInt(ratioValue, 10);
                if (!isNaN(numericRatio)) {
                    totalRatio += numericRatio;
                }
            }
        }
    }

    if (!hasAtLeastOneRecord) {
        if (firstErrorControl == "") {
            firstErrorControl = "gdShadedtls";
        }
        errMsg += GetErrorRow("gdShadedtls", "Please enter at least one record in the grid.");
    }

    if (hasAtLeastOneRecord && totalRatio > 100) {
        if (firstErrorControl == "") {
            firstErrorControl = "gdShadedtls";
        }
        errMsg += GetErrorRow("gdShadedtls", "Total Consumption Ratio should not be greater than 100%.");
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

function getVendorRawMatGridRatioTotal() {
    var grid = document.getElementById("gvVendorRawMat");
    var total = 0;

    if (!grid) {
        return total;
    }

    var ratioLabels = grid.querySelectorAll("tbody tr.tlrowlight span[id$='lblRatio']");

    for (var i = 0; i < ratioLabels.length; i++) {
        var rawValue = (ratioLabels[i].innerText || ratioLabels[i].textContent || "").trim();
        var numValue = parseFloat(rawValue.replace(/[^0-9.]/g, ""));

        if (!isNaN(numValue)) {
            total += numValue;
        }
    }

    return Math.round(total * 100) / 100;
}

function updateVendorRawMatRatioTotal() {
    var total = getVendorRawMatGridRatioTotal();

    var totalLabel = document.getElementById("lblRatioTotal");
    var statusLabel = document.getElementById("lblRatioStatus");
    if (!totalLabel || !statusLabel) {
        return;
    }

    totalLabel.innerHTML = total.toFixed(2) + "%";
    if (total > 100) {
        statusLabel.innerHTML = "Exceed 100%";
        statusLabel.style.color = "#dc3545";
    } else {
        statusLabel.innerHTML = "Within 100%";
        statusLabel.style.color = "#28a745";
    }
}

document.addEventListener("DOMContentLoaded", function () {
    updateConsumptionRatioTotal();
    updateVendorRawMatRatioTotal();
});
function validateAddRawMaterial() {
    firstErrorControl = "";
    errMsg = "";

    var lblErrorMessage = document.getElementById("lblErrorMessage");
    var ddlBrand = document.getElementById("ddlBrand");
    var ddlvendor = document.getElementById("ddlvendor");
    var hdnProductCode = document.getElementById("hdnProductCode");
    var txtrawmatid = document.getElementById("txtrawmatid");
    var txtRatio = document.getElementById("txtRatio");
    var ratio = NaN;
    var totalRatio = getVendorRawMatGridRatioTotal();

    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = "";
    }

    if (!ddlBrand || ddlBrand.value === "" || ddlBrand.selectedIndex <= 0) {
        if (firstErrorControl === "") {
            firstErrorControl = "ddlBrand";
        }
        errMsg += GetErrorRow("ddlBrand", "Please select Brand.");
    }

    if (!ddlvendor || ddlvendor.value === "" || ddlvendor.selectedIndex <= 0) {
        if (firstErrorControl === "") {
            firstErrorControl = "ddlvendor";
        }
        errMsg += GetErrorRow("ddlvendor", "Please select Vendor.");
    }

    if (!hdnProductCode || (hdnProductCode.value || "").trim() === "") {
        if (firstErrorControl === "") {
            firstErrorControl = "txtProductSearch";
        }
        errMsg += GetErrorRow("txtProductSearch", "Please enter Product.");
    }

    if (!txtrawmatid || (txtrawmatid.value || "").trim() === "") {
        if (firstErrorControl === "") {
            firstErrorControl = "txtSearchText";
        }
        errMsg += GetErrorRow("txtSearchText", "Please enter Raw Material.");
    }

    if (!txtRatio || (txtRatio.value || "").trim() === "") {
        if (firstErrorControl === "") {
            firstErrorControl = "txtRatio";
        }
        errMsg += GetErrorRow("txtRatio", "Please enter Consumption Ratio.");
    } else {
        ratio = parseFloat((txtRatio.value || "").trim());

        if (isNaN(ratio)) {
            if (firstErrorControl === "") {
                firstErrorControl = "txtRatio";
            }
            errMsg += GetErrorRow("txtRatio", "Please enter a valid Consumption Ratio.");
        } else if (ratio <= 0) {
            if (firstErrorControl === "") {
                firstErrorControl = "txtRatio";
            }
            errMsg += GetErrorRow("txtRatio", "Consumption Ratio must be greater than 0.");
        } else {
            totalRatio = Math.round((totalRatio + ratio) * 100) / 100;
            if (totalRatio > 100) {
                if (firstErrorControl === "") {
                    firstErrorControl = "txtRatio";
                }
                errMsg += GetErrorRow("txtRatio", "Total Consumption Ratio should not be greater than 100%.");
            }
        }
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        return rmFailValidation(errMsg);
    }

    return rmConfirmPostback("btnAdd", "add");
}

function validateFormulationSubmit() {
    firstErrorControl = "";
    errMsg = "";

    var lblErrorMessage = document.getElementById("lblErrorMessage");
    var ddlBrand = document.getElementById("ddlBrand");
    var ddlvendor = document.getElementById("ddlvendor");
    var hdnProductCode = document.getElementById("hdnProductCode");
    var totalRatio = getVendorRawMatGridRatioTotal();
    var grid = document.getElementById("gvVendorRawMat");
    var ratioLabels = grid ? grid.querySelectorAll("tbody tr.tlrowlight span[id$='lblRatio']") : [];

    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = "";
    }

    if (!ddlBrand || ddlBrand.value === "" || ddlBrand.selectedIndex <= 0) {
        if (firstErrorControl === "") {
            firstErrorControl = "ddlBrand";
        }
        errMsg += GetErrorRow("ddlBrand", "Please select Brand.");
    }

    if (!ddlvendor || ddlvendor.value === "" || ddlvendor.selectedIndex <= 0) {
        if (firstErrorControl === "") {
            firstErrorControl = "ddlvendor";
        }
        errMsg += GetErrorRow("ddlvendor", "Please select Vendor.");
    }

    if (!hdnProductCode || (hdnProductCode.value || "").trim() === "") {
        if (firstErrorControl === "") {
            firstErrorControl = "txtProductSearch";
        }
        errMsg += GetErrorRow("txtProductSearch", "Please enter Product.");
    }

    if (!ratioLabels || ratioLabels.length === 0) {
        if (firstErrorControl === "") {
            firstErrorControl = "gvVendorRawMat";
        }
        errMsg += GetErrorRow("gvVendorRawMat", "Please enter at least one record in the grid.");
    }

    if (ratioLabels && ratioLabels.length > 0 && totalRatio !== 100) {
        if (firstErrorControl === "") {
            firstErrorControl = "gvVendorRawMat";
        }
        errMsg += GetErrorRow("gvVendorRawMat", "Total Consumption Ratio should be equal 100%.");
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        return rmFailValidation(errMsg);
    }

    return rmConfirmPostback("btnSubmit", "submit");
}