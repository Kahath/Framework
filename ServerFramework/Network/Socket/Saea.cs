﻿using System.Net.Sockets;
using System.Threading;

namespace ServerFramework.Network.Socket
{
    public class Saea
    {
        #region Fields

        private SocketAsyncEventArgs _sender;
        private SocketAsyncEventArgs _receiver;
        private AutoResetEvent _sendResetEvent;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates object with SocketAsyncEventArgs objects
        /// for sending and receiving data
        /// </summary>
        public Saea()
        {
            _sendResetEvent = new AutoResetEvent(true);
        }

        #endregion

        #region Properties

        public SocketAsyncEventArgs Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public SocketAsyncEventArgs Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public AutoResetEvent SendResetEvent 
        {
            get { return _sendResetEvent; }
            set { _sendResetEvent = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Closes both SocketAsyncEventArgs objects
        /// </summary>
        public void Close()
        {
            this.Sender.AcceptSocket.Close();
            this.Receiver.AcceptSocket.Close();
        }

        /// <summary>
        /// Shutdown both SocketAsyncEventArgs objects
        /// </summary>
        /// <param name="how"></param>
        public void Shutdown(SocketShutdown how)
        {
            try
            {
                this.Sender.AcceptSocket.Shutdown(how);
                this.Receiver.AcceptSocket.Shutdown(how);
                this._sendResetEvent.Set();
            }
            catch (SocketException) { }
        }

        #endregion
    }
}
