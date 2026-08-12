//'**************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidateLovMaster.js
//'Created Date	: 04-December-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: WeekMasterAdd File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************

var firstErrorControl ="";
 var errMsg= "";

// JScript File

function fnValidateForgvWeekMasterAdd(rowIndex)
{
//var rowValue = parseInt(rowIndex);
//alert( rowValue -1 + "dfsgsdf");
    var theGridView = document.getElementById("gvWeekMasterAdd"); 
 
    firstErrorControl ="";
    errMsg= "";
    
    var start = false;
    var end = false;
    var diff = false;
    
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
                   
                   if(ValidateRequired(theGridView.rows(rowCount).cells(0).children(0).id, missingWeek))
                        if(ValidateNumbers(theGridView.rows(rowCount).cells(0).children(0).id, invalidWeek))
                            ValidateWeekNo(theGridView.rows(rowCount).cells(0).children(0).id, greaterNoWeek);

                   if(ValidateRequired(theGridView.rows(rowCount).cells(1).children(0).id, missingStartDate))
                        start = CheckDateFormat(theGridView.rows(rowCount).cells(1).children(0).id, invalidStartDate) 
                         
                   if(ValidateRequired(theGridView.rows(rowCount).cells(2).children(0).id, missingEndDate))
                        end = CheckDateFormat(theGridView.rows(rowCount).cells(2).children(0).id, invalidEndDate)
                        
                   if(start && end)
                        diff = ValidatetwoDates(theGridView.rows(rowCount).cells(1).children(0).id,theGridView.rows(rowCount).cells(2).children(0).id,greaterStartDate);  
                             
                   if(diff)
                        ValidateWeekDate(theGridView.rows(rowCount).cells(1).children(0).id,theGridView.rows(rowCount).cells(2).children(0).id,invalidDiffEndDate,"greaterValidTillDate");         
                        
                    ValidateCheckMonth(theGridView.rows(rowCount).cells(4).children(0).id,theGridView.rows(rowCount).cells(3).children(0).id,invalidMonth);                        
                    
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
        if(document.getElementById("lblYearWeek").innerHTML == "")
        {
            return confirm ('Are you sure to submit?')               
        }
        else
        {
            return false;
        }
    }

}

function ValidateWMAdivControls()
{
    firstErrorControl ="";
    errMsg= "";
    
    var start = false;
    var end = false;
    var diff = false;
    
//    if(ValidateRequired("txtWeekNo", missingWeek))
//        ValidateWeekNo("txtWeekNo", greaterNoWeek);
         
   if(ValidateRequired("txtWeekStartDate", missingStartDate))
        start = CheckDateFormat("txtWeekStartDate", invalidStartDate) 
                         
   if(ValidateRequired("txtWeekEndDate", missingEndDate))
        end = CheckDateFormat("txtWeekEndDate", invalidEndDate) 
        
   if(start && end) 
        diff = ValidatetwoDates("txtWeekStartDate","txtWeekEndDate",greaterStartDate);
        
   if(diff)
        ValidateWeekDate("txtWeekStartDate","txtWeekEndDate",invalidDiffEndDate,"greaterValidTillDate");    
        
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


function ValidateCheckMonth(hiddenMonthControlName, selectMonthControlName, errorMessage)
{
    var hiddenMonth =  document.getElementById(hiddenMonthControlName).value;
    var selectMonth =  document.getElementById(selectMonthControlName).value;    
    var statusCheck = true;
    
    if(parseInt(hiddenMonth)==12)
    {
        if((parseInt(selectMonth)!=1) || (parseInt(selectMonth)==1))
        {
            statusCheck = false;
        }
    }   
   
    
    if((parseInt(hiddenMonth) > parseInt(selectMonth)) && statusCheck )
    {    
       firstErrorControl = selectMonthControlName;      
       errMsg += GetErrorRow(selectMonthControlName, errorMessage);
       SetErrorColor(selectMonthControlName, false);
       return false;
    }
    else if((parseInt(selectMonth)!=1) && statusCheck == false)
    {
       firstErrorControl = selectMonthControlName;      
       errMsg += GetErrorRow(selectMonthControlName, errorMessage);
       SetErrorColor(selectMonthControlName, false);
       return false;
    }
    else
    {
       SetErrorColor(selectMonthControlName, true);
       return true;
    }
}

//Ajax to check the code

function fnCompareWeekMasterYear(Week, hWeek, Year)
{   

        //var type = document.getElementById("ddlLOV").selectedValue;
        
        CreateXMLHTTP5();
        if(xmlHttp5)
        {            
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "WeekMaster";
            requestURL += "&year=";
            requestURL += Year;
            requestURL += "&week=";
            requestURL += Week;
            requestURL += "&hweek=";
            requestURL += hWeek;
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
              
            xmlHttp5.onreadystatechange=doCurrWeekExists;
            xmlHttp5.open("GET",requestURL,true);
            xmlHttp5.send(null);
        }
        
}



function doCurrWeekExists()
{
   if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {
        if(xmlHttp5.status == 200)
        {        
           
           if(xmlHttp5.responseText == "False")
           {
             document.getElementById("lblYearWeek").innerHTML = ""; 
             return true;                
           }
           else
           {             
            document.getElementById("lblYearWeek").innerHTML = "Week already exists for the Selected Year"; 
            return false; 
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
     
}

