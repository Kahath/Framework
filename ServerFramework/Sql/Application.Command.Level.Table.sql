USE [Zavrsni]
GO
/****** Object:  Table [Application].[Command.Level]    Script Date: 17/5/2015 14:11:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Application].[Command.Level](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [CS_Application.Command.Level_Active]  DEFAULT (CONVERT([bit],(1))),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [CS_Application.Command.Level_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL,
	[DateDeactivated] [datetime] NULL,
	[Name] [varchar](10) NULL,
 CONSTRAINT [PK_Application.Command.Level] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [Application].[Command.Level] ON 

INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (1, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'One')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (2, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Two')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (3, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Three')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (4, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Four')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (5, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Five')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (6, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Six')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (7, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Seven')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (8, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Eight')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (9, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Nine')
INSERT [Application].[Command.Level] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (10, 1, CAST(N'2015-05-17 11:54:46.260' AS DateTime), NULL, NULL, N'Ten')
SET IDENTITY_INSERT [Application].[Command.Level] OFF
