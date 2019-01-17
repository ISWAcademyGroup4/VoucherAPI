USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[Value_Tbl]    Script Date: 1/15/2019 10:06:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Value_Tbl](
	[Id] [int] NOT NULL,
	[VoucherId] [nvarchar](128) NOT NULL,
	[ValueType] [int] NOT NULL,
	[VirtualPin] [bigint] NOT NULL,
 CONSTRAINT [PK_Value_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Value_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Value_Tbl_ValueType_Tbl1] FOREIGN KEY([ValueType])
REFERENCES [dbo].[ValueType_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Value_Tbl] CHECK CONSTRAINT [FK_Value_Tbl_ValueType_Tbl1]
GO

ALTER TABLE [dbo].[Value_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Value_Tbl_Voucher_Tbl] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher_Tbl] ([Id])
GO

ALTER TABLE [dbo].[Value_Tbl] CHECK CONSTRAINT [FK_Value_Tbl_Voucher_Tbl]
GO

