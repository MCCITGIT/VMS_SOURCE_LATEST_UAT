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
function validateSKUList() {
    
    var theGridView = document.getElementById('gvIndentSKUList');

    var flag = 0;
    firstErrorControl = "";

    var txtNewLoad_id = null;

    document.getElementById("lblErrorMessage").innerHTML = "";

    errMsg = "";

    // ValidateDropDown("ddlDepot", "Please Select a Depot.");
   
  //  ValidateDropDown("ddlVendorUnit", "Please Select Vendor Unit.");
    // ValidateDropDown("ddlVendorProduct", "Please Select Product.");
   
    if (ValidateRequired("ddlDepot", "Please Select a Depot.")) {
        var select = document.querySelector("#" + "ddlDepot" + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = "ddlDepot";
        var select = document.querySelector("#" + "ddlDepot" + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }
    if (ValidateRequired("ddlVendorUnit", "Please Select Vendor Unit.")) {
        var select = document.querySelector("#" + "ddlVendorUnit" + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = "ddlVendorUnit";
        var select = document.querySelector("#" + "ddlVendorUnit" + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }
    if (ValidateRequired("ddlVendorProduct", "Please Select Product.")) {
        var select = document.querySelector("#" + "ddlVendorProduct" + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "white";
        }
    }
    else {
        firstErrorControl = "ddlVendorProduct";
        var select = document.querySelector("#" + "ddlVendorProduct" + "+ .select2-container .selection .select2-selection");
        if (select != null) {
            select.style.backgroundColor = "yellow";
        }
    }

    if (theGridView != null) {

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            
            theGridView.rows[rowCount].cells[9].children[0].style.backgroundColor = "#ffffff";
        }

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            txtNewLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;

            if (lTrim(document.getElementById(txtNewLoad_id).value, " ") != "0") {
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
        document.getElementById("lblErrorMessage").innerHTML = "<table style='width:100%;'>" + errMsg + "</table>";
        return false;
    }
    else {
        var txtLoad_id = null;
        var justification = null;

        var flag1 = 0;

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            txtLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;

            if (lTrim(document.getElementById(txtLoad_id).value, " ") != "0") {
                justification = theGridView.rows[rowCount].cells[9].children[0].id;
                if (ValidateRequired(justification, "Enter justification for additional load.") == false) {
                    flag1 = 1;
                }
            }
        }

        if (flag1 == 0) {
            if (confirm('Are you sure to submit?')) {
                document.getElementById('btnSubmit').disabled = true;
                __doPostBack(document.getElementById('btnSubmit').name, '');

            }
            else {
                return false;
            }
        }
        else {
            document.getElementById("lblErrorMessage").innerHTML = "<table>" + errMsg + "</table>";
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

    document.getElementById("lblErrorMessage").innerHTML = "";

    errMsg = "";

    ValidateDropDown("ddlDepot", "Please Select a Depot.");
    ValidateDropDown("ddlVendorUnit", "Please Select Vendor Unit.");
    //            ValidateDropDown("ddlVendorProduct", "Please Select Product.")

    if (theGridView != null) {

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            theGridView.rows[rowCount].cells[9].children[0].style.backgroundColor = "#ffffff";
        }

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            txtNewLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;

            if (lTrim(document.getElementById(txtNewLoad_id).value, " ") != "0") {
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
        document.getElementById("lblErrorMessage").innerHTML = "<table style='width:100%;'>" + errMsg + "</table>";
        return false;
    }
    else {
        var txtLoad_id = null;
        var justification = null;

        var flag1 = 0;

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            txtLoad_id = theGridView.rows[rowCount].cells[8].children[0].id;

            if (lTrim(document.getElementById(txtLoad_id).value, " ") != "0") {
                justification = theGridView.rows[rowCount].cells[9].children[0].id;
                if (ValidateRequired(justification, "Enter justification for additional load.") == false) {
                    flag1 = 1;
                }
            }
        }

        if (flag1 == 0) {
            if (confirm('Are you sure to submit?')) {
                document.getElementById('btnSubmit').disabled = true;
                __doPostBack(document.getElementById('btnSubmit').name, '');

            }
            else {
                return false;
            }
        }
        else {
            document.getElementById("lblErrorMessage").innerHTML = "<table>" + errMsg + "</table>";
            return false;
        }
    }

}
