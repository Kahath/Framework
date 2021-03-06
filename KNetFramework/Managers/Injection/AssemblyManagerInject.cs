﻿/*
 * Copyright © Kahath 2015
 * Licensed under MIT license.
 */

using KNetFramework.Attributes.Base;
using KNetFramework.Attributes.Core;
using KNetFramework.Commands.Base;
using KNetFramework.Configuration.Helpers;
using KNetFramework.Database.Context;
using KNetFramework.Database.Model.KNet.Command;
using KNetFramework.Database.Model.KNet.Opcode;
using KNetFramework.Database.Model.KNet.Server;
using KNetFramework.Database.Repository;
using KNetFramework.Enums;
using KNetFramework.Events;
using KNetFramework.Extensions;
using KNetFramework.Managers.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KNetFramework.Managers.Injection
{
	public class AssemblyManagerInject : IAssemblyManager
	{
		#region Events

		public event AssemblyEventHandler OnType;
		public event AssemblyEventHandler OnMethod;

		#endregion

		#region Constructors

		AssemblyManagerInject()
		{
			Init();
		}

		#endregion

		#region Methods

		#region Init

		public void Init()
		{
			string path = Path.Combine
				(
					Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
					KNetConfig.AssemblyPath
				);

			foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.CustomAttributes
					.Any(y => typeof(ICustomAttribute).IsAssignableFrom(y.AttributeType))))
			{
				ProcessCustomAssembly(a);
			}

			foreach (string dll in Directory.GetFiles(path, "*.dll"))
				Load(dll);

			using (KNetContext context = new KNetContext())
			{
				ServerModel server = Manager.DatabaseManager
					.Get<ServerModel>(context, x => x.AsNoTracking().OrderByDescending(y => y.ID).First());

				Manager.DatabaseManager.Remove<OpcodeModel>(context, false, x => x.Where(y =>
						y.Active
						&& (y.DateModified.HasValue ? y.DateModified.Value < server.DateCreated : y.DateCreated < server.DateCreated)
					).ToList());

				Manager.DatabaseManager.Remove<CommandModel>(context, false, x => x.Where(y =>
						y.Active
						&& (y.DateModified.HasValue ? y.DateModified.Value < server.DateCreated : y.DateCreated < server.DateCreated)
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
			catch (FileNotFoundException e)
			{
				Manager.LogManager.Log(LogTypes.Error, e);
			}
			catch (FileLoadException e)
			{
				Manager.LogManager.Log(LogTypes.Error, e);
			}
			catch (ArgumentNullException e)
			{
				Manager.LogManager.Log(LogTypes.Error, e);
			}

			if (assembly != null)
				ProcessCustomAssembly(assembly);
		}

		#endregion

		#region ProcessCustomAssembly

		public void ProcessCustomAssembly(Assembly assembly)
		{
			using (KNetContext context = new KNetContext())
			{
				foreach (Type type in assembly.GetTypes())
				{
					OnCustomAssemblyType(assembly, type, context);

					foreach (MethodInfo method in type.GetAllMethods())
						OnCustomAssemblyMethod(assembly, type, method, context);
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
				retVal = assembly.GetType(typeName);

			return retVal;
		}

		#endregion

		#region GetMethod

		public MethodInfo GetMethod(string assemblyName, string typeName
			, string methodName, params Type[] parameters)
		{
			MethodInfo retVal = null;
			Type type = GetType(assemblyName, typeName);

			if (type != null)
				retVal = type.GetMethodByName(methodName, parameters);

			return retVal;
		}

		#endregion

		#region OnCustomAssemblyType

		private void OnCustomAssemblyType(Assembly assembly, Type type, KNetContext context)
		{
			IEnumerable<CommandAttribute> attributes = type.GetCustomAttributes<CommandAttribute>();

			if (attributes != null && attributes.Any())
			{
				CommandRepository CRepo = new CommandRepository(context);

				foreach (CommandAttribute attr in attributes)
				{
					if (attr != null)
					{
						MethodInfo method = type.GetMethodByName("GetCommand");

						if (method != null)
						{

							if (InvokeConstructor(type) is CommandHandlerBase commandBase)
							{
								CommandModel existingCommand = Manager.DatabaseManager.Get<CommandModel>(context, x =>
									x.FirstOrDefault(y => y.Name == commandBase.Name && y.Active));

								existingCommand = existingCommand ?? new CommandModel(commandBase);
								existingCommand.AssemblyName = assembly.FullName;
								existingCommand.TypeName = type.FullName;
								existingCommand.MethodName = method.Name;

								Manager.DatabaseManager.AddOrUpdate(context, false, existingCommand);

								Command cmd = InvokeMethod<Command>(commandBase, method);

								if (cmd != null)
									CRepo.UpdateSubCommands(cmd, existingCommand);
							}
						}
					}
				}
			}

			OnType?.Invoke(this, new AssemblyEventArgs(assembly, type, null));
		}

		#endregion

		#region OnCustomAssemblyMethod

		private void OnCustomAssemblyMethod(Assembly assembly, Type type, MethodInfo method, KNetContext context)
		{
			foreach (OpcodeAttribute attr in method.GetCustomAttributes<OpcodeAttribute>())
			{
				if (attr != null && ((KNetConfig.OpcodeAllowLevel & attr.Type) == attr.Type))
				{
					OpcodeModel existingOpcode = Manager.DatabaseManager.Get<OpcodeModel>(context, x => x.FirstOrDefault(y =>
						y.Code == attr.Opcode && y.TypeID == (int)attr.Type && y.Version == attr.Version && y.Active));

					existingOpcode = existingOpcode ?? new OpcodeModel(attr);
					existingOpcode.AssemblyName = assembly.FullName;
					existingOpcode.TypeName = type.FullName;
					existingOpcode.MethodName = method.Name;

					Manager.DatabaseManager.AddOrUpdate(context, false, existingOpcode);
				}
			}

			OnMethod?.Invoke(this, new AssemblyEventArgs(assembly, type, method));
		}

		#endregion

		#region InvokeMethod

		public T InvokeMethod<T>(object obj, MethodInfo method, params object[] args)
			where T : class
		{
			T retVal = default(T);

			try
			{
				retVal = method.Invoke(obj, args) as T;
			}
			catch (TargetInvocationException)
			{
				Manager.LogManager.Log(LogTypes.Error, $"Error invoking method {method.Name} of type {typeof(T).FullName}");
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
