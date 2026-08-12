// JScript File
//Script for find Depot For a region  and Also find Depot code

//Created by srinath on 09.12.2010

function DepotChange()
{
    var SelectedDepot=document.getElementById("ddlDepot").value;
    var Status="Y";
    funGetRegion(SelectedDepot,Status)
}

function funGetRegion(SelectedDepot,Status)
{
    CreateXMLHTTP();
        if (xmlHttp)
            {
                
                var requestURL = "AjaxServices.aspx?";
                requestURL += "Code=";
                requestURL += "GetDepotRegion";
                requestURL += "&SelectedDepot=";
                requestURL +=SelectedDepot;
                requestURL += "&status=";
                requestURL += Status;
                
              xmlHttp.onreadystatechange=doloadSchemeDetails;
              xmlHttp.open("GET",requestURL,true);
              xmlHttp.send(null);
              
            }
        
        else
        {
                    //document.getElementById('ddlUserId').options.length=0;            
                    //document.getElementById('ddlUserId').options[0] = new Option("Select");
                    //document.getElementById('ddlUserId').options[0].value = '';
                    //document.getElementById('hdnUserId').value = '';
            
        }
    
}

//  Function for creating XMLHTTP objects

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
function doloadSchemeDetails()
{
    if(xmlHttp.readyState==4 || xmlHttp.readyState == 'complete')
    {
        if(xmlHttp.status == 200)
        {
           showUserId(xmlHttp.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}
function showUserId(result)
{

    try
    {       
    
        if(result != "")
        {
            eval("var UserId =" + result);
            
            if(UserId != null && UserId.length > 0)
            {   
               //document.getElementById('ddlUserId').options.length=0; 
               //document.getElementById('ddlUserId').options[0] = new Option("Select");
                //document.getElementById('ddlUserId').options[0].value = '';           
                //document.getElementById('ddlUserId').options[UserId.length] = new Option("All");
               // document.getElementById('ddlUserId').options[UserId.length].value = 'All';
            
                for (var i=0; i<UserId.length-1; i++)
                {            
                    document.getElementById('txtRegion').value = (UserId[i].Region);            
                    
                } 
             }
             else
            {
                //document.getElementById('ddlUserId').options.length=0;            
                //document.getElementById('ddlUserId').options[0] = new Option("Select");
                //document.getElementById('ddlUserId').options[0].value = '';
            
            }
        }
        else
        {
            //alert("No Item found"); 
               // document.getElementById('ddlUserId').options.length=0;            
                //document.getElementById('ddlUserId').options[0] = new Option("Select");
                //document.getElementById('ddlUserId').options[0].value = '';
                //document.getElementById('hdnUserId').value = '';         
        }
    }
    catch(e)
        {        
                alert("There was a problem retrieving data from the server.");
        }
    
}


function RegionChange(SelectedRegion)
{
    //var SelectedRegion=document.getElementById("ddlRegion").value;
    var Status="Y";
    
    var region =  document.getElementById('ddlRegion').value;
    document.getElementById('hdnRegion').value=region;
    funGetDepot(SelectedRegion,Status)
}

function funGetDepot(SelectedRegion,Status)
{
    CreateXMLHTTP();
        if (xmlHttp)
            {
                
                var requestURL = "AjaxServices.aspx?";
                requestURL += "Code=";
                requestURL += "GetDepot";
                requestURL += "&SelectedRegion=";
                requestURL +=SelectedRegion;
                requestURL += "&status=";
                requestURL += Status;
                
              xmlHttp.onreadystatechange=doloadRegionDetails;
              xmlHttp.open("GET",requestURL,true);
              xmlHttp.send(null);
              
            }
        
        else
        {
                    document.getElementById('ddlDepot').options.length=0;            
                    document.getElementById('ddlDepot').options[0] = new Option("Select");
                    document.getElementById('ddlDepot').options[0].value = '';
                    document.getElementById('hdnDepotCode').value = '';
            
        }
    
}
function doloadRegionDetails()
{
    if(xmlHttp.readyState==4 || xmlHttp.readyState == 'complete')
    {
        if(xmlHttp.status == 200)
        {
           showDepot(xmlHttp.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}

function showDepot(result)
{

    try
    {       
    
        if(result != "")
        {
            eval("var DepotName =" + result);
            
            if(DepotName != null && DepotName.length > 0)
            {   
                document.getElementById('ddlDepot').options.length=0; 
                document.getElementById('ddlDepot').options[0] = new Option("Select");
                document.getElementById('ddlDepot').options[0].value = '';           
                //document.getElementById('ddlDepot').options[UserId.length] = new Option("All");
                //document.getElementById('ddlDepot').options[UserId.length].value = 'All';
            
                for (var i=0; i<DepotName.length-1; i++)
                {            
                    document.getElementById('ddlDepot').options[i+1] = new Option(DepotName[i].depot_name);
                    document.getElementById('ddlDepot').options[i+1].value = DepotName[i].depot_code;           
                    //document.getElementById('hdnDepotCode').value = DepotName[i].depot_code;
                } 
             }
             else
            {
                document.getElementById('ddlDepot').options.length=0;            
                document.getElementById('ddlDepot').options[0] = new Option("Select");
                document.getElementById('ddlDepot').options[0].value = '';
            
            }
        }
        else
        {
                alert("No Item found"); 
                document.getElementById('ddlDepot').options.length=0;            
                document.getElementById('ddlDepot').options[0] = new Option("Select");
                document.getElementById('ddlDepot').options[0].value = '';
                document.getElementById('hdnDepotCode').value = '';         
        }
    }
    catch(e)
        {        
                alert("There was a problem retrieving data from the server.");
        }
    
}

function DepotCode()
{
    var SelectedDepotCode=document.getElementById("ddlDepot").value;
    document.getElementById("hdnDepotCode").value=SelectedDepotCode;
}

function GetDealer()
{
var SelectedDepotCode=document.getElementById("ddlDepot").value;
document.getElementById("hdnDepotCode").value=document.getElementById("ddlDepot").value;
funGetDealerCode(SelectedDepotCode)
}
function funGetDealerCode(SelectedDepotCode)
{
    CreateXMLHTTP();
        if (xmlHttp)
            {
                
                var requestURL = "AjaxServices.aspx?";
                requestURL += "Code=";
                requestURL += "GetMachineReturnDealer";
                requestURL += "&SelectedDepotCOde=";
                requestURL +=SelectedDepotCode;
                
              xmlHttp.onreadystatechange=doloadDealerDetails;
              xmlHttp.open("GET",requestURL,true);
              xmlHttp.send(null);
              
            }
        
        else
        {
                    document.getElementById('ddlDealer').options.length=0;            
                    document.getElementById('ddlDealer').options[0] = new Option("Select");
                    document.getElementById('ddlDealer').options[0].value = '';
                    document.getElementById('hdnDealer').value = '';
            
        }
    
}
function doloadDealerDetails()
{
    if(xmlHttp.readyState==4 || xmlHttp.readyState == 'complete')
    {
        if(xmlHttp.status == 200)
        {
           showDealer(xmlHttp.responseText);
        }
        else
        {
            alert("There was a problem retrieving data from the server." );
        }
    }
}

function showDealer(result)
{

    try
    {       
    
        if(result != "")
        {
            eval("var DealerCodeName =" + result);
            
            if(DealerCodeName != null && DealerCodeName.length > 0)
            {   
                document.getElementById('ddlDealer').options.length=0; 
                document.getElementById('ddlDealer').options[0] = new Option("Select");
                document.getElementById('ddlDealer').options[0].value = '';           
                //document.getElementById('ddlDepot').options[UserId.length] = new Option("All");
                //document.getElementById('ddlDepot').options[UserId.length].value = 'All';
            
                for (var i=0; i<DealerCodeName.length-1; i++)
                {            
                    document.getElementById('ddlDealer').options[i+1] = new Option(DealerCodeName[i].GetDealerCode);
                    document.getElementById('ddlDealer').options[i+1].value = DealerCodeName[i].GetDealerCode;           
                    //document.getElementById('hdnDepotCode').value = DepotName[i].depot_code;
                } 
             }
             else
            {
                document.getElementById('ddlDealer').options.length=0;            
                document.getElementById('ddlDealer').options[0] = new Option("Select");
                document.getElementById('ddlDealer').options[0].value = '';
            
            }
        }
        else
        {
                alert("No Item found"); 
                document.getElementById('ddlDealer').options.length=0;            
                document.getElementById('ddlDealer').options[0] = new Option("Select");
                document.getElementById('ddlDealer').options[0].value = '';
               // document.getElementById('hdnDealer').value = '';         
        }
    }
    catch(e)
        {        
                alert("There was a problem retrieving data from the server.");
        }
    
}
