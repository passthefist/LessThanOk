/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                  *
*                                                                            *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono           *
*                                                                            *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                     *
*                                                                            *
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
 *                                                                           *
\*---------------------------------------------------------------------------*/
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
        #region Member Veriables
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
        NetworkSession Session;
        State GlobalState;
        STATE CurState;
        #endregion
      
        #region Iniialize LoadContent UnloadContent Update Draw
        public LTO()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";
            base.Components.Add(new GamerServicesComponent(this));
            GlobalState = new HomeState();
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
            GlobalState.Initialize(null);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalState.LoadContent(Content);
            HookHomeStateEvents();
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
                if (Session != null)
                {
                    Session.Update();
                    GlobalState.Update(gameTime, Session.LocalGamers);
                }
                else
                    GlobalState.Update(gameTime, null);
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
            GlobalState.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Home State Event Handlers
        void JoinGameHandler(object sender, EventArgs e)
        {
            AvailableNetworkSessionCollection sessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);
            if (sessions.Count < 1)
                return;
            Session = NetworkSession.Join(sessions[0]);
            if (Session != null)
            {
                HookSessionEvents();

                UnhookHomeStateEvents();

                GlobalState = new LobbyState();
                GlobalState.Initialize(null);
                GlobalState.LoadContent(Content);

                HookLobbyStateEvents();
            }
            
        }
        void CreateGameHandler(object sender, EventArgs e)
        {
            try
            {
                Session = NetworkSession.Create(NetworkSessionType.SystemLink, 2, 2);

                UnhookHomeStateEvents();

                GlobalState = new LobbyState();
                GlobalState.Initialize(null);
                GlobalState.LoadContent(Content);

                HookLobbyStateEvents();
                HookSessionEvents();
            }
            catch (Exception exception)
            {

            }
        }
        void ReplayGameHandler(object sender, EventArgs e)
        {
            UnhookHomeStateEvents();

            GlobalState = new GameState();
            GlobalState.Initialize(null);
            GlobalState.LoadContent(Content);

            HookGameStateEvents();
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
            //throw new NotImplementedException();
        }
        void SessionGameStartedHandler(object sender, GameStartedEventArgs e)
        {
            if (Session.IsHost)
            {
                GlobalState = new GameState();
                GlobalState.Initialize(null);
            }
            else
            {
                GlobalState = new ClientState();
                GlobalState.Initialize(null);
            }
            
            GlobalState.LoadContent(Content);
            HookGameStateEvents();
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
            Session.Dispose();
            UnhookGameEvents();
        }
        #endregion

        #region Event Hooking
        private void HookHomeStateEvents()
        {
            Frame_Home frame = ((HomeState)GlobalState).HomeFrame;
            frame.CreateGame += new EventHandler(CreateGameHandler);
            frame.JoinGame += new EventHandler(JoinGameHandler);
            frame.ReplayGame += new EventHandler(ReplayGameHandler);
        }
        private void HookLobbyStateEvents()
        {
            Frame_HostLobby temp = ((LobbyState)GlobalState).LobbyFrame;
            temp.PlayerNotReady += new EventHandler(PlayerNotReadyHandler);
            temp.PlayerReady += new EventHandler(PlayerReadyHandler);
            temp.StartGame += new EventHandler(StartGameHandler);
        }
        private void HookGameStateEvents()
        {
            Frame_Game temp = ((GameState)GlobalState).GameFrame;
            temp.QuitEvent += new EventHandler(QuitGameEventHandler);
        }
        private void HookPostGameEvents()
        {

        }
        private void HookSessionEvents()
        {
            Session.GameEnded += new EventHandler<GameEndedEventArgs>(SessionGameEndedHandler);
            Session.GamerJoined += new EventHandler<GamerJoinedEventArgs>(SessionGamerJoinedHandler);
            Session.GamerLeft += new EventHandler<GamerLeftEventArgs>(SessionGamerLeftHandler);
            Session.GameStarted += new EventHandler<GameStartedEventArgs>(SessionGameStartedHandler);
            Session.HostChanged += new EventHandler<HostChangedEventArgs>(SessionHostChangedHandler);
            Session.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(SessionEndedHandler);
        }
        #endregion

        #region Event Unhooking
        private void UnhookHomeStateEvents()
        {
            Frame_Home frame = ((HomeState)GlobalState).HomeFrame;
            frame.JoinGame -= JoinGameHandler;
            frame.CreateGame -= CreateGameHandler;
            frame.ReplayGame -= ReplayGameHandler;
        }
        private void UnhookLobbyStateEvents()
        {
            Frame_HostLobby frame = ((LobbyState)GlobalState).LobbyFrame;

            frame.PlayerNotReady -= PlayerNotReadyHandler;
            frame.PlayerReady -= PlayerReadyHandler;
            frame.StartGame -= StartGameHandler;
        }
        private void UnhookGameEvents()
        {
            Frame_Game frame = ((GameState)GlobalState).GameFrame;
            frame.QuitEvent -= this.QuitGameEventHandler;
        }
        private void UnhookPostGameEvents()
        {

        }
        private void UnhookSessionEvents()
        {
            Session.GameEnded -= this.SessionGameEndedHandler;
            Session.GamerJoined -= this.SessionGamerJoinedHandler;
            Session.GamerLeft -= this.SessionGamerLeftHandler;
            Session.GameStarted -= this.SessionGameStartedHandler;
            Session.HostChanged -= this.SessionHostChangedHandler;
            Session.SessionEnded -= this.SessionEndedHandler;
        }
        #endregion
    }
}
