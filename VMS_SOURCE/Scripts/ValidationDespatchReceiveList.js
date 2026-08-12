//'**************************************************
//'Copyright	: Berger, MCC, KOLKATA
//'Source	    : Scripts/ValidationDespatchReceiveList.js
//'Created Date	: 09/04/2013
//'Created By	: Rohan Mazumdar
//'Version	    : R02.00.00
//'Description	: 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function rwslctToggleSelect(clkdCheckBox, hdnLtr, hdnKg, txtRecvLtr, txtRecvKg, txtRecvDate) {

    var theGridView = document.getElementById("gvDespatchRecvList");

    var flag = 0;

    var chkbxcntrl_id = null;

    if (document.getElementById(clkdCheckBox).checked == true)
    {
        document.getElementById(clkdCheckBox).parentNode.parentNode.style.backgroundColor = "lightgreen";
        document.getElementById(txtRecvLtr).disabled = false;
        document.getElementById(txtRecvLtr).value = document.getElementById(hdnLtr).value;
        document.getElementById(txtRecvKg).disabled = false;
        document.getElementById(txtRecvKg).value = document.getElementById(hdnKg).value;
        document.getElementById(txtRecvDate).disabled = false;

        var cdate = formatDate(new Date(), "dd/MM/yyyy");
        document.getElementById(txtRecvDate).value = cdate;
    }
    else {
        document.getElementById(clkdCheckBox).parentNode.parentNode.style.backgroundColor = "";
        document.getElementById(txtRecvLtr).disabled = true;
        document.getElementById(txtRecvLtr).value = "0.00";
        document.getElementById(txtRecvKg).disabled = true;
        document.getElementById(txtRecvKg).value = "0.00";
        document.getElementById(txtRecvDate).disabled = true;
        document.getElementById(txtRecvDate).value = "";
    }

    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
        chkbxcntrl_id = theGridView.rows(rowCount).cells(0).children(0).id;

        if (document.getElementById(chkbxcntrl_id).checked == true) {
            flag = 1;
            break;
        }
    }

    if (flag == 0) {
        if (document.getElementById("btnSubmit").disabled == false) {
            document.getElementById("btnSubmit").disabled = true;
        }
    }
    else {
        if (document.getElementById("btnSubmit").disabled == true) {
            document.getElementById("btnSubmit").disabled = false;
        }
    }

}


var firstErrorControl = "";
var errMsg = "";

function validateGrid() {

    firstErrorControl = "";
    errMsg = "";

    var theGridView = document.getElementById("gvDespatchRecvList");


    for (var rowno = 1; rowno < theGridView.rows.length; rowno++)
    {
        var chkbxcntrl_id = theGridView.rows(rowno).cells(0).children(0).id;
        var txtRecvLtr = theGridView.rows(rowno).cells(10).children(0).id;
        var txtRecvKg = theGridView.rows(rowno).cells(11).children(0).id;
        var txtRecvDate = theGridView.rows(rowno).cells(12).children(0).id;
        var hdnChallanDate = theGridView.rows(rowno).cells(7).children(1).id;

        if (document.getElementById(chkbxcntrl_id).checked) {
            ValidateRequired(txtRecvLtr, "Please enter Litres received.");
            ValidateRequired(txtRecvKg, "Please enter Kgs received.");
            if (ValidateRequired(txtRecvDate, "Please enter Receipt Date.")) {
                if (CheckDateFormat(txtRecvDate, "Invalid Receipt Date.")) {
                    if (ValidatetwoDates(hdnChallanDate, txtRecvDate, "Receipt Date should be greater than Challan Date.")) {
                        ValidateGThanSystemDate(txtRecvDate, "Receipt Date cannot be greater than Today's date.");
                    }
                }
            }
        }
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {

        if (confirm("Are you sure to Submit?"))
        {
            document.getElementById("btnSubmit").disabled = true;
            __doPostBack(document.getElementById("btnSubmit").name, '');
            document.getElementById("lblErrorMessage").innerHTML = "";
        }
        else {
            return false;
        }
    }

}


function ltrim(valuetotrim) {
    var textaftertrim = "";

    for (var j = 0; j <= valuetotrim.length - 1; j++) {
        if (valuetotrim.charAt(j) != " ") {
            textaftertrim += valuetotrim.charAt(j);
        }
    }

    return textaftertrim;
}


function validateReceiptValue(valueToConvert) {
    //var result = true;

    document.getElementById(valueToConvert).style.backgroundColor = "white";

    var valueToValidate = ltrim(document.getElementById(valueToConvert).value);
    if (valueToValidate != "") {
        var val = new Number(valueToValidate);
        if (val.toString() != "NaN") {
            if (val >= 0) {
                document.getElementById(valueToConvert).value = val.toFixed(2);
            }
            else {
                alert("Value entered cannot be less than zero. Please enter a positive numeric value.");
                document.getElementById(valueToConvert).value = new Number(0).toFixed(2);
                //document.getElementById(valueToConvert).focus();
            }
        }
        else {
            alert("Value entered is not a number. Please enter a numeric value.");
            document.getElementById(valueToConvert).value = new Number(0).toFixed(2);
            //document.getElementById(valueToConvert).focus();
        }
    }
    else {
        document.getElementById(valueToConvert).value = new Number(0).toFixed(2);
        //document.getElementById(valueToConvert).focus();
    }
}