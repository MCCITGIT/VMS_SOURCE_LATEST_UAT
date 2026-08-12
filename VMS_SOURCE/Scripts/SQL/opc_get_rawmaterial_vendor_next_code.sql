USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      Swatilekha
-- Create date: 10/08/2026
-- Description: Get next auto-generated raw material vendor code (RM001, RM002, ...)
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[opc_get_rawmaterial_vendor_next_code]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @nextNumber INT;

    SELECT @nextNumber = ISNULL(MAX(CASE
        WHEN opc_vendor_code LIKE 'RM%'
         AND ISNUMERIC(SUBSTRING(opc_vendor_code, 3, LEN(opc_vendor_code))) = 1
        THEN CAST(SUBSTRING(opc_vendor_code, 3, LEN(opc_vendor_code)) AS INT)
        ELSE NULL
    END), 0) + 1
    FROM [dbo].[opc_rawmaterial_vendor_mstr] WITH (NOLOCK);

    SELECT 'RM' + RIGHT('000' + CAST(@nextNumber AS VARCHAR(10)), 3) AS vendor_code;
END
GO
