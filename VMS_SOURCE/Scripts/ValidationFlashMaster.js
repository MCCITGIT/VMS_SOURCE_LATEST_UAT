//'*************************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidationFlashMaster.js
//'Created Date	: 05-January-2007
//'Created By	: Saravanan
//'Version	    : R02.00.00
//'Description	: Flash News Master

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function ValidateFlashNewsMasterControls()
{
    firstErrorControl ="";
    errMsg= "";
    
    if(document.getElementById("txtMsg1").value !='' )    
        if(ValidateRequired("txtDoExp1",missingDoE))
            if(CheckDateFormat("txtDoExp1",invalidTillDate))
                ValidatetwoDates("txtDoE1","txtDoExp1",greaterTillDate)
        
    if(document.getElementById("txtMsg2").value !='' )    
         if(ValidateRequired("txtDoExp2",missingDoE))
            if(CheckDateFormat("txtDoExp2",invalidTillDate))
                ValidatetwoDates("txtDoE2","txtDoExp2",greaterTillDate)

    if(document.getElementById("txtMsg3").value !='' )    
         if(ValidateRequired("txtDoExp3",missingDoE))
            if(CheckDateFormat("txtDoExp3",invalidTillDate))
                ValidatetwoDates("txtDoE3","txtDoExp3",greaterTillDate)
                  
    if(document.getElementById("txtMsg4").value !='' )    
         if(ValidateRequired("txtDoExp4",missingDoE))
            if(CheckDateFormat("txtDoExp4",invalidTillDate))
                ValidatetwoDates("txtDoE4","txtDoExp4",greaterTillDate)
    
    if(document.getElementById("txtMsg5").value !='' )    
         if(ValidateRequired("txtDoExp5",missingDoE))
            if(CheckDateFormat("txtDoExp5",invalidTillDate))
                ValidatetwoDates("txtDoE5","txtDoExp5",greaterTillDate)

    if(document.getElementById("txtMsg6").value !='' )    
         if(ValidateRequired("txtDoExp6",missingDoE))
            if(CheckDateFormat("txtDoExp6",invalidTillDate))
                ValidatetwoDates("txtDoE6","txtDoExp6",greaterTillDate)
        
    if(document.getElementById("txtMsg7").value !='' )    
         if(ValidateRequired("txtDoExp7",missingDoE))
            if(CheckDateFormat("txtDoExp7",invalidTillDate))
                ValidatetwoDates("txtDoE7","txtDoExp7",greaterTillDate)
        
    if(document.getElementById("txtMsg8").value !='' )    
         if(ValidateRequired("txtDoExp8",missingDoE))
            if(CheckDateFormat("txtDoExp8",invalidTillDate))
                ValidatetwoDates("txtDoE8","txtDoExp8",greaterTillDate)        

    if(document.getElementById("txtMsg9").value !='' )    
         if(ValidateRequired("txtDoExp9",missingDoE))
            if(CheckDateFormat("txtDoExp9",invalidTillDate))
                ValidatetwoDates("txtDoE9","txtDoExp9",greaterTillDate)

    if(document.getElementById("txtMsg10").value !='' )    
         if(ValidateRequired("txtDoExp10",missingDoE))
            if(CheckDateFormat("txtDoExp10",invalidTillDate))
                ValidatetwoDates("txtDoE10","txtDoExp10",greaterTillDate)
                
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


function DisplayCurrentDate(DoEControlId, MsgtxtControlId, DoExpControlId, hdnCurrentDate)
{
   
   var checkCurrentdateObj = new Date();
    
    var y=checkCurrentdateObj.getYear()+"";
	var M=checkCurrentdateObj.getMonth()+1;
	var d=checkCurrentdateObj.getDate();
	
	var currDate ="";
	
	
//	if(parseInt(M) <= 9)
//	    currDate = d + "/" + "0" + M + "/" + y;
    
    if(parseInt(d) <= 9)
    {
        if(parseInt(M) <= 9)
        {
	        currDate = "0" + d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate = "0" + d + "/" + M + "/" + y;
	    }
	}
	else
	{
	    if(parseInt(M) <= 9)
        {
	        currDate =  d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate =  d + "/" + M + "/" + y;
	    }
	}	     
	     
    if(document.getElementById(MsgtxtControlId).value!='')
    {
        if (document.getElementById(DoEControlId).value == '' ) 
        {
            document.getElementById(DoEControlId).value = currDate;
            document.getElementById(hdnCurrentDate).value = currDate;
          
            var tillDate = dateFormatChange(currDate,1,30);
            document.getElementById(DoExpControlId).value = tillDate;
          
        }    
    }       
}