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
using LessThanOk.States.Events;
using LessThanOk.States.Events.Args;
using LessThanOk.Input;
using LessThanOk.Network.Commands;

namespace LessThanOk
{
    class LTO_Engine
    {

        private MenuManager MenuController;
        private NetworkManager NetworkController;
        private InputManager InputController;
        private NetworkSession Session;
        private CommandRequester CMDRequester;
        private GameWorld Game;

        private event EventHandler<GameStateEventArgs> test;

        public LTO_Engine()
        {
            UIElementEvents.ButtonPress += new EventHandler<ButtonEventArgs>(this.ButtonPressed);
        }

        public void init(ContentManager Content)
        {
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Bitmap1"), new Vector2(), "PersonSprite");
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Bitmap2"), new Vector2(), "GunSprite");

            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Tile"), new Vector2(20, 20), "grassTile");
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("tile2"), new Vector2(20, 20), "yellowTile");

            MenuController = new MenuManager(Content);
            NetworkController = new NetworkManager();
            InputController = new InputManager();
            CMDRequester = new CommandRequester();

            GameObjectFactory.The.loadXmlData(null);
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
                        Game.update(time);
                }
                else
                {
                    if (Game != null)
                        Game.update(time);
                }
            }


        }

        public void draw(SpriteBatch batch)
        {
            MenuController.draw(batch);
        }

        #region User Interface Event Handlers

        private void ButtonPressed(object sender, ButtonEventArgs args)
        {
            if (args.Element.Name == "start")
            {
                StartGame();
                StateChangeEvents.The.TiggerGameState(sender, new GameStateEventArgs());
            }
            else if (args.Element.Name == "create")
            {
                CreateGame();
                StateChangeEvents.The.TiggerLobbyState(sender, new LobbyStateEventArgs(true));
            }
            else if (args.Element.Name == "join")
            {
                JoingGame();
                StateChangeEvents.The.TiggerLobbyState(sender, new LobbyStateEventArgs(false));
            }
            else if (args.Element.Name == "ready")
            {
                Ready();
            }
        }

        private void StartGame()
        {
            Game = new MasterGameWorld();
        }

        private void Ready()
        {
            foreach (LocalNetworkGamer g in Session.LocalGamers)
                g.IsReady = true;
        }

        private void CreateGame()
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void JoingGame()
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
                }
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
            Game = new ClientGameWorld();
            StateChangeEvents.The.TiggerGameState(sender, new GameStateEventArgs());
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
