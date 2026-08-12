//'*************************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidationWorkFlow.js
//'Created Date	: 12-December-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: WorkFlow File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************

// JScript File



function ValidateWFHLControls()
{
    firstErrorControl ="";
    errMsg= "";
    
    if( ValidateDropDown("ddlType1",missingType1))
        ValidateDropDown("ddlSelect1",missingSelect1);
    
    if( document.getElementById('ddlType2').value != "Select")
    {
        ValidateDropDown("ddlSelect2",missingSelect2) ;
        ValidateDropDown("ddlType1",missingType1) ; 
    }
        
    if( document.getElementById('ddlType3').value != "Select")
    {
        ValidateDropDown("ddlSelect3",missingSelect3) ;
        ValidateDropDown("ddlType1",missingType1) ; 
        ValidateDropDown("ddlType2",missingType2) ;
    }
        
    if( document.getElementById('ddlType4').value != "Select")
    {
        ValidateDropDown("ddlSelect4",missingSelect4) ;
        ValidateDropDown("ddlType1",missingType1) ; 
        ValidateDropDown("ddlType2",missingType2) ;
        ValidateDropDown("ddlType3",missingType3) ;
    }
        
    if( document.getElementById('ddlType5').value != "Select")
    {
        ValidateDropDown("ddlSelect5",missingSelect5) ;
        ValidateDropDown("ddlType1",missingType1) ; 
        ValidateDropDown("ddlType2",missingType2) ;
        ValidateDropDown("ddlType3",missingType3) ;
        ValidateDropDown("ddlType4",missingType4) ;
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
    if(document.getElementById("lblTypeErrMsg").innerHTML == "")
    {
    return confirm ('Are you sure to submit?')               
    }
    else
    {
    return false;
    }
    }
    
}




//Ajax to check the FinYear & TypeDoc

function fnWorkFlowType(Wtype,wkflid)
{   

    
        //var txtboxval = extto;
        CreateXMLHTTP5();
        if(xmlHttp5)
        {            
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "WorkFlow";
            requestURL += "&type=";
            requestURL += Wtype;
            requestURL += "&id=";
            requestURL += wkflid;
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
              
            xmlHttp5.onreadystatechange=doWrkFlwTypeExists;
            xmlHttp5.open("GET",requestURL,true);
            xmlHttp5.send(null);
        }
        
      return true;
   
}



function doWrkFlwTypeExists()
{
   if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {
        if(xmlHttp5.status == 200)
        {        
           
           if(xmlHttp5.responseText == "False")
           {
             document.getElementById("lblTypeErrMsg").innerHTML = ""; 
              
              document.getElementById("btnSubmit").disabled = false;               
            //document.getElementById(clientID + "hdnUserIdCompare").value = "true";        
           }
           else
           {
             //document.getElementById("divUserIdExistErrorMsg").style.display = 'block';
            //document.getElementById("lblSaveConfirmMsg").innerHTML = "<table>" + userIdExist + "</table>";
            document.getElementById("lblTypeErrMsg").innerHTML = "Type already exists";
            
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







