//Java Script File
//Created By Debayan Das On 29-06-2018
//Validation of Depot_Despatches_Unitwise.aspx


function ValidateIndetDetailsYear(TopFin, LastFin) {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtFinYear", "Enter Process Year.");
    ValidateRequired("txtMonth", "Enter Process Month.");
    ValidateMonth("txtMonth", "Enter Month should not be Less than 1 and Greater than 12.");
    ValidateYear("txtFinYear", TopFin, LastFin, "Enter Year Not Found In FinYear");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrMsg").innerHTML = errMsg;
        return false;
    }
    else
        document.getElementById("lblErrMsg").innerHTML = "";
        return true;
}



// Required Month Field validation
function ValidateMonth(controlName, errorMessage) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var month = parseFloat(controlObject.value);

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

function ValidateYear(controlName, TopFin, LastFin, errorMessage) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var year = parseFloat(controlObject.value);
    var top = parseFloat(TopFin)
    var last = parseFloat(LastFin)

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