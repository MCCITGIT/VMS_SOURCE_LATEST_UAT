function ValidateSubmit(ddlprdname, txtparam, ddlfreqncy, ddlNumeric, ddlDropdown, txtDropDownType,txtMinValue,txtMaxValue, btnUpdate, lblErrMsg) {
    firstErrorControl = "";
    errMsg = "";
    var theGridView = document.getElementById('gvParamsList');

    ValidateDropDown1(ddlprdname, "Please Select Product Name.");
    ValidateRequired(txtparam, "Please Enter Parameters.");
    ValidateDropDown1(ddlfreqncy, "Please Select Frequency.");
    ValidateDropDown1(ddlNumeric, "Please Select Numeric Y/N.");
    ValidateDropDown1(ddlDropdown, "Please Select Drop Down Y/N.");

    if (document.getElementById(ddlNumeric).value == "Y")
    {
        ValidateRequired(txtMinValue, "Please Enter Min Value.");
        ValidateRequired(txtMaxValue, "Please Enter Max Value.");
    }
    if (document.getElementById(ddlNumeric).value == "Y" && document.getElementById(ddlDropdown).value == "Y") {
        firstErrorControl = ddlDropdown;
        errMsg += GetErrorRow(ddlDropdown, "Drop Down can not be numeric. Please select NumericYN as N.");
        SetErrorColor(ddlDropdown, false);
    }
    if (document.getElementById(ddlNumeric).value != "Y" && document.getElementById(ddlDropdown).value == "Y") {
        ValidateRequired(txtDropDownType, "Please Enter Drop Down Parameters with coma saparator.");
    }
    
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById(lblErrMsg).innerHTML = errMsg;

        return false;
    }

    else {
        document.getElementById(lblErrMsg).innerHTML = '';
        if (confirm('Are you sure to Submit?')) {
            document.getElementById(btnUpdate).disabled = true;
            __doPostBack(document.getElementById(btnUpdate).name, '');
            //            document.getElementById('btnAdd').click()
            return true;
        }
        else {
            return false;
        }
    }
   

}
function isDecimalNumber(txt, evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;
    var dot1 = txt.value.indexOf('.');
    var dot2 = txt.value.lastIndexOf('.');
    var decimalval = parseFloat(txt).toFixed(2);

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    else if (charCode == 46 && (dot1 == dot2) && dot1 != -1 && dot2 != -1)
        return false;
    return true;
}
function MinMaxValue(txt, evt) {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txt", "Please enter Some Value.")

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById(lblErrMsg).innerHTML = errMsg;

        return false;
    }

    
}
var validate = function (e) {
    var t = e.value;
    e.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), 3)) : t;
}

