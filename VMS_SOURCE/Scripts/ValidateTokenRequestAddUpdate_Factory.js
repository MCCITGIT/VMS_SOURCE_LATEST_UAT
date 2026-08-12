function validateAdd() {
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown("ddlFactory", "Select Factory");
    ValidateDropDown("ddlVendor", "Select Vendor");
    ValidateDropDown("ddlRequisitionMonth", "Select Requisition Month");
    ValidateDropDown("ddlRequisitionYear", "Select Requisition Year");

    ValidateDropDown("ddlTokenType", "Select Token type");
    ValidateDropDown("ddlMonth", "Select Month");
    ValidateDropDown("ddlYear", "Select Year");
    ValidateDropDown("ddlProduct", "Select Product");
    ValidateDropDown("ddlPackSize", "Select Pack Size");
    ValidateDropDown("ddlValue", "Select Value");

    if (ValidateRequired("txtQuantity", "Please Enter Quantity")) {
        validateNumber("txtQuantity", "Please Enter Quantity");
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblValidationMessage").innerHTML = errMsg;

        return false;
    }
    else {
        document.getElementById("lblValidationMessage").innerHTML = "";
        if (confirm('Are you sure to add ?')) {
            //            document.getElementById('ctl00_ContentPlaceHolder1_btnAdd').disabled = true;
            __doPostBack(document.getElementById('btnAdd').name, '');
        }
        else {
            return false;
        }

    }
}



function validateSubmit() {
    firstErrorControl = "";
    errMsg = "";


    ValidateGrid("gvTokenDetails", "Add Atleast One Record.")

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblValidationMessage").innerHTML = errMsg;

        return false;
    }
    else {
        document.getElementById("lblValidationMessage").innerHTML = "";
        if (confirm('Are you sure to submit ?')) {
            document.getElementById('btnSubmit').disabled = true;
            __doPostBack(document.getElementById('btnSubmit').name, '');
        }
        else {
            return false;
        }

    }
}



function ValidateGrid(controlName, errorMessage) {

    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    var cnt = controlObject.rows.length - 1;
    if (cnt == 0) {
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




function ValidateMobile(controlName, errorMessage) {
    var errorCode = true;
    var controlID = controlName;
    errorCode = IsMobile(controlID);
    if (!errorCode) {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
    }
    else
        SetErrorColor(controlID, true);
}


//checks whether it is email address or not
function IsMobile(control) {
    var controlObject = document.getElementById(control);
    var isMobile = MobileNoRegEx.test(controlObject.value);
    if (!isMobile)
        return false;
    else
        return true;
}
var MobileNoRegEx = /^([1-9][0-9]{9}){1}([,][1-9][0-9]{9})*$/;


function ltrim(valuetotrim) {
    var textaftertrim = "";

    for (var j = 0; j <= valuetotrim.length - 1; j++) {
        if (valuetotrim.charAt(j) != " ") {
            textaftertrim += valuetotrim.charAt(j);
        }
    }

    return textaftertrim;
}

//function validateNumber(valueToConvert) {
//    var result = true;

//    document.getElementById(valueToConvert).style.backgroundColor = "white";

//    var valueToValidate = ltrim(document.getElementById(valueToConvert).value);
//    if (valueToValidate != "") {
//        var val = new Number(valueToValidate);
//        if (val.toString() != "NaN") {
//            if (val > 0) {
//                document.getElementById(valueToConvert).value = valueToValidate;
//                result = true;
//            }
//            else {
//                result = false;
//                alert("Value can not be less than or equal to 0.");
//                document.getElementById(valueToConvert).value = "";
//            }
//        }
//        else {
//            result = false;
//            alert("Value entered is not a number. Please enter a numeric value.");
//            document.getElementById(valueToConvert).value = "";
//        }
//    }
//    else {
//        document.getElementById(valueToConvert).value = "";
//        result = true;
//    }

//    return result;
//}


function validateNumber(valueToConvert) {
    var result = true;

    document.getElementById(valueToConvert).style.backgroundColor = "white";

    var hdnCartonCapacity = ltrim(document.getElementById("hdnCartonCapacity").value);
    var CartonCapacity = new Number(hdnCartonCapacity);
    //    var ValDiff = new Number(0);

    var valueToValidate = ltrim(document.getElementById(valueToConvert).value);
    if (valueToValidate != "") {
        var val = new Number(valueToValidate);
        if (val.toString() != "NaN") {
            if (val > 0 && val <= 100000) {

                //                ValDiff = (val % CartonCapacity)

                if ((val % CartonCapacity) == 0) {
                    document.getElementById(valueToConvert).value = val.toFixed(0);
                    result = true;
                }
                else {
                    result = false;
                    alert("Value should be multiple of carton capacity.");
                    document.getElementById(valueToConvert).value = "";
                }


            }
            else {
                result = false;
                alert("Value can not be less than or equal to 0 or greater than 100000.");
                document.getElementById(valueToConvert).value = "";
            }
        }
        else {
            result = false;
            alert("Value entered is not a number. Please enter a numeric value.");
            document.getElementById(valueToConvert).value = "";
        }
    }
    else {
        document.getElementById(valueToConvert).value = "";
        result = true;
    }

    return result;
}