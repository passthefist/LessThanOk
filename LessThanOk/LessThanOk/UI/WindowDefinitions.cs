using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;
using LessThanOk.UI.Frames.UIElements;
using LessThanOk.UI.Events.Args;

namespace LessThanOk.UI
{
    public class WindowDefinitions
    {
        public enum FRAME
        {
            HOME,
            CLIENTLOBBY,
            HOSTLOBBY,
            GAME,
            POSTGAME
        }

        public enum BUTTON
        {
            CREATE_GAME,
            JOIN_GAME,
            READY,
            START_GAME,
            END_GAME,
            END_SESSION,
            ADD
        }

        public enum GAME_ELEMENT
        {
            ADD,
            CANCEL,
            MOVE,
            ATTACK
        }

        public Dictionary<FRAME, Frame> Frames { get { return _frames; } }

        private Dictionary<FRAME, Frame> _frames;

        public WindowDefinitions(ContentManager Content)
        {
            _frames = new Dictionary<FRAME, Frame>();
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

            Button eStart = new Button(BUTTON.START_GAME, start, 0, 400);
            Button eReady = new ToggleButton(BUTTON.READY, ready, 400, 400, Color.White, Color.Green);
            Button eJoin = new Button(BUTTON.JOIN_GAME, join, 0, 0);
            Button eCreate = new Button(BUTTON.CREATE_GAME, create, 400, 0);
            Button eEnd = new Button(BUTTON.JOIN_GAME, end, 0, 400);
            Button eAdd = new Button(BUTTON.ADD, add, 400, 400);

            //UIElement eHome = new UIElement(, home, 0, 0);
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

            //fpostgame.addElement(eHome);

            _frames.Add(FRAME.HOME, fhome);
            _frames.Add(FRAME.HOSTLOBBY, fhostlobby);
            _frames.Add(FRAME.CLIENTLOBBY, fclientlobby);
            _frames.Add(FRAME.GAME, fgame);
            _frames.Add(FRAME.POSTGAME, fpostgame);
        }

        public bool AttachHandlerTo(FRAME frame, BUTTON element, EventHandler<ButtonEventArgs> handle)
        {
            Frame outFrame;
            Button outElement;
            if (_frames.TryGetValue(frame, out outFrame))
            {
                if (outFrame.Elements.TryGetValue(element, out outElement))
                {
                    outElement.AddListener(handle);
                    return true;
                }
            }
            return false;
        }
    }
}
