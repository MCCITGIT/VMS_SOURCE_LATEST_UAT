// JScript File

function ValidateUserID()
{
        firstErrorControl ="";
        errMsg= "";    
           
        ValidateRequired("txtUserId",missingUserID)
        
        ValidateRequired("txtPassword",missingPWD)
   
           
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("UsrErrMsg").innerHTML = errMsg;
        return false;    
    }
    else
    {      
       
        return true;
      
    }

}


//function ValidatePWD()
//{
// firstErrorControl ="";
//    errMsg= "";
//    
//    ValidateRequired("txtPassword",missingPWD)
//   
//           
//    if(firstErrorControl!="")
//    {        
//        SetControlFocus(firstErrorControl);
//        errMsg = "<table>" + errMsg + "</table>";
//        document.getElementById("PwdErrMsg").innerHTML = errMsg;
//        return false;    
//    }
//    else
//    {      
//       
//       
//          return confirm ('Are you sure to submit?') 
//      
//    }

//}