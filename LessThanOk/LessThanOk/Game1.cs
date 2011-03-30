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
        NetworkSession networkSession;
        PacketWriter packetWriter;
        PacketReader packetReader;
        Monirator arbiter;

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
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;

            Content.RootDirectory = "Content";
            base.Components.Add(new GamerServicesComponent(this));

            serverProperties = new NetworkSessionProperties();
            arbiter = new Monirator();
            gameworld = new MasterGameWorld();
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
            base.Initialize();
            this.IsMouseVisible = true;

            SESSION = T_SESSION.NONE;
            STATE = T_STATE.HOME;
            DEBUG = true;
            if (DEBUG)
            {
                lol = Content.Load<Texture2D>("Bitmap1");
                font = Content.Load<SpriteFont>("Kootenay");

                SpriteBin.The._font = font;

                uiManager = new UIManager(f_root, font);
                
                

            }
            f_root.visible = true;

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
            //______________________________________________________________________
                //Sever Logic
                    //Network. 
                        //Read packets.
                        //Fill Request List
                    //Monirator
                        //Read Request List
                        //Check valididty
                        //Fill Grant List
                    //Game World
                        //Read Grant List
                        //Update World
                        //Fill Change List
                    //Monirator
                        //Read Change list
                        //Construct Commands
                        //Fill Command List
                    //Network
                        //Construct Packets
                        //Send Packets
            //_____________________________________________________________________
            if (!Guide.IsVisible)
            {
                uiManager.update(gameTime);
            }
                //Client Logic
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
            uiManager.draw(spriteBatch);
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

        private void initTestGame()
        {
            Texture2D t1 = Content.Load<Texture2D>("Bitmap1");
            Texture2D t2 = Content.Load<Texture2D>("Bitmap2");
            Texture2D t3 = Content.Load<Texture2D>("Bitmap3");

            Sprite_2D sprite1 = SpriteBin.The.AddSprite_2D(t1, Color.White);
            Sprite_2D sprite2 = SpriteBin.The.AddSprite_2D(t2, Color.White);
            Sprite_2D sprite3 = SpriteBin.The.AddSprite_2D(t3, Color.White);

            GameObjectFactory factory = GameObjectFactory.The;
            EngineType engine = new EngineType(3.0f, 0.2f, 0.5f);
            factory.addType("BasicEngine", engine);

            ArmorType armor = new ArmorType(0, 0);
            factory.addType("BasicArmor", armor);

            ProjectileType projectile = new ProjectileType(false, 0, 0, 0);
            factory.addType("BasicProjectile", projectile);

            WarheadType warhead = new WarheadType(0, 0, WarheadType.Types.NONE);
            factory.addType("BasicWarhead", warhead);

            WeaponType weapon = new WeaponType(warhead, projectile);
            factory.addType("BasicWea", weapon);

            UnitType unit1 = new UnitType(weapon, armor, engine, sprite1);
            UnitType unit2 = new UnitType(weapon, armor, engine, sprite2);
            UnitType unit3 = new UnitType(weapon, armor, engine, sprite3);
            
            factory.addType("lol4", warhead);
            factory.addType("lol5", weapon);
            factory.addType("lolTest", unit1);
        }
    }
}
