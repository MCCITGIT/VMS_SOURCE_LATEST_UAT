Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web
Imports VMS.DataAccess
Imports VMS.Web
Public Class OCSPrdPrmClass
    Public Shared Function DBNullValueorStringIfNotNull(ByVal value As String) As Object
        Dim o As Object

        If (value = String.Empty Or value Is Nothing) Then
            o = DBNull.Value
        Else
            o = value
        End If

        Return o
    End Function

    Public Shared Function DBNullValueIfZero(ByVal value As Integer) As Object
        Dim o As Object

        If (value = 0) Then
            o = DBNull.Value
        Else
            o = value
        End If

        Return o
    End Function
    Public Function GetPrdPrmDtls(ByVal Params As String) As DataSet
        Dim DS As System.Data.DataSet = New System.Data.DataSet()
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@params"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(Params.Equals(String.Empty), DBNull.Value, Params)

            DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_OCS_Prd_Prms]", System.Data.CommandType.StoredProcedure, sqlParams)
        Catch ex As Exception
            Throw ex
        End Try

        Return DS
    End Function
    Public Function InsertUpdatePrmPrd(ByVal Entity As OCSPrmPrdEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As New Integer
        Try
            Dim sqlParams(10) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@productcode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Entity.Product_Code

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@frequency"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Entity.PFrequency

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@created_user"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Entity.CreatedUser

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@active"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Entity.Status

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@params"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Entity.Paramss

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@opph_id"
            sqlParams(5).DbType = DbType.Int32
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Convert.ToInt32(Entity.PrmPrd_Id)

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@numeric"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = Entity.Numeric_YN

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@dropdown"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Entity.Dropdown_YN

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@dropdownparam"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = Entity.DropDown_Param

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@min_value"
            sqlParams(9).DbType = DbType.Decimal
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = IIf(Entity.Min_Value.Equals(String.Empty), DBNull.Value, Entity.Min_Value)


            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@max_value"
            sqlParams(10).DbType = DbType.Decimal
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = IIf(Entity.Max_Value.Equals(String.Empty), DBNull.Value, Entity.Max_Value)

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Insert_OCS_Prd_Prms]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected
    End Function
    Public Function GetProductCode(ByVal ProductType As String, ByVal UserId As String) As DataSet
        Dim Oc_SpecificationDs As New DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@ProductType"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(ProductType <> String.Empty, ProductType, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@UserId"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(UserId <> String.Empty, UserId, DBNull.Value)

        Oc_SpecificationDs = DBFactory.GetHelper().ExecuteDataSet("GetProductCodeByType", Data.CommandType.StoredProcedure, sqlParams)

        Return Oc_SpecificationDs
    End Function
    Public Function GetProductParameter(ByVal Product_Code As String, ByVal OCS_ID As String, ByVal Action As String) As DataSet
        Dim Oc_SpecificationDs As New DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Product_Code"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Product_Code <> String.Empty, Product_Code, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@Id"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(OCS_ID <> String.Empty, OCS_ID, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@Action"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(Action <> String.Empty, Action, DBNull.Value)

        Oc_SpecificationDs = DBFactory.GetHelper().ExecuteDataSet("GetProductParameterDetails", Data.CommandType.StoredProcedure, sqlParams)

        Return Oc_SpecificationDs
    End Function
End Class
