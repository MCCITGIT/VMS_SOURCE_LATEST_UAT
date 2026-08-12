function ValidateDespatchHdrUpdate(txtInvoiceNo, txtInvoiceDate, txtTransporterName, txtLorryNo, txtWayBill, txtEwayBillDate, txtValidUpTo, UploadFile, txtfinalinvoicevalue, gvDispatchAssignDtls,  btnSave, lblErrorMessage) {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired(txtInvoiceNo, "Please enter Invoice No")
    ValidateRequired(txtInvoiceDate, "Please enter Invoice Date")
    ValidateRequired(txtTransporterName, "Please enter Transporter Name")
    //ValidateRequired(txtLorryNo, "Please enter Vehicle No.")
    //ValidateRequired(txtWayBill, "Please enter E-Way Bill No.")
    ValidateRequired(txtfinalinvoicevalue, "Please enter Final Invoice Value")

    //ValidateRequired(txtEwayBillDate, "Enter E-Way Bill Date")
    //ValidateRequired(txtValidUpTo, "Enter Valid to")
    ValidateRequired(UploadFile, "Add a document.")
    var finalInvoice = document.getElementById(txtfinalinvoicevalue).value;
    var PONO = document.getElementById("txtpono").value;
    //var TotalrateincgstValue = document.getElementById(lbltotalrateincgst).value;
    var TotalInvoice="";
    var SkuRate = "";
    var SkuGST = "";
    var grid = document.getElementById(gvDispatchAssignDtls);

    var result = 0;
    var resultdiff = 0;
    var Total = 0;
    var TotalRate = 0;
    debugger;
    //if (PONO != "") {
    //    debugger;
    //    if (grid != null) {
    //        for (var rowno = 1; rowno < grid.rows.length ; rowno++) {
    //            TotalInvoice = grid.rows[rowno].cells[7].children[3].value;
    //            SkuRate = grid.rows[rowno].cells[7].children[1].value;
    //            SkuGST = grid.rows[rowno].cells[7].children[2].value;

    //            Total = parseFloat(TotalInvoice) * parseFloat(SkuRate);
    //            TotalRate = TotalRate + (Total + (Total * (parseFloat(SkuGST) / 100)));
    //        }
    //        result = TotalRate - finalInvoice;
    //        //resultdiff = TotalrateincgstValue - finalInvoice;
    //        if (result.toString().charAt(0) == "-") {
    //            if (-10 > result && result < 10) {
    //                firstErrorControl = txtfinalinvoicevalue;
    //                SetErrorColor(txtfinalinvoicevalue, false);
    //                errMsg += GetErrorRow(txtfinalinvoicevalue, "Total Rate Not matching With the Final Invoice Value.");
    //            }
    //        }
    //        else {
    //            if (-10 < result && result > 10) {
    //                firstErrorControl = txtfinalinvoicevalue;
    //                SetErrorColor(txtfinalinvoicevalue, false);
    //                errMsg += GetErrorRow(txtfinalinvoicevalue, "Total Rate Not matching With the Final Invoice Value.");
    //                //document.getElementById(lblErrorMessage).innerHTML = "Total Rate Not matching With the Final Invoice Value.";
    //                //return false;
    //            }
    //        }
    //    }
    //}
    

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById(lblErrorMessage).innerHTML = errMsg;
        return false;
    }

    else {
        document.getElementById(lblErrorMessage).innerHTML = '';
        if (confirm('Are you sure to Submit?')) {
            document.getElementById(btnSave).disabled = true;
            __doPostBack(document.getElementById(btnSave).name, '');
            return true;
        }
        else {
            return false;
        }
    }
}