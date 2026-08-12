Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient
Namespace VMS.Web
    Public Class TablesExport


        Function GetTableNames() As DataSet

            Dim METargetGetSet As System.Data.DataSet

            METargetGetSet = DBFactory.GetHelper().ExecuteDataSet(Constant.StoreProcedures.GetAllTableNames, Data.CommandType.StoredProcedure)

            Return METargetGetSet

        End Function

        Function GetColumnNames(ByVal TableName As String) As DataSet

            Dim METargetGetSet As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@TableName"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = TableName

            METargetGetSet = DBFactory.GetHelper().ExecuteDataSet(Constant.StoreProcedures.GetAllColumnNames, Data.CommandType.StoredProcedure, sqlParams)

            Return METargetGetSet

        End Function

        Function ExportExcel(ByVal Query As String) As DataSet

            Dim METargetGetSet As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@QUERY"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Query

            'sqlParams(1) = New SqlParameter()
            'sqlParams(1).ParameterName = "@TABLENAME"
            'sqlParams(1).DbType = DbType.String
            'sqlParams(1).Direction = Data.ParameterDirection.Input
            'sqlParams(1).Value = TableName

            METargetGetSet = DBFactory.GetHelper().ExecuteDataSet(Constant.StoreProcedures.ExportData_Excel, Data.CommandType.StoredProcedure, sqlParams)

            Return METargetGetSet

        End Function

#Region "Insert Details into Excel_Parameter_MSTR Table"
        Function InsertExportExcelDetails(ByRef ExcelData As VMS.Web.ExportExcelEntity) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(11) As SqlParameter
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@Ex_Table_Name"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = ExcelData.ExTableName

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@Ex_Selected_Field_Name"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = ExcelData.ExSelectedFieldName

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@Ex_Heading"
                sqlParams(2).DbType = DbType.String
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = ExcelData.ExHeading

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@Ex_FieldOrder"
                sqlParams(3).DbType = DbType.Int32
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = ExcelData.ExFieldOrder

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@Ex_Report_Name"
                sqlParams(4).DbType = DbType.String
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = ExcelData.ExReportName

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@Ex_Total_YN"
                sqlParams(5).DbType = DbType.String
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = ExcelData.ExTotalYN

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@Ex_DataType"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = ExcelData.ExDataType

                sqlParams(7) = New SqlParameter()
                sqlParams(7).ParameterName = "@Ex_OrderBy"
                sqlParams(7).DbType = DbType.String
                sqlParams(7).Direction = Data.ParameterDirection.Input
                sqlParams(7).Value = ExcelData.ExOrderBy

                sqlParams(8) = New SqlParameter()
                sqlParams(8).ParameterName = "@Ex_GeneratedQuery"
                sqlParams(8).DbType = DbType.String
                sqlParams(8).Direction = Data.ParameterDirection.Input
                sqlParams(8).Value = ExcelData.ExGeneratedQuery

                sqlParams(9) = New SqlParameter()
                sqlParams(9).ParameterName = "@tx_field_1"
                sqlParams(9).DbType = DbType.Int32
                sqlParams(9).Direction = Data.ParameterDirection.Input
                If (ExcelData.txfield1 = Int32.MinValue) Then
                    sqlParams(9).Value = DBNull.Value
                Else
                    sqlParams(9).Value = ExcelData.txfield1
                End If

                sqlParams(10) = New SqlParameter()
                sqlParams(10).ParameterName = "@tx_field_2"
                sqlParams(10).DbType = DbType.String
                sqlParams(10).Direction = Data.ParameterDirection.Input
                If (ExcelData.txfield2 = String.Empty) Then
                    sqlParams(10).Value = DBNull.Value
                Else
                    sqlParams(10).Value = ExcelData.txfield2
                End If


                sqlParams(11) = New SqlParameter()
                sqlParams(11).ParameterName = "@CreatedUser"
                sqlParams(11).DbType = DbType.String
                sqlParams(11).Direction = Data.ParameterDirection.Input
                sqlParams(11).Value = ExcelData.createduser

                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = Constant.StoreProcedures.Excel_Parameter_MSTR_Insert

                sqlCmd.Parameters.AddRange(sqlParams)
                numRowsAffected = sqlCmd.ExecuteNonQuery()

                'SqlTrans is set to commit to save the transaction
                sqlTrans.Commit()

            Catch ex As Exception
                If Not (sqlTrans Is Nothing) Then
                    'SqlTrans is set to Rollback to go back to the beginning of the transaction
                    sqlTrans.Rollback()
                End If
                Throw ex
            Finally
                If Not (sqlConn Is Nothing) Then
                    'sqlConn is set to close state after completing the transaction
                    sqlConn.Close()
                End If
            End Try

            Return numRowsAffected

        End Function
#End Region


        Function GetReportNames(ByVal TableName As String) As DataSet

            Dim METargetGetSet As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Ex_Table_Name"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = TableName

            METargetGetSet = DBFactory.GetHelper().ExecuteDataSet(Constant.StoreProcedures.Excel_Parameter_ReportName_Get, Data.CommandType.StoredProcedure, sqlParams)

            Return METargetGetSet

        End Function

        Function GetReportItemforUpdate(ByVal ReportName As String) As DataSet

            Dim METargetGetSet As System.Data.DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Ex_Report_Name"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = ReportName

            METargetGetSet = DBFactory.GetHelper().ExecuteDataSet(Constant.StoreProcedures.Excel_Parameter_ReportUpdate_GetItem, Data.CommandType.StoredProcedure, sqlParams)

            Return METargetGetSet

        End Function

        Function GetReportItemforGrid(ByVal ReportName As String, ByVal selFieldName As String) As DataSet

            Dim METargetGetSet As System.Data.DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Ex_Report_Name"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = ReportName

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Ex_Selected_Field_Name"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = selFieldName

            METargetGetSet = DBFactory.GetHelper().ExecuteDataSet(Constant.StoreProcedures.Excel_Parameter_ReportItem, Data.CommandType.StoredProcedure, sqlParams)

            Return METargetGetSet

        End Function

#Region "Delete Details from Excel_Parameter_MSTR Table"
        Function DeleteExportExcelDetails(ByRef ExcelData As VMS.Web.ExportExcelEntity) As Integer
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing
            Dim numRowsAffected As Integer
            Dim sqlParams(1) As SqlParameter
            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@Ex_Table_Name"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = ExcelData.ExTableName

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@Ex_Report_Name"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = ExcelData.ExReportName

                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = Constant.StoreProcedures.Excel_Parameter_MSTR_Delete

                sqlCmd.Parameters.AddRange(sqlParams)
                numRowsAffected = sqlCmd.ExecuteNonQuery()

                'SqlTrans is set to commit to save the transaction
                sqlTrans.Commit()

            Catch ex As Exception
                If Not (sqlTrans Is Nothing) Then
                    'SqlTrans is set to Rollback to go back to the beginning of the transaction
                    sqlTrans.Rollback()
                End If
                Throw ex
            Finally
                If Not (sqlConn Is Nothing) Then
                    'sqlConn is set to close state after completing the transaction
                    sqlConn.Close()
                End If
            End Try

            Return numRowsAffected

        End Function
#End Region

    End Class
End Namespace
