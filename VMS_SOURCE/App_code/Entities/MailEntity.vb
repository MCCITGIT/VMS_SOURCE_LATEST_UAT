'**************************************************
'Copyright	    : PROLEAD, MCC, Kolkata
'Source	        : app_code/Entity/FollowupLogEntity.vb
'Created Date	: 14 Nov 2010
'Created By	    : Neeraj
'Version	    : 1.00.00
'Description	: Code Entity file for FollowupLogEntity 

'Modified By       Modified On       Version         Reason

'*************************************************************
Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes
'Namespace PROTECTON.Web
Public Class MailEntity
    Private FromAdd As String
    Private ToAdd As String
    Private CCAdd As String
    Private BccAdd As String
    Private Subject As String
    Private Body As String
    Private Attachment As String
    Private SenderApp As String
    Private SenderTask As String
    Sub New()
        FromAdd = "BERGER VMS PORTAL SERVICE"
        ToAdd = String.Empty
        CCAdd = String.Empty
        BccAdd = String.Empty
        Subject = String.Empty
        Body = String.Empty
        Attachment = String.Empty
        SenderApp = "vms_source"
        SenderTask = String.Empty
    End Sub

    Public Property FromAddress() As String
        Get
            Return FromAdd
        End Get
        Set(ByVal value As String)
            FromAdd = value
        End Set
    End Property
    Public Property ToAddress() As String
        Get
            Return ToAdd
        End Get
        Set(ByVal value As String)
            ToAdd = value
        End Set
    End Property
    Public Property CCAddress() As String
        Get
            Return CCAdd
        End Get
        Set(ByVal value As String)
            CCAdd = value
        End Set
    End Property
    Public Property BCCAddress() As String
        Get
            Return BccAdd
        End Get
        Set(ByVal value As String)
            BccAdd = value
        End Set
    End Property
    Public Property MailSubject() As String
        Get
            Return Subject
        End Get
        Set(ByVal value As String)
            Subject = value
        End Set
    End Property
    Public Property MailBody() As String
        Get
            Return Body
        End Get
        Set(ByVal value As String)
            Body = value
        End Set
    End Property
    Public Property Attachment_Path() As String
        Get
            Return Attachment
        End Get
        Set(ByVal value As String)
            Attachment = value
        End Set
    End Property
    Public Property Sender_App() As String
        Get
            Return SenderApp
        End Get
        Set(ByVal value As String)
            SenderApp = value
        End Set
    End Property
    Public Property Sender_Task() As String
        Get
            Return SenderTask
        End Get
        Set(ByVal value As String)
            SenderTask = value
        End Set
    End Property
End Class
Class MailEntityNew
    Public Property mailToAddress As String
    Public Property mailFromAddress As String
    Public Property mailCCAddress As String
    Public Property mailBCCAddress As String
    Public Property mailSubject As String
    Public Property mailBody As String
    Public Property mailAttachement As String
    Public Property mailSenderApp As String
    Public Property mailSenderTask As String
End Class

Public Class MailResponse
    Public Property responseCode As Integer
    Public Property responseMsg As String
End Class
'End Namespace


