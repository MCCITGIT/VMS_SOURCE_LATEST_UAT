var firstErrorControl;
var errMsg;
function validateInputs() {
    //debugger;
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtBrandName", "Please enter Brand Name.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        return rmFailValidation(errMsg);
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = '';
        return rmConfirmPostback("btnSubmit", "submit");
    }
}
