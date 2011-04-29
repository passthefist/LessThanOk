/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
*                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                                License                                    *
 *                                                                           *
 * This library is free software; you can redistribute it and/or modify it   *
 * under the terms of the MIT Liscense.                                      *
 *                                                                           *
 * This library is distributed in the hope that it will be useful, but       *
 * WITHOUT ANY WARRANTY; without even the implied warranty of                *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                           *
 * You should have received a copy of the MIT Liscense with this library, if *
 * not, visit http://www.opensource.org/licenses/mit-license.php.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                            Class Overview                                 *
 *                                                                           *
 * InputManager should be called durring every update.  It grabs raw input   *
 * from the mouse and keyboard and triggers events.                          *
 *                                                                           *
\*---------------------------------------------------------------------------*/
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
        /// <summary>
        /// Method for grabbing a slice of raw input and triggering an event.
        /// </summary>
        /// <param name="gameTime">Current game time.</param>
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
        /// <summary>
        /// Method for detecting keystrokes.
        /// </summary>
        /// <param name="curKeyboardState">Current Keyboard State.</param>
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
