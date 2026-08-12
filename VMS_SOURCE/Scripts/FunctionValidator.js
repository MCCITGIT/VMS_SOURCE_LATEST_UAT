

// JScript File

//CheckBox
function ValidateCheckBox(controlName,errorMessage)
{
    var checkbx = document.getElementById(controlName);
    
    if(checkbx.checked)
    {
        return true;
    }
    else
    {        
        return false;
    }
}



//Special characters validation
function ValidateSpecialChars(controlName, errorMessage, condition)
{
    var nonValidCharsRegEx =     "`'~!@#$%^\&*()-_+{\"}|:<>?/=[];,.\\";
    var nonValidCityCharsRegEx = "'~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\";
    var nonValidUserIdRegEx =    "'~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\"; 
    var nonValidNameRegEx = "~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\1234567890"; // For using quote in name
    var nonValidCompanyRegEx =   "~!@#$%^\&*()-_+{\"}|:<>?/=[];,\\";      // For using quote in name
    var nonValidEmailCharsRegEx = "`'~!#$%^\&*()+-{\"}|:<>?/=[];,\\";
    var nonValidOthersCharsRegEx = "`'~!@#$%^\&*()_+{\"}|:<>?/=[];\\1234567890";
    var nonValidPhoneRegEx = "`'~!@#$%^\&*_{\"}|:<>?/=[];,.\\";           //for using ( ) + - in Phone No
    var nonValidTitleRegEx = "`~!@#$%^.<>:?;,";
    var controlID = controlName;
    var charValue = document.getElementById(controlID).value;    
    var charRegEx = "";
    var finalTxtVal = "";
    switch(condition)
        {
          case "all":       charRegEx = nonValidCharsRegEx;
          break;
          case "dot":       charRegEx = nonValidCityCharsRegEx;
          break;
          case "userid":    charRegEx = nonValidUserIdRegEx; 
          break;          
          case "name":      charRegEx = nonValidNameRegEx; 
          break;
          case "company":   charRegEx = nonValidCompanyRegEx; 
          break;          
          case "email":     charRegEx = nonValidEmailCharsRegEx;
          break;
          case "others":    charRegEx = nonValidOthersCharsRegEx;
          break;
          case "phone":   charRegEx = nonValidPhoneRegEx;
          break;
          case "title":     charRegEx = nonValidTitleRegEx;
          break;
                    
        } 
        
    for (i = 0; i < charValue.length; i++) 
        { 
            Char = charValue.charAt(i);
            if (!(charRegEx.indexOf(Char) == -1))
            {  
                //if(firstErrorControl == '')
                firstErrorControl = controlID;      
                errMsg += GetErrorRow(controlID, errorMessage);
                SetErrorColor(controlID, false);
                return false;
            }
            else
            {
                SetErrorColor(controlID, true);                
            }
        }
    return true;   
    }
    
    
    
    
    // Required Field validation
function ValidateRequired(controlName, errorMessage)
{
    
    var errorCode=true;
    var controlID =  controlName;
    var controlObject  = document.getElementById(controlID);
    
    errorCode = IsEmpty(controlObject);
    if(!errorCode)
    {
        //if(firstErrorControl == '')        
            firstErrorControl = controlID;         
           
        errMsg += GetErrorRow(controlID, errorMessage);
        
        SetErrorColor(controlID, false);
        
        return false;
    }
    else
      SetErrorColor(controlID, true);

    return true;
}

function CheckMaxlength(controlName, maxLengthValue, errorMessage)
{

   
    var controlID =  controlName;
    var controlObject  = document.getElementById(controlID).value;
    
    if(controlObject.length > maxLengthValue)
    {
     //if(firstErrorControl == '')        
            firstErrorControl = controlID;         
           
        errMsg += GetErrorRow(controlID, errorMessage);
        
        SetErrorColor(controlID, false);
        
        return false;
    }
    else
    {
      SetErrorColor(controlID, true);
      return true;
    }
}


// Dropdown validation
function ValidateDropDown(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    var dropdown = document.getElementById(controlID);
    var selectedIndex = dropdown.selectedIndex;
    if(selectedIndex==0)
    {    
         //if(firstErrorControl == '') 
         firstErrorControl = controlID;         
         errMsg += GetErrorRow(controlID, errorMessage);
         SetErrorColor(controlID, false);
         return false;
     }
     else
     {
      SetErrorColor(controlID, true);
      return true;
      }
}

//Validate System Date with Addn of 2 dates
function ValidateSystemtwoDate(controlName, errorMessage, getConditionVal)
{
    var getDateVal =  document.getElementById(controlName).value;
    var controlID = controlName;
    
	
	var currDate = dateFormatChange('',1,0);
	var date = dateFormatChange(getDateVal,1,0);
	
	if(getConditionVal != "greaterValidTillDate")
	{    
      if(compareDates(date, "dd/MM/yyyy",currDate, "dd/MM/yyyy")==1) // Call compareDates function in date.js file
      {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;      
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
      }
      else
      {
        SetErrorColor(controlID, true);
        
        return true;
      }
    }
    else
    {  
       
	  	var greaterDate = dateFormatChange('',1,2);
    
       
        if(compareDates1(date, "dd/MM/yyyy",currDate, "dd/MM/yyyy", greaterDate, "dd/MM/yyyy")==1) // Call compareDates function in date.js file
      {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;      
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
      }
      else
      {
        SetErrorColor(controlID, true);
        //errMsg = "";
        //document.getElementById("divErrorMessage").innerHTML += errMsg; 
        return true;
      }
    }
}

/////////////////////////
function dateFormatChange(getDate,i,j)
{
    if(getDate!='')
    {
        var dtCh= "/";
	
	    var pos1=getDate.indexOf(dtCh);
	    var pos2=getDate.indexOf(dtCh,pos1+1);
    	
	    var strDay=getDate.substring(0,pos1);
	    var strMonth=getDate.substring(pos1+1,pos2);
	    var strYear=getDate.substring(pos2+1);
        
        var checkdateObj = new Date(strMonth+"/"+strDay+"/"+strYear);
    }
    else
    {
        var checkdateObj = new Date();
    }
    
    
    
//    var y=checkdateObj.getYear()+"";
//	var M=checkdateObj.getMonth()+i;
//	var d=checkdateObj.getDate()+j;
	var dateVal ="";
	
////	//////////////////////////////////////////////////////////////
	//create the date
//var myDate = new Date();
//add a week
var dd = checkdateObj.setDate(checkdateObj.getDate() + j);
var d = checkdateObj.getDate();
//add a month
var MM = checkdateObj.setMonth(checkdateObj.getMonth() + i);
var M = checkdateObj.getMonth();
//add a year
var yy = checkdateObj.setYear(checkdateObj.getYear());
var y = checkdateObj.getFullYear();
////////////////////////////////////////////////////////
	
	
//	if(parseInt(M) <= 9)
//	    dateVal = d + "/" + "0" + M + "/" + y;
//	else
//	    dateVal = d + "/" + M + "/" + y;
	    
    
    if(parseInt(d) <= 9)
    {
        if(parseInt(M) <= 9)
        {
	        dateVal = "0" + d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       dateVal = "0" + d + "/" + M + "/" + y;
	    }
	}
	else
	{
	    if(parseInt(M) <= 9)
        {
	        dateVal =  d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       dateVal =  d + "/" + M + "/" + y;
	    }
	}
	
	return dateVal;   	
}

////////////////////////////////


//Validate two Date
function ValidatetwoDates(controlName,controlName1, errorMessage)
{
    var date =  document.getElementById(controlName).value;
    var controlID = controlName;
    var fromDate ="";
    var toDate ="";   

    var datefrom = date.split("/");
    var y=datefrom[2];
	var M=datefrom[1];
	var d=datefrom[0];    
    fromDate = d + "/" + M + "/" + y;
	      
    var date1 =  document.getElementById(controlName1).value;    
    var controlID1 = controlName1;
        
    var dateto = date1.split("/");
    var y=dateto[2];
	var M=dateto[1];
	var d=dateto[0];
	toDate = d + "/" + M + "/" + y;
	    
    if(compareDates(fromDate,"dd/MM/yyyy",toDate,"dd/MM/yyyy")==1) // Call compareDates function in date.js file
    {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID1;        
        errMsg += GetErrorRow(controlID1, errorMessage);
        SetErrorColor(controlID1, false);
        return false;
    }
    else
    {
        SetErrorColor(controlID1, true);
        return true;
    }    
}
 // Checks the date format
 function CheckDateFormat(controlName, errorMessage)
 {
    var date =  document.getElementById(controlName).value;
    
    var controlID = controlName;
    var checkDate = isDateValid(date);    
    
    if(checkDate != "invalidYear")
    {
        if(!checkDate)
        { 
          //if(firstErrorControl == '')
          firstErrorControl = controlID; 
          errMsg += GetErrorRow(controlID, errorMessage);
          SetErrorColor(controlID, false);
          return false;
        }   
        else
        {
            SetErrorColor(controlID, true);
            return true;
        }
    }
    else
    {
        //if(firstErrorControl == '')
        firstErrorControl = controlID;    
        errMsg += GetErrorRow(controlID, invalidYear);
        SetErrorColor(controlID, false);
        return false;
    }   
  } 
  
  
  // Checks if the input is a Valid date
function isDateValid(dtStr)
{
   	var daysInMonth = new Array("31","29","31","30","31","30","31","31","30","31","30","31");
	var dtCh= "/";
	
	var pos1=dtStr.indexOf(dtCh);
	var pos2=dtStr.indexOf(dtCh,pos1+1);
	
	var strDay=dtStr.substring(0,pos1);
	var strMonth=dtStr.substring(pos1+1,pos2);
	var strYear=dtStr.substring(pos2+1);	
	
	strYr=strYear;
	
	if (strDay.charAt(0)=="0" && strDay.length>1)
	 strDay=strDay.substring(1);
	
	if (strMonth.charAt(0)=="0" && strMonth.length>1) 
	    strMonth=strMonth.substring(1);
	    
	for (var i = 1; i <= 3; i++)
	 {
		if (strYr.charAt(0)=="0" && strYr.length>1) 
		    strYr=strYr.substring(1);
	 }
	month=parseInt(strMonth);
	day=parseInt(strDay);
	year=parseInt(strYr);
	if (pos1==-1 || pos2==-1)
	{
		return false;
	}
	if (strMonth.length<1 || month<1 || month>12)
	{
		return false;
	}	
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month-1])
	{
		return false;
	}
	if (strYear.length != 4 || year==0)
	{
	    if((parseInt(strYear) < 1900) || (parseInt(strYear) > 2100))
	        return "invalidYear";
//	    else if(parseInt(strYear) > 2100)
//	        return "invalidYear";
	    else
	    
		    return false;
	}
	else
	{	if((parseInt(strYear) < 1900) || (parseInt(strYear) > 2100))
	        return "invalidYear";
//	    else if(parseInt(strYear) > 2100)
//	        return "invalidYear";
        
	}
	
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || !isInteger(stripCharsInBag(dtStr, dtCh)))
	{
		return false;
	}
		
	
return true;
}


// Gets the number of days in february
function daysInFebruary(year)
{
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}



//function for comparing todates
function DateComparision(controlName1, controlName2, errorMessage)
{
   var date1 = document.getElementById(controlName1).value;
   var date2=document.getElementById(controlName2).value;
   var controlID = controlName2;
   var dateObj1 = new Date(date1);
   var dateObj2 = new Date(date2);
   if(dateObj1.valueOf()>dateObj2.valueOf())
   {
   //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}



// Checks if the passed in value is an integer
function isInteger(s)
{
	var i;
    for (i = 0; i < s.length; i++)
    {   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) 
            return false;
    }
    // All characters are numbers.
    return true;
}


// Strips a specified character in a string
function stripCharsInBag(s, bag)
{
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++)
    {   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) 
            returnString += c;
    }
    return returnString;
}



// Sets the Error Color for the specified control
function SetErrorColor(controlID, isCss)
{
    var controlToSet = document.getElementById(controlID);
    if(controlToSet)
      if(isCss)      
        //controlToSet.className = 'validclass';
        controlToSet.style.backgroundColor = "";
      else
        controlToSet.style.backgroundColor = "yellow";
        //controlToSet.className = 'errorclass';        
}



// Sets focus on the specified control id
function SetControlFocus(controlID)
{
var controlToFocus = document.getElementById(controlID);
if(controlToFocus)
        controlToFocus.focus();
}

// Checks if the control has got any value
function IsEmpty(controlID)
{
    var textVal = controlID.value;
    var count = 0;
    
    for (i=0; i <= textVal.length-1; i++)
    {
        var textValValid = textVal.charAt(i);
        
        if (textValValid == " ")
            count += 1;        
    }
    
    if (count == textVal.length)
        return false;
    else
        return true;
}



// Decimal validation
function ValidateDecimal(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsDecimal(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


//checks whether the control with the given id has a valid decimal
function IsDecimal(control)
{
    var controlObject  = document.getElementById(control);
    var isNumber = eighttwoDecimalRegEX.test(controlObject.value);
    if(!isNumber)          
        return false;
    else
        return true;       
}



// Numeric validation
function ValidateNumbers(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsNumber(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


//checks whether the control with the given id is numeric
function IsNumber(control)
{
    var controlObject  = document.getElementById(control);   
    //alert(controlObject);
 
    var isNumber = numRegEx.test(controlObject.value);
    if(!isNumber)          
        return false;
    else
        return true;       
}
// Gets the Error Row in HTML format
function GetErrorRow(controlID, errorMessage)
{   
    return "<tr align='left'><td onmouseover=\"this.style.cursor='hand'\" onclick=\"SetControlFocus('" + controlID + "')\" class='errormsg'>" + errorMessage + "</td></tr>";
}


// Time format
function ValidateTime(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsTime(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


//checks whether the control with the given id is numeric
function IsTime(control)
{
    var controlObject  = document.getElementById(control);
    var isTime = timeRegEx.test(controlObject.value);
    if(!isTime)          
        return false;
    else
        return true;       
}


//RadioButton
function ValidateRadioButton(controlName,errorMessage)
{
    var rbtn = document.getElementById(controlName);
    
    if(rbtn.checked)
    {
        return true;
    }
    else
    {        
        return false;
    }
}


//Allows Alphabet only
function ValidateAlpha(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsAlpha(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


//checks whether the control with the given id is numeric
function IsAlpha(control)
{
    var controlObject  = document.getElementById(control);
    var isAlpha = noalpha.test(controlObject.value);
    if(!isAlpha)          
        return false;
    else
        return true;       
}



// Email format
function ValidateEmail(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsMail(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


//checks whether it is email address or not
function IsMail(control)
{
    var controlObject  = document.getElementById(control);
    var isMail = emailRegEx.test(controlObject.value);
    if(!isMail)          
        return false;
    else
        return true;       
}

// Web format
function ValidateWeb(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsWeb(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


//checks whether it is web address or not
function IsWeb(control)
{
    var controlObject  = document.getElementById(control);
    var isWeb = webRegEx.test(controlObject.value);
    if(!isWeb)          
        return false;
    else
        return true;       
}

//Validate System Date(Sys is Greater)
function ValidateSystemDate(controlName, errorMessage)
{
    var date =  document.getElementById(controlName).value;
    var controlID = controlName;
    
    var checkCurrentdateObj = new Date();
    
    var y=checkCurrentdateObj.getYear()+"";
	var M=checkCurrentdateObj.getMonth()+1;
	var d=checkCurrentdateObj.getDate();
	
	var currDate ="";
	
//	if(parseInt(M) <= 9)
//	    currDate = d + "/" + "0" + M + "/" + y;
    
    if(parseInt(d) <= 9)
    {
        if(parseInt(M) <= 9)
        {
	        currDate = "0" + d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate = "0" + d + "/" + M + "/" + y;
	    }
	}
	else
	{
	    if(parseInt(M) <= 9)
        {
	        currDate =  d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate =  d + "/" + M + "/" + y;
	    }
	}
	    
      if(compareDates(date, "dd/MM/yyyy",currDate, "dd/MM/yyyy")==1) // Call compareDates function in date.js file
      {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;      
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
      }
      else
      {
        SetErrorColor(controlID, true);
        //errMsg = "";
        //document.getElementById("divErrorMessage").innerHTML += errMsg; 
        return true;
      }
}

//does not allow  Alphabet
function ValidateNotAlpha(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsnotAlpha(controlID);
    if(!errorCode)
    { 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
    {
    SetErrorColor(controlID, true);
    } 
}

//checks whether the control with the given id is Alpha
function IsnotAlpha(control)
{
    var controlObject  = document.getElementById(control);
    var isnotAlpha = phoneRegEx.test(controlObject.value);
    if(!isnotAlpha)          
        return false;
    else
        return true;       
}



//Validate System Date(Sys is Lesser)
function ValidateGThanSystemDate(controlName, errorMessage)
{
    var date =  document.getElementById(controlName).value;
    var controlID = controlName;
    
    var checkCurrentdateObj = new Date();
    
    var y=checkCurrentdateObj.getYear()+"";
	var M=checkCurrentdateObj.getMonth()+1;
	var d=checkCurrentdateObj.getDate();
	
	var currDate ="";
	
//	if(parseInt(M) <= 9)
//	    currDate = d + "/" + "0" + M + "/" + y;
    
    if(parseInt(d) <= 9)
    {
        if(parseInt(M) <= 9)
        {
	        currDate = "0" + d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate = "0" + d + "/" + M + "/" + y;
	    }
	}
	else
	{
	    if(parseInt(M) <= 9)
        {
	        currDate =  d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate =  d + "/" + M + "/" + y;
	    }
	}
	    
      if(compareDates(date, "dd/MM/yyyy",currDate, "dd/MM/yyyy")==1) // Call compareDates function in date.js file
      {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;      
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
      }
      else
      {
        SetErrorColor(controlID, true);
        //errMsg = "";
        //document.getElementById("divErrorMessage").innerHTML += errMsg; 
        return true;
      }
}

//Validate System Date(Sys is Lesser)
function ValidateGThanSystemDate1(controlName, errorMessage)
{
    var date =  document.getElementById(controlName).value;
    var controlID = controlName;
    
    var checkCurrentdateObj = new Date();
    
    var y=checkCurrentdateObj.getYear()+"";
	var M=checkCurrentdateObj.getMonth()+1;
	var d=checkCurrentdateObj.getDate();
	
	var currDate ="";
	
//	if(parseInt(M) <= 9)
//	    currDate = d + "/" + "0" + M + "/" + y;
    
    if(parseInt(d) <= 9)
    {
        if(parseInt(M) <= 9)
        {
	        currDate = "0" + d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate = "0" + d + "/" + M + "/" + y;
	    }
	}
	else
	{
	    if(parseInt(M) <= 9)
        {
	        currDate =  d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate =  d + "/" + M + "/" + y;
	    }
	}
	    
      if(compareDates1(date, "dd/MM/yyyy",currDate, "dd/MM/yyyy")==1) // Call compareDates function in date.js file
      {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;      
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
      }
      else
      {
        SetErrorColor(controlID, true);
        //errMsg = "";
        //document.getElementById("divErrorMessage").innerHTML += errMsg; 
        return true;
      }
}


//Day loads depends Month

function DayMonth(strMon, controlName)
{
    var day=new Array('0','31','28','30','31','30','31','30','31','30','31','30','31');
    
    document.getElementById(controlName).options.length=0;
    document.getElementById(controlName).options[0] = new Option("Select");
    document.getElementById(controlName).options[0].value = 0;
    var dayNo;
    for (var i=0; i<day[strMon]; i++)
    {
        if(i < 9)
            dayNo= '0'+ parseInt(i+1);
        else
            dayNo= parseInt(i+1);
            
        document.getElementById(controlName).options[dayNo] = new Option(dayNo);            
        document.getElementById(controlName).options[dayNo].value = dayNo; 
    }          
          
   
}



function KeyPressNumeric()
{
   if(!(event.keyCode >= 48 && event.keyCode <= 57))
   {
     event.keyCode=0;
   }
}

function KeyPressNumeric1()
{
   if(!((event.keyCode >= 46 && event.keyCode < 47) || (event.keyCode > 47 && event.keyCode <= 57)))
   {
     event.keyCode=0;
   }
}


function KeyPressNumerics()
{
   if(!((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 58))
   {
     event.keyCode=0;
   }
}

function KeyPressDecimal()
{
   if(!((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 46 || event.keyCode == 45 ))
   {
     event.keyCode=0;
   }
}

function ChangeHeaderAsNeeded()
{
    // Whenever a checkbox in the GridView is toggled, we need to
    // check the Header checkbox if ALL of the GridView checkboxes are
    // checked, and uncheck it otherwise
    if (CheckBoxIDs != null)
    {
        // check to see if all other checkboxes are checked
        for (var i = 1; i < CheckBoxIDs.length; i++)
        {
            var cb = document.getElementById(CheckBoxIDs[i]);
            if (!cb.checked)
            {
                // Whoops, there is an unchecked checkbox, make sure
                // that the header checkbox is unchecked
                ChangeCheckBoxState(CheckBoxIDs[0], false);
                return;
            }
        }
        
        // If we reach here, ALL GridView checkboxes are checked
        ChangeCheckBoxState(CheckBoxIDs[0], true);
    }
}


 function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            //variable to contain the cell of the grid
            var cell;
            
            if (grid.rows.length > 0)
            {
                //loop starts from 1. rows[0] points to the header.
                for (i=1; i<grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    
                    //loop according to the number of childNodes in the cell
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }



function fnTimeValidation(controlId)
{
    var str = document.getElementById(controlId).value;
    
    if(str != "")
    {
    
            if(str<10)
            {
            var str1 = "0" + str+":"+ "00";
            document.getElementById(controlId).value = str1;
            }
            else if(str < 100)
            {
               str = str + "00";
                           
                            var newstr = "";
                            var c;
                            for (var i = 0; i<str.length; i++) 
                            {
                              c = str.charAt(i);
                              newstr = newstr + c;
                              
                                if (i%2 != 0 && (i<str.length-1)) 
                                {
                                newstr = newstr + ":";
                                }
                            }
                            document.getElementById(controlId).value = newstr;
            }
            else if(str < 1000)
            {
                str = "0" + str;
                           
                            var newstr = "";
                            var c;
                            for (var i = 0; i<str.length; i++) 
                            {
                              c = str.charAt(i);
                              newstr = newstr + c;
                              
                                if (i%2 != 0 && (i<str.length-1)) 
                                {
                                newstr = newstr + ":";
                                }
                            }
                            document.getElementById(controlId).value = newstr;
            }
            else if(str < 10000)
            {
                        var newstr = "";
                            var c;
                            for (var i = 0; i<str.length; i++) 
                            {
                              c = str.charAt(i);
                              newstr = newstr + c;
                              
                                if (i%2 != 0 && (i<str.length-1)) 
                                {
                                newstr = newstr + ":";
                                }
                            }
                            document.getElementById(controlId).value = newstr;
            }
           
      }
                    
}

function ChangeCase(controlID)
{
    document.getElementById(controlID).value = document.getElementById(controlID).value.toUpperCase();
}

function fnDashBoardNewWindow(strUrl)
{
   window.open(strUrl,null,"height=400px,width=500px,status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,modal=yes");
}



// Dropdown validation
function ValidateSameDropDown(controlName1, controlName2, errorMessage)
{
    
    var dropdown1 = document.getElementById(controlName1).value;
    var dropdown2 = document.getElementById(controlName2).value;
    
    if(dropdown1==dropdown2)
    {    
         //if(firstErrorControl == '') 
         firstErrorControl = controlName2;         
         errMsg += GetErrorRow(controlName2, errorMessage);
         SetErrorColor(controlName2, false);
     }
     else
      SetErrorColor(controlName2, true);
}




// Numeric validation
function ValidateWeekNo(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsWeekNo(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


// Checks if the passed in value is an integer
function IsWeekNo(No)
{   
    var weekNo = document.getElementById(No).value;
    if ((weekNo < "0") || (weekNo > "54")) 
        return false;
    
    else
        return true;
}



//Validate Date with Addn of 7 dates
function ValidateWeekDate(controlName1, controlName2, errorMessage, getConditionVal)
{
    var getDateVal1 =  document.getElementById(controlName1).value;
    var controlID1 = controlName1;
    
    var getDateVal2 =  document.getElementById(controlName2).value;
    var controlID2 = controlName2;    

	var fromDate = dateFormatChange(getDateVal1,1,0);
	//alert("from %%"+getDateVal2);
	var toDate = dateFormatChange(getDateVal2,1,0);
	
	if(getConditionVal != "greaterValidTillDate")
	{    
      if(compareDates(date, "dd/MM/yyyy",currDate, "dd/MM/yyyy")==1) // Call compareDates function in date.js file
      {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID1;      
        errMsg += GetErrorRow(controlID1, errorMessage);
        SetErrorColor(controlID1, false);
        return false;
      }
      else
      {
        SetErrorColor(controlID2, true);
        
        return true;
      }
    }
    else
    {  
       
	  	var greaterDate = dateFormatChange(getDateVal1,1,6);
    

//   var date1 = document.getElementById(clientID + controlName1).value;
//   var date2=document.getElementById(clientID + controlName2).value;
//   var controlID = clientID + controlName1;

   var fromDateObj = new Date(fromDate);
   var greatDateObj = new Date(greaterDate);
   var toDateObj = new Date(toDate);
   if(fromDateObj.valueOf() == toDateObj.valueOf())
   {
        alert ('WeekEndDate difference less than 7 days from WeekStartDate') ; 
   }
   else if(toDateObj.valueOf() > greatDateObj.valueOf())
   {   
       firstErrorControl = controlID2;      
       errMsg += GetErrorRow(controlID2, errorMessage);
       SetErrorColor(controlID2, false);
       return false;
   }
   else if(toDateObj.valueOf() == greatDateObj.valueOf())
   {
       return true;
   }
   else
   {   
       alert ('WeekEndDate difference less than 7 days from WeekStartDate') ;  
   }
   }
}



function changeDiscontinuedDate(controlName, controlValue)
{
    if(controlValue != "Yes")
    {
        document.getElementById(controlName).style.display = 'none';
    }
    else
    {
        document.getElementById(controlName).style.display = 'block';
    }  return true; 
}



// Decimal validation
function ValidateThreeDecimal(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsThreeDecimal(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}


//checks whether the control with the given id has a valid decimal
function IsThreeDecimal(control)
{
    var controlObject  = document.getElementById(control);
    var isNumber = sixthreeDecimalRegEX.test(controlObject.value);
    if(!isNumber)          
        return false;
    else
        return true;       
}


function fnNewWindow(strUrl,strtarget)
{
   window.open(strUrl,strtarget,"status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,modal=yes,resizable=yes");
}

function ValidateDropDown1(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    var dropdown = document.getElementById(controlID);
    var selectedIndex = dropdown.selectedIndex;
    if(selectedIndex==0)
    {    
       if (dropdown.value=="Select" || dropdown.value=="0" || dropdown.value=='' )
       {
         //if(firstErrorControl == '') 
         firstErrorControl = controlID;         
         errMsg += GetErrorRow(controlID, errorMessage);
         SetErrorColor(controlID, false);
         return false;
        }
        else
        {
            SetErrorColor(controlID, true);
            return true;
        }
     }
     
     else
     {
      SetErrorColor(controlID, true);
      return true;
      }
}


function ValidateTwoDecimal(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = IsTwoDecimal(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}

function IsTwoDecimal(control)
{
    var controlObject  = document.getElementById(control);
    var isNumber = eighteentwoDecimalRegEx.test(controlObject.value);
    if(!isNumber)
        {                  
//        alert("Enter Valid Budget : Two Digit after Decimal");
//         document.all(control).focus();
//        return false;
     document.getElementById(control).value='0.00';
        }
    else
        return true;
}

function invalidChar(code)
{
var msg='';
 switch (code)
  {
   case 34:
    msg='Invalid Charecter';
   break;
    case 37:
     msg='Invalid Charecter';
   break;
    case 38:
     msg='Invalid Charecter';
   break;
    case 39:
     msg='Invalid Charecter';
   break;
    case 42:
     msg='Invalid Charecter';
   break;
   case 43:
    msg='Invalid Charecter';
   break;
    case 47:
    msg='Invalid Charecter';
   break;
    case 60:
     msg='Invalid Charecter';
   break;
    case 61:
     msg='Invalid Charecter';
   break;
    case 62:
     msg='Invalid Charecter';
   break;
    case 64:
     msg='Invalid Charecter';
   break;
    case 94:
     msg='Invalid Charecter';
   break;
   case 124:
     msg='Invalid Charecter';
   break;
  
  }
  if(! msg==''){
   alert(msg);
  return false;
  }
  else
  return true;
}

function charcheck()
{
 var asciicode=event.keyCode;
 if(invalidChar(asciicode))
 return true;
 else
 return false;
}

function currentSystemDate(){
var checkCurrentdateObj = new Date();
    
    var y=checkCurrentdateObj.getYear()+"";
	var M=checkCurrentdateObj.getMonth()+1;
	var d=checkCurrentdateObj.getDate();
	
	var currDate ="";
	
//	if(parseInt(M) <= 9)
//	    currDate = d + "/" + "0" + M + "/" + y;
    
    if(parseInt(d) <= 9)
    {
        if(parseInt(M) <= 9)
        {
	        currDate = "0" + d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate = "0" + d + "/" + M + "/" + y;
	    }
	}
	else
	{
	    if(parseInt(M) <= 9)
        {
	        currDate =  d + "/" + "0" + M + "/" + y;
	        
	    }else{
	       currDate =  d + "/" + M + "/" + y;
	    }
	}
return currDate;
}

function deleteBrowsHistory() {
      history.go(-1);
}

// Checkbox validation
function ValidateCheckBoxList(controlName, errorMessage) {
    var errorCode = true;
    var controlID = controlName;
    var chkList = document.getElementById(controlID);

    var count = 0;
    for (var i = 0; i < chkList.getElementsByTagName("input").length; i++) {
        var chknode = chkList.getElementsByTagName("input")[i];
        if (chknode != null && chknode.type == "checkbox" && chknode.checked) {
            count = count + 1;
        }
    }
    if (count == 0) {
        //if(firstErrorControl == '') 
        firstErrorControl = controlID;
        errMsg += GetErrorRow(controlID, errorMessage);
        SetErrorColor(controlID, false);
        return false;
    }
    else {
        SetErrorColor(controlID, true);
        return true;
    }
}