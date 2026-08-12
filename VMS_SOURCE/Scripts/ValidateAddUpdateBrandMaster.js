

var firstErrorControl;
var errMsg;
function validateBrandListAdd() {

    debugger

        firstErrorControl = "";
        errMsg = "";

        ValidateRequired("txtBrandName", "Please enter Brand Name.");

        if (firstErrorControl != "") {
            SetControlFocus(firstErrorControl);
            errMsg = "<table>" + errMsg + "</table>";
            document.getElementById("lblErrorMessage").innerHTML = errMsg;
            return false;
        }
        else {

            document.getElementById("lblErrorMessage").innerHTML = '';
            if (confirm("Are you sure to submit?")) {
                document.getElementById("btnSubmit").disabled = true;
                //document.getElementById("ctl00_ContentPlaceHolder1_btnReset").disabled = true;
                //document.getElementById(btnSave).click();
                __doPostBack(document.getElementById("btnSubmit").name, '');
                //document.getElementById(btnSave).disabled = true;
                return true;
            }
            else {
                return false;
            }
        }
    }
