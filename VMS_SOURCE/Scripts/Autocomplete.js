/***************************************************
'Copyright	     : VMS, MCC, KOLKATA
'JavaScript Name : Autocomplete.js
'Created Date	 : 18 Feb 2011
'Created By	     : Neeraj

'Modified By       Modified On       Version         Reason

'***************************************************/




var xmlHttp;
//AJAX for Lead Add page
function CreateXMLHTTP() {
    try {
        // Firefox, Opera 8.0+, Safari    
        xmlHttp = new XMLHttpRequest();
    }
    catch (e) {
        // Internet Explorer    
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e) {
                alert("Your browser does not support AJAX!");
                return false;
            }
        }
    }
}

var LocationId='';  
var DivId='';       
var linkobjectId='';
var additionalobjId = '';
var autoselectlistId = '';

var dlrcdID = '';
var dlrnmId = '';


function autoCompletelist() {
    var D = new PopupWindow();
   
        D.show = showhideAutolist;
        D.pop= showhideAutoMaclist;
        D.pop2 = showhideAutoMaclist2;
       return  D;

   
}

function showhideAutolist() {
 if (arguments.length>0){
         LocationId = arguments[0];
        DivId = arguments[1];
        linkobjectId = arguments[2];
        additionalobjId = arguments[3];
   }
    autolist_writeHtml()
  
}

function showhideAutoMaclist() {
    if (arguments.length>0){
       LocationId = arguments[0];
         DivId = arguments[1];
         linkobjectId = arguments[2];
         additionalobjId = arguments[3];
   }
    automaclist_writeHtml()
  
}

function showhideAutoMaclist2() {
    if (arguments.length > 0) {
        LocationId = arguments[0];
        DivId = arguments[1];
        linkobjectId = arguments[2];
        additionalobjId = arguments[3];
    }
    automaclist_writeHtml2()

}

function autolist_writeHtml() {
var divobj = document.getElementById( DivId);
    if (divobj.innerHTML != '') {
        divobj.innerHTML =''; 
    }
    else {
          dlrcdID = 'txtdlrcd';
         dlrnmId = 'txtdlrname';
         autoselectlistId = 'dlrselect';
        var result = '<table border="0"  cellspacing="0" class="fy" width="400px" >';
        result += '<tr><td class="aligntext">Dealer Search By</td></tr>';
        result += '<tr><td class="aligntext">Code :&nbsp; <input id="' +  dlrcdID + '" type="text" maxlength="6" class="txtBox" onChange="fnDlrGet()" /> &nbsp; Name : ';
        result += '<input id="' +  dlrnmId + '" type="text" class="txtBox" onChange="fnDlrGet()" /></td></tr>'
        result += '<tr><td>  <select id="' +  autoselectlistId + '" size=4  name="D1" class="autodivlist" style=" width: 99%; height: 150px;" onChange="fnDlr_selected()">'
        result += '</td></tr> </table>'
        divobj.innerHTML = result;

             
        
    }

}



function automaclist_writeHtml() {

 var divobj = document.getElementById(DivId);
    if (divobj.innerHTML != '') {
        divobj.innerHTML =''; 
    }
    else {
          dlrcdID = 'txtdlrcd';
         dlrnmId = 'txtdlrname';
         autoselectlistId = 'dlrselect';
        var result = '<table border="0"  cellspacing="0" class="fy" >';
        result += '<tr><td class="aligntext">Dealer Search By</td></tr>';
        result += '<tr><td class="aligntext">Code :&nbsp; <input id="' +  dlrcdID + '" type="text" maxlength="6" class="txtBox" onChange="fnMacDlrGet()" /> &nbsp; Name : ';
        result += '<input id="' +  dlrnmId + '" type="text" class="txtBox" onChange="fnMacDlrGet()" /></td></tr>'
        result += '<tr><td>  <select id="' +  autoselectlistId + '" size=4  name="D1" class="autodivlist" style=" width: 99%; height: 150px;" onChange="fnDlr_selected()">'
        result += '</td></tr> </table>'
        divobj.innerHTML = result;

             
        
    }

}

function automaclist_writeHtml2() {

    var divobj = document.getElementById(DivId);
    if (divobj.innerHTML != '') {
        divobj.innerHTML = '';
    }
    else {
        LocationId = "ddldepotName";
        dlrcdID = 'txtdlrcd';
        dlrnmId = 'txtdlrname';
        autoselectlistId = 'dlrselect';
        var result = '<table border="0"  cellspacing="0" class="fy" >';
        result += '<tr><td class="aligntext">Dealer Search By<br/></td></tr>';
        result += '<tr><td class="aligntext">Depot : <select id="' + LocationId + '"  class="dropDown" style="width:150px" onChange="fnMacDlrGet()"  > </select></td></tr>';
        result += '<tr><td class="aligntext">Code :&nbsp; <input id="' + dlrcdID + '" type="text" maxlength="6" class="txtBox" onChange="fnMacDlrGet()" /> &nbsp; Name : ';
        result += '<input id="' + dlrnmId + '" type="text" class="txtBox" onChange="fnMacDlrGet()" /></td></tr>'
        result += '<tr><td>  <select id="' + autoselectlistId + '" size=4  name="D1" class="autodivlist" style=" width: 99%; height: 150px;" onChange="fnDlr_selected2()">'
        result += '</td></tr> </table>'
        divobj.innerHTML = result;

        fnDepotLoad();

    }

}

function fnDlrGet() {
    var ct_location = document.getElementById( LocationId);
    var ct_dlrcode = document.getElementById( dlrcdID);
    var ct_dlrName = document.getElementById( dlrnmId);

    if (IsEmpty(ct_location) && (IsEmpty(ct_dlrcode) || IsEmpty(ct_dlrName))) {
        CreateXMLHTTP();

        if (xmlHttp) {
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "ADlrList";
            requestURL += "&Location=";
            requestURL += ct_location.value;
            requestURL += "&DlrCode=";
            requestURL += ct_dlrcode.value;
            requestURL += "&DlrName=";
            requestURL += ct_dlrName.value;
            requestURL += "&Random=";
            requestURL += Math.random();

            xmlHttp.onreadystatechange = doloadDlrDetails;
            xmlHttp.open("GET", requestURL, true);
            xmlHttp.send(null);
        }
    }
    else {
        document.getElementById( autoselectlistId).options.length = 0;
        document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
        document.getElementById( autoselectlistId).options[0].value = "";
        ct_dlrcode.value = "";

    }
}

function doloadDlrDetails() {
    document.getElementById( autoselectlistId).options.length = 0;
    document.getElementById( autoselectlistId).options[0] = new Option("Loading...");
    document.getElementById( autoselectlistId).options[0].value = "";
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == 'complete') {
        if (xmlHttp.status == 200) {
            showDlrDetails(xmlHttp.responseText);
        }
        else {
            alert("There was a problem retrieving data from the server.");
        }
    }
}

function showDlrDetails(result) {

    try {

        if (result != "") {

            eval("var SKUDetails =" + result);
            if (SKUDetails != null && SKUDetails.length > 0 && SKUDetails != "") {
                document.getElementById( autoselectlistId).options.length = 0;
                for (var i = 0; i < SKUDetails.length - 1; i++) {
                    document.getElementById( autoselectlistId).options[i] = new Option(SKUDetails[i].Dealer_add);
                    document.getElementById( autoselectlistId).options[i].value = SKUDetails[i].Dealer_code;
                }

              }
            else {
                document.getElementById( autoselectlistId).options.length = 0;
                document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
                document.getElementById( autoselectlistId).options[0].value = "";
            }
        }
        else {
            document.getElementById( autoselectlistId).options.length = 0;
            document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
            document.getElementById( autoselectlistId).options[0].value = "";
        }
    }
    catch (e) {
        document.getElementById( autoselectlistId).options.length = 0;
        document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
        document.getElementById( autoselectlistId).options[0].value = "";
    }

}


function fnMacDlrGet() {
    var ct_location = document.getElementById( LocationId);
    var ct_dlrcode = document.getElementById( dlrcdID);
    var ct_dlrName = document.getElementById( dlrnmId);

    if (IsEmpty(ct_location)) {
        CreateXMLHTTP();

        if (xmlHttp) {
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "AMacDlrList";
            requestURL += "&Location=";
            requestURL += ct_location.value;
            requestURL += "&DlrCode=";
            requestURL += ct_dlrcode.value;
            requestURL += "&DlrName=";
            requestURL += ct_dlrName.value;
            requestURL += "&Random=";
            requestURL += Math.random();

            xmlHttp.onreadystatechange = doloadMacDlrDetails;
            xmlHttp.open("GET", requestURL, true);
            xmlHttp.send(null);
        }
    }
    else {
        document.getElementById( autoselectlistId).options.length = 0;
        document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
        document.getElementById( autoselectlistId).options[0].value = "";
        ct_dlrcode.value = "";

    }
}

function doloadMacDlrDetails() {
    document.getElementById( autoselectlistId).options.length = 0;
    document.getElementById( autoselectlistId).options[0] = new Option("Loading...");
    document.getElementById( autoselectlistId).options[0].value = "";
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == 'complete') {
        if (xmlHttp.status == 200) {
            showMacDlrDetails(xmlHttp.responseText);
        }
        else {
            alert("There was a problem retrieving data from the server.");
        }
    }
}

function showMacDlrDetails(result) {

    try {

        if (result != "") {

            eval("var SKUDetails =" + result);
            if (SKUDetails != null && SKUDetails.length > 0 && SKUDetails != "") {
                document.getElementById( autoselectlistId).options.length = 0;
                for (var i = 0; i < SKUDetails.length - 1; i++) {
                    document.getElementById( autoselectlistId).options[i] = new Option(SKUDetails[i].Dealer_add);
                    document.getElementById( autoselectlistId).options[i].value = SKUDetails[i].Dealer_code;
                }

              }
            else {
                document.getElementById( autoselectlistId).options.length = 0;
                document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
                document.getElementById( autoselectlistId).options[0].value = "";
            }
        }
        else {
            document.getElementById( autoselectlistId).options.length = 0;
            document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
            document.getElementById( autoselectlistId).options[0].value = "";
        }
    }
    catch (e) {
        document.getElementById( autoselectlistId).options.length = 0;
        document.getElementById( autoselectlistId).options[0] = new Option("No Maching Record Found");
        document.getElementById( autoselectlistId).options[0].value = "";
    }

}

function fnDlr_selected() {
    try {
        var cntrl_used = document.getElementById( autoselectlistId);
        var sel_index = cntrl_used.selectedIndex;
        var sel_val = cntrl_used.value;
        if (sel_val != '') {
            var sel_text = cntrl_used.options[sel_index].innerText;
            var i = sel_text.indexOf('-');
            var sel_name = sel_text.substring(i + 1, sel_text.length);

            if ( linkobjectId != null ||  linkobjectId != undefined) {
                document.getElementById( linkobjectId).value = sel_val;
            }
            if ( additionalobjId != null ||  additionalobjId != undefined) {
                document.getElementById( additionalobjId).value = sel_name;
            }

            showhideAutolist();
           document.getElementById( linkobjectId).focus();
        }
    }

    catch (e) {
    }

}
function fnDlr_selected2() {
    try {
        var cntrl_used = document.getElementById(autoselectlistId);
        var sel_index = cntrl_used.selectedIndex;
        var sel_val = cntrl_used.value;
        document.getElementById(additionalobjId).value = document.getElementById(LocationId).value;
        if (sel_val != '') {
            var sel_text = cntrl_used.options[sel_index].innerText;
            var i = sel_text.indexOf('-');
            var sel_name = sel_text.substring(i + 1, sel_text.length);

            if (linkobjectId != null || linkobjectId != undefined) {
                document.getElementById(linkobjectId).value = sel_val;
            }
            if (additionalobjId != null || additionalobjId != undefined) {
                document.getElementById(additionalobjId).value = sel_name;
            }

            showhideAutolist();
            document.getElementById(linkobjectId).focus();
        }
    }

    catch (e) {
    }

}


function fnDepotLoad() {
    CreateXMLHTTP();
    if (xmlHttp) {
        var requestURL = "AjaxServices.aspx?";
        requestURL += "Code=";
        requestURL += "DepotDetails";
        requestURL += "&region="
        requestURL += ""
        requestURL += "&Random="
        requestURL += Math.random()

        xmlHttp.onreadystatechange = doloadDepotDetails;
        xmlHttp.open("GET", requestURL, true);
        xmlHttp.send(null);
    }
}

function doloadDepotDetails() {
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == 'complete') {
        if (xmlHttp.status == 200) {
            showDepotDetails(xmlHttp.responseText);
        }
        else {
            alert("There was a problem retrieving data from the server.");
        }
    }
}


function showDepotDetails(result) {

    try {

        if (result != "") {
            eval("var DepotDetails =" + result);
            var depocdID = LocationId;

            if (DepotDetails != null && DepotDetails.length > 0) {
                document.getElementById(depocdID).options.length = 0;
                document.getElementById(depocdID).options[0] = new Option("Select");
                document.getElementById(depocdID).options[0].value = '';

                for (var i = 0; i < DepotDetails.length - 1; i++) {
                    document.getElementById(depocdID).options[i + 1] = new Option(DepotDetails[i].DepotName);
                    document.getElementById(depocdID).options[i + 1].value = DepotDetails[i].DepotCode;
                }
            }
            else {
                document.getElementById(depocdID).options.length = 0;
                document.getElementById(depocdID).options[0] = new Option("Select");
                document.getElementById(depocdID).options[0].value = '';

            }
        }
        else {
            //alert("No Item found"); 
            document.getElementById(depocdID).options.length = 0;
            document.getElementById(depocdID).options[0] = new Option("Select");
            document.getElementById(depocdID).options[0].value = '';
            document.getElementById(depocdID).value = '';

        }
    }
    catch (e) {
        alert("There was a problem retrieving data from the server.");
    }

}