using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameWorld;
using LessThanOk.BufferedCommunication;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.Network.Commands;
using LessThanOk.Network.Commands.Decorators;

namespace LessThanOk.Network
{
    public class NetworkManager
    {
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

        public void serverWritePackets(GamerCollection<LocalNetworkGamer> gamers)
        {
            List<AdditionChange> additions;
            List<RemovalChange> removals;
            List<SetValueChange> sets;
            Command write = new Command();

            bool add;
            bool remove;
            bool set;

            add = ChangeList.pullAdd(out additions);
            remove = ChangeList.pullRem(out removals);
            set = ChangeList.pullSet(out sets);

            foreach (LocalNetworkGamer gamer in gamers)
            {
                if (add)
                {
                    foreach (AdditionChange c in additions)
                    {
                        write = new AddDecorator(c.ParentObject.ID, c.AddedObject.ID, c.Type.ID, c.TimeStamp, new Command());
                        _writer.Write(write.Cmd[0]);
                        _writer.Write(write.Cmd[1]);
                    }
                }
                if (remove)
                {
                    foreach (RemovalChange c in removals)
                    {

                    }
                }
                if (set)
                {
                    foreach (SetValueChange c in sets)
                    {

                    }
                }
                if(_writer.Length > 0)
                    gamer.SendData(_writer, SendDataOptions.ReliableInOrder);
            }
        }
        public void serverReadPackets(GamerCollection<LocalNetworkGamer> gamers)
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
                    switch (command.CmdType)
                    {
                        case Command.T_COMMAND.MOVE:
                            break;
                        case Command.T_COMMAND.ADD:
                            GlobalRequestQueue.The.push(new AddDecorator(new Command(data)));
                            break;
                        case Command.T_COMMAND.REMOVE:
                            break;
                        case Command.T_COMMAND.SET:
                            break;
                        case Command.T_COMMAND.ERROR:
                            break;
                        case Command.T_COMMAND.CANCEL:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public void clientWritePackets(GamerCollection<LocalNetworkGamer> gamers, NetworkGamer host)
        {
            Command cmd;
            foreach (LocalNetworkGamer gamer in gamers)
            {
                while (GlobalRequestQueue.The.pull(out cmd))
                {
                    _writer.Write(cmd.Cmd[0]);
                    _writer.Write(cmd.Cmd[1]);
                }
                if(_writer.Length > 0)
                    gamer.SendData(_writer, SendDataOptions.ReliableInOrder, host);
            }
        }
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
                        Command toadd = new AddDecorator(new Command(data));
                        ExicutionQueue.The.pushAdd(toadd);
                    }
                    else if (command.CmdType == Command.T_COMMAND.SET)
                    {
                        //Command_Set toset = (Command_Set)command;
                        //ExicutionQueue.The.addSet(toset);
                    }
                }
            }
        }
    }
}
