//Java Script File
//Created By Debayan Biswas On 14-01-2012
//Validation of Transit_Days_AddUpdate.aspx


function ValidateTrnstSearch() {
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown("ddlUnit", "Select Vendor")

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrMsg").innerHTML = errMsg;
        return false;
    }
    else
        return true;
}
function ValidateTrnstDy() {

    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown("ddlUnit", "Select Vendor")

    var Grid = document.getElementById('gvTransitDays');
    var rowcount = Grid.rows.length - 1;
    for (var rowno = 1; rowno < Grid.rows.length; rowno++) {
        var txtTransitDays = Grid.rows(rowno).cells(4).children(0).id;
        //ValidateRequired(txtTransitDays, "Enter Transit Days")
        ValidateTxtTransitDays(txtTransitDays,"Enter day should not be less than 1 and greater than 25")   
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrMsg").innerHTML = errMsg;
        return false;
    }
    else {
        if (confirm('Are you sure to submit?')) {
            document.getElementById('btnSubmit').disabled = true;
            __doPostBack(document.getElementById('btnSubmit').name, '');
        }
        else {
            return false;
        }

    }

}



// Required Transit Days Field validation
function ValidateTxtTransitDays(controlName, errorMessage) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var month = parseInt(controlObject.value);

    if (!(month > 0 && month < 26)) {

        errorCode = false;
    }
    else {
        errorCode = true;
    }

    if (!errorCode) {
        //if(firstErrorControl == '')        
        firstErrorControl = controlID;

        errMsg += GetErrorRow(controlID, errorMessage);

        SetErrorColor(controlID, false);

        return false;
    }
    else
        SetErrorColor(controlID, true);

    return true;
}