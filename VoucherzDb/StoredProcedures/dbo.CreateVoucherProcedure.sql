USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[CreateVoucherProcedure]    Script Date: 1/17/2019 10:09:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CreateVoucherProcedure]
(
    @VoucherId nvarchar(50),
    @Code nvarchar(max),
    @VoucherType int,
    @Campaign varchar(50),
    @DiscountType int,
    @PercentOff int,
    @AmountLimit money,
    @AmountOff money,
    @UnitOff varchar(max),
    @GiftAmount money,
    @GiftBalance money,
    @VirtualPin bigint,
    @ValueType int,
    @StartDate datetime,
    @ExpirationDate datetime,
    @RedemptionCount int,
    @Length int,
    @Charset int,
    @Prefix varchar(50),
    @Suffix varchar(50),
    @Pattern varchar(50),
    @CreatedBy varchar(50),
    @CreationDate datetime
)
AS
BEGIN TRANSACTION
	SAVE TRANSACTION mysavepoint
	BEGIN TRY

		DECLARE @CampaignId int = NULL
		
		IF @Campaign IS NOT NULL AND @Campaign != ''
			BEGIN
				--Check if the Campaign already exists in the Campaign_Tbl
				--If not, insert @Campaign record into the Campaign_Tbl
				IF NOT EXISTS(SELECT Campaign FROM Campaign_Tbl WHERE Campaign = @Campaign AND @Campaign != '')
					BEGIN
						INSERT INTO [dbo].[Campaign_Tbl] (Campaign,MerchantId) VALUES (@Campaign,@CreatedBy)
					END
			END	
		PRINT 'Inserted into Campaign_Tbl'
		
		IF @Campaign IS NOT NULL AND @Campaign != ''
			BEGIN
				--Insert the CampaignId into the Voucher_Tbl If it exists
				IF EXISTS(SELECT Campaign FROM Campaign_Tbl WHERE Campaign = @Campaign)
					BEGIN
						SELECT @CampaignId = Id FROM [dbo].[Campaign_Tbl] WHERE Campaign = @Campaign
					END
			END
		PRINT 'Retrieved CampaignId'

		--Insert Values into Voucher_Tbl
		IF NOT EXISTS (SELECT Code FROM [dbo].[Voucher_Tbl] WHERE Code = @Code)
			BEGIN
				INSERT INTO [dbo].[Voucher_Tbl] 
				(Id,Code,VoucherType,CampaignId,StartDate,ExpirationDate,CreatedBy,CreationDate) 
				VALUES 
				(@VoucherId,@Code,@VoucherType,@CampaignId,@StartDate,@ExpirationDate,@CreatedBy,@CreationDate)
			END
		PRINT 'Inserted into Voucher_Tbl'

		--Insert into VoucherType Tables depending on the voucher type specified in @Voucherype variable
		IF (@VoucherType = 0)
		BEGIN
			IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Discount_Tbl] WHERE VoucherId = @VoucherId)
			BEGIN
				--IF it is a discount voucher, Insert into necessary columns depending on the discount properties
				IF (@DiscountType = 0)
					BEGIN
						INSERT INTO [dbo].[Discount_Tbl] (VoucherId,DiscountType,AmountOff)
						VALUES (@VoucherId,@DiscountType,@AmountOff)
						PRINT 'Inserted into Discount Amount Tables'
					END
				ELSE IF (@DiscountType = 1)
				BEGIN
					INSERT INTO [dbo].[Discount_Tbl] (VoucherId,DiscountType,PercentOff,AmountLimit)
					VALUES (@VoucherId,@DiscountType,@PercentOff,@AmountLimit)
					PRINT 'Inserted into Discount Percentage Table'
				END
				ELSE IF (@DiscountType = 2)
				BEGIN
					INSERT INTO [dbo].[Discount_Tbl] (VoucherId,DiscountType,UnitOff)
					VALUES (@VoucherId,@DiscountType,@UnitOff)
					PRINT 'Inserted into Discount Unit Table'
				END
			END
		END
		ELSE IF (@VoucherType = 1)
		BEGIN
			IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Gift_Tbl] WHERE VoucherId = @VoucherId)
			BEGIN 
				INSERT INTO [dbo].[Gift_Tbl] (VoucherId,Amount,Balance)
				VALUES (@VoucherId,@GiftAmount,@GiftBalance)
				PRINT 'Inserted into Gift Table'
			END
		END
		ELSE IF (@VoucherType = 2)
		BEGIN
			IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Value_Tbl] WHERE VoucherId = @VoucherId)
			BEGIN
				INSERT INTO [VoucherzDb].[dbo].[Value_Tbl] (VoucherId,ValueType,VirtualPin)
				VALUES (@VoucherId,@ValueType,@VirtualPin)
				PRINT 'Inserted into ValueType Table'
			END
		END

	--Insert into the Redemption Table
	BEGIN
		IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Redemption_Tbl] WHERE VoucherId = @VoucherId)
		BEGIN
			INSERT INTO [VoucherzDb].[dbo].[Redemption_Tbl] (VoucherId,RedemptionCount)
			VALUES (@VoucherId,@RedemptionCount)
		END
	END
	PRINT 'Inserted into Redemption_Tbl'

	--Insert Code configuration details into the metadata table
	BEGIN
		IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Metadata_Tbl] WHERE VoucherId = @VoucherId)
		BEGIN
			INSERT INTO [VoucherzDb].[dbo].[Metadata_Tbl] (VoucherId,[Length],Charset,Prefix,Suffix,Pattern)
			VALUES (@VoucherId,@Length,@Charset,@Prefix,@Suffix,@Pattern)
		END
	END
	PRINT 'Inserted into Metadata_Tbl'

	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
		BEGIN			
			ROLLBACK TRANSACTION mysavepoint
			PRINT 'Transaction was Rolled Back'	
		END		 
	END CATCH
COMMIT

GO

