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

using ServerFramework.Constants.Attributes;
using ServerFramework.Constants.Entities.Session;
using ServerFramework.Constants.Misc;
using ServerFramework.Managers;
using System;
using System.Text;

namespace ServerFramework.Network.Packets.Handlers
{
    public static class ExampleHandler
    {
        #region Handlers

        #region ExamplePacketHandler

        #region Version 1

        [Opcode(0x0000, "Kahath", 1, OpcodeType.Test)]
        public static void ExamplePacketHandler(Packet packet)
        {
            Client pClient = Manager.SessionMgr.GetClientBySessionId(packet.SessionId);

            //Read if packet has data
            string exampleName = packet.Read<string>(8);
            byte exampleData = packet.Read<byte>();

            //Process data
            Console.WriteLine(exampleName);

            //Send back if need
            //Create new packet for send
            using (packet = new Packet(0x0001))
            {
                //Write data
                packet.Write<string>("Example string data");

                //Send data
                pClient.Send(packet);
            }
        }

        #endregion

        #region Version 2

        [Opcode(0x0000, "Kahath", 2, OpcodeType.Test)]
        public static void ExamplePacketHandlerTwo(Packet packet)
        {
            Client pClient = Manager.SessionMgr.GetClientBySessionId(packet.SessionId);

            //Read if packet has data
            string exampleName = packet.Read<string>(8);
            byte exampleData = packet.Read<byte>();

            //Process data
            Console.WriteLine(exampleName);

            //Send back if need
            //Create new packet for send
            using (packet = new Packet(0x0001))
            {
                //Write data
                packet.Write<string>("Example string data");

                //Write bits
                packet.WriteBits<int>(0x123, 8);

                packet.WriteBit(true);
                packet.WriteBit(false);

                //Flush to write remaining bits to stream
                packet.Flush();

                //Send data
                pClient.Send(packet);
            }
        }

        #endregion
        
        #endregion
        
        #endregion
    }
}
