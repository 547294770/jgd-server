USE [JGD]
GO
/****** Object:  Table [dbo].[WXUser]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WXUser](
	[openid] [varchar](36) NOT NULL,
	[nickname] [nvarchar](50) NOT NULL,
	[sex] [int] NOT NULL,
	[province] [nvarchar](50) NOT NULL,
	[city] [nvarchar](50) NOT NULL,
	[country] [nvarchar](50) NOT NULL,
	[headimgurl] [varchar](200) NOT NULL,
	[privilege] [nvarchar](50) NOT NULL,
	[unionid] [varchar](50) NOT NULL,
	[createtime] [datetime] NOT NULL,
 CONSTRAINT [PK_WXUser_1] PRIMARY KEY CLUSTERED 
(
	[openid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserID] [varchar](36) NOT NULL,
	[Token] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[User](
	[ID] [varchar](36) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[CompanyAddress] [nvarchar](50) NOT NULL,
	[Tel] [nvarchar](20) NOT NULL,
	[Contact] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ToUsers]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ToUsers](
	[TemplateID] [varchar](100) NOT NULL,
	[OpenID] [varchar](50) NOT NULL,
	[NickName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ToUsers] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC,
	[OpenID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProcessingOrder]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProcessingOrder](
	[ID] [varchar](36) NOT NULL,
	[OrderNo] [varchar](30) NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[UserID] [varchar](36) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UpdateAt] [datetime] NOT NULL,
	[DelType] [int] NOT NULL,
	[PickType] [int] NOT NULL,
	[Pic] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ProcessingOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Type=OrderStatus]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessingOrder', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Type=DeliveryType]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessingOrder', @level2type=N'COLUMN',@level2name=N'DelType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Type=PickUpType]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessingOrder', @level2type=N'COLUMN',@level2name=N'PickType'
GO
/****** Object:  Table [dbo].[ProcessingFee]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProcessingFee](
	[ID] [varchar](36) NOT NULL,
	[FeeNo] [varchar](50) NOT NULL,
	[SourceID] [varchar](36) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[ProcessingNo] [varchar](30) NOT NULL,
	[UserID] [varchar](36) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProcessingFee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Type=BillType]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProcessingFee', @level2type=N'COLUMN',@level2name=N'Type'
GO
/****** Object:  Table [dbo].[PickUpOrder]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PickUpOrder](
	[ID] [varchar](36) NOT NULL,
	[SourceID] [varchar](36) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[PickUpAt] [date] NOT NULL,
	[TimeSection] [varchar](50) NOT NULL,
	[VehicleInfo] [nvarchar](200) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[OrderNo] [varchar](30) NOT NULL,
	[ProcessingNo] [varchar](30) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_PickUpOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Type=OrderType]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PickUpOrder', @level2type=N'COLUMN',@level2name=N'Type'
GO
/****** Object:  Table [dbo].[DeliveryOrder]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeliveryOrder](
	[ID] [varchar](36) NOT NULL,
	[SourceID] [varchar](36) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[DeliveryAt] [date] NOT NULL,
	[TimeSection] [varchar](50) NOT NULL,
	[VehicleInfo] [nvarchar](200) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UserID] [varchar](36) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[OrderNo] [varchar](30) NOT NULL,
	[ProcessingNo] [varchar](30) NOT NULL,
 CONSTRAINT [PK_DeliveryOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Type=OrderType]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeliveryOrder', @level2type=N'COLUMN',@level2name=N'Type'
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [varchar](36) NOT NULL,
	[UserID] [varchar](36) NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[Contact] [nvarchar](50) NOT NULL,
	[Tel] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Attachment](
	[ID] [varchar](36) NOT NULL,
	[SourceID] [varchar](36) NOT NULL,
	[FilePath] [nvarchar](100) NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FileSize] [int] NOT NULL,
	[UpdateAt] [datetime] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Admin](
	[ID] [varchar](36) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[NickName] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccessToken]    Script Date: 11/29/2019 23:59:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccessToken](
	[Token] [varchar](200) NOT NULL,
	[Expired] [datetime] NOT NULL,
 CONSTRAINT [PK_AccessToken] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_Attachment_Name]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_Name]  DEFAULT ((0)) FOR [Name]
GO
/****** Object:  Default [DF_Attachment_FileSize]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_FileSize]  DEFAULT ((0)) FOR [FileSize]
GO
/****** Object:  Default [DF_Attachment_UpdateAt]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_UpdateAt]  DEFAULT (getdate()) FOR [UpdateAt]
GO
/****** Object:  Default [DF_Attachment_Width]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_Width]  DEFAULT ((0)) FOR [Width]
GO
/****** Object:  Default [DF_Attachment_Height]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_Height]  DEFAULT ((0)) FOR [Height]
GO
/****** Object:  Default [DF_DeliveryOrder_DeliveryTimeSection]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[DeliveryOrder] ADD  CONSTRAINT [DF_DeliveryOrder_DeliveryTimeSection]  DEFAULT ('') FOR [TimeSection]
GO
/****** Object:  Default [DF_PickUpOrder_TimeSection]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[PickUpOrder] ADD  CONSTRAINT [DF_PickUpOrder_TimeSection]  DEFAULT ('') FOR [TimeSection]
GO
/****** Object:  Default [DF_ProcessingFee_UserID]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[ProcessingFee] ADD  CONSTRAINT [DF_ProcessingFee_UserID]  DEFAULT ('') FOR [UserID]
GO
/****** Object:  Default [DF_ProcessingFee_UserName]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[ProcessingFee] ADD  CONSTRAINT [DF_ProcessingFee_UserName]  DEFAULT ('') FOR [UserName]
GO
/****** Object:  Default [DF_ProcessingOrder_UpdateAt]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[ProcessingOrder] ADD  CONSTRAINT [DF_ProcessingOrder_UpdateAt]  DEFAULT (getdate()) FOR [UpdateAt]
GO
/****** Object:  Default [DF_ProcessingOrder_DelType]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[ProcessingOrder] ADD  CONSTRAINT [DF_ProcessingOrder_DelType]  DEFAULT ((0)) FOR [DelType]
GO
/****** Object:  Default [DF_ProcessingOrder_PickType]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[ProcessingOrder] ADD  CONSTRAINT [DF_ProcessingOrder_PickType]  DEFAULT ((0)) FOR [PickType]
GO
/****** Object:  Default [DF_ProcessingOrder_Pic]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[ProcessingOrder] ADD  CONSTRAINT [DF_ProcessingOrder_Pic]  DEFAULT ('') FOR [Pic]
GO
/****** Object:  Default [DF_WXUser_createtime]    Script Date: 11/29/2019 23:59:45 ******/
ALTER TABLE [dbo].[WXUser] ADD  CONSTRAINT [DF_WXUser_createtime]  DEFAULT (getdate()) FOR [createtime]
GO
