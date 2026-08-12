function ValidateSubmit(mode) {
    firstErrorControl = "";
    errMsg = "";
    ValidateRequired("txtTokenVendorName", "Please enter token vendor name.");
    ValidateRequired("txtTokenVendorEmail", "Please enter token vendor email.");
    if (document.getElementById("txtTokenVendorEmail").value != "")
        ValidateEmail("txtTokenVendorEmail", "invalid Email")
    ValidateRequired("txtAddress", "Please enter address.");
    if (ValidateRequired("txtMobile", "Please enter mobile.")) {
        if (CheckMinLength("txtMobile", 10, "Phone no has to be 10 digits."))
        {
            if (CheckMaxlength("txtMobile", 10, "Phone no has to be 10 digits."))
            {
                ValidateNotAlpha("txtMobile", "Please enter valid mobile no.");
            }
        }
    }

    ValidateRequired("txtCity", "Please enter city.");
    ValidateRequired("txtState", "Please enter state.");
    
    if (ValidateRequired("txtZip", "Please enter zip.")) {
        ValidateNotAlpha("txtZip", "Please enter valid zip.");
    }
    if (mode == "Update") {
        ValidateRequired("ddlActive", "Please select active.");
    }

   
   
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

function CheckMinLength(controlName, minLengthValue, errorMessage) {

    var controlID = controlName;
    var controlObject = document.getElementById(controlID).value;

    if (controlObject.length < minLengthValue) {
        //if(firstErrorControl == '')        
        firstErrorControl = controlID;

        errMsg += GetErrorRow(controlID, errorMessage);

        SetErrorColor(controlID, false);

        return false;
    }
    else {

        SetErrorColor(controlID, true);
        return true;
    }

}