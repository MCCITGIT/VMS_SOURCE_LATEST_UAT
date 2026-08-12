//'**************************************************
//'Copyright	: VMS, MCC, KOLKATA
//'Source	    : Scripts/Red.js
//'Created Date	: 16-August-2007
//'Created By	: Saravanan & Arun Kumar
//'Version	    : R01.00.00
//'Description	: Validation for all screens

//'Modified By       Modified On       Version         Reason

//'*************************************************************

// JScript File

var xmlHttp1;

// AJAX for loading Location according to status

function CreateXMLHTTP1()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp1=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp1=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp1=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}



function populateLoc(statusId)
{
    
    var status = statusId;        
        CreateXMLHTTP1();
        if(xmlHttp1)
        {
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "Status";
            requestURL += "&statusId=";
            requestURL += statusId;
            
          xmlHttp1.onreadystatechange=doloadLocation;
          xmlHttp1.open("GET",requestURL,true);
          xmlHttp1.send(null);
        }
}

function doloadLocation()
{
    if(xmlHttp1.readyState==4 || xmlHttp1.readyState == 'complete')
    {
        if(xmlHttp1.status == 200)
        {
           showLocation(xmlHttp1.responseText);
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
     
            document.getElementById('ddlLocation').options.length=0;
            document.getElementById('ddlLocation').options[0] = new Option("Select");
            document.getElementById('ddlLocation').options[0].value = 0;
           
          for (var i=0; i<locationArray.length-1; i++)
          {
          
            document.getElementById('ddlLocation').options[i+1] = new Option(locationArray[i].Description);            
            document.getElementById('ddlLocation').options[i+1].value = locationArray[i].StatusValueId; 
          }          
         
        }
        else
        {
        
            document.getElementById('ddlLocation').options.length=0;
            document.getElementById('ddlLocation').options[0] = new Option("Select");
            document.getElementById('ddlLocation').options[0].value = 0;
        }
        
    }
    catch(e)
    {
        
            alert("There was a problem retrieving data from the server." );
    }
}


// Shows Calendar in a separate window
function showMycalendar(focusControl)
{
    var newWindowObj = show_calendar(focusControl);  
 	newWindowObj.focus();
 	window.onunload = function(){newWindowObj.close()}

}

function fnchkBoxClick()
{

    if(document.getElementById('chkPlotType').checked)
        document.getElementById('ddlDefaultPlotType').disabled = false;
    else
        document.getElementById('ddlDefaultPlotType').disabled = true;
        
    if(document.getElementById('chkPlotSize').checked)
        document.getElementById('txtPlotSize').disabled = false;
    else
        document.getElementById('txtPlotSize').disabled = true;
        
    if(document.getElementById('chkPlotDimension').checked)
        document.getElementById('txtPlotDimension').disabled = false;
    else
        document.getElementById('txtPlotDimension').disabled = true; 
        
    
    
}

function fnchkBoxDocClick()
{

    if(document.getElementById('chkParentDoc').checked)
        document.getElementById('listParentDoc').disabled = false;
    else
        document.getElementById('listParentDoc').disabled = true; 
        
    if(document.getElementById('chkSigningAuth').checked)
        document.getElementById('txtSigningAuth').disabled = false;
    else
        document.getElementById('txtSigningAuth').disabled = true; 
}

function CreateXMLHTTP2()
{
    try
    {   
         // Firefox, Opera 8.0+, Safari    
        xmlHttp2=new XMLHttpRequest();    
    }
    catch (e)
    {    
        // Internet Explorer    
        try
        {
            xmlHttp2=new ActiveXObject("Msxml2.XMLHTTP");      
        }
        catch (e)
        {
            try
            {
                xmlHttp2=new ActiveXObject("Microsoft.XMLHTTP");        
            }                
            catch (e)
            {
                alert("Your browser does not support AJAX!");        
                return false;
            }
        }    
     }    
}


function GetVendorNames(val)
{
    var vendorname = val;        
        CreateXMLHTTP2();
        if(xmlHttp2)
        {
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "VendorDetails";
            requestURL += "&vendorname=";
            requestURL += vendorname;
            
          xmlHttp2.onreadystatechange=doloadVendor;
          xmlHttp2.open("GET",requestURL,true);
          xmlHttp2.send(null);
        }
}

function doloadVendor()
{
    if(xmlHttp2.readyState==4 || xmlHttp2.readyState == 'complete')
    {
        
        if(xmlHttp2.status == 200)
        {
           showVendor(xmlHttp2.responseText);           
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}

function showVendor(result)
{
    try
    {                   
           if(result.length > 0)   
             eval("var vendorArray =" + result);
           else
             var vendorArray = "";   
       
            if(vendorArray != null && vendorArray.length > 0)
            {              
                document.getElementById('ddlVendorName').options.length=0;
                document.getElementById('ddlVendorName').options[0] = new Option("Select");
                document.getElementById('ddlVendorName').options[0].value = 0;
               
                  for (var i=0; i<vendorArray.length-1; i++)
                  {
                    document.getElementById('ddlVendorName').options[i+1] = new Option(vendorArray[i].Description);            
                    document.getElementById('ddlVendorName').options[i+1].value = vendorArray[i].VendorId; 
                    
                  }                        
  
            }
            else
            {            
                document.getElementById('ddlVendorName').options.length=0;
                document.getElementById('ddlVendorName').options[0] = new Option("Select");
                document.getElementById('ddlVendorName').options[0].value = 0;
            }           
        
    }
    catch(e)
    {
            alert("Loading Vendor Error");
    }
}


function fnNewWindow(strUrl)
{
    alert(strUrl);
     //window.open(strUrl,null,"status=no,toolbar=no,menubar= no,location=no");
}