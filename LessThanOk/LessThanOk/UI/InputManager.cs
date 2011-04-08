using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace LessThanOk.UI
{
    public sealed class InputManager
    {
        private static ButtonState leftClick;
        private static ButtonState rightClick;
        private static KeyboardState keyboardLastState;

        static readonly InputManager the = new InputManager();
        static InputManager() 
        {
            leftClick = Mouse.GetState().LeftButton;
            rightClick = Mouse.GetState().RightButton;

            keyboardLastState = Keyboard.GetState();
        }
        public static InputManager The { get { return the; } }

        public void init()
        {

        }
        public void update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();
            KeyboardState curKeyboardState = Keyboard.GetState();
            Vector2 mousePos = new Vector2(curMouseState.X, curMouseState.Y);
            UIElement curElement = UIManager.The.Root.getElementAt(mousePos);
           
            if (curMouseState.LeftButton.Equals(ButtonState.Pressed))
                leftClick = ButtonState.Pressed;

            else if (leftClick.Equals(ButtonState.Pressed))
            {
                // Left click detected
                leftClick = ButtonState.Released;
                if (curElement != null)
                {
                    curElement.LeftClickEvent.click(curElement);
                }

                
            }
            if (curMouseState.RightButton.Equals(ButtonState.Pressed))
                rightClick = ButtonState.Pressed;

            else if (rightClick.Equals(ButtonState.Released))
            {
                // Right click detected
                rightClick = ButtonState.Released;
                if (curElement != null)
                    curElement.RightClickEvent.click(curElement);
            }


        }
    }
}
