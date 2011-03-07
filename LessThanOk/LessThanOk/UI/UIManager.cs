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

        private Frame root;
        private KeyboardState prevKeyState;
        private MouseState prevMouseState;
        private SpriteBin spriteMaker;

        public UIManager(Frame n_root, SpriteFont font)
        {
            root = n_root;
            spriteMaker = new SpriteBin(font);
        }

        public void loadUI()
        {
            //loadHomeMenu();
            //loadLobyMenu();
            loadGameMenu();
            //loadPostGameMenu();

        }

        private void loadPostGameMenu()
        {
            throw new NotImplementedException();
        }

        private void loadGameMenu()
        {
            Frame worldFrame = new Frame(Vector2.Zero, 800, 500, null, true);
            root.addFrame(worldFrame);
            Sprite_Text hello = spriteMaker.AddTextSprite("Hellow World");
            hello.Centered = true;
            Button helloButton = new Button(hello);
            helloButton.visible = true;
            worldFrame.addElement(Vector2.Zero, helloButton);
            hello.Position = helloButton.origin;
        }

        private void loadLobyMenu()
        {
            throw new NotImplementedException();
        }

        private void loadHomeMenu()
        {
            throw new NotImplementedException();
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

            curElement = root.findElement(prevMouseState);

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
            root.draw(spriteBatch);
        }
    }

}
