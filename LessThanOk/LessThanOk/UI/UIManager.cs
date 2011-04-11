using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LessThanOk.UI.Events;

namespace LessThanOk.UI
{
    public sealed class UIManager
    {
        public Frame Root { get { return _root; } }
        public static RightClick RightClickEvent;
        public static LeftClick LeftClickEvent;

        private static WindowDefinitions windows;
        private static Frame _root;
        private static UIEventListener listener;

        static readonly UIManager the = new UIManager();
        static UIManager()
        {
            RightClickEvent = new RightClick();
            LeftClickEvent = new LeftClick();
            listener = new UIEventListener();
        }
        public static UIManager The { get { return the; } }


        public void init(ContentManager Content)
        {
            windows = new WindowDefinitions(Content);
            _root = windows.Frames["home"];
        }
        public void switchFrame(String frame)
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
