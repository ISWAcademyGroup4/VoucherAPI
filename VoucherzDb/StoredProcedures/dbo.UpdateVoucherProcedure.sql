USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[UpdateVoucherProcedure]    Script Date: 1/14/2019 4:24:04 PM ******/
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


