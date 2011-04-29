/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                  *
*                                                                            *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono           *
*                                                                            *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                     *
*                                                                            *
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
 * GameState is the state for when the game is being played.                 *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI;
using LessThanOk.Network.Commands;
using LessThanOk.Selecter;
using LessThanOk.GameData.GameWorld.GameSim;
using LessThanOk.GameData.GameWorld;
using System.IO;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld.MoniratorSpace;
using LessThanOk.Network;

namespace LessThanOk.States
{
    class GameState: State
    {
        public Frame_Game GameFrame { get { return _frame; } }
        private Frame_Game _frame;

        GameWorldController GameController;
        /// <summary>
        /// Constructor for GameState
        /// </summary>
        /// <param name="frame">Frame for hooking up User Iterface Events.</param>
        public GameState()
        {
            GameController = new GameWorldController();
         }


        #region State Members

        public void Initialize(String XMLFile)
        {
            GameObjectFactory.The.loadXmlData(null);
            Monirator m = new Monirator();
            CommandRequester c = new CommandRequester();
            NetworkManager n = new NetworkManager();
            GameSimulator s = new MasterSimulator();

            GameController.Initialize(XMLFile, false, _frame, m,s,n,c);
            GameController.connectAsInputSource(c);
            GameController.connectAsInputSource(n);
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            _frame = WindowDefinitions.BuildGameFrame(Content);
        }

        public void Update(Microsoft.Xna.Framework.GameTime time, GamerCollection<LocalNetworkGamer> Gamers )
        {
            GameController.update(time, Gamers);
            _frame.update(time);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            _frame.draw(batch);
            GameController.Draw(batch);
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void UnInitialize()
        {
            GameObjectFactory.The.ClearFactory();
        }

        #endregion
    }
}
