USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[Gift_Tbl]    Script Date: 1/15/2019 10:03:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Gift_Tbl](
	[Id] [int] NOT NULL,
	[VoucherId] [nvarchar](128) NOT NULL,
	[Amount] [money] NOT NULL,
	[Balance] [money] NOT NULL,
 CONSTRAINT [PK_Gift_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Gift_Tbl] ADD  CONSTRAINT [DF_Gift_Tbl_Amount]  DEFAULT ((0)) FOR [Amount]
GO

ALTER TABLE [dbo].[Gift_Tbl] ADD  CONSTRAINT [DF_Gift_Tbl_Balance]  DEFAULT ((0)) FOR [Balance]
GO

ALTER TABLE [dbo].[Gift_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Gift_Tbl_Voucher_Tbl] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Gift_Tbl] CHECK CONSTRAINT [FK_Gift_Tbl_Voucher_Tbl]
GO

