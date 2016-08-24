USE [hanhuynhba_H094_onlinestore]
GO
/****** Object:  Table [dbo].[ecom_ProductGroups]    Script Date: 8/24/2016 6:58:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ecom_ProductGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](200) NOT NULL,
	[Description] [nchar](500) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ecom_ProductGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ecom_Products]    Script Date: 8/24/2016 6:58:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ecom_Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NULL,
	[Unit] [int] NULL,
	[BrandId] [int] NULL,
	[CoverImageId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Description2] [nvarchar](max) NULL,
	[TotalView] [int] NULL,
	[TotalBuy] [int] NULL,
	[Tags] [nvarchar](200) NULL,
	[IsNewProduct] [bit] NOT NULL,
	[IsBestSellProduct] [bit] NOT NULL,
	[SortOrder] [int] NULL,
	[Status] [int] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ecom_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ecom_Products_ProductGroups]    Script Date: 8/24/2016 6:58:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ecom_Products_ProductGroups](
	[ProductId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_ecom_Products_ProductGroups] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ecom_Products]  WITH CHECK ADD  CONSTRAINT [FK_ecom_Products_ecom_Brands] FOREIGN KEY([BrandId])
REFERENCES [dbo].[ecom_Brands] ([Id])
GO
ALTER TABLE [dbo].[ecom_Products] CHECK CONSTRAINT [FK_ecom_Products_ecom_Brands]
GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups]  WITH CHECK ADD  CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_ProductGroups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[ecom_ProductGroups] ([Id])
GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups] CHECK CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_ProductGroups]
GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups]  WITH CHECK ADD  CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ecom_Products] ([Id])
GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups] CHECK CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_Products]
GO
