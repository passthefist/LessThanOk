/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Anthony LoBono (ajlobono@gmail.com)                           *
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
 * This class is responsible for handling UI changes and handling user       *
 * input.                                                                    *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LessThanOk;
using LessThanOk.Sprites;
using LessThanOk.GameData.GameWorld;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.UI
{
    /// <summary>
    /// UIManager contains a root Frame.  This class is responsible
    /// for managing user interface changes as well as user input.
    /// It is ment for a rudimentury window system mainly used for 
    /// debugging.  It should be expanded upon in the future.
    /// </summary>
    class UIManager
    {

        private Frame f_root;
        private Frame f_home;
        private Frame f_lobby;
        private Frame f_game;
        private Frame f_postGame;
        private Frame_Game fg_game;
        private KeyboardState curKeyState;
        private MouseState curMouseState;
        private ButtonState leftClick;
        private List<Element> elementList;
        public delegate void selectedAction(Element caller);
        
        /// <summary>
        /// Constructor for UIManager. Sets up the home screen, lobby Screen,
        /// game screen, and post game screen.
        /// </summary>
        /// <param name="n_root">Root frame that contains all other frames.</param>
        /// <param name="font">Font used for the UI.  Should be a List.</param>
        public UIManager(Frame n_root, SpriteFont font, GameWorld gameWorld)
        {
            f_root = n_root;

            f_lobby = new Frame(Vector2.Zero, new Vector2(800, 600), null);
            Button b_ready = new Button(SpriteBin.The.AddTextSprite("Ready", "sprite1"), true,
                (Element sender) => { }, "ready");
            Button b_start = new Button(SpriteBin.The.AddTextSprite("Start Game", "sprite2"), true,
                loadGameMenu, "start");
            f_lobby.addElement(Vector2.Zero, b_ready);
            f_lobby.addElement(b_start.Size, b_start);
            f_lobby.visible = true;

            f_home = new Frame(Vector2.Zero, new Vector2(800, 600), null);
            Button b_createGame = new Button(SpriteBin.The.AddTextSprite("Create Game","Create Game"), true,
                loadLobbyMenu, "Create Game");
            Button b_joinGame = new Button(SpriteBin.The.AddTextSprite("Join Game", "Join Game"), true,
                loadLobbyMenu, "Join Game");
            f_home.addElement(Vector2.Zero, b_createGame);
            f_home.addElement(b_createGame.Size, b_joinGame);
            f_home.visible = true;

            f_game = new Frame(Vector2.Zero, new Vector2(800, 600), null);
            Button b_end = new Button(SpriteBin.The.AddTextSprite("End Game", "End Game"), true,
                loadPostGameMenu, "end");
            f_game.addElement(new Vector2(0, 510), b_end);
            f_game.visible = true;

            Button b_add = new Button(SpriteBin.The.AddTextSprite("Add", "Add"), 
                true, addUnit, "add");
            f_game.addElement(new Vector2(400, 510), b_add);


            fg_game = new Frame_Game(Vector2.Zero, new Vector2(800, 500), null, gameWorld );
            fg_game.visible = true;
            f_game.addFrame(fg_game, Vector2.Zero);

    

            f_postGame = new Frame(Vector2.Zero, new Vector2(800, 600), null);
            Button b_home = new Button(SpriteBin.The.AddTextSprite("Go Home", "Go Home"), true,
                loadHomeMenu, "Home");
            f_postGame.addElement(Vector2.Zero, b_home);
            f_postGame.visible = true;

            f_root.addFrame(f_home, Vector2.Zero);
       
        }
        /// <summary>
        /// Clears the root frame and adds itself.
        /// </summary>
        /// <param name="sender">UI Element that triggered the function.</param>
        private void loadPostGameMenu(Element sender)
        {
            f_root.clear();
            f_root.addFrame(f_postGame, Vector2.Zero);
        }
        /// <summary>
        /// Clears the root frame and adds itself.
        /// </summary>
        /// <param name="sender">UI Element that triggered the function.</param>
        private void loadGameMenu(Element sender)
        {
            f_root.clear();
            f_root.addFrame(f_game, Vector2.Zero);
        }
        /// <summary>
        /// Clears the root frame and adds itself.
        /// </summary>
        /// <param name="sender">UI Element that triggered the function.</param>
        private void loadLobbyMenu(Element sender)
        {
            f_root.clear();
            f_root.addFrame(f_lobby, Vector2.Zero);
            if (sender.name == "Create Game")
            {

            }
        }
        /// <summary>
        /// Clears the root frame and adds itself.
        /// </summary>
        /// <param name="sender">UI Element that triggered the function.</param>
        private void loadHomeMenu(Element sender)
        {
            f_root.clear();
            f_root.addFrame(f_home, Vector2.Zero);
        }
        private void addUnit(Element sender)
        {
            GameWorld temp = fg_game.getGameWorld();
            List<Command> cmdList = new List<Command>();
            cmdList.Add(new Command_Add(10, 0, 
                GameObjectFactory.The.getType("lolTest").ID, new TimeSpan()));
            temp.update(new TimeSpan(), cmdList);
        }
        /// <summary>
        /// Main update call for all of the user interface. This loop also handels
        /// user input.  
        /// </summary>
        /// <param name="gameTime">Current GameTime</param>
        public void update(GameTime gameTime)
        {
            curKeyState = Keyboard.GetState();
            curMouseState = Mouse.GetState();

            // lock leftClick to pressed.
            if(curMouseState.LeftButton.Equals(ButtonState.Pressed))
                leftClick = curMouseState.LeftButton;

            Keys[] keys = curKeyState.GetPressedKeys();
            Element curElement;

            //DEBUG
            //Console.WriteLine("X: " + prevMouseState.X + "Y: " + prevMouseState.Y);

            //Find the UI Element the mouse is currently over
            curElement = f_root.findElement(curMouseState);

            //If the mouse is over an Element set that element to hover state.
            ///TODO: Unhover logic.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if(curElement != null)
                if ((gameTime.TotalGameTime.Milliseconds % 1000) == 0)
                    curElement.hover();
            
            if(keys != null)
            {
                    
            }
            // Only true if leftClick is currenly set to pressed and
            // the current mouse state's left button is not pressed.
            if (leftClick.Equals(ButtonState.Pressed) &&
                curMouseState.LeftButton.Equals(ButtonState.Released))

            {
                // Left click has been released
                leftClick = ButtonState.Released;

                // If the mouse is over a Element select it.
                if(curElement != null)
                    curElement.select();
            }

        }
        /// <summary>
        /// Calls draw on the root frame which will draw all visible Elements
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch for drawing.</param>
        public void draw(SpriteBatch spriteBatch)
        {
            f_root.draw(spriteBatch);
        }
    }

}
