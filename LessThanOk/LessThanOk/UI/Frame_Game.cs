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
 * This is a subclass of the Frame class. It is responsible for drawing the  *
 * main game window.                                                         *
 *                                                                           *   
 * See: Frame.cs                                                             *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;

namespace LessThanOk.UI
{
    class Frame_Game: Frame
    {
        public Frame_Game(Vector2 n_origin, Vector2 n_size,
                        Sprite_2D n_background)
        {
            elements = null;
            frames = null;
            background = n_background;
            origin = n_origin;
            size = n_size;

        }
        public void addFrame(Frame element)
        {
            throw new NotImplementedException();
        }
        public void addElement(Vector2 n_origin, Element element)
        {
            throw new NotImplementedException();
        }
    }
}
