function ValidateData() {

    firstErrorControl = "";
    errMsg = "";


    ValidateRequired("txtregion", missingRegion)

    ValidateRequired("txtdepot", missingDepot)

    ValidateNumbers("txtemail", invalidEmail)
    
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage1").innerHTML = errMsg;
        return false;
    }
    else {
        return confirm('Are you sure to submit?')
    }

}
