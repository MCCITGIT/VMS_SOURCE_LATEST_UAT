'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/FormMenu.vb
'Created Date	: 16-August-2007
'Created By	    : Arun
'Version	    : R02.00.00
'Description	: Code behind file for FormMenu Class

'Modified By       Modified On       Version         Reason

'*************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes
Public Class FormMenu

#Region "Get FormMenu List"

    Function GetFormMenuList(ByVal Company As String) As DataSet

        Dim FormMenuDetails As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company



        FormMenuDetails = DBFactory.GetHelper().ExecuteDataSet("Form_Menu_List", Data.CommandType.StoredProcedure, sqlParams)

        Return FormMenuDetails
    End Function
#End Region

#Region "Get UserGroup"

    Function GetFormMenu(ByVal Company As String, ByVal Desc As String, ByVal Code As String) As DataSet

        Dim UserGroupDetails As System.Data.DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@desc"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Desc

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@code"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Code



        UserGroupDetails = DBFactory.GetHelper().ExecuteDataSet("Get_Form_Menu", Data.CommandType.StoredProcedure, sqlParams)

        Return UserGroupDetails
    End Function
#End Region

#Region "Get FormType from form_mstr Table"

    Function GetFormType(ByVal Company As String) As DataSet

        Dim FrmMstr As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company


        FrmMstr = DBFactory.GetHelper().ExecuteDataSet("Form_Type_Get", Data.CommandType.StoredProcedure, sqlParams)

        Return FrmMstr

    End Function

#End Region

#Region "Insert FormMenu"
    Function InsertFrmMnu(ByVal company As String, ByVal Type As String, ByVal Name As String, ByVal Link As String, ByVal Seq As Integer, ByVal UserID As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(5) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Type

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@name"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Name

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@link"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Link

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@seq"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Seq

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@userid"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = UserID


            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Form_Menu_Insert"

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

#Region "Update FormMenu"
    Function UpdateFrmMnu(ByVal company As String, ByVal Type As String, ByVal Name As String, ByVal Link As String, ByVal Seq As Integer, ByVal UserID As String, ByVal HType As String, ByVal HName As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        'sqlTrans checks the type of operation to be performed for a particular Sql transaction
        Dim sqlTrans As SqlTransaction = Nothing
        Dim numRowsAffected As Integer
        Dim sqlParams(7) As SqlParameter
        Try
            sqlConn = DBFactory.GetHelper.OpenConnection()
            sqlTrans = sqlConn.BeginTransaction()

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@type"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Type

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@name"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Name

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@link"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = Link

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@seq"
            sqlParams(4).DbType = DbType.Int32
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Seq

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@userid"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = UserID

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@htype"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = HType

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@hname"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = HName



            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Form_Menu_Update"

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

#Region "Get FormTypeMenu List"

    Function GetFormTypeMenuList(ByVal Company As String, ByVal FormType As String) As DataSet

        Dim FormTypeMenuDetails As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@company"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Company

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@formtype"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = FormType



        FormTypeMenuDetails = DBFactory.GetHelper().ExecuteDataSet("Form_Type_Menu_List", Data.CommandType.StoredProcedure, sqlParams)

        Return FormTypeMenuDetails
    End Function
#End Region

End Class
