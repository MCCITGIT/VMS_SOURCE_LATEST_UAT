var firstErrorControl;
var errMsg;

function validateAdjustment() {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("ddlSubInventoryPop", "Please Select Sub Inventory.");
    ValidateRequired("ddlLocatorPop", "Please Select Locator.");

    if (ValidateRequired("txtQtyPop", "Please input Receipt Quantity.") === true) {
        var quant = new Number(document.getElementById("txtQtyPop").value);
        if (quant.toString() !== "NaN") {
            if (quant <= 0) {
                alert("Receive Quantity can not be 0 or Negative.");
                document.getElementById("txtQtyPop").value = "";
                return false;
            }
            else if (validatereceivequantitycheck("txtQtyPop", "lblDespopQty") === false) {
                firstErrorControl = "txtQtyPop";
                return false;
            }
        }
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        var lblError = document.getElementById("lblError");
        if (!lblError) {
            lblError = document.getElementById("ctl00_ContentPlaceHolder1_lblError");
        }
        if (lblError) {
            lblError.innerHTML = errMsg;
        }
        return false;
    }

    var lblErrorClear = document.getElementById("lblError");
    if (!lblErrorClear) {
        lblErrorClear = document.getElementById("ctl00_ContentPlaceHolder1_lblError");
    }
    if (lblErrorClear) {
        lblErrorClear.innerHTML = "";
    }
    return true;
}

function validatereceivequantitycheck(despatchquant, requestquant) {
    var requestQty = new Number(document.getElementById(requestquant).innerText || document.getElementById(requestquant).textContent);

    var valueToValidate = (document.getElementById(despatchquant).value || "").replace(/^\s+/, "");
    if (valueToValidate !== "") {
        var val = new Number(valueToValidate);
        if (val.toString() !== "NaN") {
            document.getElementById(despatchquant).value = val;
            if (val > 0) {
                if (val > requestQty) {
                    alert("Receive quantity can not greater than Balance Quantity");
                    document.getElementById(despatchquant).value = "";
                    return false;
                }
                return true;
            }
            alert("Receive Quantity can not be 0 or Negative");
            document.getElementById(despatchquant).value = "";
            return false;
        }
        alert("Value entered is not a number. Please enter a numeric value.");
        document.getElementById(despatchquant).value = "";
        return false;
    }
    return true;
}

function validateReceive() {
    debugger

    firstErrorControl = "";
    errMsg = "";
    var count = 0;
    var objgridview = document.getElementById("gvVendorRawMat");
    if (objgridview == null) {
        alert("No item found for receipt.");
        return false;
    }

    //for (var i = 1; i < objgridview.rows.length; i++) {
    //    var inputs = objgridview.rows[i].getElementsByTagName("input");
    //    for (var j = 0; j < inputs.length; j++) {
    //        if (inputs[j].type === "checkbox" && inputs[j].checked) {
    //            count = count + 1;
    //        }
    //    }
    //}

    //if (count < 1) {
    //    alert("Please do the Adjustment for any one Item from the List.");
    //    return false;
    //}
    var decimalRegex = /^\d+(\.\d{1,2})?$/;

    for (var i = 1; i < objgridview.rows.length; i++) {

        var row = objgridview.rows[i];

        var txtGood = row.querySelector('[id*="txtGood"]');
        var txtDamage = row.querySelector('[id*="txtDamage"]');
        var txtShort = row.querySelector('[id*="txtShort"]');

        // Good validation
        //if (txtGood && txtGood.value.trim() === "0") {
        //    errMsg = "Please enter Good Quantity.";
        //    firstErrorControl = txtGood;
        //    break;
        //}

        if (txtGood && !decimalRegex.test(txtGood.value.trim())) {
            errMsg = "Good Quantity must be a valid number with maximum 2 decimal places.";
            firstErrorControl = txtGood;
            break;
        }

        // Damage validation
        //if (txtDamage && txtDamage.value.trim() === "0") {
        //    errMsg = "Please enter Damage Quantity.";
        //    firstErrorControl = txtDamage;
        //    break;
        //}

        if (txtDamage && !decimalRegex.test(txtDamage.value.trim())) {
            errMsg = "Damage Quantity must be a valid number with maximum 2 decimal places.";
            firstErrorControl = txtDamage;
            break;
        }

        // Short validation
        //if (txtShort && txtShort.value.trim() === "0") {
        //    errMsg = "Please enter Short Quantity.";
        //    firstErrorControl = txtShort;
        //    break;
        //}

        if (txtShort && !decimalRegex.test(txtShort.value.trim())) {
            errMsg = "Short Quantity must be a valid number with maximum 2 decimal places.";
            firstErrorControl = txtShort;
            break;
        }
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        var lblMsg = document.getElementById("lblErrorMessage");
        if (lblMsg) {
            lblMsg.innerHTML = errMsg;
        }
        return false;
    }

    var lblMsgClear = document.getElementById("lblErrorMessage");
    if (lblMsgClear) {
        lblMsgClear.innerHTML = "";
    }
    if (confirm("Are you sure to submit ?")) {
        return true;
    }
    return false;
}
