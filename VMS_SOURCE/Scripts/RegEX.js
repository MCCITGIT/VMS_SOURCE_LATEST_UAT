
//'**************************************************
//'Copyright	: VMS, MCC, KOLKATA
//'Source	    : Scripts/RegEX.js
//'Created Date	: 16-August-2007
//'Created By	: Arun Kumar
//'Version	    : R01.00.00
//'Description	: Validation purpose only

//'Modified By       Modified On       Version         Reason

//'*************************************************************

// JScript File

// List of Regular Expressions used
var zipRegEx = /\d{4}(-\d{4})?/;
var cityRegEx = /^[a-zA-Z. ]+$/;
var policyRegEx = /^\d\-\d\d\d\-\d\d$/;
var premiumRegEx =  /^(\-?((\d{1}\,?)?(\d{2}\,?)*))\d?\d?\d?\.?[0-9]*$/;
var declaredvalueRegEx = /^[0-9]+\.?[0-9]*$/;
var decimalRegEx=/^[0-9]+$/;
var phonenumberRegEx = /^\(\d{3}\) \d{3}\-\d{4}/;
var numRegEx = /^[0-9]*$/;
var alphanumRegEx = /^[a-zA-Z0-9]*$/;
var noalpha = /^[a-zA-Z]*$/;
var twodecRegEx = /^[0-9]*\.{0,1}[0-9]{1,2}$/;
var threedecRegEx = /^[0-9]*\.{0,1}[0-9]{1,3}$/;
var sixthreeDecimalRegEX = /^\d{1,6}(\.\d{1,3})?$/;
var eighttwoDecimalRegEX = /^\d{1,8}(\.\d{1,2})?$/;
var timeRegEx =/^(([0-9])|([0-1][0-9])|([2][0-3])):(([0-9])|([0-5][0-9]))$/;
var outRegEX = /^\d{1,9}(\.\d{1,2})?$/;
var phoneRegEx = /^[-,+,(,),0-9]*$/;
var emailRegEx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/;
var webRegEx = /(www|WWW).(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/
 var eighteentwoDecimalRegEx=/^\d{1,18}(\.\d{1,2})?$/;
 var shopboardDimensionRegEx = /^([0-9]|[0-9][0-9])(([.][0-9]{1})|([.][0-9]{2}))?$/;