// JScript File
/*'**************************************************
'Copyright	    : MCCSOP, MCC, Kolkata
'Source	        : FileView.js
'Created Date	: 6-july-2010
'Created By	    : Neeraj
'Version	    : 1.00.00
'Description	: JScript file for FileView Page

'Modified By       Modified On       Version         Reason

'************************************************************/

var message="";

function right(e) {
if (navigator.appName == 'Netscape' && (e.which == 3 || e.which == 2)){
alert(message);
return false;
}
else if (navigator.appName == 'Microsoft Internet Explorer' && (event.button == 2 || event.button == 3  )) {
alert(message);
return false;

}
return false;
}

document.onmousedown=right;
document.onmouseup=right;
if (document.layers) window.captureEvents(Event.MOUSEDOWN);
if (document.layers) window.captureEvents(Event.MOUSEUP);
window.onmousedown=right;
window.onmouseup=right;
