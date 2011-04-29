/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
*                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                                License                                    *
 *                                                                           *
 * This library is free software; you can redistribute it and/or modify it   *
 * under the terms of the MIT Liscense.                                      *
 *                                                                           *
 * This library is distributed in the hope that it will be useful, but       *
 * WITHOUT ANY WARRANTY; without even the implied warranty of                *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                           *
 * You should have received a copy of the MIT Liscense with this library, if *
 * not, visit http://www.opensource.org/licenses/mit-license.php.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                            Class Overview                                 *
 *                                                                           *
 * NetworkManager is used for ready and writing packets to the network.      *
 *                                                                           *
\*---------------------------------------------------------------------------*/
         
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameWorld;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Decorators;
using LessThanOk.Network.Commands.Events;
using LessThanOk.GameData;

namespace LessThanOk.Network
{
    public class NetworkManager
    {
        public event EventHandler<NewCommandEventArgs> NewCommandEvent;
        private PacketReader _reader;
        private PacketWriter _writer;
        private int _maxGamers;
        private int _maxLocalGamers;

        public NetworkManager() 
        {
            _maxGamers = 3;
            _maxLocalGamers = 2;
            _reader = new PacketReader();
            _writer = new PacketWriter();
        }
        /// <summary>
        /// Broadcast all changes to Clients.
        /// </summary>
        /// <param name="gamers">List of Clients.</param>
        public void serverWritePackets(GamerCollection<LocalNetworkGamer> gamers)
        {
            foreach (LocalNetworkGamer gamer in gamers)
            {
                // TODO: Re-impliment   
                if(_writer.Length > 0)
                    gamer.SendData(_writer, SendDataOptions.ReliableInOrder);
            }
        }
        /// <summary>
        /// Collect requests from clients.
        /// </summary>
        /// <param name="gamers">List of Clients.</param>
        public void serverReadPackets(GamerCollection<LocalNetworkGamer> gamers, GameTime time)
        {
            UInt64[] data = new UInt64[2];

            foreach (LocalNetworkGamer gamer in gamers)
            {
                while (gamer.IsDataAvailable)
                {
                    NetworkGamer sender;
                    gamer.ReceiveData(_reader, out sender);

                    if (sender.IsLocal)
                        return;

                    data[0] = (UInt64)_reader.ReadInt64();
                    data[1] = (UInt64)_reader.ReadInt64();
                    Command command = new Command(data);
                    // TODO: Get Player from PlayerList
                    if(NewCommandEvent != null)
                        NewCommandEvent.Invoke(this, new NewCommandEventArgs(command, time, new Player()));
                }
            }
        }
        /// <summary>
        /// Write requests to the host.
        /// </summary>
        /// <param name="gamers">Clients</param>
        /// <param name="host">Host</param>
        public void clientWritePackets(GamerCollection<LocalNetworkGamer> gamers, NetworkGamer host)
        {
            Command cmd;
            foreach (LocalNetworkGamer gamer in gamers)
            {
                // TODO: Reimpliment.
                if(_writer.Length > 0)
                    gamer.SendData(_writer, SendDataOptions.ReliableInOrder, host);
            }
        }
        /// <summary>
        /// Collect Changes from the host
        /// </summary>
        /// <param name="gamers">Clients.</param>
        public void clientReadPackets(GamerCollection<LocalNetworkGamer> gamers)
        {
            UInt64[] data = new UInt64[2];
            Command command;

            foreach (LocalNetworkGamer gamer in gamers)
            {
                while (gamer.IsDataAvailable)
                {
                    NetworkGamer sender;

                    gamer.ReceiveData(_reader, out sender);

                    if (sender.IsLocal)
                        return;

                    data[0] = (UInt64)_reader.ReadInt64();
                    data[1] = (UInt64)_reader.ReadInt64();

                    command = new Command(data);
                    if (command.CmdType == Command.T_COMMAND.ADD)
                    {

                    }
                    else if (command.CmdType == Command.T_COMMAND.SET)
                    {

                    }
                }
            }
        }
    }
}
