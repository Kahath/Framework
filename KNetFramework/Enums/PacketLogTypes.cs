﻿/*
 * Copyright © Kahath 2015
 * Licensed under MIT license.
 */

using System;

namespace KNetFramework.Enums
{
	[Flags]
	public enum PacketLogTypes : byte
	{
		None	= 0x00,
		CMSG	= 0x01,
		SMSG	= 0x02,
	};
}
