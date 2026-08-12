//'**************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidateLovDetails.js
//'Created Date	: 28-November-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: LovDetails File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************

var firstErrorControl ="";
 var errMsg= "";

// JScript File

function fnValidateForgvLovDetails(rowIndex)
{
//var rowValue = parseInt(rowIndex);
//alert( rowValue -1 + "dfsgsdf");
    var theGridView = document.getElementById("gvLovDetails"); 
 
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
                 
            if ( theGridView.rows(rowCount).cells(1).children(0).value != null) 
                { 
                   
                   ValidateRequired(theGridView.rows(rowCount).cells(0).children(0).id, missingLovType)

                   ValidateRequired(theGridView.rows(rowCount).cells(1).children(0).id, missingLovDesc)
                   
                   ValidateRequired(theGridView.rows(rowCount).cells(2).children(0).id, missingLovVal)
                   
                   if( ValidateRequired(theGridView.rows(rowCount).cells(3).children(0).id, missingLovSeq)) 
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


function ValidateLDLdivControls()
{

firstErrorControl ="";
    errMsg= "";
                          
    ValidateRequired("txtType", missingLovType)

    ValidateRequired("txtDesc", missingLovDesc)
                   
    ValidateRequired("txtValue", missingLovVal)
                   
    if( ValidateRequired("txtSeq", missingLovSeq)) 
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

function fnCompareLovDetailsCode(lcode,hdnCode,type)
{   



        //var type = document.getElementById("ddlLOV").selectedValue;
        
        CreateXMLHTTP5();
        if(xmlHttp5)
        {            
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "LovDetailsCode";
            requestURL += "&lovtype=";
            requestURL += type;
            requestURL += "&Lcode=";
            requestURL += lcode;
            requestURL += "&hcode=";
            requestURL += hdnCode;
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
              
            xmlHttp5.onreadystatechange=doMenuCodeExists;
            xmlHttp5.open("GET",requestURL,true);
            xmlHttp5.send(null);
        }
        
      return true;
//   }
//   return false;
}



function doMenuCodeExists()
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
             
            document.getElementById("lblLOVCode").innerHTML = "LOVCode already exists";
            
            return false;
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
    
}