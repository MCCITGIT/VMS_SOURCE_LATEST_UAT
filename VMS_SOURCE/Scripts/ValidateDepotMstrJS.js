function ValidateSubmit() {
    firstErrorControl = "";
    errMsg = "";
    //ValidateRequired("ddlRegion", "Please select Region.");
    ValidateRequired("ddlDepot", "Please select Depot.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    //else {
    //    if (confirm('Are you sure to submit?')) {

    //        return true;
    //    } else {
    //        return false;
    //    }

    //}
}