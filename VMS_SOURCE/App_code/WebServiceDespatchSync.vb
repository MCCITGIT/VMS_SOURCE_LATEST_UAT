Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.IO
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebServiceDespatchSync
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetDespatchDtl() As DataSet
        Dim ds As New DataSet
        ds = Nothing
        Dim obj As New DespatchSyncMstr
        ds = obj.GetDespatchDetails()
        Return ds
    End Function

    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Xml)> _
   Public Function GetDespatchDtl1() As DataSet
        Dim ds As New DataSet
        ds = Nothing
        Dim obj As New DespatchSyncMstr
        ds = obj.GetDespatchDetails()
        Return ds

        'Return New JavaScriptSerializer().Serialize(ds.Tables(0))

    End Function


    <WebMethod()> _
  Public Function UpdateDespatchSyncYN(ByVal Unit As String, ByVal Depot As String, ByVal ChallanNo As Integer, ByVal challan_fin_year As String, ByVal despd_srl As Int32) As Integer
        Dim RowsAffected As Integer = Nothing
        Dim obj As New DespatchSyncMstr
        RowsAffected += obj.UpdateDespatchSyncDetails(Unit, Depot, ChallanNo, challan_fin_year, despd_srl)
        Return RowsAffected

    End Function

End Class
