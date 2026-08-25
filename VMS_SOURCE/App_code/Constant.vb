'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/Constant.vb
'Created Date	: 16-August-2007
'Created By	    : Saravanan
'Version	    : R01.00.00
'Description	: Code behind file for Project Constant Class

'Modified By       Modified On       Version         Reason

'*************************************************************


Imports Microsoft.VisualBasic

Public Class Constant

    Public Class Common
        Public Const Company As String = "Berger"
        Public Const ChangePwd As String = "9999"
        Public Const LovCode_City As String = "CITY"
        Public Const LovCode_AREA As String = "AREA"
        Public Const LovCode_STATE As String = "STATE"
        Public Const LovCode_Country As String = "COUNTRY"
        Public Const Project_Type As String = "MKT_TYPE"
        Public Const Land_Category As String = "LAND_CATG"

        Public Const ActiveStatus As String = "Y"
        Public Const REGION_TYPE As String = "REGION_TYPE"
        Public Const InActiveStatus As String = "N"
        Public Const Active As String = "Active"
        Public Const InActive As String = "InActive"

        Public Const All As String = "All"
        Public Const Selec As String = "Select"
        Public Const Doc_Type As String = "DOC_TYPE"
        Public Const Yes As String = "Yes"
        Public Const No As String = "No"
        
        Public Const Null As String = "Null"
        Public Const UserGroupMarketing As String = "MARKETING"
        Public Const UserGroupMarketingManager As String = "MKT_MANAGER"

        Public Const Lov_Vend_Branch As String = "Branch"
        Public Const Lov_Department As String = "Dept"
        Public Const Lov_Employee_Type As String = "Emp_Type"
        Public Const Lov_Designation As String = "Emp_Desig"
        Public Const Lov_Team As String = "Team"
        Public Const Lov_Sep_Reason As String = "Sep_Reason"

        Public Const REQ_TYPE As String = "REQ_TYPE"
        Public Const REQ_STATUS As String = "REQ_STATUS"
        Public Const REQ_CONTACTBY As String = "REQ_CONTACTBY"

        Public Const Lov_Prod_Group As String = "PRD_GROUP"
        Public Const Lov_gift_Group As String = "GIFT_GROUP"
        Public Const Lov_Prod_Classification As String = "PRD_CLASSF"
        Public Const Lov_Prod_From As String = "PRD_FROM"
        Public Const Lov_UOM As String = "UOM"

        Public Const Token_Req_Status_New As String = "New"
        Public Const Token_Req_Status_In_Transit As String = "In-Transit"
        Public Const Token_Req_Status_Received As String = "Received"
        Public Const Token_Req_Status_Rejected As String = "Rejected"


        Public Const Lov_Title As String = "Lov_Title"
       
        Public Const ApproveStatus As String = "APPROVED"

        Public Const PdfFormat As String = "PdfFormat"
        Public Const ExcelFormat As String = "ExcelFormat"
        Public Const WordFormat As String = "WordFormat"

        Public Const FromDate As String = "01/01/2000"
        Public Const Todate As String = "31/12/2020"

        Public Const Modify As String = "Modify"
        Public Const ViewGetDepot As String = "ViewGetDepot"


        Public Const StandardParameter_ProcessYear As String = "process_year"
        Public Const StandardParameter_ProcessMonth As String = "process_month"

        Public Const InvalidBrowser As String = "Invalid Browser"


        Public Const SMTP_HOST As String = "103.253.125.55"
        Public Const SMTP_PORT As Integer = 25

        Public Const MAIL_NETWORK_CREDENTIAL_USERNAME As String = "mailservice@bergerapps.in"
        Public Const MAIL_NETWORK_CREDENTIAL_PASSWORD As String = "ram1653$"

        Public Const PasswordChangeDateDiff As Integer = 60
        Public Const VENDOR_REASON_TYPE As String = "VENDOR_REASON_TYPE"

        Public Const LRDoc = "LRDoc"
        Public Const InvoiceDoc = "InvoiceDoc"

        Public Const userDept = "SYS ADMIN"

    End Class

    Public Class SessionKeys

        'Added session to maintain the following values in session
        Public Const UserInfo As String = "UserInfo"

        Public Const User As String = "User"
        Public Const Company As String = "Company"
        Public Const UID As String = "UID"
        Public Const UFN As String = "UFN"
        Public Const ULN As String = "ULN"
        Public Const UEMAIL As String = "UEMAIL"
        Public Const DEPT As String = "DEPT"
        Public Const AjaxPrevLocation As String = "AjaxPrevLocation"
        Public Const ErrMessage As String = "Err: Contact Administrator"
       
        Public Const UserId As String = "UserId"


      
        Public Const Roles As String = "Roles"
       
        Public Const CurrentYear As String = "CurrentYear"

        Public Const finweekstartdate As String = "finweekstartdate"
        Public Const finweekenddate As String = "finweekenddate"
        Public Const UserIncharge As String = "UserIncharge"
       

        'Lead List Constants
       

        Public Const LovDetailsSearchInfo As String = "LovDetailsSearchInfo"
        Public Const FormMenuSearchInfo As String = "FormMenuSearchInfo"
        Public Const SerialControlSearchInfo As String = "SerialControlSearchInfo"


        Public Const UPListSearchInfo As String = "UPListSearchInfo"
        Public Const DepotName As String = "DepotName"

        'File view
        Public Const File_Type As String = "File_Type"
        Public Const orderNo As String = "orderNo"
        Public Const Code As String = "Code"
        Public Const ID As String = "ID"
        Public Const DOC As String = "DOC"

        'created by deepak for Vendor Unit Master
        Public Const UnitCode As String = "UnitCode"
        Public Const VendorListSearchInfo As String = "VendorListSearchInfo"

        'Added by Debayan Biswas on 07-11-2011 For Estimated_Data_Despatched_Status
        Public Const EstmtdDataDsptchdStatSearchInfo As String = "EstmtdDataDsptchdStatSearchInfo"

        Public Const SKUCode As String = "SKUCode"
        Public Const Unit As String = "Unit"
        Public Const VendorSKUSearchInfo As String = "VendorSKUSearchInfo"

        'Added by Debayan Biswas on 14-11-2011 For Depot_Despatch_Unitwise_Report
        Public Const DptDsptchUntWiseSearchInfo As String = "DptDsptchUntWiseSearchInfo"

        'Unit Despatch by Riddhi
        Public Const Challan_No As String = "Challan_No"
        Public Const Process_Year As String = "Process_Year"

        Public Const MonthlyUnitDespatchSearchInfo As String = "MonthlyUnitDespatchSearchInfo"

        Public Const IndentListSearchInfo As String = "IndentListSearchInfo"

        'Dashboar Search Criteria Added by Riddhi
        Public Const DashboardSearch As String = "DashboardSearch"

        'added by deepak on 01/02/2012
        Public Const PendingDespatchSearchInfo As String = "PendingDespatchSearchInfo"

        'added by deepak on 29/06/2012
        Public Const MonthLoadVsDespatchSearchInfo As String = "MonthLoadVsDespatchSearchInfo"

        Public Const SessionId As String = "SessionId"
        Public Const OCS_ID As String = "OCS_ID"

        Public Const tcTestresultSearchInfo As String = "tcTestresultSearchInfo"
        Public Const tcfgQualitySearchInfo As String = "tcfgQualitySearchInfo"
    End Class



    Public Class GeneralMessages
        Public Const SaveSuccess As String = "Record Saved Successfully"
        Public Const UpdateSuccess As String = "Record Updated Successfully"
        Public Const btnUpdate As String = "Update"
        Public Const btnSubmit As String = "Submit"
        Public Const AddNew As String = "New"
        Public Const GenCode As String = "GenCode"
        Public Const Submit As String = "Submit"
        Public Const ProjectCode As String = "ProjectCode"
        Public Const btnDelete As String = "Delete"
        Public Const Back As String = "Back"
    End Class
    Public Class ExpModules
        'Declared are the Exception module object
        Public Const UnauthorisedAccess As String = "You are not authorised to view this page"
        Public Const CertificatePrint As String = "Certificate Print"
        Public Const UserEntry As String = "User Entry/Update"
        Public Const TransportError As String = "A transport-level error"
    End Class
    Public Class ErrorMessages
        Public Const DatabaseConnectionError As String = "Could not Establish Connection with the Database.Contact Database Administrator."
        Public Const GeneralError As String = "An error has ocurred.Contact Administrator."
        Public Const DatabaseConnectionBreak As String = "Connection with the Database has broken.Contact System Administrator."

        Public Const UserNotActiveMessage As String = "Your account is currently Inactive. Please contact Administrator"
        Public Const InvalidLoginMessage As String = "Enter valid User Id/Password"
        Public Const ErrorCreateFolder As String = "Error occured while creating a folder."
        Public Const ErrorPopulateCity As String = "Error occured while populating the city."
        Public Const ErrorPopulateProjectType As String = "Error occured while populating the Project Type."
        Public Const ErrorLandCategory As String = "Error occured while populating Land Category."
        Public Const ErrorProjectStatus As String = "Error occured while populating Project Status."
        Public Const ErrorProjectMasterDetail As String = "Error occured while populating Project Master Detail."
        Public Const ErrorBindingGrid As String = "Error occured while Binding Grid values."


        Public Const ErrorPopulateLocation As String = "Error occured while Populating Project List Location."
        Public Const ErrorLoadingPage As String = "Error occured while loading the page."
        Public Const ErrorPopulateLegalchklist As String = "Error occured while Populating Legal Check List."
        Public Const ErrorPopulateAcquisitionchklist As String = "Error occured while acquisition Legal Check List."
        Public Const ErrorPopulateIssueDetails As String = "Error occured while populating issue details."
        Public Const ErrorPopulateActionByDetails As String = "Error occured while populating action by details."

        Public Const ErrorPopulateVendorName As String = "Error occured while populating vendor name."
        Public Const ErrorPopulateExtentUOM As String = "Error occured while populating Extent UOM."
        Public Const ErrorPopulateLandOwner As String = "Error occured while populating Land Owner."

        Public Const ErrorGridUpdate As String = "Error occured while Updating the grid row."
        Public Const ErrorGriddelete As String = "Error occured while deleting the grid row."
        Public Const ErrorPlotDetails As String = "Error occured while Populating Plot details."

        Public Const ErrorExporttoExcel As String = "Error occured while Exporting to excel."
        Public Const ErrorPopulateParentDoc As String = "Error occured while populating parent document."
        Public Const ErrorPopulatechecklist As String = "Error occured while populating check list."

        Public Const ErrorPopulateProjectPricing As String = "Error occured while populating project pricing."

        Public Const ErrorProjectPricingInsert As String = "Error occured while populating project pricing Insert."
        Public Const ErrorProjectLinkDocuments As String = "Error occured while populating project link documents."

        Public Const ErrorPopulatePdocs As String = "Error occured while populating project plot documents."
        Public Const FileUploadError As String = "File Upload Error. Given Read and Write Permission in Document Floder."
        Public Const Filenotfound As String = "File not Found in Path."
        Public Const ErrorFileDownloadDisplay As String = "Error occured while file download."
        Public Const ErrorPopulateCostGroup As String = "Error occured while populating cost group."
        Public Const ErrorOrderCreating As String = "Error occured while Creating an Order."
        Public Const ErrorPurchaseOrderApprove As String = "Error occured while Approve an Purchase Order."

        Public Const ErrorPurchaseOrderCreating As String = "Error occured while Creating a Purchase Order."
        Public Const ErrorAnnexureCrating As String = "Error occured while Creating a Purchase Order Annexure."
        Public Const ErrorPurchaseOrderAmending As String = "Error occured while Amending an Purchase Order."
        Public Const ErrorPurchaseOrderDeleteing As String = "Error occured while Deleting an Purchase Order."
        Public Const ErrorPurchaseOrderUpdating As String = "Error occured while Updating an Purchase Order."
        Public Const ErrorMtlRcptApprove As String = "Error occured while Approving Material Recipt."
        Public Const ErrorTripSheetCreating As String = "Error occured while Creating an Trip Sheet."
        Public Const ErrorTripSheetCollection As String = "Error occured while Collection Trip Sheet."
        Public Const ErrorDeleteing As String = "Error occured while Deleting."

        Public Const ErrorAdviceCreating As String = "Error occured while Creating an Advice."
        Public Const ErrorApprove As String = "Error occured while Approving."
        Public Const ErrorUpdate As String = "Error occured while Updating."

        Public Const InvalidSchemeCode As String = "Scheme Code already exists"

        Public Const Depotnotfound As String = "Depot Not Available"
        Public Const schemenotfound As String = "Scheme Not Available"
        Public Const itemnotfound As String = "Item Not Available"
        Public Const ErrorDepotRegion As String = "Error occured while populating depot"

        Public Const ErrorBrwoser As String = "Use Internet Explorer 6 or Higher Version"
    End Class
    Public Class AjaxServices
        Public Const Status As String = "Status"
        Public Const UserId As String = "UserID"
        Public Const ChangePassword As String = "ChangePassword"
        Public Const UserGroup As String = "UserGroup"
        Public Const FinYear As String = "FinYear"
        Public Const SerialControl As String = "SerialControl"
        Public Const MenuCode As String = "MenuCode"
        Public Const LovDetailsCode As String = "LovDetailsCode"
        Public Const LovMasterType As String = "LovMasterType"
        Public Const WorkFlow As String = "WorkFlow"
        Public Const ChangePasswordLink As String = "ChangePasswordLink"
        Public Const UserGroupId As String = "GetUserId"

    End Class

    Public Enum Roles As Integer
        None = 0
        Administrator = 1
    End Enum

    Public Class FileExtension
        'File Extension constant to retreive image
        Public Const TXT As String = "TXT"
        Public Const DOC As String = "DOC"
        Public Const XLS As String = "XLS"
        Public Const PDF As String = "PDF"
        Public Const JPG As String = "JPG"
        Public Const JPEG As String = "JPEG"
        Public Const GIF As String = "GIF"
        Public Const DOCX As String = "DOCX"
        Public Const XLSX As String = "XLSX"

    End Class

    Public Class ContentType
        Public Const Excel As String = "application/vnd.ms-excel"
        Public Const Text As String = "text/plain"
        Public Const Word As String = "application/msword"
        Public Const PDF As String = "application/octet-stream"
        Public Const JPEG As String = "image/JPEG"
        Public Const GIF As String = "image/GIF"
    End Class

    Public Class UserPrivilegesType
        Public Const Read As String = "R"
        Public Const Add As String = "A"
        Public Const Edit As String = "E"
        Public Const Delete As String = "D"
        Public Const Print As String = "P"
        Public Const Approval As String = "AP"
    End Class

    Public Class UserFormAccess

        Public Const SYSADMIN As String = "SYSADMIN"
        Public Const MARKETING As String = "MARKETING"
        Public Const GENADMIN As String = "GENADMIN"
        Public Const MIS As String = "MIS"
        Public Const DEPOT As String = "DEPOT"
        Public Const HOMARKETING As String = "HO-MARKETING"
        Public Const SYSGEN As String = "SYSGEN"
        Public Const Administrator As String = "ADMIN"
        'Public Const GENLADMIN As String = "GENLADMIN"
        Public Const SECURITY As String = "SECURITY"
        Public Const SYSMSTR As String = "SYSMSTR"
        Public Const REPORTS As String = "REPORTS"
        Public Const DASHBOARD As String = "DASHBOARD"
        Public Const SCORECARD As String = "SCORECARD"
        Public Const COMPMANAGE As String = "COMP-MANAGE"
        Public Const LEGAL As String = "LEGAL"
        Public Const FINANCE As String = "FINANCE"
        Public Const PROJECT As String = "PROJECT"
        Public Const CUSPROFILE As String = "CUSTPROFILE"
        Public Const HOACCOUNTS As String = "HO-ACCOUNTS"
        Public Const HO As String = "HO"
        Public Const SHIPPER As String = "SHIPPER"
        Public Const UNIT As String = "UNIT"
        'Added by Debayan Biswas on 24-05-2012 For REGION Department
        Public Const REGION As String = "REGION"
        Public Const TOKEN As String = "TOKEN"
        Public Const QC As String = "QC"
        Public Const VRS As String = "VRS"

    End Class

    Public Class StoreProcedures




        Public Const GetAllTableNames As String = "GetallTablesFromDB"
        Public Const GetAllColumnNames As String = "GetallColumnsFromDB"
        Public Const ExportData_Excel As String = "ExportData_Excel"

        'Excel_Parameter_MSTR Stored Procedure
        Public Const Excel_Parameter_MSTR_Insert As String = "Excel_Parameter_MSTR_Insert"
        Public Const Excel_Parameter_MSTR_Delete As String = "Excel_Parameter_MSTR_Delete"
        Public Const Excel_Parameter_ReportName_Get As String = "Excel_Parameter_ReportName_Get"
        Public Const Excel_Parameter_ReportUpdate_GetItem As String = "Excel_Parameter_ReportUpdate_GetItem"
        Public Const Excel_Parameter_ReportItem As String = "Excel_Parameter_ReportItem"

        'Added by Debayan Biswas on 07-11-2011 For Estimated_Data_Despatched_Status_Report
        Public Const Estimation_Data_Get_Details_Report As String = "Estimation_Data_Get_Details_Report"

        'Added by Debayan Biswas on 14-11-2011 For Depot_Despatch_Unitwise_Report
        Public Const Depot_Despatch_Unitwise_Report As String = "Depot_Despatch_Unitwise_Report"

        'Added by Debayan Biswas on 10-11-2011 For StockUploadSummary_Report
        Public Const StockUploadSummary_Report As String = "StockUploadSummary_Report"

        'Added by Debayan Biswas on 19-12-2011 For Despatched_Advice_Report
        Public Const Despatched_Advice_Report As String = "Despatched_Advice_Report"
        Public Const GetToken_requisition_dtlsForVendorDespatch As String = "GetToken_requisition_dtlsForVendorDespatch"
        ' Created by deepak for Monthly Unit Despatch Report
        Public Const MonthlyUnitDespatch_Report As String = "MonthlyUnitDespatch_Report"

        'Added by Debayan Biswas on 21-12-2011 For Unitwise_SKU_Despatched_Report
        Public Const Unitwise_SKU_Despatch_Report As String = "Unitwise_SKU_Despatch_Report"
        Public Const Unitwise_SKU_Despatch_Report_For_Summary As String = "Unitwise_SKU_Despatch_Report_For_Summary"

        'Added by Debayan Biswas on 15-11-2011 For Monthly_Depot_Indent_List_Report
        Public Const Mnthly_Dpt_Indent_List_Report As String = "Mnthly_Dpt_Indent_List_Report"

        'Added by Debayan Biswas on 07-01-2012 For User_Profile_List_Report
        Public Const UserProfile_List_Report As String = "UserProfile_List_Report"

    End Class

    Public Class TableNames
        'Dim i As Integer

        Public Const ExcelParameterMSTRTable As String = "Excel_Parameter_MSTR"


    End Class

    Public Class ReportDatasetTableName

        'Added by Debayan Biswas on 07-11-2011 For Estimated_Data_Despatched_Status_Report
        Public Const Estimation_Data_Despatched_Status As String = "Estimation_Data_Despatched_Status"

        'Added by Debayan Biswas on 14-11-2011 For Depot_Despatch_Unitwise_Report
        Public Const Depot_Despatched_Unitwise_Rpt As String = "Depot_Despatched_Unitwise_Rpt"


        'Added by Debayan Biswas on 10-11-2011 For StockUploadSummary_Report
        Public Const Stock_Upload_Summary_Report As String = "Stock_Upload_Summary_Report"

        'Added by Debayan Biswas on 19-12-2011 For Despatched_Advice_Report
        Public Const Despatched_Advice_Rpt_Tbl As String = "Despatched_Advice_Rpt_Tbl"
        Public Const GetToken_requisition_dtlsForVendorDespatch As String = "GetToken_requisition_dtlsForVendorDespatch"

        Public Const MonthlyUnitDespatchDataSet As String = "MonthlyUnitDespatchDataSet"

        'Added by Debayan Biswas on 21-12-2011 For Unitwise_SKU_Despatched_Report
        Public Const UnitWise_SKU_Despatch_Rpt_Tbl As String = "UnitWise_SKU_Despatch_Rpt_Tbl"
        Public Const UnitWise_SKU_Despatch_Summary_Rpt_Tbl As String = "UnitWise_SKU_Despatch_Summary_Rpt_Tbl"

        'Added by Debayan Biswas on 15-11-2011 For Monthly_Depot_Indent_List_Report
        Public Const Monthly_Dpt_Indent_List_Rpt_Tbl As String = "Monthly_Dpt_Indent_List_Rpt_Tbl"

        'Added by Debayan Biswas on 07-01-2012 For User_Profile_List_Report
        Public Const UserProfileReport_DT As String = "UserProfileReport_DT"

    End Class
    Public Class ReportView
        Public Const ReportFileLoc As String = "Reports"

        Public Class ReportName
            'Public Const TripReport As String = "\TripSheetReport.rpt"

            'Added by Debayan Biswas on 07-11-2011 For Estimated_Data_Despatched_Status_Report
            Public Const Estimation_Data_Despatched_Status_Report As String = "\Estimation_Data_Despatched_Status_Report.rpt"

            'Added by Debayan Biswas on 14-11-2011 For Depot_Despatch_Unitwise_Report
            Public Const Depot_Despatch_Unitwise_Report As String = "\Depot_Despatch_Unitwise_Report.rpt"

            'Added by Debayan Biswas on 10-11-2011 For StockUploadSummary_Report
            Public Const Stock_Upload_Summary_Report As String = "\Stock_Upload_Summary_Report.rpt"

            'Added by Debayan Biswas on 19-12-2011 For Despatched_Advice_Report
            Public Const Despatched_Advice_Report As String = "\Despatched_Advice_Report.rpt"
            Public Const Token_Despatched_Advice_Report As String = "\Token_Despatched_Advice_Report.rpt"

            Public Const MonthlyUnitDespatch As String = "\MonthlyUnitDespatch.rpt"

            'Added by Debayan Biswas on 21-12-2011 For Unitwise_SKU_Despatched_Report
            Public Const Unitwise_SKU_Despatched_Report As String = "\Unitwise_SKU_Despatched_Report.rpt"
            Public Const Unitwise_SKU_Despatch_Summary_Report As String = "\Unitwise_SKU_Despatch_Summary_Report.rpt"

            'Added by Debayan Biswas on 15-11-2011 For Monthly_Depot_Indent_List_Report
            Public Const Monthly_Depot_Indent_List_Report As String = "\Monthly_Depot_Indent_List_Report.rpt"

            'Added by Debayan Biswas on 07-01-2012 For User_Profile_List_Report
            Public Const UserProfileReport As String = "\UserProfileReport.rpt"
            Public Const OcsReport As String = "Reports\ProductOC.rpt"
        End Class
        Public Class ReportCase

            'Added by Debayan Biswas on 07-11-2011 For Estimated_Data_Despatched_Status_Report
            Public Const EstmtnDataDsptchdStatRptCase As String = "EstmtnDataDsptchdStatRptCase"

            'Added by Debayan Biswas on 14-11-2011 For Depot_Despatch_Unitwise_Report
            Public Const DptDsptchUntWiseRptCase As String = "DptDsptchUntWiseRptCase"

            'Added by Debayan Biswas on 10-11-2011 For StockUploadSummary_Report
            Public Const StockUploadSummaryRptCase As String = "StockUploadSummaryRptCase"

            'Added by Debayan Biswas on 19-12-2011 For Despatched_Advice_Report
            Public Const DespatchedAdviceRptCase As String = "DespatchedAdviceRptCase"

            Public Const TokenDespatchedAdviceRptCase As String = "TokenDespatchedAdviceRptCase"

            Public Const MonthlyUnitDespatchReportCase As String = "MonthlyUnitDespatchReportCase"

            'Added by Debayan Biswas on 21-12-2011 For Unitwise_SKU_Despatched_Report
            Public Const UntWisSKUDsptchRptCase As String = "UntWisSKUDsptchRptCase"
            Public Const UntWisSKUDsptchSmmryRptCase As String = "UntWisSKUDsptchSmmryRptCase"

            'Added by Debayan Biswas on 15-11-2011 For Monthly_Depot_Indent_List_Report
            Public Const MonthlyDepotIndentListRptCase As String = "MonthlyDepotIndentListRptCase"

            'Added by Debayan Biswas on 07-01-2012 For User_Profile_List_Report
            Public Const UserProfileReportCase As String = "UserProfileReportCase"

        End Class
    End Class

    'Created by Rajesh Daniel on 10/11/2008
    Public Class DatabaseDbTypes
        Public Const DateTime As String = "DateTime"
        Public Const SqlDateTime As String = "SqlDateTime"
        Public Const UInt64 As String = "UInt64"
        Public Const UInt32 As String = "UInt32"
        Public Const UInt16 As String = "UInt16"
        Public Const [Decimal] As String = "Decimal"
        Public Const [Double] As String = "Double"
        Public Const [String] As String = "String"
        Public Const Int64 As String = "Int64"
        Public Const Int32 As String = "Int32"
        Public Const Int16 As String = "Int16"
    End Class
    'Created by Rajesh Daniel on 10/11/2008
    Public Class MinimunValues
        Public Const MinStringValue As String = "''"
        Public Const MinDataTimeValueAlt As String = "01/01/0001 12:00:00 AM"
        Public Const MinDataTimeValue As String = "1/1/0001 12:00:00 AM"
        Public Const MinInt32Value As String = "-2147483648"
        Public Const MinInt32ValueAlt As String = "0"
    End Class
    'Created by Rajesh Daniel on 10/11/2008
    Public Class Parameters
        Public Const OutParameter1 As String = "@mthgenid"
        Public Const OutParameter2 As String = "@OutputParameter2"
        Public Const OutParameter3 As String = "@OutputParameter3"
        Public Const OutParameter4 As String = "@OutputParameter4"

    End Class

    

End Class

