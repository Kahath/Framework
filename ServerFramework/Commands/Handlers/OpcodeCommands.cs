﻿/*
 * This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using ServerFramework.Attributes.Core;
using ServerFramework.Commands.Base;
using ServerFramework.Database.Context;
using ServerFramework.Database.Model.Application.Opcode;
using ServerFramework.Enums;
using ServerFramework.Managers;
using ServerFramework.Network.Packets;
using ServerFramework.Network.Session;
using System;
using System.Linq;
using System.Reflection;

namespace ServerFramework.Commands.Handlers
{
	[Command("opcode", CommandLevel.Ten, "")]
	public class OpcodeCommands : CommandHandlerBase
	{
		#region Methods

		#region GetCommand

		protected override Command GetCommand()
		{
			Command retVal = null;

			Command[] ForceSubCommands =
			{
				new Command("both", CommandLevel.Ten, null, ForceTypeVersionHandler, ""),
				new Command("type", CommandLevel.Ten, null, ForceTypeHandler, ""),
				new Command("version", CommandLevel.Ten, null, ForceVersionHandler, ""),
			};

			Command[] OpcodeSubCommands = 
			{
				new Command("force", CommandLevel.Ten, ForceSubCommands, null, "")
			};

			retVal = new Command(Name, Level, OpcodeSubCommands, null, Description);

			return retVal;
		}

		#endregion

		#region Handlers

		#region ForceVersionHandler

		private static bool ForceVersionHandler(Client client, params string[] args)
		{
			int code = Int32.Parse(args[0]);
			int version = Int32.Parse(args[1]);

			using(ApplicationContext context = new ApplicationContext())
			{
				OpcodeModel opcode = context.Opcodes.FirstOrDefault(x => x.Code == code 
					&& x.Version == version && x.Active);

				if(opcode != null)
				{
					if(Manager.PacketMgr.PacketHandlers.ContainsKey((ushort)opcode.Code))
					{
						MethodInfo method = Manager.AssemblyMgr.GetMethod
							(
								opcode.AssemblyName
							,	opcode.TypeName
							,	opcode.MethodName
							,	typeof(Client)
							,	typeof(Packet)
							);

						if (method != null)
						{
							Manager.PacketMgr.PacketHandlers[(ushort)opcode.Code]
								= Delegate.CreateDelegate(typeof(OpcodeHandler), method) as OpcodeHandler;
						}
					}
				}
			}

			return true;
		}

		#endregion

		#region ForceTypeHandler

		private static bool ForceTypeHandler(Client client, params string[] args)
		{
			int code = Int32.Parse(args[0]);
			int opcodeType = (int)Enum.Parse(typeof(OpcodeType), args[1]);

			using (ApplicationContext context = new ApplicationContext())
			{
				OpcodeModel opcode = context.Opcodes
					.Where(x => x.Code == code && x.TypeID == opcodeType && x.Active)
					.OrderByDescending(x => x.Version)
					.FirstOrDefault();

				if (opcode != null)
				{
					if (Manager.PacketMgr.PacketHandlers.ContainsKey((ushort)opcode.Code))
					{
						MethodInfo method = Manager.AssemblyMgr.GetMethod
							(
								opcode.AssemblyName
							,	opcode.TypeName
							,	opcode.MethodName
							,	typeof(Client)
							,	typeof(Packet)
							);

						if (method != null)
						{
							Manager.PacketMgr.PacketHandlers[(ushort)opcode.Code]
								= Delegate.CreateDelegate(typeof(OpcodeHandler), method) as OpcodeHandler;
						}
					}
				}
			}


			return true;
		}

		#endregion

		#region ForceTypeVersionHandler

		private static bool ForceTypeVersionHandler(Client c, params string[] args)
		{
			int code = Int32.Parse(args[0]);
			int version = Int32.Parse(args[1]);
			int opcodeType = (int)Enum.Parse(typeof(OpcodeType), args[2]);

			using(ApplicationContext context = new ApplicationContext())
			{
				OpcodeModel opcode = context.Opcodes
					.FirstOrDefault(x => x.Code == code && x.Version == version 
						&& x.TypeID == opcodeType && x.Active);

				if(opcode != null)
				{
					if (Manager.PacketMgr.PacketHandlers.ContainsKey((ushort)opcode.Code))
					{
						MethodInfo method = Manager.AssemblyMgr.GetMethod
							(
								opcode.AssemblyName
							,	opcode.TypeName
							,	opcode.MethodName
							,	typeof(Client)
							,	typeof(Packet)
							);

						if (method != null)
						{
							Manager.PacketMgr.PacketHandlers[(ushort)opcode.Code]
							= Delegate.CreateDelegate(typeof(OpcodeHandler), method) as OpcodeHandler;
						}
					}
				}
			}

			return true;
		}

		#endregion

		#endregion

		#endregion
	}
}
