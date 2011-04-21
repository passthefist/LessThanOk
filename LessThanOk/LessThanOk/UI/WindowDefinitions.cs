using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;

namespace LessThanOk.UI
{
    public class WindowDefinitions
    {
        public Dictionary<String, Frame> Frames { get { return _frames; } }

        private Dictionary<String, Frame> _frames;

        public WindowDefinitions(ContentManager Content)
        {
            _frames = new Dictionary<String, Frame>();
            // TODO: Hardcode all frames for testing purposes.
            init(Content);
        }
        public void init(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame fhome = new Frame(800, 500);
            Frame fhostlobby = new Frame(800, 500);
            Frame fclientlobby = new Frame(800, 500);
            Frame fgame = new Frame_Game(800, 500);
            Frame fpostgame = new Frame(800, 500);

            Sprite_Text start = SpriteBin.The.AddTextSprite(font, "Start Game", "start");
            Sprite_Text join = SpriteBin.The.AddTextSprite(font, "Join Game", "join");
            Sprite_Text create = SpriteBin.The.AddTextSprite(font, "Create Game", "create");
            Sprite_Text ready = SpriteBin.The.AddTextSprite(font, "Ready", "ready");
            Sprite_Text end = SpriteBin.The.AddTextSprite(font, "End Game", "end");
            Sprite_Text home = SpriteBin.The.AddTextSprite(font, "Home", "home");
            Sprite_Text lobbyText = SpriteBin.The.AddTextSprite(font, "", "lobbyText");
            Sprite_Text add = SpriteBin.The.AddTextSprite(font, "Add", "add");

            UIElement eStart = new UIElement("start", start, 0, 400);
            UIElement eReady = new UIElement("ready", ready, 400, 400);
            UIElement eJoin = new UIElement("join", join, 0, 0);
            UIElement eCreate = new UIElement("create", create, 400, 0);
            UIElement eEnd = new UIElement("end", end, 0, 400);
            UIElement eHome = new UIElement("home", home, 0, 0);
            UIElement eAdd = new UIElement("add", add, 400, 400);
            //LobbyList eLobby = new LobbyList("lobbyList", new Vector2(0, 0), lobbyText);


            fhome.addElement(eCreate);
            fhome.addElement(eJoin);

            fhostlobby.addElement(eReady);
            fhostlobby.addElement(eStart);
            //fhostlobby.addElement(eLobby);

            fclientlobby.addElement(eReady);
            //fclientlobby.addElement(eLobby);

            fgame.addElement(eEnd);
            fgame.addElement(eAdd);

            fpostgame.addElement(eHome);

            _frames.Add("home", fhome);
            _frames.Add("hostlobby", fhostlobby);
            _frames.Add("clientlobby", fclientlobby);
            _frames.Add("game", fgame);
            _frames.Add("postgame", fpostgame);
        }
        
    }
}
