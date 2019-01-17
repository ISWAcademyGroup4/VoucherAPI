USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[ListVouchersProcedure]    Script Date: 1/17/2019 10:09:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListVouchersProcedure]
	@Campaign varchar(50)
AS
    DECLARE @CampaignId int

    SELECT @CampaignId = Id FROM Campaign_Tbl WHERE Campaign = @Campaign

	SELECT 
     Code
    ,VoucherType
    ,DiscountType
    ,PercentOff
    ,AmountLimit
    ,AmountOff
    ,UnitOff
    ,StartDate
    ,ExpirationDate
    ,RedemptionCount
    ,RedeemedCount
    ,RedeemedAmount
    ,Active
    ,CreationDate
 FROM [dbo].[Voucher_Tbl] WHERE CampaignId = @CampaignId
 
RETURN 0
GO

