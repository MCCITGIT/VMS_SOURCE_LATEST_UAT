function clearBrandValidation() {
    var control = document.getElementById("txtBrandName");
    var label = document.getElementById("valBrandName");

    if (control) {
        control.classList.remove("field-invalid");
    }

    if (label) {
        label.innerHTML = "";
    }
}

function setBrandFieldError(message) {
    var control = document.getElementById("txtBrandName");
    var label = document.getElementById("valBrandName");

    if (control) {
        control.classList.add("field-invalid");
    }

    if (label) {
        label.innerHTML = message;
    }

    if (control && control.scrollIntoView) {
        control.scrollIntoView({ behavior: "smooth", block: "center" });
    }
}

function validateInputs() {
    var brandName = document.getElementById("txtBrandName");

    clearBrandValidation();

    if (!brandName || brandName.value.trim() === "") {
        setBrandFieldError("Please enter Brand Name.");
        return false;
    }

    var lblErrorMessage = document.getElementById("lblErrorMessage");
    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = "";
    }

    return rmConfirmPostback("btnSubmit", "submit");
}
