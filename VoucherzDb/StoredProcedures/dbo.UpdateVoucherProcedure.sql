USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[UpdateVoucherProcedure]    Script Date: 1/17/2019 9:55:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateVoucherProcedure]
(
    @VoucherCode varchar(max),
    @ExpirationDate datetime
)
AS
	DECLARE @CreatedBy varchar(50)

	
	UPDATE [dbo].[Voucher_Tbl] SET ExpirationDate = @ExpirationDate WHERE Code = @VoucherCode
	
GO

