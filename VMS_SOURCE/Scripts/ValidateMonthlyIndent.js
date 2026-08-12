function validate_yearmonth() {
    //debugger;
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown1("ddlProcessYr", "Please Select a Year.");
    ValidateDropDown1("ddlProcessMnth", "Please Select a Month.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrMsg").innerHTML = errMsg;
        return false;
    }
    else {
        document.getElementById("lblErrMsg").innerHTML = '';
        return true;
    }
}