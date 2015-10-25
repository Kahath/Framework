﻿/*
 * Copyright (c) 2015. Kahath.
 * Licensed under MIT license.
 */

using ServerFramework.Commands.Base;
using ServerFramework.Database.Base.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerFramework.Database.Model.Application.Command
{
	[Table("Command", Schema = "Application")]
	public class CommandModel : AssemblyEntityBase
	{
		#region Properties

		[Key]
		public int ID							{ get; set; }

		[StringLength(50)]
		public string Name						{ get; set; }
		public string Description				{ get; set; }
		public int? CommandLevelID				{ get; set; }
		public int? ParentID					{ get; set; }

		[ForeignKey("CommandLevelID")]
		public CommandLevelModel CommandLevel	{ get; set; }

		[ForeignKey("ParentID")]
		public CommandModel Parent				{ get; set; }

		#endregion

		#region Constructors

		public CommandModel()
		{

		}

		public CommandModel(CommandHandlerBase commandHandler)
		{
			Name = commandHandler.Name;
			Description = commandHandler.Description;
			CommandLevelID = (int)commandHandler.Level;
		}

		public CommandModel(ServerFramework.Commands.Base.Command command)
		{
			Name = command.Name;
			Description = command.Description;
			CommandLevelID = (int)command.CommandLevel;
		}

		#endregion
	}
}
