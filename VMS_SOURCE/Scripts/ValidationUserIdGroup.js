// JScript File

function userGroupChange(Company)
{
    document.getElementById("hdnUserGroup").value=document.getElementById("ddlUserGroup").value;
    var userGroup=document.getElementById("hdnUserGroup").value;
    var status="Y";
    fnUserIdGet(Company,userGroup,status)
    
}
function userIdChange(Company)
{
    document.getElementById("hdnUserId").value=document.getElementById("ddlUserId").value;
    var userId=document.getElementById("hdnUserId").value;
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

function fnUserIdGet(company,userGroup,status)
{
    if (company!="")
    {
        CreateXMLHTTP();
        
        if (xmlHttp)
        {
            
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "GetUserId";
            requestURL += "&company=";
            requestURL += company;
            requestURL += "&userGroup=";
            requestURL += userGroup;
            requestURL += "&status=";
            requestURL += status;
            
          xmlHttp.onreadystatechange=doloadSchemeDetails;
          xmlHttp.open("GET",requestURL,true);
          xmlHttp.send(null);
          
        }
    }
    else
    {
                document.getElementById('ddlUserId').options.length=0;            
                document.getElementById('ddlUserId').options[0] = new Option("Select");
                document.getElementById('ddlUserId').options[0].value = '';
                document.getElementById('hdnUserId').value = '';
        
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
               document.getElementById('ddlUserId').options.length=0; 
               document.getElementById('ddlUserId').options[0] = new Option("Select");
                document.getElementById('ddlUserId').options[0].value = '';           
                document.getElementById('ddlUserId').options[UserId.length] = new Option("All");
                document.getElementById('ddlUserId').options[UserId.length].value = 'All';
            
                for (var i=0; i<UserId.length-1; i++)
                {            
                    document.getElementById('ddlUserId').options[i+1] = new Option(UserId[i].user_id);            
                    document.getElementById('ddlUserId').options[i+1].value = UserId[i].user_id; 
                } 
             }
             else
            {
                document.getElementById('ddlUserId').options.length=0;            
                document.getElementById('ddlUserId').options[0] = new Option("Select");
                document.getElementById('ddlUserId').options[0].value = '';
            
            }
        }
        else
        {
            //alert("No Item found"); 
                document.getElementById('ddlUserId').options.length=0;            
                document.getElementById('ddlUserId').options[0] = new Option("Select");
                document.getElementById('ddlUserId').options[0].value = '';
                document.getElementById('hdnUserId').value = '';         
        }
    }
    catch(e)
        {        
                alert("There was a problem retrieving data from the server.");
        }
    
}

