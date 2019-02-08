USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[DiscountType_Tbl]    Script Date: 1/17/2019 9:36:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DiscountType_Tbl](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[DiscountType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DiscountType_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[DiscountType_Tbl] (DiscountType) VALUES ('amount')
GO

INSERT INTO [dbo].[DiscountType_Tbl] (DiscountType) VALUES ('percentage')
GO

INSERT INTO [dbo].[DiscountType_Tbl] (DiscountType) VALUES ('unit')
GO
