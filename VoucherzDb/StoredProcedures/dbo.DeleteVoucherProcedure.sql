USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[DeleteVoucherProcedure]    Script Date: 1/17/2019 9:55:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteVoucherProcedure]
	@VoucherCode varchar(max),
	@DeletionDate datetime
AS
	UPDATE [dbo].[Voucher_Tbl] SET isDeleted = '1', DeletionDate = @DeletionDate WHERE Code = @VoucherCode
RETURN 0
GO

