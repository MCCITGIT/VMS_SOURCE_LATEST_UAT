function clearRawMatValidation() {
    var control = document.getElementById("txtSearchText");
    var label = document.getElementById("valSearchText");

    if (control) {
        control.classList.remove("field-invalid");
    }

    if (label) {
        label.innerHTML = "";
    }
}

function setRawMatFieldError(message) {
    var control = document.getElementById("txtSearchText");
    var label = document.getElementById("valSearchText");

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
    var searchText = document.getElementById("txtSearchText");
    var rawMatId = document.getElementById("txtrawmatid");

    clearRawMatValidation();

    if (!searchText || searchText.value.trim() === "") {
        setRawMatFieldError("Please enter Raw Material name.");
        return false;
    }

    if (!rawMatId || rawMatId.value.trim() === "") {
        setRawMatFieldError("Please select Raw Material from the list.");
        return false;
    }

    var lblErrorMessage = document.getElementById("lblErrorMessage");
    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = "";
    }

    return rmConfirmPostback("btnSubmit", "submit");
}
