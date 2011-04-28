using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LessThanOk.Input.Events;
using LessThanOk.Input.Events;

namespace LessThanOk.Input
{
    public sealed class InputManager
    {
        private static ButtonState leftClick;
        private static ButtonState rightClick;
        private static MouseState mouseLastState;
        private static HashSet<Keys> keySet;

        public static event EventHandler<MouseEventArgs> MouseMovedEvent;
        public static event EventHandler<MouseEventArgs> LeftMouseUpEvent;
        public static event EventHandler<MouseEventArgs> RightMouseUpEvent;
        public static event EventHandler<MouseEventArgs> LeftMouseDownEvent;
        public static event EventHandler<MouseEventArgs> RightMouseDownEvent;

        public static event EventHandler<KeyBoardEventArgs> KeyStrokeEvent;


        static readonly InputManager the = new InputManager();
        public static InputManager The { get { return the; } }
        
        static InputManager()
        {
            keySet = new HashSet<Keys>();
            leftClick = Mouse.GetState().LeftButton;
            rightClick = Mouse.GetState().RightButton;
            mouseLastState = Mouse.GetState();
        }
        public void init() { }
        public static void update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();
            KeyboardState curKeyboardState = Keyboard.GetState();

            // mouse moved
            if (curMouseState.X != mouseLastState.X || curMouseState.Y != mouseLastState.Y)
                if(MouseMovedEvent != null)
                    MouseMovedEvent.Invoke(InputManager.The, new MouseEventArgs(curMouseState));

            // left click
            if (curMouseState.LeftButton.Equals(ButtonState.Pressed))
                leftClick = ButtonState.Pressed;
            else if (leftClick.Equals(ButtonState.Pressed))
            {
                // Left click detected
                leftClick = ButtonState.Released;
                if (LeftMouseUpEvent != null)
                    LeftMouseUpEvent.Invoke(InputManager.The, new MouseEventArgs(curMouseState));
            }

            // right click
            if (curMouseState.RightButton.Equals(ButtonState.Pressed))
                rightClick = ButtonState.Pressed;
            else if (rightClick.Equals(ButtonState.Pressed))
            {
                // Right click detected
                rightClick = ButtonState.Released;
                if(RightMouseUpEvent != null)
                    RightMouseUpEvent.Invoke(InputManager.The, new MouseEventArgs(curMouseState));
            }

            // key press
            DetectKeyStroke(curKeyboardState);

            mouseLastState = curMouseState;

        }

        private static void DetectKeyStroke(KeyboardState curKeyboardState)
        {
            HashSet<Keys> pressed = new HashSet<Keys>(curKeyboardState.GetPressedKeys());

            // Check for key up
            foreach (Keys k in keySet)
            {
                if (!pressed.Contains(k))
                {
                    KeyStrokeEvent.Invoke(InputManager.The, new KeyBoardEventArgs(k));
                    keySet.Remove(k);
                }
            }

            // Add down keys
            foreach(Keys k in pressed)
                keySet.Add(k);
            
        }
    }
}
