using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Content;
using LessThanOk.BufferedCommunication;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.Network;
using LessThanOk.Sprites;
using LessThanOk.UI;
using LessThanOk.UI.Events;
using LessThanOk.UI.Events.Args;
using LessThanOk.Selecter;
using LessThanOk.States.Events;
using LessThanOk.States.Events.Args;
using LessThanOk.Input;
using LessThanOk.Network.Commands;

namespace LessThanOk
{
    class LTO_Engine
    {
        #region lets make some #DEFINES
        WindowDefinitions.BUTTON ADD = WindowDefinitions.BUTTON.ADD;
        WindowDefinitions.BUTTON CREATE = WindowDefinitions.BUTTON.CREATE_GAME;
        WindowDefinitions.BUTTON END = WindowDefinitions.BUTTON.END_GAME;
        WindowDefinitions.BUTTON ENDSESSION = WindowDefinitions.BUTTON.END_SESSION;
        WindowDefinitions.BUTTON JOIN = WindowDefinitions.BUTTON.JOIN_GAME;
        WindowDefinitions.BUTTON READY = WindowDefinitions.BUTTON.READY;
        WindowDefinitions.BUTTON START = WindowDefinitions.BUTTON.START_GAME;

        WindowDefinitions.FRAME CLIENTLOBBY = WindowDefinitions.FRAME.CLIENTLOBBY;
        WindowDefinitions.FRAME GAME = WindowDefinitions.FRAME.GAME;
        WindowDefinitions.FRAME HOME = WindowDefinitions.FRAME.HOME;
        WindowDefinitions.FRAME HOSTLOBBY = WindowDefinitions.FRAME.HOSTLOBBY;
        WindowDefinitions.FRAME POSTGAME = WindowDefinitions.FRAME.POSTGAME;
        #endregion

        private MenuManager MenuController;
        private NetworkManager NetworkController;
        private InputManager InputController;
        private NetworkSession Session;
        private CommandRequester CMDRequester;
        private GameWorld Game;
        private ObjectSelector AGOSelecter;
        private event EventHandler<GameStateEventArgs> test;

        public LTO_Engine() { }

        public void init(ContentManager Content)
        {
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Bitmap1"), new Rectangle(0,0,48, 48), "PersonSprite");
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Bitmap2"), new Rectangle(0,0,48, 48), "GunSprite");

            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Tile"), new Rectangle(0, 0, 48, 48), "grassTile");
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("tile2"), new Rectangle(0, 0, 48, 48), "yellowTile");
            
            MenuController = new MenuManager(Content);
            NetworkController = new NetworkManager();
            InputController = new InputManager();

            AttachHandlers();

            GameObjectFactory.The.loadXmlData(null);
        }

        private void AttachHandlers()
        {
            // Magic... I know. Just check the top region "Lets make some #DEFINES."

            MenuController.AttachHandlerTo(HOME, CREATE, CreateGame);
            MenuController.AttachHandlerTo(HOME, JOIN, JoinGame);
            MenuController.AttachHandlerTo(CLIENTLOBBY, READY, Ready);
            MenuController.AttachHandlerTo(HOSTLOBBY, START, StartGame);
            MenuController.AttachHandlerTo(HOSTLOBBY, READY, Ready);
        }

        public void update(GameTime time)
        {
            // Grab mouse and keyboard information. Triggier input Events.
            InputController.update(time);
            
            // Update the Menu
            MenuController.update(time);

            if(Session != null)
            {
                Session.Update();
                if(Session.IsHost)
                {
                    if(Game != null)
                    {
                        GamerCollection<LocalNetworkGamer> gamers = Session.LocalGamers;
                        NetworkController.serverReadPackets(gamers);
                        Game.update(time);
                        NetworkController.serverWritePackets(gamers);
                    }
                }
                else
                {
                    if (Game != null)
                    {
                        GamerCollection<LocalNetworkGamer> gamers = Session.LocalGamers;
                        NetworkController.clientReadPackets(gamers);
                        Game.update(time);
                        NetworkController.clientWritePackets(gamers, Session.Host);
                    }
                }
            }


        }

        public void draw(SpriteBatch batch)
        {
            MenuController.draw(batch);
        }

        #region User Interface Event Handlers

        private void StartGame(object sender, ButtonEventArgs args)
        {
            Game = new MasterGameWorld();
            CMDRequester = new CommandRequester(MenuController);
            AGOSelecter = new ObjectSelector();
            Session.StartGame();
            StateChangeEvents.The.TiggerGameState(this, new GameStateEventArgs());
        }

        private void Ready(object sender, ButtonEventArgs args)
        {
            foreach (LocalNetworkGamer g in Session.LocalGamers)
                g.IsReady = (args.State == ButtonEventArgs.STATE.DOWN);
        }

        private void CreateGame(object sender, ButtonEventArgs args)
        {
            try
            {
                Session = NetworkSession.Create(NetworkSessionType.SystemLink, 2, 2);
                Session.GameEnded += GameEndedHandler;
                Session.GamerJoined += GamerJoinedHandler;
                Session.GamerLeft += GamerLeftHandler;
                Session.GameStarted += GameStartedHandler;
                Session.HostChanged += HostChangedHandler;
                Session.SessionEnded += SessionEndedHandler;
                StateChangeEvents.The.TiggerLobbyState(sender, new LobbyStateEventArgs(true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void JoinGame(object sender, ButtonEventArgs args)
        {
            try
            {
                AvailableNetworkSessionCollection sessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);
                if (sessions.Count > 0)
                {
                    Session = NetworkSession.Join(sessions[0]);
                    Session.GameEnded += GameEndedHandler;
                    Session.GamerJoined += GamerJoinedHandler;
                    Session.GamerLeft += GamerLeftHandler;
                    Session.GameStarted += GameStartedHandler;
                    Session.HostChanged += HostChangedHandler;
                    Session.SessionEnded += SessionEndedHandler;
                    StateChangeEvents.The.TiggerLobbyState(sender, new LobbyStateEventArgs(false));
                }
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        #region Network Session Event Handlers

        public void GameEndedHandler(object sender, GameEndedEventArgs args)
        {

        }
        public void GamerJoinedHandler(object sender, GamerJoinedEventArgs args)
        {
            
        }
        public void GamerLeftHandler(object sender, GamerLeftEventArgs args)
        {
            
        }
        public void GameStartedHandler(object sender, GameStartedEventArgs args)
        {
            if (!Session.IsHost)
            {
                Game = new ClientGameWorld();
                AGOSelecter = new ObjectSelector();

                CMDRequester = new CommandRequester(MenuController);
                
                // TODO: Hook InputEvents
                // TODO: Hook InputEvents
                StateChangeEvents.The.TiggerGameState(sender, new GameStateEventArgs());
            }
        }
        public void HostChangedHandler(object sender, HostChangedEventArgs args)
        {
            
        }
        public void SessionEndedHandler(object sender, NetworkSessionEndedEventArgs args)
        {
            
        }

        #endregion
    }
}
