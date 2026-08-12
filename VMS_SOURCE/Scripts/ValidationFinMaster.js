//'**************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidationFinMaster.js
//'Created Date	: 25-November-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: FinMaster File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function ValidateYMAControls()
{
    firstErrorControl ="";
    errMsg= "";
    
    var start = false;
    var end = false;
    

   ValidateRequired("txtFinYear", missingWeek);
         
   if(ValidateRequired("txtStartDate", missingStartDate))
        start = CheckDateFormat("txtStartDate", invalidStartDate); 
                         
   if(ValidateRequired("txtEndDate", missingEndDate))
        end = CheckDateFormat("txtEndDate", invalidEndDate); 
        
   if(start && end)
        ValidatetwoDates("txtStartDate","txtEndDate",greaterStartDate);
   
   if(ValidateRequired("txtNoWeek", missingNoWeek))
        ValidateWeekNo("txtNoWeek", greaterNoWeek);

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



//Ajax to check the FinYear

function fnFinYearDetails(val)
{    
    var txtboxval = val;
    CreateXMLHTTP5();
    if(xmlHttp5)
    {            
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "FinYear";
        requestURL += "&FinYear=";
        requestURL += txtboxval;
        requestURL += "&Random=";
        requestURL += Math.random();
        var tempDate = new Date();
        var tempDay = tempDate.getDate();     
        var tempMonth = tempDate.getMonth();
        var tempYear = tempDate.getFullYear();
        var tempHour = tempDate.getHours();
        var tempMin = tempDate.getMinutes();
        var tempSec = tempDate.getSeconds();
        var tempMil = tempDate.getMilliseconds();
        
        var tempDateString = tempDay + ":" + tempMonth +  ":" + tempYear + ':' + tempHour + ':' + tempMin + ':' + tempSec + ':' + tempMil;
        
        requestURL += "&timeStamp=";
        requestURL += tempDateString;
          
        xmlHttp5.onreadystatechange=doUserIdExists;
        xmlHttp5.open("GET",requestURL,true);
        xmlHttp5.send(null);
    }
    
  return true;
}



function doUserIdExists()
{
   if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {
        if(xmlHttp5.status == 200)
        {        
           
           if(xmlHttp5.responseText == "False")
           {
             document.getElementById("lblFinYearErrMsg").innerHTML = ""; 
              
              document.getElementById("btnSubmit").disabled = false;               
            //document.getElementById(clientID + "hdnUserIdCompare").value = "true";        
           }
           else
           {
             //document.getElementById("divUserIdExistErrorMsg").style.display = 'block';
            //document.getElementById("lblSaveConfirmMsg").innerHTML = "<table>" + userIdExist + "</table>";
            document.getElementById("lblFinYearErrMsg").innerHTML = "FinYear already exists";
            
            document.getElementById("btnSubmit").disabled = true;       
           
            //document.getElementById(clientID + "hdnUserIdCompare").value = "false";
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
    return true; 
}


