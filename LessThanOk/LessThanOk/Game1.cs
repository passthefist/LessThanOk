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
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.BufferedCommunication;
using LessThanOk.GameData.GameWorld;

namespace LessThanOk
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int SCREEN_WIDTH = 1000;
        const int SCREEN_HEIGHT = 600;

        const int MAX_GAMERS = 2;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        UIManager uiManager;
        Frame f_root;
        SpriteFont font;
        Texture2D lol;

        NetworkSessionProperties serverProperties;
        NetworkSession clientSession;
        NetworkSession hostSession;
        PacketWriter packetWriter;
        PacketReader packetReader;
        RequestList requestList;
        ChangeList changeList;

        MasterGameWorld gameworld;
        
        Boolean DEBUG;
        T_SESSION SESSION;
        T_STATE STATE;

        public enum T_SESSION
        {
            SERVER,
            CLIENT,
            OBSERVER,
            REPLAY,
            NONE
        }
        public enum T_STATE
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

            Content.RootDirectory = "Content";
            base.Components.Add(new GamerServicesComponent(this));

            requestList = new RequestList();
            changeList = new ChangeList();
            serverProperties = new NetworkSessionProperties();
            gameworld = new MasterGameWorld();
            packetReader = new PacketReader();
            packetWriter = new PacketWriter();
            f_root = new Frame(Vector2.Zero, new Vector2(1000, 600), null);

        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            
            this.IsMouseVisible = true;

            SESSION = T_SESSION.NONE;
            STATE = T_STATE.HOME;

            DEBUG = true;
            if (DEBUG)
                DEBUGINIT();

            f_root.visible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            if (hostSession != null)
            {
                UpdateServer(gameTime);
            }
            if (clientSession != null)
            {
                UpdateClient(gameTime);
            }
            else
            {
                // show home menu / post game menu
            }
            if (!Guide.IsVisible)
            {
                uiManager.update(gameTime);
            }
    
            if (DEBUG)
                DEBUGUPDATE(gameTime);
           
            base.Update(gameTime);
        }

        private void UpdateClient(GameTime gameTime)
        {
            //Network
                //Read packets
            //Monirator 
                //Contruct packets
            //Gameworld
                //Update
            //UIManager
                //Update
        }

        private void UpdateServer(GameTime gameTime)
        {
            //______________________________________________________________________
            //Network. 
                //Read packets.
                //Fill Request List
            //Game World
            gameworld.Requests = requestList.getRequests();
            gameworld.update(gameTime);
            changeList.addChanges(gameworld.Changes);
            //Network
                //Read Change List
                //Construct Packets
                //Send Packets
            //_____________________________________________________________________
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            uiManager.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DEBUGUPDATE(GameTime gameTime)
        {

        }

        private void CreateSession(out NetworkSession session)
        {
            session = null;
            try
            {
                session = NetworkSession.Create(NetworkSessionType.SystemLink, 1, MAX_GAMERS);
                HookSessionEvents();
            }
            catch (Exception e)
            {

            }
        }
       
        private void HookSessionEvents()
        {
            clientSession.GamerJoined += GamerJoinedEventHandler;
            clientSession.SessionEnded += SessionEndedEventHandler;
            clientSession.GameStarted += GameStartedHandler;
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
            //Not all games are running a host session.  
            //Only the server is running a host session.
            if (hostSession != null)
            {
                hostSession.Dispose();
                hostSession = null;
            }
            clientSession.Dispose();
            clientSession = null;

        }
        private void DEBUGINIT()
        {
            lol = Content.Load<Texture2D>("Bitmap1");
            font = Content.Load<SpriteFont>("Kootenay");

            uiManager = new UIManager(f_root, font);
            SpriteBin.The._font = font;
            Sprite_2D sprite = SpriteBin.The.AddSprite_2D(lol, Color.White, "sprite");

            EngineType engine = new EngineType(0, 0, 0);
            ArmorType armor = new ArmorType(0, 0);
            ProjectileType projectile = new ProjectileType(false, 0, 0, 0);
            WarheadType warhead = new WarheadType(0, 0, WarheadType.Types.BIO);
            WeaponType weapon = new WeaponType(warhead, projectile);
            List<WeaponType> wepList = new List<WeaponType>();
            wepList.Add(weapon);
            UnitType unit = new UnitType(wepList, armor, engine, sprite);
            GameObjectFactory factory = GameObjectFactory.The;
            factory.addType("lol1", engine);
            factory.addType("lol2", armor);
            factory.addType("lol3", projectile);
            factory.addType("lol4", warhead);
            factory.addType("lol5", weapon);
            factory.addType("lolTest", unit);
        }

    }
}
