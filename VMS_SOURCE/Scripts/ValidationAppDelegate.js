//'**************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidateAppDelegate.js
//'Created Date	: 4-December-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: ApproveDelegationAdd File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function ValidateADA()
{
 firstErrorControl ="";
    errMsg= "";
    var From = false;
    var To = false;
    var DelFrom = false;
    var DelTo = false;
    
    DelFrom = ValidateDropDown("ddlDelFrom",missingDelFrom);
    
    DelTo = ValidateDropDown("ddlDelTo",missingDelTo);
    
    if(ValidateRequired("txtFromDate",missingFromDate))
        if(CheckDateFormat("txtFromDate",invalidFromDate))
            From = ValidateGThanSystemDate("txtFromDate",lesserFromDate)
        
            
    if( ValidateRequired("txtToDate",missingToDate))
        To = CheckDateFormat("txtToDate",invalidToDate)
       
    
    if( DelFrom && DelTo)
        ValidateSameDropDown("ddlDelFrom","ddlDelTo",invalidDel)
        
    if( From && To)
        ValidatetwoDates("txtFromDate","txtToDate",invalidDate)
        
        
    
 
        
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        return false;    
    }
    else
    {      
      return confirm ('Are you sure to submit?')   
    }

}

