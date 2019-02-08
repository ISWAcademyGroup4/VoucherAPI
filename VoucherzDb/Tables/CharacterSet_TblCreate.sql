USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[CharacterSet_Tbl]    Script Date: 1/17/2019 9:36:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CharacterSet_Tbl](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[CharacterSet] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CharacterSet_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[CharacterSet_Tbl] (CharacterSet) VALUES ('Numeric')
GO

INSERT INTO [dbo].[CharacterSet_Tbl] (CharacterSet) VALUES ('Alphabetic')
GO

INSERT INTO [dbo].[CharacterSet_Tbl] (CharacterSet) VALUES ('Alphanumeric')
GO
