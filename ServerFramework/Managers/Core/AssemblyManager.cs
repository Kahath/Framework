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

using ServerFramework.Attributes.Base;
using ServerFramework.Attributes.Core;
using ServerFramework.Commands.Base;
using ServerFramework.Database.Context;
using ServerFramework.Database.Model.Application.Command;
using ServerFramework.Database.Model.Application.Opcode;
using ServerFramework.Database.Model.Application.Server;
using ServerFramework.Database.Repository;
using ServerFramework.Enums;
using ServerFramework.Events;
using ServerFramework.Extensions;
using ServerFramework.Managers.Base;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ServerFramework.Managers.Core
{
	public class AssemblyManager : ManagerBase<AssemblyManager>
	{
		#region Events

		public event AssemblyEventHandler OnType;
		public event AssemblyEventHandler OnMethod;

		#endregion

		#region Constructors

		AssemblyManager()
		{
			Init();
		}

		#endregion

		#region Methods

		#region Init

		internal override void Init()
		{
			foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.CustomAttributes
					.Any(y => typeof(ICustomAttribute).IsAssignableFrom(y.AttributeType))))
			{
				HandleCustomAssemblyTypes(a);
			}

			using(ApplicationContext context = new ApplicationContext())
			{
				ServerModel server = context.Servers.OrderByDescending(x => x.ID).First();

				context.Opcodes.RemoveRange(
					context.Opcodes.Where
					(x => 
						x.Active
						&& x.DateModified.HasValue ? x.DateModified.Value < server.DateCreated : x.DateCreated < server.DateCreated
					).ToList());

				context.Commands.RemoveRange(
					context.Commands.Where
					(x => 
						x.Active
						&& x.DateModified.HasValue ? x.DateModified.Value < server.DateCreated : x.DateCreated < server.DateCreated
					).ToList());

				context.SaveChanges();
			}
		}

		#endregion

		#region Load

		public void Load(string path)
		{
			Assembly assembly = null;

			try
			{
				assembly = Assembly.LoadFrom(path);
			}
			catch(FileNotFoundException e)
			{
				Manager.LogMgr.Log(LogType.Error, "{0}", e.ToString());
			}
			catch(FileLoadException e)
			{
				Manager.LogMgr.Log(LogType.Error, "{0}", e.ToString());
			}
			catch (ArgumentNullException e)
			{
				Manager.LogMgr.Log(LogType.Error, "{0}", e.ToString());
			}

			if(assembly != null)
			{
				HandleCustomAssemblyTypes(assembly);
			}
		}

		#endregion

		#region HandleAssemblyTypes

		public void HandleCustomAssemblyTypes(Assembly assembly)
		{
			using (ApplicationContext context = new ApplicationContext())
			{
				foreach (Type type in assembly.GetTypes())
				{
					OnCustomAssemblyType(assembly, type, context);

					foreach (MethodInfo method in type.GetAllMethods())
					{
						OnCustomAssemblyMethod(assembly, type, method, context);
					}
				}

				context.SaveChanges();
			}
		}

		#endregion

		#region GetType

		public Type GetType(string assemblyName, string typeName)
		{
			Type retVal = null;

			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies()
				.FirstOrDefault(x => x.FullName == assemblyName);

			if (assembly != null)
			{
				retVal = assembly.GetType(typeName);
			}

			return retVal;
		}

		#endregion

		#region GetMethod

		public MethodInfo GetMethod(string assemblyName, string typeName
			, string methodName, params Type[] parameters)
		{
			MethodInfo retVal = null;

			Type type = GetType(assemblyName, typeName);

			if(type != null)
			{
				retVal = type.GetMethodByName(methodName, parameters);
			}

			return retVal;
		}

		#endregion

		#region OnAssemblyType

		private void OnCustomAssemblyType(Assembly assembly, Type type, ApplicationContext context)
		{
			if (type.GetCustomAttributes<CommandAttribute>().Any())
			{
				CommandRepository CRepo = new CommandRepository(context);

				foreach (CommandAttribute attr in type.GetCustomAttributes<CommandAttribute>())
				{
					if (attr != null)
					{
						MethodInfo method = type.GetMethodByName("GetCommand");

						if (method != null)
						{
							CommandHandlerBase commandBase = InvokeConstructor(type) as CommandHandlerBase;

							if (commandBase != null)
							{
								CommandModel existingCommand = context.Commands
									.FirstOrDefault(x => x.Name == commandBase.Name && x.Active);

								if (existingCommand == null)
								{
									existingCommand = new CommandModel(commandBase);

									existingCommand.AssemblyName = assembly.FullName;
									existingCommand.TypeName = type.FullName;
									existingCommand.MethodName = method.Name;

									context.Commands.Add(existingCommand);
								}
								else
								{
									existingCommand.AssemblyName = assembly.FullName;
									existingCommand.TypeName = type.FullName;
									existingCommand.MethodName = method.Name;
									context.Entry(existingCommand).State = EntityState.Modified;
								}

								Command cmd = InvokeMethod<Command>(commandBase, method);

								if (cmd != null)
									CRepo.UpdateSubCommands(cmd, existingCommand);
							}
						}
					}
				}
			}

			if (OnType != null)
				OnType(this, new AssemblyEventArgs(assembly, type, null));
		}

		#endregion

		#region OnAssemblyMethod

		private void OnCustomAssemblyMethod(Assembly assembly, Type type, MethodInfo method, ApplicationContext context)
		{
			foreach (OpcodeAttribute attr in method.GetCustomAttributes<OpcodeAttribute>())
			{
				if (attr != null)
				{
					OpcodeModel existingOpcode = context.Opcodes.FirstOrDefault
						(x =>
							x.Code == attr.Opcode
							&& x.TypeID == (int)attr.Type
							&& x.Version == attr.Version
							&& x.Active
						);

					if (existingOpcode == null)
					{
						OpcodeModel model = new OpcodeModel(attr);
						model.AssemblyName = assembly.FullName;
						model.TypeName = method.DeclaringType.FullName;
						model.MethodName = method.Name;

						context.Opcodes.Add(model);
					}
					else
					{
						existingOpcode.AssemblyName = assembly.FullName;
						existingOpcode.TypeName = type.FullName;
						existingOpcode.MethodName = method.Name;
						context.Entry(existingOpcode).State = EntityState.Modified;
					}
				}
			}

			if (OnMethod != null)
				OnMethod(this, new AssemblyEventArgs(assembly, type, method));
		}

		#endregion

		#region InvokeMethod

		public T InvokeMethod<T>(object obj, MethodInfo method, params object[] args)
		{
			T retVal = default(T);

			try
			{
				retVal = (T)method.Invoke(obj, args);
			}
			catch (TargetInvocationException)
			{
				Manager.LogMgr.Log
					(
						LogType.Error
					,	"Error invoking method {0} of type {1}"
					,	method.Name
					,	typeof(T).FullName
					);
			}

			return retVal;
		}

		#endregion

		#region InvokeConstructor

		public object InvokeConstructor(Type type, params object[] args)
		{
			object retVal = null;

			Type[] types = args.Select(x => x.GetType()).ToArray();

			ConstructorInfo constructor = type.GetConstructor(types);

			if (constructor != null)
				retVal = constructor.Invoke(args);

			return retVal;
		}

		#endregion

		#endregion
	}
}
