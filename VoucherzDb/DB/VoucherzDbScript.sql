USE [master]
GO
/****** Object:  Database [VoucherzDb]    Script Date: 2/4/2019 6:08:40 PM ******/
CREATE DATABASE [VoucherzDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VoucherzDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VoucherzDb.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VoucherzDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VoucherzDb_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [VoucherzDb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VoucherzDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VoucherzDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VoucherzDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VoucherzDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VoucherzDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VoucherzDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [VoucherzDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VoucherzDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VoucherzDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VoucherzDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VoucherzDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VoucherzDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VoucherzDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VoucherzDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VoucherzDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VoucherzDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VoucherzDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VoucherzDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VoucherzDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VoucherzDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VoucherzDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VoucherzDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VoucherzDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VoucherzDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VoucherzDb] SET  MULTI_USER 
GO
ALTER DATABASE [VoucherzDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VoucherzDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VoucherzDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VoucherzDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VoucherzDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VoucherzDb] SET QUERY_STORE = OFF
GO
USE [VoucherzDb]
GO
/****** Object:  User [VoucherzUser]    Script Date: 2/4/2019 6:08:41 PM ******/
CREATE USER [VoucherzUser] FOR LOGIN [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [VoucherzUser]
GO
/****** Object:  Table [dbo].[Discount_Tbl]    Script Date: 2/4/2019 6:08:41 PM ******/
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
/****** Object:  Table [dbo].[Campaign_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campaign_Tbl](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Campaign] [varchar](50) NOT NULL,
	[MerchantId] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Campaign_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
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
	[BatchId] [int] NOT NULL,
 CONSTRAINT [PK_Voucher_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Redemption_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
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
/****** Object:  View [dbo].[DiscountAmountView]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DiscountAmountView]
AS
SELECT        dbo.Voucher_Tbl.Code, dbo.Campaign_Tbl.Campaign, dbo.Voucher_Tbl.VoucherType, dbo.Discount_Tbl.DiscountType, dbo.Discount_Tbl.AmountOff, dbo.Redemption_Tbl.RedemptionCount, dbo.Redemption_Tbl.RedeemedCount, 
                         dbo.Redemption_Tbl.isRedeemed, dbo.Redemption_Tbl.RedeemedAmount, dbo.Voucher_Tbl.StartDate, dbo.Voucher_Tbl.ExpirationDate, dbo.Voucher_Tbl.Active, dbo.Voucher_Tbl.CreationDate, dbo.Voucher_Tbl.CreatedBy
FROM            dbo.Voucher_Tbl INNER JOIN
                         dbo.Campaign_Tbl ON dbo.Voucher_Tbl.CampaignId = dbo.Campaign_Tbl.Id INNER JOIN
                         dbo.Discount_Tbl ON dbo.Voucher_Tbl.Id = dbo.Discount_Tbl.VoucherId INNER JOIN
                         dbo.Redemption_Tbl ON dbo.Voucher_Tbl.Id = dbo.Redemption_Tbl.VoucherId
WHERE        (dbo.Voucher_Tbl.isDeleted = 0) AND (dbo.Discount_Tbl.AmountOff > 0)
GO
/****** Object:  View [dbo].[DiscountPercentageView]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DiscountPercentageView]
AS
SELECT        dbo.Voucher_Tbl.Code, dbo.Campaign_Tbl.Campaign, dbo.Voucher_Tbl.VoucherType, dbo.Discount_Tbl.DiscountType, dbo.Discount_Tbl.PercentOff, dbo.Discount_Tbl.AmountLimit, dbo.Redemption_Tbl.RedemptionCount, 
                         dbo.Redemption_Tbl.RedeemedCount, dbo.Redemption_Tbl.isRedeemed, dbo.Redemption_Tbl.RedeemedAmount, dbo.Voucher_Tbl.StartDate, dbo.Voucher_Tbl.ExpirationDate, dbo.Voucher_Tbl.Active, 
                         dbo.Voucher_Tbl.CreatedBy, dbo.Voucher_Tbl.CreationDate
FROM            dbo.Voucher_Tbl INNER JOIN
                         dbo.Campaign_Tbl ON dbo.Voucher_Tbl.CampaignId = dbo.Campaign_Tbl.Id INNER JOIN
                         dbo.Discount_Tbl ON dbo.Voucher_Tbl.Id = dbo.Discount_Tbl.VoucherId INNER JOIN
                         dbo.Redemption_Tbl ON dbo.Voucher_Tbl.Id = dbo.Redemption_Tbl.VoucherId
WHERE        (dbo.Voucher_Tbl.isDeleted = 0) AND (dbo.Discount_Tbl.PercentOff > 0)
GO
/****** Object:  View [dbo].[DiscountUnitView]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DiscountUnitView]
AS
SELECT        dbo.Voucher_Tbl.Code, dbo.Campaign_Tbl.Campaign, dbo.Voucher_Tbl.VoucherType, dbo.Discount_Tbl.DiscountType, dbo.Discount_Tbl.UnitOff, dbo.Redemption_Tbl.RedemptionCount, dbo.Redemption_Tbl.RedeemedCount, 
                         dbo.Redemption_Tbl.isRedeemed, dbo.Redemption_Tbl.RedeemedAmount, dbo.Voucher_Tbl.StartDate, dbo.Voucher_Tbl.ExpirationDate, dbo.Voucher_Tbl.Active, dbo.Voucher_Tbl.CreatedBy, dbo.Voucher_Tbl.CreationDate
FROM            dbo.Voucher_Tbl INNER JOIN
                         dbo.Campaign_Tbl ON dbo.Voucher_Tbl.CampaignId = dbo.Campaign_Tbl.Id INNER JOIN
                         dbo.Discount_Tbl ON dbo.Voucher_Tbl.Id = dbo.Discount_Tbl.VoucherId INNER JOIN
                         dbo.Redemption_Tbl ON dbo.Voucher_Tbl.Id = dbo.Redemption_Tbl.VoucherId
WHERE        (dbo.Voucher_Tbl.isDeleted = 0) AND (dbo.Discount_Tbl.UnitOff IS NOT NULL)
GO
/****** Object:  Table [dbo].[Gift_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gift_Tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [nvarchar](128) NOT NULL,
	[Amount] [money] NOT NULL,
	[Balance] [money] NOT NULL,
 CONSTRAINT [PK_Gift_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GiftView]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GiftView]
AS
SELECT        dbo.Voucher_Tbl.Code, dbo.Campaign_Tbl.Campaign, dbo.Voucher_Tbl.VoucherType, dbo.Gift_Tbl.Amount, dbo.Gift_Tbl.Balance, dbo.Redemption_Tbl.RedemptionCount, dbo.Redemption_Tbl.RedeemedCount, 
                         dbo.Redemption_Tbl.isRedeemed, dbo.Redemption_Tbl.RedeemedAmount, dbo.Voucher_Tbl.StartDate, dbo.Voucher_Tbl.ExpirationDate, dbo.Voucher_Tbl.Active, dbo.Voucher_Tbl.CreatedBy, dbo.Voucher_Tbl.CreationDate
FROM            dbo.Voucher_Tbl INNER JOIN
                         dbo.Campaign_Tbl ON dbo.Voucher_Tbl.CampaignId = dbo.Campaign_Tbl.Id INNER JOIN
                         dbo.Gift_Tbl ON dbo.Voucher_Tbl.Id = dbo.Gift_Tbl.VoucherId INNER JOIN
                         dbo.Redemption_Tbl ON dbo.Voucher_Tbl.Id = dbo.Redemption_Tbl.VoucherId
WHERE        (dbo.Voucher_Tbl.isDeleted = 0)
GO
/****** Object:  Table [dbo].[Value_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Value_Tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [nvarchar](128) NOT NULL,
	[ValueType] [int] NOT NULL,
	[VirtualPin] [bigint] NOT NULL,
 CONSTRAINT [PK_Value_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ValueView]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ValueView]
AS
SELECT        dbo.Voucher_Tbl.Code, dbo.Campaign_Tbl.Campaign, dbo.Voucher_Tbl.VoucherType, dbo.Value_Tbl.ValueType, dbo.Value_Tbl.VirtualPin, dbo.Redemption_Tbl.RedemptionCount, dbo.Redemption_Tbl.RedeemedCount, 
                         dbo.Redemption_Tbl.isRedeemed, dbo.Redemption_Tbl.RedeemedAmount, dbo.Voucher_Tbl.StartDate, dbo.Voucher_Tbl.ExpirationDate, dbo.Voucher_Tbl.Active, dbo.Voucher_Tbl.CreatedBy, dbo.Voucher_Tbl.CreationDate
FROM            dbo.Voucher_Tbl INNER JOIN
                         dbo.Campaign_Tbl ON dbo.Voucher_Tbl.CampaignId = dbo.Campaign_Tbl.Id INNER JOIN
                         dbo.Value_Tbl ON dbo.Voucher_Tbl.Id = dbo.Value_Tbl.VoucherId INNER JOIN
                         dbo.Redemption_Tbl ON dbo.Voucher_Tbl.Id = dbo.Redemption_Tbl.VoucherId
WHERE        (dbo.Voucher_Tbl.isDeleted = 0)
GO
/****** Object:  Table [dbo].[Batch_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch_Tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchNo] [varchar](50) NOT NULL,
	[VoucherCount] [bigint] NOT NULL,
	[QuantityCreated] [bigint] NOT NULL,
 CONSTRAINT [PK_Batch_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharacterSet_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
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

INSERT INTO [dbo].[CharacterSet_Tbl] (CharacterSet) VALUES ('numeric')
GO

INSERT INTO [dbo].[CharacterSet_Tbl] (CharacterSet) VALUES ('alphabetic')
GO

INSERT INTO [dbo].[CharacterSet_Tbl] (CharacterSet) VALUES ('alphanumeric')
GO

/****** Object:  Table [dbo].[DiscountType_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
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
/****** Object:  Table [dbo].[Metadata_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
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
/****** Object:  Table [dbo].[ValueType_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
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

/****** Object:  Table [dbo].[VoucherType_Tbl]    Script Date: 2/4/2019 6:08:42 PM ******/
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

ALTER TABLE [dbo].[Batch_Tbl] ADD  CONSTRAINT [DF_Batch_Tbl_QuantityCreated]  DEFAULT ((0)) FOR [QuantityCreated]
GO
ALTER TABLE [dbo].[Gift_Tbl] ADD  CONSTRAINT [DF_Gift_Tbl_Amount]  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[Gift_Tbl] ADD  CONSTRAINT [DF_Gift_Tbl_Balance]  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_RedemptionCount]  DEFAULT ((0)) FOR [RedemptionCount]
GO
ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_RedeemedCount]  DEFAULT ((0)) FOR [RedeemedCount]
GO
ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_isRedeemed]  DEFAULT ((0)) FOR [isRedeemed]
GO
ALTER TABLE [dbo].[Redemption_Tbl] ADD  CONSTRAINT [DF_Redemption_Tbl_RedeemedAmount]  DEFAULT ((0)) FOR [RedeemedAmount]
GO
ALTER TABLE [dbo].[Voucher_Tbl] ADD  CONSTRAINT [DF_Voucher_Tbl_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Voucher_Tbl] ADD  CONSTRAINT [DF_Voucher_Tbl_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
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
ALTER TABLE [dbo].[Gift_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Gift_Tbl_Voucher_Tbl] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher_Tbl] ([Id])
GO
ALTER TABLE [dbo].[Gift_Tbl] CHECK CONSTRAINT [FK_Gift_Tbl_Voucher_Tbl]
GO
ALTER TABLE [dbo].[Metadata_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_Tbl_CharacterSet_Tbl] FOREIGN KEY([Charset])
REFERENCES [dbo].[CharacterSet_Tbl] ([Id])
GO
ALTER TABLE [dbo].[Metadata_Tbl] CHECK CONSTRAINT [FK_Metadata_Tbl_CharacterSet_Tbl]
GO
ALTER TABLE [dbo].[Metadata_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_Tbl_Voucher_Tbl] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher_Tbl] ([Id])
GO
ALTER TABLE [dbo].[Metadata_Tbl] CHECK CONSTRAINT [FK_Metadata_Tbl_Voucher_Tbl]
GO
ALTER TABLE [dbo].[Redemption_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Redemption_Tbl_Voucher_Tbl] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Voucher_Tbl] ([Id])
GO
ALTER TABLE [dbo].[Redemption_Tbl] CHECK CONSTRAINT [FK_Redemption_Tbl_Voucher_Tbl]
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
ALTER TABLE [dbo].[Voucher_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Tbl_Batch_Tbl] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch_Tbl] ([Id])
GO
ALTER TABLE [dbo].[Voucher_Tbl] CHECK CONSTRAINT [FK_Voucher_Tbl_Batch_Tbl]
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
/****** Object:  StoredProcedure [dbo].[addGiftBalance]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[addGiftBalance] 
	-- Add the parameters for the stored procedure here
	@code nvarchar(max),
	@MerchantId nvarchar(max),
	@amount money
AS
BEGIN
	DECLARE @VoucherId nvarchar(max)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS(SELECT Code FROM Voucher_Tbl WHERE Code = @code)
	BEGIN
		SELECT @VoucherId = Id FROM Voucher_Tbl WHERE Code = @code AND CreatedBy = @MerchantId
	END
	UPDATE Gift_Tbl SET Balance = (Amount - @amount) WHERE VoucherId = @VoucherId

END
GO
/****** Object:  StoredProcedure [dbo].[create]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[create]
(
    @VoucherId nvarchar(50),
    @Code nvarchar(max),
    @VoucherType int,
    @Campaign varchar(50),
    @DiscountType int,
    @PercentOff int,
    @AmountLimit money,
    @AmountOff money,
    @UnitOff varchar(max),
    @GiftAmount money,
    @GiftBalance money,
    @VirtualPin bigint,
    @ValueType int,
    @StartDate datetime,
    @ExpirationDate datetime,
    @RedemptionCount int,
    @Length int,
    @Charset int,
    @Prefix varchar(50),
    @Suffix varchar(50),
    @Pattern varchar(50),
    @CreatedBy varchar(50),
    @CreationDate datetime,
	@BatchNo varchar(50),
	@VoucherCount int
)
AS
BEGIN TRANSACTION
	SAVE TRANSACTION mysavepoint
	BEGIN TRY
		DECLARE @CampaignId int
		DECLARE @BatchId int
		
		IF NOT EXISTS(SELECT BatchNo FROM dbo.Batch_Tbl WHERE BatchNo = @BatchNo)
		BEGIN
			INSERT INTO dbo.Batch_Tbl (BatchNo,VoucherCount) VALUES (@BatchNo,@VoucherCount)			
			PRINT 'Inserted into BatchTbl'
		END		
			
		SELECT @BatchId = Id FROM dbo.Batch_Tbl WHERE BatchNo = @BatchNo

		IF NOT EXISTS(SELECT Campaign FROM Campaign_Tbl WHERE Campaign = @Campaign)
		BEGIN
			INSERT INTO [dbo].[Campaign_Tbl] (Campaign,MerchantId) VALUES (@Campaign,@CreatedBy)			
			PRINT 'Inserted into Campaign_Tbl'
		END	
		
		SELECT @CampaignId = Id FROM dbo.Campaign_Tbl WHERE Campaign = @Campaign	

		IF NOT EXISTS(SELECT Code FROM [dbo].[Voucher_Tbl] WHERE Code = @Code)
			BEGIN
				INSERT INTO [dbo].[Voucher_Tbl] 
				(Id,Code,VoucherType,CampaignId,StartDate,ExpirationDate,CreatedBy,CreationDate,BatchId) 
				VALUES 
				(@VoucherId,@Code,@VoucherType,@CampaignId,@StartDate,@ExpirationDate,@CreatedBy,@CreationDate,@BatchId)
				PRINT 'Inserted into Voucher_Tbl'
			END
		ELSE 
			RETURN 8890
			
		--Insert into VoucherType Tables depending on the voucher type specified in @Voucherype variable
		IF (@VoucherType = 0)
		BEGIN
			IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Discount_Tbl] WHERE VoucherId = @VoucherId)
			BEGIN
				
				IF (@DiscountType = 0)
					BEGIN
						INSERT INTO [dbo].[Discount_Tbl] (VoucherId,DiscountType,AmountOff)
						VALUES (@VoucherId,@DiscountType,@AmountOff)
						PRINT 'Inserted into Discount Amount Tables'
					END
				ELSE IF (@DiscountType = 1)
				BEGIN
					INSERT INTO [dbo].[Discount_Tbl] (VoucherId,DiscountType,PercentOff,AmountLimit)
					VALUES (@VoucherId,@DiscountType,@PercentOff,@AmountLimit)
					PRINT 'Inserted into Discount Percentage Table'
				END
				ELSE IF (@DiscountType = 2)
				BEGIN
					INSERT INTO [dbo].[Discount_Tbl] (VoucherId,DiscountType,UnitOff)
					VALUES (@VoucherId,@DiscountType,@UnitOff)
					PRINT 'Inserted into Discount Unit Table'
				END
			END
		END
		ELSE IF (@VoucherType = 1)
		BEGIN
			IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Gift_Tbl] WHERE VoucherId = @VoucherId)
			BEGIN 
				INSERT INTO [dbo].[Gift_Tbl] (VoucherId,Amount,Balance)
				VALUES (@VoucherId,@GiftAmount,@GiftBalance)
				PRINT 'Inserted into Gift Table'
			END
		END
		ELSE IF (@VoucherType = 2)
		BEGIN
			IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Value_Tbl] WHERE VoucherId = @VoucherId)
			BEGIN
				INSERT INTO [VoucherzDb].[dbo].[Value_Tbl] (VoucherId,ValueType,VirtualPin)
				VALUES (@VoucherId,@ValueType,@VirtualPin)
				PRINT 'Inserted into ValueType Table'
			END
		END

	
	IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Redemption_Tbl] WHERE VoucherId = @VoucherId)
	BEGIN
		INSERT INTO [VoucherzDb].[dbo].[Redemption_Tbl] (VoucherId,RedemptionCount)
		VALUES (@VoucherId,@RedemptionCount)
		PRINT 'Inserted into Redemption_Tbl'
	END
	

	--Insert Code configuration details into the metadata table
	IF NOT EXISTS (SELECT VoucherId FROM [dbo].[Metadata_Tbl] WHERE VoucherId = @VoucherId)
	BEGIN
		INSERT INTO [VoucherzDb].[dbo].[Metadata_Tbl] (VoucherId,[Length],Charset,Prefix,Suffix,Pattern)
		VALUES (@VoucherId,@Length,@Charset,@Prefix,@Suffix,@Pattern)
		PRINT 'Inserted into Metadata_Tbl'
	END
	
	UPDATE dbo.Batch_Tbl 
	SET QuantityCreated = QuantityCreated + 1 
	WHERE BatchNo = @BatchNo

	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
		BEGIN			
			ROLLBACK TRANSACTION mysavepoint
			PRINT 'Transaction was Rolled Back'	
		END		 
	END CATCH
COMMIT

GO
/****** Object:  StoredProcedure [dbo].[delete]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[delete]
	@VoucherCode varchar(max),
	@DeletionDate datetime
AS
	UPDATE [dbo].[Voucher_Tbl] SET isDeleted = '1', DeletionDate = @DeletionDate WHERE Code = @VoucherCode
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[deleteAllInfo]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[deleteAllInfo]
	
AS
BEGIN TRANSACTION
	SAVE TRANSACTION mysavepoint
	BEGIN TRY
		-- Delete statements for procedure here
		DELETE FROM Metadata_Tbl
		DELETE FROM Redemption_Tbl
		DELETE FROM Value_Tbl
		DELETE FROM Gift_Tbl
		DELETE FROM Discount_Tbl
		DELETE FROM Voucher_Tbl
		DELETE FROM Campaign_Tbl
		DELETE FROM Batch_Tbl
		PRINT 'Everything was deleted'
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION mysavepoint
				PRINT 'Transaction was Rolled Back'
			END
	END CATCH
COMMIT
GO
/****** Object:  StoredProcedure [dbo].[disable]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[disable] 
	@Code nvarchar(max),
	@MerchantId nvarchar(max)
AS
BEGIN TRANSACTION
	SAVE TRANSACTION mysavepoint
	BEGIN TRY
	DECLARE @ActiveStatus bit
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS (SELECT Code FROM dbo.Voucher_Tbl WHERE Code = @Code AND CreatedBy = @MerchantId)
	BEGIN
		SELECT @ActiveStatus = Active FROM dbo.Voucher_Tbl WHERE Code = @Code AND CreatedBy = @MerchantId
		IF @ActiveStatus = 1
		BEGIN
			UPDATE dbo.Voucher_Tbl SET Active = 0 WHERE Code = @Code AND CreatedBy = @MerchantId
		END
		ELSE
			RAISERROR ('Voucher is already disabled',10,1)
	END
	ELSE
		RAISERROR('Voucher does not exist',10,1)

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION mysavepoint
		END
	END CATCH
COMMIT
GO
/****** Object:  StoredProcedure [dbo].[enable]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[enable] 
	@Code nvarchar(max),
	@MerchantId nvarchar(max)
AS
BEGIN TRANSACTION
	SAVE TRANSACTION mysavepoint
	BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    IF EXISTS (SELECT Code FROM dbo.Voucher_Tbl WHERE Code = @Code AND CreatedBy = @MerchantId)
	BEGIN
		IF EXISTS (SELECT Code FROM dbo.Voucher_Tbl WHERE Code = @Code AND CreatedBy = @MerchantId AND Active = 1)
		BEGIN
			UPDATE dbo.Voucher_Tbl SET Active = 1 WHERE Code = @Code AND CreatedBy = @MerchantId
		END
		ELSE
			RAISERROR ('Voucher is already active',10,1)
	END
	ELSE
		RAISERROR('Voucher does not exist',10,1)

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRANSACTION mysavepoint
		END
	END CATCH
COMMIT
GO
/****** Object:  StoredProcedure [dbo].[findByCampaign]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[findByCampaign]
    @campaign nvarchar(50),
    @MerchantId nvarchar(max)
AS
BEGIN TRANSACTION
    SAVE TRANSACTION mysavepoint

    DECLARE @CampaignId int
    DECLARE @VoucherType int
    DECLARE @DiscountType int
    DECLARE @VoucherId nvarchar(max)

    BEGIN TRY
        IF EXISTS (SELECT Campaign FROM dbo.Campaign_Tbl WHERE Campaign = @campaign)
        BEGIN
            SELECT @CampaignId = Id FROM dbo.Campaign_Tbl WHERE Campaign = @campaign AND MerchantId = @MerchantId
			
        END
        PRINT 'Retrieved CampaignId'

		SELECT @VoucherType = VoucherType, @VoucherId = Id FROM dbo.Voucher_Tbl WHERE CampaignId = @CampaignId AND CreatedBy = @MerchantId
		       
        BEGIN
			IF (@VoucherType = 0)
			BEGIN
				SELECT @DiscountType = DiscountType FROM [dbo].[Discount_Tbl] WHERE VoucherId = @VoucherId
				IF (@DiscountType = 0)
				BEGIN
					SELECT * FROM [dbo].[DiscountAmountView] WHERE Campaign = @campaign
					PRINT 'Discount Amount Vouchers'
				END
				ELSE IF (@DiscountType = 1)
				BEGIN
					SELECT * FROM [dbo].[DiscountPercentageView] WHERE Campaign = @campaign
					PRINT 'Discount Percent Vouchers'
				END
				ELSE IF (@DiscountType = 2)
				BEGIN
					SELECT * FROM [dbo].[DiscountUnitView] WHERE Campaign = @campaign
					PRINT 'Discount Unit Vouchers'
				END
			END
			ELSE IF (@VoucherType = 1)
			BEGIN
				SELECT * FROM [dbo].[GiftView] WHERE Campaign = @campaign
				PRINT 'Gift Vouchers'
			END
			ELSE IF (@VoucherType = 2)
			BEGIN
				SELECT * FROM [dbo].[ValueView] WHERE Campaign = @campaign
				PRINT 'Value Vouchers'
			END
			ELSE
				PRINT 'Could not classify vouchers'
		END     
	  
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION mysavepoint
			PRINT 'Rolling Back Transaction'
        END
    END CATCH
COMMIT
GO
/****** Object:  StoredProcedure [dbo].[findByCode]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[findByCode]
(
    @Code varchar(max),
    @MerchantId varchar(50)
)
AS
BEGIN TRANSACTION
	SAVE TRANSACTION mysavepoint
	SET NOCOUNT ON
	BEGIN TRY
		DECLARE @VoucherType int
		DECLARE @VoucherId nvarchar(MAX)
		DECLARE @DiscountType int
		
		--Get Voucher Code and Id from Voucher_Tbl
		BEGIN
			IF EXISTS (SELECT Code FROM [dbo].[Voucher_Tbl] WHERE Code = @Code)
			BEGIN
				SELECT @VoucherId = Id, @VoucherType = VoucherType FROM [dbo].[Voucher_Tbl] WHERE Code = @Code
			END
			ELSE
				RAISERROR('Voucher does not exist',16,1)
		END

		BEGIN
			IF (@VoucherType = 0)
			BEGIN
				SELECT @DiscountType = DiscountType FROM [dbo].[Discount_Tbl] WHERE VoucherId = @VoucherId
				IF (@DiscountType = 0)
				BEGIN
					SELECT * FROM [dbo].[DiscountAmountView] WHERE Code = @Code
				END
				ELSE IF (@DiscountType = 1)
				BEGIN
					SELECT * FROM [dbo].[DiscountPercentageView] WHERE Code = @Code
				END
				ELSE IF (@DiscountType = 2)
				BEGIN
					SELECT * FROM [dbo].[DiscountUnitView] WHERE Code = @Code
				END
			END
			ELSE IF (@VoucherType = 1)
			BEGIN
				SELECT * FROM [dbo].[GiftView] WHERE Code = @Code
			END
			ELSE IF (@VoucherType = 2)
			BEGIN
				SELECT * FROM [dbo].[ValueView] WHERE Code = @Code
			END
		END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION mysavepoint
			END
	END CATCH
COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[getAllDiscount]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAllDiscount] 
	-- Add the parameters for the stored procedure here
	@DiscountType int,
	@Merchant varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN
		IF @DiscountType = 0
			BEGIN
				SELECT * FROM dbo.DiscountAmountView WHERE CreatedBy = @Merchant
			END
		ELSE IF @DiscountType = 1
			BEGIN
				SELECT * FROM dbo.DiscountPercentageView WHERE CreatedBy = @Merchant
			END
		ELSE IF @DiscountType = 2
			BEGIN
				SELECT * FROM dbo.DiscountUnitView WHERE CreatedBy = @Merchant
			END
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[getAllGift]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAllGift]
	-- Add the parameters for the stored procedure here
	@Merchant varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN
		SELECT * FROM dbo.GiftView WHERE CreatedBy = @Merchant
	END
END
GO
/****** Object:  StoredProcedure [dbo].[getAllValue]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAllValue]
	-- Add the parameters for the stored procedure here
	@Merchant varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM dbo.ValueView WHERE CreatedBy = @Merchant
END
GO
/****** Object:  StoredProcedure [dbo].[getBatchCount]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getBatchCount]
	@BatchNo varchar(50)
AS
BEGIN
	
	DECLARE @BatchId int
	DECLARE @VoucherCount int
	DECLARE @QuantityCreated int
	DECLARE @Value decimal

	--IF EXISTS(SELECT BatchNo FROM DBO.Batch_Tbl WHERE BatchNo = @BatchNo)
	--BEGIN
	--	SELECT @BatchId = Id, @VoucherCount = VoucherCount FROM dbo.Batch_Tbl WHERE BatchNo = @BatchNo
	--END
	--ELSE
	--	RAISERROR('Batch No does not exist',0,0)

	--SELECT @QuantityCreated = COUNT(BatchId) FROM dbo.Voucher_Tbl WHERE BatchId = @BatchId

	SELECT @QuantityCreated = QuantityCreated, @VoucherCount = VoucherCount FROM dbo.Batch_Tbl WHERE BatchNo = @BatchNo

	SET @Value = CAST ((CAST (@QuantityCreated AS DECIMAL) / CAST (@VoucherCount AS DECIMAL)) * 100 AS DECIMAL)	

	RETURN @Value
END
GO
/****** Object:  StoredProcedure [dbo].[update]    Script Date: 2/4/2019 6:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[update]
(
    @VoucherCode varchar(max),
    @ExpirationDate datetime
)
AS
	DECLARE @CreatedBy varchar(50)

	
	UPDATE [dbo].[Voucher_Tbl] SET ExpirationDate = @ExpirationDate WHERE Code = @VoucherCode
	
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Voucher_Tbl"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 232
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "Campaign_Tbl"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Discount_Tbl"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Redemption_Tbl"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 643
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DiscountAmountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DiscountAmountView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Voucher_Tbl"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 232
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "Campaign_Tbl"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Discount_Tbl"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Redemption_Tbl"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 643
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DiscountPercentageView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DiscountPercentageView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Voucher_Tbl"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "Campaign_Tbl"
            Begin Extent = 
               Top = 294
               Left = 38
               Bottom = 390
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Discount_Tbl"
            Begin Extent = 
               Top = 294
               Left = 246
               Bottom = 424
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Redemption_Tbl"
            Begin Extent = 
               Top = 294
               Left = 454
               Bottom = 424
               Right = 643
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DiscountUnitView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DiscountUnitView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Voucher_Tbl"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 232
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "Campaign_Tbl"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Gift_Tbl"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Redemption_Tbl"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 643
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GiftView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GiftView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Voucher_Tbl"
            Begin Extent = 
               Top = 102
               Left = 265
               Bottom = 232
               Right = 435
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "Campaign_Tbl"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Value_Tbl"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Redemption_Tbl"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 232
               Right = 227
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1335
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ValueView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ValueView'
GO
USE [master]
GO
ALTER DATABASE [VoucherzDb] SET  READ_WRITE 
GO
