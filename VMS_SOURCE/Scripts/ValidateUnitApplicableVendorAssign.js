function ValidateTokenVendorAssign(ddl,btn) {
    firstErrorControl = "";
    errMsg = "";
    ValidateRequired(ddl, "Please select a token vendor.")

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