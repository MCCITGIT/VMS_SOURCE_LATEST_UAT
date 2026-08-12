//'*************************************************************
//'Copyright	: AGROIII, MCC, KOLKATA
//'Source	    : Scripts/ValidationFormMenu.js
//'Created Date	: 30-November-2007
//'Created By	: Arun
//'Version	    : R02.00.00
//'Description	: Extent Convert File 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function ValidateFMAControls()
{
    firstErrorControl ="";
    errMsg= "";
    
    ValidateDropDown("ddlFormType",missingFormType)
    
    ValidateRequired("txtFormName",missingFormName)
    
    ValidateRequired("txtFormLink",missingFormLink)
    
    ValidateRequired("txtFormSeq",missingFormSeq)
    
       
    if(firstErrorControl!="")
    {        
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;
        return false;    
    }
    else
    {      
      if( confirm ('Are you sure to submit?'))
      {
        document.getElementById('btnSubmit').disabled=true;
        __doPostBack(document.getElementById('btnSubmit').name,'');
      }
      else
      { 
       return false;
      }
         
    }

}

