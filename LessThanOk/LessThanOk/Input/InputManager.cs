using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LessThanOk.Input.Events;
using LessThanOk.Input.Events.Args;

namespace LessThanOk.Input
{
    class InputManager
    {
        private ButtonState leftClick;
        private ButtonState rightClick;
        private MouseState mouseLastState;
        private HashSet<Keys> keySet;

        public InputManager()
        {
            keySet = new HashSet<Keys>();
            leftClick = Mouse.GetState().LeftButton;
            rightClick = Mouse.GetState().RightButton;
            mouseLastState = Mouse.GetState();
        }

        public void update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();
            KeyboardState curKeyboardState = Keyboard.GetState();

            // mouse moved
            if (curMouseState.X != mouseLastState.X || curMouseState.Y != mouseLastState.Y)
                InputEvents.The.TriggerMouseMoved(this, new MouseEventArgs(curMouseState));

            // left click
            if (curMouseState.LeftButton.Equals(ButtonState.Pressed))
                leftClick = ButtonState.Pressed;
            else if (leftClick.Equals(ButtonState.Pressed))
            {
                // Left click detected
                leftClick = ButtonState.Released;
                InputEvents.The.TriggerLeftClick(this, new MouseEventArgs(curMouseState));
            }

            // right click
            if (curMouseState.RightButton.Equals(ButtonState.Pressed))
                rightClick = ButtonState.Pressed;
            else if (rightClick.Equals(ButtonState.Pressed))
            {
                // Right click detected
                rightClick = ButtonState.Released;
                InputEvents.The.TriggerRightClick(this, new MouseEventArgs(curMouseState));
            }

            // key press
            DetectKeyStroke(curKeyboardState);

            mouseLastState = curMouseState;

        }

        private void DetectKeyStroke(KeyboardState curKeyboardState)
        {
            HashSet<Keys> pressed = new HashSet<Keys>(curKeyboardState.GetPressedKeys());

            // Check for key up
            foreach (Keys k in keySet)
            {
                if (!pressed.Contains(k))
                {
                    InputEvents.The.TriggerKeyStroke(this, new KeyBoardEventArgs(k));
                    keySet.Remove(k);
                }
            }

            // Add down keys
            foreach(Keys k in pressed)
                keySet.Add(k);
            
        }
    }
}
