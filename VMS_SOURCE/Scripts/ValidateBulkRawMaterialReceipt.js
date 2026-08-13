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
        var lblError = document.getElementById("lblAdjustError");
        if (lblError) {
            lblError.innerHTML = errMsg;
        }
        return false;
    }

    var lblErrorClear = document.getElementById("lblAdjustError");
    if (lblErrorClear) {
        lblErrorClear.innerHTML = "";
    }
    return true;
}

function validatereceivequantitycheck(despatchquant, requestquant) {
    debugger;
    var requestQty = new Number(document.getElementById(requestquant).innerText || document.getElementById(requestquant).textContent);

    var valueToValidate = (document.getElementById(despatchquant).value || "").replace(/^\s+/, "");
    if (valueToValidate !== "") {
        var val = new Number(valueToValidate);
        if (val.toString() !== "NaN") {
            document.getElementById(despatchquant).value = val;
            if (val > 0) {
                if (val > requestQty) {
                    alert("Receive quantity can not greater than Despatch Quantity");
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
