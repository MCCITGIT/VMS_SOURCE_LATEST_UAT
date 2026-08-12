function ValidateAdviceApp() {
              firstErrorControl = "";
        errMsg = "";
      ValidateDropDown1("ddlCourier", missingddlCourier)
        //ValidateDropDown1("ddlSchemeCode",missingschemecode)
      if (ValidateRequired("txtBoxcount", missingBoxNo)) {
          var no = parseFloat(document.getElementById('txtBoxcount').value);
          if (no == 0) {
              firstErrorControl = 'txtBoxcount';
              errMsg += GetErrorRow('txtBoxcount', missingBoxNo);
              SetErrorColor('txtBoxcount', false);
          }
      }
        ValidateRequired("txtVehicleNo", missingVehicle)

       

        if (firstErrorControl != "") {
            SetControlFocus(firstErrorControl);
            errMsg = "<table>" + errMsg + "</table>";
            document.getElementById("divErrorMessage").innerHTML = errMsg;
            return false;
        }
        else {

            var theGiftGrid = document.getElementById('gv_adviceSummery');
            var giftrowcount = theGiftGrid.rows.length - 1;
            var arrstockqty = new Array2D(giftrowcount, 2);

            for (var rowno = 1; rowno < theGiftGrid.rows.length; rowno++) {
                var itemId = theGiftGrid.rows(rowno).cells(0).children(1).id;
                var stockqtyId = theGiftGrid.rows(rowno).cells(1).children(0).id;
                arrstockqty[rowno - 1][0] = document.getElementById(itemId).value;
                arrstockqty[rowno - 1][1] = document.getElementById(stockqtyId).innerHTML;
            }


            var theGridView = document.getElementById("gvDealerAdviceItemsList");
            var totalDespatch = 0;
            var actualDespatch = 0;
            var TodespatchQty = 0;
            var advicerowcount = theGridView.rows.length - 1;
            var arritemqty = new Array2D(advicerowcount, 2);


            for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
                try {
                    var chk = theGridView.rows(rowCount).cells(0).children(0).children(0);
                    var txtActualDespatchQtyId = theGridView.rows(rowCount).cells(4).children(0).id;
                    var hdnActualDespatchQtyId = theGridView.rows(rowCount).cells(4).children(1).id;
                    var txtrate = theGridView.rows(rowCount).cells(5).children(0).id;
                    var txtConsId = theGridView.rows(rowCount).cells(7).children(0).id;

                    var txtConsDateId = theGridView.rows(rowCount).cells(8).children(0).id;
                    var ItemcodeId = theGridView.rows(rowCount).cells(2).children(1).id;



                    if (chk.checked) {

                        if (ValidateRequired(txtrate, missingRate)) {
                            ValidateDecimal(txtrate, invalidRate)
                        }
                        ValidateRequired(txtConsId, missingConsignmentNo)
                        if (ValidateRequired(txtConsDateId, missingadviceDate)) {
                             CheckDateFormat(txtConsDateId, invalidAdviceDate);
                             ValidateGThanSystemDate(txtConsDateId, greaterConDate);
                        }
                                    
                                            

                    }
                }

                catch (e) { }

            }

            if (firstErrorControl != "") {
                SetControlFocus(firstErrorControl);
                errMsg = "<table>" + errMsg + "</table>";
                document.getElementById("divErrorMessage").innerHTML = errMsg;
                return false;
            }
     

                        

                if (confirm('Are you sure to submit?')) {
                    document.getElementById('btnApproval').disabled = true;
                    //                     if(event.keyCode == 118)
                    deleteBrowsHistory()   
                    __doPostBack(document.getElementById('btnApproval').name, '');


                    //                     return true;            
                }
                else {
                    return false;
                }



        }


  
}


function fngetConsGet(type) {

    if (type == 'No') {
        var theGridView = document.getElementById("gvDealerAdviceItemsList");
        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            try {
                var chk = theGridView.rows(rowCount).cells(0).children(0).children(0);
                var txtConsId = theGridView.rows(rowCount).cells(7).children(0).id;

                var txtConsDateId = theGridView.rows(rowCount).cells(8).children(0).id;
                var ItemcodeId = theGridView.rows(rowCount).cells(2).children(1).id;



                if (chk.checked) {

                    document.getElementById(txtConsId).value = document.getElementById('txtConsno').value;

                }
            }
            catch (e) { }
        }
    }
    else {
        var theGridView = document.getElementById("gvDealerAdviceItemsList");
        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            try {
                var chk = theGridView.rows(rowCount).cells(0).children(0).children(0);

                var txtConsId = theGridView.rows(rowCount).cells(7).children(0).id;

                var txtConsDateId = theGridView.rows(rowCount).cells(8).children(0).id;
                var ItemcodeId = theGridView.rows(rowCount).cells(2).children(1).id;



                if (chk.checked) {

                    document.getElementById(txtConsDateId).value = document.getElementById('txtConsDate').value;

                }
            }
            catch (e) { }
        }
    }
}

