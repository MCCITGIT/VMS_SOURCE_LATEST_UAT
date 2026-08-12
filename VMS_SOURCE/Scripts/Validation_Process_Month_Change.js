//'**************************************************
//'Copyright	: VMS, MCC, KOLKATA
//'Source	    : Scripts/Validation_Process_Month_Change.js
//'Created Date	: 20th-April-2011
//'Created By	: Debayan Biswas
//'Version	    : R01.00.00
//'Description	: Process Month Change Validation File

//'Modified By       Modified On       Version         Reason

//'*************************************************************


function ValidateIntegerFields(fieldname) {
    var asciicode = event.keyCode;

    document.getElementById("lblErrMsg").innerHTML = "";

    //alert(asciicode.toString());
    switch (asciicode) {
        case 48:
            return true;
            break;
        case 49:
            return true;
            break;
        case 50:
            return true;
            break;
        case 51:
            return true;
            break;
        case 52:
            return true;
            break;
        case 53:
            return true;
            break;
        case 54:
            return true;
            break;
        case 55:
            return true;
            break;
        case 56:
            return true;
            break;
        case 57:
            return true;
            break;

        default:
            document.getElementById("lblErrMsg").style.color = "red";
            document.getElementById("lblErrMsg").innerHTML = "&bull; " + fieldname + " can only be Number.";

            return false;
    }

}

//******************************************************************************************************************

// Required Month Field validation
function ValidateMonth(controlName) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var month = parseFloat(controlObject.value);

    if (!(month > 0 && month < 13)) {

        alert("Process Month should not be Less than 1 and Greater than 12.");
        document.getElementById(controlID).focus();
        document.getElementById(controlID).style.backgroundColor = "yellow";
        return false;
        
    }
    else {
        document.getElementById(controlID).style.backgroundColor = "white";
        return true;
    }

}

//************************************************************************************************************

function ValidateSubmit() {
    debugger;
    firstErrorControl = "";
    errMsg = "";

    if (ValidateRequired("txtProcessYear", "&bull; Enter Process Year.")) {
        ValidateSystemYear1("txtProcessYear", "&bull; Enter Process Year is not matching with current Year.");
    }

    if (ValidateRequired("txtProcessMonth", "&bull; Enter Process Month.")) {
        ValidateSystemMonth1("txtProcessMonth", "&bull; Process Month should not be Less than 1 and Greater than 12.", "&bull; Enter Process Month is not matching with current Month.");
    }


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrMsg").innerHTML = errMsg;
        return false;
    }
    else {
        if (confirm('Are you sure to submit?')) {
//            document.getElementById('btnSubmit').disabled = true;
            document.getElementById('btnSubmit').click();
//            __doPostBack(document.getElementById('btnSubmit').name, '');
            return true;
        }
        else {
            return false;
        }

    }
}

//*************************************************************************************************************

function ValidateSystemMonth(controlName) {
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var month = parseFloat(controlObject.value);

    var checkCurrentdateObj = new Date();
    var mon = checkCurrentdateObj.getMonth()+1;
    var MM = parseFloat(mon);

    if (ValidateMonth(controlID)) { 
        if (month != MM) {
            alert("Enter Process Month is not matching with current month.");
            document.getElementById(controlID).focus();
            document.getElementById(controlID).style.backgroundColor = "yellow";
            return false;
        }
        else {
            document.getElementById(controlID).style.backgroundColor = "white";
            return true;
        }
    }


}

//****************************************************************************************************************

function ValidateSystemYear(controlName) {
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var Year = parseFloat(controlObject.value);

    var checkCurrentdateObj = new Date();
    var yr = checkCurrentdateObj.getYear();
    var YY = parseFloat(yr);

    
        if (Year != YY) {
            alert("Enter Process Year is not matching with current Year.");
            document.getElementById(controlID).focus();
            document.getElementById(controlID).style.backgroundColor = "yellow";
            return false;
        }
        else {
            document.getElementById(controlID).style.backgroundColor = "white";
            return true;
        }


    }

//**********************************************************************************************************

    function ValidateSystemMonth1(controlName, errorMessage1,errorMessage2) {
        var controlID = controlName;
        var controlObject = document.getElementById(controlID);

        var month = parseFloat(controlObject.value);

        var checkCurrentdateObj = new Date();
        var mon = checkCurrentdateObj.getMonth() + 1;
        var MM = parseFloat(mon);

        if (ValidateMonth1(controlID, errorMessage1)) {
            if (month != MM) {
                errorCode = false;
            }
            else {
                errorCode = true;
            }

            if (!errorCode) {
                //if(firstErrorControl == '')        
                firstErrorControl = controlID;

                errMsg += GetErrorRow(controlID, errorMessage2);

                SetErrorColor(controlID, false);

                return false;
            }
            else
                SetErrorColor(controlID, true);

            return true;

        }
        else {
            //if(firstErrorControl == '')
            firstErrorControl = controlID;
            errMsg += GetErrorRow(controlID, errorMessage1);
            SetErrorColor(controlID, false);
            return false;
        }   
    }

    //*******************************************************************************************************

    function ValidateSystemYear1(controlName, errorMessage) {
        var controlID = controlName;
        var controlObject = document.getElementById(controlID);

        var Year = parseFloat(controlObject.value);

        var checkCurrentdateObj = new Date();
        var yr = checkCurrentdateObj.getFullYear();
        var YY = parseFloat(yr);


        if (Year != YY) {
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


    //***********************************************************************************************************

    function ValidateMonth1(controlName, errorMessage) {

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

//            errMsg += GetErrorRow(controlID, errorMessage);

            SetErrorColor(controlID, false);

            return false;
        }
        else
            SetErrorColor(controlID, true);

        return true;
    }


    