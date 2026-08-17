var firstErrorControl;
var errMsg;
function validateInputs() {
    //debugger;
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtBrandName", "Please enter Brand Name.");

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        //errMsg = "<table>" + errMsg + "</table>";
        errMsg = errMsg;
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to submit?")) {
            document.getElementById("btnSubmit").disabled = true;            
            __doPostBack(document.getElementById("btnSubmit").name, '');            
            return true;
        }
        else {
            return false;
        }
    }
}
