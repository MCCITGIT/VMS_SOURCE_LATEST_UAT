Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports VMS.DataAccess

Public Class SKUWiseMRPClasss
    Public Function GetSkuWiseMRPList(ByVal userGroup As String, ByVal userId As String) As DataSet
        Dim DS As New DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@userGroup"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = userGroup


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@userId"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = userId
        DS = DBFactory.GetHelper().ExecuteDataSet("[SKU_Wise_MRP_List_Get]", Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function
End Class
