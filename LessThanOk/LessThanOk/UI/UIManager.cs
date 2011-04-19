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
    public sealed class UIManager : GlobalEventSubscriber
    {
        public Frame Root { get { return _root; } }
        public static RightClick RightClickEvent;
        public static LeftClick LeftClickEvent;

        private static WindowDefinitions windows;
        private static Frame _root;

        static readonly UIManager the = new UIManager();
        static UIManager()
        {
            RightClickEvent = new RightClick();
            LeftClickEvent = new LeftClick();
        }
        public static UIManager The { get { return the; } }


        public void init(ContentManager Content)
        {
            windows = new WindowDefinitions(Content);
            _root = windows.Frames["home"];
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


        #region GlobalEventSubscriber Members

        public void subscribe(List<GlobalEvent> events)
        {
            foreach(GlobalEvent e in events)
            {
                switch (e.Name)
                {
                    case GlobalEvent.EVENTNAME.JOINGAME:
                        e.Handler += JoinSessionHandler;
                        break;
                    case GlobalEvent.EVENTNAME.CREATEGAME:
                        e.Handler += CreateSessionHandler;
                        break;
                    case GlobalEvent.EVENTNAME.STARTGAME:
                        e.Handler += StartGameHandler;
                        break;
                    case GlobalEvent.EVENTNAME.ENDGAME:
                        e.Handler += EndGameHandler;
                        break;
                    default:
                        break;
                }
            }
        }

        public void JoinSessionHandler(object sender, EventArgs e)
        {
            switchFrame("clientlobby");
        }

        public void CreateSessionHandler(object sender, EventArgs e)
        {
            switchFrame("hostlobby");
        }

        public void StartGameHandler(object sender, EventArgs e)
        {
            switchFrame("game");
        }

        public void EndGameHandler(object sender, EventArgs e)
        {
            switchFrame("postgame");
        }

        #endregion
    }
}
