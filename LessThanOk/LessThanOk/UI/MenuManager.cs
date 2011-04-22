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

namespace LessThanOk.UI
{
    public class MenuManager
    {
        public Frame Root { get { return _root; } }

        private WindowDefinitions windows;
        private Frame _root;
        private UIElement hover;

        public MenuManager(ContentManager Content)
        {
            // Subscribe to input events
            InputEvents.The.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(this.LeftClickHandler);
            InputEvents.The.RightMouseUpEvent += new EventHandler<MouseEventArgs>(this.RightClickHandler);
            InputEvents.The.MouseMoved += new EventHandler<MouseEventArgs>(this.MouseMovementHandler);

            // Subscribe to state change events
            StateChangeEvents.The.GameState += new EventHandler<GameStateEventArgs>(this.ChangeState);
            StateChangeEvents.The.HomeState += new EventHandler<HomeStateEventArgs>(this.ChangeState);
            StateChangeEvents.The.LobbyState += new EventHandler<LobbyStateEventArgs>(this.ChangeState);
            StateChangeEvents.The.PostGameState += new EventHandler<PostGameStateEventArgs>(this.ChangeState);


            windows = new WindowDefinitions(Content);
            _root = windows.Frames["home"];
        }

        private void RightClickHandler(object sender, MouseEventArgs args)
        {
            UIElement element = _root.getElementAt(args.MouseState.X, args.MouseState.Y);
            if (element == null)
                return;
        }
        private void LeftClickHandler(object sender, MouseEventArgs args)
        {
            UIElement element = _root.getElementAt(args.MouseState.X, args.MouseState.Y);
            if (element == null)
                return;
            UIElementEvents.The.TriggerButtonPress(this, new ButtonEventArgs(element));
        }

        private void MouseMovementHandler(object sender, MouseEventArgs args)
        {
            UIElement element = _root.getElementAt(args.MouseState.X, args.MouseState.Y);
            setHover(element);

            //TODO: handle screen movement
        }

        public void ChangeState(object sender, EventArgs args)
        {
            if (args is HomeStateEventArgs)
            {
                switchFrame("home");
            }
            else if (args is LobbyStateEventArgs)
            {
                LobbyStateEventArgs a = (LobbyStateEventArgs)args;
                if (a.IsHost)
                    switchFrame("hostlobby");
                else
                    switchFrame("clientlobby");
            }
            else if (args is GameStateEventArgs)
            {
                switchFrame("game");
            }
            else if (args is PostGameStateEventArgs)
            {
                switchFrame("postgame");
            }
        }

        private void setHover(UIElement element)
        {
            if (hover == null && element == null)
                return;
            else if (hover == null)
            {
                hover = element;
                hover.Hover = true;
            }
            else if (element == null)
            {
                hover.Hover = false;
            }
            else if (element.Equals(hover))
            {
                hover.Hover = true;
            }
            else
            {
                hover = element;
                hover.Hover = true;
            }
        }

        private void switchFrame(String frame)
        {
            _root = windows.Frames[frame];
        }
        public void draw(SpriteBatch spriteBatch)
        {
            _root.draw(spriteBatch);
        }
        public void update(GameTime gameTime)
        {
            Root.update(gameTime);
        }
    }
}
