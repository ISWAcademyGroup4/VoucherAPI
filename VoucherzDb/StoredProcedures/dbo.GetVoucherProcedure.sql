USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[GetVoucherProcedure]    Script Date: 1/15/2019 3:01:13 PM ******/
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
	SET NOCOUNT ON

	SELECT [Code]
		,[VoucherType]
		,[DiscountType]
		,[PercentOff]
		,[AmountLimit]
		,[AmountOff]
		,[UnitOff]
		,[GiftAmount]
		,[GiftBalance]
		,[VirtualPin]
		,[ValueType]
		,[RedemptionCount]
		,[RedeemedCount]
		,[RedeemedAmount]
		,[isRedeemed]
		,[Length]
		,[Charset]
		,[Prefix]
		,[Suffix]
		,[Pattern]
		,[CreationDate]
		,[Active]
		,[Campaign_Tbl].[Campaign]
	FROM [dbo].[Voucher_Tbl], [dbo].[Campaign_Tbl] 
	WHERE Code = @Code AND CreatedBy = @MerchantId AND Voucher_Tbl.CampaignId = Campaign_Tbl.Id

RETURN 0
GO

