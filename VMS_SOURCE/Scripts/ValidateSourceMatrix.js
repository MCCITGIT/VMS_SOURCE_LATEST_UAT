function ValidateSearch() {
    debugger;
    firstErrorControl = "";
    errMsg = "";
    if (ValidateRequired("ctl00_ContentPlaceHolder1_ddlDepot", "Please Select Depot.")) {
        var select = document.querySelector("#ctl00_ContentPlaceHolder1_ddlDepot + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ctl00_ContentPlaceHolder1_ddlDepot";
        var select = document.querySelector("#ctl00_ContentPlaceHolder1_ddlDepot + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }
    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("ctl00_ContentPlaceHolder1_lblErrorMessage").innerHTML = errMsg;
        return false;
    } else {
        document.getElementById("ctl00_ContentPlaceHolder1_lblErrorMessage").innerHTML = '';
        //if (confirm("Are you sure to submit?")) {
        //    document.getElementById("btnSubmit").disabled = true;
        //    __doPostBack(document.getElementById("btnSubmit").name, '');
        //    return true;
        //} else {
        //    return false;
        //}
    }
}