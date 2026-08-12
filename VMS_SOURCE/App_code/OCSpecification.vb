Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web
Imports VMS.DataAccess
Imports VMS.Web
Imports System.Data.SqlTypes
Public Class OCSpecification

    Public Function OC_SpecificationInsertUpdate(ByRef objOCSpecification As VMS.Web.OCSpecificationEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        'sqlConn checks the status of Sql connection whether in open or close state
        Try
            'sqlConn = DBFactory.GetHelper.OpenConnection()
            'sqlTrans = sqlConn.BeginTransaction()
            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.InputOutput
            sqlParams(0).Value = IIf(objOCSpecification.Auto_Id <> Integer.MinValue, objOCSpecification.Auto_Id, 0)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@vendor_code"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = objOCSpecification.Vendor_Code

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@product"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = objOCSpecification.Product_Type

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@product_code"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = objOCSpecification.Product_Code

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@Batch_NO"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = objOCSpecification.Batch_No

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@Batch_date"
            sqlParams(5).SqlDbType = SqlDbType.DateTime
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = objOCSpecification.Batch_Date

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@Created_user"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = IIf(objOCSpecification.createduser <> String.Empty, objOCSpecification.createduser, DBNull.Value)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Ocs_Specification_AddUpdate"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            numRowsAffected = Convert.ToInt32(sqlParams(0).Value)

        Catch ex As Exception
            Throw ex
        End Try
        Return numRowsAffected
    End Function
    Public Function OC_SpecificationDelete(ByVal Auto_Id As Integer, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        'sqlConn checks the status of Sql connection whether in open or close state
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(Auto_Id <> Integer.MinValue, Auto_Id, 0)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "DeleteOcSpecification"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
        Return numRowsAffected
    End Function
    Public Function OC_SpecificationDtls(ByRef ocSpecification_dtl As OCSPrmPrdEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Id"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = ocSpecification_dtl.Auto_Id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@specifications"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = ocSpecification_dtl.Paramss

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@specification_value"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = ocSpecification_dtl.ResultType

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@created_user"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = ocSpecification_dtl.CreatedUser

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Ocs_SpecificationDtls_AddUpdate"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try
        Return numRowsAffected
    End Function
    Public Function OCSpecificationListData(ByVal Vender As String, ByVal FromDate As SqlDateTime, ByVal ToDate As SqlDateTime, ByVal Product As String) As DataSet
        'Public Function OCSpecificationListData() As DataSet
        Dim OcSpecificationDs As New DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Vender"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Vender <> String.Empty, Vender, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@FromDate"
        sqlParams(1).SqlDbType = SqlDbType.DateTime
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = FromDate

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@ToDate"
        sqlParams(2).SqlDbType = SqlDbType.DateTime
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = ToDate

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@Product"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(Product <> String.Empty, Product, DBNull.Value)

        OcSpecificationDs = DBFactory.GetHelper().ExecuteDataSet("GetOCSpecificationList", Data.CommandType.StoredProcedure, sqlParams)

        Return OcSpecificationDs
    End Function
    'Public Function OCSpecificationReport(ByVal Vender As String, ByVal FromDate As SqlDateTime, ByVal ToDate As SqlDateTime, ByVal Product As String) As DataSet
    'Public Function OCSpecificationListData() As DataSet
    Public Function OCSpecificationReport(ByVal Vender As String, ByVal FromDate As String, ByVal ToDate As String, ByVal Product As String) As DataSet
        Dim OcSpecificationDs As New DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Vender"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Vender

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Product"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(Product <> String.Empty, Product, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@FromDate"
        'sqlParams(2).SqlDbType = SqlDbType.DateTime
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(FromDate <> String.Empty, FromDate, DBNull.Value)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@ToDate"
        'sqlParams(3).SqlDbType = SqlDbType.DateTime
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(ToDate <> String.Empty, ToDate, DBNull.Value)

        OcSpecificationDs = DBFactory.GetHelper().ExecuteDataSet("[dbo].[GetOCSpecificationReport]", Data.CommandType.StoredProcedure, sqlParams)
        Return OcSpecificationDs
    End Function
    Public Function ConfirmSpecification(ByVal Entity As OCSpecificationEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As New Integer
        Try
            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@hdrid"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Entity.Auto_Id

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@created_user"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Entity.confirmed_by

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Confirm_Specification]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected
    End Function
    Public Function Edit_OCSpecificationData(ByVal ID As Integer) As DataSet
        'Public Function OCSpecificationListData() As DataSet
        Dim OcSpecificationDs As New DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@ocs_id"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = ID
        OcSpecificationDs = DBFactory.GetHelper().ExecuteDataSet("FetchOcsSpecificationDetails", Data.CommandType.StoredProcedure, sqlParams)
        Return OcSpecificationDs
    End Function
    Public Function GetOCSReport(ByVal hdrID As Integer) As DataSet
        'Public Function OCSpecificationListData() As DataSet
        Dim OcSpecificationDs As New DataSet
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@hdrID"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = hdrID

            OcSpecificationDs = DBFactory.GetHelper().ExecuteDataSet("Get_OCS_Product_Report", Data.CommandType.StoredProcedure, sqlParams)
        Catch ex As Exception

        End Try
        Return OcSpecificationDs
    End Function
    Public Function GetProdDetails(ByVal UserId As String) As DataSet
        Dim OcSpecificationDs As New DataSet
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@UserId"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = UserId

            OcSpecificationDs = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Product_Details]", Data.CommandType.StoredProcedure, sqlParams)
        Catch ex As Exception

        End Try
        Return OcSpecificationDs
    End Function
    Public Function OCSpecificationReportDownload(ByVal Product As String) As DataSet
        Dim OcSpecificationDs As New DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Product"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Product <> String.Empty, Product, DBNull.Value)

        OcSpecificationDs = DBFactory.GetHelper().ExecuteDataSet("[dbo].[GetOCSpecificationReport_Download]", Data.CommandType.StoredProcedure, sqlParams)
        Return OcSpecificationDs
    End Function

    Public Function OC_SpecificationExcelAdd(ByVal ID As Integer, dt_sheet As DataTable, ByVal Created_user As String, sqlConn As SqlConnection, sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        'sqlConn checks the status of Sql connection whether in open or close state
        Try
            'sqlConn = DBFactory.GetHelper.OpenConnection()
            'sqlTrans = sqlConn.BeginTransaction()
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Id"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.InputOutput
            sqlParams(0).Value = IIf(ID <> Integer.MinValue, ID, 0)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@tbl"
            sqlParams(1).SqlDbType = SqlDbType.Structured
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = dt_sheet

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@Created_user"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = IIf(Created_user <> String.Empty, Created_user, DBNull.Value)

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[Ocs_Specification_ExcelAdd]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
            numRowsAffected = Convert.ToInt32(sqlParams(0).Value)

        Catch ex As Exception
            Throw ex
        End Try
        Return numRowsAffected
    End Function

    Public Function QCspecification_validation(ByVal ExcelDt As DataTable, ByVal created_user As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@qcspecificationData"
        sqlParams(0).SqlDbType = SqlDbType.Structured
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = ExcelDt

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@created_user"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = created_user
        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[upload_QCspecification_validation]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
End Class
