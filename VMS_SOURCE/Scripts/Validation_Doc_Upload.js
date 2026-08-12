//Java Script File
//Created By Debayan Biswas On 03-01-2012
//For Validation Of Doc_Upload.aspx


function ValidateDocUpld(mode) {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtFromDepot", "Enter From Depot")
    //ValidateDropDown("ddlLocation", "Select Depot")
    ValidateRequired("txtFinYear", "Enter Fin Year")
    ValidateDropDown("ddlDocType", "Select Document Type")
    ValidateRequired("txtTitle", "Enter Title")
    ValidateRequired("txtDocNo", "Enter Doc No.")
    if (ValidateRequired("txtdocdt", "Enter Doc Date")) {
        if (CheckDateFormat("txtdocdt", "Date Format DD/MM/YYYY")) {
            ValidateGThanSystemDate("txtdocdt", "Doc Date Can not be Greater than Today")
        }
    }
    ValidateRequired("txtUpdatedBy", "Enter Uploaded By")
    ValidateRequired("txtUpdatedDt", "Enter Uploaded Date")
    if (mode == "Submit") { 
    ValidateRequired("sch_fld", "Please Upload A File")
    }
    


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        //        document.getElementById("").innerHTML = "";

        return false;
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = ''
        if (confirm('Are you sure to submit?')) {
            document.getElementById('btnSubmit').disabled = true;
            //                document.getElementById('btnsubmit').click();
            __doPostBack(document.getElementById('btnSubmit').name, '');
            //                document.getElementById('btnSubmit').disabled = true;

            return true;
        }
        else {
            return false;
        }

    }

}




function ValidateDocUpldUpdate() {
    firstErrorControl = "";
    errMsg = "";

    ValidateRequired("txtFromDepot", "Enter From Depot")
    //ValidateDropDown("ddlLocation", "Select Depot")
    ValidateRequired("txtFinYear", "Enter Fin Year")
    ValidateDropDown("ddlDocType", "Select Document Type")
    ValidateRequired("txtTitle", "Enter Title")
    ValidateRequired("txtDocNo", "Enter Doc No.")
    //ValidateRequired("txtdocdt", "Enter Doc Date")
    if (ValidateRequired("txtdocdt", "Enter Doc Date")) {
        if (CheckDateFormat("txtdocdt", "Date Format DD/MM/YYYY")) {
            ValidateGThanSystemDate("txtdocdt", "Doc Date Can not be Greater than Today")
        }
    }
    ValidateRequired("txtUpdatedBy", "Enter Uploaded By")
    ValidateRequired("txtUpdatedDt", "Enter Uploaded Date")
    //ValidateRequired("sch_fld", "Please Upload A File")


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        //        document.getElementById("").innerHTML = "";

        return false;
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = ''
        if (confirm('Are you sure to submit?')) {
            document.getElementById('btnSubmit').disabled = true;
            //                document.getElementById('btnsubmit').click();
            __doPostBack(document.getElementById('btnSubmit').name, '');
            //                document.getElementById('btnSubmit').disabled = true;

            return true;
        }
        else {
            return false;
        }
    }
}

function ValidateDocUpldDelete() { 
       if (confirm('Are you sure to Delete?')) {
           document.getElementById('btnDelete').disabled = true;
            //                document.getElementById('btnsubmit').click();
           __doPostBack(document.getElementById('btnDelete').name, '');
            //                document.getElementById('btnSubmit').disabled = true;

            return true;
        }
        else {
            return false;
        }
    }


//*****************************************************************************************************

var InvalidExt = "Invalid Upload File Extention";
var ext;
function fnDocExtGet(Company) {

    var fileName = document.getElementById('sch_fld').value;
    ext = fileName.substr(fileName.lastIndexOf(".") + 1, fileName.length);
    ext = ext.toUpperCase();

    if (ext != 'PDF') {
        CreateXMLHTTP();

        if (xmlHttp) {
            var requestURL = "AjaxServices.aspx?";
            requestURL += "Code=";
            requestURL += "DOC_EXT";
            requestURL += "&company=";
            requestURL += Company;
            requestURL += "&Doc_ext=";
            requestURL += ext;


            xmlHttp.onreadystatechange = doloadvalidExt;
            xmlHttp.open("GET", requestURL, true);
            xmlHttp.send(null);
        }
    }


}

function doloadvalidExt() {
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == 'complete') {
        if (xmlHttp.status == 200) {
            validExt(xmlHttp.responseText);
        }
        else {
            alert("There was a problem retrieving data from the server.");
        }
    }
}


function validExt(result) {

    try {

        if (result != 'PDF') {
            eval("var ProcessDetails =" + result);

            if (ProcessDetails != null && ProcessDetails.length > 0) {
                var resultExt = ProcessDetails[0].DocExtValue;
                if (ext == resultExt) {
                    if (resultExt == 'HTML' || resultExt == 'HTM') {
                        document.getElementById('addLink').style.display = 'block';
                    }
                    boolReturn = 'true';
                    document.getElementById('btnsubmit').disabled = false;
                }
                else {
                    boolReturn = 'false';
                    alert(ext + " : " + InvalidExt);
                    document.getElementById('btnsubmit').disabled = true;
                }
            }
            else {
                boolReturn = 'false';
                alert(ext + " : " + InvalidExt);
                document.getElementById('btnsubmit').disabled = true;
            }
        }
        else {

            boolReturn = 'false';
            alert(ext + " : " + InvalidExt);
            document.getElementById('btnsubmit').disabled = true;
        }


    }
    catch (e) {
        alert("There was a problem retrieving data from the server.");
        return false;
    }


}

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



function fnCheckExt() {

    if (document.getElementById('sch_fld').value != "") {
        var Exntsn = document.getElementById('sch_fld').value;
        var fileName = Exntsn

        var Extension = fileName.substr(fileName.lastIndexOf(".") + 1, fileName.length);
        Extension = Extension.toUpperCase();
        if (Extension != "DOC" && Extension != "DOCX" && Extension != "PDF" && Extension != "PPT" && Extension != "PPTX" && Extension != "XLS" && Extension != "XLSX" && Extension != "JPEG" && Extension != "JPG" && Extension != "TIF" && Extension != "TXT" && Extension != "GIF" && Extension != "PPS") {
            alert("Choose a Valid File");
            document.getElementById("sch_fld").focus()
            SetErrorColor("sch_fld", false);
            document.getElementById('btnSubmit').disabled = true;
            return false;
        }
        else
            SetErrorColor("sch_fld", true);
            document.getElementById('btnSubmit').disabled = false;
        return true;
    }
}