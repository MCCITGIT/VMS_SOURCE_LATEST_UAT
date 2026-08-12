// JScript File

//User_Profile_Add

function ValidateUPAControls()
{
    firstErrorControl ="";
    errMsg= "";
    
    var DOB = false;
    var DOJ = false;
   
   ValidateRequired("txtUserID",missingUserID)
   
   ValidateDropDown("ddlBranch",missingBranch)
   
   ValidateDropDown("ddlDepartment",missingDepartment)
          
   ValidateRequired("txtFirstName",missingFirstName)
   
   ValidateRequired("txtShortName",missingSName)
   
   ValidateDropDown("ddlUserGroup",missingUserGroup)
   
   ValidateDropDown("ddlPrmtTmpy",missingPrmtTmpy)
   
   ValidateDropDown("ddlDesignation",missingDesignation)

   ValidateDropDown("ddlReportingTo1", missingReportingTo1)

   ValidateDropDown("ddlReportingTo2", "Select Reporting to from the list.")
   
   //ValidateDropDown("ddlRegion",missingRegion)   
   
   CheckMaxlength("txtResAddress",300,invalidResAddress)
   
   if( document.getElementById("txtOfficePhoneNo").value != "")
         ValidateNotAlpha("txtOfficePhoneNo",invalidAlphaOfficePhoneNo)
   
   ValidateNumbers("txtExtension",invalidExtension)
   
   if( document.getElementById("txtResPhoneNo").value != "")
        ValidateNotAlpha("txtResPhoneNo",invalidAlphaResPhoneNo)
   
   if( document.getElementById("txtMobilePhoneNo").value != "")
         ValidateNotAlpha("txtMobilePhoneNo",invalidAlphaMobileNo)
   
   if( document.getElementById("txtEmail").value != "")
        ValidateEmail("txtEmail",invalidEmail)
   
   if(document.getElementById("txtDOB").value != "")
       if( CheckDateFormat("txtDOB",invalidDOB))
           DOB =  ValidateSystemDate("txtDOB",invalidDOBSYS)
   
   if(document.getElementById("txtDOJ").value != "")
       if( CheckDateFormat("txtDOJ",invalidDOJ)) 
       DOJ =  ValidateSystemDate("txtDOJ",invalidDOJSYS) 
       
   if(DOB && DOJ)
    ValidatetwoDates("txtDOB","txtDOJ",greaterDOB)

//   ValidateNumbers("txtTotalExpYears",invalidTotalExpYears)
   
//  if( ValidateNumbers("txtTotalExpMonths",invalidTotalExpMonths))
        ValidateGrNo("txtTotalExpMonths",invalidTotalExpMonths)
   
   if( document.getElementById("spanDtSprtn").innerHTML == "*" )
        if( CheckDateFormat("txtDateOfSeperation",invalidDOS))
            ValidateSystemDate("txtDateOfSeperation",invalidDOSSYS)
   
   
    
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else
    {    
      if(document.getElementById("lblSaveConfirmMsg").innerHTML == '')
      {    
            if( confirm ('Are you sure to submit?'))
        {
          document.getElementById('btnSubmit').disabled=true;
         __doPostBack(document.getElementById('btnSubmit').name,'');
        }
          else
         { 
          return false;
         }
        
      }
      else
      {
        document.getElementById("btnSubmit").disabled = false;  
        return false;
      }
   
    }
}


function fnReasonSeperation(val)
{
    if(val == "0")
        {
            document.getElementById('txtDateOfSeperation').value = "";
            document.getElementById('txtDateOfSeperation').disabled = true;
            document.getElementById('aCalendar').removeAttribute('href');
            document.getElementById('spanDtSprtn').innerHTML = "";
        }
    else
        {
            document.getElementById('txtDateOfSeperation').disabled = false;         
            document.getElementById('aCalendar').setAttribute('href',"javascript:cal1.select(document.forms[0].txtDateOfSeperation,'DateOfSeperation','dd/MM/yyyy');");
            document.getElementById('spanDtSprtn').innerHTML = "*";
        }
}




//Ajax to check the User ID

function compareUserID(txtUserID)
{    
    CreateXMLHTTP5();
    if(xmlHttp5)
    {            
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "UserID";
        requestURL += "&prjCode=";
        requestURL += txtUserID;
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
             document.getElementById("lblSaveConfirmMsg").innerHTML = ""; 
              document.getElementById("ddlBranch").focus();    
              //document.getElementById("btnSubmit").disabled = true;               
            //document.getElementById(clientID + "hdnUserIdCompare").value = "true";        
           }
           else
           {
             //document.getElementById("divUserIdExistErrorMsg").style.display = 'block';
            //document.getElementById("lblSaveConfirmMsg").innerHTML = "<table>" + userIdExist + "</table>";
            document.getElementById("lblSaveConfirmMsg").innerHTML = "UserID already exists";
            document.getElementById("txtUserID").focus();    
            //document.getElementById("btnSubmit").disabled = false;       
           
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







function ValidateGrNo(controlName1, errorMessage)
{ 
   var expMonth = document.getElementById(controlName1).value;
      
   if(parseInt(expMonth) > "12")
   {
   var controlID = controlName1;
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

function fnRegionGet(DepotCode)
{  
   
    if (DepotCode != "0")
    {          
        CreateXMLHTTP5();
        
        if(xmlHttp5)
        {
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "DepotRegion";
            requestURL += "&Depot=";
            requestURL += DepotCode;                     
            requestURL += "&Random=";
            requestURL += Math.random();
            
          xmlHttp5.onreadystatechange=doloadDepotDetails;
          xmlHttp5.open("GET",requestURL,true);
          xmlHttp5.send(null);
        }
    }
    else
    {
       alert("There was a problem retrieving data from the server." );
    }
}

function doloadDepotDetails()
{
    if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {
        if(xmlHttp5.status == 200)
        {
           showDepotDetails(xmlHttp5.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}

function showDepotDetails(result)
{

    try
    {       
    
        if( result != "")
        {
            eval("var DepotDetails =" + result);
            if(DepotDetails != null && DepotDetails.length > 0 && DepotDetails != "")
            {  
//                document.getElementById('ddlRegion').options.length=0;            
//                document.getElementById('ddlRegion').options[0] = new Option("Select");
                document.getElementById('ddlRegion').value = DepotDetails[0].Depot_region;
            
               
                                      
            }
           
        }
        else
        {
                   
        }
    }
    catch(e)
        {        
                alert("There was a problem retrieving data from the server.");
        }
    
}
