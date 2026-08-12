function ValidateHoMail(name, email, active, btnsubmit) {
    firstErrorControl = "";
    errMsg = "";
    debugger
    //document.getElementById('btnCheckUnitCode').click()

    ValidateRequired(name, "Please Enter Name")
    ValidateRequired(email, "Please Enter Email Id")

    if (ValidateRequired(active, "Please Select a Active Status")) {
        var select = document.querySelector("#" + active + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = active;
        var select = document.querySelector("#" + active + "+ .select2-container .selection .select2-selection");
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