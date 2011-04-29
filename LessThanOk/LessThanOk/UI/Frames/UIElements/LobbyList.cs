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
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;
using LessThanOk.Network;
using LessThanOk.Sprites;

namespace LessThanOk.UI.Frames.UIElements
{
    class LobbyList : Button
    {
        public LobbyList(String name, int x, int y, Sprite_Text image)
        {
            _posx = x;
            _posy = y;
            _image = image;
            _name = name;
        }

        public override void update(GameTime gameTime)
        {
            String text = "";
            Sprite_Text temp = (Sprite_Text)_image;
            /*
            foreach (Gamer g in session.AllGamers)
            {
                text += g.Gamertag;
                text += "\n";
            }
            temp.Text = text;
            _image = temp;
             */
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            _image.Draw(spriteBatch, _posx, _posy);
        }
    }
}
