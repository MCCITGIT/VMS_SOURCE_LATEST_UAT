// JScript File

//Booking_Agreement_List

function ValidateBALControls()
{
 firstErrorControl ="";
    errMsg= "";
    
   //if(document.getElementById("txtBAPlotNo").value != "")
     //   ValidateNumbers("txtBAPlotNo",invalidBAPlotNo)

        
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else
    {    
      return true; 
   
    }

}




//Registration_List

function ValidateRegnListControls()
{
 firstErrorControl ="";
    errMsg= "";
    
   //if(document.getElementById("txtPlotNo").value != "")
     //   ValidateNumbers("txtPlotNo",invalidBAPlotNo)

        
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else
    {    
      return true; 
   
    }

}









//Request_List

function ValidateReqListControls()
{
    firstErrorControl ="";
    errMsg= "";
    var compareDateFrom = false;
    var compareDateTo = false;




    if( ((document.getElementById("txtDateFrom").value != "") ||  (document.getElementById("txtDateTo").value != "")) )
        {
            compareDateFrom=CheckDateFormat("txtDateFrom", invalidDateFrom)
            compareDateTo=CheckDateFormat("txtDateTo", invalidDateTo)
            
            if(compareDateFrom && compareDateTo)
                ValidatetwoDates("txtDateFrom","txtDateTo",greaterReqDate);
        }

        
        if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else
    {    
      return true; 
   
    }

}



//Lead_List

function ValidateLeadListControls()
{
 firstErrorControl ="";
    errMsg= "";
    
   if(document.getElementById("txtDate").value != "")
        CheckDateFormat("txtDate",invalidDate)

        
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else
    {    
      return true; 
   
    }

}



//Trip_List

function ValidateTripListControls()
{
 firstErrorControl ="";
    errMsg= "";
    
   if(document.getElementById("txtSelectDate").value != "")
        CheckDateFormat("txtSelectDate",invalidSelectDate) 
             

        
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else
    {    
      return true; 
   
    }

}

//Trip Planning Add Option button selected
function raiobtnassign()
{
    var optId=document.getElementById("hdnTripOptionIdSelected").value;    
    if(optId !="")
    {               
        document.getElementById(optId).checked = true;
    }
       
}


//Trip Planning Add Option button id value assign;
function getOptionValue(strId,strValue)
{    
    document.getElementById("hdnTripOptionIdSelected").value=strId;  
    document.getElementById("hdnTripOptionSelected").value=strValue;
       
}