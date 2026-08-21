var firstErrorControl;
var errMsg;

function validateFormulationMatrixSubmit() {
    firstErrorControl = "";
    errMsg = "";

    var lblErrorMessage = document.getElementById("lblErrorMessage");
    var hdnProductCode = document.getElementById("hdnProductCode");
    var grid = document.getElementById("gvFormulationMatrix");
    var rateInputs = grid ? grid.querySelectorAll("input[id$='txtRate']") : [];
    var submitBtn = document.getElementById("btnSubmit");

    if (lblErrorMessage) {
        lblErrorMessage.innerHTML = "";
    }

    if (!hdnProductCode || (hdnProductCode.value || "").trim() === "") {
        if (firstErrorControl === "") {
            firstErrorControl = "txtProductSearch";
        }
        errMsg += GetErrorRow("txtProductSearch", "Please select Product.");
    }

    if (!rateInputs || rateInputs.length === 0) {
        if (firstErrorControl === "") {
            firstErrorControl = "gvFormulationMatrix";
        }
        errMsg += GetErrorRow("gvFormulationMatrix", "No formulation details found for the selected product.");
    }

    for (var i = 0; i < rateInputs.length; i++) {
        var rateValue = (rateInputs[i].value || "").trim();
        if (rateValue === "") {
            if (firstErrorControl === "") {
                firstErrorControl = rateInputs[i].id;
            }
            errMsg += GetErrorRow(rateInputs[i].id, "Please enter Rate for all raw materials.");
            break;
        }

        var numericRate = parseFloat(rateValue);
        if (isNaN(numericRate) || numericRate <= 0) {
            if (firstErrorControl === "") {
                firstErrorControl = rateInputs[i].id;
            }
            errMsg += GetErrorRow(rateInputs[i].id, "Please enter a valid Rate greater than 0.");
            break;
        }
    }

    if (firstErrorControl !== "") {
        SetControlFocus(firstErrorControl);
        return rmFailValidation(errMsg);
    }

    var buttonText = submitBtn ? ((submitBtn.value || "") + "").toLowerCase() : "submit";
    return rmConfirmAction(submitBtn, buttonText.indexOf("update") >= 0 ? "update" : "submit");
}

function validateFormulationMatrixUpdate(el) {
    var source = el || (window.event ? window.event.srcElement : null);
    if (source && source.closest) {
        source = source.closest("a") || source;
    }
    var row = source ? source.closest("tr") : null;
    var rateInput = row ? row.querySelector("input[id$='txtRate']") : null;
    var rateValue = rateInput ? (rateInput.value || "").trim() : "";

    if (rateValue === "") {
        return rmFailValidation("Please enter Rate.");
    }

    var numericRate = parseFloat(rateValue);
    if (isNaN(numericRate) || numericRate <= 0) {
        return rmFailValidation("Please enter a valid Rate greater than 0.");
    }

    var hdnId = row ? row.querySelector("input[id$='hdnId']") : null;
    var matrixId = hdnId ? parseInt(hdnId.value || "0", 10) : 0;
    return rmConfirmAction(source, matrixId > 0 ? "update" : "submit");
}
