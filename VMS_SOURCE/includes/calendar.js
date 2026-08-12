//calendar
//Variable decleration
var strCntlName
var strLocation	   // Document Location
var intM		       // Cur Month
var intY		   // Cur Year
var intFDay		   // First day of the month
var intNoDays	   // Number of days in the month
var intMPrev	   // Previous Month
var intYPrev	   // Previous Year
var intMNext	   // Next Month
var intYNext	   // Next Year
var intPos
var strDate
var frm				// form Name
var fld				// Field Name
		
function OnClick(cntl)
{
	var intpos=0
	var cntl
	var arrCntls=document.all.tags("DIV")
	
	//get cntl reference
	strCntlName =cntl.id
	intpos=strCntlName.indexOf("_")
	cntl=eval("document.getElementById('"+strCntlName.substring(0,intpos)+"_pnlDiv')")
		
	//alert(arrCntls.length)
	//get all div elements and disable calendar divs if opened.
	for (var i =0;i<=arrCntls.length-1;i++)
	{
		intpos=arrCntls[i].id.indexOf("_pnlDiv")
		if (intpos>0)
		{
			if (arrCntls[i].id !=cntl.id)
			{
				arrCntls[i].style.display = "none";
			}
		}	
	}
	
	if(cntl.style.display == "" || cntl.style.display == "none")
	{
		SetDMY(0,0)
		cntl.style.display = "inline";
	} 
	else
	{
	 cntl.style.display = "none";
	}
}


	
				
	function SetDMY(m,y)
	{
		intM=m
		intY=y 
		
		if (intM==0) 
		{
			var dt=new Date()
			var dt=new Date()
			intM = dt.getMonth()
			intM = intM + 1
			intY = dt.getFullYear()
		}
		else
		{
		
			if(intM==null || intY==null)
			{
			var dt=new Date()
			intM = dt.getMonth()
			intM = intM + 1
			intY = dt.getFullYear()
			}
		}
		 
		//FINDING THE WEEKDAY OF 1ST OF SELECTED MONTH
		strDate = intM + "/01/" + intY
		var dtDate=new Date(strDate)
		intFDay = dtDate.getDay()
		intFDay = intFDay+1
		//alert(intFDay)

		//FINDING THE NUMBER OF DAYS IN SELECTED MONTH
		strDate1 = parseInt(intM+1) + "/01/" + intY
		var dtDate1=new Date(strDate1)
		var intOneDay = 1000*60*60*24;
		intNoDays=Math.ceil((dtDate1.getTime()-dtDate.getTime())/(intOneDay))
		//alert(intNoDays)
		
		//Finding previous month & year
		strDate = parseInt(intM-1) + "/01/" + intY
		var dtDate=new Date(strDate)
		intMPrev = dtDate.getMonth()
		intMPrev=intMPrev+1
		intYPrev = dtDate.getFullYear()
		
		//Finding next month & year
		strDate = parseInt(intM+1) + "/01/" + intY
		var dtDate=new Date(strDate)
		intMNext = dtDate.getMonth()
		intMNext=intMNext+1
		intYNext = dtDate.getFullYear()
		//alert(dtDate) 
	
		fnWriteCalendar()
	}
	
  function fnWriteCalendar()
  {
		var strBuild1=new String()
		var strBuild2=new String()
		var strBuild3=new String()
		
		//strBuild=strBuild+WritePageTop()
		strBuild1=WriteMonthNameTable()
		strBuild1=strBuild1+WriteCalTableBegin()
		strBuild1=strBuild1+WriteWeekLabelRow()
		strBuild2=GenerateCalendar()
		strBuild3=WriteCalTableEnd()
		var intpos=strCntlName.indexOf("_")
		var cntl=eval("document.getElementById('"+strCntlName.substring(0,intpos)+"_pnlDiv')")
		cntl.innerHTML=strBuild1+strBuild2+strBuild3
	}	
	
/*function WritePageTop()
{
	var strPageTop
	strPageTop='<div id="divCalendar" style="DISPLAY: none; POSITION: absolute">'
	return strPageTop
}*/

function WriteMonthNameTable()
{	
	var strMonName
	
var arrMonthNames=new Array('January','February','March','April','May','June','July','August','September','October','November','December')
strMonName='<TABLE BORDER="0" CELLPADDING="1" CELLSPACING="0" 	WIDTH="200" STYLE="border: 1px solid black;">'
strMonName=strMonName+'<TR BGCOLOR="#0198cf" STYLE="border: 1px solid #808080;">'
strMonName=strMonName+'<TD WIDTH="12" ALIGN="CENTER" VALIGN="CENTER"><IMG SRC="images/Left.bmp" WIDTH="8" HEIGHT="8" ALT="Previous Month" BORDER="0" onclick="javascript:SetDMY(intMPrev,intYPrev);return false;"></TD>'
strMonName=strMonName+'<TD WIDTH="176" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color=#FFFFFF><b>' + arrMonthNames[intM-1] + ', ' + intY + '</b></font></STRONG></TD>'
strMonName=strMonName+'<TD WIDTH="12" ALIGN="CENTER" VALIGN="CENTER"><IMG SRC="images/Right.bmp" WIDTH="8" HEIGHT="8" ALT="Next Month" BORDER="0" onclick="javascript:SetDMY(intMNext,intYNext);return false;"></TD>'
strMonName=strMonName+'</TR></TABLE>'

return strMonName
}

function WriteCalTableBegin()
{
	var strCalBegin
	strCalBegin='<TABLE BORDER="0" bgcolor="#f0f8ff" CELLPADDING="1" CELLSPACING="0" WIDTH="200" STYLE="border: 1px solid black;">'
	
	return strCalBegin
}

function WriteWeekLabelRow()
{
	var strWkLblRow
strWkLblRow='<TR bgcolor="#fedb61"><TD> </TD>'
strWkLblRow=strWkLblRow+'<TD WIDTH="20" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color="red"><B>S</B></font></TD>'
strWkLblRow=strWkLblRow+'<TD WIDTH="20" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color="blue"><B>M</B></font></TD>'
strWkLblRow=strWkLblRow+'<TD WIDTH="20" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color="blue"><B>T</B></font></TD>'
strWkLblRow=strWkLblRow+'<TD WIDTH="20" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color="blue"><B>W</B></font></TD>'
strWkLblRow=strWkLblRow+'<TD WIDTH="20" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color="blue"><B>T</B></font></TD>'
strWkLblRow=strWkLblRow+'<TD WIDTH="20" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color="blue"><B>F</B></font></TD>'
strWkLblRow=strWkLblRow+'<TD WIDTH="20" ALIGN="CENTER" VALIGN="TOP"><font face=verdana size=2 color="blue"><B>S</B></font></TD>'
strWkLblRow=strWkLblRow+'<TD></TD></TR><TR><TD> </TD><TD COLSPAN="7"><HR SIZE="1" COLOR="#808080" NOSHADE></TD>'
strWkLblRow=strWkLblRow+'<TD></TD></TR>'

return strWkLblRow
}

function WriteCalTableEnd()
{
	var strCAlEnd
strCAlEnd='<TR><TD></TD><TD COLSPAN="7"><HR SIZE="1" COLOR="#808080" NOSHADE></TD><TD> </TD>'
strCAlEnd=strCAlEnd+'</TR></TABLE>'

return strCAlEnd 
}

function WritePageBottom()
{
  return '</DIV>'
}

function GenerateCalendar()
{
var i
var strGenCal

strGenCal='<TR><TD> </TD>'	// -- Left blank column

for(i=1;i<=42;i++)				//	-- Dump all the days
{

	if(i < intFDay)	
	strGenCal=strGenCal +WriteInActiveCalDay(' ')					// -- is it before the beginning of the month
							
	else if(i > intNoDays + intFDay - 1)			//		 -- Is it after the end of the month
		strGenCal=strGenCal + WriteInActiveCalDay(' ')					// -- Write a blank
	else
		strGenCal=strGenCal + WriteActiveCalDay(i- intFDay +1 , i)	// -- Write the day


		if(i % 7 == 0)		// -- Do we need to go to the next row?
		{
			// -- we are at the end of the week
			strGenCal=strGenCal +'<TD> </TD></TR>'	// -- End of the Row
			if(i <= intNoDays + intFDay - 1)	// -- Do we need another row? 
			{
				strGenCal=strGenCal +'<TR><TD></TD>' // -- Left blank column
			}
			else
				break							// -- we have exceeded the number of days, get out
		}

}

	if(i % 7 != 0)					// -- End the row
	{
		while(i % 7 > 0)
		{
			strGenCal=strGenCal +WriteInActiveCalDay(' ')
			i = i+1
		}

		strGenCal=strGenCal +'<TD></TD></TR>'			// -- End of the Row
	}

return strGenCal
}





function WriteInActiveCalDay(sLabel)
{
	return "<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP' BGCOLOR=''>" + sLabel + "</TD>"
}
//------------------------------------------------------
function WriteActiveCalDay(sLabel, su)
{
	var bcolor
	var mont
	var dt=new Date()
	mont=eval(dt.getMonth())
	mont=mont+1
	if((dt.getDate()==sLabel) && (mont==intM) && (dt.getFullYear()==intY))
		bcolor="pink"
	else
		bcolor=""
	
	
	if(su % 7 == 1)
		return "<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP' bgcolor='" + bcolor + "'><A HREF=javascript:void(0) style='text-decoration:none' onclick=assign('" + sLabel + "/" + intM + "/" + intY + "');return false; ><font face=verdana size=2; color='Red'><B>" + sLabel + "</B></font></A></TD>"
	else
		return "<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP' bgcolor='" + bcolor + "'><A HREF=javascript:void(0) style='text-decoration:none'><font face=verdana sIZE=2; color='#000080' onclick=assign('" + sLabel + "/" + intM + "/" + intY + "');return false;><B>" + sLabel + "</B></font></A></TD>"

}



function assign(SelDate)
{
	var intpos=0
	var arrDts = SelDate.split("/")
	var dd,mm,yy
	dd=arrDts[0]
	mm=arrDts[1]
	yy=arrDts[2]
	while(dd.toString().length<2)
	{
		dd = "0" + dd.toString()
		}
	while(mm.toString().length<2)
	{
		mm = "0" + mm.toString()
		}
	SelDate = dd + "/" + mm + "/" + yy
	intpos=strCntlName.indexOf("_")
	eval("document.getElementById('"+strCntlName.substring(0,intpos)+"_txtDate').value=SelDate")
	var cntl=eval("document.getElementById('"+strCntlName.substring(0,intpos)+"_pnlDiv')")
	cntl.style.display = "none";
	eval("document.getElementById('"+strCntlName.substring(0,intpos)+"_txtDate').focus()")

}

function today()
{
	
	var dt = new Date()
	da = dt.getDate()
	while(da.toString().length<2)
	{
		da = "0" + da.toString()
		}
	mo = dt.getMonth()+1
	while(mo.toString().length<2)
	{
		mo = "0" + mo.toString()
		}
	today = da + "/" + mo + "/" + dt.getYear()
	document.getElementById("Calendarcontrol1_txtDate").value=today
	 divCalendar.style.display = "none";
	document.getElementById("Calendarcontrol1_txtDate").focus()
	
}
