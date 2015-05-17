USE [Zavrsni]
GO
/****** Object:  Table [Application].[Command]    Script Date: 16/4/2015 19:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Application].[Command](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [CS_Application.Command_Active]  DEFAULT (CONVERT([bit],(1))),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [CS_Application.Command_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL,
	[DateDeactivated] [datetime] NULL,
	[Name] [varchar](50) NOT NULL,
	[CommandLevel] [smallint] NOT NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Application.Command] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [Application].[Command] ON 

INSERT [Application].[Command] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name], [CommandLevelID], [Description]) VALUES (1, 1, CAST(N'2015-04-12 19:14:25.650' AS DateTime), NULL, NULL, N'cls', 10, N'Usage: Clears console')
INSERT [Application].[Command] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name], [CommandLevelID], [Description]) VALUES (2, 1, CAST(N'2015-04-12 19:14:25.650' AS DateTime), NULL, NULL, N'command list', 9, N'Lists all available commands')
INSERT [Application].[Command] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name], [CommandLevelID], [Description]) VALUES (3, 1, CAST(N'2015-04-12 19:14:25.650' AS DateTime), NULL, NULL, N'help', 9, N'Usage: returns description or subcommands for wanted command')
SET IDENTITY_INSERT [Application].[Command] OFF
