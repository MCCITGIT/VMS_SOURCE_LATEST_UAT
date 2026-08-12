USE [VMS_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    Rate fix:
    1. Join RM vendor link on @rawmaterial_vender_code (NOT @vendor_code).
    2. Use COALESCE(T.rate, link rate) so TVP/grid rate is saved.
*/
CREATE OR ALTER PROCEDURE [dbo].[opc_rawmaterial_request_insert_update]
(
    @orh_Id                     INT,
    @vendor_code                VARCHAR(50),
    @rawmaterial_vender_code    VARCHAR(50) = NULL,
    @user_id                    VARCHAR(50),
    @tran_type                  INT,
    @tbl_opc_request_dtls       [dbo].[tbl_opc_request_dtls] READONLY,
    @outputCode                 INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET @outputCode = 0;

    DECLARE @NewOrh_Id INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @tran_type = 1
        BEGIN
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
                @vendor_code,
                @rawmaterial_vender_code,
                @user_id,
                GETDATE(),
                'Y'
            );

            SET @NewOrh_Id = SCOPE_IDENTITY();

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
                @NewOrh_Id,
                T.rawmaterial_code,
                T.qty,
                T.req_delivery_date,
                T.remark,
                COALESCE(NULLIF(T.rate, 0), V.vrl_rate, 0),
                @user_id,
                GETDATE(),
                'Y'
            FROM @tbl_opc_request_dtls T
            LEFT JOIN dbo.opc_vendor_rawmat_linking V WITH (NOLOCK)
                ON V.vrl_vendor_id = @rawmaterial_vender_code
               AND V.vrl_rawmat_code = T.rawmaterial_code
               AND ISNULL(V.active, 'Y') = 'Y'
            WHERE ISNULL(T.qty, 0) > 0;

            IF @@ROWCOUNT = 0
            BEGIN
                ROLLBACK TRANSACTION;
                RETURN;
            END

            SET @outputCode = @NewOrh_Id;
        END
        ELSE IF @tran_type = 2
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1
                FROM dbo.opc_request_hdr WITH (NOLOCK)
                WHERE orh_Id = @orh_Id
                  AND ISNULL(active, 'Y') = 'Y'
            )
            BEGIN
                RAISERROR('Request header does not exist or is inactive.', 16, 1);
                ROLLBACK TRANSACTION;
                RETURN;
            END

            UPDATE dbo.opc_request_hdr
            SET
                orh_vendor_code = @vendor_code,
                orh_rawmaterial_vender_code = @rawmaterial_vender_code,
                modifield_user = @user_id,
                modifield_date = GETDATE()
            WHERE orh_Id = @orh_Id;

            DELETE FROM dbo.opc_request_dtls
            WHERE ord_orh_id = @orh_Id;

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
                @orh_Id,
                T.rawmaterial_code,
                T.qty,
                T.req_delivery_date,
                T.remark,
                COALESCE(NULLIF(T.rate, 0), V.vrl_rate, 0),
                @user_id,
                GETDATE(),
                'Y'
            FROM @tbl_opc_request_dtls T
            LEFT JOIN dbo.opc_vendor_rawmat_linking V WITH (NOLOCK)
                ON V.vrl_vendor_id = @rawmaterial_vender_code
               AND V.vrl_rawmat_code = T.rawmaterial_code
               AND ISNULL(V.active, 'Y') = 'Y'
            WHERE ISNULL(T.qty, 0) > 0;

            IF @@ROWCOUNT = 0
            BEGIN
                ROLLBACK TRANSACTION;
                RETURN;
            END

            SET @outputCode = @orh_Id;
        END
        ELSE
        BEGIN
            RAISERROR('Invalid transaction type. Use 1 for Insert and 2 for Update.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO

/*
    If linking table columns are opc_vendor_id / opc_rawmat_code / opc_rate,
    use this JOIN instead:

        ON V.opc_vendor_id = @rawmaterial_vender_code
       AND V.opc_rawmat_code = T.rawmaterial_code

    and rate: COALESCE(NULLIF(T.rate, 0), V.opc_rate, 0)
*/
