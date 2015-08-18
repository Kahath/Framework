USE [Zavrsni]
GO
/****** Object:  Table [Application].[Opcode]    Script Date: 18/8/2015 21:40:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Application].[Opcode](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [CS_Application.Opcode_Active]  DEFAULT (CONVERT([bit],(1))),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [CS_Application.Opcode_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NULL,
	[DateDeactivated] [datetime] NULL,
	[Code] [int] NOT NULL,
	[TypeID] [int] NULL,
	[Version] [int] NOT NULL,
	[Author] [varchar](50) NULL,
	[AssemblyName] [varchar](255) NOT NULL,
	[TypeName] [varchar](500) NOT NULL,
	[MethodName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Application.Opcode] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [Application].[Opcode]  WITH CHECK ADD  CONSTRAINT [FK_Opcode_Opcode.Type] FOREIGN KEY([TypeID])
REFERENCES [Application].[Opcode.Type] ([ID])
GO
ALTER TABLE [Application].[Opcode] CHECK CONSTRAINT [FK_Opcode_Opcode.Type]
GO
