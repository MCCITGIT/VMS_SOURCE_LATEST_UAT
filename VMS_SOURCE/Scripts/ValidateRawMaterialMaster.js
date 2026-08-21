var firstErrorControl;
var errMsg;
function validateInputs() {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtSearchText", "Please enter Raw Material name.");
    if (firstErrorControl == "") {
        var rawMatId = document.getElementById("txtrawmatid");
        if (!rawMatId || rawMatId.value == "") {
            firstErrorControl = "txtSearchText";
            errMsg += GetErrorRow("txtSearchText", "Please select Raw Material from the list.");
            SetErrorColor("txtSearchText", false);
        }
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
