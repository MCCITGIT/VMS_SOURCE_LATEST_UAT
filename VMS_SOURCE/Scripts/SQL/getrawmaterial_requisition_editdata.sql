USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[getrawmaterial_requisition_editdata]
    @requestid INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        orh.orh_Id                                               AS request_id,
        orh.orh_vendor_code                                      AS vendor_code,
        LTRIM(RTRIM(ISNULL(up.usp_first_name, '') + ' ' + ISNULL(up.usp_last_name, ''))) AS vendor_name,
        orh.orh_rawmaterial_vender_code                          AS rawmat_vendor_code,
        ISNULL(rvm.opc_vendor_name, '')                          AS rawmat_vendor_name,
        ord.ord_rawmaterial_code                                  AS rawmaterial_code,
        ISNULL(rmm.rmm_material_name, ord.ord_rawmaterial_code)  AS rawmaterial_name,
        ord.ord_qty                                               AS quantity,
        ord.ord_req_delivery_date                                 AS delivery_date,
        ord.ord_remark                                            AS remarks,
        ord.ord_rate                                              AS rate
    FROM dbo.opc_request_hdr orh WITH (NOLOCK)
    INNER JOIN dbo.opc_request_dtls ord WITH (NOLOCK)
        ON ord.ord_orh_id = orh.orh_Id
    LEFT JOIN dbo.user_profile up WITH (NOLOCK)
        ON up.usp_user_id = orh.orh_vendor_code
    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rvm WITH (NOLOCK)
        ON rvm.opc_vendor_code = orh.orh_rawmaterial_vender_code
    LEFT JOIN dbo.raw_material_mstr rmm WITH (NOLOCK)
        ON rmm.rmm_code = ord.ord_rawmaterial_code
    WHERE orh.orh_Id = @requestid
      AND ISNULL(orh.active, 'Y') = 'Y'
      AND orh.deleted_date IS NULL
      AND ISNULL(ord.active, 'Y') = 'Y'
      AND ord.deleted_date IS NULL
    ORDER BY ord.ord_id;
END
GO

/*
    If RM vendor table uses rvm_vendor_name / rvm_vendor_code, replace join with:

    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rvm
        ON rvm.rvm_vendor_code = orh.orh_rawmaterial_vender_code
    ...
    rvm.rvm_vendor_name AS rawmat_vendor_name
*/
