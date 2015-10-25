﻿/*
 * Copyright (c) 2015. Kahath.
 * Licensed under MIT license.
 */

using ServerFramework.Database.Base.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerFramework.Database.Model.Application.PacketLog
{
	[Table("Packet.Log", Schema = "Application")]
	public class PacketLogModel : EntityBase
	{
		#region Properties

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID							{ get; set; }
		public string IP						{ get; set; }
		public int? ClientID					{ get; set; }
		public int? Size						{ get; set; }
		public int PacketLogTypeID				{ get; set; }
		public int? Opcode						{ get; set; }
		public string Message					{ get; set; }

		[ForeignKey("PacketLogTypeID")]
		public PacketLogTypeModel PacketLogType { get; set; }

		#endregion
	}
}
