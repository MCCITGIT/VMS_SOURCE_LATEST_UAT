Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class DepotMailMstrClass
#Region "Get depot mail List"

    Function GetDEpotMailMstrList(ByVal Depot As String) As DataSet
        Dim ds As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depotcode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Depot <> String.Empty, Depot, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("GetDepot_Manager_MailList", Data.CommandType.StoredProcedure, sqlParams)

        Return ds
    End Function
#End Region
#Region "Get Depot List"

    Function GetDepotList(ByVal regn As String) As DataSet
        Dim ds As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@rgn"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(regn <> String.Empty, regn, DBNull.Value)

        ds = DBFactory.GetHelper().ExecuteDataSet("Get_DepotList", Data.CommandType.StoredProcedure, sqlParams)

        Return ds
    End Function
#End Region

#Region "Get Depot List"

    Function GetRegionList() As DataSet
        Dim ds As System.Data.DataSet
        ds = DBFactory.GetHelper().ExecuteDataSet("Get_RegionList", Data.CommandType.StoredProcedure)

        Return ds
    End Function
#End Region

#Region "DEPOT MAIL INSERT"
    Function Insert_DepotMail(ByVal region As String, ByVal depot As String, ByVal email As String, ByVal userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@region"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(region <> String.Empty, region, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = depot

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@email"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = email

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@userid"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = userid

        Dim sqlCmd As New SqlCommand()
        sqlCmd.Connection = sqlConn
        sqlCmd.Transaction = sqlTrans
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "Insert_DepotEmailMstr"

        sqlCmd.Parameters.AddRange(sqlParams)
        numRowsAffected = sqlCmd.ExecuteNonQuery()
        Return numRowsAffected

    End Function
#End Region
End Class
