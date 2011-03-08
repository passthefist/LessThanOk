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
 * Sprite_2D is a subclass of Sprite and is used to contain all the          *
 * information about a 2D Texture. All instances of Sprites should be        *
 * created with SpriteBin.                                                   *
 *                                                                           *   
 * See: Sprite.cs SpriteBin.cs                                               *                               
 *                                                                           *
\*---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

[assembly: InternalsVisibleTo("SpriteBin")] 

namespace LessThanOk.Sprites
{
    public class Sprite_2D : Sprite
    {

        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }

        internal Sprite_2D(Texture2D n_texture, Color n_color)
        {
            Texture = n_texture;
            Color = n_color;
        }

        public override Microsoft.Xna.Framework.Vector2 Size()
        {
            return new Vector2(Texture.Width, Texture.Height);
        }
    }
}
