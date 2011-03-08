using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LessThanOk;
using LessThanOk.Sprites;

namespace LessThanOk.UI
{
    class UIManager
    {

        private Frame f_root;
        private Frame f_home;
        private Frame f_lobby;
        private Frame f_game;
        private Frame f_postGame;
        private KeyboardState prevKeyState;
        private MouseState prevMouseState;
        private SpriteBin spriteMaker;

        public enum UI_STATE
        {
            HOME,
            LOBBY,
            GAME,
            POSTGAME
        }

        public UIManager(Frame n_root, SpriteFont font)
        {
            f_root = n_root;
            spriteMaker = new SpriteBin(font);
        }

        public void loadUI()
        {
            loadHomeMenu();
            loadLobyMenu();
            loadGameMenu();
            //loadPostGameMenu();

        }

        private void loadPostGameMenu()
        {
            throw new NotImplementedException();
        }

        private void loadGameMenu()
        {
          
        }

        private void loadLobyMenu()
        {
            throw new NotImplementedException();
        }

        private void loadHomeMenu()
        {
            f_home = new Frame(Vector2.Zero, new Vector2(800, 600), null);
            Button b_createGame = new Button(spriteMaker.AddTextSprite("Create Game"), true);
            Button b_joinGame = new Button(spriteMaker.AddTextSprite("Join Game"), true);
            f_home.addElement(Vector2.Zero, b_createGame);
            f_home.addElement(b_createGame.Size, b_joinGame);
        }

        public void update(GameTime gameTime, Game1.T_STATE state)
        {
            prevKeyState = Keyboard.GetState();
            prevMouseState = Mouse.GetState();
            Keys[] keys = prevKeyState.GetPressedKeys();
            Element curElement;

            //DEBUG
            if ((gameTime.TotalGameTime.Milliseconds % 1000) == 0)
                Console.WriteLine("X: " + prevMouseState.X + "Y: " + prevMouseState.Y);

            //Unhover logic;

            curElement = f_root.findElement(prevMouseState);

            if(curElement != null)
                if ((gameTime.TotalGameTime.Milliseconds % 1000) == 0)
                    curElement.hover();
            
            if(keys != null)
            {
                    
            }

            if (prevMouseState.LeftButton.Equals(ButtonState.Pressed) &&
                curElement != null)
            {
  
                    curElement.select();
            }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            f_root.draw(spriteBatch);
        }
        public void switchState(Game1.T_SESSION State)
        {

        }
    }

}
