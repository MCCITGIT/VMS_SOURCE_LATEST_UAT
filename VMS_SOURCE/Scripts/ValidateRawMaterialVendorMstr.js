var vendorValidationFields = [
    { controlId: "txtUnitName", labelId: "valUnitName" },
    { controlId: "txtGstRegNo", labelId: "valGstRegNo" },
    { controlId: "txtLine1", labelId: "valLine1" },
    { controlId: "txtCity", labelId: "valCity" },
    { controlId: "txtState", labelId: "valState" },
    { controlId: "txtPin", labelId: "valPin" },
    { controlId: "txtContactPerson", labelId: "valContactPerson" },
    { controlId: "txtMobileNo", labelId: "valMobileNo" },
    { controlId: "txtEmail", labelId: "valEmail" }
];

var emailPattern = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/;

function allowOnlyMobileNumberKey(evt) {
    var charCode = evt.which ? evt.which : evt.keyCode;

    if (charCode === 8 || charCode === 9 || charCode === 13 || charCode === 37 || charCode === 39 || charCode === 46) {
        return true;
    }

    if (charCode >= 48 && charCode <= 57) {
        return true;
    }

    return false;
}

function sanitizeMobileNumberInput(control) {
    var value = (control.value || "").replace(/\D/g, "");
    if (value.length > 10) {
        value = value.substring(0, 10);
    }
    control.value = value;
}

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

function setFieldError(controlId, labelId, message, scrollToField) {
    var control = document.getElementById(controlId);
    var label = document.getElementById(labelId);

    if (control) {
        control.classList.add("field-invalid");
    }

    if (label) {
        label.innerHTML = message;
    }

    if (scrollToField && control && control.scrollIntoView) {
        control.scrollIntoView({ behavior: "smooth", block: "center" });
    }
}

function scrollToFirstInvalidField() {
    for (var i = 0; i < vendorValidationFields.length; i++) {
        var control = document.getElementById(vendorValidationFields[i].controlId);
        if (control && control.classList.contains("field-invalid") && control.scrollIntoView) {
            control.scrollIntoView({ behavior: "smooth", block: "center" });
            break;
        }
    }
}

function clearVendorValidation() {
    for (var i = 0; i < vendorValidationFields.length; i++) {
        clearFieldValidation(vendorValidationFields[i].controlId, vendorValidationFields[i].labelId);
    }
}

function clearUnitNameValidation() { clearFieldValidation("txtUnitName", "valUnitName"); }
function clearGstRegNoValidation() { clearFieldValidation("txtGstRegNo", "valGstRegNo"); }
function clearLine1Validation() { clearFieldValidation("txtLine1", "valLine1"); }
function clearCityValidation() { clearFieldValidation("txtCity", "valCity"); }
function clearStateValidation() { clearFieldValidation("txtState", "valState"); }
function clearPinValidation() { clearFieldValidation("txtPin", "valPin"); }
function clearContactPersonValidation() { clearFieldValidation("txtContactPerson", "valContactPerson"); }
function clearMobileNoValidation() { clearFieldValidation("txtMobileNo", "valMobileNo"); }
function clearEmailValidation() { clearFieldValidation("txtEmail", "valEmail"); }

function getFieldValue(controlId) {
    var control = document.getElementById(controlId);
    return control ? (control.value || "").trim() : "";
}

function validateRawMaterialVendorInputs() {
    var hasError = false;

    clearVendorValidation();

    if (getFieldValue("txtUnitName") === "") {
        setFieldError("txtUnitName", "valUnitName", "Please enter Vendor Name.", false);
        hasError = true;
    }

    if (getFieldValue("txtGstRegNo") === "") {
        setFieldError("txtGstRegNo", "valGstRegNo", "Please enter GST Registration Number.", false);
        hasError = true;
    }

    if (getFieldValue("txtLine1") === "") {
        setFieldError("txtLine1", "valLine1", "Please enter Address.", false);
        hasError = true;
    }

    if (getFieldValue("txtCity") === "") {
        setFieldError("txtCity", "valCity", "Please enter City.", false);
        hasError = true;
    }

    if (getFieldValue("txtState") === "") {
        setFieldError("txtState", "valState", "Please enter State.", false);
        hasError = true;
    }

    if (getFieldValue("txtPin") === "") {
        setFieldError("txtPin", "valPin", "Please enter Pincode.", false);
        hasError = true;
    }

    if (getFieldValue("txtContactPerson") === "") {
        setFieldError("txtContactPerson", "valContactPerson", "Please enter Contact Person.", false);
        hasError = true;
    }

    var mobileValue = getFieldValue("txtMobileNo");
    if (mobileValue === "") {
        setFieldError("txtMobileNo", "valMobileNo", "Please enter Mobile No.", false);
        hasError = true;
    } else if (!/^\d{10}$/.test(mobileValue)) {
        setFieldError("txtMobileNo", "valMobileNo", "Mobile No. must be exactly 10 digits.", false);
        hasError = true;
    }

    var emailValue = getFieldValue("txtEmail");
    if (emailValue === "") {
        setFieldError("txtEmail", "valEmail", "Please enter E-mail.", false);
        hasError = true;
    } else if (!emailPattern.test(emailValue)) {
        setFieldError("txtEmail", "valEmail", "Please enter valid E-mail.", false);
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
