USE [VoucherzDb]
GO

/****** Object:  StoredProcedure [dbo].[DeleteVoucherProcedure]    Script Date: 1/14/2019 4:47:21 PM ******/
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

