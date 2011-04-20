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

namespace LessThanOk
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game, GlobalEventSubscriber
    {
        const int SCREEN_WIDTH = 1000;
        const int SCREEN_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameWorld gameWorld;
        UIEventListener GameController;
        List<GlobalEvent> globalEvents;
        NetworkSession session;
        CommandRequester commandRequester;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;

            Content.RootDirectory = "Content";
            base.Components.Add(new GamerServicesComponent(this));

            globalEvents = new List<GlobalEvent>();
            globalEvents.Add(new GlobalEvent(GlobalEvent.EVENTNAME.CREATEGAME));
            globalEvents.Add(new GlobalEvent(GlobalEvent.EVENTNAME.JOINGAME));
            globalEvents.Add(new GlobalEvent(GlobalEvent.EVENTNAME.STARTGAME));
            globalEvents.Add(new GlobalEvent(GlobalEvent.EVENTNAME.ENDGAME));

            List<CommandEvent> commandEvents = new List<CommandEvent>();
            commandEvents.Add(new CommandEvent(CommandEvent.EVENTNAME.ADD));

            GameController = new UIEventListener(globalEvents, commandEvents);
            commandRequester = new CommandRequester();
            
            commandRequester.subscribe(commandEvents);
            subscribe(globalEvents);


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

            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Bitmap1"), new Vector2(), "PersonSprite");
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Bitmap2"), new Vector2(), "GunSprite");

            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Tile"), new Vector2(20,20), "grassTile");
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("tile2"), new Vector2(20,20), "yellowTile");
            
            UIManager.The.init(Content);
            InputManager.The.init();
     
            UIManager.The.subscribe(globalEvents);
    
            GameObjectFactory.The.loadXmlData(null);

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
            else
            {
                UIManager.The.update(gameTime);
                InputManager.The.update(gameTime);
            }
            if (session != null)
            {
                session.Update();

                if (session.IsHost)
                {
                    NetworkManager.The.serverReadPackets();
                    if(session.SessionState == NetworkSessionState.Playing)
                        gameWorld.update(gameTime.ElapsedGameTime);
                    NetworkManager.The.serverWritePackets();
                }
                else
                {
                    NetworkManager.The.clientReadPackets();
                    if (session.SessionState == NetworkSessionState.Playing)
                        gameWorld.update(gameTime.ElapsedGameTime);
                }
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
            UIManager.The.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        

        #region GlobalEventSubscriber Members

        public void StartGameHandler(object sender, EventArgs args)
        {
            if(session.IsEveryoneReady)
                session.StartGame();
        }
        public void JoinSessionHandler(object sender, EventArgs e)
        {
            AvailableNetworkSessionCollection sessions;
            sessions = NetworkSession.Find(NetworkSessionType.SystemLink, 2, null);
            if (sessions.Count > 0)
            {
                session = NetworkSession.Join(sessions[0]);
                session.GameStarted += GameSessionStartedHandler;
                foreach (LocalNetworkGamer g in session.LocalGamers)
                    g.IsReady = true;
            }
            else
            {
                throw new Exception();
            }
        }

        public void CreateSessionHandler(object sender, EventArgs e)
        {
            session = NetworkSession.Create(NetworkSessionType.SystemLink, 2, 2);
            session.GameStarted += GameSessionStartedHandler;
            foreach (LocalNetworkGamer g in session.LocalGamers)
                g.IsReady = true;
        }


        public void EndGameHandler(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void subscribe(List<GlobalEvent> events)
        {
            foreach (GlobalEvent e in globalEvents)
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

        #region SessionEventHandlers
        
        public void GameSessionStartedHandler(object sender, EventArgs e)
        {
            if (session.IsHost)
                gameWorld = new MasterGameWorld();
            else
                gameWorld = new ClientGameWorld();

        }

        #endregion
    }
}
