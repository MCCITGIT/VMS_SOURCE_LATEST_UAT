//jscript 
//Created By Deepak Yadav on 13/12/2011
//For Vendor Master Validation

// JScript File


//Vendor Profile Add

function ValidateVandorUnit()
{
    firstErrorControl ="";
    errMsg= "";
    
   //document.getElementById('btnCheckUnitCode').click()
   
   ValidateRequired("txtUnitCode","Enter Unit Code")
   ValidateRequired("txtUnitName","Enter Unit Name")
   
//   if(document.getElementById("txtWebsite").value != "")
//    ValidateWeb("txtWebsite",invalidWebsite)
   
   if(document.getElementById("txtEmail").value != "")
    ValidateEmail("txtEmail","invalid Email")
   
  
  
   
   ValidateRequired("txtLine1","Enter Line1 Address" )
   
  if(document.getElementById("txtPin").value != "")
        ValidateNumbers("txtPin","invalid Pin")
   
   //if (ValidateRequired("txtbxdate", "Enter Date"))
    if(document.getElementById("txtbxdate").value != "")
      CheckDateFormat("txtbxdate","Enter Date format dd/mm/yyyy")


        
           
    
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;      
        return false;
    }
    else
    {    
       if(confirm ('Are you sure to submit?'))
      {      
        document.getElementById('btnSubmit').style.display="none";
       
        return true;            
      }else{      
            return false;
      }   
   
    }
}





function ValidateSearchInfo()
{
   firstErrorControl ="";
    errMsg= "";
    
    document.getElementById('btnCheckUnitCode').click()
           
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg; 
        
        
        return false;
    }
    else 
    {
   return true
}
        
        
}