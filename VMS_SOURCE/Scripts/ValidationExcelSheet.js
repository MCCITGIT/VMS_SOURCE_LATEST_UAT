// JScript File


var srchval='';

function fnQryChk()
{

//defining Array for invalid Command/keyword
   var arrddl =new Array(4);
   
   arrddl[0]="CREATE";
   arrddl[1]="ALTER";
   arrddl[2]="DROP";
   arrddl[3]="TRUNCATE";
   arrddl[4]="RENAME";
       
   
   var arrdml =new Array(3);
   arrdml[0]="INSERT";
   arrdml[1]="UPDATE";
   arrdml[2]="DELETE";
   arrdml[3]="MERGE";
   
   var arrdcl =new Array(5);
   arrdcl[0]="GRANT";
   arrdcl[1]="REVOKE";
   arrdcl[2]="INTO";
   arrdcl[3]="SYS";
   arrdcl[4]="TABLE";
   arrdcl[5]="TAB";
   
   var arrtcl =new Array(2);
   arrtcl[0]="COMMIT";
   arrtcl[1]="SAVEPOINT";
   arrtcl[2]="ROLLBACK";
   
    
    var typetxt='';
   var asciiCode=event.keyCode;
   if (asciiCode==32 || asciiCode==40 || asciiCode==41 || asciiCode==59)
   {
   //chceking for ddl
    for(var i=0 ;i<=arrddl.length-1;i++)
    {
      if (srchval==arrddl[i])
      {
        alert(srchval+" is Invalid in Query\nPlease Rewrite Query");
       document.getElementById('txtQuery').value='';
       document.getElementById('txtQuery').focus();
       document.getElementById('txtQuery').style.backgroundColor = "yellow";
       srchval='';
       return false;
      }
    } 
    
     //chceking for dml
     for(var i=0 ;i<=arrdml.length-1;i++)
    {
      if (srchval==arrdml[i])
      {
       alert(srchval+" is a Invalid Keyword \nPlease Rewrite Query");
       document.getElementById('txtQuery').value='';
       document.getElementById('txtQuery').focus();
       document.getElementById('txtQuery').style.backgroundColor = "yellow";
       srchval='';
       return false;
      }
    } 
     //chceking for dcl
     for(var i=0 ;i<=arrdcl.length-1;i++)
    {
      if (srchval==arrdcl[i])
      {
      alert(srchval+" is a Invalid Keyword \nPlease Rewrite Query");
       document.getElementById('txtQuery').value='';
       document.getElementById('txtQuery').focus();
       document.getElementById('txtQuery').style.backgroundColor = "yellow";
       srchval='';
       return false;
      }
    } 
    
     //chceking for tcl
     for(var i=0 ;i<=arrtcl.length-1;i++)
    {
      if (srchval==arrtcl[i])
      {
        alert(srchval+" is a Invalid Keyword \nPlease Rewrite Query");
       document.getElementById('txtQuery').value='';
       document.getElementById('txtQuery').focus();
       document.getElementById('txtQuery').style.backgroundColor = "yellow";
       srchval='';
       return false;
      }
    } 
    
    
    
    srchval='';
   }
   else
   {
    typetxt=asciiValue(asciiCode);
    srchval=srchval+typetxt;
    
    }
  

   
   
}


/* Function For Converting Ascii Decimal Code To Equivalent Values */
function asciiValue(code)
{

 var val='';
  switch (code)
  {
  
//   case 32:
//   val=' ';
//   break;
//   case 50:
//   val='(';
//   break;
//    case 51:
//   val=')';
//   break;
//    case 59:
//   val=';';
//   break;
   
  //upper case letter
   case 65:
   val='A';
   break;
    case 66:
   val='B';
   break;
    case 67:
   val='C';
   break;
    case 68:
   val='D';
   break;
    case 69:
   val='E';
   break;
    case 70:
   val='F';
   break;
    case 71:
   val='G';
   break;
    case 72:
   val='H';
   break;
    case 73:
   val='I';
   break; 
    case 74:
   val='J';
   break;
    case 75:
   val='K';
   break;
    case 76:
   val='L';
   break;
    case 77:
   val='M';
   break;
    case 78:
   val='N';
   break;
    case 79:
   val='O';
   break;
    case 80:
   val='P';
   break;
    case 81:
   val='Q';
   break;
    case 82:
   val='R';
   break;
    case 83:
   val='S';
   break; 
   case 84:
   val='T';
   break;
    case 85:
   val='U';
   break;
    case 86:
   val='V';
   break;
    case 87:
   val='W';
   break;
    case 88:
   val='X';
   break;
    case 89:
   val='Y';
   break;
    case 90:
   val='Z';
   break;
   
  //smaller case letters 
   case 97:
   val='a';
   break;
    case 98:
   val='b';
   break;
    case 99:
   val='c';
   break;
    case 100:
   val='d';
   break;
    case 101:
   val='e';
   break;
    case 102:
   val='f';
   break;
    case 103:
   val='g';
   break;
    case 104:
   val='h';
   break;
    case 105:
   val='i';
   break; 
    case 106:
   val='j';
   break;
    case 107:
   val='k';
   break;
    case 108:
   val='l';
   break;
    case 109:
   val='m';
   break;
    case 110:
   val='n';
   break;
    case 111:
   val='o';
   break;
    case 112:
   val='p';
   break;
    case 113:
   val='q';
   break;
    case 114:
   val='r';
   break;
    case 115:
   val='s';
   break; 
   case 116:
   val='t';
   break;
    case 117:
   val='u';
   break;
    case 118:
   val='v';
   break;
    case 119:
   val='w';
   break;
    case 120:
   val='x';
   break;
    case 121:
   val='y';
   break;
    case 122:
   val='z';
   break;
   
   
  }
  
  
  return val.toUpperCase() ;

}


function validQry()
{

var arrddl =new Array(4);
   
   arrddl[0]="CREATE";
   arrddl[1]="ALTER";
   arrddl[2]="DROP";
   arrddl[3]="TRUNCATE";
   arrddl[4]="RENAME";
       
   
   var arrdml =new Array(3);
   arrdml[0]="INSERT";
   arrdml[1]="UPDATE";
   arrdml[2]="DELETE";
   arrdml[3]="MERGE";
   
   var arrdcl =new Array(5);
   arrdcl[0]="GRANT";
   arrdcl[1]="REVOKE";
   arrdcl[2]="INTO";
   arrdcl[3]="SYS";
   arrdcl[4]="TABLE";
   arrdcl[5]="TAB";
   
   var arrtcl =new Array(2);
   arrtcl[0]="COMMIT";
   arrtcl[1]="SAVEPOINT";
   arrtcl[2]="ROLLBACK";
 var word='';
 var ele='';
 var qryword =new Array();
 var k=0;
 var txt=document.getElementById('txtQuery').value;
 
         for (var j=0 ;j<=txt.length-1;j++)
         {
          
           ele=txt.charAt(j);
           if (!(ele==' '|| ele=='(' ||ele==')' ||ele==';'))
           {
             word=word+ele;
            
           }
          
            else 
            {
              if (word !='')
              { 
                qryword.push(word.toUpperCase());
              }
               word='';
            }
            
              
         }
         
     if (word !='')
     { 
       qryword.push(word.toUpperCase());
     } 
 for (var n=0;n<=qryword.length-1;n++)
 {
 
  for(var i=0 ;i<=arrddl.length-1;i++)
    {
      if (qryword[n]==arrddl[i])
      return false;
     
    } 
    
     //chceking for dml
     for(var i=0 ;i<=arrdml.length-1;i++)
    {
      if (qryword[n]==arrdml[i])
         return false;
        
    } 
     //chceking for dcl
     for(var i=0 ;i<=arrdcl.length-1;i++)
    {
      if (qryword[n]==arrdcl[i])
         return false;
         
    } 
    
     //chceking for tcl
     for(var i=0 ;i<=arrtcl.length-1;i++)
    {
      if (qryword[n]==arrtcl[i])
        return false;
        
    } 
    
   }
    
}



function Validateqry(controlName, errorMessage)
{
    var errorCode=true;
    var controlID = controlName;
    errorCode = validQry(controlID);
    if(!errorCode)
    {    
      //if(firstErrorControl == '') 
      firstErrorControl = controlID;      
      errMsg += GetErrorRow(controlID, errorMessage);
      SetErrorColor(controlID, false);
    }
    else
      SetErrorColor(controlID, true);
}

function ValidateExcelQry()
{
   firstErrorControl ="";
   errMsg= "";
  
   Validateqry("txtQuery", invalidqry)
   
   if(firstErrorControl!="")
    {       
        SetControlFocus(firstErrorControl);
        errMsg = "<table>" + errMsg + "</table>";
        document.getElementById("divErrorMessage").innerHTML = errMsg;      
        
        srchval='';
        return false;
    }
    
    
    else
    {    
       if(confirm ('Are you sure to submit?'))
        return true;            
         else
         return false;
    }
 }