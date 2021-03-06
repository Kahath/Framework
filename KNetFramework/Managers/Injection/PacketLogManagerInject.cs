﻿/*
 * Copyright © Kahath 2015
 * Licensed under MIT license.
 */

using KNetFramework.Configuration.Helpers;
using KNetFramework.Database.Context;
using KNetFramework.Database.Model.KNet.PacketLog;
using KNetFramework.Enums;
using KNetFramework.Managers.Interface;
using KNetFramework.Network.Packets;
using KNetFramework.Network.Session;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace KNetFramework.Managers.Injection
{
	public sealed class PacketLogManagerInject : IPacketLogManager, IDisposable
	{
		#region Fields

		private string _path;
		private List<PacketLogModel> _packetLog;
		private BlockingCollection<PacketLogItem> _packetLogQueue
			= new BlockingCollection<PacketLogItem>();

		#endregion

		#region Properties

		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		private BlockingCollection<PacketLogItem> PacketLogQueue
		{
			get { return _packetLogQueue; }
			set { _packetLogQueue = value; }
		}

		private List<PacketLogModel> PacketLog
		{
			get
			{
				if (_packetLog == null)
					_packetLog = new List<PacketLogModel>();

				return _packetLog;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates new instance of <see cref="Core.PacketLogManager"/> type.
		/// </summary>
		PacketLogManagerInject()
		{
			Init();
		}

		#endregion

		#region Methods

		#region Init

		/// <summary>
		/// Initialises PacketLogManager.
		/// </summary>
		public void Init()
		{
			Thread logThread = new Thread(() =>
			{
				while (true)
				{
					PacketLogItem item = PacketLogQueue.Take();

					if (item != null)
						LogPacket(item);
				}
			})
			{
				IsBackground = true
			};
			logThread.Start();

			Manager.LogManager.Log(LogTypes.Init, "PacketLogManager thread initialised");
		}

		#endregion

		#region LogPacket

		/// <summary>
		/// Logs packet.
		/// </summary>
		/// <param name="packet">Instance of <see cref="KNetFramework.Network.Packets.Packet"/> type.</param>
		private void LogPacket(PacketLogItem logItem)
		{
			PacketLogModel packetLog = new PacketLogModel()
			{
				IP = logItem?.Client?.IP,
				ClientID = logItem?.Client?.Token?.ID,
				Size = logItem.PacketHeader.Length,
				Opcode = logItem.PacketHeader.Opcode,
				PacketLogTypeID = (int)logItem.PacketLogType,
			};

			if (logItem.PacketHeader.Length > 0)
			{
				packetLog.Message =	BitConverter.ToString(logItem.PacketMessage
					, logItem.PacketHeader.Length > Int16.MaxValue ? KNetConfig.BigHeaderLength : KNetConfig.HeaderLength);
			}

			PacketLog.Add(packetLog);

			if (PacketLog.Count > KNetConfig.PacketLogSize)
			{
				Manager.DatabaseManager.AddOrUpdate<KNetContext, PacketLogModel>(PacketLog.ToArray());

				PacketLog.Clear();
			}
		}

		#endregion

		#region Log

		/// <summary>
		/// Adds packet to queue for logging.
		/// </summary>
		/// <param name="packet">Instance of <see cref="KNetFramework.Network.Packets.Packet"/> type.</param>
		public void Log(Packet packet)
		{
			if ((KNetConfig.PacketLogLevel & packet.LogType) == packet.LogType && packet.Header.IsForLog)
			{
				byte[] message = packet.ToArray();
				Client pClient = Manager.SessionManager.GetClient(packet.SessionID);

				if (pClient != null)
				{
					PacketLogItem packetLog = new PacketLogItem(pClient, packet.Header, message, packet.LogType);
					PacketLogQueue.Add(packetLog);
				}
			}
		}

		#endregion

		#region Dispose

		/// <summary>
		/// Disposes object.
		/// </summary>
		public void Dispose()
		{
			_packetLogQueue.Dispose();
		}

		#endregion

		#endregion
	}
}
