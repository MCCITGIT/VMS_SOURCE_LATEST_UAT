USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ============================================================
   TABLE: opc_request_hdr
   ============================================================ */
IF OBJECT_ID(N'dbo.opc_request_hdr', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[opc_request_hdr]
    (
        [orh_Id]                        INT IDENTITY(1,1) NOT NULL,
        [orh_vendor_code]               VARCHAR(50)       NOT NULL,
        [orh_rawmaterial_vender_code]     VARCHAR(50)       NULL,
        [created_user]                    VARCHAR(50)       NULL,
        [created_date]                    DATETIME          NULL,
        [modifield_user]                  VARCHAR(50)       NULL,
        [modifield_date]                  DATETIME          NULL,
        [deleted_user]                    VARCHAR(50)       NULL,
        [deleted_date]                    DATETIME          NULL,
        [active]                          CHAR(1)           NULL,
        CONSTRAINT [PK_opc_request_hdr] PRIMARY KEY CLUSTERED ([orh_Id] ASC)
    );
END
GO

/* ============================================================
   TABLE: opc_request_dtls
   ============================================================ */
IF OBJECT_ID(N'dbo.opc_request_dtls', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[opc_request_dtls]
    (
        [ord_id]                        INT IDENTITY(1,1) NOT NULL,
        [ord_orh_id]                    INT               NOT NULL,
        [ord_rawmaterial_code]          VARCHAR(50)       NOT NULL,
        [ord_qty]                         DECIMAL(18, 2)    NOT NULL,
        [ord_req_delivery_date]         DATETIME          NULL,
        [ord_remark]                      VARCHAR(500)      NULL,
        [ord_rate]                        DECIMAL(18, 2)    NULL,
        [created_user]                    VARCHAR(50)       NULL,
        [created_date]                    DATETIME          NULL,
        [modifield_user]                  VARCHAR(50)       NULL,
        [modifield_date]                  DATETIME          NULL,
        [deleted_user]                    VARCHAR(50)       NULL,
        [deleted_date]                    DATETIME          NULL,
        [active]                          CHAR(1)           NULL,
        CONSTRAINT [PK_opc_request_dtls] PRIMARY KEY CLUSTERED ([ord_id] ASC)
    );
END
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'FK_opc_request_dtls_hdr'
)
BEGIN
    ALTER TABLE [dbo].[opc_request_dtls] WITH CHECK
    ADD CONSTRAINT [FK_opc_request_dtls_hdr]
        FOREIGN KEY ([ord_orh_id]) REFERENCES [dbo].[opc_request_hdr] ([orh_Id]);

    ALTER TABLE [dbo].[opc_request_dtls] CHECK CONSTRAINT [FK_opc_request_dtls_hdr];
END
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_opc_request_dtls_orh_id'
      AND object_id = OBJECT_ID(N'dbo.opc_request_dtls')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_opc_request_dtls_orh_id]
        ON [dbo].[opc_request_dtls] ([ord_orh_id]);
END
GO

/* ============================================================
   TYPE: tbl_opc_request_dtls
   ============================================================ */
IF NOT EXISTS
(
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

/* ============================================================
   SP: opc_request_insert
   @outputCode = 1 -> Success
   @outputCode = 0 -> Failed / validation error
   ============================================================ */
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
        OR ISNULL(LTRIM(RTRIM(@rawmaterial_vendor_code)), '') = ''
    BEGIN
        RETURN;
    END

    IF NOT EXISTS
    (
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

/* ============================================================
   SP: opc_request_update
   @outputCode = 1 -> Success
   @outputCode = 0 -> Failed / record not found
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_request_update]
     @request_id               INT
    ,@vendor_code              VARCHAR(50)
    ,@rawmaterial_vendor_code  VARCHAR(50)
    ,@user_id                  VARCHAR(50)
    ,@active                   CHAR(1) = 'Y'
    ,@tbl                      [dbo].[tbl_opc_request_dtls] READONLY
    ,@outputCode               BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @outputCode = 0;

    IF ISNULL(@request_id, 0) = 0
        OR ISNULL(LTRIM(RTRIM(@vendor_code)), '') = ''
        OR ISNULL(LTRIM(RTRIM(@rawmaterial_vendor_code)), '') = ''
    BEGIN
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.opc_request_hdr WITH (NOLOCK)
        WHERE orh_Id = @request_id
          AND deleted_date IS NULL
          AND ISNULL(active, 'Y') = 'Y'
    )
    BEGIN
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM @tbl
        WHERE ISNULL([qty], 0) > 0
    )
    BEGIN
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.opc_request_hdr
        SET
            orh_vendor_code = LTRIM(RTRIM(@vendor_code)),
            orh_rawmaterial_vender_code = LTRIM(RTRIM(@rawmaterial_vendor_code)),
            modifield_user = @user_id,
            modifield_date = GETDATE(),
            active = ISNULL(@active, 'Y')
        WHERE orh_Id = @request_id;

        DELETE FROM dbo.opc_request_dtls
        WHERE ord_orh_id = @request_id;

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
            SET @outputCode = 0;
            RETURN;
        END

        COMMIT TRANSACTION;
        SET @outputCode = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @outputCode = 0;
        THROW;
    END CATCH
END
GO

/* ============================================================
   SP: opc_request_delete
   Soft delete header and details
   @outputCode = 1 -> Success
   @outputCode = 0 -> Failed / record not found
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_request_delete]
     @request_id   INT
    ,@user_id      VARCHAR(50)
    ,@outputCode   BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @outputCode = 0;

    IF ISNULL(@request_id, 0) = 0
    BEGIN
        RETURN;
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.opc_request_hdr WITH (NOLOCK)
        WHERE orh_Id = @request_id
          AND deleted_date IS NULL
    )
    BEGIN
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.opc_request_hdr
        SET
            active = 'N',
            deleted_user = @user_id,
            deleted_date = GETDATE()
        WHERE orh_Id = @request_id;

        UPDATE dbo.opc_request_dtls
        SET
            active = 'N',
            deleted_user = @user_id,
            deleted_date = GETDATE()
        WHERE ord_orh_id = @request_id;

        COMMIT TRANSACTION;
        SET @outputCode = 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @outputCode = 0;
        THROW;
    END CATCH
END
GO

/* ============================================================
   SP: opc_get_request_list
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_get_request_list]
     @vendor_code              VARCHAR(50) = NULL
    ,@rawmaterial_vendor_code  VARCHAR(50) = NULL
    ,@from_date                DATETIME = NULL
    ,@to_date                  DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        h.orh_Id                              AS request_id,
        h.orh_vendor_code                     AS vendor_code,
        h.orh_rawmaterial_vender_code         AS rawmaterial_vendor_code,
        rv.opc_vendor_name                    AS rawmaterial_vendor_name,
        h.created_user,
        h.created_date,
        ISNULL(h.active, 'N')                 AS active,
        ISNULL(d.item_count, 0)               AS item_count,
        ISNULL(d.total_qty, 0)                AS total_qty
    FROM dbo.opc_request_hdr h WITH (NOLOCK)
    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rv WITH (NOLOCK)
        ON rv.opc_vendor_code = h.orh_rawmaterial_vender_code
    OUTER APPLY
    (
        SELECT
            COUNT(1) AS item_count,
            SUM(dt.ord_qty) AS total_qty
        FROM dbo.opc_request_dtls dt WITH (NOLOCK)
        WHERE dt.ord_orh_id = h.orh_Id
          AND ISNULL(dt.active, 'Y') = 'Y'
          AND dt.deleted_date IS NULL
    ) d
    WHERE h.deleted_date IS NULL
      AND ISNULL(h.active, 'Y') = 'Y'
      AND (@vendor_code IS NULL OR @vendor_code = '' OR h.orh_vendor_code = @vendor_code)
      AND (@rawmaterial_vendor_code IS NULL OR @rawmaterial_vendor_code = '' OR h.orh_rawmaterial_vender_code = @rawmaterial_vendor_code)
      AND (@from_date IS NULL OR h.created_date >= @from_date)
      AND (@to_date IS NULL OR h.created_date < DATEADD(DAY, 1, CAST(@to_date AS DATE)))
    ORDER BY h.orh_Id DESC;
END
GO

/* ============================================================
   SP: opc_get_request_hdr
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_get_request_hdr]
    @request_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        h.orh_Id                              AS request_id,
        h.orh_vendor_code                     AS vendor_code,
        h.orh_rawmaterial_vender_code         AS rawmaterial_vendor_code,
        rv.opc_vendor_name                    AS rawmaterial_vendor_name,
        h.created_user,
        h.created_date,
        h.modifield_user,
        h.modifield_date,
        ISNULL(h.active, 'N')                 AS active
    FROM dbo.opc_request_hdr h WITH (NOLOCK)
    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rv WITH (NOLOCK)
        ON rv.opc_vendor_code = h.orh_rawmaterial_vender_code
    WHERE h.orh_Id = @request_id
      AND h.deleted_date IS NULL;
END
GO

/* ============================================================
   SP: opc_get_request_dtls
   Returns grid-friendly columns for RawMaterialRequisitionDtls.aspx
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_get_request_dtls]
    @request_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ISNULL(vr.opc_lin_id, 0)              AS id,
        h.orh_rawmaterial_vender_code         AS vendor_code,
        rv.opc_vendor_name                    AS vendor_name,
        d.ord_rawmaterial_code                AS rawmat_code,
        ISNULL(rm.rmm_material_name, d.ord_rawmaterial_code) AS rawmat_name,
        d.ord_qty                             AS qty,
        d.ord_req_delivery_date               AS req_date,
        d.ord_remark                          AS remark,
        ISNULL(d.ord_rate, vr.opc_rate)       AS rate,
        ISNULL(d.active, 'Y')                 AS active
    FROM dbo.opc_request_dtls d WITH (NOLOCK)
    INNER JOIN dbo.opc_request_hdr h WITH (NOLOCK)
        ON h.orh_Id = d.ord_orh_id
    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rv WITH (NOLOCK)
        ON rv.opc_vendor_code = h.orh_rawmaterial_vender_code
    LEFT JOIN dbo.opc_vendor_rawmat_linking vr WITH (NOLOCK)
        ON vr.opc_vendor_id = h.orh_rawmaterial_vender_code
       AND vr.opc_rawmat_code = d.ord_rawmaterial_code
    LEFT JOIN dbo.raw_material_mstr rm WITH (NOLOCK)
        ON rm.rmm_code = d.ord_rawmaterial_code
    WHERE d.ord_orh_id = @request_id
      AND d.deleted_date IS NULL
      AND ISNULL(d.active, 'Y') = 'Y'
    ORDER BY d.ord_id;
END
GO

/* ============================================================
   SP: opc_get_request_dtls_by_vendor
   Loads linked raw materials for new requisition entry
   (same shape as getvendor_rawmateriallink_editdata)
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_get_request_dtls_by_vendor]
    @vendor_id VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        vr.opc_lin_id                         AS id,
        vr.opc_vendor_id                      AS vendor_code,
        rv.opc_vendor_name                    AS vendor_name,
        vr.opc_rawmat_code                    AS rawmat_code,
        ISNULL(rm.rmm_material_name, vr.opc_rawmat_code) AS rawmat_name,
        vr.opc_rate                           AS rate,
        ISNULL(vr.active, 'N')                AS active
    FROM dbo.opc_vendor_rawmat_linking vr WITH (NOLOCK)
    LEFT JOIN dbo.opc_rawmaterial_vendor_mstr rv WITH (NOLOCK)
        ON rv.opc_vendor_code = vr.opc_vendor_id
    LEFT JOIN dbo.raw_material_mstr rm WITH (NOLOCK)
        ON rm.rmm_code = vr.opc_rawmat_code
    WHERE vr.opc_vendor_id = @vendor_id
      AND ISNULL(vr.active, 'Y') = 'Y'
    ORDER BY vr.opc_rawmat_code;
END
GO
