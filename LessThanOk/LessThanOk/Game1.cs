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
using LessThanOk.GameData;
using LessThanOk.Menus;
using LessThanOk.Sprites;

namespace LessThanOk
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int SCREEN_WIDTH = 800;
        const int SCREEN_HEIGHT = 600;

        const int MAX_GAMERS = 2;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MenuManager menuManager;
        SpriteFont font;
        Menu M_Home;
        Menu M_LoadReplay;
        Menu M_HostLobby;
        Menu M_CreateGame;
        Menu M_JoinGame;
        Menu M_Game;
        Menu M_PostGame;
        
        NetworkSessionProperties serverProperties;
        NetworkSession networkSession;
        PacketWriter packetWriter;
        PacketReader packetReader;
        Monirator arbiter;

        MasterGameWorld gameworld;
        
        Boolean DEBUG;
        T_SESSION SESSION;
        T_STATE STATE;

        private enum T_SESSION
        {
            SERVER,
            CLIENT,
            OBSERVER,
            REPLAY,
            NONE
        }
        private enum T_STATE
        {
            HOME,
            SELECT,
            LOBBY,
            GAME,
            POSTGAME
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;

            Content.RootDirectory = "Content";
            base.Components.Add(new GamerServicesComponent(this));

            serverProperties = new NetworkSessionProperties();
            arbiter = new Monirator();
            gameworld = new MasterGameWorld();
            menuManager = new MenuManager();
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

            SESSION = T_SESSION.NONE;
            STATE = T_STATE.HOME;
            DEBUG = true;

            

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
     
            font = Content.Load<SpriteFont>("Kootenay");

            //Setup Home Menu
            M_Home = new Menu(font, "Home", true);
            M_Home.AddMenuItem("Host Game", () => { M_Home.Enabled = false; createGame(); });
            M_Home.AddMenuItem("Join Game", () => { M_Home.Enabled = false; joinGame(); });
            M_Home.AddMenuItem("Replay Game", () => { M_Home.Enabled = false; replayGame();});
            M_Home.AddMenuItem("Quit", Exit);
            menuManager.AddMenu(M_Home);

       
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

            // Don't want our menus and the guide menus to collide 
            //   (that is, we don't want our menus and the Live Guide to both
            //    handle the same keypress/buttonpress, so we disable our menus
            //    when the guide is visible)
            if (!Guide.IsVisible)
            {
                menuManager.Update(gameTime);
            }
            
            //--------------------------------------------------------------------
                //Main Logic
            //--------------------------------------------------------------------
            if (DEBUG)
                DEBUGUPDATE(gameTime);
           
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
            menuManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DEBUGUPDATE(GameTime gameTime)
        {

        }

        private void CreateSession()
        {
            try
            {
                networkSession = NetworkSession.Create(NetworkSessionType.SystemLink, 1, MAX_GAMERS);
                HookSessionEvents();
            }
            catch (Exception e)
            {

            }
        }
       
        private void HookSessionEvents()
        {
            networkSession.GamerJoined += GamerJoinedEventHandler;
            networkSession.SessionEnded += SessionEndedEventHandler;
            networkSession.GameStarted += GameStartedHandler;
        }

        void GameStartedHandler(object sender, GameStartedEventArgs e)
        {
           
        }

        /// <summary>
        /// This event handler will be called whenever a new gamer joins the session.
        /// We use it to allocate a Tank object, and associate it with the new gamer.
        /// </summary>
        void GamerJoinedEventHandler(object sender, GamerJoinedEventArgs e)
        {
            // We get this callback when *we* join, as well as when anyone else joins.
            // Note that in a 2-player game, both players will get two callbacks
            // (one for themselves, and one for the other player)

        }


        /// <summary>
        /// Event handler notifies us when the network session has ended.
        /// </summary>
        void SessionEndedEventHandler(object sender, NetworkSessionEndedEventArgs e)
        {
            networkSession.Dispose();
            networkSession = null;
        }

        private void createGame()
        {
            CreateSession();
            SESSION = T_SESSION.SERVER;
            Console.WriteLine("Create Game Selected");
        }

        private void joinGame()
        {
            SESSION = T_SESSION.CLIENT;
            Console.WriteLine("Join Game Selected");

        }

        private void replayGame()
        {
            SESSION = T_SESSION.REPLAY;
            Console.WriteLine("Replay Selected");

        }
    }
}
