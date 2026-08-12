
function validateSubmit() {
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown("ddlVendor", "Select vendor")
    ValidateDropDown("ddlBrand", "Select brand")
    ValidateDropDown("ddlProduct", "Select product")
    ValidateRequired("txtShade", "Enter shade")
    ValidateRequired("txtBatchNo", "Enter batch no")


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {
        if (confirm('Are you sure to submit?')) {
            document.getElementById('btnSubmit').style.display = "none";

            return true;
        } else {
            return false;
        }

    }
}