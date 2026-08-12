function ValidateSubmit() {
    firstErrorControl = "";
    errMsg = "";
    var atLeastOneRowSelected = false; // New flag to track row selection

    if (ValidateRequired("ddlquartor", "Please Select Quartor.")) {
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlquartor";
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (ValidateRequired("ddlvendor", "Please Select Vendor.")) {
        var select = document.querySelector("#ddlvendor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlvendor";
        var select = document.querySelector("#ddlvendor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    var gridView = document.getElementById("gvLegalScoreDtls");

    if (gridView) {

        var rows = gridView.getElementsByTagName("tr");

        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];

            // **Skip Header & Footer Rows**
            var cells = row.getElementsByTagName("td");
            if (cells.length === 0) continue; // Ignore rows without <td> (header/footer)

            var txtObtainScore = row.querySelector('[id$="txtObtainScore"]');
            var ddlavailable = row.querySelector('[id$="ddlavailable"]');
            var txtIssueAuthority = row.querySelector('[id$="txtIssueAuthority"]');
            var fileupload = row.querySelector('[id$="FileUpload1"]');

            // Ensure values are correctly retrieved
            var txt_ObtainScore = txtObtainScore ? txtObtainScore.value.trim() : "";
            var ddl_available = ddlavailable ? ddlavailable.value.trim() : "";
            var txt_IssueAuthority = txtIssueAuthority ? txtIssueAuthority.value.trim() : "";
            var file_upload = fileupload ? fileupload.value.trim() : "";

            // **Reset row background before validation**
            row.style.backgroundColor = "";

            // **Check if all three fields have values in the same row**
            if (txt_ObtainScore !== "" && ddl_available !== "" && txt_IssueAuthority !== "" && file_upload != "") {
                atLeastOneRowSelected = true; // Valid row found
            } else {
                row.style.backgroundColor = "yellow"; // Highlight invalid row
            }
        }
    }

    // **Check if no rows have all three values**
    if (!atLeastOneRowSelected) {
        errMsg += "<tr><td>Please select at least one row with all required values.</td></tr>";
        firstErrorControl = "gvLegalScoreDtls";
    }

    //// **Check if no rows are selected**
    //if (!atLeastOneRowSelected) {
    //    errMsg += "<tr><td>Please select at least one row.</td></tr>";
    //    firstErrorControl = "gvLegalScoreDtls";
    //}

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    } else {
        document.getElementById("lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to submit?")) {
            document.getElementById("btnSubmit").disabled = true;
            __doPostBack(document.getElementById("btnSubmit").name, '');
            return true;
        } else {
            return false;
        }
    }
}

function ValidateSubmit_vr1() {

    firstErrorControl = "";
    errMsg = "";
    // debugger;

    if (ValidateRequired("ddlquartor", "Please Select Quartor.")) {
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlquartor";
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (ValidateRequired("ddlvendor", "Please Select Vendor.")) {
        var select = document.querySelector("#ddlvendor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlvendor";
        var select = document.querySelector("#ddlvendor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }
    if (ValidateRequired("ddllegal_staus", "Please Select Vendor Legal Status.")) {
        var select = document.querySelector("#ddllegal_staus + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddllegal_staus";
        var select = document.querySelector("#ddllegal_staus + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }
    if (ValidateRequired("ddlvendor_obligation", "Please Select Obligation.")) {
        var select = document.querySelector("#ddllegal_staus + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlvendor_obligation";
        var select = document.querySelector("#ddlvendor_obligation + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }
    if (ValidateRequired("ddlavailable_status", "Please Select Available status.")) {
        var select = document.querySelector("#ddlavailable_status + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlavailable_status";
        var select = document.querySelector("#ddlavailable_status + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    ValidateRequired("txtobtained_score", "Please enter Obtain Score .");
    ValidateRequired("txtIssueAuthority", "Please enter Issuing Authority .");

    var fileUpload = document.getElementById("FileUpload1");
    if (fileUpload && fileUpload.value === "") {
        firstErrorControl = "FileUpload1";

        // If using a custom wrapper or styled file input, change the background color here
        var fileUploadWrapper = fileUpload.parentElement; // This is the wrapping element (e.g., a div or span)

        if (fileUploadWrapper != null) {
            fileUploadWrapper.style.backgroundColor = "yellow"; // Apply background color to the wrapper
        }

        errMsg += "<tr><td colspan='2' style='color: red;'>Please select a file to upload.</td></tr>";
    }
    // Validate File Upload Control



    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    } else {
        document.getElementById("lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to submit?")) {
            document.getElementById("btnSubmit").disabled = true;
            __doPostBack(document.getElementById("btnSubmit").name, '');
            return true;
        } else {
            return false;
        }
    }
}

function ValidateSearch() {

    firstErrorControl = "";
    errMsg = "";
    debugger;

    if (ValidateRequired("ddlquartor", "Please Select Quartor.")) {
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlquartor";
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (ValidateRequired("ddlvendor", "Please Select Vendor.")) {
        var select = document.querySelector("#ddlvendor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlvendor";
        var select = document.querySelector("#ddlvendor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblError").innerHTML = errMsg;
        return false;
    } else {
        document.getElementById("lblError").innerHTML = '';
        return true;
        //if (confirm("Are you sure to submit?")) {
        //    document.getElementById("btnSubmit").disabled = true;
        //    __doPostBack(document.getElementById("btnSubmit").name, '');
        //    return true;
        //} else {
        //    return false;
        //}
    }
}

function Validate_VendorRate_Search() {

    firstErrorControl = "";
    errMsg = "";
    debugger;

    if (ValidateRequired("ddlquartor", "Please Select Quartor.")) {
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    } else {
        firstErrorControl = "ddlquartor";
        var select = document.querySelector("#ddlquartor + .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }
    

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblError").innerHTML = errMsg;
        return false;
    } else {
        document.getElementById("lblError").innerHTML = '';
        return true;
        //if (confirm("Are you sure to submit?")) {
        //    document.getElementById("btnSubmit").disabled = true;
        //    __doPostBack(document.getElementById("btnSubmit").name, '');
        //    return true;
        //} else {
        //    return false;
        //}
    }
}