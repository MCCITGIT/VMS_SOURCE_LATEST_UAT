
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class TokenRequisitionSummaryClass
    Public Function GetTokenRequisitionSummaryData(ByVal userGroup As String, ByVal userId As String) As DataSet
        Dim DS As New DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@user_group"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userGroup


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@user_id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId
        DS = DBFactory.GetHelper().ExecuteDataSet("[value_token_requisition_summary]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
End Class


