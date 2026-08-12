USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[opc_get_rawmaterial_requisition_list]
    @vendor_code           VARCHAR(50) = NULL,
    @rawmat_vendor_code    VARCHAR(50) = NULL,
    @approval_status       CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        orh.orh_Id                                               AS request_id,
        orh.orh_vendor_code                                      AS vendor_code,
        LTRIM(RTRIM(ISNULL(up.usp_first_name, '') + ' ' + ISNULL(up.usp_last_name, ''))) AS vendor_name,
        orh.orh_rawmaterial_vender_code                          AS rawmat_vendor_code,
        ISNULL(rvm.opc_vendor_name, '')                          AS rawmat_vendor_name,
        CASE
            WHEN ISNULL(orh.orh_approved_status, 'P') = 'P' THEN 'Pending'
            ELSE 'Approved'
        END                                                      AS approval_status
    FROM dbo.opc_request_hdr orh WITH (NOLOCK)
    LEFT JOIN dbo.user_profile up WITH (NOLOCK)
        ON up.usp_user_id = orh.orh_vendor_code
    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rvm WITH (NOLOCK)
        ON rvm.opc_vendor_code = orh.orh_rawmaterial_vender_code
    WHERE ISNULL(orh.active, 'Y') = 'Y'
      AND orh.deleted_date IS NULL
      AND (@vendor_code IS NULL OR @vendor_code = '' OR orh.orh_vendor_code = @vendor_code)
      AND (@rawmat_vendor_code IS NULL OR @rawmat_vendor_code = '' OR orh.orh_rawmaterial_vender_code = @rawmat_vendor_code)
      AND (@approval_status IS NULL OR @approval_status = '' OR ISNULL(orh.orh_approved_status, 'P') = @approval_status)
    ORDER BY orh.orh_Id DESC;
END
GO

/*
    If RM vendor table uses rvm_* columns only, replace join/select with:

    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rvm
        ON rvm.rvm_vendor_code = orh.orh_rawmaterial_vender_code
    ...
    rvm.rvm_vendor_name AS rawmat_vendor_name
*/
