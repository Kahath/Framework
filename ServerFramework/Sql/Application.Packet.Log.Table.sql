USE [Zavrsni]
GO
/****** Object:  Table [Application].[Packet.Log]    Script Date: 21/4/2015 20:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Application].[Packet.Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[DateDeactivated] [datetime] NULL,
	[IP] [varchar](16) NULL,
	[ClientID] [int] NULL,
	[Size] [int] NULL,
	[PacketLogTypeID] [int] NOT NULL,
	[Opcode] [int] NULL,
	[Message] [varchar](max) NULL,
 CONSTRAINT [PK_Application.Packet.Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [Application].[Packet.Log] ADD  CONSTRAINT [CS_Application.Packet.Log_Active]  DEFAULT (CONVERT([bit],(1))) FOR [Active]
GO
ALTER TABLE [Application].[Packet.Log] ADD  CONSTRAINT [CS_Application.Packet.Log_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [Application].[Packet.Log]  WITH CHECK ADD  CONSTRAINT [FK_Packet.Log_Packet.Log.Type] FOREIGN KEY([PacketLogTypeID])
REFERENCES [Application].[Packet.Log.Type] ([ID])
GO
ALTER TABLE [Application].[Packet.Log] CHECK CONSTRAINT [FK_Packet.Log_Packet.Log.Type]
GO
