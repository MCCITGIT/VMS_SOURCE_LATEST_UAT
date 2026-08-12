//'**************************************************
//'Copyright	: VMS, MCC, KOLKATA
//'Source	    : Scripts/ValidateVendorMaster.js
//'Created Date	: 06-12-2011
//'Created By	: Deepak
//'Version	    : R01.00.00
//'Description	: Validation Vendor Master

//'Modified By       Modified On       Version         Reason

//'*************************************************************

// JScript File



function ValidateSearchInfo()
{
 firstErrorControl ="";
    errMsg= "";
     ValidateRequired("txtSKU","Select SKU Code")
    document.getElementById('btnSkuCode').click()   
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else 
    {
   return true
}
        
        
}