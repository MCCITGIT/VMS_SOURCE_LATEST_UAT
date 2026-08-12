var firstErrorControl;
var errMsg;
function ValidateOcSpecification() {
    debugger;
    firstErrorControl = "";
    errMsg = "";
    var theGridView = document.getElementById(gvProductParameterId);
    var flag = 0;

    ValidateDropDown1("ddlVender", "Please enter Vender Code.");
    ValidateDropDown1("ddlProduct", "Please select a Product.");
    ValidateDropDown1("ddlProductCode", "Please select a Product Code.");   
    ValidateRequired("txtBatchno", "Please enter Batch No.");
    if (ValidateRequired("txtBatchDate", "Please enter Batch Date.")) {
        CheckDateFormat("txtBatchDate", "Invalid Batch Date.");
    }

    if (theGridView != null) {
       
        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            lblFrequency = theGridView.rows[rowCount].cells[2].children[0].id;
            txtresult = theGridView.rows[rowCount].cells[1].children[0].id;            
            let a = document.getElementById(txtresult).value;
            let b = document.getElementById(lblFrequency).innerText;
            
            if (b == "Each") {
                ValidateRequired(txtresult, "Entry cannot be blank for Each Frequency.");
                //flag = 1;
                //break;
            }            
        }
    }
    //if (flag == 1) {
    //    firstErrorControl = theGridView;
    //    errMsg += "Entry cannot be blank for Each Frequency.";
    //}
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {
        document.getElementById("lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to submit?")) {
            document.getElementById("btnSubmit").disabled = true;
            __doPostBack(document.getElementById("btnSubmit").name, '');
            return true;
        }
        else {
            return false;
        }
    }
}
