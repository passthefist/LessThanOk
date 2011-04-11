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
using LessThanOk.GameData.GameObjects.Tiles;

namespace LessThanOk
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int SCREEN_WIDTH = 1000;
        const int SCREEN_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;

            Content.RootDirectory = "Content";
            base.Components.Add(new GamerServicesComponent(this));

            double error = 0;

            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);

            for (int i = 0; i < 5000; i++)
            {
                p1.X += LessThanMath.random(-10, 10);
                p1.Y += LessThanMath.random(-10, 10);
                p2.X += LessThanMath.random(-10, 10);
                p2.Y += LessThanMath.random(-10, 10);

                double f = Math.Sqrt((double)((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)));

                int d = LessThanMath.approxDist(p1, p2);

                error += (f - d) / f;
            }

            error /= 5000;

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

            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("Tile"), new Vector2(), "grassTile");
            SpriteBin.The.Add2DSprite(Content.Load<Texture2D>("tile2"), new Vector2(), "yellowTile");

            UIManager.The.init(Content);
            InputManager.The.init();
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
            if (NetworkManager.Session != null)
            {
                if (NetworkManager.Session.IsHost)
                {
                    NetworkManager.The.update(gameTime);
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
    }
}
