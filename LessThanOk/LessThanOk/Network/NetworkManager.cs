using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework;

namespace LessThanOk.Network
{
    public sealed class NetworkManager
    {
        private static NetworkSession _session;
        private static int _maxGamers;
        private static int _maxLocalGamers;

        public static NetworkSession Session { get { return _session; } }


        static readonly NetworkManager the = new NetworkManager();
        static NetworkManager() 
        {
            _maxGamers = 3;
            _maxLocalGamers = 2;
        }
        public static NetworkManager The { get { return the; } }

        public void update(GameTime gameTime)
        {
            if (_session != null)
                _session.Update();
        }

        public void startSession()
        {
            try
            {
                _session = NetworkSession.Create(NetworkSessionType.SystemLink,
                                                       _maxLocalGamers, _maxGamers);

                _session.GamerJoined += GamerJoinedEventHandler;
                _session.GamerLeft += GamerLeftEventHandler;
                _session.GameEnded += SessionEndedEventHandler;
                _session.GameStarted += SessionStartedEventHandler;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void joinSession()
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

                    _session.GamerJoined += GamerJoinedEventHandler;
                    _session.GamerLeft += GamerLeftEventHandler;
                    _session.GameEnded += SessionEndedEventHandler;
                    _session.GameStarted += SessionStartedEventHandler;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void GamerJoinedEventHandler(object sender, GamerJoinedEventArgs e)
        {
 
        }
        void GamerLeftEventHandler(object sender, GamerLeftEventArgs e)
        {

        }
        void SessionStartedEventHandler(object sender, GameStartedEventArgs e)
        {

        }
        void SessionEndedEventHandler(object sender, GameEndedEventArgs e)
        {
        
        }
        
        
        
    }
}
