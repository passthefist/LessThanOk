using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using LessThanOk.Network;
using LessThanOk.Network.Commands;
using LessThanOk.GameData;
using LessThanOk.UI;
using LessThanOk.Sprites;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.Input.Events;
using LessThanOk.Input;
using LessThanOk.States;
using LessThanOk.UI.Frames;

namespace LessThanOk
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class LTO : Microsoft.Xna.Framework.Game
    {
        private enum STATE
        {
            HOME,
            CLOBBY,
            HLOBBY,
            GAME,
            POSTGAME
        }

        const int SCREEN_WIDTH = 1000;
        const int SCREEN_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Frame CurrentFrame;
        NetworkSession Session;
        State GlobalState;
        STATE CurState;

        public LTO()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";
            base.Components.Add(new GamerServicesComponent(this));
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;

            InputManager.The.init();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Frame_Home temp = WindowDefinitions.BuildHomeFrame(Content);
            temp.CreateGame += new EventHandler(CreateGameHandler);
            temp.JoinGame += new EventHandler(JoinGameHandler);

            CurrentFrame = temp;

            GlobalState = new HomeState(CurrentFrame);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive && Gamer.SignedInGamers.Count == 0)
            {
                // If there are no profiles signed in, we cannot proceed.
                // Show the Guide so the user can sign in.
                Guide.ShowSignIn(1, false);
            }
            else
            {
                InputManager.update(gameTime);
                CurrentFrame.update(gameTime);
                GlobalState.Update(gameTime);
                if (Session != null)
                    Session.Update();
            }
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            CurrentFrame.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #region Home State Event Handlers
        void JoinGameHandler(object sender, EventArgs e)
        {
            AvailableNetworkSessionCollection sessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);
            if (sessions.Count < 1)
                return;
            Session = NetworkSession.Join(sessions[0]);
            if (Session != null)
            {
                Session.GameEnded += new EventHandler<GameEndedEventArgs>(SessionGameEndedHandler);
                Session.GamerJoined += new EventHandler<GamerJoinedEventArgs>(SessionGamerJoinedHandler);
                Session.GamerLeft += new EventHandler<GamerLeftEventArgs>(SessionGamerLeftHandler);
                Session.GameStarted += new EventHandler<GameStartedEventArgs>(SessionGameStartedHandler);
                Session.HostChanged += new EventHandler<HostChangedEventArgs>(SessionHostChangedHandler);
                Session.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(SessionEndedHandler);

                ((Frame_Home)CurrentFrame).JoinGame -= JoinGameHandler;
                ((Frame_Home)CurrentFrame).CreateGame -= CreateGameHandler;

                Frame_HostLobby temp = WindowDefinitions.BuildHostLobbyFrame(Content);
                temp.PlayerNotReady += new EventHandler(PlayerNotReadyHandler);
                temp.PlayerReady += new EventHandler(PlayerReadyHandler);
                temp.StartGame += new EventHandler(StartGameHandler);
                CurrentFrame = temp;
            }
            
        }

        void CreateGameHandler(object sender, EventArgs e)
        {
            try
            {
                Session = NetworkSession.Create(NetworkSessionType.SystemLink, 2, 2);

                ((Frame_Home)CurrentFrame).JoinGame -= JoinGameHandler;
                ((Frame_Home)CurrentFrame).CreateGame -= CreateGameHandler;

                Frame_HostLobby temp = WindowDefinitions.BuildHostLobbyFrame(Content);
                temp.PlayerNotReady += new EventHandler(PlayerNotReadyHandler);
                temp.PlayerReady += new EventHandler(PlayerReadyHandler);
                temp.StartGame += new EventHandler(StartGameHandler);

                CurrentFrame = temp;

                Session.GameEnded += new EventHandler<GameEndedEventArgs>(SessionGameEndedHandler);
                Session.GamerJoined += new EventHandler<GamerJoinedEventArgs>(SessionGamerJoinedHandler);
                Session.GamerLeft += new EventHandler<GamerLeftEventArgs>(SessionGamerLeftHandler);
                Session.GameStarted += new EventHandler<GameStartedEventArgs>(SessionGameStartedHandler);
                Session.HostChanged += new EventHandler<HostChangedEventArgs>(SessionHostChangedHandler);
                Session.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(SessionEndedHandler);

            }
            catch (Exception exception)
            {

            }
        }
        #endregion

        #region Lobby State Event Handlers
        void StartGameHandler(object sender, EventArgs e)
        {
            //Should only be accessible to host
            if (!Session.IsHost)
                throw new Exception("How did the client get here?");
            Session.StartGame();
        }

        void PlayerReadyHandler(object sender, EventArgs e)
        {
            foreach(LocalNetworkGamer g in Session.LocalGamers)
                g.IsReady = true;
        }

        void PlayerNotReadyHandler(object sender, EventArgs e)
        {
            foreach(LocalNetworkGamer g in Session.LocalGamers)
                g.IsReady = false;
        }
        #endregion

        #region NetworkSession Event Handlers
        void SessionEndedHandler(object sender, NetworkSessionEndedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void SessionHostChangedHandler(object sender, HostChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void SessionGameStartedHandler(object sender, GameStartedEventArgs e)
        {
            ((Frame_HostLobby)CurrentFrame).PlayerNotReady -= PlayerNotReadyHandler;
            ((Frame_HostLobby)CurrentFrame).PlayerReady -= PlayerReadyHandler;
            ((Frame_HostLobby)CurrentFrame).StartGame -= StartGameHandler;
            Frame_Game temp = WindowDefinitions.BuildGameFrame(Content);
            temp.QuitEvent += new EventHandler(QuitGameEventHandler);
            CurrentFrame = temp;

            if (Session.IsHost)
            {
             
            }
            else
            {

            }
        }

        void SessionGamerJoinedHandler(object sender, GamerJoinedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void SessionGamerLeftHandler(object sender, GamerLeftEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void SessionGameEndedHandler(object sender, GameEndedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region GameState Event Handlers
        void QuitGameEventHandler(object sender, EventArgs e)
        {
            ((Frame_Game)CurrentFrame).QuitEvent -= QuitGameEventHandler;
            Frame_Home temp = WindowDefinitions.BuildHomeFrame(Content);
            temp.JoinGame += new EventHandler(JoinGameHandler);
            temp.CreateGame += new EventHandler(CreateGameHandler);
            CurrentFrame = temp;
            Session.Dispose();
        }
        #endregion
    }
}
