using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameWorld;
using LessThanOk.BufferedCommunication;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.Network.Commands;

namespace LessThanOk.Network
{
    public sealed class NetworkManager
    {
        private static PacketReader _reader;
        private static PacketWriter _writer;
        private static int _maxGamers;
        private static int _maxLocalGamers;

        static readonly NetworkManager the = new NetworkManager();
        static NetworkManager() 
        {
            _maxGamers = 3;
            _maxLocalGamers = 2;
            _reader = new PacketReader();
            _writer = new PacketWriter();
        }
        public static NetworkManager The { get { return the; } }

        public void serverWritePackets()
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

            if (add)
            {
                foreach (AdditionChange c in additions)
                {
                    write = new Command_Add(c.ParentObject.ID, c.AddedObject.ID, c.Type.ID,
                        c.TimeStamp);
                    _writer.Write(write.CMD[0]);
                    _writer.Write(write.CMD[1]);
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
        }
        public void serverReadPackets()
        {
            UInt64[] data = new UInt64[2];

            while (_reader.Position != _reader.Length)
            {
                data[0] = (UInt64)_reader.ReadInt64();
                data[1] = (UInt64)_reader.ReadInt64();
                RequestQueue_Server.The.push(new Command(data));
            }
        }
        public void clientWritePackets()
        {
            foreach (Command c in GlobalRequestQueue.The.Requests)
            {
                _writer.Write(c.CMD[0]);
                _writer.Write(c.CMD[1]);
            }
        }
        public void clientReadPackets()
        {
            UInt64[] data = new UInt64[2];
            Command command;

            while (_reader.Position != _reader.Length)
            {
                data[0] = (UInt64)_reader.ReadInt64();
                data[1] = (UInt64)_reader.ReadInt64();

                command = new Command(data);
                if (command.getCommandType() == Command.T_COMMAND.ADD)
                {
                    Command_Add toadd = (Command_Add)command;
                    ExicutionQueue.The.addAdd(ref toadd);
                }
                else if (command.getCommandType() == Command.T_COMMAND.SET)
                {
                    Command_Set toset = (Command_Set)command;
                    ExicutionQueue.The.addSet(ref toset);
                }
            }
        }
    }
}
