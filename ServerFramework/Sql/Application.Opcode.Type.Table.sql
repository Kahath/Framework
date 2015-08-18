USE [Zavrsni]
GO
/****** Object:  Table [Application].[Opcode.Type]    Script Date: 18/8/2015 21:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Application].[Opcode.Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [CS_Application.Opcode.Type_Active]  DEFAULT (CONVERT([bit],(1))),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [CS_Application.Opcode.Type_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL,
	[DateDeactivated] [datetime] NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Application.Opcode.Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [Application].[Opcode.Type] ON 

INSERT [Application].[Opcode.Type] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (1, 1, CAST(N'2015-08-01 08:31:02.780' AS DateTime), NULL, NULL, N'NotUsed')
INSERT [Application].[Opcode.Type] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (2, 1, CAST(N'2015-08-01 08:31:02.780' AS DateTime), NULL, NULL, N'Development')
INSERT [Application].[Opcode.Type] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (4, 1, CAST(N'2015-08-01 08:31:02.780' AS DateTime), NULL, NULL, N'Test')
INSERT [Application].[Opcode.Type] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (8, 1, CAST(N'2015-08-01 08:31:02.780' AS DateTime), NULL, NULL, N'Stable')
INSERT [Application].[Opcode.Type] ([ID], [Active], [DateCreated], [DateModified], [DateDeactivated], [Name]) VALUES (16, 1, CAST(N'2015-08-01 08:31:02.780' AS DateTime), NULL, NULL, N'Release')
SET IDENTITY_INSERT [Application].[Opcode.Type] OFF
