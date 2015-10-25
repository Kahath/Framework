﻿/*
 * Copyright (c) 2015. Kahath.
 * Licensed under MIT license.
 */

using ServerFramework.Commands.Base;
using ServerFramework.Database.Base.Repository;
using ServerFramework.Database.Context;
using ServerFramework.Database.Model.Application.Command;
using ServerFramework.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServerFramework.Database.Repository
{
	public class CommandRepository : RepositoryBase<CommandModel>
	{
		#region Properties
		
		public new ApplicationContext Context
		{
			get { return base.Context as ApplicationContext; }
		}

		#endregion

		#region Constructors

		public CommandRepository(ApplicationContext context)
			: base(context)
		{

		}

		#endregion

		#region Methods

		#region UpdateSubCommands

		public void UpdateSubCommands(Command command, CommandModel parent)
		{
			if (parent != null)
			{
				IEnumerable<CommandModel> subCommands = Context.Commands
					.Where(x => x.Parent.ID == parent.ID && x.Active);

				if (command.SubCommands != null && command.SubCommands.Any())
				{
					foreach (Command c in command.SubCommands)
					{
						CommandModel commandModel = null;

						if (!subCommands.Any(x => x.Name == c.Name))
						{
							commandModel = new CommandModel(c);
							commandModel.Parent = parent;

							Context.Commands.Add(commandModel);
						}
						else
						{
							commandModel = subCommands.FirstOrDefault(x => x.Name == c.Name);
							Context.Entry(commandModel).State = EntityState.Modified;
						}

						UpdateSubCommands(c, commandModel);
					}
				}
			}
		}

		#endregion

		#region UpdateCommandInfo
		
		public void UpdateCommandInfo(Command command, CommandModel commandModel)
		{
			if(command != null && commandModel != null)
			{
				command.Model = commandModel;
				command.CommandLevel = (CommandLevel)commandModel.CommandLevelID;
				command.Description = commandModel.Description;
			}

			IEnumerable<CommandModel> subCommands = Context.Commands
				.Where(x => x.ParentID == commandModel.ID && x.Active).ToList();

			if(command.SubCommands != null && command.SubCommands.Any())
			{
				foreach(CommandModel cm in subCommands)
				{
					Command c = command.SubCommands.FirstOrDefault(x => x.Name == cm.Name);

					if (c != null)
					{
						UpdateCommandInfo(c, cm);
					}
				}
			}
		}

		#endregion

		#endregion
	}
}
