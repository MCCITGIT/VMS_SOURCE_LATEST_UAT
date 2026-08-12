'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/Doc_Upload_App.vb
'Created Date	: 30-December-2011
'Created By	    : Debayan Biswas
'Version	    : R01.00.00
'Description	: Code behind file for Doc_Upload_App Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports VMS.DataAccess
Imports System.Data
Imports System.Data.SqlClient

Namespace VMS.Web
    Public Class Doc_Upload_App

        Public Function GetDepot(ByVal Region As String, ByVal Active As String) As DataSet
            Dim DepotDS As New DataSet
            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Region"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = IIf(Region <> String.Empty, Region, DBNull.Value)

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Active

            DepotDS = DBFactory.GetHelper().ExecuteDataSet("[Estimation_Data_Get_Depot]", Data.CommandType.StoredProcedure, sqlParams)
            Return DepotDS
        End Function

        Public Function GridListGetDetails(ByVal User As String, ByVal LoginUser As String, ByVal Active As String) As DataSet
            Dim GridDS As New DataSet
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@User"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = User

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@LogInUser"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = LoginUser

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@Active"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Active

            GridDS = DBFactory.GetHelper().ExecuteDataSet("[Doc_Upload_Get_Details]", Data.CommandType.StoredProcedure, sqlParams)
            Return GridDS
        End Function

        Public Function GetFinYear(ByVal Active As String) As DataSet
            Dim FinYearDS As New DataSet
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Active"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Active

            FinYearDS = DBFactory.GetHelper().ExecuteDataSet("[Doc_Upload_Get_FinYear]", Data.CommandType.StoredProcedure, sqlParams)
            Return FinYearDS
        End Function

        Public Function GetRowCount() As Integer
            Dim noRowsAffected As New DataSet

            noRowsAffected = DBFactory.GetHelper().ExecuteDataSet("[Doc_Upload_Get_RowCount]", Data.CommandType.StoredProcedure)
            Dim RowCount As Integer
            RowCount = noRowsAffected.Tables(0).Rows(0)("rowno")
            Return RowCount
        End Function

        Public Function GetFromDepot(ByVal Depot As String, ByVal Active As String) As DataSet
            Dim FromDepotDS As New DataSet
            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Depot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Active

            FromDepotDS = DBFactory.GetHelper().ExecuteDataSet("[Doc_Upload_Get_FromDepot]", Data.CommandType.StoredProcedure, sqlParams)
            Return FromDepotDS
        End Function

        Public Function GetEditModeDetails(ByVal GenId As String, ByVal Active As String) As DataSet
            Dim EditModeDS As New DataSet
            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@GenId"
            sqlParams(0).DbType = DbType.Int32
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = GenId

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@Active"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Active

            EditModeDS = DBFactory.GetHelper().ExecuteDataSet("[Doc_Upload_Get_UpdateMode_Details]", Data.CommandType.StoredProcedure, sqlParams)
            Return EditModeDS
        End Function

        Public Function InsertDocument(ByVal DocEntity As DocUpload_Entity, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
            Dim NumsRowAffected As New Integer

            Try
                Dim sqlparams(10) As SqlParameter

                sqlparams(0) = New SqlParameter
                sqlparams(0).ParameterName = "@sdocs_from_depot"
                sqlparams(0).DbType = DbType.String
                sqlparams(0).Direction = ParameterDirection.Input
                sqlparams(0).Value = DocEntity.DocsFromDepot

                sqlparams(1) = New SqlParameter
                sqlparams(1).ParameterName = "@sdocs_to_depot"
                sqlparams(1).DbType = DbType.String
                sqlparams(1).Direction = ParameterDirection.Input
                sqlparams(1).Value = DocEntity.DocsToDepot

                sqlparams(2) = New SqlParameter
                sqlparams(2).ParameterName = "@sdocs_doc_catg"
                sqlparams(2).DbType = DbType.String
                sqlparams(2).Direction = ParameterDirection.Input
                sqlparams(2).Value = DocEntity.DocsDocCatg

                sqlparams(3) = New SqlParameter
                sqlparams(3).ParameterName = "@sdocs_doc_title"
                sqlparams(3).DbType = DbType.String
                sqlparams(3).Direction = ParameterDirection.Input
                sqlparams(3).Value = DocEntity.DocsDocTitle

                sqlparams(4) = New SqlParameter
                sqlparams(4).ParameterName = "@sdocs_doc_no"
                sqlparams(4).DbType = DbType.String
                sqlparams(4).Direction = ParameterDirection.Input
                sqlparams(4).Value = DocEntity.DocsDocNo


                sqlparams(5) = New SqlParameter
                sqlparams(5).ParameterName = "@sdocs_doc_date"
                sqlparams(5).DbType = DbType.DateTime
                sqlparams(5).Direction = ParameterDirection.Input
                sqlparams(5).Value = DocEntity.DocsDocDate

                sqlparams(6) = New SqlParameter
                sqlparams(6).ParameterName = "@sdocs_remarks"
                sqlparams(6).DbType = DbType.String
                sqlparams(6).Direction = ParameterDirection.Input
                sqlparams(6).Value = IIf(DocEntity.DocsRemarks <> String.Empty, DocEntity.DocsRemarks, DBNull.Value)

                sqlparams(7) = New SqlParameter
                sqlparams(7).ParameterName = "@sdocs_file_name"
                sqlparams(7).DbType = DbType.String
                sqlparams(7).Direction = ParameterDirection.Input
                sqlparams(7).Value = DocEntity.DocsFileName

                sqlparams(8) = New SqlParameter
                sqlparams(8).ParameterName = "@created_user"
                sqlparams(8).DbType = DbType.String
                sqlparams(8).Direction = ParameterDirection.Input
                sqlparams(8).Value = DocEntity.CreatedUser

                sqlparams(9) = New SqlParameter
                sqlparams(9).ParameterName = "@sdocs_fin_year"
                sqlparams(9).DbType = DbType.String
                sqlparams(9).Direction = ParameterDirection.Input
                sqlparams(9).Value = DocEntity.DocsFinYear

                sqlparams(10) = New SqlParameter
                sqlparams(10).ParameterName = "@active"
                sqlparams(10).DbType = DbType.String
                sqlparams(10).Direction = ParameterDirection.Input
                sqlparams(10).Value = DocEntity.DocActive

                Dim sqlcmd As New SqlCommand
                sqlcmd.CommandText = "[Doc_Upload_Insert]"
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.Connection = sqlconn
                sqlcmd.Transaction = sqltrans
                sqlcmd.Parameters.AddRange(sqlparams)
                NumsRowAffected = sqlcmd.ExecuteNonQuery
                Return NumsRowAffected
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function UpdateDocument(ByVal DocEntity As DocUpload_Entity, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
            Dim NumsRowAffected As New Integer

            Try
                Dim sqlparams(11) As SqlParameter

                sqlparams(0) = New SqlParameter
                sqlparams(0).ParameterName = "@sdocs_from_depot"
                sqlparams(0).DbType = DbType.String
                sqlparams(0).Direction = ParameterDirection.Input
                sqlparams(0).Value = DocEntity.DocsFromDepot

                sqlparams(1) = New SqlParameter
                sqlparams(1).ParameterName = "@sdocs_to_depot"
                sqlparams(1).DbType = DbType.String
                sqlparams(1).Direction = ParameterDirection.Input
                sqlparams(1).Value = DocEntity.DocsToDepot

                sqlparams(2) = New SqlParameter
                sqlparams(2).ParameterName = "@sdocs_doc_catg"
                sqlparams(2).DbType = DbType.String
                sqlparams(2).Direction = ParameterDirection.Input
                sqlparams(2).Value = DocEntity.DocsDocCatg

                sqlparams(3) = New SqlParameter
                sqlparams(3).ParameterName = "@sdocs_doc_title"
                sqlparams(3).DbType = DbType.String
                sqlparams(3).Direction = ParameterDirection.Input
                sqlparams(3).Value = DocEntity.DocsDocTitle

                sqlparams(4) = New SqlParameter
                sqlparams(4).ParameterName = "@sdocs_doc_no"
                sqlparams(4).DbType = DbType.String
                sqlparams(4).Direction = ParameterDirection.Input
                sqlparams(4).Value = DocEntity.DocsDocNo


                sqlparams(5) = New SqlParameter
                sqlparams(5).ParameterName = "@sdocs_doc_date"
                sqlparams(5).DbType = DbType.DateTime
                sqlparams(5).Direction = ParameterDirection.Input
                sqlparams(5).Value = DocEntity.DocsDocDate

                sqlparams(6) = New SqlParameter
                sqlparams(6).ParameterName = "@sdocs_remarks"
                sqlparams(6).DbType = DbType.String
                sqlparams(6).Direction = ParameterDirection.Input
                sqlparams(6).Value = IIf(DocEntity.DocsRemarks <> String.Empty, DocEntity.DocsRemarks, DBNull.Value)

                sqlparams(7) = New SqlParameter
                sqlparams(7).ParameterName = "@sdocs_file_name"
                sqlparams(7).DbType = DbType.String
                sqlparams(7).Direction = ParameterDirection.Input
                sqlparams(7).Value = IIf(DocEntity.DocsFileName <> String.Empty, DocEntity.DocsFileName, DBNull.Value)

                sqlparams(8) = New SqlParameter
                sqlparams(8).ParameterName = "@modified_user"
                sqlparams(8).DbType = DbType.String
                sqlparams(8).Direction = ParameterDirection.Input
                sqlparams(8).Value = DocEntity.ModifiedUser

                sqlparams(9) = New SqlParameter
                sqlparams(9).ParameterName = "@sdocs_fin_year"
                sqlparams(9).DbType = DbType.String
                sqlparams(9).Direction = ParameterDirection.Input
                sqlparams(9).Value = DocEntity.DocsFinYear

                sqlparams(10) = New SqlParameter
                sqlparams(10).ParameterName = "@active"
                sqlparams(10).DbType = DbType.String
                sqlparams(10).Direction = ParameterDirection.Input
                sqlparams(10).Value = DocEntity.DocActive

                sqlparams(11) = New SqlParameter
                sqlparams(11).ParameterName = "@sdocs_gen_id"
                sqlparams(11).DbType = DbType.Int32
                sqlparams(11).Direction = ParameterDirection.Input
                sqlparams(11).Value = DocEntity.DocsGenId

                Dim sqlcmd As New SqlCommand
                sqlcmd.CommandText = "[Doc_Upload_Update]"
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.Connection = sqlconn
                sqlcmd.Transaction = sqltrans
                sqlcmd.Parameters.AddRange(sqlparams)
                NumsRowAffected = sqlcmd.ExecuteNonQuery
                Return NumsRowAffected
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function DeleteDocument(ByVal GenId As String, ByVal DeletedUser As String, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
            Dim numsrowaffected As Integer
            Try
                Dim sqlParams(1) As SqlParameter

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@sdocs_gen_id"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = GenId

                sqlParams(1) = New SqlParameter
                sqlParams(1).ParameterName = "@deleted_user"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = ParameterDirection.Input
                sqlParams(1).Value = DeletedUser

                Dim sqlcmd As New SqlCommand
                sqlcmd.CommandText = "[Doc_Upload_Delete]"
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.Connection = sqlconn
                sqlcmd.Transaction = sqltrans
                sqlcmd.Parameters.AddRange(sqlParams)
                numsrowaffected = sqlcmd.ExecuteNonQuery
                Return numsrowaffected
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace

