Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Text

Namespace VMS.Common
    Public Class VMSCOMMON
        'Define Constant Common for Project.
        Public Const INVTYPE As String = "INV_TYPE"
        Public Const SUBVERTICAL As String = "SUBVERTICAL"
        Public Const WRKESTTYPE As String = "WRK_EST_TYPE"
        Public Const CURRENCY As String = "CURRENCY"


        Public Const DOCTYPES As String = "DOC_TYPE"
        Public Const COMPANIES As String = "COMPANY"
        Public Const TERRITORY As String = "TERRITORY"
        Public Const GEOGRAPHIES As String = "GEOGRAPHIES"
        Public Const LOCATION As String = "LOCATION"
        Public Const UNITS As String = "UNITS"
        Public Const PRIORITY As String = "PRIORITY"

        Public Const POCATEGORY As String = "POCATEGORY" 'ADDED BY VENKAT ON 30-05-2006
        Public Const POEXCHANGETYPE As String = "POEXCHANGETYPE"
        Public Const POCONSIGN As String = "POCONSIGN"
        Public Const POTAXBASIS As String = "POTAXBASIS"
        Public Const POTYPE As String = "POTYPE"


        Public Const POSTATUS As String = "POSTATUS"
        Public Const CAPEXLIMIT As String = "CAPEX-LIMIT"


        Public Const VERTICALS As String = "VERTICAL"
        Public Const DEPARTMENTS As String = "DEPARTMENTS"
        Public Const DESG_GROUP As String = "DESG_GROUP"

        Public Const CDDRIVE As String = "CDDRIVE"
        Public Const LOOK As String = "LOOK"
        Public Const MAKE As String = "MAKE"

        Public Const LOV_CODE As String = "LOV_CODE"
        Public Const LOV_DESCRIPTION As String = "LOV_SHRT_DESC"
        Public Const LOV_LINK1 As String = "FIELD1_VALUE"  'Field1 of Lov
        Public Const LOV_LINK2 As String = "FIELD2_VALUE"
        Public Const LOV_LINK3 As String = "FIELD3_VALUE"

        Public Const PROJ_CLOSED_CODE = "C"
        Public Const PROJ_ACTIVE_CODE = "A"
        Public Const PROJ_CLOSED_DESC = "Closed"
        Public Const PROJ_ACTIVE_DESC = "Active"

        Public Const RECORD_ACTIVE As String = "A"
        Public Const RECORD_DELETED As String = "D"

        'added by anil
        Public Const NO_PRIVILEGES As String = "Error.aspx?Err=You are not authorised."
        Public Const GCOC_RECORD_ADD_STATUS As String = "Geo COC is Created."
        Public Const GCOC_RECORD_EDIT_STATUS As String = "Geo COC is Modified."
        Public Const SOW_RECORD_ADD_STATUS As String = "SOW is Created."
        Public Const SOW_RECORD_EDIT_STATUS As String = "SOW is Modified."
        Public Const CUST_RECORD_ADD_STATUS As String = "Customer is created."
        Public Const CUST_RECORD_EDIT_STATUS As String = "Customer is Modified."
        Public Const ERR_TEXT As String = "Error Occured"
        Public Const PREVILEGE_ADD As String = ",A,"
        Public Const PREVILEGE_MODIFY As String = ",E,"
        Public Const PREVILEGE_DELETE As String = ",D,"
        Public Const PREVILEGE_VIEW As String = ",V,"
        Public Const PREVILEGE_PRINT As String = ",P,"
        Public Const PREVILEGE_A1_APPROVAL As String = ",A1,"
        Public Const PREVILEGE_A2_APPROVAL As String = ",A2,"
        Public Const NO_OF_RECORDS_PER_PAGE As Int32 = 30


        'ADDED BY RAJENDRAPRASAD 10-01-2005
        Public Const PRIMARYKEY_VIOLATION As String = "Record already exists with this code...Pl. enter some other code"
        'Billing Work Estimation Types
        Public Const FIXED As String = "FIXED"
        Public Const TM As String = "TM"
        Public Const QUAN As String = "QUAN"
        Public Const intPwdChangeDays As Integer = 45

        'Added By Charan
        Public Const DOCTYPE_GCOC As String = "GCOC"
        Public Const DOCTYPE_COC As String = "COC"
        Public Const DOCTYPE_SOW As String = "SOW"
        Public Const DOCTYPE_CPO As String = "CPO"
        Public Const DOCTYPE_IPO As String = "IPO"
        Public Const DOCTYPE_WO As String = "WO"
        Public Const DOCTYPE_BA As String = "BA"
        Public Const DOCTYPE_INV As String = "INV"
        Public Const DOCTYPE_PYMNT As String = "PYMNT"

        'Added By Charan for Delivery Head Company
        Public Const DELHEAD_COMP As String = "IEL"
        Dim cell As TableCell

        'MAILER DETAILS
        Public Const OtbsAdminName As String = "Neelima"
        Public Const SMTP_IP As String = "192.168.240.100"
        Public Const MAILFROM As String = "e-Procure@infotechsw.com"
        Public Const MAILSUBJECT As String = "e-Procure-Approval Pending"

        '///////////////////////////////////////////////////////////////////////////////////*/
        ' Months
        '///////////////////////////////////////////////////////////////////////////////////*/

        'Public Shared MonthNames As String() = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}

        Public Function SplitString(ByVal strValue As String, ByVal intLen As Int32) As String
            If strValue <> "" AndAlso strValue.Length > intLen Then
                Dim intI, intJ As Int32
                Dim strTempResult, strResult As String

                intI = 0
                intJ = 0

                strResult = (strValue.Length / intLen).ToString

                If strResult.IndexOf(".") > 0 Then
                    intI = CType(strResult.Substring(0, strResult.IndexOf(".")), Int32)
                Else
                    intI = CType(strResult, Int32)
                End If

                For intJ = 1 To intI
                    strTempResult = strTempResult & Mid(strValue, 1, intLen) & "-<br>"
                    strValue = Mid(strValue, intLen + 1, Len(strValue))
                Next
                Return strTempResult & strValue
            Else
                Return strValue
            End If
        End Function

        Public Function MailFooter() As String
            Dim strFoter As New StringBuilder

            strFoter.Append("</td></tr><tr><td width='100%'><table width=100% border=0><tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr><tr><td width='100%'><font face=Verdana color='#000000'>")
            strFoter.Append("<a href='https://infotech4all.com/eprocure/index_1024.aspx' target='_blank'><b>Click here</b></a>&nbsp;</span></b></font> for Approval.</td></tr><tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr><tr><td width='100%'><font face='Verdana' size='2'><b>Navigation:</b></td></tr>")

            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Login->homepage->Quick Links->Approval Screen  (or) <BR></font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Login->homepage->Action Required->Click on the necessary action->Approval Screen.</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Thanking you</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Best Regards</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'><b>e-Procure - Administrator.<BR></b></font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'><i><b>Note: This is e-Procure system generated mail.Do not reply back to this mail.</b></i></font></td></tr>")
            strFoter.Append("</table></td></tr></table></body></html>")

            Return strFoter.ToString
        End Function
        Public Function MailHeader(ByVal strType As String, ByVal strStatus As String) As String
            Dim strHeader As New StringBuilder
            strHeader.Append("<html><body><table border='1' width='90%' cellspacing='0' cellpadding='0' bordercolor='#3056a7' bordercolorlight='#0198cf' bordercolordark='#0198cf'>")
            strHeader.Append("<tr><td width='100%'><img border='0' src='http://infotech4all.com/eprocure/images/mailer.jpg' width='910'></td></tr><tr><td width='100%'>")

            strHeader.Append("<font size='2' face='Arial'><BR>The following ")
            strHeader.Append(strType)
            If strStatus = "Y" Then
                strHeader.Append(" is to be approved.")
            ElseIf strStatus = "G" Then
                strHeader.Append(" is to be filled.")
            Else
                strHeader.Append(" is Rejected.")
            End If

            strHeader.Append(" Kindly review and do the needful.")
            strHeader.Append("</font><font face='Verdana' size='2'><br></font><font face='Arial' size='2'><br>")
            strHeader.Append("</font></td></tr><tr><td width='100%'>")

            Return strHeader.ToString
        End Function
        Public Function MailFooterGriar() As String
            Dim strFoter As New StringBuilder

            strFoter.Append("</td></tr><tr><td width='100%'><table width=100% border=0><tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr><tr><td width='100%' ><font face=Verdana color='#000000'>")
            strFoter.Append("<a href='https://infotech4all.com/eprocure/index_1024.aspx' target='_blank'><b>Click here</b></a>&nbsp;</span></b></font> to login.</td></tr><tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr><tr><td width='100%'><font face='Verdana' size='2'><b>Navigation:</b></td></tr>")

            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Login->homepage->Quick Links->Griar  (or) <BR></font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Login->homepage->Action Required->Click on the necessary action->Griar.</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Thanking you</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Best Regards</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'><b>e-Procure - Administrator.<BR></b></font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'><i><b>Note: This is e-Procure system generated mail.Do not reply back to this mail.</b></i></font></td></tr>")
            strFoter.Append("</table></td></tr></table></body></html>")

            Return strFoter.ToString
        End Function
        Public Function MailHeaderLaptop(ByVal strType As String, ByVal strStatus As String) As String
            Dim strHeader As New StringBuilder
            strHeader.Append("<html><body><table border='1' width='90%' cellspacing='0' cellpadding='0' bordercolor='#3056a7' bordercolorlight='#0198cf' bordercolordark='#0198cf'>")
            strHeader.Append("<tr><td width='100%'><img border='0' src='http://infotech4all.com/eprocure/images/mailer.jpg' width='910'></td></tr><tr><td width='100%'>")


            If strStatus = "R" Then
                strHeader.Append("<font size='2' face='Arial'><BR>The following ")
                strHeader.Append(strType)
                strHeader.Append(" is due from you.")
                strHeader.Append(" Kindly return. </font><font face='Verdana' size='2'><br></font><font face='Arial' size='2'><br>")
            ElseIf strStatus = "LP" Then
                strHeader.Append("<font size='2' face='Arial'><BR>The following ")
                strHeader.Append(strType)
                strHeader.Append(" is pending for approval.")
                strHeader.Append(" Kindly review and do the needful. </font><font face='Verdana' size='2'><br></font><font face='Arial' size='2'><br>")
            ElseIf strStatus = "LA" Then
                strHeader.Append("<font size='2' face='Arial'><BR>")
                strHeader.Append(strType)
                strHeader.Append(" is allocated.")
                strHeader.Append("</font><font face='Verdana' size='2'><br></font><font face='Arial' size='2'><br>")
            ElseIf strStatus = "LR" Then
                strHeader.Append("<font size='2' face='Arial'><BR>")
                strHeader.Append(strType)
                strHeader.Append(" is rejected.")
                strHeader.Append("</font><font face='Verdana' size='2'><br></font><font face='Arial' size='2'><br>")
            End If

            strHeader.Append("</font></td></tr><tr><td width='100%'>")

            Return strHeader.ToString
        End Function
        Public Function MailFooterLaptop() As String
            Dim strFoter As New StringBuilder

            strFoter.Append("</td></tr><tr><td width='100%'><table width=100% border=0><tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr><tr><td width='100%'><font face=Verdana color='#000000'>")
            'strFoter.Append("<a href='https://infotech4all.com/eprocure/index_1024.aspx' target='_blank'><b>Click here</b></a>&nbsp;</span></b></font> for Approval.</td></tr><tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr><tr><td width='100%'><font face='Verdana' size='2'><b>Navigation:</b></td></tr>")

            'strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Login->homepage->Quick Links->Approval Screen  (or) <BR></font></td></tr>")
            ' strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Login->homepage->Action Required->Click on the necessary action->Approval Screen.</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Thanking you</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>Best Regards</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'><b>e-Procure - Administrator.<BR></b></font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'>&nbsp;</font></td></tr>")
            strFoter.Append("<tr><td width='100%'><font face='Verdana' size='2'><i><b>Note: This is e-Procure system generated mail.Do not reply back to this mail.</b></i></font></td></tr>")
            strFoter.Append("</table></td></tr></table></body></html>")

            Return strFoter.ToString
        End Function

        Public Function fnRound(ByVal dclVal, ByVal pre) As String
            If IsNumeric(dclVal) Then
                Return Replace(FormatNumber(dclVal, pre), ",", "")
            Else : Return ""
            End If
        End Function
    End Class
End Namespace