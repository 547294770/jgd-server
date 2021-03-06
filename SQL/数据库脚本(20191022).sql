USE [JGD]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 10/22/2019 01:24:59 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 10/22/2019 01:24:59 ******/
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
INSERT [dbo].[User] ([ID], [UserName], [PassWord], [CreateAt], [CompanyName], [CompanyAddress], [Tel], [Contact]) VALUES (N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'1111', CAST(0x0000AACD0093371B AS DateTime), N'佛山卷板加工厂', N'佛山兰石', N'19833333333', N'张可飞')
INSERT [dbo].[User] ([ID], [UserName], [PassWord], [CreateAt], [CompanyName], [CompanyAddress], [Tel], [Contact]) VALUES (N'D338A59D-E5A6-416B-BC8B-FAA6EE2F0FCA', N'test1', N'1111', CAST(0x0000AACD00932ECB AS DateTime), N'佛山卷板加工厂', N'佛山兰石', N'19833333333', N'张可飞')
/****** Object:  Table [dbo].[ProcessingOrder]    Script Date: 10/22/2019 01:24:59 ******/
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
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'106a43c6-e5aa-431d-83b1-a3269b10e355', N'20190926000219', N'444455', CAST(0x0000AAD40000A2F8 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD40000A2F8 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'10BE8BD8-7EE0-4EC3-9687-AC0CDEF1BCA0', N'201909170004', N'加工规格324234', CAST(0x0000AACB0178D55F AS DateTime), 1, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'10dabaa4-107a-48f7-9e69-b4a8b0d2836e', N'20190925235940', N'sfsdfdsf', CAST(0x0000AAD3018B6B5E AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD3018B6B5E AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', N'201909170009', N'加工规格324234', CAST(0x0000AACB017900FC AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'181f4eee-9d2a-4791-ae8e-bc4b6c82c762', N'20190926000228', N'444455', CAST(0x0000AAD40000AE41 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD40000AE41 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'27785b43-0236-496e-a96e-1871b0950af4', N'20191014005322', N'法师法萨芬发顺丰', CAST(0x0000AAE6000EA8D3 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE6000EA8D3 AS DateTime), 2, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'32FB3F80-DA12-4FF0-8F1F-FB48DFAAF68E', N'201909170001', N'加工规格17000', CAST(0x0000AAC6000AFE00 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'34ef340a-f44a-4913-98b5-102460880713', N'20191014005226', N'啊到时分分', CAST(0x0000AAE6000E66D5 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE6000E66D5 AS DateTime), 1, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'35f849c7-bdc1-42e9-8303-6f894e97aebc', N'20190926003304', N'fdafasf', CAST(0x0000AAD4000915D0 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD4000915D0 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'3B485919-4418-4FE6-A619-EDF89601C8E5', N'201909170002', N'加工规格rewrwr', CAST(0x0000AAC6000B068B AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'4ac22b02-960f-493e-843f-50c6156201d8', N'20190926003226', N'', CAST(0x0000AAD40008E8B9 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD40008E8B9 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'50817790-9505-4B39-B93A-5037B4E285E9', N'201909170007', N'加工规格324234', CAST(0x0000AACB0178F1E5 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'5140932A-C858-4531-960F-69174F9DCF43', N'201909170012', N'加工规格324234', CAST(0x0000AACB01791CE9 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'515f95bc-aba3-432b-a053-34443514eae5', N'20191013182647', N'法师法第三方', CAST(0x0000AAE5012FFD4F AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAEC01180ADF AS DateTime), 1, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'53a249a7-a777-4407-8c39-0692a5f854cc', N'20191013182639', N'范德萨发顺丰', CAST(0x0000AAE5012FF38E AS DateTime), 11, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAED00D6A1D4 AS DateTime), 1, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'5b159e2d-c05e-4c05-8ddb-c376cc4d68c5', N'20191013183157', N'法第三方', CAST(0x0000AAE50131684D AS DateTime), 1, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE50131684D AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', N'201909170005', N'加工规格324234', CAST(0x0000AACB0178E01B AS DateTime), 14, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAEE00121BE3 AS DateTime), 1, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'61B8149C-BB52-4957-8F2C-9DF8582914AC', N'201909170003', N'加工规格324234', CAST(0x0000AAC6000B1045 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'6a7f6087-fe39-48c8-a351-ae21443875ab', N'20191015011743', N'SDFSF', CAST(0x0000AAE70015589C AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE70015589C AS DateTime), 1, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'738f62f0-d110-4a0a-902c-b63f84f2367a', N'20191014005428', N'范德萨发的说法', CAST(0x0000AAE6000EF5C8 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE6000EF5C8 AS DateTime), 1, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'7F4AEF9B-BCC2-4AEF-822C-83309BEEDE6B', N'201909170011', N'加工规格324234', CAST(0x0000AACB0179136B AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'877e90a3-05ed-4e77-895d-343f49eea8eb', N'20190925235507', N'发发发', CAST(0x0000AAD3018A2BB5 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD3018A2BB5 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'8c3e9b3a-4244-43b4-8d93-fdd8612a9394', N'20190926003613', N'放大放大时发生', CAST(0x0000AAD40009F330 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD40009F330 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'8f744312-6ac1-478c-ab94-5164bd9cb883', N'20191013182918', N'发生了分解了机', CAST(0x0000AAE50130AE9F AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE50130AE9F AS DateTime), 2, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'97528dc0-3cdd-4ba1-8fd6-e86f5227da38', N'20190926000558', N'法法师法发生的范德萨', CAST(0x0000AAD40001A453 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD40001A453 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'9F83BA7D-957A-4C4F-BF96-2F0E2D5CFAFA', N'201909170010', N'加工规格324234', CAST(0x0000AACB01790A2A AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'A6FE18BD-3D87-43A3-B0C0-8E96A5A740E2', N'201909170008', N'加工规格324234', CAST(0x0000AACB0178F907 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'af69ce20-8acc-4ba7-9356-c5b72dacb39e', N'20190926003444', N'的房间爱咖啡即可', CAST(0x0000AAD400098A64 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD400098A64 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'B4D6DE41-A710-4FC2-B442-211D37B11630', N'201909170013', N'加工规格324234', CAST(0x0000AACB017B732B AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'B747D70F-18E4-4069-848B-0475DECFB128', N'201909170006', N'加工规格324234', CAST(0x0000AACB0178E820 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD000D7A673 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'bb69e077-a8d9-4eb5-b2f5-cda8898d0899', N'20191013182335', N'法第三方', CAST(0x0000AAE5012F1BFE AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE5012F1BFE AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'be40b522-1342-45dc-b9f7-3f6d6032fd3c', N'20191013183231', N'发顺丰大师傅', CAST(0x0000AAE5013190B9 AS DateTime), 1, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE5013190B9 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'd4586cb7-ab54-4bce-9d5e-f6d664461988', N'20191013182708', N'发送发放', CAST(0x0000AAE50130167B AS DateTime), 5, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAED010B20C8 AS DateTime), 2, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'd8ff5df4-9d29-48c4-9571-0abf5f1954dd', N'20190927013526', N'发的卡上飞机垃圾费', CAST(0x0000AAD5001A3749 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD5001A3749 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'dce0de39-71f6-45e5-8b6a-52338879a466', N'20191013182128', N'EST', CAST(0x0000AAE5012E874E AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAE5012E874E AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'e3fb013c-6ca5-4a01-80aa-db769e11faa4', N'20190926000146', N'fdafafaf', CAST(0x0000AAD400007D51 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD400007D51 AS DateTime), 0, 0)
INSERT [dbo].[ProcessingOrder] ([ID], [OrderNo], [Content], [CreateAt], [Status], [UserID], [UserName], [UpdateAt], [DelType], [PickType]) VALUES (N'fe2aed90-7aef-465b-9af8-16e18ba6215c', N'20190926000328', N'666666666', CAST(0x0000AAD40000F4E8 AS DateTime), 0, N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', CAST(0x0000AAD40000F4E8 AS DateTime), 0, 0)
/****** Object:  Table [dbo].[ProcessingFee]    Script Date: 10/22/2019 01:24:59 ******/
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
INSERT [dbo].[ProcessingFee] ([ID], [FeeNo], [SourceID], [Type], [Content], [CreateAt], [ProcessingNo]) VALUES (N'37BF84C5-16A1-4329-95E5-4678061CFD91', N'201909210002', N'32FB3F80-DA12-4FF0-8F1F-FB48DFAAF68E', 0, N'加工费明细', CAST(0x0000AACF000B28CF AS DateTime), N'201909170001')
INSERT [dbo].[ProcessingFee] ([ID], [FeeNo], [SourceID], [Type], [Content], [CreateAt], [ProcessingNo]) VALUES (N'7E2B3F05-1A50-4274-9E1C-CE5F6186ECFD', N'201909210001', N'3B485919-4418-4FE6-A619-EDF89601C8E5', 0, N'加工费明细', CAST(0x0000AACF000B0668 AS DateTime), N'201909170002')
INSERT [dbo].[ProcessingFee] ([ID], [FeeNo], [SourceID], [Type], [Content], [CreateAt], [ProcessingNo]) VALUES (N'b58f763d-fe4e-4ebb-803d-673c8c52f30b', N'20191022005006', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', 0, N'3333', CAST(0x0000AAEE000DC308 AS DateTime), N'201909170005')
/****** Object:  Table [dbo].[PickUpOrder]    Script Date: 10/22/2019 01:24:59 ******/
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
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'2FD4729E-5520-48CF-B641-699DC090E82C', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170007', N'201909170009', CAST(0x0000AACC000DB5A1 AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'33a78174-9e17-4f62-b29a-8e2717bfd476', N'53a249a7-a777-4407-8c39-0692a5f854cc', 0, N'666', CAST(0x49400B00 AS Date), N'666', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191021130126', N'20191013182639', CAST(0x0000AAED00D6A19A AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'37B268B5-DE82-4CF5-9FCB-BAD36B15C913', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170006', N'201909170009', CAST(0x0000AACC000DB5A1 AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'4A5E4180-159D-4446-A5EC-551833515140', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170005', N'201909170009', CAST(0x0000AACC000DB5A1 AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'514E3299-03CD-4F24-9EBA-C7547D8E161C', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170004', N'201909170009', CAST(0x0000AACC000DB5A1 AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'6801C192-44EE-4386-9835-ED9505C2A018', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170003', N'201909170009', CAST(0x0000AACC000DB5A1 AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'78392473-9A64-49B1-9B01-C1D3FD62DE72', N'3B485919-4418-4FE6-A619-EDF89601C8E5', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170002', N'201909170002', CAST(0x0000AACC000DB5A1 AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'BC0F4178-7328-46A9-8740-35A4A3F419A7', N'32FB3F80-DA12-4FF0-8F1F-FB48DFAAF68E', 1, N'送货内容。。，卷板', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170001', N'201909170001', CAST(0x0000AACC000DB5A1 AS DateTime))
INSERT [dbo].[PickUpOrder] ([ID], [SourceID], [Type], [Content], [PickUpAt], [VehicleInfo], [UserID], [UserName], [OrderNo], [ProcessingNo], [CreateAt]) VALUES (N'bccc0fb2-3a8f-407b-916d-0689ed00d36a', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', 0, N'888', CAST(0x49400B00 AS Date), N'9999', N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191021224957', N'201909170005', CAST(0x0000AAED01784526 AS DateTime))
/****** Object:  Table [dbo].[DeliveryOrder]    Script Date: 10/22/2019 01:24:59 ******/
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
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'28ebcee4-2c6e-4aa7-b676-ecc10f477ed6', N'61B8149C-BB52-4957-8F2C-9DF8582914AC', 0, N'22255', CAST(0x2E400B00 AS Date), N'6666', CAST(0x0000AAD0015DD2BE AS DateTime), N'', N'', N'yyyyMMddHHmmsss', N'201909170003')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'2FD4729E-5520-48CF-B641-699DC090E82C', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', CAST(0x0000AACC000DB5A1 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170007', N'201909170009')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'37B268B5-DE82-4CF5-9FCB-BAD36B15C913', N'10BE8BD8-7EE0-4EC3-9687-AC0CDEF1BCA0', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', CAST(0x0000AACC000DB5A1 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170006', N'201909170004')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'4A5E4180-159D-4446-A5EC-551833515140', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', CAST(0x0000AACC000DB5A1 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170005', N'201909170009')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'514E3299-03CD-4F24-9EBA-C7547D8E161C', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', CAST(0x0000AACC000DB5A1 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170004', N'201909170009')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'6801C192-44EE-4386-9835-ED9505C2A018', N'111B8DFC-1F7F-43E6-838A-495A87B49C6F', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', CAST(0x0000AACC000DB5A1 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170003', N'201909170009')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'71f1bf32-45bb-451c-b643-33c65c8134ff', N'515f95bc-aba3-432b-a053-34443514eae5', 0, N'5555', CAST(0x48400B00 AS Date), N'5555', CAST(0x0000AAEC01180AD7 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191020165936', N'20191013182647')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'78392473-9A64-49B1-9B01-C1D3FD62DE72', N'3B485919-4418-4FE6-A619-EDF89601C8E5', 1, N'送货内容。。，卷板111', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', CAST(0x0000AACC000DB5A1 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170002', N'201909170002')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'8bd7feeb-2c4c-4cf6-a21d-6c6c89ccb260', N'd4586cb7-ab54-4bce-9d5e-f6d664461988', 1, N'333', CAST(0x2D400B00 AS Date), N'333', CAST(0x0000AAED010B20C9 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191021161235', N'20191013182708')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'9bad08e3-f314-40d0-8da3-7570fdd14999', N'738f62f0-d110-4a0a-902c-b63f84f2367a', 0, N'6666', CAST(0x48400B00 AS Date), N'66666', CAST(0x0000AAEC01148987 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191020164650', N'20191014005428')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'aa586ca2-b026-4015-b0b5-fe18e5cbd299', N'34ef340a-f44a-4913-98b5-102460880713', 0, N'9999', CAST(0x49400B00 AS Date), N'9999', CAST(0x0000AAEC0115850A AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191020165025', N'20191014005226')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'BC0F4178-7328-46A9-8740-35A4A3F419A7', N'32FB3F80-DA12-4FF0-8F1F-FB48DFAAF68E', 1, N'送货内容。。，卷板', CAST(0x27400B00 AS Date), N'车牌：粤EYF987，联系人：张生，手机：13789876764', CAST(0x0000AACC000DB5A1 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'201909170001', N'201909170001')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'c3ae08ac-0c55-4c19-beb6-5ff73d61a8b7', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', 0, N'555', CAST(0x4A400B00 AS Date), N'555', CAST(0x0000AAED0177811B AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191021224710', N'201909170005')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'dfd1c65b-d471-4399-be13-240bc170962f', N'6a7f6087-fe39-48c8-a351-ae21443875ab', 1, N'4444', CAST(0x49400B00 AS Date), N'5555', CAST(0x0000AAEC011335D5 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191020164200', N'20191015011743')
INSERT [dbo].[DeliveryOrder] ([ID], [SourceID], [Type], [Content], [DeliveryAt], [VehicleInfo], [CreateAt], [UserID], [UserName], [OrderNo], [ProcessingNo]) VALUES (N'f819e3e6-cdee-48a3-80b0-eadaa4736ccc', N'53a249a7-a777-4407-8c39-0692a5f854cc', 0, N'44444', CAST(0x49400B00 AS Date), N'44444', CAST(0x0000AAED00AEBC76 AS DateTime), N'4A355901-3556-4B7D-9E54-9FE03C1B99F8', N'test2', N'20191021103611', N'20191013182639')
/****** Object:  Table [dbo].[Attachment]    Script Date: 10/22/2019 01:24:59 ******/
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
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'08e9f2c616f74971a10dbe61041b637f', N'34ef340a-f44a-4913-98b5-102460880713', N'/upload/ec6dbae579fe48cfb072fb3579325c0d.png', N'ec6dbae579fe48cfb072fb3579325c0d.png', CAST(0x0000AAEC00B54FC6 AS DateTime), N'666.png', 150917, CAST(0x0000AAEC00B54FC6 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'1143085bc6e147dc8abceb354d0eb86a', N'6a7f6087-fe39-48c8-a351-ae21443875ab', N'/upload/627adbcb592c4f05a84bc2d67f30954e.png', N'627adbcb592c4f05a84bc2d67f30954e.png', CAST(0x0000AAEC00B2F0A7 AS DateTime), N'55.png', 48472, CAST(0x0000AAEC00B2F7F5 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'1fe22dade8434f04885abd4230d0408c', N'27785b43-0236-496e-a96e-1871b0950af4', N'/upload/1555ae2a6b214df892693ab250303ae1.png', N'1555ae2a6b214df892693ab250303ae1.png', CAST(0x0000AAEC00B39B9A AS DateTime), N'345.png', 179769, CAST(0x0000AAEC00B39B9A AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'2167a91d81cc48e8a07be4a68e7c76fb', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', N'/upload/16c429ed5258443e8a8262e81f55f8aa.png', N'16c429ed5258443e8a8262e81f55f8aa.png', CAST(0x0000AAED0177614D AS DateTime), N'55.png', 48472, CAST(0x0000AAED0177614D AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'238ff4032dbd4c7cadfd13e8364b61f6', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', N'/upload/534771cda02349f7a2cabf1720469e75.png', N'534771cda02349f7a2cabf1720469e75.png', CAST(0x0000AAED0177615B AS DateTime), N'666.png', 150917, CAST(0x0000AAED0177615B AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'333fb7a0957545b99049491871a93bcf', N'515f95bc-aba3-432b-a053-34443514eae5', N'/upload/b7c6dc1c1f5a4ce4b5d6dc39a5dd008a.png', N'b7c6dc1c1f5a4ce4b5d6dc39a5dd008a.png', CAST(0x0000AAEC0117F074 AS DateTime), N'55.png', 48472, CAST(0x0000AAEC0117F074 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'35ed6ca6dad84df1bc6cf08fbd018b4d', N'8f744312-6ac1-478c-ab94-5164bd9cb883', N'/upload/d23648f5d65e41cbbf3a3ab16ae21f48.png', N'd23648f5d65e41cbbf3a3ab16ae21f48.png', CAST(0x0000AAEC00B56BC3 AS DateTime), N'123.png', 172771, CAST(0x0000AAEC00B56BC3 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'441186a9da054c96a5481c833a6e2cf8', N'53a249a7-a777-4407-8c39-0692a5f854cc', N'/upload/e3488abfb91a4268b0df8628547c8726.png', N'e3488abfb91a4268b0df8628547c8726.png', CAST(0x0000AAED00AEA24E AS DateTime), N'QQ截图20190606005630.png', 36939, CAST(0x0000AAED00AEA24E AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'4506bab3fc374d57af3afa6555cb08e7', N'10BE8BD8-7EE0-4EC3-9687-AC0CDEF1BCA0', N'/upload/722d6c4d3eee4136899d7c7773741f8c.png', N'722d6c4d3eee4136899d7c7773741f8c.png', CAST(0x0000AAD0013E58D7 AS DateTime), N'666.png', 150917, CAST(0x0000AAD0013E58D7 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'4d896f0bc1aa441d93d666e0fda45c65', N'27785b43-0236-496e-a96e-1871b0950af4', N'/upload/18436814167940c79d86aef7a9b4f2bd.png', N'18436814167940c79d86aef7a9b4f2bd.png', CAST(0x0000AAEC00B39B8A AS DateTime), N'123.png', 172771, CAST(0x0000AAEC00B39B8A AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'523237222541495d8d08faf4d66739fe', N'34ef340a-f44a-4913-98b5-102460880713', N'/upload/c16bbd343cf84b1280732a5dcc49c004.png', N'c16bbd343cf84b1280732a5dcc49c004.png', CAST(0x0000AAEC00B54FCD AS DateTime), N'123.png', 172771, CAST(0x0000AAEC00B54FCD AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'541b4987abff4b20ba454e9b785dd0eb', N'27785b43-0236-496e-a96e-1871b0950af4', N'/upload/472651100da6461e878de6bb26bac5dd.png', N'472651100da6461e878de6bb26bac5dd.png', CAST(0x0000AAEC0088E050 AS DateTime), N'345.png', 179769, CAST(0x0000AAEC0088E050 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'5d7a3c61300a4f22bd267c9640c32c4f', N'53a249a7-a777-4407-8c39-0692a5f854cc', N'/upload/0c62a686c7f44aff8e290b9b447e9f18.png', N'0c62a686c7f44aff8e290b9b447e9f18.png', CAST(0x0000AAED00A4BF87 AS DateTime), N'123.png', 172771, CAST(0x0000AAED00A4BF87 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'74a10673ff3d4142b9d0f0274d2bf417', N'738f62f0-d110-4a0a-902c-b63f84f2367a', N'/upload/cc63bf1967a64eee938e6554118f9d99.png', N'cc63bf1967a64eee938e6554118f9d99.png', CAST(0x0000AAEC00B21E7A AS DateTime), N'666.png', 150917, CAST(0x0000AAEC00B21E7A AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'898c92fd77c04668b5e56e2ffa52a1ca', N'6a7f6087-fe39-48c8-a351-ae21443875ab', N'/upload/627adbcb592c4f05a84bc2d67f30954e.png', N'627adbcb592c4f05a84bc2d67f30954e.png', CAST(0x0000AAEC00880690 AS DateTime), N'55.png', 48472, CAST(0x0000AAEC00880690 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'8cbaabef437e4519bd24a02d0515cdbf', N'53a249a7-a777-4407-8c39-0692a5f854cc', N'/upload/0c62a686c7f44aff8e290b9b447e9f18.png', N'0c62a686c7f44aff8e290b9b447e9f18.png', CAST(0x0000AAED00AEA24B AS DateTime), N'123.png', 172771, CAST(0x0000AAED00AEA24B AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'912025d395b843c9b24d51d4411f6540', N'd4586cb7-ab54-4bce-9d5e-f6d664461988', N'/upload/d6e98830ea1248fbb8773533e64b54d4.png', N'd6e98830ea1248fbb8773533e64b54d4.png', CAST(0x0000AAED00DB011E AS DateTime), N'666.png', 150917, CAST(0x0000AAED00DB011E AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'94d79461435f4f94b988f1f24edafe28', N'738f62f0-d110-4a0a-902c-b63f84f2367a', N'/upload/cc63bf1967a64eee938e6554118f9d99.png', N'cc63bf1967a64eee938e6554118f9d99.png', CAST(0x0000AAEC00887943 AS DateTime), N'666.png', 150917, CAST(0x0000AAEC00887943 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'a2e084fd3b184303a91920080ca5b28e', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', N'/upload/16c429ed5258443e8a8262e81f55f8aa.png', N'16c429ed5258443e8a8262e81f55f8aa.png', CAST(0x0000AAED01764BB4 AS DateTime), N'55.png', 48472, CAST(0x0000AAED01764BB4 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'bd66279a889f4f8cb403a6931c585e51', N'd4586cb7-ab54-4bce-9d5e-f6d664461988', N'/upload/7bda5510ebd9488da6417adfaf9fc22f.png', N'7bda5510ebd9488da6417adfaf9fc22f.png', CAST(0x0000AAED00DB011F AS DateTime), N'123.png', 172771, CAST(0x0000AAED00DB011F AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'c1659fe5e00c4781b5a02e7e80a2b16a', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', N'/upload/f85028f7ec204f5fbad288670cde6eac.png', N'f85028f7ec204f5fbad288670cde6eac.png', CAST(0x0000AAED01764ACD AS DateTime), N'345.png', 179769, CAST(0x0000AAED01764ACD AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'd0bb6ea632c14d3f8759a84217afc230', N'34ef340a-f44a-4913-98b5-102460880713', N'/upload/ec6dbae579fe48cfb072fb3579325c0d.png', N'ec6dbae579fe48cfb072fb3579325c0d.png', CAST(0x0000AAEC0088FC7E AS DateTime), N'666.png', 150917, CAST(0x0000AAEC0088FC7E AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'd2a531171a7045489a5a0345c034bd5b', N'600B61AF-FCF3-4A0F-8431-C222C37AC4A8', N'/upload/f85028f7ec204f5fbad288670cde6eac.png', N'f85028f7ec204f5fbad288670cde6eac.png', CAST(0x0000AAED01776151 AS DateTime), N'345.png', 179769, CAST(0x0000AAED01776151 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'dc50510b001f4d7180dd1f67cedf05b8', N'd4586cb7-ab54-4bce-9d5e-f6d664461988', N'/upload/d6e98830ea1248fbb8773533e64b54d4.png', N'd6e98830ea1248fbb8773533e64b54d4.png', CAST(0x0000AAED00DA8E6B AS DateTime), N'666.png', 150917, CAST(0x0000AAED00DA8E6B AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'f43c4a04aa544eeb9a6decc983ed234d', N'515f95bc-aba3-432b-a053-34443514eae5', N'/upload/b7c6dc1c1f5a4ce4b5d6dc39a5dd008a.png', N'b7c6dc1c1f5a4ce4b5d6dc39a5dd008a.png', CAST(0x0000AAEC0117C880 AS DateTime), N'55.png', 48472, CAST(0x0000AAEC0117C880 AS DateTime))
INSERT [dbo].[Attachment] ([ID], [SourceID], [FilePath], [FileName], [CreateAt], [Name], [FileSize], [UpdateAt]) VALUES (N'f7a1209b65e744e1b6a7fce4daa8fc3f', N'515f95bc-aba3-432b-a053-34443514eae5', N'/upload/a0bbd4aaf1c14970b1c891cc2121093e.png', N'a0bbd4aaf1c14970b1c891cc2121093e.png', CAST(0x0000AAEC0117F07D AS DateTime), N'my.png', 24653, CAST(0x0000AAEC0117F07D AS DateTime))
/****** Object:  Table [dbo].[Admin]    Script Date: 10/22/2019 01:24:59 ******/
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
/****** Object:  Default [DF_Attachment_Name]    Script Date: 10/22/2019 01:24:59 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_Name]  DEFAULT ((0)) FOR [Name]
GO
/****** Object:  Default [DF_Attachment_FileSize]    Script Date: 10/22/2019 01:24:59 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_FileSize]  DEFAULT ((0)) FOR [FileSize]
GO
/****** Object:  Default [DF_Attachment_UpdateAt]    Script Date: 10/22/2019 01:24:59 ******/
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_UpdateAt]  DEFAULT (getdate()) FOR [UpdateAt]
GO
/****** Object:  Default [DF_ProcessingOrder_UpdateAt]    Script Date: 10/22/2019 01:24:59 ******/
ALTER TABLE [dbo].[ProcessingOrder] ADD  CONSTRAINT [DF_ProcessingOrder_UpdateAt]  DEFAULT (getdate()) FOR [UpdateAt]
GO
/****** Object:  Default [DF_ProcessingOrder_DelType]    Script Date: 10/22/2019 01:24:59 ******/
ALTER TABLE [dbo].[ProcessingOrder] ADD  CONSTRAINT [DF_ProcessingOrder_DelType]  DEFAULT ((0)) FOR [DelType]
GO
/****** Object:  Default [DF_ProcessingOrder_PickType]    Script Date: 10/22/2019 01:24:59 ******/
ALTER TABLE [dbo].[ProcessingOrder] ADD  CONSTRAINT [DF_ProcessingOrder_PickType]  DEFAULT ((0)) FOR [PickType]
GO
