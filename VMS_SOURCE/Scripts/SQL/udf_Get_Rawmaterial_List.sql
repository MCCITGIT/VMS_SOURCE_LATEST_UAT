USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.udf_Get_Rawmaterial_List', N'FN') IS NOT NULL
    DROP FUNCTION dbo.udf_Get_Rawmaterial_List
GO

CREATE FUNCTION [dbo].[udf_Get_Rawmaterial_List]
(
    @request_id INT
)
RETURNS VARCHAR(MAX)
AS
BEGIN
    DECLARE @p VARCHAR(MAX) = ''

    SELECT @p +=
           ord.ord_rawmaterial_code
           + '-' + ISNULL(rmm.rmm_material_name, ord.ord_rawmaterial_code)
           + '-' + CONVERT(VARCHAR(20), ord.ord_qty)
           + ','
    FROM dbo.opc_request_dtls ord WITH (NOLOCK)
    LEFT JOIN dbo.raw_material_mstr rmm WITH (NOLOCK)
        ON rmm.rmm_code = ord.ord_rawmaterial_code
    WHERE ord.ord_orh_id = @request_id
      AND ISNULL(ord.active, 'Y') = 'Y'
      AND ord.deleted_date IS NULL

    IF LEN(@p) > 0
        SET @p = LEFT(@p, LEN(@p) - 1)

    RETURN @p
END
GO

/*
    If raw_material_mstr exists only in lmis_db, replace join with:

    LEFT JOIN [lmis_db].[dbo].[raw_material_mstr] rmm WITH (NOLOCK)
        ON rmm.rmm_code = ord.ord_rawmaterial_code
*/