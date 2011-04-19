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
    public sealed class NetworkManager : GlobalEventSubscriber
    {
        private static NetworkSession _session;
        private static PacketReader _reader;
        private static PacketWriter _writer;
        private static int _maxGamers;
        private static int _maxLocalGamers;

        public static NetworkSession Session { get { return _session; } set { _session = value; } }


        static readonly NetworkManager the = new NetworkManager();
        static NetworkManager() 
        {
            _maxGamers = 3;
            _maxLocalGamers = 2;
            _reader = new PacketReader();
            _writer = new PacketWriter();
        }
        public static NetworkManager The { get { return the; } }

        public void update(GameTime gameTime)
        {
            if (_session != null)
                _session.Update();
        }
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
            foreach (Command c in RequestQueue_Client.The.Requests)
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


        #region GlobalEventSubscriber Members


        public void CreateSessionHandler(object sender, EventArgs e)
        {
            try
            {
                _session = NetworkSession.Create(NetworkSessionType.SystemLink,
                                                       _maxLocalGamers, _maxGamers);

                //_session.GamerJoined += GamerJoinedEventHandler;
                //_session.GamerLeft += GamerLeftEventHandler;
                //_session.GameEnded += SessionEndedEventHandler;
                //_session.GameStarted += SessionStartedEventHandler;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void StartGameHandler(object sender, EventArgs e)
        {
            _session.StartGame();
        }

        public void JoinSessionHandler(object sender, EventArgs e)
        {
            try
            {
                // Search for sessions.
                using (AvailableNetworkSessionCollection availableSessions =
                            NetworkSession.Find(NetworkSessionType.SystemLink,
                                                _maxLocalGamers, null))
                {
                    if (availableSessions.Count == 0)
                    {
                        throw new Exception("No Games Found");
                    }

                    // Join the first session we found.
                    _session = NetworkSession.Join(availableSessions[0]);

                    //_session.GamerJoined += GamerJoinedEventHandler;
                    //_session.GamerLeft += GamerLeftEventHandler;
                    //_session.GameEnded += SessionEndedEventHandler;
                    //_session.GameStarted += SessionStartedEventHandler;
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public void EndGameHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void subscribe(List<GlobalEvent> events)
        {
            foreach(GlobalEvent e in events)
            {
                switch (e.Name)
                {
                    case GlobalEvent.EVENTNAME.JOINGAME:
                        e.Handler += JoinSessionHandler;
                        break;
                    case GlobalEvent.EVENTNAME.CREATEGAME:
                        e.Handler += CreateSessionHandler;
                        break;
                    case GlobalEvent.EVENTNAME.STARTGAME:
                        e.Handler += StartGameHandler;
                        break;
                    case GlobalEvent.EVENTNAME.ENDGAME:
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
