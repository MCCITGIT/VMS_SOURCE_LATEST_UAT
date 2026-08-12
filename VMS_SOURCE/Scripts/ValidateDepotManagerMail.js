function ValidateDepotMail(regn,depot,email,btnsubmit) {
    firstErrorControl = "";
    errMsg = "";
  debugger
    //document.getElementById('btnCheckUnitCode').click()

  ValidateRequired(email, "Please Enter Email Id")

    if (ValidateRequired(depot, "Please Select a Depot")) {
        var select = document.querySelector("#" + depot + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = depot;
        var select = document.querySelector("#" + depot + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {
        if (confirm('Are you sure to submit?')) {
            //document.getElementById('btnSubmit').style.display = "none";

            return true;
        } else {
            return false;
        }

    }
}