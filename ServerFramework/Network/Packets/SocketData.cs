﻿/*
 * Copyright (c) 2015. Kahath.
 * Licensed under MIT license.
 */

using ServerFramework.Configuration.Helpers;

namespace ServerFramework.Network.Packets
{
	internal sealed class SocketData
	{
		#region Fields

		private Packet _packet;

		private readonly int _bufferOffset;
		private readonly int _bufferSize;
		private int _sessionId;

		private byte[] _header;

		private int _messageLength;
		private int _messageOffset;
		private int _headerLength;
		private int _headerOffset;

		private int _headerBytesDoneCount = 0;
		private int _messageBytesDoneCount = 0;
		private int _headerBytesRemainingCount = 0;
		private int _messageBytesRemainingCount = 0;
		private int _headerBytesDoneThisOp = 0;
		private int _messageBytesDoneThisOp = 0;

		private bool _isHeaderReady = false;
		private bool _isPacketReady = false;
		private bool _isBigPacket = false;
		private bool _isUnicode = false;

		#endregion

		#region Properties

		internal int MessageLength
		{
			get { return _messageLength; }
			set { _messageLength = value; }
		}

		internal int MessageOffset
		{
			get { return _messageOffset; }
			set { _messageOffset = value; }
		}

		internal int HeaderOffset
		{
			get { return _headerOffset; }
			set { _headerOffset = value; }
		}

		internal int HeaderLength
		{
			get { return _headerLength; }
			set { _headerLength = value; }
		}

		internal int HeaderBytesDoneCount
		{
			get { return _headerBytesDoneCount; }
			set { _headerBytesDoneCount = value; }
		}

		internal int MessageBytesDoneCount
		{
			get { return _messageBytesDoneCount; }
			set { _messageBytesDoneCount = value; }
		}

		internal int HeaderBytesRemainingCount
		{
			get { return _headerBytesRemainingCount; }
			set { _headerBytesRemainingCount = value; }
		}

		internal int MessageBytesRemainingCount
		{
			get { return _messageBytesRemainingCount; }
			set { _messageBytesRemainingCount = value; }
		}

		internal int HeaderBytesDoneThisOp
		{
			get { return _headerBytesDoneThisOp; }
			set { _headerBytesDoneThisOp = value; }
		}

		internal int MessageBytesDoneThisOp
		{
			get { return _messageBytesDoneThisOp; }
			set { _messageBytesDoneThisOp = value; }
		}

		internal bool IsHeaderReady
		{
			get { return _isHeaderReady; }
			set { _isHeaderReady = value; }
		}

		internal bool IsPacketReady
		{
			get { return _isPacketReady; }
			set { _isPacketReady = value; }
		}

		internal bool IsBigPacket
		{
			get { return _isBigPacket; }
			set { _isBigPacket = value; }
		}

		internal bool IsUnicode
		{
			get { return _isUnicode; }
			set { _isUnicode = value; }
		}

		internal int BufferSize
		{
			get { return _bufferSize; }
		}

		internal int BufferOffset
		{
			get { return _bufferOffset; }
		}

		internal Packet Packet
		{
			get { return _packet; }
			set { _packet = value; }
		}

		public int SessionId
		{
			get { return _sessionId; }
			internal set { _sessionId = value; }
		}

		internal byte[] Header
		{
			get { return _header; }
			set { _header = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates new instance of <see cref="ServerFramework.Network.Packets.SocketData"/> type.
		/// </summary>
		/// <param name="bufferSize">Buffer size for client.</param>
		/// <param name="bufferOffset">Buffer offset in large alocated buffer.</param>
		/// <param name="headerLength">Length of message header.</param>
		internal SocketData(int bufferSize, int bufferOffset, int headerLength)
		{
			_bufferSize = bufferSize;
			_bufferOffset = bufferOffset;
			HeaderLength = headerLength;
			HeaderOffset = bufferOffset;
			Header = new byte[ServerConfig.BigHeaderLength];
		}

		#endregion

		#region Methods

		#region Finish

		/// <summary>
		/// Finishes writing data to packet.
		/// </summary>
		internal void Finish()
		{
			MessageBytesRemainingCount = Packet.End();
		}

		#endregion

		#region Reset

		/// <summary>
		/// Resets user token to its initial state.
		/// </summary>
		/// <param name="headerOffset">Header offset</param>
		internal void Reset(int headerOffset)
		{
			Packet = null;
			IsHeaderReady = false;
			IsPacketReady = false;
			IsBigPacket = false;
			IsUnicode = false;
			HeaderBytesDoneCount = 0;
			HeaderBytesRemainingCount = 0;
			HeaderBytesDoneThisOp = 0;
			MessageBytesDoneCount = 0;
			MessageBytesRemainingCount = 0;
			MessageBytesDoneThisOp = 0;
			MessageLength = 0;
			HeaderLength = 0;
			HeaderOffset = headerOffset;
		}

		#endregion

		#endregion
	}
}
