var firstErrorControl = "";
var errMsg = "";

function validateBulkIndentSubmit() {
    firstErrorControl = "";
    errMsg = "";

    var lblMsg = document.getElementById("lblMsg");
    if (lblMsg) {
        lblMsg.innerHTML = "";
        lblMsg.style.color = "red";
    }

    var fileUpload = document.getElementById("fupUploadFile");
    if (!fileUpload || fileUpload.value.replace(/^\s+|\s+$/g, "") === "") {
        if (lblMsg) {
            lblMsg.innerHTML = "Please upload an Excel file.";
        }
        if (fileUpload) {
            SetErrorColor("fupUploadFile", false);
            SetControlFocus("fupUploadFile");
        }
        return false;
    }

    SetErrorColor("fupUploadFile", true);

    var fileName = fileUpload.value;
    var extension = fileName.substr(fileName.lastIndexOf(".") + 1).toLowerCase();
    if (extension !== "xls" && extension !== "xlsx") {
        if (lblMsg) {
            lblMsg.innerHTML = "Only Excel files (.xls/.xlsx) allowed.";
        }
        SetErrorColor("fupUploadFile", false);
        SetControlFocus("fupUploadFile");
        return false;
    }

    return confirm("Are you sure to save?");
}

function validateBulkIndentConfirm() {
    var lblMsg = document.getElementById("lblMsg");
    if (lblMsg) {
        lblMsg.innerHTML = "";
    }

    return confirm("Are you sure to save?");
}
