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

namespace LessThanOk.States
{
    class GameState: State
    {
        CommandRequester CMDRequester;
        ObjectSelector Selector;
        /// <summary>
        /// Constructor for GameState
        /// </summary>
        /// <param name="frame">Frame for hooking up User Iterface Events.</param>
        public GameState(Frame frame)
        {
            Frame_Game GameFrame;
            if (frame is Frame_Game)
                GameFrame = ((Frame_Game)frame);
            else
                throw new Exception("Wrong frame in GameState");
            GameFrame.AddUnitEvent += CMDRequester.AddButtonHandler;
        }


        #region State Members

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            throw new NotImplementedException();
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
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
