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

using ServerFramework.Configuration;
using System;
using System.IO;
using System.Text;

namespace ServerFramework.Network.Packets
{
    public sealed class Packet : IDisposable
    {
        #region Fields

        private PacketHeader _header;
        private byte[] _message;
        private int _sessionId;
        private Encoding _encoder;

        private byte _position = 0;
        private byte _value;

        private dynamic _stream;

        #endregion

        #region Properties

        public PacketHeader Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public byte[] Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public int SessionId
        {
            get { return _sessionId; }
            internal set { _sessionId = value; }
        }

        public byte Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public byte Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public dynamic Stream
        {
            get { return _stream; }
        }

        private Encoding Encoder
        {
            get { return _encoder; }
            set { _encoder = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new object for reading message.
        /// </summary>
        internal Packet(Encoding encoder = null)
        {
            Header = new PacketHeader();
            Encoder = encoder ?? Encoding.UTF8;
        }

        /// <summary>
        /// Creates new object for writing message
        /// </summary>
        /// <param name="message">opcode of message</param>
        public Packet(ushort message, Encoding encoder = null)
        {
            _stream = new BinaryWriter(new MemoryStream());
            Encoder = encoder ?? Encoding.UTF8;

            Header = new PacketHeader
            {
                Size = (ushort)ServerConfig.HeaderLength,
                Opcode = message
            };

            Write<ushort>(Header.Size);
            Write<ushort>(Header.Opcode);
        }

        #endregion

        #region Methods

        #region PrepareRead

        /// <summary>
        /// Readies buffer for reading
        /// </summary>
        internal void PrepareRead()
        {
            if (!(Stream is BinaryWriter))
                _stream = new BinaryReader(new MemoryStream(this.Message));
        }

        #endregion

        #region Read

        /// <summary>
        /// Used for reading from packet buffer
        /// </summary>
        /// <typeparam name="T">type of value</typeparam>
        /// <param name="count">not used</param>
        /// <returns>Byte, SByte, UInt16, Int16, UInt32, Int32,
        /// UInt64, Int64, Char, Double, Single, Boolena, Pascal String
        /// depending of method type</returns>
        public T Read<T>(int count = 0)
        {
            if (Stream is BinaryWriter)
                return default(T);

            switch (typeof(T).Name)
            {
                case "Byte":
                    return Stream.ReadByte();
                case "SByte":
                    return Stream.ReadSByte();
                case "UInt16":
                    return Stream.ReadUInt16();
                case "Int16":
                    return Stream.ReadInt16();
                case "UInt32":
                    return Stream.ReadUInt32();
                case "Int32":
                    return Stream.ReadInt32();
                case "UInt64":
                    return Stream.ReadUInt64();
                case "Int64":
                    return Stream.ReadInt64();
                case "Char":
                    return Stream.ReadChar();
                case "Double":
                    return Stream.ReadDouble();
                case "Single":
                    return Stream.ReadSingle();
                case "Boolean":
                    return Stream.ReadBoolean();
                case "String":
                    var bytes = Stream.ReadBytes(ReadBits<byte>(count));
                    return Encoder.GetString(bytes);
                case "Byte[]":
                    return Stream.ReadBytes(count);
                default:
                    return default(T);
            }
        }

        #endregion

        #region Write

        /// <summary>
        /// Writes value to stream buffer.
        /// </summary>
        /// <typeparam name="T">type of value</typeparam>
        /// <param name="value">value of method type</param>
        public void Write<T>(T value)
        {
            if (Stream is BinaryReader)
                return;

            switch (typeof(T).Name)
            {
                case "Byte":
                    Stream.Write(Convert.ToByte(value));
                    break;
                case "SByte":
                    Stream.Write(Convert.ToSByte(value));
                    break;
                case "UInt16":
                    Stream.Write(Convert.ToUInt16(value));
                    break;
                case "Int16":
                    Stream.Write(Convert.ToInt16(value));
                    break;
                case "UInt32":
                    Stream.Write(Convert.ToUInt32(value));
                    break;
                case "Int32":
                    Stream.Write(Convert.ToInt32(value));
                    break;
                case "UInt64":
                    Stream.Write(Convert.ToUInt64(value));
                    break;
                case "Int64":
                    Stream.Write(Convert.ToInt64(value));
                    break;
                case "Single":
                    Stream.Write(Convert.ToSingle(value));
                    break;
                case "String":
                    var data = Encoder.GetBytes(value as string);
                    Stream.Write(Convert.ToByte(data.Length));
                    Stream.Write(data);
                    break;
                case "Byte[]":
                    data = value as byte[];

                    if (data != null)
                        Stream.Write(data);
                    break;
            }
        }

        #endregion

        #region BitPack

        #region ReadBit

        public bool ReadBit()
        {
            if (Position == 0)
            {
                Value = Read<byte>();
                Position = 8;
            }

            bool retVal = Convert.ToBoolean(Value >> 7);

            --Position;
            Value <<= 1;

            return retVal;
        }

        #endregion

        #region ReadBits

        public T ReadBits<T>(int count)
        {
            int retVal = 0;

            for (int i = count - 1; i >= 0; --i)
                retVal = ReadBit() ? (1 << i) | retVal : retVal;

            return (T)Convert.ChangeType(retVal, typeof(T));
        }

        #endregion

        #region WriteBit

        public void WriteBit(bool value)
        {
            ++Position;

            if (value)
                Value |= (byte)(1 << (8 - Position));

            if (Position == 8)
            {
                Write<byte>(Value);
                Position = 0;
                Value = 0;
            }
        }

        #endregion

        #region WriteBits

        public void WriteBits<T>(T value, int count)
        {
            for (int i = count - 1; i >= 0; --i)
                WriteBit((bool)Convert.ChangeType(
                    (Convert.ToInt32(value) >> i) & 1, typeof(bool)));
        }

        #endregion

        #region Flush

        public void Flush()
        {
            if (Position != 0)
            {
                Write<byte>(Value);

                Position = 0;
                Value = 0;
            }
        }

        #endregion

        #endregion

        #region End

        /// <summary>
        /// Readies packet for sending.
        /// </summary>
        /// <returns>Size of packet minus header size</returns>
        internal int End()
        {
            Stream.BaseStream.Seek(0, SeekOrigin.Begin);
            Message = new byte[Stream.BaseStream.Length];
            Header.Size = (ushort)(Message.Length - ServerConfig.HeaderLength);

            for (int i = 0; i < Message.Length; i++)
            {
                Message[i] = (byte)Stream.BaseStream.ReadByte();
            }

            Message[0] = (byte)(Header.Size & 0xFF);
            Message[1] = (byte)((Header.Size >> 8) & 0xFF);

            return Message.Length;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            if (Stream != null)
                Stream.Close();
        }

        #endregion

        #endregion
    }
}
