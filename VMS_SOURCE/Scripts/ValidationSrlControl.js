//'*************************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidationSrlControl.js
//'Created Date	: 23-November-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: Serial Control File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function ValidateSNCAControls()
{
 firstErrorControl ="";
    errMsg= "";
    
    ValidateDropDown("ddlFinYear",missingFinYear)
    
    ValidateDropDown("ddlTypeDoc",missingTypeDoc)
    
    ValidateDropDown("ddlLocation",missingLocation)
    
    ValidateRequired("txtNo",missingNo)
    
    ValidateRequired("txtIncrement",missingInc)
   
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


function fnChangeFromTo()
{
document.getElementById("ddlTypeDoc").selectedIndex = 0;
}




//Ajax to check the FinYear & TypeDoc

function fnChangeFromToExists(screenStatus,srlid)
{   

    if( (document.getElementById("ddlFinYear").selectedIndex != 0 ) && (document.getElementById("ddlTypeDoc").selectedIndex != 0) && (document.getElementById("ddlLocation").selectedIndex != 0))
    {
     var doctype = document.getElementById("ddlTypeDoc").value;
     var finyear = document.getElementById("ddlFinYear").value;
     var loc = document.getElementById("ddlLocation").value;     
        //var txtboxval = extto;
        CreateXMLHTTP5();
        if(xmlHttp5)
        {            
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "SerialControl";
            requestURL += "&year=";
            requestURL += finyear;
            requestURL += "&doc=";
            requestURL += doctype;
            requestURL += "&screenStatus=";
            requestURL += screenStatus;
            requestURL += "&srlloc=";
            requestURL += loc;
            requestURL += "&srlid=";
            requestURL += srlid;
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
              
            xmlHttp5.onreadystatechange=doSrlCntrlExists;
            xmlHttp5.open("GET",requestURL,true);
            xmlHttp5.send(null);
        }
        
      return true;
   }
   return false;
}



function doSrlCntrlExists()
{
   if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {
        if(xmlHttp5.status == 200)
        {        
           
           if(xmlHttp5.responseText == "False")
           {
             document.getElementById("lblPwdErrMsg").innerHTML = ""; 
              
              document.getElementById("btnSubmit").disabled = false;               
            //document.getElementById(clientID + "hdnUserIdCompare").value = "true";        
           }
           else
           {
             //document.getElementById("divUserIdExistErrorMsg").style.display = 'block';
            //document.getElementById("lblSaveConfirmMsg").innerHTML = "<table>" + userIdExist + "</table>";
            document.getElementById("lblPwdErrMsg").innerHTML = "Document Type already exists";
            
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


