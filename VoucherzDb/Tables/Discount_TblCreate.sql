USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[Discount_Tbl]    Script Date: 1/17/2019 9:36:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Discount_Tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [nvarchar](128) NOT NULL,
	[DiscountType] [int] NOT NULL,
	[PercentOff] [int] NULL,
	[AmountLimit] [money] NULL,
	[AmountOff] [money] NULL,
	[UnitOff] [varchar](50) NULL,
 CONSTRAINT [PK_Discount_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Discount_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Discount_Tbl_DiscountType_Tbl] FOREIGN KEY([DiscountType])
REFERENCES [dbo].[DiscountType_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Discount_Tbl] CHECK CONSTRAINT [FK_Discount_Tbl_DiscountType_Tbl]
GO

ALTER TABLE [dbo].[Discount_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Discount_Tbl_Voucher_Tbl] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Discount_Tbl] CHECK CONSTRAINT [FK_Discount_Tbl_Voucher_Tbl]
GO

