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
 * LobbyState is the State for when the gamer is in the lobby.               *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI.Frames;
using LessThanOk.UI;

namespace LessThanOk.States
{
    class LobbyState : State
    {
        /// <summary>
        /// Constructor for LobbyState
        /// </summary>
        /// <param name="frame">Frame for hooking User Interface Events.</param>
        public LobbyState(Frame frame)
        {
            Frame_HostLobby LobbyFrame;
            if (frame is Frame_HostLobby)
                LobbyFrame = ((Frame_HostLobby)frame);
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
