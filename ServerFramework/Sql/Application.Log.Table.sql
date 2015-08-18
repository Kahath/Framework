USE [Zavrsni]
GO
/****** Object:  Table [Application].[Log]    Script Date: 18/8/2015 21:40:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Application].[Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [CS_Application.Log_Active]  DEFAULT (CONVERT([bit],(1))),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [CS_Application.Log_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL,
	[DateDeactivated] [datetime] NULL,
	[LogTypeID] [int] NOT NULL,
	[Message] [varchar](max) NULL,
 CONSTRAINT [PK_Application.Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [Application].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Packet.Log_Log.Type] FOREIGN KEY([LogTypeID])
REFERENCES [Application].[Log.Type] ([ID])
GO
ALTER TABLE [Application].[Log] CHECK CONSTRAINT [FK_Packet.Log_Log.Type]
GO
