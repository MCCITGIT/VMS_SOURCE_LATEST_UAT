function validateQty(txt) {
    firstErrorControl = "";
    errMsg = "";
    if (document.getElementById(txt).value != "") {
        var isnum = /^[-+]?[0-9]+$/.test(document.getElementById(txt).value);
        if (isnum) {
            //var qty = document.getElementById(txt).value;
            //if (qty > 0) {
            //    return true;
            //}
            //else {
            //    document.getElementById(txt).value = "";
            //    window.alert("Quantity should not be less than 0.");
            //    return false;
            //}
            return true;
        }
        else {
            document.getElementById(txt).value = "";
            window.alert("Quantity should not contain alphabets.");
            return false;
        }
    }
}

function ValidateSubmit() {
    firstErrorControl = "";
    errMsg = "";
  
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {
        var tbl = document.getElementById("gvRequisitionItemsList");
        var txtList = tbl.querySelectorAll('input[type=text]');
        var flag = false; var flag2 = false;
        for (var i = 0; i < txtList.length; i++) {
            var txtQty = txtList[i];
            if (!txtQty.disabled) {
                if (txtQty.value == "") {
                    flag = true;

                }
                else {
                    flag = false;
                    if (txtQty.value <= 0) {
                        flag2 = true;
                    }
                    else {
                        flag2 = false;
                        break;
                    }

                }
            }

        }
        if (flag) {
            window.alert("Please Fill at least one qty.");
            return false;
        }
        else {
            if (flag2) {
                window.alert("Qty. has to be greater than zero.");
                return false;
            }
            else {
                if (confirm("Are you sure to submit this?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }

    }
}
