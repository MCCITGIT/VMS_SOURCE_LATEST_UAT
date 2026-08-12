//'**************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidateLovMaster.js
//'Created Date	: 29-November-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: LovDetails File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************

var firstErrorControl ="";
 var errMsg= "";

// JScript File

function fnValidateForgvLovMstr(rowIndex)
{
//var rowValue = parseInt(rowIndex);
//alert( rowValue -1 + "dfsgsdf");
    var theGridView = document.getElementById("gvLovMstr"); 
 
    firstErrorControl ="";
    errMsg= "";
    
    var rowLength;
    var rowCountVal;
    if(rowIndex == -1)
    {
        rowCountVal = theGridView.rows.length-1;
        rowLength = theGridView.rows.length;
        }
    else
    {
        rowCountVal = 1;
        rowLength = theGridView.rows.length - 1;
    }
    for ( var rowCount = rowCountVal; rowCount < rowLength; rowCount++ ) 
         { 
                 
            if ( theGridView.rows(rowCount).cells(0).children(0).value != null) 
                { 
                   
                   ValidateRequired(theGridView.rows(rowCount).cells(0).children(0).id, missingLovType)

                   ValidateRequired(theGridView.rows(rowCount).cells(1).children(0).id, missingLovDesc)
              
                   ValidateNumbers(theGridView.rows(rowCount).cells(3).children(0).id, invalidLovSeq) 
                                      
                }
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
    if(document.getElementById("lblLOVCode").innerHTML == "")
    {
    return confirm ('Are you sure to submit?')               
    }
    else
    {
    return false;
    }
    }

}


function ValidateLMAdivControls()
{

    firstErrorControl ="";
    errMsg= "";
    
    
    ValidateRequired("txtType", missingLovType)

    ValidateRequired("txtDesc", missingLovDesc)
              
    ValidateNumbers("txtSeq", invalidLovSeq) 
                                      
                   

        
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage1").innerHTML = errMsg;
        return false;    
    }
    else
    {      
      return confirm ('Are you sure to submit?')   
    }

}




//Ajax to check the code

function fnCompareLovMstrType(type, htype)
{   



        //var type = document.getElementById("ddlLOV").selectedValue;
        
        CreateXMLHTTP5();
        if(xmlHttp5)
        {            
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "LovMasterType";
            requestURL += "&lovtype=";
            requestURL += type;
            requestURL += "&htype=";
            requestURL += htype;
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
              
            xmlHttp5.onreadystatechange=doMstrTypeExists;
            xmlHttp5.open("GET",requestURL,true);
            xmlHttp5.send(null);
        }
        
      return true;
//   }
//   return false;
}



function doMstrTypeExists()
{
   if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {
        if(xmlHttp5.status == 200)
        {        
           
           if(xmlHttp5.responseText == "False")
           {
             document.getElementById("lblLOVCode").innerHTML = ""; 
              
            return true;
           }
           else
           {
             //document.getElementById("divUserIdExistErrorMsg").style.display = 'block';
            //document.getElementById("lblSaveConfirmMsg").innerHTML = "<table>" + userIdExist + "</table>";
            document.getElementById("lblLOVCode").innerHTML = "LOV Type already exists";
            
            return false;
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
   
}