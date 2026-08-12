

function ValidateSearch(txtSkuCode, lblValidationMessage, btnSubmit) {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired(txtSkuCode, "Please enter sku name/code.");


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById(lblValidationMessage).innerHTML = errMsg;
        return false;
    }
    else {
        document.getElementById(lblValidationMessage).innerHTML = "";
        return true;
    }
}
function ValidateUpdate(NewVendor, lblSKU, lblCurrentVendor, lblNewVendorName, lblValidationMessage, btnSubmit) {
    firstErrorControl = "";
    errMsg = "";
    debugger;
    var newvendor = document.getElementById(lblNewVendorName).value;
    var SKU = document.getElementById(lblSKU).value;
    //var Depot = document.getElementById(lblDepot).value;
    var CurrentVendor = document.getElementById(lblCurrentVendor).value;

    ValidateRequired(NewVendor, "Please select valid vendor source.");


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById(lblValidationMessage).innerHTML = errMsg;
        return false;
    }
    else {
        document.getElementById(lblValidationMessage).innerHTML = "";
        if (confirm("SKU ( " + SKU + " ) will be delinked from Current Vendor ( " + CurrentVendor + " ) and linked to New Vendor (" + newvendor + "). !! \r\n Are u sure to update?")) {
            return true;
        }
        else {
            return false;
        }
    }
}
function ValidateInsert(Vendor, SKU, SKUDESC, lblValidationMessage, btnSubmit) {
    firstErrorControl = "";
    errMsg = "";
    debugger;
    ValidateRequired(Vendor, "Please select valid vendor source.");
    //ValidateRequired(Depot, "Please select valid depot.");
    
    if (ValidateRequired(SKUDESC, "Please enter valid SKU.")) {
        ValidateRequired(SKU, "Please enter valid SKU.");
    }


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById(lblValidationMessage).innerHTML = errMsg;
        return false;
    }
    else {
        document.getElementById(lblValidationMessage).innerHTML = "";
        if (confirm("Are you sure to insert?")) {
            return true;
        }
        else {
            return false;
        }
    }
}



function isIntegerNumberKey(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105)) {
        if (charCode == 46) {
            //Check if the text already contains the . character
            if (txt.value.indexOf('.') === -1) {
                return false;
            } else {
                return false;
            }
        } else {
            if (charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
        }
    }
    else {
        return false;
    }
    return true;
}

function isDecimalNumber(txt, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var dot1 = txt.value.indexOf('.');
    var dot2 = txt.value.lastIndexOf('.');

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    else if (charCode == 46 && (dot1 == dot2) && dot1 != -1 && dot2 != -1)
        return false;

    return true;
}