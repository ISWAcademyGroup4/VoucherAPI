USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[ValueType_Tbl]    Script Date: 1/17/2019 9:38:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ValueType_Tbl](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[ValueType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ValueType_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[ValueType_Tbl] (ValueType) VALUES ('Airtime')
GO

INSERT INTO [dbo].[ValueType_Tbl] (ValueType) VALUES ('Paycode')
GO
