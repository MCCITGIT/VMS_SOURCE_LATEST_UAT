var firstErrorControl;
var errMsg;

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

function validateMobileNumber(controlName, errorMessage) {
    var control = document.getElementById(controlName);
    if (!control) {
        return false;
    }

    var mobileValue = (control.value || "").trim();
    if (!/^\d{10}$/.test(mobileValue)) {
        if (firstErrorControl == "") {
            firstErrorControl = controlName;
        }
        errMsg += GetErrorRow(controlName, errorMessage);
        SetErrorColor(controlName, false);
        return false;
    }

    SetErrorColor(controlName, true);
    return true;
}

function validateRawMaterialVendorInputs() {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtUnitName", "Please enter Vendor Name.");
    ValidateRequired("txtGstRegNo", "Please enter GST Registration Number.");
    ValidateRequired("txtLine1", "Please enter Address.");
    ValidateRequired("txtCity", "Please enter City.");
    ValidateRequired("txtState", "Please enter State.");
    ValidateRequired("txtPin", "Please enter Pincode.");
    ValidateRequired("txtContactPerson", "Please enter Contact Person.");

    if (ValidateRequired("txtMobileNo", "Please enter Mobile No.")) {
        validateMobileNumber("txtMobileNo", "Mobile No. must be exactly 10 digits.");
    }

    ValidateRequired("txtEmail", "Please enter E-mail.");
    if ((document.getElementById("txtEmail").value || "").trim() !== "") {
        ValidateEmail("txtEmail", "Please enter valid E-mail.");
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        return rmFailValidation(errMsg);
    }

    document.getElementById("lblErrorMessage").innerHTML = "";
    return rmConfirmPostback("btnSubmit", "submit");
}
