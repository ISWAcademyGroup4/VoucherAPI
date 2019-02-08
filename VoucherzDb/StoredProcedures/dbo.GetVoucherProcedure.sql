USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[GetVoucherProcedure]    Script Date: 1/17/2019 9:55:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetVoucherProcedure]
(
    @Code varchar(max),
    @MerchantId varchar(50)
)
AS
BEGIN TRANSACTION
	SAVE TRANSACTION mysavepoint
	SET NOCOUNT ON
	BEGIN TRY
		DECLARE @VoucherType int
		DECLARE @VoucherId nvarchar(MAX)
		DECLARE @DiscountType int
		
		--Get Voucher Code and Id from Voucher_Tbl
		BEGIN
			IF EXISTS (SELECT Code FROM [dbo].[Voucher_Tbl] WHERE Code = @Code)
			BEGIN
				SELECT @VoucherId = Id, @VoucherType = VoucherType FROM [dbo].[Voucher_Tbl] WHERE Code = @Code
			END
			ELSE
				RAISERROR('Voucher does not exist',16,1)
		END

		BEGIN
			IF (@VoucherType = 0)
			BEGIN
				SELECT @DiscountType = DiscountType FROM [dbo].[Discount_Tbl] WHERE VoucherId = @VoucherId
				IF (@DiscountType = 0)
				BEGIN
					SELECT * FROM [dbo].[DiscountAmountView] WHERE Code = @Code
				END
				ELSE IF (@DiscountType = 1)
				BEGIN
					SELECT * FROM [dbo].[DiscountPercentageView] WHERE Code = @Code
				END
				ELSE IF (@DiscountType = 2)
				BEGIN
					SELECT * FROM [dbo].[DiscountUnitView] WHERE Code = @Code
				END
			END
			ELSE IF (@VoucherType = 1)
			BEGIN
				SELECT * FROM [dbo].[GiftView] WHERE Code = @Code
			END
			ELSE IF (@VoucherType = 2)
			BEGIN
				SELECT * FROM [dbo].[ValueView] WHERE Code = @Code
			END
		END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION mysavepoint
			END
	END CATCH
COMMIT TRANSACTION
GO

