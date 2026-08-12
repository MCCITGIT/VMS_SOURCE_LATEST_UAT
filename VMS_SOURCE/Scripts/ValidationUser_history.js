// JScript File

//function validate()
//{
  //  document.getElementById("lblErrorMessage").innerHTML = ""; 
    //firstErrorControl ="";
    //errMsg= "";
      //  CheckDateFormat("txtFromDate", "Enter valid Date") 
        // if(firstErrorControl !="" )
    //{        
      //  SetControlFocus(firstErrorControl);
        //errMsg = "<table>" + errMsg + "</table>";
        //document.getElementById("divErrorMessage").innerHTML = errMsg; 
        //document.getElementById("lblErrorMessage").innerHTML = ""; 
        
        
        //return false;
    //}
    //else
    //{
     //document.getElementById("divErrorMessage").innerHTML = ""; 
     //document.getElementById("lblErrorMessage").innerHTML = "";
     //return true;
      
   // }



//}
//function validate1()
//{
  //  document.getElementById("lblErrorMessage").innerHTML = ""; 
    //firstErrorControl ="";
    //errMsg= "";
      //  CheckDateFormat("txtToDate", "Enter valid Date") 
        // if(firstErrorControl !="" )
   // {        
     //   SetControlFocus(firstErrorControl);
       // errMsg = "<table>" + errMsg + "</table>";
        //document.getElementById("divErrorMessage").innerHTML = errMsg; 
        //document.getElementById("lblErrorMessage").innerHTML = ""; 
        
        //return false;
    //}
    //else
   // {
     // document.getElementById("divErrorMessage").innerHTML = ""; 
     //document.getElementById("lblErrorMessage").innerHTML = "";
     //return true;
    //}



//}
function validatedate()
{
  
  firstErrorControl ="";
   errMsg= "";
  
   if (document.getElementById('txtFromDate').value !="")
   CheckDateFormat("txtFromDate",invaliedate)
   
   if (document.getElementById('txtToDate').value !="")
   CheckDateFormat("txtToDate",invaliedate)
   
   ValidatetwoDates("txtFromDate","txtToDate",ErrorTodate)  
   
   if (firstErrorControl !="")
   
    {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;      
        return false;
    }
    else
    {    
      return true;     
    }
   
}


