'**************************************************
'Copyright	    : VMS, MCC, KOLKATA
'Source	        : App_Code/IndentMaster.vb
'Created Date	: 12-December-2011
'Created By	    : Rohan Mazumdar
'Version	    : R02.00.00
'Description	: Code behind file for IndentMaster Class

'Modified By       Modified On       Version         Reason

'*************************************************************

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports VMS.DataAccess
Imports System.Data.SqlTypes

Public Class IndentMaster


#Region "Get distinct vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetDstnctVendoratVndSkuMstr(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsVendor As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@product_code"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentProduct

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_DstnctVendor_VndSkuMstr", Data.CommandType.StoredProcedure, sqlParams)

        Return dsVendor

    End Function

#End Region

#Region "Get distinct vendor from dbo.vendor_sku_mstr for all depot."

    Function GetDstnctVendoratVndSkuMstr_HO() As DataSet

        Dim dsVendor As System.Data.DataSet

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_DstnctVendor_VndSkuMstr_HO", Data.CommandType.StoredProcedure)

        Return dsVendor

    End Function

#End Region

#Region "Get distinct products for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetDstnctVendorProductatVndSkuMstr(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsVendor As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@indent_id"
        sqlParams(1).DbType = DbType.Int64
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(indent_header.IndentID.Equals(Integer.MinValue), 0, indent_header.IndentID)

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@vendor_unit"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = indent_header.IndentVendorUnit

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_DstnctVendorProduct_VndSkuMstr", Data.CommandType.StoredProcedure, sqlParams)

        Return dsVendor

    End Function

#End Region

#Region "Get distinct products for a vendor from dbo.vendor_sku_mstr for all depot."

    Function GetDstnctVendorProductatVndSkuMstr_HO() As DataSet

        Dim dsVendor As System.Data.DataSet

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_DstnctVendorProduct_VndSkuMstr_HO", Data.CommandType.StoredProcedure)

        Return dsVendor

    End Function

#End Region

#Region "Get product sku codes for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetVendorProductSKUsatVndSkuMstr(ByVal indent_header As IndentHeaderEntity, ByVal updateYN As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentVendorUnit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinYear

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@fin_month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = indent_header.IndentFinMonth

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@product_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = indent_header.IndentProduct

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@update_yn"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = updateYN

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_VendorSKUDetails_VndSkuMstr", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function

#End Region

#Region "Get product sku codes for a vendor from dbo.vendor_sku_mstr for all depot."

    Function GetVendorProductSKUsatVndSkuMstr_HO(ByVal indent_header As IndentHeaderEntity, ByVal updateYN As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentVendorUnit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinYear

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@fin_month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = indent_header.IndentFinMonth

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@product_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = indent_header.IndentProduct

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@update_yn"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = updateYN

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_VendorSKUDetails_VndSkuMstr_HO_new", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function

#End Region

    Public Function InsertIndentHeader(ByRef indnt_hdr As IndentHeaderEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim IndentID As Integer

        Try

            Dim sqlParams(5) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_hdr.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_hdr.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_hdr.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@new_indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Output
            sqlParams(3).Value = Integer.MinValue

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@vendor_unit"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = indnt_hdr.IndentVendorUnit

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = indnt_hdr.IndentCreatedUser

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "IndentEntry_CreateNewHeader"
            sqlCmd.Parameters.AddRange(sqlParams)
            sqlCmd.ExecuteNonQuery()

            IndentID = CType(sqlParams(3).Value, Integer)

        Catch ex As Exception
            Throw ex
        End Try

        Return IndentID

    End Function

    Public Function InsertIndentDetails(ByRef indnt_dtl As IndentDetailEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(13) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_dtl.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_dtl.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_dtl.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = indnt_dtl.IndentID

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@vendor_unit"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = indnt_dtl.IndentVendorUnit

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = indnt_dtl.IndentCreatedUser

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@sku_code"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = indnt_dtl.IndentSKUCode

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@sku_nop"
            sqlParams(7).DbType = DbType.Int32
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = indnt_dtl.IndentSKUNOP

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@sku_vol"
            sqlParams(8).DbType = DbType.Decimal
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = indnt_dtl.IndentSKUVol

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@sku_uom"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = indnt_dtl.IndentSKUUOM

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@remarks"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = indnt_dtl.IndentSKURemarks

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@pending_load"
            sqlParams(11).DbType = DbType.Int32
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = indnt_dtl.IndentSKUPendingLoad

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@indent_to_date"
            sqlParams(12).DbType = DbType.Int32
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = indnt_dtl.IndentSKUIndentToDate

            sqlParams(13) = New SqlParameter()
            sqlParams(13).ParameterName = "@desp_to_date"
            sqlParams(13).DbType = DbType.Int32
            sqlParams(13).Direction = Data.ParameterDirection.Input
            sqlParams(13).Value = indnt_dtl.IndentSKUDespatchToDate

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "IndentEntry_CreateNewDetail"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function

    Public Function DeleteIndentDetails(ByRef indnt_dtl As IndentDetailEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_dtl.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_dtl.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_dtl.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = indnt_dtl.IndentID

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "IndentEntry_DeleteIndentDetail"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function

#Region "Get indent list from indent hdr and dtls."


    Function GetIndentList(ByVal indent_header As IndentHeaderEntity, ByVal user_id As String, ByVal Category As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(indent_header.IndentDepot = String.Empty, DBNull.Value, indent_header.IndentDepot)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@fin_year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentFinYear

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinMonth

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@status"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(indent_header.IndentStatus = String.Empty, DBNull.Value, indent_header.IndentStatus)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@user_id"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = user_id

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@category"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = Category

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentList_getIndentsList_vr1", Data.CommandType.StoredProcedure, sqlParams)
        'dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentList_getIndentsList", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function
#End Region

#Region "Get indent PO Download list from indent hdr and dtls."


    Function GetIndentPoDownloadList(ByVal indent_header As IndentHeaderEntity, ByVal user_id As String, ByVal Category As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(indent_header.IndentDepot = String.Empty, DBNull.Value, indent_header.IndentDepot)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@fin_year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentFinYear

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IIf(indent_header.IndentFinMonth = String.Empty, DBNull.Value, indent_header.IndentFinMonth)

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@status"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(indent_header.IndentStatus = String.Empty, DBNull.Value, indent_header.IndentStatus)

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@user_id"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = user_id

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@category"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = Category

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("Indent_Po_Download_getList", Data.CommandType.StoredProcedure, sqlParams)
        'dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentList_getIndentsList", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function
#End Region

#Region "Get indent Depot Email Id."
    Function GetIndentDepotEmail(ByVal depotCode As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = depotCode

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("Indent_Depot_Email", Data.CommandType.StoredProcedure, sqlParams)
        'dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentList_getIndentsList", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function
#End Region

#Region "Get indent HO Email Id."
    Function GetIndentHOEmail() As DataSet

        Dim dsSKU As System.Data.DataSet

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("Indent_HO_Email", Data.CommandType.StoredProcedure)
        'dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentList_getIndentsList", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function
#End Region

#Region " Indent Invoice Request mail status update."

    Public Function IndentRequestMailSentUpdate(ByVal indentNo As String, ByVal depotCode As String, ByVal finYr As String, ByVal remark As String, ByVal userId As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(4) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@indentNo"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indentNo

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@depotCode"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = depotCode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@finyr"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = finYr

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@remark"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = remark

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@modified_user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = userId

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Indent_Inv_Request_mail_status_update"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function

#End Region
    Public Function DeleteIndentHeaderandDetails(ByRef indnt_dtl As IndentHeaderEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_dtl.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_dtl.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_dtl.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = indnt_dtl.IndentID

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[IndentEntry_Delete_Detail]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function

    Public Function IndentEntryApproveReject(ByRef indnt_dtl As IndentHeaderEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(6) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_dtl.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_dtl.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_dtl.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = indnt_dtl.IndentID

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@approve_yn"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = indnt_dtl.IndentApproveYN

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@modified_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = indnt_dtl.IndentCreatedUser

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@remarks"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = IIf(Trim(indnt_dtl.IndentRemarks) = String.Empty, DBNull.Value, Trim(indnt_dtl.IndentRemarks))

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "IndentEntry_ApproveReject"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function

#Region "Get product sku codes for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetIndentDetails(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@fin_year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentFinYear

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinMonth

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@indent_id"
        sqlParams(3).DbType = DbType.Int32
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = indent_header.IndentID

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_IndentDetails", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function

#End Region

    Public Function ModifyIndentHeader(ByRef indnt_hdr As IndentHeaderEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim NumofRowsAffected As Integer

        Try

            Dim sqlParams(5) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_hdr.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_hdr.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_hdr.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = indnt_hdr.IndentID

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@vendor_unit"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = indnt_hdr.IndentVendorUnit

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = indnt_hdr.IndentCreatedUser

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[IndentEntry_ModifyHeader_Vr1]"
            sqlCmd.Parameters.AddRange(sqlParams)
            NumofRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return NumofRowsAffected

    End Function

    Public Function IsSerialMasterHasRecord(ByRef indnt_hdr As IndentHeaderEntity) As DataSet
        Dim ds As DataSet
        Try

            Dim sqlParams(1) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@fin_year"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_hdr.IndentFinYear

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_hdr.IndentDepot

            ds = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_IsSerialMasterHasRecord", Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try

        Return ds

    End Function

#Region "Get indent count based on indent status."

    Function GetIndentCount(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim ds As System.Data.DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(indent_header.IndentDepot = String.Empty, DBNull.Value, indent_header.IndentDepot)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@fin_year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentFinYear

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinMonth

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@status"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = IIf(indent_header.IndentStatus = String.Empty, DBNull.Value, indent_header.IndentStatus)

        ds = DBFactory.GetHelper().ExecuteDataSet("Indent_getIndentsCount", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function

#End Region

    '=================================================Code For Indent Entry Add====================================================
#Region "Get distinct vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetDstnctVendorListatVndSkuMstr(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsVendor As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot


        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_DstnctVendor_VndSkuMstr_NewIndent", Data.CommandType.StoredProcedure, sqlParams)

        Return dsVendor

    End Function

#End Region

#Region "Get distinct products for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetDstnctVendorProductListatVndSkuMstr(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsVendor As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentVendorUnit

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_DstnctVendorProduct_VndSkuMstr_NewIndent", Data.CommandType.StoredProcedure, sqlParams)

        Return dsVendor

    End Function

#End Region

#Region "Get product sku codes for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetVendorProductSKUListsatVndSkuMstr(ByVal indent_header As IndentHeaderEntity, ByVal updateYN As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentVendorUnit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinYear

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@fin_month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = indent_header.IndentFinMonth

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@product_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(indent_header.IndentProduct.Equals(String.Empty), DBNull.Value, indent_header.IndentProduct)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@update_yn"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = updateYN

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_VendorSKUDetails_VndSkuMstr_NewIndent", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function

#End Region
#Region "Get distinct Industrial products for a vendor from dbo.vendor_sku_mstr for a selected depot."
    Function GetDistinctIndustrialVendorProductListatVndSkuMstr(ByVal indent_header As IndentHeaderEntity) As DataSet
        Dim dsVendor As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentVendorUnit

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_VendorProduct_VndSkuMstr_IndustrialIndent", Data.CommandType.StoredProcedure, sqlParams)

        Return dsVendor

    End Function

#End Region

#Region "Get Industial product sku codes for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetVendorIndustrialProductSKUListsatVndSkuMstr(ByVal indent_header As IndentHeaderEntity, ByVal updateYN As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentVendorUnit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinYear

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@fin_month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = indent_header.IndentFinMonth

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@product_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(indent_header.IndentProduct.Equals(String.Empty), DBNull.Value, indent_header.IndentProduct)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@update_yn"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = updateYN

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_VendorSKUDetails_VndSkuMstr_IndustrialIndent", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function

#End Region

#Region "Get distinct products for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetDstnctSTPVendorProductListatVndSkuMstr(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsVendor As System.Data.DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        'sqlParams(1) = New SqlParameter()
        'sqlParams(1).ParameterName = "@vendor_unit"
        'sqlParams(1).DbType = DbType.String
        'sqlParams(1).Direction = Data.ParameterDirection.Input
        'sqlParams(1).Value = indent_header.IndentVendorUnit

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntrySTP_DstnctVendorProduct_VndSkuMstr_NewIndent", Data.CommandType.StoredProcedure, sqlParams)

        Return dsVendor

    End Function

#End Region

#Region "Get product sku codes for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetSTPVendorProductSKUListsatVndSkuMstr(ByVal indent_header As IndentHeaderEntity, ByVal updateYN As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(5) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendor_unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentVendorUnit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinYear

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@fin_month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = indent_header.IndentFinMonth

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@product_code"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(indent_header.IndentProduct.Equals(String.Empty), DBNull.Value, indent_header.IndentProduct)

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@update_yn"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = updateYN

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentEntrySTP_VendorSKUDetails_VndSkuMstr_NewIndent", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function

#End Region

#Region "Get distinct vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetDstnctSTPVendoratVndSkuMstr(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsVendor As System.Data.DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@product_code"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentProduct

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("IndentEntrySTP_DstnctVendor_VndSkuMstr", Data.CommandType.StoredProcedure, sqlParams)

        Return dsVendor

    End Function

#End Region

#Region "Get product sku codes for a vendor from dbo.vendor_sku_mstr for a selected depot."

    Function GetIndentDetailsMail(ByVal indent_header As IndentHeaderEntity) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = indent_header.IndentDepot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@fin_year"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = indent_header.IndentFinYear

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@fin_month"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = indent_header.IndentFinMonth

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@indent_id"
        sqlParams(3).DbType = DbType.Int32
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = indent_header.IndentID

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("IndentEntry_IndentDetails_Mail", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function

#End Region
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function GetPONumber(ByVal vendorCode As String, ByVal depotCode As String) As DataSet
        Dim DS As System.Data.DataSet
        Dim sqlParams As SqlParameter() = New SqlParameter(1) {}
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendor_code"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = System.Data.ParameterDirection.Input
        sqlParams(0).Value = vendorCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot_code"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = System.Data.ParameterDirection.Input
        sqlParams(1).Value = depotCode

        DS = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_PO_Details]", System.Data.CommandType.StoredProcedure, sqlParams)
        Return DS
    End Function

    Public Function GetSKU(ByVal skucode As String, ByVal vendorcode As String, ByVal depotcode As String) As DataSet
        Dim dsVendor As System.Data.DataSet
        Dim sqlParams As SqlParameter() = New SqlParameter(2) {}
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@skucode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = System.Data.ParameterDirection.Input
        sqlParams(0).Value = skucode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@vendorcode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = System.Data.ParameterDirection.Input
        sqlParams(1).Value = vendorcode

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@depotcode"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = System.Data.ParameterDirection.Input
        sqlParams(2).Value = depotcode

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("[vms].[dbo].[Get_SKU_Details]", System.Data.CommandType.StoredProcedure, sqlParams)
        Return dsVendor
    End Function
    Public Function insertAdditionalRequest(ByVal podt As DataTable, ByVal depotcode As String, ByVal vendorcode As String, ByVal remarks As String, ByVal Userid As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim IndentID As Integer

        Try
            Dim sqlParams As SqlParameter() = New SqlParameter(5) {}
            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@tbl"
            sqlParams(0).SqlDbType = SqlDbType.Structured
            sqlParams(0).Direction = System.Data.ParameterDirection.Input
            sqlParams(0).Value = podt

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@depot_code"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = System.Data.ParameterDirection.Input
            sqlParams(1).Value = depotcode

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@vendor_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = System.Data.ParameterDirection.Input
            sqlParams(2).Value = vendorcode

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@remarks"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = System.Data.ParameterDirection.Input
            sqlParams(3).Value = If(remarks <> String.Empty, remarks, CObj(DBNull.Value))

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@created_user"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = System.Data.ParameterDirection.Input
            sqlParams(4).Value = Userid

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@output"
            sqlParams(5).DbType = DbType.Int32
            sqlParams(5).Direction = System.Data.ParameterDirection.Output
            sqlParams(5).Value = Integer.MinValue

            Dim sqlCmd As SqlCommand = New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[vms].[dbo].[Insert_Additional_Request]"
            sqlCmd.Parameters.AddRange(sqlParams)
            sqlCmd.ExecuteNonQuery()
            IndentID = System.Convert.ToInt32(sqlParams(5).Value)
        Catch ex As Exception
            Throw ex
        End Try

        Return IndentID
    End Function
    Public Function GetSiteId(ByVal depot As String, ByVal vendorcode As String) As DataSet
        Dim dsVendor As System.Data.DataSet
        Dim sqlParams As SqlParameter() = New SqlParameter(1) {}
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@vendorCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = System.Data.ParameterDirection.Input
        sqlParams(0).Value = vendorcode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depotCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = System.Data.ParameterDirection.Input
        sqlParams(1).Value = depot

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Site_ID_vr1]", System.Data.CommandType.StoredProcedure, sqlParams)
        Return dsVendor
    End Function
    Public Function GetVendorList(ByVal depot As String) As DataSet
        Dim dsVendor As System.Data.DataSet
        Dim sqlParams As SqlParameter() = New SqlParameter(0) {}
        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = System.Data.ParameterDirection.Input
        sqlParams(0).Value = depot

        dsVendor = DBFactory.GetHelper().ExecuteDataSet("[VMS].[dbo].[VendorList_Get_vr1]", System.Data.CommandType.StoredProcedure, sqlParams)
        Return dsVendor
    End Function
    Public Function GetProductCategory() As DataSet

        Dim Category As DataSet


        Category = DBFactory.GetHelper().ExecuteDataSet("Get_Product_Category", Data.CommandType.StoredProcedure)

        Return Category

    End Function
#Region "Get Indent History."
    Function GetIndentHistory(ByVal FinYear As String, ByVal DepotCode As String, ByVal IndentId As String) As DataSet

        Dim dsSKU As System.Data.DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@FinYr"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = FinYear

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@DepotCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = DepotCode

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@IndentId"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = IndentId

        dsSKU = DBFactory.GetHelper().ExecuteDataSet("[dbo].[Get_Indent_History_Details]", Data.CommandType.StoredProcedure, sqlParams)

        Return dsSKU

    End Function
#End Region
    Public Function BlockIndent_SkuRecord(ByRef indnt_dtls As IndentDetailEntity) As DataSet
        Dim ds As DataSet
        Try

            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@sku_code"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_dtls.IndentSKUCode

            ds = DBFactory.GetHelper().ExecuteDataSet("Get_Block_Indent_Sku", Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try

        Return ds

    End Function
    Public Function InsertIndentDetails_HO(ByRef indnt_dtl As IndentDetailEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(14) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_dtl.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_dtl.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_dtl.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = indnt_dtl.IndentID

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@vendor_unit"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = indnt_dtl.IndentVendorUnit

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@created_user"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = indnt_dtl.IndentCreatedUser

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@sku_code"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = indnt_dtl.IndentSKUCode

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@sku_nop"
            sqlParams(7).DbType = DbType.Int32
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = indnt_dtl.IndentSKUNOP

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@sku_vol"
            sqlParams(8).DbType = DbType.Decimal
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = indnt_dtl.IndentSKUVol

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@sku_uom"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = indnt_dtl.IndentSKUUOM

            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@remarks"
            sqlParams(10).DbType = DbType.String
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = indnt_dtl.IndentSKURemarks

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@pending_load"
            sqlParams(11).DbType = DbType.Int32
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = indnt_dtl.IndentSKUPendingLoad

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@indent_to_date"
            sqlParams(12).DbType = DbType.Int32
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = indnt_dtl.IndentSKUIndentToDate

            sqlParams(13) = New SqlParameter()
            sqlParams(13).ParameterName = "@desp_to_date"
            sqlParams(13).DbType = DbType.Int32
            sqlParams(13).Direction = Data.ParameterDirection.Input
            sqlParams(13).Value = indnt_dtl.IndentSKUDespatchToDate

            sqlParams(14) = New SqlParameter()
            sqlParams(14).ParameterName = "@indd_priority"
            sqlParams(14).DbType = DbType.String
            sqlParams(14).Direction = Data.ParameterDirection.Input
            sqlParams(14).Value = indnt_dtl.IndentPriority

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[IndentEntry_CreateNewDetail_HO]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
    Public Function Insert_Indent_Document(ByVal IndentNo As Int32, ByVal FileName As String, ByVal Doc_Path As String, ByVal UserId As String, ByVal DepotCode As String, ByVal FinYear As String, ByVal DocMonth As String, ByVal sqlconn As SqlConnection, ByVal sqltrans As SqlTransaction) As Integer
        Dim NumsRowAffected As New Integer

        Try
            Dim sqlparams(6) As SqlParameter

            sqlparams(0) = New SqlParameter
            sqlparams(0).ParameterName = "@IndentNo"
            sqlparams(0).DbType = DbType.Int64
            sqlparams(0).Direction = ParameterDirection.Input
            sqlparams(0).Value = IndentNo

            sqlparams(1) = New SqlParameter
            sqlparams(1).ParameterName = "@FileName"
            sqlparams(1).DbType = DbType.String
            sqlparams(1).Direction = ParameterDirection.Input
            sqlparams(1).Value = FileName

            sqlparams(2) = New SqlParameter
            sqlparams(2).ParameterName = "@Doc_Path"
            sqlparams(2).DbType = DbType.String
            sqlparams(2).Direction = ParameterDirection.Input
            sqlparams(2).Value = Doc_Path

            sqlparams(3) = New SqlParameter
            sqlparams(3).ParameterName = "@UserId"
            sqlparams(3).DbType = DbType.String
            sqlparams(3).Direction = ParameterDirection.Input
            sqlparams(3).Value = UserId

            sqlparams(4) = New SqlParameter
            sqlparams(4).ParameterName = "@DepotCode"
            sqlparams(4).DbType = DbType.String
            sqlparams(4).Direction = ParameterDirection.Input
            sqlparams(4).Value = DepotCode

            sqlparams(5) = New SqlParameter
            sqlparams(5).ParameterName = "@FinYear"
            sqlparams(5).DbType = DbType.String
            sqlparams(5).Direction = ParameterDirection.Input
            sqlparams(5).Value = FinYear

            sqlparams(6) = New SqlParameter
            sqlparams(6).ParameterName = "@DocMonth"
            sqlparams(6).DbType = DbType.String
            sqlparams(6).Direction = ParameterDirection.Input
            sqlparams(6).Value = DocMonth

            Dim sqlcmd As New SqlCommand
            sqlcmd.CommandText = "[dbo].[Indent_Mail_doc]"
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
    Public Function DeleteIndent_Doc_dDetails(ByRef indnt_dtl As IndentHeaderEntity, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try

            Dim sqlParams(3) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = indnt_dtl.IndentDepot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = indnt_dtl.IndentFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@fin_month"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = indnt_dtl.IndentFinMonth

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@indent_id"
            sqlParams(3).DbType = DbType.Int32
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = indnt_dtl.IndentID

            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[dbo].[Indent_Doc_Delete]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
    Public Function Applicable_SkuRecord(ByVal SkuCode As String) As DataSet
        Dim ds As DataSet
        Try
            Dim sqlParams(0) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@skucode"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = SkuCode

            ds = DBFactory.GetHelper().ExecuteDataSet("Check_ApplicableSku", Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function
    Public Function Minimum_RateVendorList(ByRef depot As String, ByVal Sku As String, ByVal Vencode As String) As DataSet
        Dim ds As DataSet
        Try
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@depot"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = depot

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@sku"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = Sku

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@vendor_code"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = Vencode

            ds = DBFactory.GetHelper().ExecuteDataSet("Get_MinimumRateVendorList", Data.CommandType.StoredProcedure, sqlParams)

        Catch ex As Exception
            Throw ex
        End Try

        Return ds

    End Function
End Class
