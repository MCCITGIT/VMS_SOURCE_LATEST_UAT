
Imports System.Drawing

Partial Class CircularProgressBar
    Inherits System.Web.UI.UserControl
    Public Property RatingValue As Double = 0
    Public Property RatingLabel As String = "N/A"
    Public Property ProgressAngle As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' If not set externally, use defaults
        If RatingLabel = "N/A" Then
            SetRating(RatingValue)
        End If
    End Sub

    Public Sub SetRating(value As Double)
        RatingValue = value
        'value = 95
        Dim barColor As String = "#6ca700"
        ProgressAngle = CInt((value / 100.0) * 360)

        If value > 79 AndAlso value <= 100 Then
            RatingLabel = "Platinum"
            barColor = "#b68900"
        ElseIf value > 59 AndAlso value <= 79 Then
            RatingLabel = "Gold"
            barColor = "#66b201"
        ElseIf value > 50 AndAlso value <= 59 Then
            RatingLabel = "Silver"
            barColor = "#b31400"
        ElseIf value >= 0 AndAlso value <= 50 Then
            RatingLabel = "Bronze"
            barColor = "#008db6"
        End If
        circleWrapDiv.Attributes("style") = "background: conic-gradient(" & barColor & " " & ProgressAngle & "deg, #e6e6e6 0deg);"

    End Sub
End Class
