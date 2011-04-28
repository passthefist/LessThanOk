using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;
using LessThanOk.UI.Frames.UIElements;
using LessThanOk.UI.Events;
using LessThanOk.UI.Frames;

namespace LessThanOk.UI
{
    public sealed class WindowDefinitions
    {
        static WindowDefinitions()
        {
            TOWERTYPE = new String[6];
            UNITTYPE = new String[3];

            TOWERTYPE[0] = "tower1";
            TOWERTYPE[1] = "tower2";
            TOWERTYPE[2] = "tower3";
            TOWERTYPE[3] = "tower4";
            TOWERTYPE[4] = "tower5";
            TOWERTYPE[5] = "tower6";

            UNITTYPE[0] = "unit1";
            UNITTYPE[0] = "unit2";
            UNITTYPE[0] = "unit3";
        }

        private readonly WindowDefinitions the = new WindowDefinitions();
        public WindowDefinitions The { get { return the; } }

        // Used for Demo.  Not enough time to impliment using xml.....
        private static String[] TOWERTYPE;
        private static String[] UNITTYPE;

        public static Frame_Home BuildHomeFrame(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame_Home fhome = new Frame_Home(800, 500);

            Sprite_Text create = SpriteBin.The.AddTextSprite(font, "Create Game", "create");
            Sprite_Text join = SpriteBin.The.AddTextSprite(font, "Join Game", "join");

            Button eJoin = new Button("join", join, 0, 0);
            Button eCreate = new Button("create", create, 400, 0);

            fhome.addElement(eCreate);
            fhome.addElement(eJoin);
            return fhome;
        }
        public static Frame_ClientLobby BuildClientLobbyFrame(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame_ClientLobby fclientlobby = new Frame_ClientLobby(800, 500);
            Sprite_Text ready = SpriteBin.The.AddTextSprite(font, "Ready", "ready");
            ToggleButton eReady = new ToggleButton("ready", ready, 400, 400, Color.White, Color.Green);

            fclientlobby.addElement(eReady);
            return fclientlobby;
        }
        public static Frame_HostLobby BuildHostLobbyFrame(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame_HostLobby fhostlobby = new Frame_HostLobby(800, 500);

            Sprite_Text start = SpriteBin.The.AddTextSprite(font, "Start Game", "start");
            Sprite_Text ready = SpriteBin.The.AddTextSprite(font, "Ready", "ready");

            Button eStart = new Button("start", start, 0, 400);
            ToggleButton eReady = new ToggleButton("ready", ready, 400, 400, Color.White, Color.Green);

            fhostlobby.addElement(eReady);
            fhostlobby.addElement(eStart);

            return fhostlobby;
        }
        public static Frame_Game BuildGameFrame(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame_Game fgame = new Frame_Game(800, 500);
            Sprite_Text add0 = SpriteBin.The.AddTextSprite(font, "Tower 1", "add0");
            Sprite_Text add1 = SpriteBin.The.AddTextSprite(font, "Tower 2", "add1");
            Sprite_Text add2 = SpriteBin.The.AddTextSprite(font, "Tower 3", "add2");
            Sprite_Text add3 = SpriteBin.The.AddTextSprite(font, "Tower 4", "add3");
            Sprite_Text add4 = SpriteBin.The.AddTextSprite(font, "Tower 5", "add4");
            Sprite_Text add5 = SpriteBin.The.AddTextSprite(font, "Tower 6", "add5");
            Sprite_Text add6 = SpriteBin.The.AddTextSprite(font, "Unit 1", "add6");
            Sprite_Text add7 = SpriteBin.The.AddTextSprite(font, "Unit 2", "add7");
            Sprite_Text add8 = SpriteBin.The.AddTextSprite(font, "Unit 3", "add8");


            Button eAdd0 = new AddButton("add", add0, 200, 400, 0xffff, TOWERTYPE[0]);
            Button eAdd1 = new AddButton("add", add1, 300, 400, 0xffff, TOWERTYPE[1]);
            Button eAdd2 = new AddButton("add", add2, 400, 400, 0xffff, TOWERTYPE[2]);
            Button eAdd3 = new AddButton("add", add3, 200, 450, 0xffff, TOWERTYPE[3]);
            Button eAdd4 = new AddButton("add", add4, 300, 450, 0xffff, TOWERTYPE[4]);
            Button eAdd5 = new AddButton("add", add5, 400, 450, 0xffff, TOWERTYPE[5]);

            Button eAdd6 = new AddButton("add", add6, 0, 400, 0, TOWERTYPE[0]);
            Button eAdd7 = new AddButton("add", add7, 100, 400, 0, TOWERTYPE[1]);
            Button eAdd8 = new AddButton("add", add8, 0, 450, 0, TOWERTYPE[2]);

            fgame.addElement(eAdd0);
            fgame.addElement(eAdd1);
            fgame.addElement(eAdd2);
            fgame.addElement(eAdd3);
            fgame.addElement(eAdd4);
            fgame.addElement(eAdd5);
            fgame.addElement(eAdd6);
            fgame.addElement(eAdd7);
            fgame.addElement(eAdd8);


            return fgame;
        }

        public static Frame BuildFrameFromXML(ContentManager Content, String FileName)
        {
            if(FileName == "Home.xml")
            {
                return initHome(Content);
            }
            else if(FileName == "LobbyList.xml")
            {
                return initLobbyList(Content);
            }
            else if(FileName == "ClientLobby.xml")
            {
                return initClientLobby(Content);
            }
            else if(FileName == "HostLobby.xml")
            {
                return initHostLobby(Content);
            }
            else if(FileName == "Game.xml")
            {
                return initGame(Content);
            }
            else if(FileName == "PostGame.xml")
            {
                return initPostGame(Content);
            }
            throw new Exception("Invalid XML File");
        }

        private static Frame initHome(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame fhome = new Frame(800, 500);
            
            Sprite_Text create = SpriteBin.The.AddTextSprite(font, "Create Game", "create");
            Sprite_Text join = SpriteBin.The.AddTextSprite(font, "Join Game", "join");

            Button eJoin = new Button("join", join, 0, 0);
            Button eCreate = new Button("create", create, 400, 0);

            fhome.addElement(eCreate);
            fhome.addElement(eJoin);
            return fhome;
        }

        private static Frame initLobbyList(ContentManager Content)
        {
            throw new NotImplementedException();
        }

        private static Frame initClientLobby(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame fclientlobby = new Frame(800, 500);
            Sprite_Text ready = SpriteBin.The.AddTextSprite(font, "Ready", "ready");
            ToggleButton eReady = new ToggleButton("ready", ready, 400, 400, Color.White, Color.Green);

            fclientlobby.addElement(eReady);
            return fclientlobby;
        }

        private static Frame initHostLobby(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame fhostlobby = new Frame(800, 500);

            Sprite_Text start = SpriteBin.The.AddTextSprite(font, "Start Game", "start");
            Sprite_Text ready = SpriteBin.The.AddTextSprite(font, "Ready", "ready");

            Button eStart = new Button("start", start, 0, 400);
            ToggleButton eReady = new ToggleButton("ready", ready, 400, 400, Color.White, Color.Green);
            
            fhostlobby.addElement(eReady);
            fhostlobby.addElement(eStart);

            return fhostlobby;
        }

        private static Frame initGame(ContentManager Content)
        {
            SpriteFont font = Content.Load<SpriteFont>("Kootenay");
            Frame fgame = new Frame_Game(800, 500);
            Sprite_Text add = SpriteBin.The.AddTextSprite(font, "Add", "add");
            Button eAdd = new Button("add", add, 400, 400);
            fgame.addElement(eAdd);
            return fgame;
        }

        private static Frame initPostGame(ContentManager Content)
        {
            Frame fpostgame = new Frame(800, 500);
            return fpostgame;
        }
    }
}
