USE [Zavrsni]
GO
/****** Object:  Table [Application].[Command]    Script Date: 18/8/2015 21:40:54 ******/
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
	[CommandLevelID] [int] NULL,
	[ParentID] [int] NULL,
	[Description] [text] NULL,
	[AssemblyName] [varchar](255) NULL,
	[TypeName] [varchar](500) NULL,
	[MethodName] [varchar](255) NULL,
 CONSTRAINT [PK_Application.Command] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [Application].[Command]  WITH CHECK ADD  CONSTRAINT [FK_Application.Command_Command] FOREIGN KEY([ParentID])
REFERENCES [Application].[Command] ([ID])
GO
ALTER TABLE [Application].[Command] CHECK CONSTRAINT [FK_Application.Command_Command]
GO
ALTER TABLE [Application].[Command]  WITH CHECK ADD  CONSTRAINT [FK_Application.Command_Command.Level] FOREIGN KEY([CommandLevelID])
REFERENCES [Application].[Command.Level] ([ID])
GO
ALTER TABLE [Application].[Command] CHECK CONSTRAINT [FK_Application.Command_Command.Level]
GO
