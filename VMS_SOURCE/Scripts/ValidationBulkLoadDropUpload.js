// JScript File

function ValidateSubmit() {
    firstErrorControl = "";
    errMsg = "";

    var lblMsg = document.querySelector("[id$='lblMsg']");
    if (lblMsg) {
        lblMsg.innerHTML = "";
    }

    var fileUpload = document.querySelector("input[type='file'][id$='fupUploadFile']");

    if (!fileUpload || fileUpload.value === "") {
        if (fileUpload) {
            ValidateRequired(fileUpload.id, "Please upload an Excel file.");
        } else {
            errMsg += GetErrorRow("fupUploadFile", "Please upload an Excel file.");
            firstErrorControl = "fupUploadFile";
        }
    } else {
        var fileName = fileUpload.value.toLowerCase();
        var dotIndex = fileName.lastIndexOf(".");
        var fileExt = dotIndex >= 0 ? fileName.substring(dotIndex) : "";

        if (fileExt !== ".xls") {
            if (firstErrorControl === "") {
                firstErrorControl = fileUpload.id;
            }
            errMsg += GetErrorRow(fileUpload.id, "Only Excel files (.xls) allowed.");
            SetErrorColor(fileUpload.id, false);
        } else {
            SetErrorColor(fileUpload.id, true);
        }
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        if (lblMsg) {
            lblMsg.innerHTML = errMsg;
        }
        return false;
    }

    return confirm("Are you sure to submit?");
}
