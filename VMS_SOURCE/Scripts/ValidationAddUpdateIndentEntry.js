//'**************************************************
//'Copyright	: Berger, MCC, KOLKATA
//'Source	    : Scripts/ValidationAddUpdateIndentEntry.js
//'Created Date	: 21-Dec-2011
//'Created By	: Rohan Mazumdar
//'Version	    : R02.00.00
//'Description	: 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File
var errMsg = "";

function getIndentErrorLabel() {
    return document.getElementById("lblErrorMessage")
        || document.querySelector('span[id$="lblErrorMessage"]');
}

function setIndentErrorMessage(messageHtml) {
    var lblErrorMessage = getIndentErrorLabel();
    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = messageHtml || "";
    }
}

function getIndentControl(controlId) {
    return document.getElementById(controlId)
        || document.querySelector('[id$="_' + controlId + '"]');
}

function validateIndentFieldRequired(controlId, errorMessage) {
    var control = getIndentControl(controlId);
    if (!control) {
        if (!firstErrorControl) {
            firstErrorControl = controlId;
        }
        errMsg += GetErrorRow(controlId, errorMessage);
        return false;
    }

    return ValidateRequired(control.id, errorMessage);
}

function validateSKUList() {

    var theGridView = document.getElementById('gvIndentSKUList');

    var flag = 0;
    firstErrorControl = "";

    var txtNewLoad_id = null;

    setIndentErrorMessage("");

    errMsg = "";

    //ValidateDropDown("ddlDepot", "Please Select a Depot.");
    //ValidateDropDown("ddlVendorUnit", "Please Select Vendor Unit.");
    //ValidateDropDown("ddlVendorProduct", "Please Select Product.")

    var ddlDepotCtrl = getIndentControl("ddlDepot");
    var ddlDepotId = ddlDepotCtrl ? ddlDepotCtrl.id : "ddlDepot";
    if (ddlDepotCtrl && ValidateRequired(ddlDepotId, "Please Select a Depot.")) {
        var select = document.querySelector("#" + ddlDepotId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = ddlDepotId;
        if (!ddlDepotCtrl) {
            errMsg += GetErrorRow(ddlDepotId, "Please Select a Depot.");
        }
        var select = document.querySelector("#" + ddlDepotId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    var ddlVendorUnitCtrl = getIndentControl("ddlVendorUnit");
    var ddlVendorUnitId = ddlVendorUnitCtrl ? ddlVendorUnitCtrl.id : "ddlVendorUnit";
    if (ddlVendorUnitCtrl && ValidateRequired(ddlVendorUnitId, "Please Select Vendor Unit.")) {
        var select = document.querySelector("#" + ddlVendorUnitId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = ddlVendorUnitId;
        if (!ddlVendorUnitCtrl) {
            errMsg += GetErrorRow(ddlVendorUnitId, "Please Select Vendor Unit.");
        }
        var select = document.querySelector("#" + ddlVendorUnitId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    var ddlVendorProductCtrl = getIndentControl("ddlVendorProduct");
    var ddlVendorProductId = ddlVendorProductCtrl ? ddlVendorProductCtrl.id : "ddlVendorProduct";
    if (ddlVendorProductCtrl && ValidateRequired(ddlVendorProductId, "Please Select Product.")) {
        var select = document.querySelector("#" + ddlVendorProductId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = ddlVendorProductId;
        if (!ddlVendorProductCtrl) {
            errMsg += GetErrorRow(ddlVendorProductId, "Please Select Product.");
        }
        var select = document.querySelector("#" + ddlVendorProductId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (theGridView != null) {

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (theGridView.rows[rowCount].cells[9] && theGridView.rows[rowCount].cells[9].children[0]) {
                theGridView.rows[rowCount].cells[9].children[0].style.backgroundColor = "#ffffff";
            }
        }

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (!theGridView.rows[rowCount].cells[8] || !theGridView.rows[rowCount].cells[8].children[0]) {
                continue;
            }

            txtNewLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;
            var txtNewLoad = document.getElementById(txtNewLoad_id);

            if (txtNewLoad && lTrim(txtNewLoad.value, " ") != "0") {
                flag = 1;
                break;
            }
        }
    }
    if (flag == 0 || errMsg != "") {
        //document.getElementById("lblErrorMessage").innerHTML = "Atleast one entry should be non zero.";
        if (flag == 0) {
            firstErrorControl = theGridView;
            errMsg += "Atleast one entry should be non zero.";
        }
        setIndentErrorMessage("<table style='width:100%;'>" + errMsg + "</table>");
        return false;
    }
    else {
        var txtLoad_id = null;
        var justification = null;

        var flag1 = 0;

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (!theGridView.rows[rowCount].cells[8] || !theGridView.rows[rowCount].cells[8].children[0]) {
                continue;
            }

            txtLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;
            var txtLoad = document.getElementById(txtLoad_id);

            if (txtLoad && lTrim(txtLoad.value, " ") != "0") {
                if (theGridView.rows[rowCount].cells[9] && theGridView.rows[rowCount].cells[9].children[0]) {
                    justification = theGridView.rows[rowCount].cells[9].children[0].id;
                    if (validateIndentFieldRequired(justification, "Enter justification for additional load.") == false) {
                        flag1 = 1;
                    }
                }
            }
        }

        if (flag1 == 0) {
            if (confirm('Are you sure to submit?')) {
                var btnSubmit = getIndentControl('btnSubmit');
                if (btnSubmit) {
                    btnSubmit.disabled = true;
                    __doPostBack(btnSubmit.name, '');
                }

            }
            else {
                return false;
            }
        }
        else {
            setIndentErrorMessage("<table>" + errMsg + "</table>");
            return false;
        }
    }

}

function lTrim(source, target) {
    var Count = 0;
    var strTrim;
    var retVal;

    if (source.length < 2) {
        return (source);
    } else if (target.length > 1) {
        return (target);
    } else if (left(source, 1) != target) {
        return (source);
    }

    // Trim the left side of string
    while (source.substring(Count, Count + 1) == target || Count == source.length) {
        Count = Count + 1
    }

    return (source.substring(Count, source.length));
}

function left(source, len) {
    var strImage;

    if (isNaN(len)) {
        strImage = source;
    } else {
        strImage = source.substring(0, len);
    }

    return (strImage);
}


function calculatePercentage(tsl, depot_indent_nop, percent_label, indent_nop) {
    var percent = 0;

    if (parseInt(tsl) != 0) {
        if (document.getElementById(indent_nop).value != "") {
            percent = Math.round(((parseInt(depot_indent_nop) + parseInt(document.getElementById(indent_nop).value))) * 100 / tsl);

            if (percent > 70) {
                //alert("Invalid Entry. Indent to Estimate % exceeded 70%.");
                //document.getElementById(indent_nop).style.backgroundColor = "yellow";
                //document.getElementById(indent_nop).focus();
                document.getElementById(percent_label).innerHTML = percent.toString() + " %";
                document.getElementById(indent_nop).style.backgroundColor = "";
                AddTotalLtrKg()
                return true;
                //return false;
            }
            else {
                document.getElementById(percent_label).innerHTML = percent.toString() + " %";
                document.getElementById(indent_nop).style.backgroundColor = "";
                AddTotalLtrKg()
                return true;
            }
        }
        else {
            document.getElementById(indent_nop).value = "0";
            percent = Math.round(((parseInt(depot_indent_nop) + 0)) * 100 / tsl);
            document.getElementById(percent_label).innerHTML = percent.toString() + " %";

            if (percent > 70) {
                //               alert("Invalid Entry. Indent to Estimate % exceeded 70%.");
                //               document.getElementById(indent_nop).style.backgroundColor = "yellow";
                //               document.getElementById(indent_nop).focus();
                //               return false;

                document.getElementById(percent_label).innerHTML = percent.toString() + " %";
                document.getElementById(indent_nop).style.backgroundColor = "";
                AddTotalLtrKg()
                return true;
            }
            else {
                document.getElementById(percent_label).innerHTML = percent.toString() + " %";
                document.getElementById(indent_nop).style.backgroundColor = "";
                AddTotalLtrKg()
                return true;
            }
        }
    }

    AddTotalLtrKg()
}


function AddTotalLtrKg() {
    var Grid = document.getElementById('gvIndentSKUList');
    var rowcount = Grid.rows.length - 1;
    var txt, hdnUom, hdnPackSize;

    var totValLtr = 0;
    var totValKg = 0;

    for (var rowno = 1; rowno < Grid.rows.length; rowno++) {
        try {
            txt = Grid.rows[rowno].cells[8].children[0].children[1].id;
            hdnUom = Grid.rows[rowno].cells[1].children[0].children[2].id;
            hdnPackSize = Grid.rows[rowno].cells[1].children[0].children[3].id;
        }
        catch (e) {
            txt = Grid.rows[rowno].cells[8].children[0].id;
            hdnUom = Grid.rows[rowno].cells[1].children[2].id;
            hdnPackSize = Grid.rows[rowno].cells[1].children[3].id;
        }
        var uom = document.getElementById(hdnUom).value;
        var packSize = parseFloat(document.getElementById(hdnPackSize).value);
        var enteredVal;

        try {
            enteredVal = parseFloat(document.getElementById(txt).value);
        }
        catch (e) {
            enteredVal = 0;
        }
        if (uom == "K") {
            totValKg = totValKg + (enteredVal * packSize);
        }
        if (uom == "L") {
            totValLtr = totValLtr + (enteredVal * packSize);
        }
    }
    document.getElementById('lblTotKg').innerHTML = totValKg;
    document.getElementById('lblTotLtr').innerHTML = totValLtr;


}







function validateSKUListAdd() {
    var theGridView = document.getElementById('gvIndentSKUList');

    var flag = 0;
    firstErrorControl = "";

    var txtNewLoad_id = null;

    setIndentErrorMessage("");

    errMsg = "";

    var ddlDepotCtrl = getIndentControl("ddlDepot");
    var ddlDepotId = ddlDepotCtrl ? ddlDepotCtrl.id : "ddlDepot";
    if (ddlDepotCtrl && ValidateRequired(ddlDepotId, "Please Select a Depot.")) {
        var select = document.querySelector("#" + ddlDepotId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = ddlDepotId;
        if (!ddlDepotCtrl) {
            errMsg += GetErrorRow(ddlDepotId, "Please Select a Depot.");
        }
        var select = document.querySelector("#" + ddlDepotId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    var ddlVendorUnitCtrl = getIndentControl("ddlVendorUnit");
    var ddlVendorUnitId = ddlVendorUnitCtrl ? ddlVendorUnitCtrl.id : "ddlVendorUnit";
    if (ddlVendorUnitCtrl && ValidateRequired(ddlVendorUnitId, "Please Select Vendor Unit.")) {
        var select = document.querySelector("#" + ddlVendorUnitId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = ddlVendorUnitId;
        if (!ddlVendorUnitCtrl) {
            errMsg += GetErrorRow(ddlVendorUnitId, "Please Select Vendor Unit.");
        }
        var select = document.querySelector("#" + ddlVendorUnitId + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (theGridView != null) {

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (theGridView.rows[rowCount].cells[9] && theGridView.rows[rowCount].cells[9].children[0]) {
                theGridView.rows[rowCount].cells[9].children[0].style.backgroundColor = "#ffffff";
            }
        }

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (!theGridView.rows[rowCount].cells[8] || !theGridView.rows[rowCount].cells[8].children[0]) {
                continue;
            }

            txtNewLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;
            var txtNewLoad = document.getElementById(txtNewLoad_id);
            if (txtNewLoad && lTrim(txtNewLoad.value, " ") != "0") {
                flag = 1;
                break;
            }
        }
    }
    if (flag == 0 || errMsg != "") {
        if (flag == 0) {
            firstErrorControl = theGridView;
            errMsg += "Atleast one entry should be non zero.";
        }
        setIndentErrorMessage("<table style='width:100%;'>" + errMsg + "</table>");
        return false;
    }
    else {
        var txtLoad_id = null;
        var justification = null;

        var flag1 = 0;

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (!theGridView.rows[rowCount].cells[8] || !theGridView.rows[rowCount].cells[8].children[0]) {
                continue;
            }

            txtLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;
            var txtLoad = document.getElementById(txtLoad_id);
            if (txtLoad && lTrim(txtLoad.value, " ") != "0") {
                if (theGridView.rows[rowCount].cells[9] && theGridView.rows[rowCount].cells[9].children[0]) {
                    justification = theGridView.rows[rowCount].cells[9].children[0].id;
                    if (validateIndentFieldRequired(justification, "Enter justification for additional load.") == false) {
                        flag1 = 1;
                    }
                }
            }
        }

        if (flag1 == 0) {
            if (confirm('Are you sure to submit?')) {
                var btnSubmit = getIndentControl('btnSubmit');
                if (btnSubmit) {
                    btnSubmit.disabled = true;
                    __doPostBack(btnSubmit.name, '');
                }

            }
            else {
                return false;
            }
        }
        else {
            setIndentErrorMessage("<table>" + errMsg + "</table>");
            return false;
        }
    }

}
