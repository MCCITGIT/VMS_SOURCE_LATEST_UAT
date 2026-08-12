Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess
Namespace VMS.Web
    Public Class UserProfile
        Public Function UserInsert(ByRef User As VMS.Web.UserProfileEntity) As Integer
            Dim numRowsAffected As Integer
            'sqlConn checks the status of Sql connection whether in open or close state
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing

            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                Dim sqlParams(35) As SqlParameter

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@usp_company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = User.uspcompany

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@usp_user_id"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = User.uspuserid

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@usp_first_name"
                sqlParams(2).DbType = DbType.String
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = User.uspfirstname

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@usp_last_name"
                sqlParams(3).DbType = DbType.String
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = IIf(User.usplastname <> String.Empty, User.usplastname, DBNull.Value)

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@usp_initials"
                sqlParams(4).DbType = DbType.String
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = IIf(User.uspinitials <> String.Empty, User.uspinitials, DBNull.Value)

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@usp_group_code"
                sqlParams(5).DbType = DbType.String
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = User.uspgroupcode

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@usp_pswd"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = IIf(User.usppswd <> String.Empty, User.usppswd, DBNull.Value)

                sqlParams(7) = New SqlParameter()
                sqlParams(7).ParameterName = "@usp_desig"
                sqlParams(7).DbType = DbType.String
                sqlParams(7).Direction = Data.ParameterDirection.Input
                sqlParams(7).Value = IIf(User.uspdesig <> String.Empty, User.uspdesig, DBNull.Value)

                sqlParams(8) = New SqlParameter()
                sqlParams(8).ParameterName = "@usp_branch"
                sqlParams(8).DbType = DbType.String
                sqlParams(8).Direction = Data.ParameterDirection.Input
                sqlParams(8).Value = IIf(User.uspbranch <> String.Empty, User.uspbranch, DBNull.Value)

                sqlParams(9) = New SqlParameter()
                sqlParams(9).ParameterName = "@usp_dept"
                sqlParams(9).DbType = DbType.String
                sqlParams(9).Direction = Data.ParameterDirection.Input
                sqlParams(9).Value = IIf(User.uspdept <> String.Empty, User.uspdept, DBNull.Value)

                sqlParams(10) = New SqlParameter()
                sqlParams(10).ParameterName = "@usp_mailid"
                sqlParams(10).DbType = DbType.String
                sqlParams(10).Direction = Data.ParameterDirection.Input
                sqlParams(10).Value = IIf(User.uspmailid <> String.Empty, User.uspmailid, DBNull.Value)

                sqlParams(11) = New SqlParameter()
                sqlParams(11).ParameterName = "@usp_office_no"
                sqlParams(11).DbType = DbType.String
                sqlParams(11).Direction = Data.ParameterDirection.Input
                sqlParams(11).Value = IIf(User.uspofficeno <> String.Empty, User.uspofficeno, DBNull.Value)

                sqlParams(12) = New SqlParameter()
                sqlParams(12).ParameterName = "@usp_extension"
                sqlParams(12).DbType = DbType.String
                sqlParams(12).Direction = Data.ParameterDirection.Input
                sqlParams(12).Value = IIf(User.uspextension <> String.Empty, User.uspextension, DBNull.Value)

                sqlParams(13) = New SqlParameter()
                sqlParams(13).ParameterName = "@usp_home_no"
                sqlParams(13).DbType = DbType.String
                sqlParams(13).Direction = Data.ParameterDirection.Input
                sqlParams(13).Value = IIf(User.usphomeno <> String.Empty, User.usphomeno, DBNull.Value)

                sqlParams(14) = New SqlParameter()
                sqlParams(14).ParameterName = "@usp_home_add"
                sqlParams(14).DbType = DbType.String
                sqlParams(14).Direction = Data.ParameterDirection.Input
                sqlParams(14).Value = IIf(User.usphomeadd <> String.Empty, User.usphomeadd, DBNull.Value)

                sqlParams(15) = New SqlParameter()
                sqlParams(15).ParameterName = "@usp_dob"
                sqlParams(15).DbType = DbType.Date
                sqlParams(15).Direction = Data.ParameterDirection.Input
                'sqlParams(15).Value = IIf(User.uspdob <> Date.MinValue, User.uspdob, DBNull.Value)
                sqlParams(15).Value = User.uspdob

                sqlParams(16) = New SqlParameter()
                sqlParams(16).ParameterName = "@usp_emp_type"
                sqlParams(16).DbType = DbType.String
                sqlParams(16).Direction = Data.ParameterDirection.Input
                sqlParams(16).Value = User.uspemptype

                sqlParams(17) = New SqlParameter()
                sqlParams(17).ParameterName = "@usp_doj"
                sqlParams(17).DbType = DbType.Date
                sqlParams(17).Direction = Data.ParameterDirection.Input
                'sqlParams(17).Value = IIf(User.uspdoj <> Date.MinValue, User.uspdoj, DBNull.Value)
                sqlParams(17).Value = User.uspdoj

                sqlParams(18) = New SqlParameter()
                sqlParams(18).ParameterName = "@usp_exit_date"
                sqlParams(18).DbType = DbType.Date
                sqlParams(18).Direction = Data.ParameterDirection.Input
                'sqlParams(18).Value = IIf(User.uspexitdate <> Date.MinValue, User.uspexitdate, DBNull.Value)
                sqlParams(18).Value = User.uspexitdate

                sqlParams(19) = New SqlParameter()
                sqlParams(19).ParameterName = "@usp_reason"
                sqlParams(19).DbType = DbType.String
                sqlParams(19).Direction = Data.ParameterDirection.Input
                sqlParams(19).Value = IIf(User.uspreason <> String.Empty, User.uspreason, DBNull.Value)

                sqlParams(20) = New SqlParameter()
                sqlParams(20).ParameterName = "@usp_blood_group"
                sqlParams(20).DbType = DbType.String
                sqlParams(20).Direction = Data.ParameterDirection.Input
                sqlParams(20).Value = IIf(User.uspbloodgroup <> String.Empty, User.uspbloodgroup, DBNull.Value)

                sqlParams(21) = New SqlParameter()
                sqlParams(21).ParameterName = "@usp_exp_yrs"
                sqlParams(21).DbType = DbType.Int32
                sqlParams(21).Direction = Data.ParameterDirection.Input
                sqlParams(21).Value = IIf(User.uspexpyrs <> Integer.MinValue, User.uspexpyrs, DBNull.Value)

                sqlParams(22) = New SqlParameter()
                sqlParams(22).ParameterName = "@usp_exp_months"
                sqlParams(22).DbType = DbType.Int32
                sqlParams(22).Direction = Data.ParameterDirection.Input
                sqlParams(22).Value = IIf(User.uspexpmonths <> Integer.MinValue, User.uspexpmonths, DBNull.Value)

                sqlParams(23) = New SqlParameter()
                sqlParams(23).ParameterName = "@usp_last_accessed_date"
                sqlParams(23).DbType = DbType.Date
                sqlParams(23).Direction = Data.ParameterDirection.Input
                'sqlParams(23).Value = IIf(User.usplastaccesseddate <> Date.MinValue, User.usplastaccesseddate, DBNull.Value)
                sqlParams(23).Value = User.usplastaccesseddate

                sqlParams(24) = New SqlParameter()
                sqlParams(24).ParameterName = "@usp_region"
                sqlParams(24).DbType = DbType.String
                sqlParams(24).Direction = Data.ParameterDirection.Input
                sqlParams(24).Value = IIf(User.uspRegion <> String.Empty, User.uspRegion, DBNull.Value)

                sqlParams(25) = New SqlParameter()
                sqlParams(25).ParameterName = "@usp_seniority"
                sqlParams(25).DbType = DbType.Int32
                sqlParams(25).Direction = Data.ParameterDirection.Input
                sqlParams(25).Value = IIf(User.uspseniority <> Integer.MinValue, User.uspseniority, DBNull.Value)

                sqlParams(26) = New SqlParameter()
                sqlParams(26).ParameterName = "@usp_incentive_yn"
                sqlParams(26).DbType = DbType.String
                sqlParams(26).Direction = Data.ParameterDirection.Input
                sqlParams(26).Value = IIf(User.uspincentiveyn <> String.Empty, User.uspincentiveyn, DBNull.Value)

                sqlParams(27) = New SqlParameter()
                sqlParams(27).ParameterName = "@usp_reporting_usergroup"
                sqlParams(27).DbType = DbType.String
                sqlParams(27).Direction = Data.ParameterDirection.Input
                sqlParams(27).Value = User.uspreportingusergroup

                sqlParams(28) = New SqlParameter()
                sqlParams(28).ParameterName = "@usp_reporting_manager"
                sqlParams(28).DbType = DbType.String
                sqlParams(28).Direction = Data.ParameterDirection.Input
                sqlParams(28).Value = IIf(User.uspreportingmanager <> String.Empty, User.uspreportingmanager, DBNull.Value)

                sqlParams(29) = New SqlParameter()
                sqlParams(29).ParameterName = "@usp_no_times_used"
                sqlParams(29).DbType = DbType.Int32
                sqlParams(29).Direction = Data.ParameterDirection.Input
                sqlParams(29).Value = IIf(User.uspnotimesused <> Integer.MinValue, User.uspnotimesused, DBNull.Value)

                sqlParams(30) = New SqlParameter()
                sqlParams(30).ParameterName = "@usp_total_lead_alloted"
                sqlParams(30).DbType = DbType.Int32
                sqlParams(30).Direction = Data.ParameterDirection.Input
                sqlParams(30).Value = IIf(User.usptotalleadalloted <> Integer.MinValue, User.usptotalleadalloted, DBNull.Value)

                sqlParams(31) = New SqlParameter()
                sqlParams(31).ParameterName = "@usp_total_false_reported"
                sqlParams(31).DbType = DbType.Int32
                sqlParams(31).Direction = Data.ParameterDirection.Input
                sqlParams(31).Value = IIf(User.usptotalfalsereported <> Integer.MinValue, User.usptotalfalsereported, DBNull.Value)

                sqlParams(32) = New SqlParameter()
                sqlParams(32).ParameterName = "@usp_total_pending_false"
                sqlParams(32).DbType = DbType.Int32
                sqlParams(32).Direction = Data.ParameterDirection.Input
                sqlParams(32).Value = IIf(User.usptotalpendingfalse <> Integer.MinValue, User.usptotalpendingfalse, DBNull.Value)

                sqlParams(33) = New SqlParameter()
                sqlParams(33).ParameterName = "@created_user"
                sqlParams(33).DbType = DbType.String
                sqlParams(33).Direction = Data.ParameterDirection.Input
                sqlParams(33).Value = IIf(User.createduser <> String.Empty, User.createduser, DBNull.Value)

                sqlParams(34) = New SqlParameter()
                sqlParams(34).ParameterName = "@active"
                sqlParams(34).DbType = DbType.String
                sqlParams(34).Direction = Data.ParameterDirection.Input
                sqlParams(34).Value = IIf(User.activestatus <> String.Empty, User.activestatus, DBNull.Value)

                sqlParams(35) = New SqlParameter()
                sqlParams(35).ParameterName = "@usp_mobile"
                sqlParams(35).DbType = DbType.String
                sqlParams(35).Direction = Data.ParameterDirection.Input
                sqlParams(35).Value = IIf(User.uspmobile <> String.Empty, User.uspmobile, DBNull.Value)

                'sqlCmd is the object instance of the SqlCommand 
                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "user_profile_Insert"
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
        Public Function UserUpdate(ByRef User As VMS.Web.UserProfileEntity) As Integer
            Dim numRowsAffected As Integer

            'sqlConn checks the status of Sql connection whether in open or close state
            Dim sqlConn As SqlConnection = Nothing
            'sqlTrans checks the type of operation to be performed for a particular Sql transaction
            Dim sqlTrans As SqlTransaction = Nothing

            Try
                sqlConn = DBFactory.GetHelper.OpenConnection()
                sqlTrans = sqlConn.BeginTransaction()
                Dim sqlParams(38) As SqlParameter

                sqlParams(0) = New SqlParameter()
                sqlParams(0).ParameterName = "@usp_company"
                sqlParams(0).DbType = DbType.String
                sqlParams(0).Direction = Data.ParameterDirection.Input
                sqlParams(0).Value = User.uspcompany

                sqlParams(1) = New SqlParameter()
                sqlParams(1).ParameterName = "@usp_user_id"
                sqlParams(1).DbType = DbType.String
                sqlParams(1).Direction = Data.ParameterDirection.Input
                sqlParams(1).Value = User.uspuserid

                sqlParams(2) = New SqlParameter()
                sqlParams(2).ParameterName = "@usp_first_name"
                sqlParams(2).DbType = DbType.String
                sqlParams(2).Direction = Data.ParameterDirection.Input
                sqlParams(2).Value = User.uspfirstname

                sqlParams(3) = New SqlParameter()
                sqlParams(3).ParameterName = "@usp_last_name"
                sqlParams(3).DbType = DbType.String
                sqlParams(3).Direction = Data.ParameterDirection.Input
                sqlParams(3).Value = IIf(User.usplastname <> String.Empty, User.usplastname, DBNull.Value)

                sqlParams(4) = New SqlParameter()
                sqlParams(4).ParameterName = "@usp_initials"
                sqlParams(4).DbType = DbType.String
                sqlParams(4).Direction = Data.ParameterDirection.Input
                sqlParams(4).Value = IIf(User.uspinitials <> String.Empty, User.uspinitials, DBNull.Value)

                sqlParams(5) = New SqlParameter()
                sqlParams(5).ParameterName = "@usp_group_code"
                sqlParams(5).DbType = DbType.String
                sqlParams(5).Direction = Data.ParameterDirection.Input
                sqlParams(5).Value = User.uspgroupcode

                sqlParams(6) = New SqlParameter()
                sqlParams(6).ParameterName = "@usp_pswd"
                sqlParams(6).DbType = DbType.String
                sqlParams(6).Direction = Data.ParameterDirection.Input
                sqlParams(6).Value = IIf(User.usppswd <> String.Empty, User.usppswd, DBNull.Value)

                sqlParams(7) = New SqlParameter()
                sqlParams(7).ParameterName = "@usp_desig"
                sqlParams(7).DbType = DbType.String
                sqlParams(7).Direction = Data.ParameterDirection.Input
                sqlParams(7).Value = IIf(User.uspdesig <> String.Empty, User.uspdesig, DBNull.Value)

                sqlParams(8) = New SqlParameter()
                sqlParams(8).ParameterName = "@usp_branch"
                sqlParams(8).DbType = DbType.String
                sqlParams(8).Direction = Data.ParameterDirection.Input
                sqlParams(8).Value = IIf(User.uspbranch <> String.Empty, User.uspbranch, DBNull.Value)

                sqlParams(9) = New SqlParameter()
                sqlParams(9).ParameterName = "@usp_dept"
                sqlParams(9).DbType = DbType.String
                sqlParams(9).Direction = Data.ParameterDirection.Input
                sqlParams(9).Value = IIf(User.uspdept <> String.Empty, User.uspdept, DBNull.Value)

                sqlParams(10) = New SqlParameter()
                sqlParams(10).ParameterName = "@usp_mailid"
                sqlParams(10).DbType = DbType.String
                sqlParams(10).Direction = Data.ParameterDirection.Input
                sqlParams(10).Value = IIf(User.uspmailid <> String.Empty, User.uspmailid, DBNull.Value)

                sqlParams(11) = New SqlParameter()
                sqlParams(11).ParameterName = "@usp_office_no"
                sqlParams(11).DbType = DbType.String
                sqlParams(11).Direction = Data.ParameterDirection.Input
                sqlParams(11).Value = IIf(User.uspofficeno <> String.Empty, User.uspofficeno, DBNull.Value)

                sqlParams(12) = New SqlParameter()
                sqlParams(12).ParameterName = "@usp_extension"
                sqlParams(12).DbType = DbType.String
                sqlParams(12).Direction = Data.ParameterDirection.Input
                sqlParams(12).Value = IIf(User.uspextension <> String.Empty, User.uspextension, DBNull.Value)

                sqlParams(13) = New SqlParameter()
                sqlParams(13).ParameterName = "@usp_home_no"
                sqlParams(13).DbType = DbType.String
                sqlParams(13).Direction = Data.ParameterDirection.Input
                sqlParams(13).Value = IIf(User.usphomeno <> String.Empty, User.usphomeno, DBNull.Value)

                sqlParams(14) = New SqlParameter()
                sqlParams(14).ParameterName = "@usp_home_add"
                sqlParams(14).DbType = DbType.String
                sqlParams(14).Direction = Data.ParameterDirection.Input
                sqlParams(14).Value = IIf(User.usphomeadd <> String.Empty, User.usphomeadd, DBNull.Value)

                sqlParams(15) = New SqlParameter()
                sqlParams(15).ParameterName = "@usp_dob"
                sqlParams(15).DbType = DbType.Date
                sqlParams(15).Direction = Data.ParameterDirection.Input
                'sqlParams(15).Value = IIf(User.uspdob <> Date.MinValue, User.uspdob, DBNull.Value)
                sqlParams(15).Value = User.uspdob

                sqlParams(16) = New SqlParameter()
                sqlParams(16).ParameterName = "@usp_emp_type"
                sqlParams(16).DbType = DbType.String
                sqlParams(16).Direction = Data.ParameterDirection.Input
                sqlParams(16).Value = User.uspemptype

                sqlParams(17) = New SqlParameter()
                sqlParams(17).ParameterName = "@usp_doj"
                sqlParams(17).DbType = DbType.Date
                sqlParams(17).Direction = Data.ParameterDirection.Input
                'sqlParams(17).Value = IIf(User.uspdoj <> Date.MinValue, User.uspdoj, DBNull.Value)
                sqlParams(17).Value = User.uspdoj

                sqlParams(18) = New SqlParameter()
                sqlParams(18).ParameterName = "@usp_exit_date"
                sqlParams(18).DbType = DbType.Date
                sqlParams(18).Direction = Data.ParameterDirection.Input
                'sqlParams(18).Value = IIf(User.uspexitdate <> Date.MinValue, User.uspexitdate, DBNull.Value)
                sqlParams(18).Value = User.uspexitdate

                sqlParams(19) = New SqlParameter()
                sqlParams(19).ParameterName = "@usp_reason"
                sqlParams(19).DbType = DbType.String
                sqlParams(19).Direction = Data.ParameterDirection.Input
                sqlParams(19).Value = IIf(User.uspreason <> String.Empty, User.uspreason, DBNull.Value)

                sqlParams(20) = New SqlParameter()
                sqlParams(20).ParameterName = "@usp_blood_group"
                sqlParams(20).DbType = DbType.String
                sqlParams(20).Direction = Data.ParameterDirection.Input
                sqlParams(20).Value = IIf(User.uspbloodgroup <> String.Empty, User.uspbloodgroup, DBNull.Value)

                sqlParams(21) = New SqlParameter()
                sqlParams(21).ParameterName = "@usp_exp_yrs"
                sqlParams(21).DbType = DbType.Int32
                sqlParams(21).Direction = Data.ParameterDirection.Input
                sqlParams(21).Value = IIf(User.uspexpyrs <> Integer.MinValue, User.uspexpyrs, DBNull.Value)

                sqlParams(22) = New SqlParameter()
                sqlParams(22).ParameterName = "@usp_exp_months"
                sqlParams(22).DbType = DbType.Int32
                sqlParams(22).Direction = Data.ParameterDirection.Input
                sqlParams(22).Value = IIf(User.uspexpmonths <> Integer.MinValue, User.uspexpmonths, DBNull.Value)

                sqlParams(23) = New SqlParameter()
                sqlParams(23).ParameterName = "@usp_last_accessed_date"
                sqlParams(23).DbType = DbType.Date
                sqlParams(23).Direction = Data.ParameterDirection.Input
                'sqlParams(23).Value = IIf(User.usplastaccesseddate <> Date.MinValue, User.usplastaccesseddate, DBNull.Value)
                sqlParams(23).Value = User.usplastaccesseddate

                sqlParams(24) = New SqlParameter()
                sqlParams(24).ParameterName = "@usp_region"
                sqlParams(24).DbType = DbType.String
                sqlParams(24).Direction = Data.ParameterDirection.Input
                sqlParams(24).Value = IIf(User.uspRegion <> String.Empty, User.uspRegion, DBNull.Value)

                sqlParams(25) = New SqlParameter()
                sqlParams(25).ParameterName = "@usp_seniority"
                sqlParams(25).DbType = DbType.Int32
                sqlParams(25).Direction = Data.ParameterDirection.Input
                sqlParams(25).Value = IIf(User.uspseniority <> Integer.MinValue, User.uspseniority, DBNull.Value)

                sqlParams(26) = New SqlParameter()
                sqlParams(26).ParameterName = "@usp_incentive_yn"
                sqlParams(26).DbType = DbType.String
                sqlParams(26).Direction = Data.ParameterDirection.Input
                sqlParams(26).Value = IIf(User.uspincentiveyn <> String.Empty, User.uspincentiveyn, DBNull.Value)


                sqlParams(27) = New SqlParameter()
                sqlParams(27).ParameterName = "@usp_reporting_usergroup"
                sqlParams(27).DbType = DbType.String
                sqlParams(27).Direction = Data.ParameterDirection.Input
                sqlParams(27).Value = User.uspreportingusergroup

                sqlParams(28) = New SqlParameter()
                sqlParams(28).ParameterName = "@usp_reporting_manager"
                sqlParams(28).DbType = DbType.String
                sqlParams(28).Direction = Data.ParameterDirection.Input
                sqlParams(28).Value = IIf(User.uspreportingmanager <> String.Empty, User.uspreportingmanager, DBNull.Value)

                sqlParams(29) = New SqlParameter()
                sqlParams(29).ParameterName = "@usp_no_times_used"
                sqlParams(29).DbType = DbType.Int32
                sqlParams(29).Direction = Data.ParameterDirection.Input
                sqlParams(29).Value = IIf(User.uspnotimesused <> Integer.MinValue, User.uspnotimesused, DBNull.Value)

                sqlParams(30) = New SqlParameter()
                sqlParams(30).ParameterName = "@usp_total_lead_alloted"
                sqlParams(30).DbType = DbType.Int32
                sqlParams(30).Direction = Data.ParameterDirection.Input
                sqlParams(30).Value = IIf(User.usptotalleadalloted <> Integer.MinValue, User.usptotalleadalloted, DBNull.Value)

                sqlParams(31) = New SqlParameter()
                sqlParams(31).ParameterName = "@usp_total_false_reported"
                sqlParams(31).DbType = DbType.Int32
                sqlParams(31).Direction = Data.ParameterDirection.Input
                sqlParams(31).Value = IIf(User.usptotalfalsereported <> Integer.MinValue, User.usptotalfalsereported, DBNull.Value)

                sqlParams(32) = New SqlParameter()
                sqlParams(32).ParameterName = "@usp_total_pending_false"
                sqlParams(32).DbType = DbType.Int32
                sqlParams(32).Direction = Data.ParameterDirection.Input
                sqlParams(32).Value = IIf(User.usptotalpendingfalse <> Integer.MinValue, User.usptotalpendingfalse, DBNull.Value)

                sqlParams(33) = New SqlParameter()
                sqlParams(33).ParameterName = "@modified_user"
                sqlParams(33).DbType = DbType.String
                sqlParams(33).Direction = Data.ParameterDirection.Input
                sqlParams(33).Value = IIf(User.createduser <> String.Empty, User.createduser, DBNull.Value)

                sqlParams(34) = New SqlParameter()
                sqlParams(34).ParameterName = "@active"
                sqlParams(34).DbType = DbType.String
                sqlParams(34).Direction = Data.ParameterDirection.Input
                sqlParams(34).Value = IIf(User.activestatus <> String.Empty, User.activestatus, DBNull.Value)

                sqlParams(35) = New SqlParameter()
                sqlParams(35).ParameterName = "@usp_old_pswd1"
                sqlParams(35).DbType = DbType.String
                sqlParams(35).Direction = Data.ParameterDirection.Input
                sqlParams(35).Value = IIf(User.uspoldpswd1 <> String.Empty, User.uspoldpswd1, DBNull.Value)


                sqlParams(36) = New SqlParameter()
                sqlParams(36).ParameterName = "@usp_old_pswd2"
                sqlParams(36).DbType = DbType.String
                sqlParams(36).Direction = Data.ParameterDirection.Input
                sqlParams(36).Value = IIf(User.uspoldpswd2 <> String.Empty, User.uspoldpswd2, DBNull.Value)


                sqlParams(37) = New SqlParameter()
                sqlParams(37).ParameterName = "@usp_old_pswd3"
                sqlParams(37).DbType = DbType.String
                sqlParams(37).Direction = Data.ParameterDirection.Input
                sqlParams(37).Value = IIf(User.uspoldpswd3 <> String.Empty, User.uspoldpswd3, DBNull.Value)

                sqlParams(38) = New SqlParameter()
                sqlParams(38).ParameterName = "@usp_mobile"
                sqlParams(38).DbType = DbType.String
                sqlParams(38).Direction = Data.ParameterDirection.Input
                sqlParams(38).Value = IIf(User.uspmobile <> String.Empty, User.uspmobile, DBNull.Value)

                'sqlCmd is the object instance of the SqlCommand 
                Dim sqlCmd As New SqlCommand()
                sqlCmd.Connection = sqlConn
                sqlCmd.Transaction = sqlTrans
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "user_profile_Update"
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
        Public Function UserGroup_Get(ByVal Company As String) As DataSet
            Dim UserGroupSet As New DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            UserGroupSet = DBFactory.GetHelper().ExecuteDataSet("User_group_get", Data.CommandType.StoredProcedure, sqlParams)
            Return UserGroupSet
        End Function
        Public Function User_List_Get(ByVal Company As String, ByVal Branch As String, ByVal UserGroup As String, ByVal UserName As String, ByVal UserDept As String) As DataSet
            Dim UserListSet As New DataSet

            Dim sqlParams(4) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usp_branch"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Branch

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@usp_dept"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = UserDept

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@user_name"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = UserName

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@usp_group"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = UserGroup

            UserListSet = DBFactory.GetHelper().ExecuteDataSet("UserProfile_List", Data.CommandType.StoredProcedure, sqlParams)

            Return UserListSet
        End Function
        Public Function User_Edit_Get(ByVal Company As String, ByVal UserId As String) As DataSet

            Dim UserEditSet As New DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@usp_company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usp_user_id"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserId

            UserEditSet = DBFactory.GetHelper().ExecuteDataSet("User_Profile_Edit_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return UserEditSet
        End Function
        Public Function User_Profile(ByVal Company As String, ByVal Dept As String, ByVal UserId As String) As DataSet

            Dim UserSet As New DataSet
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@usp_company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = Company

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usp_dept"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Dept

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@usp_user_id"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = UserId

            UserSet = DBFactory.GetHelper().ExecuteDataSet("UserProfile", Data.CommandType.StoredProcedure, sqlParams)

            Return UserSet
        End Function


#Region "User ID Already exists"

        Public Function GetUserIdCheck(ByVal CompanyCode As String, ByVal UserId As String) As DataSet

            Dim Usrid As DataSet

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@usp_company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = CompanyCode

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@usp_user_id"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = UserId

            Usrid = DBFactory.GetHelper().ExecuteDataSet("UserID_AlreadyExists", Data.CommandType.StoredProcedure, sqlParams)

            Return Usrid

        End Function

#End Region

#Region "Get User IDs"

        Public Function GetUserIds(ByVal CompanyCode As String) As DataSet

            Dim Usrids As DataSet

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@Company"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = CompanyCode

            Usrids = DBFactory.GetHelper().ExecuteDataSet("User_Ids_Get", Data.CommandType.StoredProcedure, sqlParams)

            Return Usrids

        End Function

#End Region

#Region "User History Information Get"
        Function User_History_Get(ByVal Company As String, ByVal UserGroup As String, ByVal UserId As String, ByVal FromDate As String, ByVal ToDate As String, ByVal SearchFor As String) As DataSet
            Dim UserSet As New DataSet
            Dim sqlparams(5) As SqlParameter

            sqlparams(0) = New SqlParameter()
            sqlparams(0).ParameterName = "@usp_company"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = Data.ParameterDirection.Input
            sqlparams(0).Value = Company

            sqlparams(1) = New SqlParameter()
            sqlparams(1).ParameterName = "@usp_usergroup"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = Data.ParameterDirection.Input
            sqlparams(1).Value = IIf(UserGroup <> String.Empty, UserGroup, DBNull.Value)

            sqlparams(2) = New SqlParameter()
            sqlparams(2).ParameterName = "@usp_userid"
            sqlparams(2).DbType = DbType.String
            sqlparams(2).Direction = Data.ParameterDirection.Input
            sqlparams(2).Value = IIf(UserId <> String.Empty, UserId, DBNull.Value)

            sqlparams(3) = New SqlParameter()
            sqlparams(3).ParameterName = "@usp_fromdate"
            sqlparams(3).DbType = DbType.String
            sqlparams(3).Direction = Data.ParameterDirection.Input
            sqlparams(3).Value = IIf(FromDate <> String.Empty, FromDate, DBNull.Value)

            sqlparams(4) = New SqlParameter()
            sqlparams(4).ParameterName = "@usp_todate"
            sqlparams(4).DbType = DbType.String
            sqlparams(4).Direction = Data.ParameterDirection.Input
            sqlparams(4).Value = IIf(ToDate <> String.Empty, ToDate, DBNull.Value)

            sqlparams(5) = New SqlParameter()
            sqlparams(5).ParameterName = "@usp_searchfor"
            sqlparams(5).DbType = DbType.String
            sqlparams(5).Direction = Data.ParameterDirection.Input
            sqlparams(5).Value = SearchFor

            UserSet = DBFactory.GetHelper().ExecuteDataSet("Get_User_History", Data.CommandType.StoredProcedure, sqlparams)

            Return UserSet
        End Function
#End Region

#Region "Get Depo Name"
        Public Function Get_Depo_Name(ByVal groupCode As String, ByVal company As String) As DataSet
            Dim UserSet As New DataSet
            Dim sqlparam(1) As SqlParameter

            sqlparam(0) = New SqlParameter
            sqlparam(0).ParameterName = "@group_code"
            sqlparam(0).DbType = DbType.String
            sqlparam(0).Direction = Data.ParameterDirection.Input
            sqlparam(0).Value = groupCode

            sqlparam(1) = New SqlParameter()
            sqlparam(1).ParameterName = "@Company"
            sqlparam(1).DbType = DbType.String
            sqlparam(1).Direction = Data.ParameterDirection.Input
            sqlparam(1).Value = company

            UserSet = DBFactory.GetHelper().ExecuteDataSet("Get_Depo_Name", CommandType.StoredProcedure, sqlparam)

            Return UserSet
        End Function
#End Region
#Region "Account No already exists"
        Public Function GetAccountNoCheck(ByVal AccountNo As String, ByVal DepotCode As String) As DataSet
            Dim AccountNoDS As DataSet

            Dim sqlparams(2) As SqlParameter

            sqlparams(0) = New SqlParameter
            sqlparams(0).ParameterName = "@usp_account_no"
            sqlparams(0).DbType = DbType.String
            sqlparams(0).Direction = Data.ParameterDirection.Input
            sqlparams(0).Value = AccountNo

            sqlparams(1) = New SqlParameter
            sqlparams(1).ParameterName = "@usp_depot_code"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = Data.ParameterDirection.Input
            sqlparams(1).Value = DepotCode

            sqlparams(2) = New SqlParameter
            sqlparams(2).ParameterName = "@usp_lov_company"
            sqlparams(2).DbType = DbType.String
            sqlparams(2).Direction = Data.ParameterDirection.Input
            sqlparams(2).Value = Constant.Common.Company

            AccountNoDS = DBFactory.GetHelper.ExecuteDataSet("get_AddBankMaster_AccountNoExists", Data.CommandType.StoredProcedure, sqlparams)

            Return AccountNoDS
        End Function
#End Region

    End Class
End Namespace