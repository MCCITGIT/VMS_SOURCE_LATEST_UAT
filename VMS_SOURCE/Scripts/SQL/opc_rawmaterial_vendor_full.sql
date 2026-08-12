USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ============================================================
   TABLE: opc_rawmaterial_vendor_mstr
   ============================================================ */
IF OBJECT_ID(N'dbo.opc_rawmaterial_vendor_mstr', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[opc_rawmaterial_vendor_mstr]
    (
        [opc_vendor_code]           VARCHAR(10)  NOT NULL,
        [opc_vendor_name]           VARCHAR(100) NULL,
        [gst_registration_number]   VARCHAR(30)  NULL,
        [address]                   VARCHAR(200) NULL,
        [contact_person]            VARCHAR(50)  NULL,
        [mobile_number]             VARCHAR(20)  NULL,
        [email_address]             VARCHAR(100) NULL,
        [created_user]              VARCHAR(20)  NULL,
        [created_date]              DATETIME     NULL,
        [modified_user]             VARCHAR(20)  NULL,
        [modified_date]             DATETIME     NULL,
        [active]                    CHAR(1)      NULL,
        CONSTRAINT [PK_opc_rawmaterial_vendor_mstr] PRIMARY KEY CLUSTERED ([opc_vendor_code] ASC)
    );
END
GO

/* ============================================================
   SP: opc_rawmaterial_vendor_insertupdate
   @trantype = 1 -> Insert
   @trantype = 2 -> Update
   @outputCode = 1 -> Success
   @outputCode = 2 -> Duplicate vendor code (insert)
   @outputCode = 0 -> Failed / record not found
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_rawmaterial_vendor_insertupdate]
     @vendor_code              VARCHAR(10)
    ,@vendor_name              VARCHAR(100)
    ,@gst_registration_number  VARCHAR(30)
    ,@address                  VARCHAR(200)
    ,@contact_person           VARCHAR(50)
    ,@mobile_number            VARCHAR(20)
    ,@email_address            VARCHAR(100)
    ,@active                   CHAR(1)
    ,@user_id                  VARCHAR(20)
    ,@trantype                 INT
    ,@outputCode               BIGINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @outputCode = 0;

    BEGIN TRY
        IF @trantype = 1
        BEGIN
            IF EXISTS
            (
                SELECT 1
                FROM dbo.opc_rawmaterial_vendor_mstr WITH (NOLOCK)
                WHERE opc_vendor_code = @vendor_code
            )
            BEGIN
                SET @outputCode = 2;
                RETURN;
            END

            INSERT INTO dbo.opc_rawmaterial_vendor_mstr
            (
                opc_vendor_code,
                opc_vendor_name,
                gst_registration_number,
                address,
                contact_person,
                mobile_number,
                email_address,
                created_user,
                created_date,
                active
            )
            VALUES
            (
                @vendor_code,
                @vendor_name,
                @gst_registration_number,
                @address,
                @contact_person,
                @mobile_number,
                @email_address,
                @user_id,
                GETDATE(),
                ISNULL(@active, 'Y')
            );

            SET @outputCode = 1;
        END
        ELSE IF @trantype = 2
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1
                FROM dbo.opc_rawmaterial_vendor_mstr WITH (NOLOCK)
                WHERE opc_vendor_code = @vendor_code
            )
            BEGIN
                SET @outputCode = 0;
                RETURN;
            END

            UPDATE dbo.opc_rawmaterial_vendor_mstr
            SET
                opc_vendor_name = @vendor_name,
                gst_registration_number = @gst_registration_number,
                address = @address,
                contact_person = @contact_person,
                mobile_number = @mobile_number,
                email_address = @email_address,
                active = ISNULL(@active, 'Y'),
                modified_user = @user_id,
                modified_date = GETDATE()
            WHERE opc_vendor_code = @vendor_code;

            SET @outputCode = 1;
        END
    END TRY
    BEGIN CATCH
        SET @outputCode = 0;
        THROW;
    END CATCH
END
GO

/* ============================================================
   SP: opc_get_rawmaterial_vendor_next_code
   Returns next auto-generated vendor code (RM001, RM002, ...)
   ============================================================ */
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
    FROM dbo.opc_rawmaterial_vendor_mstr WITH (NOLOCK);

    SELECT 'RM' + RIGHT('000' + CAST(@nextNumber AS VARCHAR(10)), 3) AS vendor_code;
END
GO

/* ============================================================
   SP: opc_get_rawmaterial_vendor_list
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_get_rawmaterial_vendor_list]
    @vendor_name VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        opc_vendor_code           AS vendor_code,
        opc_vendor_name           AS vendor_name,
        gst_registration_number,
        address,
        contact_person,
        mobile_number,
        email_address,
        ISNULL(active, 'N')       AS active
    FROM dbo.opc_rawmaterial_vendor_mstr WITH (NOLOCK)
    WHERE (@vendor_name IS NULL OR @vendor_name = '' OR opc_vendor_name LIKE '%' + @vendor_name + '%')
    ORDER BY opc_vendor_code;
END
GO

/* ============================================================
   SP: opc_get_rawmaterial_vendor_edit
   ============================================================ */
CREATE OR ALTER PROCEDURE [dbo].[opc_get_rawmaterial_vendor_edit]
    @vendor_code VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        opc_vendor_code           AS vendor_code,
        opc_vendor_name           AS vendor_name,
        gst_registration_number,
        address,
        contact_person,
        mobile_number,
        email_address,
        ISNULL(active, 'N')       AS active
    FROM dbo.opc_rawmaterial_vendor_mstr WITH (NOLOCK)
    WHERE opc_vendor_code = @vendor_code;
END
GO
