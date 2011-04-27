using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LessThanOk.UI.Events;
using LessThanOk.UI.Events.Args;
using LessThanOk.Input.Events;
using LessThanOk.Input.Events.Args;
using LessThanOk.States.Events;
using LessThanOk.States.Events.Args;
using LessThanOk.UI.Frames.UIElements;

namespace LessThanOk.UI
{
    public class MenuManager
    {
        private WindowDefinitions _windows;
        private Frame _root;

        public MenuManager(ContentManager Content)
        {
            // Subscribe to input events
            InputEvents.The.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(this.LeftClickHandler);
            InputEvents.The.RightMouseUpEvent += new EventHandler<MouseEventArgs>(this.RightClickHandler);
            InputEvents.The.MouseMoved += new EventHandler<MouseEventArgs>(this.MouseMovementHandler);

            // Subscribe to state change events
            StateChangeEvents.The.GameState += new EventHandler<GameStateEventArgs>(this.ChangeStateHandler);
            StateChangeEvents.The.HomeState += new EventHandler<HomeStateEventArgs>(this.ChangeStateHandler);
            StateChangeEvents.The.LobbyState += new EventHandler<LobbyStateEventArgs>(this.ChangeStateHandler);
            StateChangeEvents.The.PostGameState += new EventHandler<PostGameStateEventArgs>(this.ChangeStateHandler);


            _windows = new WindowDefinitions(Content);
            if (!_windows.Frames.TryGetValue(WindowDefinitions.FRAME.HOME, out _root))
                throw new Exception();
            
        }

        private void RightClickHandler(object sender, MouseEventArgs args)
        {
            Button element = _root.getElementAt(args.MouseState.X, args.MouseState.Y);
            if (element == null)
                return;
        }
        private void LeftClickHandler(object sender, MouseEventArgs args)
        {
            Button element = _root.getElementAt(args.MouseState.X, args.MouseState.Y);
            if (element == null)
                return;
            element.Click();
        }

        private void MouseMovementHandler(object sender, MouseEventArgs args)
        {
            //TODO: handle screen movement
        }

        public void ChangeStateHandler(object sender, EventArgs args)
        {
            if (args is HomeStateEventArgs)
            {
                switchFrame(WindowDefinitions.FRAME.HOME);
            }
            else if (args is LobbyStateEventArgs)
            {
                LobbyStateEventArgs a = (LobbyStateEventArgs)args;
                if (a.IsHost)
                    switchFrame(WindowDefinitions.FRAME.HOSTLOBBY);
                else
                    switchFrame(WindowDefinitions.FRAME.CLIENTLOBBY);
            }
            else if (args is GameStateEventArgs)
            {
                switchFrame(WindowDefinitions.FRAME.GAME);
            }
            else if (args is PostGameStateEventArgs)
            {
                switchFrame(WindowDefinitions.FRAME.POSTGAME);
            }
        }

        public bool AttachHandlerTo(WindowDefinitions.FRAME frame, WindowDefinitions.BUTTON element, EventHandler<ButtonEventArgs> handle)
        {
            return _windows.AttachHandlerTo(frame, element, handle);
        }

        private void switchFrame(WindowDefinitions.FRAME frame)
        {
            if (!_windows.Frames.TryGetValue(frame, out _root))
                throw new Exception();
        }
        public void draw(SpriteBatch spriteBatch)
        {
            _root.draw(spriteBatch);
        }
        public void update(GameTime gameTime)
        {
            _root.update(gameTime);
        }
    }
}
