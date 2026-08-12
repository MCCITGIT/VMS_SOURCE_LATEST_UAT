//Riddhi
//10/12/2011

function Validatefile(controlName, errorMessage) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    errorCode = fnCheckExt(controlObject)
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
function fnCheckExt(controlObject) {

    if (controlObject.value != "") {
        var Exntsn = controlObject.value;
        var fileName = Exntsn

        var Extension = fileName.substr(fileName.lastIndexOf(".") + 1, fileName.length);
        Extension = Extension.toUpperCase();
        if ( Extension != "TXT") {
            //alert("Choose a Valid File");
            //document.getElementById("sch_fld").focus()
            //SetErrorColor("sch_fld", false);
            return false;
        }
        else
            return true;
    }
}
function ValidateUpload() {
    document.getElementById("lblErrorMessage").innerHTML = "";
    firstErrorControl = "";
    errMsg = "";

    if (ValidateRequired("Upload_File", "Select a File to Upload")) {
        Validatefile("Upload_File", "Chooseb a Valid File")
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        document.getElementById("lblErrorMessage").innerHTML = "";

        return false;
    }
    else {
        if (document.getElementById("divErrorMessage").innerHTML == '') {
           

        }


    }
}

