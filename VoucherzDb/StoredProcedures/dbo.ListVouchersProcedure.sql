CREATE PROCEDURE [dbo].[ListVouchersProcedure]
    @campaign nvarchar(50),
    @MerchantId nvarchar(max)
AS
BEGIN TRANSACTION
    SAVE TRANSACTION mysavepoint

    DECLARE @CampaignId int
    DECLARE @VoucherType int
    DECLARE @DiscountType int
    DECLARE @VoucherId nvarchar(max)

    BEGIN TRY
        IF EXISTS (SELECT Campaign FROM dbo.Campaign_Tbl WHERE Campaign = @campaign)
        BEGIN
            SELECT @CampaignId = Id FROM dbo.Campaign_Tbl WHERE Campaign = @campaign
        END
        
        SELECT @VoucherType = VoucherType FROM dbo.Voucher_Tbl WHERE CampaignId = @CampaignId AND CreatedBy = @MerchantId
        
        BEGIN
			IF (@VoucherType = 0)
			BEGIN
				SELECT @DiscountType = DiscountType FROM [dbo].[Discount_Tbl] WHERE VoucherId = @VoucherId
				IF (@DiscountType = 0)
				BEGIN
					SELECT * FROM [dbo].[DiscountAmountView]
				END
				ELSE IF (@DiscountType = 1)
				BEGIN
					SELECT * FROM [dbo].[DiscountPercentageView]
				END
				ELSE IF (@DiscountType = 2)
				BEGIN
					SELECT * FROM [dbo].[DiscountUnitView]
				END
			END
			ELSE IF (@VoucherType = 1)
			BEGIN
				SELECT * FROM [dbo].[GiftView]
			END
			ELSE IF (@VoucherType = 2)
			BEGIN
				SELECT * FROM [dbo].[ValueView]
			END
		END       
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION mysavepoint
        END
    END CATCH
COMMIT
