//'**************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidateChngPwd.js
//'Created Date	: 23-November-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: Change Password File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function ValidateChangePwd()
{
 firstErrorControl ="";
    errMsg= "";
    
    //ValidateRequired("txtName",missingUserID)
    
    if( ValidateRequired("txtOldPwd",missingOldPwd))
        ValidateText("hdnOldPwd","txtOldPwd",invalidOldPwd);
    
    ValidateRequired("txtNewPwd",missingNewPwd)
            
    ValidateRequired("txtConPwd",missingConPwd)
    
    if( (document.getElementById("txtNewPwd").value != "") && (document.getElementById("txtConPwd").value != "") )
            ValidateText("txtNewPwd","txtConPwd",invalidConPwd);
   
           
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        return false;    
    }
    else
    {      
       
        if( confirm ('Are you sure to submit?'))
      {
        return true;
      }
      else
      { 
       return false;
      }
      
    }

}

function ValidateChangePwdLink()
{
 firstErrorControl ="";
    errMsg= "";
    
    ValidateRequired("txtUserName",missingUserName)
          
    ValidateRequired("txtNewPwd",missingNewPwd)
            
    ValidateRequired("txtConPwd",missingConPwd)
    
    if( (document.getElementById("txtNewPwd").value != "") && (document.getElementById("txtConPwd").value != "") )
            ValidateText("txtNewPwd","txtConPwd",invalidConPwd);
   
           
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        return false;    
    }
    else
    {      
       
        //return fnPwdDetails();        
          return confirm ('Are you sure to submit?') 
      
    }

}




function ValidateText(controlName1, controlName2, errorMessage)
{
var val1 = document.getElementById(controlName1).value;
var val2 = document.getElementById(controlName2).value;
if(val1 != val2)
{
    var controlID = controlName2;
   //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
      
      return false;
}
else
{
    SetErrorColor(controlID, true);
    return true;
}

}

 
//Ajax to check the Password

function fnPwdDetails(val)
{    
    //var txtboxval = document.getElementById('txtConPwd').value;
    var txtboxval = val;
    CreateXMLHTTP5();
    if(xmlHttp5)
    {            
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "ChangePassword";
        requestURL += "&pwd=";
        requestURL += txtboxval;
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
             document.getElementById("lblPwdErrMsg").innerHTML = ""; 
             document.getElementById("btnSubmit").disabled = false;               
             //return confirm ('Are you sure to submit?') 
            
           }
           else
           {
  
            document.getElementById("lblPwdErrMsg").innerHTML = "Password already exists";
            document.getElementById("btnSubmit").disabled = true;  
              return false; 
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
            return false; 
        }
        
    }
    
}




function fnPwdLinkDetails(val,Uname)
{    
    var userid = document.getElementById("txtUserName").value;
    var txtboxval = val;
    CreateXMLHTTP5();
    if(xmlHttp5)
    {            
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "ChangePasswordLink";
        requestURL += "&pwd=";
        requestURL += txtboxval;
        requestURL += "&usrid=";
        requestURL += userid;
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
          
        xmlHttp5.onreadystatechange=doUserIdLinkExists;
        xmlHttp5.open("GET",requestURL,true);
        xmlHttp5.send(null);
    }
    
  return true;
}




function doUserIdLinkExists()
{
   if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {alert(xmlHttp5.status);
        if(xmlHttp5.status == 200)
        {        
  
           if(xmlHttp5.responseText == "False")
           {
             document.getElementById("lblPwdErrMsg").innerHTML = ""; 
             document.getElementById("btnSubmit").disabled = false;               
             //return confirm ('Are you sure to submit?') 
            
           }
           else
           {
  
            document.getElementById("lblPwdErrMsg").innerHTML = "Password already exists";
            document.getElementById("btnSubmit").disabled = true;  
              return false; 
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
            return false; 
        }
        
    }
    
}