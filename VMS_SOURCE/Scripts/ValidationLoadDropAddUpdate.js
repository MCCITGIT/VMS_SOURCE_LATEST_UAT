function ValidateSubmit() {
    firstErrorControl = "";
    errMsg = "";
    debugger;
   

    if (ValidateGrid()) {
        var Grid = document.getElementById('ctl00_ContentPlaceHolder1_gvSKUDetails');
        var rowcount = Grid.rows.length - 1;
        var txtPendQty;
        var txtQty;
        var chk
        debugger;
        for (var rowno = 1; rowno < Grid.rows.length; rowno++) {
            try {
                txtPendQty = Grid.rows[rowno].cells[9].children[0].children[0].id;
                txtQty = Grid.rows[rowno].cells[11].children[0].children[0].id;

            }
            catch (e) {
                txtPendQty = Grid.rows[rowno].cells[9].children[0].id;
                txtQty = Grid.rows[rowno].cells[11].children[0].id;
            }
            try {
                chk = Grid.rows[rowno].cells[1].children[0].children[0].id;
            
            }
            catch (e) {
                chk = Grid.rows[rowno].cells[1].children[0].id;
            }
            if (document.getElementById(chk).checked) {
                if (ValidateRequired(txtQty, "Please Enter Quantity")) {
                    ValidateNumbers(txtQty, "Enter Number Only");
                    RangeComparision(txtQty, txtPendQty, "Drop qty can not exceed pending qty.");
                }                                                                  
            }
        }
      
        if (firstErrorControl != "") {
            SetControlFocus(firstErrorControl);
            errMsg = "<table>" + errMsg + "</table>";
            document.getElementById('lblErrMsg').innerHTML = errMsg

            return false;
        }
        else {
            if (confirm('Are you sure to Update ?')) {
                document.getElementById('btnSubmit').disabled = true;
                __doPostBack(document.getElementById('btnSubmit').name, '');
                document.getElementById('lblErrMsg').innerHTML = ''
            }
            else {
                return false;
            }
        }
    }
    else {
        return false;
    }
}


function ValidateGrid() {
    var Grid = document.getElementById('ctl00_ContentPlaceHolder1_gvSKUDetails');
    var rowcount = Grid.rows.length - 1;
    var i = 0;
    var chk;


    for (var rowno = 1; rowno < Grid.rows.length; rowno++) {
        try {
            chk = Grid.rows[rowno].cells[1].children[0].children[0].id;
         

        }
        catch (e) {
            chk = Grid.rows[rowno].cells[1].children[0].id;
          
        }


        if (document.getElementById(chk).checked) {
            i = i + 1
        }
    }
    if (i == 0) {
        alert('Please Select Atleast one Record')
        return false
    }
    else {
        return true;
    }
}

function QTYLockUnlock(chkbox, txtQTY) {
    //   debugger;
    var Chk = document.getElementById(chkbox);
    var txt = document.getElementById(txtQTY);
   
    if (Chk.checked) {
        txt.disabled = false;      
    }
    else {
        txt.disabled = true;      
    }
}

function RangeComparision(controlName1, controlName2, errorMessage) {
   
    if (document.getElementById(controlName1).value != "" && document.getElementById(controlName2).value != "") {
        var plotFrom = document.getElementById(controlName1).value;
        var plotTo = document.getElementById(controlName2).innerHTML;
    }
    else if (document.getElementById(controlName1).value == "" && document.getElementById(controlName2).innerHTML != "") {
        var controlID = controlName1;
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }
    else if (document.getElementById(controlName1).value != "" && document.getElementById(controlName2).innerHTML == "") {
        var controlID = controlName1;
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }


    if (parseInt(plotFrom) > parseInt(plotTo)) {
        var controlID = controlName1;
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }
    else
        SetErrorColor(controlID, true);
    return true;
}
function isIntegerNumberKey(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105)) {
        if (charCode == 46) {
            //Check if the text already contains the . character
            if (txt.value.indexOf('.') === -1) {
                return false;
            } else {
                return false;
            }
        } else {
            if (charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;
        }
    }
    else {
        return false;
    }
    return true;
}