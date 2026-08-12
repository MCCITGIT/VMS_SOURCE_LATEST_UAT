//'**************************************************
//'Copyright	: VMS, MCC, KOLKATA
//'Source	    : Scripts/Currency.js
//'Created Date	: 16-August-2007
//'Created By	: Saravanan
//'Version	    : R01.00.00
//'Description	: Values Converted to Currency Format

//'Modified By       Modified On       Version         Reason

//'*************************************************************

// JScript File

var currencySymbol;
var decimalSeparator;
var groupSeparator;
var groupSize;


// Sets the Currency Symbol, Decimal separator, Group Separator and Group size from the server for the User Culture
function SetFormatDetails(currSymbol,decSeparator,grpSeparator,grpSize)
{
  currencySymbol = currSymbol;
  decimalSeparator = decSeparator;
  groupSeparator = grpSeparator;
  groupSize = grpSize;  
}

//for unformatting declared value for edit
function FormatCommaSeparator( FormatType, objectName, objectType)
{  

  var txtCtl = document.getElementById(objectName); 
  if(txtCtl != null && txtCtl.value != '')
  { 
    if ( FormatType )
    {
        if(objectType == "innerHTML")
        {
            var unformattedValue = UnFormatCurrency(txtCtl.innerHTML);   
            txtCtl.innerHTML = FormatCurrency(unformattedValue);        
        }
        else
        {
            var unformattedValue = UnFormatCurrency(txtCtl.value);   
            txtCtl.value = FormatCurrency(unformattedValue);        
        }
    }
    else
    {
        if(objectType == "innerHTML")
            {
                txtCtl.innerHTML = UnFormatCurrency(txtCtl.innerHTML);            
            }
        else
            {
            txtCtl.value = UnFormatCurrency(txtCtl.value);            
            //txtCtl.focus();
            }  
    }   
    //CheckForSubmit();    
  }
}

// Formats the Currency value
function FormatCurrency(currencyValue)
{
  var formattedValue = currencyValue;
  var integralPart,decimalPart;
  
  if(formattedValue.indexOf(decimalSeparator) >= 0)
  {
    integralPart = formattedValue.substring(0,formattedValue.indexOf(decimalSeparator));
    decimalPart = formattedValue.substring(formattedValue.indexOf(decimalSeparator) + 1);
    
    if(decimalPart.length > 2)
      decimalPart = decimalPart.substring(0,2);
      
    var tempIntegralPart = FormatIntegralPart(integralPart);
      
    formattedValue = tempIntegralPart + decimalSeparator + decimalPart;
  }
  else  
    formattedValue = FormatIntegralPart(formattedValue);
    
  if(formattedValue.length  > 0)    
    formattedValue = formattedValue;
    
    //formattedValue = currencySymbol + formattedValue;  
    
  return formattedValue;
}

// Formats the Integral Part of the Currency
function FormatIntegralPart(integralPart)
{
  var stripIntegral = StripLeadingZero(integralPart);
  var digitCount = stripIntegral.length;   
  var tempIntegralPart = '';    
  var counter = 0;
  for(index = digitCount-1;index >= 0; index--)
  {      
    counter++;
    tempIntegralPart = stripIntegral.charAt(index) + tempIntegralPart;    
    if(counter % groupSize == 0 && counter != 0 && counter != digitCount)
      tempIntegralPart = groupSeparator + tempIntegralPart; 
  }  
  
  return tempIntegralPart;
}

// Unformat the formatted Currency value for validation
function UnFormatCurrency(currencyValue)
{
  var tempValue = currencyValue;
  tempValue = StripSpecialCharacters(tempValue);
  
  
  return tempValue;
}

// Strip leading zeros(if any) in the integral part
function StripLeadingZero(integralPart)
{
    var index =0;
    
    while(integralPart.charAt(index)=='0' && integralPart.length != '1') 
    {
        integralPart = integralPart.replace('0','');
    }
    return integralPart;
}

// Strip off special characters in the currency value
function StripSpecialCharacters(currencyValue)
{
  var tempValue = currencyValue;
  tempValue = StripCurrencySymbol(tempValue); 
  tempValue = StripGroupSeparator(tempValue);
  
  return tempValue;
}

// Strip off the currency symbol from the input string
function StripCurrencySymbol(inputValue)
{
  var tempValue = inputValue;
  while(tempValue.indexOf(currencySymbol) >= 0)
  {
    tempValue = tempValue.replace(currencySymbol,'');   
  }
  return tempValue;
}

// Strip off the group separator from the input string
function StripGroupSeparator(inputValue)
{
  var tempValue = inputValue;
  while(tempValue.indexOf(groupSeparator) >= 0)
  {
    tempValue = tempValue.replace(groupSeparator,'');   
  }
  return tempValue;
}

