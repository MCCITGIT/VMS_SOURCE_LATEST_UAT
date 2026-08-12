/* By Rajendra Prasad on 08-01-2005 */
var TABLE_EMPTY = "No Records."
var SELECT_TO_EDIT = "Please select a record to edit."
var SELECT_TO_DELETE = "Please select a record to delete."
var ALREADY_INACTIVE = "This record is already in Inactive state."

/*    Author      : Venu Gopal Reddy Chaganti   
    Created on  : 17-Dec-2001                 */

var defaultEmptyOK = false;
var iEmail = "Enter valid E-mail address (yourname@yourprovider.com)";
// Begin of right Click deselect
/* function nothingxxx()
{
	return false;
}

if(document.all)

	document.oncontextmenu=nothingxxx;

function disableselect(e){r
return false
}

function reEnable(){
return true
}

//if IE4+
document.onselectstart=new Function ("return false")

//if NS6
if (window.sidebar){
document.onmousedown=disableselect
document.onclick=reEnable
} */
// End of right Click deselect
function clearText(txt,msg)
{
	alert("Enter Valid "+msg);
	txt.value="";
	txt.focus();
	return false;
}

/*To clear the fields and show the alert message */ 
function clearText1(txt,msg)
{
	alert(msg);
	txt.value="";
	txt.focus();
	return false;
}

/*To find the length of the text field */ 

function textLength(txt,length)
{
	if (emptyCheck(txt)) return true;
	if(txt.value.length!=length)
	{
		txt.focus();
		return false;
	}
	return true;
}

function alphaCheck(txt,msg)
{
	var valid="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ/. \n\r";
	if (emptyCheck(txt)) return true;
	var str=txt.value;
	for(i=0;i<str.length;i++)
		if (valid.indexOf(str.charAt(i))<0) return(clearText(txt,msg));
	return true;
} //end of Function

/*  To accept Only numbers from 0 to 9 */

function numCheck(txt,msg)
{
	if (emptyCheck(txt)) return true;
	var str1=txt.value;
	if(str1.indexOf("e")>=0 || str1.indexOf("E")>=0 || str1.indexOf(".")>=0 || isNaN(str1) || parseInt(str1,10)<0)
		return(clearText(txt,msg));
	return true;
}

/* Empty check  */

function emptyCheck(txt) //start of function
{
	if (txt.value=="") return true;
	str1=txt.value;
	len=txt.value.length;
	j=0;
	for (i=0;i<len;i++)
	{
		if (str1.charAt(i)==' ' || str1.charAt(i)=='\n' || str1.charAt(i)=='\r') j++;
		else break;
	}
	if (j==len)
	{
		txt.value="";
		return true;
	}
	str2="";
	for (i=j;i<len;i++)
	{
		if (str1.charAt(i)==' ' && i<len-1 && str1.charAt(i+1)==' ')
		{
			str2+=" ";
			while (str1.charAt(i)==' ' && i<len) i++;
		}
		if (i<len) str2+=str1.charAt(i);
	}
	len=str2.length;
	if (str2.charAt(len-1)==' ') str2=str2.substring(0,len-1);
	txt.value=str2;
	return false;
} //end of function

/* Alpha Numeric Check  */

function alphaNumCheck(str)
{
  string1='<>|!';
  string2=str.toUpperCase();  
  for (i=0;i<string2.length;i++)
  {
    if (string1.indexOf(string2.charAt(i))>0)
		return false
  }
  return true;
}
/* end of function */

/* Alpha Telephone Check  */
function telephoneCheck(txt,msg)
{
  string1="- +0123456789";
  if (emptyCheck(txt)) return true;
  string2=txt.value.toUpperCase();  
  for (i=0;i<string2.length;i++)
  {
    if (string1.indexOf(string2.charAt(i))<0)
		return(clearText1(txt,msg));
  }
  return true;
}
/* end of function */

function PhoneFaxCheck(txt)
{
  var string1
  var string2
  var i
  string1="()-/+ 0123456789";
  string2=txt.toUpperCase();
  for (i=0;i<string2.length;i++)
  {
    if (string1.indexOf(string2.charAt(i))<0)
		return false;
  }
  return true;
}

function DateDiffJS(startDate,endDate,ResultDate,chkHalfDay) {
	var iOut = 0;
	if((startDate.value != "") && (endDate.value != ""))
	{
	var sd=startDate.value;
	var ed=endDate.value;
	
	sd=sd.substring(3,5)+"/"+sd.substring(0,2)+"/"+sd.substring(6,10);
	ed=ed.substring(3,5)+"/"+ed.substring(0,2)+"/"+ed.substring(6,10);
   	var s = new Date(Date.parse(sd));
   	var e = new Date(Date.parse(ed));   	
    	var bufferA = Date.parse( s ) ;
    	var bufferB = Date.parse( e ) ;
    	var number = bufferB-bufferA ;
    	iOut = parseInt(number / 86400000);
    	iOut = iOut+1;
   	if(chkHalfDay!=0)   	
	    if(chkHalfDay.checked) iOut=iOut-0.5;
	}    	
	ResultDate.value=iOut;
}

function timeCheck(timeStr1,msg) {
	
	var timeStr = timeStr1.value;
	if (timeStr.length == 1 || timeStr.length == 2)
	{
	timeStr=timeStr.concat(":");
	timeStr1.value = timeStr;
	}
	if (timeStr.substring(1,2) == ':')
	{
	addVal = "0";
	timeStr=addVal.concat(timeStr);
	timeStr1.value = timeStr;
	}
	if (timeStr.length == 4 && timeStr.substring(2,3) == ':')
	{
	timeStr=timeStr.concat("0");
	timeStr1.value = timeStr;
	}
	
	var timePat = /^(\d{1,2}):(\d{2})?$/;
	var matchArray = timeStr.match(timePat);
	if(timeStr.length == 0) return true;
	
	if (matchArray == null) {
	alert("'"+msg+"'"+" is not in a valid format.[24HH:MI]");
	timeStr1.value = "";
	timeStr1.focus();
	return false;
	}
	hour = matchArray[1];
	minute = matchArray[2];
	
	if (hour < 0  || hour > 23) {
	alert("Hour must be between 0 and 23");
	timeStr1.value = "";
	timeStr1.focus();
	return false;
	}
	
	if (minute<0 || minute > 59) {
	alert ("Minute must be between 0 and 59.");
	timeStr1.value = "";
	timeStr1.focus();
	return false;
	}
	
	if (timeStr.length == 2 && timeStr.substring(1,2) == ':')
	{
	addVal = "0";
	addVal1 = "00"
	timeStr=addVal.concat(timeStr,addVal1);
	timeStr1.value = timeStr;
	}
	if (timeStr.length == 3 && timeStr.substring(2,3) == ':')
	{
	addVal1 = "00"
	timeStr=timeStr.concat(addVal1);
	timeStr1.value = timeStr;
	}
 }


function timeCheck1(timeStr1,msg) {
	
	var timeStr = timeStr1.value;
	var timePat = /^(\d{1,2}):(\d{2})?$/;
	var matchArray = timeStr.match(timePat);
	if(timeStr.length == 0) return true;
	
	if(timeStr.length < 5) 
	{
	alert("'"+msg+"'"+" is not in a valid format.[24HH:MI]");
	timeStr1.value = "";
	timeStr1.focus();
	return false;
	}
	if (matchArray == null) {
	alert("'"+msg+"'"+" is not in a valid format.[24HH:MI]");
	timeStr1.value = "";
	timeStr1.focus();
	return false;
	}
	hour = matchArray[1];
	minute = matchArray[2];
	
	if (hour < 0  || hour > 23) {
	alert("Hour must be between 0 and 23");
	timeStr1.value = "";
	timeStr1.focus();
	return false;
	}
	
	if (minute<0 || minute > 59) {
	alert ("Minute must be between 0 and 59.");
	timeStr1.value = "";
	timeStr1.focus();
	return false;
	}
}

//Checking whether checkbox is already clicked
function checkboxClick(frm)
{ 	
	var controlIndex;
	var element;
	var elementEmpno;
	var numberOfControls = frm.length;
	var chkVal = 0;
   
for (controlIndex = 0; controlIndex < numberOfControls; controlIndex++)
   {
      element = frm[controlIndex];
      if (element.type == "checkbox")
      {
         if (element.checked == true)
         {
         	chkVal = chkVal +1;
         	//alert(chkVal);
		if (chkVal > 1)
		{
			chkVal = 0;
			//alert("Already Checked");
			//alert(controlIndex);
			frm[controlIndex].checked = false;
		}
         	
         }
      }
   }
}


function numCheckD(txt,msg)
{
  string1=".0123456789\n\r";
  if (emptyCheck(txt)) return true;
  //if (!isNaN(txt.value)) return(clearText1(txt,msg));
  string2=txt.value.toUpperCase();
  for (i=0;i<string2.length;i++)
  {
    if (string1.indexOf(string2.charAt(i))<0)
		return(clearText1(txt,msg));
  }
  return true;
}

function checkBoxClick(frm)
{ 	
	var found1=true;
	var cCount;	
	var element;
	var chkCount=0;
	var NoC = frm.length;
   	
   	for (cCount = 0; cCount < NoC; cCount++)
   	{
   		element = frm[cCount];
		if (element.type == "checkbox")
		
		{
			chkCount=parseInt(chkCount,10) + 1;	
			
		}
   			
   		
   	}
	for (cCount = 0; cCount < NoC; cCount++)
	{
		element = frm[cCount];
		if (element.type == "checkbox")
		{
			if (element.checked == true) found1=false;			
		}
	}
	//alert(Number(chkCount))
	
	if((found1) && (Number(chkCount) > 0))
	{
		alert("Select an entry from the list");
		return false;
	}
	else if (Number(chkCount) == 0)
	{
		alert("No Records available...");
		return false;	
		
	 }	
		
	else
		return true;
}

function PEmail()
{
	return (checkEmail(document.frm.email,true));
} 

function checkEmail (theField, emptyOK)
{   if (checkEmail.arguments.length == 1) emptyOK = defaultEmptyOK;
    if ((emptyOK == true) && (isEmpty(theField.value))) return true;
    else if (!isEmail(theField.value, false)) 
       return warnInvalid (theField, iEmail);
    else return true;
}

/* -----------------------------------------------------------------------------------
	isSEmail()
	    Parameters 
		strEmail- 	Input parameter, this is a string parameter. 	
 	    Description: This will checks whether the given string is a valid email id or not
----------------------------------------------------------------------------------- */
function isSEmail(strMail) 
{
	if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(strMail.value))
	{
		return (true)
	}
	if(strMail.value=="")
	{
		return (true)
	}
		alert("Enter valid Email Address (Yourname@yourdomain.com)");
		strMail.value="";
		strMail.focus();
		return (false)
}
/* -----------------------------------------------------------------------------------
	isEmail()
	    Parameters 
		strEmail- 	Input parameter, this is a string parameter. 	
 	    Description: This will checks whether the given string is a valid email id or not
----------------------------------------------------------------------------------- */

	
function isEmail(strMail) 
{
	if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(strMail))
	{
		return (true)
	}
		return (false)
}

function isEmpty(s)
{   
	return ((s == null) || (s.length == 0))
}

/*function isEmail(str)
 {
   	var supported = 0;
 	 if (window.RegExp)
	 {
		    var tempStr = "a";
		    var tempReg = new RegExp(tempStr);
		    if (tempReg.test(tempStr)) supported = 1;
  	}

	  if (!supported) 
		
		  return (str.indexOf(".") > 2) && (str.indexOf("@") > 0);
		  var r1 = new RegExp("(@.*@)|(\\.\\.)|(@\\.)|(^\\.)\d");
		  var r2 = new RegExp("^.+\\@(\\[?)[a-zA-Z0-9\\-\\.]+\\.([a-zA-Z]{2,3}|[0-9]{1,3})(\\]?)$");

	return (!r1.test(str) && r2.test(str));
}*/


function warnInvalid (theField, s)
{       alert(s);
	theField.value="";
	theField.focus();
   	//theField.select()
    	return false;
}
// Function to compare the given date with Server date and focus to given date
function WithCurrDate(fDate,tDate,msg1)
{
	//alert(tDate.value);
	// Get day,month and year of fDate
	var d1=parseInt(fDate.value.substring(0,2),10);	
	var m1=parseInt(fDate.value.substring(3,5),10);
	var y1=parseInt(fDate.value.substring(6,10),10);
	var message1=msg1;
	var message2='Current Date';
	// Get day,month and year of tDate
	var d2=parseInt(tDate.value.substring(0,2),10);	
	var m2=parseInt(tDate.value.substring(3,5),10);
	var y2=parseInt(tDate.value.substring(6,10),10);
	
	var msg;
	var fld;
	//msg=""+fDate.value+" should be less than or equal to "+d2+"/"+m2+"/"+y2;
	msg=""+ message1+" ( " +fDate.value +" ) should be less than or equal to "+ message2 + "( " +d2+"/"+m2+"/"+y2 +" )";
	fld=fDate;			
	if (y1>y2) return(warnInvalid(fld ,msg));
	else if (y1==y2)
	{
	  if (m1>m2) return(warnInvalid(fld,msg));
	  else if (m1==m2)
	  {
	    if (d1>d2) return(warnInvalid(fld,msg));
	  }
	}
	return true;
}//end of function 

function DateComparision(fDate,tDate,tt,lg,msg1,msg2)
{
	var d1=parseInt(fDate.value.substring(0,2),10);
	var m1=parseInt(fDate.value.substring(3,5),10);
	var y1=parseInt(fDate.value.substring(6,10),10);
	var newDate1 = new Date();
	var d2=newDate1.getDate();
	var m2=newDate1.getMonth()+1;
	var y2=newDate1.getYear();
	var message1=msg1;
	var message2=msg2;
	if(message2=='') 
	{
	 message2='Current Date';
	}	
	if(tt == 2)
	{
		
		var d2=parseInt(tDate.value.substring(0,2),10);	
		var m2=parseInt(tDate.value.substring(3,5),10);
		var y2=parseInt(tDate.value.substring(6,10),10);				
	}
	var msg;
	lg=lg.toUpperCase();
	if(lg=="L")
	{
		if(tt==1)
			//msg=""+fDate.value+" should be less than or equal to "+d2+"/"+m2+"/"+y2;
			msg=""+ message1+" ( " +fDate.value +" ) should be less than or equal to "+ message2 + "( " +d2+"/"+m2+"/"+y2 +" )";
		else
			//msg=""+fDate.value+" should be less than or equal to "+tDate.value;
			msg=""+ message2+" ( "+tDate.value+" ) should be greater than or equal to "+ message1 +" ( " + fDate.value +" )";
	}
	else
	{
		if(tt==1) 
			//msg=""+fDate.value+" should be greater than or equal to "+d2+"/"+m2+"/"+y2;
			msg=""+ message1+" ( " +fDate.value+" ) should be greater than or equal to "+ message2 + "( " +d2+"/"+m2+"/"+y2 +" )";			
		else
			//msg=""+tDate.value+" should be greater than or equal to "+fDate.value;
			msg=""+ message2+" ( " +tDate.value+" ) should be less than or equal to "+ message1 +" ( " +fDate.value +" )";
	}		
	var fld;
	if(tt==1) fld=fDate; else fld=tDate;
	
	if(lg=="L") 
	{		
		if (y1>y2) return(warnInvalid(fld ,msg));
		else if (y1==y2)
		{
			if (m1>m2) return(warnInvalid(fld,msg));
			else if (m1==m2)
			{
				if (d1>d2) return(warnInvalid(fld,msg));
			}
		}		
	}
	else
	{
		if (y1<y2) return(warnInvalid(fld,msg));
		else if (y1==y2)
		{
			if (m1<m2) return(warnInvalid(fld,msg));
			else if (m1==m2)
			{
				if (d1<d2) return(warnInvalid(fld,msg));
			}
		}
	}
	return true;
}//end of function

function DateComparision1(fDate,tDate,tt,lg,msg1,msg2)
{
	var d1=parseInt(fDate.value.substring(0,2),10);	
	var m1=parseInt(fDate.value.substring(3,5),10);
	var y1=parseInt(fDate.value.substring(6,10),10);
	var newDate1 = new Date();
	var d2=newDate1.getDate();
	var m2=newDate1.getMonth()+1;
	var y2=newDate1.getYear();
	var message1=msg1;
	var message2=msg2;
	if(message2=='') 
	{
	 message2='Current Date';
	}	
	if(tt == 2)
	{
		
		var d2=parseInt(tDate.value.substring(0,2),10);	
		var m2=parseInt(tDate.value.substring(3,5),10);
		var y2=parseInt(tDate.value.substring(6,10),10);				
	}
	var msg;
	lg=lg.toUpperCase();
	if(lg=="L")
	{
		if(tt==1)
			msg=""+ message1+" ( " +fDate.value +" ) should be less than or equal to "+ message2 + "( " +d2+"/"+m2+"/"+y2 +" )";
		else
			msg=""+ message2+" ( "+tDate.value+" ) should be greater than or equal to "+ message1 +" ( " + fDate.value +" )";
	}
	else
	{
		if(tt==1) 
			msg=""+ message1+" ( " +fDate.value+" ) should be greater than or equal to "+ message2 + "( " +d2+"/"+m2+"/"+y2 +" )";
		else
			msg=""+ message2+" ( " +tDate.value+" ) should be greater than or equal to "+ message1 +" ( " +fDate.value +" )";
	}		
	var fld;
	if(tt==1) fld=fDate; else fld=tDate;
	
	if(lg=="L") 
	{		
		if (y1>y2) return(warnInvalid(fld ,msg));
		else if (y1==y2)
		{
			if (m1>m2) return(warnInvalid(fld,msg));
			else if (m1==m2)
			{
				if (d1>d2) return(warnInvalid(fld,msg));
			}
		}		
	}
	else
	{
		if (y1<y2) return(warnInvalid(fld,msg));
		else if (y1==y2)
		{
			if (m1<m2) return(warnInvalid(fld,msg));
			else if (m1==m2)
			{
				if (d1<d2) return(warnInvalid(fld,msg));
			}
		}
	}
	return true;
}//end of function

function ConvCompDOB(field1)
{
var fLength = field1.value.length; 
var divider_values = new Array ('-','.','/',' ',':','_',',');
var array_elements = 7; 
var day1 = new String(null); 
var month1 = new String(null); 
var year1 = new String(null); 
var divider1 = null; 
var outdate1 = null; 
var counter1 = 0; 
var divider_holder = new Array ('0','0','0'); 
var s = String(field1.value); 
var minYear;
var maxYear;
var nDate = new Date();
	
minYear = nDate.getYear()-57;
maxYear = nDate.getYear()-20;
//alert(minYear+"aa"+maxYear );
if ( fLength == 0 ) {
   return true;
}


if ( field1.value.toUpperCase() == 'NOW' || field1.value.toUpperCase() == 'TODAY' ) {
   
	var newDate1 = new Date();
  	var myYear1 =newDate1.getYear();
	var myMonth1 = newDate1.getMonth()+1;  
	var myDay1 = newDate1.getDate();	
	field1.value = myDay1 + "/" + myMonth1 + "/" + myYear1;
	fLength = field1.value.length;
	s = String(field1.value)
}


if ( fLength != 0 && (fLength < 6 || fLength > 11) ) {
	invalid_date(field1);
	return false;   
	}


for ( var i=0; i<3; i++ ) {
	for ( var x=0; x<array_elements; x++ ) {
		if ( s.indexOf(divider_values[x], counter1) != -1 ) {
			divider1 = divider_values[x];
			divider_holder[i] = s.indexOf(divider_values[x], counter1);
		   	counter1 = divider_holder[i] + 1;
			break;
		}
 	}
 }

if ( divider_holder[2] != 0 ) {
   invalid_date(field1);
	return false;   
}

if ( divider_holder[0] == 0 && divider_holder[1] == 0 ) { 
   
		if ( fLength == 6 ) {//ddmmyy
   		day1 = field1.value.substring(0,2);
     		month1 = field1.value.substring(2,4);
  			year1 = field1.value.substring(4,6);
  			if ( (year1 = validate_year(year1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
			
		else if ( fLength == 7 ) {//ddmmmy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,5);
  			year1 = field1.value.substring(5,7);
  			if ( (month1 = convert_month(month1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
  			if ( (year1 = validate_year(year1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
		else if ( fLength == 8 ) {//ddmmyyyy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,4);
  			year1 = field1.value.substring(4,8);
			}
		else if ( fLength == 9 ) {//ddmmmyyyy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,5);
  			year1 = field1.value.substring(5,9);
  			if ( (month1 = convert_month(month1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
		
		if ( (outdate1 = validate_date(day1,month1,year1)) == false ) {
   		alert("The value " + field1.value + " is not a vaild date.\n\r" +  
			"Please enter a valid date in the format DD/MM/YYYY");
			field1.focus();
			field1.value='';
			//field1.select();
			return false;
			}
		field1.value = outdate1;
		if((field1.value.substring(6,10)<minYear) || (field1.value.substring(6,10)>maxYear)) 
		 {
		 	 //1752 is the last taken date by SQL Server.
		 	 alert("Year should be greater than "+minYear+" and less than "+maxYear);
		 	 field1.value="";
			 field1.focus();
		 	 return false;
		 }		
		 return true;// All OK
		}
		
if ( divider_holder[0] != 0 && divider_holder[1] != 0 ) { 	
  	day1 = field1.value.substring(0, divider_holder[0]);
  	month1 = field1.value.substring(divider_holder[0] + 1, divider_holder[1]);  	
  	year1 = field1.value.substring(divider_holder[1] + 1, field1.value.length);
	}

if ( isNaN(day1) && isNaN(year1) ) { 
	invalid_date(field1);
	return false;  
   }

if ( day1.length == 1 ) { //Make d day dd
   day1 = '0' + day1;  
}

if ( month1.length == 1 ) {//Make m month mm
	month1 = '0' + month1;   
}

if ( year1.length == 2 ) {//Make yy year yyyy
   if ( (year1 = validate_year(year1)) == false ) {
   	invalid_date(field1);
		return false;  
		}
}

if ( month1.length == 3 || month1.length == 4 ) {//Make mmm month mm
   if ( (month1 = convert_month(month1)) == false) {
   	alert("month1" + month1);
   	invalid_date(field1);
   	return false;  
   }
}

if ( (day1.length == 2 || month1.length == 2 || year1.length == 4) == false) {
   invalid_date(field1);
   return false;
}

if ( (outdate1 = validate_date(day1, month1, year1)) == false ) {
   alert("The value " + field1.value + " is not a vaild date.\n\r" +  
	"Please enter a valid date in the format DD/MM/YYYY");
	field1.focus();
	field1.value='';
	//field1.select();
	return false;
}

field1.value = outdate1;
if((field1.value.substring(6,10)<minYear) || (field1.value.substring(6,10)>maxYear)) 
{
	//1752 is the last taken date by SQL Server.
	alert("Year should be greater than "+minYear+" and less than "+maxYear);
	field1.value="";
	field1.focus();
	return false;
}

return true;

}


function convert_date(field1)
{
var fLength = field1.value.length; 
var divider_values = new Array ('-','.','/',' ',':','_',',');
var array_elements = 7; 
var day1 = new String(null); 
var month1 = new String(null); 
var year1 = new String(null); 
var divider1 = null; 
var outdate1 = null; 
var counter1 = 0; 
var divider_holder = new Array ('0','0','0'); 
var s = String(field1.value); 
var minYear;
var maxYear;
var nDate = new Date();
	
minYear = nDate.getYear()-1;
maxYear = nDate.getYear()+1;
if ( fLength == 0 ) {
   return true;
}


if ( field1.value.toUpperCase() == 'NOW' || field1.value.toUpperCase() == 'TODAY' ) {
   
	var newDate1 = new Date();
  	var myYear1 =newDate1.getYear();
	var myMonth1 = newDate1.getMonth()+1;  
	var myDay1 = newDate1.getDate();	
	field1.value = myDay1 + "/" + myMonth1 + "/" + myYear1;
	fLength = field1.value.length;
	s = String(field1.value)
}


if ( fLength != 0 && (fLength < 6 || fLength > 11) ) {
	invalid_date(field1);
	return false;   
	}


for ( var i=0; i<3; i++ ) {
	for ( var x=0; x<array_elements; x++ ) {
		if ( s.indexOf(divider_values[x], counter1) != -1 ) {
			divider1 = divider_values[x];
			divider_holder[i] = s.indexOf(divider_values[x], counter1);
		   	counter1 = divider_holder[i] + 1;
			break;
		}
 	}
 }

if ( divider_holder[2] != 0 ) {
   invalid_date(field1);
	return false;   
}

if ( divider_holder[0] == 0 && divider_holder[1] == 0 ) { 
   
		if ( fLength == 6 ) {//ddmmyy
   		day1 = field1.value.substring(0,2);
     		month1 = field1.value.substring(2,4);
  			year1 = field1.value.substring(4,6);
  			if ( (year1 = validate_year(year1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
			
		else if ( fLength == 7 ) {//ddmmmy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,5);
  			year1 = field1.value.substring(5,7);
  			if ( (month1 = convert_month(month1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
  			if ( (year1 = validate_year(year1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
		else if ( fLength == 8 ) {//ddmmyyyy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,4);
  			year1 = field1.value.substring(4,8);
			}
		else if ( fLength == 9 ) {//ddmmmyyyy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,5);
  			year1 = field1.value.substring(5,9);
  			if ( (month1 = convert_month(month1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
		
		if ( (outdate1 = validate_date(day1,month1,year1)) == false ) {
   		alert("The value " + field1.value + " is not a vaild date.\n\r" +  
			"Please enter a valid date in the format DD/MM/YYYY");
			field1.focus();
			field1.value='';
			//field1.select();
			return false;
			}
		field1.value = outdate1;
		if((field1.value.substring(6,10)<minYear) || (field1.value.substring(6,10)>maxYear)) 
		{
			//1752 is the last taken date by SQL Server.
			alert("Year should be greater than "+minYear+" and less than "+maxYear);
			field1.value="";
			field1.focus();
			return false;
		}		
		return true;// All OK
		}
		
if ( divider_holder[0] != 0 && divider_holder[1] != 0 ) { 	
  	day1 = field1.value.substring(0, divider_holder[0]);
  	month1 = field1.value.substring(divider_holder[0] + 1, divider_holder[1]);  	
  	year1 = field1.value.substring(divider_holder[1] + 1, field1.value.length);
	}

if ( isNaN(day1) && isNaN(year1) ) { 
	invalid_date(field1);
	return false;  
   }

if ( day1.length == 1 ) { //Make d day dd
   day1 = '0' + day1;  
}

if ( month1.length == 1 ) {//Make m month mm
	month1 = '0' + month1;   
}

if ( year1.length == 2 ) {//Make yy year yyyy
   if ( (year1 = validate_year(year1)) == false ) {
   	invalid_date(field1);
		return false;  
		}
}

if ( month1.length == 3 || month1.length == 4 ) {//Make mmm month mm
   if ( (month1 = convert_month(month1)) == false) {
   	alert("month1" + month1);
   	invalid_date(field1);
   	return false;  
   }
}

if ( (day1.length == 2 || month1.length == 2 || year1.length == 4) == false) {
   invalid_date(field1);
   return false;
}

if ( (outdate1 = validate_date(day1, month1, year1)) == false ) {
   alert("The value " + field1.value + " is not a vaild date.\n\r" +  
	"Please enter a valid date in the format DD/MM/YYYY");
	field1.focus();
	field1.value='';
	//field1.select();
	return false;
}

field1.value = outdate1;
if((field1.value.substring(6,10)<minYear) || (field1.value.substring(6,10)>maxYear)) 
{
	//1752 is the last taken date by SQL Server.
	alert("Year should be greater than "+minYear+" and less than "+maxYear);
	field1.value="";
	field1.focus();
	return false;
}

return true;

}


function convert_date1(field1)
{
var fLength = field1.value.length; 
var divider_values = new Array ('-','.','/',' ',':','_',',');
var array_elements = 7; 
var day1 = new String(null); 
var month1 = new String(null); 
var year1 = new String(null); 
var divider1 = null; 
var outdate1 = null; 
var counter1 = 0; 
var divider_holder = new Array ('0','0','0'); 
var s = String(field1.value); 
var minYear;
var maxYear;
var nDate = new Date();
	
minYear = nDate.getYear()-1;
maxYear = nDate.getYear()+1;
if ( fLength == 0 ) {
   return true;
}


if ( field1.value.toUpperCase() == 'NOW' || field1.value.toUpperCase() == 'TODAY' ) {
   
	var newDate1 = new Date();
  	var myYear1 =newDate1.getYear();
	var myMonth1 = newDate1.getMonth()+1;  
	var myDay1 = newDate1.getDate();	
	field1.value = myDay1 + "/" + myMonth1 + "/" + myYear1;
	fLength = field1.value.length;
	s = String(field1.value)
}


if ( fLength != 0 && (fLength < 6 || fLength > 11) ) {
	invalid_date(field1);
	return false;   
	}


for ( var i=0; i<3; i++ ) {
	for ( var x=0; x<array_elements; x++ ) {
		if ( s.indexOf(divider_values[x], counter1) != -1 ) {
			divider1 = divider_values[x];
			divider_holder[i] = s.indexOf(divider_values[x], counter1);
		   	counter1 = divider_holder[i] + 1;
			break;
		}
 	}
 }

if ( divider_holder[2] != 0 ) {
   invalid_date(field1);
	return false;   
}

if ( divider_holder[0] == 0 && divider_holder[1] == 0 ) { 
   
		if ( fLength == 6 ) {//ddmmyy
   		day1 = field1.value.substring(0,2);
     		month1 = field1.value.substring(2,4);
  			year1 = field1.value.substring(4,6);
  			if ( (year1 = validate_year(year1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
			
		else if ( fLength == 7 ) {//ddmmmy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,5);
  			year1 = field1.value.substring(5,7);
  			if ( (month1 = convert_month(month1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
  			if ( (year1 = validate_year(year1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
		else if ( fLength == 8 ) {//ddmmyyyy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,4);
  			year1 = field1.value.substring(4,8);
			}
		else if ( fLength == 9 ) {//ddmmmyyyy
   		day1 = field1.value.substring(0,2);
  			month1 = field1.value.substring(2,5);
  			year1 = field1.value.substring(5,9);
  			if ( (month1 = convert_month(month1)) == false ) {
   			invalid_date(field1);
				return false; 
				}
			}
		
		if ( (outdate1 = validate_date(day1,month1,year1)) == false ) {
   		alert("The value " + field1.value + " is not a vaild date.\n\r" +  
			"Please enter a valid date in the format DD/MM/YYYY");
			field1.focus();
			field1.value='';
			//field1.select();
			return false;
			}
		field1.value = outdate1;
		/*if((field1.value.substring(6,10)<minYear) || (field1.value.substring(6,10)>maxYear)) 
		{
			//1752 is the last taken date by SQL Server.
			alert("Year should be greater than "+minYear+" and less than "+maxYear);
			field1.value="";
			field1.focus();
			return false;
		}		*/
		return true;// All OK
		}
		
if ( divider_holder[0] != 0 && divider_holder[1] != 0 ) { 	
  	day1 = field1.value.substring(0, divider_holder[0]);
  	month1 = field1.value.substring(divider_holder[0] + 1, divider_holder[1]);  	
  	year1 = field1.value.substring(divider_holder[1] + 1, field1.value.length);
	}

if ( isNaN(day1) && isNaN(year1) ) { 
	invalid_date(field1);
	return false;  
   }

if ( day1.length == 1 ) { //Make d day dd
   day1 = '0' + day1;  
}

if ( month1.length == 1 ) {//Make m month mm
	month1 = '0' + month1;   
}

if ( year1.length == 2 ) {//Make yy year yyyy
   if ( (year1 = validate_year(year1)) == false ) {
   	invalid_date(field1);
		return false;  
		}
}

if ( month1.length == 3 || month1.length == 4 ) {//Make mmm month mm
   if ( (month1 = convert_month(month1)) == false) {
   	alert("month1" + month1);
   	invalid_date(field1);
   	return false;  
   }
}

if ( (day1.length == 2 || month1.length == 2 || year1.length == 4) == false) {
   invalid_date(field1);
   return false;
}

if ( (outdate1 = validate_date(day1, month1, year1)) == false ) {
   alert("The value " + field1.value + " is not a vaild date.\n\r" +  
	"Please enter a valid date in the format DD/MM/YYYY");
	field1.focus();
	field1.value='';
	//field1.select();
	return false;
}

field1.value = outdate1;
/*if((field1.value.substring(6,10)<minYear) || (field1.value.substring(6,10)>maxYear)) 
{
	//1752 is the last taken date by SQL Server.
	alert("Year should be greater than "+minYear+" and less than "+maxYear);
	field1.value="";
	field1.focus();
	return false;
}*/

return true;

}





function convert_month(monthIn) {

var month_values = new Array ("JAN","FEB","MAR","APR","MAY","JUN","JUL","AUG","SEP","OCT","NOV","DEC");

monthIn = monthIn.toUpperCase(); 

if ( monthIn.length == 3 ) {
	for ( var i=0; i<12; i++ ) 
		{
   	if ( monthIn == month_values[i] ) 
   		{
			monthIn = i + 1;
			if ( monthIn != 10 && monthIn != 11 && monthIn != 12 ) 
				{
   			monthIn = '0' + monthIn;
				}
			return monthIn;
			}
		}
	}

else if ( monthIn.length == 4 && monthIn == 'SEPT') {
   monthIn = '09';
   return monthIn;
	}
	
else {
	return false;
	} 
}

function invalid_date(inField) 
{
alert("The value " + inField.value + " is not in a vaild date format.\n\r" + 
        "Please enter date in the format DD/MM/YYYY");
inField.focus();
inField.value='';
//inField.select(); changed to clear the value
return true   
}

function validate_date(day2, month2, year2)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
var DayArray = new Array(31,28,31,30,31,30,31,31,30,31,30,31);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
var MonthArray = new Array("01","02","03","04","05","06","07","08","09","10","11","12");                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
var inpDate = day2 + month2 + year2;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
var filter=/^[0-9]{2}[0-9]{2}[0-9]{4}$/;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          


if (! filter.test(inpDate))                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
  {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
  return false;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
  }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
filter=/01|02|03|04|05|06|07|08|09|10|11|12/ ;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
if (! filter.test(month2))                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
  {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
  return false;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
  }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
var N = Number(year2);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
if ( ( N%4==0 && N%100 !=0 ) || ( N%400==0 ) )                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
  	{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
   DayArray[1]=29;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
  	}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
for(var ctr=0; ctr<=11; ctr++)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
  	{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
   if (MonthArray[ctr]==month2)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
   	{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
      if (day2<= DayArray[ctr] && day2 >0 )                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
        {
        inpDate = day2 + '/' + month2 + '/' + year2;       
        return inpDate;
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
      else                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        return false;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
   	}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
   }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
}

function validate_year(inYear) 
{	
if ( inYear < 10 )
	{
   inYear = "20" + inYear;
   return inYear;
	}
else if ( inYear >= 10 )
	{
   inYear = "20" + inYear;
   return inYear;
	}
else 
	{
	return false;
	}   
}

function calendar(datefield){
  var val="";
  window.open('calendar.asp?fld='+datefield+'&val='+val,'calendar','width=150,height=160,menubar=0,left=350,top=190,screenX=350,screenY=190,scrollbars=0,resizable=1,toolbar=0');
}



/* -----------------------------------------------------------------------------------
	hasAlpha()
	    Parameters 
		vChar	- 	Input parameter, this is a string parameter. 	
 		except	-	This string will be eliminated from checking
 	Description: This will checks whether the given string has any alphabetic 
		     charcter or not. If the given string has atleast one alphabetic 
		     character then this function returns true otherwise false.
   ----------------------------------------------------------------------------------- */
function hasAlpha(vChar, except) {
var blnIsSafe = false;
var Count;
var intLength = vChar.length;

	for (Count = 0; Count < intLength; Count++) {
		// Character code ranges between 65 to 90 (A - Z) and 97 to 122 (a - z)
		if (vChar.substring(Count, Count + 1) >= "A" && vChar.substring(Count, Count + 1) <= "Z" || 
			vChar.substring(Count, Count + 1) >= "a" && vChar.substring(Count, Count + 1) <= "z") 
		{
			if (typeof(except) != "undefined") {
				// If the exception string doesn't contain the current character,
				// we've found an alpha
				if (except.indexOf(vChar.substring(Count,Count+1)) == -1)
					blnIsSafe = true;
			} else {
				blnIsSafe = true;
			}
		}
	}

	return(blnIsSafe);
}

/* -----------------------------------------------------------------------------------
	isValidPh()
	    Parameters 
		PhNumber- 	Input parameter, this is a string parameter. 	
 	    Description: This will checks whether the given string has any alphabetic 
		     and special charcters except space. If the given string has atleast one alphabetic 
		     character or special character then this function returns false otherwise true.
   ----------------------------------------------------------------------------------- */

function isValidPh(PhNumber)
	{
		var schar;
		var sLength = PhNumber.length;
	
		for(i=0;i<sLength;i++)
		{
			schar=PhNumber.charCodeAt(i);
	
			if ((schar>=48 && schar<=57) || (schar==32)||(schar==45)||(schar==43))
			{
			}
	
			else return false;
		}
		
		return true
	}
	
/* -----------------------------------------------------------------------------------
	isValidName()
	    Parameters 
		strText- 	Input parameter, this is a string parameter. 	
 	    Description: This will checks whether the given string has any digits 
		     and special charcters except space,hypen and underscore and dot. If the given string has atleast one numeric 
		     character or special character then this function returns false otherwise true.
   ----------------------------------------------------------------------------------- */

	
function isValidName(strText)
	{
		var schar;
		var sLength = strText.length;
	
		for(i=0;i<sLength;i++)
		{
			schar=strText.charCodeAt(i);
	
			if ((schar>=65 && schar<=90) || (schar>=97 && schar<=122)|| (schar==32))
			{
			}
	
			else return false;
		}
		return true;
	}
/* -----------------------------------------------------------------------------------
	isValidAccNo()
	    Parameters 
		strText- 	Input parameter, this is a string parameter. 	
 	    Description: This will checks whether the given string has any digits 
		     and special charcters except space. If the given string has atleast one numeric 
		     character or special character then this function returns false otherwise true.
   ----------------------------------------------------------------------------------- */

	
function isValidAccNo(strAccNo)
	{
		var schar;
		var sLength = strAccNo.length;
		var count=0;
		
	
		for(i=0;i<sLength;i++)
		{
			schar=strAccNo.charCodeAt(i);
			//if (schar == '+' || schar=='-')	return false;	
			if (schar>=48 && schar<=57)
			{
				
			}
	
			else return false;
		}	
		
		 return true;
	}
	
	
function switchto(src1,src2,on,tname){

		
		
		
		if(on==0){
		        for(i=0;i< document.images.length ;i++){ 
					 if (document.images[i].name==tname){
							document.images[i].src = "../images/"+ src1;
							break;
						}	
		        }	
		} 
		else{ 
		       for(i=0;i < document.images.length;i++){ 
					 if (document.images[i].name==tname){
						document.images[i].src = "../images/"+ src2;
						break;
						}
				}
		}
}

function numericOnly(vChar) // This functions allows to enter only Numeric Values .

{
	if(vChar.length!=0)
	{
		for (i=0;i<vChar.length;i++)
		{
			if ((vChar.charCodeAt(i) >= 48) && (vChar.charCodeAt(i) <= 57))
			{					
				
			}
			else
			{
				return false;
			}
		}
		return true;		 	
	}
	
}

/* ----------------------------------------------------------------------------------------------
	hasNumeric()
	    Parameters
		vChar	-	Input paramter, this is a string paramter.		
 	Description: This checks whether the given string has any alphabetic 
		     charcter or not. If the given string has atleast one numeric 
		     character then this function returns true otherwise false.
  ----------------------------------------------------------------------------------------------*/
function hasNumeric(vChar) {
var blnIsSafe = false;
var Count;
var intLength = vChar.length;

	for (Count = 0; Count < intLength; Count++) {
		// Character code range 48 to 57 (0 - 9)
		if (vChar.substring(Count, Count + 1) >= "0" && vChar.substring(Count, Count + 1) <= "9") {
			return(true);
		}
	}

	return(blnIsSafe);
}

/* -------------------------------------------------------------------------------------------------
	hasSpace()
	    Parameters
		value	-	Input paameter, This is a string paramter.
	Description: This functoin checks whether the given string has any space character or not. If
		     yes, then returns ture otherwise false.
  ------------------------------------------------------------------------------------------------- */
function hasSpaces(value) {
var blnIsSafe = false;
var Count;

	for (Count = 0; Count < value.length; Count++) {
		if (value.substring(Count, Count + 1) == " ") {
			blnIsSafe = true;
			break
		}
	}
	return blnIsSafe;
}


/* -------------------------------------------------------------------------------------------------
	hasSpace()
	    Parameters
		value	-	Input paameter, This is a string paramter.
	Description: This functoin checks whether the given string has any characters other than space. If
		     yes, then returns flase otherwise true.
  ------------------------------------------------------------------------------------------------- */
function hasOnlySpaces(value) {
var blnIsSafe = true;

	for (var Count = 0; Count < value.length; Count++) {
		if (value.substring(Count, Count + 1) != " ") {
			blnIsSafe = false;
			break
		}
	}
	return(blnIsSafe);
}


/* -----------------------------------------------------------------------------------
	hasSymbol()
	    Parameters 
		vChar	- 	Input parameter, this is a string parameter. 	
 		except	-	This string will be eliminated from checking
 	Description: This will checks whether the given string has symbol or not. If the 
		     given string has atleast one symbol then this function returns true 
		     otherwise false.
   ----------------------------------------------------------------------------------- */
function hasSymbol(vChar, except) {
var blnIsSafe = false;
var Count;
var intLength = vChar.length;

	for (Count = 0; Count < intLength; Count++) {
		// Character code ranges outside of 65 to 90 (A - Z) and 97 to 122 (a - z)
		if (!hasAlpha(vChar.substring(Count, Count + 1)) && !hasNumeric(vChar.substring(Count, Count + 1))) {
			if (typeof(except) != "undefined") {
				// If the exception string doesn't contain the current character,
				// we've found a symbol
				if (except.indexOf(vChar.substring(Count,Count+1)) == -1)
					blnIsSafe = true;
			} else {
				blnIsSafe = true;
			}
		}
	}

	return(blnIsSafe);
}

/* -----------------------------------------------------------------------------------------
	getOrder()
		Parameters
			lngNumber	-	Input paramter. This is a numeric number. 
		Description: This function will give you the order for the given number.
		Ex: if input parameter is "1" then output is "1st" like for "2" is "2nd".
  ---------------------------------------------------------------------------------------- */
function getOrder(lngNumber) {
var strPosition = "";

	if (lngNumber == 1) {
		strPosition += "1st";
	} else if (lngNumber == 2) {
		strPosition += "2nd";
	} else if (lngNumber == 3) {
		strPosition += "3rd";
	} else {
		strPosition += lngNumber + "th";
	}

	return(strPosition);
}

/* -------------------------------------------------------------------------------------------
	lTrim()
		Prameters
			Source	-	Input parameter. This is string input. 
			Target	-	Input parameter. 
		Description: This function trims all The target character on the left edge of 
					 the source parameter. 
		Ex: If Source = "CATT" and Target = "T" then the out put is "CA" Like wise same for
			any character.				
 ------------------------------------------------------------------------------------------ */
function lTrim(source, target) {
var Count = 0;
var strTrim;
var retVal;

	if (source.length < 2) {
		return(source);
	} else if (target.length > 1) {
		return(target);
	} else if (left(source, 1) != target) {
		return(source);
	}   

	// Trim the left side of string
	while (source.substring(Count, Count + 1) == target || Count == source.length) {
		Count = Count + 1
	}

	return(source.substring(Count, source.length));
}
  
/* -------------------------------------------------------------------------------------------
	lTrim()
		Prameters
			Source	-	Input parameter. This is string input. 
			Target	-	Input parameter. 
		Description: This function trims all the Target character on the right edge of 
					 the source parameter. 
		Ex: If Source = "SSAND" and Target = "S" then the out put is "AND" Like wise same for
			any character.				
 ------------------------------------------------------------------------------------------ */

function rTrim(source, target) {
var Count;
var clone;
      

	if (source.length < 2) {
		return(source);
	} else if (target.length > 1) {
		return(target);
	} else if (right(source, 1) != target) {
		return(source);
	}

	// Trim the right side of string
	for (Count = source.length; Count > 0; Count--) {
		if (source.substring(Count, Count - 1) == target) {
			clone = source.substring(0, Count - 1);
		} else {
			break
		}
	}

	return(clone);
}

/* -------------------------------------------------------------------------------------------
	oTrim()
		Prameters
			Source	-	Input parameter. This is string input. 
			Target	-	Input parameter. 
		Description: This function trims all the Target character on right and left edges of 
					 the Source parameter. 
		Ex: If Source = "ARCADA" and Target = "A" then the out put is "RCAD" Like wise same 
			for any character.				
 ------------------------------------------------------------------------------------------ */

function oTrim(source, target) {
var Count1, Count2;
var CharCount = 0;
var clone;
var imageStr;

	if (source.length < 2) {
		return(source);
	} else if (target.length > 1) {
		return(target);
	}

	if (left(source, 1) == target) {
		// Trim the left side of string
		for (Count1 = 0; Count1 < source.length; Count1++) {
			if (source.substring(Count1, Count1 + 1) == target) {
				clone = right(source, source.length - (Count1 + 1));
			} else {
				break
			}
		}
	} else {
		clone = source;
	}

	if (right(clone, 1) == target) {
		// Trim the right side of string
		for (Count2 = clone.length; Count2 > 0; Count2--) {
			if (clone.substring(Count2, Count2 - 1) == target) {
				clone = left(clone, Count2 - 1);
			} else {
				break
			}
		}
	}

	return(clone);
}

/* -------------------------------------------------------------------------------------------
	splitt()
		Prameters
			Source		-	Input parameter. This is string input. 
			Delimeter	-	Input parameter. 
		Description: This function gives an array as output for the given source parameter 
					 with the help of delimeter. Follow the example. 
		Ex: If Source = "Banglore;Hyderabad;Chennai;Delhi;Calcutta" and Delimeter = ";" 
			then the output is an array array(0) = "Banglore", array(1) = "Hyderabad"
			array(2) = "Chennai", array(3) = "Delhi", array(4) = "Calcutta"				
 ------------------------------------------------------------------------------------------ */

function splitt(source, delimeter) {
var astrSplitt = new Array();
var blnIsEnd;
var lngCount, lngArrayCount = 0;
var lngMarker = 0;

	for (lngCount = 0; lngCount < source.length + 1; lngCount++) {
		if (delimeter == null) {
			astrSplitt[lngCount - 1] = source.substring(lngCount, lngCount - 1);
		} else if (source.substring(lngCount, lngCount + 1) == delimeter || lngCount == source.length) {
			astrSplitt[lngArrayCount] = lTrim(source.substring(lngMarker, lngCount), " ");
			lngMarker = lngCount + 1;
			lngArrayCount++;
		}
	}

	return(astrSplitt);
}

/* -------------------------------------------------------------------------------------------
	join()
		Prameters
			Source		-	Input parameter. This is an array of string. 
			Delimeter	-	Input parameter. 
		Description: This function gives out a string for the given array, each element is separated
					 with a given delimiter. Follow the example. 
		Ex: If array(0)= "Banglore", array(1) = "Hyderabad", array(2) = "Chennai", array(3) = "Delhi"
				array(4) = "Calcutta" and Delimeter = "#" 
			then the output is "Banglore#Hyderabad#Chennai#Delhi#Calcutta"				
 ------------------------------------------------------------------------------------------ */

function Join(source, delimeter) {
var Count;
var Joined = "";

	if (typeof(source) == "object") {
		for (Count = 0; Count < source.length; Count++) {
			if (Count == (source.length - 1)) {
				Joined += source[Count];
			} else {
				Joined += source[Count] + delimeter;
			}
		}

		return(Joined);
	} else {
		return("");
	}
}

/* -------------------------------------------------------------------------------------------
	left()
		Prameters
			Source		-	Input parameter. This is string input. 
			len			-	Input parameter. Numeric character.
		Description: This function gives a part of the source string of lenght specified from left.
		Ex: If Source = "Application" and len = 4 then output id "Appl"
 ------------------------------------------------------------------------------------------ */

function left(source, len) {
var strImage;

	if (isNaN(len)) {
		strImage = source;
	} else {
		strImage = source.substring(0, len);
	}

	return(strImage);
}


/* -------------------------------------------------------------------------------------------
	right()
		Prameters
			Source		-	Input parameter. This is string input. 
			len			-	Input parameter. Numeric character.
		Description: This function gives a part of the source string of lenght specified from right.
		Ex: If Source = "Application" and len = 4 then output id "tion"
 ------------------------------------------------------------------------------------------ */

function right(source, len) {
var strImage;

	if (isNaN(len)) {
		strImage = strImage = source;
	} else {
		strImage = source.substring(source.length - len, source.length);
	}

	return(strImage);
}


/* -------------------------------------------------------------------------------------------
	cBoolLong()
		Prameters
			varDataType		-	Input parameter. This is may contain true or false. 
		Description: Output from this function is -1 or 0. if input value is true then output is 
					 -1 or if the input value is false then output is 0.
					 This is for converting boolean type to long datatype. 
 ------------------------------------------------------------------------------------------ */

function cBoolLong(varDataType) {
var strDataType = typeof(varDataType);

	if (strDataType == 'boolean') {
		if (varDataType == true) {
			return(-1);
		} else if (varDataType == false) {
			return(0);
		}
	} else {
		alert('Type mismatch.');
		return(0);
	}
}


/* -------------------------------------------------------------------------------------------
	cLongBool()
		Prameters
			varDataType		-	Input parameter. This is may contain -1 or 0. 
		Description: Output from this function is true or false. if input value is -1 then 
					 output is true or if the input value is 0 then output is false.
					 This is for converting long to boolean datatype. 
 ------------------------------------------------------------------------------------------ */
function cLongBool(varDataType) {
var strDataType = typeof(varDataType);

	if (strDataType == 'long' || strDataType == 'integer') {
		if (varDataType == -1) {
			return(true);
		} else if (varDataType == 0) {
			return(false);
		}
	} else {
		alert('Type mismatch.');
		return(false);
	}
}


/* -------------------------------------------------------------------------------------------
	cMonthStr()
		Prameters
			strMonth		-	Input parameter. This is may contain 1 to 12 numbers. 
		Description: This function outputs the month name depending upon the input month number. 
					 This is for converting month number to it's name. 
 ------------------------------------------------------------------------------------------ */

function cMonthStr(strMonth) {

	if (strMonth == "1") {
		return("January");
	} else if (strMonth == "2") {
		return("February");
	} else if (strMonth == "3") {
		return("March");
	} else if (strMonth == "4") {
		return("April");
	} else if (strMonth == "5") {
		return("May");
	} else if (strMonth == "6") {
		return("June");
	} else if (strMonth == "7") {
		return("July");
	} else if (strMonth == "8") {
		return("August");
	} else if (strMonth == "9") {
		return("September");
	} else if (strMonth == "10") {
		return("October");
	} else if (strMonth == "11") {
		return("November");
	} else if (strMonth == "12") {
		return("December");
	}
}


/* -------------------------------------------------------------------------------------------
	isleapYear()
		Prameters
			lngYear		-	Input parameter. This is may contain year value. 
		Description: This function tells whether the give year is leap year or not. If the 
					 supplied year is leap year then the output is true otherwise false.
 ------------------------------------------------------------------------------------------ */

function isleapYear(lngYear) {
InitLeapYear = 1880;	// Initial/base (leap) year to start from.
var blnIsSafe = false;
var lngSpan = 0;
var lngCount;

	lngSpan = lngYear - InitLeapYear;

	for (lngCount = 0; lngCount < lngSpan + 4; lngCount = lngCount + 4){
		if (lngSpan == lngCount) {
			blnIsSafe = true;
			break
		} else {
			blnIsSafe = false;
		}
	}

	return(blnIsSafe);
}

/* -----------------------------------------------------------------------------------
	parseDate()
		Parameters
			day				-	Integer, Returns the day number.
			month			-	Integer, Returns the month number.
			year			-	Integer, Returns the year number.
		Returns
			0	-	parameters are OK and meets requirements.
			1	-	the year contains less or more digits required.
			2	-	the date, month and year contain letters and/or symbols.
			3	-	the date contain letters and/or symbols.			4	-	the month contain letters and/or symbols.
			5	-	the year contain letters and/or symbols.
			6	-	the month contains less or more days than there actually is.
   ----------------------------------------------------------------------------------- */
function parseDate(day, month, year) {
var blnIsSafe = true;
var blnLeapYear = false;
var ErrorMsg = "";

	blnLeapYear = isleapYear(year);

	// Check for alphabetic and symbollic character(s) within variable
	if (hasAlpha(year) == true || hasAlpha(day) == true || hasSymbol(year) == true || hasSymbol(day) == true) {
		ErrorMsg += "  -  either the date or the year contain letters and/or symbols, these must contain numbers only.\n";
		blnIsSafe = false;
	}
	// Check that the year is consistent to 4 characters in length
	if (year.length != 4) {
		ErrorMsg += "  -  you entered a year that is not 4 digits in length (\'YYYY\').\n";
		blnIsSafe = false;
	}

	if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 ||month == 10 || month == 12) {
		if ((day > 0 && day < 32) == false) {
			ErrorMsg += "  -  there are only 31 days in " + cMonthStr(month) + ".\n";
			blnIsSafe = false;
		}
	} else if (month == 4 || month == 6 || month == 9 || month == 11) {
		if ((day > 0 && day < 31) == false) {
			ErrorMsg += "  -  there are only 30 days in " + cMonthStr(month) + ".\n";
			blnIsSafe = false;
		}
	} else if (month == 2) {
		if (blnLeapYear && day > 29) {
			ErrorMsg += "  -  February, " + year + " has only 29 days.\n";
			blnIsSafe = false;
		} else if (!blnLeapYear && day > 28) {
			ErrorMsg += "  -  February, " + year + " has only 28 days.\n";
			blnIsSafe = false;
		}
	}

	if (!blnIsSafe) {
		return(ErrorMsg);
	} else {
		return(blnIsSafe);
	}
}

function hasAtSymbol(vChar) {
var blnIsSafe;
var intLength = vChar.length;
var Count;

	for (Count = 0; Count < intLength; Count++) {
		if (vChar.substring(Count, Count + 1) == "@") {
			return(true);
		} else {
			blnIsSafe = false;
		}
	}

	return(blnIsSafe);
}


function strip(source, delimeter) {
var Count;
var startMark = 0, endMark = source.length;
var imageStr = source;

	if (delimeter == null || delimeter.length == 0) {
		return(source);
	} else {
		Count = startMark;

		while(Count < endMark) {
			if (imageStr.substring(Count, delimeter.length) == delimeter) {
				imageStr += source.substring(startMark, Count);
				Count = Count + delimeter.length;
				startMark = Count;
			} else {
				Count++;
			}
		}

		if (startMark < endMark) {
			imageStr = source.substring(startMark, endMark);
		}

		return(imageStr);
	}
}

function lastChar(value) {
var Count;
var charCount = 0;

	if (hasAlpha(value)) {
		for (Count = 0; Count < value.length + 1; Count++) {
			// Character code ranges between 65 to 90 (A - Z) and 97 to 122 (a - z)
			if (value.substring(Count, Count + 1) >= "A" && value.substring(Count, Count + 1) <= "Z" || 
				value.substring(Count, Count + 1) >= "a" && value.substring(Count, Count + 1) <= "z") {
				charCount++
			} else {
				break 
			}
		}
	}

	return (charCount);
}

function LastNumber(value) {
var Count;
var charCount = 0;

	if (hasNumeric(value)) {
		for (Count = 0; Count < value.length + 1; Count++) {
			// Character code range 48 to 57 (0 - 9)
			if (value.substring(Count, Count + 1) >= "0" && value.substring(Count, Count + 1) <= "9") {
				charCount++
			} else {
				break 
			}
		}
	}

	return (charCount);
}


/* -----------------------------------------------------------------------------------
	parseCurrency(value, params)
		Parameters
			value		-	String, Returns the object containing the array of options.
			params		-	String, Returns defined parameters on how textbox is 
							interrogated.
							Parameter options...
								symbol:a		-	Sets the currency symbol to to be used.
								dad:[n]			-	Digits After Decimal, Sets the number 
													of digits after the decimal point.
								dig:[n]			-	Digits In Group, Determines the number of  
													digits in currency to group.
								default:[n]		-	Sets the default currency value to return.
		Returns
			Value formatted to its respective currency else false.
   ----------------------------------------------------------------------------------- */
function parseCurrency(value, params) {
var acurAmount = new Array();
var aParams = new Array();
var aElement = new Array();
var blnRounded = false;
var curAmount = 0;
var curSymbol = "";
var curDef = 0;
var digitsAftDeci = 2;
var digitsInGroup = 0;
var lngCount;

	// If any, breakdown parameter string into an array.
	aParams = splitt(params, ";");

	if (typeof(params) != "undefined") {
		for (lngCount = aParams.length; lngCount > 0; lngCount--) {

			aElement = splitt(aParams[(lngCount - 1)], ":");

			if (aElement[0] == "symbol") {
				if (aElement[1] != "") curSymbol = aElement[1];
			} else if (aElement[0] == "rounded") {
				blnRounded = true;
			} else if (aElement[0] == "dad") {
				if (aElement[1] != "" || !isNaN(aElement[1])) digitsAftDeci = parseInt(aElement[1]);
			} else if (aElement[0] == "dig") {
				if (aElement[1] != "" || !isNaN(aElement[1])) digitsInGroup = parseInt(aElement[1]);
			} else if (aElement[0] == "default") {
				if (aElement[1] != "" || !isNaN(aElement[1])) curDef = parseInt(aElement[1]);
			}
		}
	}

	// Currency field is not needed but value has been added anyway.
	if (value.length > 0) {
		// Determine if the currency symbol is being used.
		if (value.substring(0, curSymbol.length) == curSymbol) {
			// Remove currency sybmbol.
			value = strip(value, curSymbol);
		} else if (curSymbol == "") {
			if (isNaN(value.substring(0, curSymbol.length))) return(curSymbol + curDef);
		} else {
		/* Check for any alphabetic and/or symbollic characters 
		   (excluding £, commas and decimal). */
			if (hasSymbol(value, curSymbol + ".") || hasAlpha(value)) return(curSymbol + curDef);
		}


		/*
			Checks currency format, making sure commas and decimal points are in the
			right posistions.
		*/
		acurAmount = splitt(value, ",");

		// Check the format of value.
		if (acurAmount.length > 1) {
			for (lngCount = 0; lngCount < acurAmount.length; lngCount++) {
				if (lngCount != 0 && lngCount != (acurAmount.length - 1)) {
					if (digitsInGroup != 0) {
						if (acurAmount[lngCount].length < digitsInGroup) {
							return(curSymbol + curDef);
							break
						}
					}
				} else if (lngCount == (acurAmount.length - 1)) {
					if (acurAmount[lngCount].length == digitsInGroup) {
						if (acurAmount[lngCount].substring(0, 1) == ".") {
							if (acurAmount[lngCount].substring(1, acurAmount[lngCount].length) == digitsAftDeci) {
								if (isNaN(acurAmount[lngCount].substring(1, 3))) {
									return(curSymbol + curDef);
									break
								}
							} else {
								return(curSymbol + curDef);
								break
							}
						} else if (isNaN(acurAmount[lngCount])) {
							return(curSymbol + curDef);
							break
						}
					}
				}
			}
		}

		if (blnRounded) {
			curAmount = parseInt(value);
		} else {
			curAmount = parseFloat(value);
		}
	}

	return(curSymbol + curAmount);
}
function alreadyChecked(frm,chk)
{
var numberOfControls = frm.length;
var controlIndex;
var element;
var status = chk.checked;
for (controlIndex = 0; controlIndex < numberOfControls; 
controlIndex++)

{
element = frm[controlIndex];
element.checked=false;

}

if (status == false)
{chk.checked= false;}
else
{
chk.checked = true;
}

}
function replChar(string1)
   {
   var Values;
   var i;
   var  n;
   Values="";
   char1="'";
   char2="`";
   for (i=0,n=string1.length;i<n;i++)
    {
     if (string1.charCodeAt(i)==char1.charCodeAt(0))
       Values=Values + char2;
     else
       Values=Values + string1.charAt(i);
    }
    return(Values);
  }


function isFloat (s)

{   var i;
    var decimalPointDelimiter=".";
    var seenDecimalPoint = false;

    if (isEmpty(s)) 
       if (isFloat.arguments.length == 1) return defaultEmptyOK;
       else return (isFloat.arguments[1] == true);

    if (s == decimalPointDelimiter) return false;

    // Search through string's characters one by one
    // until we find a non-numeric character.
    // When we do, return false; if we don't, return true.

    for (i = 0; i < s.length; i++)
    {   
        // Check that current character is number.
        var c = s.charAt(i);

        if ((c == decimalPointDelimiter) && !seenDecimalPoint) seenDecimalPoint = true;
        else if (!isDigit(c)) return false;
    }

    // All characters are numbers.
    return true;
}
// This function is Added on 5-July-2002
/*
* Method checkDecimal()
* @param beforeDeci - Input parameter, this is a number. Specifies Max number of digits before '.' (dot)
* @param afterDeci - Input parameter, this is a number. Specifies Max number of digits after '.' (dot)
* @param value - Input parameter, this is value that need to be validated

* Description: Checks whether the given value is in specified format with decimal positions.
**/
function focus(msg,txtField)
{
 alert(msg);
 txtField.select();	
 return;	
}

function checkDecimal(beforeDeci, afterDeci, cntl)
{	
		var decAmount = cntl.value.toString().replace(/\$|\,/g,'');
	
		if(isNaN(decAmount) || decAmount.indexOf('e')>0 || decAmount.indexOf('E')>0)
		{
		return false;
		}
		if(noOfOccur(cntl, '.') > 1)
		{
		return false;
		}
		decAmount = new String(decAmount);
		var chrAt0 = decAmount.charAt(0);
		if (chrAt0=='+' || chrAt0 == '-') {
		return false;
		}
		
		j = decAmount.indexOf(".");
			if (j==-1)
			{
				if (decAmount.length<=parseInt(beforeDeci,10))
					return true
				else
					return false	
			}
			else
			{
				if (j==0)
					return false;
				if (j == decAmount.length - 1) 
					return false;
				if (j > parseInt(beforeDeci,10))
				{
					return false;
				} 
				else if(decAmount.length - (j + 1) > parseInt(afterDeci,10))
				{
					return false;
				}
			}	
		 
return true;
}//end of checkDecimal()
		
function noOfOccur(strValue,searchChar){
var count = 0;
for(h=0; h<strValue.length; h++){
if(strValue.charAt(h) == searchChar){
count = count + 1;
}
}
return count;
}




			//document.onmouseover = MouseHandler;
			//document.onmouseout = MouseHandler;
			//document.onmousedown = DownHandler;
			//document.onmouseup = UpHandler;

			var nScrollDelay = 25
			function IncreaseSpeed(marquee)
			{
				if(nScrollDelay > 10 && nScrollDelay<10000)
				{
					nScrollDelay -= 5 ;
					marquee.scrollDelay = nScrollDelay;
					marquee.trueSpeed = 1;	
					marquee.scrollAmount = 1;
					SaveScrollDelay();
				}
				else if(nScrollDelay > 10000)
				{
					nScrollDelay = 25 ;
					marquee.scrollDelay = nScrollDelay;
					marquee.trueSpeed = 1;	
					marquee.scrollAmount = 1;
					SaveScrollDelay();
				}
			}
			function StopTicker(marquee)
			{
			     alert(marquee.name);
				if(nScrollDelay==100000 )
				{
					marquee.scrollDelay = 20;
					nScrollDelay=marquee.scrollDelay;
				}
				else
				{
					marquee.scrollDelay = 100000;
					nScrollDelay=marquee.scrollDelay;
					SaveScrollDelay();
				}
				return false;		
			}


			function DecreaseSpeed(marquee)
			{				
				if(nScrollDelay < 40)
				{
					nScrollDelay += 5;
					marquee.scrollDelay = nScrollDelay ;
					marquee.trueSpeed = 1;
					marquee.scrollAmount = 1;
					SaveScrollDelay();
				}
				else if(nScrollDelay > 10000)
				{
					nScrollDelay = 25 ;
					marquee.scrollDelay = nScrollDelay;
					marquee.trueSpeed = 1;	
					marquee.scrollAmount = 1;
					SaveScrollDelay();
				}

			}

function SaveScrollDelay()
{
	var exp = new Date();
	var tenYearsFromNow = exp.getTime() + (10 * 365 * 24 * 60 * 60 * 1000);				
	exp.setTime(tenYearsFromNow);
	document.cookie = "scrolldelay=" + nScrollDelay + "; expires=" + exp.toGMTString();				
}

  /**
* Method isValidDate()
* Returns true if date value is valid, otherwise false.
*
*@param strDay Input parameter,this is day of the Date.
*@param strMonth Input parameter,this is month of the Date.
*@param strYear Input parameter,this is year of the Date.
*/
//function isValidDate(strDay, strMonth, strYear) 
function isValidDate(strDateVal) {
var strDate;
var intday;
var intMonth;
var intYear;
strDate=strDateVal.value;
intday   = parseInt(strDate.substring(3,5),10);
intMonth = parseInt(strDate.substring(0,2),10);
intYear  = parseInt(strDate.substring(6,10),10);

//intday = parseInt(strDay, 10);
if (isNaN(intday)) {
alert("Day value entered is invalid.\nEnter a valid number value.");
return false;
}

//intMonth = parseInt(strMonth, 10);
if (isNaN(intMonth)) {
alert("Month value entered is invalid.\nEnter a valid number value between 1 and 12");
return false;
}

//intYear = parseInt(strYear, 10);
if (isNaN(intYear)) {
alert("Year value entered is invalid.\nEnter a valid year value in YYYY format.");
return false;
}

if (intMonth>12 || intMonth< 1) {
alert("Month value should be between 1(January) and 12(December)");
return false;
}

if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1)) {
alert("Date value should be between 1 and 31 for the month specified.");
return false;
}

if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1)) {
alert("Date value should be between 1 and 30 for the month specified.");
return false;
}

if (intMonth == 2) {
if (intday < 1) {
alert("Month specified is Feb.And Year is not a leap Year.\nDate value should be between 1 and 28.");
return false;
}

if (isLeapYear(intYear) == true) {
if (intday > 29) {
alert("The Year specified is a LeapYear and month specified is Feb.\nDate value should be between 1 and 29.");
return false;
}
} else {
if (intday > 28) {
alert("Month specified is Feb.And Year is not a leap Year.\nDate value should be between 1 and 28.");
return false;
}
}
}

return true;
}//end of isValidDate()


/**
* Method isLeapYear(int)
* This methiod checks whether the input year is a LeapYear.
* If it finds Leap Year it returns true otherwise false.
*
* @param intYear Input parameter
*/
function isLeapYear(intYear) {
if (intYear % 100 == 0) {
if (intYear % 400 == 0) {
return true;
}
} else {
if ((intYear % 4) == 0) {
return true;
}
}

return false;
}//end of isLeapYear()

// This function is used to find the Browser Version returned in numbers
function getbrowserversion()
{
  var ver=0;

  if(navigator.appName=="Microsoft Internet Explorer")
  {
    ver=parseFloat(navigator.appVersion.substring(22,28));
  }
  else if(navigator.userAgent.indexOf("Opera")>0)
  {
    ver=parseFloat(navigator.userAgent.substring(navigator.userAgent.indexOf(" v",1)+2,navigator.userAgent.length));
  }
  else if(navigator.appName=="Netscape")
  {
    ver=parseFloat(navigator.appVersion);
    if(ver<1) ver=0;
  }
  return(ver);
}

function popup(say)
{
  var popupwindow;
  if(parseFloat(getbrowserversion()) < 5.5)
  {
    popupwindow=top.open( '', 'popupwindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizeable=yes,width=200,height=150,top=300,left=400,dependent=true');
    popupwindow.document.clear();
    popupwindow.document.open('text/html');
    popupwindow.document.write('<html><head><title>Popup</title></head>');
    popupwindow.document.write("<body bgcolor='#5A9AFF' text='black' topmargin=0 leftmargin=0 marginwidth=0 marginheight=0 onBlur='self.close()'>");
    popupwindow.document.write('<table width=100% height=100% border=0 cellspacing=0 cellpadding=10>');
    popupwindow.document.write('<tr><td align=left valign=top><font face=Arial color=#FFFFFF size=3>');    
    popupwindow.document.write(say);    
    popupwindow.document.write('</font></td></tr>');
    popupwindow.document.write('<tr><td align=center valign=bottom>');
    popupwindow.document.write('<font face=Arial size=2 color=#333366><b>Click anywhere to continue.</b></font>');
    popupwindow.document.write('</td></tr>');
    popupwindow.document.write('</table>');
    popupwindow.document.writeln('</body></html>');
    popupwindow.defaultStatus='Click anywhere to continue.';
    popupwindow.document.close();
  }
}



function numericOnly(vChar) // This functions allows to enter only Numeric Values .

{
	if(vChar.length!=0)
	{
		for (i=0;i<vChar.length;i++)
		{
			if ((vChar.charCodeAt(i) >= 48) && (vChar.charCodeAt(i) <= 57))
			{					
				
			}
			else
			{
				return false;
			}
		}
		return true;		 	
	}
	
}

function fnFocus(msg,txtField)
{
 alert(msg);
 txtField.select();	
 return false;	
}

function fnCheckCurrency(value)
 {	
var decAmount = value.value;
var msg;
if(isNaN(decAmount)){
   msg='Only Numbers are allowed';
   fnFocus(msg,value);
   return false
}
if(noOfOccur(value, '.') > 1)
{
   msg='Decimal Point is to be entered only once';	  
   fnFocus(msg,value);
   return false
 }
decAmount = new String(decAmount);
chrAt0 = decAmount.charAt(0);
if (chrAt0=='+' || chrAt0 == '-') {
   msg='+ and - are not allowed';	
   fnFocus(msg,value);
   return false
}

j = decAmount.indexOf(".");

if (j != -1) {
if (j==0) return false;
if (j == value.length - 1) return false;

if(decAmount.length - (j + 1) > 2) {
msg='Only 2 Digits are allowed after Decimal';
   fnFocus(msg,value);
   //return false
}
} //else if (decAmount.length > beforeDeci) {
//msg='The number of digits the Max Limit';	
//fnFocus(msg,value);
//}
//value.value = fnRound(decAmount)
value.value = Math.round(decAmount)
value.value = fnCurrency(value.value)
return true;
}//end of checkDecimal()

function fnCurrency(prmCurr)
{
	var strCurrency
	strCurrency = new String(prmCurr);

	if(strCurrency=="")
	{
		strCurrency = "0.00"
		}
	else
	{
		if(strCurrency.indexOf(".") < 0)
		{
			strCurrency = strCurrency + ".00"
			}
		else
		{
			while(strCurrency.substr(strCurrency.indexOf(".")+1,strCurrency.length).length<2)
			{
				strCurrency = strCurrency + "0"
				}
			}
		}
	return strCurrency
	}

function fnRound(prmNum,prmDec) 
{
    prmDec = (!prmDec ? 2 : prmDec);
    return Math.round(prmNum*Math.pow(10,prmDec))/Math.pow(10,prmDec);
	}

function fnDateDiff( start, end, interval, rounding ) {

    var iOut = 0;
    
    // Create 2 error messages, 1 for each argument. 
    var startMsg = "Check the Start Date and End Date\n"
        startMsg += "must be a valid date format.\n\n"
        startMsg += "Please try again." ;
		
    var intervalMsg = "Sorry the dateAdd function only accepts\n"
        intervalMsg += "d, h, m OR s intervals.\n\n"
        intervalMsg += "Please try again." ;

    var bufferA = Date.parse( start ) ;
    var bufferB = Date.parse( end ) ;
    	
    // check that the start parameter is a valid Date. 
    if ( isNaN (bufferA) || isNaN (bufferB) ) {
        alert( startMsg ) ;
        return null ;
    }
	
    // check that an interval parameter was not numeric. 
    if ( interval.charAt == 'undefined' ) {
        // the user specified an incorrect interval, handle the error. 
        alert( intervalMsg ) ;
        return null ;
    }
    
    var number = bufferB-bufferA ;
    
    // what kind of add to do? 
    switch (interval.charAt(0))
    {
        case 'd': case 'D': 
            iOut = parseInt(number / 86400000) ;
            if(rounding) iOut += parseInt((number % 86400000)/43200001) ;
            break ;
        case 'h': case 'H':
            iOut = parseInt(number / 3600000 ) ;
            if(rounding) iOut += parseInt((number % 3600000)/1800001) ;
            break ;
        case 'm': case 'M':
            iOut = parseInt(number / 60000 ) ;
            if(rounding) iOut += parseInt((number % 60000)/30001) ;
            break ;
        case 's': case 'S':
            iOut = parseInt(number / 1000 ) ;
            if(rounding) iOut += parseInt((number % 1000)/501) ;
            break ;
        default:
        // If we get to here then the interval parameter
        // didn't meet the d,h,m,s criteria.  Handle
        // the error. 		
        alert(intervalMsg) ;
        return null ;
    }
    
    return iOut ;
}

/* -----------------------------------------------------------------------------------
	function		  :fnOptval(optCntrl)
	Parameters(input) :pass radio button control arrray
 	Description		  :one radio button must be selected for update or deletion of records
	eg				  :call function like fnOptVal(document.form.radiobuttoncontrol)
----------------------------------------------------------------------------------- */
function fnOptval(optCntrl)
{
 var i				//counter variable
 var intOpt			//holds radio butttons length	
 var blnChecked		//checks whether radio button is selected or not
 
 blnChecked=false
  intOpt=optCntrl.length
  
	if (typeof(intOpt)=="undefined" && optCntrl.checked==true)
	{
		blnChecked=true		
	}
	else
	{
		for (i=0;i<=intOpt-1;i++)
		{
			if (optCntrl[i].checked==true)
			{
				blnChecked=true
				intOpt=i
				break;
			}	 
		} 
	}		
	if (blnChecked)	
		return true
	else
		return false 
}//funtion close


function fnRadioBtnChk(optCntrl)
{
 var i				//counter variable
 var intOpt			//holds radio butttons length	
 var blnChecked		//checks whether radio button is selected or not
 
	blnChecked=false
	if (typeof(optCntrl)!='undefined')
	{
		intOpt=optCntrl.length
		
	  	if (typeof(intOpt)=="undefined" && optCntrl.checked==true)
		{
			blnChecked=true	
			intOpt=-2	
		}
		else
		{
			for (i=0;i<=intOpt-1;i++)
			{
				if (optCntrl[i].checked==true)
				{
					blnChecked=true
					intOpt=i
					break;
				}	 
			} 
		}
	}
	else
	{
		
	}			
	if (blnChecked)	
		return intOpt
	else
		return -1
}//funtion close


function numericOnly(vChar) // This functions allows to enter only Numeric Values .

{
	if(vChar.length!=0)
	{
		for (i=0;i<vChar.length;i++)
		{
			if ((vChar.charCodeAt(i) >= 48) && (vChar.charCodeAt(i) <= 57))
			{					
				
			}
			else
			{
				return false;
			}
		}
		return true;		 	
	}
	
}

function fnCodeCheck(txt)
{
//	if(txt.value=="")
//	{
//		return (true)
//	}	
	string1="ABCDEFGHIJKLMNOPQRSTUVWXYZ-_&~/0123456789";
	string2=txt.value.toUpperCase();  
	for (i=0;i<string2.length;i++)
	{
		if (string1.indexOf(string2.charAt(i))<0)
		{
		//alert("Enter valid " + msg + ".") 
		//txt.value=""
		//txt.focus()
		return false
		}	
	}
}

function fnNameCheck(txt)
{
	if(txt.value=="")
	{
		return (true)
	}	
	string1="ABCDEFGHIJKLMNOPQRSTUVWXYZ., -_0123456789#*+@():;'`/\n\r";
	string2=txt.value.toUpperCase();  
	for (i=0;i<string2.length;i++)
	{
		if (string1.indexOf(string2.charAt(i))<0)
		{
		alert("Enter valid " + msg + ".") 
		//txt.value=""
		txt.focus()
		return false
		}	
	}
}

/* -----------------------------------------------------------------------------------
	isValidAlphabet()
	    Parameters 
		strText- Input parameter, this is a string parameter. 	
 	    Description: This function allows ALPHABETS,NUMBERS,&,-,.,_
   ----------------------------------------------------------------------------------- */
	
function isValidCode(strText)
	{
		var schar;
		var sLength = strText.length;
	
		for(i=0;i<sLength;i++)
		{
			schar=strText.charCodeAt(i);
	
			if ((schar>=65 && schar<=90)||(schar>=48 && schar<=57)||(schar==38)||(schar==45)||(schar==46)||(schar==95))
			{
			}
	
			else return false;
		}
		return true;
	}


//****************************************************************************	
//added by anil

//don;t delete this ,required for assigntoIEL(lineitems) validation.
var strSubsForLineItems=''
///var dblPrjValueForLineItems=0
//var dblBillHrsForLineItems=0
/* -----------------------------------------------------------------------------------
	functon			  :DateComparision(fd,fm,fy,td,tm,ty)
	Parameters(input) :fromday,frommonth,fromyear,today,tomonth,toyear values
 						Description:used for date comparision(< or >)b/n 2 dates
 						here checking for todate > fromdate	
	Exapmle			  :call function like DateComparision(fromday,frommonth,fromyear,today,tomonth,toyear)
----------------------------------------------------------------------------------- */
function CompareDate(fd,fm,fy,td,tm,ty)
{
	var d1=parseInt(fd,10);	
	var m1=parseInt(fm,10);
	var y1=parseInt(fy,10);
	var d2=parseInt(td,10);	
	var m2=parseInt(tm,10);	
	var y2=parseInt(ty,10);	

	if (y1>y2) 
		return false;
	if (y1==y2)
	{
		if (m1>m2) 
		{
			return false;
		}	
		else if (m1==m2)
		{
			if (d1>d2) 
				return false;
		}
	}
	
	if (y1==y2 && m1==m2 && d1==d2)
		return true
}//end of function


function isDate(strDate)
{
	if (/^\d{1,2}\/\d{1,2}\/\d{4}$/g.test(strDate))
	{
		var arrSplt=strDate.split("/")
		
		if (parseInt(arrSplt[2],10)<1753)
			return false
			
		if (validate_date(arrSplt[0],arrSplt[1],arrSplt[2]))
			return true
		else
			return false	
	}
	else
	{
		return false
	}	
}
function fnChkMandatory(cntl,msg,strMandatory,strDataType)
{

	
	var blnErr=false
	var strMsg=''
	
	//check for mandatory
	if (cntl.value!='' && hasOnlySpaces(cntl.value)==true)
	{
		strMsg="Spaces are not allowed for " +msg
		blnErr=true
	}	
	if (blnErr==false && strMandatory=='Y')
	{
		if (cntl.value=='')
		{
			strMsg="Please enter " +msg
			blnErr=true
		}	
	}
	
	if (blnErr==false && strDataType=='A')
	{
		if (alphaNumCheck(cntl.value)==false)
		{
			strMsg="Please enter Alpha Numeric values for " +msg
			blnErr=true
		}	
	}
	
	//check for numeric
	if (blnErr==false && strDataType=='N')
	{
		if (numericOnly(cntl.value)==false)
		{
			strMsg="Please enter Numeric values for " +msg + "\n(Decimal values are not allowed)"
			cntl.value=cntl.value.substring(0,cntl.value.indexOf('.'))
			blnErr=true
		}	
	}
	//check for numeric
	if (blnErr==false && strDataType=='N6')
	{
		if (numericOnly(cntl.value)==false)
		{
			strMsg="Please enter Numeric values for " +msg + "\n(Decimal values are not allowed)"
			cntl.value=cntl.value.substring(0,cntl.value.indexOf('.'))
			blnErr=true
		}
		else
		{
			if (cntl.value.length>6)
			{
				strMsg="Please enter Numeric values for " +msg + "\n(6 digits are allowed)"
				blnErr=true
			}
		}	
	}
	//Numaric with decimals(10,2)
	if (blnErr==false && strDataType=='ND10')
	{
			if (checkDecimal(8,2,cntl)==false)
			{ 
				strMsg="Please enter Numeric values for "+msg+"\n(maximum digits allowed before decimal:8\nmaximum digits allowed after decimal:2)"
				blnErr=true
			}
			else
			{
				cntl.value=fnCurrency(cntl.value)
				blnErr==false
			}
	}
	
	//numeric with decimal
	if (blnErr==false && strDataType=='ND13')
	{
			if (checkDecimal(11,2,cntl)==false)
			{
				strMsg="Please enter Numeric values for "+msg+"\n(maximum digits allowed before decimal:11\nmaximum digits allowed after decimal:2)"
				blnErr=true
			}
			else
			{
				cntl.value=fnCurrency(cntl.value)
				blnErr==false
			}
	}
	//numeric with decimal
	if (blnErr==false && strDataType=='ND13C')
	{
		if (checkDecimal(11,2,cntl)==false)
		{
			strMsg="Please enter Numeric values for "+msg+"\n(maximum digits allowed before decimal:11\nmaximum digits allowed after decimal:2)"
			blnErr=true
		}
		else
		{
			cntl.value=fnCurrency(cntl.value)
			cntl.value=addCommas(cntl.value)
			
			blnErr==false
		}
	}
	//numeric with decimal
	if (blnErr==false && strDataType=='ND8')
	{
		if (checkDecimal(6,2,cntl)==false)
			{
				strMsg="Please enter Numeric values for "+msg+"\n(maximum digits allowed before decimal:6\nmaximum digits allowed after decimal:2)"
				blnErr=true
			}
			else
			{
				cntl.value=fnCurrency(cntl.value)
				blnErr==false
			}
	}
		//numeric with decimal
	if (blnErr==false && strDataType=='ND5')
	{
		
		if (parseFloat(cntl.value) >100)
		{
				strMsg="Please enter "+msg+"\n(should be less than 100.)"
				blnErr=true
		}
		else
		{
			if (checkDecimal(3,2,cntl)==false)
			{
				strMsg="Please enter Numeric values for "+msg+"\n(only 2 digits are allowed after decimal.)"
				blnErr=true
			}
			else
			{
					cntl.value=fnCurrency(cntl.value)
					blnErr==false
			}	
		}
	}
	//check for date
	if (blnErr==false && strDataType=='D')
	{
		if (isDate(cntl.value)==false)
		{
			strMsg="Please enter proper Date(dd/mm/yyyy) .\n Check for leap year."
			blnErr=true
		}	
	}
	
	//check for dropdownlist
	if (blnErr==false && strDataType=='DD')
	{
		if (cntl.value=="NA")
		{
			strMsg="Please select " + msg
			blnErr=true
		}	
	}
			
	//check for err
	if (blnErr==true)
	{
		alert(strMsg)
		cntl.focus()
		return false
	}
	else
	{
		return true
	}	
}

//strMode contains "A" for add "E" for Edit
function fnValItemDetails(strMode,strItemIndex,strCash)
{
	var blnErr=false
	var arrSplt
	var strCntl="dgIndReq"
	
	var intR=eval("document.getElementById('"+strCntl+"').rows.length")
	//get the itemindex and add 2 to get the row which is in edit mode
	if (strMode=='E')
		intR=parseInt(strItemIndex,10)+2
	
	if (fnChkMandatory(eval("document.getElementById('"+strCntl+"__ctl"+intR+"_ddl"+strMode+"Type')"),"Item type.","Y","DD")==false)
		return false
		
	if (fnChkMandatory(eval("document.getElementById('"+strCntl+"__ctl"+intR+"_txt"+strMode+"Desc')"),"Description.","Y","A")==false)
		return false
	
	/*if (strCash=="Y" && eval("document.getElementById('"+strCntl+"__ctl"+intR+"_ddl"+strMode+"Capex').value=='Y'"))
	{	
		alert("Capex form can't be set to Yes")
		return false
	}*/
	
	
	if (fnChkMandatory(eval("document.getElementById('"+strCntl+"__ctl"+intR+"_ddl"+strMode+"UOM')"),"UOM.","Y","DD")==false)
		return false
		
	//check for allow decimals	in UOM
	arrSplt=eval("document.getElementById('"+strCntl+"__ctl"+intR+"_ddl"+strMode+"UOM').value.split('|')")
	if (arrSplt[1]=='N')
	{
		if (blnErr==false && fnChkMandatory(eval("document.getElementById('"+strCntl+"__ctl"+intR+"_txt"+strMode+"ReqQty')"),"Required Quantity.","Y","N6")==false)
			blnErr=true
	}
	else
	{
		if (blnErr==false && fnChkMandatory(eval("document.getElementById('"+strCntl+"__ctl"+intR+"_txt"+strMode+"ReqQty')"),"Required Quantity.","Y","ND8")==false)
			blnErr=true
	}	
		
	if (blnErr==false && fnChkMandatory(eval("document.getElementById('"+strCntl+"__ctl"+intR+"_txt"+strMode+"ReqDt')"),"Approx. Date of Requirement.","Y","D")==false)
		blnErr=true
		
	var dt=new Date()
	var arrSplt2=eval("document.getElementById('"+strCntl+"__ctl"+intR+"_txt"+strMode+"ReqDt').value.split('/')")
	if (CompareDate(dt.getDate(),(dt.getMonth()+1),dt.getFullYear(),arrSplt2[0],arrSplt2[1],arrSplt2[2])==false)
	{
		alert("Date of Requirement is less than todays date. Please check")
	}
		
	if (blnErr==false && fnChkMandatory(eval("document.getElementById('"+strCntl+"__ctl"+intR+"_txt"+strMode+"Remarks')"),"Remarks.","N","A")==false)
		blnErr=true	

	if (blnErr==true)					
		return false
	else
		return true	
}



function fnChkCode(txt,msg)
{
  string1="ABCDEFGHIJKLMNOPQRSTUVWXYZ-_0123456789";
  if (emptyCheck(txt)) return true;
  //if (!isNaN(txt.value)) return(clearText1(txt,msg));
  string2=txt.value.toUpperCase();  
  for (i=0;i<string2.length;i++)
  {
    if (string1.indexOf(string2.charAt(i))<0)
		return(clearText1(txt,msg));
  }
  return true;
}

function fnChkDesc(txt,msg)
{
  string1="ABCDEFGHIJKLMNOPQRSTUVWXYZ., -_0123456789#*+@&'`():;/\n\r";
  if (emptyCheck(txt)) return true;
  //if (!isNaN(txt.value)) return(clearText1(txt,msg));
  string2=txt.value.toUpperCase();  
  for (i=0;i<string2.length;i++)
  {
    if (string1.indexOf(string2.charAt(i))<0)
		return(clearText1(txt,msg));
  }
  return true;
}

function fnUpper(prmVal)
{
	return prmVal.toUpperCase()
	}
	
function fnReplace(prmVal)
{
	while(prmVal.indexOf("'")>=0)
	{
		prmVal = prmVal.replace("'","`")
		}
	return prmVal
	}
	
function numericDotOnly(vChar) // This functions allows to enter only Numerics and dot only.
{
	if(vChar.length!=0)
	{
		for (i=0;i<vChar.length;i++)
		{
			if ((vChar.charCodeAt(i) == 46) || ((vChar.charCodeAt(i) >= 48) && (vChar.charCodeAt(i) <= 57)))
			{					
				
			}
			else
			{
				return false;
			}
		}
		return true;		 	
	}
	
}

function numericDotMinusOnly(vChar) // This functions allows to enter Numerics, dot and minus only.
{
	if(vChar.length!=0)
	{
		for (i=0;i<vChar.length;i++)
		{
			if ((vChar.charCodeAt(i) == 45) || (vChar.charCodeAt(i) == 46) || ((vChar.charCodeAt(i) >= 48) && (vChar.charCodeAt(i) <= 57)))
			{					
				
			}
			else
			{
				return false;
			}
		}
		return true;		 	
	}
	
}

//By RP - 04/04/2005
/* -----------------------------------------------------------------------------------
	function		  :fnValidateDate(day, month, year)
	Parameters(input) :day,month,year values
 	Description		  :This will checks for valid date and format i.e (dd/mm/yyyy)
 					   and leap year and days w.r.to months(30 or 31)
	eg				  : call function like fnvalidate(day,motnh,year)
----------------------------------------------------------------------------------- */
function fnValidateDate(day, month, year)
{
	var DayArray = new Array(31,28,31,30,31,30,31,31,30,31,30,31);
	var MonthArray = new Array("01","02","03","04","05","06","07","08","09","10","11","12");
	var inpDate = day + month + year;
	var filter=/^[0-9]{2}[0-9]{2}[0-9]{4}$/;
	if (! filter.test(inpDate))
	{
		return false;
	}
	filter=/01|02|03|04|05|06|07|08|09|10|11|12/ ;
	if (! filter.test(month))
	{
		return false;
	}
	var N = Number(year);                                               	
	if ( ( N%4==0 && N%100 !=0 ) || ( N%400==0 ) )
	{                                                                                         		
		DayArray[1]=29;
	}                                                                                         	
	for(var ctr=0; ctr<=11; ctr++)
	{                                                                                         		
		if (MonthArray[ctr]==month)
		{                                                                                 			
			if (day<= DayArray[ctr] && day >0 )
			{
					//inpDate = day + '/' + month + '/' + year;
					//alert(inpDate)
					return true;
			}
			else
			{
				return false;
			}
		}
	}
}
//added by anil
function addCommas(nStr)
{
	nStr += '';
	x = nStr.split('.');
	x1 = x[0];
	x2 = x.length > 1 ? '.' + x[1] : '';
	var rgx = /(\d+)(\d{3})/;
	while (rgx.test(x1)) {
		x1 = x1.replace(rgx, '$1' + ',' + '$2');
	}
	return x1 + x2;
}