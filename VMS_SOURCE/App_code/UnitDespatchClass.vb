Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports VMS.DataAccess

Public Class UnitDespatchClass
#Region "Get Screen Details"

    Function GetSCreenDetails(ByVal unitCode As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unitCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = unitCode
        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Get_Screen_Details]", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region
#Region "Get product list"

    Function GetProductList(ByVal unitCode As String, ByVal depoCode As String, ByVal active As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unitCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = unitCode

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depotCode"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = depoCode

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@active"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = active



        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Get_Product_List]", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region
#Region "Get SKU Details"

    Function GetSKUDetails(ByVal productCode As String, ByVal allSKU As String, ByVal active As String, ByVal unit As String, ByVal depot As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@productCode"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(productCode <> String.Empty, productCode, DBNull.Value)

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@allSKU"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = allSKU

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@active"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = active

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@unit"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = unit

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@depot"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = depot


        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Get_SKU_Details]", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region

#Region "Insert Despatch Header"
    Function InsertDespHeader(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByRef DespEntity As DespatchHeaderEntity) As Integer

        Dim numRowsAffected As Integer
        Dim challanNo As Integer
        challanNo = -1

        Try

            Dim sqlParams(17) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@desph_desp_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = DespEntity.DespUnit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@desph_desp_depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = DespEntity.DespDepot

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@desph_challan_fin_year"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = DespEntity.ChallanFinYear

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@desph_challan_no"
            sqlParams(3).DbType = DbType.Int64
            sqlParams(3).Direction = Data.ParameterDirection.Output

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@desph_challan_date"
            sqlParams(4).DbType = DbType.DateTime
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = DespEntity.ChallanDate


            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@desph_total_ltr"
            sqlParams(5).DbType = DbType.Decimal
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = DespEntity.TotalLtr

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@desph_total_kg"
            sqlParams(6).DbType = DbType.Decimal
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = DespEntity.TotalKg

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@desph_transporter_name"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = DespEntity.TransporterName

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@desph_truck_no"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = DespEntity.TruckNo

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@desph_excise_gp_no"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = DespEntity.ExciseGpNo


            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@desph_excise_gp_dt"
            sqlParams(10).DbType = DbType.DateTime
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = DespEntity.ExciseGpDt

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@created_user"
            sqlParams(11).DbType = DbType.String
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = DespEntity.CreatedUser

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@active"
            sqlParams(12).DbType = DbType.String
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = DespEntity.ActiveStatus

            sqlParams(13) = New SqlParameter()
            sqlParams(13).ParameterName = "@desph_process_month"
            sqlParams(13).DbType = DbType.String
            sqlParams(13).Direction = Data.ParameterDirection.Input
            sqlParams(13).Value = DespEntity.ProcessMonth

            sqlParams(14) = New SqlParameter()
            sqlParams(14).ParameterName = "@desph_road_permit_no"
            sqlParams(14).DbType = DbType.String
            sqlParams(14).Direction = Data.ParameterDirection.Input
            sqlParams(14).Value = DespEntity.RoadPermitNo

            sqlParams(15) = New SqlParameter()
            sqlParams(15).ParameterName = "@desph_po_no"
            sqlParams(15).DbType = DbType.String
            sqlParams(15).Direction = Data.ParameterDirection.Input
            sqlParams(15).Value = DespEntity.po_no

            sqlParams(16) = New SqlParameter()
            sqlParams(16).ParameterName = "@site_name"
            sqlParams(16).DbType = DbType.String
            sqlParams(16).Direction = Data.ParameterDirection.Input
            sqlParams(16).Value = DespEntity.site_name

            sqlParams(17) = New SqlParameter()
            sqlParams(17).ParameterName = "@delivery_depot"
            sqlParams(17).DbType = DbType.String
            sqlParams(17).Direction = Data.ParameterDirection.Input
            sqlParams(17).Value = DespEntity.delivery_depot

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Unit_Dspatch_Insert_Hdr]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

            challanNo = sqlParams(3).Value

        Catch ex As Exception
            Throw ex
        End Try

        Return challanNo

    End Function
#End Region

#Region "Insert despatch Detail"
    Function InsertDespatchDetail(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByRef DespEntity As DespatchDetailEntity) As Integer

        Dim numRowsAffected As Integer
        
        Try

            Dim sqlParams(19) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@despd_desp_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = DespEntity.DespUnit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@despd_desp_depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = DespEntity.DespDepot

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@despd_challan_fin_year"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = DespEntity.ChallanFinYear

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@despd_challan_no"
            sqlParams(3).DbType = DbType.Int64
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = DespEntity.ChallanNo

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@despd_challan_date"
            sqlParams(4).DbType = DbType.DateTime
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = DespEntity.ChallanDate


            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@despd_srl"
            sqlParams(5).DbType = DbType.Int64
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = DespEntity.Srl

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@despd_sku_code"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = DespEntity.SkuCode

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@despd_sku_uom"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = DespEntity.SkuUom

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@despd_desp_nop"
            sqlParams(8).DbType = DbType.Int64
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = DespEntity.DespNop

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@despd_sku_vol"
            sqlParams(9).DbType = DbType.Decimal
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = DespEntity.SkuVol


            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@despd_auto_indent"
            sqlParams(10).DbType = DbType.Int64
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = DespEntity.AutoIndent

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@despd_depot_indent"
            sqlParams(11).DbType = DbType.Int64
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = DespEntity.DepotIndent

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@despd_indent_total"
            sqlParams(12).DbType = DbType.Int64
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = DespEntity.IndentTotal

            sqlParams(13) = New SqlParameter()
            sqlParams(13).ParameterName = "@despd_despatch_to_date"
            sqlParams(13).DbType = DbType.Int64
            sqlParams(13).Direction = Data.ParameterDirection.Input
            sqlParams(13).Value = DespEntity.DespatchToDate

            sqlParams(14) = New SqlParameter()
            sqlParams(14).ParameterName = "@despd_pending_load"
            sqlParams(14).DbType = DbType.Int64
            sqlParams(14).Direction = Data.ParameterDirection.Input
            sqlParams(14).Value = DespEntity.PendingLoad

            sqlParams(15) = New SqlParameter()
            sqlParams(15).ParameterName = "@created_user"
            sqlParams(15).DbType = DbType.String
            sqlParams(15).Direction = Data.ParameterDirection.Input
            sqlParams(15).Value = DespEntity.CreatedUser

            sqlParams(16) = New SqlParameter()
            sqlParams(16).ParameterName = "@active"
            sqlParams(16).DbType = DbType.String
            sqlParams(16).Direction = Data.ParameterDirection.Input
            sqlParams(16).Value = DespEntity.ActiveStatus

            sqlParams(17) = New SqlParameter()
            sqlParams(17).ParameterName = "@despd_process_month"
            sqlParams(17).DbType = DbType.String
            sqlParams(17).Direction = Data.ParameterDirection.Input
            sqlParams(17).Value = DespEntity.ProcessMonth

            sqlParams(18) = New SqlParameter()
            sqlParams(18).ParameterName = "@despd_transit_till"
            sqlParams(18).DbType = DbType.DateTime
            sqlParams(18).Direction = Data.ParameterDirection.Input
            sqlParams(18).Value = DespEntity.TransitTill

            sqlParams(19) = New SqlParameter()
            sqlParams(19).ParameterName = "@despd_lot_no"
            sqlParams(19).DbType = DbType.String
            sqlParams(19).Direction = Data.ParameterDirection.Input
            sqlParams(19).Value = DespEntity.lot_no


            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Unit_Dspatch_Insert_Dtl]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()



        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region

#Region "Get Update Mode Details"
    Public Function GetUpdateModeDetail(ByVal ChallanNo As String, ByVal Year As String, ByVal unit As String) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@ChallanId"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = ChallanNo


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@ProcessYear"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Year

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = unit


        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Get_Update_Mode_Details]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

#End Region

#Region "Get SKU Details"

    Function GetSKUDetailsForUpdateMode(ByVal challanNo As Integer, ByVal year As String, ByVal active As String, ByVal unit As String, ByVal depot As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@challanNo"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = challanNo

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@processYear"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = year

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@active"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = active

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@unit"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = unit

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@depot"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = depot

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Get_SKU_Details_For_Update_Mode]", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region

#Region "Update Despatch Header"
    Function UpdateDespHeader(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByRef DespEntity As DespatchHeaderEntity) As Integer

        Dim numRowsAffected As Integer
        Try

            Dim sqlParams(17) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@desph_desp_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = DespEntity.DespUnit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@desph_desp_depot"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = DespEntity.DespDepot

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@desph_challan_fin_year"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = DespEntity.ChallanFinYear

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@desph_challan_no"
            sqlParams(3).DbType = DbType.Int64
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = DespEntity.ChallanNo

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@desph_challan_date"
            sqlParams(4).DbType = DbType.DateTime
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = DespEntity.ChallanDate


            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@desph_total_ltr"
            sqlParams(5).DbType = DbType.Decimal
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = DespEntity.TotalLtr

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@desph_total_kg"
            sqlParams(6).DbType = DbType.Decimal
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = DespEntity.TotalKg

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@desph_transporter_name"
            sqlParams(7).DbType = DbType.String
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = DespEntity.TransporterName

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@desph_truck_no"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = DespEntity.TruckNo

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@desph_excise_gp_no"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = DespEntity.ExciseGpNo


            sqlParams(10) = New SqlParameter()
            sqlParams(10).ParameterName = "@desph_excise_gp_dt"
            sqlParams(10).DbType = DbType.DateTime
            sqlParams(10).Direction = Data.ParameterDirection.Input
            sqlParams(10).Value = DespEntity.ExciseGpDt

            sqlParams(11) = New SqlParameter()
            sqlParams(11).ParameterName = "@created_user"
            sqlParams(11).DbType = DbType.String
            sqlParams(11).Direction = Data.ParameterDirection.Input
            sqlParams(11).Value = DespEntity.CreatedUser

            sqlParams(12) = New SqlParameter()
            sqlParams(12).ParameterName = "@active"
            sqlParams(12).DbType = DbType.String
            sqlParams(12).Direction = Data.ParameterDirection.Input
            sqlParams(12).Value = DespEntity.ActiveStatus

            sqlParams(13) = New SqlParameter()
            sqlParams(13).ParameterName = "@desph_process_month"
            sqlParams(13).DbType = DbType.String
            sqlParams(13).Direction = Data.ParameterDirection.Input
            sqlParams(13).Value = DespEntity.ProcessMonth

            sqlParams(14) = New SqlParameter()
            sqlParams(14).ParameterName = "@desph_road_permit_no"
            sqlParams(14).DbType = DbType.String
            sqlParams(14).Direction = Data.ParameterDirection.Input
            sqlParams(14).Value = DespEntity.RoadPermitNo

            sqlParams(15) = New SqlParameter()
            sqlParams(15).ParameterName = "@desph_po_no"
            sqlParams(15).DbType = DbType.String
            sqlParams(15).Direction = Data.ParameterDirection.Input
            sqlParams(15).Value = DespEntity.po_no

            sqlParams(16) = New SqlParameter()
            sqlParams(16).ParameterName = "@site_name"
            sqlParams(16).DbType = DbType.String
            sqlParams(16).Direction = Data.ParameterDirection.Input
            sqlParams(16).Value = DespEntity.site_name

            sqlParams(17) = New SqlParameter()
            sqlParams(17).ParameterName = "@delivery_depot"
            sqlParams(17).DbType = DbType.String
            sqlParams(17).Direction = Data.ParameterDirection.Input
            sqlParams(17).Value = DespEntity.delivery_depot


            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Unit_Dspatch_Update_Hdr]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region

#Region "Delete Despatch Detail"
    Function DeleteDespDtl(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByRef DespEntity As DespatchDetailEntity) As Integer
        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@despd_desp_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = DespEntity.DespUnit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@despd_challan_fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = DespEntity.ChallanFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@despd_challan_no"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = DespEntity.ChallanNo


            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Unit_Dspatch_Delete_Dtl]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region

#Region "Delete Despatch Detail"
    Function DeleteChallan(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByRef DespEntity As DespatchHeaderEntity) As Integer
        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@despd_desp_unit"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = DespEntity.DespUnit

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@despd_challan_fin_year"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = DespEntity.ChallanFinYear

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@despd_challan_no"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = DespEntity.ChallanNo


            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Unit_Dspatch_Delete_All]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region


#Region "Get Unit"
    Public Function GetUnit(ByVal Region As String, ByVal Active As String) As DataSet
        Dim UnitDS As New DataSet
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

        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Get_Unit]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

#End Region

#Region "Get Challan Details For List Screen"
    Public Function GetChallanDetails(ByVal Unit As String, ByVal Depot As String, ByVal Year As String, ByVal Month As String, ByVal ChalanNo As Integer) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@desph_desp_unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = IIf(Unit <> String.Empty, Unit, DBNull.Value)


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@desph_desp_depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = IIf(Depot <> String.Empty, Depot, DBNull.Value)

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@desph_challan_fin_year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Year

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@desph_process_month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = Month

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@desph_challan_no"
        sqlParams(4).DbType = DbType.Int64
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = IIf(ChalanNo <> Integer.MinValue, ChalanNo, DBNull.Value)

        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Get_Challan_Detail]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

#End Region

#Region "Aprove Challan"
    Function AproveChallan(ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction, ByVal challanNo As Integer, ByVal unit As String, ByVal year As String, ByVal month As String, ByVal active As String, ByVal user As String) As Integer

        Dim numRowsAffected As Integer
        Try

            Dim sqlParams(5) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@challanNo"
            sqlParams(0).DbType = DbType.Int64
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = challanNo

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@unit"
            sqlParams(1).DbType = DbType.String
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = unit

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@processYear"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = year

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@processMonth"
            sqlParams(3).DbType = DbType.String
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = month

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@active"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = active


            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@userid"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = user


            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Unit_Dspatch_Aprove_Challan]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region

#Region "Get Screen Details"

    Function CheckApprovalPending(ByVal depot As String, ByVal unit As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = depot


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = unit


        'sqlParams(2) = New SqlParameter()
        'sqlParams(2).ParameterName = "@exist"
        'sqlParams(2).DbType = DbType.String
        'sqlParams(2).Direction = Data.ParameterDirection.Output


        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Unit_Dspatch_Check_Pending_Aprove]", Data.CommandType.StoredProcedure, sqlParams)

        'Return sqlParams(2).Value.ToString
        Return DetailsDS
    End Function
#End Region

#Region "Get Despatch Details for mail"

    Function GetDespatchDetailsForMail(ByVal depot As String, ByVal unit As String, ByVal FinYear As String, ByVal ChallanNo As Integer, ByVal Active As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(4) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = depot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = unit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@FinYear"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = FinYear

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@ChallanNo"
        sqlParams(3).DbType = DbType.Int32
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = ChallanNo

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@Active"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = Active

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("[Despatched_Advice_Details_ForFile_Get]", Data.CommandType.StoredProcedure, sqlParams)

        'Return sqlParams(2).Value.ToString
        Return DetailsDS
    End Function
#End Region

#Region "GetMailID"
    Public Function GetMailIds(ByVal Param_name As String) As DataSet

        Dim ds As New DataSet

        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Param_name"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Param_name

        ds = DBFactory.GetHelper().ExecuteDataSet("VendorDespatchDetails_getMailID", Data.CommandType.StoredProcedure, sqlParams)

        Return ds

    End Function
#End Region

#Region "Get PO No "
    Public Function GetPONo(ByVal Depot As String, ByVal unit As String, ByVal SiteName As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(2) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Depot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = unit

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@SiteName"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = SiteName


        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Unit_Dspatch_PO_No_get", Data.CommandType.StoredProcedure, sqlParams)

        Return PrjectList

    End Function
#End Region

#Region "Get PO No "
    Public Function GetSiteDetailsList(ByVal Depot As String, ByVal unit As String) As DataSet

        Dim PrjectList As DataSet

        Dim sqlParams(1) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@Depot"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Depot

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@unit"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Unit

        PrjectList = DBFactory.GetHelper().ExecuteDataSet("Unit_Dspatch_site_details_get", Data.CommandType.StoredProcedure, sqlParams)

        Return PrjectList

    End Function
#End Region

#Region "Get SKU Despatch Quantity Details"

    Function GetSKUDespatchQuantityDetails(ByVal challanNo As Integer, ByVal year As String, ByVal SkuCode As String, ByVal PONo As String) As DataSet

        Dim DetailsDS As System.Data.DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@challanNo"
        sqlParams(0).DbType = DbType.Int64
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = challanNo

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@processYear"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = year

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@SkuCode"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = SkuCode

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@PONo"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = PONo

        DetailsDS = DBFactory.GetHelper().ExecuteDataSet("Unit_Dspatch_getSKUDespatchQuentityDetails", Data.CommandType.StoredProcedure, sqlParams)

        Return DetailsDS
    End Function
#End Region

#Region "Delete Despatch Quantity Detail"
    Function DeleteDespQuantityDtl(ByVal FinYear As String, ByVal ChallanNo As Int64, ByVal SKUCode As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer
        Try
            Dim sqlParams(2) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@FinYear"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = FinYear

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@ChallanNo"
            sqlParams(1).DbType = DbType.Int64
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = ChallanNo

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@SKUCode"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = SKUCode

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Unit_Dspatch_SKUDespatchQuentity_delete"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region

#Region "Insert Despatch Quantity Details"
    Function InsertDespQuantityDetails(ByVal FinYear As String, ByVal ChallanNo As Int64, ByVal PONo As String, ByVal DespDate As Date, ByVal Unit As String, ByVal Depot As String, ByVal SKU As String, ByVal Quantity As Decimal, ByVal User As String, ByVal Prefix As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer

        Try
            Dim sqlParams(9) As SqlParameter

            sqlParams(0) = New SqlParameter()
            sqlParams(0).ParameterName = "@FinYear"
            sqlParams(0).DbType = DbType.String
            sqlParams(0).Direction = Data.ParameterDirection.Input
            sqlParams(0).Value = FinYear

            sqlParams(1) = New SqlParameter()
            sqlParams(1).ParameterName = "@ChallanNo"
            sqlParams(1).DbType = DbType.Int64
            sqlParams(1).Direction = Data.ParameterDirection.Input
            sqlParams(1).Value = ChallanNo

            sqlParams(2) = New SqlParameter()
            sqlParams(2).ParameterName = "@PONo"
            sqlParams(2).DbType = DbType.String
            sqlParams(2).Direction = Data.ParameterDirection.Input
            sqlParams(2).Value = PONo

            sqlParams(3) = New SqlParameter()
            sqlParams(3).ParameterName = "@DespDate"
            sqlParams(3).DbType = DbType.Date
            sqlParams(3).Direction = Data.ParameterDirection.Input
            sqlParams(3).Value = DespDate

            sqlParams(4) = New SqlParameter()
            sqlParams(4).ParameterName = "@Unit"
            sqlParams(4).DbType = DbType.String
            sqlParams(4).Direction = Data.ParameterDirection.Input
            sqlParams(4).Value = Unit

            sqlParams(5) = New SqlParameter()
            sqlParams(5).ParameterName = "@Depot"
            sqlParams(5).DbType = DbType.String
            sqlParams(5).Direction = Data.ParameterDirection.Input
            sqlParams(5).Value = Depot

            sqlParams(6) = New SqlParameter()
            sqlParams(6).ParameterName = "@SKU"
            sqlParams(6).DbType = DbType.String
            sqlParams(6).Direction = Data.ParameterDirection.Input
            sqlParams(6).Value = SKU

            sqlParams(7) = New SqlParameter()
            sqlParams(7).ParameterName = "@Quantity"
            sqlParams(7).DbType = DbType.Decimal
            sqlParams(7).Direction = Data.ParameterDirection.Input
            sqlParams(7).Value = Quantity

            sqlParams(8) = New SqlParameter()
            sqlParams(8).ParameterName = "@User"
            sqlParams(8).DbType = DbType.String
            sqlParams(8).Direction = Data.ParameterDirection.Input
            sqlParams(8).Value = User

            sqlParams(9) = New SqlParameter()
            sqlParams(9).ParameterName = "@Prefix"
            sqlParams(9).DbType = DbType.String
            sqlParams(9).Direction = Data.ParameterDirection.Input
            sqlParams(9).Value = Prefix

            'sqlCmd is the object instance of the SqlCommand 
            Dim sqlCmd As New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.Transaction = sqlTrans
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[Unit_Dspatch_insert_Desp_Quantity_Dtls]"
            sqlCmd.Parameters.AddRange(sqlParams)
            numRowsAffected = sqlCmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        End Try

        Return numRowsAffected

    End Function
#End Region


#Region "Get Challan Details For Challan Cancellation List Screen"
    Public Function GetChallanDetailsForCnacellation(ByVal Unit As String, ByVal Depot As String, ByVal Year As String, ByVal Month As String) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(3) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@unit"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = Unit


        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = Depot

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@year"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = Year

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@month"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = Month


        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[get_challan_cancellation_list]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function

#End Region



    Public Function CancelChallanENtry(ByVal challanId As Integer, ByVal unit As String, ByVal depot As String, ByVal userId As String, ByVal year As String, ByVal month As String, ByVal sqlConn As SqlConnection, ByVal sqlTrans As SqlTransaction) As Integer
        Dim numRowsAffected As Integer = 0

        Dim sqlParams(6) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@challanId"
        sqlParams(0).DbType = DbType.Int32
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = challanId

        sqlParams(1) = New SqlParameter()
        sqlParams(1).ParameterName = "@depot"
        sqlParams(1).DbType = DbType.String
        sqlParams(1).Direction = Data.ParameterDirection.Input
        sqlParams(1).Value = depot

        sqlParams(2) = New SqlParameter()
        sqlParams(2).ParameterName = "@unit"
        sqlParams(2).DbType = DbType.String
        sqlParams(2).Direction = Data.ParameterDirection.Input
        sqlParams(2).Value = unit

        sqlParams(3) = New SqlParameter()
        sqlParams(3).ParameterName = "@userId"
        sqlParams(3).DbType = DbType.String
        sqlParams(3).Direction = Data.ParameterDirection.Input
        sqlParams(3).Value = userId

        sqlParams(4) = New SqlParameter()
        sqlParams(4).ParameterName = "@process_year"
        sqlParams(4).DbType = DbType.String
        sqlParams(4).Direction = Data.ParameterDirection.Input
        sqlParams(4).Value = year

        sqlParams(5) = New SqlParameter()
        sqlParams(5).ParameterName = "@process_month"
        sqlParams(5).DbType = DbType.String
        sqlParams(5).Direction = Data.ParameterDirection.Input
        sqlParams(5).Value = month

        sqlParams(6) = New SqlParameter()
        sqlParams(6).ParameterName = "@output"
        sqlParams(6).DbType = DbType.Int32
        sqlParams(6).Direction = Data.ParameterDirection.Output


        Dim sqlCmd As New SqlCommand()
        sqlCmd.Connection = sqlConn
        sqlCmd.Transaction = sqlTrans
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "Cancel_Challan_Entry"
        sqlCmd.Parameters.AddRange(sqlParams)

        Dim output As Integer = 0
        output = sqlCmd.ExecuteNonQuery()
        numRowsAffected = If(Integer.TryParse(Convert.ToString(sqlParams(6).Value), output), Convert.ToInt32(Convert.ToString(sqlParams(6).Value)), 0)

        Return numRowsAffected
    End Function

    Public Function GetTranspoterList(ByVal searchKeyword As String) As DataSet
        Dim UnitDS As New DataSet
        Dim sqlParams(0) As SqlParameter

        sqlParams(0) = New SqlParameter()
        sqlParams(0).ParameterName = "@searchkeyword"
        sqlParams(0).DbType = DbType.String
        sqlParams(0).Direction = Data.ParameterDirection.Input
        sqlParams(0).Value = searchKeyword

        UnitDS = DBFactory.GetHelper().ExecuteDataSet("[unit_despatch_transpoter_details_get]", Data.CommandType.StoredProcedure, sqlParams)
        Return UnitDS
    End Function



End Class
