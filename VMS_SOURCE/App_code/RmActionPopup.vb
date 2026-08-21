Imports System.Web.UI

Public Module RmActionPopup
    Public Sub Show(page As Page, message As String, isSuccess As Boolean, Optional redirectUrl As String = Nothing)
        If page Is Nothing Then
            Return
        End If

        Dim text As String = Convert.ToString(message).Trim()
        If String.IsNullOrWhiteSpace(text) Then
            Return
        End If

        Dim safeMessage As String = text.Replace("\", "\\").Replace("'", "\'").Replace(ControlChars.Cr, " ").Replace(ControlChars.Lf, " ")
        Dim successFlag As String = If(isSuccess, "true", "false")
        Dim redirectJs As String = "null"
        If Not String.IsNullOrWhiteSpace(redirectUrl) Then
            redirectJs = "'" & redirectUrl.Trim().Replace("\", "\\").Replace("'", "\'") & "'"
        End If
        Dim script As String =
            "window.__rmPendingActionResult={message:'" & safeMessage & "',success:" & successFlag & ",redirect:" & redirectJs & "};" &
            "if(window.rmShowResult){rmShowResult(window.__rmPendingActionResult.message, window.__rmPendingActionResult.success, window.__rmPendingActionResult.redirect); window.__rmPendingActionResult=null;}"

        ScriptManager.RegisterStartupScript(page, page.GetType(), "rmActionResult", script, True)
    End Sub

    Public Sub ShowSuccess(page As Page, message As String, Optional redirectUrl As String = Nothing)
        Show(page, message, True, redirectUrl)
    End Sub

    Public Sub ShowError(page As Page, message As String)
        Show(page, message, False)
    End Sub
End Module
