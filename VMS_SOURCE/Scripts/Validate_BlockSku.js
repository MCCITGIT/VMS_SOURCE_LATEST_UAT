var firstErrorControl;
var errMsg;
function validateSubmit() {
    //debugger;
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("ctl00_ContentPlaceHolder1_txtSkuCode", "Please enter SKU Code.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("ctl00_ContentPlaceHolder1_lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {

        document.getElementById("ctl00_ContentPlaceHolder1_lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to submit?")) {
            document.getElementById("ctl00_ContentPlaceHolder1_btnSubmit").disabled = true;
            __doPostBack(document.getElementById("ctl00_ContentPlaceHolder1_btnSubmit").name, '');
            return true;
        }
        else {
            return false;
        }
    }
}
function validateSearch() {
    //debugger;
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("ctl00_ContentPlaceHolder1_txtSearchCode", "Please enter SKU Code.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("ctl00_ContentPlaceHolder1_lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    //else {

    //    document.getElementById("lblErrorMessage").innerHTML = '';
    //    if (confirm("Are you sure to submit?")) {
    //        document.getElementById("btnSubmit").disabled = true;
    //        __doPostBack(document.getElementById("btnSubmit").name, '');
    //        return true;
    //    }
    //    else {
    //        return false;
    //    }
    //}
}
