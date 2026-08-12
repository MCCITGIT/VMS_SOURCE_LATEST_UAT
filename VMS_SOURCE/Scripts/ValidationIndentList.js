//'**************************************************
//'Copyright	: Berger, MCC, KOLKATA
//'Source	    : Scripts/ValidationDepotEnteredList.js
//'Created Date	: 16-December-2011
//'Created By	: Rohan Mazumdar
//'Version	    : R02.00.00
//'Description	: 

//'Modified By       Modified On       Version         Reason

//'*************************************************************


// JScript File

function rwslctToggleSelect(clkdCheckBox)
{

    var theGridView = document.getElementById('gvIndentList');

    var flag = 0;

    var chkbxcntrl_id = null;

    if (document.getElementById(clkdCheckBox).checked == true)
    {
        document.getElementById(clkdCheckBox).parentNode.parentNode.style.backgroundColor = "teal";
    }
    else
    {
        document.getElementById(clkdCheckBox).parentNode.parentNode.style.backgroundColor = "";
    }

    for (var rowCount = 1; rowCount < theGridView.rows.length; rowCount++)
    {

        if (theGridView.rows(rowCount).cells(0).childNodes.length != 0)
        {
            chkbxcntrl_id = theGridView.rows(rowCount).cells(0).children(0).id;

            if (document.getElementById(chkbxcntrl_id).checked == true)
            {
                flag = 1;
                break;
            }
        }
    }

    if (flag == 0)
    {
        if (document.getElementById('btnSubmit').disabled == false)
        {
            document.getElementById('btnSubmit').disabled = true;
        }
    }
    else
    {
        if (document.getElementById('btnSubmit').disabled == true)
        {
            document.getElementById('btnSubmit').disabled = false;
        }
    }

}

function validateForm()
{
    if (confirm('Are you sure to submit?'))
    {

        document.getElementById('btnSubmit').disabled = true;
        __doPostBack(document.getElementById('btnSubmit').name, '');

    }
    else
    {

        return false;
    }
}