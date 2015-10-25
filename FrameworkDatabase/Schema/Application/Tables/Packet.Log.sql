﻿CREATE TABLE [Application].[Packet.Log] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [Active]          BIT           CONSTRAINT [CS_Application.Packet.Log_Active] DEFAULT (CONVERT([bit],(1))) NOT NULL,
    [DateCreated]     DATETIME      CONSTRAINT [CS_Application.Packet.Log_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified]    DATETIME      NULL,
    [DateDeactivated] DATETIME      NULL,
    [IP]              VARCHAR (16)  NULL,
    [ClientID]        INT           NULL,
    [Size]            INT           NULL,
    [PacketLogTypeID] INT           NOT NULL,
    [Opcode]          INT           NULL,
    [Message]         VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Application.Packet.Log] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Packet.Log_Packet.Log.Type] FOREIGN KEY ([PacketLogTypeID]) REFERENCES [Application].[Packet.Log.Type] ([ID])
);

