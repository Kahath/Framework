USE [Zavrsni]
GO
/****** Object:  Table [Application].[Packet.Log.Type]    Script Date: 18/8/2015 21:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Application].[Packet.Log.Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [CS_Application.Packet.Log.Type_Active]  DEFAULT (CONVERT([bit],(1))),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [CS_Application.Packet.Log.Type_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL,
	[DateDeactivated] [datetime] NULL,
	[Name] [varchar](10) NULL,
 CONSTRAINT [PK_Application.Packet.Log.Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [Application].[Packet.Log.Type] ON 

INSERT [Application].[Packet.Log.Type] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (1, 1, CAST(N'2015-04-19 14:18:36.840' AS DateTime), NULL, NULL, N'CMSG')
INSERT [Application].[Packet.Log.Type] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (2, 1, CAST(N'2015-04-19 14:18:39.910' AS DateTime), NULL, NULL, N'SMSG')
SET IDENTITY_INSERT [Application].[Packet.Log.Type] OFF
