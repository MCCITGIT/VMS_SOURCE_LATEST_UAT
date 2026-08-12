function isDecimal(txt) {
    firstErrorControl = "";
    errMsg = "";
    SetErrorColor(txt, true);
    if (document.getElementById(txt).value != "") {
        ValidateDecimal(txt, "");
        SetErrorColor(txt, true);
    }

    if (firstErrorControl != "") {
        document.getElementById(txt).value = "";
        window.alert("Please enter a valid Number.");
        return false;
    }
    else {
        if (document.getElementById(txt).value == 0) {
            if (document.getElementById(txt).value != "") {
                document.getElementById(txt).value = "";
                window.alert("Please enter a value greater than zero.");
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }

    }
}

function validateSubmit() {
    firstErrorControl = "";
    errMsg = "";
    var grid = document.getElementById("gvProductList");
    var flag = false;
    for (var i = 0;i < grid.rows.length; i++)
    {
        if (i != 0 && i != grid.rows.length-1)
        {
            var chkBox = grid.rows[i].querySelectorAll("input[type=checkbox]");        
            if(chkBox[0].checked)
            {
                flag = true;
                break;
            }
            else
            {
                flag = false;
            }
        }       
    }
    if (flag)
    {
        var mainflag = false;
        for (var i = 0; i < grid.rows.length; i++)
        {
            if (i != 0 && i != grid.rows.length - 1)
            {
                var chkBox = grid.rows[i].querySelectorAll("input[type=checkbox]");
                if (chkBox[0].checked)
                {
                    var txtBox = grid.rows[i].querySelectorAll("input[type=text]");
                    if(txtBox[0].value == "")
                    {
                        SetErrorColor(txtBox[0].id, false);
                        document.getElementById("lblErrorMessage").innerHTML = "Please enter a denomination.";
                        return false;
                    }
                    else
                    {
                        var select = grid.rows[i].querySelectorAll("select");
                        if(select[0].value == "")
                        {
                            SetErrorColor(select[0].id, false);
                            document.getElementById("lblErrorMessage").innerHTML = "Please select a token vendor.";
                            return false;
                        }
                        else
                        {
                            mainflag = true;
                          
                        }
                    }
                }
               
            }

        }
        if(mainflag)
        {
            document.getElementById("lblErrorMessage").innerHTML = "";
            return confirm("Are you sure to submit this?");
        }
        else
        {
            return false;
        }
    }
    else
    {
        //window.alert("Please select atleast one row.");
        return true;
    }
   
}
