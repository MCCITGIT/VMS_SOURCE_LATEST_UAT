//'**************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidateFromToReportDates.js
//'Created Date	: 4-December-2007
//'Created By	: Riddhi
//'Version	    : R02.00.00
//'Description	: 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function ValidateADA()
{
    firstErrorControl ="";
    errMsg= "";
    var From = false;
    var To = false;
    var DelFrom = false;
    var DelTo = false;

    if(ValidateRequired("txtFromDate","Please Enter Booking From Date"))
        if(CheckDateFormat("txtFromDate","Invalid Booking From Date"))
            From = ValidateGThanSystemDate("txtFromDate", "Booking From Date Can not be Greater than today")

        if (ValidateRequired("txtToDate", "Please Enter Booking To Date"))
            if(CheckDateFormat("txtToDate","Invalid Booking To Date"))
                To = ValidateGThanSystemDate("txtToDate", "Booking To Date Can not be Greater than today")       
       
    if( From && To)
        ValidatetwoDates("txtFromDate","txtToDate","From Date Cannot Be Greater Than To date")
        
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        return false;    
    }
  

}

var xmlHttp;
//AJAX for Lead Add page
function CreateXMLHTTP()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}


   function fnDepotNameGet(Company, region) {


       if (region != '') {
           CreateXMLHTTP();

           if (xmlHttp) {
               var requestURL = "AjaxServices.aspx?";
               requestURL += "Code=";
               requestURL += "DepotDetails";
               requestURL += "&company=";
               requestURL += Company;
               requestURL += "&region=";
               requestURL += region;
               requestURL += "&Random=";
               requestURL += Math.random();
               //             requestURL += "&status=";
               //            requestURL += status;                            

               xmlHttp.onreadystatechange = doloadDepotDetails;
               xmlHttp.open("GET", requestURL, true);
               xmlHttp.send(null);
           }
       }
       else {
           document.getElementById('ddlDepot').options.length = 0;
           document.getElementById('ddlDepot').options[0] = new Option("Select");
           document.getElementById('ddlDepot').options[0].value = '';
           document.getElementById('ddlDepot').value = '';

       }
   }

   function doloadDepotDetails() {
       if (xmlHttp.readyState == 4 || xmlHttp.readyState == 'complete') {
           if (xmlHttp.status == 200) {
               showDepotDetails(xmlHttp.responseText);
           }
           else {
               alert("There was a problem retrieving data from the server.");
           }
       }
   }


   function showDepotDetails(result) {

       try {

           if (result != "") {
               eval("var DepotDetails =" + result);

               if (DepotDetails != null && DepotDetails.length > 0) {
                   document.getElementById('ddlDepot').options.length = 0;
                   document.getElementById('ddlDepot').options[0] = new Option("Select");
                   document.getElementById('ddlDepot').options[0].value = '';

                   for (var i = 0; i < DepotDetails.length - 1; i++) {
                       document.getElementById('ddlDepot').options[i + 1] = new Option(DepotDetails[i].DepotName);
                       document.getElementById('ddlDepot').options[i + 1].value = DepotDetails[i].DepotCode;
                   }
               }
               else {
                   document.getElementById('ddlDepot').options.length = 0;
                   document.getElementById('ddlDepot').options[0] = new Option("Select");
                   document.getElementById('ddlDepot').options[0].value = '';

               }
           }
           else {
               //alert("No Item found"); 
               document.getElementById('ddlDepot').options.length = 0;
               document.getElementById('ddlDepot').options[0] = new Option("Select");
               document.getElementById('ddlDepot').options[0].value = '';
               document.getElementById('ddlDepot').value = '';

           }
       }
       catch (e) {
           alert("There was a problem retrieving data from the server.");
       }

   }


