USE [VoucherzDb]
GO

/****** Object:  Table [dbo].[Metadata_Tbl]    Script Date: 1/15/2019 10:05:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Metadata_Tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [nvarchar](128) NOT NULL,
	[Length] [int] NOT NULL,
	[Charset] [int] NOT NULL,
	[Prefix] [varchar](10) NOT NULL,
	[Suffix] [varchar](10) NOT NULL,
	[Pattern] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Metadata] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

