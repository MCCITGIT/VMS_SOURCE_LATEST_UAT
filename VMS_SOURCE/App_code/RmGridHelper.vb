Imports System.Data
Imports System.Web.UI.WebControls

Public Module RmGridHelper
    Public Function GetTable(source As Object) As DataTable
        If TypeOf source Is DataTable Then
            Return CType(source, DataTable)
        End If

        If TypeOf source Is DataSet Then
            Dim ds As DataSet = CType(source, DataSet)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            End If
        End If

        Return Nothing
    End Function

    Public Sub BindPaged(grid As GridView, source As Object)
        Dim table As DataTable = GetTable(source)

        If grid Is Nothing Then
            Return
        End If

        If table Is Nothing OrElse table.Rows.Count = 0 Then
            grid.PageIndex = 0
            grid.DataSource = Nothing
            grid.DataBind()
            Return
        End If

        If grid.AllowPaging AndAlso grid.PageSize > 0 Then
            Dim pageCount As Integer = CInt(Math.Ceiling(table.Rows.Count / CDbl(grid.PageSize)))
            If grid.PageIndex >= pageCount Then
                grid.PageIndex = Math.Max(pageCount - 1, 0)
            End If
        End If

        grid.DataSource = table
        grid.DataBind()
    End Sub

    Public Function IsYes(ByVal value As Object) As Boolean
        Dim text As String = Convert.ToString(value).Trim().ToUpper()
        Return text = "Y" OrElse text = "YES" OrElse text = "1" OrElse text = "TRUE" OrElse text = "A" OrElse text = "APPROVED" OrElse text = "R" OrElse text = "RECEIVED"
    End Function
End Module
