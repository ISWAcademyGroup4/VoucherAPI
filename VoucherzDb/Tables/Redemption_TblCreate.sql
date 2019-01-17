USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[Redemption_Tbl]    Script Date: 1/17/2019 9:37:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Redemption_Tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [nvarchar](128) NOT NULL,
	[RedemptionCount] [int] NOT NULL,
	[RedeemedCount] [int] NOT NULL,
	[isRedeemed] [bit] NOT NULL,
	[RedeemedAmount] [money] NOT NULL,
 CONSTRAINT [PK_Redemption_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_RedemptionCount]  DEFAULT ((0)) FOR [RedemptionCount]
GO

ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_RedeemedCount]  DEFAULT ((0)) FOR [RedeemedCount]
GO

ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_isRedeemed]  DEFAULT ((0)) FOR [isRedeemed]
GO

ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_RedeemedAmount]  DEFAULT ((0)) FOR [RedeemedAmount]
GO

ALTER TABLE [dbo].[Redemption_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Redemption_Tbl_Voucher_Tbl] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Redemption_Tbl] CHECK CONSTRAINT [FK_Redemption_Tbl_Voucher_Tbl]
GO

