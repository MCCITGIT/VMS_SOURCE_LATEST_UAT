'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/Common.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.00.00
'Description	: Code behind file for Project Common Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Namespace VMS.Web
    Public Class Common
        Dim n, n1, n2, number, ctr, paises, digits As Decimal
        Dim rupees As Decimal
        Dim tRupees As String

#Region "Get City Details from Lov_Details Table"

        Function GetLovDetails(ByVal Company As String, ByVal LovType As String, ByVal LovStatus As String) As DataSet

            Dim LovDetails As System.Data.DataSet

            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company


            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Lov_Type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = LovType

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@Lov_Status"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = LovStatus

            LovDetails = DBFactory.GetHelper().ExecuteDataSet("Lov_Details_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return LovDetails

        End Function

#End Region

#Region "Get Lov_Type Lov_Details Table"

        Function GetLovTypeDetails(ByVal Company As String) As DataSet

            Dim LovDetails As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company


            LovDetails = DBFactory.GetHelper().ExecuteDataSet("Lov_Type_Details_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return LovDetails

        End Function

#End Region

#Region "Get Details from Lov_Details Table"

        Function GetLovNewsDetails(ByVal Company As String, ByVal LovType As String, ByVal Status As String, ByVal LovStatus As String) As DataSet

            Dim LovDetails As System.Data.DataSet

            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Lov_Type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = LovType

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@lov_field1_value"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Status

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@Lov_Status"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = LovStatus

            LovDetails = DBFactory.GetHelper().ExecuteDataSet("Lov_News_Details_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return LovDetails

        End Function

#End Region

#Region "Get depot details of region"
        Public Function Getdepotname(ByVal depotregn As String) As DataSet

            Dim PrjectList As DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Depotregn"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = depotregn

            'sqlParams(1) = New SqlParameter()
            'sqlParams(1).ParameterName = "@scheme_code"
            'sqlParams(1).DbType = DbType.String
            'sqlParams(1).Direction = Data.ParameterDirection.Input
            'sqlParams(1).Value = schemecode


            PrjectList = DBFactory.GetHelper().ExecuteDataSet("DepotName_get", Data.CommandType.StoredProcedure, sqlParams)

            Return PrjectList

        End Function

        Public Function Getdepotname_Vr1(ByVal depotregn As String) As DataSet

            Dim PrjectList As DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Depotregn"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = depotregn

            'sqlParams(1) = New SqlParameter()
            'sqlParams(1).ParameterName = "@scheme_code"
            'sqlParams(1).DbType = DbType.String
            'sqlParams(1).Direction = Data.ParameterDirection.Input
            'sqlParams(1).Value = schemecode


            PrjectList = DBFactory.GetHelper().ExecuteDataSet("DepotName_get_Vr1", Data.CommandType.StoredProcedure, sqlParams)

            Return PrjectList

        End Function
#End Region

#Region "Get depot details of region"
        Public Function GetdepotList(ByVal depotregn As String) As DataSet

            Dim PrjectList As DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Depotregn"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(depotregn <> String.Empty, depotregn, DBNull.Value)

            'sqlParams(1) = New SqlParameter()
            'sqlParams(1).ParameterName = "@scheme_code"
            'sqlParams(1).DbType = DbType.String
            'sqlParams(1).Direction = Data.ParameterDirection.Input
            'sqlParams(1).Value = schemecode


            PrjectList = DBFactory.GetHelper().ExecuteDataSet("DepotList_get", Data.CommandType.StoredProcedure, sqlParams)

            Return PrjectList

        End Function
#End Region

#Region "Get region"

        Function Getregion_break(ByVal Company As String, ByVal Itemcode As String) As DataSet

            Dim LovDetails As System.Data.DataSet

            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Itemcode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@Active"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Constant.Common.ActiveStatus

            LovDetails = DBFactory.GetHelper().ExecuteDataSet("Get_depot_region", Data.CommandType.StoredProcedure, sqlParams)
            Return LovDetails

        End Function

#End Region

#Region "Get Fin Year from fin_year Table"

        Function GetFinYrDetails(ByVal Company As String, ByVal active As String) As DataSet

            Dim FinYrDetails As System.Data.DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company


            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = active

            FinYrDetails = DBFactory.GetHelper().ExecuteDataSet("FinYr_Details_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return FinYrDetails

        End Function

        'Modified-by MUKESH BHAGAT on 02-09-2026 : single shared binder for Process Year
        'dropdowns. Years come from dbo.fin_year (SP [FinYr_Details_Get]) so a new process
        'year is enabled by one master-data insert instead of editing screens. Values are
        'the plain year (fin_year), matching how despatch screens store/filter it.
        'Defensive fallback: if the master returns nothing the list is generated up to the
        'current year, so a screen can never come up with an empty year list.
        Public Sub BindProcessYearDropdown(ByVal ddl As System.Web.UI.WebControls.DropDownList, ByVal Company As String, ByVal active As String)
            ddl.Items.Clear()

            Dim ds As DataSet = Nothing
            Try
                ds = GetFinYrDetails(Company, active)
            Catch
                ds = Nothing
            End Try

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0) IsNot Nothing Then
                For Each yearRow As DataRow In ds.Tables(0).Rows
                    ddl.Items.Add(Convert.ToString(yearRow("fin_year")))
                Next
            End If

            If ddl.Items.Count = 0 Then
                For y As Integer = 2010 To DateTime.Now.Year
                    ddl.Items.Insert(0, y.ToString())   ' newest first, like the SP's ordering
                Next
            End If
        End Sub

#End Region

#Region "Serial No details get from company, finyear, doctype, branch"

        Public Function GetSrlNoBranchWiseGet(ByVal Company As String, ByVal FinYear As String, ByVal DocType As String, ByVal Branch As String, ByVal active As String) As DataSet

            Dim GetSrlNoBranchWiseSet As DataSet

            Dim sqlParams(4) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@FinYear"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = FinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@DocType"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = DocType

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@Branch"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Branch

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@Active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = active

            GetSrlNoBranchWiseSet = DBFactory.GetHelper().ExecuteDataSet("SrlNoValue_BranchWise_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return GetSrlNoBranchWiseSet

        End Function

#End Region

#Region "Get  Depot Details  From Depot Master"
        Function GetDepotDetails(ByVal status As String) As DataSet
            Dim DepotDetails As DataSet
            Dim depotCode As String = String.Empty
            Dim sqlparams(1) As SqlParameter
            sqlparams(0) = New SqlParameter()
            sqlparams(0).ParameterName = "@active"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = Data.ParameterDirection.Input
            sqlparams(0).Value = status

            sqlparams(1) = New SqlParameter()
            sqlparams(1).ParameterName = "@depotcode"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = Data.ParameterDirection.Input
            sqlparams(1).Value = IIf(depotCode <> String.Empty, depotCode, DBNull.Value)

            DepotDetails = DBFactory.GetHelper().ExecuteDataSet("DepotDetais_Get", Data.CommandType.StoredProcedure, sqlparams)
            Return DepotDetails
        End Function
#End Region

#Region "Get  Depot Details  From Depot Master for depot advice"
        Function GetDepotDetails(ByVal depotCode As String, ByVal status As String) As DataSet
            Dim DepotDetails As DataSet
            Dim sqlparams(1) As SqlParameter
            sqlparams(0) = New SqlParameter()
            sqlparams(0).ParameterName = "@active"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = Data.ParameterDirection.Input
            sqlparams(0).Value = status

            sqlparams(1) = New SqlParameter()
            sqlparams(1).ParameterName = "@depotcode"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = Data.ParameterDirection.Input
            sqlparams(1).Value = IIf(depotCode <> String.Empty, depotCode, DBNull.Value)

            DepotDetails = DBFactory.GetHelper().ExecuteDataSet("DepotDetais_Get", Data.CommandType.StoredProcedure, sqlparams)
            Return DepotDetails
        End Function
#End Region

#Region "depotDetails get"
        Public Function getDepotRegndetails(ByVal Depot As String) As DataSet
            Dim GetDepotDetailsset As DataSet
            Dim sqlParams(1) As SqlParameter
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depotcode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Depot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Constant.Common.ActiveStatus

            GetDepotDetailsset = DBFactory.GetHelper().ExecuteDataSet("DepotDetais_Get", Data.CommandType.StoredProcedure, sqlParams)
            Return GetDepotDetailsset
        End Function
#End Region

#Region "User Id for specific user group"
        Function GetUserId(ByVal Company As String, ByVal UserGroup As String, ByVal Status As String) As DataSet

            Dim UserIdSet As System.Data.DataSet

            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usergroup"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserGroup

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@status"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Status

            UserIdSet = DBFactory.GetHelper().ExecuteDataSet("Get_User_ID", Data.CommandType.StoredProcedure, sqlParams)

            Return UserIdSet
        End Function
#End Region

#Region "Get Fin Year from fin_year Table"

        Function GetFinYr(ByVal DateVal As DateTime) As DataSet

            Dim FinYrDetails As System.Data.DataSet

            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Constant.Common.Company


            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Constant.Common.ActiveStatus

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@dateval"
            sqlParams(2).DbType = DbType.Date
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = DateVal

            FinYrDetails = DBFactory.GetHelper().ExecuteDataSet("FinYr_Value_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return FinYrDetails

        End Function

#End Region

#Region "Get values for a particluar Standard Parameter."

        Function GetStandardParameterValues(ByVal param_name As String) As DataSet

            Dim dsStandardParameter As DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@param_name"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = param_name

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Constant.Common.ActiveStatus

            dsStandardParameter = DBFactory.GetHelper().ExecuteDataSet("Get_StandardParameter_Values", Data.CommandType.StoredProcedure, sqlParams)

            Return dsStandardParameter

        End Function

#End Region

#Region "Get indent count based on indent status."

        Function GetUnapprovedDespatchCount(ByVal unit As String, ByVal fin_year As String, ByVal fin_month As String) As DataSet

            Dim ds As System.Data.DataSet

            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = unit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = fin_year

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = fin_month

            ds = DBFactory.GetHelper().ExecuteDataSet("Home_getUnaprvedDsptchChlanCnt", Data.CommandType.StoredProcedure, sqlParams)

            Return ds

        End Function

#End Region

#Region "Count new documents count."

        Function GetNewDocUploadedCount(ByVal depot As String) As DataSet

            Dim ds As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = depot

            ds = DBFactory.GetHelper().ExecuteDataSet("Home_getNewDocCnt", Data.CommandType.StoredProcedure, sqlParams)

            Return ds

        End Function

#End Region


        Public Function GetUserApplicableDepotList(ByVal userId As String, ByVal user_group As String) As DataSet

            Dim ds As DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@user_id"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = userId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@user_group"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = user_group

            ds = DBFactory.GetHelper().ExecuteDataSet("[Common_getUserApplicableDepot]", Data.CommandType.StoredProcedure, sqlParams)

            Return ds

        End Function



        Function GetLovDetails(ByVal LovType As String, ByVal LovStatus As String) As DataSet

            Dim LovDetails As System.Data.DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@lov_type"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = LovType

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@lov_status"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = LovStatus

            LovDetails = DBFactory.GetHelper().ExecuteDataSet("[TOKEN_GENERATION_BERGER_DB].[dbo].[Lov_Details_Get]", Data.CommandType.StoredProcedure, sqlParams)

            Return LovDetails

        End Function

        Public Shared Function ConvertToDataTable(Of T)(ByVal items As List(Of T)) As DataTable
            Dim dataTable As DataTable = New DataTable(GetType(T).Name)
            Dim Props As PropertyInfo() = GetType(T).GetProperties(BindingFlags.[Public] Or BindingFlags.Instance)

            For Each prop As PropertyInfo In Props
                dataTable.Columns.Add(prop.Name)
            Next

            For Each item As T In items
                Dim values = New Object(Props.Length - 1) {}

                For i As Integer = 0 To Props.Length - 1
                    values(i) = Props(i).GetValue(item, Nothing)
                Next

                dataTable.Rows.Add(values)
            Next

            Return dataTable
        End Function

        Public Shared Function ConvertToDataTable_old(Of T)(ByVal list As IList(Of T)) As DataTable
            Dim table As New DataTable()
            Dim fields() As FieldInfo = GetType(T).GetFields()
            For Each field As FieldInfo In fields
                table.Columns.Add(field.Name, field.FieldType)
            Next
            For Each item As T In list
                Dim row As DataRow = table.NewRow()
                For Each field As FieldInfo In fields
                    row(field.Name) = field.GetValue(item)
                Next
                table.Rows.Add(row)
            Next
            Return table
        End Function
    End Class

End Namespace
