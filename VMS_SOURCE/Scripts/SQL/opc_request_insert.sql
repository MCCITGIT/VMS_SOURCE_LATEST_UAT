USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    Tables:
    dbo.opc_request_hdr
    dbo.opc_request_dtls

    Detail TVP maps to grid columns in RawMaterialRequisitionDtls.aspx:
      rawmaterial_code   -> Raw Material Code
      qty                -> Quantity
      req_delivery_date  -> Date
      remark             -> Remarks
      rate               -> Rate from vendor raw material link
*/

IF NOT EXISTS (
    SELECT 1
    FROM sys.types t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.is_table_type = 1
      AND t.name = 'tbl_opc_request_dtls'
      AND s.name = 'dbo'
)
BEGIN
    CREATE TYPE [dbo].[tbl_opc_request_dtls] AS TABLE
    (
        [rawmaterial_code]    VARCHAR(50)    NOT NULL,
        [qty]                 DECIMAL(18, 2) NOT NULL,
        [req_delivery_date]   DATETIME       NULL,
        [remark]              VARCHAR(500)   NULL,
        [rate]                DECIMAL(18, 2) NULL
    );
END
GO

-- =============================================
-- Author:      Swatilekha
-- Create date: 11/08/2026
-- Description: Insert raw material requisition header and details
--   @outputCode = 1 -> Success
--   @outputCode = 0 -> Failed / validation error
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[opc_request_insert]
     @vendor_code              VARCHAR(50)
    ,@rawmaterial_vendor_code  VARCHAR(50)
    ,@user_id                  VARCHAR(50)
    ,@active                   CHAR(1) = 'Y'
    ,@tbl                      [dbo].[tbl_opc_request_dtls] READONLY
    ,@request_id               INT OUTPUT
    ,@outputCode               BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @outputCode = 0;
    SET @request_id = 0;

    IF ISNULL(LTRIM(RTRIM(@vendor_code)), '') = ''
    BEGIN
        RETURN;
    END

    IF ISNULL(LTRIM(RTRIM(@rawmaterial_vendor_code)), '') = ''
    BEGIN
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1
        FROM @tbl
        WHERE ISNULL([qty], 0) > 0
    )
    BEGIN
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.opc_request_hdr
        (
            orh_vendor_code,
            orh_rawmaterial_vender_code,
            created_user,
            created_date,
            active
        )
        VALUES
        (
            LTRIM(RTRIM(@vendor_code)),
            LTRIM(RTRIM(@rawmaterial_vendor_code)),
            @user_id,
            GETDATE(),
            ISNULL(@active, 'Y')
        );

        SET @request_id = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO dbo.opc_request_dtls
        (
            ord_orh_id,
            ord_rawmaterial_code,
            ord_qty,
            ord_req_delivery_date,
            ord_remark,
            ord_rate,
            created_user,
            created_date,
            active
        )
        SELECT
            @request_id,
            LTRIM(RTRIM(t.rawmaterial_code)),
            t.qty,
            t.req_delivery_date,
            t.remark,
            t.rate,
            @user_id,
            GETDATE(),
            ISNULL(@active, 'Y')
        FROM @tbl t
        WHERE ISNULL(t.qty, 0) > 0;

        IF @@ROWCOUNT = 0
        BEGIN
            ROLLBACK TRANSACTION;
            SET @request_id = 0;
            SET @outputCode = 0;
            RETURN;
        END

        COMMIT TRANSACTION;
        SET @outputCode = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @request_id = 0;
        SET @outputCode = 0;
        THROW;
    END CATCH
END
GO
