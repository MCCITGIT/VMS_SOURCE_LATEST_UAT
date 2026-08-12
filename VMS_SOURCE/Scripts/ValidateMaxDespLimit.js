

function ValidateSubmit() {
    firstErrorControl = "";
    errMsg = "";

    var Grid = document.getElementById('gvDespDtl');
    var rowcount = Grid.rows.length - 1;

    for (var rowno = 1; rowno < Grid.rows.length; rowno++) {
       var txt;
        try {
            txt = Grid.rows[rowno].cells[3].children[0].children[0].id;
        }
        catch (e) {
            txt = Grid.rows[rowno].cells[3].children[0].id;
        }
        if (ValidateRequired(txt,'Enter Limit.')) {
            ValidateTwoDecimal(txt, 'Enter Valid Two Decimal Value.');
        }

    }
    
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        //         document.getElementById("divErrorMessage").innerHTML = errMsg;
        //         document.getElementById("lblErrorMessage").innerHTML = "";CheckDateFormat 
        document.getElementById("lblErrorMessage").innerHTML = errMsg

        return false;
    }
    else {
        if (confirm('Are you sure to Submit?')) {
            document.getElementById('btnSubmit').disabled = true;
            __doPostBack(document.getElementById('btnSubmit').name, '');
            document.getElementById("lblErrorMessage").innerHTML = ''
        }
        else {
            return false;
        }
    }

}
