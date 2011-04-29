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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace LessThanOk.States
{
    class ClientState : State
    {
        public Frame_Game GameFrame { get { return _frame; } }
        private Frame_Game _frame;
        /// <summary>
        /// Constructor for GameState
        /// </summary>
        /// <param name="frame">Frame for hooking up User Iterface Events.</param>
        public ClientState()
        {

        }


        #region State Members

        public void Initialize(String XMLFile, bool isHost)
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            _frame = WindowDefinitions.BuildGameFrame(Content);
        }

        public void Update(Microsoft.Xna.Framework.GameTime time, GamerCollection<LocalNetworkGamer> Gamers)
        {

        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void UnInitialize()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
