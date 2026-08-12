function ValidateDetails(chkbxChildDepotList, hdnParentDepot, lblPopValidationMessage, btnSubmit) {
    firstErrorControl = "";
    errMsg = "";
    ValidateRequired(hdnParentDepot, "Please select parent depot.");
    var CheckBoxdepot = document.getElementById(chkbxChildDepotList);
    var count = 0;
    if (CheckBoxdepot != null) {
        for (var i = 0; i < CheckBoxdepot.getElementsByTagName("input").length; i++) {
            var chknode = CheckBoxdepot.getElementsByTagName("input")[i];
            if (chknode != null && chknode.type == "checkbox" && chknode.checked == true) {
                count = count + 1;
            }
        }
    }
    if (count < 1) {
        firstErrorControl = chkbxChildDepotList;
        errMsg += GetErrorRow(chkbxChildDepotList, "Please select atleast one child depot");
        SetErrorColor(chkbxChildDepotList, false);
    }
    else {
        SetErrorColor(chkbxChildDepotList, true);
    }
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById(lblPopValidationMessage).innerHTML = errMsg;
        return false;
    }
    else {

        document.getElementById(lblPopValidationMessage).innerHTML = "";
        if (confirm("Are you sure to submit?")) {
            document.getElementById(btnSubmit).disabled = true;
            __doPostBack(document.getElementById(btnSubmit).name, "");
         
            return true;
        }
        else {
            return false;
        }
    }
}