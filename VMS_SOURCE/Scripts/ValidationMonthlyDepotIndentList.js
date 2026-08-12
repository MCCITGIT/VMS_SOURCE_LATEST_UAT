//Java Script File
//Created By Debayan Biswas On 15-12-2011
//Validation of Monthly_Depot_Indent_List.aspx


function ValidateMnthlyDptIndntLst(TopFin, LastFin) {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtFinYear", "Enter Process Year.");
    ValidateRequired("txtMonth", "Enter Process Month.");
    ValidateMonth("txtMonth", "Enter Month should not be Less than 1 and Greater than 12.");
    ValidateYear("txtFinYear", TopFin, LastFin, "Enter Year Not Found In FinYear")

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrMsg").innerHTML = errMsg;
        return false;
    }
    else
        return true;
}



// Required Month Field validation
function ValidateMonth(controlName, errorMessage) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var month = parseInt(controlObject.value);

    if (!(month > 0 && month < 13)) {

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


// Required Year Field validation
function ValidateYear(controlName, TopFin, LastFin, errorMessage) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var year = parseInt(controlObject.value);
    var top = parseInt(TopFin)
    var last = parseInt(LastFin)

    if (!(year >= top && year <= last)) {

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