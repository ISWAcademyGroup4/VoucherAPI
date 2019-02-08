USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[Voucher_Tbl]    Script Date: 1/17/2019 9:38:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Voucher_Tbl](
	[Id] [nvarchar](128) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[VoucherType] [int] NOT NULL,
	[CampaignId] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[isDeleted] [bit] NULL,
	[DeletionDate] [datetime] NULL,
 CONSTRAINT [PK_Voucher_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Voucher_Tbl] ADD  CONSTRAINT [DF_Voucher_Tbl_Active]  DEFAULT ((0)) FOR [Active]
GO

ALTER TABLE [dbo].[Voucher_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Tbl_Campaign_Tbl] FOREIGN KEY([CampaignId])
REFERENCES [dbo].[Campaign_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Voucher_Tbl] CHECK CONSTRAINT [FK_Voucher_Tbl_Campaign_Tbl]
GO

ALTER TABLE [dbo].[Voucher_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Tbl_VoucherType_Tbl] FOREIGN KEY([VoucherType])
REFERENCES [dbo].[VoucherType_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Voucher_Tbl] CHECK CONSTRAINT [FK_Voucher_Tbl_VoucherType_Tbl]
GO

