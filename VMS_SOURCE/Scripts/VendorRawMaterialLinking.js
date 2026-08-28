function clearFieldValidation(controlId, labelId) {
    var control = document.getElementById(controlId);
    var label = document.getElementById(labelId);

    if (control) {
        control.classList.remove("field-invalid");
    }

    if (label) {
        label.innerHTML = "";
    }
}

function setFieldError(controlId, labelId, message) {
    var control = document.getElementById(controlId);
    var label = document.getElementById(labelId);

    if (control) {
        control.classList.add("field-invalid");
    }

    if (label) {
        label.innerHTML = message;
    }
}

function clearVendorValidation() {
    clearFieldValidation("txtVendorSearch", "valVendorSearch");
}

function clearRawMatValidation() {
    clearFieldValidation("txtSearchText", "valSearchText");
}

function clearGridValidation() {
    var label = document.getElementById("valGrid");
    if (label) {
        label.innerHTML = "";
    }
}

function setGridFieldError(message) {
    var label = document.getElementById("valGrid");

    if (label) {
        label.innerHTML = message;
    }

    var grid = document.getElementById("gvVendorRawMat");
    if (grid && grid.scrollIntoView) {
        grid.scrollIntoView({ behavior: "smooth", block: "center" });
    }
}

function clearLinkValidation() {
    clearVendorValidation();
    clearRawMatValidation();
    clearGridValidation();
}

function scrollToFirstInvalidField() {
    var fields = [
        { controlId: "txtVendorSearch" },
        { controlId: "txtSearchText" }
    ];

    for (var i = 0; i < fields.length; i++) {
        var control = document.getElementById(fields[i].controlId);
        if (control && control.classList.contains("field-invalid") && control.scrollIntoView) {
            control.scrollIntoView({ behavior: "smooth", block: "center" });
            return;
        }
    }

    var grid = document.getElementById("gvVendorRawMat");
    if (grid && grid.scrollIntoView) {
        grid.scrollIntoView({ behavior: "smooth", block: "center" });
    }
}

function getFieldValue(controlId) {
    var control = document.getElementById(controlId);
    return control ? (control.value || "").trim() : "";
}

function isDuplicateRawMaterial(vendorCode, rawMatCode) {
    var grid = document.getElementById("gvVendorRawMat");
    if (!grid || !vendorCode || !rawMatCode) {
        return false;
    }

    var rows = grid.querySelectorAll("tbody tr.tlrowlight");
    for (var i = 0; i < rows.length; i++) {
        var vendorCodeControl = rows[i].querySelector("input[id$='hdnVendorCode']");
        var existingVendorCode = vendorCodeControl ? (vendorCodeControl.value || "").trim().toUpperCase() : "";
        var cells = rows[i].getElementsByTagName("td");
        var existingRawMatCode = "";

        if (cells.length > 1) {
            existingRawMatCode = (cells[1].innerText || cells[1].textContent || "").trim().toUpperCase();
        }

        if (existingVendorCode === vendorCode.toUpperCase() && existingRawMatCode === rawMatCode.toUpperCase()) {
            return true;
        }
    }

    return false;
}

function validateAddRawmaterial() {
    var hasError = false;

    clearLinkValidation();

    if (getFieldValue("txtVendorSearch") === "") {
        setFieldError("txtVendorSearch", "valVendorSearch", "Please enter Vendor name.");
        hasError = true;
    } else if (getFieldValue("hdnVendorCode") === "") {
        setFieldError("txtVendorSearch", "valVendorSearch", "Please select Vendor from the list.");
        hasError = true;
    }

    if (getFieldValue("txtSearchText") === "") {
        setFieldError("txtSearchText", "valSearchText", "Please enter Raw Material name.");
        hasError = true;
    } else if (getFieldValue("txtrawmatid") === "") {
        setFieldError("txtSearchText", "valSearchText", "Please select Raw Material from the list.");
        hasError = true;
    }

    if (!hasError &&
        isDuplicateRawMaterial(getFieldValue("hdnVendorCode"), getFieldValue("txtrawmatid"))) {
        setFieldError("txtSearchText", "valSearchText", "Selected Raw Material already added.");
        hasError = true;
    }

    if (hasError) {
        scrollToFirstInvalidField();
        return false;
    }

    var lblErrorMessage = document.getElementById("lblErrorMessage");
    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = "";
    }

    return rmConfirmPostback("btnAdd", "add");
}

function validateVendorRawMaterialLinkAdd() {
    var hasError = false;
    var grid = document.getElementById("gvVendorRawMat");
    var dataRows = grid ? grid.querySelectorAll("tbody tr.tlrowlight") : [];

    clearLinkValidation();

    if (!dataRows || dataRows.length === 0) {
        setGridFieldError("Please add at least one new Raw Material.");
        hasError = true;
    }

    if (hasError) {
        scrollToFirstInvalidField();
        return false;
    }

    var lblErrorMessage = document.getElementById("lblErrorMessage");
    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = "";
    }

    return rmConfirmPostback("btnSubmit", "submit");
}
