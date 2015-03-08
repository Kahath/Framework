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

using ServerFramework.Constants.Misc;
using ServerFramework.Managers;
using ServerFramework.Managers.Core;
using ServerFramework.Network.Packets;
using ServerFramework.Network.Socket;
using System;
using System.Net;

namespace ServerFramework.Constants.Entities.Session
{
    public sealed class Client
    {
        #region Fields

        private Saea _saea;
        private IClient _clientToken;

        #endregion

        #region Properties

        internal Saea Saea
        {
            get { return _saea; }
            set { _saea = value; }
        }

        public IClient Token
        {
            get { return _clientToken; }
            set { _clientToken = value; }
        }

        public string IP
        {
            get { return (Saea.Receiver.AcceptSocket.RemoteEndPoint as IPEndPoint).Address.ToString(); }
        }

        public int Port
        {
            get { return (Saea.Receiver.AcceptSocket.RemoteEndPoint as IPEndPoint).Port;}
        }

        public int SessionID
        {
            get { return (Saea.Receiver.UserToken as UserToken).SessionId; }
        }

        #endregion

        #region Constructors

        internal Client(Saea saea)
        {
            Saea = saea;
        }

        #endregion

        #region Events

        public event PacketSendEventHandler BeforePacketSend;

        #endregion

        #region Methods

        #region Send
		
        public void Send(Packet packet)
        {
            if (BeforePacketSend != null)
                BeforePacketSend(packet, new EventArgs());

            this.Saea.SendResetEvent.WaitOne();
            UserToken token = Saea.Sender.UserToken as UserToken;
            token.Packet = packet;
            token.Finish();
            token.Packet.SessionId = token.SessionId;

            Manager.LogMgr.Log(LogType.Debug, "Packet Content {0}", BitConverter.ToString(packet.Message));
			KahathFramework.Server.Send(this.Saea.Sender);
        }

        #endregion

        #endregion  
    }
}
