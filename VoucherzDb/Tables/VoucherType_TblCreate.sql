USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[VoucherType_Tbl]    Script Date: 1/17/2019 9:38:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VoucherType_Tbl](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[VoucherType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_VoucherType_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[VoucherType_Tbl] (VoucherType) VALUES ('Discount')
GO

INSERT INTO [dbo].[VoucherType_Tbl] (VoucherType) VALUES ('Gift')
GO

INSERT INTO [dbo].[VoucherType_Tbl] (VoucherType) VALUES ('Value')
GO
