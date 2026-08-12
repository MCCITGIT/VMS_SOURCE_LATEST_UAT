
//'**************************************************
//'Copyright	: VMS, MCC, KOLKATA
//'Source	    : Scripts/AjaxServices.vb
//'Created Date	: 16-August-2007
//'Created By	: Saravanan
//'Version	    : R01.00.00
//'Description	: Ajax Script File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************

// JScript File
var xmlHttp5;
var xmlHttp6;
var xmlHttp7;
var xmlHttp8;
var xmlHttp9;
var xmlHttp10;

//Ajax for ProjectCode already exists or not

function compareProjectCode(txtProjectCode)
{    
    CreateXMLHTTP5();
    if(xmlHttp5)
    {            
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "ProjectCode";
        requestURL += "&prjCode=";
        requestURL += txtProjectCode;
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
          
        xmlHttp5.onreadystatechange=doProjectCodeExists;
        xmlHttp5.open("GET",requestURL,true);
        xmlHttp5.send(null);
    }
    
  return true;
}

function CreateXMLHTTP5()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp5=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp5=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp5=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}

function doProjectCodeExists()
{
   if(xmlHttp5.readyState==4 || xmlHttp5.readyState == 'complete')
    {
        if(xmlHttp5.status == 200)
        {        
           if(xmlHttp5.responseText == "True")
           {
             //document.getElementById("divUserIdExistErrorMsg").style.display = 'block';
            //document.getElementById("lblSaveConfirmMsg").innerHTML = "<table>" + userIdExist + "</table>";
            document.getElementById("lblSaveConfirmMsg").innerHTML = "Project Code already exists";
           
            //document.getElementById(clientID + "hdnUserIdCompare").value = "false";
           }
           else
           {
             document.getElementById("lblSaveConfirmMsg").innerHTML = ""; 
              document.getElementById("txtPrjLocation").focus();                   
            //document.getElementById(clientID + "hdnUserIdCompare").value = "true";        
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
    return true; 
}

//End of ProjectCode Exists or not


//Ajax for Plot No Display already exists or not

function comparePlotNoDisplay(txtPlotNoDisplay,txtPlotNo)
{    
    CreateXMLHTTP10();
    if(xmlHttp10)
    {            
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "PlotNoDisplay";
        requestURL += "&CheckPlotNoDisplay=";
        requestURL += txtPlotNoDisplay;
        requestURL += "&CheckPlotNo=";
        requestURL += txtPlotNo;
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
          
        xmlHttp10.onreadystatechange=doPlotNoDisplayExists;
        xmlHttp10.open("GET",requestURL,true);
        xmlHttp10.send(null);
    }
    
  return true;
}

function CreateXMLHTTP10()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp10=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp10=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp10=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}

function doPlotNoDisplayExists()
{
   if(xmlHttp10.readyState==4 || xmlHttp10.readyState == 'complete')
    {
        if(xmlHttp10.status == 200)
        {   
           if(xmlHttp10.responseText == "True")
           {             
                document.getElementById("lblSaveConfirmMsg").innerHTML = "Plot No. already exists";
                return false;
           }
           else 
           {            
                document.getElementById("lblSaveConfirmMsg").innerHTML = "";
                return true;
           }
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
            return true;
        }
    }    
}

//End of Plot No Display Exists or not

//Ajax for Document Type Listing

function populateDocumentType(txtDocType)
{    
    CreateXMLHTTP7();
    if(xmlHttp7)
    {            
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "DocumentType";
        requestURL += "&DocType=";
        requestURL += txtDocType;
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
          
        xmlHttp7.onreadystatechange=doDocumentTypeListing;
        xmlHttp7.open("GET",requestURL,true);
        xmlHttp7.send(null);
    }
    
  return true;
}

function CreateXMLHTTP7()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp7=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp7=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp7=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}

function doDocumentTypeListing()
{
    if(xmlHttp7.readyState==4 || xmlHttp7.readyState == 'complete')
    {
        if(xmlHttp7.status == 200)
        {
           showDocumentType(xmlHttp7.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}

function showDocumentType(result)
{
    try
    {    
        if(result.length > 0)   
        eval("var docTypeArray =" + result);
       else
        var docTypeArray = "";     
        
        
        if(docTypeArray != null && docTypeArray.length > 0)
        {  
     
            document.getElementById('ddlDocType').options.length=0;
            document.getElementById('ddlDocType').options[0] = new Option("Select");
            document.getElementById('ddlDocType').options[0].value = 0;
           
          for (var i=0; i<docTypeArray.length-1; i++)
          {
          
            document.getElementById('ddlDocType').options[i+1] = new Option(docTypeArray[i].DoctypeDescription);            
            document.getElementById('ddlDocType').options[i+1].value = docTypeArray[i].DoctypeValue; 
          }          
        }
        else
        {
        
            document.getElementById('ddlDocType').options.length=0;
            document.getElementById('ddlDocType').options[0] = new Option("Select");
            document.getElementById('ddlDocType').options[0].value = 0;
        }
        
    }
    catch(e)
    {
        
            alert("There was a problem retrieving data from the server." );
    }
}

//End of Document Type Listing


//Ajax for populating Layout Code in on change event

function fnGetLayoutCode(status)
{
    CreateXMLHTTP6();    
    if(xmlHttp6)
    {        
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "Layout";
        requestURL += "&statusId=";
        requestURL += status;
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
         
        xmlHttp6.onreadystatechange=doloadLocation;
        xmlHttp6.open("GET",requestURL,true);
        xmlHttp6.send(null);
    }
}

function CreateXMLHTTP6()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp6=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {       
            xmlHttp6=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp6=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}

function doloadLocation()
{

    if(xmlHttp6.readyState==4 || xmlHttp6.readyState == 'complete')
    {
        if(xmlHttp6.status == 200)
        {
           showLocation(xmlHttp6.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}

function showLocation(result)
{
    try
    {    
        if(result.length > 0)   
        eval("var locationArray =" + result);
       else
        var locationArray = "";     
        
        
        if(locationArray != null && locationArray.length > 0)
        {  
     
            document.getElementById('ddlLayoutCode').options.length=0;
            document.getElementById('ddlLayoutCode').options[0] = new Option("Select");
            document.getElementById('ddlLayoutCode').options[0].value = 0;
           
          for (var i=0; i<locationArray.length-1; i++)
          {
          
            document.getElementById('ddlLayoutCode').options[i+1] = new Option(locationArray[i].Description);            
            document.getElementById('ddlLayoutCode').options[i+1].value = locationArray[i].StatusValueId; 
          }          
          
//          if(strStateId==0)
//          document.getElementById(clientID + 'ddlUserState').value = '0';
//          else  
//          document.getElementById(clientID + 'ddlUserState').value = strStateId;
         
        }
        else
        {
        
            document.getElementById('ddlLayoutCode').options.length=0;
            document.getElementById('ddlLayoutCode').options[0] = new Option("Select");
            document.getElementById('ddlLayoutCode').options[0].value = 0;
        }
        
    }
    catch(e)
    {
        
            alert("There was a problem retrieving data from the server." );
    }
}

function KeyPressNumeric()
{
   if(!(event.keyCode >= 48 && event.keyCode <= 57))
   {
     event.keyCode=0;
   }
}

function fnGetPlotStatus(val)
{
   if(document.getElementById("ddlLayoutCode").value != 0)
   {
        var txtboxval = val;
        var plotCount = document.getElementById('hdnPlotCount').value;
        if(parseInt(txtboxval) > parseInt(plotCount) || parseInt(txtboxval) == 0)
        {
            document.getElementById("lblPlotNoErrMsg").innerHTML = "Invalid Plot Number";
            document.getElementById("btnSubmit").disabled = true;
        }
        else
        {
               var prjCode = document.getElementById("ddlLayoutCode").value;
                if(document.getElementById("txtPlotNo").value != '')
                {
                        document.getElementById("btnSubmit").disabled = true;
                        CreateXMLHTTP8();
                        if(xmlHttp8)
                        {            
                            var requestURL = "AjaxServices.aspx?";
                            requestURL += "Code=";
                            requestURL += "PlotNo";
                            requestURL += "&plotno=";
                            requestURL += txtboxval;
                            requestURL += "&prjCode=";
                            requestURL += prjCode;
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
                              
                            xmlHttp8.onreadystatechange=doLoadPlotNoStatus;
                            xmlHttp8.open("GET",requestURL,true);
                            xmlHttp8.send(null);
                        }
                 }
                 
        }
        
    }
    
    
}


function doLoadPlotNoStatus()
{
   if(xmlHttp8.readyState==4 || xmlHttp8.readyState == 'complete')
    {
        if(xmlHttp8.status == 200)
        {        
            PlotNoexistsfun(xmlHttp8.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }    
}

function PlotNoexistsfun(result)
{

    try
    {         
            if(result.length > 0)   
            {
                eval("var plotArray =" +result);
                
                for (var i=0; i<=plotArray.length-1; i++)
                {
                    if(plotArray[i].PlotNo != '')
                    {
                        document.getElementById("hdnPlotNo").value=plotArray[i].PlotNo;                        
                        document.getElementById("lblPlotNoErrMsg").innerHTML = "";                         
                        document.getElementById("btnSubmit").disabled = false;     
                        document.getElementById("txtProspectName").focus();               
                    }
                }
                
            }else{            
                document.getElementById("hdnPlotNo").value=0;
                document.getElementById("lblPlotNoErrMsg").innerHTML = "Plot Not Vacant";
                document.getElementById("btnSubmit").disabled = true;  
            }            
        
    }
    catch(e)
    {
            alert("There was a problem retrieving data from the server." );
    }
    return true;
}

function CreateXMLHTTP8()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp8=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp8=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp8=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}


function CreateXMLHTTP9()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp9=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp9=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp9=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}


function fnPlotNoDetails(val)
{
 if(document.getElementById("ddlLayoutName").value != 0)
   {
        var txtboxval = val;
        //var plotCount = document.getElementById('hdnPlotCount').value;
//        if(parseInt(txtboxval) > parseInt(plotCount) || parseInt(txtboxval) == 0)
//        {
//            document.getElementById("lblPlotNoErrMsg").innerHTML = "Invalid Plot Number";
//            document.getElementById("btnSubmit").disabled = true;
//        }
//        else
//        {
               var prjCode = document.getElementById("ddlLayoutName").value;
                if(document.getElementById("txtPlotNumber").value != '')
                {
                        CreateXMLHTTP9();
                        if(xmlHttp9)
                        {            
                            var requestURL = "AjaxServices.aspx?";
                            requestURL += "Code=";
                            requestURL += "BookPlotNo";
                            requestURL += "&plotno=";
                            requestURL += txtboxval;
                            requestURL += "&prjCode=";
                            requestURL += prjCode;
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
                              
                            xmlHttp9.onreadystatechange=doLoadPlotNoDetails;
                            xmlHttp9.open("GET",requestURL,true);
                            xmlHttp9.send(null);
                        }
                 }
                 
        //}
        
    }
}

function doLoadPlotNoDetails()
{

    if(xmlHttp9.readyState==4 || xmlHttp9.readyState == 'complete')
    {
        if(xmlHttp9.status == 200)
        {
           plotDetails(xmlHttp9.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}

function plotDetails(result)
{
    try
    {  
    
        if(result.length > 0)   
        {
        eval("var bookingArray =" +result);
        }
       else
       {
        document.getElementById("lblPlotErrMsg").innerHTML = "Invalid Plot Number"; 
        document.getElementById('lblExtent').innerHTML = "";
        document.getElementById('txtRatePerSqft').value = "";
        document.getElementById('lblPlotCost').innerHTML = "";
        document.getElementById('lblStampDuty').innerHTML = "";
        document.getElementById('lblStampDutyPercent').innerHTML = "";
        document.getElementById('txtRegistrationCharge').value = "";
        document.getElementById('lblRegistrationChargePercent').innerHTML = "";
        document.getElementById('txtPattaOtherCharges').value = "";
        document.getElementById('txtGuideLineVal').value = "";
        document.getElementById('lblGuideLineAmt').innerHTML = "";
        document.getElementById('lbltotalCost').innerHTML="";
                
        var bookingArray = "";
        document.getElementById("btnSubmit").disabled = true;
       }
        if(bookingArray != null && bookingArray.length > 0)
        {  
            for (var i=0; i<=bookingArray.length-1; i++)
          {
            if(bookingArray[i].BookingAgreement == '' && bookingArray[i].PriceRange1 != '')
            {
                document.getElementById("btnSubmit").disabled = false;
                document.getElementById("lblPlotErrMsg").innerHTML = "";
                
                document.getElementById('hdnPlotNo').value=bookingArray[i].PlotNo;
                document.getElementById('lblExtent').innerHTML = bookingArray[i].PlotSize;                
                document.getElementById('hdnExtentSqft').value = bookingArray[i].PlotSize;
                document.getElementById('txtRatePerSqft').value = bookingArray[i].PriceRange1;
                document.getElementById('lblPlotCost').innerHTML = parseInt(bookingArray[i].PlotSize)* parseInt(bookingArray[i].PriceRange1);
                document.getElementById('txtGuideLineVal').value = bookingArray[i].GuideLineValue;
                //document.getElementById('lblGuideLineAmt').innerHTML = parseInt(document.getElementById('lblPlotCost').innerHTML)* parseInt(bookingArray[i].GuideLineValue);
                document.getElementById('lblGuideLineAmt').innerHTML = parseInt(bookingArray[i].PlotSize)* parseInt(bookingArray[i].GuideLineValue);
                document.getElementById('lblStampDutyPercent').innerHTML = bookingArray[i].RegCharge1;
                document.getElementById('lblStampDuty').innerHTML = (parseInt(document.getElementById('lblGuideLineAmt').innerHTML)* parseFloat(document.getElementById('lblStampDutyPercent').innerHTML)/100);
                document.getElementById('lblRegistrationChargePercent').innerHTML = bookingArray[i].RegCharge2;
                document.getElementById('txtRegistrationCharge').value = (parseInt(document.getElementById('lblGuideLineAmt').innerHTML)* parseFloat(document.getElementById('lblRegistrationChargePercent').innerHTML)/100);
                document.getElementById('txtPattaOtherCharges').value = bookingArray[i].OtherCharge1;
                document.getElementById('lbltotalCost').innerHTML = parseInt(document.getElementById('lblPlotCost').innerHTML)+parseInt(document.getElementById('lblStampDuty').innerHTML)+parseInt(document.getElementById('txtRegistrationCharge').value)+parseInt(document.getElementById('txtPattaOtherCharges').value);
                FormatCommaSeparator(true,"lblExtent","innerHTML");
                FormatCommaSeparator(true,"lblPlotCost","innerHTML");
                FormatCommaSeparator(true,"lblGuideLineAmt","innerHTML");
                FormatCommaSeparator(true,"lblStampDuty","innerHTML");
                FormatCommaSeparator(true,"lbltotalCost","innerHTML");
                FormatCommaSeparator(true,"txtRatePerSqft");
                FormatCommaSeparator(true,"txtGuideLineVal");
                FormatCommaSeparator(true,"txtRegistrationCharge");
                FormatCommaSeparator(true,"txtPattaOtherCharges");
                
                
//                document.getElementById("txtPaySchedDate1").disabled=false;
//                document.getElementById("txtPaySchedDescription1").disabled=false;
//                document.getElementById("txtPaySchedAmount1").disabled=false;
               

                
                
            }
            else if(bookingArray[i].BookingAgreement != '')
            {
                document.getElementById("lblPlotErrMsg").innerHTML = "Plot Already Booked";
                document.getElementById('lblExtent').innerHTML = "";
                document.getElementById('txtRatePerSqft').value = "";
                document.getElementById('lblPlotCost').innerHTML = "";
                document.getElementById('lblStampDuty').innerHTML = "";
                document.getElementById('lblStampDutyPercent').innerHTML = "";
                document.getElementById('txtRegistrationCharge').value = "";
                document.getElementById('lblRegistrationChargePercent').innerHTML = "";
                document.getElementById('txtPattaOtherCharges').value = "";
                document.getElementById('txtGuideLineVal').value = "";
                document.getElementById('lblGuideLineAmt').innerHTML = "";
                document.getElementById('lbltotalCost').innerHTML="";
                document.getElementById("btnSubmit").disabled = true;
            }
            else if(bookingArray[i].PriceRange1 == '')
            {
                document.getElementById("lblPlotErrMsg").innerHTML = "Pricing not fixed";
                document.getElementById('lblExtent').innerHTML = "";
                document.getElementById('txtRatePerSqft').value = "";
                document.getElementById('lblPlotCost').innerHTML = "";
                document.getElementById('lblStampDuty').innerHTML = "";
                document.getElementById('lblStampDutyPercent').innerHTML = "";
                document.getElementById('txtRegistrationCharge').value = "";
                document.getElementById('lblRegistrationChargePercent').innerHTML = "";
                document.getElementById('txtPattaOtherCharges').value = "";
                document.getElementById('txtGuideLineVal').value = "";
                document.getElementById('lblGuideLineAmt').innerHTML = "";
                document.getElementById('lbltotalCost').innerHTML="";
                document.getElementById("btnSubmit").disabled = true;
            }
            else
            {
                document.getElementById("lblPlotErrMsg").innerHTML = "";
                document.getElementById('lblExtent').innerHTML = "";
                document.getElementById('txtRatePerSqft').value = "";
                document.getElementById('lblPlotCost').innerHTML = "";
                document.getElementById('lblStampDuty').innerHTML = "";
                document.getElementById('lblStampDutyPercent').innerHTML = "";
                document.getElementById('txtRegistrationCharge').value = "";
                document.getElementById('lblRegistrationChargePercent').innerHTML = "";
                document.getElementById('txtPattaOtherCharges').value = "";
                document.getElementById('txtGuideLineVal').value = "";
                document.getElementById('lblGuideLineAmt').innerHTML = "";
                document.getElementById('lbltotalCost').innerHTML="";
                document.getElementById("btnSubmit").disabled = true;
            }    
            
         }
        }
        
    }
    catch(e)
    {
            alert("There was a problem retrieving data from the server." );
    }
}



function KeyPressNumeric1()
{
   if(!((event.keyCode >= 46 && event.keyCode < 47) || (event.keyCode > 47 && event.keyCode <= 57)))
   {
     event.keyCode=0;
   }
}
