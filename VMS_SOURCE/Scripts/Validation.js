// JScript File
var firstErrorControl;
var errMsg = "";

// Checks the control in Project_Master_Creation
//function ValidatePMCControls()
//{
//    document.getElementById("lblConfirmMsg").innerHTML = ""; 
//    firstErrorControl ="";
//    errMsg= "";
//    var compareDate1 = false;
//    var compareDate2 = false;
//    var compareDate3 = false;
//    var compareDate4 = false;
//    var compareDate5 = false;
//    var compareDate6 = false;
//        
//   
//    if(ValidateRequired("txtPrjCode", missingPrjCode) )
//       ValidateSpecialChars("txtPrjCode", invalidPrjCode,"title")
//            
//    if(ValidateRequired("txtPrjLocation", missingPrjLocation) )
//       ValidateSpecialChars("txtPrjLocation", invalidPrjLocation,"title") 
//        
//    ValidateDropDown("ddlCity", missingddlCity)
//    
//    if(ValidateRequired("txtPrjLayoutCode", missingPrjLayoutCode) )
//       ValidateSpecialChars("txtPrjLayoutCode", invalidPrjLayoutCode,"title")   
//        
//    if(ValidateRequired("txtPrjHighlights", missingPrjHighlights))
//        CheckMaxlength("txtPrjHighlights",2048,greaterPrjHighlights)
//    
//    ValidateDropDown("ddlProjectType", missingddlProjectType)
//     
//    ValidateRequired("txtPrjTotalArea", missingPrjTotalArea)
//    
//    ValidateRequired("txtPrjMarkertableArea", missingPrjMarkertableArea)
//    
//    if(document.getElementById("txtPrjTotalArea").value!="" && document.getElementById("txtPrjMarkertableArea").value!="" )
//         areaComparision("txtPrjTotalArea","txtPrjMarkertableArea", greaterPrjMarkertableArea)
//    
//    ValidateRequired("txtPrjNoOfPlots", missingPrjNoOfPlots) 
//    
//    ValidateRequired("txtPrjAcquistionCost", missingPrjAcquistionCost) 
//     
//    ValidateRequired("txtPrjGuidelineValue", missingPrjGuidelineValue)    
//           
//    if(document.getElementById("txtPrjAcquistionCost").value!="" && document.getElementById("txtPrjGuidelineValue").value!="" )
//         areaComparision("txtPrjAcquistionCost","txtPrjGuidelineValue",greaterPrjGuidelineValue)
//    
//    
//    if(document.getElementById("lblGroup11").innerHTML=="*")
//    {
//        ValidateRequired("txtPrjApprovalNumber", invalidPrjApprovalNumber)
//        
//    }  
//    else
//    {
//        if(document.getElementById("txtPrjApprovalNumber").value!="")
//            ValidateSpecialChars("txtPrjApprovalNumber", invalidPrjApprovalNumber,"title")
//        else
//            SetErrorColor("txtPrjApprovalNumber", true); 
//    }
//        
//        
//    if(document.getElementById("lblGroup12").innerHTML=="*")
//    {
//        if(ValidateRequired("txtPrjApprovalDate", missingPrjApprovalDate))
//            if(CheckDateFormat("txtPrjApprovalDate", invalidPrjApprovalDate))
//                ValidateSystemDate("txtPrjApprovalDate", greaterPrjApprovalDate)    
//    }
//    else
//    {
//        if(document.getElementById("txtPrjApprovalDate").value!="")
//        {
//            if(CheckDateFormat("txtPrjApprovalDate", invalidPrjApprovalDate))
//                ValidateSystemDate("txtPrjApprovalDate", greaterPrjApprovalDate)
//        } 
//        else
//            SetErrorColor("txtPrjApprovalDate", true); 
//    }
//        
//    //ValidateRequired("txtPrjApprovedBy", missingPrjApprovedBy)  
//    
//    if(document.getElementById("lblGroup13").innerHTML=="*")
//    {
//        if(ValidateRequired("txtPrjApprovedBy", invalidPrjApprovedBy))
//        ValidateSpecialChars("txtPrjApprovedBy", invalidPrjApprovedBy,"title")
//    }
//    
//    else
//    {
//        if(document.getElementById("txtPrjApprovedBy").value!="")
//            ValidateSpecialChars("txtPrjApprovedBy", invalidPrjApprovedBy,"title")
//        else
//            SetErrorColor("txtPrjApprovedBy", true); 
//    }          
//     
//    
//          
//    ValidateRequired("txtPrjDevelopmentCost", missingPrjDevelopmentCost) 
//    
//    ValidateDropDown("ddlLandCategory", missingddlLandCategory)
//   
//    if(ValidateRequired("txtPrjMarkupPercentage", missingPrjMarkupPercentage) )
//        ValidateMarkDecimal("txtPrjMarkupPercentage", invalidPrjMarkupPercentage)    
//    
//    
//    ValidateDropDown("ddlProjectStatus",missingddlProjectStatus)
//    
////    if(document.getElementById("txtPrjLastUpdated").value!="")
////        CheckDateFormat("txtPrjLastUpdated", invalidPrjLastUpdated)
////    else
////        SetErrorColor("txtPrjLastUpdated", true); 

//    if(document.getElementById("manDevelpPlanSDate").innerHTML=="*")
//    {
//        if(ValidateRequired("txtPrjDevelopPlannedStartDate", missingPrjDevelopPlannedStartDate))
//            compareDate1=CheckDateFormat("txtPrjDevelopPlannedStartDate", invalidPrjDevelopPlannedStartDate)
//    }
//    else
//    {
//        if(document.getElementById("txtPrjDevelopPlannedStartDate").value!="")
//            compareDate1=CheckDateFormat("txtPrjDevelopPlannedStartDate", invalidPrjDevelopPlannedStartDate)
//        else
//            SetErrorColor("txtPrjDevelopPlannedStartDate", true); 
//    }
//      
//    if(document.getElementById("manDevelpPlanADate").innerHTML=="*")
//    {
//    if(ValidateRequired("txtPrjDevelopPlannedActualStartDate", missingPrjDevelopPlannedActualStartDate))
//        compareDate3=CheckDateFormat("txtPrjDevelopPlannedActualStartDate", invalidPrjDevelopPlannedActualStartDate)
//    }
//    else
//    {
//    if(document.getElementById("txtPrjDevelopPlannedActualStartDate").value!="")
//        compareDate3=CheckDateFormat("txtPrjDevelopPlannedActualStartDate", invalidPrjDevelopPlannedActualStartDate)
//    else
//        SetErrorColor("txtPrjDevelopPlannedActualStartDate", true); 
//    }   
//    
//       
//    if(document.getElementById("manDevelpPlanCompDate").innerHTML=="*")
//    {
//        if(ValidateRequired("txtPrjDevelopPlannedCompletionDate", missingPrjDevelopPlannedCompletionDate))
//            compareDate2=CheckDateFormat("txtPrjDevelopPlannedCompletionDate", invalidPrjDevelopPlannedCompletionDate)
//    }
//    else
//    {
//        if(document.getElementById("txtPrjDevelopPlannedCompletionDate").value!="")
//            compareDate2=CheckDateFormat("txtPrjDevelopPlannedCompletionDate", invalidPrjDevelopPlannedCompletionDate)
//        else
//            SetErrorColor("txtPrjDevelopPlannedCompletionDate", true); 
//    }   

//    //Compare Development Planned startdate and Development Planned Completion Date
//   if(compareDate1 && compareDate2)
//        ValidatetwoDates("txtPrjDevelopPlannedStartDate","txtPrjDevelopPlannedCompletionDate",greaterPrjDevelopPlannedCompletionDate)
//       
//   if(document.getElementById("manDevelpPlanACompDate").innerHTML=="*")
//    {
//    if(ValidateRequired("txtPrjDevelopPlannedActualCompletionDate", missingPrjDevelopPlannedActualCompletionDate))
//        compareDate4=CheckDateFormat("txtPrjDevelopPlannedActualCompletionDate", invalidPrjDevelopPlannedActualCompletionDate)
//    }
//    else
//    {
//    if(document.getElementById("txtPrjDevelopPlannedActualCompletionDate").value!="")
//        compareDate4=CheckDateFormat("txtPrjDevelopPlannedActualCompletionDate", invalidPrjDevelopPlannedActualCompletionDate)
//    else
//        SetErrorColor("txtPrjDevelopPlannedActualCompletionDate", true); 
//    }   
//        
//    //Compare Actual startdate and Actual Completion Date
//   if(compareDate3 && compareDate4)
//        ValidatetwoDates("txtPrjDevelopPlannedActualStartDate","txtPrjDevelopPlannedActualCompletionDate",greaterActualCompletionDate)

//       
//    if(document.getElementById("manDevelpPlanLanuchDate").innerHTML=="*")
//    {
//    if(ValidateRequired("txtPrjLaunchPlannedDate", missingPrjLaunchPlannedDate))
//        compareDate5=CheckDateFormat("txtPrjLaunchPlannedDate", invalidPrjLaunchPlannedDate)
//    }
//    else
//    {
//    if(document.getElementById("txtPrjLaunchPlannedDate").value!="")
//        compareDate5=CheckDateFormat("txtPrjLaunchPlannedDate", invalidPrjLaunchPlannedDate)
//    else
//        SetErrorColor("txtPrjLaunchPlannedDate", true); 
//    }   
//        
//    
//       
//   if(document.getElementById("manDevelpALanuchDate").innerHTML=="*")
//    {
//    if(ValidateRequired("txtPrjActualLaunchDate", missingPrjActualLaunchDate))
//        if( CheckDateFormat("txtPrjActualLaunchDate", invalidPrjActualLaunchDate))
//            ValidateSystemDate("txtPrjActualLaunchDate", invalidSysPrjActualLaunchDate)
//    }
//    else
//    {
//    if(document.getElementById("txtPrjActualLaunchDate").value!="")
//        if( CheckDateFormat("txtPrjActualLaunchDate", invalidPrjActualLaunchDate))
//            ValidateSystemDate("txtPrjActualLaunchDate", invalidSysPrjActualLaunchDate)
//    else
//        SetErrorColor("txtPrjActualLaunchDate", true); 
//    }   
//   
//    
//       
//    if(document.getElementById("manDevelpClosedDate").innerHTML=="*")
//    {
//    if(ValidateRequired("txtPrjClosedDate", missingPrjClosedDate))
//        compareDate6=CheckDateFormat("txtPrjClosedDate", invalidPrjClosedDate)
//    }
//    else
//    {
//    if(document.getElementById("txtPrjClosedDate").value!="")
//        compareDate6=CheckDateFormat("txtPrjClosedDate", invalidPrjClosedDate)
//    else
//        SetErrorColor("txtPrjClosedDate", true); 
//    }

//   //Compare Lanuch date and Closed Date
//   if(compareDate5 && compareDate6)
//        ValidatetwoDates("txtPrjLaunchPlannedDate","txtPrjClosedDate",greaterClosedDate)
//              
//   CheckMaxlength("txtPrjAdditionalAddress",300,greaterAdditionalAddress)
//   
//              
//    if(firstErrorControl !="" )
//    {        
//        SetControlFocus(firstErrorControl);
//        errMsg = "<table>" + errMsg + "</table>";
//        document.getElementById("divErrorMessage").innerHTML = errMsg; 
//        document.getElementById("lblConfirmMsg").innerHTML = ""; 
//        
//        
//        return false;
//    }
//    else
//    {    
//      return confirm ('Are you sure to submit?')   

//    }
//}


////Ajax post back events handler ofr Master Link Doc Screen buttons
function landOwnerButtonControls(ControlName) {
    if (ControlName == 'btnSubmit') {

        if (ValidateplaControls()) {
            //document.getElementById('hdnSelectedVendor').value=document.getElementById('ddlVendorName').value;
            document.getElementById('hdnSelectedVendorId').value = document.getElementById('ddlVendorName').value;
            document.getElementById('ddlVendorName').disabled = true;
            return true;
        } else { return false; }

    }
}
//checks the control in project_landowner_add
function ValidateplaControls() {
    document.getElementById("lblConfirmMsg").innerHTML = "";
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown("ddlVendorName", missingddlVendorName)

    ValidateDropDown("ddlPrimaryVendor", missingddlParentVendor)

    if (ValidateRequired("txtLandDescription", missingLandDescription))
        CheckMaxlength("txtLandDescription", 2048, greaterLandDescription)


    //    if(ValidateRequired("txtSurveyNumber", missingSurveyNumber))
    //        //if(ValidateFirstNumber("txtSurveyNumber", invalidSurveyNumber))
    //            ValidateSpecialChars("txtSurveyNumber", invalidSurveyNumber, "title")
    //    
    //    
    //    if(ValidateSpecialChars("txtSubDivisionNo", invalidSubDivisionNo, "title"))
    //       ValidateRequired("txtSubDivisionNo", missingSubDivisionNo)

    ValidateRequired("txtSurveyNoDetails", missingSurveyNoDetails)

    ValidateSpecialChars("txtSROOffice", invalidSROOffice, "title")

    if (document.getElementById("txtDROOffice").value != "")
        if (ValidateSpecialChars("txtDROOffice", invalidDROOffice, "title"))
            ValidateRequired("txtDROOffice", missingDROOffice)

    if (ValidateSpecialChars("txtPurchaserNames", invalidPurchaserNames, "title"))
        ValidateRequired("txtPurchaserNames", missingPurchaserNames)

    if (ValidateSpecialChars("txtSellerName", invalidSellerName, "title"))
        ValidateRequired("txtSellerName", missingSellerName)

    ValidateDropDown("ddlExtentUOM", missingddlExtentUOM)

    if (ValidateRequired("txtTotalExtent", missingTotalExtent))
        ValidateDecimal("txtTotalExtent", invalidTotalExtent)

    if (ValidateRequired("txtScheduleDescription", missingScheduleDescription))
        CheckMaxlength("txtScheduleDescription", 2048, greaterScheduleDescription)

    if (ValidateRequired("txtBoundaryDescription", missingBoundaryDescription))
        CheckMaxlength("txtBoundaryDescription", 2048, greaterBoundaryDescription)

    if (ValidateRequired("txtLinearDimension", missingLinearDimension))
        CheckMaxlength("txtLinearDimension", 2048, greaterLinearDimension)

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        document.getElementById("lblConfirmMsg").innerHTML = "";

        return false;
    }
    else {
        return confirm('Are you sure to submit?')

    }
}


//checks the control in project_issues_add

////Ajax post back events handler ofr Master Link Doc Screen buttons
function masterLinkDocButtonControls(ControlName) {
    if (ControlName == 'btnSubmit') {
        if (ValidatelmdControls()) {
            document.getElementById('hdnDocType').value = document.getElementById('ddlDocType').value;
            document.getElementById('ddlDocType').disabled = true;
            return true;
        } else { return false; }

    }
}

//checks the control in link_master_docs
function ValidatelmdControls(clickValue) {
    firstErrorControl = "";
    errMsg = "";

    ValidateDropDown("ddlPrimaryDoc", missddlPrimaryDoc)

    ValidateDropDown("ddlParentSuppDoc", missddlParentSuppDoc)

    ValidateDropDown("ddlDocType", missddlDocType)

    if (ValidateRequired("txtDocTitle", missDocTitle))
        ValidateSpecialChars("txtDocTitle", invalDocTitle, "title")


    if (document.getElementById("Span5").innerHTML == "*") {
        if (ValidateRequired("txtLandDescription", missLandDescription))
            CheckMaxlength("txtLandDescription", 2048, greaterLandDescription)
    }
    else
        SetErrorColor("txtLandDescription", true);

    if (document.getElementById("Span3").innerHTML == "*") {
        if (ValidateRequired("txtScheduleDescription", missScheduleDescription))
            CheckMaxlength("txtScheduleDescription", 2048, greaterScheduleDescription)
    }
    else
        SetErrorColor("txtScheduleDescription", true);

    if (document.getElementById("Span4").innerHTML == "*") {
        if (ValidateRequired("txtBoundaryDescription", missBoundaryDescription))
            CheckMaxlength("txtBoundaryDescription", 2048, greaterBoundaryDescription)
    }
    else
        SetErrorColor("txtBoundaryDescription", true);

    if (document.getElementById("Span11").innerHTML == "*") {
        if (ValidateRequired("txtLinearDimension", missLinearDimension))
            CheckMaxlength("txtLinearDimension", 2048, greaterLinearDimension)
    }
    else
        SetErrorColor("txtLinearDimension", true);

    //    if(document.getElementById('txtSurveyNumber').value != "")        
    //         ValidateSpecialChars("txtSurveyNumber",invalSurveyNumber,"title")
    //    
    //    if(document.getElementById('txtSubDivisionNo').value != "")               
    //        ValidateSpecialChars("txtSubDivisionNo",invalSubDivisionNo,"title")


    if (document.getElementById("Span13").innerHTML == "*") {
        if (ValidateRequired("txtDocNo", missDocNo))
            ValidateSpecialChars("txtDocNo", invalDocNo, "title")
    }
    else
        SetErrorColor("txtDocNo", true);

    if (document.getElementById("Span16").innerHTML == "*") {
        if (ValidateRequired("txtDocDate", missDocDate))
            CheckDateFormat("txtDocDate", invalDocDate)
    }
    else
        SetErrorColor("txtDocDate", true);

    if (document.getElementById("Span20").innerHTML == "*") {
        if (ValidateRequired("txtSROOffice", missSROOffice))
            ValidateSpecialChars("txtSROOffice", invalSROOffice, "title")
    }
    else
        SetErrorColor("txtSROOffice", true);

    if (document.getElementById("Span14").innerHTML == "*") {
        if (ValidateRequired("txtDROOffice", missDROOffice))
            ValidateSpecialChars("txtDROOffice", invalDROOffice, "title")
    }
    else
        SetErrorColor("txtDROOffice", true);

    if (document.getElementById("Span9").innerHTML == "*") {
        if (ValidateRequired("txtPurchaserNames", missPurchaserNames))
            ValidateSpecialChars("txtPurchaserNames", invalPurchaserNames, "title")
    }
    else
        SetErrorColor("txtPurchaserNames", true);

    if (document.getElementById("Span10").innerHTML == "*") {
        if (ValidateRequired("txtSellerName", missSellerName))
            ValidateSpecialChars("txtSellerName", invalSellerName, "title")
    }
    else
        SetErrorColor("txtSellerName", true);

    if (document.getElementById("Span1").innerHTML == "*")
        ValidateDropDown("ddlExtentUOM", missddlExtentUOM)
    else
        SetErrorColor("ddlExtentUOM", true);

    if (document.getElementById("Span2").innerHTML == "*") {
        if (ValidateRequired("txtTotalExtent", missTotalExtent))
            ValidateDecimal("txtTotalExtent", invalTotalExtent)
    }
    else
        SetErrorColor("txtTotalExtent", true);

    ValidateDropDown("ddlLinkParentRef", missLinkParentRef)

    if (ValidateRequired("txtSeqno", missSeqno))
        ValidateNumbers("txtSeqno", invalSeqno)

    if (document.getElementById("fileUpload_Man").innerHTML != "")
        ValidateRequired("FileUpload1", missFileUpload1)


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;

        return false;
    }
    else {
        return confirm('Are you sure to submit?');
    }
}


// Checks the control in project_plot_details
function ValidateppdsaveControls(clickValue) {
    firstErrorControl = "";
    errMsg = "";


    if (ValidateRequired("txtPlotNoStartVal", missingPlotNoStartVal))
        ValidateNumbers("txtPlotNoStartVal", invalidPlotNoStartVal)
    //Validatetotplot("txtPlotNoStartVal",invalidPlotStartRange)

    if (ValidateRequired("txtPlotNoEndVal", missingPlotNoEndVal))
        ValidateNumbers("txtPlotNoEndVal", invalidPlotNoEndVal)
    //Validatetotplot("txtPlotNoEndVal",invalidPlotEndRange)


    //ValidateSpecialChars("txtPlotDimension",invalidPlotDimension,"all");

    RangeComparision("txtPlotNoStartVal", "txtPlotNoEndVal", invalidPlotRange);

    if (document.getElementById("txtPlotSize").disabled != true) {
        if (ValidateRequired("txtPlotSize", missingPlotSize)) {
            ValidateNumbers("txtPlotSize", invalidPlotSize);
        }
    }

    if (document.getElementById("ddlDefaultPlotType").disabled != true)
        ValidateDropDown("ddlDefaultPlotType", selectDefaultPlotType)


    var checkErrorCode = new Array(3);

    checkErrorCode[0] = ValidateCheckBox("chkPlotType", selectDefaultValues);

    checkErrorCode[1] = ValidateCheckBox("chkPlotSize", selectDefaultValues);

    checkErrorCode[2] = ValidateCheckBox("chkPlotDimension", selectDefaultValues);

    var i = 0;
    var errorVal = false;
    for (i = 0; i < checkErrorCode.length; i++) {
        if (checkErrorCode[i]) {
            errorVal = true;
            break;
        }
    }

    if (!errorVal) {
        if (firstErrorControl == '')
            firstErrorControl = "chkPlotType";

        errMsg += GetErrorRow("chkPlotType", selectDefaultValues);
        SetErrorColor("chkPlotType", false);
        SetErrorColor("chkPlotSize", false);
        SetErrorColor("chkPlotDimension", false);
    }
    else {
        SetErrorColor("chkPlotType", true);
        SetErrorColor("chkPlotSize", true);
        SetErrorColor("chkPlotDimension", true);
    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;

        return false;
    }
    else {
        return confirm('Are you sure to save?')
    }
}


function ValidateppdgoControls(clickvalue) {
    firstErrorControl = "";
    errMsg = "";

    ValidateNumbers("txtPlotNoStartVal", invalidPlotNoStartVal);
    //Validatetotplot("txtPlotNoStartVal",invalidPlotStartRange);

    ValidateNumbers("txtPlotNoEndVal", invalidPlotNoEndVal);
    //Validatetotplot("txtPlotNoEndVal",invalidPlotEndRange);

    RangeComparision("txtPlotNoStartVal", "txtPlotNoEndVal", invalidPlotRange);

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;

        return false;
    }
    else {
        return true;
    }
}

//function to validate pricing information
function ValidatePriceControls() {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtMarketAmountSpend", missingMarketAmtSpend);

    ValidateRequired("txtIncentive", missingIncentive);

    ValidateRequired("txtIncidentalExpenses", missingIncidentalExp);

    ValidateRequired("txtOtherExpenses1", missingOtherExp1);

    ValidateRequired("txtOtherExpenses2", missingOtherExp2);

    ValidateRequired("txtOtherExpenses3", missingOtherExp3);

    ValidateRequired("txtProfit", missingProfit);

    if (ValidateRequired("txtRemarks", missingRemarks))
        CheckMaxlength("txtRemarks", 2048, greaterRemarks)


    ValidateRequired("txtSalePriceRangeFrom", missingSalePriceRangeFrom);

    ValidateRequired("txtSalePriceRangeTo", missingSalePriceRangeTo);

    ValidateRequired("txtRegChargeFrom", missingRegdChargeFrom);

    ValidateRequired("txtRegChargeTo", missingRegdChargeTo);

    ValidateRequired("txtPattaOtherExp", missingPattaOtherExp);

    ValidateRequired("txtLandOwnerPayment", missingLandOwnerPay);

    ValidateRequired("txtGuidelineValue", missingGuideLnValue);

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;

        return false;
    }
    else {
        return true;
    }
}

//Link Plot Document for GO btn
function ValidatedoclinkgoControls() {
    firstErrorControl = "";
    errMsg = "";

    ValidateNumbers("txtPlotNoStartVal", invalidPlotNoStartVal);
    //Validatetotplot("txtPlotNoStartVal",invalidPlotStartRange);

    ValidateNumbers("txtPlotNoEndVal", invalidPlotNoEndVal);
    //Validatetotplot("txtPlotNoEndVal",invalidPlotEndRange);

    RangeComparision("txtPlotNoStartVal", "txtPlotNoEndVal", invalidPlotRange);

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;

        return false;
    }
    else {
        return true;
    }
}

//Link Plot Document for SAVE btn
function ValidatedoclinksaveControls() {
    firstErrorControl = "";
    errMsg = "";


    if (ValidateRequired("txtPlotNoStartVal", missingPlotNoStartVal))
        ValidateNumbers("txtPlotNoStartVal", invalidPlotNoStartVal)

    if (ValidateRequired("txtPlotNoEndVal", missingPlotNoEndVal))
        ValidateNumbers("txtPlotNoEndVal", invalidPlotNoEndVal)


    RangeComparision("txtPlotNoStartVal", "txtPlotNoEndVal", invalidPlotRange);



    //   if(document.getElementById("listParentDoc").disabled != true)
    //   {
    //   if(document.getElementById("listParentDoc").items(1).selected ==  true)
    //   alert(document.getElementById("listParentDoc").items(0).selected);
    //   }   
    if (document.getElementById("txtSigningAuth").disabled != true) {
        if (ValidateRequired("txtSigningAuth", missingSigningAuth))
            CheckMaxlength("txtSigningAuth", 500, greaterSigningAuth)
    }

    var checkErrorCode = new Array(3);

    checkErrorCode[0] = ValidateCheckBox("chkParentDoc", selectDefaultValues);
    checkErrorCode[1] = ValidateCheckBox("chkSigningAuth", selectDefaultValues);
    if (checkErrorCode[0]) {
        var e = document.getElementById('listParentDoc');
        var listVal = false;
        for (i = 0; i < e.length; i++) {
            if (e.options[i].selected)
                listVal = true;
            break;
        }
        if (!listVal) {
            if (firstErrorControl == '')
                firstErrorControl = "listParentDoc";

            errMsg += GetErrorRow("listParentDoc", selectList);
            SetErrorColor("listParentDoc", false);


        }

    }
    else {
        SetErrorColor("listParentDoc", true);
    }



    var i = 0;
    var errorVal = false;
    for (i = 0; i < checkErrorCode.length; i++) {
        if (checkErrorCode[i]) {
            errorVal = true;
            break;
        }
    }

    if (!errorVal) {
        if (firstErrorControl == '')
            firstErrorControl = "chkParentDoc";

        errMsg += GetErrorRow("chkParentDoc", selectDefaultValues);
        SetErrorColor("chkParentDoc", false);
        SetErrorColor("chkSigningAuth", false);

    }
    else {
        SetErrorColor("chkParentDoc", true);
        SetErrorColor("chkParentDoc", true);

    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;

        return false;
    }
    else {
        return confirm('Are you sure to save?')
    }
}

//CheckBox
function ValidateCheckBox(controlName, errorMessage) {
    var checkbx = document.getElementById(controlName);

    if (checkbx.checked) {
        return true;
    }
    else {
        return false;
    }
}

// Comapres the Plot Range
function RangeComparision(controlName1, controlName2, errorMessage) {
    if (document.getElementById(controlName1).value != "" && document.getElementById(controlName2).value != "") {
        var plotFrom = document.getElementById(controlName1).value;
        var plotTo = document.getElementById(controlName2).value;
    }
    else if (document.getElementById(controlName1).value == "" && document.getElementById(controlName2).value != "") {
        var controlID = controlName1;
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }
    else if (document.getElementById(controlName1).value != "" && document.getElementById(controlName2).value == "") {
        var controlID = controlName2;
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }


    if (parseInt(plotFrom) > parseInt(plotTo)) {
        var controlID = controlName2;
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

//checks the plot value with totplot
function Validatetotplot(controlName, errorMessage) {
    var plotTotal = document.getElementById("totplot").value
    var plotRange = document.getElementById(controlName).value
    if (parseInt(plotTotal) < parseInt(plotRange)) {
        var controlID = controlName;
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



//Special characters validation
function ValidateSpecialChars(controlName, errorMessage, condition) {
    var nonValidCharsRegEx = "`'~!@#$%^\&*()-_+{\"}|:<>?/=[];,.\\";
    var nonValidCityCharsRegEx = "'~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\";
    var nonValidUserIdRegEx = "'~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\"; // Included by Sam for Alpha Numeric test only
    var nonValidNameRegEx = "~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\1234567890"; // For using quote in name
    var nonValidCompanyRegEx = "~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\"; // For using quote in name
    var nonValidEmailCharsRegEx = "`'~!#$%^\&*()+-{\"}|:<>?/=[];,\\";
    var nonValidOthersCharsRegEx = "`'~!@#$%^\&*()_+{\"}|:<>?/=[];\\1234567890";
    var nonValidShipperRegEx = "~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\1234567890"; // For using quote in name     
    var nonValidTitleRegEx = "`~!@#$%^.<>:?;,";
    var nonValidplotnodisRegEx = "`'~!@#$%^&*+{\"}|:<>?/=;,.\\";
    var controlID = controlName;
    var charValue = document.getElementById(controlID).value;
    var charRegEx = "";
    var finalTxtVal = "";

    switch (condition) {
        case "all": charRegEx = nonValidCharsRegEx;
            break;
        case "dot": charRegEx = nonValidCityCharsRegEx;
            break;
        case "userid": charRegEx = nonValidUserIdRegEx;
            break;
        case "name": charRegEx = nonValidNameRegEx;
            break;
        case "company": charRegEx = nonValidCompanyRegEx;
            break;
        case "email": charRegEx = nonValidEmailCharsRegEx;
            break;
        case "others": charRegEx = nonValidOthersCharsRegEx;
            break;
        case "shipper": charRegEx = nonValidShipperRegEx;
            break;
        case "plotnodis": charRegEx = nonValidplotnodisRegEx;
            break;
        case "title": charRegEx = nonValidTitleRegEx;
            break;

    }

    for (i = 0; i < charValue.length; i++) {
        Char = charValue.charAt(i);
        if (!(charRegEx.indexOf(Char) == -1)) {
            //if(firstErrorControl == '')
            firstErrorControl = controlID;
            errMsg += GetErrorRow(controlID, errorMessage);
            SetErrorColor(controlID, false);
            return false;
        }
        else {
            SetErrorColor(controlID, true);
        }
    }
    return true;
}

// Required Field validation
function ValidateRequired(controlName, errorMessage) {


    var errorCode = true;
    var controlID = controlName;
    var controlObject = document.getElementById(controlID);

    errorCode = IsEmpty(controlObject);
    if (!errorCode) {
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

function CheckMaxlength(controlName, maxLengthValue, errorMessage) {


    var controlID = controlName;
    var controlObject = document.getElementById(controlID).value;

    if (controlObject.length > maxLengthValue) {
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


// Dropdown validation
function ValidateDropDown(controlName, errorMessage) {
    var errorCode = true;
    var controlID = controlName;
    var dropdown = document.getElementById(controlID);
    var selectedIndex = dropdown.selectedIndex;
    if (selectedIndex == 0) {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
    }
    else
        SetErrorColor(controlID, true);
}

//Validate System Date
function ValidateSystemDate(controlName, errorMessage) {
    var date = document.getElementById(controlName).value;
    var controlID = controlName;

    var checkCurrentdateObj = new Date();

    var y = checkCurrentdateObj.getYear() + "";
    var M = checkCurrentdateObj.getMonth() + 1;
    var d = checkCurrentdateObj.getDate();

    var currDate = "";

    //	if(parseInt(M) <= 9)
    //	    currDate = d + "/" + "0" + M + "/" + y;

    if (parseInt(d) <= 9) {
        if (parseInt(M) <= 9) {
            currDate = "0" + d + "/" + "0" + M + "/" + y;

        } else {
            currDate = "0" + d + "/" + M + "/" + y;
        }
    }
    else {
        if (parseInt(M) <= 9) {
            currDate = d + "/" + "0" + M + "/" + y;

        } else {
            currDate = d + "/" + M + "/" + y;
        }
    }

    if (compareDates(date, "dd/MM/yyyy", currDate, "dd/MM/yyyy") == 1) // Call compareDates function in date.js file
    {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }
    else {
        SetErrorColor(controlID, true);
        //errMsg = "";
        //document.getElementById("divErrorMessage").innerHTML += errMsg; 
        return true;
    }
}

//Validate two Date
function ValidatetwoDates(controlName, controlName1, errorMessage) {
    var date = document.getElementById(controlName).value;
    var controlID = controlName;
    var fromDate = "";
    var toDate = "";

    var datefrom = date.split("/");
    var y = datefrom[2];
    var M = datefrom[1];
    var d = datefrom[0];
    fromDate = d + "/" + M + "/" + y;

    var date1 = document.getElementById(controlName1).innerHTML;
    var controlID1 = controlName1;

    var dateto = date1.split("/");
    var y = dateto[2];
    var M = dateto[1];
    var d = dateto[0];
    toDate = d + "/" + M + "/" + y;

    if (compareDates(fromDate, "dd/MM/yyyy", toDate, "dd/MM/yyyy") == 1) // Call compareDates function in date.js file
    {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID1;
        errMsg += GetErrorRow(controlID1, errorMessage);
        SetErrorColor(controlID1, false);
        return false;
    }
    else {
        SetErrorColor(controlID1, true);
        return true;
    }
}
// Checks the date format
function CheckDateFormat(controlName, errorMessage) {
    var date = document.getElementById(controlName).value;

    var controlID = controlName;
    var checkDate = isDateValid(date);

    if (checkDate != "invalidYear") {
        if (!checkDate) {
            //if(firstErrorControl == '')
            firstErrorControl = controlID;
            errMsg += GetErrorRow(controlID, errorMessage);
            SetErrorColor(controlID, false);
            return false;
        }
        else {
            SetErrorColor(controlID, true);
            return true;
        }
    }
    else {
        //if(firstErrorControl == '')
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, invalidYear);
        SetErrorColor(controlID, false);
        return false;
    }
}


// Checks if the input is a Valid date
function isDateValid(dtStr) {
    var daysInMonth = new Array("31", "29", "31", "30", "31", "30", "31", "31", "30", "31", "30", "31");
    var dtCh = "/";

    var pos1 = dtStr.indexOf(dtCh);
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1);

    var strDay = dtStr.substring(0, pos1);
    var strMonth = dtStr.substring(pos1 + 1, pos2);
    var strYear = dtStr.substring(pos2 + 1);

    strYr = strYear;

    if (strDay.charAt(0) == "0" && strDay.length > 1)
        strDay = strDay.substring(1);

    if (strMonth.charAt(0) == "0" && strMonth.length > 1)
        strMonth = strMonth.substring(1);

    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1)
            strYr = strYr.substring(1);
    }
    month = parseInt(strMonth);
    day = parseInt(strDay);
    year = parseInt(strYr);
    if (pos1 == -1 || pos2 == -1) {
        return false;
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        return false;
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month - 1]) {
        return false;
    }
    if (strYear.length != 4 || year == 0) {
        if ((parseInt(strYear) < 1900) || (parseInt(strYear) > 2100))
            return "invalidYear";
            //	    else if(parseInt(strYear) > 2100)
            //	        return "invalidYear";
        else

            return false;
    }
    else {
        if ((parseInt(strYear) < 1900) || (parseInt(strYear) > 2100))
            return "invalidYear";
        //	    else if(parseInt(strYear) > 2100)
        //	        return "invalidYear";
    }

    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || !isInteger(stripCharsInBag(dtStr, dtCh))) {
        return false;
    }


    return true;
}


// Gets the number of days in february
function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}

//area comparision
function areaComparision(controlName1, controlName2, errorMessage) {
    FormatCommaSeparator(false, controlName1);
    FormatCommaSeparator(false, controlName2);
    //var firstErrorControl1 = "";
    //errMsg="";
    var value1 = document.getElementById(controlName1).value;
    var value2 = document.getElementById(controlName2).value;


    var controlID = controlName2;
    if (parseInt(value1) < parseInt(value2)) {
        // if(firstErrorControl1 == '')

        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        FormatCommaSeparator(true, controlName1);
        FormatCommaSeparator(true, controlName2);
        return false;
    }
    else {
        SetErrorColor(controlID, true);
        FormatCommaSeparator(true, controlName1);
        FormatCommaSeparator(true, controlName2);
        return true;
    }
    //}
}


//function for comparing todates
function DateComparision(controlName1, controlName2, errorMessage) {
    var date1 = document.getElementById(controlName1).value;
    var date2 = document.getElementById(controlName2).value;
    var controlID = controlName2;
    var dateObj1 = new Date(date1, "dd/MM/yyyy");
    var dateObj2 = new Date(date2);

    var year1 = dateObj1.getYear();
    var year2 = dateObj2.getYear();

    if (year1 <= year2) {
        var month1 = dateObj1.getMonth();
        var month2 = dateObj2.getMonth();
        if (month1 < month2) {
            SetErrorColor(controlID, true);
            return true;
        }
        else if (month1 == month2) {
            var dateval1 = dateObj1.getDate();
            var dateval2 = dateObj2.getDate();
            if (dateval1 < dateval2) {
                SetErrorColor(controlID, true);
                return true;
            }
            else {
                firstErrorControl = controlID;
                errMsg += GetErrorRow(controlID, errorMessage);
                SetErrorColor(controlID, false);
            }
        }
        else {
            firstErrorControl = controlID;
            errMsg += GetErrorRow(controlID, errorMessage);
            SetErrorColor(controlID, false);
        }
    }
    else {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
    }


}



// Checks if the passed in value is an integer
function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9")))
            return false;
    }
    // All characters are numbers.
    return true;
}


// Strips a specified character in a string
function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1)
            returnString += c;
    }
    return returnString;
}



// Sets the Error Color for the specified control
function SetErrorColor(controlID, isCss) {
    var controlToSet = document.getElementById(controlID);
    if (controlToSet)
        if (isCss)
            //controlToSet.className = 'validclass';
            controlToSet.style.backgroundColor = "";
        else
            controlToSet.style.backgroundColor = "yellow";
    //controlToSet.className = 'errorclass';        
}



// Sets focus on the specified control id
function SetControlFocus(controlID) {
    var controlToFocus = document.getElementById(controlID);
    if (controlToFocus)
        controlToFocus.focus();
}

// Checks if the control has got any value
function IsEmpty(controlID) {
    var textVal = controlID.value;
    var count = 0;

    for (i = 0; i <= textVal.length - 1; i++) {
        var textValValid = textVal.charAt(i);

        if (textValValid == " ")
            count += 1;
    }

    if (count == textVal.length)
        return false;
    else
        return true;
}



// Decimal validation
function ValidateDecimal(controlName, errorMessage) {
    var errorCode = true;
    var controlID = controlName;
    errorCode = IsDecimal(controlID);
    if (!errorCode) {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
    }
    else
        SetErrorColor(controlID, true);
}


//checks whether the control with the given id has a valid decimal
function IsDecimal(control) {
    var controlObject = document.getElementById(control);
    var isNumber = twodecRegEx.test(controlObject.value);
    if (!isNumber)
        return false;
    else
        return true;
}





// Decimal validation for markup
function ValidateMarkDecimal(controlName, errorMessage) {
    var errorCode = true;
    var controlID = controlName;
    errorCode = MDecimal(controlID);
    if (!errorCode) {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
    }
    else
        SetErrorColor(controlID, true);
}


//checks whether the control with the given id has a valid decimal
function MDecimal(control) {
    var controlObject = document.getElementById(control);
    var isNumber = markRegEX.test(controlObject.value);
    if (!isNumber)
        return false;
    else
        return true;
}




// Numeric validation(RegEx)
function ValidateNumbers(controlName, errorMessage) {
    var errorCode = true;
    var controlID = controlName;
    errorCode = IsNumber(controlID);
    if (!errorCode) {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
    }
    else
        SetErrorColor(controlID, true);
}


//checks whether the control with the given id is numeric
function IsNumber(control) {
    var controlObject = document.getElementById(control);
    var isNumber = numRegEx.test(controlObject.value);
    if (!isNumber)
        return false;
    else
        return true;
}
// Gets the Error Row in HTML format
function GetErrorRow(controlID, errorMessage) {
    return "<tr align='left'><td onmouseover=\"this.style.cursor='hand'\" onclick=\"SetControlFocus('" + controlID + "')\" class='errormsg'>" + errorMessage + "</td></tr>";
}


//Calculate Total Project Outlay in Project Master Creation page
function CalculateTotalOutLay() {

    var landAcqCost = 0;
    var developmentCost = 0;
    var markupPercent = 0;
    if (document.getElementById("txtPrjAcquistionCost").value != "")
        landAcqCost = document.getElementById("txtPrjAcquistionCost").value;

    if (document.getElementById("txtPrjDevelopmentCost").value != "")
        developmentCost = document.getElementById("txtPrjDevelopmentCost").value;

    if (document.getElementById("txtPrjMarkupPercentage").value != "")
        markupPercent = document.getElementById("txtPrjMarkupPercentage").value;

    var tot = 0;
    var tempIntegralPart = 0;
    var digitCount = developmentCost.length;
    for (var i = 0; i < digitCount; i++) {
        if ((developmentCost.charAt(i)) != ",") {
            if (i == 0) {
                tempIntegralPart = developmentCost.charAt(i);
            } else {
                tempIntegralPart = tempIntegralPart + developmentCost.charAt(i);
            }
        }
    }
    developmentCost = tempIntegralPart;

    digitCount = landAcqCost.length;
    tempIntegralPart = 0;
    for (var i = 0; i < digitCount; i++) {
        if ((landAcqCost.charAt(i)) != ",") {
            if (i == 0) {
                tempIntegralPart = landAcqCost.charAt(i);
            } else {
                tempIntegralPart = tempIntegralPart + landAcqCost.charAt(i);
            }
        }
    }
    landAcqCost = tempIntegralPart;


    digitCount = markupPercent.length;
    tempIntegralPart = 0;
    for (var i = 0; i < digitCount; i++) {
        if ((markupPercent.charAt(i)) != ",") {
            if (i == 0) {
                tempIntegralPart = markupPercent.charAt(i);
            } else {
                tempIntegralPart = tempIntegralPart + markupPercent.charAt(i);
            }
        }
    }
    markupPercent = tempIntegralPart;

    //alert("L " + landAcqCost + " A "+developmentCost+" P "+markupPercent);
    tot = (parseInt(landAcqCost) + parseInt(developmentCost)) + (((parseInt(landAcqCost) + parseInt(developmentCost)) * parseFloat(markupPercent)) / 100)
    tot = Math.round(tot * Math.pow(10, 2)) / Math.pow(10, 2);
    tot = Math.round(tot);
    document.getElementById("lblPrjTotProjectOutlay").innerHTML = tot;

}


//change condn for Approved Layout=="No"

function NoControls(str) {
    var str1 = str;
    document.getElementById("lblGroup11").innerHTML = "";
    document.getElementById("lblGroup12").innerHTML = "";
    document.getElementById("lblGroup13").innerHTML = "";

    if (str1 == 'Yes') {
        document.getElementById("lblGroup11").innerHTML = "*";
        document.getElementById("lblGroup12").innerHTML = "*";
        document.getElementById("lblGroup13").innerHTML = "*";

    } else if (str1 == 'No') {
        document.getElementById("lblGroup11").innerHTML = "";
        document.getElementById("lblGroup12").innerHTML = "";
        document.getElementById("lblGroup13").innerHTML = "";
    }


}



//mandatory for parent document
function yesParent() {

    var str1 = document.getElementById("ddlParentSuppDoc").value;
    var str2 = document.getElementById("ddlPrimaryDoc").value;


    if ((str1 == 'Parent') && (str2 == 'Yes')) {
        document.getElementById("Span5").innerHTML = "*";
        document.getElementById("Span3").innerHTML = "*";
        document.getElementById("Span4").innerHTML = "*";
        document.getElementById("Span11").innerHTML = "*";
        document.getElementById("Span13").innerHTML = "*";
        document.getElementById("Span16").innerHTML = "*";
        document.getElementById("Span20").innerHTML = "*";
        document.getElementById("Span14").innerHTML = "*";
        document.getElementById("Span9").innerHTML = "*";
        document.getElementById("Span10").innerHTML = "*";
        document.getElementById("Span1").innerHTML = "*";
        document.getElementById("Span2").innerHTML = "*";
    }
    else {
        document.getElementById("Span5").innerHTML = "";
        document.getElementById("Span3").innerHTML = "";
        document.getElementById("Span4").innerHTML = "";
        document.getElementById("Span11").innerHTML = "";
        document.getElementById("Span13").innerHTML = "";
        document.getElementById("Span16").innerHTML = "";
        document.getElementById("Span20").innerHTML = "";
        document.getElementById("Span14").innerHTML = "";
        document.getElementById("Span9").innerHTML = "";
        document.getElementById("Span10").innerHTML = "";
        document.getElementById("Span1").innerHTML = "";
        document.getElementById("Span2").innerHTML = "";
    }

}

//Project Status some controls visible false or true
function ConditionsControls(str) {
    var str1 = str;

    document.getElementById("manDevelpPlanSDate").innerHTML = "";
    document.getElementById("manDevelpPlanCompDate").innerHTML = "";

    document.getElementById("manDevelpPlanADate").innerHTML = "";
    document.getElementById("manDevelpPlanACompDate").innerHTML = "";

    document.getElementById("manDevelpPlanLanuchDate").innerHTML = "";
    document.getElementById("manDevelpALanuchDate").innerHTML = "";

    document.getElementById("manDevelpClosedDate").innerHTML = "";


    if (str1 == 'DEVELOPMENT') {
        document.getElementById("manDevelpPlanSDate").innerHTML = "*";
        document.getElementById("manDevelpPlanCompDate").innerHTML = "*";

    } else if (str1 == 'MARKET') {
        document.getElementById("manDevelpPlanSDate").innerHTML = "*";
        document.getElementById("manDevelpPlanCompDate").innerHTML = "*";
        document.getElementById("manDevelpPlanADate").innerHTML = "*";
        document.getElementById("manDevelpPlanACompDate").innerHTML = "*";

    } else if (str1 == 'LAUNCHED') {
        document.getElementById("manDevelpPlanLanuchDate").innerHTML = "*";
        document.getElementById("manDevelpALanuchDate").innerHTML = "*";

    } else if (str1 == 'CLOSED') {
        document.getElementById("manDevelpClosedDate").innerHTML = "*";

    } else if (str1 == '0') {
        document.getElementById("manDevelpPlanSDate").innerHTML = "*";
        document.getElementById("manDevelpPlanCompDate").innerHTML = "*";
        document.getElementById("manDevelpPlanADate").innerHTML = "*";
        document.getElementById("manDevelpPlanACompDate").innerHTML = "*";
        document.getElementById("manDevelpPlanLanuchDate").innerHTML = "*";
        document.getElementById("manDevelpALanuchDate").innerHTML = "*";
        document.getElementById("manDevelpClosedDate").innerHTML = "*";
    }

}

function KeyPressNumeric() {
    if (!(event.keyCode >= 48 && event.keyCode <= 57)) {
        event.keyCode = 0;
    }
}

function KeyPressDecimal() {
    if (!(event.keyCode >= 48 && event.keyCode <= 57) && (event.keyCode != 46)) {
        event.keyCode = 0;
    }
}

function KeyPressDate() {
    if (!(event.keyCode >= 48 && event.keyCode <= 57) && (event.keyCode != 47)) {
        event.keyCode = 0;
    }
}



//validation in grid for Project_Budgeting

function fnValidateForGridBudget() {

    var theGridView = document.getElementById("gridChkList");

    firstErrorControl = "";
    errMsg = "";
    var cellsNo = 0;
    var compareDate1 = false;
    var compareDate2 = false;

    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {

        if (theGridView.rows(rowCount).cells.length == 11) {
            if (theGridView.rows(rowCount).cells(4).children(0).value != null) {
                if (ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missingStartDate))
                    compareDate1 = CheckDateFormat(theGridView.rows(rowCount).cells(4).children(0).id, invalidStartDate)

                if (ValidateRequired(theGridView.rows(rowCount).cells(5).children(0).id, missingCompletionDate))
                    compareDate2 = CheckDateFormat(theGridView.rows(rowCount).cells(5).children(0).id, invalidCompletionDate)

                if (compareDate1 && compareDate2)
                    DateComparision(theGridView.rows(rowCount).cells(4).children(0).id, theGridView.rows(rowCount).cells(5).children(0).id, compareDate);

                if (ValidateRequired(theGridView.rows(rowCount).cells(6).children(0).id, missingBudgetCostPer))
                    ValidateNumbers(theGridView.rows(rowCount).cells(6).children(0).id, invalidBudgetCostPer)

                //if(ValidateRequired(theGridView.rows(rowCount).cells(7).children(0).id, missingBudgetCost))
                //ValidateNumbers(theGridView.rows(rowCount).cells(7).children(0).id, invalidBudgetCost)
                theGridView.rows(rowCount).cells(7).children(0).disabled = false;
                ValidateRequired(theGridView.rows(rowCount).cells(7).children(0).id, missingBudgetCost)

                if (theGridView.rows(rowCount).cells(8).children(0).value != "")
                    CheckMaxlength(theGridView.rows(rowCount).cells(8).children(0).id, 400, remarksmaxlength);

                if (firstErrorControl != "") {

                    SetControlFocus(firstErrorControl);
                    errMsg = "<table>" + errMsg + "</table>";
                    document.getElementById("divErrorMessage").innerHTML = errMsg;

                    return false;
                }
                else {
                    return confirm('Are you sure to submit?');

                }

            }
        } else {
            if (theGridView.rows(rowCount).cells(3).children(0).value != null) {
                if (ValidateRequired(theGridView.rows(rowCount).cells(3).children(0).id, missingStartDate))
                    compareDate1 = CheckDateFormat(theGridView.rows(rowCount).cells(3).children(0).id, invalidStartDate)

                if (ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missingCompletionDate))
                    compareDate2 = CheckDateFormat(theGridView.rows(rowCount).cells(4).children(0).id, invalidCompletionDate)

                if (compareDate1 && compareDate2)
                    DateComparision(theGridView.rows(rowCount).cells(3).children(0).id, theGridView.rows(rowCount).cells(4).children(0).id, compareDate);

                if (ValidateRequired(theGridView.rows(rowCount).cells(5).children(0).id, missingBudgetCostPer))
                    ValidateNumbers(theGridView.rows(rowCount).cells(5).children(0).id, invalidBudgetCostPer)

                //if(ValidateRequired(theGridView.rows(rowCount).cells(6).children(0).id, missingBudgetCost))
                //ValidateNumbers(theGridView.rows(rowCount).cells(6).children(0).id, invalidBudgetCost)
                theGridView.rows(rowCount).cells(6).children(0).disabled = false;
                ValidateRequired(theGridView.rows(rowCount).cells(6).children(0).id, missingBudgetCost)

                if (theGridView.rows(rowCount).cells(7).children(0).value != "")
                    CheckMaxlength(theGridView.rows(rowCount).cells(7).children(0).id, 400, remarksmaxlength);

                if (firstErrorControl != "") {

                    SetControlFocus(firstErrorControl);
                    errMsg = "<table>" + errMsg + "</table>";
                    document.getElementById("divErrorMessage").innerHTML = errMsg;

                    return false;
                }
                else {
                    return confirm('Are you sure to submit?');

                }

            }
        }


    }
}




//validation in grid for Project_Actual

function fnValidateForGridActual() {
    var theGridView = document.getElementById("gridChkList");

    firstErrorControl = "";
    errMsg = "";
    var cellsNo = 0;
    var compareDate1 = false;
    var compareDate2 = false;

    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {

        if (theGridView.rows(rowCount).cells.length == 11) {

            if (theGridView.rows(rowCount).cells(4).children(0).value != null) {
                if (ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missingStartDate)) {
                    compareDate1 = CheckDateFormat(theGridView.rows(rowCount).cells(4).children(0).id, invalidStartDate)

                    if (compareDate1)
                        ValidateSystemDate(theGridView.rows(rowCount).cells(4).children(0).id, greaterStartDate)

                }

                if (ValidateRequired(theGridView.rows(rowCount).cells(6).children(0).id, missingCompletionDate)) {
                    compareDate2 = CheckDateFormat(theGridView.rows(rowCount).cells(6).children(0).id, invalidCompletionDate)
                    if (compareDate2)
                        ValidateSystemDate(theGridView.rows(rowCount).cells(6).children(0).id, greaterCompletionDate)

                }

                if (compareDate1 && compareDate2)
                    DateComparision(theGridView.rows(rowCount).cells(4).children(0).id, theGridView.rows(rowCount).cells(6).children(0).id, compareDateActual);

                if (ValidateRequired(theGridView.rows(rowCount).cells(8).children(0).id, missingBudgetCost))
                    ValidateNumbers(theGridView.rows(rowCount).cells(8).children(0).id, invalidBudgetCost)


                if (firstErrorControl != "") {

                    SetControlFocus(firstErrorControl);
                    errMsg = "<table>" + errMsg + "</table>";
                    document.getElementById("divErrorMessage").innerHTML = errMsg;

                    return false;
                }
                else {
                    return confirm('Are you sure to submit?')

                }

            }
        } else {

            if (theGridView.rows(rowCount).cells(3).children(0).value != null) {

                if (ValidateRequired(theGridView.rows(rowCount).cells(3).children(0).id, missingStartDate)) {
                    compareDate1 = CheckDateFormat(theGridView.rows(rowCount).cells(3).children(0).id, invalidStartDate)
                    if (compareDate1)
                        ValidateSystemDate(theGridView.rows(rowCount).cells(3).children(0).id, greaterStartDate)

                }

                if (ValidateRequired(theGridView.rows(rowCount).cells(5).children(0).id, missingCompletionDate)) {
                    compareDate2 = CheckDateFormat(theGridView.rows(rowCount).cells(5).children(0).id, invalidCompletionDate)
                    if (compareDate2)
                        ValidateSystemDate(theGridView.rows(rowCount).cells(5).children(0).id, greaterCompletionDate)

                }

                if (compareDate1 && compareDate2)
                    DateComparision(theGridView.rows(rowCount).cells(3).children(0).id, theGridView.rows(rowCount).cells(5).children(0).id, compareDateActual);

                if (ValidateRequired(theGridView.rows(rowCount).cells(7).children(0).id, missingBudgetCost))
                    ValidateNumbers(theGridView.rows(rowCount).cells(7).children(0).id, invalidBudgetCost)

                if (firstErrorControl != "") {

                    SetControlFocus(firstErrorControl);
                    errMsg = "<table>" + errMsg + "</table>";
                    document.getElementById("divErrorMessage").innerHTML = errMsg;

                    return false;
                }
                else {
                    return confirm('Are you sure to submit?');

                }

            }
        }


    }
}





//validation in grid for Project_Legal_Chklist

function fnValidateForGridLegal() {
    var theGridView = document.getElementById("gridChkList");

    firstErrorControl = "";
    errMsg = "";

    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {


        if (theGridView.rows(rowCount).cells(3).children(0).value != null) {
            if (ValidateCheckBox(theGridView.rows(rowCount).cells(1).children(0).id)) {
                if (ValidateRequired(theGridView.rows(rowCount).cells(3).children(0).id, missingremarks))
                    CheckMaxlength(theGridView.rows(rowCount).cells(3).children(0).id, 800, remarksmaxlength);
            }
            else {
                if (theGridView.rows(rowCount).cells(3).children(0).value != "")
                    CheckMaxlength(theGridView.rows(rowCount).cells(3).children(0).id, 800, remarksmaxlength);
            }



            if (ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missSeqno))
                ValidateNumbers(theGridView.rows(rowCount).cells(4).children(0).id, invalSeqno)



            if (firstErrorControl != "") {

                SetControlFocus(firstErrorControl);
                errMsg = "<table>" + errMsg + "</table>";
                document.getElementById("divErrorMessage").innerHTML = errMsg;

                return false;
            }
            else {
                return confirm('Are you sure to submit?')

            }

        }
    }
}



//Function to validate Acquisition Check List Grid

function fnValidateForGridAcquisition() {
    var theGridView = document.getElementById("gridAcqChkList");

    firstErrorControl = "";
    errMsg = "";

    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
        if (theGridView.rows(rowCount).cells(4).children(0).value != null) {

            if (ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missSeqno))
                ValidateNumbers(theGridView.rows(rowCount).cells(4).children(0).id, invalSeqno)



            if (ValidateCheckBox(theGridView.rows(rowCount).cells(1).children(0).id)) {
                if (ValidateRequired(theGridView.rows(rowCount).cells(3).children(0).id, missingremarks))
                    CheckMaxlength(theGridView.rows(rowCount).cells(3).children(0).id, 800, remarksmaxlength);

            }
            else {
                if (theGridView.rows(rowCount).cells(3).children(0).value != "")
                    CheckMaxlength(theGridView.rows(rowCount).cells(3).children(0).id, 800, remarksmaxlength);
            }


            if (firstErrorControl != "") {

                SetControlFocus(firstErrorControl);
                errMsg = "<table>" + errMsg + "</table>";
                document.getElementById("divErrorMessage").innerHTML = errMsg;

                return false;
            }
            else {
                return confirm('Are you sure to submit?')

            }

        }
    }
}






//Function to validate Milestone Grid

function fnValidateForGridMile() {
    var theGridView = document.getElementById("gridChkList");

    firstErrorControl = "";
    errMsg = "";


    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
        if (theGridView.rows(rowCount).cells(4).children(0).value != null) {

            if (parseInt(theGridView.rows(rowCount).cells(4).children(0).value) > 100) {
                firstErrorControl = theGridView.rows(rowCount).cells(4).children(0).id;

            }
            //if(ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missingCompletionPer))
            //   ValidateNumbers(theGridView.rows(rowCount).cells(4).children(0).id, invalidCompletionPer)

            if (firstErrorControl != "") {
                errMsg += GetErrorRow(firstErrorControl, invalidCompletionPer);
                SetErrorColor(firstErrorControl, false);
                SetControlFocus(firstErrorControl);
                errMsg = "<table>" + errMsg + "</table>";
                document.getElementById("divErrorMessage").innerHTML = errMsg;

                return false;
            }
            else {
                return confirm('Are you sure to submit?')

            }
        }
    }
}


//Function to validate Project Plot Details

function fnValidateForGridPlotDetails() {
    var theGridView = document.getElementById("gridPlotDetails");

    firstErrorControl = "";
    errMsg = "";
    var plotNoDisplayID = "";
    //var controlObject  = document.getElementById(controlID);

    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
        if (theGridView.rows(rowCount).cells(2).children(0).value != null) {

            plotNoDisplayID = document.getElementById(theGridView.rows(rowCount).cells(1).children(0).id).value;

            if (ValidateRequired(theGridView.rows(rowCount).cells(1).children(0).id, missingPlotNoDisplay))
                ValidateSpecialChars(theGridView.rows(rowCount).cells(5).children(0).id, invalidPlotNoDisplay, "plotnodis")


            if (ValidateRequired(theGridView.rows(rowCount).cells(3).children(0).id, missingPlotSize))
                ValidateNumbers(theGridView.rows(rowCount).cells(3).children(0).id, invalidPlotSize)


            ValidateRequired(theGridView.rows(rowCount).cells(5).children(0).id, missingPlotDimension)
            //ValidateSpecialChars(theGridView.rows(rowCount).cells(5).children(0).id, invalidPlotDimension,"all")             

            if (firstErrorControl != "") {

                SetControlFocus(firstErrorControl);
                errMsg = "<table>" + errMsg + "</table>";
                document.getElementById("divErrorMessage").innerHTML = errMsg;

                return false;
            }
            else {
                if (document.getElementById("lblSaveConfirmMsg").innerHTML == "") {
                    return confirm('Are you sure to submit?')
                } else { return false; }
            }
        }
    }
}


//Function to Validate in Plot Doc Link
function fnValidateForGriddoclink() {
    var theGridView = document.getElementById("gvProjectPlotDocLink");

    firstErrorControl = "";
    errMsg = "";


    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
        if (theGridView.rows(rowCount).cells(4).children(0).value != null) {
            if (ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missingSigningAuth))
                CheckMaxlength(theGridView.rows(rowCount).cells(4).children(0).id, 500, greaterSigningAuth)
            //            if(theGridView.rows(rowCount).cells(4).children(0).value != null)            
            //             ValidateRequired(theGridView.rows(rowCount).cells(4).children(0).id, missingLinkedDoc)
            if (ValidateRequired(theGridView.rows(rowCount).cells(3).children(0).id, missingLinkedDoc))
                CheckMaxlength(theGridView.rows(rowCount).cells(3).children(0).id, 2048, greaterLinkedDoc)

            if (firstErrorControl != "") {

                SetControlFocus(firstErrorControl);
                errMsg = "<table>" + errMsg + "</table>";
                document.getElementById("divErrorMessage").innerHTML = errMsg;

                return false;
            }
            else {
                return confirm('Are you sure to submit?')

            }
        }
    }
}


//validate if Marketable area sqft > Total area sqft
function ValidateGreater(controlName1, controlName2, checkValue) {
    FormatCommaSeparator(false, controlName1);
    FormatCommaSeparator(false, controlName2);
    firstErrorControl = "";
    errMsg = "";
    var controlValue1 = document.getElementById(controlName1).value;
    var controlValue2 = document.getElementById(controlName2).value;

    if (controlValue1 != "" && controlValue2 != "") {
        if (parseInt(controlValue1) < parseInt(controlValue2)) {
            if (checkValue == "area") {
                alert(areasqft);
            }
            else if (checkValue == "cost") {
                alert(costvalue);
            }

            var controlID = controlName2;
            if (firstErrorControl == '')
                firstErrorControl = controlID;

            SetErrorColor(controlID, false);
            //SetControlFocus(firstErrorControl);

            FormatCommaSeparator(true, controlName1);
            FormatCommaSeparator(true, controlName2);
            return false;
        }
        else {
            var controlID1 = controlName1;
            var controlID2 = controlName2;

            //if(controlValue2!="")
            SetErrorColor(controlID1, true);
            SetErrorColor(controlID2, true);


            FormatCommaSeparator(true, controlName1);
            FormatCommaSeparator(true, controlName2);
            return true;
        }
    }
    else {
        FormatCommaSeparator(true, controlName1);
        FormatCommaSeparator(true, controlName2);
        //ValidatePMCControls();
        return false;
    }
}

function ChangeCase(controlID) {
    document.getElementById(controlID).value = document.getElementById(controlID).value.toUpperCase();
}

//Calculate Budget cost calculation
function budgetCostCalculate(strValue) {
    if (strValue != "") {
        var strBaseCost = document.getElementById("hdnBaseCost").value;
        var budgetCost = (parseFloat(strBaseCost) * parseFloat(strValue)) / 100
        budgetCost = Math.round(budgetCost * Math.pow(10, 2)) / Math.pow(10, 2);
        budgetCost = Math.round(budgetCost);

        var theGridView = document.getElementById("gridChkList");

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (theGridView.rows(rowCount).cells.length == 11) {
                theGridView.rows(rowCount).cells(7).children(0).value = budgetCost;
                theGridView.rows(rowCount).cells(7).children(0).disabled = true;
            } else {
                theGridView.rows(rowCount).cells(6).children(0).value = budgetCost;
                theGridView.rows(rowCount).cells(6).children(0).disabled = true;
            }

        }
    } else {

        var theGridView = document.getElementById("gridChkList");

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (theGridView.rows(rowCount).cells.length == 11) {
                //theGridView.rows(rowCount).cells(7).children(0).value=budgetCost;                
                theGridView.rows(rowCount).cells(7).children(0).disabled = false;
                theGridView.rows(rowCount).cells(7).children(0).focus();
            } else {
                theGridView.rows(rowCount).cells(6).children(0).value = budgetCost;
                theGridView.rows(rowCount).cells(6).children(0).disabled = false;
                theGridView.rows(rowCount).cells(6).children(0).focus();
            }

        }
    }

}


//function to save ajax value in project list page to a hidden field

function locationSave() {
    document.getElementById("hdnLocationVal").value = document.getElementById("ddlLocation").value;
    document.getElementById("ddlLocation").disabled = true;
    //alert(document.getElementById("hdnLocationVal").value);
}




//Calculate Budget cost percentage
function budgetCostpercentage(strValue) {
    if (strValue != "") {
        var strBaseCost = document.getElementById("hdnBaseCost").value;
        var budgetper = (100 * parseFloat(strValue)) / parseFloat(strBaseCost)
        //budgetper=Math.round(budgetper);
        budgetper = Math.round(budgetper);

        var theGridView = document.getElementById("gridChkList");

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (theGridView.rows(rowCount).cells.length == 11) {
                theGridView.rows(rowCount).cells(6).children(0).value = budgetper;
                //theGridView.rows(rowCount).cells(6).children(0).disabled = true;
            } else {
                theGridView.rows(rowCount).cells(5).children(0).value = budgetper;
                // theGridView.rows(rowCount).cells(5).children(0).disabled = true;
            }

        }
    } else {

        var theGridView = document.getElementById("gridChkList");

        for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++) {
            if (theGridView.rows(rowCount).cells.length == 11) {
                //theGridView.rows(rowCount).cells(7).children(0).value=budgetCost;                
                theGridView.rows(rowCount).cells(6).children(0).disabled = false;
                theGridView.rows(rowCount).cells(6).children(0).focus();
            } else {
                theGridView.rows(rowCount).cells(5).children(0).value = budgetper;
                theGridView.rows(rowCount).cells(5).children(0).disabled = false;
                theGridView.rows(rowCount).cells(5).children(0).focus();
            }

        }
    }

}






function ValidateSchemeDocs() {

    document.getElementById("lblErrorMessage").innerHTML = "";
    firstErrorControl = "";
    errMsg = "";
    var compareDate1 = false;
    var compareDate2 = false;
    var compareDate3 = false;
    var compareDate4 = false;
    var compareDate5 = false;
    var compareDate6 = false;
    //var fileexnt=document.getElementById("sch_fld").value



    ValidateRequired("txtSchemecode", missingschemecode)

    ValidateDropDown("ddlDocType", missingdoctype)

    ValidateRequired("txtdocdescription", missingdocdes)

    ValidateRequired("txtsch_docno", missingpodocsno)

    ValidateRequired("txtsch_docdt", missingdocdate)

    ValidateRequired("txtsch_finyear", missingfinyear)

    ValidateRequired("txtsch_bline", missingschbil)

    ValidateRequired("txtsch_no", missingschno)

    ValidateRequired("txtsch_seqno", missingschseqno)
    CheckDateFormat("txtsch_docdt", Validdate)

    if (ValidateRequired("sch_fld", missingfilename)) {
        var Exntsn = document.getElementById("sch_fld").value;

        if (!fnCheckExt(Exntsn))

            return false;

    }


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        document.getElementById("lblErrorMessage").innerHTML = "";


        return false;
    }
    else {
        return confirm('Are you sure to submit?')

    }
}







function fnCheckExt(strExtn) {
    var fileName = strExtn

    var Extension = fileName.substr(fileName.lastIndexOf(".") + 1, fileName.length);
    Extension = Extension.toUpperCase();
    if (Extension != "DOC" && Extension != "DOCX" && Extension != "PDF" && Extension != "TXT" && Extension != "XLS" && Extension != "XLSX" && Extension != "JPEG" && Extension != "JPG" && Extension != "TIF") {
        alert("Choose a Valid File");
        return false;
    }
    else
        return true;
}


function validschemecode() {
    document.getElementById("lblErrorMessage").innerHTML = "";
    firstErrorControl = "";
    errMsg = "";
    ValidateDropDown("ddlSchemeCode", missingschemecode)
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        document.getElementById("lblErrorMessage").innerHTML = "";


        return false;
    }
    else {
        return true;
    }
}





function Dealervalidate(control) {

    document.getElementById("lblErrorMessage").innerHTML = "";
    firstErrorControl = "";
    errMsg = "";
    var compareDate1 = false;
    var compareDate2 = false;
    var compareDate3 = false;
    var compareDate4 = false;
    var compareDate5 = false;
    var compareDate6 = false;

    if (ValidateRequired(control, missingfilename)) {
        var Exntsn = document.getElementById(control).value;

        if (!Dealerext(Exntsn))

            return false;

    }

    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        document.getElementById("lblErrorMessage").innerHTML = "";


        return false;
    }
    else {

        if (confirm('Are You Sure to Upload?')) {

            document.getElementById("btnUpload").style.display = 'none';
            document.getElementById("lblupload").style.display = 'block';
            document.getElementById("progressbar").style.display = 'block';

            document.getElementById("lnkCancel").disabled = true;
            return true;
        }
        else
            return false;

    }

}



function Dealerext(strExtn) {
    var fileName = strExtn

    var Extension = fileName.substr(fileName.lastIndexOf(".") + 1, fileName.length);
    Extension = Extension.toUpperCase();
    if (Extension != "TXT") {
        alert("Choose a Text File");
        return false;
    }
    else
        return true;
}


function disable() {
    document.getElementById("progressbar").style.display = 'none';
    document.getElementById("lblupload").innerHTML = "";
}



function validatedepot() {
    document.getElementById("lblErrorMessage").innerHTML = "";
    firstErrorControl = "";
    errMsg = "";
    ValidateDropDown("ddldepotname", "Select a Depot")
    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        document.getElementById("lblErrorMessage").innerHTML = "";


        return false;
    }
    else {
        return true;
    }
}

function roundingeffectiveprice(controlname) {
    var effctvprice = document.getElementById(controlname).value;;
    var effctvprice = Math.round((effctvprice * 10000) / 100) / 100;
    if (effctvprice == "0") {

        firstErrorControl = controlname;
        errMsg += GetErrorRow(controlname, "Effective Price Must be greater than Zero");
        SetErrorColor(controlname, false);
    }
    else

        document.getElementById(controlname).value = effctvprice;


}

// Checkbox validation
function ValidateCheckBoxList(controlName, errorMessage) {
    var errorCode = true;
    var controlID = controlName;
    var chkList = document.getElementById(controlID);

    var count = 0;
    for (var i = 0; i < chkList.getElementsByTagName("input").length; i++) {
        var chknode = chkList.getElementsByTagName("input")[i];
        if (chknode != null && chknode.type == "checkbox" && chknode.checked) {
            count = count + 1;
        }
    }
    if (count == 0) {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }
    else {
        SetErrorColor(controlID, true);
        return true;
    }
}

