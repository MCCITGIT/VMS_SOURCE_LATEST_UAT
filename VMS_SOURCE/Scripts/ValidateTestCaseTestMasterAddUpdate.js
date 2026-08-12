function ValidateAddOption() {
    firstErrorControl = "";
    errMsg = "";
    ValidateRequired("txtResultTypeOption", "Please enter result type option.");
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {
        return true;
    }
};

function ValidateSubmit() {
    firstErrorControl = "";
    errMsg = "";
    ValidateRequired("txtTestName", "Please enter test name.");
    ValidateRequired("ddlFrequency", "Please select frequency.");
    //ValidateRequired("ddlUOM", "Please select UOM.");
    ValidateRequired("ddlResultType", "Please select result type.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {
        if (confirm('Are you sure to submit?')) {

            return true;
        } else {
            return false;
        }

    }
}