Imports Microsoft.VisualBasic
Imports System.Data.SqlTypes
Namespace VMS.Web
    Public Class UserProfileEntity
        Private usp_company As String
        Private usp_user_id As String
        Private usp_first_name As String
        Private usp_last_name As String
        Private usp_initials As String
        Private usp_group_code As String
        Private usp_pswd As String
        Private usp_old_pswd1 As String
        Private usp_old_pswd2 As String
        Private usp_old_pswd3 As String
        Private usp_desig As String
        Private usp_branch As String
        Private usp_dept As String
        Private usp_mailid As String
        Private usp_office_no As String
        Private usp_extension As String
        Private usp_mobile As String
        Private usp_home_no As String
        Private usp_home_add As String
        Private usp_dob As SqlDateTime
        Private usp_emp_type As String
        Private usp_doj As SqlDateTime
        Private usp_exp_yrs As Integer
        Private usp_exp_months As Integer
        Private usp_last_accessed_date As SqlDateTime
        Private usp_Region As String
        Private usp_reporting_manager As String
        Private usp_no_times_used As Integer
        Private created_user As String
        Private created_date As Date
        Private modified_user As String
        Private modified_date As Date
        Private deleted_user As String
        Private deleted_date As Date
        Private active As String
        Private usp_exit_date As SqlDateTime
        Private usp_reason As String
        Private usp_blood_group As String
        Private usp_seniority As Integer
        Private usp_incentive_yn As String
        Private usp_reporting_usergroup As String
        Private usp_total_lead_alloted As Integer
        Private usp_total_false_reported As Integer
        Private usp_total_pending_false As Integer
        Public Sub New()
            usp_company = String.Empty
            usp_user_id = String.Empty
            usp_first_name = String.Empty
            usp_last_name = String.Empty
            usp_initials = String.Empty
            usp_group_code = String.Empty
            usp_pswd = String.Empty
            usp_old_pswd1 = String.Empty
            usp_old_pswd2 = String.Empty
            usp_old_pswd3 = String.Empty
            usp_desig = String.Empty
            usp_branch = String.Empty
            usp_dept = String.Empty
            usp_mailid = String.Empty
            usp_office_no = String.Empty
            usp_extension = String.Empty
            usp_mobile = String.Empty
            usp_home_no = String.Empty
            usp_home_add = String.Empty
            usp_dob = SqlDateTime.Null
            usp_emp_type = String.Empty
            usp_doj = SqlDateTime.Null
            usp_exp_yrs = Integer.MinValue
            usp_exp_months = Integer.MinValue
            usp_last_accessed_date = SqlDateTime.Null
            usp_Region = String.Empty
            usp_reporting_manager = String.Empty
            usp_no_times_used = Integer.MinValue
            created_user = String.Empty
            created_date = Date.MinValue
            modified_user = String.Empty
            modified_date = Date.MinValue
            deleted_user = String.Empty
            deleted_date = Date.MinValue
            active = String.Empty
            usp_exit_date = SqlDateTime.Null
            usp_reason = String.Empty
            usp_blood_group = String.Empty
            usp_seniority = Integer.MinValue
            usp_incentive_yn = String.Empty
            usp_reporting_usergroup = String.Empty
            usp_total_lead_alloted = Integer.MinValue
            usp_total_false_reported = Integer.MinValue
            usp_total_pending_false = Integer.MinValue
        End Sub
        Public Property uspcompany() As String
            Get
                Return usp_company
            End Get
            Set(ByVal value As String)
                usp_company = value
            End Set
        End Property
        Public Property uspuserid() As String
            Get
                Return usp_user_id
            End Get
            Set(ByVal value As String)
                usp_user_id = value
            End Set
        End Property
        Public Property uspfirstname() As String
            Get
                Return usp_first_name
            End Get
            Set(ByVal value As String)
                usp_first_name = value
            End Set
        End Property
        Public Property usplastname() As String
            Get
                Return usp_last_name
            End Get
            Set(ByVal value As String)
                usp_last_name = value
            End Set
        End Property
        Public Property uspinitials() As String
            Get
                Return usp_initials
            End Get
            Set(ByVal value As String)
                usp_initials = value
            End Set
        End Property
        Public Property uspgroupcode() As String
            Get
                Return usp_group_code
            End Get
            Set(ByVal value As String)
                usp_group_code = value
            End Set
        End Property
        Public Property usppswd() As String
            Get
                Return usp_pswd
            End Get
            Set(ByVal value As String)
                usp_pswd = value
            End Set
        End Property
        Public Property uspoldpswd1() As String
            Get
                Return usp_old_pswd1
            End Get
            Set(ByVal value As String)
                usp_old_pswd1 = value
            End Set
        End Property
        Public Property uspoldpswd2() As String
            Get
                Return usp_old_pswd2
            End Get
            Set(ByVal value As String)
                usp_old_pswd2 = value
            End Set
        End Property
        Public Property uspoldpswd3() As String
            Get
                Return usp_old_pswd3
            End Get
            Set(ByVal value As String)
                usp_old_pswd3 = value
            End Set
        End Property
        Public Property uspdesig() As String
            Get
                Return usp_desig
            End Get
            Set(ByVal value As String)
                usp_desig = value
            End Set
        End Property
        Public Property uspbranch() As String
            Get
                Return usp_branch
            End Get
            Set(ByVal value As String)
                usp_branch = value
            End Set
        End Property
        Public Property uspdept() As String
            Get
                Return usp_dept
            End Get
            Set(ByVal value As String)
                usp_dept = value
            End Set
        End Property
        Public Property uspmailid() As String
            Get
                Return usp_mailid
            End Get
            Set(ByVal value As String)
                usp_mailid = value
            End Set
        End Property
        Public Property uspofficeno() As String
            Get
                Return usp_office_no
            End Get
            Set(ByVal value As String)
                usp_office_no = value
            End Set
        End Property
        Public Property uspextension() As String
            Get
                Return usp_extension
            End Get
            Set(ByVal value As String)
                usp_extension = value
            End Set
        End Property
        Public Property uspmobile() As String
            Get
                Return usp_mobile
            End Get
            Set(ByVal value As String)
                usp_mobile = value
            End Set
        End Property
        Public Property usphomeno() As String
            Get
                Return usp_home_no
            End Get
            Set(ByVal value As String)
                usp_home_no = value
            End Set
        End Property
        Public Property usphomeadd() As String
            Get
                Return usp_home_add
            End Get
            Set(ByVal value As String)
                usp_home_add = value
            End Set
        End Property
        Public Property uspdob() As SqlDateTime
            Get
                Return usp_dob
            End Get
            Set(ByVal value As SqlDateTime)
                usp_dob = value
            End Set
        End Property
        Public Property uspemptype() As String
            Get
                Return usp_emp_type
            End Get
            Set(ByVal value As String)
                usp_emp_type = value
            End Set
        End Property
        Public Property uspdoj() As SqlDateTime
            Get
                Return usp_doj
            End Get
            Set(ByVal value As SqlDateTime)
                usp_doj = value
            End Set
        End Property
        Public Property uspexpyrs() As Integer
            Get
                Return usp_exp_yrs
            End Get
            Set(ByVal value As Integer)
                usp_exp_yrs = value
            End Set
        End Property
        Public Property uspexpmonths() As Integer
            Get
                Return usp_exp_months
            End Get
            Set(ByVal value As Integer)
                usp_exp_months = value
            End Set
        End Property
        Public Property usplastaccesseddate() As SqlDateTime
            Get
                Return usp_last_accessed_date
            End Get
            Set(ByVal value As SqlDateTime)
                usp_last_accessed_date = value
            End Set
        End Property
        Public Property uspRegion() As String
            Get
                Return usp_Region
            End Get
            Set(ByVal value As String)
                usp_Region = value
            End Set
        End Property
        Public Property uspreportingmanager() As String
            Get
                Return usp_reporting_manager
            End Get
            Set(ByVal value As String)
                usp_reporting_manager = value
            End Set
        End Property
        Public Property uspnotimesused() As Integer
            Get
                Return usp_no_times_used
            End Get
            Set(ByVal value As Integer)
                usp_no_times_used = value
            End Set
        End Property
        Public Property createduser() As String
            Get
                Return created_user
            End Get
            Set(ByVal value As String)
                created_user = value
            End Set
        End Property
        Public Property modifieduser() As String
            Get
                Return modified_user
            End Get
            Set(ByVal value As String)
                modified_user = value
            End Set
        End Property
        Public Property deleteduser() As String
            Get
                Return deleted_user
            End Get
            Set(ByVal value As String)
                deleted_user = value
            End Set
        End Property
        Public Property createddate() As Date
            Get
                Return created_date
            End Get
            Set(ByVal value As Date)
                created_date = value
            End Set
        End Property
        Public Property modifieddate() As Date
            Get
                Return modified_date
            End Get
            Set(ByVal value As Date)
                modified_date = value
            End Set
        End Property
        Public Property activestatus() As String
            Get
                Return active
            End Get
            Set(ByVal value As String)
                active = value
            End Set
        End Property
        Public Property uspexitdate() As SqlDateTime
            Get
                Return usp_exit_date
            End Get
            Set(ByVal value As SqlDateTime)
                usp_exit_date = value
            End Set
        End Property
        Public Property uspreason() As String
            Get
                Return usp_reason
            End Get
            Set(ByVal value As String)
                usp_reason = value
            End Set
        End Property
        Public Property uspseniority() As Integer
            Get
                Return usp_seniority
            End Get
            Set(ByVal value As Integer)
                usp_seniority = value
            End Set
        End Property
        Public Property uspincentiveyn() As String
            Get
                Return usp_incentive_yn
            End Get
            Set(ByVal value As String)
                usp_incentive_yn = value
            End Set
        End Property
        Public Property uspreportingusergroup() As String
            Get
                Return usp_reporting_usergroup
            End Get
            Set(ByVal value As String)
                usp_reporting_usergroup = value
            End Set
        End Property
        Public Property usptotalleadalloted() As Integer
            Get
                Return usp_total_lead_alloted
            End Get
            Set(ByVal value As Integer)
                usp_total_lead_alloted = value
            End Set
        End Property
        Public Property usptotalfalsereported() As Integer
            Get
                Return usp_total_false_reported
            End Get
            Set(ByVal value As Integer)
                usp_total_false_reported = value
            End Set
        End Property
        Public Property usptotalpendingfalse() As Integer
            Get
                Return usp_total_pending_false
            End Get
            Set(ByVal value As Integer)
                usp_total_pending_false = value
            End Set
        End Property
        Public Property uspbloodgroup() As String
            Get
                Return usp_blood_group
            End Get
            Set(ByVal value As String)
                usp_blood_group = value
            End Set
        End Property

    End Class

End Namespace