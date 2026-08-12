

var firstErrorControl;
var errMsg;
function validateBrandProductLinkAdd() {

    firstErrorControl = "";
    errMsg = "";
    ValidateDropDown1("ddlBrand", "Please Select Brand.");


    if (firstErrorControl != "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("lblErrorMessage").innerHTML = errMsg;
        return false;
    }
    else {

        document.getElementById("lblErrorMessage").innerHTML = '';
        if (confirm("Are you sure to submit?")) {
            document.getElementById("btnSubmit").disabled = true;
            //document.getElementById("ctl00_ContentPlaceHolder1_btnReset").disabled = true;
            //document.getElementById(btnSave).click();
            __doPostBack(document.getElementById("btnSubmit").name, '');
            //document.getElementById(btnSave).disabled = true;
            return true;
        }
        else {
            return false;
        }
    }
}

function searchText() {
    clearSearch();
    var searchtxt = document.getElementById('searchInput').value;
    var checkboxList = document.getElementById('chkbxListApplProducts');
    var searchExp = new RegExp(searchtxt, 'gi'); // 'gi' for global and case-insensitive search

    var labels = checkboxList.getElementsByTagName('label');
    var found = false;
    debugger
    for (var i = 0; i < labels.length; i++) {
        var labelText = labels[i].textContent;
        if (searchExp.test(labelText)) {
            labels[i].innerHTML = labelText.replace(searchExp, '<span class="highlight">$&</span>');
            labels[i].scrollIntoView({ behavior: 'smooth', block: 'center' }); // Scroll to the label
            labels[i].focus(); // Focus on the label
            found = true;
            break; // Stop after first match if you only want to focus on the first match
        }
    }
    var inputElement = document.getElementById('searchInput');
    inputElement.focus();
}

function clearSearch() {
    var content = document.getElementById('chkbxListApplProducts');
    var html = content.innerHTML;
    html = html.replace(/<span class="highlight">|<\/span>/gi, '');
    content.innerHTML = html;
}