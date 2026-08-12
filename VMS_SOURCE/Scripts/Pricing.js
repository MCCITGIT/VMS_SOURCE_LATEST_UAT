//'**************************************************
//'Copyright	: VMS, MCC, KOLKATA
//'Source	    : Scripts/Pricing.js
//'Created Date	: 16-August-2007
//'Created By	: Aneston
//'Version	    : R01.00.00
//'Description	: Pricing Validation only

//'Modified By       Modified On       Version         Reason

//'*************************************************************


//function to calculate Market Amount per square feet
function pricingSqFeetCalCulation(getId)
{   
    //to remove comma seperator in the label fields
    FormatCommaSeparator(false,"lblTotalMarketAmtPerSqFLabel","innerHTML");
    FormatCommaSeparator(false,"lblTotalMarketAmt","innerHTML");
    FormatCommaSeparator(false,"lblMarketingArea","innerHTML");
    
    var controlID1 = getId;
    var controlID2 = getId + "PerSqFLabel";
    var marketareaControlID = "lblMarketingArea";
    //FormatCommaSeparator(false,controlID1);
    
    var amountValue = "";
    
    if(document.getElementById(controlID1).value != "")
        amountValue = document.getElementById(controlID1).value;        
    else
        amountValue = 0;
        
    var marketareaValue = document.getElementById(marketareaControlID).innerHTML;
    
    //calculation to find per square feet
    var perSqFeetValue = parseInt(amountValue) / parseInt(marketareaValue);    
    
    //to display the per squarefeet value on the corresponding column  
    //bugId: 89 to remove round of -- By Arun  
    document.getElementById(controlID2).innerHTML = perSqFeetValue.toFixed(2);
    
    totalAmount();   
    FormatCommaSeparator(true,controlID1);
    //to add comma seperator in the label fields
    FormatCommaSeparator(true,"lblTotalMarketAmtPerSqFLabel","innerHTML");
    FormatCommaSeparator(true,"lblTotalMarketAmt","innerHTML");
    FormatCommaSeparator(true,"lblMarketingArea","innerHTML");
}

//function to find total amount
function totalAmount()
{
    //to remove comma seperator in the label fields
    FormatCommaSeparator(false,"lblTotalMarketAmtPerSqFLabel","innerHTML");
    FormatCommaSeparator(false,"lblTotalMarketAmt","innerHTML");
    FormatCommaSeparator(false,"lblMarketingArea","innerHTML");
    
    var controlID = new Array(8);
    
    controlID[0] = "lblTotalCost";
    controlID[1] = "txtMarketAmountSpend";
    controlID[2] = "txtIncentive";
    controlID[3] = "txtIncidentalExpenses";
    controlID[4] = "txtOtherExpenses1";
    controlID[5] = "txtOtherExpenses2";
    controlID[6] = "txtOtherExpenses3";
    controlID[7] = "txtProfit";
    
    var amountValue = 0;
    var i=0;
    for (i=0; i<controlID.length; i++)
    {
        
        if(i==0)
        {
            FormatCommaSeparator(false,controlID[i], "innerHTML");
            //value when retrieved from label
            if(document.getElementById(controlID[i]).innerHTML != "")
                amountValue += parseInt(document.getElementById(controlID[i]).innerHTML);
            else
                amountValue += 0;
        }
        else
        {
            FormatCommaSeparator(false,controlID[i]);
            
            //value when retrieved from textbox            
            if(document.getElementById(controlID[i]).value != "")
                amountValue += parseInt(document.getElementById(controlID[i]).value);   
            else
                amountValue += 0;
        }        
    }
    
    document.getElementById("lblTotalMarketAmt").innerHTML = amountValue;
    
    //to find total market area amount
    var marketareaValue = document.getElementById("lblMarketingArea").innerHTML;
    
    //calculation to find per square feet
    var perSqFeetValue = parseInt(amountValue) / parseInt(marketareaValue);
    //bugId: 89 to remove round of -- By Arun
    document.getElementById("lblTotalMarketAmtPerSqFLabel").innerHTML = perSqFeetValue.toFixed(2);
    
    var i=0;
    for(i=0; i<controlID.length; i++)
    {        
        if(i != 0)
        {     
            FormatCommaSeparator(true,controlID[i]);
        }
        
    }
    
    //to add comma seperator in the label fields
    FormatCommaSeparator(true,"lblTotalMarketAmtPerSqFLabel","innerHTML");
    FormatCommaSeparator(true,"lblTotalMarketAmt","innerHTML");
    FormatCommaSeparator(true,"lblMarketingArea","innerHTML");
}